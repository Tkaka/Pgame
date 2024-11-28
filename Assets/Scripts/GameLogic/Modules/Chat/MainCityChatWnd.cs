using UI_Chat;
using FairyGUI;
using UnityEngine;
using System;
using DG.Tweening;
using Message.Chat;
using Message.Role;
using System.Collections.Generic;

public class MainCityChatWnd : BaseWindow
{
    private UI_MainCityChatWnd m_window;
    private float m_imgBgStartHeight;    //背景图片的起始高度
    private float m_chatListStartHeight;  //列表的起始高度
    private float m_shirkHeight = 50f;    //拉伸的高度
    private Vector3 m_arrowStartPos;      //箭头的起始位置
    private Vector3 m_trumpetStartPos;    //喇叭起始位置
    private float m_fixedHeight = 30;     //喇叭背景拉伸的修正高度
    private int m_trumpetShowTime = 5;    //喇叭显示时间
    private long m_corId;


    public override void OnOpen()
    {
        base.OnOpen();
        m_window = getUiWindow<UI_MainCityChatWnd>();       
        m_window.m_trumpetGroup.visible = false;
        _Init();
        InitView();

    }

    private void _Init()
    {
        m_imgBgStartHeight = m_window.m_imgBg.height;
        m_chatListStartHeight = m_window.m_chatList.height;
        m_arrowStartPos = m_window.m_btnArrow.position;
        m_trumpetStartPos = m_window.m_trumpetGroup.position;

    }

    public override void AddEventListener()
    {
        base.AddEventListener();
        GED.ED.addListener(EventID.ChatInfoRefresh, _OnChatInfoRefresh);
        GED.ED.addListener(EventID.ChatShowChannelChange, _OnShowChannelChange);
    }

    public override void RemoveEventListener()
    {
        base.RemoveEventListener();
        GED.ED.removeListener(EventID.ChatInfoRefresh, _OnChatInfoRefresh);
        GED.ED.removeListener(EventID.ChatShowChannelChange, _OnShowChannelChange);
    }

    private void _OnChatInfoRefresh(GameEvent evt)
    {
        ChatInfo chatInfo = evt.Data as ChatInfo;
        if (chatInfo != null && chatInfo.isBell)
        {
            _ShowTrumpetInfo(chatInfo);
        }
        _ShowChatInfo();
    }

    private void _OnShowChannelChange(GameEvent evt)
    {
        ChatService.Singleton.FilterChatInfo();
        _ShowChatInfo();
    }


    public override void InitView()
    {
        base.InitView();
        m_window.m_chatList.itemProvider = _GetChatCellUrl;
        m_window.m_chatList.itemRenderer = _OnChatCellRender;
        m_window.m_chatList.SetVirtual();

        _OnArrowClick();
        m_window.m_btnArrow.onClick.Add(_OnArrowClick);
        m_window.m_btnSet.onClick.Add(_OnSetClick);
        m_window.m_chatList.onClick.Add(_OnWndClick);

        _ShowChatInfo();
    }   


    private void _ShowTrumpetInfo(ChatInfo chatInfo)
    {
        mainCityChatCell cell = m_window.m_objInfo as mainCityChatCell;
        if (cell != null)
        {
            cell.Init(chatInfo);
            cell.RefreshView();
            m_window.m_imgTrumpetBg.height = cell.height + m_fixedHeight;
            m_window.m_objInfo.position = m_window.m_imgTrumpetBg.position;
        }

        m_window.m_trumpetGroup.visible = true;
        CoroutineManager.Singleton.stopCoroutine(m_corId);
        m_corId = CoroutineManager.Singleton.delayedCall(m_trumpetShowTime, () =>
        {
            m_window.m_trumpetGroup.visible = false;
        });

    }

    private void _ShowChatInfo()
    {               
        List<ChatInfo> chatInfos = ChatService.Singleton.GetFilterChatInfos();
    

        if (chatInfos == null)
        {
            m_window.m_chatList.numItems = 0;
            return;
        }


        m_window.m_chatList.numItems = chatInfos.Count;
        m_window.m_chatList.ScrollToView(0, true, true);
 
    }

    private string _GetChatCellUrl(int index)
    {
        return mainCityChatCell.URL;
    }

    private void _OnChatCellRender(int index, GObject obj)
    {
        mainCityChatCell cell = obj as mainCityChatCell;
        if (cell == null)
            return;

        List<ChatInfo> chatInfos = ChatService.Singleton.GetFilterChatInfos();
        if (index >= chatInfos.Count || index < 0)
            return;

        ChatInfo chatInfo = chatInfos[index];
        cell.Init(chatInfo);
        cell.RefreshView();

    }


    private void _OnArrowClick()
    {
        if (m_window.m_btnArrow.rotation > 0)
        {
            //拉伸
            m_window.m_btnArrow.rotation = 0;
            m_window.m_btnArrow.position = m_arrowStartPos;
            m_window.m_imgBg.height = m_imgBgStartHeight;
            m_window.m_chatList.height = m_chatListStartHeight;
            m_window.m_trumpetGroup.position = m_trumpetStartPos;

        }
        else
        {
            //收缩
            m_window.m_btnArrow.rotation = 180;
            m_window.m_btnArrow.position = new Vector3(m_arrowStartPos.x, m_arrowStartPos.y + m_shirkHeight, m_arrowStartPos.z);
            m_window.m_imgBg.height = m_imgBgStartHeight - m_shirkHeight;
            m_window.m_chatList.height = m_chatListStartHeight - m_shirkHeight;
            m_window.m_trumpetGroup.position = new Vector3(m_trumpetStartPos.x, m_trumpetStartPos.y + m_shirkHeight, m_trumpetStartPos.z);
        }
    }


    private void _OnSetClick()
    {
        WinMgr.Singleton.Open<ChatChannelSelectWnd>();
    }

    private void _OnWndClick()
    {
        WinMgr.Singleton.Open<ChatWnd>(null, UILayer.TopHUD);
    }

    protected override void OnClose()
    {
        base.OnClose();
    }
}
