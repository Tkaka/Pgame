using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Message.GuildBattle;

/// <summary>
/// 公会战的类型
/// </summary>
public enum GuildBattleType
{
    ChuSai1 = 1,
    ChuSai2 = 2,
    FuSai1 = 3,
    FuSai2 = 4,
    FuSai3 = 5,
    JueSai = 6,
}

public class GuildBattleService : SingletonService<GuildBattleService> {
    /// <summary>
    /// 公会战报名信息
    /// </summary>
    public ResApplyInfo applyInfo { get; private set; }

    protected override void RegisterEventListener()
    {
        base.RegisterEventListener();

        GED.NED.addListener(ResApplyInfo.MsgId, OnResApplyInfo);
        GED.NED.addListener(ResApply.MsgId, OnResApply);
        GED.NED.addListener(ResExchange.MsgId, OnResExchange);
    }

    #region 服务器回调 ******************************************************************************************************
    /// <summary>
    /// 公会战报名信息
    /// </summary>
    /// <param name="evt"></param>
    private void OnResApplyInfo(GameEvent evt)
    {
        ResApplyInfo msg = GetCurMsg<ResApplyInfo>(evt.EventId);
        applyInfo = msg;

        // 打开公会战界面
        WinMgr.Singleton.Open<GuildBattleMianWindow>(null, UILayer.Popup);
    }

    private void OnResApply(GameEvent evt)
    {
        ResApply msg = GetCurMsg<ResApply>(evt.EventId);

        // 报名成功打开阵容界面
        WinMgr.Singleton.Open<GuildBattleBuZhengWindow>(null, UILayer.Popup);
    }

    private void OnResExchange(GameEvent evt)
    {
        ResExchange msg = GetCurMsg<ResExchange>(evt.EventId);


    }

    private void OnResRankInfo()
    {

    }
    #endregion

    #region 请求 ******************************************************************************************************
    public void ReqGuildBattleInfo()
    {
        ReqGuildBattleInfo msg = GetEmptyMsg<ReqGuildBattleInfo>();

        SendMsg<ReqGuildBattleInfo>(ref msg);
    }
    /// <summary>
    /// 公会战报名
    /// </summary>
    public void ReqApply()
    {
        ReqApply msg = GetEmptyMsg<ReqApply>();

        SendMsg<ReqApply>(ref msg);
    }
    /// <summary>
    /// 公会战信息
    /// </summary>
    public void ReqApplyInfo()
    {
        ReqApplyInfo msg = GetEmptyMsg<ReqApplyInfo>();

        SendMsg<ReqApplyInfo>(ref msg);
    }
    /// <summary>
    /// 兑换
    /// </summary>
    /// <param name="id"></param>
    public void ReqExchange(int id)
    {
        ReqExchange msg = GetEmptyMsg<ReqExchange>();

        msg.id = id;
        SendMsg<ReqExchange>(ref msg);
    }
    /// <summary>
    /// 获取排行榜信息
    /// </summary>
    /// <param name="type">1~5表示初赛和复赛。0表示总排行</param>
    public void ReqRankInfo(int type)
    {
        ReqRankInfo msg = GetEmptyMsg<ReqRankInfo>();

        msg.type = type;
        SendMsg<ReqRankInfo>(ref msg);
    }
    /// <summary>
    /// 昨日战况
    /// </summary>
    /// <param name="round">轮次号，最高轮次填-1</param>
    public void ReqYesterdayFightInfo(int round)
    {
        ReqYesterdayFightInfo msg = GetEmptyMsg<ReqYesterdayFightInfo>();

        msg.round = round;
        SendMsg<ReqYesterdayFightInfo>(ref msg);
    }
    /// <summary>
    /// 接收实时战况
    /// </summary>
    public void ReqOpenFightInfo()
    {
        ReqOpenFightInfo msg = GetEmptyMsg<ReqOpenFightInfo>();

        SendMsg<ReqOpenFightInfo>(ref msg);
    }
    /// <summary>
    /// 取消接收实时战况
    /// </summary>
    public void ReqCloseFightInfo()
    {
        ReqCloseFightInfo msg = GetEmptyMsg<ReqCloseFightInfo>();

        SendMsg<ReqCloseFightInfo>(ref msg);
    }
    #endregion

    #region 数据处理 **************************************************************************************************
    public int GetGuildBattleType()
    {
        System.DateTime dateTime = TimeUtils.currentServerDateTime2();
        int weekDay = (int)(dateTime.DayOfWeek);
        return weekDay;
    }
    #endregion;
}
