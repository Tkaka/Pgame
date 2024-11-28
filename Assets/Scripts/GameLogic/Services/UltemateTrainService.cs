using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Message.Challenge;
using Message.Bag;
using Data.Beans; 
using Message.Fight;

public class UltemateTrainService : SingletonService<UltemateTrainService> {
    /// <summary>
    /// 终极试炼信息
    /// </summary>
    public ResTrialInfo trainInfo { get; private set; }
    /// <summary>
    /// 终极试炼跳过信息
    /// </summary>
    public ResTrialSkip trainSkipInfo { get;  set; }
    /// <summary>
    /// 试炼奖励信息
    /// </summary>
    public TrialScoreAwardInfo trainScoreAwardInfo { get; private set; }
    public int curSelectPropertyIndex { get; set; }
    public int fightResult = -1;
    public bool isOpenWindow;
    public BaseWindow parentWindow;

    protected override void RegisterEventListener()
    {
        base.RegisterEventListener();

        GED.NED.addListener(ResTrialInfo.MsgId, OnResTrialInfo);
        GED.NED.addListener(ResTrialSkip.MsgId, OnResTrialSkip);
        GED.NED.addListener(ResTrialMonsterFloor.MsgId, OnResTrialMonsterFloor);
        GED.NED.addListener(ResTrialAttrFloor.MsgId, OnResTrialAttrrFloor);
        GED.NED.addListener(ResTrialBoxFloor.MsgId, OnResTrialBoxFloor);
        GED.NED.addListener(ResTrialSingleBoxOpen.MsgId, OnResTrialSingleBoxOpen);
        GED.NED.addListener(ResTrialScoreAwardInfo.MsgId, OnResTrialScoreAwardInfo);
        GED.NED.addListener(ResTrialScoreAwardGet.MsgId, OnResTrialScoreAwardGet);
        GED.NED.addListener(ResTrialFightStart.MsgId, OnResTrialFightStart);
        GED.NED.addListener(ResTrialBatchBoxOpen.MsgId, OnResTrialBatchBoxOpen);
    }

    protected override void UnRegisterEventListener()
    {
        base.UnRegisterEventListener();

        GED.NED.removeListener(ResTrialInfo.MsgId, OnResTrialInfo);
        GED.NED.removeListener(ResTrialSkip.MsgId, OnResTrialSkip);
        GED.NED.removeListener(ResTrialMonsterFloor.MsgId, OnResTrialMonsterFloor);
        GED.NED.removeListener(ResTrialAttrFloor.MsgId, OnResTrialAttrrFloor);
        GED.NED.removeListener(ResTrialBoxFloor.MsgId, OnResTrialBoxFloor);
        GED.NED.removeListener(ResTrialSingleBoxOpen.MsgId, OnResTrialSingleBoxOpen);
        GED.NED.removeListener(ResTrialScoreAwardInfo.MsgId, OnResTrialScoreAwardInfo);
        GED.NED.removeListener(ResTrialScoreAwardGet.MsgId, OnResTrialScoreAwardGet);
        GED.NED.removeListener(ResTrialFightStart.MsgId, OnResTrialFightStart);
        GED.NED.removeListener(ResTrialBatchBoxOpen.MsgId, OnResTrialBatchBoxOpen);
    }

