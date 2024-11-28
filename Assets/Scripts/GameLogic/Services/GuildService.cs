using Data.Beans;
using FairyGUI;
using Message.Guild;
using System.Collections.Generic;

public class GuildService : SingletonService<GuildService>
{
    private GuildInfo m_guildInfo;      //公会信息

    private List<Member> m_memberList;  //成员列表

    private Dictionary<long, Applyer> m_applyerDic = new Dictionary<long, Applyer>();       //申请者列表

    private Dictionary<EAuthority, int> m_authorityDic = new Dictionary<EAuthority, int>();  //权限

    private List<GuildListInfo> m_guildList;

    private int m_rewardState = 0;  //（0 不能领 1可领 2已领）

    private long m_lastZhaoMuTime;  //上次招募时间

    private bool m_forHelpCooling = false;   //求助是否冷却中

    public enum ELimitType
    {
        ALL,        // 任何人
        APPLY,      // 申请
        FORBID,     // 禁止
    }

    public enum EGuildType
    {
        RELAXATION, // 休闲
        ATHLETICS,  // 竞技
    }

    public enum EJobType
    {
        Chair_Man     = 1,       // 帮主
        Deputy_Leader = 2,       // 副帮主
        Elite         =3,        // 精英
        Normal        =4,        // 帮众
    }

    public enum EAuthority
    {
        Nothing = 0,                 // 什么都不能做
        Break = 1,                   // 解散社团
        Change_Name = 2,             // 修改名字
        Change_Badge = 3,            // 修改公会徽章
        Change_Type = 4,             // 修改公会类型
        Change_Notice = 5,           // 修改公告
        Set_Condition = 6,           // 招募限制设置
        Invite_Member = 7,           // 招募社员
        Check_ApplyList = 8,         // 管理申请者列表
        Send_Mail = 9,               // 发放公会邮件
        Change_ChairMan = 10,        // 修改帮主
        Change_DeputyLeader = 11,    // 修改副帮主
        Change_Elite = 12,           // 修改长老
        Quit = 13,                   // 请离帮派
    }

    public enum EDonateType
    {
        Begin,
        /** 公会经验 */
        GuildExp,
        /** 公会成员数量 */
        GuildMemberNum,
        /** 工会精英数量 */
        GuildEliteNum,
        /** 红包金币 */
        GoldHongbao,
        /** 工会钻石 */
        DiamondHongbao,
        /** 神器之源 */
        GodWeaponHongbao,

        End,
    }

    public enum EDonateBigType
    {
        Guild,      // 社团
        Lib,        // 社团实验室
        Play,       // 玩法
        Hongbao,    // 红包
    }

    protected override void RegisterEventListener()
    {
        base.RegisterEventListener();
        GED.NED.addListener(ResGuildInfo.MsgId, _ResGuildInfo);
        GED.NED.addListener(ResChangeName.MsgId, _ResChangeName);
        GED.NED.addListener(ResMemberInfo.MsgId, _ResMemberInfo);
        GED.NED.addListener(ResChangeBadge.MsgId, _ResChangeBadge);
        GED.NED.addListener(ResChangeGuildType.MsgId, _ResChangeGuildType);
        GED.NED.addListener(ResChangeNotice.MsgId, _ResChangeNotice);
        GED.NED.addListener(ResApplyerList.MsgId, _ResApplyerList);
        GED.NED.addListener(ResChangeLimit.MsgId, _ResChangeLimit);
        GED.NED.addListener(ResGuildList.MsgId, _ResGuildList);
        GED.NED.addListener(ResFindGuildByName.MsgId, _ResFindGuildByName);
        GED.NED.addListener(ResGuildIdChange.MsgId, _ResGuildIdChange);
        GED.NED.addListener(ResGuildLog.MsgId, _ResGuildLog);
        GED.NED.addListener(ResRewardState.MsgId, _ResRewardState);
        GED.NED.addListener(ResEMailNumChange.MsgId, _ResEMailNumChange);
        //2018/5/17
        GED.NED.addListener(ResDonate.MsgId, _ResDonate);
        GED.NED.addListener(ResQuickRoleInfo.MsgId, _ResQuickRoleInfo);
        GED.NED.addListener(ResExpHomeRoleList.MsgId, _ResExpHomeRoleList);
        GED.NED.addListener(ResQuickRole.MsgId, _ResQuickRole);
        GED.NED.addListener(ResSetExpPet.MsgId, _ResSetExpPet);
        GED.NED.addListener(ResBuyPos.MsgId, _ResBuyPos);
        GED.NED.addListener(ResOpenExpHome.MsgId, _ResOpenExpHome);
        GED.NED.addListener(ResWish.MsgId, _ResWish);
        GED.NED.addListener(ResWishInfo.MsgId, _ResWishInfo);
        //红包
        GED.NED.addListener(ResOpenHongbaoPage.MsgId, OnResOpenHongbaoPage);
        GED.NED.addListener(ResSendHongbaoNum.MsgId, OnFaHongBaoNumChange);
        GED.NED.addListener(ResHongbaoRank.MsgId, OnResHongbaoRank);
        GED.NED.addListener(ResHongbaoList.MsgId, OnHongBaoLieBiao);
        GED.NED.addListener(ResHasHongbao.MsgId, OnYouHongBao);
        GED.NED.addListener(ResReceiveHongbao.MsgId, OnResReceiveHongbao);
        GED.NED.addListener(ResGuildHongbaoRefresh.MsgId,OnResGuildHongbaoRefresh);
    }

