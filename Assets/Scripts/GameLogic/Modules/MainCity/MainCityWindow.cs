
using FairyGUI;
using UI_MainCity;
using DG.Tweening;
using UI_Common;
using System;
using UnityEngine;
using System.Collections;

public class MainCityWindow : BaseWindow, IGuidable
{
    private UI_MainCityWindow window;

    private JoystickController joystick;
    private PlayerInfoItem playerInfo;
    private DoActionInterval timeDoAction;
    private int nowTimeS;

    private long clickTimer;
    CommonHeadIcon headIcon;

    public static ActorMC guidePet;
    private string chatWndName;

    public override void OnOpen()
    {
        base.OnOpen();
        window = getUiWindow<UI_MainCityWindow>();
        //window.m_MysteriousShopBtn.visible = ShopService.Singleton.GetShopIsOpen(ShopService.EShopType.Mysterious);
        //window.alpha = 0;
        //window.TweenFade(1, 0.5f).OnComplete(OnOpenEffectEnd);
        WinMgr.Singleton.Open<TopRoleInfo>(null, UILayer.TopHUD);

        PlayOpenEffect();
        InitView();
    }

    public override void PlayOpenEffect()
    {
        base.PlayOpenEffect();

        window.m_bottomColumn.m_anim.Play(OnOpenEffectEnd);
        window.m_battleBtn.m_anim.Play();
        window.m_roleInfo.m_anim.Play();
        window.m_btnEmail.m_anim.Play();
    }

    public GObject GetGuideObj(string param)
    {
        if(param == "1")
        {
            GGraph gra = new GGraph();
            gra.DrawRect(100, 100, 0, Color.clear, Color.clear);
            view.AddChild(gra);
            gra.onClick.Add(() =>
            {
                WinMgr.Singleton.Open<ZhenRongWindow>(null, UILayer.Popup);
                gra.parent.RemoveChild(gra, true);
            });

            //坐标转换
            var pos = new Vector3(-1, 0, 0);
            if (guidePet != null && guidePet.ShowObj != null)
                pos = guidePet.ShowObj.transform.position;
            Vector3 screenPos = Camera.main.WorldToScreenPoint(pos);
            screenPos.y = Screen.height - screenPos.y;
            Vector2 pt = GRoot.inst.GlobalToLocal(screenPos);
            gra.SetXY(pt.x - gra.width / 2f, pt.y - gra.height);
            return gra;
        }
        return null;
    }

    public override void AddEventListener()
    {
        base.AddEventListener();

        GED.ED.addListener(EventID.ResRoleInfo, OnRoleInfoChange);
        GED.ED.addListener(EventID.OnStriveHegemongBaoMingKuaiJie,OnQuanHuangZhengBaBaoMingKuaiJie);
        GED.ED.addListener(EventID.OnStriveHegemongGuanZhan,OnGuanZhanAnNiu);
        GED.ED.addListener(EventID.OnStriveHegemongEnd, OnStriveHegemongEnd);
        GED.ED.addListener(EventID.ShopOpenOrClose, _ShopOpenOrClose);
        GED.ED.addListener(EventID.TipFuncChange, refreshFuncTip);
        GED.ED.addListener(EventID.OnResModifyIcon, OnRoleInfoChange);
        GED.ED.addListener(EventID.OnResModifyNickname, OnRoleInfoChange);
    }

    public override void RemoveEventListener()
    {
        base.RemoveEventListener();

        GED.ED.removeListener(EventID.ResRoleInfo, OnRoleInfoChange);
        GED.ED.removeListener(EventID.OnStriveHegemongBaoMingKuaiJie, OnQuanHuangZhengBaBaoMingKuaiJie);
        GED.ED.removeListener(EventID.OnStriveHegemongGuanZhan, OnGuanZhanAnNiu);
        GED.ED.removeListener(EventID.OnStriveHegemongEnd, OnStriveHegemongEnd);
        GED.ED.removeListener(EventID.ShopOpenOrClose, _ShopOpenOrClose);
        GED.ED.removeListener(EventID.TipFuncChange, refreshFuncTip);
        GED.ED.removeListener(EventID.OnResModifyIcon, OnRoleInfoChange);
        GED.ED.removeListener(EventID.OnResModifyNickname, OnRoleInfoChange);
    }

    public override void InitView()
    {
        base.InitView();

        InitHeadIcon();
        InitTimeShow();
        RefreshRoleInfo();

        _RegisterRedDot("Shop", window.m_bottomColumn.m_btnShop.m_imgRed);
        _RegisterRedDot("EMail", window.m_btnEmail.m_imgRed);
        _RegisterRedDot("Guild", window.m_bottomColumn.m_guild.m_imgRed);
        _RegisterRedDot("mainArean", window.m_bottomColumn.m_jingJiBtn.m_imgRed);
        _RegisterRedDot("Task", window.m_bottomColumn.m_TaskBtn.m_Task_HongDian);

        refreshFuncTip();
    }

