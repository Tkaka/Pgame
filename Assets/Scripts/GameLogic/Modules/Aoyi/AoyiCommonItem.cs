using UI_AoYi;
using UI_Common;
using Message.Profound;
using Message.Pet;
using FairyGUI;
using System.Collections.Generic;
using Data.Beans;
using UnityEngine;
using System;


public class AoyiCommonItem : UI_AoyiCommonItem
{
    private int m_itemId;
    private int m_level;
    private string m_wndName;
    public new static AoyiCommonItem CreateInstance()
    {
        return UI_AoyiCommonItem.CreateInstance() as AoyiCommonItem;
    }

    public void RefreshView(int itemId, int level = 0, bool isShowLevel = true)
    {
        t_itemBean bean = ConfigBean.GetBean<t_itemBean, int>(itemId);
        if (bean != null)
        {
            // è®¾ç½®å“è´¨æ¡†

            if (!string.IsNullOrEmpty(bean.t_quality))
                UIGloader.SetUrl(m_imgBorder, UIUtils.GetItemBorder(itemId));



            UIGloader.SetUrl(m_imgIcon, UIUtils.GetItemIcon(itemId));
            m_txtLevel.text = "+" +level;
        }

        t_aoyiBean aoyiBean = ConfigBean.GetBean<t_aoyiBean, int>(itemId);
        if (aoyiBean != null)
        {
            m_txtType.text = _GetTypeName(aoyiBean.t_type);
        }

        m_imgIcon.color = UIUtils.GetItemColor(itemId);
        m_imgSelect.visible = false;
        m_txtLevel.visible = isShowLevel;
    }

    //è®¾ç½®ç­‰çº§ä¸ºæ•°é‡(ä¸“ä¸ºèŽ·å¾—æ—¶å€™çš„å åŠ å¤„ç†)
    public void SetNum(int num)
    {
        m_txtLevel.visible = true;
        m_txtLevel.text = num + "";
    }

    private string _GetTypeName(int type)
    {
        string str = "";
        switch ((AoyiService.EAoyiType)type)
        {
            case AoyiService.EAoyiType.GongFang:
                str = "攻·防";
                break;
            case AoyiService.EAoyiType.GongXue:
                str = "攻·血";
                break;
            case AoyiService.EAoyiType.FangXue:
                str = "防·血";
                break;
            case AoyiService.EAoyiType.Shang:
                str = "伤";
                break;
            case AoyiService.EAoyiType.Mian:
                str = "免";
                break;
            default:
                break;
        }

        return str;
    }
    public void SelectToggle(bool flag)
    {
        m_imgSelect.visible = flag;
    }

    public void AddPopupEvent()
    {
        this.onTouchBegin.Add(OnItemTouchBegin);
        this.onTouchEnd.Add(OnItemTouchEnd);
        this.onRollOut.Add(OnItemTouchEnd);
    }

    private void OnItemTouchBegin()
    {
        FourParam<int, Vector2, GComponent, int> threePara = new FourParam<int, Vector2, GComponent, int>();
        threePara.value1 = m_itemId;
        threePara.value2 = new Vector2(this.x + this.size.x * 0.5f, this.y);
        threePara.value3 = this.parent;
        threePara.value4 = m_level;

        if (m_wndName != null)
            WinMgr.Singleton.Close(m_wndName);
        m_wndName = WinMgr.Singleton.Open<AoyiTipsWnd>(WinInfo.Create(false, null, true, threePara), UILayer.TopHUD);
    }

    private void OnItemTouchEnd()
    {
        WinMgr.Singleton.Close(m_wndName);
        m_wndName = null;
    }
}