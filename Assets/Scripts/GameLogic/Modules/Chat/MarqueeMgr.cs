using UI_Chat;
using FairyGUI;
using UnityEngine;
using System;
using DG.Tweening;
using Message.Chat;
using Message.Role;
using System.Collections.Generic;

public class MarqueeMgr
{
    private static MarqueeMgr m_instance;
    private string m_marqueeWnd = "";
    private Queue<ChatInfo> m_msgQueue = new Queue<ChatInfo>();
    public static MarqueeMgr GetInstance()
    {
        if (m_instance == null)
            m_instance = new MarqueeMgr();
        return m_instance;
    }

    public ChatInfo GetMarqueeInfo()
    {
        if (m_msgQueue.Count > 0)
        {
            return m_msgQueue.Dequeue();
        }

        return null;
    }

    //显示跑马灯信息
    public void ShowMarqueeInfo(ChatInfo chatInfo)
    {
        if (chatInfo.isMarquee == false)
            return;

        m_msgQueue.Enqueue(chatInfo);

        if (m_marqueeWnd.Equals("") || WinMgr.Singleton.GetWindow<MarqueeWnd>(m_marqueeWnd) == null)
        {
            m_marqueeWnd = WinMgr.Singleton.Open<MarqueeWnd>(null, UILayer.Notice);
        }

    }

    public void CloseMarqueeWnd()
    {
        if (!m_marqueeWnd.Equals("") && WinMgr.Singleton.GetWindow<MarqueeWnd>(m_marqueeWnd) != null)
        {
            WinMgr.Singleton.Close(m_marqueeWnd);
            m_marqueeWnd = "";
        }
    }

}