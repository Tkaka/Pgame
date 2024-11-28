using System;

public enum EventID
{
    // 网络连接事件
    Net_Connected,                     			// 连接成功
    Net_Disconnect,                             // 连接断开
    Net_ConnectFailed,                              // 连接失败
    Net_Error,                                  // 连接错误



    Net_Connected_FS,                     			// 连接成功
    Net_Disconnect_FS,                             // 连接断开
    Net_ConnectFailed_FS,                              // 连接失败
    Net_Error_FS,                                  // 连接错误


    //EventStart = 1234567,
    //--Startup
    //DownloadComplete,
    //StartupEnd,
    //--Frame Event
    //ApplicationPaused,
    //FullScreenUIOpen,
    //FullScreenUIClose,
    //WindowOpened,
    //WindowClosed,
    ClearServiceData,
    //CameraPosChange,

    //--Game Event
    RedDotValueChange, //红点状态改变
    HongDianChange,

    SceneLoadFinish,
    BattleMapInited,
    MapSpawnerTriggered,
    MonsterSpawnCmp,

    OnRightJoystickMove,
    OnRightJoystickTouchStart,
    OnRightJoystickTouchEnd,


    PageOpen,                   //窗口打开
    PageClose,                  //窗口关闭

    //Battle
    FightEnd,
    OnSelectActor,               //选中角色
    OnPetNormalSkill,
    OnPetBigSkill,
    OnProduceHurt,
    PlayerMoveCmp,
    ActorMoveCmp,

    CurTurnClear,               //当前轮所有怪物死亡
    CatchPet,

    //Timeline
    TimelineEvt,
    MainCityDollyCmp,



    ServerListLoaded,
    NoticeLoaded,


    //Service --> UI
    ResRoleList,
    ResRoleInfo,
    RoleInfoChange,
    ResServerTime,
    ResItemSell,
    ResBagUpdate,
    ResBoxItemUse,
    ResBagInfo,
    ResPetFragmentCompose,
    


    //---------UI--------
    SelectServerWindowClose,
    OnConnectServer,
    OnShangZhenChongWuId,           // 宠物ID
    OnPetShuXingChanged,            // 宠物属性改变时(升级，升品，升星==)
    OnWuQiHunSuiPianHeCheng,        // 武器魂碎片合成
    OnJueXingJieGuo,                // 觉醒成功界面弹出
    OnJiNengDianGouMai,
    OnJiNengShengJi,                  //技能升级

    OnLevelOpenBox,                  // 打开关卡中的箱子
    OnRefreshTaskList,               // 增加或者删除任务刷新任务列表刷新任务列表
    OnTaskInfoChange,                // 任务列表里面的信息该改变
    OnTaskType,                      //点击的任务的类型
    OnPetTeamListChanged,            // 宝贝上阵阵容改变
    OnBossBriefWindowClose,
    OnBattleDialogWindowClose,
    OnFightSuccessed,                // 战斗胜利
    OnDungeonInfoUpdate,             // 副本信息更新

    ActDataUpdate,                   //关卡数据更新   
    OnTaskEndAction,                 //任务界面停止协程  

    OnResDrawCard,                   //接收到抽卡结果信息
    OnDrawCardDongHuaClose,          //抽卡界面宠物展示窗口关闭
    OnDrawCard,
    OnDrawCardEndAnimition,
    OnDrawCardEndZhanShi,
    OnDrawCardCloseZhaoHuan,
    OnResAcross,                    //抽卡界面接收到跨天信息，也就是免费抽卡次数刷新

    RolePropertyChange,             //主角属性改变事件
    CurrencyChange,                 //货币改变
    OnCloseLaiYuan,                  //关闭来源窗口

    //---------------竞技场
    RewardStateChange,              //领奖状态改变
    ClearCoolTime,                  //清除冷却时间
    ArenaInfoRefresh,               //竞技场信息刷新
    PlayersChange,                  //竞技场玩家列表变化 
    PlayerChangeCount,              //竞技场换一批次数
    ChallengeCountChange,           //竞技场挑战次数改变

    OnReceiveChallengeInfo,          // 收到挑战信息 
    OnBuZhenStarFightBtnClick,       // 布阵界面的开始战斗按钮点击 
    OnActivityFightStartRes,         // 活动副本请求战斗的结果
    OnActivityFightEndRes,           // 活动副本战斗结果

