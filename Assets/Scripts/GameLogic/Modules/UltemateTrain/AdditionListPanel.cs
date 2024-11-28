using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_UltemateTrain;
using Message.Challenge;
using Data.Beans;

public class AdditionListPanel {

    UI_additionListPanel panel;

    private List<IntVsInt> propertyList;
    private float offsetH;
    public List<IntVsInt> PropertyList
    {
        set
        {
            if (propertyList == value)
                return;

            propertyList.Clear();
            propertyList.AddRange(value);
            RefreshView();
        }
    }
	public AdditionListPanel(UI_additionListPanel additionListPanel)
    {
        this.panel = additionListPanel;
        propertyList = new List<IntVsInt>();
        offsetH = panel.m_propertyBg.height - panel.m_propertyListLabel.height;
    }

    private void RefreshView()
    {
        string propertyStr = "";
        int count = propertyList.Count;
        if (count == 0)
        {
            propertyStr += "当前没有属性加成";
        }
        else
        {
            IntVsInt property;
            for (int i = 0; i < count; i++)
            {
                if (i != 0)
                    propertyStr += "\n";

                property = propertyList[i];
                AdditionPropertyType type = (AdditionPropertyType)property.int1;
                int propertyID = 15;
                switch (type)
                {
                    case AdditionPropertyType.Atk:
                        propertyID = 1;
                        break;
                    case AdditionPropertyType.Def:
                        propertyID = 2;
                        break;
                    case AdditionPropertyType.GeDang:
                        propertyID = 7;
                        break;
                    case AdditionPropertyType.BaoJi:
                        propertyID = 4;
                        break;
                    case AdditionPropertyType.XiXue:
                        propertyID = 13;
                        break;
                    case AdditionPropertyType.FanShang:
                        propertyID = 22;
                        break;
                    case AdditionPropertyType.SingleHuiNu:
                        break;
                    case AdditionPropertyType.AllHuiNu:
                        break;
                    case AdditionPropertyType.SingleHuiXue:
                        break;
                    case AdditionPropertyType.AllHuiXue:
                        break;
                    case AdditionPropertyType.FuHuo:
                        break;
                    default:
                        break;
                }
                t_attr_nameBean attrNameBean = ConfigBean.GetBean<t_attr_nameBean, int>(propertyID);
                if (attrNameBean != null)
                {
                    propertyStr += string.Format("[color=#5CACEE]{0}:[/color]{1}%", attrNameBean.t_name_id, property.int2 * 0.01f);
                }
            }
        }

        panel.m_propertyListLabel.text = propertyStr;
        panel.m_propertyBg.height = panel.m_propertyListLabel.height + offsetH;
    }

    public void Dispose()
    {
        propertyList.Clear();
        panel = null;
    }
}
