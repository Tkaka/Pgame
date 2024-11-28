/* 
 * -----------------------------------------------
 * Copyright (c) 1ktower.com All rights reserved.
 * -----------------------------------------------
 * 
 * Coder：Zhou XiQuan
 * Time ：2017.10.20
*/

using UnityEngine;
using System.Collections.Generic;

public class ConfigManager
{
    private static ConfigManager m_instance;
    public static ConfigManager Singleton
    {
        get
        {
            if(m_instance == null)
                m_instance = new ConfigManager();
            return m_instance;
        }
    }

    private Dictionary<string, byte[]> byteMap = new Dictionary<string, byte[]>();

    public void LoadBeans()
    {
#if !UNITY_EDITOR
        byteMap.Clear();
        string path = PathUtil.GetABBuildinPath(PathUtil.ConfigBundleName);
        AssetBundle ab = AssetBundle.LoadFromFile(path);
        if(ab == null)
        {
            Debuger.Err("没有配置表");
            return;
        }

        TextAsset[] beans = ab.LoadAllAssets<TextAsset>();
        if(beans != null)
        {
            for(int i = 0, len = beans.Length; i < len; ++i)
                byteMap[beans[i].name] = beans[i].bytes;
        }
        ab.Unload(false);

        ForceManager.Singleton.LoadBean((name, bytes) =>{
            byteMap[name] = bytes;
        });
#endif
    }

    public byte[] GetData(string name)
    {
        if(byteMap.ContainsKey(name))
            return byteMap[name];

        return null;
    }
}