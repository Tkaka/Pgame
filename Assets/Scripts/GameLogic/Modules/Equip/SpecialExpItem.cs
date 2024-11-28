using UnityEngine;
using UI_Equip;
using FairyGUI;
using Data.Beans;
using Message.Role;
using Message.Pet;

public class SpecialExpItem : UI_expItem {

    public int itemID;
    private int useNum;
    private int perPrice;
    private int expValue;
    private bool isLongPress;

    private EquipStrengthPanel parentUI;
    private Message.Bag.GridInfo gridInfo;
    private LongPressGesture longPressGesture;
    public new static UI_expItem CreateInstance()
    {
        return (UI_expItem)UIPackage.CreateObject("UI_Equip", "expItem");
    }

    public void Init(EquipStrengthPanel parentUI)
    {
        this.parentUI = parentUI;
        useNum = 0;
        RefreshItem();
        BindEvent();
    }

    private void BindEvent()
    {
        m_itemToucher.onClick.Add(OnClickItem);

        // 添加长按手势
        longPressGesture = new LongPressGesture(m_itemToucher);
        longPressGesture.trigger = 1;
        longPressGesture.interval = 0.01f;
        longPressGesture.onAction.Add(LongPressOnAction);
        longPressGesture.onEnd.Add(LongPressEnd);
    }

    public void RefreshItem()
    {
        t_itemBean itemBean = ConfigBean.GetBean<t_itemBean, int>(itemID);
        if (itemBean == null)
            return;

        string[] valueStrArr = itemBean.t_value.Split('+');
        perPrice = int.Parse(valueStrArr[1]);
        expValue = int.Parse(valueStrArr[0]);
        gridInfo = parentUI.equipData.GetSpecialExpGridInfoByID(itemID);
        int haveNum = gridInfo == null ? 0 : gridInfo.itemInfo.num;
        UIGloader.SetUrl(m_iconLoader, itemBean.t_icon);
        m_iconLoader.grayed = haveNum == 0;
        m_noHaveGroup.visible = haveNum == 0;
        m_numLabel.text = haveNum == 0 ? "" : haveNum.ToString();
        UIGloader.SetUrl(m_boardLoader, UIUtils.GetItemBorder(itemID));
        m_expValueLabel.text = string.Format("经验值+{0}", expValue);
        m_goldLabel.text = perPrice.ToString();

        RoleInfo roleInfo = RoleService.Singleton.GetRoleInfo();
        m_goldLabel.color = roleInfo.gold >= perPrice ? Color.white : Color.red;
    }

    private bool IsEnoughGold
    {
        get
        {
            RoleInfo roleInfo = RoleService.Singleton.GetRoleInfo();
            return roleInfo.gold >= perPrice;
        }
    }

    private void OnClickItem()
    {
        if (gridInfo == null || gridInfo.itemInfo.num <= 0 || isLongPress == true)
        {
            if (isLongPress)
            {
                isLongPress = !isLongPress;
            }
            else
            {
                TwoParam<int, int> twoParan = new TwoParam<int, int>();
                twoParan.value1 = itemID;
                twoParan.value2 = -1;
                WinInfo winInfo = new WinInfo();
                winInfo.param = twoParan;
                WinMgr.Singleton.Open<LaiYuanWindow>(winInfo, UILayer.Popup);
            }
            
        }
        else
        {
            if (IsEnoughGold)
            {
                parentUI.equipData.ReqSpecialEquipUpgrade(itemID, 1);
                gridInfo.itemInfo.num--;
                parentUI.RefreshSpecialExpBar(expValue);
            }
            else
            {
                TipWindow.Singleton.ShowTip("金币不足!");
            }
        }
    }

    private void LongPressOnAction()
    {
        isLongPress = true;
        if (gridInfo == null || useNum == -1)
        {
            return;
        }

        PetEquip petEquip = parentUI.equipData.GetCurSelectPetEquip();
        t_special_equip_lvcolorupBean specialEquipLvColorUpBean = parentUI.equipData.GetSpecialEquipLvColorUpBean(petEquip.color);
        if (specialEquipLvColorUpBean == null)
        {
            return;
        }

        if (petEquip.level >= specialEquipLvColorUpBean.t_lv_max)
        {
            parentUI.equipData.ReqSpecialEquipUpgrade(itemID, useNum);
            useNum = -1;
            Logger.log("达到上限了");
            //达到等级上限了，转换成升品界面
            return;
        }

        if (gridInfo.itemInfo.num > 0)
        {
            gridInfo.itemInfo.num--;
            useNum++;
            parentUI.RefreshLongPressExpBar(expValue, useNum);
        }
        else
        {
            parentUI.equipData.ReqSpecialEquipUpgrade(itemID, useNum);
            useNum = -1;
        }
    }

    private void LongPressEnd()
    {
        if (useNum == -1)
        {
            useNum = 0;
            return;
        }
        parentUI.equipData.ReqSpecialEquipUpgrade(itemID, useNum);
        useNum = 0;
    }

    public override void Dispose()
    {
        base.Dispose();

        parentUI = null;
        gridInfo = null;
        longPressGesture = null;
    }
}
