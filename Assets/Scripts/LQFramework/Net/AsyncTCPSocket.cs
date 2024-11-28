/* 
 * -----------------------------------------------
 * Copyright (c) 1ktower.com All rights reserved.
 * -----------------------------------------------
 * 
 * Coder：Zhou XiQuan
 * Time ：2017.11.09
*/

using System;
using System.Net.Sockets;
using System.Runtime.InteropServices;

public enum NetCode
{
    Unknown = -100,
    CreateTCPErr,
    ConnectErr,
    EndConnectErr,
    ConnectFalse,
    ReciveDisconnect,
    CloseErr,
    TCPNullErr,
    SendErr,
    ReceiveErr,
    EndReadErr,
    IsConnecting,
    IsConnected,

    TimeOut = -111,     //超时
    Success = -222,     //连接成功
    Closed  = -333,     //断开连接
}

public class AsyncTCPSocket
{
    private enum SocketStatus
    {
        Unconnect,
        Connecting,
        Connected
    }

    private enum LogType
    {
        Err,
        Wrn,
        Log
    }

#if UNITY_IPHONE && !UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern string getIPv6(string mHost, string mPort);
#endif

    //"192.168.1.1&&ipv4"
    private static string GetIPv6(string mHost, string mPort)
    {
#if UNITY_IPHONE && !UNITY_EDITOR
		string mIPv6 = getIPv6(mHost, mPort);
		return mIPv6;
#else
        return mHost + "&&ipv4";
#endif
    }

    private int port;
    private string ip;
    private TcpClient client;
    private SocketStatus netStatus;

    private Action<NetCode> connectRetCb;
    private Action<NetCode> disconnectedCb;
    private Action<int, byte[], int> receiveCb;

    private IAsyncResult readRet;
    private IAsyncResult connectRet;

    private byte[] readBuffer;
    private ThreadHandler normalHandler;
    private ThreadLoopHandler sendHandler;
    private NetPackageHandler msgHandler;

    public AsyncTCPSocket()
    {
        readBuffer = new byte[1024 * 8];
        netStatus = SocketStatus.Unconnect;
        msgHandler = new NetPackageHandler(sendBuff, IsConnected);
    }

    private void createClient()
    {
        if(client != null)
            return;

        AddressFamily ipType = AddressFamily.InterNetwork;
        try
        {
            string ipv6 = GetIPv6(ip, port.ToString());
            if(!string.IsNullOrEmpty(ipv6))
            {
                string[] tmp = System.Text.RegularExpressions.Regex.Split(ipv6, "&&");
                if(tmp != null && tmp.Length >= 2)
                {
                    string type = tmp[1];
                    if(type == "ipv6")
                    {
                        ip = tmp[0];
                        ipType = AddressFamily.InterNetworkV6;
                    }
                }
            }
        }catch(Exception e)
        {
            LogMsg(e.Message + "\n" + e.StackTrace);
            notifyConnectRet(NetCode.CreateTCPErr);
        }
        client = new TcpClient(ipType);
    }

    /// 启动后台线程
    private void startBackThread()
    {
        if(normalHandler == null || !normalHandler.IsRunning)
        {
            normalHandler = new ThreadHandler(3);
            normalHandler.Start();
        }

        if(sendHandler == null || !sendHandler.IsRunning)
        {
            sendHandler = new ThreadLoopHandler(sendProxy, 10);
            sendHandler.Start();
        }
    }

    /// <summary>
    /// 连接服务器
    /// </summary>
    /// <param name="onConnect">连接成功回调</param>
    /// <param name="onDisconnect">连接失败回调</param>
    /// <param name="onReceive">收到数据回调(msgId, data, dataLength)</param>
    public void ConnectServer(string _ip, int _port, Action<NetCode> onConnect, Action<NetCode> onDisconnect, Action<int, byte[], int> onReceive)
    {
        startBackThread();
        ip = _ip;
        port = _port;
        receiveCb = onReceive;
        connectRetCb = onConnect;
        disconnectedCb = onDisconnect;
        normalHandler.PushHandler(threadConnectServer, null);
    }

