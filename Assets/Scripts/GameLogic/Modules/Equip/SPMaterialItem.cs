using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_Equip;
using FairyGUI;
using Data.Beans;

public class SPMaterialItem : UI_equipPropItem {

    public int itemID;

    private EquipStrengthPanel parentUI;
    private EquipDataManager equipData
    {
        get { return parentUI.equipData; }
    }


    public new static SPMaterialItem CreateInstance()
    {
        return UI_equipPropItem.CreateInstance() as SPMaterialItem;
    }

    public void Init(EquipStrengthPanel parentUI)
    {
        this.parentUI = parentUI;

        m_itemToucher.onClick.Add(OnClickItem);
        RefreshView();
    }

    public void RefreshView()
    {
        int haveNum = equipData.GetMaterialHaveNum(itemID);
        int needNum = equipData.GetMaterialNeedNum(itemID);

        if (needNum == 0)
        {
            this.visible = false;
            return;
        }

        t_itemBean itemBean = ConfigBean.GetBean<t_itemBean, int>(itemID);
        UIGloader.SetUrl(m_iconLoader, UIUtils.GetItemIcon(itemBean.t_id));
        UIGloader.SetUrl(m_boardLoader, UIUtils.GetItemBorder(itemID));
        m_numLabel.text = string.Format("{0}/{1}", haveNum, needNum);
        m_numLabel.color = haveNum >= needNum ? Color.white : Color.red;

        m_iconLoader.grayed = haveNum < needNum;
        m_unFullGroup.visible = haveNum < needNum || haveNum == 0;
    }

    private void OnClickItem()
    {
        //显示来源界面
        TwoParam<int, int> twoParam = new TwoParam<int, int>();
        twoParam.value1 = itemID;
        twoParam.value2 = equipData.GetMaterialNeedNum(itemID);
        WinMgr.Singleton.Open<LaiYuanWindow>(WinInfo.Create(false, null, false, twoParam), UILayer.Popup);
    }

    public override void Dispose()
    {
        base.Dispose();

        parentUI = null;
    }
}
