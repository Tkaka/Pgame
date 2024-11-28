using UI_BattleEnd;
using FairyGUI;
using UnityEngine;

public class BattleEndWindow : BaseWindow
{
    private UI_BattleEndWindow win;

    private GoWrapper winEft;

    public override void OnOpen()
    {
        base.OnOpen();
        win = getUiWindow<UI_BattleEndWindow>();
        win.m_exitBtn.onClick.Add(OnExitBtn);
        //GameObject go = Res.Singleton.InstantiateCEffect("UI/eff_ui_tongguan_win_chibang");
        GameObject go = FightManager.R.LoadGo("eff_ui_tongguan_win_chibang");
        //GTools.ScaleParticleSystem(go, 0.8f);
        winEft = new GoWrapper(go);
        win.m_winEft.SetNativeObject(winEft);
        //SoundManager.Singleton.PlaySFX("JiangLi");
        AudioManager.Singleton.PlayEffect("snd_jiangli");
    }

    private void OnExitBtn()
    {
        Logger.log("On exit Btn");
        WinMgr.Singleton.CloseAll();
        //UIPackage.RemovePackage(WinEnum.UI_BattleEnd);
        //UIPackage.RemovePackage(WinEnum.UI_Battle);
        WinMgr.RemovePackage(WinEnum.UI_BattleEnd);
        WinMgr.RemovePackage(WinEnum.UI_Battle);
        SceneLoader.Singleton.nextState = GameState.MainCity;
        SceneLoader.Singleton.sceneName = GSceneName.MaiCity;
        GameManager.Singleton.changeState(GameState.Loading);
    }

    protected override void OnClose()
    {
        winEft.Dispose();
        base.OnClose();
    }

}