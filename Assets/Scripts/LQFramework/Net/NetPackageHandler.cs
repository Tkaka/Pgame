/* 
 * -----------------------------------------------
 * Copyright (c) 1ktower.com All rights reserved.
 * -----------------------------------------------
 * 
 * Coder：Zhou XiQuan
 * Time ：2017.11.09
*/

using System;
using System.IO;
using System.Collections.Generic;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.GZip;
using System.Runtime.InteropServices;

public class NetPackageHandler
{
    private class SendMsgEntry : IClassCache
    {
        public override bool doCache { get { return true; } }
        public IMsg msg;

        public int size;
        public int msgId;
        public byte[] buff;

        public override void FakeDtr()
        {
            base.FakeDtr();
            if(buff != null)
            {
                GlobalNetBytesCache.Free(buff);
                buff = null;
            }
            var cache = msg as IClassCache;
            if(cache != null)
                ClassCacheManager.Delete(ref cache);
            msg = null;
        }
    }

    // 每次最多可以发送的数据量 
    private static readonly int MAX_SEND_SIZE = 1024 * 8;
    // 待发送缓冲区
    private byte[] m_sendOnceBuffer = new byte[MAX_SEND_SIZE];
    // 待发送缓冲区不够时，发送了一半的缓冲区
    private byte[] m_remainBuffer = null;
    // m_remainBuffer待发送的长度
    private int m_remainBufferSize = 0;
    // m_remainBuffer当前位置
    private int m_remainBufferIdx = 0;
    // 发送序列号
    private static int Magic = 988;
    // 本次待发送的数据量
    private int m_sendOnceSize = 0;

    private NetByteBuffer receiveBuff = new NetByteBuffer(1024 * 30, true);
    private Queue<SendMsgEntry> msgQueue = new Queue<SendMsgEntry>();

    private System.Func<bool> connectFunc;
    private System.Action<byte[], int> sendFunc;

    public NetPackageHandler(System.Action<byte[], int> postFunc, System.Func<bool> connectCheck)
    {
        sendFunc = postFunc;
        connectFunc = connectCheck;
    }

    /// <summary>
    /// 添加待发送的消息到列表
    /// </summary>
    public void AddToSendQueue(int msgId, byte[] buff, int size)
    {
        SendMsgEntry msg = ClassCacheManager.New<SendMsgEntry>();
        msg.msgId = msgId;
        msg.size = size;
        msg.buff = buff;
        lock(msgQueue)
        {
            msgQueue.Enqueue(msg);
        }
    }

