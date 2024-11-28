//击杀怒气
//初始怒气
//1	攻击
//2	防御
//3	生命
//4	暴击率
//5	抗爆率
//6	暴击强度
//7	格挡率
//8	破击率
//9	格挡强度
//10	伤害率
//11	免伤率
//12	能量
//13	吸血率
//14	治疗率
//15	治疗效果
//16	控制率
//17	免控率
//18	绝技伤害率
//19	绝技防御率
//20	伤害反弹率
//21	对攻伤害率
//22	对技伤害率
//23	对防伤害率
//24	对攻免伤率
//25	对技免伤率
//26	对防免伤率
//27	能量回复速度
//28	抵抗率
//29	血量型抵抗率
//30	Debuff免疫概率
public enum PropertyType
{
    //基本属性
    None,
    Atk = 1,                          // 攻击	
    Def = 2,                          // 防御	
    Hp = 3,                           // 生命		
    BaoJiLv = 4,                      // 暴击率
    KangBaoJiLv = 5,                  // 抗暴击率
    BaoJiQiangDu = 6,                 // 暴击强度
    GeDangLv = 7,                     // 格挡率
    PoJiLv = 8,                       // 破击率
    GeDangQiangDu = 9,                // 格挡强度
    ShangHaiLv = 10,               
    MianShangLv = 11,
    Mp = 12,
    XiXueLv = 13,
    ZhiLiaoLv = 14,
    ZhiLiaoXiaoGuo = 15,
    KongZhiLv = 16,
    MianKongLv = 17,
    JueJiShangHaiLv = 18,
    JueJiFangYuLv = 19,             //绝技抗性/防御率
    GongJiLv = 20,                     // 攻击率
    FangYuLv = 21,                     // 防御率
    ShangHaiFanTanLv = 22,
    DuiGongShangHaiLv = 23,
    DuiJiShangHaiLv = 24,
    DuiFangShangHaiLv = 25,
    DuiGongMianShangLv = 26,
    DuiJiMianShangLv = 27,
    DuiFangMianShangLv = 28,
    NengLiangHuiHuSuDu = 29,
    DiKangLv = 30,
    XueLiangXingDiKangLv = 31,
    DebuffMianYiLv = 32,
    SmallSkillRateAdd = 33,                 // 小技能概率加成
    KillGetMp = 34,                              // 击杀获得能量增加值
    AtkGetMp = 35,                             // 攻击获得能量增加值
    HurtGetMp = 36,                            // 受击获得能量增加值
    FriendDeadGetMp = 37,                 // 每有队友死亡时获得能量  
    EnemyDeadGetMp = 38,                // 敌方死亡时获得 
    TurnGetMp = 39,                           // 每回合回复能量
    MaxPropertyType = 40,                  //最大属性id（用于循环遍历用）

    /*************************************************************/

    //逻辑计算用
    HurtAdd1 = 100,                     //伤害加成类型1
    HurtAdd2 = 101,                     //伤害加成类型2

    //buff标记
    IsNumbness = 200,                       //是否麻痹
    IsDizziness = 201,                         //是否眩晕
    IsIce = 202,                                  //是否冰冻
    ImmuneCtrlPriority = 203,              //免疫控制优先级
    ImmuneDebuffPriority = 204,          //免疫debuff优先级
    IsSilence = 205,                             //是否沉默
    FuHuoLv = 206,                              //复活概率
    HasBehead = 207,                           //是否有斩杀/致命
    CanAttack = 208,                            //能否攻击 

    BuCuo = 800,                    // 不错
    HenBang = 801,                   // 很棒
    WanMei = 802,                    // 完美
 
}

public enum EPropertyFlag
{
    NULL,
    Base,       // 基础属性
    Attach,     // 额外基础
    Percent,    // 百分比
}