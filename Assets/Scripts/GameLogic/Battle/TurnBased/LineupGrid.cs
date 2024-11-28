using Data.Beans;
using System.Collections.Generic;

public class ActorTurnStatus
{
    public long actorId;           //角色ID
    public int templateId;       //配置表ID
    public bool InCmdQueue;  //是否在命令行队列
    public bool MasterInCmdQueue;  //大技能是否在队列中 
    public bool attacked;        //此轮是否攻击过
    public bool IsActuallyDead;     //是否实际上死亡 (只是变现上还未扣完血)
    public int skillId;             //技能id（事先生成是普攻还是触发小技能）
    //离场 下回合回来
    //复活 自己阵营回合复活
    //变石像 第一条命打死之后 变成石像（可以被攻击） 如果没被打死第三回合复活

    public void Clear()
    {
        actorId = 0;
        templateId = 0;
        attacked = false;
        InCmdQueue = false;
        IsActuallyDead = false;
    }

    public bool CanAction(bool isMaster=false)
    {
        if (actorId <= 0 || attacked)
            return false;

        if (isMaster && MasterInCmdQueue)
            return false;

        if (!isMaster && InCmdQueue)
            return false;

        Actor actor = ActorManager.Singleton.Get(actorId);
        if (actor == null)
            return false;

        if (actor.getProperty(PropertyType.CanAttack) > 0)
            return false;

        //沉默只能释放小技能
        if (isMaster && !actor.IsCanMasterSkill())
            return false;

        //判断是否昏迷,麻痹,冰冻
        if (actor.getProperty(PropertyType.IsDizziness) > 0 ||
           actor.getProperty(PropertyType.IsNumbness) > 0 ||
            actor.getProperty(PropertyType.IsIce) > 0)
        {
            //Logger.log(" IsDizziness " + actor.getProperty(PropertyType.IsDizziness) +
            //                " IsNumbness " + actor.getProperty(PropertyType.IsNumbness) +
            //                " IsIce " + actor.getProperty(PropertyType.IsIce));
            return false;
        }

        return true;
    }

}

public class LineupGrid
{

    public ActorTurnStatus[] PlayerCamp { private set; get; }

    public ActorTurnStatus[] EnemyCamp { private set; get; }

    public LineupGrid()
    {
        PlayerCamp = new ActorTurnStatus[6];
        for (int i = 0; i < 6; i++)
        {
            PlayerCamp[i] = new ActorTurnStatus();
        }
        EnemyCamp = new ActorTurnStatus[6];
        for (int i = 0; i < 6; i++)
        {
            EnemyCamp[i] = new ActorTurnStatus();
        }
    }

    public void Clear()
    {
        if (PlayerCamp != null)
        {
            for (int i = 0; i < PlayerCamp.Length; i++)
            {
                PlayerCamp[i].Clear();
            }
        }
        if (EnemyCamp != null)
        {
            for (int i = 0; i < EnemyCamp.Length; i++)
            {
                EnemyCamp[i].Clear();
            }
        }
    }

    public void Add(ActorCamp camp, long actorId, int gridId)
    {
        if (camp == ActorCamp.CampFriend)
        {
            if (gridId < PlayerCamp.Length)
            {
                PlayerCamp[gridId].actorId = actorId;
                Actor actor = ActorManager.Singleton.Get(actorId);
                if (actor != null)
                {
                    PlayerCamp[gridId].templateId = actor.getTemplateId();
                }
            }
            else
            {
                Logger.err("LineupGrid:gridId:下标越界:" + gridId);
            }
        }
        else if (camp == ActorCamp.CampEnemy)
        {
            if (gridId < EnemyCamp.Length)
            {
                EnemyCamp[gridId].actorId = actorId;
                Actor actor = ActorManager.Singleton.Get(actorId);
                if (actor != null)
                {
                    EnemyCamp[gridId].templateId = actor.getTemplateId();
                }
            }
            else
            {
                Logger.err("LineupGrid:gridId:下标越界:" + gridId);
            }
        }
        else
        {
            Logger.err("LineupGrid:Add:无法识别的阵营:" + camp);
        }
    }

    public void Remove(ActorCamp camp, long actorId, int gridId)
    {
        if (camp == ActorCamp.CampFriend)
        {
            if (gridId < PlayerCamp.Length)
                PlayerCamp[gridId].Clear();
            else
                Logger.err("LineupGrid:Remove:下标越界:" + gridId);
        }
        else if (camp == ActorCamp.CampEnemy)
        {
            if (gridId < PlayerCamp.Length)
                EnemyCamp[gridId].Clear();
            else
                Logger.err("LineupGrid:Remove:下标越界:" + gridId);
        }
        else
        {
            Logger.err("FightManager:Remove:无法识别的阵营:" + camp);
        }
    }

