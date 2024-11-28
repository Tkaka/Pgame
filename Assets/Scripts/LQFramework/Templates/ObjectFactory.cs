
/*
 * file ObjectFactory.cs
 *
 * author: Pengmian
 * date: 2014/11/11   
 */

using System.Collections.Generic;


/// <summary>
/// 工厂对象必须实现的接口
/// </summary>
public interface FactoryObj
{
    bool IsCached { get; set; }
    void recycle();
}


/// <summary>
/// 对象工厂模版（用于减少频繁的new操作）
/// </summary>
/// <typeparam name="T"></typeparam>
public class ObjectFactory<T> where T : FactoryObj, new()
{

    protected static Stack<T> mFreeList = new Stack<T>();

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="size"></param>
    public static void initialize(int size)
    {
        T t;
        for (int a = 0; a < size; ++a)
        {
            t = new T();
            t.IsCached = true;
            mFreeList.Push(t);
        }
    }

    /// <summary>
    /// 创建对象
    /// </summary>
    /// <returns></returns>
    public static T create()
    {
        T t;
        if (mFreeList.Count <= 0)
        {
            for (int a = 0; a < DefaultSize; ++a)
            {
                t = new T();
                t.IsCached = true;
                mFreeList.Push(t);
            }
        }

        if (mFreeList.Count <= 0)
        {
            t = default(T);
            t.IsCached = false;
            return default(T);
        }

        t = mFreeList.Pop();
        t.IsCached = true;
        return t;
    }

    /// <summary>
    /// 回收对象
    /// </summary>
    /// <param name="obj"></param>
    public static void recycle(T obj)
    {
        if (obj.IsCached)
        {
            obj.recycle();
            mFreeList.Push(obj);
        }
        else
        {
            obj.recycle();
        }
    }

    /// <summary>
    /// 默认大小
    /// </summary>
    public static int DefaultSize = 10;

}

