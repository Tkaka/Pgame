using Message.Guild;
using Data.Beans;
using UnityEngine;
using FairyGUI;
using System.Collections.Generic;
using UI_Guild;

public class ModifyNoticeWnd : BaseWindow
{
    private UI_ModifyNoticeWnd m_window;

    public override void OnOpen()
    {
        base.OnOpen();
        m_window = getUiWindow<UI_ModifyNoticeWnd>();
        m_window.m_btnClose.onClick.Add(Close);
        m_window.m_btnOk.onClick.Add(_OnOkClick);

        GuildInfo guildInfo = GuildService.Singleton.GetGuildInfo();
        m_window.m_txtInput.text = guildInfo.notice;
    }


    //确认点击
    private void _OnOkClick()
    {
        if (m_window.m_txtInput.text.Equals(""))
        {
            TipWindow.Singleton.ShowTip("公告不能为空");
            return;
        }
        GuildService.Singleton.ReqChangeNotice(m_window.m_txtInput.text);
        Close();
    }


    protected override void OnClose()
    {
        base.OnClose();
    }
}