using UI_Guild;
using Message.Guild;
using Data.Beans;
using UnityEngine;
using FairyGUI;

public class GuildExplainWnd : BaseWindow
{
    private UI_GuildExplainWnd m_window;
    public override void OnOpen()
    {
        base.OnOpen();
        m_window = getUiWindow<UI_GuildExplainWnd>();
        m_window.m_btnClose.onClick.Add(Close);
        _ShowTxtList();
    }


    private void _ShowTxtList()
    {
        GTextField txt = new GTextField();
        txt.textFormat.color = Color.white;
        txt.textFormat.size = 20;
        txt.textFormat.lineSpacing = 5;
        txt.text = "我是公会规则描述\n\n我是工会规则描述";
        m_window.m_txtList.AddChild(txt);
    }

    protected override void OnClose()
    {
        base.OnClose();
    }


}