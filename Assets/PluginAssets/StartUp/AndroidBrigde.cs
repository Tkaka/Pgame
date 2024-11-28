using System;
using UnityEngine;

public class AndroidBrigde
{
    private AndroidJavaObject m_javaObject;

    private static AndroidBrigde m_instance;
    public static AndroidBrigde Singleton
    {
        get
        {
            if (m_instance == null)
                m_instance = new AndroidBrigde();
            return m_instance;
        }
    }

    private AndroidBrigde()
    {
        RefreshCurrentActivity();
    }

    public AndroidJavaObject GetCurrentActivity(bool fromCache = true)
    {
        if(fromCache)
            return m_javaObject;
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        return jc.GetStatic<AndroidJavaObject>("currentActivity");
    }

    public void RefreshCurrentActivity()
    {
        using(AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            m_javaObject = jc.GetStatic<AndroidJavaObject>("currentActivity");
        }
    }

    public void CallStaticMethod(string method, params object[] parameters)
    {
        try
        {
            m_javaObject.CallStatic(method, parameters);
        }
        catch (Exception ex)
        {
            Debug.LogError(ex);
        }
    }


    public T CallStaticMethod<T>(string method, params object[] parameters)
    {
        try
        {
            return m_javaObject.CallStatic<T>(method, parameters);
        }
        catch (Exception ex)
        {
            Debug.LogError(ex);
        }
        return default(T);
    }
        
    public void CallMethod(string method, params object[] parameters)
    {
        try
        {
            m_javaObject.Call(method, parameters);
        }
        catch (Exception ex)
        {
            Debug.LogError(ex);
        }
    }


    public T CallMethod<T>(string method, params object[] parameters)
    {
        try
        {
            return m_javaObject.Call<T>(method, parameters);
        }
        catch(Exception ex)
        {
            Debug.LogError(ex);
        }
        return default(T);
    }
}
