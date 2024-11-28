using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_Common;
using FairyGUI;
using Message.Pet;
using Data.Beans;

public class EquipItem : UI_equipItem {

    public EquipPosition equipPos;
    public int equipType;
    public int petID;
    /// <summary>
    /// 是否显示选中框
    /// </summary>
    public bool isShowSelect = true;
    /// <summary>
    /// 选择的装备部位
    /// </summary>
    public EquipPosition selectedPos;
    System.Action<EquipItem> clickCall;

    public new static UI_equipItem CreateInstance()
    {
        return (UI_equipItem)UIPackage.CreateObject("UI_Equip", "equipItem");
    }


    public void Init(int equipPos, int petID, int equipType = -1, System.Action<EquipItem> clickCall = null)
    {
        this.equipPos = (EquipPosition)equipPos;
        this.petID = petID;
        this.equipType = equipType;
        this.clickCall = clickCall;

        m_equipItemToucher.onClick.Add(OnClickItem);
        RefreshItem();
    }

    public void RefreshItem()
    {
        PetEquip petEquip = GetPetEquip();
        if (petEquip == null)
            return;

        UIGloader.SetUrl(m_boardLoader, UIUtils.GetLoaderUrl(WinEnum.UI_Common, UIUtils.GetBorderByQuality(petEquip.color)));
        UIGloader.SetUrl(m_iconLoader,UIUtils.GetEquipIcon(petID, (int)equipPos, petEquip.star));
        m_levelLabel.text = string.Format("+{0}", petEquip.level);
        
        m_jiBanIconLoader.visible = IsActiveJiBan();

        StarList starList = new StarList((UI_StarList)m_starList);
        starList.SetStar(petEquip.star);

        if (isShowSelect)
            m_selectIcon.visible = equipPos == selectedPos;
        else
            m_selectIcon.visible = false;
        // 设置豆子
        PetQualityDou petQualityDou = m_petQualityDou as PetQualityDou;
        petQualityDou.InitView(petEquip.color);

        RefreshUpArrow();
        ShowRedTip();
        RefreshLockState();
    }

    private void RefreshLockState()
    {
        switch (equipPos)
        {
            case EquipPosition.Weapon:
            case EquipPosition.Cloth:
            case EquipPosition.KuZi:
            case EquipPosition.Shoes:
                FuncService.Singleton.SetFuncLock(this, 1101);
                break;
            case EquipPosition.HuiZhan:
            case EquipPosition.MiJi:
                FuncService.Singleton.SetFuncLock(this, 1102);
                break;
            default:
                break;
        }
    }

    private void OnClickItem()
    {

        bool isOpen = IsOpen();
        if (isOpen == false)
            return;

        if (clickCall != null)
        {
            clickCall(this);
        }
    }
    /// <summary>
    /// 是否打开
    /// </summary>
    /// <returns></returns>
    private bool IsOpen(bool isShowTip = true)
    {
        bool isOpen = false;
        switch (equipPos)
        {
            case EquipPosition.Weapon:
            case EquipPosition.Cloth:
            case EquipPosition.KuZi:
            case EquipPosition.Shoes:
                if(isShowTip)
                    isOpen = FuncService.Singleton.TipFuncNotOpen(1101);
                else
                    isOpen = FuncService.Singleton.IsFuncOpen(1101);
                break;
            case EquipPosition.HuiZhan:
            case EquipPosition.MiJi:
                if(isShowTip)
                    isOpen = FuncService.Singleton.TipFuncNotOpen(1102);
                else
                    isOpen = FuncService.Singleton.IsFuncOpen(1102);
                break;
            default:
                break;
        }

        return isOpen;
    }

    private PetEquip GetPetEquip()
    {
        PetInfo petInfo = PetService.Singleton.GetPetByID(petID);
        PetEquip petEquip = petInfo.equipInfo.equips[(int)equipPos];

        return petEquip;
    }

    /// <summary>
    /// 是否激活羁绊
    /// </summary>
    /// <returns></returns>
    private bool IsActiveJiBan()
    {

        PetEquip petEquip = GetPetEquip();

        if (equipPos == EquipPosition.Weapon || equipPos == EquipPosition.MiJi || equipPos == EquipPosition.HuiZhan)
        {
            if (petEquip.star > 0)
            {
                return true;
            }
        }

        return false;
    }
    /// <summary>
    /// 刷新升级提示箭头
    /// </summary>
    private void RefreshUpArrow()
    {
        PetEquip petEquip = GetPetEquip();
        t_equip_colorupBean equipColorUpBean = ConfigBean.GetBean<t_equip_colorupBean, int>(petEquip.color);
        if (equipColorUpBean == null)
            return;

        int maxLevel = equipColorUpBean.t_lv_max;
        if (maxLevel > petEquip.level)
        {
            if(IsOpen(false))
            {
                m_upArrowIcon.visible = true;
                if (m_upArrowAnim.playing)
                    return;
                else
                {
                    m_upArrowAnim.Play();
                }
            }
            else
            {
                m_upArrowIcon.visible = false;
                if (m_upArrowAnim.playing)
                    m_upArrowAnim.Stop();
            }
        }
        else
        {
            m_upArrowIcon.visible = false;
        }
    }

    private void ShowRedTip()
    {
        if (PetService.Singleton.IsPetEquipCanColorUp(petID, (int)equipPos) ||
            PetService.Singleton.IsPetEquipCanStarUp(petID, (int)equipPos))
        {
            m_redPoint.visible = true;
        }
        else
        {
            m_redPoint.visible = false;
        }
    }

    public override void Dispose()
    {
        base.Dispose();
    }
}
