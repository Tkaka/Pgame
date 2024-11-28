using Data.Beans;
using FairyGUI;
using UI_Shop;
using Message.Shop;
using Message.Role;

public class EquipBoxItem : BaseWindow
{
    private UI_EquipBoxItem m_window;

    public override void OnOpen()
    {
        base.OnOpen();
        m_window = getUiWindow<UI_EquipBoxItem>();
        m_window.m_btnRewardShow.onClick.Add(_OnRewardShowClick);
        m_window.m_btnDuiHuan.onClick.Add(_OnDuiHuanClick);
        m_window.m_objBox.onClick.Add(_OnBoxClick);
        InitView();
    }

    public override void InitView()
    {
        base.InitView();
        _ShowOpen1Info();
        _ShowOpen10Info();
        _ShowRewardInfo();
        _ShowSuiPianNum();
    }

    public override void AddEventListener()
    {
        base.AddEventListener();
        GED.ED.addListener(EventID.EquipTreasureBoxRefresh, _OpenInfoRefresh);
        GED.ED.addListener(EventID.CurrencyChange, _CurrencyChange);

    }


    public override void RemoveEventListener()
    {
        base.RemoveEventListener();
        GED.ED.removeListener(EventID.EquipTreasureBoxRefresh, _OpenInfoRefresh);
        GED.ED.removeListener(EventID.CurrencyChange, _CurrencyChange);
    }

    //货币发生变化
    private void _CurrencyChange(GameEvent evt)
    {
        RoleService.ECurrencyType type = (RoleService.ECurrencyType)evt.Data;
        if (type != RoleService.ECurrencyType.AWAKEN_FRAGMENT)
            return;

        _ShowSuiPianNum();

    }

    //显示碎片数量
    private void _ShowSuiPianNum()
    {
        m_window.m_txtNum.text = RoleService.Singleton.GetCurrencyNum(-5) + "";
        UIGloader.SetUrl(m_window.m_imgComsume, UIUtils.GetItemIcon((int)ItemType.AwakeFrag));
    }

    //免费信息刷新
    private void _OpenInfoRefresh(GameEvent evt)
    {
        _ShowOpen1Info();
    }

    private void _ShowOpen1Info()
    {
        ResEquipBoxInfo info = ShopService.Singleton.GetEquipBoxInfo();
        if (info == null)
            return;

        m_window.m_objOpen1.m_txtCount.text = info.drawNum + "";
        t_equipawakenboxBean bean = ConfigBean.GetBean<t_equipawakenboxBean, int>(1);
        if (bean == null)
            return;
        int daiBiNum = BagService.Singleton.GetItemNum(bean.t_ticket);
        if (daiBiNum > 0 && info.free == false)
        {
            //m_window.m_objOpen1.m_imgComsume.url = UIUtils.GetItemIcon(bean.t_ticket);
            UIGloader.SetUrl(m_window.m_objOpen1.m_imgComsume, UIUtils.GetItemIcon(bean.t_ticket));
            m_window.m_objOpen1.m_txtNum.text = string.Format("{0}/{1}", daiBiNum, 1);
        }
        else
        {
            //m_window.m_objOpen1.m_imgComsume.url = UIUtils.GetItemIcon(-2); //钻石
            UIGloader.SetUrl(m_window.m_objOpen1.m_imgComsume, UIUtils.GetItemIcon(-2));
            if (info.free)
                m_window.m_objOpen1.m_txtNum.text = "0";
            else
                m_window.m_objOpen1.m_txtNum.text = bean.t_price + "";
        }

        m_window.m_objOpen1.m_btnOpen1.onClick.Clear();
        m_window.m_objOpen1.m_btnOpen1.onClick.Add(() =>
        {
            int type = 1;
            if (info.free)
                type = 2;
            else if (daiBiNum > 0)
                type = 3;
            else
                type = 1;
            ShopService.Singleton.ReqOpenBox(1, type);
        });
    }

    private void _ShowOpen10Info()
    {
        t_equipawakenboxBean bean = ConfigBean.GetBean<t_equipawakenboxBean, int>(10);
        if (bean == null)
            return;

        int daiBiNum = BagService.Singleton.GetItemNum(bean.t_ticket);
        if (daiBiNum >= 10)
        {
            //m_window.m_objOpen10.m_imgComsume.url = UIUtils.GetItemIcon(bean.t_ticket);
            UIGloader.SetUrl(m_window.m_objOpen10.m_imgComsume, UIUtils.GetItemIcon(bean.t_ticket));
            m_window.m_objOpen10.m_txtNum.text = string.Format("{0}/{1}", daiBiNum, 10);
        }
        else
        {
            //m_window.m_objOpen10.m_imgComsume.url = UIUtils.GetItemIcon(-2); //钻石
            UIGloader.SetUrl(m_window.m_objOpen10.m_imgComsume, UIUtils.GetItemIcon(-2));
            m_window.m_objOpen10.m_txtNum.text = bean.t_price + "";
        }

        m_window.m_objOpen10.m_btnOpen10.onClick.Clear();
        m_window.m_objOpen10.m_btnOpen10.onClick.Add(() =>
        {
            int type = 1;
            if (daiBiNum >= 10)
                type = 3;
            else
                type = 1;
            ShopService.Singleton.ReqOpenBox(10, type);
        });
    }

    private void _ShowRewardInfo()
    {
        t_globalBean gBean = ConfigBean.GetBean<t_globalBean, int>(130202);
        if (gBean == null)
            return;

        int [] arr = GTools.splitStringToIntArray(gBean.t_string_param, '+');
        CommonItem cell = null;
        for (int i = 0; i < arr.Length; i++)
        {
            cell = CommonItem.CreateInstance();
            cell.itemId = arr[i];
            cell.isShowNum = false;
            cell.RefreshView();
            cell.SetScale(0.75f, 0.75f);
            m_window.m_rewardList.AddChild(cell);
        }
        if (cell != null)
            m_window.m_rewardList.columnGap = -(int)(0.25f * cell.width) + 20;
    }

    private void _OnRewardShowClick()
    {
        WinMgr.Singleton.Open<RewardPreviewWnd>(null, UILayer.Popup);
    }

    private void _OnDuiHuanClick()
    {
        WinMgr.Singleton.Open<SuiPianDuiHuanWnd>(null, UILayer.Popup);
    }
    
    //宝箱点击
    private void _OnBoxClick()
    {
    }


    protected override void OnClose()
    {
        base.OnClose();
    }
}