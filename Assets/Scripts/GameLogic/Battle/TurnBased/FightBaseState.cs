
public abstract class FightBaseState : State
{

    protected FightManager fm;

    public override void setOwner(object owner)
    {
        mOwner = owner;
        fm = (FightManager)mOwner;
    }

    public override void onEnter(object obj = null)
    {
        //Logger.log("enter: " + getStateKey());
    }

    public override void onLeave(string stateKey)
    {
       
    }

    public override void onReEnter(object obj = null)
    {
        Logger.err("重复进入状态: " + getStateKey());
    }

    public override void onUpdate()
    {
        
    }

}