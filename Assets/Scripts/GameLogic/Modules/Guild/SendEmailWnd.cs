using UI_Guild;
using Message.Guild;
using Data.Beans;
using UnityEngine;
using FairyGUI;

public class SendEmailWnd : BaseWindow
{
    private UI_SendEmailWnd m_window;

    public override void OnOpen()
    {
        base.OnOpen();
        m_window = getUiWindow<UI_SendEmailWnd>();
        m_window.m_btnClose.onClick.Add(Close);
        m_window.m_btnSend.onClick.Add(_OnSendClick);
    }


    private void _OnSendClick()
    {
        if (m_window.m_txtInput.text.Equals(""))
        {
            TipWindow.Singleton.ShowTip("未输入任何文字");
            return;
        }
        GuildInfo guildInfo = GuildService.Singleton.GetGuildInfo();
        int maxNum = ConfigBean.GetBean<t_globalBean, int>(1601004).t_int_param;
        if (guildInfo.mailNum >= maxNum)
            TipWindow.Singleton.ShowTip("今天发邮件的次数用完了");
        else
            GuildService.Singleton.ReqSendMail(m_window.m_txtInput.text);

        Close();
    }


    protected override void OnClose()
    {
        base.OnClose();
    }
}