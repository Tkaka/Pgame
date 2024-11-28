
/*******Actor基本状态********/
public class ActorState
{
    public const string dead = "dead";
    public const string idle = "idle";
    public const string move = "move";
    public const string mc_move = "mc_move";
    public const string hurt = "hurt";
    public const string attack = "attack";
}

/*******ActorHurt子状态********/
public class ActorHurtSubState
{
    public const string ShouShang = "shoushang";     //普通受伤
    public const string HunMi = "hunmi";                   //昏迷
}

public enum Profession
{
    Boy = 100,
    Girl = 200,   
}

/// <summary>
/// 种族：1=火、2=水、3=草、4=妖精、5=格斗、6=飞行、7=鬼、8=超能力、9=虫、10=龙、11=电、12=大地
/// </summary>
public enum ActorRace
{
    Huo = 1,
    Shui,
    Cao,
    YaoJing,
    GeDou,
    FeiXing,
    Gui,
    ChaoNengLi,
    Chong,
    Long,
    Dian,
    DaDi,
}

/// <summary>
/// 1= 攻 2 = 防 3 = 技
/// </summary>
/*public enum ActorGrowType
{
    Atk = 1,    
    Def,
    Skill,
}*/