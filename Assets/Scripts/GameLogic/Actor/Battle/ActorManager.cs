using System.Collections.Generic;
using System.Linq;
using System;

/// <summary>
/// 实体管理器
/// </summary>
public class ActorManager : SingletonTemplate<ActorManager>
{
    private Dictionary<long, Actor> mActors;
    public Dictionary<long, Actor> Actors
    {
        get { return mActors; }
    }

    private long mMainPlayerId;

    public ActorManager()
    {
        mActors = new Dictionary<long, Actor>();
    }

    /// <summary>
    /// 主玩家
    /// </summary>
    public ActorPlayer MainPlayer
    {
        get
        {
            return Get(mMainPlayerId) as ActorPlayer;
        }
    }

    /// <summary>
    /// 创建一个实体
    /// </summary>
    /// <param name="templateId"></param>
    /// <param name="type"></param>
    /// <param name="camp"></param>
    /// <param name="actorId"></param>
    /// <param name="instanceData"></param>
    /// <returns></returns>
    /// 
    //生成实例ID
    private static long _GenerateActorId(int templateId, ActorType type, ActorCamp camp, int gridId, long actorId = 0)
    {
        if (actorId != 0)
            return actorId;

        int typeId = type == ActorType.Player ? 1 : 0;
        return ((long)templateId) * 1000 + (int)camp * 100 + gridId * 10 + typeId;
    }

    public static Actor Create(int templateId, ActorType type, ActorCamp camp, ActorParam instanceData, long actorId = 0, bool addToManager = true)
    {
        Actor actor = null;
        actorId = _GenerateActorId(templateId, type, camp, instanceData.GridId, actorId);//(actorId == 0) ? IdAssginer.getId(IdAssginer.IdType.ActorId) : actorId;
        //Debuger.Log("生成实例ID" + actorId);
        switch (type)
        {
            case ActorType.Player:
                actor = new ActorPlayer(templateId, type, camp, actorId);
                break;
            case ActorType.Pet:
                actor = new ActorPet(templateId, type, camp, actorId);
                break;
            case ActorType.Monster:
                actor = new ActorMonster(templateId, type, camp, actorId);
                break;
            case ActorType.Boss:
                actor = new ActorBoss(templateId, type, camp, actorId);
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
            ActorManager.Singleton.Add(actor);
            if(type != ActorType.Player)
                FightManager.Singleton.Add(camp, actorId, actor.GridId);
        }

        return actor;
    }

    /// <summary>
    /// 添加一个实体到实体管理器
    /// </summary>
    /// <param name="actor"></param>
    public void Add(Actor actor)
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
        Actor actor = Get(actorId);
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
        KeyValuePair<long, Actor> kv;
        for (int a = mActors.Count - 1; a >= 0; --a)
        {
            kv = mActors.ElementAt(a);
            Remove(kv.Key);
        }
    }

    /// <summary>
    /// 索引一个实体
    /// </summary>
    /// <param name="actorId"></param>
    /// <returns></returns>
    public Actor Get(long actorId)
    {
        Actor actor = null;
        mActors.TryGetValue(actorId, out actor);
        return actor;
    }

    public ActorMonster GetBoss()
    {
        foreach (KeyValuePair<long, Actor> kv in mActors)
        {
            if (kv.Value.isActorType(ActorType.Boss))
            {
                return kv.Value as ActorMonster;
            }
        }
        return null;
    }

    /// <summary>
    /// 更新所有实体
    /// </summary>
    public void Update()
    {
        Actor actor = null;
        KeyValuePair<long, Actor> kv;
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

    public void KillActorByCamp(ActorCamp camp)
    {
        Actor actor;
        for (int a = mActors.Count - 1; a >= 0; --a)
        {
            KeyValuePair<long, Actor> kv = mActors.ElementAt(a);
            actor = kv.Value;
            if (actor.isCampOf(camp))
                actor.killMe();
        }
    }

    public void ToggleVisible(ActorCamp camp, bool flag)
    {
        Actor actor;
        for (int a = mActors.Count - 1; a >= 0; --a)
        {
            KeyValuePair<long, Actor> kv = mActors.ElementAt(a);
            actor = kv.Value;
            if (actor.isActorType(ActorType.Player))
                continue;
            if (actor.isCampOf(camp))
                actor.ToggleVisible(flag);
        }
    }

    /*public void DestroyAll()
    {
        for (int a = mActors.Count - 1; a >= 0; --a)
        {
            KeyValuePair<long, Actor> kv = mActors.ElementAt(a);
            Actor actor = kv.Value;
            actor.getStateMachine().changeState(ActorState.idle);
            actor.destoryMe();
        }
        Clear();
    }*/

    /// <summary>
    ///  清空ActorManager
    /// </summary>
    public void Clear()
    {
        mActors.Clear();
    }

}

