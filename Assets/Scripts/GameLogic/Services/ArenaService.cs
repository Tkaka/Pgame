using Message.Arena;
using System.Collections.Generic;
using Message.Role;
using Data.Beans;
using UnityEngine;

public class ArenaService : SingletonService<ArenaService>
{
    private List<PlayerInfo> m_top10PlayerInfos = new List<PlayerInfo>();     //前十的玩家信息
    private List<PlayerInfo> m_canChanllangeList = new List<PlayerInfo>();    //后五的玩家信息
    private int m_chanllangeCount;                   //挑战剩余次数
    private int m_changeCount;                       //换一批已换的次数
    private long m_coolTime;                         //挑战的冷却时间戳
    private int m_buyedNum;                          //已购买的挑战次数
    private int m_highestRank;                       //最高排名
    private int m_score;                             //当前积分

    private Dictionary<int, Reward> m_rankRewardDic;          //排名奖励
    private Dictionary<int, Reward> m_scoreRewardDic;         //积分奖励
    private List<RankInfo> m_rankList;              //排行榜信息

    public ArenaService()
    {
        if (GameManager.Singleton.IsDebug)
        {
            _TestData();

        }

        _InitRewardDic();
    }

    protected override void RegisterEventListener()
    {
        base.RegisterEventListener();
        GED.NED.addListener(ResArenaInfo.MsgId, _ResArenaInfo);
        GED.NED.addListener(ResClearCoolTime.MsgId, _ResClearCoolTime);
        GED.NED.addListener(ResRankRewardInfo.MsgId, _ResRankRewardInfo);
        GED.NED.addListener(ResRankReward.MsgId, _ResRankReward);
        GED.NED.addListener(ResArena.MsgId, _ResArena);
        GED.NED.addListener(ResContinueArena.MsgId, _ResContinueArena);
        GED.NED.addListener(ResBuyResult.MsgId, _ResBuyResult);
        GED.NED.addListener(ResRankInfoChange.MsgId, _ResPlayerInfosChange);
        GED.NED.addListener(ResChangeNum.MsgId, _ResChangeNum);
        GED.NED.addListener(ResRank.MsgId, _ResRank);
        GED.NED.addListener(ResChangeHighestRank.MsgId, _ResChangeHighestRank);
        GED.NED.addListener(ResSeeOther.MsgId, _ResSeeOther);
        GED.NED.addListener(ResBuyNum.MsgId, _ResBuyNum);

        GED.ED.addListener(EventID.FunOpenEvent, _FunOpenEvent);

    }

    protected override void UnRegisterEventListener()
    {
        base.UnRegisterEventListener();
        GED.NED.removeListener(ResArenaInfo.MsgId, _ResArenaInfo);
        GED.NED.removeListener(ResClearCoolTime.MsgId, _ResClearCoolTime);
        GED.NED.removeListener(ResRankRewardInfo.MsgId, _ResRankRewardInfo);
        GED.NED.removeListener(ResRankReward.MsgId, _ResRankReward);
        GED.NED.removeListener(ResArena.MsgId, _ResArena);
        GED.NED.removeListener(ResContinueArena.MsgId, _ResContinueArena);
        GED.NED.removeListener(ResBuyResult.MsgId, _ResBuyResult);
        GED.NED.removeListener(ResRankInfoChange.MsgId, _ResPlayerInfosChange);
        GED.NED.removeListener(ResChangeNum.MsgId, _ResChangeNum);
        GED.NED.removeListener(ResRank.MsgId, _ResRank);
        GED.NED.removeListener(ResChangeHighestRank.MsgId, _ResChangeHighestRank);
        GED.NED.removeListener(ResSeeOther.MsgId, _ResSeeOther);
        GED.NED.removeListener(ResBuyNum.MsgId, _ResBuyNum);

        GED.ED.removeListener(EventID.FunOpenEvent, _FunOpenEvent);
    }


    //初始化最高排名奖励字典和积分奖励字典
    private void _InitRewardDic()
    {
        m_rankRewardDic = new Dictionary<int, Reward>();
        List<t_top_rewardBean> rankRewards = ConfigBean.GetBeanList<t_top_rewardBean>();
        if (rankRewards == null)
        {
            return;
        }
 

        for (int i = 0; i < rankRewards.Count; i++)
        {
            Reward reward = new Reward();
            reward.id = rankRewards[i].t_id;
            reward.state = 0;
            if (!m_rankRewardDic.ContainsKey(reward.id))
            {
                m_rankRewardDic.Add(reward.id, reward);
            }
        }

        m_scoreRewardDic = new Dictionary<int, Reward>();
        List<t_integral_rewardBean> scoreRandwards = ConfigBean.GetBeanList<t_integral_rewardBean>();
        for (int i = 0; i < scoreRandwards.Count; i++)
        {
            Reward reward = new Reward();
            reward.id = scoreRandwards[i].t_id;
            reward.state = 0;
            if (!m_scoreRewardDic.ContainsKey(reward.id))
            {
                m_scoreRewardDic.Add(reward.id, reward);
            }
        }
    }

