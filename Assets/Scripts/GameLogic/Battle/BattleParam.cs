using Data.Beans;

public class BattleParam
{
    /// <summary>
    /// 角色能量上限
    /// </summary>
    public static int MaxMp;

    /// <summary>
    /// 攻击一次得到能量
    /// </summary>
    public static int AtkGetMp;

    /// <summary>
    /// 受击一次得到能量
    /// </summary>
    public static int HurtGetMp;

    /// <summary>
    /// 受到1%伤害得到能量
    /// </summary>
    public static int PercentHurtGetMp;

    /// <summary>
    /// 杀死一个单位得到能量
    /// </summary>
    public static int KillGetMp;

    /// <summary>
    /// 万分比 基础暴击伤害倍率
    /// </summary>
    public static LNumber BaseBaoJiBeiLv;

    /// <summary>
    /// 万分比 基础格挡伤害减免
    /// </summary>
    public static LNumber BaseGeDangJianMian;

    /// <summary>
    /// 万分比 攻击换算战斗力系数
    /// </summary>
    public static LNumber AtkTransFightPower;

    /// <summary>
    /// 万分比 防御换算战斗力系数
    /// </summary>
    public static LNumber DefTransFightPower;

    /// <summary>
    /// 万分比 生命换算战斗力系数
    /// </summary>
    public static LNumber HpTransFightPower;

    /// <summary>
    /// 暴击率换算战斗力系数
    /// </summary>
    public static int BaoJiLvTransFightPower;

    /// <summary>
    /// 抗爆率换算战斗力系数
    /// </summary>
    public static int KangBaoLvTransFightPower;

    /// <summary>
    /// 格挡率换算战斗力系数
    /// </summary>
    public static int GeDangLvTransFightPower;

    /// <summary>
    /// 破击率换算战斗力系数
    /// </summary>
    public static int PoJiLvTransFightPower;

    /// <summary>
    /// 伤害率换算战斗力系数
    /// </summary>
    public static int ShangHaiLvTransFightPower;

    /// <summary>
    /// 免伤率换算战斗力系数
    /// </summary>
    public static int MianShangLvTransFightPower;

    /// <summary>
    /// 等级差对应基础伤害系数
    /// </summary>
    public static LNumber LvDiffHurtFactor;

    /// <summary>
    /// 宝贝升级加成攻击
    /// </summary>
    public static int LvUpAddAtk;

    /// <summary>
    /// 宝贝升级加成防御
    /// </summary>
    public static int LvUpAddDef;

    /// <summary>
    /// 宝贝升级加成生命
    /// </summary>
    public static LNumber LvUpAddHpPer;

    /// <summary>
    /// 小技能触发概率
    /// </summary>
    public static LNumber SmallSkillRate;

    /// <summary>
    /// 每波怪物增加能量值
    /// </summary>
    public static LNumber WaveAddMp;

    /// <summary>
    /// 大招阶段cd
    /// </summary>
    public static int MasterSkillCD;

    /// <summary>
    /// 普通攻击cd
    /// </summary>
    public static int NormalSkillCD;

    /// <summary>
    /// 非活动关卡最大回合数
    /// </summary>
    public static int MaxTurn;


    //金币和金币副本呢关卡最大回合数
    public static int ExpOrCoinMaxTurn;
    /// <summary>
    /// 自动 普通，不错，很好，完美 概率
    /// </summary>
    public static int NormalRate;
    public static int NotBabRate;
    public static int GoodRate;
    public static int PerfectRate;


    /*1	1000	整型 角色能量上限
       2	170	整型 攻击一次得到能量
       3	175	整型 受击一次得到能量
       4	185	整型 杀死一个单位得到能量
       5	5	整型 受到1%伤害得到能量
       6	15000	万分比 基础暴击伤害倍率，使用时除以10000
       7	3000	万分比 基础格挡伤害减免，使用时除以10000
       8	4150	万分比 攻击换算战斗力系数，使用时除以10000
       9	4150	万分比 防御换算战斗力系数，使用时除以10000
       10	 800	万分比 生命换算战斗力系数，使用时除以10000
       11	 360	整型 暴击率换算战斗力系数
       12	 360	整型 抗爆率换算战斗力系数
       13	 500	整型 格挡率换算战斗力系数
       14	 500	整型 破击率换算战斗力系数
       15	 900	整型 伤害率换算战斗力系数
       16	 900	整型 免伤率换算战斗力系数
       17	 250	万分比 等级差对应基础伤害系数, 使用时除以10000
       18	 6	整型 宝贝升级加成攻击
       19	 2	整型 宝贝升级加成防御
       20	 305000	万分比 宝贝升级加成生命，使用时先除以10000
       21	 3000	万分比 小技能出现基础概率
       22	 180		关卡内每波怪物增加能量*/

