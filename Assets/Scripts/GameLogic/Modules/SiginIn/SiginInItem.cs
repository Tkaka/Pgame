using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_SiginIn;
using Data.Beans;

public class SiginInItem : UI_singinInItem {

    public bool isInit;
    public int index;
    Message.Role.RoleInfo roleInfo;

    public new static SiginInItem CreateInstance()
    {
        return UI_singinInItem.CreateInstance() as SiginInItem;
    }

    public void InitView()
    {
        isInit = true;
        m_toucher.onClick.Add(OnClickItem);
        RefreshView();
    }

    public void RefreshView()
    {
        roleInfo = RoleService.Singleton.GetRoleInfo();

        // 刷新道具信息
        CommonItem item = m_item as CommonItem;
        t_sign_in_monthBean signInBean = ConfigBean.GetBean<t_sign_in_monthBean, int>(index);
        if(signInBean != null)
        {
            //  1501512  月签到宠物碎片列表
            int itemID = 0;
            if (signInBean.t_replace == 1)
            {
                t_globalBean globalBean = ConfigBean.GetBean<t_globalBean, int>(1501512);
                if (globalBean != null)
                {
                    int[] itemFragIDArr = GTools.splitStringToIntArray(globalBean.t_string_param);
                    if (itemFragIDArr.Length > 0)
                        itemID = itemFragIDArr[roleInfo.signInPetIndex];
                }
            }
            else
                itemID = signInBean.t_fix_id;
            item.itemId = itemID;
            item.itemNum = signInBean.t_fix_num;
            item.isShowNum = true;
            item.AddPopupEvent();
            item.RefreshView();

            // 显示vip双倍的标记
            if (signInBean.t_vip_double != 0)
            {
                m_vipDoubleGroup.visible = true;
                m_vipNum.text = signInBean.t_vip_double + "";
            }
            else
            {
                m_vipDoubleGroup.visible = false;
            }
        }

        // 刷新提示标记
        if (IsGeted())
        {
            if(IsCanGetAgain())
            {
                m_getAgainTip.visible = true;
                m_getedGroup.visible = false;
                m_toucher.visible = true;
            }
            else
            {
                m_getAgainTip.visible = false;
                m_getedGroup.visible = true;
                m_toucher.visible = false;
            }
        }
        else
        {
            m_getAgainTip.visible = false;
            m_getedGroup.visible = false;
            if (index == RoleService.Singleton.GetSignBeanIndex())
                m_toucher.visible = true;
            else
                m_toucher.visible = false;
        }
    }
    /// <summary>
    /// 是否领取了
    /// </summary>
    /// <returns></returns>
    private bool IsGeted()
    {
        int signIndex = RoleService.Singleton.GetSignBeanIndex();
        if(index <= signIndex)
        {
            if (index < signIndex)
                return true;

            return roleInfo.dailySignInFlag > 0;
        }

        return false;
    }
    /// <summary>
    /// 是否能继续领取
    /// </summary>
    /// <returns></returns>
    private bool IsCanGetAgain()
    {
        int signIndex = RoleService.Singleton.GetSignBeanIndex();
        if (roleInfo.dailySignInFlag == 1 && index == signIndex)
        {
            t_sign_in_monthBean signInBean = ConfigBean.GetBean<t_sign_in_monthBean, int>(index);
            if (signInBean != null && signInBean.t_vip_double != 0)
            {
                return roleInfo.vip >= signInBean.t_vip_double;
            }
        }

        return false;
    }

    private void OnClickItem()
    {
        RoleService.Singleton.ReqSignIn();
    }
}
