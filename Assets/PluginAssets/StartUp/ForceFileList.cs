/* 
 * -----------------------------------------------
 * Copyright (c) 1ktower.com All rights reserved.
 * -----------------------------------------------
 * 
 * Coder：Zhou XiQuan
 * Time ：2017.10.25
*/

using System.IO;
using SimpleJSON;
using UnityEngine;
using System.Collections.Generic;

public class ForceFileList : BaseDownloader
{
    private static ForceFileList instance;
    public static ForceFileList Singleton
    {
        get
        {
            if(instance == null)
                instance = new ForceFileList();
            return instance;
        }
    }

    /// <summary>
    /// 检测更新
    /// </summary>
    public void CheckUpdate(string localVersion, System.Action callback)
    {
#if UNITY_EDITOR
        if(callback != null)
            callback();
#else
        Debuger.Log("startUp ", GetType().Name);
        mCallback = callback;
        if(VersionCompare.CompareVersion(mVersion, localVersion) > 0)
        {
            WWWLoader.Singleton.Download(getDownloadUrl() + "?" + mVersion, onLoadCmp, null);
            //UnityWebLoader.Singleton.Download(getDownloadUrl() + "?" + mVersion, onLoadCmp, null);
        } else
        {
            if(mCallback != null)
                mCallback();
        }
#endif
    }

    private JSONClass newJson;
    private JSONArray tmpJson;
    private JSONClass jsonForceUpdated;

