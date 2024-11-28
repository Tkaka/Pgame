
//跳转类型
public enum JumpType
{
    None,
    PetShengPing = 1,  //宠物升品
    PetShengJi = 2, // 宠物升级
    EquipStrength = 3, //装备强化
    EquipJueXing = 4,  //装备觉醒
    ZhanHunStrength = 5, //战魂强化
    ShengQi = 6,         //神器
    EquipBoxChouKa = 7,  //装备宝箱抽卡
    PetShengXing = 8,    //宝贝升星
    PetManager = 9,      //宝贝管理界面
    JiuBa = 10,        //酒吧界面
    QuangHuangZhengBa = 11, //拳皇争霸商店
    GuildFightShop = 12,   //公会战商店
    MingRenTang = 13,      //名人堂

    JingJiChang = 14,   //竞技场
    JinBiFuBen = 15,   //金币副本
    GongHui = 16,       //工会
    ZhuangBeiJueXingBaoXiang = 17,//装备觉醒宝箱
    ZaHuoShangDian = 18, // 杂货商店
    RongYuShangDian = 19,//荣誉商店
    ShiLianShangDian = 20,// 试炼商店
    SheTuanShangDian = 21,//社团商店
    JingYingGuanKa = 22, //精英关卡
    ZhongJiShiLian = 23,//终极试炼
    JingYanTiaoZhan = 24,//经验挑战
    HuanXiangTiaoZhan = 25,//幻想挑战
    QuanHuangZhengBa = 26, //拳皇争霸
    EMengFuBen = 27,//噩梦副本
    ChengJiu = 28, //成就
    TongXiangGuan = 29,//铜像馆
    AoYiChouJiang = 30,//奥义抽奖
}