    /// <summary>
    /// 添加待发送的消息到列表
    /// </summary>
    public void AddToSendQueue(IMsg msg)
    {
        SendMsgEntry entry = ClassCacheManager.New<SendMsgEntry>();
        entry.msg = msg;
        lock(msgQueue)
        {
            msgQueue.Enqueue(entry);
        }
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    public void LoopSendPackage()
    {
        if(sendFunc == null)
            return;

        //断网后继续发送，相当于抛弃消息
        /*if(connectFunc != null && !connectFunc())
        {
            System.Threading.Thread.Sleep(100);
            return;
        }*/

        try
        {
            // 临时存在的未发送完成的缓冲区先处理
            if(m_remainBuffer != null && m_remainBufferSize > 0)
            {
                int freeSize = MAX_SEND_SIZE - m_sendOnceSize;
                if(m_remainBufferSize > freeSize)
                {
                    Array.Copy(m_remainBuffer, m_remainBufferIdx, m_sendOnceBuffer, m_sendOnceSize, freeSize);
                    m_remainBufferSize -= freeSize;
                    m_remainBufferIdx += freeSize;
                    m_sendOnceSize += freeSize;
                } else
                {
                    Array.Copy(m_remainBuffer, m_remainBufferIdx, m_sendOnceBuffer, m_sendOnceSize, m_remainBufferSize);
                    m_sendOnceSize += m_remainBufferSize;
                    GlobalNetBytesCache.Free(m_remainBuffer);
                    m_remainBufferSize = 0;
                    m_remainBufferIdx = 0;
                    m_remainBuffer = null;
                }
            }

            while(m_sendOnceSize < MAX_SEND_SIZE)
            {
                if(msgQueue.Count <= 0)
                    break;

                int size = 0;
                SendMsgEntry entry = null;
                lock(msgQueue)
                {
                    entry = msgQueue.Dequeue();
                }

                byte[] bytes = null;
                if(entry == null)
                    bytes = encodePackage(entry.msgId, entry.buff, entry.size, out size);
                else
                    bytes = encodePackage(entry.msg.GetMsgId(), entry.msg.GetMsgData(), entry.msg.GetMsgSize(), out size);
                ClassCacheManager.Delete(ref entry);

                // 当前可用的剩余缓冲区长度
                int freeSize = MAX_SEND_SIZE - m_sendOnceSize;
                if(size > freeSize)
                {
                    Array.Copy(bytes, 0, m_sendOnceBuffer, m_sendOnceSize, freeSize);
                    m_sendOnceSize += freeSize;
                    m_remainBuffer = bytes;
                    m_remainBufferSize = (size - freeSize);
                    m_remainBufferIdx += freeSize;
                } else
                {
                    Array.Copy(bytes, 0, m_sendOnceBuffer, m_sendOnceSize, size);
                    m_sendOnceSize += size;
                    GlobalNetBytesCache.Free(bytes);
                }
            }

            if(m_sendOnceSize > 0)
            {
                sendFunc(m_sendOnceBuffer, m_sendOnceSize);
                m_sendOnceSize = 0;
            }
        } catch(Exception ex)
        {
            logErr(ex.ToString());
        }
    }

    [StructLayout(LayoutKind.Auto)]
    private struct PackageHead
    {
        public int Length;
        public long Times;
        public int Magic;
        public int MsgId;
    }

    private byte[] encodePackage(int msgId, byte[] data, int len, out int sendSize)
    {
        PackageHead head;
        head.Times = TimeUtils.currentMilliseconds(false, true);
        head.MsgId = msgId;
        head.Magic = Magic++;
        head.Magic ^= (0xFE98 << 8);
        head.Length = 4 + 8 + 4 + 4 + len;
        head.Magic ^= head.Length;

        byte[] arr = GlobalNetBytesCache.Alloc(head.Length);
        //byte[] arr = new byte[head.Length];

        int offset = 0;
        XBuffer.WriteInt(head.Length, arr, ref offset);
        XBuffer.WriteLong(head.Times, arr, ref offset);
        XBuffer.WriteInt(head.Magic, arr, ref offset);
        XBuffer.WriteInt(head.MsgId, arr, ref offset);
        /*Array.Copy(BitConverter.GetBytes(head.Length.ToBigEndian()), 0, arr, offset, 4);
        offset += 4;
        Array.Copy(BitConverter.GetBytes(head.Times.ToBigEndian()), 0, arr, offset, 8);
        offset += 8;
        Array.Copy(BitConverter.GetBytes(head.Magic.ToBigEndian()), 0, arr, offset, 4);
        offset += 4;
        Array.Copy(BitConverter.GetBytes(head.MsgId.ToBigEndian()), 0, arr, offset, 4);
        offset += 4;*/
        Array.Copy(data, 0, arr, offset, len);

        sendSize = head.Length;
        return arr;
    }



    /// <summary>
    /// 解析消息包
    /// </summary>
    /// <param name="bytes">待解析的数据</param>
    /// <param name="toReadLength">消息长度</param>
    /// <param name="callback">回调 消息id, 消息内容，消息长度</param>
    public void DecodePackage(byte[] bytes, int toReadLength, Action<int, byte[], int> callback)
    {
        if(bytes == null || toReadLength <= 0)
        {
            logErr("接受数据异常: bytesToRead <= 0 || bytes == null");
            return;
        }
        if(callback == null)
        {
            logErr("解析消息的回调函数为空");
            return;
        }

        NetByteBuffer buffer = receiveBuff;
        if(toReadLength > buffer.canUse())
            buffer = resetByteBuffer(toReadLength);
        buffer.put(bytes, 0, toReadLength);
        GlobalNetBytesCache.Free(bytes);

        try
        {
            do
            {
                if(buffer.remaining() < 4)
                    break;

                int tmp = buffer.readInt32();
                int bufLen = (tmp & 0x7FFFFF);
                bool isZip = (((tmp >> 24) & 0xFFFFFFFF) == 1);
                if(bufLen > buffer.maxSize() - 4 || bufLen < 4)
                {
                    logErr("接受数据异常, 数据长度超出限制: " + bufLen + " " + tmp.ToBigEndian());
                    buffer.resetHead(4);
                    break;
                }

                if(buffer.remaining() < bufLen)
                {
                    buffer.resetHead(4);
                    break;
                }

                int msgId = buffer.readInt32();
                int size = bufLen - 4;
                if(size < 0)
                {
                    logErr("消息长度不对，已经小于0了!!");
                    break;
                }

                if(size == 0)
                {
                    callback(msgId, null, 0);
                    continue;
                }

                byte[] content = GlobalNetBytesCache.Alloc(size);//new byte[size];
                //// 压缩过的需要解压缩
                if(isZip)
                {
                    // 解压缩的临时数据从网络线程的缓存中分配
                    buffer.readBytes(content, 0, size);
                    int realSize;
                    byte[] data = unzipData(content, size, out realSize);
                    GlobalNetBytesCache.Free(content);
                    callback(msgId, data, realSize);
                } else
                {
                    // 没有压缩过的，直接抛出去， 该缓冲区是抛到应用层的，需要应用层主动回收
                    buffer.readBytes(content, 0, size);
                    callback(msgId, content, size);
                }
            } while(true);
        }
        catch(Exception e)
        {
            logErr("解析数据异常," + e.Message + "\n" + e.StackTrace);
        }
    }

    /// 解压zip
    private static byte[] buf = new byte[4096];
    private byte[] unzipData(byte[] content, int size, out int realSize)
    {
        try
        {
            using(MemoryStream inputStream = new MemoryStream(content, 0, size))
            {
                using(GZipInputStream gzipStream = new GZipInputStream(inputStream))
                {
                    using(MemoryStream outputStream = new MemoryStream())
                    {
                        StreamUtils.Copy(gzipStream, outputStream, buf);

                        outputStream.Position = 0;
                        realSize = (int)outputStream.Length;
                        byte[] output = GlobalNetBytesCache.Alloc(realSize);//new byte[size];
                        outputStream.Read(output, 0, realSize);
                        return output;
                    }
                }
            }
        } catch(Exception ex)
        {
            logErr(ex.ToString());
            realSize = 0;
        }
        return null;
    }

    private NetByteBuffer resetByteBuffer(int len)
    {
        NetByteBuffer buffer = new NetByteBuffer(receiveBuff.maxSize() * 2 + len, true);
        buffer.copyFrom(receiveBuff);
        receiveBuff = buffer;
        return receiveBuff;
    }

    private void logErr(string str)
    {
        UnityEngine.Debug.LogError(str);
    }
}