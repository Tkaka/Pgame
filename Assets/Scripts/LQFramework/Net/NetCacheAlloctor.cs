using System;
using System.Collections.Generic;
public class NetCacheAlloctor
{
    private class DefaultComparer : IEqualityComparer<int>
    {

        public bool Equals(int x, int y)
        {
            return x == y;
        }

        public int GetHashCode(int obj)
        {
            return obj;
        }
    }

    private Dictionary<int, Queue<byte[]>> m_cacheQueues = new Dictionary<int,Queue<byte[]>>(new DefaultComparer());

    private int m_minSize = 0;
    private int m_maxSize = 0;
    private int m_increase = 0;

    public bool Init(int maxSize, int minSize, int increase, int count)
    {
        if (minSize <= 0 || maxSize < minSize || count <= 0)
        {
            return false;
        }

        m_maxSize = maxSize;
        m_minSize = minSize;
        m_increase = increase;

        for (int a = m_minSize; a <= m_maxSize; a += m_increase)
        {
            Queue<byte[]> queue = new Queue<byte[]>();
            for (int b = 0; b < count; ++b)
            {
                queue.Enqueue(new byte[a]);
            }
            m_cacheQueues.Add(a, queue);
        }
        return true;
    }

    public void FreeAll()
    {
        Dictionary<int, Queue<byte[]>>.Enumerator it = m_cacheQueues.GetEnumerator();
        while(it.MoveNext())
        {
            Queue<byte[]> queue = it.Current.Value;
            queue.Clear();
        }
        m_cacheQueues.Clear();
    }

    public byte[] Alloc(int size)
    {
        //if (size <= 0)
        //    return null;

        int lvl = _GetLevelBySize(size);
        if (lvl == -1)
        {
            return new byte[size];
        }
        else
        {
            byte[] result = null;
            Queue<byte[]> queue = null;
            if (m_cacheQueues.TryGetValue(lvl, out queue) && queue != null)
            {
                try
                {
                    //lock(queue)
                    {
                        result = queue.Dequeue();
                        Array.Clear(result, 0, result.Length - 1);
                    }
                }
                catch (InvalidOperationException)
                {
                    // 空队列    
                    result = new byte[lvl];
                }
            }


            //Report.ReportError("分配缓冲区长度： " + result.Length);
            return result;   
        }
    }
      
    public void Free(byte[] bytes)
    {

        //Report.ReportError("归还缓冲区长度： " + bytes.Length);
       
        if (bytes == null || bytes.Length <= 0)
            return;

        int lvl = _GetLevelBySize(bytes.Length);        // 4字节的头长度
        if (lvl == -1)
        {
            bytes = null;
            return;
        }

        Queue<byte[]> queue = null;
        if (m_cacheQueues.TryGetValue(lvl, out queue) && queue != null)
        {
            if (bytes.Length == lvl && lvl != -1)
            {
                queue.Enqueue(bytes);
            }
            else
            {
                UnityEngine.Debug.LogError("归还的缓冲区长度不是key：" + lvl + " " + bytes.Length);
            }
        }
        
        bytes = null;
    }

    private int _GetLevelBySize(int size)
    {
        int needSize = size;
        if (needSize > m_maxSize)
            return -1;

        if (needSize <= m_minSize)
            return m_minSize; 

        for (int a = m_minSize; a <= m_maxSize; a += m_increase)
        {
            if (needSize <= a)
            {
                return a;    
            }
        }
        return -1;
    }


    internal static void FillBuffer(byte[] m_readBuffer, int p, byte[] buffer, int bytesToRead)
    {
        Array.Copy(m_readBuffer, 0, buffer, 0, bytesToRead);
    }
}

