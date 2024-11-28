
using System.Collections.Generic;
using Data.Beans;
using System;


//范围条件类型 1=站位类 2=种族类  3=成长类型类
//4=首领类 5=基础属性类 6=当前属性类  7=指定宝贝 9=元素系 10=魂类
public enum RangeConditionType
{
    Race = 2,
    GrowType = 3,            //攻防技
    Leader = 4,                 //TODO
    BaseProperty = 5,
    CurProperty = 6,
    AppointTarget = 7,      //指定宝贝
    Element = 9,               //元素系  
    SoulType = 10,            //魂类
}

//站位类型 1=前排 2=后排 3=全体 4=自己所在行 5=自己所在列 
//6=目标所在行 7=目标所在列 8=自己 9=自己前排 10=自己后排
//11=自己以及身后 12=目标周围 98=技能主效果
public enum LineupType
{
    FrontRow = 1,
    BackRow = 2,
    All = 3,
    SelfRow = 4,
    SelfCol = 5,
    TargetRow = 6,
    TargetCol = 7,
    Self = 8,
    SelfFrontRow = 9,
    SelfBackRow = 10,
    SelfBack = 11,
    TargetAround = 12,
    SkillMainEffect = 98,
}

public class RangeSelector : SingletonTemplate<RangeSelector>
{

    private LineupGrid Grid;

    public RangeSelector()
    {
        Grid = FightManager.Singleton.Grid;
    }

    /// <summary>
    /// 获取选择列的后排角色
    /// </summary>
    /// <param name="selectedId"></param>
    /// <param name="lineupType"></param>
    /// <param name="self"></param>
    /// <returns></returns>
    public long GetSelectedBackRowTarget(long selectedId, LineupType lineupType, Actor self)
    {
        List<long> res = TargetCol(self.getCamp(), selectedId);
        if (res == null || res.Count <= 0)
            return 0;
        else if (res.Count >= 2)
        {
            return res[1];
        }
        else if (res.Count == 1)
        {
            return res[0];
        }
        return 0;
    }

    /// <summary>
    /// 获取技能参考目标
    /// </summary>
    /// <param name="lineupType"></param>
    /// <param name="self"></param>
    /// <returns></returns>
    public long GetReferenceTarget(LineupType lineupType, Actor self)
    {
        List<long> res = null;

        //目标操作阵营
        ActorCamp targetCamp = AttackUtils.GetEnemyCamp(self.getCamp());
        switch (lineupType)
        {
            case LineupType.FrontRow:
                res = FrontRow(targetCamp);
                break;
            case LineupType.BackRow:
                res = BackRow(targetCamp);
                break;
            default:
                Logger.err("RangeSelector:LineupRangeType:未实现的站位类型" + lineupType);
                break;
        }

        //没找到参考目标
        if (res == null || res.Count <= 0)
        {
            return 0;
        }

        //排除即将死亡的
        RemoveDead(res);

        //如果有多余则删除
        int count = 1;
        int num = res.Count;
        if (num > count)
        {
            RandomRemove(res, num - count);
        }

        return res[0];
    }

    public List<long> GetTargets(int templateId, Actor self, long targetActorId = 0, bool isMainEffect=false)
    {
        List<long> res = null;
        t_skill_scopeBean bean = ConfigBean.GetBean<t_skill_scopeBean, int>(templateId);
        if (bean == null)
        {
            return new List<long>();
        }

        //主效果目标优先选择目标对象
        if (isMainEffect && bean.t_count == 1 && targetActorId > 0)
        {
            return new List<long> { targetActorId };
        }

        //首先按站位进行选择
        res = LineupRange(bean, self, targetActorId);

        //如果存在筛选条件,按照条件筛选
        if (bean.t_condition_type > 0)
        {
            RangeConditionType conditionType = (RangeConditionType)bean.t_condition_type;
            switch (conditionType)
            {
                case RangeConditionType.Race:
                    RaceFilter(bean, res);
                    break;
                case RangeConditionType.GrowType:
                    GrowTypeFilter(bean, res);
                    break;
                case RangeConditionType.BaseProperty:
                    PropertyFilter(bean, res, true);
                    break;
                case RangeConditionType.CurProperty:
                    PropertyFilter(bean, res, false);
                    break;
                case RangeConditionType.Leader:
                    LeaderFilter(bean, res);
                    break;
                case RangeConditionType.AppointTarget:
                    AppointTargetFilter(bean, res);
                    break;
                case RangeConditionType.Element:
                    ElementFilter(bean, res);
                    break;
                case RangeConditionType.SoulType:
                    SoulTypeFilter(bean, res);
                    break;
                default:
                    Logger.err("RangeSelector:LineupRangeType:未实现的范围条件类型" + conditionType);
                    break;
            }
        }

        //移除多余的数量
        if (res != null && res.Count > 0)
        {
            int count = bean.t_count;
            int num = res.Count;
            if (num > count)
            {
                RandomRemove(res, num - count);
            }
        }

        return res;
    }

