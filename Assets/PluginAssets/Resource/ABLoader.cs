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

public class ABLoader
{
    private static ABLoader instance;
    public static ABLoader Singleton
    {
        get
        {
            if(instance == null)
                instance = new ABLoader();
            return instance;
        }
    }

    private int nowCoroutineNum = 0;
    private const int maxCoroutineNum = 13;
    private Dictionary<string, System.Action<string, AssetBundle>> loadingMap = new Dictionary<string, System.Action<string, AssetBundle>>();
    private Dictionary<string, ResPriority> loadingPriMap = new Dictionary<string, ResPriority>();
    private Queue<string> toLoadList = new Queue<string>();

    /// <summary>
    /// 同步加载ab文件
    /// </summary>
    public void LoadAssetBundle(string resName, System.Action<string, AssetBundle> onCmp)
    {
        string path = PathUtil.GetABPath(resName);
        AssetBundle ab = AssetBundle.LoadFromFile(path);
        if(onCmp != null)
        {
            try
            {
                onCmp(resName, ab);
            }catch(System.Exception e)
            {
                Debuger.Err(e.Message, e.StackTrace);
            }
        }
    }

    /// <summary>
    /// 异步加载ab文件
    /// </summary>
    public void LoadAssetBundleAsync(string resName, System.Action<string, AssetBundle> onCmp, ResPriority priority)
    {
        if(priority == ResPriority.Sync)
        {
            //改同步加载
            if(loadingMap.ContainsKey(resName))
            {
                onCmp += loadingMap[resName];
                loadingMap.Remove(resName);
            }
            LoadAssetBundle(resName, onCmp);
            return;
        }

        if(!loadingMap.ContainsKey(resName))
        {
            toLoadList.Enqueue(resName);
            loadingMap.Add(resName, onCmp);
            loadingPriMap[resName] = priority;
            if(nowCoroutineNum < maxCoroutineNum)
                CoroutineManager.Singleton.startCoroutine(loadABAsyncLimited());
        } else
        {
            //如果已经在加载了，那么只需添加回调
            loadingMap[resName] += onCmp;
            //还没开始加载修改优先级
            if(loadingPriMap.ContainsKey(resName))
                loadingPriMap[resName] = priority;
        }
    }

    //限制协程数量
    private IEnumerator loadABAsyncLimited()
    {
        nowCoroutineNum++;
        while(true)
        {
            if(toLoadList.Count > 0)
            {
                string resName = toLoadList.Dequeue();
                yield return loadABAsync(resName);
            } else
            {
                yield return null;
            }
        }
    }

    /// <summary>
    /// 异步加载ab文件
    /// </summary>
    private IEnumerator loadABAsync(string resName)
    {
        string path = PathUtil.GetABPath(resName);
        AssetBundleCreateRequest abc = AssetBundle.LoadFromFileAsync(path);
        abc.priority = (int)loadingPriMap[resName];
        loadingPriMap.Remove(resName);
        yield return abc;

        if(loadingMap.ContainsKey(resName))
        {
            //异步加载时同步加载来了，会在同步加载时回调
            System.Action<string, AssetBundle> cb = loadingMap[resName];
            if(cb != null)
            {
                try
                {
                    cb(resName, abc.assetBundle);
                } catch(System.Exception e)
                {
                    Debuger.Err(e.Message, e.StackTrace);
                }
            } else
            {
                Debug.LogError("加载ab文件，怎么可能没有回调：" + resName);
            }

            if(loadingMap.ContainsKey(resName))
                loadingMap.Remove(resName);
        }
    }
}