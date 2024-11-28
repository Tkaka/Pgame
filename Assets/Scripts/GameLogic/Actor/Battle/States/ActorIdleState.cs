
public class ActorIdleState : ActorBaseState
{

    public override void onEnter(object obj = null)
    {
        mActor = mOwner as Actor;
        if (mActor != null)
        {
            if(!mActor.isActorType(ActorType.Player) && 
                mActor.ViewPropertyMgr.GetProperty(PropertyType.IsDizziness) > 0)
                mActor.GetActionManager().PlayCommonAnimation(AniName.xuanyun.ToString());
            else
                mActor.GetActionManager().PlayCommonAnimation(getStateKey());
        }
        else
        {
            Logger.err("ActorBaseState Owner Type Error");
        }
    }

    public override string getStateKey()
    {
        return ActorState.idle;
    }

}