    OnActivitySaoDanRes,             // 扫荡完成

    OnResExhibitionInfo,             // 获得铜像馆所有展厅信息
    OnResExhibitionRoomInfo,         // 展厅信息的回调
    OnResStatueInfo,                 // 铜像信息的回调
    OnExhibitionInfoChange,          // 铜像馆信息改变
    //-------名人堂
    OnResHofSingleInfo,               //获取到单个名人堂信息
    OnAllPriority,                    //总先手值
    OnItemList,                       //奖品列表

    //--------邮件
    MainRefresh,                    //邮件刷新

    TipFuncChange,                  //当前预告的功能id改变
    FunOpenEvent,                   //功能开启事件

    //商店
    ShopPrepareRefresh,            //商店准备刷新
    ShopRefresh,                   //商店刷新
    ShopBuyResult,                 //商店购买结果
    EquipTreasureBoxRefresh,       //装备觉醒宝箱信息刷新
    ShopOpenOrClose,               //商店关闭或开启                 

    //----成就系统
    OnAchievmentchage,          //任务信息改变

    //天赋
    TalentLevelUp,              //天赋等级改变
    TalentReset,                //天赋重置
    TalentPageUnlock,           //天赋页解锁
     
    OnAchievementRank,          //成就排行榜

    OnResShenQiUnlock,             // 神器解锁回调
    OnResShenQiCulture,            // 神器培养回调
    OnResArtifactTrainSave,        // 神器保存回调

    OnBoxReceivedWindowClose,      // 宝箱打开界面关闭
    OnSelectPetListItem,           // 点击了宠物选择界面的选择按钮

    // 终极试炼
    OnResTrainInfo,                // 终极试炼信息回调
    OnResTrialSkip,                // 终极试炼跳过回调
    OnResTrainFloor,               // 试炼层信息回调
    OnResTrialSingleBoxOpen,       // 单个试炼宝箱打开回调
    OnResTrialBatchBoxOpen,        // 批量试炼宝箱打开回调
    OnResTrialScoreAwardInfo,      // 终极试炼积分奖励信息
    OnResTrialScoreAwardGet,       // 领取试炼积分奖励的回调
    OnResTrialFightStart,          // 终极试炼战斗开始
    OnLeaveSecretBoxWindow,        // 离开试炼宝箱界面
    OnLeavePropertyFloor,          // 离开属性层界面

    //拳皇争霸游戏内消息
    OnStriveHegemongOpenSignUp,               //开启报名
    OnStriveHegemongOpen,                   //开启活动
    OnStriveHegemongEnd,                    //活动结束,主城同事接收通知显示观战按钮
    OnStriveHegemongJoin,                   //报名
    OnStriveHegemongEightGameOpen,         //八强开赛
    OnNextCompetitionOpen,                 //下场比赛开始
    OnSH_RoleInfo,                         //角色信息
    OnTargetFightInfo,                    //参赛选手信息
    OnStriveHegemongOpenBox,               //打开宝箱
    OnStriveHegemongExchange,             //兑换
    OnStriveHegemongBetInfo,               //下注信息
    OnStriveHegemongOpenMainWindow,      //收到此消息打开主界面
    OnstriveHegemongMySelfInfo,           //收到此消息刷新我的赛程板块已开战函数
    OnStriveHegemongBaoMingKuaiJie,       //发给主城的显示报名快捷
    OnStriveHegemongGuanZhan,              //发给主城显示观战按钮
    OnStriveHegemongHuiGu,                 //昨日回顾
    OnStriveHegemongXiaZhu,                 //点击下注



    //公会
    GuildLogRefresh,             //公会日志
    GuildFindResult,             //公会查找结果
    GuildRewardStateChange,      //公会会长每日奖励状态改变
    GuildInfo,                   //公会信息
    GuildChange,                 //帮会ID改变
    GuildBadgeChange,            //公会徽章改变
    GuildNameChange,             //公会名改变
    GuildMailNumChange,          //公会邮件数量改变
    JoinLimitChange,             //公会加入限制改变
    GuildTypeChange,             //社团类型改变
    GuildListInfo,               //公会列表信息返回
    GuildMemberInfoChange,       //公会成员信息改变
    GuildApplyerInfo,            //公会申请者信息
    GuildNoticeChange,           //公会公告改变
    GuildWishInfo,               //公会许愿信息
    GuildWishResult,             //帮会许愿结果
    GuildDonateInfo,             //帮会捐献信息
    GuildEnter,                  //社团进入事件

