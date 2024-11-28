using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_Equip;
using FairyGUI;
using Data.Beans;
using Message.Pet;

public class QuickUpgradeItem : UI_quickUpgradeItem {

    public int itemID;
    int haveNum;
    int needNum;

    EquipDataManager equipData;

    public new static UI_quickUpgradeItem CreateInstance()
    {
        return (UI_quickUpgradeItem)UIPackage.CreateObject("UI_Equip", "quickUpgradeItem");
    }

    public void Init(QuickUpgradeWindow parentUI)
    {
        equipData = parentUI.EquipData;
        m_itemToucher.onClick.Add(OnItemClick);
        InitView();
    }

    private void InitView()
    {


        switch (equipData.CurSelectEquipPos)
        {
            case EquipPosition.Weapon:
            case EquipPosition.Cloth:
            case EquipPosition.KuZi:
            case EquipPosition.Shoes:
                haveNum = equipData.GetMaterialHaveNum(itemID);
                needNum = equipData.GetMaterialNeedNum(itemID);
                break;
            case EquipPosition.HuiZhan:
            case EquipPosition.MiJi:
                Message.Bag.GridInfo gridInof = equipData.GetSpecialExpGridInfoByID(itemID);
                haveNum = gridInof == null ? 0 : gridInof.itemInfo.num;
                if (equipData.QuickUpgradeUseExpItemDict.ContainsKey(itemID))
                {
                    needNum = equipData.QuickUpgradeUseExpItemDict[itemID];
                }
                break;
            default:
                break;
        }

        //if (needNum == 0)
        //{
        //    this.visible = false;
        //}

        t_itemBean itemBean = ConfigBean.GetBean<t_itemBean, int>(itemID);
        PetEquip petEquip = equipData.GetCurSelectPetEquip();
        UIGloader.SetUrl(m_iconLoader, itemBean.t_icon);
        if (!string.IsNullOrEmpty(itemBean.t_quality))
            UIGloader.SetUrl(m_boardLoader, UIUtils.GetItemBorder(itemID));

        m_numLabel.text = string.Format("{0}/{1}", haveNum, needNum);
        m_numLabel.color = (haveNum >= needNum && haveNum > 0) ? Color.white : Color.red;
        m_iconLoader.grayed = haveNum < needNum;
        m_unFullGroup.visible = haveNum < needNum || haveNum == 0;
    }

    private void OnItemClick()
    {
        if (haveNum < needNum)
        {
            // 显示来源界面
            int number = equipData.GetMaterialNeedNum(itemID);
            TwoParam<int, int> twoParam = new TwoParam<int, int>();
            twoParam.value1 = itemID;
            twoParam.value2 = number;
            WinMgr.Singleton.Open<LaiYuanWindow>(WinInfo.Create(false, null, false, twoParam), UILayer.Popup);
        }
    }
}
