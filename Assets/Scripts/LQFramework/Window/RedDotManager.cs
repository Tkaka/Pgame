using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * 用法：假设竞技场里面有一个排行奖励可已领取,此时需要在对应的奖励页签显示红点，同时还需要在主界面的入口上面显示小红点，
 *       我们只需要在奖励数据刷新时调用: RedDotManager.Singleton.SetRedDotValue(path, isShow);
 *       path：是自定义的路径  xxx/xxx的格式， 比如竞技场奖励可以将路径设为 "mainArean/rankReward";
 *       isShow: 是否显示小红点
 *       
 *        
 *       在奖励窗口类里面调用： _RegisterRedDot("mainArean/RankReward", redObj); 控制奖励里面的红点
 *       在主界面入口界面调用：  _RegisterRedDot("mainArean", redObj);  控制入口上的红点
 *       多层级的以此类推
 *       
 *       （_RegisterRedDot(path, obj)方法在BaseWindow上，支持条路径对应多个红点对象）
 *       
 *       我们永远只需要用SetRedDotValue方法改变最下层的红点的数据即可
 *       
 * 
 */

/// <summary>
/// 全局红点管理器
/// </summary>
public class RedDotManager 
{
    private static RedDotManager m_instance;
    public static RedDotManager Singleton
    {
        get
        {
            if (m_instance==null)
                m_instance = new RedDotManager();
            return m_instance;
        }
    }

    private RedDotManager()
    {
        m_redDotDict = new Dictionary<string, RedDot>();
    }

    private Dictionary<string, RedDot> m_redDotDict;    //红点信息集合

    /// <summary>
    /// 获取红点状态
    /// </summary>
    public bool GetRedDotValue(string path)
    {
        var redDot = _GetRedDot(path);
        if (redDot == null)
            return false;
        return redDot.value;
    }


    /// <summary>
    /// 设置红点状态
    /// </summary>
    public void SetRedDotValue(string path,bool value)
    {
        var redDot = _GetRedDot(path);
        if (redDot == null)
            return;
        if (redDot.childDict.Count > 0)
        {
            UnityEngine.Debug.LogErrorFormat("只能设置最底层红点的状态,路径:{0}",redDot.path);
            return;
        }
        redDot.SetValue(value);
    }

    /// <summary>
    /// 移除红点及其子红点
    /// </summary>
    public void RemoveRedDot(string path)
    {
        if (string.IsNullOrEmpty(path))
            return;
        if (!m_redDotDict.ContainsKey(path))
            return;
        var redDot = m_redDotDict[path];
        redDot.Remove();
        ClassCacheManager.Delete(ref redDot);
    }

    /// <summary>
    /// 获取红点
    /// </summary>
    private RedDot _GetRedDot(string path)
    {
        RedDot red = null;
        if (m_redDotDict.TryGetValue(path, out red))
            return red;
        return _CreateRedDot(path);
    }

    /// <summary>
    /// 创建红点
    /// </summary>
    private RedDot _CreateRedDot(string path)
    {
        if (string.IsNullOrEmpty(path))
        {
            UnityEngine.Debug.LogError("红点路径不能为空！！！");
            return null;
        }

        try
        {
            string[] names = path.Split('/');
            RedDot root = null;
            RedDot last = null;
            if (m_redDotDict.ContainsKey(names[0]))
                root = m_redDotDict[names[0]];
            else
                root = _CreateOneRedDot(names[0]);
            last = root;
            for (int i = 1; i < names.Length; ++i)
            {
                var name = names[i];
                var child = last.GetChild(name);
                if (child != null)
                {
                    last = child;
                    continue;
                }
                last = _CreateOneRedDot(name,last);
            }
            return last;
        }
        catch (Exception ex)
        {
            UnityEngine.Debug.LogErrorFormat("创建红点失败！！！,{0}  {1}  {2}",path,ex.Message,ex.StackTrace);
            return null;
        }
    }

    /// <summary>
    /// 创建单个红点
    /// </summary>
    private RedDot _CreateOneRedDot(string name, RedDot parent = null)
    {
        var redDot = ClassCacheManager.New<RedDot>();
        redDot.name = name;
        if (parent != null)
        {
            redDot.parent = parent;
            parent.childDict[name] = redDot;
            redDot.path = string.Format("{0}/{1}", parent.path,name);
        }
        else
        {
            redDot.path = name;
        }
        m_redDotDict[redDot.path] = redDot;
        return redDot;
    }
}

public class RedDot:IClassCache
{
    public override bool doCache { get { return true; } }
    public string name;
    public string path;
    public bool value;
    public RedDot parent;
    public Dictionary<string, RedDot> childDict;

    public RedDot()
    {
        childDict = new Dictionary<string, RedDot>();
    }


    public override void FakeCtr(IParam param)
    {
        base.FakeCtr(param);
        childDict.Clear();
        name = null;
        path = null;
        parent = null;
        value = false;
    }

    public override void FakeDtr()
    {
        base.FakeDtr();
    }

    public bool HasChild(string name)
    {
        return childDict.ContainsKey(name);
    }

    public RedDot GetChild(string name)
    {
        RedDot red = null;
        childDict.TryGetValue(name, out red);
        return red;
    }

    /// <summary>
    /// 设置红点状态
    /// </summary>
    public void SetValue(bool value)
    {
        if (this.value == value)
            return;
        this.value = value;

        var param = new TwoParam<string, bool>();
        param.value1 = path;
        param.value2 = value;
        GED.ED.dispatchEvent(EventID.HongDianChange, param);

        if (parent == null)
            return;
        if(value&&!parent.value)
            parent.SetValue(value);
        else if(!value&&parent.value)
            parent.ChangeValueByChilds();
    }

    /// <summary>
    /// 更新红点状态 by 子红点
    /// </summary>
    public void ChangeValueByChilds()
    {
        bool value = false;
        var enu = childDict.GetEnumerator();
        while (enu.MoveNext())
        {
            if (enu.Current.Value.value)
            {
                value = true;
                break;
            }
        }
        enu.Dispose();
        SetValue(value);
    }

    /// <summary>
    /// 移除红点及其子红点
    /// </summary>
    public void Remove()
    {
        var param = new TwoParam<string, bool>();
        param.value1 = path;
        param.value2 = false;
        GED.ED.dispatchEvent(EventID.HongDianChange, param);

        var enu = childDict.GetEnumerator();
        while (enu.MoveNext())
        {
            enu.Current.Value.Remove();
            var red = enu.Current.Value;
            ClassCacheManager.Delete(ref red);
        }
        enu.Dispose();
    }

    public override string ToString()
    {
        return string.Format("Name: {0}, Path: {1}, Value: {2}, Parent: {3}, ChildCount: {4}", name, path, value, parent.path, childDict.Count);
    }
}
