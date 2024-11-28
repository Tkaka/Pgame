using Message.Role;
using UnityEngine;
using FairyGUI;
using UI_Shop;
using Message.Shop;
using Data.Beans;
using System.Collections.Generic;


public class MysteriousShopWnd : BaseWindow
{
    private UI_MysteriousShopWnd m_window;
    private DoActionInterval m_normalTime;

    private ResGoodsInfo m_curGoodsInfo;
    ShopCommonList m_shopList;

    public override void OnOpen()
    {
        base.OnOpen();
        m_window = getUiWindow<UI_MysteriousShopWnd>();
        m_window.m_popupView.m_btnClose.onClick.Add(Close);
        m_window.m_mask.onClick.Add(Close);

        m_shopList = m_window.m_popupView.m_objList as ShopCommonList;
        if (m_shopList != null)
        {
            m_shopList.Init();
        }
        else
        {
            Close();
            return;
        }
        InitView();
        PlayPopupAnim(m_window.m_mask, m_window.m_popupView);
    }

    public override void AddEventListener()
    {
        base.AddEventListener();
        GED.ED.addListener(EventID.ShopPrepareRefresh, _OnPrepareRefresh);
        GED.ED.addListener(EventID.ShopRefresh, _OnRefresh);
        GED.ED.addListener(EventID.ShopBuyResult, _OnBuyedResult);
        GED.ED.addListener(EventID.ShopOpenOrClose, _ShopOpenOrClose);
    }

    public override void RemoveEventListener()
    {
        base.RemoveEventListener();
        GED.ED.removeListener(EventID.ShopPrepareRefresh, _OnPrepareRefresh);
        GED.ED.removeListener(EventID.ShopRefresh, _OnRefresh);
        GED.ED.removeListener(EventID.ShopBuyResult, _OnBuyedResult);
        GED.ED.removeListener(EventID.ShopOpenOrClose, _ShopOpenOrClose);
    }

    public override void InitView()
    {
        base.InitView();

        if (ShopService.Singleton.GetShopInfo(ShopService.EShopType.Mysterious) != null)
        {
            if (ShopService.Singleton.GetShopIsRefreshed(ShopService.EShopType.Mysterious))
            {
                ShopService.Singleton.ReqGoodsInfo((int)ShopService.EShopType.Mysterious);
                return;
            }
            _ShowShopInfo(ShopService.Singleton.GetShopInfo(ShopService.EShopType.Mysterious));
        }
        else
        {
            ShopService.Singleton.ReqGoodsInfo((int)ShopService.EShopType.Mysterious);
        }


    }

    private void _ShopOpenOrClose(GameEvent evt)
    {
        ShopService.EShopType shop = (ShopService.EShopType)evt.Data;
        if (shop != ShopService.EShopType.Mysterious)
            return;

        Close();
    }


    //准备刷新
    private void _OnPrepareRefresh(GameEvent evt)
    {
        List<int> shopTypes = evt.Data as List<int>;
        if (shopTypes == null)
            return;

        for (int i = 0; i < shopTypes.Count; i++)
        {
            if (shopTypes[i] == (int)ShopService.EShopType.Mysterious)
            {
                ShopService.Singleton.ReqGoodsInfo((int)ShopService.EShopType.Mysterious);
                break;
            }
        }

    }

    //开始刷新
    private void _OnRefresh(GameEvent evt)
    {
        if (ShopService.Singleton.GetShopInfo(ShopService.EShopType.Mysterious) != null)
        {
            _ShowShopInfo(ShopService.Singleton.GetShopInfo(ShopService.EShopType.Mysterious));
        }
    }

    //购买刷新
    private void _OnBuyedResult(GameEvent evt)
    {
        ThreeParam<int, int, int> param = evt.Data as ThreeParam<int, int, int>;
        if (param == null)
            return;
        if (param.value3 != (int)ShopService.EShopType.Mysterious)
            return;

        if (ShopService.Singleton.GetShopInfo(ShopService.EShopType.Mysterious) == null)
        {
            return;

        }


        _ShowShopInfo(ShopService.Singleton.GetShopInfo(ShopService.EShopType.Mysterious));
    }

    //显示商店信息
    private void _ShowShopInfo(ResGoodsInfo info)
    {
        if (info == null)
            return;

        m_curGoodsInfo = info;

        m_shopList.RefreshView(m_curGoodsInfo);
        _ShowShopCloseCountDown();
    }


    //显示神秘商店关闭倒计时
    private void _ShowShopCloseCountDown()
    {

        int remainTime = -(int)(TimeUtils.CalculateDelta(m_curGoodsInfo.refreshTime) / 1000);
        if (remainTime <= 0)
        {
            return;
        }

        if (m_normalTime != null)
        {
            m_normalTime.kill();
            m_normalTime = null;
        }

        m_normalTime = new DoActionInterval();
        m_normalTime.doAction(1, (param) =>
        {
            remainTime--;
            if (remainTime <= 0)
            {
                Close();
                return;
            }
            m_window.m_popupView.m_txtTime.text = TimeUtils.FormatTime(remainTime);
        });

    }

    protected override void OnClose()
    {
        base.OnClose();

        if (m_normalTime != null)
            m_normalTime.kill();


    }

}