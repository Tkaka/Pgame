using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 定时器（注意最高精度为：一帧的时间 60FPS --> 0.0166666666666667 --> 17ms）
/// 建议调用间隔时间 大于 0.5秒    10FPS --> 100ms  30FPS --> 33ms
/// </summary>
public class DoActionInterval
{

    private long mCoroutineID;                    //协程ID

    private float mPassedTime = 0;              //已经过去的时间(秒)

    private float mTotalPassedTime = 0;       //总的已经经过的时间

    private float mIntervalTime = 0;             //时间间隔(秒)

    private float mDuration = -1;                   //持续时间(秒)(-1为持续调用)

    private Action<object> mRunAction = null;      //回调函数

    private Action<object> mCmpAction = null;          //Duration结束时候的回调

    private object mCmpParam = null;                 //回调参数

    private object mParam = null;                 //回调参数

    private int mTimes = -1;      //当调用 doActionWithTimes 时生效

    private int mExcutedTimes = 0;    //已经执行了次数

    public bool IsRunning { get; private set; }

    public DoActionInterval() { }

    public void doAction(float intervalTime,
                                  Action<object> runAction,
                                  object param = null,
                                  bool doImmediately = false)
    {
        mIntervalTime = intervalTime;
        mRunAction = runAction;
        mParam = param;
        if (doImmediately && mRunAction != null)
            mRunAction(mParam);
        IsRunning = true;
        mCoroutineID = CoroutineManager.Singleton.startCoroutine(onCoroutine());
    }

    /// <summary>
    /// 指定执行多长时间
    /// </summary>
    /// <param name="intervalTime"></param>
    /// <param name="runAction"></param>
    /// <param name="param"></param>
    /// <param name="doImmediately"></param>
    /// <param name="duration"></param>
    /// <param name="cmpAction"></param>
    public void doActionWithDuration(float intervalTime,
                                                     Action<object> runAction,
                                                     object param = null,
                                                     bool doImmediately = false,
                                                     float duration = -1,
                                                     Action<object> cmpAction = null,
                                                     object cmpParam = null)
    {
        mDuration = duration;
        mCmpAction = cmpAction;
        mCmpParam = cmpParam;
        doAction(intervalTime, runAction, param, doImmediately);
    }

    /// <summary>
    /// 指定执行次数
    /// </summary>
    /// <param name="intervalTime"></param>
    /// <param name="runAction"></param>
    /// <param name="param"></param>
    /// <param name="doImmediately"></param>
    /// <param name="times"></param>
    public void doActionWithTimes(float intervalTime,
                                                  Action<object> runAction,
                                                  object param = null,
                                                  bool doImmediately = false,
                                                  int times = 1)
    {
        mTimes = times;
        doAction(intervalTime, runAction, param, doImmediately);
    }

    public void changeIntervalTime(float intervalTime)
    {
        mIntervalTime = intervalTime;
    }


    private IEnumerator OnCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(mIntervalTime);
        }
    }

    private IEnumerator onCoroutine()
    {
        while (true)
        {
            if (mDuration > 0 && mTotalPassedTime > mDuration)
            {
                if (mCmpAction != null)
                    mCmpAction(mCmpParam);
                kill();
                yield return null;
            }

            if (mPassedTime >= mIntervalTime)
            {
                mPassedTime = 0;
                if (mRunAction != null)
                    mRunAction(mParam);
                if (mTimes > 0)
                {
                    mExcutedTimes++;
                    if (mExcutedTimes >= mTimes)
                        kill();
                }
            }
        
            mPassedTime += Time.deltaTime;
            mTotalPassedTime += Time.deltaTime;

            yield return null;
        }
    }

    public void kill()
    {
        if (IsRunning)
        {
            mPassedTime = 0;
            mIntervalTime = 0;
            mTotalPassedTime = 0;
            mDuration = -1;
            mRunAction = null;
            mCmpAction = null;
            mParam = null;
            mCmpParam = null;
            IsRunning = false;
            mTimes = -1;
            mExcutedTimes = 0;
            CoroutineManager.Singleton.stopCoroutine(mCoroutineID);
        }
    }

}