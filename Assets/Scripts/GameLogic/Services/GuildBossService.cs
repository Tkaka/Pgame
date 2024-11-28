using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Message.GuildBoss;
using Data.Beans;

public class GuildBossService : SingletonService<GuildBossService> {
    /// <summary>
    /// 工会副本信息
    /// </summary>
    public ResGuildDungeonInfo guildBossInfo { get; private set; }
    /// <summary>
    /// 公会副本默认的boss星级
    /// </summary>
    public int guildBossDefaultStar = 4;
    /// <summary>
    /// 成员进度上拉刷新的条数
    /// </summary>
    public int progressRefreshNum = 5;
    /// <summary>
    /// 伤害排名每次刷新的数量
    /// </summary>
    public int damageRankRefreshNum = 10;
    /// <summary>
    /// 通关排行每次刷新的数量
    /// </summary>
    public int passRankRefreshNum = 10;
    /// <summary>
    /// 初始请求列表的长度
    /// </summary>
    public int listDefeaultNum = 10;
    /// <summary>
    /// boss血量进度条最大值
    /// </summary>
    public int MAX_PROGRESS = 100;

    protected override void RegisterEventListener()
    {
        base.RegisterEventListener();

        GED.NED.addListener(ResGuildDungeonInfo.MsgId, OnResGuildDungeonInfo);
        GED.NED.addListener(ResGuildRankInfo.MsgId, OnResGuildPassRankInfo);
        GED.NED.addListener(ResAllotRecordInfo.MsgId, OnResAllotRecordInfo);
        GED.NED.addListener(ResProgressInfo.MsgId, OnResProgressInfo);
        GED.NED.addListener(ResBossInfo.MsgId, OnResBossInfo);
        GED.NED.addListener(ResDamageRank.MsgId, OnResDamageRank);
        GED.NED.addListener(ResFightStart.MsgId, OnResFightStart);
        GED.NED.addListener(ResFightEnd.MsgId, OnResFightEnd);
        GED.NED.addListener(ResGetReward.MsgId, OnResGetReward);
    }

    protected override void UnRegisterEventListener()
    {
        base.UnRegisterEventListener();

        GED.NED.removeListener(ResGuildDungeonInfo.MsgId, OnResGuildDungeonInfo);
        GED.NED.removeListener(ResGuildRankInfo.MsgId, OnResGuildPassRankInfo);
        GED.NED.removeListener(ResAllotRecordInfo.MsgId, OnResAllotRecordInfo);
        GED.NED.removeListener(ResProgressInfo.MsgId, OnResProgressInfo);
        GED.NED.removeListener(ResBossInfo.MsgId, OnResBossInfo);
        GED.NED.removeListener(ResDamageRank.MsgId, OnResDamageRank);
        GED.NED.removeListener(ResFightStart.MsgId, OnResFightStart);
        GED.NED.removeListener(ResFightEnd.MsgId, OnResFightEnd);
        GED.NED.removeListener(ResGetReward.MsgId, OnResGetReward);
    }

    #region 服务器回调 *******************************************************************************************************************
    /// <summary>
    /// 公会副本信息回调
    /// </summary>
    /// <param name="evt"></param>
    private void OnResGuildDungeonInfo(GameEvent evt)
    {
        ResGuildDungeonInfo msg = GetCurMsg<ResGuildDungeonInfo>(evt.EventId);

        guildBossInfo = msg;
        GED.ED.dispatchEvent(EventID.OnResGuildDungeonInfo);
    }
    /// <summary>
    /// 公会通关排行
    /// </summary>
    /// <param name="evt"></param>
    private void OnResGuildPassRankInfo(GameEvent evt)
    {
        ResGuildRankInfo msg = GetCurMsg<ResGuildRankInfo>(evt.EventId);

        GED.ED.dispatchEvent(EventID.OnResGuildPassRankInfo, msg);
    }
    /// <summary>
    /// 分配记录
    /// </summary>
    /// <param name="evt"></param>
    private void OnResAllotRecordInfo(GameEvent evt)
    {
        ResAllotRecordInfo msg = GetCurMsg<ResAllotRecordInfo>(evt.EventId);

        GED.ED.dispatchEvent(EventID.OnResAllotRecordInfo, msg.records);
    }
    /// <summary>
    /// 成员进度
    /// </summary>
    /// <param name="evt"></param>
    private void OnResProgressInfo(GameEvent evt)
    {
        ResProgressInfo msg = GetCurMsg<ResProgressInfo>(evt.EventId);

        GED.ED.dispatchEvent(EventID.OnResProgressInfo, msg.progress);
    }
    /// <summary>
    /// 当前boss信息
    /// </summary>
    /// <param name="evt"></param>
    private void OnResBossInfo(GameEvent evt)
    {
        ResBossInfo msg = GetCurMsg<ResBossInfo>(evt.EventId);

        GED.ED.dispatchEvent(EventID.OnResBossInfo, msg);
    }
    /// <summary>
    /// 伤害排名
    /// </summary>
    /// <param name="evt"></param>
    private void OnResDamageRank(GameEvent evt)
    {
        ResDamageRank msg = GetCurMsg<ResDamageRank>(evt.EventId);

        GED.ED.dispatchEvent(EventID.OnResDamageRank, msg.records);
    }
    /// <summary>
    /// 开始战斗
    /// </summary>
    /// <param name="evt"></param>
    private void OnResFightStart(GameEvent evt)
    {
        ResFightStart msg = GetCurMsg<ResFightStart>(evt.EventId);

        GED.ED.dispatchEvent(EventID.OnResFightStart);
    }
    /// <summary>
    /// 战斗结果
    /// </summary>
    /// <param name="evt"></param>
    private void OnResFightEnd(GameEvent evt)
    {
        ResFightEnd msg = GetCurMsg<ResFightEnd>(evt.EventId);
        GED.ED.dispatchEvent(EventID.OnResFightEnd, msg);
    }
    /// <summary>
    /// 领取boss首通奖励
    /// </summary>
    /// <param name="evt"></param>
    private void OnResGetReward(GameEvent evt)
    {
        ResGetReward msg = GetCurMsg<ResGetReward>(evt.EventId);
        guildBossInfo.canGetRewardBossIds.Clear();
        guildBossInfo.canGetRewardBossIds.AddRange(msg.canGetRewardBossIds);

        GED.ED.dispatchEvent(EventID.OnResGuildBossGetReward, msg.rewards);
    }
    #endregion

