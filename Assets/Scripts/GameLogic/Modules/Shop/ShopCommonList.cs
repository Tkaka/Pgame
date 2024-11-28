using Message.Role;
using UnityEngine;
using FairyGUI;
using UI_Shop;
using Message.Shop;
using Data.Beans;
using System.Collections.Generic;


public class ShopCommonList : UI_ShopCommonList
{

    private ResGoodsInfo m_curGoodsInfo;
    private long m_supperRefreshTime = 0;
    private DoActionInterval m_supperTime;
    private Dictionary<GTextField, bool> m_txtTimeDic = new Dictionary<GTextField, bool>();

    public void Init()
    {
        m_mainList.scrollPane.onScroll.Add(_OnScroll);
        m_btnLeft.onClick.Add(_OnLeftClick);
        m_btnRight.onClick.Add(_OnRightClick);
        m_mainList.SetVirtual();
        m_mainList.itemProvider = _itemProvider;
        m_mainList.itemRenderer = _itemRenderer;
        m_btnLeft.visible = false;
        m_btnRight.visible = true;
    }

    public void RefreshView(ResGoodsInfo shopInfo)
    {
        if (shopInfo == null)
            return;

        m_curGoodsInfo = shopInfo;
        m_supperRefreshTime = 0;
        m_txtTimeDic.Clear();
        m_curGoodsInfo.infos.Sort((a, b) => a.index.CompareTo(b.index));
        m_mainList.numItems = m_curGoodsInfo.infos.Count;
        m_mainList.ScrollToView(0);
    }



    private void _OnLeftClick()
    {
        m_mainList.ScrollToView(0, true, true);

    }

    private void _OnRightClick()
    {
        m_mainList.ScrollToView(m_mainList.numItems - 1, true, true);
    }


    private void _OnScroll()
    {

        m_btnLeft.visible = false;
        m_btnRight.visible = false;
        if (!(m_mainList.scrollPane.posX == 0))
        {
            m_btnLeft.visible = true;
        }

        if (!m_mainList.scrollPane.isRightMost)
        {
            m_btnRight.visible = true;
        }

    }

    private string _itemProvider(int index)
    {
        if (index < 0 || index >= m_curGoodsInfo.infos.Count)
            return "";

        ShopInfo shopInfo = m_curGoodsInfo.infos[index];

        t_shopBean shopBean = ConfigBean.GetBean<t_shopBean, int>(m_curGoodsInfo.shopType * 100 + shopInfo.index);
        if (shopBean == null)
        {
            return "";
        }

        if (shopBean.t_super == 1)
        {
            return UI_supperGoodsCell.URL;
        }

        return UI_goodsCell.URL;
    }

    private void _itemRenderer(int index, GObject obj)
    {
        if (index < 0 || index >= m_curGoodsInfo.infos.Count)
            return;

        ShopInfo shopInfo = m_curGoodsInfo.infos[index];

        t_shopBean shopBean = ConfigBean.GetBean<t_shopBean, int>(m_curGoodsInfo.shopType * 100 + shopInfo.index);
        if (shopBean == null)
        {
            return;
        }

        if (shopBean.t_super == 1)
        {
            UI_supperGoodsCell supperCell = obj as UI_supperGoodsCell;
            _OnSupperShopCellShow(supperCell, shopInfo);
        }
        else
        {
            UI_goodsCell cell = obj as UI_goodsCell;
            _OnNormalShopCellShow(cell, shopInfo);
        }

        obj.onClick.Clear();
        obj.onClick.Add(() =>
        {
            TwoParam<ShopInfo, int> param = new TwoParam<ShopInfo, int>();
            param.value1 = shopInfo;
            param.value2 = m_curGoodsInfo.shopType;
            switch ((ShopService.EShopType)m_curGoodsInfo.shopType)
            {
                case ShopService.EShopType.Convert:
                    if(shopBean.t_super == 1)
                        WinMgr.Singleton.Open<BuyShopWnd>(WinInfo.Create(false, null, false, param), UILayer.Popup);
                    else
                        WinMgr.Singleton.Open<SuiPianBuyShopWnd>(WinInfo.Create(false, null, false, param), UILayer.Popup);
                    break;
                default:
                    WinMgr.Singleton.Open<BuyShopWnd>(WinInfo.Create(false, null, false, param), UILayer.Popup);
                    break;
            }
             
        });
    }

