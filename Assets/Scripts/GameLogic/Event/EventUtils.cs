using System;
using System.Collections.Generic;

public class EventUtils
{

    private Dictionary<int, List<Action<GameEvent>>> evtMap = new Dictionary<int, List<Action<GameEvent>>>();


    /// <summary>
    /// 添加事件
    /// </summary>
    public void addListener(EventID evtId, Action<GameEvent> callBack)
    {
        addListener((int)evtId, callBack);
    }

    /// <summary>
    /// 添加事件
    /// </summary>
    private void addListener(int evtId, Action<GameEvent> callBack)
    {
        GED.ED.addListener(evtId, callBack);
        if (!evtMap.ContainsKey(evtId))
            evtMap.Add(evtId, new List<Action<GameEvent>>());
        if (!evtMap[evtId].Contains(callBack))
            evtMap[evtId].Add(callBack);
    }

    /// <summary>
    /// 移除事件
    /// </summary>
    public void removeListener(EventID evtId, Action<GameEvent> callBack)
    {
        removeListener((int)evtId, callBack);
    }

    /// <summary>
    /// 移除事件
    /// </summary>
    private void removeListener(int evtId, Action<GameEvent> callBack)
    {
        GED.ED.removeListener((int)evtId, callBack);
        if (evtMap.ContainsKey(evtId))
        {
            if (evtMap[evtId].Contains(callBack))
                evtMap[evtId].Remove(callBack);
        }
    }

    public void removeAllListeners()
    {
        foreach (int id in evtMap.Keys)
        {
            if (evtMap.ContainsKey(id))
            {
                List<Action<GameEvent>> list = evtMap[id];
                foreach (Action<GameEvent> act in list)
                    GED.ED.removeListener(id, act);
            }
        }
        evtMap.Clear();
    }

}
