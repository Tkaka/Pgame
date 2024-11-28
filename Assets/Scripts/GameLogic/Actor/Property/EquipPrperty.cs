
using System.Collections.Generic;
using Data.Beans;
using UnityEngine;
using Message.Pet;
 

public class EquipProperty
{

    private Dictionary<PropertyType, PropertyStruct> m_propertyDic = new Dictionary<PropertyType, PropertyStruct>();   //属性值(同一属性不存在不同的值类型)
    private PetEquip m_petEquip;
    private int m_petId;

    public Dictionary<PropertyType, PropertyStruct> GetEquipPropertyDic()
    {
        return m_propertyDic;
    }

    public EquipProperty(int petId, PetEquip petEquip)
    {

        CalcEquipProperty(petId, petEquip);
    }
    public void CalcEquipProperty(int petId, PetEquip equip)
    {
        m_petId = petId;
        m_petEquip = equip;

        int equipLv = equip.level;
        int equipColor = equip.color;
        int equipStar = equip.star;

        Dictionary<PropertyType, PropertyStruct> property = CalcEquipProperty(equipLv, equipColor, equipStar);

        m_propertyDic.Clear();
        foreach (var pro in property)
        {
            if (m_propertyDic.ContainsKey(pro.Key))
            {
                m_propertyDic[pro.Key] = pro.Value;
            }
            else
            {
                m_propertyDic.Add(pro.Key, pro.Value);
            }
        }

        //if (equip.id == (int)EquipPosition.MiJi || equip.id == (int)EquipPosition.HuiZhan)
        //{
        //    //特殊装备
 
        //    int v = 0;
        //    t_special_equip_starup_argBean specialEquipStarUpAttrBean = ConfigBean.GetBean<t_special_equip_starup_argBean, int>(equipStar * 100 + equip.id);

        //    if (specialEquipStarUpAttrBean != null)
        //    {
        //        v = specialEquipStarUpAttrBean.t_add_shuXing;
        //    }

        //    for (int color = 1; color <= equipColor; color++)
        //    {
        //        t_special_equip_attrBean specialEquipAttrBean = ConfigBean.GetBean<t_special_equip_attrBean, int>(color * 100 + equip.id);
        //        int baseNum = specialEquipAttrBean.t_base_v_list;
        //        int addNum = specialEquipAttrBean.t_add_v_list;
        //        t_special_equip_lvcolorupBean specialEquipLvColorUpBean = ConfigBean.GetBean<t_special_equip_lvcolorupBean, int>(color * 100 + equip.id);

        //        int baseLv = specialEquipLvColorUpBean.t_lv_base;
        //        int maxLv = specialEquipLvColorUpBean.t_lv_max;
        //        int n = equipLv < maxLv ? equipLv - baseLv : maxLv - baseLv;
        //        v += baseNum + addNum * n;
        //    }

        //    _SetPropertyValue(_GetSpecialEquipAddProperty(), EPropertyFlag.Percent, v);
        //}
        //else
        //{
        //    // 升星属性
        //    t_star_up_propertyBean starUpPropertyBean = null;
        //    if (equipStar != 0)
        //       starUpPropertyBean =  ConfigBean.GetBean<t_star_up_propertyBean, int>(equip.equipTypeId * 100 + equipStar);// BeanTemplet.getEquipStarUpPropertyBean(id, star);

        //    // 升星缩放
        //    t_star_attach_precentBean percentBean = ConfigBean.GetBean<t_star_attach_precentBean, int>(equip.equipTypeId);//BeanTemplet.getEquipStarAttachPercentBean(id);
        //    int typeId = percentBean.t_base_id;
        //    int sumRate = percentBean.t_rate;
        //    float per = sumRate / 10000.0f;

        //    // 升品属性
        //    t_equip_color_up_propertyBean colorUpPropertyBean = ConfigBean.GetBean<t_equip_color_up_propertyBean, int>(typeId * 100 + equipColor);//BeanTemplet.getEquipColorUpPropertyBean(typeId, color);

        //    // 升品限制
        //    int level = 0;
        //    t_equip_colorupBean colorUpBean = ConfigBean.GetBean<t_equip_colorupBean, int>(equipColor);//BeanTemplet.getEquipColorUpBean(color);
        //    if (colorUpBean != null)
        //    {
        //        level = colorUpBean.t_lv_base;
        //        level = equipLv - level;
        //    }

        //    // 升星属性
        //    Dictionary<int, float> starProperty = null;
        //    if (starUpPropertyBean != null)
        //    {
        //        starProperty = _parseProperty(starUpPropertyBean.t_sum_property, per, 1);
        //    }
        //    // 星级比例
        //    int rate = ConfigBean.GetBean<t_globalBean, int>(106003).t_int_param;//BeanTemplet.getGlobalBean(106003).getT_int_param();
        //    float add = rate * equipStar / 10000.0f + 1;

        //    // 当前品总属性
        //    Dictionary<int, float> colorUpProperty = _parseProperty(colorUpPropertyBean.t_sum_property, per * add, 10000);

        //    // 品级属性
        //    Dictionary<int, float> colorLvProperty = _parseProperty(colorUpPropertyBean.t_lv_up_property, per * add * level, 10000);

        //    _mergeMap(starProperty, colorLvProperty);
        //    _mergeMap(colorLvProperty, colorUpProperty);

        //    m_propertyDic.Clear();

        //    foreach (var pro in colorUpProperty)
        //    {
        //        _SetPropertyValue((PropertyType)pro.Key, EPropertyFlag.Attach, (LNumber)pro.Value);
        //    }
        //}
    }

