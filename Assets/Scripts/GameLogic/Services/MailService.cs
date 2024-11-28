using Message.Mail;
using System.Collections.Generic;
using Message.Role;
using Data.Beans;
using UnityEngine;

public class MailService : SingletonService<MailService>
{
    private List<MailInfo> m_mailList;

    public MailService()
    {

    }

    protected override void RegisterEventListener()
    {
        base.RegisterEventListener();
        GED.NED.addListener(ResMailList.MsgId, _ResMailList);
        GED.NED.addListener(ResAddMail.MsgId, _ResAddMail);
        GED.NED.addListener(ResMailStateChange.MsgId, _ResMailStateChange);
        GED.NED.addListener(ResDelMail.MsgId, _ResDelMail);


    }

    protected override void UnRegisterEventListener()
    {
        base.UnRegisterEventListener();
        GED.NED.removeListener(ResMailList.MsgId, _ResMailList);
        GED.NED.removeListener(ResAddMail.MsgId, _ResAddMail);
        GED.NED.removeListener(ResMailStateChange.MsgId, _ResMailStateChange);
        GED.NED.removeListener(ResDelMail.MsgId, _ResDelMail);
    }


    //获得邮件列表
    public List<MailInfo> GetMailList()
    {
        return m_mailList;
    }

    //刷新邮件红点
    private void _RefreshMailRedDot()
    {
        bool isShowRed = false;
        do
        {
            if (m_mailList == null)
                break;

            for (int i = 0; i < m_mailList.Count; i++)
            {
                if (m_mailList[i].state == 0)
                {
                    //未读
                    isShowRed = true;
                    break;
                }
            }
        }
        while (false);

        RedDotManager.Singleton.SetRedDotValue("EMail", isShowRed);
    }
    //------------------------------------------------------消息

    //邮件列表
    private void _ResMailList(GameEvent evt)
    {
        ResMailList msg = GetCurMsg<ResMailList>(evt.EventId);
        m_mailList = msg.mails;
        _RefreshMailRedDot();
    }

    //添加邮件
    private void _ResAddMail(GameEvent evt)
    {
        ResAddMail msg = GetCurMsg<ResAddMail>(evt.EventId);
        if (m_mailList != null)
        {
            m_mailList.Add(msg.mail);
        }
        else
        {
            //Debug.LogError("邮件列表为空！");
            m_mailList = new List<MailInfo>();
            m_mailList.Add(msg.mail);
        }

        _RefreshMailRedDot();
        GED.ED.dispatchEvent(EventID.MainRefresh);

    }

    //邮件状态改变
    private void _ResMailStateChange(GameEvent evt)
    {
        ResMailStateChange msg = GetCurMsg<ResMailStateChange>(evt.EventId);
        if (m_mailList != null)
        {
            for (int index = 0; index < msg.mails.Count; index++)
            {
                for (int i = 0; i < m_mailList.Count; i++)
                {
                    if (m_mailList[i].id == msg.mails[index].id)
                    {
                        m_mailList[i].state = msg.mails[index].state;
                        break;
                    }
                 }
            }
 
        }
        else
        {
            Debug.LogError("邮件列表为空！");
        }

        _RefreshMailRedDot();
        GED.ED.dispatchEvent(EventID.MainRefresh);
    }

    //删除邮件
    private void _ResDelMail(GameEvent evt)
    {
        ResDelMail msg = GetCurMsg<ResDelMail>(evt.EventId);
        for (int index = 0; index < msg.ids.Count; index++)
        {
            for (int i = 0; i < m_mailList.Count; i++)
            {
                if (msg.ids[index] == m_mailList[i].id)
                {
                    m_mailList.Remove(m_mailList[i]);
                    break;
                }
            }
        }

        _RefreshMailRedDot();
        GED.ED.dispatchEvent(EventID.MainRefresh);
    }

    //------------------------------------------------------------请求
    //请求领取
    public void ReqReceive(List<long> ids)
    {
        ReqReceive msg = GetEmptyMsg<ReqReceive>();
        for (int i = 0; i < ids.Count; i++)
        {
            msg.ids.Add(ids[i]);
        }

        SendMsg<ReqReceive>(ref msg);

    }

    public void ReqReadMail(long id)
    {
        ReqReadMail msg = GetEmptyMsg<ReqReadMail>();
        msg.id = id;
        SendMsg<ReqReadMail>(ref msg);
    }

}