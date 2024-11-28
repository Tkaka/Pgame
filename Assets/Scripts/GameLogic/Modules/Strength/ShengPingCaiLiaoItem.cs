using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_Strength;
using FairyGUI;
using Message.Bag;
using Data.Beans;

public class ShengPingCaiLiaoItem :  UI_caiLiaoItem{

    private int _index;
    StrengthShengPing _parentUI;
    private int needNum;
    private int itemID;

    public new static UI_caiLiaoItem CreateInstance()
    {
        return (ShengPingCaiLiaoItem)UIPackage.CreateObject("UI_Strength", "caiLiaoItem");
    }

    public void RefreshView(StrengthShengPing parentUI, int index)
    {
        _parentUI = parentUI;
        _index = index;

        var gridInfo = _parentUI.StrengthData.GetGridInfoByIndex(_index);
        var petInfo = _parentUI.StrengthData.CurSelectPetInfo;
        itemID = _parentUI.StrengthData.GetItemIDByIndex(_index);
        var itemBean = ConfigBean.GetBean<t_itemBean, int>(itemID);
        if (itemBean != null)
        {
            UIGloader.SetUrl(m_borderBg, UIUtils.GetItemBorder(itemID));
        }

        int haveNum = 0;
        needNum = _parentUI.StrengthData.GetNumByIndex(_index);

        if (gridInfo != null)
        {
            haveNum = gridInfo == null ? 0 : gridInfo.itemInfo.num;
            m_unFullGroup.visible = haveNum < needNum;
        }

        m_numLabel.text = string.Format("{0}/{1}", haveNum, needNum);
        m_numLabel.color = haveNum >= needNum ? Color.white : Color.red;
        UIGloader.SetUrl(m_caiLiaoIcon, itemBean.t_icon);
        // 不变灰
        //m_caiLiaoIcon.grayed = haveNum < needNum;

        m_itemBtn.onClick.Add(OnItemClick);
    }

    /// <summary>
    /// 材料是否满足
    /// </summary>
    public bool IsFull
    {
        get
        {
            var gridInfo = _parentUI.StrengthData.GetGridInfoByIndex(_index);
            if (gridInfo != null)
            {
                var num = _parentUI.StrengthData.GetNumByIndex(_index);
                if (num <= gridInfo.itemInfo.num)
                {
                    return true;
                }
            }

            return false;
        }
    }

    private void OnItemClick()
    {
        TwoParam<int, int> param = new TwoParam<int, int>();
        param.value1 = _parentUI.StrengthData.GetItemIDByIndex(_index);
        param.value2 = needNum;
        WinMgr.Singleton.Open<LaiYuanWindow>(WinInfo.Create(false, null, false, param), UILayer.Popup);
    }
}