    protected override void UnRegisterEventListener()
    {
        base.UnRegisterEventListener();
        GED.NED.removeListener(ResGuildInfo.MsgId, _ResGuildInfo);
        GED.NED.removeListener(ResChangeName.MsgId, _ResChangeName);
        GED.NED.removeListener(ResMemberInfo.MsgId, _ResMemberInfo);
        GED.NED.removeListener(ResChangeBadge.MsgId, _ResChangeBadge);
        GED.NED.removeListener(ResChangeGuildType.MsgId, _ResChangeGuildType);
        GED.NED.removeListener(ResChangeNotice.MsgId, _ResChangeNotice);
        GED.NED.removeListener(ResApplyerList.MsgId, _ResApplyerList);
        GED.NED.removeListener(ResChangeLimit.MsgId, _ResChangeLimit);
        GED.NED.removeListener(ResGuildList.MsgId, _ResGuildList);
        GED.NED.removeListener(ResFindGuildByName.MsgId, _ResFindGuildByName);
        GED.NED.removeListener(ResGuildIdChange.MsgId, _ResGuildIdChange);
        GED.NED.removeListener(ResGuildLog.MsgId, _ResGuildLog);
        GED.NED.removeListener(ResRewardState.MsgId, _ResRewardState);
        GED.NED.removeListener(ResEMailNumChange.MsgId, _ResEMailNumChange);
        GED.NED.removeListener(ResDonate.MsgId, _ResDonate);
        GED.NED.removeListener(ResQuickRoleInfo.MsgId, _ResQuickRoleInfo);
        GED.NED.removeListener(ResExpHomeRoleList.MsgId, _ResExpHomeRoleList);
        GED.NED.removeListener(ResQuickRole.MsgId, _ResQuickRole);
        GED.NED.removeListener(ResSetExpPet.MsgId, _ResSetExpPet);
        GED.NED.removeListener(ResBuyPos.MsgId, _ResBuyPos);
        GED.NED.removeListener(ResOpenExpHome.MsgId, _ResOpenExpHome);
        GED.NED.removeListener(ResWish.MsgId, _ResWish);
        GED.NED.removeListener(ResWishInfo.MsgId, _ResWishInfo);
        GED.NED.removeListener(ResOpenHongbaoPage.MsgId, OnResOpenHongbaoPage);
        GED.NED.removeListener(ResSendHongbaoNum.MsgId, OnFaHongBaoNumChange);
        GED.NED.removeListener(ResHongbaoRank.MsgId, OnResHongbaoRank);
        GED.NED.removeListener(ResHongbaoList.MsgId, OnHongBaoLieBiao);
        GED.NED.removeListener(ResHasHongbao.MsgId, OnYouHongBao);
        GED.NED.removeListener(ResReceiveHongbao.MsgId, OnResReceiveHongbao);
        GED.NED.removeListener(ResGuildHongbaoRefresh.MsgId, OnResGuildHongbaoRefresh);
    }