    /// <summary>
    /// 站位类型
    /// </summary>
    /// <returns></returns>
    public List<long> LineupRange(t_skill_scopeBean bean, Actor self, long targetActorId)
    {
        List<long> res = null;
        if (bean == null)
            return new List<long>();

        //目标操作阵营
        ActorCamp targetCamp = AttackUtils.GetCamp(bean.t_camp, self.getCamp());
        ActorTurnStatus[] target = Grid.GetCamp(targetCamp);

        //如果目标阵营和自身阵营相同则，targetId == 自身(注意这里是相对阵营)
        if (bean.t_camp == (int)ActorCamp.CampFriend)
            targetActorId = self.getActorId();

        LineupType lineupType = (LineupType)bean.t_range_type;
        switch (lineupType)
        {
            case LineupType.FrontRow:
                res = FrontRow(targetCamp);
                break;
            case LineupType.BackRow:
                res = BackRow(targetCamp);
                break;
            case LineupType.All:
                res = All(targetCamp, bean.t_count);
                break;
            case LineupType.SelfRow:
                res = TargetRow(targetCamp, self.getActorId());
                break;
            case LineupType.SelfCol:
                res = TargetCol(targetCamp, self.getActorId());
                break;
            case LineupType.TargetRow:
                res = TargetRow(targetCamp, targetActorId);
                break;
            case LineupType.TargetCol:
                res = TargetCol(targetCamp, targetActorId);
                break;
            case LineupType.SelfBack:
                res = SelfBack(targetCamp, targetActorId);
                break;
            case LineupType.TargetAround:
                res = TargetAround(targetCamp, targetActorId);
                break;
            case LineupType.Self:
                res = new List<long>();
                res.Add(self.getActorId());
                break;
            default:
                Logger.err(bean.t_id + " RangeSelector:LineupRangeType:未实现的站位类型:" + lineupType);
                break;
        }


        if (res != null)
        {
            //一定要先排除目标(目标是相对的，被动时目标就是自己)
            if (bean.t_include_self == 1)
            {
                if(targetActorId > 0)
                    ExclueSelf(res, targetActorId);
            }
        }
        else
        {
            res = new List<long>();
        }

        return res;
    }

    /*********************范围过滤*****************************/

    /// <summary>
    /// 种族过滤器
    /// </summary>
    public void RaceFilter(t_skill_scopeBean bean, List<long> res)
    {
        if (res == null || res.Count <= 0)
            return;
        if (Enum.IsDefined(typeof(ActorRace), bean.t_int_param1))
        {
            ActorRace race = (ActorRace)bean.t_int_param1;
            for (int i = res.Count - 1; i >= 0; i--)
            {
                Actor actor = ActorManager.Singleton.Get(res[i]);
                if (!actor.IsActorRace(race))
                {
                    res.RemoveAt(i);
                }
            }
        }
        else
        {
            Logger.err("RangeSlector.RaceFilter.race参数错误" + bean.t_int_param1);
        }
    }

