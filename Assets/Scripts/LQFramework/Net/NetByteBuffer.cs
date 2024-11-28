using System;

public class NetByteBuffer
{
    private bool mNetOrder;
    private int mMaxSize;
    private CircularBuffer<Byte> mBuffer;

    public NetByteBuffer(int maxSize, bool netOrder)
    {
        mMaxSize = maxSize;
        mBuffer = new CircularBuffer<byte>(maxSize);
        mNetOrder = netOrder;
    }

    public void clear()
    {
        mBuffer.Clear();
    }


    public int put(byte[] bytes, int offset, int len)
    {
        return mBuffer.Put(bytes, offset, len);
    }

    public int put(byte[] bytes)
    {
        return mBuffer.Put(bytes);
    }

    public void resetHead(int size)
    {
        mBuffer.ResetHead(size);
    }


    public int readInt32()
    {
        byte[] data = mBuffer.Get(4);
        int ret = BitConverter.ToInt32(data, 0);
        if(mNetOrder)
            ret = ret.ToBigEndian();
        return ret;

    }

    public int readBytes(byte[] dst, int offset, int length)
    {
        if(mBuffer.Size >= length)
        {
            mBuffer.Get(dst, 0, length);
            return length;
        } else
        {
            //     Logger.log("剩余数据不够！", Thread.CurrentThread.ManagedThreadId);
            return 0;
        }
    }

    public int remaining()
    {
        return mBuffer.Size;
    }

    public int maxSize()
    {
        return mMaxSize;
    }

    public void copyFrom(NetByteBuffer other)
    {
        if(other.mBuffer.Size <= 0)
            return;

        byte[] bytes = new byte[other.mBuffer.Size];
        other.readBytes(bytes, 0, bytes.Length);
        mBuffer.Put(bytes);
    }

    public int canUse()
    {
        return mBuffer.Capacity - mBuffer.Size;
    }

    public byte readByte()
    {
        byte[] bytes = mBuffer.Get(1);
        return bytes[0];
    }
}