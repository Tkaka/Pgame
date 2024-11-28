
public class FightEndState : FightBaseState
{
    public override void onEnter(object obj = null)
    {
        base.onEnter(obj);
        ActorCamp winCamp = (ActorCamp)obj;
        BattleWindMgr.CurrentBtlWin.BattleEnd(winCamp);
    }

    public override string getStateKey()
    {
        return FightState.End;
    }
}