    /// <summary>
    /// 魂类型过滤器
    /// </summary>
    /// <param name="bean"></param>
    /// <param name="res"></param>
    public void SoulTypeFilter(t_skill_scopeBean bean, List<long> res)
    {
        if (res == null || res.Count <= 0)
            return;

        Dictionary<SoulType, bool> soulTypes = new Dictionary<SoulType, bool>();
        string soulStr = bean.t_str_param1;
        if (!string.IsNullOrEmpty(soulStr))
        {
            int[] arr = GTools.splitStringToIntArray(soulStr);
            if (arr != null && arr.Length > 0)
            {
                for (int i = 0; i < arr.Length; i++)
                {
                    t_pet_soulBean soulBean = ConfigBean.GetBean<t_pet_soulBean, int>(arr[i]);
                    if (soulBean != null)
                    {
                        SoulType temp = (SoulType)soulBean.t_type;
                        soulTypes.Add(temp, true);
                    }
                }
            }

            if (soulTypes.Count > 0)
            {
                for (int i = res.Count - 1; i >= 0; i--)
                {
                    Actor actor = ActorManager.Singleton.Get(res[i]);
                    if (actor != null && !soulTypes.ContainsKey(actor.SoulType))
                    {
                        res.RemoveAt(i);
                    }
                }
            }
        }
    }

    /// <summary>
    /// 成长类型过滤器
    /// </summary>
    /// <param name="bean"></param>
    /// <param name="res"></param>
    public void GrowTypeFilter(t_skill_scopeBean bean, List<long> res)
    {
        if (res == null || res.Count <= 0)
            return;
        if (Enum.IsDefined(typeof(PetType), bean.t_int_param1))
        {
            PetType growType = (PetType)bean.t_int_param1;
            for (int i = res.Count - 1; i >= 0; i--)
            {
                Actor actor = ActorManager.Singleton.Get(res[i]);
                if (!actor.IsGrowType(growType))
                {
                    res.RemoveAt(i);
                }
            }
        }
        else
        {
            Logger.err("RangeSlector.RaceFilter.GrowType参数错误" + bean.t_int_param1);
        }
    }


    /// <summary>
    /// 属性过滤器
    /// </summary>
    /// <param name="bean"></param>
    /// <param name="res"></param>
    public void PropertyFilter(t_skill_scopeBean bean, List<long> res, bool isBase)
    {
        if (res == null || res.Count <= 0)
            return;
        if (Enum.IsDefined(typeof(PropertyType), bean.t_int_param2))
        {
            LNumber highestVal = 0;
            LNumber lowestVal = LNumber.Max;
            long lastActorId = 0;
            PropertyType properType = (PropertyType)bean.t_int_param2;
            for (int i = res.Count - 1; i >= 0; i--)
            {
                Actor actor = ActorManager.Singleton.Get(res[i]);
                if (actor != null)
                {

                    LNumber val = 0;
                    if (isBase)
                        val = actor.getBaseProperty(properType);
                    else
                        val = actor.getProperty(properType);

                    //(属性id，1 = 最高 2 = 最低)
                    if (bean.t_int_param1 == 1)
                    {
                        if (val > highestVal)
                        {
                            lastActorId = actor.getActorId();
                        }
                    }
                    else
                    {
                        if (val < lowestVal)
                        {
                            lastActorId = actor.getActorId();
                        }
                    }
                }
            }
            //将list清空，放入挑选出来的属性最低或者最高者 
            res.Clear();
            if (lastActorId > 0)
                res.Add(lastActorId);
            else
                Logger.err("未找到属性最低或者最高" + lastActorId);
        }
        else
        {
            Logger.err("RangeSlector.RaceFilter.GrowType参数错误" + bean.t_int_param1);
        }
    }

    /// <summary>
    /// 首领过滤器（只有boss才有）
    /// </summary>
    /// <param name="bean"></param>
    /// <param name="res"></param>
    public void LeaderFilter(t_skill_scopeBean bean, List<long> res)
    {
        if (res == null || res.Count <= 0)
            return;
        for (int i = res.Count - 1; i >= 0; i--)
        {
            Actor actor = ActorManager.Singleton.Get(res[i]);
            if (!actor.isActorType(ActorType.Boss))
            {
                res.RemoveAt(i);
            }
        }
    }

