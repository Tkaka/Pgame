public class GameBattleReplayState : GameBaseState
{
    public override void onEnter(object obj = null)
    {
        base.onEnter(obj);
        Logger.log("enter Battle Replay state");
        //BattleParam.Init();
        //重新设置游戏速度 
        float speed = GPlayerPrefs.GetFloat(GPlayerPrefs.BattleSpeedKey);
        if (speed > 2.0 || speed < 0.8)
            speed = 1;

        FightManager.Singleton.IsAuto = true;
        FightManager.Singleton.IsReplay = true;
        FightManager.Singleton.GameSpeed = speed;
        //BattleWindMgr.OpenBatlleWind<EmptyBattleWindow>();
        BattleWindMgr.OpenBatlleWind<BattleReplayWindow>();
        AudioManager.Singleton.PlayBGM("snd_bgm_battle_01");
        ReplayManager.Singleton.BeginReplay(ReplayService.Singleton.GetCurReplay());
    }



    public override void onUpdate()
    {
        //Debuger.Log("------------------------>>>>" + FightManager.Singleton.CurTurn + "            " + FightManager.Singleton.MaxTurnNum + "      " + FightManager.Singleton.TurnCount);
        ReplayManager.Singleton.Update();
    }

    public override void onLeave(string stateKey)
    {
        base.onLeave(stateKey);
        ReplayManager.Singleton.LeaveBattle();
    }

    public override string getStateKey()
    {
        return GameState.BattleReplay;
    }
}