using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_TongXiangGuan;
using FairyGUI;
using Data.Beans;
public class TongXiangGuanWindow : BaseWindow {

    UI_TongXiangGuanWindow window;
    private int pageCount;
    private float pageWidth;
    private bool isChangePage = false;
    public override void OnOpen()
    {
        base.OnOpen();

        window = getUiWindow<UI_TongXiangGuanWindow>();

        TongXiangGuanServices.Singleton.curPage = 0;
        pageWidth = window.m_tongXiangPageList.scrollPane.viewWidth;
        window.m_tongXiangPageList.scrollPane.bouncebackEffect = false;
        window.m_tongXiangPageList.scrollPane.mouseWheelEnabled = false;

        BindEvent();
        InitView();
        RefreshView();
    }

    private void BindEvent()
    {
        window.m_backBtn.onClick.Add(OnCloseBtn);
        window.m_switchLeftBtn.onClick.Add(OnSwitchLeftBtnClick);
        window.m_switchRightBtn.onClick.Add(OnSwitchRightBtnClick);
        window.m_ruleBtn.onClick.Add(OnRuleBtnClick);

        window.m_tongXiangPageList.scrollPane.onScrollEnd.Add(OnScrollEnd);
        window.m_tongXiangPageList.scrollPane.onScroll.Add(OnScroll);
    }

    public override void AddEventListener()
    {
        base.AddEventListener();

        GED.ED.addListener(EventID.OnResExhibitionInfo, OnResExhibitionInfo);
        GED.ED.addListener(EventID.OnExhibitionInfoChange, OnExhibitionInfoChanged);
        GED.ED.addListener(EventID.OnResExhibitionRoomInfo, OnResExhibitionRoom);
    }

    public override void RemoveEventListener()
    {
        base.RemoveEventListener();

        GED.ED.removeListener(EventID.OnResExhibitionInfo, OnResExhibitionInfo);
        GED.ED.removeListener(EventID.OnExhibitionInfoChange, OnExhibitionInfoChanged);
        GED.ED.removeListener(EventID.OnResExhibitionRoomInfo, OnResExhibitionRoom);
    }

    public override void InitView()
    {
        base.InitView();

        InitPageList();
    }

    private void InitPageList()
    {
        List<t_exhibitionBean> exhibitionBeanList = ConfigBean.GetBeanList<t_exhibitionBean>();
        pageCount = Mathf.CeilToInt(exhibitionBeanList.Count / 4.0f);
        TongXiangGuanPage page = null;
        window.m_tongXiangPageList.RemoveChildren(0, -1, true);
        for (int i = 0; i < pageCount; i++)
        {
            page = TongXiangGuanPage.CreateInstance();
            page.SetSize(GRoot.inst.width, GRoot.inst.height);
            page.AddRelation(window, RelationType.Size);
            page.pageIndex = i;
            page.Init();
            window.m_tongXiangPageList.AddChild(page);
        }
    }

    public override void RefreshView()
    {
        base.RefreshView();

        RefreshSwitchBtn();
        RefreshBaseInfo();
        RefreshPage();
    }

    private void RefreshPage()
    {
        // 只刷新当前的page
        TongXiangGuanPage curPage = window.m_tongXiangPageList.GetChildAt(TongXiangGuanServices.Singleton.curPage) as TongXiangGuanPage;
        curPage.RefreshView();
    }

    private void RefreshBaseInfo()
    {
        window.m_totalValueLabel.text = TongXiangGuanServices.Singleton.totalVlaue + "";
    }

    private void RefreshSwitchBtn()
    {
        window.m_switchLeftBtn.visible = TongXiangGuanServices.Singleton.curPage > 0;
        window.m_switchRightBtn.visible = TongXiangGuanServices.Singleton.curPage < pageCount - 1;
    }
    /// <summary>
    /// 显示当前选中的是第几个页面
    /// </summary>
    private void SetPageTipList()
    {
        window.m_pageTipList.RemoveChildren(0, -1, true);
        for (int i = 0; i < pageCount; i++)
        {
            GImage img;
            if (i == TongXiangGuanServices.Singleton.curPage)
                img = UIPackage.CreateObject(WinEnum.UI_Common, "UI_TY_tubiao_xing_zise").asImage;

            else
                img = UIPackage.CreateObject(WinEnum.UI_Common, "UI_TY_tubiao_xing_shenhuangse").asImage;

            window.m_pageTipList.AddChild(img);
        }
    }

