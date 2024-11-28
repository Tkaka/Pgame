
using FairyGUI;
using UI_WorldMap;

public class SelectMissionWindow : BaseWindow
{

    private UI_SelectMissionWindow win;

    public override void OnOpen()
    {
        base.OnOpen();
        win = getUiWindow<UI_SelectMissionWindow>();
        win.m_panel.m_enterBtn.onClick.Add(OnEnterBtn);
        win.m_panel.m_closeBtn.onClick.Add(() =>
        {
            Close();
        });
    }

    private void OnEnterBtn()
    {
        //Close();
        WinMgr.Singleton.CloseAll();
        SceneLoader.Singleton.nextState = GameState.Battle;
        SceneLoader.Singleton.sceneName = GSceneName.Battle;
        UIPackage.AddPackage(WinEnum.BasePath + WinEnum.UI_Battle);
        GameManager.Singleton.changeState(GameState.Loading);
    }

}