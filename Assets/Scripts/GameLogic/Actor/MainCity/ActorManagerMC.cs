using System;
using System.Collections.Generic;
using System.Linq;

public class ActorManagerMC : SingletonTemplate<ActorManagerMC>
{
    private Dictionary<long, ActorMC> mActors;

    private long mMainPlayerId;

    /// <summary>
    /// 主玩家
    /// </summary>
    public ActorPlayerMC MainPlayer
    {
        get
        {
            return Get(mMainPlayerId) as ActorPlayerMC;
        }
    }

    public ActorManagerMC()
    {
        mActors = new Dictionary<long, ActorMC>();
    }

    public static ActorMC Create(int templateId, ActorType type, ActorCamp camp, ActorParam instanceData, long actorId = 0, bool addToManager = true)
    {
        ActorMC actor = null;
        actorId = actorId == 0 ? IdAssginer.getId(IdAssginer.IdType.ActorId) : actorId;
        switch (type)
        {
            case ActorType.Player:
                actor = new ActorPlayerMC(templateId, type, camp, actorId);
                break;
            case ActorType.Monster:
            case ActorType.Pet:
                actor = new ActorMC(templateId, type, camp, actorId);
                break;
            default:
                Logger.err("未知的实体类型: " + type.ToString());
                break;
        }

        if (!actor.initialize(instanceData))
        {
            Logger.err("实体初始化失败!");
            return null;
        }

        if (addToManager)
        {
            Singleton.Add(actor);
        }

        return actor;
    }

    /// <summary>
    /// 索引一个实体
    /// </summary>
    /// <param name="actorId"></param>
    /// <returns></returns>
    public ActorMC Get(long actorId)
    {
        ActorMC actor = null;
        mActors.TryGetValue(actorId, out actor);
        return actor;
    }

    /// <summary>
    /// 添加一个实体到实体管理器
    /// </summary>
    /// <param name="actor"></param>
    public void Add(ActorMC actor)
    {
        if (actor == null)
        {
            Logger.err("待添加到实体管理器中的实体为null");
            return;
        }

        if (!mActors.ContainsKey(actor.getActorId()))
        {
            mActors.Add(actor.getActorId(), actor);
            if (actor.getActorType() == ActorType.Player)
            {
                mMainPlayerId = actor.getActorId();
            }
        }
        else
        {
            Logger.wrn("重复添加实体: " + actor.getActorId());
        }
    }

    public void Remove(long actorId)
    {
        /*ActorMC actor = Get(actorId);
        if (actor != null)
        {
            Remove(actor);
        }*/
        ActorMC actor = Get(actorId);
        if (actor != null)
        {
            actor.getStateMachine().changeState(ActorState.idle);
            actor.destoryMe();
            mActors.Remove(actor.getActorId());
        }
        else
        {
            Logger.wrn("待移除的实体不在实体管理器中：" + actorId);
        }
    }

    public void RemoveAll()
    {
        KeyValuePair<long, ActorMC> kv;
        for (int a = mActors.Count - 1; a >= 0; --a)
        {
            kv = mActors.ElementAt(a);
            Remove(kv.Key);
        }
    }

    /// <summary>
    /// 更新所有实体
    /// </summary>
    public void Update()
    {
        ActorMC actor;
        KeyValuePair<long, ActorMC> kv;
        for (int a = mActors.Count - 1; a >= 0; --a)
        {
            kv = mActors.ElementAt(a);
            if (kv.Value == null)
            {
                mActors.Remove(kv.Key);
            }
            else
            {
                actor = kv.Value;
                actor.update();
            }
        }
    }

    public void Clear()
    {
        mActors.Clear();
    }

}

