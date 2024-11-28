using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_Strength;
using FairyGUI;
using Data.Beans;

public class ZhanHunMaterialItem : UI_zhanHunCaiLiaoItem {

    public int gridID;
    public ZhanHunStrengthWindow parentWindow;
    private int _maxNum;

    public new static UI_zhanHunCaiLiaoItem CreateInstance()
    {
        return (UI_zhanHunCaiLiaoItem)UIPackage.CreateObject("UI_Strength", "zhanHunCaiLiaoItem");
    }

    public void Init(ZhanHunStrengthWindow parentWindow)
    {
        this.parentWindow = parentWindow;
        Message.Bag.GridInfo gridInfo = BagService.Singleton.GetGrid(gridID);
        if (gridInfo == null)
            return;

        RefreshItem();
    }

    public void RefreshItem()
    {
        Message.Bag.GridInfo gridInfo = BagService.Singleton.GetGrid(gridID);
        if (gridInfo == null)
            return;
        t_itemBean itemBean = ConfigBean.GetBean<t_itemBean, int>(gridInfo.itemInfo.id);
        int num = parentWindow.DataManager.GetSelectItemNum(gridID);
        _maxNum = gridInfo.itemInfo.num;
        m_numLabel.text = string.Format("{0}/{1}", num, _maxNum);
        m_itemIconLoader.grayed = num >= _maxNum;
        UIGloader.SetUrl(m_boardLoader, UIUtils.GetItemBorder(gridInfo.itemInfo.id));
        UIGloader.SetUrl(m_itemIconLoader, itemBean.t_icon);
        m_selectIcon.visible = gridID == parentWindow.DataManager.curSelectGridID;

        m_fullUseIcon.visible = IsShowUseIcon(num);
    }

    private bool IsShowUseIcon(int num)
    {
        return num >= 1;
    }
}
