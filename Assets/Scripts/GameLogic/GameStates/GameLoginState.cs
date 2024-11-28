using FairyGUI;
using UnityEngine;

public class GameLoginState : GameBaseState
{
    public override void onEnter(object obj = null)
    {
        base.onEnter(obj);
        Debug.Log("enter login state");

        cmdInit();
        GameMainCityState.EnterGameCamAniPlayed = false;
        WinMgr.Singleton.Open<LoginWindow>();
        //WinMgr.Singleton.Open<ConnectingWindow>();
    }

    private bool cmdInited;
    private void cmdInit()
    {
        ServiceManager.Singleton.ClearData();
        if (cmdInited)
            return;

        cmdInited = true;
        // ³õÊ¼»¯ËùÓÐService
        new InitServiceCmd().Excute();

        // ´°¿Ú×¢²á
        new RegisterWindowCmd().Excute();
    }


    public override string getStateKey()
    {
        return GameState.Login;
    }
}