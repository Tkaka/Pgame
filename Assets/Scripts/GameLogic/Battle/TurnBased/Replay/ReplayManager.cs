using System.Text.RegularExpressions;
using Message.Replay;
using System;
using UnityEngine;

public class ReplayManager : SingletonTemplate<ReplayManager>
{
    public int thisFirstVal
    {
        get { return replayInfo.waveList[SpawnerHelper.Singleton.CurWave].firstVal; }//curWave.firstVal; }
    }

    public int EnemyFirstVal
    {
        get { return replayInfo.waveList[SpawnerHelper.Singleton.CurWave].enemyFirstVal; }// curWave.enemyFirstVal; }
    }

    public ActorCamp BattleWinner
    {
        get { return replayInfo.firstWin ? ActorCamp.CampFriend : ActorCamp.CampEnemy; }
    }

    private ReplayInfo replayInfo { get { return ReplayService.Singleton.GetCurReplay(); } }

    private int waveIdx;
    private WaveInfo curWave;

    private int turnIdx;
    private TurnInfo curTurn;
    private int curWaveCmdNum;        //当前波的指令数

    //开始战斗
	public void BeginReplay(ReplayInfo replay)
    {
        waveIdx = -1;
        //replayInfo = replay;
        SpawnerHelper.Singleton.Init(SpawnerManager.Singleton);
        for (int i=0; i<replay.waveList.Count; ++i)
            SpawnerHelper.Singleton.InitPets(i, replay.waveList[i].actorList);
        NextWave();

        FightManager.Singleton.ChangeState(FightState.PreState);
    }

    //当前战斗是否还有剩余回合
    public bool CurWaveHasTurnLeft()
    {
        return curTurn != null;
    }

    private int exuCmdNum = 0;
    //添加一个回合的指令
    public bool AddTrunCMD(CmdMgr mgr)
    {
        if(curTurn != null)
        {
            for (int i = 0; i < curTurn.turns.Count; ++i)
            {
                var msg = curTurn.turns[i];
                var cmd = new FightCmd();
                cmd.actorId = msg.actorId;

                Actor actor = ActorManager.Singleton.Get(cmd.actorId);
                if (actor.getCamp() != FightManager.Singleton.CurTurn)
                    continue;


               // Logger.log("use cmd " + cmd.actorId);
                cmd.skillId = msg.skillId;
                cmd.isMasterSkill = msg.isMasterSkill;
                cmd.comboType = (ComboType)msg.comboType;

                // 随机数占位 无其他作用
                if (!cmd.isMasterSkill)
                    FightAutoAttackState.GetRandomCombo();

                Skill skill = actor.getSkillManager().getSkill(cmd.skillId);
                skill.TargetID = msg.targetId;

                mgr.Enqueue(cmd);
                exuCmdNum++;
            }

             

            if (FightManager.Singleton.FirstTurn != FightManager.Singleton.CurTurn)
                turnIdx++;

            //Debuger.Log("---------------------->>已播放轮数" + turnIdx + "        " + curWave.turnList.Count + "     " + FightManager.Singleton.TurnCount + "    " + FightManager.Singleton.MaxTurnNum);
            if (turnIdx < curWave.turnList.Count && FightManager.Singleton.FirstTurn != FightManager.Singleton.CurTurn)
            {
                curTurn = curWave.turnList[turnIdx];
            }

            if (exuCmdNum >= curWaveCmdNum)
            {
                curTurn = null;
                turnIdx = 0;
                exuCmdNum = 0;
            }

//            else if(turnIdx >= curWave.turnList.Count)
//            {
//                curTurn = null;
//                turnIdx = 0;
//            }

            return true;
        }

        return false;
    }


    //下一场战斗
    public bool NextWave()
    {
        waveIdx++;
        if(replayInfo.waveList.Count > waveIdx)
        {
            curWave = replayInfo.waveList[waveIdx];
            turnIdx = 0;
            curWaveCmdNum = 0;
            curTurn = curWave.turnList[turnIdx];
            for (int i = 0; i < curWave.turnList.Count; i++)
            {
                curWaveCmdNum += curWave.turnList[i].turns.Count;
            }

            return true;
        }
        return false;
    }

    public void GetPetInfo(int petId, ActorCamp actorCamp, out int star, out int color)
    {
        star = 0;
        color = 0;

        if (curWave != null)
        {
            foreach (var info in curWave.actorList)
            {
                if (petId == info.tmpId && info.camp == (int)actorCamp)
                {
                    star = info.star;
                    color = info.color;
                    break;
                }
            }
        }

    }

    public string GetEnemyName()
    {
        string name = "";
        if (replayInfo != null)
            name = replayInfo.enemyName;

        return name;
    }

    public string GetThisFirstName()
    {
        string name = "";
        if (replayInfo != null)
            name = replayInfo.firstName;

        return name;
    }


    public void Update()
    {
        ActorManager.Singleton.Update();
        HurtNumberMgr.Singleton.Update();
        CmdMgr.Singleton.Update();
        FightManager.Singleton.Update();
    }

    public void LeaveBattle()
    {
        SpawnerHelper.Singleton.Clear();
        ViewUtils.Singleton.Clear();
        CmdMgr.Singleton.Clear();
        ActorManager.Singleton.RemoveAll();
        FightManager.Singleton.Stop();
        BattleStatistics.Singleton.Clear();
        FightManager.R.ReleaseAllRes();
        HurtNumberMgr.Singleton.Clear();
    }
}