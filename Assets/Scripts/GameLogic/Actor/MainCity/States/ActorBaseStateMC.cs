
public abstract class ActorBaseStateMC : State
{
    protected AngelaBaby mActor;

    public override void onEnter(object obj = null)
    {
        mActor = mOwner as AngelaBaby;
        if (mActor != null)
            mActor.GetActionManager().PlayCommonAnimation(getStateKey());
        else
            Logger.err("ActorBaseState Owner Type Error");
    }

    public override void onUpdate()
    {

    }

    public override void onReEnter(object obj = null)
    {

    }

    public override void onLeave(string stateKey)
    {

    }

}
