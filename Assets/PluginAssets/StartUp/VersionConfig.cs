/* 
 * -----------------------------------------------
 * Copyright (c) 1ktower.com All rights reserved.
 * -----------------------------------------------
 * 
 * Coder：Zhou XiQuan
 * Time ：2017.10.25
*/

using SimpleJSON;
using System.Collections.Generic;

public class VersionConfig : BaseDownloader
{
    private static VersionConfig instance;
    public static VersionConfig Singleton
    {
        get
        {
            if(instance == null)
                instance = new VersionConfig();
            return instance;
        }
    }

    /// app版本号
    public string AppVersion { get; private set; }
    /// 是否为强更版本（app）
    public bool IsForceUpdateApp { get; private set; }
    ///后台资源版本号
    public string BackResVersion { get; private set; }
    ///强更资源版本号
    public string ForceResVersion { get; private set; }
    ///服务器列表版本号
    public string ServerListVersion { get; private set; }
    ///公告版本号
    public string NoticeVersion { get; private set; }
    /// 是否在审核中
    public static bool IsInAuditing { get; private set; }

    public List<UrlEntry> ConfigUrlList = new List<UrlEntry>();
    public List<UrlEntry> AppUrlList = new List<UrlEntry>();
    public List<UrlEntry> BackResUrlList = new List<UrlEntry>();
    public List<UrlEntry> ForceResUrlList = new List<UrlEntry>();
    public List<UrlEntry> ServerListUrlList = new List<UrlEntry>();
    public List<UrlEntry> NoticeUrlList = new List<UrlEntry>();

    public List<UrlEntry> ForceDownloadUrlList = new List<UrlEntry>();
    public List<UrlEntry> BackDownloadUrlList = new List<UrlEntry>();

    public override void Download()
    {
        string url = getDownloadUrl();
        WWWLoader.Singleton.Download(url, onLoadCmp, onLoadUpdate, System.DateTime.Now.Ticks.ToString(), loadCache);
        //UnityWebLoader.Singleton.Download(url, onLoadCmp, onLoadUpdate, System.DateTime.Now.Ticks.ToString(), true);
    }

    protected override string getDownloadUrl()
    {
        string platform = "android";
#if UNITY_ANDROID
        platform = "android";
#elif UNITY_IPHONE
        platform = "ios";
#else
#endif
        //sdk类型, 渠道id, app版本号，平台
        return string.Format(getOrgUrl(), LocalConfig.Singleton.LocalAppVersion, platform);
    }

    /// 下载完成
    protected override void onLoadCmp(string path, bool success, byte[] data)
    {
        base.onLoadCmp(path, success, data);
        if(!Loaded)
            return;

        if(success && data != null)
        {
            string str = System.Text.Encoding.Default.GetString(data);
            JSONClass json = JSONClass.Parse(str) as JSONClass;
            if(json != null && json["app"] != null)
            {
                //解析
                //版本号
                AppVersion = json["app"].Value;
                IsForceUpdateApp = json["forceApp"].AsBool;
                ServerListVersion = json["serverList"].Value;
                ForceResVersion = json["forceRes"].Value;
                BackResVersion = json["backRes"].Value;
                NoticeVersion = json["notice"].Value;
                IsInAuditing = json["auditing"].AsBool;

                AppUrlList.Clear();
                NoticeUrlList.Clear();
                ConfigUrlList.Clear();
                BackResUrlList.Clear();
                ForceResUrlList.Clear();
                ServerListUrlList.Clear();
                BackDownloadUrlList.Clear();
                ForceDownloadUrlList.Clear();

                string platform = "android";
                #if UNITY_ANDROID
                    platform = "android";
                #elif UNITY_IPHONE
                    platform = "ios";
                #endif

                //网址
                JSONArray arr = json["appUrl"] as JSONArray;
                if(arr != null)
                {
                    for(int i = 0; i < arr.Count; ++i)
                        AppUrlList.Add(new UrlEntry { url = arr[i]["url"].Value, random = arr[i]["random"].AsInt });
                }

                arr = json["configUrl"] as JSONArray;
                if(arr != null)
                {
                    for(int i = 0; i < arr.Count; ++i)
                        ConfigUrlList.Add(new UrlEntry { url = arr[i]["url"].Value, random = arr[i]["random"].AsInt });
                }

                arr = json["backResUrl"] as JSONArray;
                if(arr != null)
                {
                    for(int i = 0; i < arr.Count; ++i)
                        BackResUrlList.Add(new UrlEntry { url = arr[i]["url"].Value, random = arr[i]["random"].AsInt });
                }
                arr = json["forceResUrl"] as JSONArray;
                if(arr != null)
                {
                    for(int i = 0; i < arr.Count; ++i)
                        ForceResUrlList.Add(new UrlEntry { url = arr[i]["url"].Value, random = arr[i]["random"].AsInt });
                }

                arr = json["backDownloadUrl"] as JSONArray;
                if(arr != null)
                {
                    for(int i = 0; i < arr.Count; ++i)
                        BackDownloadUrlList.Add(new UrlEntry { url = string.Format(arr[i]["url"].Value, BackResVersion, platform), random = arr[i]["random"].AsInt });
                }

                arr = json["forceDownloadUrl"] as JSONArray;
                if(arr != null)
                {
                    for(int i = 0; i < arr.Count; ++i)
                        ForceDownloadUrlList.Add(new UrlEntry { url = string.Format(arr[i]["url"].Value, ForceResVersion, platform), random = arr[i]["random"].AsInt });
                }

                arr = json["serverListUrl"] as JSONArray;
                if(arr != null)
                {
                    for(int i = 0; i < arr.Count; ++i)
                        ServerListUrlList.Add(new UrlEntry { url = arr[i]["url"].Value, random = arr[i]["random"].AsInt });
                }
                arr = json["noticeUrl"] as JSONArray;
                if(arr != null)
                {
                    for(int i = 0; i < arr.Count; ++i)
                        NoticeUrlList.Add(new UrlEntry { url = arr[i]["url"].Value, random = arr[i]["random"].AsInt });
                }

                if(mCallback != null)
                    mCallback();
            }else
            {
                Debuger.Log("解析失败文件", GetType().ToString());
                StartupTip.Singleton.TipNoNetwork(GetType().Name, ReDownload);
            }
        }

        if(!success || data == null)
        {
            Debuger.Log("网络错误:" + GetType().ToString());
            StartupTip.Singleton.TipNoNetwork(GetType().Name, ReDownload);
        }
    }
}
