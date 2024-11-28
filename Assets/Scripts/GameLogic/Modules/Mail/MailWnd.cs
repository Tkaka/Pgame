using Message.Role;
using UnityEngine;
using FairyGUI;
using UI_Mail;
using Message.Mail;
using Data.Beans;
using System.Collections.Generic;

public class MailWnd : BaseWindow
{
    private UI_MailWnd m_window;
    private int m_maxMailNum = 100;     //最大邮件数量
    private List<long> m_CanGetItemList = new List<long>();  //可领取道具的邮件列表
    public override void OnOpen()
    {
        base.OnOpen();
        m_window = getUiWindow<UI_MailWnd>();
        m_window.m_btnClose.onClick.Add(Close);
        m_window.m_btnOneKeyGet.onClick.Add(_OnOneKeyGetClick);
        InitView();
    }

    public override void AddEventListener()
    {
        base.AddEventListener();
        GED.ED.addListener(EventID.MainRefresh, _RefreshWnd);
    }

    public override void RemoveEventListener()
    {
        base.RemoveEventListener();
        GED.ED.removeListener(EventID.MainRefresh, _RefreshWnd);
    }

    public override void InitView()
    {
        base.InitView();
        _RefreshMail();
    }

    //邮件发生变化
    private void _RefreshWnd(GameEvent evt)
    {
        _RefreshMail();
    }

    //刷新邮件
    private void _RefreshMail()
    {
        m_window.m_mainList.RemoveChildren(0, -1, true);
        List<MailInfo> mailList = MailService.Singleton.GetMailList();
        if (mailList == null)
            return;

        m_CanGetItemList.Clear();
        mailList.Sort((a, b) => a.state.CompareTo(b.state));
         
        for (int i = 0; i < mailList.Count; i++)
        {
            if (mailList[i].state < 2 && mailList[i].items.Count > 0)
            {
                m_CanGetItemList.Add(mailList[i].id);
            }

            UI_MailCell cell = UI_MailCell.CreateInstance();
            _OnMailCellShow(cell, mailList[i]);
            m_window.m_mainList.AddChild(cell);
        }

        m_window.m_txtMailNum.text = string.Format("{0}/{1}", mailList.Count, m_maxMailNum);
        m_window.m_btnOneKeyGet.enabled = m_CanGetItemList.Count > 0;
        m_window.m_btnOneKeyGet.grayed = !(m_CanGetItemList.Count > 0);

    }

    private void _OnMailCellShow(UI_MailCell cell, MailInfo info)
    {
        cell.m_txtMain.text = info.topic;
        cell.m_txtFrom.text = info.sender;
        cell.m_txtDate.text = TimeUtils.TimeToStringFormat(info.time, "{0}-{1}-{2}"); //TimeUtils.TimeToString(info.time);
        cell.m_imgReaded.visible = info.state > 0;
        cell.m_imgNoRead.visible = info.state == 0;

        if (info.items.Count > 0)
        {
            cell.m_icon.visible = true;
            cell.m_objNormal.visible = false;
            GObject obj = cell.GetChild("itemIcon");
            CommonItem itemIcon;
            if (obj != null)
            {
                itemIcon = obj as CommonItem;
            }
            else
            {
                itemIcon = CommonItem.CreateInstance();
                itemIcon.name = "itemIcon";
                cell.m_icon.AddChild(itemIcon);
                itemIcon.position = new Vector3(0, 0, 0);
            }
             
            itemIcon.itemId = info.items[0].id;
            itemIcon.itemNum = info.items[0].num;
            itemIcon.isShowNum = true;
            itemIcon.RefreshView();
            
        }
        else
        {
            cell.m_icon.visible = false;
            cell.m_objNormal.visible = true;
            //cell.m_icon.m_imgIcon.url = "";  //默认图标
        }

        cell.onClick.Add(() =>
        {
            //打开领取界面
            if (info.state == 0)
            {
                MailService.Singleton.ReqReadMail(info.id);
            }
             
            WinMgr.Singleton.Open<MailContentWnd>(WinInfo.Create(false, null, false, info));
        });
    }


    //一键领取点击
    private void _OnOneKeyGetClick()
    {
        MailService.Singleton.ReqReceive(m_CanGetItemList);
    }

    protected override void OnClose()
    {
        base.OnClose();
    }
}