    //获得竞技场主角的排名
    public int GetRoleRank()
    {
        RoleInfo roleInfo = RoleService.Singleton.GetRoleInfo();
        for (int i = 0; i < m_canChanllangeList.Count; i++)
        {
            if (m_canChanllangeList[i].roleId == roleInfo.roleId)
                return m_canChanllangeList[i].rank; 

        }
        for (int i = 0; i < m_top10PlayerInfos.Count; i++)
        {
            if (m_top10PlayerInfos[i].roleId == roleInfo.roleId)
                return m_top10PlayerInfos[i].rank;
        }

        return 0;
    }

    //获得挑战剩余次数
    public int GetChanllangeCount()
    {
        return m_chanllangeCount;
    }

    //获得冷却时间戳
    public long GetCoolTime()
    {
        return m_coolTime;
    }


    //换一换已换的次数
    public int GetChangeCount()
    {
        return m_changeCount;
    }

    //已购买的挑战次数
    public int GetBuyedNum()
    {
        return m_buyedNum;
    }

    //获得最高排名
    public int GetHighestRank()
    {
        return m_highestRank;
    }

    public List<PlayerInfo> GetTop10PlayersInfo()
    {
        return m_top10PlayerInfos;
    }


    public List<PlayerInfo> GetLastFivePlayerInfo()
    {
        return m_canChanllangeList;
    }

    //获得最高排名奖励信息
    public Dictionary<int, Reward> GetHighestRankRewards()
    {
 
        return m_rankRewardDic;
    }

    //获得积分奖励信息
    public Dictionary<int, Reward> GetScoreRewards()
    {
        return m_scoreRewardDic;
    }

    //1排名奖励 2积分奖励
    public Reward GetRewardById(int type, int id)
    {
        Dictionary<int, Reward> rewardDic = null;
        if (type == 1)
        {
            rewardDic = m_rankRewardDic;
        }
        else
        {
            rewardDic = m_scoreRewardDic;
        }

        if (rewardDic.ContainsKey(id))
        {
            return rewardDic[id];
        }

        return null;
    
    }

    //设置（1最高排名 2积分）的领奖状态
    private void _SetRewardState(int type, bool isInit = false)
    {
        bool isRefresh = false;
        if (type == 1)
        {

            foreach (var info in m_rankRewardDic)
            {
                if (m_highestRank <= info.Key && info.Value.state == 0)
                {
                    info.Value.state = 1;
                    isRefresh = true;
                }
            }
   
        }
        else
        {
            foreach (var info in m_scoreRewardDic)
            {
                if (m_score >= info.Key && info.Value.state == 0)
                {
                    info.Value.state = 1;
                    isRefresh = true;
                }
            }


        }
             
        if (isRefresh)
        {
            if (isInit == false)
            {
                if (type == 1)
                    _RefreshRankRedDotInfo();
                else
                    _RefreshScoreRedDotInfo();
            }
 

            GED.ED.dispatchEvent(EventID.RewardStateChange, type);
        }
            

    }

    //获得当前积分
    public int GetCurScore()
    {
        return m_score;
    }

    //获得排行榜数据
    public List<RankInfo> GetRankList()
    {
        return m_rankList;
    }


    //功能开启事件
    private void _FunOpenEvent(GameEvent evt)
    {
        int funId = (int)evt.Data;
        if (funId == 1701)
        {
            //竞技场功能
            _RefreshRankRedDotInfo();
            _RefreshScoreRedDotInfo();
        }
    }

    //刷新排名奖励红点信息
    private void _RefreshRankRedDotInfo()
    {
        string path = "mainArean/ArenaPage/RankReward";

        bool isShowRed = false;
        if (FuncService.Singleton.IsFuncOpen(1701))
        {
            foreach (var info in m_rankRewardDic)
            {
                if (info.Value.state == 1)
                {
                    //有一个可领奖
                    isShowRed = true;
                    break;
                }
            }
        }
 

        RedDotManager.Singleton.SetRedDotValue(path, isShowRed);
    }

