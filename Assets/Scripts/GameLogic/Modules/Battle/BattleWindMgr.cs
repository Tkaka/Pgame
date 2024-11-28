public interface IBattleWindow
{
    void ToogleVisible(bool visible);

    void InitHpView();

    void ChangeTurn(ActorCamp camp);

    void ChangeWave(bool startOrEnd);

    void OnNewWave();


    void ChangeFightState(string state, object param=null);

    void OnSpawnerActor(Actor actor);

    void BattleEnd(ActorCamp winCamp);

    void OnAutoBtn();

    void PlayComboAni();

    void OnTrogglePetIcon(bool isShow);
}

public class BattleWindMgr
{
    public static IBattleWindow CurrentBtlWin { get; private set; }
    public static string OpenBatlleWind<T>(WinInfo info = null, UILayer layer = UILayer.HUD) where T : BaseWindow, new()
    {
        if (typeof(T) == typeof(EmptyBattleWindow))
        {
            WinMgr.AddPackage(WinEnum.UI_Battle);
            CurrentBtlWin = new EmptyBattleWindow();
            return "empty";
        }

        string name = WinMgr.Singleton.Open<T>(info, layer);
        CurrentBtlWin = WinMgr.Singleton.GetWindow<BaseWindow>(name) as IBattleWindow;
        return name;
    }
}

public class EmptyBattleWindow : BaseWindow, IBattleWindow
{
    public void BattleEnd(ActorCamp winCamp)
    {
        WinMgr.Singleton.CloseAll();
        SceneLoader.Singleton.nextState = GameState.MainCity;
        SceneLoader.Singleton.sceneName = GSceneName.MaiCity;
        GameManager.Singleton.changeState(GameState.Loading);
    }

    public void ChangeFightState(string state, object param = null)
    {
    }

    public void ChangeTurn(ActorCamp camp)
    {
        
    }

    public void ChangeWave(bool startOrEnd)
    {
        
    }

    public void InitHpView()
    {
        
    }

    public void OnSpawnerActor(Actor actor)
    {
        
    }

    public override void ToogleVisible(bool flag)
    {
        
    }

    public void OnAutoBtn()
    {
    }

    public void PlayComboAni()
    {
    }

    public void OnNewWave()
    {
    }

    public void OnTrogglePetIcon(bool isShow)
    {

    }
}