    public override void ClearData()
    {
        base.ClearData();

    }

    //获得工会信息
    public GuildInfo GetGuildInfo()
    {
        return m_guildInfo;
    }

    //公会列表
    public List<GuildListInfo> GetGuildList()
    {
        return m_guildList;
    }

    public bool GetForHelpIsCooling()
    {
        return m_forHelpCooling;
    }

    public void SetForHelpCooling()
    {
        m_forHelpCooling = true;
        int time = 30 * 60;
        t_globalBean gBean = ConfigBean.GetBean<t_globalBean, int>(1602006);
        if (gBean != null)
            time = gBean.t_int_param * 60;
        CoroutineManager.Singleton.delayedCall(time, () =>
        {
            m_forHelpCooling = false;
        });
    }

    public Dictionary<long, Applyer> GetApplyers()
    {
        return m_applyerDic;
    }

    public bool IsHaveAuthority(EAuthority authority)
    {
        if (m_authorityDic.ContainsKey(authority))
            return true;
        return false;
    }

    public List<Member> GetMembers()
    {
        return m_memberList;
    }

    //公会领奖状态
    public int GetRewardState()
    {
        return m_rewardState;
    }

    public string GetLastOnLineTime(long time)
    {
        if (time <= 0)
            return "在线";

        int disTime = (int)(TimeUtils.CalculateDelta(time) / 1000);
        int month = disTime / (60 * 60 * 24 * 30);
        if (month > 0)
        {
            if (month > 12)
                month = 12;
            return string.Format("{0}月之前", month);
        }

        int day = disTime / (60 * 60 * 24);
        if (day > 0)
            return string.Format("{0}天之前", day);

        int hour = disTime / (60 * 60);
        if (hour > 0)
            return string.Format("{0}小时之前", hour);

        int min = disTime / 60;
        if (min > 0)
            return string.Format("{0}分钟之前", min);
        else
            return "1分钟之前";
    }

    public string GetLimitTypeDes(int limitType)
    {
        string strType;
        if (limitType == 0)
            strType = "自由加入";
        else if (limitType == 1)
            strType = "申请加入";
        else
            strType = "禁止加入";
        return strType;
    }

    public string GetLimitLevelDes(int limitLevel)
    {
        string strLevel = "";
        if (limitLevel == 0)
            strLevel = "无限制";
        else if (limitLevel == -1)
            strLevel = "禁止加入";
        else
            strLevel = string.Format("{0}级", limitLevel);
        return strLevel;
    }

    //获得职业描述
    public string GetJobDes(int job)
    {
        switch ((EJobType)job)
        {
            case EJobType.Chair_Man:
                return "社长";
            case EJobType.Deputy_Leader:
                return "副社长";
            case EJobType.Elite:
                return "精英";
            case EJobType.Normal:
                return "社员";
            default:
                return "";
        }
    }

    private void _InitAuthority()
    {
        m_authorityDic.Clear();
        if (m_guildInfo == null)
            return;

        t_authorityBean authorityBean = ConfigBean.GetBean<t_authorityBean, int>(m_guildInfo.roleJob);
        if (authorityBean != null)
        {
            int[] authorityArr = GTools.splitStringToIntArray(authorityBean.t_authority_type, '+');
            if (authorityArr != null)
            {
                for (int i = 0; i < authorityArr.Length; i++)
                {
                    if (!m_authorityDic.ContainsKey((EAuthority)authorityArr[i]))
                        m_authorityDic.Add((EAuthority)authorityArr[i], authorityArr[i]);
                }
            }
        }

    }

    public long GetLastZhaoMuTime()
    {
        return m_lastZhaoMuTime;
    }

    public void SetLastZhaoMuTime(long time)
    {
        m_lastZhaoMuTime = time;
    }

    //刷新社长福利红点
    private void _RefreshRewardRed()
    {
        RedDotManager.Singleton.SetRedDotValue("Guild/Reward", m_rewardState == 1);
    }

