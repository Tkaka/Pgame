using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_Equip;
using FairyGUI;

public class EquipAttributeLabelItem :  UI_attributeItem{

    public new static UI_attributeItem CreateInstance()
    {
        return (UI_attributeItem)UIPackage.CreateObject("UI_Equip", "attributeItem");
    }

    public void RefreshView(int attributeID, LNumber attributeValue, bool isShowAnim, EquipPosition equipPos)
    {
        m_tip.text = UIUtils.GetTextByAttributeID(attributeID);
        switch (equipPos)
        {
            case EquipPosition.Weapon:
            case EquipPosition.Cloth:
            case EquipPosition.KuZi:
            case EquipPosition.Shoes:
                m_value.text = string.Format("+{0}", (int)attributeValue);
                break;
            case EquipPosition.HuiZhan:
            case EquipPosition.MiJi:
                m_value.text = string.Format("+{0}%", ((int)(attributeValue / 100.0f * 100)) / 100.0f);
                break;
            default:
                break;
        }
       

        if (isShowAnim)
        {
            if(m_animation.playing)
            {
                m_animation.Stop();
            }

            m_animation.Play();
        }
    }
}
