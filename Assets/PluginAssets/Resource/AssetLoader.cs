/* 
 * -----------------------------------------------
 * Copyright (c) 1ktower.com All rights reserved.
 * -----------------------------------------------
 * 
 * Coder：Zhou XiQuan
 * Time ：2017.09.27
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AssetLoader
{
    private static AssetLoader instance;
    public static AssetLoader Singleton
    {
        get
        {
            if(instance == null)
                instance = new AssetLoader();
            return instance;
        }
    }

    private class AssetInfo : IClassCache
    {
        public override bool doCache { get { return true; } }
        public string depName;
        public AssetBundle ab;
        public System.Type type;
        public ResPriority priority;
        public System.Action<string, System.Type> callBack;
        public override void FakeDtr()
        {
            base.FakeDtr();
            ab = null;
            type = null;
            depName = null;
            callBack = null;
        }
    }

    private int nowCoroutineNum = 0;
    private const int maxCoroutineNum = 13;
    private Queue<AssetInfo> toLoadList = new Queue<AssetInfo>();

    /// <summary>
    /// 同步加载ab内部资源
    /// </summary>
    public void LoadAsset(AssetBundle ab, string depName, System.Type type, System.Action<string, System.Type> onCmp)
    {
        if(onCmp != null)
        {
            if(ab != null)
            {
                if(type == null)
                    ab.LoadAsset(depName);
                else
                    ab.LoadAsset(depName, type);
            }
            try
            {
                onCmp(depName, type);
            } catch(System.Exception e)
            {
                Debuger.Err(e.Message, e.StackTrace);
            }
        }
    }

    /// <summary>
    /// 异步加载ab内部资源
    /// </summary>
    public void LoadAssetAsync(AssetBundle ab, string depName, System.Type type, System.Action<string, System.Type> onCmp, ResPriority priority)
    {
        if(priority == ResPriority.Sync)
        {
            //改同步加载
            LoadAsset(ab, depName, type, onCmp);
            return;
        }

        var info = ClassCacheManager.New<AssetInfo>();
        info.ab = ab;
        info.type = type;
        info.depName = depName;
        info.callBack = onCmp;
        info.priority = priority;
        toLoadList.Enqueue(info);
        if(nowCoroutineNum < maxCoroutineNum)
            CoroutineManager.Singleton.startCoroutine(loadAssetAsyncLimited());
    }

    //限制协程数量
    private IEnumerator loadAssetAsyncLimited()
    {
        nowCoroutineNum++;
        while(true)
        {
            if(toLoadList.Count > 0)
            {
                var info = toLoadList.Dequeue();
                yield return loadAssetAsync(info);
            } else
            {
                yield return null;
            }
        }
    }

    /// <summary>
    /// 异步加载ab文件
    /// </summary>
    private IEnumerator loadAssetAsync(AssetInfo info)
    {
        if(info.callBack != null)
        {
            Object o = null;
            if(info.ab != null)
            {
                AssetBundleRequest abr = null;
                if(info.type == null)
                    abr = info.ab.LoadAssetAsync(info.depName);
                else if(info.type == typeof(Sprite))
                    abr = info.ab.LoadAllAssetsAsync();
                else
                    abr = info.ab.LoadAssetAsync(info.depName, info.type);

                abr.priority = (int)info.priority;
                yield return abr;
                o = abr.asset;
            }

            try
            {
                info.callBack(info.depName, info.type);
            }catch(System.Exception e)
            {
                Debuger.Err(e.Message, e.StackTrace);
            }
        }
        ClassCacheManager.Delete(ref info);
    }
}