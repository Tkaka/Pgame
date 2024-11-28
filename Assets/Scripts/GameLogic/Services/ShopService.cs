using Message.Shop;
using System.Collections.Generic;
using Message.Role;
using Data.Beans;
using UnityEngine;

public class ShopService : SingletonService<ShopService>
{

    public enum EShopType
    {
        Sundry = 1,     // 杂货商店
        Honor = 2,      // 荣誉商店
        Trial = 3,      // 试练商店
        Guild = 4,      // 工会商店
        Prop = 5,       // 道具商店
        Convert = 6,    // 兑换商店
        Mysterious = 7, // 神秘商店

    }

    private Dictionary<EShopType, ResGoodsInfo> m_shopsDic;  //商店信息
    private Dictionary<EShopType, bool> m_shopRefreshFlagDic;  //商店需要刷新的标记
    private ResEquipBoxInfo m_equipBoxInfo;                  //装备宝箱信息
    private Dictionary<EShopType, bool> m_shopOpenDic = new Dictionary<EShopType, bool>();     //商店开启信息

    public ShopService()
    {
        m_shopsDic = new Dictionary<EShopType, ResGoodsInfo>();
        m_shopRefreshFlagDic = new Dictionary<EShopType, bool>();
    }

    protected override void RegisterEventListener()
    {
        base.RegisterEventListener();
        GED.NED.addListener(ResGoodsInfo.MsgId, _ResGoodsInfo);
        GED.NED.addListener(ResShopUpdate.MsgId, _ResShopUpdate);
        GED.NED.addListener(ResBuy.MsgId, _ResBuy);
        GED.NED.addListener(ResEquipBoxInfo.MsgId, _ResEquipBoxInfo);
        GED.NED.addListener(ResOpenResult.MsgId, _ResOpenResult);
        GED.NED.addListener(ResOpenShop.MsgId, _ResOpenShop);
        GED.NED.addListener(ResCloseShop.MsgId, _ResCloseShop);
        GED.NED.addListener(ResShopInit.MsgId, _ResShopInit);


    }

    protected override void UnRegisterEventListener()
    {
        base.UnRegisterEventListener();
        GED.NED.removeListener(ResGoodsInfo.MsgId, _ResGoodsInfo);
        GED.NED.removeListener(ResShopUpdate.MsgId, _ResShopUpdate);
        GED.NED.removeListener(ResBuy.MsgId, _ResBuy);
        GED.NED.removeListener(ResEquipBoxInfo.MsgId, _ResEquipBoxInfo);
        GED.NED.removeListener(ResOpenResult.MsgId, _ResOpenResult);
        GED.NED.removeListener(ResOpenShop.MsgId, _ResOpenShop);
        GED.NED.removeListener(ResCloseShop.MsgId, _ResCloseShop);
        GED.NED.removeListener(ResShopInit.MsgId, _ResShopInit);
    }

    public bool GetShopIsOpen(EShopType shopType)
    {
        if (m_shopOpenDic.ContainsKey(shopType))
            return m_shopOpenDic[shopType];
        return false;
    }

    //获得商店信息
    public ResGoodsInfo GetShopInfo(EShopType type)
    {
        if (m_shopsDic.ContainsKey(type))
        {
            return m_shopsDic[type];
        }

        return null;

    }

    //商店是否已经刷新
    public bool GetShopIsRefreshed(EShopType shopType)
    {
        if (m_shopRefreshFlagDic.ContainsKey(shopType))
            return m_shopRefreshFlagDic[shopType];

        return false;
    }

    public int GetToTargetRemainTime(int gId, int addHour = 0)
    {
        t_globalBean gBean = ConfigBean.GetBean<t_globalBean, int>(gId);
        if (gBean == null)
        {
            return 0;
        }

        int curHour = TimeUtils.currentServerDateTime2().Hour;
        int curMin = TimeUtils.currentServerDateTime2().Minute;
        int curSecoend = TimeUtils.currentServerDateTime2().Second;
        int disHour = 999;
        int disMin = 999;
        string[] arrTime = GTools.splitString(gBean.t_string_param, '+');
        for (int i = 0; i < arrTime.Length; i++)
        {
            int[] iArrTime = GTools.splitStringToIntArray(arrTime[i], ':');
            if (iArrTime == null || iArrTime.Length < 2)
                continue;
            int disH = (iArrTime[0] + addHour) - curHour;
            if (disH < 0)
                continue;

            if (disH <= disHour)
            {
                disHour = disH;
                int disM = iArrTime[1] - curMin;
                if (disHour == 0 && disM < 0)
                {
                    //时间已过
                    disHour = 999;
                    continue;
                }

                if (disM < disMin)
                {
                    disMin = disM;
                }
            }

        }
        int targetSecond = 0;
        if (disHour == 999)
        {
            if (disMin == 999)
            {
                //没有对应的时间点了则向后一天
                return GetToTargetRemainTime(gId, 24);
            }
            else
            {
                targetSecond += disMin * 60;
            } 
        }
        else
        {
            targetSecond += disHour * 60 * 60;
            if (disMin != 999)
            {
                targetSecond += disMin * 60;
            }
        }

        targetSecond -= curSecoend;
        return targetSecond;
    }


    //获得装备宝箱信息
    public ResEquipBoxInfo GetEquipBoxInfo()
    {
        return m_equipBoxInfo;
    }

    //刷新装备宝箱红点
    private void _RefreshEquipBoxRedDot()
    {
        if (m_equipBoxInfo == null)
            return;
        RedDotManager.Singleton.SetRedDotValue("Shop/EquipBox", m_equipBoxInfo.free);
    }
    //------------------------------------------------------------------------------消息