    //刷新申请者红点
    private void _RefreshApplyerRed()
    {
        RedDotManager.Singleton.SetRedDotValue("Guild/btnHall/Check", m_applyerDic.Count > 0);
    }
    //---------------------------------------------------------------------------------消息
    //工会信息
    private void _ResGuildInfo(GameEvent evt)
    {
        ResGuildInfo msg = GetCurMsg<ResGuildInfo>(evt.EventId);
        m_guildInfo = msg.info;
        m_memberList = msg.info.members;
        _InitAuthority();
        GED.ED.dispatchEvent(EventID.GuildInfo);

        //切到工会场景
        if (!GameManager.Singleton.IsStateOf(GameState.Guild))
        {
            WinMgr.Singleton.CloseAll();
            SceneLoader.Singleton.nextState = GameState.Guild;
            SceneLoader.Singleton.sceneName = GSceneName.Guild;
            GameManager.Singleton.changeState(GameState.Loading);
        }
 

    }

    //改名结果
    private void _ResChangeName(GameEvent evt)
    {
        ResChangeName msg = GetCurMsg<ResChangeName>(evt.EventId);
        m_guildInfo.name = msg.name;
        GED.ED.dispatchEvent(EventID.GuildNameChange);
    }

    //成员信息
    private void _ResMemberInfo(GameEvent evt)
    {
        ResMemberInfo msg = GetCurMsg<ResMemberInfo>(evt.EventId);
        m_memberList = msg.members;
        GED.ED.dispatchEvent(EventID.GuildMemberInfoChange);
 
    }

    //改徽章
    private void _ResChangeBadge(GameEvent evt)
    {
        ResChangeBadge msg = GetCurMsg<ResChangeBadge>(evt.EventId);
        m_guildInfo.badge = msg.id;
        GED.ED.dispatchEvent(EventID.GuildBadgeChange);
    }

    //改帮会类型
    private void _ResChangeGuildType(GameEvent evt)
    {
        ResChangeGuildType msg = GetCurMsg<ResChangeGuildType>(evt.EventId);
        m_guildInfo.guildType = msg.id;
        GED.ED.dispatchEvent(EventID.GuildTypeChange);
    }

    //改公告
    private void _ResChangeNotice(GameEvent evt)
    {
        ResChangeNotice msg = GetCurMsg<ResChangeNotice>(evt.EventId);
        m_guildInfo.notice = msg.content;
        GED.ED.dispatchEvent(EventID.GuildNoticeChange);
    }

    //申请列表
    private void _ResApplyerList(GameEvent evt)
    {
        ResApplyerList msg = GetCurMsg<ResApplyerList>(evt.EventId);

        if (msg.operate == 2)
        {
            m_applyerDic.Clear();
        }
        else if (msg.operate == 1)
        {
            for (int i = 0; i < msg.applyer.Count; i++)
            {
                Applyer applyer = msg.applyer[i];
                if (m_applyerDic.ContainsKey(applyer.roleId))
                {
                    m_applyerDic.Remove(applyer.roleId);
                }
            }
        }
        else if (msg.operate == 0)
        {
            for (int i = 0; i < msg.applyer.Count; i++)
            {
                Applyer applyer = msg.applyer[i];
                if (m_applyerDic.ContainsKey(applyer.roleId))
                {
                    m_applyerDic[applyer.roleId] = applyer;
                }
                else
                {
                    m_applyerDic.Add(applyer.roleId, applyer);
                }
            }
        }

        //TODO抛事件刷新
        GED.ED.dispatchEvent(EventID.GuildApplyerInfo);

        _RefreshApplyerRed();

    }

    //限制设置
    private void _ResChangeLimit(GameEvent evt)
    {
        ResChangeLimit msg = GetCurMsg<ResChangeLimit>(evt.EventId);
        m_guildInfo.levelLimt = msg.limitLevel;
        m_guildInfo.limitType = msg.limitType;
        GED.ED.dispatchEvent(EventID.JoinLimitChange);

    }

    //帮会列表
    private void _ResGuildList(GameEvent evt)
    {
        ResGuildList msg = GetCurMsg<ResGuildList>(evt.EventId);

        GED.ED.dispatchEvent(EventID.GuildListInfo, msg);
    }

