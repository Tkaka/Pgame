

public class GameUpdateResState : GameBaseState
{
    public override void onEnter(object obj = null)
    {
        Logger.log("enter UpdateRes state");
        WinMgr.Singleton.CloseAll();
        WinMgr.Singleton.Open<UpdateLoadingWindow>();
        StartupManager.Singleton.Start(()=>
        {
            UpdateLoadingWindow.Singleton.Close();
            GameManager.Singleton.changeState(GameState.Login);
        });
    }
    
    public override string getStateKey()
    {
        return GameState.UpdateRes;
    }
}