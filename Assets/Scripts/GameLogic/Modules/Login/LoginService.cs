using Message.Login;
using System;

public class LoginService : SingletonService<LoginService>
{
    private const int backLoginMaxTime = 3;
    private int backLoginedNum = 0;
    private int curProfession;
    private string curUeserName;
    public bool isReLogin { get; private set; }//当前是否在后台重连
    public ResRoleList RoleList { get; private set; }

    //登录事件监听
    protected override void RegisterEventListener()
    {
        base.RegisterEventListener();
        //联网
        //获取消息的id，根据id调用回调函数进行处理
        //添加网络事件监听
        GED.NED.addListener(MessageHandle.ConnectSucceedEvt, OnConnectServer);
        GED.NED.addListener(MessageHandle.DisconnectEvt, onDisconnect);

        //登录是否成功
        GED.NED.addListener(ResLogin.MsgId, OnResLogin);
        //请求角色列表
        GED.NED.addListener(ResRoleList.MsgId, OnResRoleList);
        //创建角色
        GED.NED.addListener(ResCreateRole.MsgId, OnCreateRole);
        //重连回调
        GED.NED.addListener(ResReLogin.MsgId, resReLogin);
    }

    private void OnConnectServer(GameEvent evt)
    {
        Logger.log("-------OnConnectServer------" + evt.Data);
        int code = (int)evt.Data;
        if ((NetCode)code == NetCode.Success)
        {
            if (isReLogin)
                this.DoLogin(curUeserName);
            else
                GED.ED.dispatchEvent(EventID.OnConnectServer);
        } else
        {
            if (isReLogin)
            {
                loginFailedTip();
                return;
            }

            if ((NetCode)code == NetCode.TimeOut)
            {
                ConnectingWindow.Singleton.Hide();
                AgainConfirmWindow.Singleton.ShowTip("连接超时，请稍后再试！");
            }
            else
            {
                ConnectingWindow.Singleton.Hide();
                AgainConfirmWindow.Singleton.ShowTip("连接失败，请检查您的网络后重试！");
            }
        }
    }

    private void onDisconnect(GameEvent evt)
    {
        int code = (int)evt.Data;
        if((NetCode)code != NetCode.Closed)
        {
            Debuger.Log("断开连接", evt.Data);
            loginFailedTip();
        }else
        {
            Debuger.Log("手动断开连接");
            //手动关闭 不触发重连
        }
    }

    public void DoLogin(String userName)
    {
        var curServer = ServerList.Singleton.CurrentServer;

        curUeserName = userName;
        ReqLogin msg = GetEmptyMsg<ReqLogin>();
        msg.userName = userName;
        msg.serverId = curServer != null ? curServer.id : 100;
        msg.platformId = 2;
        msg.loginTime = TimeUtils.currentMilliseconds();
        SendMsg(ref msg);
    }

    //将指定内容发送到服务器
    private void OnResLogin(GameEvent evt)
    {
        //得到消息内容，evt是事件分发器，
        //通过事件分发器内的Id得到消息内容再赋给
        //msg，msg得到的就是消息的内容
        ResLogin msg = GetCurMsg<ResLogin>(evt.EventId);
        Logger.log("username:" + msg.username);
        //Debuger.Log("roleId:" + msg.loginResult);
        //Debuger.Log("roleId:" + msg.failedReason);
        //ClassCacheManager.Delete(ref msg);
        
        ReqRoleList roleMsg = GetEmptyMsg<ReqRoleList>();
        SendMsg(ref roleMsg);
    }


    private void OnResRoleList(GameEvent evt)
    {
        RoleList = GetCurMsg<ResRoleList>(evt.EventId);
        Logger.log("role.size:" + RoleList.roles.Count);
        if (isReLogin)
        {
            SelectRole((Profession)curProfession);
        }
        else
        {
            ConnectingWindow.Singleton.Hide();
            GED.ED.dispatchEvent(EventID.ResRoleList);
        }
    }


    public void CreateRole(string roleName, int profession)
    {
        Logger.log("---------->创建角色");
        //ConnectingWindow.Singleton.Show();
        ReqCreateRole crole = GetEmptyMsg<ReqCreateRole>();
        crole.roleName = roleName;
        crole.profession = profession;   //1:男 2：女
        curProfession = profession;
        SendMsg(ref crole);
    }

