
public class FightDialogState : FightBaseState
{

    public override void onEnter(object obj = null)
    {
        base.onEnter(obj);
        GED.ED.addListenerOnce(EventID.OnBattleDialogWindowClose, OnDialogWindowClose);
        BattleWindow.Singleton.OpenChild<BattleDialogWindow>(WinInfo.Create(false, null, false));
    }

    private void OnDialogWindowClose(GameEvent evt)
    {
        fm.PrepareNextTurn(ChangeReason.DialogCmp);
    }

    public override string getStateKey()
    {
        return FightState.BossEnter;
    }

}

