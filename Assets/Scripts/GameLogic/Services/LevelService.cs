using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Message.Dungeon;
using Message.Bag;
using FairyGUI;
using Data.Beans;
using System;
using Message.Fight;

public enum LevelModel
{
    None = 0,              // 空
    Main = 51,             // 主线
    Elite = 52,            // 精英
    Nightmare = 53,        // 噩梦
    Cycle = 54,            // 轮回
}

public enum BoxOpenType
{
    Level = 0,             // 关卡宝箱
    Chapter = 1,           // 章节
    Key = 2,               // 一键开启
}

public class LevelService : SingletonService<LevelService> {

    private LevelModel levelModel;
    /// <summary>
    /// 当前选择的章节ID 
    /// </summary>
    public int currSelectChapterID;

    public LevelModel LevelModel
    {
        get { return levelModel; }
        set
        {
            levelModel = value;
        }
    }

    public ResDungeonInfo ReqDungeonInfo { get; private set; }
    /// <summary>
    /// 章节信息
    /// </summary>
    private Dictionary<int, ChapterInfo> chapterInfoDict = new Dictionary<int, ChapterInfo>();
    /// <summary>
    /// 关卡信息
    /// </summary>
    private Dictionary<int, ActInfo> actInfoDict = new Dictionary<int, ActInfo>();

    /// <summary>
    /// 当前普通副本可大的最新关卡ID
    /// </summary>
    private int normalRecentlyID;
    /// <summary>
    /// 当前精英副本可大的最新关卡ID
    /// </summary>
    private int eliteRecentlyID;

    public int NormalRecentlyID
    {
        get { return normalRecentlyID; }
    }

    public int EliteRecentlyID
    {
        get { return eliteRecentlyID; }
    }

    public LevelService()
    {
        if (GameManager.Singleton.IsDebug)
            InitTestData();
    }

    protected override void RegisterEventListener()
    {
        base.RegisterEventListener();

        GED.NED.addListener(ResDungeonInfo.MsgId, OnDungeonInfo);
        GED.NED.addListener(ResOpenBox.MsgId, OnOpenBox);
        //GED.NED.addListener(ResSweepAct.MsgId, OnSaoDangResult);
        GED.NED.addListener(ResFastSweepAct.MsgId, OnFastSaoDangResult);
        GED.NED.addListener(ResActInfoUpdate.MsgId, OnResActInfoUpdate);
    }

    protected override void UnRegisterEventListener()
    {
        base.UnRegisterEventListener();

        GED.NED.removeListener(ResDungeonInfo.MsgId, OnDungeonInfo);
        GED.NED.removeListener(ResOpenBox.MsgId, OnOpenBox);
        //GED.NED.removeListener(ResSweepAct.MsgId, OnSaoDangResult);
        GED.NED.removeListener(ResFastSweepAct.MsgId, OnFastSaoDangResult);
        GED.NED.removeListener(ResActInfoUpdate.MsgId, OnResActInfoUpdate);
    }
    /// <summary>
    /// 获得章节信息
    /// </summary>
    /// <returns></returns>
    public ChapterInfo GetChapterInfoByID(int chapterID)
    {

        if (chapterInfoDict.ContainsKey(chapterID))
        {
            return chapterInfoDict[chapterID];
        }
        Logger.err("不存在章节ID :" + chapterID);
        return null;
    }
    /// <summary>
    /// 获得关卡信息
    /// </summary>
    /// <returns></returns>
    public ActInfo GetActInfoByID(int actID)
    {
        if (actInfoDict.ContainsKey(actID))
        {
            return actInfoDict[actID];
        }
        Logger.wrn("没有开启关卡ID：" + actID);
        return null;
    }
    /// <summary>
    /// 获得当前模式下的最新的关卡ID
    /// </summary>
    /// <returns></returns>
    public int GetLastActID(LevelModel chapterModel)
    {
        switch (chapterModel)
        {
            case LevelModel.Main:
                return normalRecentlyID;
            case LevelModel.Elite:
                return eliteRecentlyID;
            case LevelModel.Nightmare:
                break;
            case LevelModel.Cycle:
                break;
            default:
                break;
        }
        Logger.err("不能获得最新的关卡信息:" + levelModel.ToString());
        return int.MaxValue;
    }
    /// <summary>
    /// 获得副本信息
    /// </summary>
    /// <returns></returns>
    public DungeonInfo GetDungeonInfo(LevelModel model)
    {
        int modelIndex = (int)model;
        List<DungeonInfo> dungeonInfoList = ReqDungeonInfo.dungeonInfos;
        int count = dungeonInfoList.Count;

        DungeonInfo dungeonInfo = null;
        for (int i = 0; i < count; i++)
        {
            dungeonInfo = dungeonInfoList[i];
            if (dungeonInfo.dungeonId == modelIndex)
                return dungeonInfo;
        }

        return null;
    }

