using Message.Role;
using UnityEngine;
using FairyGUI;
using UI_Shop;
using Message.Shop;
using Data.Beans;
using System.Collections.Generic;


//购买商品窗口
public class SuiPianBuyShopWnd : BaseWindow
{
    private UI_SuiPianBuyShopWnd m_window;
    private ShopInfo m_info;
    private int m_shopType;         //商店类型
    private int m_curNum = 1;       //当前数量
    private int m_maxNum = 1;       //最大数量

    public override void OnOpen()
    {
        base.OnOpen();
        m_window = getUiWindow<UI_SuiPianBuyShopWnd>();
        m_window.m_popupView.m_btnClose.onClick.Add(Close);
        m_window.m_mask.onClick.Add(Close);
        m_window.m_popupView.m_btnBuy.onClick.Add(_OnBuyClick);
        m_window.m_popupView.m_btnAdd.onClick.Add(_OnAddClick);
        m_window.m_popupView.m_btnSub.onClick.Add(_OnSubClick);
        m_window.m_popupView.m_btnAddMax.onClick.Add(_OnAddMaxClick);

        TwoParam<ShopInfo, int> twoParam = Info.param as TwoParam<ShopInfo, int>;
        if (twoParam == null)
            return;
        m_info = twoParam.value1;
        m_shopType = twoParam.value2;

        InitView();
        PlayPopupAnim(m_window.m_mask, m_window.m_popupView);
    }

    public override void InitView()
    {
        base.InitView();

        t_shop_item_libBean shopLibBean = ConfigBean.GetBean<t_shop_item_libBean, int>(m_info.shopId);
        if (shopLibBean == null)
        {
            return;
        }

        t_shopBean shopBean = ConfigBean.GetBean<t_shopBean, int>(m_shopType * 100 + m_info.index);
        if (shopBean == null)
            return;

        _ShowItemInfo(shopLibBean.t_itemId);

        UIGloader.SetUrl(m_window.m_popupView.m_imgCoin, UIUtils.GetItemIcon(m_info.currency));

        if (m_info.price != 0)
        {
            long num = RoleService.Singleton.GetCurrencyNum(m_info.currency) / m_info.price;
            if (num == 0)
                m_maxNum = 1;
            else
                m_maxNum = (int)num;
        }

        _ShowBuyNum();


    }

    private void _ShowBuyNum()
    {
        m_window.m_popupView.m_txtCoinNum.text = (m_info.price * m_curNum) + "";
        m_window.m_popupView.m_txtNum.text = m_curNum + "";
        _ShowBtnInfo();
    }

    private void _ShowBtnInfo()
    {
        if (m_curNum == m_maxNum)
        {
            m_window.m_popupView.m_btnAdd.enabled = false;
            m_window.m_popupView.m_btnAddMax.enabled = false;
            m_window.m_popupView.m_btnAdd.grayed = true;
            m_window.m_popupView.m_btnAddMax.grayed = true;
        }
        else
        {
            m_window.m_popupView.m_btnAdd.enabled = true;
            m_window.m_popupView.m_btnAddMax.enabled = true;
            m_window.m_popupView.m_btnAdd.grayed = false;
            m_window.m_popupView.m_btnAddMax.grayed = false;
        }

        if (m_curNum == 1)
        {
            m_window.m_popupView.m_btnSub.enabled = false;
            m_window.m_popupView.m_btnSub.grayed = true;
        }
        else
        {
            m_window.m_popupView.m_btnSub.enabled = true;
            m_window.m_popupView.m_btnSub.grayed = false;
        }
    }

    private void _ShowItemInfo(int itemId)
    {
        t_itemBean itemBean = ConfigBean.GetBean<t_itemBean, int>(itemId);
        if (itemBean == null)
        {
            return;
        }

        m_window.m_popupView.m_txtName.text = itemBean.t_name;
        m_window.m_popupView.m_txtHaveNum.text = BagService.Singleton.GetItemNum(itemId) + "";
        m_window.m_popupView.m_txtDescribe.text = itemBean.t_describe;

        CommonItem commonItem = m_window.m_popupView.m_itemIcon as CommonItem;
        commonItem.Init(itemId);
        commonItem.RefreshView();

    }

    //购买点击
    private void _OnBuyClick()
    {
        ShopService.Singleton.ReqBuy(m_shopType, m_info.index, m_info.shopId, m_curNum);
        Close();
    }

    private void _OnAddClick()
    {
        if (m_curNum >= m_maxNum)
            return;

        m_curNum++;
        _ShowBuyNum();
    }

    private void _OnSubClick()
    {
        if (m_curNum == 1)
            return;

        m_curNum--;
        _ShowBuyNum();
    }

    private void _OnAddMaxClick()
    {
        m_curNum = m_maxNum;
        _ShowBuyNum();
    }

    protected override void OnClose()
    {
        base.OnClose();

    }
}