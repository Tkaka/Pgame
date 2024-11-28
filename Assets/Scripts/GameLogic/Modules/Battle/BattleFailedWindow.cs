using UI_Battle;

public class BattleFailedWindow : BaseWindow
{

    private UI_BattleFailedWindow window;

    public override void OnOpen()
    {
        base.OnOpen();
        InitView();
    }

    public override void InitView()
    {
        base.InitView();
        window = getUiWindow<UI_BattleFailedWindow>();
        window.m_bg.onClick.Add(OnCloseBtn);
        window.m_jiubaBtn.onClick.Add(OnJinHuaBtn);
        window.m_qiangHuaBtn.onClick.Add(OnQiangHuaBtn);
        window.m_shengPinBtn.onClick.Add(OnShengPinBtn);
        window.m_turnTxt.text = FightManager.Singleton.TurnCount +  "";
        LNumber totalHp = FightManager.Singleton.CurWaveEnemyTotalHp;
        LNumber curHp = FightManager.Singleton.GetTotalHp(ActorCamp.CampEnemy, false);
        window.m_hpbar.fillAmount = (float)(curHp / totalHp);
        // 偏移
        window.m_rightBg.x += 1f;

        BattleWindMgr.CurrentBtlWin.OnTrogglePetIcon(false);
    }

    private void OnJinHuaBtn()
    {
        RestoreWndMgr.Singleton.ClearData();
        BattleService.Singleton.QuitBattle(false);
        JumpWndMgr.Singleton.JumpToWnd(JumpType.JiuBa);
    }

    private void OnQiangHuaBtn()
    {
        if (FuncService.Singleton.TipFuncNotOpen(1101))
        {
            RestoreWndMgr.Singleton.ClearData();
            BattleService.Singleton.QuitBattle(false);
            JumpWndMgr.Singleton.JumpToWnd(JumpType.EquipStrength, (int)EquipPosition.Weapon);
        }
    }

    private void OnShengPinBtn()
    {
        RestoreWndMgr.Singleton.ClearData();
        BattleService.Singleton.QuitBattle(false);
        JumpWndMgr.Singleton.JumpToWnd(JumpType.PetShengPing);
    }

    protected override void OnCloseBtn()
    {
        base.OnCloseBtn();
        BattleService.Singleton.QuitBattle();
    }

}