    public int GetActStarInfoById(int actId)
    {
        ActInfo actInfo = GetActInfoByID(actId);
        if (actInfo == null)
            return -1;
        return actInfo.star;
    }

    //过滤道具中的经验和金币
    public void FilterItem(List<ItemInfo> rewards, out int expNum, out int coinNum, out List<ItemInfo> items)
    {
        int coinTotalNum = 0;
        int expTotalNum = 0;
        List<ItemInfo> newItems = new List<ItemInfo>();
        for (int i = 0; i < rewards.Count; i++)
        {
            if (rewards[i].id == -1)
            {
                //金币
                coinTotalNum += rewards[i].num;
            }
            else if (rewards[i].id == -9)
            {
                //经验
                expTotalNum += rewards[i].num;
            }
            else
                newItems.Add(rewards[i]);
        }

        expNum = expTotalNum;
        coinNum = coinTotalNum;
        items = newItems;
    }

    //获得能扫荡的章节列表
    public List<ChapterInfo> GetCanSaoDangChapterList(LevelModel model)
    {
        List<ChapterInfo> chapterList = new List<ChapterInfo>();
        foreach (var chapterInfo in chapterInfoDict)
        {
            int type = chapterInfo.Key / 1000000;
            if (type == (int)model)
            {
                List<ActInfo> actList = chapterInfo.Value.actInfos;
                //对应难度
                for (int i = 0; i < actList.Count; i++)
                {
                    if (this.CanSaoDang(actList[i].actId))
                    {
                        chapterList.Add(chapterInfo.Value);
                        break;
                    }
              }
            }
 
        }

        return chapterList;
    }

    //能否扫荡50
    public bool CheckCanSweep50(int actId)
    {
        int open50 = ConfigBean.GetBean<t_globalBean, int>(19016).t_int_param;
        int star = GetActStarInfoById(actId);
        int roleLevel = RoleService.Singleton.GetRoleInfo().level;
        if (CanSaoDang(actId) && roleLevel >= open50)
            return true;
        return false;
    }

    //能否扫荡10
    public bool CheckCanSweep10(int actId)
    {
        int open10 = ConfigBean.GetBean<t_globalBean, int>(19015).t_int_param;
        int star = GetActStarInfoById(actId);
        int roleLevel = RoleService.Singleton.GetRoleInfo().level;
        if (CanSaoDang(actId) && roleLevel >= open10)
            return true;
        return false;
    }

    public bool CanSaoDang(int actId)
    {
        t_dungeon_actBean actBean = ConfigBean.GetBean<t_dungeon_actBean, int>(actId);
        if (actBean == null)
            return false;

        int starLevel = GetActStarInfoById(actId);
        if (starLevel <= 0)
            return false;

        if (starLevel >= 3)
        {
            return true;
        }

        int levelDis = 10;
        t_globalBean bean = ConfigBean.GetBean<t_globalBean, int>(19032);
        if (bean != null)
            levelDis = bean.t_int_param;


        return (RoleService.Singleton.GetRoleInfo().level >= actBean.t_level_limit + levelDis);


    }


