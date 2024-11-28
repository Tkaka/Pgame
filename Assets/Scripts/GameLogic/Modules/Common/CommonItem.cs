using UI_Common;
using FairyGUI;
using FairyGUI.Utils;
using Data.Beans;
using UnityEngine;
using System;

public class CommonItem : UI_ItemIcon
{

    //道具id
    public int itemId;

    //道具数量 
    public int itemNum;
    public bool isShowNum = false;

    public string strText;

    string winName;

    public new static CommonItem CreateInstance()
    {
        return UI_ItemIcon.CreateInstance() as CommonItem;
    }

    public void AddPopupEvent()
    {
        m_toucher.onTouchBegin.Add(OnItemTouchBegin);
        m_toucher.onTouchEnd.Add(OnItemTouchEnd);
        m_toucher.onRollOut.Add(OnItemTouchEnd);
    }

    public void SetIconScale(float scaleX, float scaleY)
    {
        this.scaleX = scaleX;
        this.scaleY = scaleY;
        this.height = height * scaleY;
        this.width = width * scaleX;
    }

    public void Init(int itemId, int itemNum = 0, bool isShowNum = false, bool isShowUseNum = false)
    {
        this.itemId = itemId;
        this.itemNum = itemNum;
        this.isShowNum = isShowNum;
    }

    public void SetEmptyIcon(bool isEmpty = true)
    {
        if (isEmpty)
        {
            //默认一个品质框

            UIGloader.SetUrl(m_borderLoader, UIUtils.GetIocnBorderByQuility(0));
            m_nameLabel.visible = false;
            m_fragIcon.visible = false;
            m_iconLoader.visible = false;
            m_imgSelect.visible = false;
            m_numTxt.visible = false;
            m_junZhouGroup.visible = false;
            this.touchable = false;

        }
        else
        {
            m_nameLabel.visible = true;
            m_fragIcon.visible = true;
            m_iconLoader.visible = true;
            m_imgSelect.visible = true;
            m_numTxt.visible = true;
            this.touchable = true;
        }
 
    }

    public void SelectToggle(bool flag)
    {
        m_imgSelect.visible = flag;
    }
    public void RefreshView(bool isShowName = false, Color? numColor = null)
    {
        SetEmptyIcon(false);

        m_numTxt.visible = isShowNum;
  
        t_itemBean bean = ConfigBean.GetBean<t_itemBean, int>(itemId);
        if (bean != null)
        {
            // 设置品质框
            if (bean.t_type == (int)ItemType.EquipShenPinJuanZhou)
            {
                SetJuanZhouQuality();
            }
            else
            {
                if (!string.IsNullOrEmpty(bean.t_quality))
                    UIGloader.SetUrl(m_borderLoader, UIUtils.GetItemBorder(itemId, itemNum));
                m_junZhouGroup.visible = false;
            }

            UIGloader.SetUrl(m_iconLoader, UIUtils.GetItemIcon(itemId, itemNum));
            m_numTxt.text = string.IsNullOrEmpty(strText) ? itemNum + "" : strText;
            m_numTxt.color = numColor == null ? Color.white : numColor.Value;
            if (bean.t_type == (int)ItemType.PetFragment || bean.t_type == (int)ItemType.EquipStarFrag)
            {
                m_fragIcon.visible = true;
            }
            else
            {
                m_fragIcon.visible = false;
            }

            m_nameLabel.visible = isShowName;
            if (isShowName)
            {
                m_nameLabel.text = bean.t_name;
                this.height += m_nameLabel.height;
                m_nameLabel.color = UIUtils.GetItemColor(itemId, itemNum);
            }
        }

        m_imgSelect.visible = false;
    }

    private void OnItemTouchBegin()
    {
        ThreeParam<int, Vector2, GComponent> threePara = new ThreeParam<int, Vector2, GComponent>();
        threePara.value1 = itemId;
        threePara.value2 = new Vector2(this.x + this.size.x * 0.5f, this.y);
        threePara.value3 = this.parent;

        if (winName != null)
            WinMgr.Singleton.Close(winName);
        winName = WinMgr.Singleton.Open<ItemTipsWindow>(WinInfo.Create(false, null, true, threePara), UILayer.TopHUD);
    }

    private void OnItemTouchEnd()
    {
        WinMgr.Singleton.Close(winName);
        winName = null;
    }

    public override void Dispose()
    {
        base.Dispose();
    }

    private void SetJuanZhouQuality()
    {
        t_itemBean bean = ConfigBean.GetBean<t_itemBean, int>(itemId);
        if (bean != null)
        {
            int quality = int.Parse(bean.t_quality);
            UIGloader.SetUrl(m_borderLoader, UIUtils.GetLoaderUrl(WinEnum.UI_Common, UIUtils.GetBorderByQuality(quality)));

            t_color_name_beanBean colorNameBean = ConfigBean.GetBean<t_color_name_beanBean, int>(quality);
            if (colorNameBean != null)
            {
                if (colorNameBean.t_num > 0)
                {
                    m_junZhouGroup.visible = true;
                    m_junZhouQualityNum.text = string.Format("+{0}", colorNameBean.t_num);
                }
                else
                {
                    m_junZhouGroup.visible = false;
                }
            }
        }
    }
}
