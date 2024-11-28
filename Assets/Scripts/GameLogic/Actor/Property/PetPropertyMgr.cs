using System.Collections.Generic;
using Message.Pet;
using Data.Beans;
using UnityEngine;

public class PropertyStruct
{
    public PropertyType type;  //属性类型

    private LNumber m_baseValue = 0;  //基础值（貌似不需要..）
    private LNumber m_attachValue = 0; //附加值
    private LNumber m_perCentValue = 0; //百分比值

    public LNumber baseValue
    {
        get { return m_baseValue; }
        private set { m_baseValue = value; }
    }

    public LNumber attachValue
    {
        get { return m_attachValue; }
        private set { m_attachValue = value; }
    }

    public LNumber percentValue
    {
        get { return m_perCentValue; }
        private set { m_perCentValue = value; }
    }

    public PropertyStruct(PropertyType type)
    {
        this.type = type;

    }

    public void SetPropertyValue(EPropertyFlag flag, LNumber value)
    {
        switch (flag)
        {
            case EPropertyFlag.Attach:
                attachValue = value;
                break;
            case EPropertyFlag.Base:
                baseValue = value;
                break;
            case EPropertyFlag.Percent:
                percentValue = value;
                break;
            default:
                break;
        }
    }

    public void AddPropertyValue(EPropertyFlag flag, LNumber value)
    {
        switch (flag)
        {
            case EPropertyFlag.Attach:
                attachValue += value;
                break;
            case EPropertyFlag.Base:
                baseValue += value;
                break;
            case EPropertyFlag.Percent:
                percentValue += value;
                break;
            default:
                break;
        }
    }
}



public class PetPropertyMgr
{
    //private static PetPropertyMgr m_singleton;
    //public static PetPropertyMgr Singleton
    //{
    //    get
    //    {
    //        if (m_singleton == null)
    //        {
    //            m_singleton = new PetPropertyMgr();
    //        }
    //        return m_singleton;
    //    }
    //}

    private PetInfo m_petInfo;   //宠物ID
    private Dictionary<PropertyType, LNumber> m_petSelfPropertyDic = new Dictionary<PropertyType, LNumber>();   //宠物自身属性
    private Dictionary<EquipPosition, EquipProperty> m_petEquipPropertyDic = new Dictionary<EquipPosition, EquipProperty>(); //宠物装备属性
    private Dictionary<PropertyType, PropertyStruct> m_petExtPropertyDic = new Dictionary<PropertyType, PropertyStruct>();   //附加属性

    private LNumber fightPower = 0;


    public PetPropertyMgr(PetInfo petInfo)
    {
        InitPetProperty(petInfo);
    }

    public void InitPetProperty(PetInfo petInfo)
    {
        m_petInfo = petInfo;
        m_petSelfPropertyDic.Clear();
        m_petEquipPropertyDic.Clear();

        _InitPetSelfProperty();
        _InitPetEquipProperty();
        _InitPetExtProperty();
        RefreshFighrPowert();
        //PrintPropertyValue();
    }

    public void ChangeExtProperty(List<Property> list)
    {
        foreach (var pro in list)
        {
            if (m_petExtPropertyDic.ContainsKey((PropertyType)pro.id))
            {
                m_petExtPropertyDic[(PropertyType)pro.id].AddPropertyValue((EPropertyFlag)pro.flag, (LNumber)pro.value);
            }
            else
            {
                PropertyStruct struc = new PropertyStruct((PropertyType)pro.id);
                struc.SetPropertyValue((EPropertyFlag)pro.flag, (LNumber)pro.value);
                m_petExtPropertyDic.Add((PropertyType)pro.id, struc);
            }
        }

        RefreshFighrPowert();
    }

    //获得宠物拥有的属性值
    public LNumber GetPropertyValue(PropertyType propertyType)
    {
        LNumber baseValue = 0;
        if (m_petSelfPropertyDic.ContainsKey(propertyType))
        {
            baseValue = m_petSelfPropertyDic[propertyType];
        }

        LNumber equipAttachValue = 0;
        LNumber equipPercentValue = 0;
        _GetEquipAddProperty(propertyType, out equipAttachValue, out equipPercentValue);

        // 获取条件属性
        LNumber conditionAttachValue = 0;
        LNumber conditionPercentValue = 0;
        PetService.Singleton.GetConditionProperty(m_petInfo, propertyType, out conditionAttachValue, out conditionPercentValue);

        LNumber totalAttach = equipAttachValue + conditionAttachValue;
        LNumber totalPercent = equipPercentValue + conditionPercentValue;

        if (m_petExtPropertyDic.ContainsKey(propertyType))
        {
            PropertyStruct str = m_petExtPropertyDic[propertyType];
            totalAttach += str.attachValue;
            totalPercent += str.percentValue;
        }

        //Debug.Log("=======================>>>>>>>>>>>单条属性" + "    " + propertyType + "    " + baseValue + "    " + totalAttach + "    " + totalPercent);
        return baseValue + totalAttach + (baseValue * totalPercent / 10000);
    }

