using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_Strength;
using FairyGUI;
using Data.Beans;
using UI_Common;
using Message.Pet;

public enum PetItemType
{
    Normal = 0,
    Pet = 1,
    PetEquip = 2,
}

public class PetItem : UI_petItem {

    public int petID;

    public new static UI_petItem CreateInstance()
    {
        return (UI_petItem)UIPackage.CreateObject("UI_Strength", "petItem");
    }

    public void RefreshView(EquipedPetInfo petInfo, bool isShowName = false)
    {
        m_levelLabel.text = petInfo.level.ToString();
        Data.Beans.t_petBean petBean = ConfigBean.GetBean<Data.Beans.t_petBean, int>(petInfo.id);


        // 图片
        if (petBean != null)
            UIGloader.SetUrl(m_iconLoader, UIUtils.GetIconPath(petBean, petInfo.star));
        if (petInfo != null)
            UIGloader.SetUrl(m_borderBg, UIUtils.GetLoaderUrl(WinEnum.UI_Common, UIUtils.GetBorderByQuality(petInfo.color)));
        // 豆子
        PetQualityDou petQualityDou = m_petQualityDou as PetQualityDou;
        if (petInfo != null)
            petQualityDou.InitView(petInfo.color);
        // 名字
        m_petName.visible = isShowName;
        if (isShowName && petInfo != null)
        {
            m_petName.text = UIUtils.GetPetName(petBean, petInfo.star);
            m_petName.color = UIUtils.GetColorByQuality(petInfo.color);
        }

        // 上阵Icon
        m_shangZhenGroup.visible = false;

        // 选中框
        m_selectIcon.visible = false;

        // 星级

        StarList starList = new StarList((UI_StarList)m_starList); 
        starList.SetStar(petInfo.star);
  
        // 红点
        m_redPoint.visible = false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="selectID"></param>
    /// <param name="type"></param>
    /// <param name="isShowShangZhen">是否需要判断显示上阵图标</param>
    public void RefreshItem(int selectID, PetItemType type, bool isShowShangZhen = true, bool isShowName = false)
    {
        Message.Pet.PetInfo petInfo = PetService.Singleton.GetPetByID(petID);
        Data.Beans.t_petBean petBean = ConfigBean.GetBean<Data.Beans.t_petBean, int>(petID);

        if (petInfo == null)
            m_levelLabel.visible = false;
        else
        {
            m_levelLabel.visible = true;
            m_levelLabel.text = petInfo.basInfo.level.ToString();
        }
        // 图片
        if(petBean != null)
            UIGloader.SetUrl(m_iconLoader, UIUtils.GetIconPath(petBean, petInfo.basInfo.star));
        if (petInfo != null)
            UIGloader.SetUrl(m_borderBg, UIUtils.GetLoaderUrl(WinEnum.UI_Common, UIUtils.GetBorderByQuality(petInfo.basInfo.color)));
        // 豆子
        PetQualityDou petQualityDou = m_petQualityDou as PetQualityDou;
        if(petInfo != null)
            petQualityDou.InitView(petInfo.basInfo.color);
        // 名字
        m_petName.visible = isShowName;
        if (isShowName && petInfo != null)
        {
            m_petName.text = UIUtils.GetPetName(petBean, petInfo.basInfo.star);
            m_petName.color = UIUtils.GetColorByQuality(petInfo.basInfo.color);
        }
        // 上阵Icon
        if (isShowShangZhen)
            m_shangZhenGroup.visible = PetService.Singleton.ShangZhenList(petID);
        else
            m_shangZhenGroup.visible = false;
        // 选中框
        if (selectID != 0)
            m_selectIcon.visible = petID == selectID;
        else
            m_selectIcon.visible = false;
        // 星级
        if (petInfo == null)
        {
            m_starList.visible = false;
        }
        else
        {
            StarList starList = new StarList((UI_StarList)m_starList);
            starList.SetStar(petInfo.basInfo.star);
        }
        // 红点
        switch (type)
        {
            case PetItemType.Normal:
                m_redPoint.visible = false;
                break;
            case PetItemType.Pet:
                m_redPoint.visible = PetService.Singleton.IsShowPetRedTip(petID);
                break;
            case PetItemType.PetEquip:
                m_redPoint.visible = PetService.Singleton.IsShowPetEquipRedTip(petID);
                break;
            default:
                break;
        }
    }


}
