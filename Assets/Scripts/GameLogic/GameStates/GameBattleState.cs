using Data.Beans;

public class GameBattleState : GameBaseState
{

    public override void onEnter(object obj = null)
    {
        base.onEnter(obj);
        Logger.log("enter battle state");
        VirtualCameraMgr.Singleton.ChangeCamera(SpawnerHelper.Singleton.RealWave);
        //BattleParam.Init();
        //重新设置游戏速度 
        float speed = GPlayerPrefs.GetFloat(GPlayerPrefs.BattleSpeedKey);
        if (speed > 1.2 || speed < 0.8)
            speed = 1;
        FightManager.Singleton.IsReplay = false;
        FightManager.Singleton.GameSpeed = speed;
        //FightManager.Singleton.IsAuto = (GPlayerPrefs.GetInt(GPlayerPrefs.BattleAutoKey) == 1);
        BattleWindMgr.OpenBatlleWind<BattleWindow>();
        ComboCtrl.Singleton.SetSpeed();
        AudioManager.Singleton.PlayBGM("snd_bgm_battle_01");
        LoadPrefabs();
    }

    private void LoadPrefabs()
    {
        //初始化怪物
        BattleService.Singleton.SeparateGold();
        SpawnerHelper.Singleton.Init(SpawnerManager.Singleton);
        SpawnerHelper.Singleton.InitEnemys();
        SpawnerHelper.Singleton.InitPets();
        FightManager.Singleton.ChangeState(FightState.PreState);
    }

    public override void onUpdate()
    {
        ActorManager.Singleton.Update();
        HurtNumberMgr.Singleton.Update();
        CmdMgr.Singleton.Update();
        FightManager.Singleton.Update();
        //Logger.log(UnityEngine.Time.time + "");
    }

    public override void onLeave(string stateKey)
    {
        base.onLeave(stateKey);
        SpawnerHelper.Singleton.Clear();
        ViewUtils.Singleton.Clear();
        CmdMgr.Singleton.Clear();
        ActorManager.Singleton.RemoveAll();
        FightManager.Singleton.Stop();
        BattleStatistics.Singleton.Clear();
        FightManager.R.ReleaseAllRes();
        HurtNumberMgr.Singleton.Clear();
        DropItemMgr.Singleton.Clear();
    }

    public override string getStateKey()
    {
        return GameState.Battle;
    }
}