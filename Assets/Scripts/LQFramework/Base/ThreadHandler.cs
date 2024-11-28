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

public class ThreadHandler
{
    private static List<ThreadHandler> m_threadList = new List<ThreadHandler>();
    public static void StopAll()
    {
        for(int i=0; i < m_threadList.Count; ++i)
        {
            if(m_threadList[i].IsRunning)
                m_threadList[i].Stop();
        }
        m_threadList.Clear();
        ThreadLoopHandler.StopAll();
    }

    private bool m_running;
    private Thread m_thread;
    private Queue<Handler> m_queue = new Queue<Handler>();
    private Queue<Handler> m_addQueue = new Queue<Handler>();
    private long m_freeAutoStopTimeout;
    private long m_freeTime;
    private bool m_doing;
    private int sleepTime;
    public ThreadHandler(int sleepSpace = 100)
    {
        m_thread = new Thread(run);
        sleepTime = sleepSpace;
        m_threadList.Add(this);
    }

    private void run()
    {
        while(m_running)
        {
            lock(m_addQueue)
            {
                while(m_addQueue.Count > 0)
                    m_queue.Enqueue(m_addQueue.Dequeue());
            }
            
            if(m_queue.Count > 0)
            {
                m_freeTime = 0;
                m_doing = true;
                Handler handler = m_queue.Dequeue();
                if(handler != null)
                    handler.action(handler.param);
                m_doing = false;
            } else
            {
                /*if(m_freeAutoStopTimeout >= 0)
                {
                    m_freeTime += 100;
                    if(m_freeTime >= m_freeAutoStopTimeout)
                        m_running = false;
                }*/
            }
            Thread.Sleep(sleepTime);
        }
    }

    public bool IsRunning
    {
        get{
            return m_running;
        }
    }

    public bool IsFree
    {
        get{
            return m_queue.Count == 0 && !m_doing;
        }
    }

    ///开始线程
    ///freeAutoStopTimeout 空闲时自动退出时间
    public void Start(long freeAutoStopTimeout = -1)
    {
        if(!m_running)
        {
            m_freeAutoStopTimeout = freeAutoStopTimeout;
            m_running = true;
            if(m_thread == null)
            {
                m_thread = new Thread(run);
                m_threadList.Add(this);
            }
            m_thread.Start();
        }
    }

    //停止线程
    public void Stop()
    {
        if(m_running)
        {
            m_running = false;
            m_thread.Join();

            m_doing = true;
            while(m_queue.Count > 0)
            {
                Handler handler = m_queue.Dequeue();
                if(handler != null)
                    handler.action(handler.param);
            }
            while(m_addQueue.Count > 0)
            {
                Handler handler = m_addQueue.Dequeue();
                if(handler != null)
                    handler.action(handler.param);
            }
            m_doing = false;
            m_thread = null;
        }
    }
    
    public void PushHandler(System.Action<object> handler, object param)
    {
        lock(m_addQueue)
        {
            if(handler != null)
                m_addQueue.Enqueue(new Handler { action = handler, param = param });
        }
    }

    private class Handler
    {
        public System.Action<object> action;
        public object param;
    }
}