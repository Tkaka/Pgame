using Data.Beans;

/// <summary>
/// buff类型(101=属性buff 102=斩灭 103=致命 104=标记buff （免疫控制 debuff免疫 眩晕 麻痹) 105=转魂)
/// </summary>
public enum BuffType
{
    Unknown = -1,
    HotDotBuff = 100,                 //HP,MP相关的Buff
    ProperyBuff = 101,                //属性buff
    Behead = 102,                      //斩灭
    Mortal = 103,                        //致命
    MarkBuff = 104,                    //标记buff
    ChangeSoul = 105,                //转魂 
    FuHuo = 109,
    //ImmuneCtrl = 104,             //免疫控制       
    //ImmuneDebuff = 105,         //免疫debuff
    //DizzinessBuff = 106,            //眩晕
    //Numbness = 107,                //麻痹 
}

public enum BuffReplaceType
{
    Unknown = 0,                         //未知 
    Overlay = 1,                           //叠加
    ReplaceByPriority = 2,             //根据优先级替换
    Refresh = 3,                           //刷新
}

/// <summary>
/// Buff生效
/// 1.	生效周期，一般是附加状态的状态回合数
/// 2.	生效时机，是指的，技能结算前生效，技能结算后生效；
/// </summary>
public enum BuffImpactTime
{
    SelfTurn = 0,                        //自己回合开始时执行buff效果 
    CurSkill = 1,                         //此次技能
    Immediately = 2,                  //立即
}

/// <summary>
/// buff = 效果 + 时间 + 替换规则
/// 有附带条件的情况 ---> 当敌人生命低于45%时，伤害加成25%
/// </summary>
public class BaseBuff
{

    /// <summary>
    /// 配置表id
    /// </summary>
    public int TemplateId { protected set; get; }

    public TriggerEnum triggerType = TriggerEnum.NoCondition;

    /// <summary>
    /// buff表
    /// </summary>
    public t_buffBean BuffBean { protected set; get; }

    /// <summary>
    /// 技能效果表
    /// </summary>
    public t_skill_effectBean EffectBean { protected set; get; }

    /// <summary>
    /// 增益-减益
    /// </summary>
    public SUB_ADD SubOrAdd { protected set; get; }

    /// <summary>
    /// buff 功能类型
    /// </summary>
    public BuffType BuffType = BuffType.Unknown;

    /// <summary>
    /// 持续多少回合
    /// </summary>
    public int LeftTurn { protected set; get; }

    //buff生效时机
    public BuffImpactTime ImpactTime { protected set; get; }

    /// <summary>
    /// 是否表现
    /// </summary>
    public bool IsShow { protected set; get; }

    public Actor Owner { get; protected set; }

    protected bool IsEffect = false;          //是否生效过

    /// <summary>
    /// buffid 生效方式 剩余回合数 是否表现 参数1 参数2 参数3 
    /// </summary>
    /// <param name="param"></param>
    public virtual void ParseParam(int effectId, TwoParam<LNumber, LNumber> param)
    {
        EffectBean = ConfigBean.GetBean<t_skill_effectBean, int>(effectId);
        if (EffectBean != null)
            ParseParam(effectId, EffectBean.t_buff_id, param);
        else
            Logger.err("buff初始化失败");
    }

    public virtual void ParseParam(int effectId, int buffId, TwoParam<LNumber, LNumber> param)
    {
        EffectBean = ConfigBean.GetBean<t_skill_effectBean, int>(effectId);
        if (EffectBean != null)
        {
            TemplateId = buffId;
            if (System.Enum.IsDefined(typeof(BuffImpactTime), EffectBean.t_buff_effect_type))
                ImpactTime = (BuffImpactTime)EffectBean.t_buff_effect_type;
            else
                ImpactTime = BuffImpactTime.SelfTurn;
            LeftTurn = EffectBean.t_buff_turn;
            if (LeftTurn == -1)
                LeftTurn = int.MaxValue;
            if (EffectBean.t_is_show <= 0)
                IsShow = false;
            else
                IsShow = true;
            BuffBean = ConfigBean.GetBean<t_buffBean, int>(TemplateId);
            if (BuffBean != null)
            {
                if (System.Enum.IsDefined(typeof(SUB_ADD), BuffBean.t_type))
                    SubOrAdd = (SUB_ADD)BuffBean.t_type;
                else
                    Logger.err("无法识别的buff增减益类型" + BuffBean.t_type);
                if (System.Enum.IsDefined(typeof(BuffType), BuffBean.t_fun_type))
                    BuffType = (BuffType)BuffBean.t_fun_type;
                else
                    Logger.err("无法识别的buff功能类型" + BuffBean.t_fun_type);
            }
        }
    }

    public int GetReplaceMark()
    {
        if (BuffBean != null)
            return BuffBean.t_relace_mark;
        return -1;
    }

    public int GetReplaceType()
    {
        if (BuffBean != null)
            return BuffBean.t_replace_type;
        return 0;
    }

    public virtual void OnAdd(Actor owner)
    {
        Owner = owner;
        if (ImpactTime == BuffImpactTime.Immediately || 
            ImpactTime == BuffImpactTime.CurSkill)
        {
            Apply(false);
        }
        Show();
    }

    private void Show()
    {
        ShowAddBuff showBuff = new ShowAddBuff();
        showBuff.actorId = Owner.getActorId();
        showBuff.buffId = TemplateId;
        //区分回合开始添加，还是关键帧添加
        if (triggerType == TriggerEnum.OnTurnStart)
        {
            showBuff.timeNode = TimeNode.TurnStart;
            showBuff.Show();
        }
        else
        {
            showBuff.timeNode = TimeNode.Buffkeyframe;
            ViewUtils.Singleton.AddShow(showBuff);
        }
    }

    public virtual void OnRemove(bool fromTurnStart)
    {
        Logger.log(Owner.Name + Owner.getTemplateId() + " buff removed " + TemplateId);
        ShowRemoveBuff removeBuff = new ShowRemoveBuff();
        removeBuff.actorId = Owner.getActorId();
        removeBuff.buffId = TemplateId;
        if (fromTurnStart)
        {
            removeBuff.timeNode = TimeNode.TurnStart;
            removeBuff.Show();
        }
        else
        {
            removeBuff.timeNode = TimeNode.Buffkeyframe;
            ViewUtils.Singleton.AddShow(removeBuff);
        }
    }

    /// <summary>
    /// 按时附加效果
    /// </summary>
    public virtual void ApplyEffect(bool fromTurnStart)
    {

    }

    public virtual void Apply(bool fromTurnStart)
    {
        if (Owner != null)
        {
            if (LeftTurn > 0)
                ApplyEffect(fromTurnStart);
            LeftTurn--;
            //等到下一轮buff时间再移除
            if (LeftTurn < 0)
            {
                Owner.BuffMgr.Remove(this, true);
            }
        }
    }

    /// <summary>
    /// 当回合开始
    /// </summary>
    public virtual void OnTurnStart()
    {
        Apply(true);
    }

}
