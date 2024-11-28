/* 
 * -----------------------------------------------
 * Copyright (c) 1ktower.com All rights reserved.
 * -----------------------------------------------
 * 
 * Coder：Zhou XiQuan
 * Time ：2017.10.25
*/

using System.IO;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ABDownLoader
{
    private static ABDownLoader instance;
    public static ABDownLoader Singleton
    {
        get
        {
            if(instance == null)
                instance = new ABDownLoader();
            return instance;
        }
    }

    private ThreadHandler thHandler = new ThreadHandler();
    private List<UrlEntry> serverList = new List<UrlEntry>();

    /// <summary>
    /// 服务器地址
    /// </summary>
    public void SetDownLoadServers(List<UrlEntry> list)
    {
        serverList = list;
    }

    protected virtual string getDownloadUrl()
    {
        int all = 0;
        for(int i = 0; i < serverList.Count; ++i)
            all += serverList[i].random;
        int random = UnityEngine.Random.Range(0, all);

        int weight = 0;
        for(int i = 0; i < serverList.Count; ++i)
        {
            weight += serverList[i].random;
            if(random <= weight)
                return serverList[i].url;
        }

        Debuger.Err("随机地址 没有随机到 > ABDownloader", random, all);
        if(serverList.Count > 0)
            return serverList[0].url;
        return "";
    }

    public System.Action<string> OnWriteComplete;
    private Dictionary<string, byte[]> writingMap = new Dictionary<string, byte[]>();
    private Dictionary<string, WirteInfo> wroteMap = new Dictionary<string, WirteInfo>();
    private Dictionary<string, LoaderCb> loadingMap = new Dictionary<string, LoaderCb>();

    /// <summary>
    /// 是否处于空闲状态
    /// </summary>
    public bool IsFree
    {
        get
        {
            return writingMap.Count + loadingMap.Count + toLoadList.Count == 0;
        }
    }

    /// <summary>
    /// 下载资源，下载并写入磁盘成功后逻辑自行从磁盘加载资源，可节约内存暂用
    /// </summary>
    /// <param name="resName">资源名</param>
    /// <param name="onSuccess">成功回调（下载并且写入磁盘）</param>
    /// <param name="onFailed">失败回调（下载或者写入磁盘）</param>
    /// <param name="version">版本号</param>
    /// <param name="important">是否优先下载</param>
    public void Load(string resName, System.Action<string> onSuccess, System.Action<string, AssetBundle> onFailed, int version = 0, bool important = true)
    {
        if(!loadingMap.ContainsKey(resName))
        {
            //添加到加载列表
            loadingMap.Add(resName, new LoaderCb { failedCb = onFailed, successCb = onSuccess });
            if(important)
                toLoadList.Insert(0, new LoadInfo { resName = resName, version = version });
            else
                toLoadList.Add(new LoadInfo { resName = resName, version = version });

            if(nowCoroutineNum < maxCoroutineNum)
                CoroutineManager.Singleton.startCoroutine(downloadResourceLimited());
        } else
        {
            //正在下载或者写磁盘
            loadingMap[resName].failedCb += onFailed;
            loadingMap[resName].successCb += onSuccess;
        }
    }

    private int nowCoroutineNum;
    private const int maxCoroutineNum = 2;
    private List<LoadInfo> toLoadList = new List<LoadInfo>();

    private WaitForSeconds waitOneSecond = new WaitForSeconds(1f);
    private WaitForSeconds waitPointOneSecond = new WaitForSeconds(0.1f);

    private IEnumerator downloadResourceLimited()
    {
        nowCoroutineNum++;
        while(true)
        {
            if(toLoadList.Count > 0)
            {
                LoadInfo li = toLoadList[0];
                toLoadList.RemoveAt(0);
                //已经下载了就不用下载了(逻辑层先于偷跑下载可能出现)
                if(!writingMap.ContainsKey(li.resName))
                    yield return downloadResource(li.resName, li.version);
            } else
            {
                yield return waitOneSecond;
            }
        }
    }

    /// <summary>
    /// 下载文件
    /// </summary>
    private IEnumerator downloadResource(string resName, int version)
    {
        //?用于cdn版本号
        WWW download = new WWW(PathUtil.GetServerPath(getDownloadUrl(), resName) + "?" + version);
        //UnityWebRequest download = new UnityWebRequest(PathUtil.GetServerPath(getDownloadUrl(), resName) + "?" + version, UnityWebRequest.kHttpVerbGET, new DownloadHandlerBuffer(), null);
        yield return download;

        //if (download.isError || download.responseCode != 200)
        if(!string.IsNullOrEmpty(download.error))
        {
            //下载失败
            Debuger.Err(download.error, download.url);
            if(loadingMap.ContainsKey(resName))
            {
                var cb = loadingMap[resName];
                try
                {
                    if(cb.failedCb != null)
                        cb.failedCb(resName, null);
                } catch(System.Exception e)
                {
                    Debuger.Err(e.Message, e.StackTrace);
                }
                loadingMap.Remove(resName);
            }
        } else
        {
            byte[] data = download.bytes;
            while(true)
            {
                // 等待写线程写入完成
                if(!writingMap.ContainsKey(resName))
                {
                    // 先加到写队列，通知线程写入
                    writingMap.Add(resName, data);
                    writeToDisk(resName, data);
                } else
                {
                    WirteInfo info = null;
                    wroteMap.TryGetValue(resName, out info);
                    if(info != null && info.deal == false)
                    {
                        // 写文件已完成   
                        info.deal = true;
                        var cb = loadingMap[resName];
                        writingMap.Remove(resName);
                        loadingMap.Remove(resName);
                        try
                        {
                            if(info.result)
                            {
                                if(OnWriteComplete != null)
                                    OnWriteComplete(resName);
                                if(cb.successCb != null)
                                    cb.successCb(resName);
                            } else
                            {
                                if(cb.failedCb != null)
                                    cb.failedCb(resName, download.assetBundle);
                            }
                        } catch(System.Exception e)
                        {
                            Debuger.Err(e.Message, e.StackTrace);
                        }
                        break;
                    } else
                    {
                        yield return waitPointOneSecond;
                    }
                }
            }
        }
        download.Dispose();
        yield return null;
    }

    ///写磁盘异步
    private void writeToDisk(string resName, byte[] downloadData)
    {
        if(!thHandler.IsRunning)
            thHandler.Start(10 * 60 * 1000); //10分钟
        thHandler.PushHandler(threadSave, new SaveInfo { data = downloadData, res = PathUtil.GetBackABPath(resName) });
    }

    private void threadSave(object obj)
    {
        SaveInfo si = obj as SaveInfo;
        if(si != null)
            save(si.data, si.res);
    }

    private readonly object wirteLock = new object();
    private class SaveInfo
    {
        public byte[] data;
        public string res;
    }

    private void save(byte[] data, string fullPath)
    {
        lock(wirteLock)
        {
            string resName = Path.GetFileName(fullPath);
            string resName2 = Path.GetFileNameWithoutExtension(fullPath);
            try
            {
                File.WriteAllBytes(fullPath, data);
                if(wroteMap.ContainsKey(resName))
                {
                    wroteMap[resName].deal = false;
                    wroteMap[resName].result = true;

                    wroteMap[resName2].deal = false;
                    wroteMap[resName2].result = true;
                } else
                {
                    wroteMap.Add(resName, new WirteInfo { deal = false, result = true });
                    wroteMap.Add(resName2, new WirteInfo { deal = false, result = true });
                }
            } catch(System.Exception e)
            {
                if(File.Exists(fullPath))
                    File.Delete(fullPath);

                if(wroteMap.ContainsKey(resName))
                {
                    wroteMap[resName].deal = false;
                    wroteMap[resName].result = false;
                } else
                {
                    wroteMap.Add(resName, new WirteInfo { deal = false, result = false });
                }
                //Debuger.Err("写文件出错 ABDownLoader", resName, e.Message);
            }
        }
    }

    private class WirteInfo
    {
        //是否已处理
        public bool deal;
        //是否写成功
        public bool result;
    }

    private struct LoadInfo
    {
        public string resName;
        public int version;
    }

    private class LoaderCb
    {
        public System.Action<string> successCb;
        public System.Action<string, AssetBundle> failedCb;
    }
}