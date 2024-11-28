using Data.Beans;
using Message.Vip;
using UI_VIP;
using UI_Common;
using FairyGUI;

public class VipMainWnd : BaseWindow
{
    private UI_VipMainWnd m_window;

    public override void OnOpen()
    {
        base.OnOpen();
        m_window = getUiWindow<UI_VipMainWnd>();
        UI_commonTop commonTop = m_window.m_commonTop as UI_commonTop;
        if (commonTop != null)
        {
            commonTop.m_closeBtn.onClick.Add(Close);
        }

        m_window.m_btnLeft.onClick.Add(_OnLeftClick);
        m_window.m_btnRight.onClick.Add(_OnRightClick);
        m_window.m_btnRight.visible = true;
        m_window.m_btnLeft.visible = false;
        _InitList();
        InitView();
        
    }

    private void _InitList()
    {
        m_window.m_pageList.SetVirtual();
        m_window.m_pageList.itemProvider = _ItemProvider;
        m_window.m_pageList.itemRenderer = _ItemRender;
        m_window.m_pageList.scrollPane.onScroll.Add(_OnScroll);
        m_window.m_pageList.scrollPane.touchEffect = false;
        //m_window.m_pageList.touchable = false;
         
    }

    private string _ItemProvider(int index)
    {
        return VipPageCell.URL;
    }

    private void _ItemRender(int index, GObject obj)
    {
        VipPageCell cell = obj as VipPageCell;
        if (cell == null)
            return;

        cell.RefreshView(index + 1);
    }

    public override void InitView()
    {
        base.InitView();
       _ShowPageList();
        _ShowBaseInfo();

    }

    private void _ShowBaseInfo()
    {
        VipTitle vipTitle = m_window.m_vipTitle as VipTitle;
        if (vipTitle != null)
        {
            vipTitle.RefreshView(true);
        }
    }

    private void _ShowPageList()
    {
        m_window.m_pageList.numItems = ConfigBean.GetBeanList<t_vipBean>().Count - 1;
        if (VipService.Singleton.VipLevel > 0)
        {
            m_window.m_pageList.ScrollToView(VipService.Singleton.VipLevel - 1);
        }
 
    }

 


    private void _OnLeftClick()
    {
        int curIndex = m_window.m_pageList.GetFirstChildInView();
        if (curIndex > 0)
        {
            m_window.m_pageList.ScrollToView(curIndex - 1, true, true);
        }
        
    }

    private void _OnRightClick()
    {
        int curIndex = m_window.m_pageList.GetFirstChildInView();
        if (curIndex < (m_window.m_pageList.numItems - 1))
        {
            m_window.m_pageList.ScrollToView(curIndex + 1, true, true);
        }
    }

    private void _OnScroll()
    {
        m_window.m_btnLeft .visible = false;
        m_window.m_btnRight.visible = false;
        if (!(m_window.m_pageList.scrollPane.posX == 0))
        {
            m_window.m_btnLeft.visible = true;
        }

        if (!m_window.m_pageList.scrollPane.isRightMost)
        {
            m_window.m_btnRight.visible = true;
        }
    }

    public override void AddEventListener()
    {
        base.AddEventListener();
        GED.ED.addListener(EventID.VipInfoChange, _OnVipInfoChange);
        GED.ED.addListener(EventID.VipGiftBagStateChange, _OnVipGiftBagStateChange);
    }

    public override void RemoveEventListener()
    {
        base.RemoveEventListener();
        GED.ED.removeListener(EventID.VipInfoChange, _OnVipInfoChange);
        GED.ED.removeListener(EventID.VipGiftBagStateChange, _OnVipGiftBagStateChange);
    }

    private void _OnVipInfoChange(GameEvent evt)
    {
        _ShowBaseInfo();
        m_window.m_pageList.RefreshVirtualList();
    }

    private void _OnVipGiftBagStateChange(GameEvent evt)
    {
        m_window.m_pageList.RefreshVirtualList();
    }

    protected override void OnClose()
    {
        base.OnClose();
    }
}