    public void ResetStatus(ActorCamp camp, int gridId)
    {

    }

    public ActorTurnStatus[] GetCamp(ActorCamp camp)
    {
        if (camp == ActorCamp.CampFriend)
            return PlayerCamp;
        else
            return EnemyCamp;
    }

    /// <summary>
    /// 某一行是否阵亡
    /// </summary>
    /// <returns></returns>
    public int RowAliveNum(ActorCamp camp, int row)
    {
        int res = 0;
        ActorTurnStatus[] target = null;
        if (camp == ActorCamp.CampFriend)
            target = PlayerCamp;
        else
            target = EnemyCamp;
        if (target[0 + row * 3].actorId > 0 && !target[0 + row * 3].IsActuallyDead)
            res++;
        if (target[1 + row * 3].actorId > 0 && !target[1 + row * 3].IsActuallyDead)
            res++;
        if (target[2 + row * 3].actorId > 0 && !target[2 + row * 3].IsActuallyDead)
            res++;
        return res;
    }

    public bool IsRowDead(ActorCamp camp, int row)
    {
        return RowAliveNum(camp, row) <= 0;
    }

    public int AliveNum(ActorCamp camp, bool excludeActuallyDead = true)
    {
        ActorTurnStatus[] target = null;
        if (camp == ActorCamp.CampFriend)
            target = PlayerCamp;
        else
            target = EnemyCamp;
        int res = 0;
        for (int i = 0; i < target.Length; i++)
        {
            if (excludeActuallyDead)
            {
                if (target[i].actorId > 0 && !target[i].IsActuallyDead)
                    res++;
            }
            else
            {
                if (target[i].actorId > 0)
                    res++;
            }
        }
        return res;
    }

    public bool IsAllDead(ActorCamp camp)
    {
        return AliveNum(camp) <= 0;
    }


    //指定某个宠物是否存在且存活
    public bool IsRealAlive(ActorCamp camp, int templateId)
    {
        ActorTurnStatus[] target = GetCamp(camp);
        for (int i = 0; i < target.Length; i++)
        {
            if (target[i].templateId == templateId && !target[i].IsActuallyDead)
                return true;
        }
        return false;
    }

    /// <summary>
    /// 是否存在某个宠物/怪物
    /// </summary>
    /// <param name="camp"></param>
    /// <param name="templateId"></param>
    /// <returns></returns>
    public bool IsExistImpl(ActorCamp camp, int templateId)
    {
        ActorTurnStatus[] target = GetCamp(camp);
        for (int i = 0; i < target.Length; i++)
        {
            if (target[i].templateId == templateId)
                return true;
        }
        return false;
    }

    /// <summary>
    /// 是否存在某个宠物/怪物
    /// </summary>
    /// <param name="camp"></param>
    /// <param name="templateId"></param>
    /// <returns></returns>
    public bool IsExist(ActorCamp camp, int templateId)
    {
        if (templateId <= 0)
            return false;
        if (camp == ActorCamp.CampAll)
        {
            return IsExistImpl(ActorCamp.CampFriend, templateId) || IsExistImpl(ActorCamp.CampEnemy, templateId);
        }
        else
        {
            return IsExistImpl(camp, templateId);
        }
    }

    /// <summary>
    /// 判断某个角色在某行
    /// </summary>
    /// <param name="camp"></param>
    /// <param name="actorId"></param>
    /// <returns></returns>
    public int InWhichRow(ActorCamp camp, long actorId)
    {
        ActorTurnStatus[] target = GetCamp(camp);
        for (int i = 0; i < target.Length; i++)
        {
            if (target[i].actorId == actorId)
            {
                if(i < 3)
                    return GridEnum.Row0;
                else
                    return GridEnum.Row1;
            }
        }
        return -1;
    }

    /// <summary>
    /// 判断某个角色在某列
    /// </summary>
    /// <param name="camp"></param>
    /// <param name="actorId"></param>
    /// <returns></returns>
    public int InWhichCol(ActorCamp camp, long actorId)
    {
        ActorTurnStatus[] target = GetCamp(camp);
        for (int i = 0; i < target.Length; i++)
        {
            if (target[i].actorId == actorId)
            {
                if (i < 3)
                    return i;
                else
                    return i - 3;
            }
        }
        return 0;
    }