    GameObjectClick,             //物体点击事件

    // 克隆组队战
    OnResTeamFightMonsterInfo,              // 克隆组队战怪物信息回调
    OnResTeamFightTeamInfo,                 // 克隆组队战队伍信息回调
    OnResTeamFightTeammateChange,           // 克隆组队战队伍信息改变
    OnResTeamFightForbibFastEnter,          // 克隆组队战是否禁止自动加入回调 
    OnResTeamFightStart,                    // 克隆组队战战斗开始回调
    OnResTeamFightEnd,                      // 克隆组队战战斗结束
    OnResTeamFightChangePet,                // 克隆组队战宠物改变
    OnResTeamFightOpenBox,                  // 克隆组队战开箱
    OnResTeamFightNoticeSuccess,            // 克隆组队战通知队员成功
    OnResTeamFightInviteSuccess,            // 克隆组队战邀请成功
    OnResTeamFightFriendInviteSuccess,      // 克隆组队战邀请好友成功


    //聊天
    ChatInfoRefresh,             //聊天消息刷新
    ChatShowChannelChange,       //主界面显示的聊天频道改变

    // 工会副本
    OnResGuildDungeonInfo,           // 获得公会副本消息
    OnResAllotRecordInfo,            // 公会副本分配记录
    OnResGuildPassRankInfo,          // 公会通关排行
    OnResProgressInfo,               // 公会成员进度
    OnResBossInfo,                   // 当前boss信息
    OnResDamageRank,                 // 伤害排名
    OnResFightStart,                 // 公会副本战斗开始
    OnResFightEnd,                   // 公会副本战斗结果
    OnResGuildBossGetReward,         // 领取boss首通奖励

    //工会训练所
    OnGuildDrillOpenMainWin,     //收到此消息更新主界面
    OnGuildDrillReceiveData,     //接收数据
    OnGuildDrillExpediteRole,    //社团营地可加速角色列表
    OnGuildDrillOpenNewPlace,   //开通新的训练场地
    OnGuildDrillChangePet,      //更换宠物
    OnGuildDrillAffirmChange,   //确认更换宠物
    OnGuildDrillChangePetWindow,//更换宠物窗口
    OnGuildDrillSetPet,          //设置宠物
    OnGuildDrillRolePets,          //玩家的宠物列表，玩家在训练所的宠物列表
    OnGuildDrillJiaSuPetId,        //为别的玩家加速时发送的petid
    OnGuildDrillTaRenJiaSuFanHui, //为别的玩家加速返回

    EnergyBuyCountChange,         //体力购买次数改变
    OnResModifyIcon,              // 更换头像信息
    OnResModifyNickname,          // 角色名字改变
    //社团红包
    OnGuildRedSheTuan,         //社团红包页签
    OnGuildRedDataManger,      //红包数据整理完毕
    OnGuildHongBaoNumChange,   //发红包次数改变
    OnGuildFaHongBaoPaiHang,  //得到发红包排行榜
    OnGuildSleepQiangHongBao,  //刷新抢红包
    OnGuildHongBaoLieBiao,    //抢红包列表
    OnGuildHasHongBao,        //有红包了
    OnGuildHuoDeHongBao,      //获得红包

    //vip
    VipInfoChange,               //vip信息改变
    VipGiftBagStateChange,       //vip礼包状态改变

    OnOpenXiangQingMianBan,       //查看详情面板
    OnYinChangJinBi,              //隐藏金币的条

    OnTopDataChange,              //排行榜信息改变

    //奥义
    AoyiStoneInfoChange,          //奥义石头信息改变
    AoyiRewardStateChange,        //奥义技能领奖改变
    AoyiDrawRewardInfoChange,     //奥义抽卡奖励信息改变

    //  -- 签到--
    OnResSignIn,                  // 签到回调
    OnResSignInBox,               // 累计签到奖励回调

    DailyActivityRefresh,           //日常活动数据刷新
    DailySubActivityChangeState,        //活动项目状态改变

    PetInfoInit,       //宠物初始化

}