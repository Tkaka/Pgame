
public class BaseEffect 
{

    //触发时间
    //1.生效时机   2.生效回合  3.最大生效次数


    public SkillEffectType EftType { private set; get; }

    protected long actorId;

    protected long defenderId;

    //protected float delayTime;     //表现延迟时间

    public virtual void ProcessParam(string param)
    {

    }

    public virtual void Apply(Actor attack, Actor defender)
    {

    }

    /// <summary>
    /// 预计算(用于判断敌人是否即将死亡)
    /// </summary>
    public virtual void PreCompute()
    {

    }

}