    private void InitHeadIcon()
    {
        Message.Role.RoleInfo roleInfo = RoleService.Singleton.GetRoleInfo();
        headIcon = window.m_roleInfo.m_headIcon as CommonHeadIcon;
        if (roleInfo == null)
        {
            Debug.LogError("服务器的主角数据信息未发下来");
            return;
        }
        headIcon.Init(roleInfo.headIconId, OnHeadIconBtnClick);

    }
    private void RefreshRoleInfo()
    {
        Message.Role.RoleInfo roleInfo = RoleService.Singleton.GetRoleInfo();
        window.m_roleInfo.m_Level.text = roleInfo.level + "";
        window.m_roleInfo.m_Name.text = roleInfo.roleName;
        window.m_roleInfo.m_ZhanLi.text = roleInfo.fightPower + "";
        headIcon.headID = roleInfo.headIconId;
        headIcon.RefreshView();
    }
    private void InitTimeShow()
    {
        DateTime nowTime = TimeUtils.currentServerDateTime().AddHours(8);
        nowTimeS = nowTime.Hour * 3600 + nowTime.Minute * 60 + nowTime.Second;

        timeDoAction = new DoActionInterval();
        timeDoAction.doAction(1, OnRefreshTime, null ,true);
    }

    private void OnRefreshTime(object obj)
    {
        nowTimeS++;
        window.m_timeTxt.text = TimeUtils.FormatTime3(nowTimeS);
    }

    protected override void OnOpenEffectEnd()
    {
        base.OnOpenEffectEnd();
        window.m_joystick.visible = false;
        //joystick = new JoystickController(window.m_joystick);
        if(chatWndName == null)
            //chatWndName = WinMgr.Singleton.Open<MainCityChatWnd>();
        
        window.m_battleBtn.onClick.Add(OnEnterBtn);
        window.m_bottomColumn.m_BagBtn.onClick.Add(OnBagBtn);
        window.m_bottomColumn.m_teamBtn.onClick.Add(OnTeamBtn);
        window.m_bottomColumn.m_GeDouJia.onClick.Add(OnGeDouJia);
        window.m_gmBtn.onClick.Add(OnGmBtn);
        window.m_bottomColumn.m_TaskBtn.onClick.Add(OnTaskBtn);
        window.m_bottomColumn.m_jingJiBtn.onClick.Add(OnJinJiBtnClick);
        window.m_bottomColumn.m_tiaoZhanBtn.onClick.Add(OnTiaoZhanBtnClick);
        window.m_bottomColumn.m_ZhaoHuan.onClick.Add(OnZhaoHuan);
        window.m_btnEmail.onClick.Add(OnEmailClick);
        window.m_bottomColumn.m_btnShop.onClick.Add(OnShopClick);
        window.m_bottomColumn.m_guild.onClick.Add(OnGuildClick);
        window.m_SH_GuanZhanBtn.onClick.Add(OnQuanHuangZhengBa);
        window.m_SH_baomingkuaijie.onClick.Add(OnQuanHuangZhengBa);
        window.m_MysteriousShopBtn.onClick.Add(OnMysteriousShopClick);
        window.m_funcIcon.onClick.Add(onViewFunc);
        //window.m_btnChat.onClick.Add(_OnChatClick);
        window.m_CeShiChuangKouBtn.onClick.Add(OnTestWindow);
        window.m_btnRecharge.onClick.Add(_OnRechargeClick);
        window.m_PaiHangBangBtn.onClick.Add(OnOpenPaiHangBangWindow);
        window.m_qianDaoBtn.onClick.Add(OnQianDaoBtnClick);
        MainCityCamCtrl.Singleton.SetTouchHolder(window.m_touchHold);

        if(clickTimer == 0)
            clickTimer = CoroutineManager.Singleton.startCoroutine(updateTouch());
    }

