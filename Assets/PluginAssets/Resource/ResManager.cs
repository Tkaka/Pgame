/* 
 * -----------------------------------------------
 * Copyright (c) 1ktower.com All rights reserved.
 * -----------------------------------------------
 * 
 * Coder：Zhou XiQuan
 * Time ：2017.09.26
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 资源加载优先级
/// </summary>
public enum ResPriority
{
    /// <summary>
    /// 同步
    /// </summary>
    Sync = 5,
    /// <summary>
    /// 异步
    /// </summary>
    Async = 4,
    /// <summary>
    /// 分帧
    /// </summary>
    Frame = 3,
    /// <summary>
    /// 缓慢
    /// </summary>
    Slow = 2
}

public class ResManager
{
    private static ResManager instance;
    public static ResManager Singleton
    {
        get
        {
            if(instance == null)
                instance = new ResManager();
            return instance;
        }
    }

    //gc策略函数
    private System.Action gcPolicyFunc;

    //统一加载优先级（切图loading时可能需要）
    private bool isPriorityUnited;
    private ResPriority unitePriority = ResPriority.Async;

    //逻辑层的资源
    private Dictionary<string, bool> logicMap = new Dictionary<string, bool>();
    //常驻内存列表
    private Dictionary<string, bool> constResMap = new Dictionary<string, bool>();

    //已加载列表
    private Dictionary<string, GameAsset> loadedAssetsMap = new Dictionary<string, GameAsset>();
    //正在加载任务列表
    private Dictionary<string, ABLoadingTask> loadingTaskMap = new Dictionary<string, ABLoadingTask>();

    /// <summary>
    /// 设置GC策略函数,GC策略函数为空时会有简单的策略
    /// 每次请求资源前都会调用此策略函数
    /// </summary>
    /// <param name="policyFunc">策略函数</param>
    public void SetGCPolicy(System.Action policyFunc)
    {
        gcPolicyFunc = policyFunc;
    }

    /// <summary>
    /// 检查优先级是否合格
    /// </summary>
    private bool _PriorityLegal(ResPriority priority)
    {
        int value = (int)priority;
        return value <= 5 && value >= 2;
    }

    /// <summary>
    /// 请求一个资源
    /// </summary>
    /// <param name="resName">资源名字</param>
    /// <param name="func">回调</param>
    /// <param name="priority">优先级</param>
    /// /// <param name="isLogicReq">是否是游戏逻辑</param>
    public void Request(string resName, System.Action<string> func, ResPriority priority = ResPriority.Async, bool isLogicReq = true)
    {
        if (string.IsNullOrEmpty(resName))
        {
            Debuger.Err("ResManager.Request 资源名字为空");
            if (func != null)
                func(resName);
            return;
        }

        //已加载
        if(loadedAssetsMap.ContainsKey(resName))
        {
            if(func != null)
            {
                try
                {
                    func(resName);
                } catch(System.Exception e)
                {
                    Debuger.Err(e.Message, e.StackTrace);
                }
            }
            return;
        }

        if(false == _PriorityLegal(priority))
        {
            Debuger.Wrn("资源加载优先级不合法");
            priority = ResPriority.Async;
        }

        if(isLogicReq)
            logicMap[resName] = true;

        //是否使用一致的加载优先级
        if(isPriorityUnited)
            priority = unitePriority;

        ///添加资源加载任务
        if(loadingTaskMap.ContainsKey(resName))
        {
            loadingTaskMap[resName].AddCallback(priority, func);
        } else
        {
            try
            {
                //尝试GC
                if(gcPolicyFunc != null)
                    gcPolicyFunc();
                else
                    _SimpleGCPolicy();
            }catch(System.Exception e)
            {
                Debuger.Err(e.Message, e.StackTrace);
            }

            ABLoadingTask abt = new ABLoadingTask();
            loadingTaskMap[resName] = abt;
            abt.Init(resName, _OnABLoaded, func, GetDependences(resName), priority);
        }
    }

    /// <summary>
    /// 统一下载优先级
    /// </summary>
    /// <param name="unite">是否统一</param>
    /// <param name="priority">优先级</param>
    public void UnitePriority(bool unite, ResPriority priority = ResPriority.Async)
    {
        if(false == _PriorityLegal(priority))
        {
            Debuger.Wrn("资源加载优先级不合法");
            return;
        }

        isPriorityUnited = unite;
        unitePriority = priority;
    }

    /// <summary>
    /// 所有资源加载完都会来这里
    /// </summary>
    private void _OnABLoaded(string resName, AssetBundle ab)
    {
        GameAsset ga = null;
        if(!loadedAssetsMap.ContainsKey(resName))
        {
            ga = new GameAsset();
            ga.Init(resName, ab, GetDependences(resName));
            loadedAssetsMap.Add(resName, ga);
        } else
        {
            Debuger.Log("==========资源加载重复了？？？？:" + resName);
            return;
        }

        //移除加载任务
        if(loadingTaskMap.ContainsKey(ga.resName))
        {
            ABLoadingTask abt = loadingTaskMap[ga.resName];
            var callback = abt.GetOutCallBack();
            loadingTaskMap.Remove(ga.resName);
            if(callback != null)
            {
                try
                {
                    callback(resName);
                }catch(System.Exception e)
                {
                    Debuger.Err(e.Message, e.StackTrace);
                }
            }
        }
    }
    
    /// <summary>
    /// 获取一个资源所依赖的资源列表
    /// </summary>
    private string[] GetDependences(string resName)
    {
        return ResDepManager.Singleton.GetDependence(resName);
    }

    /// <summary>
    /// 资源是否已加载
    /// </summary>
    public bool IsResLoaded(string resName)
    {
        if (string.IsNullOrEmpty(resName))
            return false;
        return loadedAssetsMap.ContainsKey(resName);
    }

    /// 添加引用
    internal void AddRef(string resName, bool editorLoadGo = false)
    {
        if(loadedAssetsMap.ContainsKey(resName))
        {
            loadedAssetsMap[resName].AddRef();
#if UNITY_EDITOR
            //如果GameObject引用GameObject
            //编辑器下被引用的GameObject的shader需要手动还原
            if (editorLoadGo)
                loadedAssetsMap[resName].LoadGoAsset();
#endif
        }
        else
        {
            Debuger.Err("找不到资源，无法添加引用 > " + resName);
        }
    }
    


    /// <summary>
    /// 获取已加载的资源
    /// </summary>
    /// <param name="resName">资源名</param>
    /// <param name="depName">内部资源名</param>
    /// <param name="type">内部资源类型</param>
    /// <returns>资源对象</returns>
    public Object GetLoadedObj(string resName, string depName = null, System.Type type = null)
    {
        if (string.IsNullOrEmpty(resName))
        {
            Debuger.Err("ResManager.GetLoadedObj 资源名字为空");
            return null;
        }

        if (string.IsNullOrEmpty(depName))
            depName = resName;

        //从缓存中拿资源
        if(cachePool.ContainsKey(resName) && cachePool[resName].Count > 0)
        {
            GameObject go = cachePool[resName].Pop();
            if(go != null)
            {
                go.SetActive(true);
                return go;
            }
            cachePool[resName].Clear();
            Debuger.Err("缓存中的go已经被销毁: " + resName);
        }

        //从资源列表中查找
        if(loadedAssetsMap.ContainsKey(resName))
        {
            GameAsset ga = loadedAssetsMap[resName];
            ga.AddRef(); //添加引用
            Object o = ga.GetAsset(depName, type);
            if(o is GameObject)
            {
                GameObject go = o as GameObject;
                Object copy = GameObject.Instantiate(go);
                copy.name = go.name;
                return copy;
            }
            return o;
        }
        Debuger.Err("没有你要的资源，请先Request资源:" + resName + "," + depName);
        return null;
    }

    /// <summary>
    /// 获取已加载的资源
    /// </summary>
    /// <param name="resName">资源名</param>
    /// <param name="depName">内部资源名</param>
    /// <param name="type">内部资源类型</param>
    /// <returns>资源对象</returns>
    public T GetLoadedObj<T>(string resName, string depName = null) where T : Object
    {
        return GetLoadedObj(resName, depName, typeof(T)) as T;
    }

    /// <summary>
    /// 异步获取已加载资源的内部资源
    /// </summary>
    /// <param name="resName">资源名字</param>
    /// <param name="depName">内部资源名</param>
    /// <param name="callback">回调函数</param>
    /// /// <param name="priority">加载优先级</param>
    public void GetLoadedObjSync(string resName, string depName, System.Type type, System.Action<string, string, System.Type> callback, ResPriority priority = ResPriority.Async)
    {
        if (string.IsNullOrEmpty(resName))
        {
            Debuger.Err("ResManager.GetLoadedObjSync 资源名字为空");
            if (callback != null)
                callback(resName, depName, type);
            return;
        }

        if (string.IsNullOrEmpty(depName))
            depName = resName;

        //从资源列表中查找
        if(loadedAssetsMap.ContainsKey(resName))
        {
            if(false == _PriorityLegal(priority))
            {
                Debuger.Wrn("资源加载优先级不合法");
                priority = ResPriority.Async;
            }
            GameAsset ga = loadedAssetsMap[resName];
            ga.LoadAsset(depName, type, callback, priority);
        }
    }

    /// <summary>
    /// 获取资源，同步
    /// </summary>
    public Object LoadObjSync(string resName, string depName = null, System.Type type = null)
    {
        if (string.IsNullOrEmpty(resName))
        {
            Debuger.Err("ResManager.LoadObjSync 资源名字为空");
            return null;
        }

        if (loadedAssetsMap.ContainsKey(resName))
            return GetLoadedObj(resName, depName, type);

        Request(resName, null, ResPriority.Sync);
        Object obj = GetLoadedObj(resName, depName, type);
        if(obj != null)
            return obj;

        Debuger.Wrn("ResManager.LoadObjSync 没有找到资源 >> " + resName);
        return null;
    }

    public GameObject LoadObjSync(string resName, Vector3 pos, Quaternion rot)
    {
        if (string.IsNullOrEmpty(resName))
        {
            Debuger.Err("ResManager.LoadObjSync 资源名字为空");
            return null;
        }

        if (!loadedAssetsMap.ContainsKey(resName))
            Request(resName, null, ResPriority.Sync);

        //从资源列表中查找
        if (loadedAssetsMap.ContainsKey(resName))
        {
            GameAsset ga = loadedAssetsMap[resName];
            ga.AddRef(); //添加引用
            Object o = ga.GetAsset(resName, typeof(GameObject));
            if (o is GameObject)
            {
                GameObject go = o as GameObject;
                GameObject copy = GameObject.Instantiate(go, pos, rot);
                copy.name = go.name;
                return copy;
            }else
            {
                if (o != null)
                    Debuger.Err("你要的资源不是一个GameObject", resName);
            }
        }
        return null;
    }

    /// <summary>
    /// 退还资源，减引用
    /// </summary>
    public void ReturnObj(string resName)
    {
        if (string.IsNullOrEmpty(resName))
        {
            Debuger.Err("ResManager.ReturnObj 资源名字为空");
            return;
        }

        if (loadedAssetsMap.ContainsKey(resName))
        {
            loadedAssetsMap[resName].RemoveRef(); //减引用
        } else
        {
            Debuger.Wrn("退还资源不在资源列表里：" + resName);
        }
    }

    #region -----缓存机制-----
    private int cacheMaxNum = 20;
    private Dictionary<string, Stack<GameObject>> cachePool = new Dictionary<string, Stack<GameObject>>();

    /// <summary>
    /// 设置最大缓存GameObject个数
    /// 默认20
    /// </summary>
    /// <param name="num">缓存数量</param>
    public void SetMaxCacheNum(int num)
    {
        cacheMaxNum = num;
        ClearCache(num);
    }

    /// <summary>
    /// 是否有缓存
    /// </summary>
    public bool HasCache(string resName)
    {
        if (string.IsNullOrEmpty(resName))
            return false;

        if (cachePool.ContainsKey(resName))
            return cachePool[resName].Count > 0;
        return false;
    }

    /// <summary>
    /// 回收资源进缓存()
    /// </summary>
    public void RecycleObj(GameObject go)
    {
        if(go == null)
            return;

        string resName = go.name;
        if(!cachePool.ContainsKey(resName))
            cachePool.Add(resName, new Stack<GameObject>());
        //每种资源缓存上线
        if(cachePool[resName].Count >= cacheMaxNum)
        {
            GameObject.DestroyImmediate(go);
            return;
        }

        go.SetActive(false);
        cachePool[resName].Push(go);
        go.transform.SetParent(null, false);
        //从资源列表中查找,需要添加一个引用
        if(loadedAssetsMap.ContainsKey(resName))
        {
            GameAsset ga = loadedAssetsMap[resName];
            ga.AddRef();
        }
    }

    /// <summary>
    /// 清理缓存
    /// </summary>
    /// <param name="leftMax">保留个数</param>
    public void ClearCache(int leftMax = 0)
    {
        leftMax = Mathf.Max(0, leftMax);
        GameAsset ga = null;
        string resName = null;
        var enu = cachePool.GetEnumerator();
        while(enu.MoveNext())
        {
            ga = null;
            resName = enu.Current.Key;
            if(loadedAssetsMap.ContainsKey(resName))
                ga = loadedAssetsMap[resName];
            var cache = enu.Current.Value;
            while(cache.Count > leftMax)
            {
                GameObject obj = cache.Pop();
                GameObject.DestroyImmediate(obj);
                if(ga != null)
                    ga.RemoveRef();
            }
        }
        enu.Dispose();
    }
    #endregion

    #region -----gc模块-----
    /// <summary>
    /// 添加常驻内存资源
    /// </summary>
    public void AddConstRes(string resName)
    {
        if (string.IsNullOrEmpty(resName))
        {
            Debuger.Err("ResManager.AddConstRes 资源名字为空");
            return;
        }

        constResMap[resName] = true;
    }

    /// <summary>
    /// 移除常驻内存资源
    /// </summary>
    public void RemoveConstRes(string resName)
    {
        if (string.IsNullOrEmpty(resName))
        {
            Debuger.Err("ResManager.RemoveConstRes 资源名字为空");
            return;
        }

        if (constResMap.ContainsKey(resName))
            constResMap.Remove(resName);
    }

    /// <summary>
    /// 是否可以gc
    /// </summary>
    private bool _CanGC(string resName)
    {
        if(constResMap.ContainsKey(resName))
            return false;
        return true;
    }

    /// <summary>
    /// GC资源
    /// </summary>
    public void GCObj(string resName)
    {
        if (string.IsNullOrEmpty(resName))
        {
            Debuger.Err("ResManager.GCObj 资源名字为空");
            return;
        }

        if (loadedAssetsMap.ContainsKey(resName))
        {
            if(_CanGC(resName))
            {
                GameAsset ga = loadedAssetsMap[resName];
                if(ga.Unload(true))
                {
                    loadedAssetsMap.Remove(resName);
                }
            }
        } else
        {
            Debuger.Wrn("GC资源不在资源列表里：" + resName);
        }
    }

    /// <summary>
    /// 按名字GC资源
    /// </summary>
    /// <param name="name">资源名</param>
    /// <param name="mustEqual">名字是否需要完全匹配</param>
    /// <param name="gcDep">是否深度GC</param>
    public void GCByName(string name, bool mustEqual = false, bool gcDep = false)
    {
        if (string.IsNullOrEmpty(name))
        {
            Debuger.Err("ResManager.GCByName 资源名字为空");
            return;
        }

        List<string> toGCList = new List<string>();
        ///释放无引用的资源
        string resName = null;
        var enu = loadedAssetsMap.GetEnumerator();
        while(enu.MoveNext())
        {
            resName = enu.Current.Key;
            if(enu.Current.Value.refNum > 0)
                continue;

            if(resName == name || (!mustEqual && resName.Contains(name)))
            {
                if(_CanGC(resName))
                    toGCList.Add(resName);
            }
        }
        enu.Dispose();

        for(int i = 0, len = toGCList.Count; i < len; ++i)
        {
            resName = toGCList[i];
            GameAsset ga = loadedAssetsMap[resName];
            if(ga.Unload(gcDep))
            {
                loadedAssetsMap.Remove(resName);
            }
        }
    }

    /// <summary>
    /// GC所有资源,一般在切场景时调用
    /// </summary>
    /// <param name="depGC">是否深度GC</param>
    public void GC(bool depGC = true)
    {
        ClearCache();

        GameAsset ga = null;
        string resName = null;
        bool hasZeroRef = false;
        List<string> toRemoveList = new List<string>();
        ///释放无引用的资源
        var enu = loadedAssetsMap.GetEnumerator();
        while(enu.MoveNext())
        {
            ga = enu.Current.Value;
            if(ga.refNum > 0)
                continue;

            resName = ga.resName;
            if(_CanGC(resName) && ga.Unload())
            {
                toRemoveList.Add(resName);
                hasZeroRef = true;
            }
        }
        enu.Dispose();

        ///已销毁的从资源列表移除
        for(int i = 0, len = toRemoveList.Count; i < len; ++i)
            loadedAssetsMap.Remove(toRemoveList[i]);
        toRemoveList.Clear();

        ///GC成功则再次GC，引用可能可以回收了
        if(depGC && hasZeroRef)
            GC();
    }

    private int tryGCNum = 0;
    private int gcTriggerNum = 20;
    /// <summary>
    /// 简单GC策略
    /// </summary>
    private void _SimpleGCPolicy()
    {
        tryGCNum++;
        if(tryGCNum < gcTriggerNum)
            return;
        tryGCNum = 0;

        ClearCache(5);
        GCByTime();
    }

    private List<float> gcTimeList = new List<float>();
    /// <summary>
    /// 按时间GC资源
    /// </summary>
    /// <param name="gcNum">逻辑资源gc个数</param>
    public void GCByTime(int gcNum = 20)
    {
        if(gcTimeList.Count == 0)
        {
            //从小到大添加
            gcTimeList.Add(60);           //1分钟
            gcTimeList.Add(60 * 3);       //3分钟
            gcTimeList.Add(60 * 5);       //5分钟
            gcTimeList.Add(60 * 10);      //10分钟
        }

        float now = Time.time;
        Dictionary<string, bool> gcList = new Dictionary<string, bool>();

        //资源分时间段
        GameAsset ga = null;
        float deltaTime = 0;
        for(int i = gcTimeList.Count - 1; i >= 0; --i)
        {
            var enu = loadedAssetsMap.Values.GetEnumerator();
            while(enu.MoveNext())
            {
                ga = enu.Current;
                if(ga.refNum > 0)
                    continue;
                if(gcList.ContainsKey(ga.resName))
                    continue;

                if(logicMap.ContainsKey(ga.resName) && _CanGC(ga.resName))
                {
                    deltaTime = now - ga.lastRefTime;
                    if(deltaTime > gcTimeList[i])
                    {
                        gcList[ga.resName] = true;
                        if(gcList.Count > gcNum)
                            break;
                    }
                }
            }
            enu.Dispose();
            if(gcList.Count > gcNum)
                break;
        }

        //统一GC
        string resName = null;
        var gcEnu = gcList.GetEnumerator();
        while(gcEnu.MoveNext())
        {
            resName = gcEnu.Current.Key;
            ga = loadedAssetsMap[resName];
            if(ga.Unload(true))
            {
                loadedAssetsMap.Remove(resName);
            }
        }
        gcEnu.Dispose();
        gcList.Clear();
    }
    #endregion

    /// <summary>
    /// 打印当前所有资源的引用情况
    /// </summary>
    public void DebugAllRefNow()
    {
        Debuger.Wrn("当前引用计数：----------------------↓↓↓↓--------------------------" + loadedAssetsMap.Count);
        foreach(GameAsset ga in loadedAssetsMap.Values)
            Debuger.Wrn("resName:" + ga.resName + "\trefNum: " + ga.refNum + "\tlastRefTime:" + ga.lastRefTime);
    }
}