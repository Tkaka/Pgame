
using System;


public class GameEventFactory : ObjectFactory<GameEvent>
{
    public GameEventFactory()
    {
        initialize(20);
    }
}

/// <summary>
/// 游戏事件类
/// </summary>
public class GameEvent : FactoryObj
{

    public bool IsCached { get; set;}

    /// <summary>
    /// 事件类型
    /// </summary>
    public Int32 EventId { get; set; }

    /// <summary>
    /// 事件参数
    /// </summary>
    public object Data { get; set; }

    /// <summary>
    /// 默认构造
    /// </summary>
    public GameEvent()
    {
        EventId = -1;
        Data = null;
    }

    public GameEvent(Int32 id, object data)
    {
        EventId = id;
        Data = data;
    }

    /// <summary>
    /// 实现回收接口
    /// </summary>
    public void recycle()
    {
        EventId = -1;
        Data = null;
    }
}
