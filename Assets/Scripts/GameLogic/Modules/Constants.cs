

/// <summary>
/// 道具类型  (-1以下为代币，-1 ：金币 -2 ：钻石 -3：试练币 -4：荣誉币 -5 社团币  1：宝箱 2：钥匙 3：体力药剂 4：升品石 5：角色碎片 6：魂 
/// 道具类型 -1=金币 -2=钻石 -3=红钻 -4=体力  -5=觉醒碎片  -6=荣誉币 -7=试练币 -8=社团币 -9=训练师经验   
/// 1=宝箱 2=钥匙 3=体力药剂  4=升品道具 5=宝贝碎片 6=觉醒石  7=经验药水    8=装备升品卷轴 9=万能碎片 10=经验秘籍 11=经验勋章 
/// 12=装备升星道具装备升星道具碎片 13=装备升星道具碎片 14=玩法系统道具 20=徽章 21=秘籍 22=战魂经验 23=宠物整卡 24=食物 30=抽卡用代金券 31=能量之源 32=喇叭
/// </summary>
public enum ItemType
{
    Gold = -1,
    Damond = -2,
    RedDamond = -3,
    EnergyCurrency = -4,
    AwakeFrag = -5,           //觉醒碎片
    HonorCurrency = -6,      //荣誉碎片
    ShiLianCurrency = -7,
    TeamCurrency = -8,
    PlayerExp = -9,   
    Box = 1,
    Key = 2,
    EnergyYaoJi = 3,
    ShengPinShi = 4,
    PetFragment = 5,
    AwakeStone = 6,
    ExpWater = 7,
    EquipShenPinJuanZhou = 8,
    WangNengFrag = 9,
    ExpBook = 10,
    ExpMedal= 11,
    EquipStarItem = 12,
    EquipStarFrag = 13,
    ShopIten = 14,   //玩法系统道具
    HuiZhang = 20,   //徽章
    MiJi = 21,      //秘籍
    ZhanHunExp = 22,
    PetCard = 23,
    Food = 24,
    ChouKaJun = 30,
    NengLiangYuan = 31,
    LaBa = 32,
}


//道具类型（0：特殊 1：装备材料 2：材料 3：碎片 4：消耗品 5:代币 6:奥义石
public enum ItemCategory
{
    Special = 0,
    EquipMaterial = 1,
    Materials = 2,
    Fragment = 3,
    Consume =4,
    DaiBi = 5,
    AoyiStone= 6,
}
/// <summary>
/// 1.战队等级达成{xx}，2.宠物品质达到{xx}数{xx}，3.关卡星数达到{xx},4.宠物个数达到{xx}，5.完成关卡+ID，6.宠物突破{xx}星的个数{xx}
/// </summary>
public enum ShenQiConditionType
{
    ZhanDuiDengJi = 1, 
    ChongWuPinZhi = 2,
    GuanQiaXingShu = 3,
    ChongWuGeShu = 4,
    WanChengGuanQia = 5,
    ChongWuXingShuGeShu = 6,
}
/// <summary>
/// 终极试炼 加成属性ID（1=攻、2=防、3=格挡、4=暴击、5=吸血、6=反伤、7=单体回怒、8=群体回怒、9=单体回血、10=群体回血、11=复活）
/// </summary>
public enum AdditionPropertyType
{
    Atk = 1,
    Def = 2,
    GeDang = 3,
    BaoJi = 4,
    XiXue = 5,
    FanShang = 6,
    SingleHuiNu = 7,
    AllHuiNu = 8,
    SingleHuiXue = 9,
    AllHuiXue = 10,
    FuHuo = 11
}


//道具跳转类型
public enum ItemJumpType
{
    None,
    PetShengPing = 1,  //宠物升品
    PetShengJi = 2, // 宠物升级
    EquipStrength = 3, //装备强化
    EquipJueXing = 4,  //装备觉醒
    ZhanHunStrength = 5, //战魂强化
    ShengQi = 6,         //神器
    EquipBoxChouKa = 7,  //装备宝箱抽卡
    PetShengXing  =8,    //宝贝升星
    PetManager = 9,      //宝贝管理界面
    JiuBa = 10,        //酒吧界面
    QuangHuangZhengBa = 11, //拳皇争霸商店
    GuildFightShop = 12,   //公会战商店
    MingRenTang = 13,      //名人堂

}
