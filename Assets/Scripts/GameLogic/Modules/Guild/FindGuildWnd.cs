using Message.Guild;
using Data.Beans;
using UnityEngine;
using FairyGUI;
using System.Collections.Generic;
using UI_Guild;


public class FindGuildWnd : BaseWindow
{
    private UI_FindGuildWnd m_window;

    public override void OnOpen()
    {
        base.OnOpen();
        m_window = getUiWindow<UI_FindGuildWnd>();
        m_window.m_btnFind.onClick.Add(_OnFindClick);
    }

    public override void AddEventListener()
    {
        base.AddEventListener();
        GED.ED.addListener(EventID.GuildFindResult, _ShowGuildInfo);
    }

    public override void RemoveEventListener()
    {
        base.RemoveEventListener();
        GED.ED.removeListener(EventID.GuildFindResult, _ShowGuildInfo);
    }


    private void _OnFindClick()
    {
        if (m_window.m_txtGuildName.text.Equals(""))
        {
            TipWindow.Singleton.ShowTip("请输入公会名");
            return;
        }

        GuildService.Singleton.ReqFindGuildByName(m_window.m_txtGuildName.text);
        m_window.m_guildList.RemoveChildren(0, -1, true);
    }


    private void _ShowGuildInfo(GameEvent evt)
    {
        GuildListInfo guildInfo = evt.Data as GuildListInfo;
        if (guildInfo == null)
            return;

        GuildInfoCell cell = GuildInfoCell.CreateInstance();
        cell.guildInfo = guildInfo;
        cell.RefreshView();
        m_window.m_guildList.AddChild(cell);
    }

    protected override void OnClose()
    {
        base.OnClose();
    }
}