    //检测能否挑战或扫荡
    public bool CheckCanDo(int actId, int num, bool isSaoDang = true)
    {
        if (isSaoDang && !CanSaoDang(actId))
        {
            TipWindow.Singleton.ShowTip(61801035);
            return false;
        }

        t_dungeon_actBean actBean = ConfigBean.GetBean<t_dungeon_actBean, int>(actId);
        if (actBean == null)
            return false;

        int curEnergy = RoleService.Singleton.RoleInfo.roleInfo.energy;
        if (curEnergy < actBean.t_comsumePower * num)
        {
            //体力不足
            CommonTipsManager.GetInstance().ShowTips(TipsType.SingleButton, "体力不足", "体力不足,是否前往购买?", ()=> {
                RoleService.Singleton.BuyEnergy();
            });
            return false;
        }

        return true;
    }


    private void SetRecentlyID()
    {
        DungeonInfo dungeonInfo = GetDungeonInfo(LevelModel.Main);
        int count = 0;
        if (dungeonInfo != null)
        {
            t_dungeon_actBean actBean = ConfigBean.GetBean<t_dungeon_actBean, int>(dungeonInfo.bestRecordId);
            if (actBean == null)
            {
                count = dungeonInfo.chapterInfos.Count;
                normalRecentlyID = GetChapterLastActID(dungeonInfo.chapterInfos[count - 1].chapterId);
            }
            else
                normalRecentlyID = dungeonInfo.bestRecordId;
        }

        dungeonInfo = GetDungeonInfo(LevelModel.Elite);
        if (dungeonInfo != null)
        {
            t_dungeon_actBean actBean = ConfigBean.GetBean<t_dungeon_actBean, int>(dungeonInfo.bestRecordId);
            if (actBean == null)
            {
                count = dungeonInfo.chapterInfos.Count;
                normalRecentlyID = GetChapterLastActID(dungeonInfo.chapterInfos[count - 1].chapterId);
            }
            else
                eliteRecentlyID = dungeonInfo.bestRecordId;
        }
    }
    /// <summary>
    /// 战斗结果后更新关卡信息
    /// </summary>
    /// <param name="msg"></param>
    public void UpdateDungeonInfo(MissionResult msg)
    {
        if (msg.openNewChapterFlag == 1)
        {
            return;
        }
        t_dungeon_actBean actBean = ConfigBean.GetBean<t_dungeon_actBean, int>(msg.actId);
        if (actBean == null)
            return;

        ChapterInfo chapterInfo = GetChapterInfoByID(actBean.t_chapter_id);
        if (chapterInfo == null)
            return;
        // 更新章节信息
        chapterInfo.star = msg.chapterStar;
        chapterInfo.boxStatus.Clear();
        chapterInfo.boxStatus.AddRange(msg.boxStatus);
        DungeonInfo dungeonInfo = null;
        // 更新最新关卡ID
        switch (levelModel)
        {
            case LevelModel.Main:
                dungeonInfo = GetDungeonInfo(LevelModel.Main);
                dungeonInfo.bestRecordId = msg.__bestRecordId;
                break;
            case LevelModel.Elite:
                dungeonInfo = GetDungeonInfo(LevelModel.Elite);
                dungeonInfo.bestRecordId = msg.__bestRecordId;
                break;
            case LevelModel.Nightmare:
                break;
            case LevelModel.Cycle:
                break;
            default:
                break;
        }
        SetRecentlyID();

        GED.ED.dispatchEvent(EventID.OnFightSuccessed, msg.actId);
    }

    /// <summary>
    /// 获得关卡模式的提示图片
    /// </summary>
    /// <returns></returns>
    public string GetLevelModelTipIcon(LevelModel levelModel)
    {
        string icon = "";
        switch (levelModel)
        {
            case LevelModel.Main:
                icon = "putong_01";
                break;
            case LevelModel.Elite:
                icon = "jingying_01";
                break;
            case LevelModel.Nightmare:
                icon = "emeng_01";
                break;
            case LevelModel.Cycle:
                break;
            default:
                break;
        }

        return icon;
    }
    public string GetLevelModelTipNameBgIcon(LevelModel levelModel)
    {
        string icon = "";
        switch (levelModel)
        {
            case LevelModel.Main:
                icon = "taitou04";
                break;
            case LevelModel.Elite:
                icon = "taitou05";
                break;
            case LevelModel.Nightmare:
                icon = "taitou06";
                break;
            case LevelModel.Cycle:
                break;
            default:
                break;
        }

        return icon;
    }

