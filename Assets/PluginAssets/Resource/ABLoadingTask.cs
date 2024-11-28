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

/// <summary>
/// ab资源加载，组装任务
/// </summary>
public class ABLoadingTask : IClassCache
{
    public override bool doCache { get { return true; } }
    private string resName;
    private ResPriority priority;
    private string[] dependences;
    private System.Action<string> outCallBack;
    private System.Action<string, AssetBundle> resCallBack;
    private Dictionary<string, bool> loadedDepMap = new Dictionary<string, bool>();

    private int depCount;
    private int loadedDepNum;

    public override void FakeDtr()
    {
        base.FakeDtr();
        resName = null;
        outCallBack = null;
        resCallBack = null;
        dependences = null;
        loadedDepMap.Clear();
    }

    public System.Action<string> GetOutCallBack()
    {
        return outCallBack;
    }

    /// <summary>
    /// 初始化任务
    /// </summary>
    /// <param name="res">资源名</param>
    /// <param name="onResLoaded">加载回调</param>
    /// <param name="outCb">外部回调</param>
    /// <param name="deps">依赖列表</param>
    /// <param name="_priority">加载优先级</param>
    public void Init(string res, System.Action<string, AssetBundle> onResLoaded, System.Action<string> outCb, string[] deps, ResPriority _priority)
    {
        resName = res;
        loadedDepNum = 0;
        dependences = deps;
        outCallBack = outCb;
        resCallBack = onResLoaded;
        priority = _priority;
        loadedDepMap.Clear();
        _Setup(onResLoaded);
    }

    /// <summary>
    /// 添加回调
    /// </summary>
    /// <param name="_priority">优先级</param>
    /// <param name="callback">回调</param>
    public void AddCallback(ResPriority _priority, System.Action<string> callback)
    {
        outCallBack += callback;
        if(_priority > priority)
        {
            priority = _priority;
            _Setup(null);
        }
    }

    /// 组建对象
    private void _Setup(System.Action<string, AssetBundle> callBack)
    {
        if(dependences == null || dependences.Length == 0)
        {
            ABManager.Singleton.LoadAB(resName, resCallBack, priority);
        }
        else
        {
            depCount = dependences.Length;
            for(int i = 0; i < depCount; ++i)
                ResManager.Singleton.Request(dependences[i], _OnDepResLoaded, priority, false);
        }
    }

    /// 依赖加载完成回调
    private void _OnDepResLoaded(string res)
    {
        if(loadedDepMap.ContainsKey(res))
            return;

        loadedDepNum++;
        loadedDepMap[res] = true;
        ResManager.Singleton.AddRef(res, true);
        if(loadedDepNum >= depCount)
            ABManager.Singleton.LoadAB(resName, resCallBack, priority);
    }
}