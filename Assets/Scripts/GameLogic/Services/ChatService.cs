using Data.Beans;
using FairyGUI;
using Message.Chat;
using System.Collections.Generic;
using System;

public class ChatService : SingletonService<ChatService>
{
    public enum EChannelType
    {
        System,           //系统
        World,            //世界
        Guild,            //社团
        ZuiDui,           //组队
        Team,             //队伍
    }

    public enum EJumpType
    {
        None,
        Help,          //协助
        JoinGuild,     //加入社团
        JoinTeam,      //加入队伍
        PlayVideo,     //播放录像
    }

    //喇叭类型
    public enum ETrumpetFont
    {
        Small,          //小号字体
        Big,            //大号字体
    }

    public enum ETrumpetMove
    {
        Left,     //从右往左
        LeftDown, //从右上往左下
        LeftUp,   //从右下往左上
    }

    private Dictionary<EChannelType, List<ChatInfo>> m_chatInfoDic = new Dictionary<EChannelType, List<ChatInfo>>();

    private List<ChatInfo> m_chatInfoList = new List<ChatInfo>();                //所有聊天信息列表
    private List<ChatInfo> m_filterChatInfoList = new List<ChatInfo>();          //过滤频道后的聊天信息列表

    private int m_singleChannelMaxNum = 50;                                     //单频道最大大的消息数量

    public ChatService()
    {
        t_globalBean bean = ConfigBean.GetBean<t_globalBean, int>(502008);
        if (bean != null)
        {
            m_singleChannelMaxNum = bean.t_int_param;
        }

    }

    protected override void RegisterEventListener()
    {
        base.RegisterEventListener();
        GED.NED.addListener(ResChatInfo.MsgId, _ResChatInfo);
        GED.NED.addListener(ResChatInit.MsgId, _ResChatInit);
    }


    protected override void UnRegisterEventListener()
    {
        base.UnRegisterEventListener();
        GED.NED.removeListener(ResChatInfo.MsgId, _ResChatInfo);
        GED.NED.removeListener(ResChatInit.MsgId, _ResChatInit);
    }

    public override void ClearData()
    {
        base.ClearData();

        m_chatInfoDic.Clear();
        m_chatInfoList.Clear();
        m_filterChatInfoList.Clear();
    }

    //------------------------------------------------------------------------数据
    public void InitLoaclChatInfo()
    {
        List<ChatInfo> teamInfo = ChatTool.GetInstance().GetChatInfoInLocal(EChannelType.Team);
        List<ChatInfo> guildInfo = ChatTool.GetInstance().GetChatInfoInLocal(EChannelType.Guild);
        m_chatInfoDic[EChannelType.Guild] = guildInfo;
        m_chatInfoDic[EChannelType.Team] = teamInfo;
        m_chatInfoList.AddRange(teamInfo);
        m_chatInfoList.AddRange(guildInfo);
        FilterChatInfo();
    }

    public void SaveLocalChatInfo()
    {
        if (m_chatInfoDic.ContainsKey(EChannelType.Guild))
        {
            ChatTool.GetInstance().SaveChatInfoToLocal(EChannelType.Guild, m_chatInfoDic[EChannelType.Guild]);
        }

        if (m_chatInfoDic.ContainsKey(EChannelType.Team))
        {
            ChatTool.GetInstance().SaveChatInfoToLocal(EChannelType.Team, m_chatInfoDic[EChannelType.Team]);
        }
    }

    public List<ChatInfo> GetChatInfoByChannel(EChannelType type)
    {
        if (m_chatInfoDic.ContainsKey(type))
            return m_chatInfoDic[type];
        return null;
    }

    public List<ChatInfo> GetChatInfos()
    {
        return m_chatInfoList;
    }

    public List<ChatInfo> GetFilterChatInfos()
    {
        return m_filterChatInfoList;
    }

    //过滤对应频道的聊天信息
    public void FilterChatInfo()
    {
        m_filterChatInfoList.Clear();
        for (int i = 0; i < m_chatInfoList.Count; i++)
        {
            if (ChatTool.GetInstance().GetChannelIsShow((EChannelType)m_chatInfoList[i].channel))
            {
                m_filterChatInfoList.Add(m_chatInfoList[i]);
            }
        }
    }