    #region 服务器回调 **********************************************************************************************************
    /// <summary>
    /// 终极试炼信息回调
    /// </summary>
    /// <param name="evt"></param>
    private void OnResTrialInfo(GameEvent evt)
    {
        ResTrialInfo msg = GetCurMsg<ResTrialInfo>(evt.EventId);

        trainInfo = msg;
        if (isOpenWindow == false)
        {
            if (parentWindow != null)
                parentWindow.OpenChild<UltemateTrainMainWindow>(null);
            else
                WinMgr.Singleton.Open<UltemateTrainMainWindow>(null, UILayer.Popup);

            isOpenWindow = true;
        }
        GED.ED.dispatchEvent(EventID.OnResTrainInfo);
    }
    /// <summary>
    /// 终极试炼跳过回调
    /// </summary>
    /// <param name="evt"></param>
    private void OnResTrialSkip(GameEvent evt)
    {
        ResTrialSkip msg = GetCurMsg<ResTrialSkip>(evt.EventId);

        trainSkipInfo = msg;
        trainInfo.trialInfo = msg.trialInfo;

        GED.ED.dispatchEvent(EventID.OnResTrialSkip);
    }
    /// <summary>
    /// 怪物层信息回调
    /// </summary>
    private void OnResTrialMonsterFloor(GameEvent evt)
    {
        ResTrialMonsterFloor msg = GetCurMsg<ResTrialMonsterFloor>(evt.EventId);
        GED.ED.dispatchEvent(EventID.OnResTrainFloor, msg.monsters);
    }
    /// <summary>
    /// 属性层信息回调
    /// </summary>
    /// <param name="evt"></param>
    private void OnResTrialAttrrFloor(GameEvent evt)
    {
        ResTrialAttrFloor msg = GetCurMsg<ResTrialAttrFloor>(evt.EventId);
        trainInfo.buffs.Clear();
        trainInfo.buffs.AddRange(msg.buffs);
        trainInfo.trialInfo.star = msg.star;
        trainInfo.trialInfo.petStatus.Clear();
        trainInfo.trialInfo.petStatus.AddRange(msg.petStatus);
        trainInfo.trialInfo.floor = msg.floor - 1;

        GED.ED.dispatchEvent(EventID.OnResTrainFloor, msg.trialFloorAttr);
    }
    /// <summary>
    /// 宝箱层信息回调
    /// </summary>
    /// <param name="evt"></param>
    private void OnResTrialBoxFloor(GameEvent evt)
    {
        ResTrialBoxFloor msg = GetCurMsg<ResTrialBoxFloor>(evt.EventId);
        trainInfo.trialInfo.floor = msg.floor - 1;
        GED.ED.dispatchEvent(EventID.OnResTrainFloor, msg.rewards);
    }
    /// <summary>
    /// 隐秘宝箱开启回调
    /// </summary>
    /// <param name="evt"></param>
    private void OnResTrialSingleBoxOpen(GameEvent evt)
    {
        ResTrialSingleBoxOpen msg = GetCurMsg<ResTrialSingleBoxOpen>(evt.EventId);

        if(trainSkipInfo != null)
        {
            List<IntVsInt> boxInfoList = new List<IntVsInt>();
            boxInfoList.Add(msg.diamondBoxInfo);
            UpdateTrainSkipBoxInfo(boxInfoList);
        }
        
        GED.ED.dispatchEvent(EventID.OnResTrialSingleBoxOpen, msg);
    }

    private void OnResTrialBatchBoxOpen(GameEvent evt)
    {
        ResTrialBatchBoxOpen msg = GetCurMsg<ResTrialBatchBoxOpen>(evt.EventId);

        if (trainSkipInfo != null)
        {
            UpdateTrainSkipBoxInfo(msg.diamondBoxInfo);
        }

        GED.ED.dispatchEvent(EventID.OnResTrialBatchBoxOpen, msg);
    }
    /// <summary>
    /// 积分奖励信息回调
    /// </summary>
    /// <param name="evt"></param>
    private void OnResTrialScoreAwardInfo(GameEvent evt)
    {
        ResTrialScoreAwardInfo msg = GetCurMsg<ResTrialScoreAwardInfo>(evt.EventId);
        trainScoreAwardInfo = msg.trialScoreAwardInfo;
        GED.ED.dispatchEvent(EventID.OnResTrialScoreAwardInfo, msg.trialScoreAwardInfo);
    }
    /// <summary>
    /// 领取积分奖励回调
    /// </summary>
    /// <param name="evt"></param>
    private void OnResTrialScoreAwardGet(GameEvent evt)
    {
        ResTrialScoreAwardGet msg = GetCurMsg<ResTrialScoreAwardGet>(evt.EventId);
        trainScoreAwardInfo = msg.trialScoreAwardInfo;
        GED.ED.dispatchEvent(EventID.OnResTrialScoreAwardGet, msg.rewards);
    }
    /// <summary>
    /// 试炼战斗开始回调
    /// </summary>
    /// <param name="evt"></param>
    private void OnResTrialFightStart(GameEvent evt)
    {
        ResTrialFightStart msg = GetCurMsg<ResTrialFightStart>(evt.EventId);
        GED.ED.dispatchEvent(EventID.OnResTrialFightStart);
    }
    /// <summary>
    /// 战斗结束
    /// </summary>
    /// <param name="evt"></param>
    public void OnResTrialFightEnd(ResFightResultInfo resultInfo)
    {
        TrailResult msg = resultInfo.result as TrailResult;
        trainInfo.trialInfo = msg.trialInfo;
        fightResult = msg.result;
    }

