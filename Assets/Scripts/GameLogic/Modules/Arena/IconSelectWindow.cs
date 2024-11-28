using UI_Arena;
using Message.Arena;
using System.Collections.Generic;
using UnityEngine;
using Data.Beans;
using FairyGUI;
using Message.Role;

public class IconSelectWindow : BaseWindow
{
    private UI_IconSelectWindow m_window;

    public override void OnOpen()
    {
        base.OnOpen();         
        m_window = getUiWindow<UI_IconSelectWindow>();
        m_window.m_btnClose.onClick.Add(Close);
        InitView();
    }

    public override void InitView()
    {
        base.InitView();
        _ShowList();

    }

    //显示已有的形象列表
    private void _ShowList()
    {

    }

    protected override void OnClose()
    {
        base.OnClose();

    }

}