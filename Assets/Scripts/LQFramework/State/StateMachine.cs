using System;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 状态机
/// </summary>
/// <typeparam name="T">状态机拥有者类型</typeparam>

public class StateMachine
{

    public const string InvalidState = "Invalid";

    protected Map<string, State> mStateCache;
    protected State mCurrentState = null;
    protected State mLastState = null;
    protected State mGlobalState = null;
    protected object mOwner;

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="owner"></param>
    public StateMachine(object owner)
    {
        mStateCache = new Map<string, State>();
        mOwner = owner;
    }

    public bool isExist(string stateKey)
    {
        return mStateCache.ContainsKey(stateKey);
    }

    public State getStateByKey(string stateKey)
    {
        return mStateCache.get(stateKey);
    }


    /// <summary>
    /// 设置拥有者
    /// </summary>
    /// <param name="owner"></param>
    public void setOwner(object owner)
    {
        mOwner = owner;
    }

    /// <summary>
    /// 注册状态
    /// </summary>
    /// <param name="type">状态类型</param>
    /// <param name="state">状态</param>
    public void registerState(string key, State state)
    {
        State exists = mStateCache.get(key);
        if (exists == null)
        {
            mStateCache.add(key, state);
        }
        else
        {
            mStateCache.Container[key] = state;
        }
    }


    public void setGlobalState(State state, object obj = null)
    {
        mGlobalState = state;
        mGlobalState.setOwner(mOwner);
        mGlobalState.onEnter(obj);
    }


    /// <summary>
    /// 移除状态
    /// </summary>
    /// <param name="type"></param>
    public void removeState(int id)
    {
        mStateCache.remove(id.ToString());
    }

    /// <summary>
    /// 改变状态
    /// </summary>
    /// <param name="type"></param>
    public virtual void changeState(string key, object obj = null)
    {
        State newState = mStateCache.get(key);
        if (newState == null)
        {
            Debug.LogError("unregister state type: " + key);
            return;
        }

        if (mCurrentState != null)
        {
            mCurrentState.onLeave(newState.getStateKey());
            //Logger.dbg(mOwner.ToString() + "from:[" + mCurrentState.getStateId().ToString() + "] to: [" + id.ToString() + "]");
        }

        mLastState = mCurrentState;
        mCurrentState = newState;
        mCurrentState.setOwner(mOwner);
        mCurrentState.onEnter(obj);
    }

    /// <summary>
    /// 更新
    /// </summary>
    public virtual void update()
    {
        if (mGlobalState != null)
            mGlobalState.onUpdate();
        if (mCurrentState != null)
            mCurrentState.onUpdate();
    }

    /// <summary>
    /// 当前状态类型 
    /// </summary>
    /// <returns></returns>
    public string getCurrentState()
    {
        if (mCurrentState != null)
        {
            return mCurrentState.getStateKey();
        }
        return StateMachine.InvalidState;
    }

    /// <summary>
    /// 释放
    /// </summary>
    public void clear()
    {
        if (mCurrentState != null)
            mCurrentState.onLeave(StateMachine.InvalidState);
        mStateCache.Container.Clear();
        mCurrentState = null;
        mLastState = null;
    }

}
