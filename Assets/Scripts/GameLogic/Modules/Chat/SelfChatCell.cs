using UI_Chat;
using Message.Chat;
using System;
using FairyGUI;
using UnityEngine;


public class SelfChatCell : UI_SelfChatCell
{
    private ChatInfo m_chatInfo;
    private float m_textStartHeight = 0f;        //文本的起始高度
    private float m_imgFixHeight = 20f;
    private float m_imgFixWight = 20f;
    private float m_maxWith = 184;

    public void Init(ChatInfo chatInfo)
    {
        m_chatInfo = chatInfo;
        m_textStartHeight = m_txtContent.height;
        m_txtContent.align = AlignType.Right;


    }

    public new static SelfChatCell CreateInstance()
    {
        return UI_SelfChatCell.CreateInstance() as SelfChatCell;
    }

    public void RefreshView()
    {
        if (m_chatInfo == null)
            return;

        //m_imgIcon.m_imgIcon.url  //头像
        UIGloader.SetUrl(m_imgIcon.m_imgIcon, UIUtils.GetHeadIcon(m_chatInfo.iconId));
        m_name.text = m_chatInfo.playerName;
        m_channel.m_txtChannel.text = ChatTool.GetInstance().GetChannel((ChatService.EChannelType)m_chatInfo.channel);
        m_vip.visible = m_chatInfo.vipLevel > 0;
        m_vip.m_txtVipLevel.text = m_chatInfo.vipLevel + "";

        _ShowContent();
    }

    //显示聊天类容
    private void _ShowContent()
    {
        
        m_txtContent.text = m_chatInfo.content;
        //Debug.Log("宽度" + m_txtContent.height + "          " + m_txtContent.width  + "   " + m_txtContent.textHeight + 
        //    "    " + m_txtContent.textWidth + "       " + m_txtContent.richTextField.width + "   " + m_txtContent.richTextField.height);


        //Debug.Log("------------------------------->>>>>>>>>>" + m_txtContent.textWidth + "          " + m_txtContent.width);
        //if ((m_txtContent.textHeight / m_txtContent.textFormat.size) >= 2)
        //{
        //换行了改为左对齐
        //m_txtContent.align = AlignType.Left;

        //}
        if (m_txtContent.textWidth >= m_maxWith && (m_txtContent.textHeight / m_txtContent.textFormat.size) >= 2)
        {
            m_txtContent.align = AlignType.Left;
        }

        float addHeight = m_txtContent.height - m_textStartHeight;
        m_imgBubble.height = m_txtContent.textHeight + m_imgFixHeight;
        m_imgBubble.width = m_txtContent.textWidth + m_imgFixWight;
        this.height += addHeight;
    }

}