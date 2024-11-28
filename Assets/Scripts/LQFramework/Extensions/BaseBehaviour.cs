/*
 * file BaseBehaviour.cs
 *
 * author: Pengmian
 * date:   2014/09/16 
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 用于所有的Behavior类继承
/// </summary>
public class BaseBehaviour : MonoBehaviour
{
    // 缓存的Component
    protected Map<string, Component> mComponents = new Map<string, Component>();
    // 启动的协程列表
    protected List<long> mCoroutines = new List<long>();
    //5.x版本引擎已经自动缓存
    protected Transform trans;
    // 缓存的GameObject
    protected GameObject mGameObject = null;

    /// 获得一个组件，如果该组件不存在，则添加一个到对象上
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T GetOrAddComponent<T>() where T : Component
    {
        string key = typeof(T).Name;
        Component exists = mComponents.get(key);
        if (exists == null)
        {
            exists = gameObject.GetOrAddComponent<T>();
            mComponents.add(key, exists);
        }
        return (T)exists;
    }


    /// <summary>
    /// 只从缓存的组件中获取，如果该组件不存在，则添加一个到缓存中
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T getComponent<T>() where T : Component
    {
        string key = typeof(T).Name;
        Component exists = mComponents.get(key);
        if (exists == null)
        {
            exists = gameObject.GetComponent<T>();
            if (exists != null)
            {
                mComponents.add(key, exists);
            }
        }
        return (T)exists;
    }

    /// <summary>
    /// 缓存的Transform
    /// </summary>
    public Transform TransformExt
    {
        get
        {
            if (trans == null)
                trans = transform;
            return trans;
        }
    }

    /// <summary>
    /// 缓存的GameObject
    /// </summary>
    public GameObject GameObjectExt
    {
        get
        {
            if (mGameObject == null)
                mGameObject = gameObject;
            return mGameObject;
        }
    }

    protected virtual void Awake()
    {

    }

    protected virtual void Start()
    {
 
    }

    protected virtual void Update()
    {
 
    }

    protected virtual void OnEnable()
    {
 
    }

    protected virtual void OnApplicationPause(bool paused)
    {

    }

    protected virtual void OnApplicationQuit()
    {
 
    }

    protected virtual void OnDisable()
    {
        stopAllCoroutine();
    }

    protected virtual void OnDestroy()
    {
        stopAllCoroutine();
    }

    protected void stopAllCoroutine()
    {
        foreach (long id in mCoroutines)
        {
            CoroutineManager.Singleton.stopCoroutine(id);
        }
        mCoroutines.Clear();
    }

    /// <summary>
    /// 延迟调用
    /// </summary>
    /// <param name="delayedTime"></param>
    /// <param name="callback"></param>
    /// <returns></returns>
    public long delayCall(float delayedTime, Action callback)
    {
        long ret = CoroutineManager.Singleton.delayedCall(delayedTime, callback);
        mCoroutines.Add(ret);
        return ret;
    }

    public long delayCall(float delayedTime, Action<object> callback, object param)
    {
        long ret = CoroutineManager.Singleton.delayedCall(delayedTime, callback, param);
        mCoroutines.Add(ret);
        return ret;
    }

    /// <summary>
    /// 取消一个延迟调用
    /// </summary>
    /// <param name="id"></param>
    public void cancelDelayCall(long id)
    {
        stopCoroutine(id);  
    }

    /// <summary>
    /// 启动一个协程
    /// </summary>
    /// <param name="co"></param>
    /// <returns></returns>
    public long startCoroutine(IEnumerator co)
    {
        long ret = CoroutineManager.Singleton.startCoroutine(co);
        mCoroutines.Add(ret);
        return ret;
    }

    /// <summary>
    /// 停止一个协程
    /// </summary>
    /// <param name="id"></param>
    public void stopCoroutine(long id)
    {
        CoroutineManager.Singleton.stopCoroutine(id);
        int idx = mCoroutines.IndexOf(id);
        if (idx >= 0)
        {
            mCoroutines.RemoveAt(idx);
        }
    }

    /// <summary>
    /// 根据路径获取子节点 
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public Transform getChild(string path)
    {
        return TransformExt.Find(path);
    }

}