    private IEnumerator updateTouch()
    {
        while(true)
        {
            bool isMouseUp = false;
            bool isOnUI = Stage.isTouchOnUI;
            if(isOnUI)
            {
                var touchObj = Stage.inst.touchTarget;
                if (touchObj != null && window.m_touchHold.displayObject == touchObj)
                {
                    isOnUI = false;
                }
            }
            Vector2 mousePos = Vector2.zero;
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                isMouseUp = true;
                mousePos = Input.GetTouch(0).position;
            }

            if (Input.GetMouseButtonUp(0))
            {
                isMouseUp = true;
                mousePos = Input.mousePosition;
            }

            //点击模型打开阵容界面
            if (!isOnUI && isMouseUp) //点在了UI上
            {
                var camera = Camera.main;
                if (camera == null)
                    camera = Camera.current;
                if (camera != null)
                {
                    //var mousePos = Input.mousePosition;
                    Ray _ray = camera.ScreenPointToRay(mousePos);
                    RaycastHit objhit;
                    if (Physics.Raycast(_ray, out objhit, 1000))
                    {
                        int layer = objhit.transform.gameObject.layer;
                        if (layer == LayerMask.NameToLayer("Actor"))
                        {
                            WinInfo info = new WinInfo();
                            var ab = objhit.transform.GetComponent<ActorBehavior>();
                            if (ab != null)
                            {
                                ActorBase actor = ActorManagerMC.Singleton.Get(ab.actorId);
                                if(actor != null)
                                    info.param = actor.getTemplateId();
                            }
                            //WinMgr.Singleton.Open<ZhenRongWindow>(info, UILayer.Popup);
                            OpenChild<ZhenRongWindow>(info);
                        }
                    }
                }
            }
            yield return null;
        }
    }

    private void refreshFuncTip(GameEvent evt = null)
    {
        // （暂时屏蔽）
        return;

        if (FuncService.Singleton.curTipFuncID <= 0)
        {
            window.m_funcIcon.visible = false;
            return;
        }

        var bean = ConfigBean.GetBean<Data.Beans.t_moduleBean, int>(FuncService.Singleton.curTipFuncID);
        if(bean == null)
        {
            window.m_funcIcon.visible = false;
            return;
        }

        window.m_funcIcon.visible = true;
        var icon = window.m_funcIcon.m_tipIcon;
        var name = window.m_funcIcon.m_name;
        var condition = window.m_funcIcon.m_cond;
        UIGloader.SetUrl(icon, bean.t_icon);
        name.text = bean.t_name;

        string tip = "开启";
        var lb = ConfigBean.GetBean<Data.Beans.t_languageBean, int>(7201005);
        if (lb != null) tip = lb.t_content;
        condition.text = FuncService.Singleton.GetFuncCondition(FuncService.Singleton.curTipFuncID) + tip;
    }

    //功能预告
    private void onViewFunc()
    {
        WinMgr.Singleton.Open<FuncTipWindow>(null, UILayer.Popup);
    }

    #region   事件响应 ------------------------------------------------------------------------

    protected override void OnClose()
    {
        guidePet = null;
        CoroutineManager.Singleton.stopCoroutine(clickTimer);
        //topRoleInfo.OnWindowClose();
        if (timeDoAction != null)
        {
            timeDoAction.kill();
            timeDoAction = null;
        }
        base.OnClose();
    }
    private void OnTestWindow()
    {
        WinInfo info = new WinInfo();
        info.param = 100;
        WinMgr.Singleton.Open<Top_mainWindow>(info, UILayer.Popup);
    }
    private void OnOpenPaiHangBangWindow()
    {
        //TipWindow.Singleton.ShowTip("暂未开放！");
        //return;
        if (FuncService.Singleton.IsFuncOpen(7011))
        {
            WinInfo info = new WinInfo();
            WinMgr.Singleton.Open<Top_mainWindow>(info, UILayer.Popup);
        }
        else
        {
            FuncService.Singleton.TipFuncNotOpen(7011);
        }
    }

    private void OnQianDaoBtnClick()
    {
        //TipWindow.Singleton.ShowTip("暂未开放！");
        //return;
        OpenChild<SiginInWindow>(WinInfo.Create(false, null, false));
    }
    private void OnGmBtn()
    {
        //OpenChild<GMWindow>(WinInfo.Create(false, winName, false));
        WinMgr.Singleton.Open<GMWindow>(WinInfo.Create(true, winName, false), UILayer.Popup);
    }
    //拳皇争霸报名快捷
    private void OnQuanHuangZhengBaBaoMingKuaiJie(GameEvent evt)
    {
        // （暂时屏蔽）
        return;
        if (StriveHegemongService.Singleton.join == false)
            window.m_SH_baomingkuaijie.visible = true;
        else
            window.m_SH_baomingkuaijie.visible = false;
    }
    //观战按钮
    private void OnGuanZhanAnNiu(GameEvent evt)
    {
        window.m_SH_GuanZhanBtn.visible = true;
        window.m_SH_baomingkuaijie.visible = false;
    }
    //拳皇争霸结束
    private void OnStriveHegemongEnd(GameEvent evt)
    {
        window.m_SH_GuanZhanBtn.visible = false;
    }

    //神秘商店开启或关闭
    private void _ShopOpenOrClose(GameEvent evt)
    {
        ShopService.EShopType shop = (ShopService.EShopType)evt.Data;
        if (shop != ShopService.EShopType.Mysterious)
            return;

        window.m_MysteriousShopBtn.visible = ShopService.Singleton.GetShopIsOpen(shop);
    }

    private void OnTeamBtn()
    {
        //OpenChild<ZhenRongWindow>(WinInfo.Create(true));
        //OpenChild<TongXiangGuanWindow>(WinInfo.Create(true));

        //TipWindow.Singleton.ShowTip("暂未开放！");
        //return;

        OpenChild<XunLianJiaWindow>(WinInfo.Create(true, winName));
    }

    private void OnBagBtn()
    {
        OpenChild<BagWindow>(WinInfo.Create(true, winName));
    }

    private void OnEnterBtn()
    {
        //SceneLoader.Singleton.nextState = GameState.Battle;
        //SceneLoader.Singleton.sceneName = "Test_LuYe_01";
        //UIPackage.AddPackage(WinEnum.BasePath + WinEnum.UI_Battle);
        //GameManager.Singleton.changeState(GameState.Loading);
        //LevelService.Singleton.ReqFightStart(10101);
        OpenChild<LevelMainWindow>(WinInfo.Create(true, winName));
    }
    private void OnGeDouJia()
    {
        OpenChild<GeDouJiaWindow>(WinInfo.Create(true, winName));
    }
    private void OnTaskBtn()
    {
        OpenChild<TaskWindow>(WinInfo.Create(true, winName, true));
    }
    private void OnZhaoHuan()
    {
        WinInfo info = new WinInfo();
        OpenChild<DrawCardWindow>(WinInfo.Create(true, winName, true));
    }

    private void OnJinJiBtnClick()
    {
        //WinMgr.Singleton.Open<SH_MainWindow>();
        //WinMgr.Singleton.Open<ArenaMainWindow>(null, UILayer.Popup);
        //TipWindow.Singleton.ShowTip("暂未开放！");
        //return;

        OpenChild<ArenaTypeWnd>(WinInfo.Create(true, winName));
    }


    //神秘商店点击
    private void OnMysteriousShopClick()
    {
        //TipWindow.Singleton.ShowTip("暂未开放！");
        //return;
        WinMgr.Singleton.Open<MysteriousShopWnd>(null, UILayer.Popup);
    }


    private void OnQuanHuangZhengBa()
    {
        //TipWindow.Singleton.ShowTip("暂未开放！");
        //return;
        OpenChild<SH_MainWindow>(WinInfo.Create(true, winName));
    }

    private void OnEmailClick()
    {
        //TipWindow.Singleton.ShowTip("暂未开放！");
        //return;
        OpenChild<MailWnd>(WinInfo.Create(true, winName));

        //SceneLoader.Singleton.nextState = GameState.BattleReplay;
        //SceneLoader.Singleton.sceneName = "lvl_gq01_mysl_01";
       // GameManager.Singleton.changeState(GameState.Loading);
    }

    private void OnGuildClick()
    {
        //SceneLoader.Singleton.nextState = GameState.BattleReplay;
        //SceneLoader.Singleton.sceneName = "lvl_gq01_mysl_01";
        //GameManager.Singleton.changeState(GameState.Loading);
        //TipWindow.Singleton.ShowTip("暂未开放！");
        //return;
        if (!FuncService.Singleton.TipFuncNotOpen(1601))
        {
            return;
        }

        if (RoleService.Singleton.GetRoleInfo().guildId <= 0)
        {
            WinMgr.Singleton.Open<JoinGuildMainWnd>();
        }
        else
        {
            GuildService.Singleton.ReqGuildInfo();

        }
         
    }


    //充值点击
    private void _OnRechargeClick()
    {
        //TipWindow.Singleton.ShowTip("暂未开放！");
        //return;
        VipWndMgr.Singleton.OpenWnd(EVipWndType.Recharge);
        WinMgr.Singleton.Open<VipMainWnd>(null, UILayer.Popup);
        //OpenChild<AoyiMainWnd>(WinInfo.Create(true, winName));
    }

    //聊天点击
    private void _OnChatClick()
    {
        //TipWindow.Singleton.ShowTip("暂未开放！");
        //return;
        WinMgr.Singleton.Open<ChatWnd>(null, UILayer.TopHUD);
    }

    private void OnShopClick()
    {
        //TipWindow.Singleton.ShowTip("暂未开放！");
        //return;
        OpenChild<ShopMainWindow>(WinInfo.Create(true, winName));
    }

    private void OnTiaoZhanBtnClick()
    {
        //TipWindow.Singleton.ShowTip("暂未开放！");
        //return;
        ChallegeService.Singleton.window = this;
        ChallegeService.Singleton.ReqChallengeInfo();
    }

    private void OnRoleInfoChange(GameEvent evt)
    {
        RefreshRoleInfo();
    }

    private void OnHeadIconBtnClick(object obj)
    {
        WinMgr.Singleton.Open<RoleInfoWindow>(null, UILayer.Popup);
    }

    #endregion;
}