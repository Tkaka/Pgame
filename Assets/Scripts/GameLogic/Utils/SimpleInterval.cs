using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// 定时器（注意最高精度为：一帧的时间 60FPS --> 0.0166666666666667 --> 17ms）
/// 建议调用间隔时间 大于 0.5秒    10FPS --> 100ms  30FPS --> 33ms
/// </summary>
public class SimpleInterval
{

    private long mCoroutineID;                //协程ID

    private float mIntervalTime = 0;         //时间间隔(秒)

    private Action mRunAction = null;      //回调函数

    private int mTimes = -1;                   //当调用 doActionWithTimes 时生效

    private int mExcutedTimes = 0;         //已经执行了次数

    private bool doUpdate = false;

    //是否忽略时间缩放
    private bool unscaled = false;

    public bool IsRunning { get; private set; }

    public SimpleInterval(bool unscaled=false)
    {
        this.unscaled = unscaled;
    }

    public void DoUpdate(Action runAction, bool doImmediately = false)
    {
        doUpdate = true;
        DoAction(Time.deltaTime, runAction, doImmediately);
    }

    public void DoAction(float intervalTime,
                                  Action runAction,
                                  bool doImmediately = false)
    {
        mIntervalTime = intervalTime;
        mRunAction = runAction;
        if (doImmediately && mRunAction != null)
            mRunAction();
        IsRunning = true;
        mCoroutineID = CoroutineManager.Singleton.startCoroutine(OnCoroutine());
    }

    /// <summary>
    /// 指定执行次数
    /// </summary>
    /// <param name="intervalTime"></param>
    /// <param name="runAction"></param>
    /// <param name="param"></param>
    /// <param name="doImmediately"></param>
    /// <param name="times"></param>
    public void DoActionWithTimes(float intervalTime,
                                                  Action runAction,
                                                  bool doImmediately = false,
                                                  int times = 1)
    {
        mTimes = times;
        DoAction(intervalTime, runAction, doImmediately);
    }

    public void ChangeInterval(float intervalTime)
    {
        mIntervalTime = intervalTime;
    }

    private IEnumerator OnCoroutine()
    {
        while (true)
        {
            if (doUpdate)
            {
                yield return null;
            }
            else
            {
                if(unscaled)
                    yield return new WaitForSecondsRealtime(mIntervalTime);
                else
                    yield return new WaitForSeconds(mIntervalTime);
            }
            if (mRunAction != null)
                mRunAction();
            if (mTimes > 0)
            {
                mExcutedTimes++;
                if (mExcutedTimes >= mTimes)
                    Kill();
            }
        }
    }

    /// <summary>
    /// 一定要记住KILL
    /// </summary>
    public void Kill()
    {
        if (IsRunning)
        {
            doUpdate = false;
            mIntervalTime = 0;
            mRunAction = null;
            IsRunning = false;
            mTimes = -1;
            mExcutedTimes = 0;
            CoroutineManager.Singleton.stopCoroutine(mCoroutineID);
        }
    }

}