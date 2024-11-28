using Data.Beans;
using System.Collections.Generic;


public enum CompareSymbol
{
    Greater = 1,         //大于
    GreaterOrEqual,    //大于等于
    Equal,            //等于
    Smaller,         //小于
    SmallerOrEqual,    //小于等于
}

//条件类型 1=在场类 2=魂数量类 3=目标生命 4=上场回合 5=x资质以上y数量  6=是否携带某buffid
//7=存活人数 8=战斗场景 
public enum ConditionType
{
    NoCondition = 0,
    IsPresent = 1,
    SoulCount = 2,
    TargetHp = 3,
    PresentTurn = 4,
    QualityCount = 5,
    ExistBuff = 6,
    AliveNum = 7,
    MissionType = 8,
}

public class ConditionJudger : SingletonTemplate<ConditionJudger>
{

    /// <summary>
    /// 条件是否满足
    /// </summary>
    /// <param name="templateId">条件配置id</param>
    /// <param name="self">攻击者</param>
    /// <param name="targetId">目标（当攻击时为技能目标，被动时为自己）</param>
    /// <returns></returns>
    public bool IsConditionMeet(int templateId, Actor self, long targetId = 0)
    {
        t_skill_conditionBean bean = ConfigBean.GetBean<t_skill_conditionBean, int>(templateId);
        if (bean == null || bean.t_type <= 0)
            return false;

        ConditionType conditionType = (ConditionType)bean.t_type;
        switch (conditionType)
        {
            case ConditionType.NoCondition:
                return true;
            case ConditionType.IsPresent:
                return IsPresent(templateId, self, targetId);
            case ConditionType.SoulCount:
                return SoulCount(templateId, self, targetId);
            case ConditionType.TargetHp:
                return TargetHp(templateId, targetId);
            case ConditionType.PresentTurn:
                return PresentTurn(templateId);
            case ConditionType.QualityCount:
                return QualityCount(templateId, self, targetId);
            case ConditionType.ExistBuff:
                return ExistBuffId(templateId, self, targetId);
            case ConditionType.MissionType:
                return MissionType(templateId);
            default:
                break;
        }

        return false;
    }

    /// <summary>
    /// 当前战斗类型
    /// </summary>
    /// <param name="templateId"></param>
    /// <returns></returns>
    public bool MissionType(int templateId)
    {
        t_skill_conditionBean bean = ConfigBean.GetBean<t_skill_conditionBean, int>(templateId);
        if (bean != null)
            return bean.t_int_param1 == (int)FightService.Singleton.FightType;
        return false;
    }

    /// <summary>
    /// 是否存在buffid 
    /// </summary>
    /// <param name=""></param>
    /// <param name=""></param>
    /// <param name=""></param>
    /// <returns></returns>
    public bool ExistBuffId(int templateId, Actor self, long targetId)
    {
        t_skill_conditionBean bean = ConfigBean.GetBean<t_skill_conditionBean, int>(templateId);
        if (bean == null || string.IsNullOrEmpty(bean.t_str_param))
            return false;

        //buffid
        int[] arr = GTools.splitStringToIntArray(bean.t_str_param);
        if (arr == null || arr.Length <= 0)
            return false;

        List<long> res = RangeSelector.Singleton.GetTargets(bean.t_range_id, self, targetId);
        if (res == null || res.Count <= 0)
            return false;

        
        //每个角色都必须要有这些buffid
        for (int j = 0; j < res.Count; j++)
        {
            Actor actor = ActorManager.Singleton.Get(res[j]);
            if (actor != null)
            {
                for (int i = 0; i < arr.Length; i++)
                {
                    //只要有一个找不到就返回false
                    if (!actor.BuffMgr.Exist(arr[i]))
                        return false;
                }
            }
        }

        return true;
    }


