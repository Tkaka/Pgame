public class HurtKnockOutState : HurtBaseState
{
    public override void OnEnter()
    {
        base.OnEnter();
        if (mActor != null)
            mActor.GetActionManager().PlayCommonAnimation(AniName.jifei.ToString(), OnActionFinish);
        else
            Logger.err("ActorHurtState Owner Type Error");
    }
    public override void OnReEnter()
    {
        mActor.GetActionManager().PlayCommonAnimation(AniName.jifei.ToString(), OnActionFinish);
    }

    private void OnActionFinish(int key)
    {
        mActor.changeState(ActorState.idle);
    }

}
