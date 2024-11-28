using System;
using System.Collections;
using System.Collections.Generic;


    public class CTimerManager
    {
        private class CTimer
        {
            public ulong id = 0;
            public float time = 0;
            public int calledNum = 0;
            public float firstDelay = 0;
            public float delay = 0;
            public int count = 1;
            public object callObj = null;
            public Action<IParam> func = null;
            public IParam param = null;


            public CTimer()
            {
                id = 0;
                time = 0;
                calledNum = 0;
                firstDelay = 0;
                delay = 0;
                count = 1;
                callObj = null;
                func = null;
                param = null;
            }

        }

        private ulong m_idxNow = 0;
        private Dictionary<ulong, CTimer> m_timers = new Dictionary<ulong, CTimer>();
        private List<ulong> m_deleteTimerIds = new List<ulong>();
        private List<ulong> m_deleteImmediateIds = new List<ulong>();
        private List<CTimer> m_addTimerList = new List<CTimer>();

        public CTimerManager()
        {
            m_idxNow = 0;
        }

        public ulong AddTimer(float firstDelay, float delay, int count, object callObj, Action<IParam> func, IParam datas = null)
        {
            CTimer timer = new CTimer();
            timer.id = _NewId();
            timer.firstDelay = firstDelay;
            timer.delay = delay;
            timer.count = count;
            timer.callObj = callObj;
            timer.func = func;
            timer.param = datas;

            if (firstDelay == 0)
            {
                timer.calledNum = 1;
                func(datas);
                if (timer.count == 1)
                {
                    return timer.id;
                }
            }
            //m_timers[timer.id] = timer;
            m_addTimerList.Add(timer);
            return timer.id;
        }

        public ulong AddDelay(float delaytime, object callObj, Action<IParam> func, IParam datas = null)
        {
            return AddTimer(delaytime, delaytime, 1, callObj, func, datas);
        }

        public void Update(float dt)
        {
            //添加
            for (int a = 0; a < m_addTimerList.Count; ++a)
            {
                CTimer timer = m_addTimerList[a];
                m_timers.Add(timer.id, timer);
            }
            m_addTimerList.Clear();

            //移除
            for(int a = 0; a < m_deleteTimerIds.Count; ++a)
            {
                ulong key = m_deleteTimerIds[a];
                CTimer timer = null;
                if(m_timers.TryGetValue(key, out timer) && timer != null)
                {
                    m_timers.Remove(key);
                    timer = null;
                }
            }
            m_deleteTimerIds.Clear();

            Dictionary<ulong, CTimer>.Enumerator it = m_timers.GetEnumerator();
            while (it.MoveNext())
            {
                CTimer timer = it.Current.Value;
                timer.time += dt;
                if(m_deleteImmediateIds.Contains(timer.id))
                    continue;

                if (timer.calledNum == 0)
                {
                    //第一次
                    if (timer.firstDelay <= timer.time)
                    {
                        timer.time = timer.time - timer.firstDelay;
                        timer.func(timer.param);
                        timer.calledNum++;

                        if (timer.count != -1 && timer.count <= timer.calledNum)
                        {
                            m_deleteTimerIds.Add(it.Current.Key);
                        }
                    }
                }
                else
                {
                    //循环
                    if (timer.delay <= timer.time)
                    {
                        timer.time = timer.time - timer.delay;
                        timer.func(timer.param);
                        timer.calledNum++;

                        if (timer.count != -1 && timer.count <= timer.calledNum)
                        {
                            m_deleteTimerIds.Add(it.Current.Key);
                        }
                    }
                }
            }
            it.Dispose();

            m_deleteImmediateIds.Clear();
        }


        public void RemoveId(ulong id, bool immediate = false)
        {
            m_deleteTimerIds.Add(id);
            if(immediate)
                m_deleteImmediateIds.Add(id);
        }

        //private List<ulong> m_keysTemp = new List<ulong>();
        public void RemoveObj(object obj, bool immediate = false)
        {
            if (obj != null)
            {
                var it = m_timers.GetEnumerator();
                while (it.MoveNext())
                {
                    //KeyValuePair<ulong, CTimer> pair = (KeyValuePair<ulong, CTimer>)it.Current;
                    if (object.ReferenceEquals(it.Current.Value, obj))
                    {
                        m_deleteTimerIds.Add(it.Current.Key);
                        if(immediate)
                            m_deleteImmediateIds.Add(it.Current.Key);
                    }
                }
            }
        }

        private ulong _NewId()
        {
            while (true)
            {
                m_idxNow++;
                if (m_timers.ContainsKey(m_idxNow) == false && m_idxNow != 0)
                {
                    return m_idxNow;
                }
            }
        }

        private static CTimerManager m_instance = null;
        public static CTimerManager GetInstance()
        {
            if (m_instance == null)
            {
                m_instance = new CTimerManager(); ;
            }
            return m_instance;
        }
    }

