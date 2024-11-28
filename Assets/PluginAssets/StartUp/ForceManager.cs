/* 
 * -----------------------------------------------
 * Copyright (c) 1ktower.com All rights reserved.
 * -----------------------------------------------
 * 
 * Coder：Zhou XiQuan
 * Time ：2017.10.20
*/

using System.IO;
using UnityEngine;
using System.Collections.Generic;

public class ForceManager
{
    private static ForceManager m_instance;
    public static ForceManager Singleton
    {
        get
        {
            if(m_instance == null)
                m_instance = new ForceManager();
            return m_instance;
        }
    }

    private SimpleJSON.JSONClass json;
    private ForceManager()
    {
        string path = PathUtil.GetConfigPath() + PathUtil.UpdatedConfName;
        if(File.Exists(path))
            json = SimpleJSON.JSONClass.LoadFromFile(path) as SimpleJSON.JSONClass;

        if(json == null)
            json = new SimpleJSON.JSONClass();

        if(json[PathUtil.UpdateLuaSuffix] == null)
            json[PathUtil.UpdateLuaSuffix] = new SimpleJSON.JSONArray();

        if(json[PathUtil.UpdateBeanSuffix] == null)
            json[PathUtil.UpdateBeanSuffix] = new SimpleJSON.JSONArray();

        if(json[PathUtil.UpdateDepSuffix] == null)
            json[PathUtil.UpdateDepSuffix] = new SimpleJSON.JSONArray();
    }

    /// <summary>
    /// 添加更新文件
    /// </summary>
    public void AddUpdateFile(string name)
    {
        string key = "";
        if(name.EndsWith(PathUtil.UpdateLuaSuffix))
            key = PathUtil.UpdateLuaSuffix;
        else if(name.EndsWith(PathUtil.UpdateBeanSuffix))
            key = PathUtil.UpdateBeanSuffix;
        else if(name.EndsWith(PathUtil.UpdateDepSuffix))
            key = PathUtil.UpdateDepSuffix;

        var arr = json[key];
        if(arr != null)
        {
            for(int i = 0; i < arr.Count; ++i )
            {
                //已经添加了
                if(arr[i].Value == name)
                    return;
            }
            json[key].Add(name);
        }
    }

    /// <summary>
    /// 更新结束
    /// </summary>
    public void UpdateEnd()
    {
        json.SaveToFile(PathUtil.GetConfigPath() + PathUtil.UpdatedConfName);
    }

    /// <summary>
    /// 清理更新文件
    /// </summary>
    public void Clear()
    {
        string path = "";
        var enu = json.Childs.GetEnumerator();
        while(enu.MoveNext())
        {
            var arr = enu.Current as SimpleJSON.JSONArray;
            if(arr == null)
                continue;
            for(int i = 0, len = arr.Count; i < len; ++i)
            {
                path = PathUtil.GetForceABPath(arr[i].Value);
                if(File.Exists(path))
                    File.Delete(path);
            }
        }
        enu.Dispose();

        json = new SimpleJSON.JSONClass();
        json.SaveToFile(PathUtil.GetConfigPath() + PathUtil.UpdatedConfName);
    }

    public void LoadLua(System.Action<string, byte[]> func)
    {
        load(PathUtil.UpdateLuaSuffix, func);
    }

    public void LoadBean(System.Action<string, byte[]> func)
    {
        load(PathUtil.UpdateBeanSuffix, func);
    }

    public void LoadDep(System.Action<string, byte[]> func)
    {
        load(PathUtil.UpdateDepSuffix, func);
    }

    private void load(string suffix, System.Action<string, byte[]> func)
    {
        if(func == null)
            return;

        string name = "";
        var arr = json[suffix] as SimpleJSON.JSONArray;
        if(arr == null)
            return;
        for(int i = 0, len = arr.Count; i < len; ++i)
        {
            name = arr[i].Value;
            string path = PathUtil.GetForceABPath(name);
            AssetBundle ab = AssetBundle.LoadFromFile(path);
            if(ab == null)
                continue;
            TextAsset[] tas = ab.LoadAllAssets<TextAsset>();
            if(tas != null)
            {
                for(int j = 0, num = tas.Length; j < num; ++j)
                    func(tas[j].name, tas[j].bytes);
            }
            ab.Unload(false);
        }
    }
}