    public static void Init()
    {
        t_fighting_argsBean args = ConfigBean.GetBean<t_fighting_argsBean, int>(1);
        MaxMp = args.t_value;
        args = ConfigBean.GetBean<t_fighting_argsBean, int>(2);
        AtkGetMp = args.t_value;
        args = ConfigBean.GetBean<t_fighting_argsBean, int>(3);
        HurtGetMp = args.t_value;
        args = ConfigBean.GetBean<t_fighting_argsBean, int>(4);
        KillGetMp = args.t_value;
        args = ConfigBean.GetBean<t_fighting_argsBean, int>(5);
        PercentHurtGetMp = args.t_value;
        args = ConfigBean.GetBean<t_fighting_argsBean, int>(6);
        BaseBaoJiBeiLv = GTools.ScaleInt2LNumber(args.t_value);
        args = ConfigBean.GetBean<t_fighting_argsBean, int>(7);
        BaseGeDangJianMian = GTools.ScaleInt2LNumber(args.t_value);
        args = ConfigBean.GetBean<t_fighting_argsBean, int>(8);
        AtkTransFightPower = GTools.ScaleInt2LNumber(args.t_value);
        args = ConfigBean.GetBean<t_fighting_argsBean, int>(9);
        DefTransFightPower = GTools.ScaleInt2LNumber(args.t_value);
        args = ConfigBean.GetBean<t_fighting_argsBean, int>(10);
        HpTransFightPower = GTools.ScaleInt2LNumber(args.t_value);

        args = ConfigBean.GetBean<t_fighting_argsBean, int>(11);
        BaoJiLvTransFightPower = args.t_value;
        args = ConfigBean.GetBean<t_fighting_argsBean, int>(12);
        KangBaoLvTransFightPower = args.t_value;
        args = ConfigBean.GetBean<t_fighting_argsBean, int>(13);
        GeDangLvTransFightPower = args.t_value;
        args = ConfigBean.GetBean<t_fighting_argsBean, int>(14);
        PoJiLvTransFightPower = args.t_value;
        args = ConfigBean.GetBean<t_fighting_argsBean, int>(15);
        ShangHaiLvTransFightPower = args.t_value;
        args = ConfigBean.GetBean<t_fighting_argsBean, int>(16);
        MianShangLvTransFightPower = args.t_value;
        args = ConfigBean.GetBean<t_fighting_argsBean, int>(17);
        LvDiffHurtFactor = GTools.ScaleInt2LNumber(args.t_value);
        args = ConfigBean.GetBean<t_fighting_argsBean, int>(18);
        LvUpAddAtk = args.t_value;
        args = ConfigBean.GetBean<t_fighting_argsBean, int>(19);
        LvUpAddDef = args.t_value;
        args = ConfigBean.GetBean<t_fighting_argsBean, int>(20);
        LvUpAddHpPer = args.t_value;

        args = ConfigBean.GetBean<t_fighting_argsBean, int>(21);
        SmallSkillRate = args.t_value;
        //SmallSkillRate = 10000;
        args = ConfigBean.GetBean<t_fighting_argsBean, int>(22);
        WaveAddMp = args.t_value;

        args = ConfigBean.GetBean<t_fighting_argsBean, int>(23);
        MasterSkillCD = args.t_value;
        args = ConfigBean.GetBean<t_fighting_argsBean, int>(24);
        NormalSkillCD = args.t_value;
        args = ConfigBean.GetBean<t_fighting_argsBean, int>(25);
        MaxTurn = args.t_value;

        args = ConfigBean.GetBean<t_fighting_argsBean, int>(26);
        ExpOrCoinMaxTurn = args.t_value;
        //
        args = ConfigBean.GetBean<t_fighting_argsBean, int>(27);
        NormalRate = args.t_value;
        args = ConfigBean.GetBean<t_fighting_argsBean, int>(28);
        NotBabRate = args.t_value;
        args = ConfigBean.GetBean<t_fighting_argsBean, int>(29);
        GoodRate = args.t_value;
        args = ConfigBean.GetBean<t_fighting_argsBean, int>(30);
        PerfectRate = args.t_value;

    }

}
