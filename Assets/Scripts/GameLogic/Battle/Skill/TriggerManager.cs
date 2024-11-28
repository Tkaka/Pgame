using Data.Beans;
using System.Collections.Generic;


public class TriggerParam
{
    /// <summary>
    /// 事件类型
    /// </summary>
    public TriggerEnum triggerType = TriggerEnum.NoCondition;

    //角色ID
    public long actorId;

    //伤害值
    public LNumber hurt;

    //阵营
    public ActorCamp camp = ActorCamp.CampFriend;

    public static TriggerParam Create(TriggerEnum triggerType, long actorId)
    {
        TriggerParam param = new TriggerParam();
        param.triggerType = triggerType;
        param.actorId = actorId;
        return param;
    }
}


public class TriggerManager : SingletonTemplate<TriggerManager>
{

    private Dictionary<TriggerEnum, List<PassiveEffect>> triggerDic = new Dictionary<TriggerEnum, List<PassiveEffect>>();

    public List<PassiveEffect> GetTriggerList(TriggerEnum type)
    {
        if (triggerDic.ContainsKey(type))
        {
            return triggerDic[type];
        }
        else
        {
            List<PassiveEffect> list = new List<PassiveEffect>();
            triggerDic.Add(type, list);
            return list;
        }
    }

    /// <summary>
    /// 初始化核心被动效果
    /// </summary>
    public void InitPassiveEffect(Actor actor, int skillId, int level)
    {
        if (actor.isActorType(ActorType.Pet) || actor.isActorType(ActorType.Boss))
        {
            t_skillBean skillBean = ConfigBean.GetBean<t_skillBean, int>(skillId);
            if (skillBean != null && !string.IsNullOrEmpty(skillBean.t_bd_effect_id) && skillBean.t_bd_effect_id != "0")
            {
                int[] effectIds = GTools.splitStringToIntArray(skillBean.t_bd_effect_id);
                for (int i = 0; i < effectIds.Length; i++)
                {
                    if (effectIds[i] <= 0)
                        continue;

                    EffectInfo info = new EffectInfo();
                    info.effectId = effectIds[i];
                    info.ownerId = actor.getActorId();
                    info.targetId = actor.getActorId();
                    //info.skill = null;    //核心被动不需要和技能绑定
                    info.level = level;

                    PassiveEffect eft = new PassiveEffect();
                    eft.Init(actor, info);
                    List<PassiveEffect> passiveEftList = GetTriggerList(eft.triggerType);
                    passiveEftList.Add(eft);
                }
            }
        }
    }

    /// <summary>
    /// 需要传入造成该事件的效果id
    /// </summary>
    /// <param name="triggerType"></param>
    /// <param name="actorId"></param>
    /// <param name="hurt"></param>
    public void OnEvtTriggered(TriggerParam triggerParam)
    {
        if (triggerDic.ContainsKey(triggerParam.triggerType))
        {
            Logger.log(triggerParam.triggerType + " 事件触发");
            List<PassiveEffect> list = triggerDic[triggerParam.triggerType];
            if (list != null && list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    //如果不是该阵营回合，直接跳过
                    if (list[i].Owner == null || !list[i].Owner.isCampOf(triggerParam.camp))
                        continue;
                    list[i].OnEvtTrigger(triggerParam);
                }
            }
        }
    }

    public void ClearAll()
    {
        if(triggerDic != null)
            triggerDic.Clear();
    }


    public void Clear(long actorId)
    {
        List<PassiveEffect> list = null;
        foreach (TriggerEnum key in triggerDic.Keys)
        {
            list = triggerDic[key];
            for (int i = list.Count-1; i >= 0; i--)
            {
                if (list[i].Owner.getActorId() == actorId)
                    list.RemoveAt(i);
            }
        }
    }

}
