
using UI_Friend;
using Message.Chat;

public class FriendInfoWnd : BaseWindow
{
    private UI_FriendInfoWnd m_window;
    private ChatInfo m_chatInfo;

    public override void OnOpen()
    {
        base.OnOpen();
        m_window = getUiWindow<UI_FriendInfoWnd>();
        m_window.m_btnClose.onClick.Add(Close);
        InitView();
    }

    public override void InitView()
    {
        base.InitView();
        m_chatInfo = Info.param as ChatInfo;
        if (m_chatInfo == null)
            return;

        m_window.m_txtGuild.text = string.Format("社团:{0}", m_chatInfo.guildName);
        m_window.m_txtLevel.text = string.Format("等级{0}", m_chatInfo.level);
        m_window.m_objVip.m_txtVipLevel.text = m_chatInfo.vipLevel + "";
        m_window.m_txtName.text = m_chatInfo.playerName;
        m_window.m_txtGuild.visible = !string.IsNullOrEmpty(m_chatInfo.guildName);
        m_window.m_objVip.visible = m_chatInfo.vipLevel > 0;
        UIGloader.SetUrl(m_window.m_objIcon.m_imgIcon, UIUtils.GetHeadIcon(m_chatInfo.iconId));


    }

    protected override void OnClose()
    {
        base.OnClose();
    }
}
