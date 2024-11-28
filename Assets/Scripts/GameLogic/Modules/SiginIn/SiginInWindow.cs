using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_SiginIn;
using Data.Beans;

public class SiginInWindow : BaseWindow {

    UI_SiginInWindow window;

    public override void OnOpen()
    {
        base.OnOpen();

        window = getUiWindow<UI_SiginInWindow>();
        window.m_mask.onClick.Add(OnCloseBtn);
        window.m_popupView.m_closeBtn.onClick.Add(OnCloseBtn);

        InitView();
        PlayPopupAnim(window.m_mask, window.m_popupView);
    }

    public override void InitView()
    {
        base.InitView();

        window.m_popupView.m_getBtn.onClick.Add(OnClickLeiJiGetBtn);
        InitRewardList();
        RefreshLeiJiRewardView();
        RefreshLeiJiTip();
    }

    public override void RefreshView()
    {
        base.RefreshView();


        RefreshLeiJiTip();
        RefreshLeiJiRewardView();
        RefreshRewardList();
    }

    private void RefreshLeiJiTip()
    {
        Message.Role.RoleInfo roleInfo = RoleService.Singleton.GetRoleInfo();
        window.m_popupView.m_leiJiSiginUpTip.text = string.Format("本月累计签到{0}次", roleInfo.monthSignIn);
    }

    public override void AddEventListener()
    {
        base.AddEventListener();

        GED.ED.addListener(EventID.OnResSignIn, OnResSignIn);
        GED.ED.addListener(EventID.OnResSignInBox, OnResSignInBox);
    }

    public override void RemoveEventListener()
    {
        base.RemoveEventListener();

        GED.ED.removeListener(EventID.OnResSignIn, OnResSignIn);
        GED.ED.removeListener(EventID.OnResSignInBox, OnResSignInBox);
    }

    private void InitRewardList()
    {
        window.m_popupView.m_siginRewardList.SetVirtual();
        window.m_popupView.m_siginRewardList.itemRenderer = RewardListItemRender;

        List<t_sign_in_monthBean> signInBeanList = ConfigBean.GetBeanList<t_sign_in_monthBean>();
        window.m_popupView.m_siginRewardList.numItems = signInBeanList.Count;
    }

    private void RewardListItemRender(int index, FairyGUI.GObject obj)
    {
        SiginInItem item = obj as SiginInItem;
        item.index = index + 1;

        if (item.isInit == false)
            item.InitView();
        else
            item.RefreshView();
    }

    private void RefreshRewardList()
    {
        window.m_popupView.m_siginRewardList.RefreshVirtualList();
    }

    private void RefreshLeiJiRewardView()
    {
        bool isCanGetLeiJiReard = IsCanGetLeiJiReward();
        window.m_popupView.m_getBtn.visible = isCanGetLeiJiReard;
        window.m_popupView.m_unGetBtn.visible = !isCanGetLeiJiReard;

        // 奖励列表
        Message.Role.RoleInfo roleInfo = RoleService.Singleton.GetRoleInfo();
        t_sign_in_totalBean signInTotalBean = ConfigBean.GetBean<t_sign_in_totalBean, int>(roleInfo.signInAwardIndex);
        if (signInTotalBean != null)
        {
            window.m_popupView.m_leiJiRewardTip.text = string.Format("{0}/{1}", roleInfo.totalSignIn, signInTotalBean.t_day);

            string[] itemArr = GTools.splitString(signInTotalBean.t_items, ';');
            if (itemArr.Length > 0)
            {
                int count = itemArr.Length;
                CommonItem item = null;
                int[] itemInfo = null;
                window.m_popupView.m_timesRewardList.RemoveChildren(0, -1, true);
                for (int i = 0; i < count; i++)
                {
                    itemInfo = GTools.splitStringToIntArray(itemArr[i]);
                    if (itemInfo.Length == 2)
                    {
                        item = CommonItem.CreateInstance();
                        item.itemId = itemInfo[0];
                        item.itemNum = itemInfo[1];
                        item.isShowNum = true;
                        item.AddPopupEvent();
                        item.RefreshView();
                        item.SetScale(0.8f, 0.8f);

                        window.m_popupView.m_timesRewardList.AddChild(item);
                    }
                }

                if (item != null)
                    window.m_popupView.m_timesRewardList.lineGap = (int)(-0.2f * item.width) + 5;
            }
        }
    }
    /// <summary>
    /// 是否能领取累计签到奖励
    /// </summary>
    /// <returns></returns>
    private bool IsCanGetLeiJiReward()
    {
        Message.Role.RoleInfo roleInfo = RoleService.Singleton.GetRoleInfo();
        t_sign_in_totalBean signInTotalBean = ConfigBean.GetBean<t_sign_in_totalBean, int>(roleInfo.signInAwardIndex);
        if (signInTotalBean != null)
        {
            return signInTotalBean.t_day <= roleInfo.totalSignIn;
        }

        return false;
    }
    /// <summary>
    /// 领取累计签到奖励
    /// </summary>
    /// <param name="evt"></param>
    private void OnResSignInBox(GameEvent evt)
    {
        RefreshView();
    }
    /// <summary>
    /// 领取签到奖励
    /// </summary>
    /// <param name="evt"></param>
    private void OnResSignIn(GameEvent evt)
    {
        RefreshView();
        ThreeParam<bool, List<Message.Bag.ItemInfo>, string> param = new ThreeParam<bool, List<Message.Bag.ItemInfo>, string>();
        param.value1 = false;
        param.value2 = evt.Data as List<Message.Bag.ItemInfo>;

        Message.Role.RoleInfo roleInfo = RoleService.Singleton.GetRoleInfo();
        int signInID = RoleService.Singleton.GetSignBeanIndex();
        t_sign_in_monthBean signInBean = ConfigBean.GetBean<t_sign_in_monthBean, int>(signInID);
        if (signInBean != null && signInBean.t_vip_double != 0)
            param.value3 = string.Format("贵族{0}能够获得双倍奖励", signInBean.t_vip_double);

        WinMgr.Singleton.Open<BoxReceiveWidow>(WinInfo.Create(false, null, false, param), UILayer.Popup);
    }

    private void OnClickLeiJiGetBtn()
    {
        RoleService.Singleton.ReqSignInBox();
    }
}
