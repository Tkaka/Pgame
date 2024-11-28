using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_Strength;
using FairyGUI;
using Data.Beans;
using Message.Pet;

public class ZhanHunItem : UI_zhanHunItem {

    public int zhanHunID;
    public int index;

    private ZhanHunPanel _parentUI;

    public new static UI_zhanHunItem CreateInstance()
    {
        return (UI_zhanHunItem)UIPackage.CreateObject("UI_Strength", "zhanHunItem");
    }

    public void Init(ZhanHunPanel parentUI)
    {
        _parentUI = parentUI;
        m_itemToucher.onClick.Add(OnClickItem);
        m_selectedIcon.visible = false;
    }

    public void RefreshView()
    {
        PetInfo petInfo = _parentUI.StrengthData.CurSelectPetInfo;
        if (petInfo == null)
            return;

        t_pet_soulBean petSoulBean = ConfigBean.GetBean<t_pet_soulBean, int>(zhanHunID);
        if (petSoulBean == null)
        {
            return;
        }
        m_lvLabel.text = petInfo.soulInfo.souls[index].level.ToString();
        bool isUnlock = PetService.Singleton.ZhanHunIsUnlock(index, petInfo.petId);
        m_lvGroup.visible = isUnlock;
        m_lockIcon.visible = !isUnlock;
        UIGloader.SetUrl(m_zhanHunIconLoader, petSoulBean.t_icon);
        m_zhanHunIconLoader.grayed = !isUnlock;
    }

    private void OnClickItem()
    {
        _parentUI.OnClickZhanHunItem(index);
    }

    public override void Dispose()
    {
        _parentUI = null;

        base.Dispose();
    }
}
