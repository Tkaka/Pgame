using Message.Role;
using UnityEngine;
using FairyGUI;
using UI_Shop;
using Message.Shop;
using Data.Beans;
using System.Collections.Generic;


//购买商品窗口
public class BuyShopWnd : BaseWindow
{
    private UI_BuyShopWnd m_window;
    private ShopInfo m_info;
    private int m_shopType;         //商店类型
    private DoActionInterval m_refreshTimer;

    public override void OnOpen()
    {
        base.OnOpen();
        m_window = getUiWindow<UI_BuyShopWnd>();
        m_window.m_popupView.m_btnClose.onClick.Add(Close);
        m_window.m_mask.onClick.Add(Close);
        m_window.m_popupView.m_btnBuy.onClick.Add(_OnBuyClick);
        m_window.m_popupView.m_btnBuy2.onClick.Add(_OnBuyClick);
        m_window.m_popupView.m_btnRefresh.onClick.Add(_OnRefreshClick);
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

        m_window.m_popupView.m_txtBuyNum.text = m_info.num + "";

        //int[] arrPriceInfo = GTools.splitStringToIntArray(shopLibBean.t_price);
        //if (arrPriceInfo != null && arrPriceInfo.Length >= 3)
        //{
            //m_window.m_imgCoin.url = UIUtils.GetItemIcon(m_info.currency);
        UIGloader.SetUrl(m_window.m_popupView.m_imgCoin, UIUtils.GetBuyGoodsPriceIcon(m_info.currency));
            m_window.m_popupView.m_txtCoinNum.text = m_info.price + "";
        //}

        if (shopBean.t_super == 1)
        {
            m_window.m_popupView.m_supperGroup.visible = true;
            m_window.m_popupView.m_btnBuy2.visible = false;
            m_window.m_popupView.m_btnBuy.visible = true;
            m_window.m_popupView.m_btnRefresh.visible = true;
            int totalCount = ConfigBean.GetBean<t_globalBean, int>(130104).t_int_param;
            m_window.m_popupView.m_txtRefreshTime.text = string.Format("{0}/{1}", totalCount - m_info.refreshCount, totalCount);

            _ShowRefreshCountDown();
 
        }
        else
        {
            m_window.m_popupView.m_supperGroup.visible = false;
            m_window.m_popupView.m_btnBuy2.visible = true;
            m_window.m_popupView.m_btnBuy.visible = false;
            m_window.m_popupView.m_btnRefresh.visible = false;
        }

        if (m_info.buyNum == 0)
        {
            //售罄
            m_window.m_popupView.m_btnBuy.enabled = false;
            m_window.m_popupView.m_btnBuy.grayed = true;
            m_window.m_popupView.m_btnBuy2.enabled = false;
            m_window.m_popupView.m_btnBuy2.grayed = true;
        }
        else
        {
            m_window.m_popupView.m_btnBuy.enabled = true;
            m_window.m_popupView.m_btnBuy.grayed = false;
            m_window.m_popupView.m_btnBuy2.enabled = true;
            m_window.m_popupView.m_btnBuy2.grayed = false;
        }
        

        _ShowItemInfo(shopLibBean.t_itemId);
 


    }

    private void _ShowItemInfo(int itemId)
    {
        t_itemBean itemBean = ConfigBean.GetBean<t_itemBean, int>(itemId);
        if (itemBean == null)
        {
            return;
        }

        m_window.m_popupView.m_txtName.text = itemBean.t_name;
        m_window.m_popupView.m_txtName.color = UIUtils.GetItemColor(itemId);
        m_window.m_popupView.m_txtHaveNum.text = BagService.Singleton.GetItemNum(itemId) + "";
        m_window.m_popupView.m_txtDescribe.text = itemBean.t_describe;

        CommonItem itemIcon = m_window.m_popupView.m_itemIcon as CommonItem;
        itemIcon.Init(itemId, 0, false);
        itemIcon.RefreshView();

    }

    //显示超级商品倒计时
    private void _ShowRefreshCountDown()
    {
        if (m_refreshTimer != null)
        {
            m_refreshTimer.kill();
            m_refreshTimer = null;
        }

        int remainTime = -(int)(TimeUtils.CalculateDelta(m_info.refreshTime)/ 1000); //ShopService.Singleton.GetToTargetRemainTime(130102);
        if (remainTime <= 0)
        {
            return;
        }
        m_refreshTimer = new DoActionInterval();
        m_refreshTimer.doAction(1, (param) =>
        {
            remainTime --;
            if (remainTime <= 0)
            {
                m_refreshTimer.kill();
                m_refreshTimer = null;

                return;
            }
            string strTime = TimeUtils.FormatTime(remainTime);
            m_window.m_popupView.m_txtTime.text = strTime;

        }, null, true);
    }

    //购买点击
    private void _OnBuyClick()
    {
        ShopService.Singleton.ReqBuy(m_shopType, m_info.index, m_info.shopId, 1);
        Close();
    }

    //刷新点击
    private void _OnRefreshClick()
    {
        int totalRefresgCount = ConfigBean.GetBean<t_globalBean, int>(130104).t_int_param;
        t_globalBean gBean = ConfigBean.GetBean<t_globalBean, int>(130106);
        int[] arr = GTools.splitStringToIntArray(gBean.t_string_param, '+');
        int comsume = 0;
        if (m_info.refreshCount > arr.Length - 1)
        {
            comsume = arr[arr.Length - 1];
        }
        else
        {
            comsume = arr[m_info.refreshCount];
        }

        string strDes = string.Format("立即刷新商店(不包括超级商品)需花费钻石：{0}\n今日剩余次数：{1}/{2}",
            comsume, totalRefresgCount - m_info.refreshCount, totalRefresgCount);
        CommonTipsManager.GetInstance().ShowTips(TipsType.SingleButton, "刷新商店", strDes, () =>
        {
            ShopService.Singleton.ReqRefresh(m_shopType, 2, m_info.index);
            Close();
        });
         
    }

    protected override void OnClose()
    {
        base.OnClose();
        if(m_refreshTimer != null)
             m_refreshTimer.kill();
    }
}