    //帮会查找结果
    private void _ResFindGuildByName(GameEvent evt)
    {
        ResFindGuildByName msg = GetCurMsg<ResFindGuildByName>(evt.EventId);
        GED.ED.dispatchEvent(EventID.GuildFindResult, msg.info);
    }

    //帮会改变
    private void _ResGuildIdChange(GameEvent evt)
    {
        ResGuildIdChange msg = GetCurMsg<ResGuildIdChange>(evt.EventId);
        RoleService.Singleton.ChangeGuildId(msg.id);
        GED.ED.dispatchEvent(EventID.GuildChange);
    }

    private void _ResGuildLog(GameEvent evt)
    {
        ResGuildLog msg = GetCurMsg<ResGuildLog>(evt.EventId);
        GED.ED.dispatchEvent(EventID.GuildLogRefresh, msg.logList);
    }

    //领奖状态
    private void _ResRewardState(GameEvent evt)
    {
        ResRewardState msg = GetCurMsg<ResRewardState>(evt.EventId);
        m_rewardState = msg.state;
        _RefreshRewardRed();
        GED.ED.dispatchEvent(EventID.GuildRewardStateChange);
    }

    private void _ResEMailNumChange(GameEvent evt)
    {
        ResEMailNumChange msg = GetCurMsg<ResEMailNumChange>(evt.EventId);
        m_guildInfo.mailNum = msg.sendNum;
        GED.ED.dispatchEvent(EventID.GuildMailNumChange);
    }


    //训练所加速目标信息
    private void _ResQuickRoleInfo(GameEvent evt)
    {
        //返回训练所角色的宠物信息
        ResQuickRoleInfo msg = GetCurMsg<ResQuickRoleInfo>(evt.EventId);
        GED.ED.dispatchEvent(EventID.OnGuildDrillRolePets,msg);
    }

    //训练所角色列表
    private void _ResExpHomeRoleList(GameEvent evt)
    {
        ResExpHomeRoleList msg = GetCurMsg<ResExpHomeRoleList>(evt.EventId);
        GED.ED.dispatchEvent(EventID.OnGuildDrillExpediteRole,msg);
    }
    //为玩家加速返回
    private void _ResQuickRole(GameEvent evt)
    {
        ResQuickRole msg = GetCurMsg<ResQuickRole>(evt.EventId);
        GED.ED.dispatchEvent(EventID.OnGuildDrillTaRenJiaSuFanHui,msg);
    }

    //设置加速宠物
    private void _ResSetExpPet(GameEvent evt)
    {
        ResSetExpPet msg = GetCurMsg<ResSetExpPet>(evt.EventId);
        GED.ED.dispatchEvent(EventID.OnGuildDrillSetPet,msg.info);
    }
    //购买加速位置
    private void _ResBuyPos(GameEvent evt)
    {
        ResBuyPos msg = GetCurMsg<ResBuyPos>(evt.EventId);
        GED.ED.dispatchEvent(EventID.OnGuildDrillOpenNewPlace,msg.id);
    }
    //初始化我的训练所
    private void _ResOpenExpHome(GameEvent evt)
    {
        ResOpenExpHome msg = GetCurMsg<ResOpenExpHome>(evt.EventId);
        GED.ED.dispatchEvent(EventID.OnGuildDrillReceiveData,msg);
        GED.ED.dispatchEvent(EventID.OnGuildDrillOpenMainWin);
    }
    //许愿的道具ID
    private void _ResWish(GameEvent evt)
    {
        ResWish msg = GetCurMsg<ResWish>(evt.EventId);
        GED.ED.dispatchEvent(EventID.GuildWishResult, msg.itemId);
    }

    //许愿信息
    private void _ResWishInfo(GameEvent evt)
    {
        ResWishInfo msg = GetCurMsg<ResWishInfo>(evt.EventId);
        GED.ED.dispatchEvent(EventID.GuildWishInfo, msg);
    }