    public Dictionary<PropertyType, PropertyStruct> CalcEquipProperty(int equipLv, int equipColor, int equipStar)
    {
        Dictionary<PropertyType, PropertyStruct> propertyDic = new Dictionary<PropertyType, PropertyStruct>();

        int equipId = m_petEquip.id;
        int equipTypeId = m_petEquip.equipTypeId;

        if (equipId == (int)EquipPosition.MiJi || equipId == (int)EquipPosition.HuiZhan)
        {
            //特殊装备

            int v = 0;
            t_special_equip_starup_argBean specialEquipStarUpAttrBean = ConfigBean.GetBean<t_special_equip_starup_argBean, int>(equipStar * 100 + equipId);

            if (specialEquipStarUpAttrBean != null)
            {
                v = specialEquipStarUpAttrBean.t_add_shuXing;
            }

            for (int color = 1; color <= equipColor; color++)
            {
                t_special_equip_attrBean specialEquipAttrBean = ConfigBean.GetBean<t_special_equip_attrBean, int>(color * 100 + equipId);
                int baseNum = specialEquipAttrBean.t_base_v_list;
                int addNum = specialEquipAttrBean.t_add_v_list;
                t_special_equip_lvcolorupBean specialEquipLvColorUpBean = ConfigBean.GetBean<t_special_equip_lvcolorupBean, int>(color * 100 + equipId);

                int baseLv = specialEquipLvColorUpBean.t_lv_base;
                int maxLv = specialEquipLvColorUpBean.t_lv_max;
                int n = equipLv < maxLv ? equipLv - baseLv : maxLv - baseLv;
                v += baseNum + addNum * n;
            }

            if (propertyDic.ContainsKey(_GetSpecialEquipAddProperty()))
            {
                propertyDic[_GetSpecialEquipAddProperty()].SetPropertyValue(EPropertyFlag.Percent, v);
            }
            else
            {
                PropertyStruct propertyStruct = new PropertyStruct(_GetSpecialEquipAddProperty());
                propertyStruct.SetPropertyValue(EPropertyFlag.Percent, v);
                propertyDic.Add(_GetSpecialEquipAddProperty(), propertyStruct);
            }

        }
        else
        {
            // 升星属性
            t_star_up_propertyBean starUpPropertyBean = null;
            if (equipStar != 0)
                starUpPropertyBean = ConfigBean.GetBean<t_star_up_propertyBean, int>(equipTypeId * 100 + equipStar);// BeanTemplet.getEquipStarUpPropertyBean(id, star);

            // 升星缩放
            t_star_attach_precentBean percentBean = ConfigBean.GetBean<t_star_attach_precentBean, int>(equipTypeId);//BeanTemplet.getEquipStarAttachPercentBean(id);
            int typeId = percentBean.t_base_id;
            int sumRate = percentBean.t_rate;
            float per = sumRate / 10000.0f;

            // 升品属性
            t_equip_color_up_propertyBean colorUpPropertyBean = ConfigBean.GetBean<t_equip_color_up_propertyBean, int>(typeId * 100 + equipColor);//BeanTemplet.getEquipColorUpPropertyBean(typeId, color);

            // 升品限制
            int level = 0;
            t_equip_colorupBean colorUpBean = ConfigBean.GetBean<t_equip_colorupBean, int>(equipColor);//BeanTemplet.getEquipColorUpBean(color);
            if (colorUpBean != null)
            {
                level = colorUpBean.t_lv_base;
                level = equipLv - level;
            }

            // 升星属性
            Dictionary<int, float> starProperty = null;
            if (starUpPropertyBean != null)
            {
                starProperty = _parseProperty(starUpPropertyBean.t_sum_property, per, 1);
            }
            // 星级比例
            int rate = ConfigBean.GetBean<t_globalBean, int>(106003).t_int_param;//BeanTemplet.getGlobalBean(106003).getT_int_param();
            float add = rate * equipStar / 10000.0f + 1;

            // 当前品总属性
            Dictionary<int, float> colorUpProperty = _parseProperty(colorUpPropertyBean.t_sum_property, per * add, 10000);

            // 品级属性
            Dictionary<int, float> colorLvProperty = _parseProperty(colorUpPropertyBean.t_lv_up_property, per * add * level, 10000);

            _mergeMap(starProperty, colorLvProperty);
            _mergeMap(colorLvProperty, colorUpProperty);


            foreach (var pro in colorUpProperty)
            {
                if (propertyDic.ContainsKey((PropertyType)pro.Key))
                {
                    propertyDic[(PropertyType)pro.Key].SetPropertyValue(EPropertyFlag.Attach, (LNumber)pro.Value);
                }
                else
                {
                    PropertyStruct propertyStruct = new PropertyStruct((PropertyType)pro.Key);
                    propertyStruct.SetPropertyValue(EPropertyFlag.Attach, (LNumber)pro.Value);
                    propertyDic.Add((PropertyType)pro.Key, propertyStruct);
                }
            }
        }

        return propertyDic;
    }