    //刷新积分奖励红点信息
    private void _RefreshScoreRedDotInfo()
    {
        string path = "mainArean/ArenaPage/ScoreReward";

        bool isShowRed = false;
        if (FuncService.Singleton.IsFuncOpen(1701))
        {
            foreach (var info in m_scoreRewardDic)
            {
                if (info.Value.state == 1)
                {
                    //有一个可领奖
                    isShowRed = true;
                    break;
                }
            }
        }
 

        RedDotManager.Singleton.SetRedDotValue(path, isShowRed);
    }

    //-------------------------------------------------------------------------------通知
    //主界面信息(相当初始化信息）
    private void _ResArenaInfo(GameEvent evt)
    {
        ResArenaInfo msg = GetCurMsg<ResArenaInfo>(evt.EventId);
        m_chanllangeCount = msg.arenaNum;
        m_changeCount = msg.changeNum;
        m_coolTime = msg.hasCoolTime() ? msg.coolTime : m_coolTime;
        m_buyedNum = msg.BuyNum;
        if (msg.info.Count > 10)
        {
            m_top10PlayerInfos.Clear();
            m_canChanllangeList.Clear();
            for (int i = 0; i < msg.info.Count; i++)
            {
                if (msg.info[i].rank <= 10)
                {
                    m_top10PlayerInfos.Add(msg.info[i]);
                }
                else
                {
                    m_canChanllangeList.Add(msg.info[i]);
                }
                 
            }

            m_top10PlayerInfos.Sort((x, y) => x.rank.CompareTo(y.rank));
            m_canChanllangeList.Sort((x, y) => x.rank.CompareTo(y.rank));
        }

        GED.ED.dispatchEvent(EventID.ArenaInfoRefresh);

    }

    //冷却清除
    private void _ResClearCoolTime(GameEvent evt)
    {
        ResClearCoolTime msg = GetCurMsg<ResClearCoolTime>(evt.EventId);
        m_coolTime = 0;

        GED.ED.dispatchEvent(EventID.ClearCoolTime);
        //TODO()
        
    }


    //奖励初始信息
    private void _ResRankRewardInfo(GameEvent evt)
    {
        ResRankRewardInfo msg = GetCurMsg<ResRankRewardInfo>(evt.EventId);
        m_highestRank = msg.hasHighestRank() ? msg.highestRank : m_highestRank;
        m_score = msg.hasScore() ? msg.score : m_score;

        Dictionary<int, Reward> rewardDic;
        if (msg.type == 1)
        {
            rewardDic = m_rankRewardDic;

        }
        else
        {
            rewardDic = m_scoreRewardDic;
        }

        _SetRewardState(msg.type, true);
        for (int i = 0; i < msg.reward.Count; i++)
        {
            //服务器只会发下以领奖的信息
            Reward reward = msg.reward[i];
            if (rewardDic.ContainsKey(reward.id))
            {
                rewardDic[reward.id].state = reward.state;
            }
            else
            {
                Debug.LogError("服务器发下的奖励表ID不存在" + reward.id);
            }
        }

        if (msg.type == 1)
            _RefreshRankRedDotInfo();
        else
            _RefreshScoreRedDotInfo();

    }

    private void _ResRankReward(GameEvent evt)
    {
        ResRankReward msg = GetCurMsg<ResRankReward>(evt.EventId);
        Dictionary<int, Reward> rewardDic = null;
        if (msg.type == 1)
            rewardDic = m_rankRewardDic;
        else
            rewardDic = m_scoreRewardDic;

        for (int index = 0; index < msg.rewards.Count; index++)
        {
            Reward reward = msg.rewards[index];
            if (rewardDic.ContainsKey(reward.id))
            {
                rewardDic[reward.id].state = reward.state;
            }
            else
            {
                Debug.LogError("服务器发下的奖励表ID不存在" + reward.id);
            }

        }

        if (msg.type == 1)
            _RefreshRankRedDotInfo();
        else
            _RefreshScoreRedDotInfo();

        //TODO 抛事件刷新界面
        GED.ED.dispatchEvent(EventID.RewardStateChange, msg.type);
    }

    //单次挑战结果
    private void _ResArena(GameEvent evt)
    {
        ResArena msg = GetCurMsg<ResArena>(evt.EventId);
        m_chanllangeCount = msg.arenaNum;
        m_coolTime = msg.coolTime;
        m_score += msg.core;
        _SetRewardState(2);

        GED.ED.dispatchEvent(EventID.ChallengeCountChange);
    }