    //捐献结果
    private void _ResDonate(GameEvent evt)
    {
        ResDonate msg = GetCurMsg<ResDonate>(evt.EventId);
        if (msg.bigType == (int)EDonateBigType.Guild)
        {
            if (msg.donateInfos.Count > 0)
            {
                if (msg.donateInfos[0].target == (int)EDonateType.GuildExp)
                {
                    m_guildInfo.level = msg.donateInfos[0].level;
                }
            }
               
        }

        GED.ED.dispatchEvent(EventID.GuildDonateInfo, msg);
    }

    //--------------------------------------------------------------------------------请求
    //创建公会
    public void ReqCreate(string name, int badge, int type)
    {
        ReqCreate msg = GetEmptyMsg<ReqCreate>();
        msg.name = name;
        msg.badge = badge;
        msg.type = type;
        SendMsg<ReqCreate>(ref msg);
    }

    //改变公告
    public void ReqChangeNotice(string content)
    {
        ReqChangeNotice msg = GetEmptyMsg<ReqChangeNotice>();
        msg.content = content;
        SendMsg<ReqChangeNotice>(ref msg);
    }

    //改名
    public void ReqChangeName(string name)
    {
        ReqChangeName msg = GetEmptyMsg<ReqChangeName>();
        msg.name = name;
        SendMsg<ReqChangeName>(ref msg);
    }

    //解散
    public void ReqBreak()
    {
        ReqBreak msg = GetEmptyMsg<ReqBreak>();
        SendMsg<ReqBreak>(ref msg);
    }

    //改变徽章
    public void ReqChangeBadge(int badge)
    {
        ReqChangeBadge msg = GetEmptyMsg<ReqChangeBadge>();
        msg.badge = badge;
        SendMsg<ReqChangeBadge>(ref msg);
    }

    //改变社团类型
    public void ReqChangeGuildType(int type)
    {
        ReqChangeGuildType msg = GetEmptyMsg<ReqChangeGuildType>();
        msg.type = type;
        SendMsg<ReqChangeGuildType>(ref msg);
    }


    //发送邮件
    public void ReqSendMail(string content)
    {
        ReqSendMail msg = GetEmptyMsg<ReqSendMail>();
        msg.content = content;
        SendMsg<ReqSendMail>(ref msg);
    }

    //请求工会信息
    public void ReqGuildInfo()
    {
        ReqGuildInfo msg = GetEmptyMsg<ReqGuildInfo>();
        SendMsg<ReqGuildInfo>(ref msg);
    }

    //请求工会列表
    public void ReqGuildList(int page)
    {
        ReqGuildList msg = GetEmptyMsg<ReqGuildList>();
        msg.page = page;
        SendMsg<ReqGuildList>(ref msg);
    }

    //申请加入公会
    public void ReqApplyJoin(bool isQuick, long guildId = -1)
    {
        ReqApplyJoin msg = GetEmptyMsg<ReqApplyJoin>();
        msg.quick = isQuick;
        if (guildId != -1)
        {
            msg.guildId = guildId;
        }
        SendMsg<ReqApplyJoin>(ref msg);
    }

    //退出公会
    public void ReqExit()
    {
        ReqExit msg = GetEmptyMsg<ReqExit>();
        SendMsg<ReqExit>(ref msg);
    }

    //清空申请列表
    public void ReqClearApplyList()
    {
        ReqClearApplyList msg = GetEmptyMsg<ReqClearApplyList>();
        SendMsg<ReqClearApplyList>(ref msg);
    }

 
    public void ReqOperateApplyer(long roleId, bool agree)
    {
        ReqOperateApplyer msg = GetEmptyMsg<ReqOperateApplyer>();
        msg.agree = agree;
        msg.roleId = roleId;
        SendMsg<ReqOperateApplyer>(ref msg);
    }


    //请求成员列表
    public void ReqMemberList()
    {
        ReqMemberList msg = GetEmptyMsg<ReqMemberList>();
        SendMsg<ReqMemberList>(ref msg);
    }

    //查找工会
    public void ReqFindGuildByName(string name)
    {
        ReqFindGuildByName msg = GetEmptyMsg<ReqFindGuildByName>();
        msg.name = name;
        SendMsg<ReqFindGuildByName>(ref msg);
    }


