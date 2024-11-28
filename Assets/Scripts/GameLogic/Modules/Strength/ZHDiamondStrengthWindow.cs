using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_Strength;
using Data.Beans;
using Message.Pet;
using Message.Role;

public class ZHDiamondStrenthWindow : BaseWindow {

    UI_ZHDiamondStrenthWindow _window;
    ThreeParam<int, int, int> _threePara;
    private int diamondNum;
    public override void OnOpen()
    {
        base.OnOpen();

        _window = getUiWindow<UI_ZHDiamondStrenthWindow>();
        _threePara = Info.param as ThreeParam<int, int, int>;

        InitView();
        PlayPopupAnim(_window.m_mask, _window.m_popupView);
    }

    public override void InitView()
    {
        base.InitView();

        _window.m_mask.onClick.Add(OnCloseBtn);
        _window.m_popupView.m_confirmBtn.onClick.Add(OnClickConfirmBtn);
        PetInfo petInfo = PetService.Singleton.GetPetByID(_threePara.value3);
        t_pet_soulBean petSoulBean = ConfigBean.GetBean<t_pet_soulBean, int>(_threePara.value1);
        SoulInfo soulInfo = petInfo.soulInfo.souls[_threePara.value2];
        int id = petSoulBean.t_type * 100 + soulInfo.level;
        t_pet_soulup_expBean petSoulExpBean = ConfigBean.GetBean<t_pet_soulup_expBean, int>(id);
        t_globalBean globalBean = ConfigBean.GetBean<t_globalBean, int>(90003);
        int exp = petSoulExpBean.t_exp - soulInfo.remainExp;
        diamondNum = exp * globalBean.t_int_param;

        _window.m_popupView.m_diamondNum.text = diamondNum + "";
        _window.m_popupView.m_zhanHunName.text = petSoulBean.t_nameLanguageID;
        _window.m_popupView.m_levelLabel.text = (soulInfo.level + 1) + "";
    }

    public override void AddEventListener()
    {
        base.AddEventListener();

        GED.ED.addListener(EventID.OnPetShuXingChanged, OnSuccess);
    }

    public override void RemoveEventListener()
    {
        base.RemoveEventListener();
        GED.ED.removeListener(EventID.OnPetShuXingChanged, OnSuccess);
    }

    private void OnSuccess(GameEvent evt)
    {
        OnCloseBtn();
    }

    protected override void OnCloseBtn()
    {
        _threePara = null;
        _window = null;

        base.OnCloseBtn();
    }

    private void OnClickConfirmBtn()
    {
        if (IsEnoughDiamond())
        {
            PetService.Singleton.ReqDiamondZhanHunStrength(_threePara.value3, _threePara.value2);
        }
        else
        {
            TipWindow.Singleton.ShowTip("钻石不足！");
        }
    }

    private bool IsEnoughDiamond()
    {
        RoleInfo roleInfo = RoleService.Singleton.GetRoleInfo();
        return roleInfo.damond >= diamondNum;
    }
}
