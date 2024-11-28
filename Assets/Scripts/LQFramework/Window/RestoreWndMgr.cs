//还原窗口管理类
using System.Collections.Generic;
using System;
using System.Reflection;

public class RestoreWndMgr
{
    private class RestoreData
    {
        public Action func;
        public Type type;
    }
    protected static RestoreWndMgr mSingleton = null;
    private Stack<RestoreData> m_wndDataStack = new Stack<RestoreData>();

    public static RestoreWndMgr Singleton
    {
        get
        {
            if (mSingleton == null)
            {
                mSingleton = new RestoreWndMgr();
            }
            return mSingleton;
        }
    }


    public void SaveWndData<T>(WinInfo wndData = null, UILayer pop = UILayer.Popup) where T : BaseWindow, new()
    {
        RestoreData data = new RestoreData();
        data.func = () => { WinMgr.Singleton.Open<T>(wndData, pop); };
        data.type = typeof(T);
        bool isNoHave = true;
        foreach (var info in m_wndDataStack)
        {
            if (info.type == data.type)
            {
                data.func = info.func;
                isNoHave = false;
                break;
            }
        }

        if(isNoHave)
            m_wndDataStack.Push(data);
    }

    public void RestoreWnd()
    {
        while (m_wndDataStack.Count > 0)
        {
            RestoreData data = m_wndDataStack.Pop();
            if(data.func != null)
                data.func();
        }

    }

    public void ClearData()
    {
        m_wndDataStack.Clear();
    }

    public void RemoveData<T>()
    {
        Type type = typeof(T);
        foreach (var info in m_wndDataStack)
        {
            if (info.type == type)
            {
                info.func = null;
                break;
            }
        }
    }

}