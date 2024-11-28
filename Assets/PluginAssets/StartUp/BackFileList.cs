/* 
 * -----------------------------------------------
 * Copyright (c) 1ktower.com All rights reserved.
 * -----------------------------------------------
 * 
 * Coder：Zhou XiQuan
 * Time ：2017.10.25
*/
using System;
using SimpleJSON;
using UnityEngine;
using System.Collections.Generic;

public class BackFileList : BaseDownloader
{
    private static BackFileList instance;
    public static BackFileList Singleton
    {
        get
        {
            if (instance == null)
                instance = new BackFileList();
            return instance;
        }
    }

    public override void CheckUpdate(System.Action callback, bool useCache = true)
    {

#if UNITY_EDITOR
       if(callback != null)
           callback();
#else
        base.CheckUpdate(callback, useCache);
#endif
    }

    protected List<FileEntry> fileList = new List<FileEntry>();
    protected List<UrlEntry> downloadUrlList = new List<UrlEntry>();
    protected JSONArray fileArr;
    protected int totalCount;
    protected float m_totalSize = 0;

    public void SetDownloadUrl(List<UrlEntry> list)
    {
        downloadUrlList = list;
    }

    protected override string getDownloadUrl()
    {
        string platform = "android";
#if UNITY_ANDROID
        platform = "android";
#elif UNITY_IPHONE
        platform = "ios";
#endif
        return string.Format(getOrgUrl(), mVersion, platform);
    }

    protected override void onLoadCmp(string path, bool success, byte[] data)
    {
        base.onLoadCmp(path, success, data);
        if (Loaded)
        {
            fileList.Clear();
            if (success && data != null)
            {
                string str = System.Text.Encoding.Default.GetString(data);
                JSONClass json = JSONClass.Parse(str) as JSONClass;
                if (json != null && json["files"] != null && json["urls"] != null)
                {
                    fileArr = json["files"] as JSONArray;
                    compareFiles();

                    //ab下载服务器设置
                    ABManager.Singleton.SaveVersionToDisk();
                    ABDownLoader.Singleton.SetDownLoadServers(downloadUrlList);

                    if (mCallback != null)
                        mCallback();
                }
                else
                {
                    Debuger.Err("偷跑列表下载失败，请检查网络链接", path);
                    CoroutineManager.Singleton.delayedCall(10, ReDownload);
                }
            }
            else
            {
                Debuger.Err("偷跑列表下载失败，请检查网络链接", path);
                CoroutineManager.Singleton.delayedCall(10, ReDownload);
            }
        }
    }

    private void compareFiles()
    {
        if (fileArr == null)
            return;

        m_totalSize = 0;
        fileList.Clear();
        string name = "";
        int ver, level, sz;
        for (int i = 0; i < fileArr.Count; ++i)
        {
            //添加后台需要下载的资源
            ver = fileArr[i]["ver"].AsInt;
            name = fileArr[i]["res"].Value;
            level = fileArr[i]["level"].AsInt;
            sz = fileArr[i]["size"].AsInt;
        
            m_totalSize += sz;

            //需要偷跑的加入下载列表
            if (level > 0 && ver > ABManager.Singleton.GetLocalVersion(name))
            {
                ABManager.Singleton.SetServerVersion(name, ver);
                fileList.Add(new FileEntry { resName = name, version = ver, priority = level, size = sz });
            }
        }
    }

    private int sortFileList(FileEntry f1, FileEntry f2)
    {
        return f2.priority.CompareTo(f1.priority);
    }

    private long checkId;
   /// <summary>
    /// 开始后台下载，优先级大于0才会在后台下载
    /// </summary>
    public void BeginBackDownLoad()
    {
        fileList.Sort(sortFileList);
        CoroutineManager.Singleton.delayedCall(1f, downloadBackList);
    }

    private void downloadBackList()
    {
        Debuger.Log("-----------偷跑长度", fileList.Count);
        if (fileList.Count > 0)
        {
            if(!LocalConfig.Singleton.FullPackage)
            {
                bool isWifi = GameSDK.Singleton.IsWifi();
                //分包
                if(isWifi || fileList[0].priority > 12345)
                {
                    for(int i = 0, len = fileList.Count; i < len; ++i)
                    {
                        if(isWifi || fileList[i].priority > 12345)
                            ABDownLoader.Singleton.Load(fileList[i].resName, null, null, fileList[i].version, false);
                        else
                            break;
                    }
                }

                if(checkId <= 0)
                    checkId = CoroutineManager.Singleton.delayedCall(10f, onCheckFailed);
            }else
            {
                //整包
                for(int i = 0, len = fileList.Count; i < len; ++i)
                {
                    if(fileList[i].priority > 1234567)
                        ABDownLoader.Singleton.Load(fileList[i].resName, null, null, fileList[i].version, false);
                    else
                        break;
                }
            }
        }
        else
        {
            if (checkId > 0)
                CoroutineManager.Singleton.stopCoroutine(checkId);
            checkId = 0;
        }
    }


    private void onCheckFailed()
    {
        checkId = 0;
        if (ABDownLoader.Singleton.IsFree)
        {
            if (GameSDK.Singleton.IsDiskLowMemory())
            {
                //磁盘写满，等待磁盘清空
                Debuger.Log("-----------磁盘空间不足，偷跑稍后再作尝试");
                checkId = CoroutineManager.Singleton.delayedCall(15f, onCheckFailed);
            }
            else
            {
                //中途断网，重新开始下载
                compareFiles();
                if (fileList.Count > 0)
                    downloadBackList();
                else
                    Debuger.Log("-----------偷跑已完成");
            }
        }
        else
        {
            checkId = CoroutineManager.Singleton.delayedCall(10f, onCheckFailed);
        }
    }
}