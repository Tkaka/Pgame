using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Message.Fight;
using Message.Challenge;

public class CloneTeamFightService : SingletonService<CloneTeamFightService> {
    /// <summary>
    /// 组队战队伍信息
    /// </summary>
    public ResTeamFightTeamInfo fightTeamInfo { get; private set; }
    /// <summary>
    /// 克隆组队战的最大次数
    /// </summary>
    public int cloneMaxTimes = 3;
    /// <summary>
    /// 克隆组队战的队伍最大人数
    /// </summary>
    public int teamMaxRoleNum = 5;
    protected override void RegisterEventListener()
    {
        base.RegisterEventListener();

        GED.NED.addListener(ResTeamFightMonsterInfo.MsgId, OnResTeamFightMonsterInfo);
        GED.NED.addListener(ResTeamFightTeamInfo.MsgId, OnResTeamFightTeamInfo);
        GED.NED.addListener(ResTeamFightTeammateChange.MsgId, OnResTeamFightTeammateChange);
        GED.NED.addListener(ResTeamFightForbibFastEnter.MsgId, OnResTeamFightForbibFastEnter);
        GED.NED.addListener(ResTeamFightStart.MsgId, OnResTeamFightStart);
        //GED.NED.addListener(ResTeamFightEnd.MsgId, OnResTeamFightEnd);//(弃用)
        GED.NED.addListener(ResTeamFightChangePet.MsgId, OnResTeamFightChangePet);
        GED.NED.addListener(ResTeamFightOpenBox.MsgId, OnResTeamFightOpenBox);
        GED.NED.addListener(ResTeamFightNoticeSuccess.MsgId, OnResTeamFightNoticeSuccess);
        GED.NED.addListener(ResTeamFightInviteSuccess.MsgId, OnResTeamFightInviteSuccess);
        GED.NED.addListener(ResTeamFightFriendInviteSuccess.MsgId, OnResTeamFightFriendInviteSuccess);
    }

    protected override void UnRegisterEventListener()
    {
        base.UnRegisterEventListener();

        GED.NED.removeListener(ResTeamFightMonsterInfo.MsgId, OnResTeamFightMonsterInfo);
        GED.NED.removeListener(ResTeamFightTeamInfo.MsgId, OnResTeamFightTeamInfo);
        GED.NED.removeListener(ResTeamFightTeammateChange.MsgId, OnResTeamFightTeammateChange);
        GED.NED.removeListener(ResTeamFightForbibFastEnter.MsgId, OnResTeamFightForbibFastEnter);
        GED.NED.removeListener(ResTeamFightStart.MsgId, OnResTeamFightStart);
        //GED.NED.removeListener(ResTeamFightEnd.MsgId, OnResTeamFightEnd);
        GED.NED.removeListener(ResTeamFightChangePet.MsgId, OnResTeamFightChangePet);
        GED.NED.removeListener(ResTeamFightOpenBox.MsgId, OnResTeamFightOpenBox);
        GED.NED.removeListener(ResTeamFightNoticeSuccess.MsgId, OnResTeamFightNoticeSuccess);
        GED.NED.removeListener(ResTeamFightInviteSuccess.MsgId, OnResTeamFightInviteSuccess);
        GED.NED.removeListener(ResTeamFightFriendInviteSuccess.MsgId, OnResTeamFightFriendInviteSuccess);
    }

