
public class HurtBaseState
{

    protected Actor mActor;

    public HurtSubState HurtState { protected set; get; }

    public virtual void SetOwner(Actor actor, HurtSubState subState)
    {
        mActor = actor;
        HurtState = subState;
    }

    public virtual void OnEnter()
    {

    }

    public virtual void OnReEnter()
    {

    }

    public virtual void OnUpdate()
    {

    }

    public virtual void OnLeave()
    {

    }

}