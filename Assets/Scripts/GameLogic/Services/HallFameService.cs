using Message.Bag;
using Message.Team;
using System.Collections.Generic;

public class HallFameService : SingletonService<HallFameService>
{
    private Dictionary<int, HofItem> hoflist = new Dictionary<int, HofItem>();

    public HallFameService()
    {
        if (GameManager.Singleton.IsDebug)
            InitTestData();
    }
    protected override void RegisterEventListener()
    {
        base.RegisterEventListener();
        GED.NED.addListener(ResHofInfo.MsgId, OnAcquireHallFameInfos);
        GED.NED.addListener(ResHofAddExp.MsgId,OnHallFameInfo);
        GED.NED.addListener(ResHofColorUp.MsgId, OnColorUp);
    }
    protected override void UnRegisterEventListener()
    {
        base.UnRegisterEventListener();
        GED.NED.removeListener(ResHofInfo.MsgId, OnAcquireHallFameInfos);
        GED.NED.removeListener(ResHofAddExp.MsgId,OnHallFameInfo);
        GED.NED.removeListener(ResHofColorUp.MsgId,OnColorUp);
    }
    /// <summary>
    /// 获得名人堂信息数据
    /// </summary>
    /// <param name="evt"></param>
    private void OnAcquireHallFameInfos(GameEvent evt)
    {
        ResHofInfo res = GetCurMsg<ResHofInfo>(evt.EventId);
        if (hoflist != null && res.hofItems != null)
        {
            foreach (HofItem item in res.hofItems)
            {
                if (hoflist.ContainsKey(item.petId))
                    hoflist[item.petId] = item;
                else
                    hoflist.Add(item.petId,item);
            }
        }
        GED.ED.dispatchEvent(EventID.OnAllPriority,res.totalPriority);
    }
    
    /// <summary>
    /// 获取名人堂经验增加信息
    /// </summary>
    /// <param name="evt"></param>
    private void OnHallFameInfo(GameEvent evt)
    {
        ResHofAddExp hofItem = GetCurMsg<ResHofAddExp>(evt.EventId);
        if (hoflist.ContainsKey(hofItem.hofItem.petId))
            hoflist[hofItem.hofItem.petId] = hofItem.hofItem;
        else
            hoflist.Add(hofItem.hofItem.petId, hofItem.hofItem  );
        GED.ED.dispatchEvent(EventID.OnResHofSingleInfo);
    }
    /// <summary>
    /// 获取名人堂升品信息
    /// </summary>
    /// <param name="evt"></param>
    private void OnColorUp(GameEvent evt)
    {
        ResHofColorUp res = GetCurMsg<ResHofColorUp>(evt.EventId);
        if (hoflist.ContainsKey(res.hofItem.petId))
            hoflist[res.hofItem.petId] = res.hofItem;
        else
            hoflist.Add(res.hofItem.petId, res.hofItem);
       
        TwoParam<HofItem, List<ItemInfo>> twoParam = new TwoParam<HofItem, List<ItemInfo>>();
        twoParam.value1 = res.hofItem;
        twoParam.value2 = res.itemInfos;
        GED.ED.dispatchEvent(EventID.OnItemList, twoParam);
        GED.ED.dispatchEvent(EventID.OnResHofSingleInfo, res.itemInfos);
    }
    /// <summary>
    /// 获取宠物的名人堂信息
    /// </summary>
    /// <param name="petid"></param>
    /// <returns></returns>
    public HofItem GetHofItem(int petid)
    {
        if (hoflist.ContainsKey(petid))
            return hoflist[petid];
        else
            return null;
    }
    /// <summary>
    /// 请求增加经验
    /// </summary>
    public void OnReqHofAddExp(int teamid,int petid, int itemid,int number)
    {
        ReqHofAddExp addExp = GetEmptyMsg<ReqHofAddExp>();
        addExp.teamId = teamid;
        addExp.petId = petid;
        addExp.gridId = BagService.Singleton.GetGridInfo(itemid).id;
        addExp.number = number;
        SendMsg(ref addExp);
    }
    /// <summary>
    /// 请求名人堂升品
    /// </summary>
    public void OnReqHofColorUp(int petid)
    {
        ReqHofColorUp reqHofColorUp = GetEmptyMsg<ReqHofColorUp>();
        reqHofColorUp.petId = petid;
        SendMsg(ref reqHofColorUp);
    }
    /// <summary>
    /// 请求获取名人堂信息
    /// </summary>
    /// <param name="petid"></param>
    public void OnReqHofInfo()
    {
        ReqHofInfo reqHof = GetEmptyMsg<ReqHofInfo>();
        SendMsg(ref reqHof);
    }
    /// <summary>
    /// 测试数据
    /// </summary>
    private void InitTestData()
    { }
    public override void ClearData()
    {
        if (hoflist.Count > 0)
            hoflist.Clear();
        base.ClearData();
    }
}