    #region 服务器消息回调 ****************************************************************************************
    /// <summary>
    /// 克隆组队战怪物信息回调
    /// </summary>
    /// <param name="evt"></param>
    private void OnResTeamFightMonsterInfo(GameEvent evt)
    {
        ResTeamFightMonsterInfo msg = GetCurMsg<ResTeamFightMonsterInfo>(evt.EventId);

        GED.ED.dispatchEvent(EventID.OnResTeamFightMonsterInfo, msg.petIds);
    }
    /// <summary>
    /// 克隆组队战队伍信息
    /// </summary>
    /// <param name="evt"></param>
    private void OnResTeamFightTeamInfo(GameEvent evt)
    {
        ResTeamFightTeamInfo msg = GetCurMsg<ResTeamFightTeamInfo>(evt.EventId);
        fightTeamInfo = msg;

        GED.ED.dispatchEvent(EventID.OnResTeamFightTeamInfo);
    }
    /// <summary>
    /// 克隆组队战成员变动
    /// </summary>
    /// <param name="evt"></param>
    private void OnResTeamFightTeammateChange(GameEvent evt)
    {
        ResTeamFightTeammateChange msg = GetCurMsg<ResTeamFightTeammateChange>(evt.EventId);
        fightTeamInfo.roleId = msg.roleId;
        // 更新队伍成员信息 插入到第一个为0的位置
        int count = fightTeamInfo.teammates.Count;
        TeamFightRoleInfo teamRoleInfo = null;
        for (int i = 0; i < count; i++)
        {
            teamRoleInfo = fightTeamInfo.teammates[i];
            if (teamRoleInfo == null)
            {
                fightTeamInfo.teammates[i] = msg.teammate;
            }
        }

        GED.ED.dispatchEvent(EventID.OnResTeamFightTeammateChange, msg.enter);
    }
    /// <summary>
    /// 组队战是否禁止自动加入
    /// </summary>
    /// <param name="evt"></param>
    private void OnResTeamFightForbibFastEnter(GameEvent evt)
    {
        ResTeamFightForbibFastEnter msg = GetCurMsg<ResTeamFightForbibFastEnter>(evt.EventId);

        GED.ED.dispatchEvent(EventID.OnResTeamFightForbibFastEnter, msg.enter);
    }
    /// <summary>
    /// 组队战开始战斗
    /// </summary>
    /// <param name="evt"></param>
    private void OnResTeamFightStart(GameEvent evt)
    {
        ResTeamFightStart msg = GetCurMsg<ResTeamFightStart>(evt.EventId);

        GED.ED.dispatchEvent(EventID.OnResTeamFightStart);
    }
    /// <summary>
    /// 组队战结束
    /// </summary>
    /// <param name="evt"></param>
    public void OnResTeamFightEnd(ResFightResultInfo msg)
    {
        TeamFightResult fightResult = msg.result as TeamFightResult;
        // 更新玩家完成的次数
        TeamFightRoleInfo selfFightInfo = GetPlayerFightRoleInfo();
        selfFightInfo.count = fightResult.completeTimes;

        if (fightResult.result == 0)
        {
            BattleWindow.Singleton.OpenChild<BattleFailedWindow>(WinInfo.Create(false, null, false));
        }
        else
        {
            WinMgr.Singleton.Open<CloneWinWindow>(null, UILayer.Popup);
            //GED.ED.dispatchEvent(EventID.OnResTeamFightEnd, msg.result);
        }
 
    }
    /// <summary>
    /// 组队战改变上阵宠物
    /// </summary>
    /// <param name="evt"></param>
    private void OnResTeamFightChangePet(GameEvent evt)
    {
        ResTeamFightChangePet msg = GetCurMsg<ResTeamFightChangePet>(evt.EventId);
        // 更新队伍成员信息
        int count = fightTeamInfo.teammates.Count;
        TeamFightRoleInfo fightRoleInfo = null;
        for (int i = 0; i < count; i++)
        {
            fightRoleInfo = fightTeamInfo.teammates[i];
            if (fightRoleInfo != null && fightRoleInfo.roleId == msg.teamFightRoleInfo.roleId)
            {
                fightTeamInfo.teammates[i] = msg.teamFightRoleInfo;
            }
        }

        GED.ED.dispatchEvent(EventID.OnResTeamFightChangePet);
    }
    /// <summary>
    /// 组队战开箱
    /// </summary>
    /// <param name="evt"></param>
    private void OnResTeamFightOpenBox(GameEvent evt)
    {
        ResTeamFightOpenBox msg = GetCurMsg<ResTeamFightOpenBox>(evt.EventId);

        GED.ED.dispatchEvent(EventID.OnResTeamFightOpenBox, msg);
    }
    /// <summary>
    /// 通知队员成功
    /// </summary>
    /// <param name="evt"></param>
    private void OnResTeamFightNoticeSuccess(GameEvent evt)
    {
        ResTeamFightNoticeSuccess msg = GetCurMsg<ResTeamFightNoticeSuccess>(evt.EventId);

        GED.ED.dispatchEvent(EventID.OnResTeamFightNoticeSuccess);
    }
    /// <summary>
    /// 组队战邀请成功
    /// </summary>
    /// <param name="evt"></param>
    private void OnResTeamFightInviteSuccess(GameEvent evt)
    {
        ResTeamFightInviteSuccess msg = GetCurMsg<ResTeamFightInviteSuccess>(evt.EventId);

        GED.ED.dispatchEvent(EventID.OnResTeamFightInviteSuccess, msg.type);
    }
    /// <summary>
    /// 好友邀请成功
    /// </summary>
    /// <param name="evt"></param>
    private void OnResTeamFightFriendInviteSuccess(GameEvent evt)
    {
        ResTeamFightFriendInviteSuccess msg = GetCurMsg<ResTeamFightFriendInviteSuccess>(evt.EventId);

        GED.ED.dispatchEvent(EventID.OnResTeamFightFriendInviteSuccess);
    }
    #endregion