    //超级商品格子信息显示
    private void _OnSupperShopCellShow(UI_supperGoodsCell cell, ShopInfo info)
    {
        t_shop_item_libBean shopBean = ConfigBean.GetBean<t_shop_item_libBean, int>(info.shopId);
        if (shopBean == null)
        {
            return;
        }

        t_itemBean itemBean = ConfigBean.GetBean<t_itemBean, int>(shopBean.t_itemId);
        if (itemBean == null)
        {
            return;
        }
        CommonItem itemIcon = cell.m_imgIcon as CommonItem;
        itemIcon.itemId = shopBean.t_itemId;
        itemIcon.isShowNum = false;
        itemIcon.RefreshView();

        cell.m_txtName.text = info.num > 1 ? string.Format("{0}X{1}", itemBean.t_name, info.num) : itemBean.t_name;
        cell.m_txtName.color = UIUtils.GetItemColor(shopBean.t_itemId);
        cell.m_imgSellOut.visible = info.buyNum == 0;


        UIGloader.SetUrl(cell.m_imgComsume, UIUtils.GetBuyGoodsPriceIcon(info.currency));
        cell.m_txtNum.text = info.price + "";

        int disCount = info.discount / 1000;
        cell.m_txtDiscount.text = string.Format("{0}折", disCount);
        cell.m_objDiscount.visible = disCount < 10;
        int totalRefresgCount = ConfigBean.GetBean<t_globalBean, int>(130104).t_int_param;
        cell.m_txtCount.text = string.Format("{0}/{1}", totalRefresgCount - info.refreshCount, totalRefresgCount);
        if (m_supperRefreshTime == 0)
        {
            m_supperRefreshTime = info.refreshTime;
        }

        _ShowSupperShopRefreshCountDown(cell.m_txtTime);
    }

    //普通商品格子信息显示
    private void _OnNormalShopCellShow(UI_goodsCell cell, ShopInfo info)
    {
        t_shop_item_libBean shopBean = ConfigBean.GetBean<t_shop_item_libBean, int>(info.shopId);
        if (shopBean == null)
        {
            return;
        }

        t_itemBean itemBean = ConfigBean.GetBean<t_itemBean, int>(shopBean.t_itemId);
        if (itemBean == null)
        {
            return;
        }
        CommonItem itemIcon = cell.m_imgIcon as CommonItem;
        itemIcon.itemId = shopBean.t_itemId;
        itemIcon.isShowNum = false;
        itemIcon.RefreshView();


        cell.m_txtName.text = info.num > 1 ? string.Format("{0}X{1}", itemBean.t_name, info.num) : itemBean.t_name;
        cell.m_txtName.color = UIUtils.GetItemColor(shopBean.t_itemId);
        cell.m_imgSellOut.visible = info.buyNum == 0;


        UIGloader.SetUrl(cell.m_imgComsume, UIUtils.GetBuyGoodsPriceIcon(info.currency));
        cell.m_txtNum.text = info.price + "";


        int disCount = info.discount / 1000;
        cell.m_txtDiscount.text = string.Format("{0}折", disCount);
        cell.m_objDiscount.visible = disCount < 10;

    }

    //显示本商店超级商品的刷新时间
    private void _ShowSupperShopRefreshCountDown(GTextField txtTime)
    {
        if (m_txtTimeDic.ContainsKey(txtTime))
        {
            return;

        }
        else
        {
            m_txtTimeDic.Add(txtTime, true);
        }

        if (m_supperTime != null)
        {
            m_supperTime.kill();
            m_supperTime = null;
        }

        int remainTime = -(int)(TimeUtils.CalculateDelta(m_supperRefreshTime) / 1000);

        if (remainTime <= 0)
        {
            //请求刷新
            return;
        }

        m_supperTime = new DoActionInterval();
        m_supperTime.doAction(1, (param) =>
        {
            remainTime -= 1;
            if (remainTime <= 0)
            {
                m_supperTime.kill();
                m_supperTime = null;
                //请求刷新

                return;
            }
            string strTime = TimeUtils.FormatTime(remainTime);
            foreach (var obj in m_txtTimeDic)
            {
                GTextField txt = obj.Key;
                if (txt != null)
                {
                    txt.text = strTime;
                }
            }

        }, null, true);
    }

}