    #endregion

    #region 请求**************************************************************************************************************
    /// <summary>
    /// 获取终极试炼的信息
    /// </summary>
    public void ReqUltemateTrialInfo()
    {
        ReqTrialInfo msg = GetEmptyMsg<ReqTrialInfo>();

        SendMsg<ReqTrialInfo>(ref msg);
    }
    /// <summary>
    /// 请求终极试炼跳过
    /// </summary>
    public void ReqUltemateTrialSkip()
    {
        ReqTrialSkip msg = GetEmptyMsg<ReqTrialSkip>();

        SendMsg<ReqTrialSkip>(ref msg);
    }
    /// <summary>
    /// 请求终极试炼属性兑换
    /// </summary>
    /// <param name="index">属性下标</param>
    public void ReqUltemateTrialAttrExchange(int floor, int index, int petID)
    {
        ReqTrialAttrExchange msg = GetEmptyMsg<ReqTrialAttrExchange>();
        msg.index = index;
        msg.floor = floor;
        msg.petId = petID;

        SendMsg<ReqTrialAttrExchange>(ref msg);
    }
    /// <summary>
    /// 请求终极试炼打开钻石宝箱
    /// </summary>
    public void ReqUltemateTrialBoxOpen(int floor, int num)
    {
        ReqTrialBoxOpen msg = GetEmptyMsg<ReqTrialBoxOpen>();

        msg.floor = floor;
        msg.number = num;

        SendMsg<ReqTrialBoxOpen>(ref msg);
    }
    /// <summary>
    /// 终极试炼批量打开钻石宝箱
    /// </summary>
    public void ReqUltemateTrialBatchBoxOpen(int num)
    {
        ReqTrialBatchBoxOpen msg = GetEmptyMsg<ReqTrialBatchBoxOpen>();

        int count = trainSkipInfo.diamondBoxInfos.Count;
        IntVsInt boxInfo = null;
        for (int i = 0; i < count; i++)
        {
            boxInfo = trainSkipInfo.diamondBoxInfos[i];
            msg.floors.Add(boxInfo.int1);
        }
        msg.number = num;

        SendMsg<ReqTrialBatchBoxOpen>(ref msg);
    }
    /// <summary>
    /// 请求当前层的试炼信息
    /// </summary>
    public void ReqUltemateTrialFloorInfo()
    {
        ReqTrialFloorInfo msg = GetEmptyMsg<ReqTrialFloorInfo>();

        //List<int> bestTeamPets = PetService.Singleton.GetBestTeamList(ZhenRongType.ZhongJiShiLian);
        msg.floor = trainInfo.trialInfo.floor;
        SendMsg<ReqTrialFloorInfo>(ref msg);
    }
    /// <summary>
    /// 获取终极试炼的积分奖励信息
    /// </summary>
    public void ReqTrialScoreAwardInfo()
    {
        ReqTrialScoreAwardInfo msg = GetEmptyMsg<ReqTrialScoreAwardInfo>();

        SendMsg<ReqTrialScoreAwardInfo>(ref msg);
    }
    /// <summary>
    /// 领取终极试炼积分奖励
    /// </summary>
    public void ReqTrialScoreAwardGet(int index)
    {
        ReqTrialScoreAwardGet msg = GetEmptyMsg<ReqTrialScoreAwardGet>();

        msg.index = index;
        SendMsg<ReqTrialScoreAwardGet>(ref msg);
    }
    /// <summary>
    /// 请求战斗开始
    /// </summary>
    /// <param name="index"></param>
    public void ReqTrialFightStart(int index)
    {
        FightService.Singleton.ReqFight(EFightType.ZhongJiShiLian, trainInfo.trialInfo.floor * 1000 + index);
        //ReqTrialFightStart msg = GetEmptyMsg<ReqTrialFightStart>();
        //msg.floor = trainInfo.trialInfo.floor;
        //msg.index = index;

        //SendMsg<ReqTrialFightStart>(ref msg);
    }
    /// <summary>
    /// 请求战斗结果
    /// </summary>
    /// <param name="result"></param>
    public void ReqTrialFightEnd(int result, List<TrialPetStatus> petStatues)
    {
        ReqTrialFightEnd msg = GetEmptyMsg<ReqTrialFightEnd>();

        msg.result = result;
        msg.floor = trainInfo.trialInfo.floor;
        msg.status.AddRange(petStatues);

        SendMsg<ReqTrialFightEnd>(ref msg);
    }
    /// <summary>
    /// 请求自动购买
    /// </summary>
    public void ReqTrialAutoBuyBuff()
    {
        ReqTrialAutoBuyBuff msg = GetEmptyMsg<ReqTrialAutoBuyBuff>();

        SendMsg<ReqTrialAutoBuyBuff>(ref msg);
    }

