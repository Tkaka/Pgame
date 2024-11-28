using UI_Chat;
using FairyGUI;
using UnityEngine;
using System;
using DG.Tweening;
using Message.Chat;
using Message.Role;
using System.Collections.Generic;

public class ChatChannelSelectWnd : BaseWindow
{
    private UI_ChatChannelSelectWnd m_window;
    public override void OnOpen()
    {
        base.OnOpen();
        m_window = getUiWindow<UI_ChatChannelSelectWnd>();
        m_window.m_btnClose.onClick.Add(Close);
        m_window.m_btnOk.onClick.Add(_OnOkClick);
        InitView();
    }

    public override void InitView()
    {
        base.InitView();
        m_window.m_btnGuild.selected = ChatTool.GetInstance().GetChannelIsShow(ChatService.EChannelType.Guild);
        m_window.m_btnSystem.selected = ChatTool.GetInstance().GetChannelIsShow(ChatService.EChannelType.System);
        m_window.m_btnTeam.selected = ChatTool.GetInstance().GetChannelIsShow(ChatService.EChannelType.Team);
        m_window.m_btnWorld.selected = ChatTool.GetInstance().GetChannelIsShow(ChatService.EChannelType.World);
        m_window.m_btnZuDui.selected = ChatTool.GetInstance().GetChannelIsShow(ChatService.EChannelType.ZuiDui);
    }


    private void _OnOkClick()
    {
        ChatTool.GetInstance().SetChannelIsShow(ChatService.EChannelType.Guild, m_window.m_btnGuild.selected ? 1 : 0);
        ChatTool.GetInstance().SetChannelIsShow(ChatService.EChannelType.System, m_window.m_btnSystem.selected ? 1 : 0);
        ChatTool.GetInstance().SetChannelIsShow(ChatService.EChannelType.Team, m_window.m_btnTeam.selected ? 1 : 0);
        ChatTool.GetInstance().SetChannelIsShow(ChatService.EChannelType.World, m_window.m_btnWorld.selected ? 1 : 0);
        ChatTool.GetInstance().SetChannelIsShow(ChatService.EChannelType.ZuiDui, m_window.m_btnZuDui.selected ? 1 : 0);
        GED.ED.dispatchEvent(EventID.ChatShowChannelChange);
        Close();

    }

    protected override void OnClose()
    {
        base.OnClose();
    }
}