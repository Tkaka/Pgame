using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_MainCity;
using Data.Beans;

public class HeadListItem :  UI_headListItem{

    private bool isGeted;
    private List<t_headBean> headBeanList;
    private float oldListHeight;

    public new static HeadListItem CreateInstance()
    {
        return UI_headListItem.CreateInstance() as HeadListItem;
    }

    public void Init(bool isGeted, List<t_headBean> headBeanList)
    {
        this.isGeted = isGeted;
        this.headBeanList = headBeanList;
        oldListHeight = m_headIconList.height;
        InitView();
    }

    private void InitView()
    {
        if (isGeted)
            m_tip.text = "基础头像";
        else
            m_tip.text = "未获得头像";

        InitHeadIconList();
    }

    private void InitHeadIconList()
    {
        int count = headBeanList.Count;
        if (count == 0)
            return;

        m_headIconList.RemoveChildren(0, -1, true);
        CommonHeadIcon headIcon = null;
        for (int i = 0; i < count; i++)
        {
            headIcon = CommonHeadIcon.CreateInstance();
            if(isGeted)
                headIcon.Init(headBeanList[i].t_id, OnGetedHeadIconClick, isGeted);
            else
                headIcon.Init(headBeanList[i].t_id, OnUnGetedHeadIconClick, isGeted);
            m_headIconList.AddChild(headIcon);
        }
        m_headIconList.ResizeToFit(count);
        m_headIconList.scrollPane.touchEffect = false;

        float addHeight = m_headIconList.height - oldListHeight;
        m_bg.height += addHeight;
        this.height += addHeight;
    }

    private void OnGetedHeadIconClick(object obj)
    {
        // 请求更换头像
        t_headBean headBean = obj as t_headBean;
        RoleService.Singleton.ReqModifyIcon(headBean.t_id);
    }

    private void OnUnGetedHeadIconClick(object obj)
    {
        // 打开解锁头像条件界面
        WinMgr.Singleton.Open<HeadIconUnlockTipWindow>(WinInfo.Create(false, null, false, obj), UILayer.Popup);
    }
}
