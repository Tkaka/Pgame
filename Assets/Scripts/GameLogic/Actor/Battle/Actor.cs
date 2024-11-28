using UnityEngine;
using Data.Beans;
using System.Text;
using System.Collections.Generic;

/// <summary>
/// 不能攻击的原因
/// </summary>
public enum AttackFailedReason
{
    No = 0,                // 可以攻击
    SystemError,           // 系统错误
    UnregisterSkill,       // 未注册的技能
    NoEnoughMP,            // MP不足
    NotCoolDown,           // 技能未冷却
    WrongAction,           // 当前动作不可打断
    Silence,               // 被沉默了
}

public abstract class Actor : AngelaBaby
{

    // 实体属性管理器
    public ActorPropertyManager PropertyMgr { protected set; get; }
    // 用于显示的属性管理器
    public ViewPropertyMgr ViewPropertyMgr { protected set; get; }

    // 技能管理器
    protected SkillManager mSkillManager;
    //伤害数字和特效位置分发器
    public PosDistributer posDistributer { get; protected set; }
    //连击位置
    public PosDistributer EftPosDistributer { get; protected set; }

    public BuffMgr BuffMgr { get; protected set; }

    /// <summary>
    /// 是否实际上死亡 (只是变现上还未扣完血)
    /// </summary>
    public bool IsActuallyDead = false;

    //public EffectManager EftMgr { private set; get; }

    public IHeadBar headBar;

    //阵容-格子ID
    public int GridId;
    //攻击者id
    public long attackerId;
    //初始位置
    public Vector3 OriginPos = Vector3.zero;
    //初始方向 
    public Vector3 OriginDir = Vector3.zero;

    /// <summary>
    /// 角色等级
    /// </summary>
    public int Level { protected set; get; }

    /// <summary>
    /// 是否被选中
    /// </summary>
    public bool IsSelected = false;

    public int NormalSkillId;

    public int SmallSkillId;

    public int MasterSkillId;

    //核心别动
    public int CoreSkillId;
    //核心被动
    public int CoreSkillLevel;

    /// <summary>
    /// 是否在下回合复活
    /// </summary>
    public bool WillReborn;

    public bool WillUseSmallSkill = false;

    /**********************************/

    public Actor(int profession, ActorType type, ActorCamp camp, long roleId)
        : base(profession, type, camp, roleId)
    {
        mVelocity = 4;
        PropertyMgr = new ActorPropertyManager(this);
        ViewPropertyMgr = new ViewPropertyMgr(this);
        BuffMgr = new BuffMgr(this);
        mSkillManager = new SkillManager();
        posDistributer = new PosDistributer();
        EftPosDistributer = new PosDistributer(PosDistributer.PositionType.StatusEffect);
    }

    public override bool initialize(ActorParam instanceData)
    {
        PropertyMgr.Initialize();
        ViewPropertyMgr.Initialize();
        bool flag = base.initialize(instanceData);
        GridId = instanceData.GridId;
        createHeadBar();
        registerSkills();
        //TriggerManager.Singleton.InitPassiveEffect(this);
        OriginPos = instanceData.Pos;
        OriginDir = instanceData.Dir;
        return flag;
    }

    protected override void loadPrefab(ActorParam instanceData)
    {

    }

    protected virtual void registerSkills()
    {

    }

    public bool IsCanMasterSkill()
    {
        if (isActorType(ActorType.Boss) || isActorType(ActorType.Pet))
        {
            if (getProperty(PropertyType.IsSilence) > 0)
            {
                return false;
            }
            //Logger.err(getProperty(PropertyType.Mp) + "__" + BattleParam.MaxMp);
            return getProperty(PropertyType.Mp) >= BattleParam.MaxMp;
        }
        return false;
    }

    public override void registerAllState()
    {
        registerState(ActorState.idle, new ActorIdleState());
        registerState(ActorState.dead, new ActorDeadState());
    }

    public override void move(Vector3 dir)
    {
        Vector3 speed = dir.normalized * mVelocity * Time.deltaTime;
        TransformExt.Translate(speed);
    }

    protected override Vector3 getSpeed()
    {
        return TransformExt.forward * mVelocity * Time.deltaTime;
    }

    /// <summary>
    /// 返回技能管理器
    /// </summary>
    /// <returns></returns>
    public SkillManager getSkillManager()
    {
        return mSkillManager;
    }