    /// 异步连接网络
    private void threadConnectServer(object obj)
    {
        LogMsg("请求连接服务器 " + ip + ":" + port, LogType.Log);
        if(netStatus == SocketStatus.Connecting)
        {
            notifyConnectRet(NetCode.IsConnecting);
            return;
        }
        if(netStatus == SocketStatus.Connected)
        {
            notifyConnectRet(NetCode.IsConnected);
            return;
        }

        createClient();
        try
        {
            netStatus = SocketStatus.Connecting;
            connectRet = client.BeginConnect(ip, port, null, null);
            //同一线程处理连接超时，否则会存在多线程问题
            if(connectRet.AsyncWaitHandle.WaitOne(5000, true))
                onConnectFinish(connectRet);
            else
                onConnectTimeOut(connectRet);
        } catch(Exception e)
        {
            client = null;
            netStatus = SocketStatus.Unconnect;
            LogMsg(e.Message + "\n" + e.StackTrace);
            SocketException sex = e as SocketException;
            if(sex != null)
                notifyConnectRet((NetCode)sex.ErrorCode);
            else
                notifyConnectRet(NetCode.CreateTCPErr);
        }
    }

    /// <summary>
    /// 连接完成,后台线程
    /// </summary>
    private void onConnectFinish(IAsyncResult ret)
    {
        if(client == null)
        {
            netStatus = SocketStatus.Unconnect;
            notifyConnectRet(NetCode.TCPNullErr);
            return;
        }

        try
        {
            client.EndConnect(ret);
        }catch(Exception e)
        {
            netStatus = SocketStatus.Unconnect;
            LogMsg(e.Message + "\n" + e.StackTrace);
            notifyConnectRet(NetCode.EndConnectErr);
            return;
        }

        if(!client.Connected)
        {
            LogMsg("连接完成后 状态为未连接");
            netStatus = SocketStatus.Unconnect;
            notifyConnectRet(NetCode.ConnectFalse);
            return;
        }

        LogMsg("连接服务器成功", LogType.Log);
        netStatus = SocketStatus.Connected;
        notifyConnectRet(NetCode.Success);
        beginReceiveData();
    }

    /// <summary>
    /// 关闭连接
    /// </summary>
    /// <param name="notify">是否通知逻辑层</param>
    public void CloseSocket(bool notify = false)
    {
        object obj = notify ? new object() : null;
        if(normalHandler != null)
            normalHandler.PushHandler(threadCloseSocket, obj);
    }

    /// 异步关闭连接
    private void threadCloseSocket(object obj)
    {
        bool notify = obj != null;
        if(netStatus == SocketStatus.Unconnect)
            return;

        netStatus = SocketStatus.Unconnect;
        if(client != null)
        {
            try
            {
                if(readRet != null)
                {
                    readRet.AsyncWaitHandle.Close();
                    readRet = null;
                }
                if(client.Connected)
                    client.GetStream().Close();
                client.Close();
            } catch(Exception e)
            {
                LogMsg(e.Message + "\n" + e.StackTrace);
                notifyDisconnectRet(NetCode.CloseErr);
            }
            client = null;
        }

        if(notify)
            notifyDisconnectRet(NetCode.Closed);
    }

    /// <summary>
    /// 连接超时
    /// </summary>
    private void onConnectTimeOut(IAsyncResult ret)
    {
        if(netStatus == SocketStatus.Connecting)
        {
            CloseSocket();
            notifyConnectRet(NetCode.TimeOut);
        }
    }

    /// <summary>
    /// 通知逻辑层连接
    /// </summary>
    private void notifyConnectRet(NetCode code)
    {
        if(connectRetCb != null)
            connectRetCb(code);
    }

    /// <summary>
    /// 通知逻辑层断开连接
    /// </summary>
    private void notifyDisconnectRet(NetCode code)
    {
        if(disconnectedCb != null)
            disconnectedCb(code);
    }