    #region 请求 **************************************************************************************************
    /// <summary>
    /// 请求克隆组队战信息
    /// </summary>
    public void ReqTeamFightInfo()
    {
        ReqTeamFightInfo msg = GetEmptyMsg<ReqTeamFightInfo>();

        SendMsg<ReqTeamFightInfo>(ref msg);
    }
    /// <summary>
    /// 请求组队战加入队伍
    /// </summary>
    /// <param name="isQuickJoin">false:创建并加入队伍，true：快速加入队伍（没有可加入则创建队伍）</param>
    public void ReqTeamFightEnterTeam(bool isQuickJoin, int petID)
    {
        ReqTeamFightJoinTeam msg = GetEmptyMsg<ReqTeamFightJoinTeam>();
        msg.fastEnter = isQuickJoin;
        msg.petId = petID;
        msg.bestPetId = PetService.Singleton.GetHightestFightPowerPet();

        SendMsg<ReqTeamFightJoinTeam>(ref msg);
    }
    /// <summary>
    /// 请求组队战离开队伍
    /// </summary>
    public void ReqTeamFightLeaveTeam()
    {
        ReqTeamFightLeaveTeam msg = GetEmptyMsg<ReqTeamFightLeaveTeam>();

        SendMsg<ReqTeamFightLeaveTeam>(ref msg);
    }
    /// <summary>
    /// 请求组队战邀请好友
    /// </summary>
    /// <param name="roleID">好友id</param>
    public void ReqTeamFightFriendInvite(int roleID)
    {
        ReqTeamFightFriendInvite msg = GetEmptyMsg<ReqTeamFightFriendInvite>();
        msg.roleId = roleID;

        SendMsg<ReqTeamFightFriendInvite>(ref msg);
    }
    /// <summary>
    /// 请求组队战发送邀请消息
    /// </summary>
    /// <param name="type">1：世界频道，2：社团频道</param>
    public void ReqTeamFightSendInviteMessage(int type)
    {
        ReqTeamFightSendInviteMessage msg = GetEmptyMsg<ReqTeamFightSendInviteMessage>();
        msg.channel = type;

        SendMsg<ReqTeamFightSendInviteMessage>(ref msg);
    }
    /// <summary>
    /// 请求组队战设置禁止快速加入
    /// </summary>
    /// <param name="isForbid"></param>
    public void ReqTeamFightForbidFastEnter(bool isForbid)
    {
        ReqTeamFightForbidFastEnter msg = GetEmptyMsg<ReqTeamFightForbidFastEnter>();
        msg.forbid = isForbid;

        SendMsg<ReqTeamFightForbidFastEnter>(ref msg);
    }
    /// <summary>
    /// 请求组队战通知队友
    /// </summary>
    public void ReqTeamFightNotifyTeammates()
    {
        ReqTeamFightNotifyTeammates msg = GetEmptyMsg<ReqTeamFightNotifyTeammates>();

        SendMsg<ReqTeamFightNotifyTeammates>(ref msg);
    }
    /// <summary>
    /// 请求组队战更换上阵宠物
    /// </summary>
    /// <param name="petID"></param>
    public void ReqTeamFightChangePet(int petID)
    {
        ReqTeamFightChangePet msg = GetEmptyMsg<ReqTeamFightChangePet>();
        msg.petId = petID;

        SendMsg<ReqTeamFightChangePet>(ref msg);
    }
    /// <summary>
    /// 请求组队战开启宝箱
    /// </summary>
    public void ReqTeamFightOpenBox(int index)
    {
        ReqTeamFightOpenBox msg = GetEmptyMsg<ReqTeamFightOpenBox>();
        msg.index = index;
        SendMsg<ReqTeamFightOpenBox>(ref msg);
    }
    /// <summary>
    /// 请求组队战接受邀请
    /// </summary>
    /// <param name="roleID">邀请者角色ID</param>
    public void ReqTeamFightAgreeInvate(int roleID)
    {
        ReqTeamFightAgreeInvite msg = GetEmptyMsg<ReqTeamFightAgreeInvite>();
        msg.roleId = roleID;

        SendMsg<ReqTeamFightAgreeInvite>(ref msg);
    }
    /// <summary>
    /// 请求组队战请求战斗
    /// </summary>
    public void ReqTeamFightStart()
    {
        ReqTeamFightStart msg = GetEmptyMsg<ReqTeamFightStart>();

        SendMsg<ReqTeamFightStart>(ref msg);
    }
    /// <summary>
    /// 请求组队战战斗结果
    /// </summary>
    /// <param name="result">结果（0：失败，1：成功）</param>
    public void ReqTeamFightEnd(int result)
    {
        ReqTeamFightEnd msg = GetEmptyMsg<ReqTeamFightEnd>();
        msg.result = result;
        SendMsg<ReqTeamFightEnd>(ref msg);
    }
    #endregion;

