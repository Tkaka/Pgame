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
using System.Collections;
using System.Collections.Generic;
using System.Threading;

public class ABManager
{
    private static ABManager instance;
    public static ABManager Singleton
    {
        get
        {
            if(instance == null)
                instance = new ABManager();
            return instance;
        }
    }

    private JSONArray jsonForce;
    private JSONClass jsonLocal;
    private JSONClass jsonServer;

    private JSONClass jsonLocalSave;
    private JSONClass jsonServerSave;
    private JSONArray jsonChange;

    private string localPath = PathUtil.GetConfigPath() + "local.conf";
    private string serverPath = PathUtil.GetConfigPath() + "server.conf";
    public ABManager()
    {
        ABDownLoader.Singleton.OnWriteComplete = RemoveServerToLocal;
        jsonLocal = new JSONClass();
        jsonServer = new JSONClass();
        jsonForce = new JSONArray();

        jsonLocalSave = new JSONClass();
        jsonServerSave = new JSONClass();
        jsonChange = new JSONArray();

        LoadVersionConf();
    }

    public void LoadVersionConf()
    {
        //已更新资源
        if(System.IO.File.Exists(localPath))
        {
            try { 
                jsonLocal = JSONNode.LoadFromFile(localPath) as JSONClass;
                jsonLocalSave = JSONNode.LoadFromFile(localPath) as JSONClass;
            }catch(Exception e)
            {
                Debuger.Err(e.Message, e.StackTrace);
            }
        }
        if(jsonLocal == null)
        {
            jsonLocal = new JSONClass();
            jsonLocalSave = new JSONClass();
        }

        //未更新资源
        if(System.IO.File.Exists(serverPath))
        {
            try
            {
                jsonServer = JSONNode.LoadFromFile(serverPath) as JSONClass;
                jsonServerSave = JSONNode.LoadFromFile(serverPath) as JSONClass;
            }catch(Exception e)
            {
                Debuger.Err(e.Message, e.StackTrace);
            }
        }
        if(jsonServer == null)
        {
            jsonServer = new JSONClass();
            jsonServerSave = new JSONClass();
        }

        try
        {
            //强更资源
            if(System.IO.File.Exists(PathUtil.ForceFileListOutPath))
                jsonForce = JSONArray.Parse(System.IO.File.ReadAllText(PathUtil.ForceFileListOutPath)) as JSONArray;
        }catch(Exception e)
        {
            Debuger.Err(e.Message, e.StackTrace);
        }
        if(jsonForce == null)
            jsonForce = new JSONArray();
    }

    /// <summary>
    /// 修改服务器资源版本号
    /// </summary>
    public void SetServerVersion(string resName, int version)
    {
        resName = resName.ToLower();
        jsonServer[resName].AsInt = version;

        JSONClass node = new JSONClass();
        node["name"] = resName;
        node["type"].AsInt = 1; //server add
        node["ver"].AsInt = version;
        jsonChange.Add(node);
    }

    /// <summary>
    /// 本地版本号
    /// </summary>
    public int GetLocalVersion(string resName)
    {
        resName = resName.ToLower();
        if(jsonLocal[resName] != null)
            return jsonLocal[resName].AsInt;
        return -1;
    }

    /// <summary>
    /// 服务器版本号
    /// </summary>
    private int GetServerVersion(string resName)
    {
        resName = resName.ToLower();
        if(jsonServer[resName] != null)
            return jsonServer[resName].AsInt;
        return -1;
    }

    private long saveId;
    private long lastSaveTime;
    private long lastBackTime;
    private const long saveCoolTime = 10000 * 1000 * 30;//30秒写一次
    /// <summary>
    /// 服务器资源移动到本地资源列表中
    /// </summary>
    public void RemoveServerToLocal(string resName)
    {
        resName = resName.ToLower();
        SimpleJSON.JSONNode serverVer = jsonServer[resName];
        ///有后缀
        if(serverVer == null)
        {
            if(!resName.Contains("."))
                resName += PathUtil.abSuffix;
            serverVer = jsonServer[resName];
        }
        if(serverVer != null)
        {
            jsonLocal[resName] = serverVer;
            jsonServer.Remove(resName);

            JSONClass node = new JSONClass();
            node["name"] = resName;
            node["type"].AsInt = 2; //server remove to local
            node["ver"].AsInt = serverVer.AsInt;
            jsonChange.Add(node);
        }

        _SaveToLocalJson();
    }

    private void _SaveToLocalJson()
    {
        long now = System.DateTime.Now.Ticks;
        bool allLoaded = ABDownLoader.Singleton.IsFree;
        if(allLoaded || now - lastSaveTime > saveCoolTime)
        {
            lastSaveTime = now;
            //存磁盘
            saveJson(jsonChange);
            jsonChange = new JSONArray();
        }
    }

    /// <summary>
    /// 保存列表
    /// </summary>
    public void SaveVersionToDisk()
    {
        //同步存
        saveJson(jsonChange);
        jsonChange = new JSONArray();
    }