    private bool IsOpenNextPage()
    {
        int needLv = GetOpenNextPageLv();
        Message.Role.RoleInfo roleInfo = RoleService.Singleton.GetRoleInfo();
        return roleInfo.level >= needLv;
    }

    private int GetOpenNextPageLv()
    {
        // 80605  铜像馆页签开放的等级全局ID
        t_globalBean globalBean = ConfigBean.GetBean<t_globalBean, int>(80605);
        if (globalBean != null)
        {
            int nextPage = TongXiangGuanServices.Singleton.curPage + 1;
            if (!string.IsNullOrEmpty(globalBean.t_string_param))
            {
                string[] strArr = globalBean.t_string_param.Split('+');
                if (strArr.Length > nextPage)
                {
                    return int.Parse(strArr[nextPage]);
                }
            }
        }

        return int.MaxValue;
    }

    #region   事件响应 -------------------------------------------------------------------------------------------------

    private void OnSwitchLeftBtnClick()
    {
        window.m_tongXiangPageList.scrollPane.ScrollLeft(1, true);
        isChangePage = true;
    }

    private void OnSwitchRightBtnClick()
    {
        if (!IsOpenNextPage())
        {
            isChangePage = false;
            // 提示下一页开放等级
            string tipStr = string.Format("下一个展区{0}级开放", GetOpenNextPageLv());
            TipWindow.Singleton.ShowTip(tipStr);
        }
        else
        {
            window.m_tongXiangPageList.scrollPane.ScrollRight(1, true);
            TongXiangGuanServices.Singleton.curPage++;
            SetPageTipList();
            RefreshSwitchBtn();

            isChangePage = false;
        }
    }

    private void OnRuleBtnClick()
    {
        // 打开规则界面
        WinMgr.Singleton.Open<TongXiangRuleWindow>(null, UILayer.Popup);
    }

    private void OnScroll()
    {
        float curPosX = TongXiangGuanServices.Singleton.curPage * pageWidth;
        if (window.m_tongXiangPageList.scrollPane.posX > curPosX)
        {
            // 向右滑动是需要判断是否解锁
            if (!IsOpenNextPage())
            {
                window.m_tongXiangPageList.scrollPane.posX = curPosX;
                isChangePage = false;
                // 提示下一页开放等级
                string tipStr = string.Format("下一个展区{0}级开放", GetOpenNextPageLv());
                TipWindow.Singleton.ShowTip(tipStr);
            }
            else
            {
                if (isChangePage == false)
                {
                    if (window.m_tongXiangPageList.scrollPane.posX > curPosX + 0.5f * pageWidth)
                    {
                        TongXiangGuanServices.Singleton.curPage++;

                        isChangePage = true;
                    }
                }
            }
        }
        else
        {
            isChangePage = true;
        }
    }

    private void OnScrollEnd()
    {
        if (isChangePage)
        {
            TongXiangGuanServices.Singleton.curPage = (int)(window.m_tongXiangPageList.scrollPane.posX / pageWidth);
            SetPageTipList();
            RefreshSwitchBtn();

            isChangePage = false;
        }
    }

    private void OnResExhibitionInfo(GameEvent evt)
    {
        RefreshBaseInfo();
    }

    private void OnExhibitionInfoChanged(GameEvent evt)
    {
        RefreshView();
    }

    private void OnResExhibitionRoom(GameEvent evt)
    {
        TongXiangGuanPage curPage = window.m_tongXiangPageList.GetChildAt(TongXiangGuanServices.Singleton.curPage) as TongXiangGuanPage;
        curPage.OnResExhibitionRoom();
    }
    #endregion;

    protected override void OnCloseBtn()
    {
        base.OnCloseBtn();
    }
}
