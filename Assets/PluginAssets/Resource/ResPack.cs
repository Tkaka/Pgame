/* 
 * -----------------------------------------------
 * Copyright (c) 1ktower.com All rights reserved.
 * -----------------------------------------------
 * 
 * Coder：Zhou XiQuan
 * Time ：2017.09.26
*/

using FairyGUI;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ResPack
{
#if UNITY_EDITOR
    private static List<ResPack> resList = new List<ResPack>();
#endif
    public static void CheckResRelease()
    {
#if UNITY_EDITOR
        foreach(var res in resList)
        {
            if(res.resMap.Count > 0)
            {
                Debuger.Err("!!!!!!存在未释放的资源包 > " + res.GetType().Name, res.resOwnerObj);
                foreach(var name in res.resMap.Keys)
                    Debuger.Wrn(name);
            }
        }
#endif
    }

#if UNITY_EDITOR
    protected object resOwnerObj;
#endif

    public ResPack(object owner)
    {
#if UNITY_EDITOR
        if (!(this is BaseWindow) && (owner == null || owner.ToString() == ""))
            Debuger.Err("创建ResPack未传递参数" + this.ToString());
        resOwnerObj = owner;
        resList.Add(this);
#endif
    }

    private Dictionary<string, int> resMap = new Dictionary<string, int>();
    /// 请求资源
    public void Request(string abName, Action<string> callback, ResPriority priority = ResPriority.Async)
    {
        ResManager.Singleton.Request(abName, callback, priority);
    }

    /// 请求资源
    public void Request(string abName, string resName, Type type, Action<string, string, Type> callback, ResPriority priority = ResPriority.Async)
    {
        ResManager.Singleton.Request(abName, (name)=>{
            ResManager.Singleton.GetLoadedObjSync(name, resName, type, callback, priority);
        }, priority);
    }

    ///获取资源
    public object GetObject(string abName, string resName = null, Type type = null)
    {
        object obj = ResManager.Singleton.LoadObjSync(abName, resName, type);
        changeRef(abName, true);
        return obj;
    }


    public GameObject LoadGo(string goName, Vector3 ? pos = null, Quaternion ? rot = null)
    {
        Vector3 p = Vector3.zero;
        if (pos != null)
            p = pos.Value;
        Quaternion q = new Quaternion();
        if (rot != null)
            q = rot.Value;

        GameObject obj = ResManager.Singleton.LoadObjSync(goName, p, q);
        changeRef(goName, true);
        return obj;
    }

    protected void changeRef(string res, bool addOrRemove)
    {
        if (string.IsNullOrEmpty(res))
            return;

        if (addOrRemove)
        {
            if (resMap.ContainsKey(res))
                resMap[res] = resMap[res] + 1;
            else
                resMap[res] = 1;
        }
        else
        {
            if (resMap.ContainsKey(res))
            {
                //加引用在调用的地方掉，减引用在此减
                if (resMap[res] >= 1)
                    ResManager.Singleton.ReturnObj(res);

                resMap[res] = resMap[res] - 1;
                if (resMap[res] <= 0)
                    resMap.Remove(res);
            }else
            {
                Debuger.Err("释放的资源未曾获取过....." + res);
            }
        }
    }
    
    /// 释放资源引用
    public void ReleaseRes(string abName)
    {
        changeRef(abName, false);
    }

    /// 释放所有资源引用
    public virtual void ReleaseAllRes()
    {
        var enu = resMap.GetEnumerator();
        while(enu.MoveNext())
        {
            int count = enu.Current.Value;
            string name = enu.Current.Key;
            while(count > 0)
            {
                ResManager.Singleton.ReturnObj(name);
                count--;
            }
        }
        enu.Dispose();
        resMap.Clear();   
    }
}