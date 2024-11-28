using UI_Chat;
using FairyGUI;
using UnityEngine;
using System;
using DG.Tweening;
using Message.Chat;
using Message.Role;
using System.Collections.Generic;

public class ChatWnd : BaseWindow
{
    private UI_ChatWnd m_window;
    private ChatService.EChannelType m_curChannel;  //当前频道
    private int m_noReadMgsNum = 0;          //未读消息数量
    private int m_curIndex = 0;              //当前显示格子的索引
    private int m_maxContentLength = 65;          //最大文本长度

    public override void OnOpen()
    {
        base.OnOpen();
        m_window = getUiWindow<UI_ChatWnd>();
        m_window.m_btnClose.onClick.Add(CloseWnd);
        m_window.m_bg.onClick.Add(CloseWnd);
        m_window.m_btnSend.onClick.Add(_OnSendClick);
        m_window.m_btnBell.onClick.Add(_OnTrumpetClick);
        m_window.m_btnEmoji.onClick.Add(_OnEmojiClick);
        m_window.m_tabControl.onChanged.Add(_OnChannelChange);
        m_window.m_objNoRead.onClick.Add(_OnJumpFirstInfo);
        m_window.m_chatList.scrollPane.onScroll.Add(_OnListScroll);

        m_window.m_objNoRead.visible = false;
        InitView();
    }

    public override void InitView()
    {
        base.InitView();
        m_window.m_chatList.itemProvider = _GetChatCellUrl;
        m_window.m_chatList.itemRenderer = _OnChatCellRender;
        m_window.m_chatList.SetVirtual();

        m_window.m_tabControl.selectedIndex = -1;
        m_window.m_tabControl.selectedIndex = 0;

    }

    public override void AddEventListener()
    {
        base.AddEventListener();
        GED.ED.addListener(EventID.ChatInfoRefresh, _OnChatInfoRefresh);
    }

    public override void RemoveEventListener()
    {
        base.RemoveEventListener();
        GED.ED.removeListener(EventID.ChatInfoRefresh, _OnChatInfoRefresh);
    }

    private void _OnChatInfoRefresh(GameEvent evt)
    {
        ChatInfo chatInfo = evt.Data as ChatInfo;
        if (chatInfo == null || chatInfo.channel != (int)m_curChannel)
            return;

        if (m_window.m_btnLockScreen.selected)
        {
            m_noReadMgsNum++;
            m_window.m_objNoRead.m_txtDes.text = string.Format("未读消息{0}条", m_noReadMgsNum);
            m_window.m_objNoRead.visible = true;

            if (m_curIndex == 0)
            {
                m_curIndex = m_window.m_chatList.GetFirstChildInView() + 1;
            }
            else
            {
                m_curIndex++;
            }

        }
        else
        {
            m_noReadMgsNum = 0;
            m_curIndex = 0;
        }

 
        _ShowChatInfo();
    }

    private void _OnChannelChange()
    {
        if (m_window.m_tabControl.selectedIndex > (int)ChatService.EChannelType.ZuiDui || 
            m_window.m_tabControl.selectedIndex < (int)ChatService.EChannelType.System)
            return;

        m_curChannel = (ChatService.EChannelType)m_window.m_tabControl.selectedIndex;
        _ShowChatInfo();
    }

    private void _ShowChatInfo()
    {
                
        List<ChatInfo> chatInfos = ChatService.Singleton.GetChatInfoByChannel(m_curChannel);
        if (chatInfos == null)
        {
            m_window.m_chatList.numItems = 0;
            return;
        }

        m_window.m_chatList.numItems = chatInfos.Count;
        if (!m_window.m_btnLockScreen.selected)
        {
            //没锁屏
            m_window.m_chatList.ScrollToView(0, true, true);
        }
        else
        {
            //滑到
            if (m_curIndex < m_window.m_chatList.numItems && m_curIndex >= 0)
            {
                m_window.m_chatList.ScrollToView(m_curIndex, false, false);
            }
            else
            {
                Debug.Log("index beyond arround" + (m_curIndex + m_noReadMgsNum));
            }
 
        }
 
    }

    private string _GetChatCellUrl(int index)
    {
        List<ChatInfo> chatInfos = ChatService.Singleton.GetChatInfoByChannel(m_curChannel);
        if (index >= chatInfos.Count || index < 0)
            return null;

        ChatInfo chatInfo = chatInfos[index];
        if (chatInfo.roleId > 0)
        {
            if (RoleService.Singleton.GetRoleInfo().roleId == chatInfo.roleId)
                return SelfChatCell.URL;
            else
                return OtherChatCell.URL;
        }

        return NormalChatCell.URL;
    }

