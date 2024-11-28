using Data.Beans;


/// <summary>
/// 效果类型
/// 各个系统产生的效果
/// </summary>
public enum EffectType
{
    Skill = 1,          //技能
    Soul = 2,          //战魂
    Talent = 3,        //天赋
}

/// <summary>
/// 效果信息
/// </summary>
public class EffectInfo
{
    public int effectId;          //被动技能效果id 战魂效果id 天赋效果id (按目前的规则只有来自四号技能的效果id)
    public int level;              //等级
    public long ownerId;       //所有者id
    public long targetId;       //效果目标id
    public Skill skill;             //主动触发时有值（技能额外效果，被动触发效果为null）      
}

//受伤时
//自己暴击时
//自己被暴击时
//自己格挡时 //格挡还分 抵抗 和 抵挡两种额外的类型
//自己被格挡时
//血量下降到XXX以下
//再加一个：被附加负面状态时
//伊丽莎白的护盾破裂事件
//回合开始时
//类型(1=被暴击 2=被格挡 3=被造成伤害 4=出现暴击 5=出现格挡 6=死亡 7=被附加负面状态 9=回合开始前)
public enum TriggerEnum
{
    //触发条件
    NoCondition = 0,      //无条件
    OnBeiBaoJi = 1,
    OnBeiGeDang = 2,
    OnHurt = 3,
    OnBaoJi = 4,
    OnGeDang = 5,
    OnDead = 6,
    OnDebuff = 7,
    OnSheildBreak = 8,         //混沌破裂
    OnTurnStart = 9,            //回合开始时
    OnStage = 10,               //登场时
}

/// <summary>
/// 被动效果
/// 无敌之龙：提升己方前排反伤率，--- buff
/// 若己方场上仅一人则额外增加自身20%反伤率，   触发条件：
/// 若登场时或援护阵中存在罗伯特2002UM，则本次战斗中自己每回合恢复12%生命值；
/// 己方罗伯特2002UM阵亡时，继承其100%怒气驱散自身负面效果，并永久获得50%伤害率，
/// 若登场时己方场上同时存在特瑞2003、罗伯特2002UM，且不存在古利查力度，己方二代空手先生视作虎魂（不再视作蛇魂）  条件 -- 转魂
/// </summary>
public class PassiveEffect
{
    //1.生效时机   2.生效回合  3.最大生效次数

    private t_skill_effectBean eftBean;

    private t_skill_triggerBean triggerBean;

    public Actor Owner { protected set; get; }

    private EffectInfo info;

    public int SkillLevel { protected set; get; }

    private int TriggeredCount = 0;

    public TriggerEnum triggerType { protected set; get; }

    public void Init(Actor owner, EffectInfo info)
    {
        Owner = owner;
        this.info = info;
        eftBean = ConfigBean.GetBean<t_skill_effectBean, int>(info.effectId);
        if (eftBean != null)
        {
            triggerBean = ConfigBean.GetBean<t_skill_triggerBean, int>(eftBean.t_trigger_id);
            if (triggerBean != null)
            {
                if (System.Enum.IsDefined(typeof(TriggerEnum), triggerBean.t_type))
                    triggerType = (TriggerEnum)triggerBean.t_type;
                else
                    Logger.err("无法识别的trigger类型" + triggerBean.t_type);
            }
            else
            {
                Logger.err("被动技能效果id的触发条件为空:" + info.effectId);
            }
        }
    }

    /// <summary>
    ///己方罗伯特2002UM阵亡时，继承其100%怒气驱散自身负面效果，并永久获得50%伤害率
    /// </summary>
    /// <param name="evt"></param>
    public void OnEvtTrigger(TriggerParam triggerParam)
    {
        if (eftBean.t_effect_count != -1)
        {
            if (TriggeredCount >= eftBean.t_effect_count)
                return;
        }
        //触发类型 == 事件id才触发
        if (eftBean != null && triggerBean != null)
        {
            switch (triggerType)
            {
                case TriggerEnum.OnBaoJi:
                case TriggerEnum.OnBeiBaoJi:
                case TriggerEnum.OnGeDang:
                case TriggerEnum.OnBeiGeDang:
                case TriggerEnum.OnDebuff:
                    if (triggerParam.actorId == Owner.getActorId())
                    {
                        Apply(triggerType, triggerParam);
                    }
                    break;
                case TriggerEnum.OnHurt:
                    if (triggerParam.actorId == Owner.getActorId())
                    {
                        //Logger.log("伤害事件触发");
                        LNumber baseHp = Owner.getBaseProperty(PropertyType.Hp);
                        LNumber nowHp = Owner.getProperty(PropertyType.Hp) - triggerParam.hurt;
                        if ((nowHp / baseHp * 10000) < triggerBean.t_param)
                        {
                            Apply(triggerType, triggerParam);
                        }
                    }
                    break;
                case TriggerEnum.OnDead:
                    //Logger.log("死亡事件触发");
                    OnDead(triggerParam.actorId, triggerParam);
                    break;
                case TriggerEnum.OnTurnStart:
                    if (FightManager.Singleton.TurnCount >= eftBean.t_start_turn)
                    {
                        triggerParam.actorId = Owner.getActorId();
                        Apply(triggerType, triggerParam);
                    }
                    break;
            }
        }
    }


    protected void OnDead(long deaderId, TriggerParam triggerParam)
    {
        Actor deader = ActorManager.Singleton.Get(deaderId);
        if (deader != null && triggerBean != null)
        {
            //自己死亡的事件触发
            if (triggerBean.t_camp == (int)ActorCamp.Self)
            {
                if(deaderId == Owner.getActorId())
                    Apply(TriggerEnum.OnDead, triggerParam);
            }
            else
            {
                ActorCamp targetCamp = AttackUtils.GetCamp(triggerBean.t_camp, Owner.getCamp());
                //检查阵营是否匹配
                if (deader.getCamp() == targetCamp)
                {
                    //如果宠物id检查模板id是否匹配
                    if (triggerBean.t_pet_id > 0)
                    {
                        if(triggerBean.t_pet_id == deader.GetFixTemplateId())
                            Apply(TriggerEnum.OnDead, triggerParam);
                    }
                    else
                    {
                        Apply(TriggerEnum.OnDead, triggerParam);
                    }
                }
            }
        }
    }

    /*protected void OnDead(long deader, TriggerParam triggerParam)
    {
        Actor actor = ActorManager.Singleton.Get(deader);
        if (actor != null && triggerBean != null)
        {
            //如果t_pet_id > 0 表示其他人阵亡
            if (triggerBean.t_pet_id > 0 && triggerBean.t_pet_id == actor.getTemplateId())
            {
                Apply(TriggerEnum.OnDead, triggerParam);
            }
            //如果t_pet_id <= 0 表示自己阵亡
            else if (triggerBean.t_pet_id <= 0 && actor.getActorId() == Owner.getActorId())
            {
                Apply(TriggerEnum.OnDead, triggerParam);
            }
        }
    }*/

    /// <summary>
    /// 附加效果(效果生效)
    /// </summary>
    public bool Apply(TriggerEnum triggerType, TriggerParam triggerParam)
    {
        if (AttackUtils.ApplyEffect(info, triggerParam))
        {
            TriggeredCount++;
            return true;
        }
        return false;
    }

}
