
using System.Collections.Generic;
using UnityEngine;

public enum ServiceType
{
    TestService = 0, //用于测试的service
    LoginService = 1,
    RoleService = 2,
    PetService = 3,
    GMService,
    BagService,
    LevelService,
    BattleService,
    TaskService,
    DrawCardService,
    ChallegeService,
    ArenaService,
    TongXiangGuanService,
    HallFameService,
    ShopService,
    MailService,
    AchievementService,
    ShenQiService,
    TalentService,
    UltemateTrainService,
    GuildService,
    StriveHegemongService,
    CloneTeamFightService,
    ChatService,
    GuildBossService,
    FuncService,
    VipService,
    RechargeService,
    GuideService,
    GuildBattleService,
    TopService,
    AoyiService,
    FightService,
    ReplayService,
    DailyActivity,
}

/// <summary>
/// service 管理
/// </summary>
public class ServiceManager
{
    private static ServiceManager m_instance;

    public static ServiceManager Singleton
    {
        get
        {
            if(m_instance == null)
                m_instance = new ServiceManager();
            return m_instance;
        }
    }


    private Dictionary<ServiceType, BaseService> m_serviceDict;

    private ServiceManager()
    {
        m_serviceDict = new Dictionary<ServiceType, BaseService>();
    }


    /// <summary>
    /// 注册service
    /// </summary>
    public void RegisterService<T>(ServiceType type) where T : BaseService, new()
    {
        if(m_serviceDict.ContainsKey(type) && m_serviceDict[type] != null)
        {
            Debug.LogErrorFormat("重复注册service， Type:{0}", type);
            return;
        }
        var service = new T();
        m_serviceDict[type] = service;
    }

    /// <summary>
    /// 取消注册service
    /// </summary>
    public void UnRegisterService(ServiceType type)
    {
        BaseService service = null;
        if (m_serviceDict.TryGetValue(type, out service))
        {
            service.ClearData();
            m_serviceDict.Remove(type);
        }
    }

    /// <summary>
    /// 取消注册service for lua
    /// </summary>
    public void UnRegisterService(int type)
    {
        UnRegisterService((ServiceType)type);
    }

    /// <summary>
    /// 获取service
    /// </summary>
    public T GetService<T>(ServiceType type) where T : BaseService
    {
        BaseService service = null;
        if(m_serviceDict.TryGetValue(type, out service))
            return service as T;
        Debug.LogErrorFormat("获取service失败, Type:{0}", type);
        return null;
    }


    /// <summary>
    /// 清除数据
    /// </summary>
    public void ClearData()
    {
        var iterator = m_serviceDict.Values.GetEnumerator();
        while(iterator.MoveNext())
        {
            if(iterator.Current != null)
                iterator.Current.ClearData();
        }
        iterator.Dispose();
        //Global.GEvtDispatcher.DispatchEvent(EventID.ClearServiceData);
        GED.ED.dispatchEvent(EventID.ClearServiceData);
    }
}


public class SingletonService<T> : BaseService where T : BaseService, new()
{
    protected static T mSingleton = null;

    public static T Singleton
    {
        get{ return mSingleton; }
    }

    public SingletonService()
    {
        mSingleton = this as T;
        RegisterEventListener();
    }

    public override void ClearData()
    {
        base.ClearData();
    }
}


/// <summary>
/// service 基类
/// </summary>
public class BaseService : IClassCache
{
    public override void FakeCtr(IParam param)
    {
        base.FakeCtr(param);
        RegisterEventListener();
    }

    /// <summary>
    /// 获取当前网络消息队列的第一个消息
    /// 在监听到网络消息事件的时候调用
    /// </summary>
    /// <param name="msgId">用于校验的消息ID</param>
    /// <returns></returns>
    protected T GetCurMsg<T>(int msgId) where T : BaseMessage, new()
    {
        var rMsg = MessageHandle.GetInstance().GetCurMsg();
        if(rMsg == null)
            return null;
        if(rMsg.MsgId != msgId)
        {
            UnityEngine.Debug.LogErrorFormat("获取网络消息失败, mine:{0}   cur:{1}", msgId, rMsg.MsgId);
            return null;
        }

        //已经提前解析好了
        if(rMsg.msg != null)
            return rMsg.msg as T;
        T msg = new T();
        int offset = 0;
        msg.Read(rMsg.ByteContent, ref offset);
        MessageHandle.GetInstance().RegisterBackAnalyzeMsg<T>(msg.GetMsgId());
        return msg;
    }

    /// <summary>
    /// 获取一个空消息，用于发消息的时候调用
    /// </summary>
    protected T GetEmptyMsg<T>() where T : BaseMessage, new()
    {
        return new T();
    }

    /// <summary>
    /// 发消息
    /// </summary>
    protected void SendMsg<T>(ref T msg) where T : BaseMessage
    {
        MessageHandle.GetInstance().Send(msg);
    }

    /// <summary>
    /// 注册事件监听
    /// </summary>
    protected virtual void RegisterEventListener()
    {

    }

    /// <summary>
    /// 取消事件监听
    /// </summary>
    protected virtual void UnRegisterEventListener()
    {
        
    }

    /// <summary>
    /// 清除数据
    /// </summary>
    public virtual void ClearData()
    {
        
    }
}