    #endregion;

    #region 数据处理 ******************************************************************************************************************
    /// <summary>
    /// 将intVsInt的列表转成ItemInfo列表
    /// </summary>
    /// <param name="intVsIntList"></param>
    /// <returns></returns>
    public List<ItemInfo> TransformIntVsIntToItemInfo(List<IntVsInt> intVsIntList)
    {
        List<ItemInfo> itemList = new List<ItemInfo>();
        ItemInfo itemInfo = null;
        IntVsInt boxInfo = null;
        int count = intVsIntList.Count;
        for (int i = 0; i < count; i++)
        {
            boxInfo = intVsIntList[i];
            itemInfo = new ItemInfo();
            itemInfo.id = boxInfo.int1;
            itemInfo.num = boxInfo.int2;
            itemList.Add(itemInfo);
        }

        return itemList;
    }

    private void UpdateTrainSkipBoxInfo(List<IntVsInt> boxInfoList)
    {
        int count = boxInfoList.Count;
        if (count == 1)
        {
            // 替换
            count = trainSkipInfo.diamondBoxInfos.Count;
            IntVsInt boxInfo = null;
            for (int i = 0; i < count; i++)
            {
                boxInfo = trainSkipInfo.diamondBoxInfos[i];
                if (boxInfo.int1 == boxInfoList[0].int1)
                {
                    boxInfo.int2 = boxInfoList[0].int2;
                }
            }
        }
        else
        {
            trainSkipInfo.diamondBoxInfos.Clear();
            trainSkipInfo.diamondBoxInfos.AddRange(boxInfoList);
        }
    }
    /// <summary>
    /// 获得隐秘宝箱的最大开启次数
    /// </summary>
    public int GetSecretMaxOpenTimes()
    {
        // 1801002 付费宝箱的最大购买次数
        t_globalBean globalBean = ConfigBean.GetBean<t_globalBean, int>(1801002);
        int maxTimes = 0;
        if (globalBean != null)
            maxTimes = globalBean.t_int_param;

        return maxTimes;
    }
    /// <summary>
    /// 获得试炼的宠物状态信息
    /// </summary>
    /// <param name="petID"></param>
    /// <returns></returns>
    public TrialPetStatus GetTrialPetStatue(int petID)
    {
        if (trainInfo != null)
        {
            int count = trainInfo.trialInfo.petStatus.Count;
            TrialPetStatus petStatue;
            for (int i = 0; i < count; i++)
            {
                petStatue = trainInfo.trialInfo.petStatus[i];
                if (petStatue.petId == petID)
                {
                    return petStatue;
                }
            }
        }

        return null;
    }

    #endregion

    public override void ClearData()
    {
        base.ClearData();

        isOpenWindow = false;
        if (parentWindow != null)
            parentWindow.Close();
    }

}
