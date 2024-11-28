

public class HurtNormalState : HurtBaseState
{

    public override void OnEnter()
    {
        base.OnEnter();
        if (mActor != null)
            mActor.GetActionManager().PlayCommonAnimation(ActorState.hurt, OnActionFinish);
        else
            Logger.err("ActorHurtState Owner Type Error");
    }
    public override void OnReEnter()
    {
        mActor.GetActionManager().PlayCommonAnimation(ActorState.hurt, OnActionFinish);
    }

    private void OnActionFinish(int key)
    {
        mActor.changeState(ActorState.idle);
    }

}
