using UI_Equip;
using Message.Pet;
using Data.Beans;

public class JueXingShuXingItem : UI_JueXingShuXingItem
{
    public new static JueXingShuXingItem CreateInstance()
    {
        return (JueXingShuXingItem)UI_JueXingShuXingItem.CreateInstance();
    }
    public void Init(EquipPosition position, PropertyType equiptype,int oldshuxing,int newshuxing)
    {
        m_Type.text = UIUtils.GetTextByAttributeID((int)equiptype);
        switch (position)
        {
            case EquipPosition.Weapon:
            case EquipPosition.Cloth:
            case EquipPosition.KuZi:
            case EquipPosition.Shoes:
                m_oldnumber.text = string.Format("+{0}", oldshuxing);
                m_newnumber.text = string.Format("+{0}", newshuxing);
                break;
            case EquipPosition.HuiZhan:
            case EquipPosition.MiJi:
                m_oldnumber.text = string.Format("+{0}%", ((float)oldshuxing / 10000 * 100));
                m_newnumber.text = string.Format("+{0}%", ((float)newshuxing / 10000 * 100));
                break;
            default: break;
        }
    }
}