    //连续挑战结果
    private void _ResContinueArena(GameEvent evt)
    {
        ResContinueArena msg = GetCurMsg<ResContinueArena>(evt.EventId);
        for (int i = 0; i < msg.result.Count; i++)
        {
            m_score += msg.result[i].core;
        }

        m_chanllangeCount = msg.arenaNum;

        //打开结果界面
        _SetRewardState(2);
        GED.ED.dispatchEvent(EventID.ChallengeCountChange);
        WinMgr.Singleton.Open<ContinuousChallengeWindow>(WinInfo.Create(false, null, true,msg));
    }

    //已购买次数变化
    private void _ResBuyResult(GameEvent evt)
    {
        ResBuyResult msg = GetCurMsg<ResBuyResult>(evt.EventId);
        m_buyedNum = msg.count;
        GED.ED.dispatchEvent(EventID.ChallengeCountChange);

    }

    //购买后挑战次数改变
    private void _ResBuyNum(GameEvent evt)
    {
        ResBuyNum msg = GetCurMsg<ResBuyNum>(evt.EventId);
        m_chanllangeCount = msg.num;
        GED.ED.dispatchEvent(EventID.ChallengeCountChange);
    }

    //排行信息发生变化
    private void _ResPlayerInfosChange(GameEvent evt)
    {
        ResRankInfoChange msg = GetCurMsg<ResRankInfoChange>(evt.EventId);
        if (msg.info.Count > 10)
        {
            //整个刷新了
            m_top10PlayerInfos.Clear();
            m_canChanllangeList.Clear();
            for (int i = 0; i < msg.info.Count; i++)
            {
                if (msg.info[i].rank <= 10)
                    m_top10PlayerInfos.Add(msg.info[i]);
                else
                    m_canChanllangeList.Add(msg.info[i]);

            }

            m_top10PlayerInfos.Sort((x, y) => x.rank.CompareTo(y.rank));
            m_canChanllangeList.Sort((x, y) => x.rank.CompareTo(y.rank));
        }
        else
        {
            for (int i = 0; i < msg.info.Count; i++)
            {
                bool isFinded = false;
                for (int index = 0; index < m_top10PlayerInfos.Count; index++)
                {
                    if (m_top10PlayerInfos[index].roleId == msg.info[i].roleId)
                    {
                        m_top10PlayerInfos[index] = msg.info[i];
                        isFinded = true;
                        break;
                    }
                }

                if (isFinded == false)
                {
                    for (int index = 0; index < m_canChanllangeList.Count; index++)
                    {
                        if (m_canChanllangeList[index].roleId == msg.info[i].roleId)
                        {
                            m_canChanllangeList[index] = msg.info[i];
                            break;
                        }
                    }
                }

            }
        }


        //TODO  抛列表刷新事件
        GED.ED.dispatchEvent(EventID.PlayersChange);
    }

    //换一换已换的次数
    private void _ResChangeNum(GameEvent evt)
    {
        ResChangeNum msg = GetCurMsg<ResChangeNum>(evt.EventId);
        m_changeCount = msg.count;
        GED.ED.dispatchEvent(EventID.PlayerChangeCount);
    }

    //排行榜信息
    private void _ResRank(GameEvent evt)
    {
        ResRank msg = GetCurMsg<ResRank>(evt.EventId);
        m_rankList = msg.rank;
        WinMgr.Singleton.Open<RankWindow>(null, UILayer.Popup);
    }

    //主角最高排名发生改变
    private void _ResChangeHighestRank(GameEvent evt)
    {
        ResChangeHighestRank msg = GetCurMsg<ResChangeHighestRank>(evt.EventId);
        m_highestRank = msg.highestRank;
        _SetRewardState(1);
    }


    //查看他人信息返回
    private void _ResSeeOther(GameEvent evt)
    {
        ResSeeOther msg = GetCurMsg<ResSeeOther>(evt.EventId);
        WinMgr.Singleton.Open<SeeOtherInfoWindow>(WinInfo.Create(false, null, false, msg),UILayer.Popup);
    }

    //------------------------------------------------------------------------------------请求
    //请求挑战玩家
    public void ReqArena(long roleId, int times, int rank, int consuemItem = -1)
    {
        ReqArena msg = GetEmptyMsg<ReqArena>();
        msg.roleId = roleId;
        msg.times = times;
        msg.rank = rank;

        if (consuemItem != -1)
            msg.consumeItem = consuemItem;
        SendMsg<ReqArena>(ref msg);
    }

    //请求清除冷却
    public void ReqClearCoolTime()
    {
        ReqClearCoolTime msg = GetEmptyMsg<ReqClearCoolTime>();
        SendMsg<ReqClearCoolTime>(ref msg);

    }

    //换一批
    public void ReqChangeTarget()
    {
        ReqChangeTarget msg = GetEmptyMsg<ReqChangeTarget>();
        SendMsg<ReqChangeTarget>(ref msg);
    }

