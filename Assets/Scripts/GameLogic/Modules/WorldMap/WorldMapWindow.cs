
using UI_WorldMap;

public class WorldMapWindow : BaseWindow
{

    private WorldMapListCtrl listCtrl;

    private UI_WorldMapWindow wmWin;

    public override void OnOpen()
    {
        base.OnOpen();
        wmWin = getUiWindow<UI_WorldMapWindow>();
        listCtrl = new WorldMapListCtrl(this);
        wmWin.m_backBtn.onClick.Add(OnBackBtn);
    }

    private void OnBackBtn()
    {
        listCtrl.OnClose();
        Close();
    }

    protected override void OnClose()
    {
        listCtrl.OnClose();
        base.OnClose();
    }

}