    private void _mergeMap(Dictionary<int, float> source, Dictionary<int, float> to)
    {
        if (source == null)
            return;

        if (to == null)
        {
            to = new Dictionary<int, float>();
            foreach (var dic in source)
            {
                to.Add(dic.Key, dic.Value);
            }
            return;
        }

        foreach (var dic in source)
        {
            if (to.ContainsKey(dic.Key))
            {
                to[dic.Key] += dic.Value;
            }
            else
            {
                to.Add(dic.Key, dic.Value);
            }
        }
    }

    private Dictionary<int, float> _parseProperty(string str, float mul, int div)
    {
        Dictionary<int, float> map = new Dictionary<int, float>();
        if (str == null || str.Length == 0)
            return map;

        string[] split = GTools.splitString(str, ';');
        for (int i = 0; i < split.Length; i++)
        {
            string s = split[i];
            if (s == null || s.Length == 0)
                continue;

            string[] _val = GTools.splitString(s, '+');
            if (_val.Length != 2)
            {
                Debuger.Err("配置错误,配置的属性格式不为:属性id+值. param:" + s);
                continue;
            }

            int id = int.Parse(_val[0]);
            float value = float.Parse(_val[1]);
            value = ((int)(value * mul * 100 / div)) / 100.0f;

            if (map.ContainsKey(id))
                map[id] += value;
            else
                map.Add(id, value);
        }

        return map;
    }

    private void _SetPropertyValue(PropertyType type, EPropertyFlag flag, LNumber value)
    {
        if (m_propertyDic.ContainsKey(type))
        {
            m_propertyDic[type].SetPropertyValue(flag, value);
        }
        else
        {
            PropertyStruct propertyStruct = new PropertyStruct(type);
            propertyStruct.SetPropertyValue(flag, value);
            m_propertyDic.Add(type, propertyStruct);
        }
    }

    private PropertyType _GetSpecialEquipAddProperty()
    {
        if (m_petEquip.id == (int)EquipPosition.MiJi)
            return PropertyType.Hp;
        else if (m_petEquip.id == (int)EquipPosition.HuiZhan)
            return PropertyType.Atk;
        else
            return PropertyType.None;
    }
}
