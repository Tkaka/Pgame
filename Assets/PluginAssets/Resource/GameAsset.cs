/* 
 * -----------------------------------------------
 * Copyright (c) 1ktower.com All rights reserved.
 * -----------------------------------------------
 * 
 * Coder：Zhou XiQuan
 * Time ：2017.09.26
*/

using UnityEngine;
using System.Collections.Generic;

public class GameAsset
{
    /// <summary>
    /// 资源名字
    /// </summary>
    public string resName { get; private set; }

    /// <summary>
    /// 引用计数
    /// </summary>
    public int refNum { get; private set; }

    /// <summary>
    /// 上次引用时间
    /// </summary>
    public float lastRefTime { get; private set; }

    //依赖资源列表
    private string[] dependences;
    private AssetBundle assetBundle;

    //已经加载的资源清单,同名不同类型只存在与单Sprite纹理，不影响
    private Dictionary<string, bool> loadedObjMap = new Dictionary<string, bool>();
    private Dictionary<string, System.Action<string, string, System.Type>> callMap = new Dictionary<string, System.Action<string, string, System.Type>>();

    /// <summary>
    /// 初始化一个资源
    /// </summary>
    /// <param name="res">资源名</param>
    /// <param name="objs">资源列表</param>
    /// <param name="deps">依赖列表</param>
    public void Init(string res, AssetBundle ab, string[] deps)
    {
        resName = res;
        assetBundle = ab;
        dependences = deps;
    }

#if UNITY_EDITOR
    internal void LoadGoAsset()
    {
        if (assetBundle != null)
        {
            var go = assetBundle.LoadAsset<GameObject>(resName);
            GetShaderBack(go);
        }
    }
#endif

    /// <summary>
    /// 获取某个资源
    /// </summary>
    public Object GetAsset(string depName, System.Type type)
    {
        if(assetBundle == null)
            return null;
        
        Object obj = null;
        if(type == null)
        {
            obj = assetBundle.LoadAsset(depName);
        } else if(type == typeof(Sprite))
        {
            var objs = assetBundle.LoadAllAssets();
            for(int i = objs.Length - 1; i >= 0; --i)
            {
                if(objs[i].name == depName && objs[i].GetType() == type)
                {
                    obj = objs[i];
                    break;
                }
            }
        } else
        {
            obj = assetBundle.LoadAsset(depName, type);
        }

        loadedObjMap[depName] = true;
        if(obj == null)
            Debuger.Err(resName + " 中不存在资源：" + depName + (type == null ? "" : " >> 类型：" + type.Name));

#if UNITY_EDITOR
        GetShaderBack(obj as GameObject);
#endif
        return obj;
    }

    /// <summary>
    /// 异步获取内部资源
    /// </summary>
    /// <param name="name">内部资源名</param>
    /// <param name="type">内部资源类型</param>
    /// <param name="callBack">回调</param>
    /// <param name="priority">加载优先级</param>
    public void LoadAsset(string depName, System.Type type, System.Action<string, string, System.Type> callBack, ResPriority priority)
    {
        if(assetBundle == null || loadedObjMap.ContainsKey(depName))
        {
            try
            {
                if(callBack != null)
                    callBack(resName, depName, type);
            }catch(System.Exception e)
            {
                Debuger.Err(e.Message, e.StackTrace);
            }
        }else
        {
            if(callMap.ContainsKey(depName))
                callMap[depName] += callBack;
            else
                callMap.Add(depName, callBack);
            AddRef();
            AssetLoader.Singleton.LoadAssetAsync(assetBundle, depName, type, _OnAssetLoaded, priority);
        }
    }

    /// 资源加载回调
    private void _OnAssetLoaded(string depName, System.Type type)
    {
        RemoveRef();
        loadedObjMap[depName] = true;
        if(callMap.ContainsKey(depName))
        {
            var callBack = callMap[depName];
            try
            {
                if(callBack != null)
                    callBack(resName, depName, type);
            } catch(System.Exception e)
            {
                Debuger.Err(e.Message, e.StackTrace);
            }
            callMap.Remove(depName);
        }
    }

    /// <summary>
    /// 添加引用
    /// </summary>
    public void AddRef()
    {
        refNum++;
        lastRefTime = Time.time;
    }

    /// <summary>
    /// 减少引用
    /// </summary>
    public void RemoveRef()
    {
        refNum--;
    }

    /// <summary>
    /// 释放资源
    /// </summary>
    public bool Unload(bool unloadDep = false)
    {
        if(refNum > 0) return false;

        if(dependences != null && dependences.Length > 0)
        {
            //依赖引用归还
            for(int i = 0, len = dependences.Length; i < len; ++i)
            {
                ResManager.Singleton.ReturnObj(dependences[i]);
                if(unloadDep)
                    ResManager.Singleton.GCObj(dependences[i]);
            }
        }

        if(assetBundle != null)
        {
#if UNITY_EDITOR
            //强制卸载所有资源,管理正常的情况下不会出问题
            Debuger.Log("---release res--->" + assetBundle.name);
            assetBundle.Unload(true);
#else
            assetBundle.Unload(true);
#endif
        }
        refNum = 0;
        resName = null;
        lastRefTime = 0;
        dependences = null;
        assetBundle = null;
        callMap.Clear();
        loadedObjMap.Clear();
        return true;
    }

#if UNITY_EDITOR
    /// 编辑器下还原Renderer的Shader
    private static void GetShaderBack(GameObject obj)
    {
        if(obj == null)
            return;
        foreach(Renderer element in obj.GetComponentsInChildren<Renderer>(true))
        {
            foreach(Material mat in element.sharedMaterials)
            {
                if(mat != null)
                    mat.shader = Shader.Find(mat.shader.name);
            }
        }

        foreach(var ri in obj.GetComponentsInChildren<UnityEngine.UI.Graphic>(true))
        {
            if(ri.material)
                ri.material.shader = Shader.Find(ri.material.shader.name);
        }
    }
#endif
}