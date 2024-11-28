

//11=恶虎之魂、12=黑虎之魂、13=虎狼之魂、14=金虎之魂、15=猛虎之魂、16=虎狼之魂、
//21=蝮蛇之魂、22=灵蛇之魂、23=蛟蛇之魂、24=翠蛇之魂、25=岚蛇之魂、26=暴蛇之魂、
//31=野熊之魂、32=幻熊之魂、33=暴熊之魂、34=斗熊之魂、35=雷熊之魂、
//41=赤龟之魂、42=神龟之魂、43=玄武之魂、44=真武之魂、45=王霸之魂、
//51=本源之魂、
//61=真空之魂、
//100=毒草之魂、101=猎鹰之魂、102=坚石之魂

public enum SoulDetailType
{
    EHu = 11,
    HeiHu = 12,
    YanHu = 13,
    JinHu = 14,
    MengHu = 15,
    BaiHu = 16,
    HuLang = 17,

    FuShe = 21,
    LingShe = 22,
    JiaoShe = 23,
    CuiShe = 24,
    LanShe = 25,
    BaoShe = 26,

    YeXiong = 31,
    HuanXiong = 32,
    BaoXiong = 33,
    DouXiong = 34,
    LeiXiong = 35,

    ChiGui = 41,
    ShenGui = 42,
    XuanWu = 43,
    ZhenWu = 44,
    WangBa = 45,

    BenYuan = 51,
    ZhenKong = 61,
 
    DuCao = 100,
    LieYing = 101,
    JianShi = 102,

}


//战魂类型1=虎之魂、2=蛇之魂、3=熊之魂、4=龟之魂、5=本源之魂、6=真空之魂
//100=毒草之魂、101=猎鹰之魂、102=坚石之魂

public enum SoulType
{
    None = 0,       //无 
    Hu = 1,
    She = 2,
    Xiong = 3,
    Gui = 4,
    BenYuan = 5,
    ZhenKong = 6,
    DuCao = 100,
    LieYing = 101,
    JianShi = 102,
}
