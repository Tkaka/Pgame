using UI_KFHD_FightPower;
using UI_Common;
using UI_MainCity;
using UI_Login;
using UI_Battle;
using UI_BattleEnd;
using UI_Loading;
using UI_Beibao;
using UI_GeDouJia;
using UI_SaoDangJieSuan;
using UI_Strength;
using FairyGUI;
using UI_BuZhen;
using UI_Equip;
using UI_Level;
using UI_TaskSystem;
using UI_DrawCard;
using UI_Activity;
using UI_Arena;
using UI_HallFame;
using UI_TongXiangGuan;
using UI_XunLianJia;
using UI_Shop;
using UI_Mail;
using UI_ShenQi;
using UI_Achievement;
using UI_Talent;
using UI_StriveHegemong;
using UI_Guild;
using UI_UltemateTrain;
using UI_CloneTeamFight;
using UI_Chat;
using UI_Friend;
using UI_GuildDrill;
using UI_GuildBoss;
using UI_GuillRedEnvelope;
using UI_PetParticulars;
using UI_VIP;
using UI_Top;
using UI_AoYi;
using UI_JinHuaLian;
using UI_GuildBattle;
using UI_SiginIn;
using UI_DailyActivity;

/// <summary>
/// TODO：由工具自动生成
/// </summary>
public class WinEnum
{
#if UNITY_ANDROID
    public const string BasePath = "UITextures/";
#elif UNITY_IPHONE
    public const string BasePath = "UITextures_IOS/";
#endif

    public const string UI_Common = "UI_Common";
    public const string UI_Loading = "UI_Loading";
    public const string UI_Login = "UI_Login";
    public const string UI_MainCity = "UI_MainCity";
    public const string UI_WorldMap = "UI_WorldMap";
    public const string UI_Battle = "UI_Battle";
    public const string UI_BattleEnd = "UI_BattleEnd";
    public const string UI_Beibao = "UI_Beibao";
    public const string UI_BuZhen = "UI_BuZhen";
    public const string UI_GeDouJia = "UI_GeDouJia";
    public const string UI_SaoDangJieSuan = "UI_SaoDangJieSuan";
    public const string UI_Strength = "UI_Strength";
    public const string UI_Equip = "UI_Equip";
    public const string UI_Level = "UI_Level";
    public const string UI_TaskSystem = "UI_TaskSystem";
    public const string UI_DrawCard = "UI_DrawCard";
    public const string UI_Activity = "UI_Activity";
    public const string UI_Arena = "UI_Arena";
    public const string UI_HallFame = "UI_HallFame";
    public const string UI_TongXiangGuan = "UI_TongXiangGuan";
    public const string UI_XunLianJia = "UI_XunLianJia";
    public const string UI_Shop = "UI_Shop";
    public const string UI_Mail = "UI_Mail";
    public const string UI_ShenQi = "UI_ShenQi";
    public const string UI_Achievement = "UI_Achievement";
    public const string UI_Talent = "UI_Talent";
    public const string UI_StriveHegemong = "UI_StriveHegemong";
    public const string UI_Guild = "UI_Guild";
    public const string UI_UltemateTrain = "UI_UltemateTrain";
    public const string UI_CloneTeamFight = "UI_CloneTeamFight";
    public const string UI_Chat = "UI_Chat";
    public const string UI_Friend = "UI_Friend";
    public const string UI_GuildDrill = "UI_GuildDrill";
    public const string UI_GuildBoss = "UI_GuildBoss";
    public const string UI_GuillRedEnvelope = "UI_GuillRedEnvelope";
    public const string UI_PetParticulars = "UI_PetParticulars";
    public const string UI_VIP = "UI_VIP";
    public const string UI_Top = "UI_Top";
    public const string UI_AoYi = "UI_AoYi";
    public const string UI_JinHuaLian = "UI_JinHuaLian";
    public const string UI_GuildBattle = "UI_GuildBattle";
    public const string UI_SiginIn = "UI_SiginIn";
    public const string UI_DailyActivity = "UI_DailyActivity";
    public const string UI_KaiFuFightRank = "UI_KFHD_FightPower";

    public static string BasePath { get; internal set; }
}

