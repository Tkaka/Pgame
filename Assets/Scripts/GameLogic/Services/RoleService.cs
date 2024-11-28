

using Message.Role;
using Data.Beans;

public class RoleService : SingletonService<RoleService>
{
    public enum ECurrencyType
    {
        /** 金币 */
        GOLD = -1,
        /** 钻石 */
        DIAMOND = -2,
        /** 红钻 */
        RED_DIAMOND = -3,
        /** 体力 */
        ENERGY=-4,
        /** 觉醒碎片 */
        AWAKEN_FRAGMENT= -5,
        /** 荣誉币 */
        HONOR_CURRENCY=-6,
        /** 试炼币 */
        TRAIL_CURRENCY=-7,
        /** 社团币 */
        CLUB_CURRENCY=-8,
        /** 训练师经验 */
        EXP=-9,
        /** 天赋点数*/
        TALENT = -10,
        /** 奥义精华*/
        AoyiJingHua = -11,
    }
    
    public ResRoleInfo RoleInfo { get; private set; }
    private DoActionInterval interval = new DoActionInterval();
    private const float beatOutTime = 60f;
    private long checkBeatTimer = -1;
    private int[] m_energyBuyPrice;


    public int ServerOpenDay { get; private set; }

    protected override void RegisterEventListener()
    {
        base.RegisterEventListener();
        GED.NED.addListener(ResRoleInfo.MsgId, OnResRoleInfo);
        GED.NED.addListener(ResCurrentTime.MsgId, OnServerTime);
        GED.NED.addListener(ServerHeartBeat.MsgId, OnServerHeartBeat);
        GED.NED.addListener(ClientHeartBeat.MsgId, OnClientHeartBeat);
        GED.NED.addListener(ResPrompt.MsgId, _ResPrompt);
        GED.NED.addListener(ResFightPowerChange.MsgId, _ResFightPowerChange);
        GED.NED.addListener(ResCurrencyChange.MsgId, _ResCurrencyChange);
        GED.NED.addListener(ResLevelUp.MsgId, _ResLevelUp);
        GED.NED.addListener(ResChangePrecedeValue.MsgId, OnPrecedeChange);
        GED.NED.addListener(ResSkillPointChange.MsgId, OnResSkillPointChange);
        GED.NED.addListener(ResResetExitGuildTime.MsgId, _ResResetExitGuildTime);
        GED.NED.addListener(ResOpenServerDays.MsgId, _ResOpenServerDays);
        GED.NED.addListener(ResEnergyBuyCount.MsgId, _ResEnergyBuyCount);
        GED.NED.addListener(ResModifyIcon.MsgId, OnResModifyIcon);
        GED.NED.addListener(ResModifyNickname.MsgId, OnResModifyNickname);
        GED.NED.addListener(ResSignIn.MsgId, OnResSignIn);
        GED.NED.addListener(ResSignInBox.MsgId, OnResSignInBox);
    }

    protected override void UnRegisterEventListener()
    {
        base.UnRegisterEventListener();
        GED.NED.removeListener(ResRoleInfo.MsgId, OnResRoleInfo);
        GED.NED.removeListener(ResCurrentTime.MsgId, OnServerTime);
        GED.NED.removeListener(ServerHeartBeat.MsgId, OnServerHeartBeat);
        GED.NED.removeListener(ClientHeartBeat.MsgId, OnClientHeartBeat);
        GED.NED.removeListener(ResPrompt.MsgId, _ResPrompt);
        GED.NED.removeListener(ResFightPowerChange.MsgId, _ResFightPowerChange);
        GED.NED.removeListener(ResCurrencyChange.MsgId, _ResCurrencyChange);
        GED.NED.removeListener(ResLevelUp.MsgId, _ResLevelUp);
        GED.NED.removeListener(ResChangePrecedeValue.MsgId, OnPrecedeChange);
        GED.NED.removeListener(ResSkillPointChange.MsgId, OnResSkillPointChange);
        GED.NED.removeListener(ResResetExitGuildTime.MsgId, _ResResetExitGuildTime);
        GED.NED.removeListener(ResOpenServerDays.MsgId, _ResOpenServerDays);
        GED.NED.removeListener(ResEnergyBuyCount.MsgId, _ResEnergyBuyCount);
    }

    private void OnPrecedeChange(GameEvent evt)
    {
        ResChangePrecedeValue msg = GetCurMsg<ResChangePrecedeValue>(evt.EventId);
        RoleInfo.roleInfo.precedeValue = msg.value;
    }

