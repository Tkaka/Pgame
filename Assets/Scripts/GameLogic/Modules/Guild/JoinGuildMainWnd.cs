using UI_Guild;
using Message.Guild;
using Data.Beans;
using UnityEngine;
using FairyGUI;
using System.Collections.Generic;

public class JoinGuildMainWnd : BaseWindow
{
    private UI_JoinGuildMainWnd m_window;
    private string m_curOpenWnd = "";
    public override void OnOpen()
    {
        base.OnOpen();
        m_window = getUiWindow<UI_JoinGuildMainWnd>();
        m_window.m_btnClose.onClick.Add(Close);
        m_window.m_tabControl.onChanged.Add(_OnControlChange);
        m_window.m_tabControl.selectedIndex = -1;
        m_window.m_tabControl.selectedIndex = 0;
    }

    public override void AddEventListener()
    {
        base.AddEventListener();
        GED.ED.addListener(EventID.GuildInfo, _OnGuildInfo);
    }

    public override void RemoveEventListener()
    {
        base.RemoveEventListener();
        GED.ED.removeListener(EventID.GuildInfo, _OnGuildInfo);
    }

    private void _OnGuildInfo(GameEvent evt)
    {
        Close();
    }

    private void _OnControlChange()
    {
        if (!m_curOpenWnd.Equals(""))
        {
            WinMgr.Singleton.Close(m_curOpenWnd);
            m_curOpenWnd = "";
        }

        switch (m_window.m_tabControl.selectedIndex)
        {
            case 0:
                //加入社团
                m_curOpenWnd = WinMgr.Singleton.Open<JoinGuildWnd>();
                break;
            case 1:
                //创建社团
                m_curOpenWnd = WinMgr.Singleton.Open<CreateGuildWnd>();
                break;
            case 2:
                //查找社团
                m_curOpenWnd = WinMgr.Singleton.Open<FindGuildWnd>();
                break;
            default:
                return;
        }
    }

    protected override void OnClose()
    {
        base.OnClose();
        if (!m_curOpenWnd.Equals(""))
        {
            WinMgr.Singleton.Close(m_curOpenWnd);
            m_curOpenWnd = "";
        }
    }
}