    public virtual float Attack(FightCmd cmd)
    {
        if (cmd == null)
        {
            Logger.err(Name + " 攻击指令为空");
            return 0;
        }
        Skill skill = mSkillManager.getSkill(cmd.skillId);
        if (skill != null)
        {
            // 1. 找到技能模板数据
            t_skillBean bean = skill.getSkillBean();
            if (bean == null)
            {
                Logger.err("Actor:attack can not get skillBean");
                return 0;
            }

            // 2. 只有绝技才检查怒气是否充足
            /*if (skill.IsMasterSkill())
            {
                LNumber curMp = getProperty(PropertyType.Mp);
                if (curMp < 1000)
                    return 0;
            }*/

            if (isCampOf(ActorCamp.CampFriend) && !skill.IsMasterSkill())
            {
                skill.showComboTip = cmd.ShowComboTip();   //手动自动都显示
                //手动 不是最后一个 且参数3为真 是才显示圈
                skill.showComboCircle = cmd.ShowComboCircle();
                skill.autoComboType = cmd.comboType;
            }
            else
            {
                skill.showComboTip = false;
                skill.showComboCircle = false;
            }
            skill.isMannul = cmd.isManual;
            skill.cmdId = cmd.cmdId;

            // 4. 直接使用技能 
            SkillStateParam skillParam = SkillStateParam.create(skill);
            if (!changeState(ActorState.attack, skillParam))
            {
                Logger.err(Name + "使用技能切换状态失败:" + cmd.skillId);
                return 0;
            }
            return skill.GetSkillTime();
        }
        else
        {
            Logger.err("Actor:Attack:找不到技能:" + cmd.skillId);
        }
        return 0;
    }

    /// <summary>
    /// 主要用于boss获取对应宠物id
    /// </summary>
    /// <returns></returns>
    public int GetFixTemplateId()
    {
        if (isActorType(ActorType.Boss))
        {
            t_monster_boosBean bossBean = ConfigBean.GetBean<t_monster_boosBean, int>(mTemplateId);
            if (bossBean != null)
                return bossBean.t_pet_id;
        }
        return mTemplateId;
    }

    public virtual bool isDead()
    {
        //return IsActuallyDead;
        return getProperty(PropertyType.Hp) <= 0;
    }

    public virtual void killMe()
    {
        changePropertyTo(PropertyType.Hp, 0);
        changeState(ActorState.dead);
    }

    public virtual void reBuildActor()
    {
        PropertyMgr.reBuildProperty();
    }

    public override void ToggleVisible(bool flag, bool onlyMain = true)
    {
        base.ToggleVisible(flag, onlyMain);
    }

    /******************属性操作相关***********************/
    /// <summary>
    /// 返回实体属性
    /// </summary>
    /// <param name="property"></param>
    /// <returns></returns>
    public LNumber getProperty(PropertyType property)
    {
        return PropertyMgr.getProperty(property);
    }

    public LNumber getBaseProperty(PropertyType property)
    {
        return PropertyMgr.getBaseProperty(property);
    }

    /// <summary>
    /// 注意：只用于初始化数据
    /// </summary>
    /// <param name="property"></param>
    /// <param name="val"></param>
    public void setBaseProperty(PropertyType property, LNumber val)
    {
        PropertyMgr.setBaseProperty(property, val);
    }

    /// <summary>
    /// 改变属性
    /// </summary>
    /// <param name="property"></param>
    /// <param name="val">增量值</param>
    public virtual void ChangeProperty(PropertyType property, LNumber val)
    {
        //if(property == PropertyType.Mp)
        //    Logger.err(Name + "逻辑加怒气" + val);
        PropertyMgr.changeProperty(property, val);
    }

    /// 修改某个属性到某一个值
    /// </summary>
    /// <param name="type"></param>
    /// <param name="val"></param>
    public void changePropertyTo(PropertyType type, LNumber val)
    {
        LNumber nowVal = getProperty(type);
        PropertyMgr.changeProperty(type, val - nowVal);
    }

    /// <summary>
    /// 用于非HP, MP直接设置最终值(如果buff修改)
    /// </summary>
    /// <param name="type"></param>
    /// <param name="val"></param>
    public void SetProperty(PropertyType type, LNumber val)
    {
        PropertyMgr.setProperty(type, val);
    }

    /******************属性操作相关***********************/

    public override void update()
    {
        base.update();
        if (headBar != null)
            headBar.Update();
    }

    /// <summary>
    /// 是否是某种种族
    /// </summary>
    /// <returns></returns>
    public abstract bool IsActorRace(ActorRace raceType);

    public override void destoryMe()
    {
        base.destoryMe();
        TriggerManager.Singleton.Clear(mId);
        BuffMgr.Clear();
    }


    public void PrintProperty(string str)
    {
        StringBuilder strb = new StringBuilder();
        strb.Append(str + mTemplateId);
        for (int i = (int)PropertyType.Atk; i < (int)PropertyType.MaxPropertyType; i++)
        {
            t_attr_nameBean bean = ConfigBean.GetBean<t_attr_nameBean, int>(i);
            if (bean != null)
            {
                strb.Append("|" + bean.t_name_id + ":" + getProperty((PropertyType)i));
            }
        }
        strb.Append("|伤害加成类型1" + ":" + getProperty(PropertyType.HurtAdd1));
        strb.Append("|伤害加成类型2" + ":" + getProperty(PropertyType.HurtAdd2));
        Logger.print(strb.ToString());
    }

}