    protected int totalSize = 0;
    protected int loadedSize = 0;
    protected int loadedNum = 0;
    protected bool hideLoading = false;
    protected bool checkMD5 = true;
    protected List<UrlEntry> downloadUrlList = new List<UrlEntry>();
    protected string tmpLoadedPath = PathUtil.ForceFileTmpLoadedPath;
    protected Dictionary<string, FileEntry> fileMap = new Dictionary<string, FileEntry>();

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
        if(Loaded)
        {
            checkMD5 = true;
            totalSize = 0;
            loadedSize = 0;
            loadedNum = 0;
            fileMap.Clear();
            if(success && data != null)
            {
                newJson = new JSONClass();
                string forcePath = PathUtil.ForceFileListOutPath;
                //本地强更资源列表
                if(File.Exists(forcePath))
                    jsonForceUpdated = JSONClass.Parse(File.ReadAllText(forcePath)) as JSONClass;
                if(jsonForceUpdated == null)
                    jsonForceUpdated = new JSONClass();

                //临时保存列表
                if(File.Exists(tmpLoadedPath))
                    tmpJson = JSONArray.Parse(File.ReadAllText(tmpLoadedPath)) as JSONArray;
                if(tmpJson == null)
                    tmpJson = new JSONArray();
                List<string> tmpList = new List<string>();
                var enu = tmpJson.GetEnumerator();
                while(enu.MoveNext())
                {
                    JSONNode node = enu.Current as JSONNode;
                    if(node != null && !tmpList.Contains(node.Value))
                        tmpList.Add(node.Value);
                }

                //解析列表
                string str = System.Text.Encoding.UTF8.GetString(data);
                JSONClass json = JSONClass.Parse(str) as JSONClass;
                //string url = "";
                if(json != null && json["files"] != null)
                {
                    JSONArray arr = json["files"] as JSONArray;
                    int size;
                    string md5, res;
                    for(int i=0; i < arr.Count; ++i)
                    {
                        res = arr[i]["res"].Value;
                        md5 = arr[i]["md5"].Value;
                        size = arr[i]["size"].AsInt;

                        //添加到本地列表
                        JSONClass node = new JSONClass();
                        newJson[res] = node;
                        node["md5"] = md5;

                        //本地没有或者md5码不一样则下载
                        if(jsonForceUpdated[res] == null || jsonForceUpdated[res]["md5"].Value != md5)
                        {
                            if(!fileMap.ContainsKey(res))
                            {
                                if(!tmpList.Contains(res))
                                {
                                    fileMap.Add(res, new FileEntry { resName = res, md5 = md5, size = size });
                                    totalSize += size;
                                }
                            } else
                            {
                                Debuger.Err("致命错误，检测到重复的强更资源", res);
                            }
                        }
                    }

                    if(fileMap.Count > 0)
                    {
                        hideLoading = json["hideLoading"].AsBool;
                        if(json["showPopSize"].AsInt <= totalSize / 1024)
                            StartupTip.Singleton.TipForceResUpdate(json["title"].Value, json["tip"].Value, totalSize / 1024, startDownload);
                        else
                            startDownload();
                    } else
                    {
                        if(mCallback != null)
                            mCallback();
                    }
                } else
                {
                    Debuger.Log("下载失败，请检查网络链接 json 解析失败", path);
                    StartupTip.Singleton.TipNoNetwork(GetType().Name, ReDownload);
                }
            } else
            {
                Debuger.Log("下载失败，请检查网络链接 null", path);
                StartupTip.Singleton.TipNoNetwork(GetType().Name, ReDownload);
            }
        }
    }

    /// <summary>
    /// 更新进度
    /// </summary>
    protected override void onLoadUpdate(string path, float progress, ulong loadedBytes)
    {
        int thisLoadedSize = 0;
        string name = Path.GetFileName(path);
        if(fileMap.ContainsKey(name))
            thisLoadedSize = (int)(fileMap[name].size * progress);

        if(hideLoading)
            StartupTip.Singleton.TipProgress(GetType().Name, (thisLoadedSize + loadedSize) / (float)totalSize, 0, true);
        else
            StartupTip.Singleton.TipProgress(GetType().Name, (thisLoadedSize + loadedSize) / 1024f, totalSize / 1024);
    }

    /// <summary>
    /// 开始下载
    /// </summary>
    protected virtual void startDownload()
    {
        loadedNum = 0;
        downloadFileList();
    }

    /// <summary>
    /// 下载列表
    /// </summary>
    protected virtual void downloadFileList()
    {
        var enu = fileMap.GetEnumerator();
        while(enu.MoveNext())
        {
            FileEntry fe = enu.Current.Value;
            string url = getForceFileUrl();
            string resName = fe.resName;
            //UnityWebLoader.Singleton.Download(PathUtil.GetServerPath(url, resName), onFileLoaded, onLoadUpdate, fe.md5);
            WWWLoader.Singleton.Download(PathUtil.GetServerPath(url, resName), onFileLoaded, onLoadUpdate, fe.md5);
        }
        tryComplete();
    }

    private long lastWirteTime;
    protected virtual void tryComplete()
    {
        try
        {
            string path = PathUtil.ForceFileListOutPath;
            //没有可以下载的
            if(fileMap.Count == loadedNum)
            {
                try
                {
                    var enu = newJson.GetEnumerator();
                    while(enu.MoveNext())
                    {
                        var node = (KeyValuePair<string, JSONNode>)enu.Current;
                        ForceManager.Singleton.AddUpdateFile(node.Key);
                        jsonForceUpdated[node.Key] = node.Value;
                    }
                }catch(System.Exception e)
                {
                    Debuger.Err(e.Message, e.StackTrace);
                }
                ForceManager.Singleton.UpdateEnd();

                File.WriteAllText(path, jsonForceUpdated.ToString());
                //ABManager.Singleton.LoadVersionConf();

                if(File.Exists(tmpLoadedPath))
                    File.Delete(tmpLoadedPath);
                dispose();

                if(mCallback != null)
                    mCallback();
            } else
            {
                long now = System.DateTime.Now.Ticks / 10000;
                if(loadedNum % 10 == 0 && now - lastWirteTime > 5000)
                {
                    //5秒或者10个文件写一次
                    lastWirteTime = now;
                    if(File.Exists(tmpLoadedPath))
                        File.Delete(tmpLoadedPath);
                    File.WriteAllText(tmpLoadedPath, tmpJson.ToString());
                }
            }
        } catch(System.Exception e)
        {
            Debuger.Err(e.Message);
        }
    }

    /// <summary>
    /// 下载完成
    /// </summary>
    protected virtual void onFileLoaded(string path, bool success, byte[] data)
    {
        if(success && data != null)
        {
            string name = Path.GetFileName(path);
            byte[] hash = new System.Security.Cryptography.MD5CryptoServiceProvider().ComputeHash(data);
            string md5 = System.BitConverter.ToString(hash).Replace("-", "");
            if(!fileMap.ContainsKey(name))
            {
                Debuger.Err("下载的资源不再强更列表中", path);
                return;
            }

            FileEntry fe = fileMap[name];
            //比较MD5码，验证是否下载完全
            if(fe.md5 == md5 || !checkMD5)
            {
                //保存到本地
                loadedNum++;
                loadedSize += fe.size;
                string mPath = PathUtil.GetForceABPath(name);
                try
                {
                    if(System.IO.File.Exists(mPath))
                        System.IO.File.Delete(mPath);
                    System.IO.File.WriteAllBytes(mPath, data);
                } catch(System.Exception e)
                {
                    Debuger.Err("写文件出错 ForceFileList", path, e.Message);
                    StartupTip.Singleton.TipWriteFileError(GetType().Name, startDownload);
                    return;
                }

                //修改本地列表，断点下载
                tmpJson.Add(name);

                tryComplete();
            } else
            {
                Debuger.Err("文件MD5校验失败，重新下载", path);
                WWWLoader.Singleton.Download(path, onFileLoaded, onLoadUpdate, fe.md5);
                //UnityWebLoader.Singleton.Download(path, onFileLoaded, onLoadUpdate, fe.md5);
            }
        } else
        {
            Debuger.Err("下载失败，请检查网络链接", path);
            StartupTip.Singleton.TipNoNetwork(GetType().Name, startDownload);
        }
    }

    private string getForceFileUrl()
    {
        int all = 0;
        for(int i=0; i < downloadUrlList.Count; ++i)
            all += downloadUrlList[i].random;
        int random = UnityEngine.Random.Range(0, all);

        int weight = 0;
        for(int i = 0; i < downloadUrlList.Count; ++i)
        {
            weight += downloadUrlList[i].random;
            if(random <= weight)
                return downloadUrlList[i].url;
        }

        Debuger.Err("随机地址 没有随机到 强制更新资源下载服务器", random, all);
        if(downloadUrlList.Count > 0)
            return downloadUrlList[0].url;
        return "";
    }

    private void dispose()
    {
        jsonForceUpdated = null;
        newJson = null;
        tmpJson = null;
        fileMap.Clear();
        instance = null;
    }
}
