using UI_Guild;
using Message.Guild;
using Data.Beans;
using UnityEngine;
using FairyGUI;
using System.Collections.Generic;
using System;


public class FirstOpenNoticeWnd : BaseWindow
{
    private UI_FirstOpenNoticeWnd m_window;
    public override void OnOpen()
    {
        base.OnOpen();
        m_window = getUiWindow<UI_FirstOpenNoticeWnd>();
        m_window.m_btnClose.onClick.Add(Close);
        InitView();
    }

    public override void InitView()
    {
        base.InitView();
        GuildInfo guildInfo = GuildService.Singleton.GetGuildInfo();
        m_window.m_txtChairMan.text = UIUtils.GetStrByLanguageID(71601050) + guildInfo.chairmanName;
        m_window.m_txtNotice.text = guildInfo.notice;
    }

    protected override void OnClose()
    {
        base.OnClose();
    }

}