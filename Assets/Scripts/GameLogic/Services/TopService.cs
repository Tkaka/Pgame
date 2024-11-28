using Message.Rank;
using System.Collections.Generic;
using Message.Arena;

public enum TopType
{
    /** 战斗力 */
    Fight_Power = 1,
    /** 关卡星星 */
    Mission_Star = 2,
    /** 名人堂 */
    Hall = 3,
    /** 格斗家 单个宝贝战斗力 */
    Role_Fight = 4,
    /** 社团 */
    Guild = 5,
    /** 争霸赛 */
    King_Fight = 6,
}

public class TopService : SingletonService<TopService>
{
    public int Fight_page { get; set; }//战力当前页
    public int Mission_page { get; set; }//关卡星当前页
    public int Hall_page { get; set; }//名人堂当前页
    public int Pet_page { get; set; }//宠物当前页
    public int Guild_page { get; set; }//社团当前页
    public int King_page { get; set; }//拳皇当前页
    public TopType topType = TopType.Fight_Power;//当前页签
    private Dictionary<int, RankData> TopInfo;//当前指向
    private Dictionary<int, RankData> zhanliDic = new Dictionary<int, RankData>();
    private Dictionary<int, RankData> guankaDic = new Dictionary<int, RankData>();
    private Dictionary<int, RankData> mingrentangDic = new Dictionary<int, RankData>();
    private Dictionary<int, RankData> chongwuDic = new Dictionary<int, RankData>();
    private Dictionary<int, RankData> quanhuangzhengbaDic = new Dictionary<int, RankData>();
    private Dictionary<int, GuildRankData> guildrankDic = new Dictionary<int, GuildRankData>();
    private RankData myzhanli;//我的战力
    private RankData myguanka;//我的关卡
    private RankData mymingrentang;//我的名人堂
    private RankData mypet;//我的宠物
    private GuildRankData myGuild;//我的社团
    private RankData myKing;//我的拳皇争霸排行榜
    public Petdata petdata;//单独宠物数据
    protected override void RegisterEventListener()
    {
        base.RegisterEventListener();
        GED.NED.addListener(ResRankData.MsgId, OnPaiHangBangList);
        GED.NED.addListener(ResGuildRankData.MsgId, OnGuildRank);
        GED.NED.addListener(ResPetData.MsgId, OnPetData);
        GED.NED.addListener(ResPetData.MsgId, OnOpenChongWuXiangQing);
    }
    protected override void UnRegisterEventListener()
    {
        base.UnRegisterEventListener();
        GED.NED.removeListener(ResRankData.MsgId, OnPaiHangBangList);
        GED.NED.removeListener(ResGuildRankData.MsgId, OnGuildRank);
        GED.NED.removeListener(ResPetData.MsgId, OnPetData);
        GED.NED.removeListener(ResPetData.MsgId, OnOpenChongWuXiangQing);
    }
    //收到排行榜数据
    private void OnPaiHangBangList(GameEvent evt)
    {
        ResRankData rankData = GetCurMsg<ResRankData>(evt.EventId);

        switch (rankData.rankType)
        {
            case (int)TopType.Fight_Power:
                {
                    if (rankData.data.Count > 0)
                        Fight_page += 1;
                    for (int i = 0; i < rankData.data.Count; ++i)
                    {
                        if (rankData.data[i].rank == 0)
                        { Fight_page = 0; }
                        if (zhanliDic.ContainsKey(rankData.data[i].rank))
                        { zhanliDic[rankData.data[i].rank] = rankData.data[i]; }
                        else
                        { zhanliDic.Add(rankData.data[i].rank, rankData.data[i]); }
                    }
                    if (rankData.hasMyRanke())
                    { myzhanli = rankData.myRanke; }
                } break;
            case (int)TopType.Mission_Star:
                {
                    if (rankData.data.Count > 0)
                        Mission_page += 1;
                    for (int i = 0; i < rankData.data.Count; ++i)
                    {
                        if (rankData.data[i].rank == 0)
                        { Mission_page = 0; }
                        if (guankaDic.ContainsKey(rankData.data[i].rank))
                        { guankaDic[rankData.data[i].rank] = rankData.data[i]; }
                        else
                        { guankaDic.Add(rankData.data[i].rank, rankData.data[i]); }
                    }
                    if (rankData.hasMyRanke())
                        myguanka = rankData.myRanke;
                } break;
            case (int)TopType.Hall:
                {
                    if (rankData.data.Count > 0)
                        Hall_page += 1;
                    for (int i = 0; i < rankData.data.Count; ++i)
                    {
                        if (rankData.data[i].rank == 0)
                        { Hall_page = 0; }
                        if (mingrentangDic.ContainsKey(rankData.data[i].rank))
                        { mingrentangDic[rankData.data[i].rank] = rankData.data[i]; }
                        else
                        { mingrentangDic.Add(rankData.data[i].rank, rankData.data[i]); }
                    }
                    if (rankData.hasMyRanke())
                        mymingrentang = rankData.myRanke;
                } break;
            case (int)TopType.Role_Fight:
                {
                    if (rankData.data.Count > 0)
                        Pet_page += 1;
                    for (int i = 0; i < rankData.data.Count; ++i)
                    {
                        if (rankData.data[i].rank == 0)
                        { Pet_page = 0; }
                        if (chongwuDic.ContainsKey(rankData.data[i].rank))
                        { chongwuDic[rankData.data[i].rank] = rankData.data[i]; }
                        else
                        { chongwuDic.Add(rankData.data[i].rank, rankData.data[i]); }
                    }
                    if (rankData.hasMyRanke())
                        mypet = rankData.myRanke;
                } break;
            case (int)TopType.King_Fight:
                {
                    if (rankData.data.Count > 0)
                        King_page += 1;
                    for (int i = 0; i < rankData.data.Count; ++i)
                    {
                        if (rankData.data[i].rank == 0)
                        { King_page = 0; }
                        if (quanhuangzhengbaDic.ContainsKey(rankData.data[i].rank))
                        { quanhuangzhengbaDic[rankData.data[i].rank] = rankData.data[i]; }
                        else
                        { quanhuangzhengbaDic.Add(rankData.data[i].rank, rankData.data[i]); }
                    }
                    if (rankData.hasMyRanke())
                        myKing = rankData.myRanke;
                } break;
        }
        topType = (TopType)rankData.rankType;
        GED.ED.dispatchEvent(EventID.OnTopDataChange);
    }
    //请求排行榜数据
    public void OnReqRankData(TopType type, int pageData)
    {
        //当前页为零表示切换了新页签
        topType = type;
        ReqRankData rankData = GetEmptyMsg<ReqRankData>();
        rankData.page = pageData;
        rankData.rankType = (int)type;
        SendMsg(ref rankData);
    }
    //得到社团排行榜
    private void OnGuildRank(GameEvent evt)
    {
        ResGuildRankData rankData = GetCurMsg<ResGuildRankData>(evt.EventId);
        if (rankData.data.Count > 0)
            Guild_page += 1;
        for (int i = 0; i < rankData.data.Count; ++i)
        {
            if (rankData.data[i].rank == 0)
            { Guild_page = 0; }
            if (guildrankDic.ContainsKey(rankData.data[i].rank))
                guildrankDic[rankData.data[i].rank] = rankData.data[i];
            else
                guildrankDic.Add(rankData.data[i].rank, rankData.data[i]);
        }
        myGuild = rankData.myGuild;
        GED.ED.dispatchEvent(EventID.OnTopDataChange);
    }
    //得到当前显示的列表对应角色信息
    public RankData OnGetRankData(int index)
    {
        if (TopInfo.ContainsKey(index))
        {
            return TopInfo[index];
        }
        return null;
    }
    private void OnPetData(GameEvent evt)
    {
        ResPetData resPet = GetCurMsg<ResPetData>(evt.EventId);
        petdata = resPet.data;
    }
    public Petdata GetPetdata()
    {
        return petdata;
    }
    public void OnReqPetData(int petId, long roleId)
    {
        ReqPetData req = GetEmptyMsg<ReqPetData>();
        req.petid = petId;
        req.roleid = roleId;
        SendMsg(ref req);
    }
    public void OnJueSeXiangQing(long roleID)
    {
        ReqSeeOther reqSee = GetEmptyMsg<ReqSeeOther>();
        reqSee.roleId = roleID;
        SendMsg(ref reqSee);
    }
    private void OnOpenChongWuXiangQing(GameEvent evt)
    {
        ResPetData petData = GetCurMsg<ResPetData>(evt.EventId);
        WinInfo info = new WinInfo();
        TwoParam<int, XiangQingType> two = new TwoParam<int, XiangQingType>();
        two.value1 = petData.data.petId;
        two.value2 = XiangQingType.PaiHangBang;
        info.param = two;
        WinMgr.Singleton.Open<ChongWuXiangQingWindow>(info,UILayer.Popup);
    }
    //得到角色列表
    public List<int> OnRoleIdInfo()
    {
        List<int> roles = new List<int>();
        if (topType == TopType.Fight_Power)
        {
            roles.AddRange(zhanliDic.Keys);
            TopInfo = zhanliDic;
        }
        else if (topType == TopType.Mission_Star)
        {
            roles.AddRange(guankaDic.Keys);
            TopInfo = guankaDic;
        }
        else if (topType == TopType.Hall)
        {
            roles.AddRange(mingrentangDic.Keys);
            TopInfo = mingrentangDic;
        }
        else if (topType == TopType.Role_Fight)
        {
            roles.AddRange(chongwuDic.Keys);
            TopInfo = chongwuDic;
        }
        else if (topType == TopType.King_Fight)
        {
            roles.AddRange(quanhuangzhengbaDic.Keys);
            TopInfo = quanhuangzhengbaDic;
        }
        else if (topType == TopType.Guild)
            roles.AddRange(guildrankDic.Keys);
        return roles;
    }
    /// <summary>
    /// 得到自己的排行信息
    /// </summary>
    /// <returns></returns>
    public RankData OnGetMyRankData()
    {
        switch (topType)
        {
            case TopType.Fight_Power:return myzhanli;
            case TopType.Mission_Star:return myguanka;
            case TopType.Hall:return mymingrentang;
            case TopType.Role_Fight:return mypet;
            case TopType.King_Fight: return myKing;
            default:return null;
        }
    }
    public GuildRankData OnGetMyGuildData()
    {
        return myGuild;
    }
    public GuildRankData GetGuildData(int index)
    {
        if (guildrankDic.ContainsKey(index))
            return guildrankDic[index];
        else
            return null;
    }
    
    public override void ClearData()
    {
        TopInfo = null;
        zhanliDic.Clear();
        guankaDic.Clear();
        mingrentangDic.Clear();
        chongwuDic.Clear();
        quanhuangzhengbaDic.Clear();
        guildrankDic.Clear();
        myzhanli = null;
        myguanka = null;
        mymingrentang = null;
        mypet = null;
        myGuild = null;
        myKing = null;
        petdata = null ;
        base.ClearData();
    }
}