    #region 请求 *******************************************************************************************************************
    /// <summary>
    /// 请求工会副本信息
    /// </summary>
    public void ReqGuildDungeonInfo()
    {
        ReqGuildDungeonInfo msg = GetEmptyMsg<ReqGuildDungeonInfo>();

        SendMsg<ReqGuildDungeonInfo>(ref msg);
    }
    /// <summary>
    /// 请求公会通关排行
    /// </summary>
    /// <param name="bossID"></param>
    /// <param name="startIndex"></param>
    /// <param name="endIndex"></param>
    public void ReqGuildPassRankInfo(int bossID, int startIndex)
    {
        ReqGuildRankInfo msg = GetEmptyMsg<ReqGuildRankInfo>();
        msg.id = bossID;
        msg.from = startIndex;
        msg.end = startIndex + passRankRefreshNum;

        SendMsg<ReqGuildRankInfo>(ref msg);
    }
    /// <summary>
    /// 获取分配信息
    /// </summary>
    /// <param name="startIndex"></param>
    /// <param name="endIndex"></param>
    public void ReqAllotRecordInfo()
    {
        ReqAllotRecordInfo msg = GetEmptyMsg<ReqAllotRecordInfo>();

        SendMsg<ReqAllotRecordInfo>(ref msg);
    }
    /// <summary>
    /// 获取成员进度
    /// </summary>
    /// <param name="startIndex"></param>
    /// <param name="endIndex"></param>
    public void ReqProgressInfo(int startIndex)
    {
        ReqProgressInfo msg = GetEmptyMsg<ReqProgressInfo>();
        msg.from = startIndex;
        msg.end = startIndex + progressRefreshNum;

        SendMsg<ReqProgressInfo>(ref msg);
    }
    /// <summary>
    /// 请求boss信息
    /// </summary>
    public void ReqBossInfo(int bossID)
    {
        ReqBossInfo msg = GetEmptyMsg<ReqBossInfo>();
        msg.bossId = bossID;

        SendMsg<ReqBossInfo>(ref msg);
    }
    /// <summary>
    /// 伤害排名
    /// </summary>
    public void ReqDamageRank(int bossID, int startIndex)
    {
        ReqDamageRank msg = GetEmptyMsg<ReqDamageRank>();
        msg.bossId = bossID;
        msg.from = startIndex;
        msg.end = startIndex + damageRankRefreshNum;

        SendMsg<ReqDamageRank>(ref msg);
    }
    /// <summary>
    /// 请求战斗
    /// </summary>
    public void ReqFightStart(int bossID)
    {
        //ReqFightStart msg = GetEmptyMsg<ReqFightStart>();
        //msg.bossId = bossID;

        //SendMsg<ReqFightStart>(ref msg);
        FightService.Singleton.ReqFight(EFightType.GuildBossDungeon, bossID);
    }
    /// <summary>
    /// 发送战斗结果
    /// </summary>
    public void ReqFightEnd(long damage, int bossID)
    {
        ReqFightEnd msg = GetEmptyMsg<ReqFightEnd>();
        msg.damage = damage;
        msg.bossId = bossID;

        SendMsg<ReqFightEnd>(ref msg);
    }
    /// <summary>
    /// 获取首通奖励
    /// </summary>
    /// <param name="bossID"></param>
    public void ReqGetFirstPassAward(int bossID)
    {
        ReqGetFirstPassAward msg = GetEmptyMsg<ReqGetFirstPassAward>();
        msg.id = bossID;

        SendMsg<ReqGetFirstPassAward>(ref msg);
    }
    /// <summary>
    /// 一键获取首通奖励
    /// </summary>
    public void ReqOneKeyGetFirstPassAward()
    {
        ReqOneKeyGetFirstPassAward msg = GetEmptyMsg<ReqOneKeyGetFirstPassAward>();

        SendMsg<ReqOneKeyGetFirstPassAward>(ref msg);
    }
    #endregion

    #region 数据处理 *******************************************************************************************************************
    public int GetMaxFightTimes()
    {
        t_globalBean globalBean = ConfigBean.GetBean<t_globalBean, int>(1603005);
        if (globalBean != null)
        {
            return globalBean.t_int_param;
        }

        return 0;
    }
    /// <summary>
    /// 获得公会副本开启等级
    /// </summary>
    /// <returns></returns>
    public int GetGuildBossOpenLevel()
    {
        // 1603009   公会副本开启公会等级
        t_globalBean globalBean = ConfigBean.GetBean<t_globalBean, int>(1603009);
        if (globalBean != null)
        {
            return globalBean.t_int_param;
        }

        return int.MaxValue;
    }
    #endregion
}
