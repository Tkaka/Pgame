using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_ShenQi;
using FairyGUI;
using Message.Team;
using Data.Beans;

public class PeiYangResItem : UI_peiYangResItem {

    public ArtifactRandomAttr attr;

    public new static PeiYangResItem CreateInstance()
    {
        return (PeiYangResItem)UIPackage.CreateObject("UI_ShenQi", "peiYangResItem");
    }

    public void Init()
    {
        m_doubleSelectBtn.onChanged.Add(OnDoubleBtnChanged);
        bool isSave = ShenQiService.Singleton.IsOpenAtuoRecomond() && attr.recommend == 1;
        m_doubleSelectBtn.selected = isSave;
        m_bg.visible = isSave;

        InitView();
    }

    private void InitView()
    {
        m_recommendIcon.visible = attr.recommend == 1;

        if (attr.attr.Count == 3)
        {
            PeiYangResAttrItem resAttrItem = new PeiYangResAttrItem(m_peiYangResAttrItem1);
            resAttrItem.InitView(attr.attr[0]);

            resAttrItem = new PeiYangResAttrItem(m_peiYangResAttrItem2);
            resAttrItem.InitView(attr.attr[1]);

            resAttrItem = new PeiYangResAttrItem(m_peiYangResAttrItem3);
            resAttrItem.InitView(attr.attr[2]);
        }
    }

    private void OnDoubleBtnChanged()
    {
        int isSave = m_doubleSelectBtn.selected ? 1 : 0;
        IsSave(isSave);
    }

    public void RefreshView(bool isSave)
    {
        int saveIndex = 0;
        if (isSave && attr.recommend == 1)
        {
            saveIndex = 1;
        }

        IsSave(saveIndex);
    }

    private void IsSave(int isSave)
    {
        int attrIndex = ShenQiService.Singleton.GetRandomAttrIndex(attr);
        if (attrIndex != -1)
        {
            ShenQiService.Singleton.saveInfoList[attrIndex] = isSave;
        }
        bool isSelected = isSave == 1;
        m_bg.visible = isSelected;
        m_doubleSelectBtn.selected = isSelected;
    }
}
