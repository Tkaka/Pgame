using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_MainCity;
using Data.Beans;

public class HeadIconUnlockTipWindow : BaseWindow {

    UI_HeadIconUnlockTipWindow window;
    t_headBean headBean;

    public override void OnOpen()
    {
        base.OnOpen();

        window = getUiWindow<UI_HeadIconUnlockTipWindow>();
        window.m_closeBtn.onClick.Add(OnCloseBtn);
        window.m_mask.onClick.Add(OnCloseBtn);

        headBean = Info.param as t_headBean;

        InitView();
    }

    public override void InitView()
    {
        base.InitView();

        CommonHeadIcon headIcon = window.m_headIcon as CommonHeadIcon;
        headIcon.Init(headBean.t_id, null, false);
        if (headBean.t_cond_type == (int)HeadUnlockCoditionType.PetShape)
        {
            int[] conditionInfo = GTools.splitStringToIntArray(headBean.t_cond_arg);
            if (conditionInfo.Length == 2)
            {
                t_petBean petBean = ConfigBean.GetBean<t_petBean, int>(conditionInfo[0]);
                window.m_unlcokCoditionLabel.text = string.Format("需要获得宠物{0}", UIUtils.GetPetName(petBean, conditionInfo[1]));
            }
        }
        else if (headBean.t_cond_type == (int)HeadUnlockCoditionType.HaveSkin)
        {
            int itemID = int.Parse(headBean.t_cond_arg);
            t_itemBean itemBean = ConfigBean.GetBean<t_itemBean, int>(itemID);
            if (itemBean != null)
                window.m_unlcokCoditionLabel.text = string.Format("需要获得{0}", itemBean.t_name);
        }
    }
}
