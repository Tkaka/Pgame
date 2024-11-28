
/*
 * file CrossThreadQueue.cs
 *
 * author: Pengmian
 * date:   2014/11/6 
 */

/// <summary>
/// 跨线程处理队列
/// </summary>
using System;
using UnityEngine;
using System.Collections.Generic;

public class CrossThreadQueue
{  
    // 消息队列
    protected BlockingQueue<RMessage> mQueue;

    public CrossThreadQueue()
    {
        mQueue = new BlockingQueue<RMessage>();
    }

    public RMessage peek()
    {
        if (mQueue != null && !mQueue.empty())
        {
            return mQueue.peek();
        }
        return null;
    }

    public void shift()
    {
        mQueue.pop();
    }

    
    /// <summary>
    /// unity3d 调用
    /// </summary>
    public RMessage pop()
    {
        if (mQueue != null &&!mQueue.empty())
        {
            return mQueue.get();
        }
        return null;
    }

    // 网络线程调用
    public void push(RMessage rMsg)
    {
        mQueue.add(rMsg);
    }

    /// <summary>
    /// 退出清空
    /// </summary>
    public void destroy()
    {
        if (mQueue != null)
            mQueue.clear();
    }

    internal int length()
    {
        if (mQueue != null)
            return mQueue.size();
        return 0;
    }
}
