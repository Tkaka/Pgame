/* 
 * Coder：Zhou XiQuan
 * Time ：2017.09.19
*/
using System;
using System.Collections.Generic;

//使用方法：使用GetNew来new一个对象，使用Recycle来回收自己不要的对象
public static class ClassCacheManager
{
    private static Dictionary<IClassCache, bool> m_checkMap = new Dictionary<IClassCache, bool>();
    private static Dictionary<Type, Queue<IClassCache>> m_cacheMap = new Dictionary<Type, Queue<IClassCache>>();

    /// <summary>
    /// 通过类缓存New一个对象
    /// </summary>
    public static T New<T>(IParam param = null) where T : IClassCache, new()
    {
        Type t = typeof(T);
        Queue<IClassCache> queue = null;
        if(m_cacheMap.TryGetValue(t, out queue) && queue != null)
        {
            if(queue.Count > 0)
            {
                T cache = (T)queue.Dequeue();
                cache.FakeCtr(param);
                if(m_checkMap.ContainsKey(cache))
                    m_checkMap.Remove(cache);
                return cache;
            }
        }

        T ret = new T();
        ret.FakeCtr(param);
        return ret;
    }

    /// <summary>
    /// 通过类缓存New一个对象
    /// </summary>
    public static T New<T>(bool doCache, IParam param = null) where T : IClassCache, new()
    {
        var ret = New<T>(param);
        ret.doCache = doCache;
        return ret;
    }

    /// <summary>
    /// 不再使用的类对象放入缓存
    /// </summary>
    public static void Delete<T>(ref T obj) where T : IClassCache
    {
        if(obj == null)
        {
            Logger.err("CClassCacheManager Delete的对象为空");
            return;
        }

        //是否缓存
        if(!obj.doCache)
        {
            obj.FakeDtr();
            obj = null;
            return;
        }

        if(m_checkMap.ContainsKey(obj))
        {
            Logger.err("CClassCacheManager Delete 对象重复缓存");
            return;
        }

        Type type = obj.GetType();
        if(!m_cacheMap.ContainsKey(type))
            m_cacheMap.Add(type, new Queue<IClassCache>());

        if(m_cacheMap[type].Count < 50) //暂且定一个上限
        {
            m_cacheMap[type].Enqueue(obj);
            m_checkMap[obj] = true;
        }
        obj.FakeDtr();
        obj = null;
    }
}