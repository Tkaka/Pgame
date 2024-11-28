using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Message.Challenge;
using Data.Beans;
using Message.Fight;

public enum ActivityType
{
    Gold = 1,                   // 金币挑战
    Exp = 2,                    // 经验挑战
    NvGeDouJia = 3,             // 女格斗家
    HuanXiang = 4,              // 幻象挑战
}

public enum ChallengeType
{
    ZhongJiShiLian = 1,          // 终极试炼
    HuoDongGuanQia = 2,          // 活动关卡
    KeLongMoShi = 3,             // 克隆模式
}

public enum DifficultyType
{
    Easy = 1,                    // 简单
    Normal = 2,                  // 普通
    Hard = 3,                    // 困难
    Master = 4,                  // 大师
    ShenYuan = 5,                // 深渊
    KuNan = 6,                   // 苦难
}

public class ChallegeService : SingletonService<ChallegeService> {

    public ResActivityActInfo ActivityActInfo { get; private set; }
    public ActivityType activityType;
    public DifficultyType difficultyType;
    /// <summary>
    /// 打开的上一个窗口
    /// </summary>
    public BaseWindow window;

    private List<int> challengeList = new List<int>();
    public List<int> ChallengeList
    {
        get { return challengeList; }
    }
    protected override void RegisterEventListener()
    {
        base.RegisterEventListener();

        GED.NED.addListener(ResActivityActInfo.MsgId, OnResActivityActInfo);
        GED.NED.addListener(ResChallengeInfo.MsgId, OnResChallengeInfo);
        GED.NED.addListener(ResActivityFightStart.MsgId, OnResActivityFightStar);
        //GED.NED.addListener(ResActivityFightEnd.MsgId, OnResActivityFightEnd);
        GED.NED.addListener(ResActivitySingle.MsgId, OnResActivitySingle);
    }

    protected override void UnRegisterEventListener()
    {
        base.UnRegisterEventListener();

        GED.NED.removeListener(ResActivityActInfo.MsgId, OnResActivityActInfo);
        GED.NED.removeListener(ResChallengeInfo.MsgId, OnResChallengeInfo);
        GED.NED.removeListener(ResActivityFightStart.MsgId, OnResActivityFightStar);
        //GED.NED.removeListener(ResActivityFightEnd.MsgId, OnResActivityFightEnd);
        GED.NED.removeListener(ResActivitySingle.MsgId, OnResActivitySingle);
    }

    #region  服务器消息回调 ----------------------------------------------------------------------------------------------
    private void OnResActivityActInfo(GameEvent evt)
    {
        ResActivityActInfo msg = GetCurMsg<ResActivityActInfo>(evt.EventId);
        ActivityActInfo = msg;

        if(window != null)
            window.OpenChild<ActivityWindow>(WinInfo.Create(false, window.winName, true, null));
        else
            WinMgr.Singleton.Open<ActivityWindow>(null, UILayer.Popup);
    }

    private void OnResChallengeInfo(GameEvent evt)
    {
        ResChallengeInfo msg = GetCurMsg<ResChallengeInfo>(evt.EventId);
        challengeList.Clear();
        challengeList.AddRange(msg.infos);

        if(window != null)
            window.OpenChild<TiaoZhanWindow>(WinInfo.Create(false, window.winName, true, null));
        else
            WinMgr.Singleton.Open<TiaoZhanWindow>(null, UILayer.Popup);
    }

    private void OnResActivityFightStar(GameEvent evt)
    {
        ResActivityFightStart msg = GetCurMsg<ResActivityFightStart>(evt.EventId);

        GED.ED.dispatchEvent(EventID.OnActivityFightStartRes, msg.result);
    }

    public void OnResActivityFightEnd(ResFightResultInfo msg)
    {
        //ResActivityFightEnd msg = GetCurMsg<ResActivityFightEnd>(evt.EventId);
        ActivityDungeonResult result = msg.result as ActivityDungeonResult;
        if (result == null)
        {
            Debug.LogError("活动副本结算消息结构异常");
            return;
        }

        UpdateActivityInfo(result.activityActBaseInfo);
        GED.ED.dispatchEvent(EventID.OnActivityFightEndRes);
    }

