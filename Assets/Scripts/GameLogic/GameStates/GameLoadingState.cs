public class GameLoadingState : GameBaseState
{

    private string winId;
    private object m_param;

    public override void onEnter(object obj = null)
    {
        base.onEnter(obj);
        TwoParam<string, bool> param = obj as TwoParam<string, bool>;
        if(obj != null)
        {
            m_param = param.value1;
        }

        Logger.log("enter loading state");
        //string sceneName = obj as string;
        //SceneLoader.Singleton.sceneName = sceneName;
        winId = WinMgr.Singleton.Open<LoadingWindow>(WinInfo.Create(false, null, false, param), UILayer.Loading);
    }

    public override void onLeave(string stateKey)
    {
        base.onLeave(stateKey);
        WinMgr.Singleton.Close(winId);
        if (m_param != null)
        {
            string state = m_param as string;
            if (state != null && (state.Equals(GameState.Battle)))
            {
                //上一个状态是战斗状态(恢复进入战斗时打开的窗口)
                RestoreWndMgr.Singleton.RestoreWnd();
                m_param = null;
            }
        }
    }

    public override string getStateKey()
    {
        return GameState.Loading;
    }

}