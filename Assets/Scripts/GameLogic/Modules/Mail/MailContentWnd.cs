using Message.Role;
using UnityEngine;
using FairyGUI;
using UI_Mail;
using Message.Mail;
using Data.Beans;
using System.Collections.Generic;

public class MailContentWnd : BaseWindow
{
    private UI_MailContentWnd m_window;
    private MailInfo m_info;
    private Vector3 m_fromStartPos;

    public override void OnOpen()
    {
        base.OnOpen();
        m_window = getUiWindow<UI_MailContentWnd>();
        m_window.m_btnClose.onClick.Add(Close);
        m_window.m_btnGet.onClick.Add(_OnGetClick);
        m_fromStartPos = m_window.m_txtFrom.position;
        InitView();
    }

    public override void InitView()
    {
        base.InitView();
        m_info = Info.param as MailInfo;
        if (m_info == null)
        {
            Debug.LogError("邮件参数为空");
            return;
        }

        m_window.m_txtTitle.text = m_info.topic;
        m_window.m_txtDescribe.text = m_info.content;
        m_window.m_txtFrom.text = m_info.sender;

        if (m_info.items.Count == 0)
        {
            m_window.m_rewardGroup.visible = false;
            m_window.m_txtFrom.position = m_fromStartPos + new Vector3(0, 100, 0);
        }
        else
        {
            m_window.m_rewardGroup.visible = true;
            m_window.m_btnGet.visible = m_info.state < 2;
            m_window.m_imgGeted.visible = m_info.state == 2;
            m_window.m_txtFrom.position = m_fromStartPos;
            for (int i = 0; i < m_info.items.Count; i++)
            { 

                CommonItem cell = CommonItem.CreateInstance();
                cell.itemId = m_info.items[i].id;
                cell.itemNum = m_info.items[i].num;
                cell.isShowNum = true;
                cell.RefreshView();

                m_window.m_rewardList.AddChild(cell);
            }
        }

    }



    private void _OnGetClick()
    {
        List<long> ids = new List<long>();
        ids.Add(m_info.id);
        MailService.Singleton.ReqReceive(ids);
        Close();
    }

    protected override void OnClose()
    {
        base.OnClose();
        m_info = null;
    }
}