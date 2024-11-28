using Message.Pet;
using Message.KingFight;
using System.Collections.Generic;

public class StriveHegemongService : SingletonService<StriveHegemongService>
{
    public bool join;//是否报名
    public bool isjoin = true;//是否可以报名
    public int opencoutdown;//活动开启倒计时
    public bool bet;//是否下注
    public int open;//是否开启活动0未开启、1已开启、2已结束进八强赛
    public List<int> shangzhenList = new List<int>();
    public ResBetInfo betInfo { get; private set; }//下注信息
    public ResMainInfo maininfo { get; private set; }//主赛场信息
    public FightInfo fightInfo { get; private set; }//赛程战斗信息
    public ResEightMatchInfo eightMatchInfo { get; private set; }//八强匹配信息
    public ResTargetFightInfo targetFightInfo { get; private set; }//对手战斗信息
    public ResSetTeam teanInfo;
    public ResSelfInfo myRaceInfo { get; private set; }//我的赛程的信息
    public ResYesterday Yesterday;
    public StriveHegemongService()
    {
        if (GameManager.Singleton.IsDebug)
            InitTestData();
    }
    private void InitTestData()
    {
        maininfo = new ResMainInfo();
        join = true;
        
        for (int i = 0; i < 3; ++i)
        {
            MainInfo info = new MainInfo();
            info.name = "Hall Word";
            info.guildName = "code";
            info.rank = i;
            for (int j = 0; j < 3; ++j)
            {
                EquipedPetInfo equiped = new EquipedPetInfo();
                equiped.id = 100;
                equiped.star = 3;
                equiped.level = 50;
                equiped.color = 8;
                info.petBaseInfos.Add(equiped);
            }
            maininfo.playId = 1;
            maininfo.mainInfo.Add(info);
        }
        myRaceInfo = new ResSelfInfo();
        for (int i = 0; i < 5; ++i)
        {
            FightInfo fight = new FightInfo();
            fight.index = i;
            fight.result = 1;
            fight.boxstate = 0;
            fight.level = 10;
            fight.name = "你好世界";
            fight.time = 100;
            EquipedPetInfo equiped = new EquipedPetInfo();
            equiped.id = 100;
            equiped.star = 3;
            equiped.level = 50;
            equiped.color = 8;
            fight.petBaseInfo.Add(equiped);
            myRaceInfo.infos.Add(fight);
        }
        FightInfo fight1 = new FightInfo();
        fight1.index = 5;
        fight1.level = 10;
        fight1.name = "你好世界";
        fight1.time = 100;
        EquipedPetInfo equiped1 = new EquipedPetInfo();
        equiped1.id = 101;
        equiped1.star = 3;
        equiped1.level = 50;
        equiped1.color = 8;
        fight1.petBaseInfo.Add(equiped1);
        myRaceInfo.infos.Add(fight1);

        for (int i = 0; i < 10; ++i)
        {
            shangzhenList.Add(100);
        }
        betInfo = new ResBetInfo();
        //betInfo.roleId = 10025;
        //betInfo.type = 1;
        for (int i = 0; i < 8; ++i)
        {
            BetInfo info = new BetInfo();
            info.name = "你好世界";
            info.level = 20 + i;
            info.rank = i;
            info.title = 5;
            info.odds = 11.25f;
            info.roleId = 10020 + i;
            betInfo.info.Add(info);
        }
    }
    protected override void RegisterEventListener()
    {
        base.RegisterEventListener();
        GED.NED.addListener(ResOpenSignUp.MsgId,OnOpenSignUp);
        GED.NED.addListener(ResCountDown.MsgId,OnOpenCountDown);
        GED.NED.addListener(ResOpen.MsgId,OnOpen);
        GED.NED.addListener(ResBetInfo.MsgId, OnResBetInfo);
        GED.NED.addListener(ResMainInfo.MsgId,OnMainInfo);
        GED.NED.addListener(ResSetTeam.MsgId,OnTeamInfo);
        GED.NED.addListener(ResMatchTarget.MsgId, OnMatchTarget);
        GED.NED.addListener(ResTargetFightInfo.MsgId,OnTargetFightInfo);
        GED.NED.addListener(ResExchange.MsgId,OnExchange);
        GED.NED.addListener(ResCourseInfo.MsgId, OnCourseInfo);
        GED.NED.addListener(ResSelfInfo.MsgId, OnResSelfInfo);
        GED.NED.addListener(ResEightMatchInfo.MsgId, OnResEightMatchInfo);
        GED.NED.addListener(ResOver.MsgId, OnResOver);
        GED.NED.addListener(ResYesterday.MsgId, OnResYesterday);
        GED.NED.addListener(ResOpenBox.MsgId,OnResOpenBox);
    }
    protected override void UnRegisterEventListener()
    {
        base.UnRegisterEventListener();
        GED.NED.removeListener(ResOpenSignUp.MsgId,OnOpenSignUp);
        GED.NED.removeListener(ResCountDown.MsgId, OnOpenCountDown);
        GED.NED.removeListener(ResOpen.MsgId,OnOpen);
        GED.NED.removeListener(ResBetInfo.MsgId,OnResBetInfo);
        GED.NED.removeListener(ResMainInfo.MsgId,OnMainInfo);
        GED.NED.removeListener(ResSetTeam.MsgId,OnTeamInfo);
        GED.NED.removeListener(ResMatchTarget.MsgId,OnMatchTarget);
        GED.NED.removeListener(ResTargetFightInfo.MsgId,OnTargetFightInfo);
        GED.NED.removeListener(ResExchange.MsgId,OnExchange);
        GED.NED.removeListener(ResCourseInfo.MsgId, OnCourseInfo);
        GED.NED.removeListener(ResSelfInfo.MsgId, OnResSelfInfo);
        GED.NED.removeListener(ResEightMatchInfo.MsgId, OnResEightMatchInfo);
        GED.NED.removeListener(ResOver.MsgId, OnResOver);
        GED.NED.removeListener(ResYesterday.MsgId, OnResYesterday);
        GED.NED.removeListener(ResOpenBox.MsgId,OnResOpenBox);
    }
    /// <summary>
    /// 活动开启报名
    /// </summary>
    /// <param name="evt"></param>
    private void OnOpenSignUp(GameEvent evt)
    {
        ResOpenSignUp res = GetCurMsg<ResOpenSignUp>(evt.EventId);
        join = res.join;
        isjoin = true;
        if (!join)
        {
            GED.ED.dispatchEvent(EventID.OnStriveHegemongOpenSignUp);
        }
        if (join == false)
        {
            GED.ED.dispatchEvent(EventID.OnStriveHegemongBaoMingKuaiJie);
        }
    }
    //活动开启倒计时,收到这个信息后主城界面弹出提示标志
    private void OnOpenCountDown(GameEvent evt)
    {
        ResCountDown res = GetCurMsg<ResCountDown>(evt.EventId);
        opencoutdown = res.time;
        GED.ED.dispatchEvent(EventID.OnStriveHegemongGuanZhan);
    }
    //活动开启
    private void OnOpen(GameEvent evt)
    {
        ResOpen res = GetCurMsg<ResOpen>(evt.EventId);
        open = 1;
        GED.ED.dispatchEvent(EventID.OnStriveHegemongOpen);

    }
    //接收下注信息
    private void OnResBetInfo(GameEvent evt)
    {
        betInfo = GetCurMsg<ResBetInfo>(evt.EventId);
        bet = betInfo.hasRoleId();
        GED.ED.dispatchEvent(EventID.OnStriveHegemongBetInfo);
    }
    //主赛场信息
    private void OnMainInfo(GameEvent evt)
    {
        maininfo = GetCurMsg<ResMainInfo>(evt.EventId);
        join = maininfo.join;
        GED.ED.dispatchEvent(EventID.OnStriveHegemongOpenMainWindow);
    }
    //得到队伍信息
    private void OnTeamInfo(GameEvent evt)
    {
        shangzhenList.Clear();
        join = true;
        teanInfo = GetCurMsg<ResSetTeam>(evt.EventId);
        int lenght = teanInfo.team.Count;
        for (int i = 0; i < 10; ++i)
        {
            bool yongyou = false;
            for (int j = 0; j < lenght; ++j)
            {
                if (teanInfo.team[j].index == i)
                {
                    shangzhenList.Add(teanInfo.team[i].petId);
                    yongyou = true;
                    break;
                }
            }
            if (yongyou == false)
            {
                shangzhenList.Add(0);
            }
        }
    }
    /// <summary>
    /// 用于我的赛程，比赛开始后八强赛之前显示我的对战信息的
    /// 结果以及情况的消息
    /// </summary>
    /// <param name="evt"></param>
    private void OnMatchTarget(GameEvent evt)
    {
        ResMatchTarget res = GetCurMsg<ResMatchTarget>(evt.EventId);
        
    }
    /// <summary>
    /// 参赛选手信息
    /// </summary>
    /// <param name="gameEvent"></param>
    private void OnTargetFightInfo(GameEvent gameEvent)
    {
        targetFightInfo = GetCurMsg<ResTargetFightInfo>(gameEvent.EventId);
        targetFightInfo.baseInfo.Sort(StorPaml);
        open = 1;
        GED.ED.dispatchEvent(EventID.OnTargetFightInfo);
    }
    /// <summary>
    /// 兑换信息
    /// </summary>
    /// <param name="evt"></param>
    private void OnExchange(GameEvent evt)
    {
        ResExchange res = GetCurMsg<ResExchange>(evt.EventId);
        GED.ED.dispatchEvent(EventID.OnStriveHegemongExchange,res);
    }
    /// <summary>
    /// 开赛后，进入把之前随机刷新的对战信息
    /// 包含场次信息及string,直接显示string即可
    /// </summary>
    private void OnCourseInfo(GameEvent evt)
    {
        open = 1;
        ResCourseInfo res = GetCurMsg<ResCourseInfo>(evt.EventId);
        GED.ED.dispatchEvent(EventID.OnNextCompetitionOpen, res);
        
    }
    /// <summary>
    /// 我的赛程界面显示信息
    /// </summary>
    /// <param name="evt"></param>
    private void OnResSelfInfo(GameEvent evt)
    {
        GED.ED.dispatchEvent(EventID.OnStriveHegemongOpenMainWindow);
        join = true;
        myRaceInfo = GetCurMsg<ResSelfInfo>(evt.EventId);
        //myRaceInfo.infos.Sort(OnStorFightInfo);
        if (myRaceInfo.infos.Count == 0)
            open = 0;
        GED.ED.dispatchEvent(EventID.OnstriveHegemongMySelfInfo);
    }
    /// <summary>
    /// 八强赛以后的主赛场接收的消息
    /// </summary>
    /// <param name="evt"></param>
    private void OnResEightMatchInfo(GameEvent evt)
    {
        ResEightMatchInfo res = GetCurMsg<ResEightMatchInfo>(evt.EventId);
        if(eightMatchInfo == null)
            eightMatchInfo = new ResEightMatchInfo();
        for (int i = 0; i < res.info.Count; ++i)
        {
            eightMatchInfo.info.Add(res.info[i]);
        }
        open = 2;
        GED.ED.dispatchEvent(EventID.OnStriveHegemongEightGameOpen);
    }
    /// <summary>
    /// 昨日回顾八强信息，配合决赛结果信息科拼出昨日
    /// 回顾界面完整信息
    /// </summary>
    /// <param name="evt"></param>
    private void OnResYesterday(GameEvent evt)
    {
        Yesterday = GetCurMsg<ResYesterday>(evt.EventId);
        Yesterday.myinfos.Sort(OnStorFightInfo);
        GED.ED.dispatchEvent(EventID.OnStriveHegemongHuiGu);
    }

