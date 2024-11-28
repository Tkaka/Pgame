using Message.Guild;
using Data.Beans;
using UnityEngine;
using FairyGUI;
using System.Collections.Generic;
using UI_Guild;

public class JoinGuildWnd : BaseWindow
{
    private UI_JoinGuildWnd m_window;
    private int m_maxPage = 1;
    private int m_curPage = 1;
    private int m_perPageMemberNum = ConfigBean.GetBean<t_globalBean, int>(1601009).t_int_param;  //每页显示的人数
    public override void OnOpen()
    {
        base.OnOpen();
        m_window = getUiWindow<UI_JoinGuildWnd>();
        m_window.m_btnLeft.onClick.Add(_OnLeftClick);
        m_window.m_btnRight.onClick.Add(_OnRightClick);
        m_window.m_btnFastJoin.onClick.Add(_OnFastJoinClick);

        GuildService.Singleton.ReqGuildList(1);
    }

    public override void AddEventListener()
    {
        base.AddEventListener();
        GED.ED.addListener(EventID.GuildListInfo, _OnGuildInfo);
    }

    public override void RemoveEventListener()
    {
        base.RemoveEventListener();
        GED.ED.removeListener(EventID.GuildListInfo, _OnGuildInfo);
    }

    private void _OnGuildInfo(GameEvent evt)
    {
        ResGuildList msg = evt.Data as ResGuildList;
        if (msg == null)
            return;

        if (msg.hasGuildNum())
        {
            m_maxPage = (int)Mathf.Ceil(msg.guildNum / (m_perPageMemberNum * 1.0f));
            if (m_maxPage == 0)
                m_maxPage = 1;
            m_curPage = 1;

            _ShowPageInfo();
        }

        _ShowGuildList(msg.listInfo);
    }


    private void _ShowPageInfo()
    {
        if (m_curPage == 1)
        {
            m_window.m_btnLeft.grayed = true;
            m_window.m_btnLeft.enabled = false;
        }
        else
        {
            m_window.m_btnLeft.grayed = false;
            m_window.m_btnLeft.enabled = true;
        }

        if (m_curPage == m_maxPage)
        {
            m_window.m_btnRight.grayed = true;
            m_window.m_btnRight.enabled = false;
        }
        else
        {
            m_window.m_btnRight.grayed = false;
            m_window.m_btnRight.enabled = true;
        }

        m_window.m_txtPage.text = string.Format("{0}/{1}", m_curPage, m_maxPage);
 
    }

    private void _OnLeftClick()
    {
        if (m_curPage == 1)
            return;
        m_curPage--;
        _ShowPageInfo();
        GuildService.Singleton.ReqGuildList(m_curPage);
    }

    private void _OnRightClick()
    {
        if (m_curPage == m_maxPage)
            return;
        m_curPage++;
        _ShowPageInfo();
        GuildService.Singleton.ReqGuildList(m_curPage);
    }

    //显示公会列表
    private void _ShowGuildList(List<GuildListInfo> guildLists)
    {
        m_window.m_guildList.RemoveChildren(0, -1, true);
        for (int i = 0; i < guildLists.Count; i++)
        {
            GuildInfoCell cell = GuildInfoCell.CreateInstance();
            cell.guildInfo = guildLists[i];
            cell.RefreshView();
            m_window.m_guildList.AddChild(cell);
        }
    }

    private void _OnFastJoinClick()
    {
        GuildService.Singleton.ReqApplyJoin(true);
    }

    protected override void OnClose()
    {
        base.OnClose();
    }
}