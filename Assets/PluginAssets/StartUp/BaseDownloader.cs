/* 
 * -----------------------------------------------
 * Copyright (c) 1ktower.com All rights reserved.
 * -----------------------------------------------
 * 
 * Coder：Zhou XiQuan
 * Time ：2017.10.25
*/

using UnityEngine;
using System.Collections.Generic;

public class BaseDownloader
{
    protected List<int> triedUrlIdxList = new List<int>();
    protected List<UrlEntry> urlList = new List<UrlEntry>();
    public bool Loaded { get; private set; }

    protected System.Action mCallback;
    protected string mVersion;
    protected bool loadCache;

    /// <summary>
    /// 检测更新，下载
    /// </summary>
    public virtual void CheckUpdate(System.Action callback, bool useCache = true)
    {
        Debug.Log("startUp " + GetType().Name);
        triedUrlIdxList.Clear();
        mCallback = callback;
        loadCache = useCache;
        Download();
    }

    public virtual void ReDownload()
    {
        triedUrlIdxList.Clear();
        Download();
    }

    public virtual void Download()
    {
        Debuger.Log("请重写下载接口", GetType().Name);
    }

    /// <summary>
    /// 下载完成
    /// </summary>
    protected virtual void onLoadCmp(string path, bool success, byte[] data)
    {
        if(!success || data == null)
        {
            if(!AllUrlTried())
                Download();
            else
                Loaded = true;
        }

        if(success && data != null)
        {
            Loaded = true;
        }
    }

    /// <summary>
    /// 进度更新
    /// </summary>
    protected virtual void onLoadUpdate(string path, float progress, ulong loadedBytes)
    {
        StartupTip.Singleton.TipProgress(GetType().Name, progress, (long)loadedBytes);
    }

    protected virtual string getDownloadUrl()
    {
        string platform = "android";
#if UNITY_ANDROID
        platform = "android";
#elif UNITY_IPHONE
        platform = "ios";
#endif
        //sdk类型, 渠道id, app版本号，平台
        return string.Format(getOrgUrl(), LocalConfig.Singleton.LocalAppVersion, platform);
    }

    protected virtual string getOrgUrl()
    {
        int all = 0;
        for(int i=0; i < urlList.Count; ++i)
        {
            if(!triedUrlIdxList.Contains(i))
                all += urlList[i].random;
        }
        int random = UnityEngine.Random.Range(0, all);

        int weight = 0;
        for(int i = 0; i < urlList.Count; ++i)
        {
            weight += urlList[i].random;
            if(random <= weight && !triedUrlIdxList.Contains(i))
            {
                triedUrlIdxList.Add(i);
                return urlList[i].url;
            }
        }

        Debug.Log("随机地址 没有随机到:" + GetType().Name +  urlList.Count +  random +  all);
        if(urlList.Count > 0)
            return urlList[0].url;
        return "";
    }

    /// <summary>
    /// 是否所有服务器都尝试过了
    /// </summary>
    public bool AllUrlTried()
    {
        return urlList.Count == triedUrlIdxList.Count;
    }

    /// <summary>
    /// 重置
    /// </summary>
    public virtual void Reset(string version)
    {
        mVersion = version;
        triedUrlIdxList.Clear();
        urlList.Clear();
    }

    /// <summary>
    /// 设置来源服务器列表
    /// </summary>
    public void SetUrlList(List<UrlEntry> list)
    {
        if(list!= null && list.Count > 0)
        {
            urlList.Clear();
            urlList.AddRange(list);
        }
    }
}