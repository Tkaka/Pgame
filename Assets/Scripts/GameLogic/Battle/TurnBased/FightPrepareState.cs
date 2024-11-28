
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;
using System.Linq;

public class FightPrepareState : FightBaseState
{


    public override void onEnter(object obj = null)
    {
        base.onEnter(obj);
        BattleWindMgr.CurrentBtlWin.ToogleVisible(false);
        Camera.main.cullingMask &= ~(1 << LayerMask.NameToLayer("Actor"));
        //释放精灵
        SpawnerHelper.Singleton.SpawnPlayer();
        ActorPlayer player = ActorManager.Singleton.MainPlayer;
        /*if (player != null && !fm.ballThrowed)
        {
            player.ThrowBall(OnThrowBallCmp);
            fm.ballThrowed = true;
        }
        else
        {
            OnThrowBallCmp();
        }*/
        OnThrowBallCmp();
    }

    private void OnThrowBallCmp()
    {
        BattleWindMgr.CurrentBtlWin.ToogleVisible(true);
        Camera.main.cullingMask |= (1 << LayerMask.NameToLayer("Actor"));
        //SpawnerHelper.Singleton.SpawnPlayer();
        SpawnerHelper.Singleton.SpawnMonster();
        ActorManager.Singleton.MainPlayer.ToggleVisible(false);
        SpawnerHelper.Singleton.SpawnPets();
        CoroutineManager.Singleton.delayedCall(1.0f, () =>
        {
            OnCreatePetsFinish();
        });
    }

    private void OnCreatePetsFinish()
    {
        BattleWindMgr.CurrentBtlWin.InitHpView();
        fm.Start();
    }

    public override string getStateKey()
    {
        return FightState.PreState;
    }

}
