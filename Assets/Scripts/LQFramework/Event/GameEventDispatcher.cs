
/*
 * file EventDispatcher.cs
 *
 * author: Pengmian
 * date:   2014/10/8
 */

using System;

using System.Collections.Generic;
using System.Linq;

/// <summary>
/// GED: gload event dispatcher
/// lua里也有自己的的事件转发功能，与C#相对独立，主要是为了性能考虑，所以逻辑设计的时候，Lua和C#这边的事件其实是独立的，不相干的
/// </summary>
///  

public class GameEventDispatcher
{ 
    /// <summary>
    /// C#事件
    /// </summary>
    private Dictionary<Int32,Action<GameEvent>> mEventHandlers;
    private Dictionary<Int32,Action<GameEvent>> mEventOnceHandlers;
      
    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="owner">事件分派器所有者</param>
    public GameEventDispatcher()
    { 
        mEventHandlers = new Dictionary<Int32, Action<GameEvent>>();
        mEventOnceHandlers = new Dictionary<Int32, Action<GameEvent>>();
    }

    /// <summary>
    /// 添加一个事件监听
    /// </summary>
    /// <param name="evtType">监听的事件类型</param>
    /// <param name="handler">回调处理</param>
    public void addListener(Int32 evtType, Action<GameEvent> handler)
    {
        addListener(mEventHandlers, evtType, handler);
    }
     
    /// <summary>
    /// 添加一个一次性的事件监听
    /// </summary>
    /// <param name="evtType">监听的事件类型</param>
    /// <param name="handler">回调处理</param>
    public void addListenerOnce(Int32 evtType, Action<GameEvent> handler)
    {
        addListener(mEventOnceHandlers, evtType, handler);
    }

    /// <summary>
    /// 移除一个事件监听
    /// </summary>
    /// <param name="evtType"></param>
    /// <param name="handler"></param>
    public void removeListener(Int32 evtType, Action<GameEvent> handler)
    {
        removeListener(mEventHandlers, evtType, handler);
		removeListener(mEventOnceHandlers, evtType, handler);
    }

    /// <summary>
    /// 分发事件
    /// </summary>
    /// <param name="evtType">Evt type.</param>
    /// <param name="parameter">Parameter.</param> 
    public void dispatchEvent(object evtType, object parameter = null)
    {
        //GameEvent GameEvent = new GameEvent(Convert.ToInt32(evtType), parameter);
        GameEvent evt = GameEventFactory.create();
        evt.EventId = Convert.ToInt32(evtType);
        evt.Data = parameter;
        dispatchEvent(evt);
    }

    /// <summary>
    /// 分派事件
    /// </summary>
    /// <param name="evt"></param>
    public void dispatchEvent(GameEvent evt)
    {
        try
        {
            handleEvent(evt);
        }
        catch(System.Exception e)
        {
            Logger.err(e.ToString());
        }
    }

    /// <summary>
    /// 处理事件
    /// </summary>
    /// <param name="evt"></param>  
    private void handleEvent(GameEvent evt)
    {
        Action<GameEvent> handler = getHandler(mEventHandlers, evt.EventId);
        if (handler != null)
        {
            handler(evt);
        }

        handler = getHandler(mEventOnceHandlers, evt.EventId);
        if (handler != null)
        {
            handler(evt);
            removeListener(mEventOnceHandlers, evt.EventId, handler);
        }
        //evt.recycle();
        GameEventFactory.recycle(evt);
    }

    /// <summary>
    /// 移除事件处理实现
    /// </summary>
    /// <param name="eventHandlers"></param>
    /// <param name="evtType"></param>
    /// <param name="handler"></param>
    private void removeListener(Dictionary<Int32, Action<GameEvent>> eventHandlers, Int32 evtType, Action<GameEvent> handler)
    {
        Action<GameEvent> exists = getHandler(eventHandlers, evtType);
        if (exists != null)
        {
            if (exists.GetInvocationList().Contains(handler))
            {
                exists -= handler;
            }
            //不添加这句，多个事件时不会触发？？？
            eventHandlers[evtType] = exists;
            if (exists==null || exists.GetInvocationList().Length==0)
            {
                eventHandlers.Remove(evtType);
            }
        }
    }
      

    /// <summary>
    /// 添加事件处理实现
    /// </summary>
    /// <param name="eventHandlers"></param>
    /// <param name="evtType"></param>
    /// <param name="handler"></param>
    private void addListener(Dictionary<Int32, Action<GameEvent>> eventHandlers, Int32 evtType, Action<GameEvent> handler)
    {
        Action<GameEvent> exists = getHandler(eventHandlers, evtType);
        if (exists != null)
        {
            if (!exists.GetInvocationList().Contains(handler))
            {
                exists += handler;
            }
            else
            {
                UnityEngine.Debug.LogError("多次添加事件:"+evtType.ToString());
            }
            //不添加这句，多个事件时不会触发？？？
            eventHandlers[evtType] = exists;
        }
        else
        {  
            eventHandlers[evtType] = handler;
        }
    }

    /// <summary>
    /// 返回handler
    /// </summary>
    /// <param name="eventHandlers"></param>
    /// <param name="evtType"></param>
    /// <returns></returns>
    private Action<GameEvent> getHandler(Dictionary<Int32, Action<GameEvent>> eventHandlers, Int32 evtType)
    {
        Action<GameEvent> handler = null;
        eventHandlers.TryGetValue(evtType, out handler);
        return handler;
    }
      
    /// <summary>
    /// 清除所有未分发的事件
    /// </summary>
    public void clear()
    {
        mEventHandlers.Clear();
        mEventOnceHandlers.Clear();
    }

}

