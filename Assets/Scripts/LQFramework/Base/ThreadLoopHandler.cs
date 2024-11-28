/* 
 * -----------------------------------------------
 * Copyright (c) 1ktower.com All rights reserved.
 * -----------------------------------------------
 * 
 * Coder：Zhou XiQuan
 * Time ：2017.06.01
*/

using System.Threading;
using System.Collections.Generic;

public class ThreadLoopHandler
{
    private static List<ThreadLoopHandler> m_threadList = new List<ThreadLoopHandler>();
    internal static void StopAll()
    {
        for(int i = 0; i < m_threadList.Count; ++i)
            m_threadList[i].Stop();
        m_threadList.Clear();
    }

    private int sleepTime;
    private bool m_running;
    private Thread m_thread;

    private object param;
    private System.Action<object> action;

    public ThreadLoopHandler(System.Action<object> handler, int sleepSpace = 100)
    {
        action = handler;
        sleepTime = sleepSpace;
        m_threadList.Add(this);
        m_thread = new Thread(run);
    }

    private void run()
    {
        while(m_running)
        {
            if(action != null)
                action(param);
            Thread.Sleep(sleepTime);
        }
    }

    public bool IsRunning
    {
        get
        {
            return m_running;
        }
    }

    public void SetParam(object obj)
    {
        param = obj;
    }

    public void Start()
    {
        if(m_running)
            return;

        m_running = true;
        if(m_thread == null)
        {
            m_thread = new Thread(run);
            m_threadList.Add(this);
        }
        m_thread.Start();
    }

    public void Stop()
    {
        m_running = false;
        m_thread.Join();
        m_thread = null;
    }
}