    /// <summary>
    /// 元素/性别过滤器
    /// </summary>
    /// <param name="bean"></param>
    /// <param name="res"></param>
    public void ElementFilter(t_skill_scopeBean bean, List<long> res)
    {
        if (res == null || res.Count <= 0)
            return;
        for (int i = res.Count - 1; i >= 0; i--)
        {
            Actor actor = ActorManager.Singleton.Get(res[i]);
            if ((int)actor.Sex != bean.t_int_param1)
            {
                res.RemoveAt(i);
            }
        }
    }

    /// <summary>
    /// 指定宝贝过滤器
    /// </summary>
    /// <param name="bean"></param>
    /// <param name="res"></param>
    public void AppointTargetFilter(t_skill_scopeBean bean, List<long> res)
    {
        if (res == null || res.Count <= 0)
            return;
        int targetId = bean.t_int_param1;
        if (targetId <= 0)
            return;
        for (int i = res.Count - 1; i >= 0; i--)
        {
            Actor actor = ActorManager.Singleton.Get(res[i]);
            if (actor.isActorType(ActorType.Boss))
            {
                t_monster_boosBean bossBean = ConfigBean.GetBean<t_monster_boosBean, int>(actor.getTemplateId());
                if ((bossBean == null) || (bossBean != null && bossBean.t_pet_id != targetId))
                {
                    res.RemoveAt(i);
                }
            }
            else if (actor.isActorType(ActorType.Pet))
            {
                t_petBean petBean = ConfigBean.GetBean<t_petBean, int>(actor.getTemplateId());
                if ((petBean == null) || (petBean != null && petBean.t_id != targetId))
                {
                    res.RemoveAt(i);
                }
            }
        }
    }

    /*********************范围过滤*****************************/


    /*********************站位类型*****************************/
    /// <summary>
    /// 前排
    /// </summary>
    /// <param name="camp">调用者阵营</param>
    /// <returns></returns>
    public List<long> FrontRow(ActorCamp camp)
    {
        List<long> res = new List<long>();
        //如果前排没人，则后排变成前排
        int realRow = GridEnum.Row0;
        int num = Grid.RowAliveNum(camp, realRow);
        if (num <= 0)
        {
            realRow = GridEnum.Row1;
            num = Grid.RowAliveNum(camp, realRow);
            if (num <= 0)
            {
                return res;
            }
        }

        ActorTurnStatus[] target = Grid.GetCamp(camp);
        int start = realRow * 3;
        int end = start + 3;
        for (int i = start; i < end; i++)
        {
            if (target[i].actorId > 0)
            {
                res.Add(target[i].actorId);
            }
        }

        return res;
    }


    /// <summary>
    ///  后排
    /// </summary>
    /// <param name="camp"></param>
    /// <returns></returns>
    public List<long> BackRow(ActorCamp camp)
    {
        List<long> res = new List<long>();
        //如果后排没人，则前排变成后排
        int realRow = GridEnum.Row1;
        int num = Grid.RowAliveNum(camp, realRow);
        if (num <= 0)
        {
            realRow = GridEnum.Row0;
            num = Grid.RowAliveNum(camp, realRow);
            if (num <= 0)
            {
                return res;
            }
        }

        ActorTurnStatus[] target = Grid.GetCamp(camp);
        int start = realRow * 3;
        int end = start + 3;
        for (int i = start; i < end; i++)
        {
            if (target[i].actorId > 0)
            {
                res.Add(target[i].actorId);
            }
        }

        return res;
    }

    /// <summary>
    /// 某个阵营
    /// </summary>
    /// <param name="camp"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    public List<long> All(ActorCamp camp, int count)
    {
        List<long> res = new List<long>();
        ActorTurnStatus[] target = Grid.GetCamp(camp);
        for (int i = 0; i < target.Length; i++)
        {
            if (target[i].actorId > 0)
            {
                res.Add(target[i].actorId);
            }
        }

        return res;
    }

