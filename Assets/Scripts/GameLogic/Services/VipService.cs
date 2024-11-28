using System.Collections.Generic;
using Message.Role;
using Data.Beans;
using UnityEngine;
using Message.Vip;

public class VipService : SingletonService<VipService>
{

    private int m_vipLevel = 0;

    private int m_vipExp = 0;

    private Dictionary<int, int> m_giftBagStateDic = new Dictionary<int, int>();              //已领礼包

    public int VipLevel
    {
        private set { m_vipLevel = value; }
        get { return m_vipLevel; }
    }

    public int VipExp
    {
        private set { m_vipExp = value; }
        get { return m_vipExp; }
    }


    //获取vip等级礼包的状态
    public bool GetVipGiftBagState(int vipLevel)
    {
        if (!m_giftBagStateDic.ContainsKey(vipLevel))
            return false;

        return true;
    }

    public VipService()
    {

    }

    protected override void RegisterEventListener()
    {
        base.RegisterEventListener();
        GED.NED.addListener(ResVipInfo.MsgId, _ResVipInfo);
        GED.NED.addListener(ResGiftBagStateChange.MsgId, _ResGiftBagStateChange);
        GED.NED.addListener(ResVipInfoChange.MsgId, _ResVipInfoChange);

    }

    protected override void UnRegisterEventListener()
    {
        base.UnRegisterEventListener();
        GED.NED.removeListener(ResVipInfo.MsgId, _ResVipInfo);
        GED.NED.removeListener(ResGiftBagStateChange.MsgId, _ResGiftBagStateChange);
        GED.NED.removeListener(ResVipInfoChange.MsgId, _ResVipInfoChange);

    }

    //--------------------------------------------------------------------------------消息
    private void _ResVipInfo(GameEvent evt)
    {
        ResVipInfo msg = GetCurMsg<ResVipInfo>(evt.EventId);
        m_vipExp = msg.vipExp;
        m_vipLevel = msg.vipLevel;
        for (int i = 0; i < msg.giftBagStateInfo.Count; i++)
        {
            if (!m_giftBagStateDic.ContainsKey(msg.giftBagStateInfo[i]))
                m_giftBagStateDic.Add(msg.giftBagStateInfo[i], msg.giftBagStateInfo[i]);
        }
    }

    private void _ResGiftBagStateChange(GameEvent evt)
    {
        ResGiftBagStateChange msg = GetCurMsg<ResGiftBagStateChange>(evt.EventId);
        if (!m_giftBagStateDic.ContainsKey(msg.vipLevel))
        {
            m_giftBagStateDic.Add(msg.vipLevel, msg.vipLevel);
        }

        GED.ED.dispatchEvent(EventID.VipGiftBagStateChange);
    }

    private void _ResVipInfoChange(GameEvent evt)
    {
        ResVipInfoChange msg = GetCurMsg<ResVipInfoChange>(evt.EventId);
        m_vipExp = msg.vipExp;
        m_vipLevel = msg.vipLevel;
        GED.ED.dispatchEvent(EventID.VipInfoChange);
    }

    //--------------------------------------------------------------------------------请求

    public void ReqBuyVipGiftBag(int vipLevel)
    {
        ReqBuyVipGiftBag msg = GetEmptyMsg<ReqBuyVipGiftBag>();
        msg.vipLevel = vipLevel;
        SendMsg<ReqBuyVipGiftBag>(ref msg);
    }

}