    /// <summary>
    /// 某些宠物/怪物是否在场
    /// </summary>
    /// <param name="templateId">技能条件id</param>
    /// <param name="self">攻击者</param>
    /// <param name="targetId">目标</param>
    /// <returns></returns>
    public bool IsPresent(int templateId, Actor self, long targetId)
    {
        t_skill_conditionBean bean = ConfigBean.GetBean<t_skill_conditionBean, int>(templateId);
        if (bean == null || string.IsNullOrEmpty(bean.t_str_param))
            return false;

        int[] arr = GTools.splitStringToIntArray(bean.t_str_param);
        if (arr == null || arr.Length <= 0)
            return false;

        List<long> res = RangeSelector.Singleton.GetTargets(bean.t_range_id, self, targetId);
        if (res == null || res.Count <= 0)
            return false;

        for (int i = 0; i < arr.Length; i++)
        {
            bool finded = false;
            for (int j = 0; j < res.Count; j++)
            {
                Actor actor = ActorManager.Singleton.Get(res[j]);
                if (actor != null && actor.getTemplateId() == arr[i])
                {
                    finded = true;
                }
            }
            //只要有一个找不到则条件不成立
            if (!finded)
                return false;
        }

        return true;
    }


    /// <summary>
    /// 魂类数量
    /// </summary>
    /// <param name="templateId">配置表id</param>
    /// <returns></returns>
    public bool SoulCount(int templateId, Actor self, long targetId)
    {
        t_skill_conditionBean bean = ConfigBean.GetBean<t_skill_conditionBean, int>(templateId);
        if (bean == null)
            return false;

        List<long> res = RangeSelector.Singleton.GetTargets(bean.t_range_id, self, targetId);
        if (res == null || res.Count <= 0)
            return false;

        Dictionary<SoulType, int> dic = new Dictionary<SoulType, int>();
        for (int i = 0; i < res.Count; i++)
        {
            Actor actor = ActorManager.Singleton.Get(res[i]);
            if (actor != null)
            {
                SoulType key = actor.SoulType;
                if (key == SoulType.None)
                    continue;
                if (!dic.ContainsKey(key))
                {
                    dic.Add(key, 1);
                }
                else
                {
                    dic[key] = dic[key] + 1;
                }
            }
        }
        CompareSymbol symbol = (CompareSymbol)bean.t_compare1;
        return AttackUtils.Compare(symbol, dic.Count, bean.t_int_param1);
    }

    public bool TargetHp(int templateId, long targetId)
    {
        Actor actor = ActorManager.Singleton.Get(targetId);
        if (actor == null)
            return false;
        t_skill_conditionBean bean = ConfigBean.GetBean<t_skill_conditionBean, int>(templateId);
        if (bean == null)
            return false;

        LNumber nowHp = actor.getProperty(PropertyType.Hp);
        LNumber baseHp = actor.getBaseProperty(PropertyType.Hp);
        LNumber rate = nowHp / baseHp * 10000;
        CompareSymbol symbol = (CompareSymbol)bean.t_compare1;

        return AttackUtils.Compare(symbol, (int)rate, bean.t_int_param1);
    }


    /// <summary>
    /// 上场回合数
    /// </summary>
    /// <param name="templateId"></param>
    /// <returns></returns>
    public bool PresentTurn(int templateId)
    {
        t_skill_conditionBean bean = ConfigBean.GetBean<t_skill_conditionBean, int>(templateId);
        if (bean == null)
            return false;
        return AttackUtils.Compare((CompareSymbol)bean.t_compare2, FightManager.Singleton.TurnCount, bean.t_int_param1);
    }


    /// <summary>
    /// 资质
    /// </summary>
    /// <param name="templateId"></param>
    /// <returns></returns>
    public bool QualityCount(int templateId, Actor self, long targetId)
    {
        t_skill_conditionBean bean = ConfigBean.GetBean<t_skill_conditionBean, int>(templateId);
        if (bean == null)
            return false;

        List<long> res = RangeSelector.Singleton.GetTargets(bean.t_range_id, self, targetId);
        if (res == null || res.Count <= 0)
            return false;

        CompareSymbol symbol = (CompareSymbol)bean.t_compare1;
        int quality = bean.t_int_param1;
        int count = 0;
        for (int i = 0; i < res.Count; i++)
        {
            Actor actor = ActorManager.Singleton.Get(res[i]);
            if (actor != null)
            {
                t_petBean petBean = ConfigBean.GetBean<t_petBean, int>(actor.getTemplateId());
                if (petBean != null)
                {
                    if (AttackUtils.Compare(symbol, petBean.t_zizhi, quality))
                    {
                        count++;
                    }
                }
            }
        }

        return AttackUtils.Compare((CompareSymbol)bean.t_compare2, count, bean.t_int_param2);
    }

}
