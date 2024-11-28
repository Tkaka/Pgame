using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_BuZhen;
using UI_Common;
using FairyGUI;
using Message.Pet;
using Data.Beans;

public class ZhenRongEquipItem : UI_zhenRongEquipItem {
    // 装备的位置
    public int pos;

    private ZhenRongWindow parentUI;
    private PetEquip petEquip;

    public new static UI_zhenRongEquipItem CreateInstance()
    {
        return (UI_zhenRongEquipItem)UIPackage.CreateObject("UI_BuZhen", "zhenRongEquipItem");
    }

    private int PetID
    {
        get { return parentUI.CurPetID; }
    }

    public void Init(ZhenRongWindow parentUI)
    {
        this.parentUI = parentUI;
        petEquip = GetPetEquip();
        if (petEquip == null)
            return;

        m_equipItemToucher.onClick.Add(OnClickEquipItem);

        RefreshView();
    }

    private PetEquip GetPetEquip()
    {
        PetInfo petInfo = PetService.Singleton.GetPetByID(PetID);
        if (petInfo.equipInfo == null || petInfo.equipInfo.equips == null)
        {
            return null;
        }
        return petInfo.equipInfo.equips[pos];
    }

    public void RefreshView()
    {

        UIGloader.SetUrl(m_boardLoader, UIUtils.GetLoaderUrl(WinEnum.UI_Common, UIUtils.GetBorderByQuality(petEquip.color)));
        UIGloader.SetUrl(m_iconLoader, UIUtils.GetEquipIcon(PetID, pos, petEquip.star));
        m_levelLabel.text = string.Format("+{0}", petEquip.level);

        if (petEquip != null)
        {
            m_jiBanIconLoader.visible = petEquip.star > 0;
            StarList starList = new StarList((UI_StarList)m_starList);
            starList.SetStar(petEquip.star);
        }

        RefreshUpArrow();
        ShowRedTip();
    }

    private void ShowRedTip()
    {
        if (PetService.Singleton.IsPetEquipCanColorUp(PetID, pos) || PetService.Singleton.IsPetEquipCanStarUp(PetID, pos))
        {
            m_redPoint.visible = true;
        }
        else
        {
            m_redPoint.visible = false;
        }
    }

    private void RefreshUpArrow()
    {
        t_equip_colorupBean colorUpBean = ConfigBean.GetBean<t_equip_colorupBean, int>(petEquip.color);
        if (colorUpBean != null)
        {
            int colorMaxBean = colorUpBean.t_lv_max;
            if (colorMaxBean > petEquip.color)
            {
                m_upArrowIcon.visible = true;
                if (m_upArrowAnim.playing)
                    m_upArrowAnim.Stop();

                m_upArrowAnim.Play();
            }
            else
            {
                m_upArrowIcon.visible = false;
            }
        }
        else
        {
            m_upArrowIcon.visible = false;
        }
    }

    private void OnClickEquipItem()
    {
        // 进入装备强化界面
        WinInfo winInfo = new WinInfo();
        ThreeParam<int, int,int> twoParam = new ThreeParam<int, int,int>();
        twoParam.value1 = PetID;
        twoParam.value2 = pos;
        twoParam.value3 = 0;
        winInfo.param = twoParam;
        WinMgr.Singleton.Open<EquipStrengthWindow>(winInfo, UILayer.Popup);
    }

    public override void Dispose()
    {
        base.Dispose();

        petEquip = null;
        parentUI = null;
    }
}