    public bool CheckChatMsgIsNormal(ChatInfo chatInfo)
    {
        if (string.IsNullOrEmpty(chatInfo.content))
        {
            TipWindow.Singleton.ShowTip(UIUtils.GetStrByLanguageID(70502002));
            return false;
        }

        if (chatInfo.channel == (int)EChannelType.Team)
        {
            if (CloneTeamFightService.Singleton.fightTeamInfo == null)
            {
                TipWindow.Singleton.ShowTip("当前没有队伍");
                return false;
            }

        }
        else if (chatInfo.channel == (int)EChannelType.Guild)
        {
            if (RoleService.Singleton.GetRoleInfo().guildId <= 0)
            {
                TipWindow.Singleton.ShowTip(UIUtils.GetStrByLanguageID(6502001));
                return false;
            }
        }
 
        return true;
    }

    private void _InsetMsgToLsit(List<ChatInfo> msgList, ChatInfo chatInfo, int maxNum)
    {
        if (msgList.Count >= maxNum)
        {
            msgList.RemoveAt(msgList.Count - 1); 
        }

        msgList.Insert(0, chatInfo);
    }

    //----------------------------------------------------------------------通知
    //聊天消息初始化（发过来的消息已经是最新的在前面）
    private void _ResChatInit(GameEvent evt)
    {
        ResChatInit msg = GetCurMsg<ResChatInit>(evt.EventId);
        for (int i = 0; i < msg.chatInfo.Count; i++)
        {
            ChatInfo chatInfo = msg.chatInfo[i];
            if (!m_chatInfoDic.ContainsKey((EChannelType)chatInfo.channel))
            {
                m_chatInfoDic[(EChannelType)chatInfo.channel] = new List<ChatInfo>();
            }

            m_chatInfoDic[(EChannelType)chatInfo.channel].Add(chatInfo);

        }

        m_chatInfoList.InsertRange(0, msg.chatInfo);
 

        FilterChatInfo();
    }


    private void _ResChatInfo(GameEvent evt)
    {
        ResChatInfo msg = GetCurMsg<ResChatInfo>(evt.EventId);

        if (!m_chatInfoDic.ContainsKey((EChannelType)msg.chatInfo.channel))
        {
            m_chatInfoDic[(EChannelType)msg.chatInfo.channel] = new List<ChatInfo>();
        }

        _InsetMsgToLsit(m_chatInfoDic[(EChannelType)msg.chatInfo.channel], msg.chatInfo, m_singleChannelMaxNum);
        _InsetMsgToLsit(m_chatInfoList, msg.chatInfo, Enum.GetNames(typeof(EChannelType)).Length);
        //m_chatInfoList.Insert(0, msg.chatInfo);

        if (ChatTool.GetInstance().GetChannelIsShow((EChannelType)msg.chatInfo.channel))
        {
            _InsetMsgToLsit(m_filterChatInfoList, msg.chatInfo, ChatTool.GetInstance().GetShowChannelNum());
             
        }

        if (msg.chatInfo.isBell)
        {
            TrumpetMsgMgr.GetInstance().ShowTrumpetInfo(msg.chatInfo);
        }
 

        if (msg.chatInfo.isMarquee)
        {
            MarqueeMgr.GetInstance().ShowMarqueeInfo(msg.chatInfo);
        }

        GED.ED.dispatchEvent(EventID.ChatInfoRefresh, msg.chatInfo);
    }

    //--------------------------------------------------------------------请求
    public void ReqSendChatInfo(ChatInfo chatInfo, int comsumeItemType = -1)
    {

        chatInfo.content = ChatTool.GetInstance().ConvertToFilterWords(chatInfo.content);
        ReqSendChatInfo msg = GetEmptyMsg<ReqSendChatInfo>();
        msg.chatInfo = chatInfo;
        if (comsumeItemType != -1)
        {
            msg.type = comsumeItemType;
        }
 
        SendMsg<ReqSendChatInfo>(ref msg);
    }
}