using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_ShenQi;
using FairyGUI;
using Message.Team;
using Data.Beans;

public class ShenQiShuXingItem : UI_shenQiShuXingItem {

    public int shenQiShuXinID;

    public new static ShenQiShuXingItem CreateInstance()
    {
        return (ShenQiShuXingItem)UIPackage.CreateObject("UI_ShenQi", "shenQiShuXingItem");
    }

    public void Init()
    {
        RefreshView();
    }

    public void RefreshView(bool isShowChange = false)
    {
        RefreshChangeAttr(isShowChange);
        RefreshBaseInfo();
    }

    private void RefreshChangeAttr(bool isShowChange)
    {
        m_changeTipLabel.visible = isShowChange;
        if (isShowChange)
        {
            Attr changeAttr = ShenQiService.Singleton.GetChangeAttr(shenQiShuXinID);
            if (changeAttr != null)
            {
                m_changeTipLabel.visible = true;
                string tipStr = "";
                bool isZhenShu = shenQiShuXinID <= 3;

                if (changeAttr.value > 0)
                {
                    m_changeTipLabel.color = Color.green;
                    if (isZhenShu)
                        tipStr = string.Format("+{0}", changeAttr.value);
                    else
                        tipStr = string.Format("+{0}%", (changeAttr.value * 0.01f).ToString("0.00"));
                }
                else if (changeAttr.value < 0)
                {
                    m_changeTipLabel.color = Color.red;
                    if (isZhenShu)
                        tipStr = string.Format("{0}", changeAttr.value);
                    else
                        tipStr = string.Format("{0}%", (changeAttr.value * 0.01f).ToString("0.00"));
                }
                else
                {
                    m_changeTipLabel.color = Color.green;
                    tipStr = "--";
                }


                m_changeTipLabel.text = tipStr;
            }
            else
                m_changeTipLabel.visible = false;
        }
    }
    private void RefreshBaseInfo()
    {
        ArtifactAttr shenQiShuXin = ShenQiService.Singleton.GetShenQiAttrByID(shenQiShuXinID);
        t_attr_nameBean attrNameBean = ConfigBean.GetBean<t_attr_nameBean, int>(shenQiShuXin.id);
        if (attrNameBean != null)
        {
            m_tipLabel.text = attrNameBean.t_name_id;
        }

        m_prograssBar.max = shenQiShuXin.max;
        m_prograssBar.value = shenQiShuXin.value;

        bool isZhenShu = shenQiShuXin.id <= 3;
        string tipStr = "";
        if (isZhenShu)
            tipStr = string.Format("{0}/{1}", shenQiShuXin.value, shenQiShuXin.max);
        else
            tipStr = string.Format("{0}%/{1}%", (shenQiShuXin.value * 0.01f).ToString("0.00"), (shenQiShuXin.max * 0.01f).ToString("0.00"));

        m_progressLabel.text = tipStr;
    }
}