    /// <summary>
    /// 开始接受数据,后台线程
    /// </summary>
    private void beginReceiveData()
    {
        if(client == null || !client.Connected)
        {
            LogMsg("接收消息时连接不存在");
            notifyDisconnectRet(NetCode.ConnectFalse);
            return;
        }

        try
        {
            readRet = client.GetStream().BeginRead(readBuffer, 0, readBuffer.Length, onReceiveData, null);
        }catch(Exception e)
        {
            LogMsg("接受消息发生异常" + e.Message + "\n" + e.StackTrace);
            SocketException sex = e as SocketException;
            if(sex != null)
                notifyDisconnectRet((NetCode)sex.ErrorCode);
            else
                notifyDisconnectRet(NetCode.ReceiveErr);
        }
    }

    private void onReceiveData(IAsyncResult ret)
    {
        normalHandler.PushHandler(threadReceiveData, ret);
    }

    /// 异步接收数据
    private void threadReceiveData(object obj)
    {
        IAsyncResult ret = obj as IAsyncResult;
        if(ret == null) return;
        try
        {
            if(client != null && client.Connected)
            {
                int bytesToRead = 0;
                bytesToRead = client.GetStream().EndRead(ret);
                if(bytesToRead <= 0)
                {
                    LogMsg("断开连接, 收到消息时可读长度异常: " + bytesToRead);
                    notifyDisconnectRet(NetCode.EndReadErr);
                    CloseSocket(true);
                } else
                {
                    byte[] buffer = GlobalNetBytesCache.Alloc(bytesToRead);//new byte[bytesToRead];
                    Array.Copy(readBuffer, 0, buffer, 0, bytesToRead);
                    beginReceiveData();
                    msgHandler.DecodePackage(buffer, bytesToRead, receiveCb);
                }
            } else
            {
                LogMsg("收到消息时连接断开 client == null || !client.Connected");
                notifyDisconnectRet(NetCode.EndReadErr);
            }
        } catch(Exception e)
        {
            LogMsg("EndRead异常 " + e.Message + "\n" + e.StackTrace);
            SocketException sex = e as SocketException;
            if(sex != null)
                notifyDisconnectRet((NetCode)sex.ErrorCode);
            else
                notifyDisconnectRet(NetCode.EndReadErr);
            CloseSocket(true);
        }
    }

    /// <summary>
    /// 是否处于连接状态
    /// </summary>
    public bool IsConnected()
    {
        return client != null && client.Connected;
    }

    private void sendProxy(object obj)
    {
        if(msgHandler != null)
            msgHandler.LoopSendPackage();
    }

    /// <summary>
    /// 对外发送消息接口
    /// </summary>
    /// <param name="msgId">消息id</param>
    /// <param name="buff">内容</param>
    /// <param name="size">大小</param>
    public void SendToServer(int msgId, byte[] buff, int size)
    {
        if(msgHandler != null)
            msgHandler.AddToSendQueue(msgId, buff, size);
    }

    public void SendToServer(IMsg msg)
    {
        if(msgHandler != null)
            msgHandler.AddToSendQueue(msg);
    }

    /// 异步发送数据
    private void sendBuff(byte[] buffer, int size)
    {
        if(netStatus != SocketStatus.Connected)
        {
            LogMsg("发送消息时网络未连接: " + netStatus);
            return;
        }

        if(client == null || !client.Connected)
        {
            LogMsg("发送消息时网络状态未连接: " + client.ToString());
            return;
        }

        try
        {
            client.GetStream().Write(buffer, 0, size);
        }
        catch (Exception e)
        {
            LogMsg(e.Message + "\n" + e.StackTrace);
            notifyConnectRet(NetCode.SendErr);
        }
    }

    /// 日志
    private void LogMsg(string str, LogType type = LogType.Err)
    {
        switch(type)
        {
            case LogType.Log:
                UnityEngine.Debug.Log(str);
                break;
            case LogType.Wrn:
                UnityEngine.Debug.LogWarning(str);
                break;
            case LogType.Err:
                UnityEngine.Debug.LogError(str);
                break;
        }
    }
}