    public int GetChapterLastActID(int chapterID)
    {
        t_dungeon_chapterBean chapterBean = ConfigBean.GetBean<t_dungeon_chapterBean, int>(chapterID);
        if (chapterBean != null)
        {
            int[] actArr = GTools.splitStringToIntArray(chapterBean.t_act_id);
            if (actArr.Length > 1)
            {
                return actArr[actArr.Length - 1];
            }
        }

        return 0;
    }
    /// <summary>
    /// 获得最近通关的关卡ID
    /// </summary>
    /// <returns></returns>
    public int GetCurrPassActID(LevelModel model)
    {
        int currPassActID = 0;
        switch (model)
        {
            case LevelModel.Main:
                currPassActID = normalRecentlyID;
                break;
            case LevelModel.Elite:
                currPassActID = eliteRecentlyID;
                break;
            case LevelModel.Nightmare:
                break;
            case LevelModel.Cycle:
                break;
            default:
                break;
        }
        // 如果最新的关卡已经通关直接返回
        ActInfo actInfo = GetActInfoByID(currPassActID);
        if (actInfo != null && actInfo.star > 0)
            return currPassActID;

        t_dungeon_actBean actBean = ConfigBean.GetBean<t_dungeon_actBean, int>(currPassActID);
        if(actBean != null)
        {
            t_dungeon_chapterBean chapterBean = ConfigBean.GetBean<t_dungeon_chapterBean, int>(actBean.t_chapter_id);
            if (chapterBean != null)
            {
                // 遍历最新关卡的章节对应的关卡是否有通过的, 倒序遍历
                int[] actIDArr = GTools.splitStringToIntArray(chapterBean.t_act_id);
                int actID = 0;
                for (int i = actIDArr.Length - 1; i >= 0; i--)
                {
                    actID = actIDArr[i];
                    if (actID < currPassActID)
                    {
                        actInfo = GetActInfoByID(actID);
                        if (actInfo != null && actInfo.star > 0)
                            return actID;
                    }
                }
                // 遍历最新章节的上一个章节的关卡是否通关, 倒序遍历
                chapterBean = ConfigBean.GetBean<t_dungeon_chapterBean, int>(actBean.t_chapter_id - 1);
                if(chapterBean != null)
                {
                    actIDArr = GTools.splitStringToIntArray(chapterBean.t_act_id);
                    int count = actIDArr.Length;
                    for (int i = count - 1; i >= 0; i--)
                    {
                        actID = actIDArr[i];
                        actInfo = GetActInfoByID(actID);
                        if (actInfo != null && actInfo.star > 0)
                            return actID;
                    }
                }
            }
        }
        return currPassActID;
    }
    
    #region    监听服务器的信息---------------------------------------------------------------------------------------------------