    /// <summary>
    /// 活动结束
    /// </summary>
    private void OnResOver(GameEvent evt)
    {
        open = 0;
        GED.ED.dispatchEvent(EventID.OnStriveHegemongEnd);
    }
    /// <summary>
    /// 打开宝箱
    /// </summary>
    /// <param name="evt"></param>
    private void OnResOpenBox(GameEvent evt)
    {
        ResOpenBox openBox = GetCurMsg<ResOpenBox>(evt.EventId);
        for (int i = 0; i < myRaceInfo.infos.Count; ++i)
        {
            if (myRaceInfo.infos[i].index == openBox.index)
            {
                if (myRaceInfo.infos[i].hasBoxstate())
                {
                    myRaceInfo.infos[i].boxstate = 0;
                    break;
                }
            }
        }
        GED.ED.dispatchEvent(EventID.OnStriveHegemongOpenBox);
    }
    /// <summary>
    /// 请求参赛信息
    /// </summary>
    public void OnReqFightInfo(long roleid)
    {
        if (roleid == -1)
            return;

        ReqFightInfo req = GetEmptyMsg<ReqFightInfo>();
        req.roleId = roleid;
        SendMsg(ref req);
    }
    /// <summary>
    /// 上传参赛宠物id列表
    /// </summary>
    /// <param name="petsid"></param>
    public void OnReqSetTeam(List<int> petsid)
    {
        ReqSetTeam req = GetEmptyMsg<ReqSetTeam>();
        shangzhenList.Clear();
        for (int i = 0; i < petsid.Count; ++i)
        {
            shangzhenList.Add(petsid[i]);
            TeamInfo info = new TeamInfo();
            if (petsid[i] == 0)
            {
                continue;
            }
            else
            {
                info.petId = petsid[i];
                info.index = i;
            }
            
            req.team.Add(info);
        }
        for (int i = 0; i < req.team.Count; ++i)
        {
            if (req.team[i].petId == 0)
            { 
                req.team.RemoveAt(i);
            }
        }   
        SendMsg(ref req);
        if (join == false)
        {
            join = true;
            GED.ED.dispatchEvent(EventID.OnStriveHegemongJoin);
        }
        GED.ED.dispatchEvent(EventID.OnStriveHegemongBaoMingKuaiJie);
    }
    /// <summary>
    /// 请求兑换道具
    /// </summary>
    public void OnReqExchange(int itemid)
    {
        ReqExchange req = GetEmptyMsg<ReqExchange>();
        req.id = itemid;
        SendMsg(ref req);
    }
    /// <summary>
    /// 请求下注信息
    /// </summary>
    public void OnReqBetInfo()
    {
        ReqBetInfo req = GetEmptyMsg<ReqBetInfo>();
        SendMsg(ref req);
    }
    /// <summary>
    /// 请求下注
    /// </summary>
    public void OnReqBet(int type,long roleid)
    {
        ReqBet req = GetEmptyMsg<ReqBet>();
        req.type = type;
        req.roleId = roleid;
        SendMsg(ref req);
    }
    /// <summary>
    /// 打开主赛场
    /// </summary>
    public void OnReqOpenMainInfo()
    {
        ReqOpenMainInfo req = GetEmptyMsg<ReqOpenMainInfo>();
        SendMsg(ref req);
        if (open == 2)
        {
            if (eightMatchInfo != null)
            {
                eightMatchInfo.info.Clear();
            }
        }
    }
    /// <summary>
    /// 关闭主赛场
    /// </summary>
    public void OnReqCloseMainInfo()
    {
        ReqCloseMainInfo req = GetEmptyMsg<ReqCloseMainInfo>();
        SendMsg(ref req);
    }
    /// <summary>
    /// 打开我的赛场
    /// </summary>
    public void OnReqOpenSelfInfo()
    {
        ReqOpenSelfInfo req = GetEmptyMsg<ReqOpenSelfInfo>();
        SendMsg(ref req);
    }
    /// <summary>
    /// 关闭我的赛程界面
    /// </summary>
    public void OnReqCloseSelfInfo()
    {
        ReqCloseSelfInfo req = GetEmptyMsg<ReqCloseSelfInfo>();
        SendMsg(ref req);
    }
    /// <summary>
    /// 请求昨日赛程
    /// </summary>
    public void OnReqYesterday()
    {
        ReqYesterday req = GetEmptyMsg<ReqYesterday>();
        SendMsg(ref req);
    }
    private int OnStorFightInfo(FightInfo a, FightInfo b)
    {
        int resA = 0;
        int resB = 0;

        if (a.index > b.index)
            resA += 1000;
        else if (a.index < b.index)
            resB += 1000;

        if (resA > resB)
            return 1;
        else if (resA == resB)
            return 0;
        else
            return -1;
    }
    //参赛选手队伍出场信息排序
    private int StorPaml(BaseInfo a,BaseInfo b)
    {
        int resA = 0;
        int resB = 0;

        if (a.index > b.index)
            resA += 1000;
        else if (a.index < b.index)
            resB += 1000;
        if (resA > resB)
            return -1;
        else if (resA == resB)
            return 1;
        else
            return 1;
    }
    /// <summary>
    /// 请求打开宝箱
    /// </summary>
    public void OnResOpenBox(int index,int state)
    {
        ResOpenBox res = GetEmptyMsg<ResOpenBox>();
        res.index = index;
        res.state = state;
        SendMsg(ref res);
    }
}
