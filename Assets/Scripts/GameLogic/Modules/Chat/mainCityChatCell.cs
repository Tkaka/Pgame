//主界面聊天中聊天信息
using UI_Chat;
using Message.Chat;
using System;

public class mainCityChatCell : UI_mainCityChatCell
{
    private ChatInfo m_chatInfo;
    private float m_textStartHeight = 0f;        //文本的起始高度

    public void Init(ChatInfo chatInfo)
    {
        m_chatInfo = chatInfo;
        m_textStartHeight = m_txtContent.height;

    }
    public new static mainCityChatCell CreateInstance()
    {
        return UI_mainCityChatCell.CreateInstance() as mainCityChatCell;
    }

    public void RefreshView()
    {
        if (m_chatInfo == null)
            return;

         m_objChannel.m_txtChannel.text = ChatTool.GetInstance().GetChannel((ChatService.EChannelType)m_chatInfo.channel);

        _ShowContent();
    }


    //显示聊天类容
    private void _ShowContent()
    {
        if (!string.IsNullOrEmpty(m_chatInfo.playerName))
        {
            m_txtContent.text = string.Format("             <font color=\"#0000aa\">[{0}]</font>{1}", m_chatInfo.playerName, m_chatInfo.content);
        }
        else
        {
            m_txtContent.text = string.Format("             {0}", m_chatInfo.content);
        }
        float addHeight = m_txtContent.height - m_textStartHeight;
        this.height += addHeight;
    }


}