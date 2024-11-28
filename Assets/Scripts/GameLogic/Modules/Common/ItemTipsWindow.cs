using UI_Common;
using DG.Tweening;
using Data.Beans;
using UnityEngine;
using System.Collections.Generic;
using FairyGUI;

public class ItemTipsWindow : BaseWindow
{
    private UI_ItemTipsWindow m_window;
    private CommonItem m_itemIcon;
    private int m_itemId;
    private Vector2 pos;

    public override void OnOpen()
    {
        base.OnOpen();
        m_window = getUiWindow<UI_ItemTipsWindow>();
        m_window.m_mask.onClick.Add(Close);
        InitPara();
        InitView();
    }

    private void InitPara()
    {
        ThreeParam<int, Vector2, GComponent> winPara = Info.param as ThreeParam<int, Vector2, GComponent>;
        m_itemId = winPara.value1;
        pos = winPara.value3.TransformPoint(winPara.value2, m_window);
        
    }

    public override void InitView()
    {
        base.InitView();
        t_itemBean itemBean = ConfigBean.GetBean<t_itemBean, int>(m_itemId);
        if (itemBean == null)
        {
            Debug.Log("不存在的道具ID" + m_itemId);
            return;
        }

        m_itemIcon = m_window.m_itemIcon as CommonItem;
        m_itemIcon.itemId = m_itemId;
        m_itemIcon.isShowNum = false;
        m_itemIcon.RefreshView();


        m_window.m_txtName.text = itemBean.t_name;
        m_window.m_txtName.color = UIUtils.GetItemColor(m_itemId);


        m_window.m_haveGroup.visible = itemBean.t_type > 0;
        if (m_itemId > 0)
            m_window.m_txtNum.text = BagService.Singleton.GetItemNum(m_itemId) + "";

        m_window.m_txtDescribe.text = itemBean.t_describe;
        //Debug.Log("===============>>>>>>>>>>>>>>背景拉伸高度" + (m_window.m_titleGroup.height + m_window.m_txtDescribe.height + 20));

        m_window.m_imgBg.height = m_window.m_titleGroup.height + m_window.m_txtDescribe.height + 60;
        m_window.m_txtBg.height = m_window.m_txtDescribe.height + 15;
        // m_window.m_bgGroup.height = m_window.m_titleGroup.height + m_window.m_txtDescribe.height + 20;
        //Debug.Log("高度++++++++++++++" + m_window.m_bgGroup.height);
        //m_window.m_bgGroup.EnsureBoundsCorrect();

        pos = new Vector3(pos.x - m_window.m_imgBg.size.x * 0.5f, pos.y - m_window.m_imgBg.size.y);
        m_window.m_itemGroup.xy = pos;
    }

    protected override void OnClose()
    {
        if(m_itemIcon != null)
            m_itemIcon.Dispose();
        base.OnClose();
    }


}