    public void PrintPropertyValue()
    {
        Debuger.Log("()()()()()()()()()()()()()>>>宠物 " + m_petInfo.petId);
        foreach (var info in m_petSelfPropertyDic)
        {
            Debuger.Log("    " + info.Key + "     " + GetPropertyValue(info.Key));
        }
    }

    //获取所有装备增加的属性
    private void _GetEquipAddProperty(PropertyType property, out LNumber attachValue, out LNumber percentValue)
    {
        attachValue = 0;
        percentValue = 0;

        foreach (var equipInfo in m_petEquipPropertyDic)
        {
            Dictionary<PropertyType, PropertyStruct> propertyDic = equipInfo.Value.GetEquipPropertyDic();

            if (propertyDic.ContainsKey(property))
            {
                attachValue += propertyDic[property].attachValue;
                percentValue += propertyDic[property].percentValue;
            }    
        }
    }

    
    //初始化宠物装备属性
    private void _InitPetEquipProperty()
    {
        for (int i = 0; i < m_petInfo.equipInfo.equips.Count; i++)
        {
            PetEquip equip = m_petInfo.equipInfo.equips[i];
            if (m_petEquipPropertyDic.ContainsKey((EquipPosition)equip.id))
            {
                Debug.Log("已经存在的装备部位" + equip.id);
                continue;
            }
            m_petEquipPropertyDic.Add((EquipPosition)equip.id, new EquipProperty(m_petInfo.petId, equip));
        }
    }

    // 宠物附加属性
    private void _InitPetExtProperty()
    {
        List<Property> list = m_petInfo.property;
        if (list == null || list.Count == 0)
            return;

        ChangeExtProperty(list);
    }

    //初始化宠物自身的属性
    private void _InitPetSelfProperty()
    {
        t_petBean bean = ConfigBean.GetBean<t_petBean, int>(m_petInfo.petId);
        // 攻防技
        int type = bean.t_type;
        // 等级
        int level = m_petInfo.basInfo.level;
        if (level <= 0)
        {
            return;
        }
        // 星级
        int star = m_petInfo.basInfo.star;
        // 品阶
        int color = m_petInfo.basInfo.color;

        // 初始属性
        int base_atk = bean.t_atk; //bean.getT_atk();
        int base_def = bean.t_def; //bean.getT_def();
        int base_hp = bean.t_hp; //bean.getT_hp();
        int base_critical = bean.t_baoji; //bean.getT_baoji();
        int base_antiCritical = bean.t_anti_baoji; //bean.getT_anti_baoji();
        int base_criticalDamage = bean.t_baoji_strength; //bean.getT_baoji_strength();
        int base_block = bean.t_gedang; //bean.getT_gedang();
        int base_antiBlock = bean.t_poji;//bean.getT_poji();
        int base_blockValue = bean.t_gedang_strength;//bean.getT_gedang_strength();
        int base_damageDeppen = bean.t_shanghai; //bean.getT_shanghai();
        int base_damageAvoid = bean.t_mianshang; //bean.getT_mianshang();
        // 修正
        int atk_fix = bean.t_atk_fix; //bean.getT_atk_fix();
        int def_fix = bean.t_def_fix; //bean.getT_def_fix();
        int hp_fix = bean.t_hp_fix; //bean.getT_hp_fix();
        // 等级加成
        t_pet_attr_argBean lv_arg = ConfigBean.GetBean<t_pet_attr_argBean, int>(10 + type);  //BeanTemplet.getGrowUpArgBean(11);

        // 升品成长
        t_pet_colorup_attrBean colorUpAttr = ConfigBean.GetBean<t_pet_colorup_attrBean, int>(color); // BeanTemplet.getPetColorUpPropSumBean(color);
        // 升品属性和
        t_pet_colorup_attr_sumBean colorUpPropSum = ConfigBean.GetBean<t_pet_colorup_attr_sumBean, int>(color * 10 + type);// BeanTemplet.getPetColorUpPropSumBean(color, type);
        // 升星属性和
        t_pet_starup_attr_sumBean starUpSum = ConfigBean.GetBean<t_pet_starup_attr_sumBean, int>(star * 10 + type); //BeanTemplet.getPetStarUpSumBean(star, type);
        // 升星加成成长
        t_pet_starup_addBean starUpAdd = ConfigBean.GetBean<t_pet_starup_addBean, int>(star * 10 + type); //BeanTemplet.getPetStarUpAddBean(star, type);

        // 计算初始攻防血
        int atk = _getAttr(level, base_atk, lv_arg.t_atk, colorUpAttr.t_atk,
                starUpAdd.t_atk, colorUpPropSum.t_atk, starUpSum.t_atk, atk_fix);
        int def = _getAttr(level, base_def, lv_arg.t_def, colorUpAttr.t_def,
                starUpAdd.t_def, colorUpPropSum.t_def, starUpSum.t_def, def_fix);
        int hp = _getAttr(level, base_hp, lv_arg.t_hp, colorUpAttr.t_hp,
                starUpAdd.t_hp, colorUpPropSum.t_hp, starUpSum.t_hp, hp_fix);

        this.m_petSelfPropertyDic.Clear();
        this.m_petSelfPropertyDic.Add(PropertyType.Atk, atk);
        this.m_petSelfPropertyDic.Add(PropertyType.Def, def);
        this.m_petSelfPropertyDic.Add(PropertyType.Hp, hp);
        this.m_petSelfPropertyDic.Add(PropertyType.BaoJiLv, base_critical);
        this.m_petSelfPropertyDic.Add(PropertyType.KangBaoJiLv, base_antiCritical);
        this.m_petSelfPropertyDic.Add(PropertyType.BaoJiQiangDu, base_criticalDamage);
        this.m_petSelfPropertyDic.Add(PropertyType.GeDangLv, base_block);
        this.m_petSelfPropertyDic.Add(PropertyType.PoJiLv, base_antiBlock);
        this.m_petSelfPropertyDic.Add(PropertyType.GeDangQiangDu, base_blockValue);
        this.m_petSelfPropertyDic.Add(PropertyType.ShangHaiLv, base_damageDeppen);
        this.m_petSelfPropertyDic.Add(PropertyType.MianShangLv, base_damageAvoid);
    }

