//聊天中别人发的格子聊天信息
using UI_Chat;
using Message.Chat;
using System;

public class OtherChatCell : UI_otherChatCell
{
    private ChatInfo m_chatInfo; 
    private float m_textStartHeight = 0f;        //文本的起始高度
    private float m_imgFixHeight = 20f;
    private float m_imgFixWight = 15;

    public void Init(ChatInfo chatInfo)
    {
        m_chatInfo = chatInfo;
        m_textStartHeight = m_txtContent.height;
  
    }
    public new static OtherChatCell CreateInstance()
    {
        return UI_otherChatCell.CreateInstance() as OtherChatCell;
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
 
        _ShowJump();
        _ShowContent();

        this.m_imgIcon.onClick.Clear();
        this.m_imgIcon.onClick.Add(_OnClickPlayerIcon);
    }


    private void _OnClickPlayerIcon()
    {
        if (m_chatInfo.roleId != RoleService.Singleton.GetRoleInfo().roleId)
        {
            WinMgr.Singleton.Open<FriendInfoWnd>(WinInfo.Create(false, null, true, m_chatInfo), UILayer.TopHUD);
        }
 
    }

    //显示聊天类容
    private void _ShowContent()
    {
        m_txtContent.text = m_chatInfo.content;
        float addHeight = m_txtContent.height - m_textStartHeight;
        m_imgBubble.height = m_txtContent.textHeight + m_imgFixHeight;
        m_imgBubble.width = m_txtContent.textWidth + m_imgFixWight;
        this.height += addHeight;
    }

    private void _ShowJump()
    {
        int jumpType = m_chatInfo.jumpType;
        m_btnJump.onClick.Clear();
        m_btnJump.visible = true;
        switch ((ChatService.EJumpType)jumpType)
        {
            case ChatService.EJumpType.Help:
                m_btnJump.m_txtQuit.text = "协助";
                m_btnJump.onClick.Add(() => { });
                break;
            case ChatService.EJumpType.JoinGuild:
                m_btnJump.m_txtQuit.text = "申请";
                m_btnJump.onClick.Add(() => { });
                break;
            case ChatService.EJumpType.JoinTeam:
                m_btnJump.m_txtQuit.text = "加入";
                m_btnJump.onClick.Add(() => { });
                break;
            case ChatService.EJumpType.PlayVideo:
                m_btnJump.m_txtQuit.text = "播放";
                m_btnJump.onClick.Add(() => { });
                break;
            default:
                m_btnJump.visible = false;
                break;
        }
    }
}