using Message.Guild;
using Data.Beans;
using UnityEngine;
using FairyGUI;
using System.Collections.Generic;
using UI_Guild;

public class ChairmanRewardWnd : BaseWindow
{
    private UI_ChairmanRewardWnd m_window;
    public override void OnOpen()
    {
        base.OnOpen();
        m_window = getUiWindow<UI_ChairmanRewardWnd>();
        m_window.m_btnClose.onClick.Add(Close);
        m_window.m_btnGet.onClick.Add(_OnGetClick);
        _ShowRewardState();
        _ShowRewardInfo();
    }


    public override void AddEventListener()
    {
        base.AddEventListener();
        GED.ED.addListener(EventID.GuildRewardStateChange, _OnRefreshRewardState);
    }

    public override void RemoveEventListener()
    {
        base.RemoveEventListener();
        GED.ED.removeListener(EventID.GuildRewardStateChange, _OnRefreshRewardState);
    }

    private void _OnRefreshRewardState(GameEvent evt)
    {
        _ShowRewardState();
    }

    private void _ShowRewardState()
    {
        int state = GuildService.Singleton.GetRewardState();
        m_window.m_btnGet.visible = state == 1;
        m_window.m_objGeted.visible = state == 2;
    }

    private void _ShowRewardInfo()
    {
        GuildInfo guildInfo = GuildService.Singleton.GetGuildInfo();
        t_guildBean guildBean = ConfigBean.GetBean<t_guildBean, int>(guildInfo.level);
        if (guildBean == null)
            return;

        string[] strRewardArr = GTools.splitString(guildBean.t_daily_reward, ';');
        for (int i = 0; i < strRewardArr.Length; i++)
        {
            int[] rewardArr = GTools.splitStringToIntArray(strRewardArr[i], '+');
            if (rewardArr == null || rewardArr.Length < 2)
                continue;

            CommonItem cell = CommonItem.CreateInstance();
            cell.itemId = rewardArr[0];
            cell.itemNum = rewardArr[1];
            cell.isShowNum = true;
            cell.RefreshView();
            m_window.m_rewardList.AddChild(cell);
        }
    }

    private void _OnGetClick()
    {
        GuildService.Singleton.ReqReward();
        Close();
    }

    protected override void OnClose()
    {
        base.OnClose();
    }
}