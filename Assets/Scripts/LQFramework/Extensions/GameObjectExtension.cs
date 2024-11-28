using UnityEngine;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// GameObject扩展类
/// </summary>
public static class GameObjectExtension
{
    /// <summary>
    /// 获得一个组件，不存在则添加
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="go"></param>
    /// <returns></returns>
    public static T GetOrAddComponent<T>(this GameObject go) where T : Component
    {
        T ret = go.GetComponent<T>();
        if (ret == null)
            ret = go.AddComponent<T>();
        return ret;
    }

    /// <summary>
    /// GetComponent获取接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="inObj"></param>
    /// <returns></returns>
    public static T GetInterface<T>(this GameObject inObj) where T : class
    {
        if (!typeof(T).IsInterface)
        {
            Debug.LogError(typeof(T).ToString() + ": is not an actual interface!");

            return null;
        }
        var tmps = inObj.GetComponents<Component>().OfType<T>();
        if (tmps.Count() == 0) return null;
        return tmps.First();
    }

    /// <summary>
    /// GetComponent获取所用接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="inObj"></param>
    /// <returns></returns>
    public static IEnumerable<T> GetInterfaces<T>(this GameObject inObj) where T : class
    {
        if (!typeof(T).IsInterface)
        {
            Debug.LogError(typeof(T).ToString() + ": is not an actual interface!");
            return Enumerable.Empty<T>();
        }
        return inObj.GetComponents<Component>().OfType<T>();
    }


    public static GameObject[] getChilds(this GameObject obj)
    {
        GameObject[] objs = new GameObject[obj.transform.childCount];
        for (int i = 0; i < obj.transform.childCount; i++)
        {
            objs[i] = obj.transform.GetChild(i).gameObject;
        }
        return objs;
    }

    public static void removeComponent<T>(this GameObject obj) where T: Component
    {
        GameObject.Destroy(obj.GetComponent<T>());
    }

    public static void setLayer(this GameObject obj, string layerName)
    {
        if (obj == null)
            return;
        int layer = LayerMask.NameToLayer(layerName);
        Transform[] childs = obj.transform.GetComponentsInChildren<Transform>(true);
        foreach (Transform child in childs)
            child.gameObject.layer = layer;
    }

}

