
using UI_Guild;
using Message.Guild;
using Data.Beans;
using FairyGUI;

public class FragmentWishWnd : BaseWindow
{
    private ResWishInfo m_wishInfo;
    private UI_FragmentWishWnd m_window;
    public override void OnOpen()
    {
        base.OnOpen();
        m_window = getUiWindow<UI_FragmentWishWnd>();
        m_window.m_btnClose.onClick.Add(Close);
        if (RoleService.Singleton.IsExitGuildCooling())
        {
            TipWindow.Singleton.ShowTip(UIUtils.GetStrByLanguageID(61601035));
            Close();
        }
        else
        {
            GuildService.Singleton.ReqWishInfo();
        }

       

    }

    public override void AddEventListener()
    {
        base.AddEventListener();
        GED.ED.addListener(EventID.GuildWishInfo, _OnRefreshInfo);
        GED.ED.addListener(EventID.GuildWishResult, _OnWishResult);
    }

    public override void RemoveEventListener()
    {
        base.RemoveEventListener();
        GED.ED.removeListener(EventID.GuildWishInfo, _OnRefreshInfo);
        GED.ED.removeListener(EventID.GuildWishResult, _OnWishResult);
    }


    private void _OnRefreshInfo(GameEvent evt)
    {
        ResWishInfo info = evt.Data as ResWishInfo;
        if (info == null)
            return;

        m_wishInfo = info;
        InitView();
    }


    private void _OnWishResult(GameEvent evt)
    {
        int itemId = (int)evt.Data;
        m_wishInfo.itemId = itemId;
        _ShowRoleInfo();

    }

    public override void InitView()
    {
        base.InitView();
        _ShowRoleInfo();
        _ShowPlayerInfo();
    }

    private void _ShowRoleInfo()
    {
        int maxGiftNum = ConfigBean.GetBean<t_globalBean, int>(1602004).t_int_param;
        m_window.m_txtCount.text = string.Format("今日赠予次数:{0}/{1}", m_wishInfo.presentNum, maxGiftNum);

        WishRole wishInfo = new WishRole();
        wishInfo.roleId = RoleService.Singleton.GetRoleInfo().roleId;
        wishInfo.name = RoleService.Singleton.GetRoleInfo().roleName;
        //头像
        wishInfo.level = RoleService.Singleton.GetRoleInfo().level;
        wishInfo.itemId = m_wishInfo.itemId;
        wishInfo.num = m_wishInfo.num;
        wishInfo.type = m_wishInfo.type;
        objWishPlayerCell cell = m_window.m_objRole as objWishPlayerCell;
        if (cell != null)
        {
            cell.Init(wishInfo);
            cell.RefreshView();
        }
    }

    private void _ShowPlayerInfo()
    {
        m_window.m_txtNotice.visible = m_wishInfo.wishRoles.Count == 0;
        m_window.m_playerList.SetVirtual();
        m_window.m_playerList.defaultItem = objWishPlayerCell.URL;
        m_window.m_playerList.itemProvider = _ItemProvider;
        m_window.m_playerList.itemRenderer = _ItemRenderer;

        m_window.m_playerList.numItems = m_wishInfo.wishRoles.Count;

    }

    private string _ItemProvider(int index)
    {
        return objWishPlayerCell.URL;
    }

    private void _ItemRenderer(int index, GObject obj)
    {
        if (index >= m_wishInfo.wishRoles.Count || index < 0)
            return;

        WishRole info = m_wishInfo.wishRoles[index];
        objWishPlayerCell cell = obj as objWishPlayerCell;
        if (cell != null)
        {
            cell.Init(info);
            cell.RefreshView();
        }
    }

    protected override void OnClose()
    {
        base.OnClose();
    }
}