using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayChangeWaveState : FightBaseState
{
    public override void onEnter(object obj = null)
    {
        base.onEnter(obj);
        if(false == ReplayManager.Singleton.NextWave())
        {
            FightManager.Singleton.ChangeState(FightState.End, ReplayManager.Singleton.BattleWinner);
        }else
        {
            nextWaveBegin();
        }
    }

    private void nextWaveBegin()
    {
        //fm.ChangeState(FightState.MoveState);
        SpawnerHelper.Singleton.CurWave++;
        BattleWindMgr.CurrentBtlWin.OnNewWave();
        //ActorManager.Singleton.RemoveAll();
        SpawnerHelper.Singleton.SpawnPets();
        SpawnerHelper.Singleton.SpawnMonster();
        FightManager.Singleton.LastSeletedId = 0;
        FightManager.Singleton.IsNewWave = true;
        VirtualCameraMgr.Singleton.ChangeCamera(SpawnerHelper.Singleton.RealWave);
        FightManager.Singleton.ChangeState(FightState.PrepareNextTurnState, ChangeReason.AllAttack);
    }

    public override string getStateKey()
    {
        return FightState.ReplayChangeWaveState;
    }
}