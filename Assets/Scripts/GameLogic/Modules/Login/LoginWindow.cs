using FairyGUI;
using System;
using System.Collections.Generic;
using UI_Login;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginWindow : BaseWindow
{

    private UI_LoginWindow window;
    private GameObject gameObject;

    private int logoCount = 1;
    private int logoTotalCount = 1;
    private string logoURL = "loginLogo";

    public override void OnOpen()
    {
        base.OnOpen();
        window = getUiWindow<UI_LoginWindow>();
        window.m_loginBtn.onClick.Add(OnLoginBtn);
        InitView();
        ShowImgs();
        LoginScene.Singleton.Load("lvl_hd03_xr");
    }

    private void ShowImgs()
    {
        string imgURL = logoURL + logoCount;
        UIGloader.SetUrl(window.m_logoLoader, UIUtils.GetLoaderUrl(WinEnum.UI_Login, imgURL));
        if (window.m_logoAnim.playing)
            window.m_logoAnim.Stop();
        window.m_logoAnim.Play(ShowImgsFinishCall);
        logoCount++;
    }

    private void ShowImgsFinishCall()
    {
        if (logoCount > logoTotalCount)
        {
            gameObject = LoadGo("eff_ui_denglu", new Vector3(0, 0, 300));
            GoWrapper wrapper = new GoWrapper(gameObject);
            window.m_texiao.SetNativeObject(wrapper);

            window.m_logoGroup.visible = false;
            // 播放动画
            window.m_loginLogo.m_anim.Play();
        }
        else
        {
            ShowImgs();
        }
    }

    public override void AddEventListener()
    {
        base.AddEventListener();
        GED.ED.addListener(EventID.SelectServerWindowClose, OnSelectServerWindowClose);
        GED.ED.addListener(EventID.OnConnectServer, OnConnectServer);
        GED.ED.addListenerOnce(EventID.ServerListLoaded, onServerListLoaded);
    }

    private void onServerListLoaded(GameEvent evt)
    {
        InitView();
    }

    public override void RemoveEventListener()
    {
        base.RemoveEventListener();
        GED.ED.removeListener(EventID.SelectServerWindowClose, OnSelectServerWindowClose);
        GED.ED.removeListener(EventID.OnConnectServer, OnConnectServer);
        GED.ED.removeListener(EventID.ServerListLoaded, onServerListLoaded);
    }

    private void OnSelectServerWindowClose(GameEvent evt)
    {
        window.m_serverBtn.text = ServerList.Singleton.CurrentServer.name;
        //window.m_serverBtn.text = GameConfig.ServerList.CurrentServer.ServerName;
    }

    private void OnConnectServer(GameEvent evt)
    {
        string userName = window.m_userName.text;
        //TODO:加上锁屏
        GED.ED.removeListener(EventID.ResRoleList, OnRoleList);
        GED.ED.addListener(EventID.ResRoleList, OnRoleList);
        userName = userName.Trim();
        LoginService.Singleton.DoLogin(userName);
    }

    public override void InitView()
    {
        base.InitView();
        if (!ServerList.Singleton.Loaded)
            return;
        //window.m_serverBtn.text = GameConfig.ServerList.CurrentServer.ServerName;
        window.m_serverBtn.text = ServerList.Singleton.CurrentServer.name;
        window.m_serverBtn.onClick.Add(OnServerBtn);
        window.m_userName.text = (string)PlayerPrefs.GetString(PlayerPrefsKeys.key_Login_Name, RandomAUserName());
    }
    /// <summary>
    /// 随机一个用户名
    /// </summary>
    /// <returns></returns>
    private string RandomAUserName()
    {
        // 取当前时间作为基数
        string userName = DateTime.Now.Ticks + "";
        // 随机一个数字
        int randomNum = new System.Random().Next(1, int.MaxValue);
        userName += randomNum; 
        return userName;
    }
    
    private void OnServerBtn()
    {
        WinMgr.Singleton.Open<SelectServerWindow>();
        //OneParam<int> param = new OneParam<int>();
        //param.value = 10101;
        //OpenChild<GuanQiaWindow>(WinInfo.Create(true, null, false, param));
        //LevelService.Singleton.TestSaoDang();
        //WinMgr.Singleton.Open<SupperSaoDangWindow>();
        // WinMgr.Singleton.Open<eliteSupperSaoDangWnd>(null, UILayer.Popup);
    }

    private void OnLoginBtn()
    {
        string userName = window.m_userName.text;
        if (string.IsNullOrEmpty(userName))
        {
            TipWindow.Singleton.ShowTip("用户名不能为空");
            return;
        }

        var config = ServerList.Singleton.CurrentServer;
        if(config.state >= 0 || SelectServerWindow.IsTrustUser)
        {
            //白名单或者开服时间可以进服务器
            //PlayerPrefs.DeleteAll();
            PlayerPrefs.SetString(PlayerPrefsKeys.key_Login_Name, userName);
            LoginService.Singleton.ConnectServer();
        }else
        {
            AgainConfirmWindow.Singleton.TipOneButton("服务器正在维护，请稍后重试，详情请查看公告！");
        }
    }

    private void OnRoleList(GameEvent evt)
    {
        Close();
        GameManager.Singleton.changeState(GameState.Character);
    }

    protected override void OnClose()
    {
        base.OnClose();
        if(gameObject != null)
             GameObject.DestroyObject(gameObject);
        GED.ED.removeListener(EventID.ResRoleList, OnRoleList);
    }

}