using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_Equip;
using Message.Bag;
using Message.Role;
using Data.Beans;

public class DiamondBuyMaterialWindow : BaseWindow {

    private int needDiamond;

    UI_DiamondBuyMaterialWindow window;
    List<ItemInfo> itemInfoList;

    public override void OnOpen()
    {
        base.OnOpen();

        window = getUiWindow<UI_DiamondBuyMaterialWindow>();
        itemInfoList = Info.param as List<ItemInfo>;

        InitView();
    }

    public override void InitView()
    {
        base.InitView();

        window.m_mask.onClick.Add(OnCloseBtn);
        window.m_confirmBtn.onClick.Add(OnConfirmBtnClick);

        needDiamond = GetBuyMaterialDiamond();
        string color = IsEnoughDiamond() ? "FFFFFF" : "FF0000";
        window.m_tipLabel.text = string.Format("是否花费<img src='{0}' width='20' height='20'/>[color=#{1}]{2}[/color]购买所缺材料并进行升品？", "ui://UI_Common/_common_12", color, needDiamond);
    }

    public override void AddEventListener()
    {
        base.AddEventListener();

        GED.ED.addListener(EventID.OnPetShuXingChanged,OnPetShuXingChanged);
    }

    public override void RemoveEventListener()
    {
        base.RemoveEventListener();

        GED.ED.removeListener(EventID.OnPetShuXingChanged, OnPetShuXingChanged);
    }

    private void OnPetShuXingChanged(GameEvent evt)
    {
        OnCloseBtn();
    }

    private void OnConfirmBtnClick()
    {
        if (IsEnoughDiamond())
        {
            //钻石足够 显示升品成功界面，然后关闭当前界面
            OnCloseBtn();
        }
        else
        {
            // TODO : 进入充值界面充值
        }
    }

    #region 数据-------------------------------------------------------------

    /// <summary>
    /// 获得购买所缺材料所花费的钻石
    /// </summary>
    /// <returns></returns>
    private int GetBuyMaterialDiamond()
    {
        int count = itemInfoList.Count;
        int diamond = 0;
        t_itemBean itemBean = null;
        ItemInfo itemInfo = null;
        for (int i = 0; i < count; i++)
        {
            itemInfo = itemInfoList[i];
            itemBean = ConfigBean.GetBean<t_itemBean, int>(itemInfo.id);
            if (itemBean != null)
            {
                diamond += int.Parse(itemBean.t_value) * itemInfo.num;
            }
        }

        return diamond;
    }

    private bool IsEnoughDiamond()
    {
        RoleInfo roleInfo = RoleService.Singleton.GetRoleInfo();
        return roleInfo.damond >= needDiamond;
    }

    #endregion;


    protected override void OnCloseBtn()
    {

        window = null;
        itemInfoList = null;

        base.OnCloseBtn();
    }
}