    /// <summary>
    /// 收到副本信息
    /// </summary>
    /// <param name="evt"></param>
    private void OnDungeonInfo(GameEvent evt)
    {
        ResDungeonInfo info = GetCurMsg<ResDungeonInfo>(evt.EventId);
        ReqDungeonInfo = GetCurMsg<ResDungeonInfo>(evt.EventId);
        chapterInfoDict.Clear();
        actInfoDict.Clear();
        if (ReqDungeonInfo != null)
        {
            List<DungeonInfo> dungeonInfoList = ReqDungeonInfo.dungeonInfos;
            for (int i = 0; i < dungeonInfoList.Count; i++)
            {
                List<ChapterInfo> chapterList = ReqDungeonInfo.dungeonInfos[i].chapterInfos;
                int count = chapterList.Count;
                for (int j = 0; j < count; j++)
                {
                    ChapterInfo chapterInfo = chapterList[j];
                    if (chapterInfoDict.ContainsKey(chapterInfo.chapterId))
                    {
                        chapterInfoDict[chapterInfo.chapterId] = chapterInfo;
                    }
                    else
                    {
                        chapterInfoDict.Add(chapterInfo.chapterId, chapterInfo);
                    }
                     
                    List<ActInfo> normalActList = chapterInfo.actInfos;

                    for (int k = 0; k < normalActList.Count; k++)
                    {
                        ActInfo normalActInfo = normalActList[k];
                        if (actInfoDict.ContainsKey(normalActInfo.actId))
                        {
                            actInfoDict[normalActInfo.actId] = normalActInfo;
                        }
                        else
                        {
                            actInfoDict.Add(normalActInfo.actId, normalActInfo);
                        }
                    }
                }
            }
        }

        SetRecentlyID();
        if (levelModel == LevelModel.None)
        {
            levelModel = LevelModel.Main;
        }

        GED.ED.dispatchEvent(EventID.OnDungeonInfoUpdate);
    }
    /// <summary>
    /// 收到宝箱开启
    /// </summary>
    /// <param name="evt"></param>
    private void OnOpenBox(GameEvent evt)
    {
        ResOpenBox resOpenBox = GetCurMsg<ResOpenBox>(evt.EventId);

        BoxOpenType boxOpenType = (BoxOpenType)resOpenBox.type;
        switch (boxOpenType)
        {
            case BoxOpenType.Level:
                ActInfo actInfo = GetActInfoByID(resOpenBox.chapterOrActId);
                if (actInfo == null)
                    return;
                actInfo.boxStatus = 1;
                break;
            case BoxOpenType.Chapter:
                ChapterInfo chapterInfo = GetChapterInfoByID(resOpenBox.chapterOrActId);
                if (chapterInfo == null)
                    return;
                chapterInfo.boxStatus[resOpenBox.chapterBoxSerialNum] = 1;
                break;
            case BoxOpenType.Key:
                break;
            default:
                break;
        }

        GED.ED.dispatchEvent(EventID.OnLevelOpenBox, resOpenBox.items);
    }

    /// <summary>
    /// 关卡信息更新
    /// </summary>
    /// <param name="evt"></param>
    /// 
    private void OnResActInfoUpdate(GameEvent evt)
    {
        ResActInfoUpdate msg = GetCurMsg<ResActInfoUpdate>(evt.EventId);
        for (int i = 0; i < msg.actInfos.Count; i++)
        {

            t_dungeon_actBean actBean = ConfigBean.GetBean<t_dungeon_actBean, int>(msg.actInfos[i].actId);
            if (actBean == null)
                continue;

            ChapterInfo chapterInfo = GetChapterInfoByID(actBean.t_chapter_id);
            if (chapterInfo == null)
                continue;


            // 更新关卡信息
            if (actInfoDict.ContainsKey(msg.actInfos[i].actId))
            {
                actInfoDict[msg.actInfos[i].actId] = msg.actInfos[i];
            }

            int count = chapterInfo.actInfos.Count;
            ActInfo actInfo = null;
            for (int j = 0; j < count; j++)
            {
                actInfo = chapterInfo.actInfos[j];
                if (actInfo.actId == msg.actInfos[i].actId)
                {

                    chapterInfo.actInfos[j] = msg.actInfos[i];
                    break;
                }
            }
        }

        //关卡数据更新
        GED.ED.dispatchEvent(EventID.ActDataUpdate);
    }

    /// <summary>
    /// 快速扫荡结果
    /// </summary>
    /// <param name="evt"></param>
    /// 
    private void OnFastSaoDangResult(GameEvent evt)
    {
        ResFastSweepAct msg = GetCurMsg<ResFastSweepAct>(evt.EventId);
  
        TwoParam<List<SweepItem>, List<ItemInfo>> param = new TwoParam<List<SweepItem>, List<ItemInfo>>();
        param.value1 = msg.awards;
        param.value2 = msg.extraAward;
        WinMgr.Singleton.Open<SaoDangJieSuanWindow>(WinInfo.Create(false, null, false, param), UILayer.TopHUD);
    }
 

