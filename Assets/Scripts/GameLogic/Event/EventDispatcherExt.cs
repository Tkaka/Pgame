using System;

public class GED
{
    public static EventDispatcherExt ED = new EventDispatcherExt(); //ÓÎÏ·
    public static EventDispatcherExt NED = new EventDispatcherExt(); //ÍøÂç
    public static EventDispatcherExt GuideED = new EventDispatcherExt(); //ĞÂÊÖ
}

public class EventDispatcherExt : GameEventDispatcher
{
    public void removeListener(EventID evtType, Action<GameEvent> handler)
    {
        removeListener((Int32)evtType, handler);
    }

    public void addListener(EventID evtType, Action<GameEvent> handler)
    {
        addListener((Int32)evtType, handler);
    }

    public void addListenerOnce(EventID evtType, Action<GameEvent> handler)
    {
        addListenerOnce((Int32)evtType, handler);
    }

}