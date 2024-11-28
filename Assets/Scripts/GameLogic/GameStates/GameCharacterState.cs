using System.Collections;
public class GameCharacterState : GameBaseState
{
    private string wind;
    public override void onEnter(object obj = null)
    {
        base.onEnter(obj);
        if (GPlayerPrefs.HasKey("ChuCiJinRu"))
        {
            //GPlayerPrefs.DeleteKey("ChuCiJinRu");
            wind = WinMgr.Singleton.Open<SelectRoleWindow>();
        }
        else
        {
            GPlayerPrefs.SetString("ChuCiJinRu", "true");
            wind = WinMgr.Singleton.Open<OpenPlayWindow>();
        }
    }

    public override void onLeave(string stateKey)
    {
        base.onLeave(stateKey);
        LoginScene.Singleton.Unload();
        WinMgr.Singleton.Close(wind);
    }

    public override string getStateKey()
    {
        return GameState.Character;
    }
}