    #endregion;

    #region    请求-----------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// 请求开启关卡宝箱箱子
    /// </summary>
    /// <param name="boxID"></param>
    public void ReqOpenActBox(int actID)
    {
        ReqOpenActBox msg = GetEmptyMsg<ReqOpenActBox>();
        msg.actId = actID;

        SendMsg<ReqOpenActBox>(ref msg);
    }
    /// <summary>
    /// 请求开启章节宝箱
    /// </summary>
    /// <param name="chapterID"></param>
    /// <param name="index"></param>
    public void ReqOpenChapterBox(int chapterID, int index)
    {
        ReqOpenChapterBox msg = GetEmptyMsg<ReqOpenChapterBox>();
        msg.chapterId = chapterID;
        msg.serialNum = index;

        SendMsg<ReqOpenChapterBox>(ref msg);
    }
    /// <summary>
    /// 请求一键开启宝箱
    /// </summary>
    public void ReqFastOpenBox()
    {
        ReqFastOpenBox msg = GetEmptyMsg<ReqFastOpenBox>();

        msg.dungeonId = (int)levelModel;

        SendMsg<ReqFastOpenBox>(ref msg);
    }
    /// <summary>
    /// 请求扫荡
    /// </summary>
    /// <param name="actID">关卡ID</param>
    public void ReqSweepAct(List<SweepReqInfo> sweepInfos)
    {
        ReqSweepAct msg = GetEmptyMsg<ReqSweepAct>();
        msg.sweepReqInfos.Clear();
        for (int i = 0; i < sweepInfos.Count; i++)
        {
            msg.sweepReqInfos.Add(sweepInfos[i]);

        }
       SendMsg(ref msg);
    }

    //单个关卡扫荡
    public void SingleActSweep(int actId, int num)
    {
        List<SweepReqInfo> sweeps = new List<SweepReqInfo>();
        SweepReqInfo sweepInfo = new SweepReqInfo();
        sweepInfo.actId = actId;
        sweepInfo.num = num;
        sweeps.Add(sweepInfo);
        ReqSweepAct(sweeps);
    }


    /// <summary>
    /// 请求开始战斗
    /// </summary>
    /// <param name="actID">关卡ID</param>
    public void ReqFightStart(int actID)
    {
        FightService.Singleton.ReqFight(EFightType.Level, actID);
        //ReqFightStart msg = GetEmptyMsg<ReqFightStart>();
        //msg.actId = actID;
        //BattleService.Singleton.Init(actID);
        //SendMsg(ref msg);
    }

    /// <summary>
    /// 请求重置精英关卡攻打次数
    /// </summary>
    /// <param name="actID">关卡ID</param>
    public void ReqResetAttackNum(int actID)
    {
        ReqResetAttackNum msg = GetEmptyMsg<ReqResetAttackNum>();
        msg.actId = actID;
        SendMsg(ref msg);
    }
    /// <summary>
    /// 递交战斗结果
    /// </summary>
    /// <param name="actID"></param>
    /// <param name="figthResult">0失败，1成功</param>
    /// <param name="star"></param>
    public void ReqFightResult(int actID, int figthResult, int star)
    {
        ReqFightResult msg = GetEmptyMsg<ReqFightResult>();
        msg.actId = actID;
        msg.fightResult = figthResult;
        msg.star = star;

        SendMsg<ReqFightResult>(ref msg);
    }

    #endregion;

