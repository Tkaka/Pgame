using Message.DrawCard;
using Data.Beans;
using System.Collections.Generic;
using Message.Pet;

public class DrawCardService : SingletonService<DrawCardService>
{
    public ResDrawInfo ResDrawInfo { get; private set; }
    public List<DrawInfo> infos = new List<DrawInfo>();

    public void ResResDrawInfo()
    {
        if (GameManager.Singleton.IsDebug)
            InitTestData();
    }
    protected override void RegisterEventListener()
    {
        base.RegisterEventListener();
        GED.NED.addListener(ResDrawInfo.MsgId, OnDrawCard);
        GED.NED.addListener(ResDraw.MsgId, OnDraw);
        GED.NED.addListener(ResAcross.MsgId, OnResAcross);
    }
    protected override void UnRegisterEventListener()
    {
        base.UnRegisterEventListener();
        GED.NED.removeListener(ResDrawInfo.MsgId, OnDrawCard);
        GED.NED.removeListener(ResDraw.MsgId, OnDraw);
        GED.NED.removeListener(ResAcross.MsgId, OnResAcross);
    }
    /// <summary>
    /// 测试数据
    /// </summary>
    private void InitTestData()
    {
        
    }
    private void OnDrawCard(GameEvent evt)
    {
        ResDrawInfo = GetCurMsg<ResDrawInfo>(evt.EventId);
        if (ResDrawInfo != null && ResDrawInfo.drawInfo != null)
        {
            infos.Clear();
            for (int i = 0; i < ResDrawInfo.drawInfo.Count; ++i)
                infos.Add(ResDrawInfo.drawInfo[i]);
        }
    }
    /// <summary>
    /// 返回奖励列表
    /// </summary>
    /// <returns></returns>
    private List<DrawInfo> GetDrawCardInfos()
    { return infos; }
    /// <summary>
    /// 得到抽奖信息
    /// </summary>
    /// <param name="evt"></param>
    private void OnDraw(GameEvent evt)
    {
        ResDraw res = GetCurMsg<ResDraw>(evt.EventId);
        DrawInfo draw = res.info;
        for (int i = 0; i < infos.Count; ++i)
        {
            if (draw.type == infos[i].type)
            {
                if (draw.numInfo.num == infos[i].numInfo.num)
                {
                    infos[i] = draw;
                }
            }
        }
        for (int i = 0; i < res.items.Count; ++i)
        {
            t_itemBean itemBean = ConfigBean.GetBean<t_itemBean,int>(res.items[i].id);
            if (itemBean != null)
            {
                if (itemBean.t_type == 23)
                {
                    string[] pet = GTools.splitString(itemBean.t_value, ';');
                    int petId = int.Parse(pet[0]);
                    PetInfo petInfo = PetService.Singleton.GetPetInfo(petId);
                    if (petInfo == null)
                    {
                        PetService.Singleton.yongyou = false;
                    }
                    else
                    {
                        PetService.Singleton.yongyou = true;
                    }
                }
            }
        }
        GED.ED.dispatchEvent(EventID.OnResDrawCard, res);
    }
    private void OnResAcross(GameEvent evt)
    {
        ResAcross res = GetCurMsg<ResAcross>(evt.EventId);
        GED.ED.dispatchEvent(EventID.OnResAcross);//接收到该id下信息后重置免费
    }
   
    /// <summary>
    /// 获得金币十连抽信息
    /// </summary>
    /// <returns></returns>
    public NumInfo GetJinBiShiLianChouInfo()
    { return GetChouXiangXinXi(1, 10); }
    /// <summary>
    /// 获得金币100连抽
    /// </summary>
    /// <returns></returns>
    public NumInfo GetJinBiYiBanLianChou()
    { return GetChouXiangXinXi(1, 100); }
    /// <summary>
    /// 获得金币单次抽卡
    /// </summary>
    /// <returns></returns>
    public NumInfo GetJinBiDanChou()
    { return GetChouXiangXinXi(1,1); }
    /// <summary>
    /// 获得钻石单次抽卡
    /// </summary>
    /// <returns></returns>
    public NumInfo GetZuanShiDanChou()
    { return GetChouXiangXinXi(2,1); }
    public NumInfo GetZuanShiShiLianChou()
    { return GetChouXiangXinXi(2,10); }
    /// <summary>
    /// 获得获得抽卡信息
    /// </summary>
    /// <param name="type"></param>
    /// <param name="num"></param>
    /// <returns></returns>
    public NumInfo GetChouXiangXinXi(int type,int num)
    {
        for (int i = 0; i < infos.Count; ++i)
        {
            if (type == infos[i].type)
            {
                if (num == infos[i].numInfo.num)
                { return infos[i].numInfo; }
            }
        }
        return null;
    }
    public DrawInfo OnGetDrawInfo(int type,int num)
    {
        for (int i = 0; i < infos.Count; ++i)
        {
            if (type == infos[i].type)
            {
                if (num == infos[i].numInfo.num)
                    return infos[i];
            }
        }
        return null;
    }
    /// <summary>
    /// 请求抽奖
    /// </summary>
    public void OnReqDraw(int type, int num, bool free, bool useticket)
    {
        ReqDraw req = GetEmptyMsg<ReqDraw>();
        req.type = type;
        req.num = num;
        req.free = free;
        req.useTicket = useticket;
        SendMsg(ref req);
    }
    public override void ClearData()
    {
        if (infos.Count > 0)
            infos.Clear();
        ResDrawInfo = null;
        base.ClearData();
    }
}