    //装备觉醒宝箱信息
    private void _ResEquipBoxInfo(GameEvent evt)
    {
        ResEquipBoxInfo msg = GetCurMsg<ResEquipBoxInfo>(evt.EventId);
        m_equipBoxInfo = msg;

        _RefreshEquipBoxRedDot();
        GED.ED.dispatchEvent(EventID.EquipTreasureBoxRefresh);
    }

    //装备觉醒宝箱开启结果
    private void _ResOpenResult(GameEvent evt)
    {
        ResOpenResult msg = GetCurMsg<ResOpenResult>(evt.EventId);
        //打开窗口
        WinMgr.Singleton.Open<OpenBoxReawardWnd>(WinInfo.Create(false, null, false, msg), UILayer.Popup);
    }

    //商品信息
    private void _ResGoodsInfo(GameEvent evt)
    {
        ResGoodsInfo msg = GetCurMsg<ResGoodsInfo>(evt.EventId);
        if (m_shopsDic.ContainsKey((EShopType)msg.shopType))
        {
            m_shopsDic[(EShopType)msg.shopType] = msg;
        }
        else
        {
            m_shopsDic.Add((EShopType)msg.shopType, msg);
        }

        if (m_shopRefreshFlagDic.ContainsKey((EShopType)msg.shopType))
        {
            //清除商店需要刷新标记
            m_shopRefreshFlagDic[(EShopType)msg.shopType] = false;
        }

        GED.ED.dispatchEvent(EventID.ShopRefresh);
    }


    //商店刷新
    private void _ResShopUpdate(GameEvent evt)
    {
        ResShopUpdate msg = GetCurMsg<ResShopUpdate>(evt.EventId);

        //记下需要刷新的标记，等到打开界面时请求刷新
        for (int i = 0; i < msg.shopType.Count; i++)
        {
            EShopType type = (EShopType)msg.shopType[i];
            if (m_shopRefreshFlagDic.ContainsKey(type))
            {
                m_shopRefreshFlagDic[type] = true;
            }
            else
            {
                m_shopRefreshFlagDic.Add(type, true);
            }
        }

        GED.ED.dispatchEvent(EventID.ShopPrepareRefresh, msg.shopType);

    }

    //购买商品结果
    private void _ResBuy(GameEvent evt)
    {
        ResBuy msg = GetCurMsg<ResBuy>(evt.EventId);
        if (!m_shopsDic.ContainsKey((EShopType)msg.shopType))
        {
            Debug.LogError("服务器发下不存在的商店类型" + msg.shopType);
            return;
        }


        for (int i = 0; i < m_shopsDic[(EShopType)msg.shopType].infos.Count; i++)
        {
            ShopInfo shopInfo = m_shopsDic[(EShopType)msg.shopType].infos[i];
            if (msg.index == shopInfo.index)
            {
                shopInfo.buyNum = msg.buyNum;
                break;
            }
        }

        ThreeParam<int, int, int> param = new ThreeParam<int, int, int>();
        param.value1 = msg.shopId;
        param.value2 = msg.buyNum;
        param.value3 = msg.shopType;
        GED.ED.dispatchEvent(EventID.ShopBuyResult, param);
    }

    private void _ResOpenShop(GameEvent evt)
    {
        ResOpenShop msg = GetCurMsg<ResOpenShop>(evt.EventId);
        EShopType shopType = (EShopType)msg.id;
        if (m_shopOpenDic.ContainsKey(shopType))
        {
            m_shopOpenDic[shopType] = true;
        }
        else
        {
            m_shopOpenDic.Add(shopType, true);
        }

        GED.ED.dispatchEvent(EventID.ShopOpenOrClose, msg.id);
    }

    private void _ResCloseShop(GameEvent evt)
    {
        ResCloseShop msg = GetCurMsg<ResCloseShop>(evt.EventId);
        EShopType shopType = (EShopType)msg.id;
        if (m_shopOpenDic.ContainsKey(shopType))
        {
            m_shopOpenDic[shopType] = false;
        }

        GED.ED.dispatchEvent(EventID.ShopOpenOrClose, msg.id);
    }

    private void _ResShopInit(GameEvent evt)
    {
        ResShopInit msg = GetCurMsg<ResShopInit>(evt.EventId);
        for (int i = 0; i < msg.id.Count; i++)
        {
            EShopType shopType = (EShopType)msg.id[i];
            if (m_shopOpenDic.ContainsKey(shopType))
            {
                m_shopOpenDic[shopType] = true;
            }
            else
            {
                m_shopOpenDic.Add(shopType, true);
            }
        }
    }

    //--------------------------------------------------------请求
    public void ReqRefresh(int shopType, int refreshType, int index = -1)
    {
        ReqRefresh msg = GetEmptyMsg<ReqRefresh>();
        msg.shopType = shopType;
        msg.type = refreshType;
        if (index != -1)
            msg.index = index;
        SendMsg<ReqRefresh>(ref msg);
    }

    public void ReqGoodsInfo(int shopType)
    {
        ReqGoodsInfo msg = GetEmptyMsg<ReqGoodsInfo>();
        msg.shopType = shopType;
        SendMsg<ReqGoodsInfo>(ref msg);
    }


    public void ReqBuy(int shopType, int index, int shopId, int buyNum)
    {
        ReqBuy msg = GetEmptyMsg<ReqBuy>();
        msg.shopType = shopType;
        msg.index = index;
        msg.shopId = shopId;
        msg.num = buyNum;
        SendMsg<ReqBuy>(ref msg);
    }

    //类型 1花钱 2免费 3道具
    public void ReqOpenBox(int count, int type)
    {
        ReqOpenBox msg = GetEmptyMsg<ReqOpenBox>();
        msg.num = count;
        msg.type = type;
        SendMsg<ReqOpenBox>(ref msg);
    }

}