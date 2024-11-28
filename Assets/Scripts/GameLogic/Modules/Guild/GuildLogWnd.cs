using UI_Guild;
using Message.Guild;
using Data.Beans;
using UnityEngine;
using FairyGUI;
using System.Collections.Generic;

public class GuildLogWnd : BaseWindow
{
    private UI_GuildLogWnd m_window;
    public override void OnOpen()
    {
        base.OnOpen();
        m_window = getUiWindow<UI_GuildLogWnd>();
        GuildService.Singleton.ReqGuildLog();
    }


    private void _OnLogShow(GameEvent evt)
    {
        List<string> logList = evt.Data as List<string>;
        if (logList == null)
            return;

        m_window.m_LogList.RemoveChildren(0, -1, true);
        UI_LogCell cell = UI_LogCell.CreateInstance();
        cell.m_txtDate.text = TimeUtils.TimeToStringFormat(TimeUtils.currentMilliseconds(), "{0}年{1}月{2}日");
        m_window.m_LogList.AddChild(cell);
        for (int i = 0; i < logList.Count; i++)
        {
            m_window.m_LogList.AddChild(_GetText(logList[i]));
        }
    }

    private GTextField _GetText(string strDes)
    {
        GTextField aTextField = new GTextField();
        aTextField.SetSize(100, 100);
        aTextField.textFormat.color = Color.white;
        aTextField.textFormat.size = 20;
        aTextField.UBBEnabled = true;
        aTextField.text = strDes;
        return aTextField;
    }

    public override void AddEventListener()
    {
        base.AddEventListener();
        GED.ED.addListener(EventID.GuildLogRefresh, _OnLogShow);
    }

    public override void RemoveEventListener()
    {
        base.RemoveEventListener();
        GED.ED.removeListener(EventID.GuildLogRefresh, _OnLogShow);
    }

    protected override void OnClose()
    {
        base.OnClose();
    }
}