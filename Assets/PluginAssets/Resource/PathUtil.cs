/* 
 * -----------------------------------------------
 * Copyright (c) 1ktower.com All rights reserved.
 * -----------------------------------------------
 * 
 * Coder：Zhou XiQuan
 * Time ：2017.09.22
*/

///恶心的是Android下用File去判断是否存在，用WWW去加载和AssetBundle.LoadFromFile的路径不一样
using System.IO;
using UnityEngine;

public class PathUtil
{
    ///加载路径
    public static string GetABPath(string resName, bool wwwPath = false)
    {
        //编辑器下
#if UNITY_EDITOR
        if(File.Exists(GetForceABPath(resName)))
            return GetForceABPath(resName);
        return GetBackABPath(resName);
#else
        //手机上
        string path = GetForceABPath(resName);
        if(File.Exists(path))   //有更新/强更
            return path;

        path = GetBackABPath(resName);
        if(File.Exists(path))   //有更新/偷跑
            return path;

        //无更新
        if(wwwPath)
            return GetWWWABBuildInPath(resName);
        else
            return GetABBuildinPath(resName);
#endif
    }

    public static bool IsInStreamFolder(string resName)
    {
#if UNITY_EDITOR
        if (File.Exists(GetForceABPath(resName)))
            return true;
        if (File.Exists(GetBackABPath(resName)))
            return true;
        return false;
#else
        //手机上
        string path = GetForceABPath(resName);
        if (File.Exists(path))   //有更新/强更
            return true;

        path = GetBackABPath(resName);
        if (File.Exists(path))   //有更新/偷跑
            return true;
        path = GetABBuildinPath(resName);
        if (File.Exists(path))
            return true;
        return false;
#endif
    }

    ///内置版本号
    public const string BuildInVersionConfigName = "LocalConfig.json";

    ///lua bundle名字
    public const string LuaScriptsBundleName = "lua_scripts.bytes";
    ///config bundle名字
    public const string ConfigBundleName = "bean_conf.bytes";
    ///dep ab依赖bundle名字
    public static string BuildinDepConfName = "_711_.dep";

    //强更lua后缀
    public const string UpdateLuaSuffix = ".lua_bytes";
    //强更bean后缀
    public const string UpdateBeanSuffix = ".bean_bytes";
    //依赖配置文件后缀
    public const string UpdateDepSuffix = ".dep";

    //强更lua,bean信息
    public const string UpdatedConfName = "_important.json";

    /// <summary>
    /// ab文件后缀名
    /// </summary>
    public static string abSuffix
    {
        get
        {
#if UNITY_IPHONE
            return ".8";
#elif UNITY_ANDROID
            return ".9";
#else
            Debug.LogError( "当前平台暂时不支持" );
            return ".0";
#endif
        }
    }

#if UNITY_EDITOR
    private static string abBuildinPath = Application.dataPath + "/StreamingAssets/ab/";
#elif UNITY_ANDROID
    private static string abBuildinPath = Application.dataPath + "!assets/ab/";
#elif UNITY_IPHONE
    private static string abBuildinPath = Application.dataPath + "/Raw/ab/";
#endif

    ///ab包内路径, AssetBundle api读取
    public static string GetABBuildinPath(string resName = null)
    {
        if(string.IsNullOrEmpty(resName))
            return abBuildinPath;

        if(resName.Contains("."))
            return abBuildinPath + resName;
        else
            return abBuildinPath + resName + abSuffix;
    }

    ///ab包内路径 WWW读取
    public static string GetWWWABBuildInPath(string resName = null)
    {
        string dic = "";
#if UNITY_EDITOR
#if UNITY_EDITOR_WIN
        dic = "file://" + Application.dataPath + "/StreamingAssets/ab/";
#else
        dic = Application.dataPath + "/StreamingAssets/ab/";
#endif
#elif UNITY_ANDROID
        dic = "jar:file://" + Application.dataPath + "!/assets/ab/";
#elif UNITY_IPHONE
        dic = Application.dataPath + "/Raw/ab/";
#endif

        if(string.IsNullOrEmpty(resName))
            return dic;

        if(resName.Contains("."))
            return dic + resName;
        else
            return dic + resName + abSuffix;
    }

    /// <summary>
    /// 初始化目录
    /// </summary>
    public static void InitPath()
    {
        if(!Directory.Exists(GetConfigPath()))
            Directory.CreateDirectory(GetConfigPath());
        if(!Directory.Exists(GetBackABPath()))
            Directory.CreateDirectory(GetBackABPath());
        if(!Directory.Exists(GetForceABPath()))
            Directory.CreateDirectory(GetForceABPath());
    }

    public static void ClearForce()
    {
        if(File.Exists(PathUtil.ForceFileListOutPath))
            File.Delete(PathUtil.ForceFileListOutPath);
        if(File.Exists(PathUtil.ForceFileTmpLoadedPath))
            File.Delete(PathUtil.ForceFileTmpLoadedPath);
#if !UNITY_EDITOR
        if(Directory.Exists(GetForceABPath()))
            Directory.Delete(GetForceABPath(), true);
        if(!Directory.Exists(GetForceABPath()))
            Directory.CreateDirectory(GetForceABPath());
#endif
    }

    /// <summary>
    /// 获取网络路径
    /// </summary>
    public static string GetServerPath(string url, string resName)
    {
        if(resName.Contains("."))
            return url + resName;
        else
            return url + resName + abSuffix;
    }

    private static string abBackPath = Application.persistentDataPath + "/ab/";
    public static string GetBackABPath(string resName = null)
    {
#if UNITY_EDITOR
#if UNITY_ANDROID
        string dic = Application.dataPath + "/../../PGameMain/ABOut/android/back/";
#elif UNITY_IPHONE
        string dic = Application.dataPath + "/../../PGameMain/ABOut/ios/back/";
#else
        string dic = Application.dataPath + "/../ABOut/other/back/";
#endif
#else
        string dic = abBackPath;
#endif
        if (string.IsNullOrEmpty(resName))
            return dic;

        if(resName.Contains("."))
            return dic + resName;
        else
            return dic + resName + abSuffix;
    }

    private static string abforcepath = Application.persistentDataPath + "/abForce/";
    public static string GetForceABPath(string resName = null)
    {
#if UNITY_EDITOR
#if UNITY_ANDROID
        string dic = Application.dataPath + "/../../PGameMain/ABOut/android/force/";
#elif UNITY_IPHONE
        string dic = Application.dataPath + "/../../PGameMain/ABOut/ios/force/";
#else
        string dic = Application.dataPath + "/../ABOut/other/force/";
#endif
#else
        string dic = abforcepath;
#endif
        if (string.IsNullOrEmpty(resName))
            return dic;

        if(resName.Contains("."))
            return dic + resName;
        else
            return dic + resName + abSuffix;
    }

    public static string bytesSuffix
    {
        get
        {
            return ".bytes";
        }
    }

    /// <summary>
    /// 配置目录
    /// </summary>
    public static string GetConfigPath()
    {
#if UNITY_EDITOR
        return Application.dataPath + "/../98/config/";
#else
        return Application.persistentDataPath + "/config/";
#endif
    }

    /// 强更资源清单
    public static string ForceFileListOutPath = GetConfigPath() + "force.conf";
    /// 强更资源已下载清单
    public static string ForceFileTmpLoadedPath = GetConfigPath() + "tmpLoaded.conf";

    ///内置资源的清单
    public const string BuildInFileListName = "buildInResList.json";
}