    public void ReqOperateMember(long roleId, int oldJob, int newJob)
    {
        ReqOperateMember msg = GetEmptyMsg<ReqOperateMember>();
        msg.roleId = roleId;
        msg.oldJob = oldJob;
        msg.nweJob = newJob;   //-1为踢出
        SendMsg<ReqOperateMember>(ref msg);
    }

    //请求领奖
    public void ReqReward()
    {
        ReqReward msg = GetEmptyMsg<ReqReward>();
        SendMsg<ReqReward>(ref msg);
    }

    //设置加入限制
    //type :限制类型0任何人 1申请 2禁止
    //level:限制等级 0任何人 -1禁止所有 >0等级"
    public void ReqChangeLimit(int type, int level)
    {
        ReqChangeLimit msg = GetEmptyMsg<ReqChangeLimit>();
        msg.limitType = type;
        msg.limitLevel = level;
        SendMsg < ReqChangeLimit>(ref msg);
    }

    public void ReqGuildLog()
    {
        ReqGuildLog msg = GetEmptyMsg<ReqGuildLog>();
        SendMsg<ReqGuildLog>(ref msg);
    }

    //招募
    public void ReqZhaoMu()
    {
        ReqZhaoMu msg = GetEmptyMsg<ReqZhaoMu>();
        SendMsg<ReqZhaoMu>(ref msg);
        SetLastZhaoMuTime(TimeUtils.currentMilliseconds());
    }

    public void ReqDonate(int type, int target, bool isAll, int bigType)
    {
        ReqDonate msg = GetEmptyMsg<ReqDonate>();
        msg.type = type;
        msg.target = target;
        msg.all = isAll;
        msg.bigType = bigType;
        SendMsg<ReqDonate>(ref msg);
    }

    public void ReqOpenDonate(int target)
    {
        ReqOpenDonate msg = GetEmptyMsg<ReqOpenDonate>();
        msg.target = target;
        SendMsg<ReqOpenDonate>(ref msg);
    }
    //请求训练所加速目标信息
    public void ReqQuickRoleInfo(long roleId)
    {
        ReqQuickRoleInfo msg = GetEmptyMsg<ReqQuickRoleInfo>();
        msg.roleId = roleId;
        SendMsg<ReqQuickRoleInfo>(ref msg);

    }
    //请求训练所角色列表
    public void ReqExpHomeRoleList()
    {
        ReqExpHomeRoleList msg = GetEmptyMsg<ReqExpHomeRoleList>();
        SendMsg<ReqExpHomeRoleList>(ref msg);
    }
    //请求随机加速
    public void ReqRandomQuick(bool isOnce)
    {
        ReqRandomQuick msg = GetEmptyMsg<ReqRandomQuick>();
        msg.once = isOnce;
        SendMsg<ReqRandomQuick>(ref msg);
    }
    //请求为玩家加速
    public void ReqQuickRole(long roleId, int id)
    {
        ReqQuickRole msg = GetEmptyMsg<ReqQuickRole>();
        msg.roleId = roleId;
        msg.id = id;
        SendMsg<ReqQuickRole>(ref msg);
    }
    //请求设置加速宠物
    public void ReqSetExpPet(int id, int petId)
    {
        ReqSetExpPet msg = GetEmptyMsg<ReqSetExpPet>();
        msg.id = id;
        msg.petId = petId;
        SendMsg<ReqSetExpPet>(ref msg);
    }
    //请求购买加速位置
    public void ReqBuyPos(int pos)
    {
        ReqBuyPos msg = GetEmptyMsg<ReqBuyPos>();
        msg.id = pos;
        SendMsg<ReqBuyPos>(ref msg);
    }
    //请求训练所数据
    public void ReqOpenExpHome()
    {
        ReqOpenExpHome msg = GetEmptyMsg<ReqOpenExpHome>();
        SendMsg<ReqOpenExpHome>(ref msg);
    }

    public void ReqWish(int type, int itemId)
    {
        ReqWish msg = GetEmptyMsg<ReqWish>();
        msg.itemId = itemId;
        msg.type = type;
        SendMsg<ReqWish>(ref msg);
    }

