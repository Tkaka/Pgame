
public class ShowDead : ShowBase
{

    public bool fromBuff = false;

    public override void Show()
    {
        Actor actor = ActorManager.Singleton.Get(actorId);
        if (actor != null)
        {
            actor.changeState(ActorState.dead, fromBuff);
            //Logger.err("dead show: " + actor.Name + actor.getActorId());
        }
    }

}
