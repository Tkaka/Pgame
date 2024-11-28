/* 
 * -----------------------------------------------
 * Copyright (c) 1ktower.com All rights reserved.
 * -----------------------------------------------
 * 
 *  Coder：Zhou XiQuan
 *  Time ：2017.03.11
*/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WWWLoader
{
    private static WWWLoader instance;
    public static WWWLoader Singleton
    {
        get
        {
            if(instance == null)
                instance = new WWWLoader();
            return instance;
        }
    }

    private bool m_usecache;
    public bool UseCache
    {
        get
        {
            return m_usecache;
        }
        set
        {
            m_usecache = value;
            if(value)
                initCache();
        }
    }

    private string listName = "98.98";
    public long MaxCacheFileSize = 1024 * 1024; // 1M
    public long MaxCacheDiskSize = 10 * 1024 * 1024; // 10M

#if UNITY_EDITOR
    private string cachePath = Application.dataPath + "/../98/";
#else
    private string cachePath = Application.persistentDataPath + "/98/";
#endif

    private Dictionary<string, string> cacheVersionMap = new Dictionary<string, string>();

    private void initCache()
    {
        if(!System.IO.Directory.Exists(cachePath))
            System.IO.Directory.CreateDirectory(cachePath);
        if(cacheVersionMap.Count == 0 && System.IO.File.Exists(cachePath + listName))
        {
            string str = System.IO.File.ReadAllText(cachePath + listName);
            if(str != null)
            {
                SimpleJSON.JSONArray json = SimpleJSON.JSONArray.Parse(str) as SimpleJSON.JSONArray;
                if(json.Count > 0)
                {
                    for(int i=0; i<json.Count; ++i)
                        cacheVersionMap.Add(json[i]["name"].Value, json[i]["ver"].Value);
                }
            }
        }
    }

    private void saveCacheList()
    {
        if(cacheVersionMap.Count > 0)
        {
            SimpleJSON.JSONArray json = new SimpleJSON.JSONArray();
            var enu = cacheVersionMap.GetEnumerator();
            while(enu.MoveNext())
            {
                SimpleJSON.JSONClass node = new SimpleJSON.JSONClass();
                node.Add("name", enu.Current.Key);
                node.Add("ver", enu.Current.Value.ToString());
                json.Add(node);
            }
            try{
                System.IO.File.WriteAllText(cachePath + listName, json.ToString());
            }catch(System.Exception e){
                Debuger.Err("WWWLoader cache 写文件失败", e.Message);
            }

            if(cacheVersionMap.Count > MaxCacheDiskSize / MaxCacheFileSize)
                clearCaheToMaxDiskSize();
        }
    }

    /// <summary>
    /// 清理到磁盘上线
    /// </summary>
    private void clearCaheToMaxDiskSize()
    {
        ClearCache();
    }

    /// <summary>
    /// 清理缓存
    /// </summary>
    public void ClearCache()
    {
        initCache();
        if(System.IO.File.Exists(cachePath + listName))
            System.IO.File.Delete(cachePath + listName);
        var enu = cacheVersionMap.GetEnumerator();
        while(enu.MoveNext())
        {
            if(System.IO.File.Exists(cachePath + enu.Current.Key))
                System.IO.File.Delete(cachePath + enu.Current.Key);
        }
        cacheVersionMap.Clear();
    }


    private int nowNum = 0;
    private int maxNum = 10;
    private Dictionary<string, loadInfo> loadingMap = new Dictionary<string, loadInfo>();
    private List<string> loadingList = new List<string>();

    /// <summary>
    /// 下载资源
    /// </summary>
    /// <param name="path">完整路径</param>
    /// <param name="callback">下载完成回调（路径，是否成功，数据）</param>
    /// <param name="onUpdate">下载更新回调（路径，百分比，下载字节数）</param>
    /// <param name="version">版本号（用于控制CDN下载）</param>
    /// <param name="fromCache">是否优先下载本地缓存</param>
    /// <param name="priority">是否优先下载</param>
    public void Download(string path, System.Action<string, bool, byte[]> callback, System.Action<string, float, ulong> onUpdate, string version = null, bool fromCache = false, bool priority = false)
    {
        initCache();
        if(!loadingMap.ContainsKey(path))
        {
            loadInfo info = ClassCacheManager.New<loadInfo>();
            info.useCache = fromCache;
            info.version = version;
            info.updateCb = onUpdate;
            info.cmpCb = callback;
            loadingMap.Add(path, info);
            if(priority)
                loadingList.Insert(0, path);
            else
                loadingList.Add(path);
            if(nowNum < maxNum)
                CoroutineManager.Singleton.startCoroutine(loadQueue());
        }else
        {
            if(loadingMap.ContainsKey(path))
            {
                loadInfo info = loadingMap[path];
                info.cmpCb += callback;
                info.updateCb += onUpdate;
                if(info.version != version)
                {
                    Debuger.Err("已经有不同同版本号的www在加载了", info.version);
                }
            }else
            {
                Debuger.Err("loadingMap中不该没有", path);
            }
        }
    }

    private IEnumerator loadQueue()
    {
        nowNum++;
        float time = 0;
        while(true)
        {
            if(loadingList.Count > 0)
            {
                time = 0;
                string path = loadingList[0];
                loadingList.RemoveAt(0);
                yield return loadwww(path);
                yield return null;//下载之间等一帧
            }else
            {
                time += Time.deltaTime;
                if(time < 180)
                {
                    yield return null;
                } else
                {
                    //3分钟（60 * 3）无加载就释放协成
                    nowNum--;
                    break;
                }
            }
        }
    }

    private WaitForSeconds waitPointOneSecond = new WaitForSeconds(0.1f);
    private IEnumerator loadwww(string path)
    {
        WWW www = null;
        loadInfo info = null;
        Debuger.Log("www下载：" + path);
        string file = System.IO.Path.GetFileName(path);
        if(loadingMap.ContainsKey(path))
            info = loadingMap[path];

        if(info != null)
        {
            bool isFromCache = false;
            if(info.useCache && UseCache)
            {
                if(cacheVersionMap.ContainsKey(file) && cacheVersionMap[file] == info.version)
                {
                    if(System.IO.File.Exists(cachePath + file))
                    {
                        isFromCache = true;
#if UNITY_EDITOR_WIN || UNITY_ANDROID
                        www = new WWW( "file://" + cachePath + file);
#else
                        www = new WWW(cachePath + file);
#endif
                        Debuger.Log("从缓存读取 ", www.url);
                    }
                }
            }

            if(www == null)
            {
                if(string.IsNullOrEmpty(info.version))
                    www = new WWW(path);
                else
                    www = new WWW(path + "?" + info.version);
            }

            yield return waitPointOneSecond;

            while(true)
            {
                try
                {
                    if(info.updateCb != null)
                        info.updateCb(path, www.progress, 0);//info.updateCb(path, www.progress, www.bytesDownloaded);
                } catch(System.Exception e)
                {
                    Debuger.Err(e.Message, e.StackTrace);
                }

                if(!string.IsNullOrEmpty(www.error))
                {
                    if(isFromCache)
                    {
                        //缓存失败，再从服务器拉
                        Debuger.Err("cache 转从服务器拉", file, www.error);
                        isFromCache = false;
                        if(string.IsNullOrEmpty(info.version))
                            www = new WWW(path);
                        else
                            www = new WWW(path + "?" + info.version);
                        if(System.IO.File.Exists(cachePath + file))
                            System.IO.File.Delete(cachePath + file);
                    }else
                    {
                        if(loadingMap.ContainsKey(path))
                            loadingMap.Remove(path);
                        Debuger.Err(www.error);
                        Debuger.Err(path);
                        try
                        {
                            if(info.cmpCb != null)
                                info.cmpCb(path, false, null);
                        } catch(System.Exception e)
                        {
                            Debuger.Err(e.Message, e.StackTrace);
                        }
                        break;
                    }
                } else if(www.isDone)
                {
                    try
                    {
                        if(info.updateCb != null)
                            info.updateCb(path, 1, 0);//info.updateCb(path, 1, www.bytesDownloaded);
                    } catch(System.Exception e)
                    {
                        Debuger.Err(e.Message, e.StackTrace);
                    }

                    if(loadingMap.ContainsKey(path))
                        loadingMap.Remove(path);

                    try
                    {
                        if(info.cmpCb != null)
                            info.cmpCb(path, true, www.bytes);
                    } catch(System.Exception e)
                    {
                        Debuger.Err(e.Message, e.StackTrace);
                    }

                    //写缓存
                    if(UseCache && !isFromCache && info.useCache && www.bytes.Length <= MaxCacheFileSize)
                    {
                        if(!cacheVersionMap.ContainsKey(file))
                            cacheVersionMap.Add(file, info.version);
                        else
                            cacheVersionMap[file] = info.version;
                        saveCacheList();

                        //异步写文件
                        if(System.IO.File.Exists(cachePath + file))
                            System.IO.File.Delete(cachePath + file);
                        System.IO.FileStream fs = System.IO.File.Create(cachePath + file);
                        try
                        {
                            fs.BeginWrite(www.bytes, 0, www.bytes.Length, (ar) =>{
                                fs.EndWrite(ar);
                                fs.Flush();
                                fs.Close();
                                fs.Dispose();
                            }, null);
                        }catch(System.Exception e){
                            Debuger.Err("WWWLoader 写文件失败 ", e.Message);
                        }
                    }
                    yield return null;
                    break;
                } else
                {
                    yield return waitPointOneSecond;
                }
            }
            ClassCacheManager.Delete(ref info);
        }
    }

    private class loadInfo : IClassCache
    {
        public System.Action<string, bool, byte[]> cmpCb;
        public System.Action<string, float, ulong> updateCb;
        public string version;
        public bool useCache;

        public override void FakeDtr()
        {
            base.FakeDtr();
            cmpCb = null;
            updateCb = null;
        }
    }
}