    private void OnResActivitySingle(GameEvent evt)
    {
        ResActivitySingle msg = GetCurMsg<ResActivitySingle>(evt.EventId);

        UpdateActivityInfo(msg.act);
        GED.ED.dispatchEvent(EventID.OnActivitySaoDanRes);
    }
    #endregion

    #region  服务器请求 ----------------------------------------------------------------------------------------------

    public void ReqActivitySweep(ActivityType activityType, DifficultyType difficultyType)
    {
        ReqActivitySweep msg = GetEmptyMsg<ReqActivitySweep>();
        msg.act = new ActivityAct();
        msg.act.activityId = (int)activityType;
        msg.act.difficulty = (int)difficultyType;

        SendMsg<ReqActivitySweep>(ref msg);
    }

    public void ReqChallengeInfo()
    {
        ReqChallengeInfo msg = GetEmptyMsg<ReqChallengeInfo>();

        SendMsg<ReqChallengeInfo>(ref msg);
    }

    public void ReqActivityActInfo()
    {
        ReqActivityActInfo msg = GetEmptyMsg<ReqActivityActInfo>();

        SendMsg<ReqActivityActInfo>(ref msg);
    }
    /// <summary>
    /// 请求战斗开始
    /// </summary>
    /// <param name="activityType"></param>
    /// <param name="difficultyType"></param>
    public void ReqActivityFightStart(ActivityType activityType, DifficultyType difficultyType)
    {
        EFightType fightType = EFightType.None;
        if (activityType == ActivityType.Exp)
        {
            fightType = EFightType.ExpDungeon;
        }
        else if (activityType == ActivityType.Gold)
        {
            fightType = EFightType.CoinDungeon;
        }
        else if (activityType == ActivityType.HuanXiang)
        {
            fightType = EFightType.HuanXiangDungeon;
        }
        else if (activityType == ActivityType.NvGeDouJia)
        {
            fightType = EFightType.WomanFighterDungeon;
        }
        FightService.Singleton.ReqFight(fightType, (int)difficultyType);
        //ReqActivityFightStart msg = GetEmptyMsg<ReqActivityFightStart>();

        //msg.act = new ActivityAct();
        //msg.act.activityId = (int)activityType;
        //msg.act.difficulty = (int)difficultyType;

        BattleService.Singleton.Init(GetDiffictyActID((int)difficultyType));
        //SendMsg<ReqActivityFightStart>(ref msg);
    }
    /// <summary>
    /// 请求战斗结果
    /// </summary>
    /// <param name="res">战斗结果：0失败，1成功</param>
    /// <param name="percent">损血百分比</param>
    public void ReqActivityFightEnd(int res, int percent)
    {
        ReqActivityFightEnd msg = GetEmptyMsg<ReqActivityFightEnd>();

        msg.act = new ActivityAct();
        msg.act.activityId = (int)activityType;
        msg.act.difficulty = (int)difficultyType;

        msg.result = res;
        msg.bloodPer = percent;

        SendMsg<ReqActivityFightEnd>(ref msg);
    }

    #endregion

    #region  数据处理 ----------------------------------------------------------------------------------------------

    public ActivityActInfo GetActivityInfoByType(ActivityType type)
    {
        int typeIndex = (int)type;
        if (ActivityActInfo != null)
        {
            int count = ActivityActInfo.activityActInfo.Count;
            ActivityActInfo activityActInfo;
            for (int i = 0; i < count; i++)
            {
                activityActInfo = ActivityActInfo.activityActInfo[i];
                if (activityActInfo.baseInfo.activityId == typeIndex)
                {
                    return activityActInfo;
                }
            }
        }

        return null;
    }

