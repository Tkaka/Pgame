
public class ActorMoveState : ActorBaseState
{
    public override void onEnter(object obj = null)
    {
        base.onEnter(obj);
    }

    public override void onReEnter(object obj = null)
    {
        base.onReEnter(obj);
    }

    public override void onUpdate()
    {
        base.onUpdate();
        if(mActor.isActorType(ActorType.Player))
            mActor.move();
    }

    public override string getStateKey()
    {
        return ActorState.move;
    }

}