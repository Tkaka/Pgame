using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Message.Team;

public enum TongXiangMaterial
{
    QingTong = 1,           // 青铜
    BaiYing = 2,            // 白银
    DuJin = 3,              // 镀金
    LiuJin = 4,             // 鎏金
    ChunJin = 5,            // 纯金
    BaiJin = 6,             // 白金
}

public enum TongXiangRank
{
    XueTu = 0,              // 学徒
    GaoShou = 1,            // 高手
    DaShi = 2,              // 大师
    GongJian = 3,           // 工匠
}


public class TongXiangGuanServices : SingletonService<TongXiangGuanServices> {
    /// <summary>
    /// 铜像馆当前选中的页，从0开始
    /// </summary>
    public int curPage;
    /// <summary>
    /// 铜像购买界面当前选中的材质
    /// </summary>
    public int curMaterialIndex;
    /// <summary>
    /// 开放展厅拥有的铜像数量
    /// </summary>
    public List<int> zhanTingInfoList { get; protected set; }
    /// <summary>
    /// 当前铜像的信息
    /// </summary>
    public Statue statueInfo { get; protected set; }
    /// <summary>
    /// 当前展厅的信息
    /// </summary>
    public Exhibition exhibitionInfo { get; protected set; }
    /// <summary>
    /// 铜像总价值
    /// </summary>
    public int totalVlaue;

    private bool isOpenWindow = false;

    protected override void RegisterEventListener()
    {
        base.RegisterEventListener();

        GED.NED.addListener(ResExhibitionInfo.MsgId, OnResExhibitionInfo);
        GED.NED.addListener(ResExhibitionRoomInfo.MsgId, OnResExhibitionRoomInfo);
        GED.NED.addListener(ResStatueInfo.MsgId, OnResStatueInfo);
        GED.NED.addListener(ResStatueBuy.MsgId, OnResStatueBuy);
        GED.NED.addListener(ResExchangeStatue.MsgId, OnResExchangeStatue);
    }

    protected override void UnRegisterEventListener()
    {
        base.UnRegisterEventListener();

        GED.NED.removeListener(ResExhibitionInfo.MsgId, OnResExhibitionInfo);
        GED.NED.removeListener(ResExhibitionRoomInfo.MsgId, OnResExhibitionRoomInfo);
        GED.NED.removeListener(ResStatueInfo.MsgId, OnResStatueInfo);
        GED.NED.removeListener(ResStatueBuy.MsgId, OnResStatueBuy);
        GED.NED.removeListener(ResExchangeStatue.MsgId, OnResExchangeStatue);
    }


    #region   请求结果 ------------------------------------------------------------------------------
    /// <summary>
    /// 铜像馆的信息回调
    /// </summary>
    /// <param name="evt"></param>
    public void OnResExhibitionInfo(GameEvent evt)
    {
        ResExhibitionInfo msg = GetCurMsg<ResExhibitionInfo>(evt.EventId);
        zhanTingInfoList = msg.owns;
        totalVlaue = msg.value;

        if (isOpenWindow == false)
        {
            WinMgr.Singleton.Open<TongXiangGuanWindow>(null, UILayer.Popup);
            isOpenWindow = true;
        }

        GED.ED.dispatchEvent(EventID.OnResExhibitionInfo);
    }
    /// <summary>
    /// 展厅信息的回调
    /// </summary>
    /// <param name="evt"></param>
    public void OnResExhibitionRoomInfo(GameEvent evt)
    {
        ResExhibitionRoomInfo msg = GetCurMsg<ResExhibitionRoomInfo>(evt.EventId);
        exhibitionInfo = msg.exhibition;
        
        GED.ED.dispatchEvent(EventID.OnResExhibitionRoomInfo);
    }
    /// <summary>
    /// 铜像信息回调
    /// </summary>
    /// <param name="evt"></param>
    public void OnResStatueInfo(GameEvent evt)
    {
        ResStatueInfo msg = GetCurMsg<ResStatueInfo>(evt.EventId);

        statueInfo = msg.statue;
        GED.ED.dispatchEvent(EventID.OnResStatueInfo);
    }

