using UI_Battle;
using UnityEngine;

public class PauseWindow : BaseWindow
{

    private UI_PauseWindow window;

    public override void OnOpen()
    {
        base.OnOpen();
        InitView();
    }

    public override void InitView()
    {
        base.InitView();
        window = getUiWindow<UI_PauseWindow>();
        window.m_goonBtn.onClick.Add(OnGoonBtn);
        window.m_levelBtn.onClick.Add(OnLeveBtn);
        window.m_closeBtn.onClick.Add(OnGoonBtn);
    }

    private void OnGoonBtn()
    {
        Time.timeScale = FightManager.Singleton.GameSpeed;
        BattleCDCtrl.Singleton.PauseToggle(false);
        Close();
    }

    private void OnLeveBtn()
    {
        Close();
        BattleCDCtrl.Singleton.Stop();
        OpenChild<BattleFailedWindow>(WinInfo.Create(false, winName, false));
    }

}
