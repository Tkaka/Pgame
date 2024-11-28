using UnityEngine;
using Data.Beans;
public class FightMoveState : FightBaseState
{

    public override void onEnter(object obj = null)
    {
        base.onEnter(obj);
        BattleCDCtrl.Singleton.Stop();
        GED.ED.addListener(EventID.PlayerMoveCmp, OnPlayerMoveCmp);
        StartMove();
    }

    string winId = "";
    private void StartMove()
    {
        //BattleWindow.Singleton.PetHeadBarTouchToggle(false);
        int nextIndex = SpawnerHelper.Singleton.RealWave+1;
        WaveTrigger trigger = SpawnerHelper.Singleton.GetTrigger(nextIndex);
        //创建主角
        PathParam pathParam;
        ActorPlayer player = ActorManager.Singleton.MainPlayer;
        VirtualCameraMgr.Singleton.ChangeToPlayerCamera(player.monoBehavior.headBar, player.monoBehavior.mainObj.transform);
        player.ToggleVisible(true);
        pathParam = SpawnerHelper.Singleton.GetPlayerPathParam(SpawnerHelper.Singleton.RealWave);
        Vector3? lastPos = pathParam.GetLastPos();
        if (lastPos.HasValue)
        {
            float dis = GTools.distanceIgnoreY(lastPos.Value, player.TransformExt.position);
            pathParam.dur = dis / player.Velocity;
        }
        player.changeState(ActorState.move, pathParam);

        float playerDur = pathParam.dur;
        if (trigger != null)
        {
            Actor actor = null;
            ActorTurnStatus status = null;
            for (int i = 0; i < fm.Grid.PlayerCamp.Length; i++)
            {
                status = fm.Grid.PlayerCamp[i];
                if (status.actorId <= 0)
                    continue;
                actor = ActorManager.Singleton.Get(status.actorId);
                if (actor != null && !actor.IsDestoryed)
                {
                    pathParam = SpawnerHelper.Singleton.GetPathParam(SpawnerHelper.Singleton.RealWave, actor.GridId);
                    pathParam.dur = playerDur;
                    actor.changeState(ActorState.move, pathParam);
                }
            }
        }

        if (SpawnerHelper.Singleton.NextIsBossLevel())
        {
            float dur = playerDur - 2.0f;
            if (dur < 0)
                dur = 0;
            CoroutineManager.Singleton.delayedCall(dur, () =>
            {
                winId = WinMgr.Singleton.Open<BossWarningWindow>();
            });
        }
    }

    private void OnPlayerMoveCmp(object obj)
    {
        Camera.main.cullingMask |= (1 << LayerMask.NameToLayer("Actor"));
        SpawnerHelper.Singleton.CurWave++;
        FightManager.Singleton.LastSeletedId = 0;
        BattleWindMgr.CurrentBtlWin.OnNewWave();
        BattleWindow.Singleton.ToogleVisible(true);
        VirtualCameraMgr.Singleton.ChangeCamera(SpawnerHelper.Singleton.RealWave);
        ActorManager.Singleton.MainPlayer.ToggleVisible(false);
        FightManager.Singleton.IsNewWave = true;
        CoroutineManager.Singleton.delayedCall(0.5f, () =>
        {
            SpawnerHelper.Singleton.SpawnMonster();
            fm.PrepareNextTurn(ChangeReason.MoveCmp);
        });
    }

    public override void onLeave(string stateKey)
    {
        base.onLeave(stateKey);
        WinMgr.Singleton.Close(winId, 0.5f);
        GED.ED.removeListener(EventID.PlayerMoveCmp, OnPlayerMoveCmp);
    }

    public override string getStateKey()
    {
        return FightState.MoveState;
    }

}