    /// <summary>
    ///  配置测试数据
    /// </summary>
    private void InitTestData()
    {
        ReqDungeonInfo = new ResDungeonInfo();
        #region   章节1
        ChapterInfo normaChapterInfo = new ChapterInfo();
        normaChapterInfo.boxStatus.Add(-1);
        normaChapterInfo.boxStatus.Add(0);
        normaChapterInfo.boxStatus.Add(1);
        normaChapterInfo.chapterId = 101;

        ActInfo normalActInfo = new ActInfo();
        normalActInfo.actId = 10101;
        normalActInfo.boxStatus = -2;
        normalActInfo.star = 1;

        ActInfo normalActInfo1 = new ActInfo();
        normalActInfo1.actId = 10102;
        normalActInfo1.boxStatus = 0;
        normalActInfo1.star = 1;

        ActInfo normalActInfo2 = new ActInfo();
        normalActInfo2.actId = 10103;
        normalActInfo2.boxStatus = 0;
        normalActInfo2.star = 1;

        ActInfo normalActInfo3 = new ActInfo();
        normalActInfo3.actId = 10104;
        normalActInfo3.boxStatus = -2;
        normalActInfo3.star = 0;

        ActInfo normalActInfo4 = new ActInfo();
        normalActInfo4.actId = 10105;
        normalActInfo4.boxStatus = 0;
        normalActInfo4.star = 2;

        ActInfo normalActInfo5 = new ActInfo();
        normalActInfo5.actId = 10106;
        normalActInfo5.boxStatus = -2;
        normalActInfo5.star = 3;

        normaChapterInfo.actInfos.Add(normalActInfo);
        normaChapterInfo.actInfos.Add(normalActInfo1);
        normaChapterInfo.actInfos.Add(normalActInfo2);
        normaChapterInfo.actInfos.Add(normalActInfo3);
        normaChapterInfo.actInfos.Add(normalActInfo4);
        normaChapterInfo.actInfos.Add(normalActInfo5);

        actInfoDict.Add(normalActInfo.actId, normalActInfo);
        actInfoDict.Add(normalActInfo1.actId, normalActInfo1);
        actInfoDict.Add(normalActInfo2.actId, normalActInfo2);
        actInfoDict.Add(normalActInfo3.actId, normalActInfo3);
        actInfoDict.Add(normalActInfo4.actId, normalActInfo4);
        actInfoDict.Add(normalActInfo5.actId, normalActInfo5);
#endregion
        #region 章节2 -----------------------------------------------------------------------------
        ChapterInfo normaChapterInfo2 = new ChapterInfo();
        normaChapterInfo2.boxStatus.Add(-1);
        normaChapterInfo2.boxStatus.Add(0);
        normaChapterInfo2.boxStatus.Add(1);
        normaChapterInfo2.chapterId = 102;

        ActInfo normalActInfo11 = new ActInfo();
        normalActInfo11.actId = 10201;
        normalActInfo11.boxStatus = -2;
        normalActInfo11.star = 2;

        ActInfo normalActInfo12 = new ActInfo();
        normalActInfo12.actId = 10202;
        normalActInfo12.boxStatus = 0;
        normalActInfo12.star = 0;

        ActInfo normalActInfo21 = new ActInfo();
        normalActInfo21.actId = 10203;
        normalActInfo21.boxStatus = 0;
        normalActInfo21.star = 0;

        normaChapterInfo2.actInfos.Add(normalActInfo11);
        normaChapterInfo2.actInfos.Add(normalActInfo12);
        normaChapterInfo2.actInfos.Add(normalActInfo21);

        actInfoDict.Add(normalActInfo11.actId, normalActInfo11);
        actInfoDict.Add(normalActInfo12.actId, normalActInfo12);
        actInfoDict.Add(normalActInfo21.actId, normalActInfo21);

        chapterInfoDict.Add(normaChapterInfo2.chapterId, normaChapterInfo2);
        chapterInfoDict.Add(normaChapterInfo.chapterId, normaChapterInfo);
        #endregion;

        ReqDungeonInfo.dungeonInfos.Add(new DungeonInfo());
        ReqDungeonInfo.dungeonInfos[0].chapterInfos.Add(normaChapterInfo);
        ReqDungeonInfo.dungeonInfos[0].chapterInfos.Add(normaChapterInfo2);
        ReqDungeonInfo.dungeonInfos[0].dungeonId = 51;

        levelModel = LevelModel.Main;
        normalRecentlyID = 10201;
    }


}
