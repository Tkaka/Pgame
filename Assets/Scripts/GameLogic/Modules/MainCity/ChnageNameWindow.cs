using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_MainCity;
using Data.Beans;

public class ChnageNameWindow : BaseWindow {

    UI_ChnageNameWindow window;

    public override void OnOpen()
    {
        base.OnOpen();

        window = getUiWindow<UI_ChnageNameWindow>();

        BindEvent();
        InitView();
    }

    public override void AddEventListener()
    {
        base.AddEventListener();

        GED.ED.addListener(EventID.OnResModifyNickname, OnResModifyNickname);
    }

    public override void RemoveEventListener()
    {
        base.RemoveEventListener();

        GED.ED.removeListener(EventID.OnResModifyNickname, OnResModifyNickname);
    }

    private void BindEvent()
    {
        window.m_closeBtn.onClick.Add(OnCloseBtn);
        window.m_mask.onClick.Add(OnCloseBtn);
        window.m_comfirmBtn.onClick.Add(OnConfirmBtnClick);
    }

    public override void InitView()
    {
        base.InitView();
        Message.Role.RoleInfo roleInfo = RoleService.Singleton.GetRoleInfo();
        window.m_name.text = roleInfo.roleName;
        if (roleInfo.nicknameModifyCount == 0)
        {
            // 免费
            window.m_firstFreeTip.visible = true;
            window.m_noramlTip.visible = false;
        }
        else
        {
            // 20101
            window.m_firstFreeTip.visible = false;
            window.m_noramlTip.visible = true;
            t_globalBean globalBean = ConfigBean.GetBean<t_globalBean, int>(20101);
            if (globalBean != null)
            {
                int[] modifyNamePrice = GTools.splitStringToIntArray(globalBean.t_string_param);
                int price = int.MaxValue;
                if (modifyNamePrice.Length == 2)
                {
                    price = modifyNamePrice[1];
                }
                window.m_costDiamond.text = price + "";
            }
        }
    }

    private void OnConfirmBtnClick()
    {
        // 发送头像修改的请求
        RoleService.Singleton.ReqModifyNickname(window.m_name.text);
    }

    private void OnResModifyNickname(GameEvent evt)
    {
        OnCloseBtn();
    }
}
