using System.Collections.Generic;

public class MessageHandle
{
    #region util class
    private abstract class IBackAna
    {
        public virtual BaseMessage New() { return null; }
    }

    private class BackAna<T> : IBackAna where T : BaseMessage, new()
    {
        public override BaseMessage New()
        {
            return ClassCacheManager.New<T>();
        }
    }
    #endregion

    private const float DISPATCH_MAX_TIME = 0.03f;  //每一帧最大的派发事件时间，超过这个时间则停止派发，等到下一帧再派发

    private static MessageHandle instance = null;
    public static MessageHandle GetInstance()
    {
        if(instance == null)
        {
            instance = new MessageHandle();
        }
        return instance;
    }

    public const int ConnectSucceedEvt   = 101; //连接成功
    public const int DisconnectEvt       = 102; //连接断开

    private Dictionary<int, AsyncTCPSocket> sockets = new Dictionary<int, AsyncTCPSocket>();
    // 消息队列
    private CrossThreadQueue m_Queue = new CrossThreadQueue();
    // RMessage缓存
    private Queue<RMessage> mMsgCache = new Queue<RMessage>();
    //后台解析列表
    private Dictionary<int, IBackAna> backAnalyzeMap = new Dictionary<int, IBackAna>();

    private RMessage _GetMsg()
    {
        RMessage rMsg = null;
        lock(mMsgCache)
        {
            if(mMsgCache.Count > 0)
                rMsg = mMsgCache.Dequeue();
        }

        if(rMsg == null)
            rMsg = new RMessage();
        return rMsg;
    }

    /// <summary>
    /// 注册后台线程解析消息
    /// </summary>
    /// <param name="msgId">消息id</param>
    /// <param name="msgType">消息类型</param>
    public void RegisterBackAnalyzeMsg<T>(int msgId) where T : BaseMessage, new()
    {
        if (backAnalyzeMap.ContainsKey(msgId))
            return;
        backAnalyzeMap[msgId] = new BackAna<T>();
    }

    /// <summary>
    /// 获取当前的消息
    /// </summary>
    public RMessage GetCurMsg()
    {
        return m_Queue.peek();
    }

    /// <summary>
    /// 连接服务器
    /// </summary>
    public void BeginConnect(string ip, int port, int socketId = 0)
    {
        GlobalNetBytesCache.Init(512, 64, 64, 5);
        if(!sockets.ContainsKey(socketId))
            sockets.Add(socketId, new AsyncTCPSocket());
        sockets[socketId].ConnectServer(ip, port, ConnectCallback, DisconnectCallback, ReceiveCallback);
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    public void Send(int msgId, byte[] data, int size, int socketId = 0)
    {
        if(sockets.ContainsKey(socketId))
            sockets[socketId].SendToServer(msgId, data, size);
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    public void Send(IMsg msg, int socketId = 0)
    {
        if(sockets.ContainsKey(socketId))
            sockets[socketId].SendToServer(msg);
    }

    /// <summary>
    /// 关闭连接
    /// </summary>
    public void CloseSocket(int socketId = 0)
    {
        if(sockets.ContainsKey(socketId))
            sockets[socketId].CloseSocket();
    }

    /// <summary>
    /// 是否处于连接状态
    /// </summary>
    public bool IsConnected(int socketId = 0)
    {
        if(sockets.ContainsKey(socketId))
            return sockets[socketId].IsConnected();
        return false;
    }

    /// 缓存消息结构
    private void recycleMsg(RMessage rMsg)
    {
        if(rMsg != null)
        {
            GlobalNetBytesCache.Free(rMsg.ByteContent);
            rMsg.msg = null;
            rMsg.ByteContent = null;
            lock(mMsgCache)
            {
                mMsgCache.Enqueue(rMsg);
            }
        }
    }

    private void ConnectCallback(NetCode code)
    {
        RMessage rMsg = _GetMsg();
        rMsg.MsgId = ConnectSucceedEvt;
        rMsg.RetCode = (int)code;
        m_Queue.push(rMsg);
    }

    /// <summary>
    /// 断线
    /// </summary>
    private void DisconnectCallback(NetCode code)
    {
        RMessage rMsg = _GetMsg();
        rMsg.MsgId = DisconnectEvt;
        rMsg.RetCode = (int)code;
        m_Queue.push(rMsg);
    }

    /// <summary>
    /// 接收数据
    /// </summary>
    private void ReceiveCallback(int msgId, byte[] bytes, int len)
    {
        RMessage rMsg = _GetMsg();
        rMsg.RetCode = 0;
        rMsg.MsgId = msgId;
        rMsg.ByteContent = bytes;
        if(backAnalyzeMap.ContainsKey(msgId))
        {
            int offset = 0;
            var msg = backAnalyzeMap[msgId].New();
            if(msg.GetMsgId() == msgId)
            {
                msg.Read(rMsg.ByteContent, ref offset);
                rMsg.msg = msg;
            }else
            {
                UnityEngine.Debug.LogError(string.Format("后台解析消息失败，注册消息id和消息无法对应.real:{0}, register:{1}", msg.GetMsgId(), msgId));
            }
        }
        m_Queue.push(rMsg);
    }

    /// <summary>
    /// 每帧调用
    /// </summary>
    public void Update()
    {
        var enu = sockets.GetEnumerator();
        while(enu.MoveNext())
        {
            var socket = enu.Current.Value;
            float endTime = UnityEngine.Time.realtimeSinceStartup + DISPATCH_MAX_TIME;
            float curTime = UnityEngine.Time.realtimeSinceStartup;
            while(curTime < endTime)
            {
                if(m_Queue.length() < 1)
                    break;

                var msg = m_Queue.peek();
                if(msg == null)
                    break;

                GED.NED.dispatchEvent(msg.MsgId, msg.RetCode);
                m_Queue.shift();
                recycleMsg(msg);
                curTime = UnityEngine.Time.realtimeSinceStartup;
            }
        }
        enu.Dispose();
    }
}