
public class ShowChangeProperty : ShowBase
{

    public LNumber value;

    public PropertyType propertyType = PropertyType.Hp;

    //改变 还是 赋值
    public bool isChange = true;

    public override void Show()
    {
        Actor actor = ActorManager.Singleton.Get(actorId);
        if (actor != null)
        {
            if (isChange)
            {
                if (propertyType == PropertyType.Hp)
                {
                    actor.changeState(ActorState.hurt, HurtSubState.Normal);
                    if(value > 0)
                        HurtNumberMgr.Singleton.Emit(actor, NumberType.Cure, (long)value, false);
                    else
                        HurtNumberMgr.Singleton.Emit(actor, NumberType.Hurt, (long)value, false);
                }
                actor.ViewPropertyMgr.ChangeProperty(propertyType, value);

            }
            else
            {
                actor.ViewPropertyMgr.SetProperty(propertyType, value);
            }
        }
    }

}