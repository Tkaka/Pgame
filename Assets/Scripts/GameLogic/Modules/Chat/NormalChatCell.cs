//聊天中别人发的格子聊天信息
using UI_Chat;
using Message.Chat;
using System;

public class NormalChatCell : UI_normalChatCell
{
    private ChatInfo m_chatInfo; 
    private float m_textStartHeight = 0f;        //文本的起始高度

    public void Init(ChatInfo chatInfo)
    {
        m_chatInfo = chatInfo;
        m_textStartHeight = m_txtContent.height;
  
    }
    public new static NormalChatCell CreateInstance()
    {
        return UI_normalChatCell.CreateInstance() as NormalChatCell;
    }

    public void RefreshView()
    {
        if (m_chatInfo == null)
            return;

        //m_imgIcon.m_imgIcon.url  //头像

        m_channel.m_txtChannel.text = ChatTool.GetInstance().GetChannel((ChatService.EChannelType)m_chatInfo.channel);

        _ShowContent();
    }


    //显示聊天类容
    private void _ShowContent()
    {
        m_txtContent.text = m_chatInfo.content;
        float addHeight = m_txtContent.height - m_textStartHeight;
        this.height += addHeight;
    }


}