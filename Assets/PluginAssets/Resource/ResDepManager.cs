/* 
 * -----------------------------------------------
 * Copyright (c) 1ktower.com All rights reserved.
 * -----------------------------------------------
 * 
 * Coder：Zhou XiQuan
 * Time ：2017.10.18
*/

using UnityEngine;
using System.Collections.Generic;

public class ResDepManager
{
    private static ResDepManager instance;
    public static ResDepManager Singleton
    {
        get
        {
            if(instance == null)
                instance = new ResDepManager();
            return instance;
        }
    }

    private Dictionary<string, string[]> depConfMap = new Dictionary<string, string[]>();
    /// <summary>
    /// 初始化依赖信息，手机上才需要
    /// </summary>
    public void LoadDeps()
    {
#if !UNITY_EDITOR
        depConfMap.Clear();
        string path = PathUtil.GetABBuildinPath(PathUtil.BuildinDepConfName);
        var ab = AssetBundle.LoadFromFile(path);
        if(ab == null)
        {
            Debuger.Err("找不到ab依赖配置文件");
            return;
        }
        var ta = ab.LoadAsset<TextAsset>(System.IO.Path.GetFileNameWithoutExtension(PathUtil.BuildinDepConfName));
        if(ta == null)
        {
            Debuger.Err("ab依赖配置文件内容为空");
            return;
        }

        //解析
        using(var stream = new System.IO.MemoryStream(ta.bytes))
        {
            using(var reader = new System.IO.BinaryReader(stream))
            {
                int count = reader.ReadInt32();
                for(int ii = 0; ii < count; ++ii)
                {
                    string name = reader.ReadString();
                    int len = reader.ReadInt16();
                    string[] deps = new string[len];
                    for(int i = 0; i < len; ++i)
                        deps[i] = reader.ReadString();
                    depConfMap[name] = deps;
                }
            }
        }
        ab.Unload(true);

        //更新的依赖
        ForceManager.Singleton.LoadDep((res, datas) => {
            if(datas == null) return;
            using(var stream = new System.IO.MemoryStream(datas))
            {
                using(var reader = new System.IO.BinaryReader(stream))
                {
                    int count = reader.ReadInt32();
                    for(int ii = 0; ii < count; ++ii)
                    {
                        string name = reader.ReadString();
                        int len = reader.ReadInt16();
                        string[] deps = new string[len];
                        for(int i = 0; i < len; ++i)
                            deps[i] = reader.ReadString();
                        depConfMap[name] = deps;
                    }
                }
            }
        });
#endif
    }

    ///获取依赖
    public string[] GetDependence(string resName)
    {
#if UNITY_EDITOR
        string path = PathUtil.GetForceABPath() + resName + ".d";
        if(System.IO.File.Exists(path))
            return System.IO.File.ReadAllLines(path);
#else
        if(depConfMap.ContainsKey(resName))
            return depConfMap[resName];
#endif
        return null;
    }
}