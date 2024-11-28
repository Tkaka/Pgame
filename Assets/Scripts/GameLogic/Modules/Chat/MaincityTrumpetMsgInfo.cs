using UI_Chat;
using FairyGUI;
using UnityEngine;
using System;
using Message.Chat;
using Message.Role;
using System.Collections.Generic;
using Data.Beans;

public class MaincityTrumpetMsgInfo : UI_MaincityTrumpetMsgInfo
{
    private ChatInfo m_chatInfo;

    public static new MaincityTrumpetMsgInfo CreateInstance()
    {
        return UI_MaincityTrumpetMsgInfo.CreateInstance() as MaincityTrumpetMsgInfo;
    }

    public void Init(ChatInfo chatInfo)
    {
        m_chatInfo = chatInfo;
        WinMgr.Singleton.HudLayer.AddChild(this);
        this.visible = false;

    }

    public void RefreshView()
    {
        if (m_chatInfo == null || !m_chatInfo.isBell)
            return;

        this.visible = true;

        switch ((ChatService.ETrumpetFont)m_chatInfo.bellFontType)
        {
            case ChatService.ETrumpetFont.Big:
                m_txtMsg.textFormat.size = 25;
                break;
            case ChatService.ETrumpetFont.Small:
                m_txtMsg.textFormat.size = 15;
                break;
            default:
                break;
        }

        t_trumpet_colorBean bean = ConfigBean.GetBean<t_trumpet_colorBean, int>(m_chatInfo.bellColor);
        if (bean != null)
        {
            float[] arr = GTools.splitStringToFloatArray(bean.t_color_value, '+');
            if (arr != null && arr.Length >= 4)
            {
                Color color = new Color();
                color.r = arr[0];
                color.g = arr[1];
                color.b = arr[2];
                color.a = arr[3];
                m_txtMsg.color = color;
            }
 
        }
  
        m_txtMsg.text = m_chatInfo.content;
        m_imgBg.width = m_txtMsg.width + 20f;
        m_imgBg.height = m_txtMsg.height + 10f;
    }


}