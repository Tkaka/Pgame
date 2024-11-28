/* 
 * -----------------------------------------------
 * Copyright (c) 1ktower.com All rights reserved.
 * -----------------------------------------------
 * 
 * Coder：Zhou XiQuan
 * Time ：2017.10.24
*/

using System;
using System.IO;
using SimpleJSON;
using UnityEngine;
using System.Collections.Generic;

public class LocalConfig
{
    private static LocalConfig instance;
    public static LocalConfig Singleton
    {
        get
        {
            if(instance == null)
                instance = new LocalConfig();
            return instance;
        }
    }
    /// <summary>
    /// 是否是全量包
    /// </summary>
    public bool FullPackage { get; private set; }
    /// <summary>
    /// 本地版本号
    /// </summary>
    public string LocalAppVersion { get; private set; }
    /// <summary>
    /// 本地资源版本号
    /// </summary>
    public string LocalForceResVersion { get; private set; }
    /// <summary>
    /// 包内版本号大于包外版本号
    /// 说明是有覆盖安装，可能需要将老包内的全部资源解压出来
    /// 清理之前的强更资源
    /// </summary>
    public bool IsNewApp { get; private set; }
    //当前使用的配置
    private JSONClass usingJson = null;
    //所有配置
    private JSONClass json = null;

    private const string FORMAL_KEY = "formal";
    private const string LOCAL_CONF_KEY = "LC_URL_ADREE_Key"; 

    private bool localFake;
    private string cachePath = "";
    public List<UrlEntry> ConfigUrlList = new List<UrlEntry>();

    public void Init()
    {
        json = null;
        IsNewApp = false;
        // 默认情况下为全量包
        FullPackage = true;

        //初始化路径
        string confPath = PathUtil.GetConfigPath();
        cachePath = confPath + PathUtil.BuildInVersionConfigName;
        if(!Directory.Exists(confPath))
            Directory.CreateDirectory(confPath);

        //初始化包内和包外json配置
        JSONClass jsonOut = null;
        JSONClass jsonIn = null;
        if(File.Exists(cachePath))
        {
            try{
                //包外文件可能被修改，需要try一下
                string str = File.ReadAllText(cachePath);
                jsonOut = JSONClass.Parse(str) as JSONClass;
            }catch(Exception e)
            {
                Debuger.Err(e.Message, e.StackTrace);
            }
        }
        TextAsset ta = Resources.Load<TextAsset>(Path.GetFileNameWithoutExtension(PathUtil.BuildInVersionConfigName));
        if(ta != null)
            jsonIn = JSONClass.Parse(ta.text) as JSONClass;

        //获取当前配置
        string fakeName = PlayerPrefs.GetString(LOCAL_CONF_KEY, FORMAL_KEY);
        localFake = fakeName != FORMAL_KEY;
        JSONClass confJsonOut = null;
        if(jsonOut != null)
            confJsonOut = jsonOut[fakeName] as JSONClass;
        JSONClass confJsonIn = null;
        if(jsonIn != null)
            confJsonIn = jsonIn[fakeName] as JSONClass;

        //判断用外面还是用里面
        if(confJsonOut == null || confJsonOut["appVer"] == null)
        {
            //外部没有，首包首次启动
            json = jsonIn;
            usingJson = confJsonIn;
        }else if(confJsonIn != null && VersionCompare.CompareVersion(confJsonIn["appVer"].Value, confJsonOut["appVer"].Value) > 0)
        {
            //外部版本号低于内部版本号，更新后首次启动
            json = jsonIn;
            usingJson = confJsonIn;
        }else
        {
            //一般情况
            json = jsonOut;
            usingJson = confJsonOut;
        }

        //解析ConfigUrl
        if(usingJson != null && usingJson["appVer"] != null)
        {
            ConfigUrlList.Clear();
            if(localFake)
                Debuger.Log("你发现了一个惊天大秘密,当前fake配置", fakeName);

            LocalAppVersion = usingJson["appVer"].Value;
            FullPackage = usingJson["fullPackage"].AsBool;
            LocalForceResVersion = usingJson["forceResVer"].Value;
            JSONArray arr = usingJson["configUrl"] as JSONArray;
            if(arr != null)
            {
                for(int i = 0; i < arr.Count; ++i)
                    ConfigUrlList.Add(new UrlEntry { url = arr[i]["url"].Value, random = arr[i]["random"].AsInt });
            } else
            {
                Debuger.Err("ConfigUrl 配置错误");
            }
        }

        if(confJsonIn != null && confJsonOut != null)
        {
            //内部资源版本号比外面高，则需要全部解压出来
            if(VersionCompare.CompareVersion(confJsonIn["forceResVer"].Value, confJsonOut["forceResVer"].Value) > 0)
            {
                LocalForceResVersion = confJsonIn["forceResVer"].Value;
                IsNewApp = true;
            }
        }

        if(LocalAppVersion == null)
        {
            //本地配置失效
            Debuger.Err("读取本地版本号失败");
            LocalAppVersion = "0.0.0";
            LocalForceResVersion = "0.0.0";
            FullPackage = false;
        }

        var enu = json.GetEnumerator();
        while(enu.MoveNext())
        {
            var kv = (KeyValuePair<string, JSONNode>)enu.Current;
            Debuger.AddTrigger(kv.Value["cmd"].Value, onFakeTriggerd);
        }
        var dis = enu as IDisposable;
        if(dis != null) dis.Dispose();
    }

    private void onFakeTriggerd(string trigger)
    {
        var enu = json.GetEnumerator();
        while(enu.MoveNext())
        {
            var kv = (KeyValuePair<string, JSONNode>)enu.Current;
            if(kv.Value["cmd"].Value == trigger)
            {
                if(kv.Key == FORMAL_KEY)
                    Debuger.Log("你已经切回Formal配置，重启生效");
                else
                    Debuger.Log("你发现了一个秘密，已切换至fake配置，重启生效", kv.Key);
                PlayerPrefs.SetString(LOCAL_CONF_KEY, kv.Key);
                PlayerPrefs.Save();
                break;
            }
        }
        var dis = enu as IDisposable;
        if(dis != null) dis.Dispose();
    }

    /// <summary>
    /// 更新本地配置
    /// </summary>
    /// <param name="confUrlList"></param>
    /// <param name="appVer"></param>
    /// <param name="forceResVer"></param>
    public void SaveToLocal(List<UrlEntry> confUrlList, string forceResVer)
    {
        if(string.IsNullOrEmpty(forceResVer))
            return;
        if(confUrlList != null && confUrlList.Count > 0)
            ConfigUrlList = confUrlList;

        if(json != null)
        {
            LocalForceResVersion = forceResVer;
            usingJson["forceResVer"] = forceResVer;

            JSONArray arr = usingJson["configUrl"] as JSONArray;
            //清除之前的地址
            while(arr.Count > 0)
                arr.Remove(arr.Count - 1);
            //添加新的地址
            for(int i = 0; i < ConfigUrlList.Count; ++i)
            {
                JSONClass node = new JSONClass();
                node["url"] = ConfigUrlList[i].url;
                node["random"].AsInt = ConfigUrlList[i].random;
                arr.Add(node);
            }

            try
            {
                File.WriteAllText(cachePath, json.ToString());
            }catch(Exception e)
            {
                Debuger.Err("写入磁盘出错 LocalConfig", e.Message, e.StackTrace);
                StartupTip.Singleton.TipWriteFileError(GetType().Name, null);
            }
        }
    }
}