    /// <summary>
    /// 购买铜像回调
    /// </summary>
    /// <param name="evt"></param>
    public void OnResStatueBuy(GameEvent evt)
    {
        ResStatueBuy msg = GetCurMsg<ResStatueBuy>(evt.EventId);

        statueInfo = msg.statue;
        exhibitionInfo = msg.exhibition;
        int cout = zhanTingInfoList.Count;
        if (msg.exhibition.exhibitionId <= cout)
        {
            zhanTingInfoList[msg.exhibition.exhibitionId - 1] = msg.exhibition.currentStatueIds.Count;
        }
        totalVlaue = msg.value;
        GED.ED.dispatchEvent(EventID.OnExhibitionInfoChange);
    }
    /// <summary>
    /// 交换铜像回调
    /// </summary>
    /// <param name="evt"></param>
    public void OnResExchangeStatue(GameEvent evt)
    {
        ResExchangeStatue msg = GetCurMsg<ResExchangeStatue>(evt.EventId);

        exhibitionInfo.exhibitionAtk = msg.exhibitionAtk;
        exhibitionInfo.exhibitionDef = msg.exhibitionDef;
        exhibitionInfo.exhibitionHp = msg.exhibitionHp;
        statueInfo.currentStatueId = msg.currentStatueId;

        GED.ED.dispatchEvent(EventID.OnExhibitionInfoChange);
    }

    #endregion

    #region   请求 ----------------------------------------------------------------------------------
    /// <summary>
    /// 请求铜像馆信息
    /// </summary>
    public void ReqExhibitionInfo()
    {
        ReqExhibitionInfo msg = GetEmptyMsg<ReqExhibitionInfo>();

        SendMsg<ReqExhibitionInfo>(ref msg);
    }
    /// <summary>
    /// 获得展厅信息
    /// </summary>
    /// <param name="zhanTingIndex">展厅的index,从1开始</param>
    public void ReqExhibitionRoomInfo(int zhanTingIndex)
    {
        ReqExhibitionRoomInfo msg = GetEmptyMsg<ReqExhibitionRoomInfo>();
        msg.exhibitionId = zhanTingIndex;

        SendMsg<ReqExhibitionRoomInfo>(ref msg);
    }
    /// <summary>
    /// 获得铜像信息
    /// </summary>
    /// <param name="petID">铜像对应的宠物ID</param>
    public void ReqStatueInfo(int petID, int zhanTingID)
    {
        ReqStatueInfo msg = GetEmptyMsg<ReqStatueInfo>();
        msg.statueId = petID;
        msg.exhibitionId = zhanTingID;

        SendMsg<ReqStatueInfo>(ref msg);
    }
    /// <summary>
    /// 请求购买铜像
    /// </summary>
    /// <param name="petID"></param>
    /// <param name="material"></param>
    /// <param name="rank"></param>
    public void ReqStatueBuy(int petID, int material, int rank, int zhanTingID)
    {
        ReqStatueBuy msg = GetEmptyMsg<ReqStatueBuy>();

        msg.statueId = UIUtils.GetStatueID(petID, material, rank);
        msg.exhibitionId = zhanTingID;
        SendMsg<ReqStatueBuy>(ref msg);
    }
    /// <summary>
    /// 请求铜像交换
    /// </summary>
    /// <param name="oldSatueID"></param>
    /// <param name="newStatueID"></param>
    public void ReqStatueExchange(int oldSatueID, int newStatueID, int zhanTingID)
    {
        ReqStatueExchange msg = GetEmptyMsg<ReqStatueExchange>();

        msg.oldStatueId = oldSatueID;
        msg.newStatueId = newStatueID;
        msg.exhibitionId = zhanTingID;

        SendMsg<ReqStatueExchange>(ref msg);
    }

    #endregion;

    #region   数据处理--------------------------------------------------------------------------------

    /// <summary>
    /// 获得当前展厅展示的铜像数量
    /// </summary>
    /// <param name="zhanTingIndex">从0开始</param>
    /// <returns></returns>
    public int GetZhanTingStatueNum(int zhanTingIndex)
    {
        if (zhanTingInfoList == null || zhanTingIndex >= zhanTingInfoList.Count)
            return 0;

        return zhanTingInfoList[zhanTingIndex];
    }
    /// <summary>
    /// 获得宠物id在铜像馆中的下标，没有就是-1
    /// </summary>
    /// <param name="petID"></param>
    /// <returns></returns>
    public int GetPetIDIndex(int petID)
    {
        if (exhibitionInfo != null)
        {
            int count = exhibitionInfo.currentStatueIds.Count;
            int tempID;
            for (int i = 0; i < count; i++)
            {
                tempID = exhibitionInfo.currentStatueIds[i];
                tempID = exhibitionInfo.currentStatueIds[i] / 100;
                if (tempID == petID)
                {
                    return i;
                }
            }
        }

        return -1;
    }

    #endregion;

    public override void ClearData()
    {
        base.ClearData();

        isOpenWindow = false;
    }
}
