using UI_Chat;
using FairyGUI;
using UnityEngine;
using System;
using Message.Chat;
using Message.Role;
using System.Collections.Generic;
using Data.Beans;

public class FriendGiftWnd : BaseWindow
{
    //private UI_FriendGiftWnd m_window;
    public override void OnOpen()
    {
        base.OnOpen();
       // m_window = getUiWindow<UI_FriendGiftWnd>();
        //m_window.m_btnClose.onClick.Add(Close);
        InitView();
    }

    public override void InitView()
    {
        base.InitView();

    }

    protected override void OnClose()
    {
        base.OnClose();
    }
}