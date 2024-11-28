using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_CloneTeamFight;
using Data.Beans;

public class CloneWinBoxItem {

    UI_cloneWinBoxItem view;
    bool isClick = false;
    private CloneWinWindow parentUI;
    private int m_index = 0;   //下标 左是0 右是1

	public CloneWinBoxItem(UI_cloneWinBoxItem view, CloneWinWindow parentUI, int index)
    {
        this.view = view;
        this.parentUI = parentUI;
        this.m_index = index;
        InitView();
        RefreshView();
    }

    private void InitView()
    {
        view.m_toucher.onClick.Add(OnClickItem);
    }

    public void RefreshView()
    {
        if (isClick)
            UIGloader.SetUrl(view.m_boxIcon , UIUtils.GetLoaderUrl(WinEnum.UI_CloneTeamFight, "openBox"));
        view.m_costDiamondGroup.visible = IsShowCostGroup();
        view.m_costDiamondLabel.text = GetOpenBoxDiamond() + "";
    }

    private void OnClickItem()
    {
        if (isClick == false && parentUI.coroutineID == -1)
        {
            if (IsEnoughDiamond())
            {
                isClick = true;
                CloneTeamFightService.Singleton.ReqTeamFightOpenBox(m_index);
            }
            else
            {
                TipWindow.Singleton.ShowTip("钻石不足");
            }
            
        }
    }
    /// <summary>
    /// 钻石是否足够
    /// </summary>
    /// <returns></returns>
    private bool IsEnoughDiamond()
    {
        if (parentUI.count == 0)
            return true;
        Message.Role.RoleInfo roleInfo = RoleService.Singleton.GetRoleInfo();
        return roleInfo.damond >= GetOpenBoxDiamond();
    }
    /// <summary>
    /// 宝箱开启需要的钻石
    /// </summary>
    /// <returns></returns>
    private int GetOpenBoxDiamond()
    {
        // 1803003 组队宝箱开启消耗的最是数量
        t_globalBean globalBean = ConfigBean.GetBean<t_globalBean, int>(1803003);
        if (globalBean != null)
            return globalBean.t_int_param;

        return int.MaxValue;
    }

    private bool IsShowCostGroup()
    {
        // 打开一个，没打开的那个显示消耗的钻石
        if (parentUI.count == 1 && isClick == false)
        {
            return true;
        }

        return false;
    }
}