    /// <summary>
    /// 加载资源
    /// </summary>
    /// <param name="resName">资源名字</param>
    /// <param name="callback">回调函数</param>
    /// <param name="async">是否异步</param>
    public void LoadAB(string resName, System.Action<string, AssetBundle> callback, ResPriority priority)
    {
#if UNITY_EDITOR
        ABLoader.Singleton.LoadAssetBundleAsync(resName, callback, priority);
#else    
        SimpleJSON.JSONNode serverVer = jsonServer[resName];
        SimpleJSON.JSONNode localVer  = jsonLocal[resName];

        ///有后缀
        if(serverVer == null && localVer == null)
        {
            string webResName = resName;
            if(!webResName.Contains("."))
                webResName += PathUtil.abSuffix;
            serverVer = jsonServer[webResName];
            localVer = jsonLocal[webResName];
        }

        bool fromServer = true;
        if(jsonForce[resName] != null)
        {
            //强更资源
            fromServer = false;
        }else
        {
            //偷跑资源
            if(serverVer == null)
                fromServer = false;     //未下载清单没有，从本地下载(都没有则从本地读取)
            else if(localVer == null)
                fromServer = true;      //本地没有从服务器下载
            else
                fromServer = serverVer.AsInt > localVer.AsInt; //下载版本号大的(一样大从本地读取)
        }

        //整包只从本地读取
        if(LocalConfig.Singleton.FullPackage)
            fromServer = false;

        if(callback != null)
        {
            if(callbackMap.ContainsKey(resName))
                callbackMap[resName] += callback;
            else
                callbackMap.Add(resName, callback);
        }

        if(fromServer)
        {
            //优先级需要提到最高
            ABDownLoader.Singleton.Load(resName, onServerSuccess, callback, GetServerVersion(resName), true);
            if(priority == ResPriority.Sync)
            {
                Debuger.Err("服务器资源无法同步加载, 先从本地读取替代", resName);
                if(callbackMap.ContainsKey(resName))
                {
                    ABLoader.Singleton.LoadAssetBundle(resName, callbackMap[resName]);
                    callbackMap.Remove(resName);
                }
                else
                {
                    ABLoader.Singleton.LoadAssetBundle(resName, null);
                }
                return;
            }
            priMap[resName] = priority;
        }else
        {
            //容错机制，本地读取失败，则强行从服务器读取
            ABLoader.Singleton.LoadAssetBundleAsync(resName, onLocalABLoaded, priority);
        }
#endif
    }

    private Dictionary<string, ResPriority> priMap = new Dictionary<string, ResPriority>();
    private Dictionary<string, Action<string, AssetBundle>> callbackMap = new Dictionary<string, Action<string, AssetBundle>>();
    /// <summary>
    /// 从服务器下载成功
    /// </summary>
    private void onServerSuccess(string resName)
    {
        if(!callbackMap.ContainsKey(resName))
            return;

        var priority = ResPriority.Async;
        if(priMap.ContainsKey(resName))
        {
            priority = priMap[resName];
            priMap.Remove(resName);
        }

        ABLoader.Singleton.LoadAssetBundleAsync(resName, callbackMap[resName], priority);
        callbackMap.Remove(resName);
    }

    /// <summary>
    /// 从服务器下载失败（可能写本地磁盘失败）
    /// </summary>
    private void onServerFailed(string resName, AssetBundle ab)
    {
        if(!callbackMap.ContainsKey(resName))
            return;

        if(priMap.ContainsKey(resName))
            priMap.Remove(resName);

        try
        {
            callbackMap[resName](resName, ab);
        }catch(Exception e)
        {
            Debuger.Err(e.Message + "\n" + e.StackTrace);
        }
        callbackMap.Remove(resName);
    }

    /// <summary>
    /// 本地加载ab结果
    /// </summary>
    private void onLocalABLoaded(string resName, AssetBundle ab)
    {
        if(!callbackMap.ContainsKey(resName))
            return;

        if(ab != null)
        {
            try
            {
                callbackMap[resName](resName, ab);
            } catch(Exception e)
            {
                Debuger.Err(e.Message + "\n" + e.StackTrace);
            }
            callbackMap.Remove(resName);
        } else
        {
            ABDownLoader.Singleton.Load(resName, onServerSuccess, onServerFailed, GetServerVersion(resName), true);
        }
    }






    /// 保存配置逻辑↓↓↓
    private ThreadHandler thHandler = new ThreadHandler();
    private readonly object wirteLock = new object();
    private void saveJson(JSONArray arr)
    {
         if(!thHandler.IsRunning)
             thHandler.Start(10 * 60 * 1000);//10分钟
         thHandler.PushHandler(threadSave, arr);
    }

    private void threadSave(object obj)
    {
        lock(wirteLock)
        {
            JSONArray arr = obj as JSONArray;
            if(arr != null)
            {
                string resName = "";
                int version = 0;
                var enu = arr.GetEnumerator();
                while(enu.MoveNext())
                {
                    JSONNode node = enu.Current as JSONNode;
                    resName = node["name"].Value;
                    version = node["ver"].AsInt;
                    switch(node["type"].AsInt)
                    {
                        case 1: //设置server版本号
                        jsonServerSave[resName].AsInt = version;
                        break;
                        case 2: //将server移到local
                        jsonServerSave.Remove(resName);
                        jsonLocalSave[resName].AsInt = version; 
                        break;
                    }
                }
            }

            try
            {
                if(System.IO.File.Exists(localPath))
                    System.IO.File.Delete(localPath);
                if(System.IO.File.Exists(serverPath))
                    System.IO.File.Delete(serverPath);

                jsonLocalSave.SaveToFile(localPath);
                jsonServerSave.SaveToFile(serverPath);
            } catch(System.Exception e)
            {
                if(System.IO.File.Exists(localPath))
                    System.IO.File.Delete(localPath);
                if(System.IO.File.Exists(serverPath))
                    System.IO.File.Delete(serverPath);
                try
                {
                    System.IO.File.AppendAllText(localPath + "_errLog", string.Format("时间:{0}\nab列表写入失败，已删除重新下载\nMessage:{1}\nStackTrace:{2}\n\n", System.DateTime.Now.ToString(), e.Message, e.StackTrace));
                }catch(Exception e2)
                {

                }
                //Debuger.Err("写文件出错 ABManager", path, e.Message);
            }
        }
    }
}