    /// <summary>
    /// 自身所在行
    /// </summary>
    /// <param name="camp"></param>
    /// <param name="count"></param>
    /// <param name="actorId"></param>
    /// <returns></returns>
    public List<long> TargetRow(ActorCamp camp, long actorId)
    {
        List<long> res = new List<long>();
        int row = Grid.InWhichRow(camp, actorId);
        if (row < 0)
            return res;

        ActorTurnStatus[] target = Grid.GetCamp(camp);
        for (int i = row * 3; i < target.Length; i++)
        {
            if (target[i].actorId > 0)
            {
                res.Add(target[i].actorId);
            }
        }

        return res;
    }

    /// <summary>
    /// 自己及身后
    /// </summary>
    /// <param name="camp"></param>
    /// <param name="count"></param>
    /// <param name="actorId"></param>
    /// <returns></returns>
    public List<long> SelfBack(ActorCamp camp, long actorId)
    {
        List<long> res = new List<long>();
        Actor actor = ActorManager.Singleton.Get(actorId);
        if (actor != null)
        {
            res.Add(actorId);
            //前排
            if (actor.GridId <= 2)
            {
                ActorTurnStatus[] target = Grid.GetCamp(camp);
                if(target[actor.GridId + 3].actorId > 0)
                    res.Add(target[actor.GridId + 3].actorId);
            }
        }
        return res;
    }

    /// <summary>
    /// 目标周围
    /// </summary>
    /// <param name="camp"></param>
    /// <param name="count"></param>
    /// <param name="actorId"></param>
    /// <returns></returns>
    public List<long> TargetAround(ActorCamp camp, long actorId)
    {
        List<long> res = new List<long>();
        Actor actor = ActorManager.Singleton.Get(actorId);
        if (actor != null)
        {
            res.Add(actorId);
            long targetId = 0;
            //2，3号位单独处理 
            if (actor.GridId != 3)
            {
                targetId = GetTarget(actor.GridId - 1, camp);
                if (targetId > 0)
                    res.Add(targetId);
            }
            if (actor.GridId != 2)
            {
                targetId = GetTarget(actor.GridId + 1, camp);
                if (targetId > 0)
                    res.Add(targetId);
            }
            targetId = GetTarget(actor.GridId - 3, camp);
            if (targetId > 0)
                res.Add(targetId);
            targetId = GetTarget(actor.GridId + 3, camp);
            if (targetId > 0)
                res.Add(targetId);
        }
        return res;
    }

    private long GetTarget(int gridId, ActorCamp camp)
    {
        if (gridId >= 0 && gridId <= 5)
        {
            ActorTurnStatus[] target = Grid.GetCamp(camp);
            if (target[gridId].actorId > 0)
            {
                return target[gridId].actorId;
            }
        }
        return 0;
    }

    /// <summary>
    /// 自身所在列
    /// </summary>
    /// <param name="camp"></param>
    /// <param name="count"></param>
    /// <param name="actorId"></param>
    /// <returns></returns>
    public List<long> TargetCol(ActorCamp camp, long actorId)
    {
        List<long> res = new List<long>();
        int col = Grid.InWhichCol(camp, actorId);
        if (col < 0)
            return res;

        if (col < 3)
        {
            ActorTurnStatus[] target = Grid.GetCamp(camp);
            if (target[col].actorId > 0)
                res.Add(target[col].actorId);
            if (target[col + 3].actorId > 0)
                res.Add(target[col+3].actorId);
        }
        
        return res;
    }


    /// <summary>
    /// 自身
    /// </summary>
    /// <param name="camp"></param>
    /// <param name="actorId"></param>
    /// <returns></returns>
    public List<long> Self(ActorCamp camp, long actorId)
    {
        List<long> res = new List<long>();
        ActorTurnStatus[] target = Grid.GetCamp(camp);
        for (int i = 0; i < target.Length; i++)
        {
            if (target[i].actorId == actorId)
            {
                res.Add(target[i].actorId);
                break;
            }
        }
        return res;
    }

