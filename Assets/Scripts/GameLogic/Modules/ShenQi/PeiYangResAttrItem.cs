using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_ShenQi;
using Message.Team;
using Data.Beans;

public class PeiYangResAttrItem {

    UI_peiYangResAttrItem item;

	public PeiYangResAttrItem(UI_peiYangResAttrItem item)
    {
        this.item = item;
    }

    public void InitView(Attr attr)
    {
        t_attr_nameBean attrNameBean = ConfigBean.GetBean<t_attr_nameBean, int>(attr.id);
        if (attrNameBean != null)
            item.m_tipLabel.text = attrNameBean.t_name_id;

        bool isZhenShu = attr.id <= 3;
        item.m_upGroup.visible = false;
        item.m_downGroup.visible = false;
        item.m_noChangeLabel.visible = false;
        if (attr.value > 0)
        {
            item.m_upGroup.visible = true;
            if (isZhenShu)
                item.m_upLabel.text = string.Format("+{0}", attr.value);
            else
                item.m_upLabel.text = string.Format("+{0}%", (attr.value * 0.01f).ToString("0.00"));
        }
        else if(attr.value == 0)
        {
            item.m_noChangeLabel.visible = true;
        }
        else
        {
            item.m_downGroup.visible = true;
            if (isZhenShu)
                item.m_downLabel.text = string.Format("{0}", attr.value);
            else
                item.m_downLabel.text = string.Format("{0}%", (attr.value * 0.01f).ToString("0.00"));
        }
    }
}