public class JumpWndMgr
{
    private static JumpWndMgr m_instance;
    public static JumpWndMgr Singleton
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = new JumpWndMgr();
            }
            return m_instance;
        }
    }
    
    private int _GetDefaultPetId()
    {
        if (PetService.Singleton.GetPetInfos() == null || PetService.Singleton.GetPetInfos().Count == 0)
        {
            TipWindow.Singleton.ShowTip("当前没有宠物");
            return -1;
        }

        return PetService.Singleton.GetPetInfos()[0].petId;
    }

    public bool JumpToWnd(JumpType jumpType, int jumpParam = 0)
    {
        bool isHaveJump = true;
        switch ((JumpType)jumpType)
        {
            case JumpType.PetShengPing:
                TwoParam<int, StrengthType> twoParam = new TwoParam<int, StrengthType>();
                twoParam.value1 = _GetDefaultPetId();
                twoParam.value2 = StrengthType.ShengPing;
                WinMgr.Singleton.Open<StrengthWindow>(WinInfo.Create(false, null, false, twoParam), UILayer.Popup);
                break;

            case JumpType.PetShengJi:
                {
                    TwoParam<int, StrengthType> p1 = new TwoParam<int, StrengthType>();
                    p1.value1 = _GetDefaultPetId();
                    p1.value2 = StrengthType.ShengJi;
                    WinMgr.Singleton.Open<StrengthWindow>(WinInfo.Create(false, null, false, p1), UILayer.Popup);
                }
                break;
            case JumpType.EquipStrength:
                {
                    if (FuncService.Singleton.TipFuncNotOpen(1101))
                    {
                        // 宠物ID， 装备位置，打开的页签
                        ThreeParam<int, int, int> param = new ThreeParam<int, int, int>();
                        param.value1 = _GetDefaultPetId();
                        param.value2 = jumpParam;
                        param.value3 = 0;
                        WinMgr.Singleton.Open<EquipStrengthWindow>(WinInfo.Create(false, null, false, param), UILayer.Popup);
                    }
                }
                break;
            case JumpType.EquipJueXing:
                {
                    int petId = _GetDefaultPetId();
                    if (petId != -1)
                    {
                        ThreeParam<int, int, int> threeParam = new ThreeParam<int, int, int>();
                        threeParam.value1 = petId;
                        threeParam.value2 = 0;
                        threeParam.value3 = 1;

                        WinMgr.Singleton.Open<EquipStrengthWindow>(WinInfo.Create(false, null, false, threeParam), UILayer.Popup);
                    }

                }
                break;
            case JumpType.ZhanHunStrength:
                {
                    if (FuncService.Singleton.TipFuncNotOpen(1005))
                    {
                        int petId = _GetDefaultPetId();
                        if (petId != -1)
                        {
                            TwoParam<int, StrengthType> p = new TwoParam<int, StrengthType>();
                            p.value1 = petId;
                            p.value2 = StrengthType.ZhanHun;
                            WinMgr.Singleton.Open<StrengthWindow>(WinInfo.Create(false, null, false, p), UILayer.Popup);
                        }
                    }
                }
                break;
            case JumpType.ShengQi:
                {
                    ShenQiService.Singleton.ReqArtifactInfo();
                }
                break;
            case JumpType.EquipBoxChouKa:
                TipWindow.Singleton.ShowTip("跳转装备宝箱抽卡");
                break;
            case JumpType.PetShengXing:
                {
                    TwoParam<int, StrengthType> p3 = new TwoParam<int, StrengthType>();
                    p3.value1 = _GetDefaultPetId();
                    p3.value2 = StrengthType.StarUp;
                    WinMgr.Singleton.Open<StrengthWindow>(WinInfo.Create(false, null, false, p3), UILayer.Popup);
                }
                break;
            case JumpType.PetManager:
                { //判断数量

                    WinMgr.Singleton.Open<GeDouJiaWindow>(null, UILayer.Popup);
                    //owner.OpenChild<GeDouJiaWindow>(WinInfo.Create(true, owner.winName, true));

                }
                break;
            case JumpType.JiuBa:
                WinMgr.Singleton.Open<DrawCardWindow>(WinInfo.Create(true, null, true), UILayer.Popup);
                break;

            case JumpType.QuangHuangZhengBa:
                {
                    WinMgr.Singleton.Open<SH_ExchangeWindow>(null, UILayer.Popup);
                }
                break;

            case JumpType.GuildFightShop:
                TipWindow.Singleton.ShowTip("跳转帮会战兑换商店");
                break;
            case JumpType.JingJiChang:
                {
                    WinMgr.Singleton.Open<ArenaTypeWnd>(null, UILayer.Popup);
                }
                break;
            case JumpType.GongHui:
                {
                    if (FuncService.Singleton.TipFuncNotOpen(1601))
                    {
                        break;
                    }

                    if (RoleService.Singleton.GetRoleInfo().guildId <= 0)
                    {
                        WinMgr.Singleton.Open<JoinGuildMainWnd>();
                    }
                    else
                    {
                        GuildService.Singleton.ReqGuildInfo();

                    }
                }
                break;
            case JumpType.ZhuangBeiJueXingBaoXiang:
                {
                    if (FuncService.Singleton.TipFuncNotOpen(1301))
                        WinMgr.Singleton.Open<ShopMainWindow>(WinInfo.Create(false, null, false, 0), UILayer.Popup);
                }
                break;
            case JumpType.ZaHuoShangDian:
                {
                    if (FuncService.Singleton.TipFuncNotOpen(1302))
                    {
                        WinMgr.Singleton.Open<ShopMainWindow>(WinInfo.Create(false, null, false, (int)ShopService.EShopType.Sundry), UILayer.Popup);
                    }
                }
                break;
            case JumpType.RongYuShangDian:
                {
                    if (FuncService.Singleton.TipFuncNotOpen(1303))
                        WinMgr.Singleton.Open<ShopMainWindow>(WinInfo.Create(false, null, false, (int)ShopService.EShopType.Honor), UILayer.Popup);
                }
                break;
            case JumpType.ShiLianShangDian:
                {
                    if (FuncService.Singleton.TipFuncNotOpen(1304))
                        WinMgr.Singleton.Open<ShopMainWindow>(WinInfo.Create(false, null, false, (int)ShopService.EShopType.Trial), UILayer.Popup);
                }
                break;
            case JumpType.SheTuanShangDian:
                {
                    if (FuncService.Singleton.TipFuncNotOpen(1305))
                        WinMgr.Singleton.Open<ShopMainWindow>(WinInfo.Create(false, null, false, (int)ShopService.EShopType.Guild), UILayer.Popup);
                }
                break;
            case JumpType.JinBiFuBen:
                {
                    if (FuncService.Singleton.TipFuncNotOpen(18021))
                    {
                        ChallegeService.Singleton.ReqActivityActInfo();
                    }
                }
                break;
            case JumpType.ZhongJiShiLian:
                {
                    if (FuncService.Singleton.TipFuncNotOpen(1801))
                    {
                        UltemateTrainService.Singleton.ReqUltemateTrialInfo();
                    }
                }
                break;
            case JumpType.JingYanTiaoZhan:
                {
                    if (FuncService.Singleton.TipFuncNotOpen(18021))
                    {
                        ChallegeService.Singleton.ReqActivityActInfo();
                    }
                }
                break;
            case JumpType.HuanXiangTiaoZhan:
                {
                    if (FuncService.Singleton.TipFuncNotOpen(18021))
                    {
                        ChallegeService.Singleton.ReqActivityActInfo();
                    }
                }
                break;
            case JumpType.TongXiangGuan:
                {
                    if (FuncService.Singleton.TipFuncNotOpen(806))
                    {
                        TongXiangGuanServices.Singleton.ReqExhibitionInfo();
                    }
                }
                break;
            default:
                isHaveJump = false;
                break;
        }

        return isHaveJump;
    }

}