    public Dictionary<SoulType, int> CampSoulCount(ActorCamp camp)
    {
        Dictionary<SoulType, int> dic = new Dictionary<SoulType, int>();
        ActorTurnStatus[] target = GetCamp(camp);
        for (int i = 0; i < target.Length; i++)
        {
            if (target[i].templateId <= 0)
                continue;
            Actor actor = ActorManager.Singleton.Get(target[i].actorId);
            if (actor != null)
            {
                t_petBean petBean = null;
                if (actor.isActorType(ActorType.Pet))
                {
                    petBean = ConfigBean.GetBean<t_petBean, int>(target[i].templateId);
                }
                else if (actor.isActorType(ActorType.Boss))
                {
                    t_monster_boosBean bossBean = ConfigBean.GetBean<t_monster_boosBean, int>(target[i].templateId);
                    if (bossBean != null)
                        petBean = ConfigBean.GetBean<t_petBean, int>(bossBean.t_pet_id);
                }
                if (petBean != null)
                {
                    //t_pet_soulBean soulBean = ConfigBean.GetBean<t_pet_soulBean, int>(petBean.t_soul_detail_type);
                    SoulType key = UIUtils.GetSoulType(petBean.t_soul_detail_type);
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
        }
        return dic;
    }

    /// <summary>
    /// 返回阵营所拥有的魂类型和数量
    /// </summary>
    /// <param name="camp"></param>
    /// <returns></returns>
    public Dictionary<SoulType, int> SoulCount(ActorCamp camp)
    {
        if (camp == ActorCamp.CampAll)
        {
            Dictionary<SoulType, int> pCamp = CampSoulCount(ActorCamp.CampFriend);
            Dictionary<SoulType, int> eCamp = CampSoulCount(ActorCamp.CampEnemy);
            foreach (SoulType key in eCamp.Keys)
            {
                if (pCamp.ContainsKey(key))
                {
                    pCamp[key] = pCamp[key] + eCamp[key];
                }
                else
                {
                    pCamp.Add(key, eCamp[key]);
                }
            }
            return pCamp;
        }
        else
        {
            return CampSoulCount(camp);
        }
    }

    public int CampQualityCount(ActorCamp camp, int quality, CompareSymbol symbol)
    {
        int res = 0;
        ActorTurnStatus[] target = GetCamp(camp);
        for (int i = 0; i < target.Length; i++)
        {
            if (target[i].templateId <= 0)
                continue;
            t_petBean petBean = ConfigBean.GetBean<t_petBean, int>(target[i].templateId);
            if (petBean != null)
            {
                if (AttackUtils.Compare(symbol, petBean.t_zizhi, quality))
                {
                    res++;
                }
            }
        }
        return res;
    }

    /// <summary>
    /// 阵营在CompareSymbol条件下的数量  
    /// </summary>
    /// <param name="camp"></param>
    /// <param name="quality"></param>
    /// <param name="symbol"></param>
    /// <returns></returns>
    public int QualityCount(ActorCamp camp, int quality, CompareSymbol symbol)
    {
        if (camp == ActorCamp.CampAll)
        {
            return CampQualityCount(ActorCamp.CampFriend, quality, symbol) + CampQualityCount(ActorCamp.CampEnemy, quality, symbol);
        }
        else
        {
            return CampQualityCount(camp, quality, symbol);
        }
    }

    /// <summary>
    /// 获取阵营第一个角色
    /// </summary>
    /// <param name="camp"></param>
    /// <returns></returns>
    public long GetFirst(ActorCamp camp)
    {
        ActorTurnStatus[] target = GetCamp(camp);
        for (int i = 0; i < target.Length; i++)
        {
            if (target[i].actorId <= 0)
                continue;
            return target[i].actorId;
        }
        return 0;
    }

    public ActorTurnStatus Get(ActorCamp camp, int gridId)
    {
        ActorTurnStatus[] target = GetCamp(camp);
        if (gridId >= 0 && gridId < target.Length)
        {
            return target[gridId];
        }
        return null;
    }

    public ActorTurnStatus GetByTemplateId(ActorCamp camp, int templateId)
    {
        if (templateId <= 0)
            return null;
        ActorTurnStatus[] target = GetCamp(camp);
        for (int i = 0; i < target.Length; i++)
        {
            if (target[i].templateId == templateId)
                return target[i];
        }
        return null;
    }

}