    /// <summary>
    /// 获取先手值
    /// </summary>
    /// <returns></returns>
    public int GetPrecedeVal()
    {
        if (RoleInfo != null && RoleInfo.roleInfo !=null)
            return RoleInfo.roleInfo.precedeValue;
        else
            return 0;
    }

    //得到玩家信息
    public RoleInfo GetRoleInfo()
    {
        if (RoleInfo != null)
            return RoleInfo.roleInfo;
        else
            return null;
    }

    //是否在退帮冷却中
    public bool IsExitGuildCooling()
    {
        return RoleInfo.roleInfo.exitGuildTime > 0;
    }

    //获得货币数量
    public long GetCurrencyNum(int currencyType)
    {
        long num = 0;
        switch ((ECurrencyType)currencyType)
        {
            case ECurrencyType.AWAKEN_FRAGMENT:
                num = RoleInfo.roleInfo.awakenFragment;
                break;
            case ECurrencyType.CLUB_CURRENCY:
                num = RoleInfo.roleInfo.clubCurrency;
                break;
            case ECurrencyType.DIAMOND:
                num = RoleInfo.roleInfo.damond;
                break;
            case ECurrencyType.ENERGY:
                num = RoleInfo.roleInfo.energy;
                break;
            case ECurrencyType.EXP:
                num = RoleInfo.roleInfo.curExp;
                break;
            case ECurrencyType.GOLD:
                num = RoleInfo.roleInfo.gold;
                break;
            case ECurrencyType.HONOR_CURRENCY:
                num = RoleInfo.roleInfo.honorCurrency;
                break;
            case ECurrencyType.TRAIL_CURRENCY:
                num = RoleInfo.roleInfo.trailCurrency;
                break;
            case ECurrencyType.TALENT:
                num = RoleInfo.roleInfo.talent;
                break;
            case ECurrencyType.AoyiJingHua:
                num = RoleInfo.roleInfo.aoyiJinghua;
                break;

        }

        return num;
    }

    //体力已购买次数改变
    private void _ResEnergyBuyCount(GameEvent evt)
    {
        ResEnergyBuyCount msg = GetCurMsg<ResEnergyBuyCount>(evt.EventId);
        RoleInfo.roleInfo.energyBuyCount =  msg.energyBuyCount;
        GED.ED.dispatchEvent(EventID.EnergyBuyCountChange);

    }

    private void OnResModifyIcon(GameEvent evt)
    {
        ResModifyIcon msg = GetCurMsg<ResModifyIcon>(evt.EventId);
        RoleInfo.roleInfo.headIconId = msg.iconId;
        GED.ED.dispatchEvent(EventID.OnResModifyIcon);
    }

    private void OnResModifyNickname(GameEvent evt)
    {
        ResModifyNickname msg = GetCurMsg<ResModifyNickname>(evt.EventId);
        RoleInfo.roleInfo.roleName = msg.nickname;
        RoleInfo.roleInfo.nicknameModifyCount++;
        GED.ED.dispatchEvent(EventID.OnResModifyNickname);
    }

    //服务器通知的提示
    private void _ResPrompt(GameEvent evt)
    {
        ResPrompt msg = GetCurMsg<ResPrompt>(evt.EventId);
        string str = "";
        if (msg.lanId == -1)
        {
            if (msg.content.Count > 0)
                str = msg.content[0];
        }
        else
        {
            t_languageBean lanBean = ConfigBean.GetBean<t_languageBean, int>(msg.lanId);
            if (lanBean != null)
            {
                str = lanBean.t_content;
                if (msg.content.Count > 0)
                {
                    str = string.Format(str, msg.content.ToArray());
                }
            }
            else
                str = string.Format("服务器通知不存在的语言包ID：" + msg.lanId);
        }

        switch (msg.type)
        {
            case 1:
                TipWindow.Singleton.ShowTip(str);
                break;
            case 2:
                AgainConfirmWindow.Singleton.TipOneButton(str);
                break;
            case 3:
                AgainConfirmWindow.Singleton.TipOneButton(str, () => {
                    MessageHandle.GetInstance().CloseSocket();
                    SceneLoader.Singleton.sceneName = null;
                    SceneLoader.Singleton.nextState = GameState.UpdateRes;
                    GameManager.Singleton.changeState(GameState.Loading);
                });

                break;
            default:
                TipWindow.Singleton.ShowTip(str);
                break;
        }
    }

