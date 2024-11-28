using System;

public abstract class GameBaseState : State
{

    protected EventUtils mEvtUtils = new EventUtils();

    public override void onReEnter(object obj = null)
    {
        
    }

    public override void onEnter(object obj = null)
    {
        
    }

    public override void onUpdate()
    {
        
    }

    public override void onLeave(string stateKey)
    {
        removeAllListeners();
    }

    /// <summary>
    /// 添加事件
    /// </summary>
    protected void addListener(EventID evtId, Action<GameEvent> callBack)
    {
        mEvtUtils.addListener(evtId, callBack);
    }

    protected void removeListener(EventID evtId, Action<GameEvent> callBack)
    {
        mEvtUtils.removeListener(evtId, callBack);
    }

    protected void removeAllListeners()
    {
        mEvtUtils.removeAllListeners();
    }

}
