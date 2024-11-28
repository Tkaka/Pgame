using System.Collections.Generic;
using Message.Role;
using Data.Beans;
using UnityEngine;
using Message.Recharge;

public class RechargeService : SingletonService<RechargeService>
{
    protected override void RegisterEventListener()
    {
        base.RegisterEventListener();
        GED.NED.addListener(ResRecharge.MsgId, _ResRecharge);
    }


    protected override void UnRegisterEventListener()
    {
        base.UnRegisterEventListener();
        GED.NED.removeListener(ResRecharge.MsgId, _ResRecharge);
    }

    private void _ResRecharge(GameEvent evt)
    {
        ResRecharge msg = GetCurMsg<ResRecharge>(evt.EventId);
        
    }


    public void ReqRecharge(int id)
    {
        ReqRecharge msg = GetEmptyMsg<ReqRecharge>();
        msg.id = id;
        SendMsg<ReqRecharge>(ref msg);
    }

}