using UI_AoYi;
using DG.Tweening;
using Data.Beans;
using UnityEngine;
using System.Collections.Generic;
using FairyGUI;

public class AoyiTipsWnd : BaseWindow
{
    private UI_AoyiTipsWnd m_window;
    private AoyiCommonItem m_itemIcon;
    private int m_itemId;
    private int m_level;
    private Vector2 pos;

    public override void OnOpen()
    {
        base.OnOpen();
        m_window = getUiWindow<UI_AoyiTipsWnd>();
        m_window.m_mask.onClick.Add(Close);
        InitPara();
        InitView();
    }

    private void InitPara()
    {
        FourParam<int, Vector2, GComponent, int> winPara = Info.param as FourParam<int, Vector2, GComponent, int>;
        m_itemId = winPara.value1;
        m_level = winPara.value4;
        pos = winPara.value3.TransformPoint(winPara.value2, m_window);

    }

    public override void InitView()
    {
        base.InitView();
        t_itemBean itemBean = ConfigBean.GetBean<t_itemBean, int>(m_itemId);
        if (itemBean == null)
        {
            Debug.Log("ä¸å­˜åœ¨çš„é“å…·ID" + m_itemId);
            return;
        }

        m_itemIcon = m_window.m_itemIcon as AoyiCommonItem;
        m_itemIcon.RefreshView(m_itemId, m_level);


        m_window.m_txtName.text = itemBean.t_name;
        m_window.m_txtName.color = UIUtils.GetItemColor(m_itemId);


        m_window.m_haveGroup.visible = itemBean.t_type > 0;
        if (m_itemId > 0)
            m_window.m_txtNum.text = BagService.Singleton.GetItemNum(m_itemId) + "";

        string strDes = "";
        List<AoyiService.PropertyInfo> propertyList = AoyiService.Singleton.GetStoneAddPropertyInfo(m_itemId, m_level);
        for (int i = 0; i < propertyList.Count; i++)
        {
            AoyiService.PropertyInfo propertyInfo = propertyList[i];
            t_attr_nameBean propertyBean = ConfigBean.GetBean<t_attr_nameBean, int>(propertyInfo.propertyId);
            if (propertyBean == null)
                continue;

            string str = "";
            if (propertyBean.t_value_type == 0)
            {
                str = string.Format("{0}  +{1}", propertyBean.t_name_id, (propertyInfo.propertyValue * 0.01) + "%");
            }
            else
            {
                str = string.Format("{0}  +{1}", propertyBean.t_name_id, propertyInfo.propertyValue);
            }

            if (string.IsNullOrEmpty(strDes))
                strDes = str;
            else
                strDes += "\n" + str;

        }
        m_window.m_txtDescribe.text = strDes;

        m_window.m_imgBg.height = m_window.m_titleGroup.height + m_window.m_txtDescribe.height + 60;
        m_window.m_txtBg.height = m_window.m_txtDescribe.height + 40;


        pos = new Vector3(pos.x - m_window.m_imgBg.size.x * 0.5f, pos.y - m_window.m_imgBg.size.y);
        m_window.m_itemGroup.xy = pos;
    }

    protected override void OnClose()
    {
        if (m_itemIcon != null)
            m_itemIcon.Dispose();
        base.OnClose();
    }


}


