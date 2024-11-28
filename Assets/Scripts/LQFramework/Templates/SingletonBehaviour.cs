/*
 * file SingletonBehaviour.cs
 *
 * author: Pengmian
 * date:   2014/09/16 
 */

using UnityEngine;


/// <summary>
/// 单件模版 
/// 所有派生的单件对象都需要挂到GameObject上
/// </summary>
/// <typeparam name="T"></typeparam>
public class SingletonBehaviour<T> : BaseBehaviour where T : BaseBehaviour
{
    public static T Singleton;

    protected override void Awake()
    {
        Singleton = this as T;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        Singleton = null;
    }

}
