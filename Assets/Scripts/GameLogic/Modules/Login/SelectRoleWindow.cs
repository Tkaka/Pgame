using FairyGUI;
using Message.Login;
using Message.Role;
using UI_Login;
using UnityEngine;
using Data.Beans;

public class SelectRoleWindow : BaseWindow
{

    private UI_SelectRoleWindow window;
    GameObject gameObject;//模型
    DoActionInterval doAction;
    Camera camera;
    private int time = 6;
    bool shouci = false;
    Animator animator;
    //SimpleAnimation

    public override void OnOpen()
    {
        base.OnOpen();
        window = getUiWindow<UI_SelectRoleWindow>();
        //InitView();
        OnAddKeyEvent();
        Init();
    }

    //public override void InitView()
    //{
    //    base.InitView();
    //    window.m_boyEnter.visible = false;
    //    window.m_boyEnter.onClick.Add(OnBoyEnter);
    //    window.m_girlEnter.visible = false;
    //    window.m_girlEnter.onClick.Add(OnGirlEnter);
    //    window.m_boyCreate.visible = true;
    //    window.m_boyCreate.onClick.Add(OnBoyCreate);
    //    window.m_girlCreate.visible = true;
    //    window.m_girlCreate.onClick.Add(OnGirlCreate);
    //    ResRoleList roleList = LoginService.Singleton.RoleList;
    //    if (roleList.roles.Count > 0)
    //    {
    //        foreach (Message.Login.LoginRoleInfo roleInfo in roleList.roles)
    //        {
    //            if (roleInfo.profession == (int)Profession.Boy)
    //            {
    //                window.m_boyName.text = roleInfo.roleName;
    //                window.m_boyEnter.visible = true;
    //                window.m_boyCreate.visible = false;
    //            }
    //            else if (roleInfo.profession == (int)Profession.Girl)
    //            {
    //                window.m_girlName.text = roleInfo.roleName;
    //                window.m_girlEnter.visible = true;
    //                window.m_girlCreate.visible = false;
    //            }
    //        }
    //    }
    //}
    private void OnAddKeyEvent()
    {
        window.m_jinruyouxi.onClick.Add(OnJinRuYouXi);//进入游戏
        window.m_chuangjianjuese.onClick.Add(OnChuangJianJueSe);//创建角色
        window.m_ChongZhiBtn.onClick.Add(OnChongzhi); //重置选定角色名
        window.m_OkBtn.onClick.Add(OnQoeDing);//确定角色名
        window.m_TiaoGuo.onClick.Add(OnTiaoGuo);
        window.m_YouXi.visible = false;
        GED.ED.addListener(EventID.ResServerTime, OnResServerTime);
    }
    private void Init()
    {
        window.m_jinruyouxi.visible = false;
        window.m_chuangjianjuese.visible = true;
        shouci = true;
        ResRoleList roleList = LoginService.Singleton.RoleList;
        if (roleList.roles.Count > 0)
        {
            foreach (Message.Login.LoginRoleInfo roleInfo in roleList.roles)
            {
                if (roleInfo.profession == (int)Profession.Boy)
                {
                    LoginService.Singleton.SelectRole(Profession.Boy);
                    OnClose();
                    SceneLoader.Singleton.nextState = GameState.MainCity;
                    SceneLoader.Singleton.sceneName = GSceneName.MaiCity;
                    GameManager.Singleton.changeState(GameState.Loading);
                }
            }
        }
        else
        {
            doAction = new DoActionInterval();
            doAction.doAction(1, OnDaoJiShi, null, true);
            OnShowScecn();
        }
    }
    private void OnTiaoGuo()
    {
        //if(animator != null)
        //    animator.Play(5);
        if(animator != null)
            animator.speed *= 100;
        animator = null;
        time = 0;
    }
    //重置角色名
    private void OnChongzhi()
    {
        window.m_roleName.text = "";
    }
    //确定角色名
    private void OnQoeDing()
    {
        string roleName = window.m_roleName.text.Trim();
        if (!string.IsNullOrEmpty(roleName) && roleName != "未创建")
        {
            LoginService.Singleton.CreateRole(roleName, (int)Profession.Boy);
            //window.m_JinRuYouXi.alpha = 0;
        }
        else
            Logger.err("rolename is null");
    }
    //进入游戏
    private void OnJinRuYouXi()
    {
        LoginService.Singleton.SelectRole(Profession.Boy);
    }
    //创建角色
    private void OnChuangJianJueSe()
    {
        window.m_JinRuYouXi.visible = false;
        window.m_JueSeMing.visible = true;//设置角色名的组
    }
    private void OnResServerTime(GameEvent evt)
    {
        ResRoleInfo resRoleInfo = RoleService.Singleton.RoleInfo;
        if (resRoleInfo.result == 1)
        {
            window.m_JinRuYouXi.visible = false;
            window.m_JueSeMing.visible = false;
            OnOpenMainCity();
        }
        else
        {
            Logger.err("登陆失败");
        }
    }