    //主角战斗力改变
    private void _ResFightPowerChange(GameEvent evt)
    {
        ResFightPowerChange msg = GetCurMsg<ResFightPowerChange>(evt.EventId);
        if (RoleInfo != null)
        {
            RoleInfo.roleInfo.fightPower = msg.fightPower;
            GED.ED.dispatchEvent(EventID.RoleInfoChange);
        }
    }
    //技能点改变
    private void OnResSkillPointChange(GameEvent evt)
    {
        ResSkillPointChange res = GetCurMsg<ResSkillPointChange>(evt.EventId);
        RoleInfo.roleInfo.skillPoints = res.num;
        if (res.buy)
        {
            RoleInfo.roleInfo.skillPointsBuyCount += 1;
        }
        GED.ED.dispatchEvent(EventID.OnJiNengDianGouMai,res);
    }
    //货币改变
    private void _ResCurrencyChange(GameEvent evt)
    {
        ResCurrencyChange msg = GetCurMsg<ResCurrencyChange>(evt.EventId);
        switch ((ECurrencyType)msg.type)
        {
            case ECurrencyType.AWAKEN_FRAGMENT:
                RoleInfo.roleInfo.awakenFragment = (int)msg.num;
                break;
            case ECurrencyType.CLUB_CURRENCY:
                RoleInfo.roleInfo.clubCurrency = (int)msg.num;
                break;
            case ECurrencyType.DIAMOND:
                RoleInfo.roleInfo.damond = (int)msg.num;
                break;
            case ECurrencyType.ENERGY:
                RoleInfo.roleInfo.energy = (int)msg.num;
                break;
            case ECurrencyType.EXP:
                RoleInfo.roleInfo.curExp = msg.num;
                break;
            case ECurrencyType.GOLD:
                RoleInfo.roleInfo.gold = msg.num;
                break;
            case ECurrencyType.HONOR_CURRENCY:
                RoleInfo.roleInfo.honorCurrency = (int)msg.num;
                break;
            case ECurrencyType.TRAIL_CURRENCY:
                RoleInfo.roleInfo.trailCurrency = (int)msg.num;
                break;
            case ECurrencyType.TALENT:
                RoleInfo.roleInfo.talent = (int)msg.num;
                break;
            case ECurrencyType.AoyiJingHua:
                RoleInfo.roleInfo.aoyiJinghua = msg.num;
                break;
        }

        GED.ED.dispatchEvent(EventID.CurrencyChange, msg.type);
    }

    private void _ResLevelUp(GameEvent evt)
    {
        ResLevelUp msg = GetCurMsg<ResLevelUp>(evt.EventId);
        TwoParam<int, int> twoParam = new TwoParam<int, int>();
        twoParam.value1 = RoleInfo.roleInfo.level;
        twoParam.value2 = msg.level;

        if (GameManager.Singleton.IsStateOf(GameState.Battle))
        {
            RestoreWndMgr.Singleton.SaveWndData<RoleLevelUpWnd>(WinInfo.Create(false, null, false, twoParam), UILayer.Notice);
 
        }
        else
        {
            WinMgr.Singleton.Open<RoleLevelUpWnd>(WinInfo.Create(false, null, false, twoParam), UILayer.Notice);
        }
          
        if (RoleInfo != null)
        {
            RoleInfo.roleInfo.level = msg.level;
        }

        GED.ED.dispatchEvent(EventID.ResRoleInfo);
        GED.GuideED.dispatchEvent((int)GuideEventID.GuideTriggerCheck);
    }

    private void _ResResetExitGuildTime(GameEvent evt)
    {
        ResResetExitGuildTime msg = GetCurMsg<ResResetExitGuildTime>(evt.EventId);
        if (RoleInfo != null)
        {
            RoleInfo.roleInfo.exitGuildTime = msg.time;
        }
 
    }

    private void _ResOpenServerDays(GameEvent evt)
    {
        var msg = GetCurMsg<ResOpenServerDays>(evt.EventId);
        ServerOpenDay = msg.OpenServerDays;
    }
    private void OnResRoleInfo(GameEvent evt)
    {
        RoleInfo = GetCurMsg<ResRoleInfo>(evt.EventId);
        //Logger.log(RoleInfo.roleInfo.roleName);
        GED.ED.dispatchEvent(EventID.ResRoleInfo);
        if (RoleInfo.result != 1)
        {
            //TODO:弹窗提示 
            Logger.err("RoleService登陆失败:" + RoleInfo.roleInfo.roleName);
            return;
        }
        if (GameManager.Singleton.IsStateOf(GameState.Login))
        {
            //告诉服务器客户端初始化完成
            ReqClientInitOver();
            ChatService.Singleton.InitLoaclChatInfo();
        }else
        {
            //短线重连
            ReqClientInitOver();
        }
    }
    /// <summary>
    /// 获得体力恢复的速度
    /// </summary>
    /// <returns></returns>
    public int GetTiLiHuiFuSpeed()
    {
        // 30301
        t_globalBean globalBean = ConfigBean.GetBean<t_globalBean, int>(30301);
        if (globalBean != null)
            return globalBean.t_int_param;

        return int.MaxValue;
    }
    public void ReqClientInitOver()
    {
        ReqClientInitOver msg = GetEmptyMsg<ReqClientInitOver>();
        SendMsg(ref msg);
    }

