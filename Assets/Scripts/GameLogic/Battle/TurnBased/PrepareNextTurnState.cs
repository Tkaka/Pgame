
using Data.Beans;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// 下一回合的准备状态
/// </summary>
public class PrepareNextTurnState : FightBaseState
{

    private long coroId;

    public override void onEnter(object obj = null)
    {
        base.onEnter(obj);
        if (obj == null)
        {
            Logger.err("PrepareNextTurnState:参数为空");
            return;
        }
        ChangeReason reason = (ChangeReason)obj;
        //Logger.err("change to next turn " + reason.ToString());
        switch (reason)
        {
            //每一轮开始
            case ChangeReason.Start:
            case ChangeReason.MoveCmp:
                OnStart(reason);
                break;
            //直接切换到TurnStart状态
            case ChangeReason.BossBriefCmp:
            case ChangeReason.DialogCmp:
            case ChangeReason.RunAwayCmp:
                fm.ChangeState(FightState.TurnStart, reason);
                break;
            //流程结算
            case ChangeReason.AllAttack:
            case ChangeReason.AllRemove:
                coroId = CoroutineManager.Singleton.startCoroutine(TurnStart(reason));
                break;
        }
    }

    private void OnStart(ChangeReason reason)
    {
        if (_IsHaveRunaway()) return;

        //如果不存在对话
        if (!IsExistDialog())
        {
            //检查是否有boss需要介绍
            if (!HasBossBrife())
                fm.ChangeState(FightState.TurnStart, reason);
        }

    }

    //是否
    private bool _IsHaveRunaway()
    {
        if (FightService.Singleton.FightType != EFightType.HuanXiangDungeon)
            return false;

        List<Actor> actorList = SpawnerHelper.Singleton.GetSamePetInfo();
        if (actorList.Count > 0)
        {
            fm.ChangeState(FightState.RunAway);
            return true;
        }

        return false;
    }

    private IEnumerator TurnStart(ChangeReason reason)
    {
        //Logger.err("turnGapDelta is :-------------------->>> " + FightManager.turnGapDelta);
        yield return new WaitForSeconds(FightManager.turnGapDelta);
        FightManager.turnGapDelta = 0;

        /***************显示结算信息***********************/
        if (fm.PlayerTurn)
        {
            fm.CurTurnHurt = 0;
            //回收魂珠 
            if (fm.soulBallList.Count > 0)
            {
                foreach (SoulBall ball in fm.soulBallList)
                {
                    ball.StartFly();
                }
                fm.soulBallList.Clear();
                yield return new WaitForSeconds(FightManager.soulBallTime);
            }
        }
        /***************显示结算信息***********************/

        //检查当前wave有没有完毕
        if (!IsCurWaveCmp())
        {
            fm.ChangeState(FightState.TurnStart, reason);
        }
    }
    
    /// <summary>
    /// 是否有boss简介
    /// </summary>
    /// <returns></returns>
    private bool HasBossBrife()
    {
        if (SpawnerHelper.Singleton.IsBossLevel())
        {
            t_dungeon_actBean bean = ConfigBean.GetBean<t_dungeon_actBean, int>(BattleService.Singleton.MissionId);
            if (bean != null && bean.t_boss_id > 0)
            {
                fm.ChangeState(FightState.BossEnter);
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// 是否存在对话框
    /// </summary>
    /// <returns></returns>
    private bool IsExistDialog()
    {
        return false;
        t_dungeon_actBean bean = ConfigBean.GetBean<t_dungeon_actBean, int>(BattleService.Singleton.MissionId);
        string dialog = null;
        if (SpawnerHelper.Singleton.CurWave == 0)
            dialog = bean.t_wave_dialog1;
        else if (SpawnerHelper.Singleton.CurWave == 1)
            dialog = bean.t_wave_dialog2;
        else if (SpawnerHelper.Singleton.CurWave == 2)
            dialog = bean.t_wave_dialog3;

        if (!string.IsNullOrEmpty(dialog))
        {
            fm.ChangeState(FightState.DialogState);
            return true;
        }
        return false;
    }

    /// <summary>
    /// 判断当前轮是否结束
    /// </summary>
    private bool IsCurWaveCmp()
    {
        //重放时回合是否已完有剩余指令数决定
        if (FightManager.Singleton.IsReplay)
        {
            if (!ReplayManager.Singleton.CurWaveHasTurnLeft())
            {
                fm.ChangeState(FightState.ReplayChangeWaveState);
                return true;
            }
            return false;
        }

        if (fm.Grid.AliveNum(ActorCamp.CampEnemy) <= 0)
        {
            if (SpawnerHelper.Singleton.IsBossLevel())
            {
                fm.ChangeState(FightState.End, ActorCamp.CampFriend);
            }
            else
            {
                fm.ChangeState(FightState.MoveState);
            }
            return true;
        }

        if (fm.Grid.AliveNum(ActorCamp.CampFriend) <= 0)
        {
            fm.ChangeState(FightState.End, ActorCamp.CampEnemy);
            return true;
        }
        return false;
    }

    public override void onLeave(string stateKey)
    {
        base.onLeave(stateKey);
        CoroutineManager.Singleton.stopCoroutine(coroId);
    }

    public override string getStateKey()
    {
        return FightState.PrepareNextTurnState;
    }

}
