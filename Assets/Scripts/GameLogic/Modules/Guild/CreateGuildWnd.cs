using Message.Guild;
using Data.Beans;
using UnityEngine;
using FairyGUI;
using System.Collections.Generic;
using UI_Guild;
using System;


public class CreateGuildWnd : BaseWindow
{
    private UI_CreateGuildWnd m_window;
    private int m_curBadge = 1;
    public override void OnOpen()
    {
        base.OnOpen();
        m_window = getUiWindow<UI_CreateGuildWnd>();
        m_window.m_objBadge.onClick.Add(_OnBadgeClick);
        //m_window.m_c1.onChanged.Add(_OnControlChange);
        _ShowBaseInfo();

    }

    private void _ShowBaseInfo()
    {
        int needLevel = ConfigBean.GetBean<t_globalBean, int>(1601000).t_int_param;
        int comsumeDimondNum = ConfigBean.GetBean<t_globalBean, int>(1601001).t_int_param;
        m_window.m_txtCondition.text = string.Format("需战队等级达到{0}", needLevel);
        m_window.m_txtDiamiondNum.text = comsumeDimondNum + "";
        _ShowBadgeIcon();

        m_window.m_btnCreate.onClick.Add(()=>{
            int level = RoleService.Singleton.GetRoleInfo().level;
            int haveDiamond =(int)RoleService.Singleton.GetCurrencyNum((int)RoleService.ECurrencyType.DIAMOND);
            int nameLength = ConfigBean.GetBean<t_globalBean, int>(1601002).t_int_param;

            if (level < needLevel)
            {
                TipWindow.Singleton.ShowTip(string.Format("创建公会需{0}级", needLevel));
                return;
            }

            if (m_window.m_txtGuildName.text.Length > nameLength || m_window.m_txtGuildName.text.Equals(""))
            {
                TipWindow.Singleton.ShowTip("公会名字长度不符");
                return;
            }

            if (haveDiamond < comsumeDimondNum)
            {
                TipWindow.Singleton.ShowTip("宝石数量不足");
                return;
            }

            GuildService.Singleton.ReqCreate(m_window.m_txtGuildName.text, m_curBadge, m_window.m_c1.selectedIndex);

        });
    }

    private void _ShowBadgeIcon()
    {
        t_iconBean iconBean = ConfigBean.GetBean<t_iconBean, int>(m_curBadge);
        if (iconBean != null)
            UIGloader.SetUrl(m_window.m_objBadge.m_imgBadge, iconBean.t_icon);
           // m_window.m_objBadge.m_imgBadge.url = iconBean.t_icon;
    }



    private void _OnBadgeClick()
    {
        OneParam<Action<int>> oneParam = new OneParam<Action<int>>();
        oneParam.value = _OnCallBack;

        WinMgr.Singleton.Open<ChangeBadgeWnd>(WinInfo.Create(false, null, true, oneParam));
    }


    private void _OnCallBack(int id)
    {
        m_curBadge = id;
        _ShowBadgeIcon();
    }

    protected override void OnClose()
    {
        base.OnClose();
    }
}