    private void OnServerTime(GameEvent evt)
    {
        Logger.log("---------on server time-------------");
        ConnectingWindow.Singleton.Hide();
        ResCurrentTime msg = GetCurMsg<ResCurrentTime>(evt.EventId);
        TimeUtils.setServerTime(msg.curTime);
        GED.ED.dispatchEvent(EventID.ResServerTime);
        LoginService.Singleton.ReConnectSuccess();

        interval.kill();
        interval.doAction(25, DoHeartBeat);
    }

    private void DoHeartBeat(object param)
    {
        ClientHeartBeat heartBeat = GetEmptyMsg<ClientHeartBeat>();
        heartBeat.time = TimeUtils.currentMilliseconds();
        SendMsg(ref heartBeat);
    }

    private void OnServerHeartBeat(GameEvent evt)
    {
        ServerHeartBeat msg = GetCurMsg<ServerHeartBeat>(evt.EventId);

        ServerHeartBeat heartBeat = GetEmptyMsg<ServerHeartBeat>();
        heartBeat.time = msg.time;
        SendMsg(ref heartBeat);

        CoroutineManager.Singleton.stopCoroutine(checkBeatTimer);
        checkBeatTimer = CoroutineManager.Singleton.delayedCall(beatOutTime, heartBeatOutOfTime);
        //        Logger.log("服务器心跳.....");
    }

    private void OnClientHeartBeat(GameEvent evt)
    {
        ClientHeartBeat msg = GetCurMsg<ClientHeartBeat>(evt.EventId);
        long sendTime = msg.time;
        long delay = TimeUtils.currentMilliseconds() - sendTime;

        CoroutineManager.Singleton.stopCoroutine(checkBeatTimer);
        checkBeatTimer = CoroutineManager.Singleton.delayedCall(beatOutTime, heartBeatOutOfTime);
//        Logger.log("客户端心跳.....  " + delay);
    }

    private void heartBeatOutOfTime()
    {
        //心跳超时，开启重连
        LoginService.Singleton.DoRelogin();
    }

    //请求购买技能点
    public void JiNengDianGouMai()
    {
        ReqBuySkillPoint msg = GetEmptyMsg<ReqBuySkillPoint>();
        SendMsg(ref msg);
    }

    //请求购买货币
    public void ReqBuyCurrency(ECurrencyType type, int num)
    {
        ReqBuyCurrency msg = GetEmptyMsg<ReqBuyCurrency>();
        msg.type = (int)type;
        msg.num = num;
        SendMsg<ReqBuyCurrency>(ref msg);
    }
    /// <summary>
    /// 请求更换头像
    /// </summary>
    /// <param name="headIconID"></param>
    public void ReqModifyIcon(int headIconID)
    {
        ReqModifyIcon msg = GetEmptyMsg<ReqModifyIcon>();

        msg.iconId = headIconID;
        SendMsg<ReqModifyIcon>(ref msg);
    }
    /// <summary>
    /// 请求更换昵称
    /// </summary>
    /// <param name="name"></param>
    public void ReqModifyNickname(string name)
    {
        ReqModifyNickname msg = GetEmptyMsg<ReqModifyNickname>();

        msg.nickname = name;
        SendMsg<ReqModifyNickname>(ref msg);
    }

    public void ChangGold(int num)
    {
        if (RoleInfo != null && RoleInfo.roleInfo != null)
        {
            if (RoleInfo.roleInfo.gold >= int.MaxValue 
                || RoleInfo.roleInfo.gold + num > int.MaxValue)
            {
                RoleInfo.roleInfo.gold = int.MaxValue;
            }
            else
            {
                RoleInfo.roleInfo.gold += num;
            }
            GED.ED.dispatchEvent(EventID.RoleInfoChange);
        }
    }

    public void ChangeGuildId(long guildId)
    {
        RoleInfo.roleInfo.guildId = guildId;
    }

    public int GetEnergyBuyMaxNum()
    {
        int num = 0;

        t_globalBean gBean = ConfigBean.GetBean<t_globalBean, int>(30305);
        if (gBean != null)
        {
            num = gBean.t_int_param;
        }
        return num;
    }

