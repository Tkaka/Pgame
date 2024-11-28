/* 
 * -----------------------------------------------
 * Copyright (c) 1ktower.com All rights reserved.
 * -----------------------------------------------
 * 
 * Coder：Zhou XiQuan
 * Time ：2017.09.25
*/

using System.IO;
using UnityEngine;
using System.Collections.Generic;

public class LuaManager
{
    private static LuaManager instance;
    public static LuaManager Singleton
    {
        get
        {
            if(instance == null)
                instance = new LuaManager();
            return instance;
        }
    }

    private Dictionary<string, byte[]> luaMap = new Dictionary<string, byte[]>();
    /// <summary>
    /// 获取lua数据
    /// </summary>
    /// <param name="filePath">文件名/文件路径</param>
    public byte[] GetLuaBytes(ref string filePath)
    {
        string luaName = Path.GetFileNameWithoutExtension(filePath);
#if UNITY_EDITOR
        var files = Directory.GetFiles(UnityEngine.Application.dataPath + "/XLua/Lua/", luaName + ".lua", SearchOption.AllDirectories);
        if(files.Length > 0)
            return File.ReadAllBytes(files[0]);
#else
        if(luaMap.ContainsKey(luaName))
            return luaMap[luaName];
#endif
        return null;
    }

    /// <summary>
    /// 初始化lua脚本
    /// </summary>
    public void LoadScripts()
    {
#if !UNITY_EDITOR
        luaMap.Clear();
        string path = PathUtil.GetABBuildinPath(PathUtil.LuaScriptsBundleName);
        AssetBundle ab = AssetBundle.LoadFromFile(path);
        if(ab == null)
        {
            Debuger.Err("没有lua文件");
            return;
        }

        TextAsset[] scripts = ab.LoadAllAssets<TextAsset>();
        if(scripts != null)
        {
            for(int i = 0, len = scripts.Length; i < len; ++i)
            {
                luaMap[scripts[i].name] = scripts[i].bytes;
            }
        }
        ab.Unload(false);

        ForceManager.Singleton.LoadLua((name, data)=>{
            luaMap[name] = data;
        });
#endif
    }
}