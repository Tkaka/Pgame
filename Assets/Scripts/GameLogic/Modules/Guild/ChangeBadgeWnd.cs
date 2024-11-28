using UI_Guild;
using Message.Guild;
using Data.Beans;
using UnityEngine;
using FairyGUI;
using System.Collections.Generic;
using System;

public class ChangeBadgeWnd : BaseWindow
{
    private UI_ChangeBadgeWnd m_window;
    private Action<int> m_callBack;
    public override void OnOpen()
    {
        base.OnOpen();
        m_window = getUiWindow<UI_ChangeBadgeWnd>();
        m_window.m_btnClose.onClick.Add(Close);
        OneParam<Action<int>> oneParam = Info.param as OneParam<Action<int>>;
        if (oneParam != null)
            m_callBack = oneParam.value as Action<int>;

        _ShowBadgeList();
    }


    private void _ShowBadgeList()
    {
        m_window.m_badgeList.RemoveChildren(0, -1, true);
        List<t_iconBean> beanList = ConfigBean.GetBeanList<t_iconBean>();
        for (int i = 0; i < beanList.Count; i++)
        {
            t_iconBean iconBean = beanList[i];
            UI_badgeCell cell = UI_badgeCell.CreateInstance();
            //cell.m_imgIcon.url = iconBean.t_icon;
            UIGloader.SetUrl(cell.m_imgIcon, iconBean.t_icon);
            m_window.m_badgeList.AddChild(cell);
            cell.onClick.Add(() =>
            {
                if (m_callBack != null)
                    m_callBack(iconBean.t_id);
                //GuildService.Singleton.ReqChangeBadge(iconBean.t_id);
                Close();
            });
        }
    }

    protected override void OnClose()
    {
        base.OnClose();
    }
}