    //private void OnBoyCreate()
    //{
    //    string roleName = window.m_boyName.text.Trim();
    //    if (!string.IsNullOrEmpty(roleName) && roleName != "未创建")
    //        LoginService.Singleton.CreateRole(roleName, (int)Profession.Boy);
    //    else
    //        Logger.err("rolename is null");
    //}

    //private void OnGirlCreate()
    //{
    //    string roleName = window.m_girlName.text.Trim();
    //    if (!string.IsNullOrEmpty(roleName) && roleName != "未创建")
    //        LoginService.Singleton.CreateRole(roleName, (int)Profession.Girl);
    //    else
    //        Logger.err("rolename is null");
    //}

    /*private bool CheckName(bool isBoy)
    {
        string userName;
        if (isBoy)
            userName = view.m_boyName.text.Trim();
        else
            userName = view.m_girlName.text.Trim();
        if (!string.IsNullOrEmpty(userName) && userName != "未创建")
        {
            return true;
        }
        else
        {
            Logger.log("user name 不合法");
            return false;
        }
    }*/

    //private void OnBoyEnter()
    //{
    //    LoginService.Singleton.SelectRole(Profession.Boy);
    //}

    //private void OnGirlEnter()
    //{
    //    LoginService.Singleton.SelectRole(Profession.Girl);
    //}
    private void OnShowScecn()
    {
        t_professionBean professionBean = ConfigBean.GetBean<t_professionBean, int>(100);
        if (professionBean != null)
        {
            string name = professionBean.t_city_prefab;
            gameObject = this.LoadGo(name);
        }
        else
        {
            Logger.err("未能找到模型名");
        }
        window.m_ShouCiHuanYing.visible = false;
        camera = Camera.main;
        if (camera == null)
            camera = Camera.current;
        if (camera != null)
        {
            if(animator == null)
                animator = camera.GetComponent<Animator>();
            animator.Play("lvl_hd03_ani_xuanren_cam");
        }
    }
    //登录完成
    private void OnOpenMainCity()
    {
        if (shouci)
        {
            //window.m_ShouCiHuanYing.visible = true;
            window.m_Name.text = window.m_roleName.text;
        }
        if (camera != null)
        {
            if(animator == null)
                animator = camera.GetComponent<Animator>();
            animator.Play("lvl_hd03_ani_xuanren_cam_login");
            animator.speed = 1;
            window.m_JinRuYouXi.visible = false;
            doAction = new DoActionInterval();
            time = 8;
            doAction.doAction(1, DengLuDaoJiShi, null, true);
        }
        if(gameObject != null)
        {
            SimpleAnimation roleAnimator = gameObject.GetComponentInChildren<SimpleAnimation>();
            if (roleAnimator != null)
                roleAnimator.Play("win");
        }
    }
    private void OnDaoJiShi(object obj)
    {
        time--;
        if (time < 0)
        {
            if (doAction != null)
            {
                doAction.kill();
                doAction = null;
                window.m_YouXi.visible = true;
                window.m_Xianshi.Play();
            }
        }
    }
    private void DengLuDaoJiShi(object obj)
    {
        time--;
        if (time < 6)
        {
            window.m_ShouCiHuanYing.visible = false;
        }
        if (time < 0)
        {
            if (doAction != null)
            {
                doAction.kill();
                doAction = null;
            }
            OnClose();
            SceneLoader.Singleton.nextState = GameState.MainCity;
            SceneLoader.Singleton.sceneName = GSceneName.MaiCity;
            GameManager.Singleton.changeState(GameState.Loading);
        }
    }

    protected override void OnClose()
    {
        base.OnClose();
        if (animator != null)
        {
            animator.StopPlayback();
            animator = null;
        }
        if (gameObject != null)
        {
            GameObject.DestroyObject(gameObject);
            gameObject = null;
            camera = null;
        }
        window = null;
        GED.ED.removeListener(EventID.ResServerTime, OnResServerTime);
    }
}