using UI_Chat;
using FairyGUI;
using UnityEngine;
using System;
using DG.Tweening;
using Message.Chat;
using Message.Role;
using System.Collections.Generic;

public class MarqueeWnd : BaseWindow
{
    private UI_MarqueeWnd m_window;
    private Vector3 m_txtStartPos;
    private DoActionInterval m_timer;
 
    public override void OnOpen()
    {
        base.OnOpen();
        m_window = getUiWindow<UI_MarqueeWnd>();
        m_txtStartPos = m_window.m_txtDes.position;
        m_timer = new DoActionInterval();
        _ShowMarqueeInfo();
    }


    private void _ShowMarqueeInfo()
    {
        ChatInfo chatInfo = MarqueeMgr.GetInstance().GetMarqueeInfo();
        if (chatInfo == null)
        {
            MarqueeMgr.GetInstance().CloseMarqueeWnd();
            return;
        }
        m_window.m_txtDes.position = m_txtStartPos;
        Vector3 targetPos = m_window.m_imgBg.position;
        m_window.m_txtDes.text = chatInfo.content;
 
        m_timer.doAction(0.01f, (param) =>
        {
            if (m_window.m_txtDes.position.x + m_window.m_txtDes.textWidth <= targetPos.x)
            {               
                 m_timer.kill(); 
                _ShowMarqueeInfo();
            }
            else
            {
                m_window.m_txtDes.position = new Vector3(m_window.m_txtDes.position.x - 2f, m_window.m_txtDes.position.y, m_window.m_txtDes.z);
            }
        });

        //Debug.Log("------------------->>>>>>>>>>目标位置" + m_window.m_imgBg.position  +  "      " + m_txtStartPos);
        //m_window.m_txtDes.TweenMoveX(targetPos.x - m_txtStartPos.x, 5).SetEase(Ease.Linear).OnComplete(_ShowMarqueeInfo);

    }

    protected override void OnClose()
    {
        base.OnClose();
        if(m_timer != null)
            m_timer.kill();
    }
}