 using UI_Chat;
using FairyGUI;
using UnityEngine;
using System;
using Message.Chat;
using Message.Role;
using System.Collections.Generic;
using Data.Beans;
using DG.Tweening;

public class TrumpetMsgMgr
{
    private static TrumpetMsgMgr m_instance;
    private List<MaincityTrumpetMsgInfo> m_objList = new List<MaincityTrumpetMsgInfo>();
    private Vector3 m_rightPos;
    private Vector3 m_rightUpPos;
    private Vector3 m_rightDownPos;

    public static TrumpetMsgMgr GetInstance()
    {
        if (m_instance == null)
        {
            m_instance = new TrumpetMsgMgr();
        }

        return m_instance;
    }

    private TrumpetMsgMgr()
    {
        Vector3 pos = GRoot.inst.position;
        m_rightPos = pos + new Vector3(GRoot.inst.width * 0.8f, GRoot.inst.height * 0.5f, 0);
        m_rightUpPos = pos + new Vector3(GRoot.inst.width * 0.8f, GRoot.inst.height * 0.3f, 0);
        m_rightDownPos = pos + new Vector3(GRoot.inst.width * 0.8f, GRoot.inst.height * 0.7f, 0);

    }


    public void ShowTrumpetInfo(ChatInfo chatInfo)
    {
        if (chatInfo.hasBellStyle() == false)
        {
            //普通喇叭不需要显示
            return;
        }

        MaincityTrumpetMsgInfo obj = null;
        for (int i = 0; i < m_objList.Count; i++)
        {
            if (m_objList[i].visible == false)
            {
                obj = m_objList[i];
                break;
            }
        }

        if (obj == null)
        {
            obj = MaincityTrumpetMsgInfo.CreateInstance();
            m_objList.Add(obj);
        }

        obj.Init(chatInfo);
        obj.RefreshView();

        switch ((ChatService.ETrumpetMove)chatInfo.bellStyle)
        {
            case ChatService.ETrumpetMove.Left:
                _MoveToLeft(obj);
                break;
            case ChatService.ETrumpetMove.LeftDown:
                _MoveToLeftDown(obj);
                break;
            case ChatService.ETrumpetMove.LeftUp:
                _MoveToLeftUp(obj);
                break;
            default:
                break;

        }
 

    }

    private void _MoveToLeft(GObject obj)
    {
        obj.position = m_rightPos;
        obj.rotation = 0;
        Vector2 targetPos = new Vector2(m_rightPos.x, m_rightPos.y) + (new Vector2(-1, 0) * 600);
        obj.TweenMove(targetPos, 5).SetEase(Ease.Linear).OnComplete(() => obj.visible = false);
    }

    private void _MoveToLeftUp(GObject obj)
    {
        obj.position = m_rightDownPos;
        obj.rotation = 45;
        Vector2 targetPos = new Vector2(m_rightDownPos.x, m_rightDownPos.y) + (new Vector2(-1, -1) * 600);
        obj.TweenMove(targetPos, 5).SetEase(Ease.Linear).OnComplete(() => obj.visible = false);
    }

    private void _MoveToLeftDown(GObject obj)
    {
        obj.position = m_rightUpPos;
        obj.rotation = -45;
        Vector2 targetPos = new Vector2(m_rightUpPos.x, m_rightUpPos.y) + (new Vector2(-1, 1) * 600);
        obj.TweenMove(targetPos, 5).SetEase(Ease.Linear).OnComplete(() => obj.visible = false);
    }

}