    public void OnCreateRole(GameEvent evt)
    {
        ResCreateRole createRole = GetCurMsg<ResCreateRole>(evt.EventId);
        if (createRole.success)
        {
            Logger.log("---------->创建角色成功");
        }
        else
        {
            Logger.log("---------->创建角色失败");
        }
    }

    public void SelectRole(Profession profession)
    {
        Logger.log("---------->选择角色");
        long roleId = GetRoleId(profession);
        if (roleId > 0)
        {
            //ConnectingWindow.Singleton.Show();
            curProfession = (int)profession;
            ReqSelectRole srole = GetEmptyMsg<ReqSelectRole>();
            srole.roleId = roleId;
            SendMsg(ref srole);
        }
        else
        {
            Logger.err("LoginService:SelectRole:roleId <= 0 : " + roleId);
        }
    }


    public long GetRoleId(Profession profession)
    {
        foreach (LoginRoleInfo roleInfo in RoleList.roles)
        {
            if (roleInfo.profession == (int)profession)
            {
                return roleInfo.roleId;
            }
        }
        return 0;
    }

    public void ConnectServer()
    {
        //ConnectingWindow.Singleton.Show();
        //接受服务器消息
        //                       服务器列表类 服务器列表结构 服务器监听入口      
        //ServerListEntry config = GameConfig.ServerList.CurrentServer;
        var config = ServerList.Singleton.CurrentServer;
        //消息操作类
        MessageHandle.GetInstance().BeginConnect(config.ip, config.port);
    }

    private long reloginTimer;
    public void DoRelogin()
    {
        if (false == isReLogin)
        {
            backLoginedNum = 0;
            relogin();
        }
    }

    private void relogin()
    {
        //登陆界面以后才做短线重连
        if (GameManager.Singleton.IsStateOf(GameState.Invalid))
            return;
        if (GameManager.Singleton.IsStateOf(GameState.UpdateRes))
            return;
        if (GameManager.Singleton.IsStateOf(GameState.Login))
            return;

        if(GameManager.Singleton.IsStateOf(GameState.Character))
        {
            //选角界面特殊处理
            if (!MessageHandle.GetInstance().IsConnected())
                playerQuitRelogin();
            return;
        }

        backLoginedNum++;
        isReLogin = true;
        var roleInfo = RoleService.Singleton.GetRoleInfo();
        if (roleInfo != null && backLoginedNum <= backLoginMaxTime)
        {
            if (MessageHandle.GetInstance().IsConnected())
            {
                Debuger.Log("开始重连", backLoginedNum, roleInfo.token);
                ConnectingWindow.Singleton.Show();
                var req = GetEmptyMsg<ReqReLogin>();
                req.token = roleInfo.token;
                SendMsg(ref req);
                CoroutineManager.Singleton.stopCoroutine(reloginTimer);
                reloginTimer = CoroutineManager.Singleton.delayedCall(5, relogin);
            }else
            {
                //重新走登陆流程
                Debuger.Log("连接已断开，走后台默默登陆");
                MessageHandle.GetInstance().CloseSocket();
                ConnectingWindow.Singleton.Show();
                ConnectServer();
            }
        }else
        {
            loginFailedTip();
        }
    }
    
    private void resReLogin(GameEvent evt)
    {
        var res = GetCurMsg<ResReLogin>(evt.EventId);
        if(res.success)
        {
            Debuger.Log("重连成功");
            isReLogin = false;
            ConnectingWindow.Singleton.Hide();
        }
        else
        {
            Debuger.Log("重连失败，重新走登陆流程");
            MessageHandle.GetInstance().CloseSocket();
            ConnectServer();
        }
        CoroutineManager.Singleton.stopCoroutine(reloginTimer);
    }

    public void ReConnectSuccess()
    {
        if(isReLogin)
        {
            Debuger.Log("重连成功");
            isReLogin = false;
            ConnectingWindow.Singleton.Hide();
            CoroutineManager.Singleton.stopCoroutine(reloginTimer);
        }
    }

    private void loginFailedTip()
    {
        ConnectingWindow.Singleton.Hide();
        AgainConfirmWindow.Singleton.ShowTip("网络连接失败，是否重新连接？", playerReconnect, playerQuitRelogin, true);
    }

    private void playerReconnect()
    {
        isReLogin = false;
        DoRelogin();
    }

    private void playerQuitRelogin()
    {
        isReLogin = false;
        SceneLoader.Singleton.sceneName = null;
        SceneLoader.Singleton.nextState = GameState.UpdateRes;
        GameManager.Singleton.changeState(GameState.Loading);
    }
}