    //请求排名信息
    public void ReqRank()
    {
        ReqRank msg = GetEmptyMsg<ReqRank>();
        SendMsg<ReqRank>(ref msg);
    }

    //请求领取排名奖励
    public void ReqRankReward(int rankId)
    {
        ReqRankReward msg = GetEmptyMsg<ReqRankReward>();
        msg.id = rankId;
        SendMsg<ReqRankReward>(ref msg);
    }


    //请求领取积分奖励
    public void ReqCoreReward(bool isOneKey, int id = -1)
    {
        ReqCoreReward msg = GetEmptyMsg<ReqCoreReward>();
        msg.oneKey = isOneKey;
        if (id != -1)
        {
            msg.id = id;
        }

        SendMsg<ReqCoreReward>(ref msg);
    }

    //消耗道具 1券 2钻石
    public void ReqBuyNum(int consumItem)
    {
        ReqBuyNum msg = GetEmptyMsg<ReqBuyNum>();
        msg.comsumeItem = consumItem;
        SendMsg<ReqBuyNum>(ref msg);
    }


    //请求膜拜
    public void ReqWorship(bool isOneKey, long roleId = -1)
    {
        ReqWorship msg = GetEmptyMsg<ReqWorship>();
        if (roleId != -1)
        {
            msg.id = roleId;
        }

        msg.oneKey = isOneKey;
        SendMsg<ReqWorship>(ref msg);
    }

    public void ReqAreanInfo()
    {
        ReqAreanInfo msg = GetEmptyMsg<ReqAreanInfo>();
        SendMsg<ReqAreanInfo>(ref msg);
    }

    //请求查看他人信息
    public void ReqSeeOther(long roleId)
    {
        if (GameManager.Singleton.IsDebug)
        {
            ResSeeOther info = new ResSeeOther();
            info.playerName = "我的名字";
            info.level = 56;
            info.rank = 2;
            info.victoryCount = 100;
            info.xuanYan = "逗比你好";
            info.xianShou = 10086;
            info.iconId = 2;
            info.fightPower = 10087;
            info.guildName = "丐帮";

            WinMgr.Singleton.Open<SeeOtherInfoWindow>(WinInfo.Create(false, null, false, info));
            return;
        }

        ReqSeeOther msg = GetEmptyMsg<ReqSeeOther>();
        msg.roleId = roleId;
        SendMsg<ReqSeeOther>(ref msg);
    }
    //-------------------------------------------------------------------------------------------数据
    private void _TestData()
    {

        List<Reward> rewardList = new List<Reward>();

        Reward reward = new Reward();
        reward.id = 1;
        reward.state = 2;
        rewardList.Add(reward);

        Reward reward1 = new Reward();
        reward1.id = 3;
        reward1.state = 1;
        rewardList.Add(reward1);

        Reward reward2 = new Reward();
        reward2.id = 6;
        reward2.state = 0;
        rewardList.Add(reward2);

        rewardList.Sort((a, b) => a.id.CompareTo(b.id));
        //m_rankRewardList = rewardList;

        List<Reward> rewardList2 = new List<Reward>();

        Reward reward3 = new Reward();
        reward3.id = 2;
        reward3.state = 2;
        rewardList2.Add(reward3);

        Reward reward4 = new Reward();
        reward4.id = 4;
        reward4.state = 1;
        rewardList2.Add(reward4);

        Reward reward5 = new Reward();
        reward5.id = 6;
        reward5.state = 0;
        rewardList2.Add(reward5);

        rewardList2.Sort((a, b) => a.id.CompareTo(b.id));
        //m_scoreRewardList = rewardList2;


        List<RankInfo> rankList = new List<RankInfo>();
        for (int i = 0; i < 50; i++)
        {
            RankInfo info = new RankInfo();
            info.rank = i + 1;
            info.roleName = string.Format("PlayerName" + (i + 1));
            info.guildName = "丐帮";
            info.level = 56;
            info.fightPower = 10000 - i * 20;
            info.roleId = i+1;
            info.iconId = 2;

            rankList.Add(info);
        }

        RankInfo info2 = new RankInfo();
        info2.rank = 2323;
        info2.roleName = string.Format("PlayerName" + (434 + 1));
        info2.guildName = "丐帮";
        info2.level = 56;
        info2.fightPower = 10000 - 33 * 20;
        info2.roleId = 0;
        info2.iconId = 2;
        rankList.Add(info2);
        m_rankList = rankList;

        m_coolTime = TimeUtils.currentMilliseconds() + 60000;
    }


}