    /*********************站位类型*****************************/

    /// <summary>
    /// 排除自身
    /// </summary>
    /// <param name="list"></param>
    /// <param name="selfActorId"></param>
    private void ExclueSelf(List<long> list, long selfActorId)
    {
        if (list != null)
        {
            list.Remove(selfActorId);
        }
    }

    /// <summary>
    /// 排除即将死亡的
    /// </summary>
    /// <param name="list"></param>
    private void RemoveDead(List<long> list)
    {
        for (int i = list.Count - 1; i >= 0; i--)
        {
            Actor actor = ActorManager.Singleton.Get(list[i]);
            if (actor != null && actor.IsActuallyDead)
            {
                list.RemoveAt(i);
            }
        }
    }

    /// <summary>
    /// 存活数量 > 所需数量，对res进行洗牌，然后返回最前面cout数的结果
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="num"></param>
    /// <param name="count"></param>
    private void RandomRemove<T>(List<T> list, int removeNum)
    {
        GTools.RandomList(list);
        for (int i = 0; i < removeNum; i++)
        {
            //挨个从最后一个移除
            list.RemoveAt(list.Count - 1);
        }
    }

    /// <summary>
    /// 获取某阵营随机单体
    /// </summary>
    /// <param name="camp"></param>
    /// <returns></returns>
    public long GetRandom(ActorCamp camp)
    {
        int num = Grid.AliveNum(camp);
        if (num < 0)
            return 0;
        ActorTurnStatus[] target = Grid.GetCamp(camp);
        int randId = UnityEngine.Random.Range(0, num);
        for (int i = 0; i < target.Length; i++)
        {
            if (target[i].actorId > 0 && i >= randId)
            {
                return target[i].actorId;
            }
        }
        return 0;
    }

    /// <summary>
    /// 敌方前排单体
    /// </summary>
    /// <param name="camp"></param>
    /// <returns></returns>
    public long EnemyFrontRowSingle(ActorCamp camp)
    {
        if (camp == ActorCamp.CampFriend)
            return FrontRowSingle(ActorCamp.CampEnemy);
        else
            return FrontRowSingle(ActorCamp.CampFriend);
    }

    /// <summary>
    /// 前排单体
    /// </summary>
    /// <returns></returns>
    public long FrontRowSingle(ActorCamp camp)
    {
        int realRow = GridEnum.Row0;
        int num = Grid.RowAliveNum(camp, realRow);
        if (num <= 0)
        {
            realRow = GridEnum.Row1;
            num = Grid.RowAliveNum(camp, realRow);
            if (num <= 0)
            {
                return 0;
            }
        }
        int randId = UnityEngine.Random.Range(0, num);
        ActorTurnStatus[] target = Grid.GetCamp(camp);
        for (int i = realRow * 3; i < target.Length; i++)
        {
            if (target[i].actorId > 0 && i >= randId)
            {
                return target[i].actorId;
            }
        }
        return 0;
    }






































    /// <summary>
    /// 我方阵营
    /// </summary>
    /// <returns></returns>
    public List<Actor> SelfCamp(ActorCamp camp)
    {
        ActorTurnStatus[] target = FightManager.Singleton.Grid.GetCamp(camp);
        return GetCamp(target);
    }

    /// <summary>
    /// 地方阵营
    /// </summary>
    /// <returns></returns>
    public List<Actor> EnemyCamp(ActorCamp camp)
    {
        ActorTurnStatus[] target = FightManager.Singleton.Grid.GetCamp(AttackUtils.GetEnemyCamp(camp));
        return GetCamp(target);
    }


    private List<Actor> GetCamp(ActorTurnStatus[] target)
    {
        List<Actor> res = new List<Actor>();
        if (target != null)
        {
            for (int i = 0; i < target.Length; i++)
            {
                if (target[i].actorId > 0)
                {
                    Actor actor = ActorManager.Singleton.Get(target[i].actorId);
                    if (actor != null && !actor.isDead())
                    {
                        res.Add(actor);
                    }
                }
            }
        }
        return res;
    }

}
