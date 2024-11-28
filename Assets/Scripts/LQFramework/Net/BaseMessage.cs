using System;
using System.IO;
using System.Text;

public interface IMsg
{
    int GetMsgId();
    int GetMsgSize();
    byte[] GetMsgData();
}

public class BaseMessage : IClassCache, IMsg
{
    private static byte[] tmpBuffer = GlobalNetBytesCache.Alloc(1024*8);

    private byte[] m_data;
    private int m_size;

    public virtual void Read(byte[] buffer, ref int offset)
    {

    }

    public virtual int Read(byte[] buffer, int offset)
    {
        return offset;
    }

    public virtual void Write(byte[] buffer,ref int offset)
    {
        
    }

    public virtual int Write(byte[] buffer, int offset)
    {
        return offset;
    }

    public virtual int WriteWithType(byte[] buffer, int offset)
    {
        return offset;
    }

    public virtual void Reset()
    {

    }

    public virtual int GetMsgId()
    {
        return 0;
    }

    public int GetMsgSize()
    {
        if (m_data == null)
            GetMsgData();
        return m_size;
    }

    public byte[] GetMsgData()
    {
        if (m_data == null)
        {
            try
            {
                int offset = 0;
                Write(tmpBuffer, ref offset);
                m_data = GlobalNetBytesCache.Alloc(offset);
                m_size = offset;
                Array.Copy(tmpBuffer, m_data, offset);
            }
            catch (Exception ex)
            {
                if (ex is XBufferOutOfIndexException)
                {
                    GlobalNetBytesCache.Free(tmpBuffer);
                    tmpBuffer = GlobalNetBytesCache.Alloc(tmpBuffer.Length*2);
                    return GetMsgData();
                }
                else
                {
                    UnityEngine.Debug.LogError(ex.Message+"\n"+ex.StackTrace);
                }
            }
            
        }
        return m_data;
    }

    public byte[] GetNewMsgData()
    {
        if (m_data == null)
        {
            try
            {
                int offset = Write(tmpBuffer, 0);
                m_data = GlobalNetBytesCache.Alloc(offset);
                m_size = offset;
                Array.Copy(tmpBuffer, m_data, offset);
            }
            catch (Exception ex)
            {
                if (ex is XBufferOutOfIndexException)
                {
                    GlobalNetBytesCache.Free(tmpBuffer);
                    tmpBuffer = GlobalNetBytesCache.Alloc(tmpBuffer.Length * 2);
                    return GetNewMsgData();
                }
                else
                {
                    UnityEngine.Debug.LogError(ex.Message + "\n" + ex.StackTrace);
                }
            }

        }
        return m_data;
    }

    public override void FakeDtr()
    {
        GlobalNetBytesCache.Free(m_data);
        m_data = null;
        m_size = 0;
    }
}

public class BaseMsgStruct:IClassCache
{
    public virtual void Write(byte[] buffer, ref int offset)
    {

    }

    public virtual void WriteWithType(byte[] buffer, ref int offset)
    {
        
    }

    public virtual void Read(byte[] buffer, ref int offset)
    {
        
    }

}