    private int _getAttr(int level, int baseValue, int levelAdd, int colorUpGrow, int starUpAddGrow, int colorUpPropSum, int starUpPropSum, int fix)
    {
        //Debug.Log("升星属性" + "  " + level + "  " + "  " + baseValue + "  " + "  " + levelAdd + "  " + "  " + colorUpGrow + "  " + "  " + starUpAddGrow + "  " + "  " + colorUpPropSum + "  " + "  " + starUpPropSum + "  " + fix);
        return (int)(baseValue + ((levelAdd + colorUpGrow + starUpAddGrow) * (level / 10000d)
                + colorUpPropSum + starUpPropSum) * (fix / 10000d));
    }


    public void PetBaseInfoChange(PetInfo petInfo)
    {
        m_petInfo = petInfo;
        _InitPetSelfProperty();
        RefreshFighrPowert();
        //PrintPropertyValue();
    }

    public void PetEquipInfoChange(PetEquip petEquip)
    {
        if (m_petEquipPropertyDic.ContainsKey((EquipPosition)petEquip.id))
        {
            m_petEquipPropertyDic[(EquipPosition)petEquip.id].CalcEquipProperty(m_petInfo.petId, petEquip);
            RefreshFighrPowert();
        }
        else
        {
            Debug.LogError("不存在的装备部位" + petEquip.id);
        }

        //PrintPropertyValue();
    }

    public LNumber GetFightPower()
    {
        return fightPower;
    }

    public EquipProperty GetPetEquipProperty(EquipPosition pos)
    {
        if (m_petEquipPropertyDic.ContainsKey(pos))
        {
            return m_petEquipPropertyDic[pos];
        }

        return null;
    }

    public Dictionary<PropertyType, PropertyStruct> GetPetEquipProperty(EquipPosition pos, int equipLv, int equipColor, int equipStar)
    {
        if (m_petEquipPropertyDic.ContainsKey(pos))
        {
            return m_petEquipPropertyDic[pos].CalcEquipProperty(equipLv, equipColor, equipStar);
        }

        return null;
    }

    /**
     * 刷新战斗力
     */
    public void RefreshFighrPowert()
    {
        fightPower = 0;

        for (int i = (int)PropertyType.None + 1; i < (int)PropertyType.MaxPropertyType; i++)
        {
            fightPower += CalcFightPowert((PropertyType)i, GetPropertyValue((PropertyType)i));
        }
        for (int i = 0; i < m_petInfo.skillInfo.skillInfos.Count; ++i)
        {
            fightPower += m_petInfo.skillInfo.skillInfos[i].level * 8;
        }
    }


    public static LNumber CalcFightPowert(PropertyType type, LNumber value)
    {
        if (value == 0)
            return 0;

        switch (type)
        {
            case PropertyType.Atk:
                return value * 4150 / 10000;
            case PropertyType.Def:
                return value * 4150 / 10000;
            case PropertyType.Hp:
                return value * 800 / 10000;
            case PropertyType.BaoJiLv:
                return value * 360 / 10000;
            case PropertyType.KangBaoJiLv:
                return value * 360 / 10000;
            case PropertyType.GeDangLv:
                return value * 500 / 10000;
            case PropertyType.PoJiLv:
                return value * 500 / 10000;
            case PropertyType.ShangHaiLv:
                return value * 900 / 10000;
            case PropertyType.MianShangLv:
                return value * 900 / 10000;
            default:
                return 0;
        }
    }


}