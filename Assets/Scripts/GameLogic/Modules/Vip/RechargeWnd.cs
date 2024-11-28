using UI_VIP;
using Message.Recharge;
using Data.Beans;
using UI_Common;
using System.Collections.Generic;
using FairyGUI;

public class RechargeWnd : BaseWindow
{

    private UI_RechargeWnd m_window;
    private List<t_rechargeBean> m_rechargeBeans;

    public override void OnOpen()
    {
        base.OnOpen();
        m_window = getUiWindow<UI_RechargeWnd>();
        UI_commonTop commonTop = m_window.m_commonTop as UI_commonTop;
        if (commonTop != null)
        {
            commonTop.m_closeBtn.onClick.Add(Close);
        }
        _Init();

        _ShowVipInfo();
        _ShowRechargeInfo();
    }


    private void _Init()
    {
        m_rechargeBeans = ConfigBean.GetBeanList<t_rechargeBean>();
        m_rechargeBeans.Sort((a, b) => a.t_index.CompareTo(b.t_index));
        m_window.m_rechargeList.SetVirtual();
        m_window.m_rechargeList.itemProvider = _ItemProvider;
        m_window.m_rechargeList.itemRenderer = _ItemRender;
    }

    private string _ItemProvider(int index)
    {
        return RechargeCell.URL;
    }

    private void _ItemRender(int index, GObject obj)
    {
        RechargeCell cell = obj as RechargeCell;
        if (cell == null)
            return;

        if (index < 0 || index >= m_rechargeBeans.Count)
            return;

        cell.RefreshView(m_rechargeBeans[index].t_id);
    }

    private void _ShowVipInfo()
    {
        VipTitle vipTitle = m_window.m_vipTitle as VipTitle;
        if (vipTitle != null)
        {
            vipTitle.RefreshView(false);
        }
    }


    //显示充值信息
    private void _ShowRechargeInfo()
    {
        m_window.m_rechargeList.numItems = m_rechargeBeans.Count;

    }

    public override void AddEventListener()
    {
        base.AddEventListener();
        GED.ED.addListener(EventID.VipInfoChange, _OnVipInfoChange);
    }


    public override void RemoveEventListener()
    {
        base.RemoveEventListener();
        GED.ED.removeListener(EventID.VipInfoChange, _OnVipInfoChange);
    }

    private void _OnVipInfoChange(GameEvent evt)
    {
        _ShowVipInfo();
    }

    protected override void OnClose()
    {
        base.OnClose();
    }
}