    public void ReqPresent(long roleId)
    {
        ReqPresent msg = GetEmptyMsg<ReqPresent>();
        msg.roleId = roleId;
        SendMsg<ReqPresent>(ref msg);
    }

    public void ReqWishInfo()
    {
        ReqWishInfo msg = GetEmptyMsg<ReqWishInfo>();
        SendMsg<ReqWishInfo>(ref msg);
    }
    //红包系统
    /// <summary>
    /// 打开社团红包界面
    /// </summary>
    public void OnReqOpenHongbaoPage()
    {
        ReqOpenHongbaoPage res = GetEmptyMsg<ReqOpenHongbaoPage>();
        SendMsg(ref res);
    }
    //打开社团红包界面接收到的消息
    private void OnResOpenHongbaoPage(GameEvent evt)
    {
        ResOpenHongbaoPage res = GetCurMsg<ResOpenHongbaoPage>(evt.EventId);
        GED.ED.dispatchEvent(EventID.OnGuildRedSheTuan, res);//社团红包
        Logger.err("得到社团红包列表");
    }
    private void OnFaHongBaoNumChange(GameEvent evt)
    {
        ResSendHongbaoNum res = GetCurMsg<ResSendHongbaoNum>(evt.EventId);
        GED.ED.dispatchEvent(EventID.OnGuildHongBaoNumChange, res.num);
        Logger.err("发红包次数改变");
    }
    private void OnResHongbaoRank(GameEvent evt)
    {
        ResHongbaoRank res = GetCurMsg<ResHongbaoRank>(evt.EventId);
        GED.ED.dispatchEvent(EventID.OnGuildFaHongBaoPaiHang, res);
        Logger.err("收到服务器红包排行榜信息" + res.rank.Count);
    }
    private void OnHongBaoLieBiao(GameEvent evt)
    {
        ResHongbaoList res = GetCurMsg<ResHongbaoList>(evt.EventId);
        GED.ED.dispatchEvent(EventID.OnGuildHongBaoLieBiao, res);
        Logger.err("得到抢红包列表" + res.hongbaos.Count);
    }
    private void OnYouHongBao(GameEvent evt)
    {
        ResHasHongbao res = GetCurMsg<ResHasHongbao>(evt.EventId);
        GED.ED.dispatchEvent(EventID.OnGuildHasHongBao, res.id);
        Logger.err("收到有红包消息id" + res.id);
    }
    private void OnResReceiveHongbao(GameEvent evt)
    {
        ResReceiveHongbao res = GetCurMsg<ResReceiveHongbao>(evt.EventId);
        GED.ED.dispatchEvent(EventID.OnGuildHuoDeHongBao,res);
        Logger.err("收到抢红包获得消息");
    }
    private void OnResGuildHongbaoRefresh(GameEvent evt)
    {
        ResGuildHongbaoRefresh res = GetCurMsg<ResGuildHongbaoRefresh>(evt.EventId);
        Logger.err("收到工会红包刷新信息");
    }
    /// <summary>
    /// 抢红包
    /// </summary>
    /// <param name="id">红包id</param>
    public void OnQiangHongBao(long id)
    {
        ReqReceiveHongbao req = GetEmptyMsg<ReqReceiveHongbao>();
        req.id = id;
        SendMsg(ref req); Logger.err("请求抢红包---" + id);
    }
    public void OnFaHongBao(int id)
    {
        ReqSendHongbao req = GetEmptyMsg<ReqSendHongbao>();
        req.id = id;
        SendMsg(ref req);
        Logger.err("请求发红包" + id);
    }
    public void OnReqFaHongBaoPaiHang()
    {
        ReqHongbaoRank req = GetEmptyMsg<ReqHongbaoRank>();
        SendMsg(ref req); Logger.err("请求发红包排行");
    }
    public void ReqQiangHongBaoList()
    {
        ReqQiangHongBaoList req = GetEmptyMsg<ReqQiangHongBaoList>();
        SendMsg(ref req); Logger.err("请求抢红包列表");
    }
    public void OnReqHongbaoRank()
    {
        ReqHongbaoRank req = GetEmptyMsg<ReqHongbaoRank>();
        SendMsg(ref req);Logger.err("请求发红包排行");
    }
}