    #region 数据处理 **********************************************************************************************
    /// <summary>
    /// 获得玩家的战斗信息
    /// </summary>
    /// <returns></returns>
    public TeamFightRoleInfo GetPlayerFightRoleInfo()
    {
        if (fightTeamInfo != null)
        {
            Message.Role.RoleInfo roleInfo = RoleService.Singleton.GetRoleInfo();

            int count = fightTeamInfo.teammates.Count;
            TeamFightRoleInfo fightRoleInfo = null;
            for (int i = 0; i < count; i++)
            {
                fightRoleInfo = fightTeamInfo.teammates[i];
                if (fightRoleInfo != null && fightRoleInfo.roleId == roleInfo.roleId)
                    return fightRoleInfo;
            }
        }

        return null;
    }
    public TeamFightRoleInfo GetTeamFightRoleInfo(int index)
    {
        if (fightTeamInfo != null)
        {
            int count = fightTeamInfo.teammates.Count;
            TeamFightRoleInfo fightRoleInfo = null;
            for (int i = 0; i < count; i++)
            {
                fightRoleInfo = fightTeamInfo.teammates[i];
                if (fightRoleInfo != null && fightRoleInfo.index == index)
                    return fightRoleInfo;
            }
        }

        return null;
    }
    /// <summary>
    /// 是否已经进行了战斗
    /// </summary>
    /// <returns></returns>
    public bool IsFighted()
    {
        TeamFightRoleInfo roleFightInfo = GetPlayerFightRoleInfo();
        if (roleFightInfo != null)
        {
            return roleFightInfo.count > 0;
        }
        Logger.err(false.ToString());
        return false;
    }
    /// <summary>
    /// 是否完成了所有战斗
    /// </summary>
    /// <returns></returns>
    public bool IsFinishedFight()
    {
        TeamFightRoleInfo roleFightInfo = Singleton.GetPlayerFightRoleInfo();
        if (roleFightInfo != null)
        {
            return roleFightInfo.count == Singleton.cloneMaxTimes;
        }

        return false;
    }
    /// <summary>
    /// 是否存在没打完的队友
    /// </summary>
    /// <returns></returns>
    public bool IsExistUnFinishedTeammate()
    {
        if (fightTeamInfo != null)
        {
            int count = fightTeamInfo.teammates.Count;
            TeamFightRoleInfo roleFightInfo = null;
            TeamFightRoleInfo selfFightInfo = GetPlayerFightRoleInfo();
            for (int i = 0; i < count; i++)
            {
                roleFightInfo = fightTeamInfo.teammates[i];
                if (roleFightInfo != null && roleFightInfo != selfFightInfo && roleFightInfo.count < cloneMaxTimes)
                {
                    return true;
                }
            }
        }

        return false;
    }
    /// <summary>
    /// 队伍是否满了
    /// </summary>
    /// <returns></returns>
    public bool IsTeamFull()
    {
        if (fightTeamInfo != null)
        {
            return fightTeamInfo.teammates.Count == teamMaxRoleNum;
        }

        return true;
    }
    /// <summary>
    /// 获得队长的信息
    /// </summary>
    /// <returns></returns>
    public TeamFightRoleInfo GetCaptainRoleInfo()
    {
        if (fightTeamInfo != null)
        {
            int count = fightTeamInfo.teammates.Count;
            TeamFightRoleInfo fightRoleInfo = null;
            for (int i = 0; i < count; i++)
            {
                fightRoleInfo = fightTeamInfo.teammates[i];
                if (fightRoleInfo != null && fightRoleInfo.roleId == fightTeamInfo.roleId)
                    return fightRoleInfo;
            }
        }

        return null;
    }

    /// <summary>
    /// 玩家是否是队长
    /// </summary>
    /// <returns></returns>
    public bool PlayerIsCaptain()
    {
        ResTeamFightTeamInfo teamFightInfo = CloneTeamFightService.Singleton.fightTeamInfo;
        if (teamFightInfo != null)
        {
            Message.Role.RoleInfo roleInfo = RoleService.Singleton.GetRoleInfo();
            return roleInfo.roleId == teamFightInfo.roleId;
        }

        return false;
    }
    /// <summary>
    /// 获得完成克隆组队战的成员数量
    /// </summary>
    /// <returns></returns>
    public int GetFinishedRoleNum()
    {
        int num = 0;
        if (fightTeamInfo != null)
        {
            int count = fightTeamInfo.teammates.Count;
            TeamFightRoleInfo fightRoleInfo = null;
            for (int i = 0; i < count; i++)
            {
                fightRoleInfo = fightTeamInfo.teammates[i];
                if (fightRoleInfo.count == cloneMaxTimes)
                    num++;
            }
        }
        return num;
    }
    #endregion

}
