/*
 * file State.cs
 *
 * author: Pengmian
 * date:   2014/10/9
 * 
 * Edit by liqiang 
 * date:   2015/12/17
 */

/// <summary>
/// 状态
/// </summary>
using System;
using System.Collections;

public abstract class State
{
    protected object mOwner;

    /// <summary>
    /// 状态拥有者
    /// </summary>
    /// <param name="owner"></param>
    public virtual void setOwner(object owner)
    {
        mOwner = owner;
    }

    /// <summary>
    /// 返回状态拥有者
    /// </summary>
    /// <returns></returns>
    public object getOwner()
    {
        return mOwner;
    }

    public abstract void onReEnter(object obj=null);

    /// <summary>
    /// 进入状态
    /// </summary>
    /// <param name="owner"></param>
    public abstract void onEnter(object obj = null);

    /// <summary>
    /// 状态更新
    /// </summary>
    /// <param name="owner"></param>
    public abstract void onUpdate();

    /// <summary>
    /// 状态结束
    /// </summary>
    /// <param name="owner"></param>
    public abstract void onLeave(string stateKey);

    /// <summary>
    /// 返回状态ID
    /// </summary>
    public abstract string getStateKey();

}

