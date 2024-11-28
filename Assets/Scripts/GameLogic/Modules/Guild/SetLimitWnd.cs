using UI_Guild;
using Message.Guild;
using Data.Beans;
using UnityEngine;
using FairyGUI;
using System.Collections.Generic;
using Data.Beans;

public class SetLimitWnd : BaseWindow
{
    private UI_SetLimitWnd m_window;
    private int m_curSelectType;
    private int m_curSelectLevel;
    private int m_serviceMaxLevel = ConfigBean.GetBean<t_globalBean, int>(8010).t_int_param; //当前服务器最大等级
    private int m_minLevel = ConfigBean.GetBean<t_globalBean, int>(1601012).t_int_param;        //最小等级
    

    public override void OnOpen()
    {
        base.OnOpen();
        m_window = getUiWindow<UI_SetLimitWnd>();
        m_window.m_btnClose.onClick.Add(Close);
        m_window.m_btnOk.onClick.Add(_OnOkClick);
        m_window.m_btnLevelLeft.onClick.Add(_OnLevelLeftClick);
        m_window.m_btnLevelRight.onClick.Add(_OnLevelRightClick);
        m_window.m_btnTypeLeft.onClick.Add(_OnTypeLeftClick);
        m_window.m_btnTypeRight.onClick.Add(_OnTypeRightClick);
        m_window.m_btnCancel.onClick.Add(Close);

        _InitShow();
    }

    private void _InitShow()
    {
        GuildInfo guildInfo = GuildService.Singleton.GetGuildInfo();
        m_curSelectLevel = guildInfo.levelLimt;
        m_curSelectType = guildInfo.limitType;
        _ShowTypeDes();
        _ShowLevelDes();
    }

    private void _OnOkClick()
    {
        GuildService.Singleton.ReqChangeLimit(m_curSelectType, m_curSelectLevel);
        Close();
    }

    private void _OnLevelLeftClick()
    {
        if (m_curSelectLevel == -1)
            m_curSelectLevel = m_serviceMaxLevel;
        else if (m_curSelectLevel == m_minLevel + 1)
            m_curSelectLevel = 0;
        else
            m_curSelectLevel--;
        _ShowLevelDes();

    }

    private void _OnLevelRightClick()
    {
        if (m_curSelectLevel == m_serviceMaxLevel)
        {
            m_curSelectLevel = -1;
        }
        else if (m_curSelectLevel == 0)
        {
            m_curSelectLevel = m_minLevel;
            m_curSelectLevel++;
        }
        else
        {
            m_curSelectLevel++;
        }
 
        _ShowLevelDes();
    }

    private void _OnTypeLeftClick()
    {
        if (m_curSelectType == 0)
            m_curSelectType = 2;
        else
            m_curSelectType--;
        _ShowTypeDes();
    }

    private void _OnTypeRightClick()
    {
        if (m_curSelectType == 2)
            m_curSelectType = 0;
        else
            m_curSelectType++;
        _ShowTypeDes();
    }

    private void _ShowTypeDes()
    {
        m_window.m_txtType.text = GuildService.Singleton.GetLimitTypeDes(m_curSelectType);
    }

    private void _ShowLevelDes()
    {
        m_window.m_txtLevel.text = GuildService.Singleton.GetLimitLevelDes(m_curSelectLevel);
    }

    protected override void OnClose()
    {
        base.OnClose();
    }
}