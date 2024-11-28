using Message.Pet;
using System.Collections.Generic;
using UI_Equip;
using Data.Beans;

public class ShuXingItem : UI_ShuXingItem
{
    public new static ShuXingItem CreateInstance()
    {
        return (ShuXingItem)UI_ShuXingItem.CreateInstance();
    }
    public void Init(EquipPosition type, PropertyType equiptype, int number)
    {
        m_Type.text = UIUtils.GetTextByAttributeID((int)equiptype);
        switch (type)
        {
            case EquipPosition.Weapon:
            case EquipPosition.Cloth:
            case EquipPosition.KuZi:
            case EquipPosition.Shoes:
                m_Data.text = string.Format("+{0}", number);
                break;
            case EquipPosition.HuiZhan:
            case EquipPosition.MiJi:
                m_Data.text = string.Format("+{0}%", ((float)number / 10000 * 100));
                break;
            default: break;
        }
    }
    public override void Dispose()
    {
        base.Dispose();
    }
}