    private void _OnChatCellRender(int index, GObject obj)
    {
        List<ChatInfo> chatInfos = ChatService.Singleton.GetChatInfoByChannel(m_curChannel);
        if (index >= chatInfos.Count || index < 0)
            return;

        ChatInfo chatInfo = chatInfos[index];
        if (chatInfo.roleId > 0)
        {
            long selfRoleId = RoleService.Singleton.GetRoleInfo().roleId;
            if (chatInfo.roleId != selfRoleId)
            {
                OtherChatCell cell = obj as OtherChatCell;
                if (cell == null)
                {
                    Debug.LogError("chat cell type error");
                    return;
                }
                cell.Init(chatInfo);
                cell.RefreshView();
            }
            else
            {
                SelfChatCell cell = obj as SelfChatCell;
                if (cell == null)
                {
                    Debug.LogError("chat cell type error");
                    return;
                }

                cell.Init(chatInfo);
                cell.RefreshView();
            }
        }
        else
        {
            NormalChatCell cell = obj as NormalChatCell;
            cell.Init(chatInfo);
            cell.RefreshView();
        }


    }

    private void _OnSendClick()
    {
        if (ChatTool.GetInstance().GetCurChannelIsCooling(m_curChannel))
        {
            TipWindow.Singleton.ShowTip(UIUtils.GetStrByLanguageID(70502003));
            return;
        }
        RoleInfo roleInfo = RoleService.Singleton.GetRoleInfo();
        ChatInfo chatInfo = new ChatInfo();
        chatInfo.channel = (int)m_curChannel;
        chatInfo.content = m_window.m_txtInput.text;
        chatInfo.iconId = 0;
        chatInfo.level = roleInfo.level;
        chatInfo.playerName = roleInfo.roleName;
        chatInfo.vipLevel = roleInfo.vip;
        chatInfo.roleId = roleInfo.roleId;
        chatInfo.iconId = roleInfo.headIconId;
        chatInfo.content = ChatTool.GetInstance().ConvertToFilterEmoji(chatInfo.content);
        m_window.m_txtInput.text = "";
        if (ChatService.Singleton.CheckChatMsgIsNormal(chatInfo))
        {
            ChatService.Singleton.ReqSendChatInfo(chatInfo);
            ChatTool.GetInstance().SetChannelChatCool(m_curChannel);
        }
 
    }

    private void _OnTrumpetClick()
    {
        WinMgr.Singleton.Open<TrumpetWnd>(null, UILayer.TopHUD);

    }

    private void _OnEmojiClick()
    {
        OneParam<Action<string>> oneParam = new OneParam<Action<string>>();
        oneParam.value = _SelectEmoji;
        WinMgr.Singleton.Open<ChatEmojiWnd>(WinInfo.Create(false, null, true, oneParam), UILayer.TopHUD);
    }


    private void _SelectEmoji(string emojiPath)
    {
        string strContent = m_window.m_txtInput.text + emojiPath;
        if (strContent.Length > m_maxContentLength)
        {
            TipWindow.Singleton.ShowTip("已达到文本最大长度");
            m_window.m_txtInput.text = strContent.Substring(0, m_maxContentLength);
        }
        else
        {
            m_window.m_txtInput.text = strContent;
        }
 
    }


    //跳到第一条消息
    private void _OnJumpFirstInfo()
    {
        m_window.m_chatList.ScrollToView(0, true, true);
        m_window.m_objNoRead.visible = false;
        m_window.m_btnLockScreen.selected = false;
    }

    //列表滚动
    private void _OnListScroll()
    {
        int index = m_window.m_chatList.GetFirstChildInView();
        if (index < 0)
            return;

        if (index == 0)
        {

            m_window.m_btnLockScreen.selected = false;
            m_window.m_objNoRead.visible = false;
            m_curIndex = 0;
            m_noReadMgsNum = 0;
        }
        else
        {
            m_window.m_btnLockScreen.selected = true;
            if (m_curIndex != index)
            {
                //手动滑过了位置将位置重置
                m_curIndex = 0;
            }
 
        }
    }

    public void CloseWnd()
    {
         m_window.m_chatGroup.TweenMoveX(-m_window.m_chatGroup.width, 0.5f).OnComplete(()=>{ Close(); });

    }

    protected override void OnClose()
    {
        base.OnClose();
    }
}