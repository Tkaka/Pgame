using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_UltemateTrain;
using Message.Challenge;
using Message.Bag;
using Data.Beans;

public class SecretBoxWindow : BaseWindow {

    UI_SecretBoxWindow window;
    
    private bool isClick = false;
    IntVsInt boxInfo;
    List<IntVsInt> itemList = new List<IntVsInt>();

    public override void OnOpen()
    {
        base.OnOpen();

        window = getUiWindow<UI_SecretBoxWindow>();
        window.m_leaveBtn.onClick.Add(OnLeaveBtnClick);
        window.m_buyBtn.onClick.Add(OnBuyBtnClick);
        window.m_mask.onClick.Add(OnMaskClick);
        boxInfo = Info.param as IntVsInt;

        InitView();
        RefreshView();
    }

    public override void InitView()
    {
        base.InitView();

        window.m_itemList.visible = false;
        window.m_mask.touchable = false;
    }

    public override void RefreshView()
    {
        base.RefreshView();

        RefreshBaseInfo();
        ShowItemList();
        RefreshBtnState();
    }

    private void RefreshBaseInfo()
    {
        Message.Role.RoleInfo roleInfo = RoleService.Singleton.GetRoleInfo();
        window.m_remainDiamondLabel.text = roleInfo.damond + "";
        int maxTiems = UltemateTrainService.Singleton.GetSecretMaxOpenTimes();
        window.m_remainTimes.text = string.Format("{0}/{1}", maxTiems- boxInfo.int2, maxTiems);

        int consumeDiamond = GetConsumeDiamond();
        window.m_comsumeDiamondLabel.text = consumeDiamond + "";
    }

    private void RefreshItemList()
    {
        window.m_itemList.RemoveChildren(0, -1, true);
        int count = itemList.Count;
        CommonItem commonItem = null;
        IntVsInt itemInfo = null;
        for (int i = 0; i < count; i++)
        {
            commonItem = CommonItem.CreateInstance();
            itemInfo = itemList[i];
            commonItem.itemId = itemInfo.int1;
            commonItem.itemNum = itemInfo.int2;
            commonItem.isShowNum = true;
            commonItem.AddPopupEvent();
            commonItem.RefreshView(true);

            window.m_itemList.AddChild(commonItem);
        }
    }

    private void ShowItemList()
    {
        if (isClick)
        {
            window.m_itemList.visible = true;
            window.m_mask.touchable = true;
            RefreshItemList();
        }
    }

    private void RefreshBtnState()
    {
        if (isClick)
        {
            window.m_buyBtn.text = "继续购买";
        }
    }

    public override void AddEventListener()
    {
        base.AddEventListener();

        GED.ED.addListener(EventID.OnResTrialSingleBoxOpen, OnResTrialBoxOpen);
    }

    public override void RemoveEventListener()
    {
        base.RemoveEventListener();

        GED.ED.removeListener(EventID.OnResTrialSingleBoxOpen, OnResTrialBoxOpen);
    }

    private void OnBuyBtnClick()
    {
        // 判断是否能够购买
        if (IsHaveTimes())
        {
            if (IsEnoughDiamond())
            {
                isClick = true;
                // 请求宝箱开启
                UltemateTrainService.Singleton.ReqUltemateTrialBoxOpen(boxInfo.int1, 1);
            }
            else
            {
                TipWindow.Singleton.ShowTip("钻石不足");
            }

        }
        else
        {
            TipWindow.Singleton.ShowTip("次数以用完！");
        }
    }

    private void OnMaskClick()
    {
        window.m_itemList.visible = false;
        window.m_mask.touchable = false;
    }

    private void OnLeaveBtnClick()
    {
        UltemateTrainService.Singleton.trainInfo.trialInfo.floor++;
        GED.ED.dispatchEvent(EventID.OnLeaveSecretBoxWindow);
        OnCloseBtn();
    }

    private bool IsHaveTimes()
    {
        int maxTimes = UltemateTrainService.Singleton.GetSecretMaxOpenTimes();
        return boxInfo.int2 < maxTimes;
    }

    private bool IsEnoughDiamond()
    {
        int consumeDiamond = GetConsumeDiamond();
        Message.Role.RoleInfo roleInfo = RoleService.Singleton.GetRoleInfo();
        return roleInfo.damond >= consumeDiamond;
    }
    /// <summary>
    /// 获得本次购买需要消耗的钻石
    /// </summary>
    /// <returns></returns>
    private int GetConsumeDiamond()
    {
        int consumeDiamond = 0;
        t_globalBean globalBean = ConfigBean.GetBean<t_globalBean, int>(1801001);
        if (globalBean != null && !string.IsNullOrEmpty(globalBean.t_string_param))
        {
            string[] priceArr = globalBean.t_string_param.Split('+');
            int length = priceArr.Length;
            int index = boxInfo.int2 >= length ? length - 1 : boxInfo.int2;
            consumeDiamond = int.Parse(priceArr[index]);
        }

        return consumeDiamond;
    }

    private void OnResTrialBoxOpen(GameEvent evt)
    {
        ResTrialSingleBoxOpen msg = evt.Data as ResTrialSingleBoxOpen;
        boxInfo.int1 = msg.diamondBoxInfo.int1;
        boxInfo.int2 = msg.diamondBoxInfo.int2;
        itemList.Clear();
        itemList.AddRange(msg.rewards);

        RefreshView();
    }

    protected override void OnCloseBtn()
    {
        itemList.Clear();
        itemList = null;

        base.OnCloseBtn();
    }
}
