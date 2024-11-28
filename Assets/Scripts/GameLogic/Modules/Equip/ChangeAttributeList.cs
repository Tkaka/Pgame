using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_Equip;
using FairyGUI;

public class ChangeAttributeList {

    UI_attributeChangeList attributeList;

	public ChangeAttributeList(UI_attributeChangeList list)
    {
        attributeList = list;
    }

    public void SetData(List<int> ids, List<int> oldValueList, List<int> newValueList, EquipPosition equipPos)
    {

        //switch (equipPos)
        //{
        //    case EquipPosition.Weapon:
        //        oldValueList.RemoveAt(oldValueList.Count - 1);
        //        oldValueList.RemoveAt(oldValueList.Count - 1);

        //        newValueList.RemoveAt(newValueList.Count - 1);
        //        newValueList.RemoveAt(newValueList.Count - 1);
        //        break;
        //    case EquipPosition.Cloth:
        //        oldValueList.RemoveAt(0);
        //        oldValueList.RemoveAt(0);

        //        newValueList.RemoveAt(0);
        //        newValueList.RemoveAt(0);
        //        break;
        //    case EquipPosition.KuZi:
        //        oldValueList.RemoveAt(0);

        //        newValueList.RemoveAt(0);
        //        break;
        //    case EquipPosition.Shoes:
        //        oldValueList.RemoveAt(0);

        //        newValueList.RemoveAt(0);
        //        break;
        //    default:
        //        break;
        //}

        int newCount = ids.Count;
        UI_attributeChangeLabel attributeLabel;
        attributeList.m_attributeList.RemoveChildren(0, -1, true);
        for (int i = 0; i < newCount; i++)
        {
            attributeLabel = UI_attributeChangeLabel.CreateInstance();
            attributeList.m_attributeList.AddChild(attributeLabel);
            attributeLabel.m_nameLabel.text = UIUtils.GetTextByAttributeID(ids[i]);
            switch (equipPos)
            {
                case EquipPosition.Weapon:
                case EquipPosition.Cloth:
                case EquipPosition.KuZi:
                case EquipPosition.Shoes:
                    attributeLabel.m_oldValue.text = string.Format("+{0}", oldValueList[i]);
                    attributeLabel.m_newValue.text = string.Format("+{0}", newValueList[i]);
                    break;
                case EquipPosition.HuiZhan:
                case EquipPosition.MiJi:
                    attributeLabel.m_oldValue.text = string.Format("+{0} %", GTools.rateConvert(oldValueList[i]) * 100);
                    attributeLabel.m_newValue.text = string.Format("+{0} %", GTools.rateConvert(newValueList[i]) * 100);
                    break;
                default:
                    break;
            }
            attributeLabel.m_newValue.color = Color.green;
        }
    }

    public void SetData(EquipDataManager equipData)
    {
        Dictionary<PropertyType, PropertyStruct> oldDataDict = equipData.oldDictProperty;
        Dictionary<PropertyType, PropertyStruct> curDataDict = equipData.GetAttributeData();
        EquipPosition equipPos = equipData.CurSelectEquipPos;
        List<PropertyType> keys = new List<PropertyType>();
        keys.AddRange(oldDataDict.Keys);

        int newCount = keys.Count;
        UI_attributeChangeLabel attributeLabel;
        attributeList.m_attributeList.RemoveChildren(0, -1, true);
        for (int i = 0; i < newCount; i++)
        {
            attributeLabel = UI_attributeChangeLabel.CreateInstance();
            attributeList.m_attributeList.AddChild(attributeLabel);
            attributeLabel.m_nameLabel.text = UIUtils.GetTextByAttributeID((int)keys[i]);
            switch (equipPos)
            {
                case EquipPosition.Weapon:
                case EquipPosition.Cloth:
                case EquipPosition.KuZi:
                case EquipPosition.Shoes:
                    attributeLabel.m_oldValue.text = string.Format("+{0}", oldDataDict[keys[i]].attachValue.Floor);
                    attributeLabel.m_newValue.text = string.Format("+{0}", curDataDict[keys[i]].attachValue.Floor);
                    break;
                case EquipPosition.HuiZhan:
                case EquipPosition.MiJi:
                    attributeLabel.m_oldValue.text = string.Format("+{0} %", oldDataDict[keys[i]].percentValue.Floor * 0.01f);
                    attributeLabel.m_newValue.text = string.Format("+{0} %", curDataDict[keys[i]].percentValue.Floor * 0.01f);
                    break;
                default:
                    break;
            }
            attributeLabel.m_newValue.color = Color.green;
        }
    }
}
