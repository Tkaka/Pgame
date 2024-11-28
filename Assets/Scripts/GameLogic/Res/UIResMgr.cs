using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Object = UnityEngine.Object;

public class UIResMgr : SingletonTemplate<UIResMgr>
{
    private Dictionary<string, AssetBundle> uiABMap = new Dictionary<string, AssetBundle>();
    private Dictionary<string, Object> uiResMap = new Dictionary<string, Object>();
    private Dictionary<string, bool> constMap = new Dictionary<string, bool>();

    private Dictionary<string, float> resZeroRefMap = new Dictionary<string, float>();

    public void AddConstPkg(string pkgName)
    {
        constMap[pkgName] = true;
    }

#region GLoader加载

    private Dictionary<Texture, int> iconDic = new Dictionary<Texture, int>();
    public void LoadIcon(Texture tex)
    {
        if (iconDic.ContainsKey(tex))
        {
            iconDic[tex]++;
        }
        else
        {
            iconDic.Add(tex, 1);
        }
    }

    public void ReleaseIcon(Texture tex)
    {
        if (iconDic.ContainsKey(tex))
        {
            iconDic[tex]--;
            if (iconDic[tex] <= 0)
            {
                iconDic.Remove(tex);
                Resources.UnloadAsset(tex);
            }
        }
        else
        {
            Logger.err("Res:ReleaseIcon:释放未知纹理 > " + tex.name);
        }
    }

    public void LoadWWWTexture(string url, Action<Texture2D> callback)
    {
        CoroutineManager.Singleton.startCoroutine(loadWWW(url, callback));
    }

    private IEnumerator loadWWW(string url, Action<Texture2D> callback)
    {
        WWW www = new WWW(url);
        yield return www;
        while(!www.isDone)
            yield return null;

        if (callback != null)
            callback(www.texture);
    }

    public void PreloadTexturePackage(string pkg, Action callback, bool fromResource)
    {
        if(fromResource)
        {
            //Resources下加载
            CoroutineManager.Singleton.startCoroutine(resourceAsyncLoad(pkg, callback));
            return;
        }

        //ab加载
        string name = System.IO.Path.GetFileName(pkg).ToLower();
        string path = PathUtil.GetABPath(name);
        if (!PathUtil.IsInStreamFolder(name))
        {
            //外部没有更新，Resources下加载
            CoroutineManager.Singleton.startCoroutine(resourceAsyncLoad(pkg, callback));
        }
        else
        {
            ABManager.Singleton.LoadAB(name, (res, ab)=>{
                if(ab != null)
                    uiABMap[res] = ab;
                if (callback != null)
                    callback();
            }, ResPriority.Async);
        }
    }

    private IEnumerator resourceAsyncLoad(string res, Action callback)
    {
        var req = Resources.LoadAsync(res);
        yield return req;
        while (!req.isDone)
            yield return null;

        if (callback != null)
            callback();
    }
    #endregion

    #region package 加载代理

    private AssetBundle getAssetBundle(string res)
    {
        if (uiABMap.ContainsKey(res))
            return uiABMap[res];
        string path = PathUtil.GetABPath(res);
        uiABMap[res] = AssetBundle.LoadFromFile(path);
        return uiABMap[res];
    }
    public object UILoadDelegate(string res, string ext, Type type)
    {
        string name = System.IO.Path.GetFileName(res);
        string[] arr = name.Split('@');
        string abName = arr[0].ToLower();
        if (arr.Length > 1)
            abName = abName + "_res";

        if (PathUtil.IsInStreamFolder(abName))
        {
            //有更新文件
            object ret = null;
            AssetBundle ab = getAssetBundle(abName);
            if (ab != null)
            {
                if (ext == ".bytes")
                    ret = ab.LoadAsset<TextAsset>(name);
                else
                    ret = ab.LoadAsset(name);
            }
            return ret;
        }
        else
        {
            return Resources.Load(res, type);
        }
    }
#endregion

    public void RemoveUIPackage(string pkgName)
    {
        //常驻内存
        if (constMap.ContainsKey(pkgName))
            return;

        string name = System.IO.Path.GetFileName(pkgName).ToLower();
        if(uiABMap.ContainsKey(name))
        {
#if UNITY_EDITOR
            Debuger.Log("---release ui res--->" + name);
#endif
            uiABMap[name].Unload(true);
            uiABMap.Remove(name);
        }

        name = name + "_res";
        if (uiABMap.ContainsKey(name))
        {
#if UNITY_EDITOR
            Debuger.Log("-------release ui res->" + name);
#endif
            uiABMap[name].Unload(true);
            uiABMap.Remove(name);
        }
    }

    //简单GC
    public void ReleaseNoUseRes()
    {
        if (uiABMap.Count <= 0)
        {
            if (resZeroRefMap.Count > 3)
                Resources.UnloadUnusedAssets();
            return;
        }

        List<string> toReleaseList = new List<string>();
        float time = Time.realtimeSinceStartup;
        if (resZeroRefMap.Count > 3)
        {
            List<string> list = new List<string>(resZeroRefMap.Keys);
            list.Sort((a, b) => {
                return resZeroRefMap[a].CompareTo(resZeroRefMap[b]);
            });

            for (int i = list.Count - 1; i >= 0; --i)
            {
                if (i > 3 || time - resZeroRefMap[list[i]] > 30f)
                    toReleaseList.Add(list[i]);
            }
        }
        else
        {
            var enu = resZeroRefMap.GetEnumerator();
            while (enu.MoveNext())
            {
                if (time - enu.Current.Value > 60)
                    toReleaseList.Add(enu.Current.Key);
            }
            enu.Dispose();
        }

        foreach (var res in toReleaseList)
            RemoveUIPackage(res);
        if (toReleaseList.Count > 3)
            Resources.UnloadUnusedAssets();
    }

    #region package 引用计数

    private Dictionary<string, int> pkgRefMap = new Dictionary<string, int>();
    public void AddPkgRef(string pkgName)
    {
        if (pkgRefMap.ContainsKey(pkgName))
            pkgRefMap[pkgName] = pkgRefMap[pkgName] + 1;
        else
            pkgRefMap[pkgName] = 1;

        if (resZeroRefMap.ContainsKey(pkgName))
            resZeroRefMap.Remove(pkgName);
    }

    public void RemovePkgRef(string pkgName)
    {
        if (pkgRefMap.ContainsKey(pkgName))
        {
            pkgRefMap[pkgName] = pkgRefMap[pkgName] - 1;
            if (pkgRefMap[pkgName] <= 0)
                resZeroRefMap[pkgName] = Time.time;
        }
        else
            Debug.LogError("移除引用计数错误，找不到包>" + pkgName);
    }

    public void RemoveZeroRefPkg()
    {
        var enu = pkgRefMap.GetEnumerator();
        while(enu.MoveNext())
        {
            if (enu.Current.Value <= 0)
                RemoveUIPackage(enu.Current.Key);
        }
        enu.Dispose();
        Resources.UnloadUnusedAssets();
    }
#endregion

    public void CheckPkgRef()
    {
#if UNITY_EDITOR
        var enu = pkgRefMap.GetEnumerator();
        while(enu.MoveNext())
        {
            if (enu.Current.Value > 0)
                Debuger.Err("UI存在未释放的Package>" + enu.Current.Key + ">" + enu.Current.Value);
        }
        enu.Dispose();

        var enu1 = iconDic.GetEnumerator();
        while(enu1.MoveNext())
            Debuger.Err("未释放的第三方纹理>" + enu1.Current.Key);
        enu1.Dispose();
#endif
    }
}