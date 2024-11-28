using UI_Equip;
public class ManXingShuXingItem : UI_ManXingShuXingItem
{
    public new static ManXingShuXingItem CreateInstance()
    {
        return (ManXingShuXingItem)UI_ManXingShuXingItem.CreateInstance();
    }
    public void Init(EquipPosition position, int Leixing,int number)
    {
        m_Name.text = UIUtils.GetTextByAttributeID(Leixing);
        switch (position)
        {
            case EquipPosition.Weapon:
            case EquipPosition.Cloth:
            case EquipPosition.KuZi:
            case EquipPosition.Shoes:
                m_Number.text = string.Format("+{0}", number);
                break;
            case EquipPosition.HuiZhan:
            case EquipPosition.MiJi:
                m_Number.text = string.Format("+{0}%",number);
                break;
            default:break;
        }

    }
}