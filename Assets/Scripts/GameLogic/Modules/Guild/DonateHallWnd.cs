using Message.Guild;
using UI_Guild;
using Data.Beans;
using UnityEngine;
using System.Collections.Generic;

public class DonateHallWnd : BaseWindow
{
    private UI_DonateHallWnd m_window;
    private ResDonate m_msg;
    private int m_maxDonateNum = ConfigBean.GetBean<t_globalBean, int>(1601013).t_int_param;
    public override void OnOpen()
    {
        base.OnOpen();
        m_window = getUiWindow<UI_DonateHallWnd>();
        m_window.m_btnClose.onClick.Add(Close);
        m_window.m_btnDonate.onClick.Add(_OnDonateClick);
        GuildService.Singleton.ReqOpenDonate((int)GuildService.EDonateBigType.Guild);


    }

    public override void AddEventListener()
    {
        base.AddEventListener();
        GED.ED.addListener(EventID.GuildDonateInfo, _OnGuildDonateInfoRefresh);
    }


    public override void RemoveEventListener()
    {
        base.RemoveEventListener();
        GED.ED.removeListener(EventID.GuildDonateInfo, _OnGuildDonateInfoRefresh);
    }

    public override void InitView()
    {
        base.InitView();
    }

    private void _OnGuildDonateInfoRefresh(GameEvent evt)
    {
        ResDonate msg = evt.Data as ResDonate;
        if (msg == null)
            return;

        if (msg.bigType != (int)GuildService.EDonateBigType.Guild || msg.donateInfos.Count == 0)
            return;

        m_msg = msg;
        DonateInfo donateInfo = msg.donateInfos[0];

        m_window.m_txtDonateCount.text = string.Format("{0}/{1}", m_maxDonateNum - msg.num, m_maxDonateNum);
        m_window.m_txtGuildLevel.text = donateInfo.level + "";
        m_window.m_txtDonateCount.color = m_maxDonateNum > msg.num ? Color.blue : Color.red;

        t_guildBean guildBean = ConfigBean.GetBean<t_guildBean, int>(donateInfo.level);
        if (guildBean != null)
        {
            m_window.m_tadayProgressBar.value = (1.0f * msg.dailyExp / guildBean.t_exp_max) * 100;
            m_window.m_totalPorgressBar.value = (1.0f * donateInfo.exp / guildBean.t_exp) * 100;
            m_window.m_tadayProgressBar.m_txtProgress.text = string.Format("{0}/{1}", msg.dailyExp, guildBean.t_exp_max);
            m_window.m_totalPorgressBar.m_txtProgress.text = string.Format("{0}/{1}", donateInfo.exp, guildBean.t_exp);
        }

        _ShowDonateTypeList();
    }

    private void _ShowDonateTypeList()
    {
        m_window.m_donateList.RemoveChildren(0, -1, true);
        List<t_donate_typeBean> beans = ConfigBean.GetBeanList<t_donate_typeBean>();
        for (int i = 0; i < beans.Count; i++)
        {
            t_donate_typeBean bean = beans[i];
            UI_objDonateBigTypeCell cell = UI_objDonateBigTypeCell.CreateInstance();
            if (m_msg != null && m_msg.donateInfos.Count > 0)
                cell.m_imgLock.visible = m_msg.donateInfos[0].level < bean.t_level;

            UIGloader.SetUrl(cell.m_imgBg, bean.t_bg_icon);
            m_window.m_donateList.AddChild(cell);

            cell.onClick.Add(() =>
            {
                Debug.Log("打开捐献详细界面");
                WinMgr.Singleton.Open<DonatePageWnd>(WinInfo.Create(false, null, true, bean.t_id), UILayer.Popup);
            });
             
        }
    }


    private void _OnDonateClick()
    {
        TwoParam<DonateInfo, ResDonate> twoParam = new TwoParam<DonateInfo, ResDonate>();
        twoParam.value1 = m_msg.donateInfos[0];
        twoParam.value2 = m_msg;
        WinMgr.Singleton.Open<DonateRewardWnd>(WinInfo.Create(false, null, true, twoParam), UILayer.Popup);
    }

    protected override void OnClose()
    {
        base.OnClose();
    }
}