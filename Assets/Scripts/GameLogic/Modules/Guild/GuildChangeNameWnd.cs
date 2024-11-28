using UI_Guild;
using Message.Guild;
using Data.Beans;
using UnityEngine;
using FairyGUI;
using System.Collections.Generic;

public class GuildChangeNameWnd : BaseWindow
{
    public UI_GuildChangeNameWnd m_window;

    public override void OnOpen()
    {
        base.OnOpen();
        m_window = getUiWindow<UI_GuildChangeNameWnd>();
        m_window.m_btnClose.onClick.Add(Close);
        m_window.m_btnOk.onClick.Add(_OkClick);
        int comsumeNum = ConfigBean.GetBean<t_globalBean, int>(1601005).t_int_param;
        m_window.m_txtDiamondNum.text = comsumeNum + "";
    }


    private void _OkClick()
    {
        if (m_window.m_txtInput.text.Equals(""))
        {
            TipWindow.Singleton.ShowTip("公会名字不能为空");
        }             
        else
        {
            int comsumeNum = ConfigBean.GetBean<t_globalBean, int>(1601005).t_int_param;
            int haveNum = (int)RoleService.Singleton.GetCurrencyNum((int)RoleService.ECurrencyType.DIAMOND);
            if (haveNum < comsumeNum)
                TipWindow.Singleton.ShowTip("钻石不足");
            else
                GuildService.Singleton.ReqChangeName(m_window.m_txtInput.text);

        }
            
        Close();
    }


    //文本改变
    //private void _OnTxtChanged()
    //{
    //    string strDes = m_window.m_txtInput.text;
    //    if (strDes.Length > m_window.m_txtInput.maxLength)
    //        m_window.m_txtInput.text = strDes.Substring(0, m_window.m_txtInput.maxLength);
    //}

    protected override void OnClose()
    {
        base.OnClose();
    }
}