public class BasicWindowRegisterCmd : ICommand
{
    public void Excute()
    {
        UI_LoadingBinder.BindAll();
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Loading, typeof(LoadingWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Loading, typeof(UpdateLoadingWindow));

        UI_CommonBinder.BindAll();
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Common, typeof(ConnectingWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Common, typeof(TipWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Common, typeof(TopRoleInfo));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Common, typeof(SingleBtnCofirmWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Common, typeof(ItemTipsWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Common, typeof(AgainConfirmWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Common, typeof(BoxReceiveWidow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Common, typeof(RoleLevelUpWnd));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Common, typeof(ChongWuDongHuaWindow));

        UIObjectFactory.SetPackageItemExtension(UI_buZhenItem.URL, typeof(BuZhenItem));
        UIObjectFactory.SetPackageItemExtension(UI_commonHeadIcon.URL, typeof(CommonHeadIcon));
        UIObjectFactory.SetPackageItemExtension(UI_petQualityDou.URL, typeof(PetQualityDou));
        UIObjectFactory.SetPackageItemExtension(UI_equipItem.URL, typeof(EquipItem));

        //新手引导
        UIObjectFactory.SetPackageItemExtension(UI_GuideDialog.URL, typeof(GuideDialog));
        UIObjectFactory.SetPackageItemExtension(UI_GuideTip.URL, typeof(GuideTip));
    }
}


public class RegisterWindowCmd : ICommand
{

    public void Excute()
    {
        UIResMgr.Singleton.AddConstPkg(WinEnum.UI_Common);

        UI_MainCityBinder.BindAll();
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_MainCity, typeof(MainCityWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_MainCity, typeof(GMWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_MainCity, typeof(FuncTipWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_MainCity, typeof(ModifyHeadIconWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_MainCity, typeof(RoleInfoWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_MainCity, typeof(HeadIconUnlockTipWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_MainCity, typeof(ChnageNameWindow));

        UIObjectFactory.SetPackageItemExtension(UI_headListItem.URL, typeof(HeadListItem));
        UIObjectFactory.SetPackageItemExtension(UI_mainCityPetInfo.URL, typeof(MainCityPetInfo));
        UIObjectFactory.SetPackageItemExtension(UI_mainCityPetName.URL, typeof(MianCityPetName));

        // 背包
        UI_BeibaoBinder.BindAll();
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Beibao, typeof(BagWindow));
        UIObjectFactory.SetPackageItemExtension(UI_BagListItem.URL, typeof(BagListItem));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Beibao, typeof(ItemSellWindow));
        //WinMgr.Singleton.RegisterPackage(WinEnum.UI_Beibao, typeof(ItemGetWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Beibao, typeof(ItemUseWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Beibao, typeof(ItemComposeWindow));


        // 登录
        UI_LoginBinder.BindAll();
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Login, typeof(LoginWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Login, typeof(SelectServerWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Login, typeof(SelectRoleWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Login, typeof(OpenPlayWindow));

        UI_BuZhenBinder.BindAll();
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_BuZhen, typeof(BuZhenWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_BuZhen, typeof(ZhenRongWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_BuZhen, typeof(ShangZhenWindow));
        UIObjectFactory.SetPackageItemExtension(UI_YiShangZhen.URL, typeof(ShangZhenListItem));
        UIObjectFactory.SetPackageItemExtension(UI_zhenRongPetItem.URL,typeof(ZhenRongPetItem));
        UIObjectFactory.SetPackageItemExtension(UI_zhenRongEquipItem.URL, typeof(ZhenRongEquipItem));
        UIObjectFactory.SetPackageItemExtension(UI_FetterListItem.URL,typeof(FetterListItem));

        //注册窗口
        UI_GeDouJiaBinder.BindAll();
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_GeDouJia,typeof(GeDouJiaWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_GeDouJia,typeof(LaiYuanWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_GeDouJia, typeof(SuMingJiHuoWindow));
        UIObjectFactory.SetPackageItemExtension(UI_PetListItem.URL, typeof(PetListItem));
        UIObjectFactory.SetPackageItemExtension(UI_SuMingPetItem.URL, typeof(SuMingPetItem));
        UIObjectFactory.SetPackageItemExtension(UI_SuMingListItem.URL,typeof(SuMingListItem));
        UIObjectFactory.SetPackageItemExtension(UI_GuanKaListItem.URL,typeof(GuanKaListItem));
        UIObjectFactory.SetPackageItemExtension(UI_DiaoLuoListItem.URL, typeof(DiaoLuoListItem));
        UIObjectFactory.SetPackageItemExtension(UI_ShangDianHeWanFaListItem.URL, typeof(ShangDianHeWanFaListItem));
        UIObjectFactory.SetPackageItemExtension(UI_SaoDangType_item.URL, typeof(SaoDangType_item));
        
        //注册窗口
        UI_SaoDangJieSuanBinder.BindAll();
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_SaoDangJieSuan,typeof(SaoDangJieSuanWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_SaoDangJieSuan, typeof(eliteSupperSaoDangWnd));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_SaoDangJieSuan, typeof(SupperSaoDangWindow));

        //战斗窗口
        UI_BattleBinder.BindAll();
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Battle, typeof(BattleWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Battle, typeof(BossWarningWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Battle, typeof(CatchPetWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Battle, typeof(CatchPetWarningWindow));
        UIObjectFactory.SetPackageItemExtension(UI_PetHeadBar.URL, typeof(PetHeadBar));
        UIObjectFactory.SetPackageItemExtension(UI_MonsterHeadBar.URL, typeof(MonsterHeadBar));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Battle, typeof(PauseWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Battle, typeof(BossBriefWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Battle, typeof(BattleExpWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Battle, typeof(BattleAwardWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Battle, typeof(BattleFailedWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Battle, typeof(BattleDialogWindow));
        UIObjectFactory.SetPackageItemExtension(UI_BattleExpItem.URL, typeof(BattleExpItem));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_BattleEnd, typeof(BattleEndWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Battle, typeof(BattleSucessWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Battle, typeof(UltimateSucessWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Battle, typeof(BattleVictoryWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Battle, typeof(BattleReplayWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Battle, typeof(ArenaSucessWindow));
        UIObjectFactory.SetPackageItemExtension(UI_BattleSet.URL, typeof(BattleSet));
        UIObjectFactory.SetPackageItemExtension(UI_nomalStarList.URL, typeof(NomalStarList));
        UIObjectFactory.SetPackageItemExtension(UI_jingYingStarList.URL, typeof(JingYingStarList));
        UIObjectFactory.SetPackageItemExtension(UI_BattleInfo.URL, typeof(BattleInfo));
        UIObjectFactory.SetPackageItemExtension(UI_battlePetGroup.URL, typeof(BattlePetGroup));


        UI_StrengthBinder.BindAll();
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Strength, typeof(StrengthWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Strength, typeof(BuyExpWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Strength, typeof(ShengPingSuccessWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Strength, typeof(SkillDetailWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Strength, typeof(ZhanHunStrengthWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Strength, typeof(ZHDiamondStrenthWindow));
        UIObjectFactory.SetPackageItemExtension(UI_petItem.URL, typeof(PetItem));
        UIObjectFactory.SetPackageItemExtension(UI_expPropItem.URL, typeof(ExpPropItem));
        UIObjectFactory.SetPackageItemExtension(UI_caiLiaoItem.URL, typeof(ShengPingCaiLiaoItem));
        UIObjectFactory.SetPackageItemExtension(UI_PetBooth.URL, typeof(PetBooth));
        UIObjectFactory.SetPackageItemExtension(UI_jiNengItem.URL, typeof(SkillItem));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Strength, typeof(WanNengFragWindow));
        UIObjectFactory.SetPackageItemExtension(UI_zhanHunItem.URL, typeof(ZhanHunItem));
        UIObjectFactory.SetPackageItemExtension(UI_zhanHunCaiLiaoItem.URL, typeof(ZhanHunMaterialItem));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Strength, typeof(JinHuaSuccessWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Strength,typeof(JiNengDianGouMaiWindow));

        UI_EquipBinder.BindAll();
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Equip, typeof(EquipStrengthWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Equip,typeof(XiangQingWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Equip,typeof(JiangXingWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Equip, typeof(JueXingChengGongWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Equip, typeof(DiamondBuyMaterialWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Equip, typeof(QuickUpgradeWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Equip, typeof(EquipSPSuccessWindow));
        UIObjectFactory.SetPackageItemExtension(UI_equipPropItem.URL, typeof(SPMaterialItem));
        UIObjectFactory.SetPackageItemExtension(UI_ShuXingItem.URL, typeof(ShuXingItem));
        UIObjectFactory.SetPackageItemExtension(UI_JueXingShuXingItem.URL, typeof(JueXingShuXingItem));
        UIObjectFactory.SetPackageItemExtension(UI_attributeItem.URL, typeof(EquipAttributeLabelItem));
        UIObjectFactory.SetPackageItemExtension(UI_expItem.URL, typeof(SpecialExpItem));
        UIObjectFactory.SetPackageItemExtension(UI_quickUpgradeItem.URL, typeof(QuickUpgradeItem));
        UIObjectFactory.SetPackageItemExtension(UI_ManXingShuXingItem.URL,typeof(ManXingShuXingItem));

        UI_LevelBinder.BindAll();
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Level, typeof(LevelMainWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Level, typeof(GuanQiaWindow));
        UIObjectFactory.SetPackageItemExtension(UI_mainLevel.URL, typeof(MainLevel));
        UIObjectFactory.SetPackageItemExtension(UI_ItemIcon.URL, typeof(CommonItem));
        UIObjectFactory.SetPackageItemExtension(UI_mainLevelItem.URL, typeof(MainLevelItem));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Level, typeof(NormalBoxOpenWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Level, typeof(KeyBoxOpenWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Level, typeof(eliteGuanQiaWindow));
        UIObjectFactory.SetPackageItemExtension(UI_eliteLevelItem.URL, typeof(EliteLevelItem));
        UIObjectFactory.SetPackageItemExtension(UI_starBoxItem.URL, typeof(StarBoxItem));
        UIObjectFactory.SetPackageItemExtension(UI_levelBoxItem.URL, typeof(LevelBoxItem));
        UIObjectFactory.SetPackageItemExtension(UI_keyBoxReceiveItem.URL, typeof(KeyBoxReceiveItem));
        UIObjectFactory.SetPackageItemExtension(UI_quickJumpChapterItem.URL, typeof(QuickJumpChapterItem));
        UIObjectFactory.SetPackageItemExtension(UI_quickJumpLevelItem.URL, typeof(QuickJumpLevelItem));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Level, typeof(QuickJumpWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Level, typeof(NewChapterOpenWindow));
        UIObjectFactory.SetPackageItemExtension(UI_mainTopItem.URL, typeof(MainTopItem));

        UI_TaskSystemBinder.BindAll();
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_TaskSystem, typeof(TaskWindow));
        //WinMgr.Singleton.RegisterPackage(WinEnum.UI_TaskSystem, typeof(DaoJuHuoDeWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_TaskSystem, typeof(BaoXiangHuoDeWindow));
        UIObjectFactory.SetPackageItemExtension(UI_RenWuListItem.URL, typeof(RenWuListItem));

        UI_DrawCardBinder.BindAll();
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_DrawCard,typeof(DrawCardWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_DrawCard, typeof(ZhaoHuanWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_DrawCard, typeof(JiangPinZhanShiWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_DrawCard,typeof(KeHuoDeWindow));
        UIObjectFactory.SetPackageItemExtension(UI_GD_ChongWuItem.URL,typeof(GD_ChongWuItem));
        UIObjectFactory.SetPackageItemExtension(UI_JiangPingListItem.URL, typeof(JiangPingListItem));
        UIObjectFactory.SetPackageItemExtension(UI_DaoJuListItem.URL, typeof(DaoJuListItem));
        UIObjectFactory.SetPackageItemExtension(UI_JieSuoDengJiFenGeXian.URL,typeof(JieSuoDengJiFenGeXian));
        UIObjectFactory.SetPackageItemExtension(UI_DC_Type.URL,typeof(DC_Type));

        UI_ActivityBinder.BindAll();
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Activity, typeof(TiaoZhanWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Activity, typeof(ActivityWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Activity, typeof(SelectDifficultyWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Activity, typeof(ZhenRongSelectWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Activity, typeof(RuleDetailWindow));
        UIObjectFactory.SetPackageItemExtension(UI_activityPropItem.URL, typeof(ActivityPropItem));
        UIObjectFactory.SetPackageItemExtension(UI_activityPanel.URL, typeof(ActivityPanel));
        UIObjectFactory.SetPackageItemExtension(UI_tiaoZhanCardItem.URL, typeof(ActivityCardItem));
        UIObjectFactory.SetPackageItemExtension(UI_difficultyItem.URL, typeof(DifficultyItem));

        //竞技场
        UI_ArenaBinder.BindAll();
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Arena, typeof(ArenaMainWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Arena, typeof(RewardWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Arena, typeof(ScoreRewardWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Arena, typeof(RankWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Arena, typeof(SeeOtherInfoWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Arena, typeof(IconSelectWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Arena, typeof(ContinuousChallengeWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Arena, typeof(ArenaRuleWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Arena, typeof(ArenaTypeWnd));

        UIObjectFactory.SetPackageItemExtension(UI_JinJiIconCell.URL, typeof(JinjiIconCell));

        //名人堂
        UI_HallFameBinder.BindAll();
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_HallFame, typeof(HallFameListWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_HallFame, typeof(TeamWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_HallFame, typeof(ExplainWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_HallFame, typeof(ColorUpWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_HallFame,typeof(HF_ShuXingXianShiWindow));
        UIObjectFactory.SetPackageItemExtension(UI_HF_shuxing.URL, typeof(HF_shuxing));
        UIObjectFactory.SetPackageItemExtension(UI_HallFameListItem.URL,typeof(HallFameListItem));
        UIObjectFactory.SetPackageItemExtension(UI_TeamListItem.URL,typeof(TeamListItem));
        UIObjectFactory.SetPackageItemExtension(UI_TeamPetSelectItem.URL,typeof(TeamPetSelectItem));
        UIObjectFactory.SetPackageItemExtension(UI_HF_MeiShi.URL,typeof(HF_MeiShi));
        UIObjectFactory.SetPackageItemExtension(UI_ExplainListItem.URL,typeof(ExplainListItem));


        // 铜像馆
        UI_TongXiangGuanBinder.BindAll();
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_TongXiangGuan, typeof(TongXiangGuanWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_TongXiangGuan, typeof(ZhanTingWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_TongXiangGuan, typeof(TongXiangShuXingWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_TongXiangGuan, typeof(BuyTongXiangWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_TongXiangGuan, typeof(TongXiangRuleWindow));

        UIObjectFactory.SetPackageItemExtension(UI_tongXiangGuanPage.URL, typeof(TongXiangGuanPage));
        UIObjectFactory.SetPackageItemExtension(UI_zhanTingItem.URL, typeof(ZhanTingItem));
        UIObjectFactory.SetPackageItemExtension(UI_TongXiangItem.URL, typeof(TongXiangItem));
        UIObjectFactory.SetPackageItemExtension(UI_tongXiangMaterialBtn.URL, typeof(TongXiangMaterialBtn));
        UIObjectFactory.SetPackageItemExtension(UI_tongXiangGoodsItem.URL, typeof(TongXiangGoodsItem));

        // 训练家
        UI_XunLianJiaBinder.BindAll();
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_XunLianJia, typeof(XunLianJiaWindow));

        //商店
        UI_ShopBinder.BindAll();
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Shop, typeof(ShopMainWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Shop, typeof(BuyShopWnd));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Shop, typeof(EquipBoxItem));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Shop, typeof(SuiPianDuiHuanWnd));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Shop, typeof(SuiPianBuyShopWnd));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Shop, typeof(OpenBoxReawardWnd));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Shop, typeof(RewardPreviewWnd));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Shop, typeof(MysteriousShopWnd));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Shop, typeof(SellCoinItemWnd));
        UIObjectFactory.SetPackageItemExtension(UI_ShopCommonList.URL, typeof(ShopCommonList));

        //邮件
        UI_MailBinder.BindAll();
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Mail, typeof(MailWnd));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Mail, typeof(MailContentWnd));

        // 神器
        UI_ShenQiBinder.BindAll();
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_ShenQi, typeof(ShenQiMainWidow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_ShenQi, typeof(IntroduceWindow));

        UIObjectFactory.SetPackageItemExtension(UI_unlockCoditionItem.URL, typeof(UnlockCoditionItem));
        UIObjectFactory.SetPackageItemExtension(UI_shenQiItem.URL, typeof(ShenQiItem));
        UIObjectFactory.SetPackageItemExtension(UI_shenQiShuXingItem.URL, typeof(ShenQiShuXingItem));
        UIObjectFactory.SetPackageItemExtension(UI_peiYangResItem.URL, typeof(PeiYangResItem));

        //成就
        UI_AchievementBinder.BindAll();
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Achievement,typeof(AM_MainWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Achievement,typeof(AM_TitleWindow));
        UIObjectFactory.SetPackageItemExtension(UI_AM_TitleLietItem.URL,typeof(AM_TitleLietItem));
        UIObjectFactory.SetPackageItemExtension(UI_AM_List_Item.URL,typeof(AM_List_Item));
        UIObjectFactory.SetPackageItemExtension(UI_AM_StartListItem.URL,typeof(AM_StartListItem));

        //天赋
        UI_TalentBinder.BindAll();
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Talent, typeof(TalentMainWnd));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Talent, typeof(TalentShowWnd));
        UIObjectFactory.SetPackageItemExtension(UI_talentCell1.URL, typeof(TalentCell));

        //拳皇争霸
        UI_StriveHegemongBinder.BindAll();
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_StriveHegemong,typeof(SH_MainWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_StriveHegemong,typeof(SH_XiaZhuWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_StriveHegemong,typeof(SH_ExchangeWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_StriveHegemong,typeof(SH_ApplyWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_StriveHegemong,typeof(SH_BQ_CanSaiZhenRongWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_StriveHegemong,typeof(SH_DuiShou_ZhenRongWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_StriveHegemong,typeof(SH_WoDeSaiCheng_BuZhenWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_StriveHegemong,typeof(SH_DF_BuZhenWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_StriveHegemong,typeof(SH_RuleWindow));
        UIObjectFactory.SetPackageItemExtension(UI_SH_GZ_Time.URL,typeof(SH_GZ_Time));
        UIObjectFactory.SetPackageItemExtension(UI_SH_DH_DaoJuIcon.URL,typeof(SH_DH_DaoJuIcon));
        UIObjectFactory.SetPackageItemExtension(UI_SH_GZ_Jiangli.URL,typeof(SH_GZ_Jiangli));
        UIObjectFactory.SetPackageItemExtension(UI_SH_GZ_ListItem.URL,typeof(SH_GZ_ListItem));
        UIObjectFactory.SetPackageItemExtension(UI_SH_GZ_FenGeXian.URL,typeof(SH_GZ_FenGeXian));
        UIObjectFactory.SetPackageItemExtension(UI_SH_DH_DuiHuanListItem.URL,typeof(SH_DH_DuiHuanListItem));
        UIObjectFactory.SetPackageItemExtension(UI_SH_HG_MyCondition.URL,typeof(SH_HG_MyCondition));
        UIObjectFactory.SetPackageItemExtension(UI_SH_HG_baqiangJianKuang.URL,typeof(SH_HG_baqiangJianKuang));
        UIObjectFactory.SetPackageItemExtension(UI_SH_BaQiangListItem.URL,typeof(SH_BaQiangListItem));
        UIObjectFactory.SetPackageItemExtension(UI_SH_HG_Figure.URL,typeof(SH_HG_Figure));
        UIObjectFactory.SetPackageItemExtension(UI_SH_DaoJiShi.URL,typeof(SH_DaoJiShi));
        UIObjectFactory.SetPackageItemExtension(UI_SH_WS_ListItem.URL,typeof(SH_WS_ListItem));
        UIObjectFactory.SetPackageItemExtension(UI_SH_ZR_SaiCheng.URL,typeof(SH_ZR_SaiCheng));
        UIObjectFactory.SetPackageItemExtension(UI_SH_ZR_DianFen.URL,typeof(SH_ZR_DianFen));
        UIObjectFactory.SetPackageItemExtension(UI_SH_XZ_XiaZhuListItem.URL,typeof(SH_XiaZhuListItem));
        UIObjectFactory.SetPackageItemExtension(UI_SH_BM_ListItem.URL, typeof(SH_BM_ListItem));
        UIObjectFactory.SetPackageItemExtension(UI_SH_ZS_OFF_ListIten.URL,typeof(SH_ZS_OFF_ListIten));

 
        // 终极试炼
        UI_UltemateTrainBinder.BindAll();
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_UltemateTrain, typeof(UltemateTrainMainWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_UltemateTrain, typeof(UltemateTrainRuleWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_UltemateTrain, typeof(KeyTrainResWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_UltemateTrain, typeof(BuyPropertyWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_UltemateTrain, typeof(SecretBoxWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_UltemateTrain, typeof(KeyJumpTipWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_UltemateTrain, typeof(SelectOpponentWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_UltemateTrain, typeof(TrainScoreRewardWindow));
        UIObjectFactory.SetPackageItemExtension(UI_floorPropertyItem.URL, typeof(FloorPropertyItem));
        UIObjectFactory.SetPackageItemExtension(UI_rankRangeRewardItem.URL, typeof(RankRangeRewardItem));
        UIObjectFactory.SetPackageItemExtension(UI_scoreRewardItem.URL, typeof(ScoreRewardItem));
        UIObjectFactory.SetPackageItemExtension(UI_ultematePetItem.URL, typeof(UltematePetItem));
        UIObjectFactory.SetPackageItemExtension(UI_opponentItem.URL, typeof(OpponentItem));
        UIObjectFactory.SetPackageItemExtension(UI_opponentPetItem.URL, typeof(OpponentPetItem));
        UIObjectFactory.SetPackageItemExtension(UI_floorTipItem.URL, typeof(FloorTipItem));
        UIObjectFactory.SetPackageItemExtension(UI_jumpBoxItem.URL, typeof(JumpBoxItem));

        //公会
        UI_GuildBinder.BindAll();
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Guild, typeof(GuildScenceWnd));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Guild, typeof(GuildHallWnd));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Guild, typeof(GuildBaseInfoWnd));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Guild, typeof(SendEmailWnd));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Guild, typeof(ChangeBadgeWnd));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Guild, typeof(GuildChangeNameWnd));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Guild, typeof(GuildExplainWnd));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Guild, typeof(SeeMemberInfoWnd));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Guild, typeof(CheckMemberJoinWnd));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Guild, typeof(SetLimitWnd));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Guild, typeof(GuildLogWnd));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Guild, typeof(JoinGuildMainWnd));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Guild, typeof(JoinGuildWnd));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Guild, typeof(CreateGuildWnd));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Guild, typeof(FindGuildWnd));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Guild, typeof(ChairmanRewardWnd));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Guild, typeof(ModifyNoticeWnd));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Guild, typeof(FirstOpenNoticeWnd));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Guild, typeof(FragmentWishWnd));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Guild, typeof(WishSelectWnd));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Guild, typeof(DonateHallWnd));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Guild, typeof(DonatePageWnd));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Guild, typeof(DonateRewardWnd));

        UIObjectFactory.SetPackageItemExtension(UI_objWishPlayerCell.URL, typeof(objWishPlayerCell));
        UIObjectFactory.SetPackageItemExtension(UI_GuildInfoCell.URL, typeof(GuildInfoCell));
        UIObjectFactory.SetPackageItemExtension(UI_HangPointnInfo.URL, typeof(HangPointnInfo));

        // 克隆组队战
        UI_CloneTeamFightBinder.BindAll();
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_CloneTeamFight, typeof(CloneMainWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_CloneTeamFight, typeof(CloneRuleWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_CloneTeamFight, typeof(CloneTeamWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_CloneTeamFight, typeof(CloneInviteFriendWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_CloneTeamFight, typeof(CloneWinWindow));

        UIObjectFactory.SetPackageItemExtension(UI_cloneItem.URL, typeof(CloneItem));
        UIObjectFactory.SetPackageItemExtension(UI_cloneTeammateItem.URL, typeof(CloneTeammateItem));
        //聊天
        UI_ChatBinder.BindAll();
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Chat, typeof(ChatWnd));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Chat, typeof(TrumpetWnd));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Chat, typeof(MainCityChatWnd));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Chat, typeof(ChatChannelSelectWnd));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Chat, typeof(MarqueeWnd));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Chat, typeof(ChatEmojiWnd));
        UIObjectFactory.SetPackageItemExtension(UI_otherChatCell.URL, typeof(OtherChatCell));
        UIObjectFactory.SetPackageItemExtension(UI_SelfChatCell.URL, typeof(SelfChatCell));
        UIObjectFactory.SetPackageItemExtension(UI_normalChatCell.URL, typeof(NormalChatCell));
        UIObjectFactory.SetPackageItemExtension(UI_mainCityChatCell.URL, typeof(mainCityChatCell));
        UIObjectFactory.SetPackageItemExtension(UI_MaincityTrumpetMsgInfo.URL, typeof(MaincityTrumpetMsgInfo));

        //好友
        UI_FriendBinder.BindAll();
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Friend, typeof(FriendInfoWnd));

        //工会训练所
        UI_GuildDrillBinder.BindAll();
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_GuildDrill,typeof(GD_MainWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_GuildDrill,typeof(GD_XuanZeWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_GuildDrill,typeof(GD_WeTaJiaSuWindow));
        UIObjectFactory.SetPackageItemExtension(UI_GD_JiaSuiListItem.URL,typeof(GD_JiaSuiListItem));
        UIObjectFactory.SetPackageItemExtension(UI_GD_JiaSuRiZhiListItem.URL, typeof(GD_JiaSuRiZhiListItem));
        UIObjectFactory.SetPackageItemExtension(UI_GD_XunLianWei.URL,typeof(GD_XunLianWei));
        UIObjectFactory.SetPackageItemExtension(UI_GD_XuanZeListItem.URL,typeof(GD_XuanZeListItem));

        // 公会副本
        UI_GuildBossBinder.BindAll();
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_GuildBoss, typeof(GuildBossMianWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_GuildBoss, typeof(GuildBossPassRankWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_GuildBoss, typeof(GuildBossDamageRankWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_GuildBoss, typeof(GuildBossDistributeWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_GuildBoss, typeof(GuildBossLevelWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_GuildBoss, typeof(GuildBossPassRewardWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_GuildBoss, typeof(GuildBossRewradWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_GuildBoss, typeof(GuildMemberProgressWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_GuildBoss, typeof(GuildBossRuleWindow));

        UIObjectFactory.SetPackageItemExtension(UI_guildBossItem.URL, typeof(GuildBossItem));
        UIObjectFactory.SetPackageItemExtension(UI_guildBossRewardItem.URL, typeof(GuildBossRewardItem));
        UIObjectFactory.SetPackageItemExtension(UI_guildBossDamageRankItem.URL, typeof(GuildBossDamageRankItem));
        UIObjectFactory.SetPackageItemExtension(UI_guildBossRankTip.URL, typeof(GuildBossRankTip));
        UIObjectFactory.SetPackageItemExtension(UI_guildBossDistributeItem.URL, typeof(GuildBossDistributeItem));
        UIObjectFactory.SetPackageItemExtension(UI_frontGuildItem.URL, typeof(FrontGuildItem));
        UIObjectFactory.SetPackageItemExtension(UI_memberProgressItem.URL, typeof(MemberProgressItem));
        UIObjectFactory.SetPackageItemExtension(UI_passRankItem.URL, typeof(PassRankItem));
        UIObjectFactory.SetPackageItemExtension(UI_passRewardItem.URL, typeof(PassRewardItem));

        //工会红包
        UI_GuillRedEnvelopeBinder.BindAll();
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_GuillRedEnvelope,typeof(GRE_MainWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_GuillRedEnvelope, typeof(GRE_FaHongBaoWindow));
        UIObjectFactory.SetPackageItemExtension(UI_GRE_RedEnvelopeItem.URL, typeof( GRE_RedEnvelopeItem));
        UIObjectFactory.SetPackageItemExtension(UI_GRE_RedEnvelopeItem.URL, typeof(GRE_RedEnvelopeItem));
        UIObjectFactory.SetPackageItemExtension(UI_GRE_TheHighestItem.URL,typeof(GRE_TheHighestItem));
        UIObjectFactory.SetPackageItemExtension(UI_GRE_FaHongBao_type.URL,typeof(GRE_FaHongBao_type));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_GuillRedEnvelope,typeof(GER_PaiHangWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_GuillRedEnvelope,typeof(GRE_GuiZeWindow));
        UIObjectFactory.SetPackageItemExtension(UI_GRE_GuiZeListitem.URL,typeof(GRE_GuiZeListitem));
        UIObjectFactory.SetPackageItemExtension(UI_GRE_RankListItem.URL,typeof(GRE_RankListItem));
        UIObjectFactory.SetPackageItemExtension(UI_GRE_Top_Qiang_ListItem.URL,typeof(GRE_Top_Qiang_ListItem));
        UIObjectFactory.SetPackageItemExtension(UI_GER_WoDeHuoDe.URL,typeof(GER_WoDeHuoDe));

        //宠物详情界面
        UI_PetParticularsBinder.BindAll();
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_PetParticulars, typeof(ChongWuXiangQingWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_PetParticulars, typeof(ZhanHUnXiangQingWindow));
        UIObjectFactory.SetPackageItemExtension(UI_XinXiMianBan.URL,typeof(XinXiMianBan));
        UIObjectFactory.SetPackageItemExtension(UI_ZhanHunMianBan.URL, typeof(ZhanHunMianBan));
        UIObjectFactory.SetPackageItemExtension(UI_JiNengMianBan.URL, typeof(JiNengMianBan));
        UIObjectFactory.SetPackageItemExtension(UI_JiBanMianBan.URL, typeof(JiBanMianBan));
        UIObjectFactory.SetPackageItemExtension(UI_JianJieMianBan.URL, typeof(JianJieMianBan));
        UIObjectFactory.SetPackageItemExtension(UI_DingWeiMianBan.URL,typeof(DingWeiMianBan));
        UIObjectFactory.SetPackageItemExtension(UI_JiNengListItem.URL,typeof(JiNengListItem));
        UIObjectFactory.SetPackageItemExtension(UI_JiBanListItem.URL,typeof(JiBanListItem));
        UIObjectFactory.SetPackageItemExtension(UI_ZhanHunListItem.URL,typeof(ZhanHunListItem));


        //VIP  充值
        UI_VIPBinder.BindAll();
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_VIP, typeof(VipMainWnd));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_VIP, typeof(RechargeWnd));
        UIObjectFactory.SetPackageItemExtension(UI_vipPageCell.URL, typeof(VipPageCell));
        UIObjectFactory.SetPackageItemExtension(UI_VipTitle.URL, typeof(VipTitle));
        UIObjectFactory.SetPackageItemExtension(UI_RechargeCell.URL, typeof(RechargeCell));

        //排行榜
        UI_TopBinder.BindAll();
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Top, typeof(Top_mainWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_Top, typeof(Top_XiangQingWindow));
        UIObjectFactory.SetPackageItemExtension(UI_Top_ListItem.URL, typeof(Top_ListItem));
        UIObjectFactory.SetPackageItemExtension(UI_Top_GuildListItem.URL, typeof(Top_GuildListItem));


        //奥义
        UI_AoYiBinder.BindAll();
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_AoYi, typeof(AoyiMainWnd));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_AoYi, typeof(AoyiChangeWnd));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_AoYi, typeof(OneKeyPlaceWnd));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_AoYi, typeof(AoyiResolveWnd));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_AoYi, typeof(ResolveFastSelectWnd));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_AoYi, typeof(AoyiStrengthWnd));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_AoYi, typeof(AoyiRewardWnd));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_AoYi, typeof(AoyiOnekeyStrengthWnd));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_AoYi, typeof(AoyiDrawWnd));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_AoYi, typeof(DrawResultWnd));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_AoYi, typeof(AoyiTipsWnd));

        UIObjectFactory.SetPackageItemExtension(UI_placeAyCell.URL, typeof(PlaceAyCell));
        UIObjectFactory.SetPackageItemExtension(UI_addAyCell.URL, typeof(AddAoyiCell));
        UIObjectFactory.SetPackageItemExtension(UI_changeAyCell.URL, typeof(ChangeAyCell));
        UIObjectFactory.SetPackageItemExtension(UI_ayIocnList.URL, typeof(AoyiIconList));
        UIObjectFactory.SetPackageItemExtension(UI_AoyiCommonItem.URL, typeof(AoyiCommonItem));


        //进化链
        UI_JinHuaLianBinder.BindAll();
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_JinHuaLian, typeof(JinHuaLianWindow));
        UIObjectFactory.SetPackageItemExtension(UI_JinHuaDiZuo.URL, typeof(JinHuaDiZuo));

        // 公会战
        UI_GuildBattleBinder.BindAll();
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_GuildBattle, typeof(GuildBattleMianWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_GuildBattle, typeof(GuildBattleBuZhengWindow));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_GuildBattle, typeof(GuildBattleExchangeWindow));

        UIObjectFactory.SetPackageItemExtension(UI_guildBattlePetItem.URL, typeof(GuildBattlePetItem));
        UIObjectFactory.SetPackageItemExtension(UI_guildBattleZhenRongItem.URL, typeof(GuildBattleZhenRongItem));
        UIObjectFactory.SetPackageItemExtension(UI_guildBattleExchangeItem.URL, typeof(GuildBattleExchangeItem));

        // 签到
        UI_SiginInBinder.BindAll();
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_SiginIn, typeof(SiginInWindow));

        UIObjectFactory.SetPackageItemExtension(UI_singinInItem.URL, typeof(SiginInItem));

        //日常活动
        UI_DailyActivityBinder.BindAll();
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_DailyActivity, typeof(WndDailyRoot));
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_DailyActivity, typeof(WndDuiHuanNum));


        UI_KFHD_FightPowerBinder.BindAll();
        WinMgr.Singleton.RegisterPackage(WinEnum.UI_KaiFuFightRank, typeof(WndKFHDFightPower));
    }
}