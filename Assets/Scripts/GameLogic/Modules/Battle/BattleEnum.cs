using DG.Tweening;
using UnityEngine;

//宠物类型
public enum ActorType
{
    Player = 1,             // 玩家
    EnemyPlayer = 2,   //敌对玩家 
    Monster = 3,
    Pet = 4,
    Boss = 5,
    Damageable = 6,  //¿ÉÆÆ»µÎï¼þ
    Build = 7,       //½¨Öþ
}


public enum ColorElement
{
    //Water = 100,       
    //Light = 200,
    //Dark = 300,
    //Fire = 400,
    //Wood = 500,
    //Normal = 600,
    Blue01,
    Yellow01,
    Purple01,
    Purple02,
    Orange01,
    Green01,
    Green02,
    White01,
}


public enum AniName
{
    idle,
    move,
    hurt,
    dead,
    attack,
    skill1,
    skill2,
    skill3,
    skill4,
    skill5,
    fukong,
    jifei,
    xuanyun,
    jidao,
    chuchang,
}


/// <summary>
/// 技能类型 0=普攻，1 = 小技能2=绝技，3=强化被动4=核心技5=通用伤害率6=通用免伤率
/// </summary>
public enum SkillType
{
    NormalAttack = 0,          //小技能
    SmallSkill = 1,               //普攻
    MasterSkill = 2,              //大招
    ThreeSkill = 3,
    CoreSkill = 4,                 //核心被动
    FiveSkill = 5,
    SixSkill = 6,
    ConditionSkill = 7,             //条件替换技能
}

/// <summary>
/// 千万不要在中间添加类型
/// </summary>
public enum SkillTemplate
{
    CommonSkill,               //普通技能
}

public enum BulletModel
{
    Trigger,     //穿透
    Collider,    //碰撞
}

public enum HurtSubState
{
    //XuanYun = -1,            //眩晕(放到Idle去处理)
    Normal = 0,                  //普通伤害
    KnockOut = 1,              //击倒
    Floating = 2,                //浮空
    SmallFly = 3,               //小击飞
    Fly = 4,                       //击飞
}


[System.Serializable]
public class SkillKeyFrame
{
    public enum Type
    {
        Hurt,
        Bullet,                    //普通子弹(每个关键帧触发一个子弹)
        Thunder,                //落雷-按持续时间/攻击间隔计算
        MoveForward,         //位移
        Circle,                    //出圈
        HeadEft,                //头部特效
        AtkEft,                   //攻击特效
        AddBuff,                //添加buff（用于在技能主效果之后的buff添加）
        MoveBackward,
        ColMove,
        PlayAni,                //播放动画
        Splash,                 //播放黑白屏
        CloseShot,            //镜头特写
        FreezeFrame,        //定帧
        BulletHurt,         //子弹伤害
        ShakeCamera,        //震屏
    }

    public SkillKeyFrame()
    {
        this.ease = Ease.Linear;
    }

    public SkillKeyFrame(SkillKeyFrame.Type type)
    {
        this.type = type;
        this.ease = Ease.Linear;
    }

    [Tooltip("关键帧")]
    public int keyFrame;
    public Type type;
    public Ease ease = Ease.Linear;
    public int hurtCount = 1;      //伤害次数
    public ThunderPoint thunderPos = ThunderPoint.OppositeCenter;
    [Tooltip("单边时间")]
    public float ColMoveTime = 0.5f;    //列向移动时间
    public float ColMoveDis = 5.0f;    //列向移动距离
    public BulletModel BulletModel = BulletModel.Collider;
    public HurtSubState hurtState = HurtSubState.Normal;
    public float FreezeTime;     //定帧时间 
    public bool Back = true;             //是否移动回来
}


/// <summary>
/// 站位类型
/// </summary>
public enum StandingPoint
{
    SingleFrontRow,        //单体前排
    SingleBackRow,         //单体后排
    AssistMid,                    //中点辅助位
    AssistCol,                    //列辅助位
    [Tooltip("原地")]
    Original,                   //原地
    OppositeCenter,
    BulletMid,                 //中点子弹
    BulletCol,                  //列子弹 
}

/// <summary>
/// 落雷位置
/// </summary>
public enum ThunderPoint
{
    Target,                   //目标点
    OppositeCenter,      //对方中心点
    ColCenter,              //目标列中点
    FrontRowCenter,     //前排中点
    BackRowCenter,      //后排中点
}

public enum ActorCamp
{
    CampFriend = 100,         //ÓÑ¾üÕóÓª
    CampEnemy = 200,        //µÐ¶ÔÕóÓª, ¿ÉÆÆ»µÎï¼þ
    CampAll = 300,
    Self = 400,                     //×Ô¼º
    CampNeutral = 500,       //ÖÐÁ¢ÕóÓª
}


public class GridEnum
{
    public const int Row0 = 0;
    public const int Row1 = 1;

    public const int Col0 = 0;
    public const int Col1 = 1;
    public const int Col2 = 2;
}