    private void UpdateActivityInfo(ActivityActBaseInfo baseInfo)
    {
        ActivityActInfo activityInfo = GetActivityInfoByType(activityType);
        if (activityInfo != null)
        {
            activityInfo.baseInfo = baseInfo;
        }
    }

    //通过战斗类型获得怪物ID(金币和经验本的)
    public int GetMonterIdByFightType(EFightType fightType, int difficultyIndex)
    {
        int actID = GetActivityActId(fightType, difficultyIndex);


        t_dungeon_actBean bean = ConfigBean.GetBean<t_dungeon_actBean, int>(actID);
        if (bean == null)
            return -1;

        int id = 0;
        int.TryParse(bean.t_wave_monster1, out id);
        return id;
    }

    //通过战斗类型和难度获得活动关卡ID
    public int GetActivityActId(EFightType type, int difficultyIndex)
    {
        int beanID = 0;
        switch (type)
        {
            case EFightType.CoinDungeon:
                beanID = 90000001;
                break;
            case EFightType.ExpDungeon:
                beanID = 91000001;
                break;
            case EFightType.WomanFighterDungeon:
                beanID = 92000001;
                break;
            case EFightType.HuanXiangDungeon:
                beanID = 93000001;
                break;
            default:
                break;
        }
        t_dungeon_chapterBean chapterBean = ConfigBean.GetBean<t_dungeon_chapterBean, int>(beanID);
        if (chapterBean != null && !string.IsNullOrEmpty(chapterBean.t_act_id))
        {
            string[] actIDArr = chapterBean.t_act_id.Split('+');
            if (actIDArr.Length > difficultyIndex)
            {
                beanID = int.Parse(actIDArr[difficultyIndex]);
                return beanID;
            }
        }
        return -1;
    }

    public int GetDiffictyActID(int difficultyIndex)
    {
        int beanID = 0;
        switch (activityType)
        {
            case ActivityType.Gold:
                beanID = 90000001;
                break;
            case ActivityType.Exp:
                beanID = 91000001;
                break;
            case ActivityType.NvGeDouJia:
                beanID = 92000001;
                break;
            case ActivityType.HuanXiang:
                beanID = 93000001;
                break;
            default:
                break;
        }
        t_dungeon_chapterBean chapterBean = ConfigBean.GetBean<t_dungeon_chapterBean, int>(beanID);
        if (chapterBean != null && !string.IsNullOrEmpty(chapterBean.t_act_id))
        {
            string[] actIDArr = chapterBean.t_act_id.Split('+');
            if (actIDArr.Length > difficultyIndex)
            {
                beanID = int.Parse(actIDArr[difficultyIndex - 1]);
                return beanID;
            }
        }
        return -1;
    }
    /// <summary>
    /// 获得活动副本的类型图标
    /// </summary>
    /// <returns></returns>
    public string GetActivityTypeIcon()
    {
        string iconStr = "";
        switch (activityType)
        {
            case ActivityType.Gold:
                iconStr = "jinbitiaozhan";
                break;
            case ActivityType.Exp:
                iconStr = "jingyantiaozhan";
                break;
            case ActivityType.NvGeDouJia:
                iconStr = "jinbitiaozhan";
                break;
            case ActivityType.HuanXiang:
                iconStr = "jinbitiaozhan";
                break;
            default:
                break;
        }

        return UIUtils.GetLoaderUrl(WinEnum.UI_Activity, iconStr);
    }


    //获得幻象副本今日挑战的下标序号
    public int GetHuanxiangChallengeIndex()
    {
        int index = 0;
        for (int i = 0; i < ActivityActInfo.activityActInfo.Count; i++)
        {
            if (ActivityActInfo.activityActInfo[i].hasPhantomIndex())
            {
                index = ActivityActInfo.activityActInfo[i].phantomIndex;
                break;
            }
        }

        return index;
    }

    #endregion

    public override void ClearData()
    {
        base.ClearData();

        if (window != null)
            window.Close();
    }
}

