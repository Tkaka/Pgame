using Message.Role;
using UnityEngine;
using FairyGUI;
using UI_Shop;
using Message.Shop;
using Data.Beans;
using System.Collections.Generic;


public class SuiPianDuiHuanWnd : BaseWindow
{
    private UI_SuiPianDuiHuanWnd m_window;
    private DoActionInterval m_normalTime;

    private ResGoodsInfo m_curGoodsInfo;
    ShopCommonList m_shopList;

    public override void OnOpen()
    {
        base.OnOpen();
        m_window = getUiWindow<UI_SuiPianDuiHuanWnd>();
        m_window.m_popupView.m_btnClose.onClick.Add(Close);
        m_window.m_mask.onClick.Add(Close);
        m_window.m_popupView.m_btnRefresh.onClick.Add(_OnRefreshClick);
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
    }

    public override void RemoveEventListener()
    {
        base.RemoveEventListener();
        GED.ED.removeListener(EventID.ShopPrepareRefresh, _OnPrepareRefresh);
        GED.ED.removeListener(EventID.ShopRefresh, _OnRefresh);
        GED.ED.removeListener(EventID.ShopBuyResult, _OnBuyedResult);
    }

    public override void InitView()
    {
        base.InitView();
        _ShowShopCurrenyInfo();

        if (ShopService.Singleton.GetShopInfo(ShopService.EShopType.Convert) != null)
        {
            if (ShopService.Singleton.GetShopIsRefreshed(ShopService.EShopType.Convert))
            {
                ShopService.Singleton.ReqGoodsInfo((int)ShopService.EShopType.Convert);
                return;
            }
            _ShowShopInfo(ShopService.Singleton.GetShopInfo(ShopService.EShopType.Convert));
        }
        else
        {
            ShopService.Singleton.ReqGoodsInfo((int)ShopService.EShopType.Convert);
        }


    }

    //准备刷新
    private void _OnPrepareRefresh(GameEvent evt)
    {
        List<int> shopTypes = evt.Data as List<int>;
        if (shopTypes == null)
            return;

        for (int i = 0; i < shopTypes.Count; i++)
        {
            if (shopTypes[i] == (int)ShopService.EShopType.Convert)
            {
                ShopService.Singleton.ReqGoodsInfo((int)ShopService.EShopType.Convert);
                break;
            }
        }

    }

    //开始刷新
    private void _OnRefresh(GameEvent evt)
    {
        if (ShopService.Singleton.GetShopInfo(ShopService.EShopType.Convert) != null)
        {
            _ShowShopInfo(ShopService.Singleton.GetShopInfo(ShopService.EShopType.Convert));
        }
    }

    //购买刷新
    private void _OnBuyedResult(GameEvent evt)
    {
        ThreeParam<int, int, int> param = evt.Data as ThreeParam<int, int, int>;
        if (param == null)
            return;
        if (param.value3 != (int)ShopService.EShopType.Convert)
            return;

        if (ShopService.Singleton.GetShopInfo(ShopService.EShopType.Convert) == null)
        {
            return;

        }


        _ShowShopInfo(ShopService.Singleton.GetShopInfo(ShopService.EShopType.Convert));
    }

    //显示商店信息
    private void _ShowShopInfo(ResGoodsInfo info)
    {
        if (info == null)
            return;

        m_curGoodsInfo = info;

        m_shopList.RefreshView(m_curGoodsInfo);

        _ShowNormalShopRefreshInfo(info);
    }

    //显示商店需要的货币信息
    private void _ShowShopCurrenyInfo()
    {
        t_globalBean gBean = ConfigBean.GetBean<t_globalBean, int>(130201);
        if (gBean == null)
            return;

        string[] strInfo = GTools.splitString(gBean.t_string_param, ';');
        for (int i = 0; i < strInfo.Length; i++)
        {
            int[] arrInfo = GTools.splitStringToIntArray(strInfo[i], '+');
            if (arrInfo == null || arrInfo.Length < 3)
                continue;
            if (arrInfo[0] == (int)ShopService.EShopType.Convert)
            {
                //m_window.m_imgComsume.url = UIUtils.GetItemIcon(arrInfo[1]);
                UIGloader.SetUrl(m_window.m_popupView.m_imgComsume, UIUtils.GetItemIcon(arrInfo[1]));
                m_window.m_popupView.m_txtNum.text = RoleService.Singleton.GetCurrencyNum(arrInfo[1]) + "";
                m_window.m_popupView.m_txtCoinDes.text = UIUtils.GetStrByLanguageID(arrInfo[2]);
                break;
            }
        }

    }




    //显示本商店普通商品的刷新信息
    private void _ShowNormalShopRefreshInfo(ResGoodsInfo info)
    {
        int totalRefresgCount = ConfigBean.GetBean<t_globalBean, int>(130103).t_int_param;
        m_window.m_popupView.m_txtCount.text = string.Format("{0}/{1}", totalRefresgCount - info.refreshCount, totalRefresgCount);
        int remainTime = -(int)(TimeUtils.CalculateDelta(info.refreshTime) / 1000);// ShopService.Singleton.GetToTargetRemainTime(130101);
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
                m_normalTime.kill();
                m_normalTime = null;
                return;
            }
            m_window.m_popupView.m_txtTime.text = TimeUtils.FormatTime4(remainTime);
        });

    }


    //刷新点击
    private void _OnRefreshClick()
    {
        if (ShopService.Singleton.GetShopInfo(ShopService.EShopType.Convert) != null)
        {
            ResGoodsInfo shopInfo = ShopService.Singleton.GetShopInfo(ShopService.EShopType.Convert);
            int totalRefresgCount = ConfigBean.GetBean<t_globalBean, int>(130103).t_int_param;
            t_globalBean gBean = ConfigBean.GetBean<t_globalBean, int>(130105);
            int[] arr = GTools.splitStringToIntArray(gBean.t_string_param, '+');
            int comsume = 0;
            if (shopInfo.refreshCount > arr.Length - 1)
            {
                comsume = arr[arr.Length - 1];
            }
            else
            {
                comsume = arr[shopInfo.refreshCount];
            }

            string strDes = string.Format("立即刷新商店(不包括超级商品)需花费钻石：{0}\n今日剩余次数：{1}/{2}",
                comsume, totalRefresgCount - shopInfo.refreshCount, totalRefresgCount);
            CommonTipsManager.GetInstance().ShowTips(TipsType.SingleButton, "刷新商店", strDes, () =>
            {
                ShopService.Singleton.ReqRefresh(shopInfo.shopType, 1);
            });

        }
    }



    protected override void OnClose()
    {
        base.OnClose();
 
        if (m_normalTime != null)
            m_normalTime.kill();


    }

}