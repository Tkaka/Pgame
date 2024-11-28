//收集战斗指令类
using Message.Replay;
using System.Collections.Generic;

public class CmdCollectMgr
{
    protected static CmdCollectMgr mSingleton = null;
    private ReplayInfo m_curReplayInfo;

    public CmdCollectMgr()
    {
        m_curReplayInfo = new ReplayInfo();
    }

    public static CmdCollectMgr Singleton
    {
        get
        {
            if (mSingleton == null)
            {
                mSingleton = new CmdCollectMgr();
            }
            return mSingleton;
        }
    }

    public void PrepareCollect()
    {
        m_curReplayInfo.waveList.Clear();
    }

    public ReplayInfo GetReplayInfo()
    {
        return m_curReplayInfo;
    }

    public void InitActor(Actor actor)
    {
        if (FightManager.Singleton.IsReplay)
            return;

        if (actor == null)
            return;
        WaveInfo waveInfo = null;
        if (m_curReplayInfo.waveList.Count > SpawnerHelper.Singleton.CurWave)
        {
            waveInfo = m_curReplayInfo.waveList[SpawnerHelper.Singleton.CurWave];
        }
        else
        {
            waveInfo = new WaveInfo();
            waveInfo.firstVal = RoleService.Singleton.GetRoleInfo().precedeValue;
            waveInfo.enemyFirstVal = FightService.Singleton.GetWavePrecedeVal();
            m_curReplayInfo.waveList.Add(waveInfo);
        }

        _SetMsgActorInfo(actor, waveInfo);
    }



    public void SetCmd(FightCmd cmd)
    {
        if (FightManager.Singleton.IsReplay)
            return;

        Actor actor = ActorManager.Singleton.Get(cmd.actorId);
        if (actor == null)
        {
            Debuger.Log("收集指令异常");
            return;
        }

        WaveInfo waveInfo = null;
        if (m_curReplayInfo.waveList.Count > SpawnerHelper.Singleton.CurWave)
        {
            waveInfo = m_curReplayInfo.waveList[SpawnerHelper.Singleton.CurWave];
        }
        else
        {
            waveInfo = new WaveInfo();
            waveInfo.firstVal = RoleService.Singleton.GetRoleInfo().precedeValue;
            waveInfo.enemyFirstVal = FightService.Singleton.GetWavePrecedeVal();
            m_curReplayInfo.waveList.Add(waveInfo);
        }

        //初始实体数据
        _SetMsgActorInfo(actor, waveInfo);

        //添加指令
        TurnInfo turnInfo = null;
        if (waveInfo.turnList.Count >= FightManager.Singleton.TurnCount)
        {
            turnInfo = waveInfo.turnList[FightManager.Singleton.TurnCount - 1];
        }
        else
        {
            turnInfo = new TurnInfo();
            waveInfo.turnList.Add(turnInfo);
        }

        FightCMD fightCMD = new FightCMD();
        fightCMD.actorId = actor.getActorId();
        fightCMD.skillId = cmd.skillId;
        fightCMD.comboType = (int)cmd.comboType;
        fightCMD.isMasterSkill = cmd.isMasterSkill;
        fightCMD.targetId = actor.getSkillManager().getSkill(cmd.skillId).TargetID;
        turnInfo.turns.Add(fightCMD);

    }

    private void _SetMsgActorInfo(Actor actor, WaveInfo waveInfo)
    {

        bool isHaveMsgActor = false;
        for (int i = 0; i < waveInfo.actorList.Count; i++)
        {
            MsgActor msgActor = waveInfo.actorList[i];
            if (msgActor.actorId == actor.getActorId())
            {
                isHaveMsgActor = true;
                break;
            }
        }

        if (isHaveMsgActor == false)
        {
            MsgActor msgActor = new MsgActor();
            msgActor.actorId = actor.getActorId();
            msgActor.tmpId = actor.getTemplateId();
            msgActor.type = (int)actor.getActorType();
            msgActor.camp = (int)actor.getCamp();
            msgActor.gridId = actor.GridId;
            Dictionary<int, LNumber> propertyDic = actor.PropertyMgr.GetBasePropertyDic();
            foreach (var info in propertyDic)
            {
                msgActor.propertyTypes.Add(info.Key);
                msgActor.basePropertyVals.Add(info.Value.raw);
                long curValue = actor.PropertyMgr.getProperty((PropertyType)info.Key).raw;
                msgActor.propertyVals.Add(curValue);
            }

            waveInfo.actorList.Add(msgActor);
        }
    }
}

 