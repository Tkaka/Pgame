using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class NoneBlockingSequenceQueue<TKey, TVal> where TKey : class, IComparable
{
    private List<TKey> mSequence;
    private Map<TKey, TVal> mQueue;

    public NoneBlockingSequenceQueue()
    {
        mSequence = new List<TKey>();
        mQueue = new Map<TKey, TVal>();
    }

    /// <summary>
    /// 添加一个元素
    /// </summary>
    /// <param name="obj"></param>
    public void add(TKey key, TVal val)
    {
        if (!mQueue.ContainsKey(key))
        {
            mSequence.Add(key);
            mQueue.add(key, val);
        }
        else
        {
            Logger.err("重复添加元素: " + key.ToString());
        }

    }

    /// <summary>
    /// 根据Key返回值
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public TVal peek(TKey key)
    {
        return mQueue.get(key);
    }

    public TVal peek()
    {
        TVal ret = default(TVal);
        if (mSequence .Count > 0)
        {
            TKey key = mSequence[0];
            ret = mQueue.get(key);
        }
        return ret;
    }



    /// <summary>
    /// 获得下一个元素
    /// </summary>
    /// <returns></returns>
    public TVal get()
    {
        TVal ret = default(TVal);
        if (mSequence.Count > 0)
        {
            TKey key = mSequence[0];
            ret = mQueue.get(key);

            mQueue.remove(key);
            mSequence.RemoveAt(0);

        }
        return ret;
    }

    /// <summary>
    /// 队列是否为空
    /// </summary>
    /// <returns></returns>
    public bool empty()
    {
        return mSequence.Count <= 0;
    }

    /// <summary>
    /// 清空队列
    /// </summary>
    public void clear()
    {
        mSequence.Clear();
        mQueue.clear();
    }

    /// <summary>
    /// 对len长度的对象，调用action
    /// </summary>
    /// <param name="len"></param>
    /// <param name="action"></param>
    public void runAction(int len, Action<TVal> action)
    {
        int cur = Math.Min(len, mSequence.Count);
        for (int a = 0; a < cur; ++a)
        {
            TVal val = get();
            if (val != null)
            {
                action(val);
            }
        }
    }

    /// <summary>
    /// 删除元素
    /// </summary>
    /// <param name="key"></param>
    public void remove(TKey key)
    {
        for (int a = 0; a < mSequence.Count; ++a)
        {
            if (mSequence[a].CompareTo(key) == 0)
            {
                mSequence.RemoveAt(a);
                mQueue.remove(key);
                break;
            }
        }
    }

    /// <summary>
    /// 返回内部序列
    /// </summary>
    public List<TKey> Container
    {
        get { return mSequence; }
    }

    /// <summary>
    /// 在指定的位置插入
    /// </summary>
    /// <param name="idx"></param>
    /// <param name="key"></param>
    /// <param name="val"></param>
    public void insert(int idx, TKey key, TVal val)
    {
        mSequence.Insert(idx, key);
        mQueue.add(key, val);
    }
}