    public RoleService()
    {
        if (GameManager.Singleton.IsDebug)
        {
            InitTestData();
        }

        t_globalBean gBean = ConfigBean.GetBean<t_globalBean, int>(30306);
        if (gBean != null)
            m_energyBuyPrice = GTools.splitStringToIntArray(gBean.t_string_param, '+');
    }

    public void BuyEnergy()
    {
        //Logger.log("----on Energy click----");
        int maxBuyCount = RoleService.Singleton.GetEnergyBuyMaxNum();
        int curBuyCount = RoleService.Singleton.GetRoleInfo().energyBuyCount;
        if (curBuyCount >= maxBuyCount)
        {
            AgainConfirmWindow.Singleton.TipOneButton("购买体力", "今日体力购买已达上限", null, null, true);
            return;
        }

        int comsumeDiamond = 0;
        if (curBuyCount >= 0 && curBuyCount < m_energyBuyPrice.Length)
            comsumeDiamond = m_energyBuyPrice[curBuyCount];
        else
            comsumeDiamond = m_energyBuyPrice[m_energyBuyPrice.Length - 1];

        int buyNum = 120;
        t_globalBean gBean = ConfigBean.GetBean<t_globalBean, int>(30307);
        if (gBean != null)
            buyNum = gBean.t_int_param;

        if (RoleService.Singleton.GetCurrencyNum((int)RoleService.ECurrencyType.DIAMOND) < comsumeDiamond)
        {
            AgainConfirmWindow.Singleton.TipOneButton("钻石不足", "钻石不足,是否前往购买？", () => {
                Debuger.Log("跳转到充值");
            }, null, false);
            return;
        }

        string strDes = string.Format("购买 {0} 体力需消耗钻石 {1}", buyNum, comsumeDiamond);
        AgainConfirmWindow.Singleton.TipOneButton("购买体力", strDes, () =>
        {
            RoleService.Singleton.ReqBuyCurrency(RoleService.ECurrencyType.ENERGY, buyNum);
        }, null, false);
    }
    /// <summary>
    /// 获得训练家最大等级
    /// </summary>
    /// <returns></returns>
    public int GetRoleMaxLevel()
    {
        // 8010
        t_globalBean globalBean = ConfigBean.GetBean<t_globalBean, int>(8010);
        if (globalBean != null)
        {
            return globalBean.t_int_param;
        }

        return 0;
    }

    #region 签到 ******************************************************************************
    /// <summary>
    /// 领取签到奖励
    /// </summary>
    public void ReqSignIn()
    {
        ReqSignIn msg = GetEmptyMsg<ReqSignIn>();

        SendMsg<ReqSignIn>(ref msg);
    }
    /// <summary>
    /// 领取总签到次数奖励
    /// </summary>
    public void ReqSignInBox()
    {
        ReqSignInBox msg = GetEmptyMsg<ReqSignInBox>();

        SendMsg<ReqSignInBox>(ref msg);
    }
    /// <summary>
    /// 总签到礼包回调
    /// </summary>
    /// <param name="evt"></param>
    private void OnResSignInBox(GameEvent evt)
    {
        ResSignInBox msg = GetCurMsg<ResSignInBox>(evt.EventId);

        RoleInfo.roleInfo.signInAwardIndex = msg.signInAwardIndex;
        GED.ED.dispatchEvent(EventID.OnResSignInBox);
    }
    /// <summary>
    /// 月签到回调
    /// </summary>
    /// <param name="evt"></param>
    private void OnResSignIn(GameEvent evt)
    {
        ResSignIn msg = GetCurMsg<ResSignIn>(evt.EventId);

        RoleInfo.roleInfo.dailySignInFlag = msg.dailySignInFlag;
        if (msg.dailySignInFlag == 1)
        {
            RoleInfo.roleInfo.monthSignIn++;
            RoleInfo.roleInfo.totalSignIn++;
        }
        GED.ED.dispatchEvent(EventID.OnResSignIn, msg.itemInfos);
    }

    public int GetSignBeanIndex()
    {
        int index = RoleInfo.roleInfo.dailySignInFlag == 0 ? RoleInfo.roleInfo.monthSignIn + 1 : RoleInfo.roleInfo.monthSignIn;
        return index;
    }

    #endregion;

    private void InitTestData()
    {
        RoleInfo = new ResRoleInfo();
        RoleInfo roleInfo = new RoleInfo();
        roleInfo.level = 7;
        RoleInfo.roleInfo = roleInfo;
        roleInfo.roleName = "测试玩家";
    }

    public override void ClearData()
    {
        base.ClearData();
    }



}
