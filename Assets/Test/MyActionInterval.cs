using System;
using System.Collections.Generic;

public class MyActionInterval
{

    /// <summary>
    /// 按间隔时间，调用Action
    /// </summary>
    /// <param name="intervalTime">间隔时长（秒）</param>
    /// <param name="runAction">调用的函数</param>
    /// <param name="param">参数</param>
    /// <param name="doImmediately">是否立即执行</param>
    public void DoAction(float intervalTime,
                                  Action<object> runAction,
                                  object param = null,
                                  bool doImmediately = false)
    {

    }

    /// <summary>
    /// 按间隔时间，调用Action,并有时长限定
    /// </summary>
    /// <param name="intervalTime">间隔时长（秒）</param>
    /// <param name="runAction">调用的函数</param>
    /// <param name="param">参数</param>
    /// <param name="doImmediately">是否立即执行</param>
    /// <param name="duration">执行时长</param>
    public void DoActionWithDuration(float intervalTime,
                                                    Action<object> runAction,
                                                    object param = null,
                                                    bool doImmediately = false,
                                                    float duration = -1)
    {

    }

    /// <summary>
    /// 按间隔时间，调用Action,并有次数限定
    /// </summary>
    /// <param name="intervalTime">间隔时长（秒）</param>
    /// <param name="runAction">调用的函数</param>
    /// <param name="param">参数</param>
    /// <param name="doImmediately">是否立即执行</param>
    /// <param name="times">执行次数</param>
    public void DoActionWithTimes(float intervalTime,
                                                  Action<object> runAction,
                                                  object param = null,
                                                  bool doImmediately = false,
                                                  int times = 1)
    {

    }

    /// <summary>
    /// 修改间隔时长
    /// </summary>
    /// <param name="intervalTime"></param>
    public void ChangeIntervalTime(float intervalTime)
    {

    }

    /// <summary>
    /// 终止调用
    /// </summary>
    public void Kill()
    {

    }

}
