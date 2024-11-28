using UI_Battle;

public class BattleVictoryWindow : BaseWindow
{

    private UI_BattleVictoryWindow window;

    public override void OnOpen()
    {
        base.OnOpen();
        BattleWindow.Singleton.OnTrogglePetIcon(false);
        InitView();
    }

    public override void InitView()
    {
        base.InitView();
        window = getUiWindow<UI_BattleVictoryWindow>();
        window.m_t1.Play(OnAniCmp);

    }

    private void OnAniCmp()
    {
        Close();
        BattleWindow.Singleton.OpenChild<BattleExpWindow>(WinInfo.Create(false, null, false));
    }

}
