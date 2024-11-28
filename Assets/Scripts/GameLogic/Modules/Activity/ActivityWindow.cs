using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_Activity;
using Message.Challenge;

public class ActivityWindow : BaseWindow {

    private int goldTime;
    private int expTime;
    private int nvGeDouJiaTime;
    private int huanXiangTime;

    UI_ActivityWindow window;
    DoActionInterval doAction;

    UITable table;

    public override void OnOpen()
    {
        base.OnOpen();

        window = getUiWindow<UI_ActivityWindow>();
        
        InitData();
        BindEvent();
        InitView();
        PlayOpenEffect();
    }

    public override void PlayOpenEffect()
    {
        base.PlayOpenEffect();

        (window.m_commonTop as UI_Common.UI_commonTop).m_anim.Play();
        window.m_activityGroup.m_anim.Play();
    }

    public override void AddEventListener()
    {
        base.AddEventListener();

        GED.ED.addListener(EventID.OnActivityFightEndRes, OnFightEnd);
        GED.ED.addListener(EventID.OnActivitySaoDanRes, OnFightEnd);
    }

    public override void RemoveEventListener()
    {
        base.RemoveEventListener();
        GED.ED.removeListener(EventID.OnActivityFightEndRes, OnFightEnd);
        GED.ED.removeListener(EventID.OnActivitySaoDanRes, OnFightEnd);
    }

    private void InitData()
    {
        // 进来默认选择金币挑战
        PetService.Singleton.zhenRongType = ZhenRongType.GoldTiaoZhan;
        // 倒计时的显示
        //InitRemainTime();
        doAction = new DoActionInterval();
        doAction.doAction(1, DoActionCall, null, true);
    }
    /// <summary>
    /// 初始化剩余的时间
    /// </summary>
    private void InitRemainTime()
    {
        ActivityActInfo goldInfo = ChallegeService.Singleton.GetActivityInfoByType(ActivityType.Gold);
        if (goldInfo != null)
        {
            goldTime = goldInfo.baseInfo.seconds;
        }

        ActivityActInfo womenInfo = ChallegeService.Singleton.GetActivityInfoByType(ActivityType.NvGeDouJia);
        if (womenInfo != null)
        {
            nvGeDouJiaTime = womenInfo.baseInfo.seconds;
        }

        ActivityActInfo huanXiangInfo = ChallegeService.Singleton.GetActivityInfoByType(ActivityType.HuanXiang);
        if (huanXiangInfo != null)
        {
            huanXiangTime = huanXiangInfo.baseInfo.seconds;
        }

        ActivityActInfo expInfo = ChallegeService.Singleton.GetActivityInfoByType(ActivityType.Exp);
        if (expInfo != null)
        {
            expTime = expInfo.baseInfo.seconds;
        }
    }

    private void BindEvent()
    {
        (window.m_commonTop as UI_Common.UI_commonTop).m_closeBtn.onClick.Add(OnCloseBtn);
    }

    public override void InitView()
    {
        base.InitView();

        ActivityPanel activityPanel = window.m_activityPanel as ActivityPanel;
        activityPanel.Init();

        (window.m_commonTop as UI_Common.UI_commonTop).m_title.text = "活动关卡";

        table = new UITable();
        table.Init(window.m_activityGroup.m_activityCtrl, OnActivityCtrl);
        table.AddBtnAnim(window.m_activityGroup.m_goldBtn, window.m_activityGroup.m_expBtn,
            window.m_activityGroup.m_womenBtn, window.m_activityGroup.m_huanXiangBtn);

        OnActivityCtrl();
        RefreshView();
        InitFuncState();
    }
    /// <summary>
    /// 初始化功能的开放状态
    /// </summary>
    private void InitFuncState()
    {
        //FuncService.Singleton.SetFuncLock(window.m_goldBtn, 18021);
        //FuncService.Singleton.SetFuncLock(window.m_expBtn, 18022);
        //FuncService.Singleton.SetFuncLock(window.m_huanXiangBtn, 18024);
        //FuncService.Singleton.SetFuncLock(window.m_womenBtn, 18023);
        ResActivityActInfo activityActInfo = ChallegeService.Singleton.ActivityActInfo;
        if (activityActInfo != null)
        {
            window.m_activityGroup.m_goldBtn.visible = activityActInfo.activityActInfo.Count >= 1;
            window.m_activityGroup.m_expBtn.visible = activityActInfo.activityActInfo.Count >= 2;
            window.m_activityGroup.m_huanXiangBtn.visible = activityActInfo.activityActInfo.Count >= 3;
            window.m_activityGroup.m_womenBtn.visible = activityActInfo.activityActInfo.Count >= 4;
        }
    }

    public override void RefreshView()
    {
        base.RefreshView();

        RefreshActivityPage();
    }
    /// <summary>
    /// 刷新活动页签的显示
    /// </summary>
    private void RefreshActivityPage()
    {
        RefreshGoldPage();
        RefreshNvGeDouJiaPage();
        RefreshHuanXiangPage();
        RefreshExpPage();
    }

    private void RefreshGoldPage()
    {
        ActivityActInfo goldInfo = ChallegeService.Singleton.GetActivityInfoByType(ActivityType.Gold);
        if (goldInfo == null)
            window.m_activityGroup.m_goldBtn.visible = false;
        else
        {
            window.m_activityGroup.m_goldBtn.m_finishGroup.visible = goldInfo.baseInfo.completeTimes >= goldInfo.maxTimes;
            window.m_activityGroup.m_goldBtn.m_unOpenGroup.visible = goldInfo.isOpen == 0;
            window.m_activityGroup.m_goldBtn.m_openTipLabel.text = UIUtils.GetStrByLanguageID(71802006);
        }
    }

    private void RefreshNvGeDouJiaPage()
    {
        ActivityActInfo nvGeDouJiaInfo = ChallegeService.Singleton.GetActivityInfoByType(ActivityType.NvGeDouJia);
        if (nvGeDouJiaInfo == null)
            window.m_activityGroup.m_womenBtn.visible = false;
        else
        {
            window.m_activityGroup.m_womenBtn.m_finishGroup.visible = nvGeDouJiaInfo.baseInfo.completeTimes >= nvGeDouJiaInfo.maxTimes;
            window.m_activityGroup.m_womenBtn.m_unOpenGroup.visible = nvGeDouJiaInfo.isOpen == 0;
            window.m_activityGroup.m_womenBtn.m_openTipLabel.text = UIUtils.GetStrByLanguageID(71802011);
        }
    }

    private void RefreshHuanXiangPage()
    {
        ActivityActInfo huanXiangInfo = ChallegeService.Singleton.GetActivityInfoByType(ActivityType.HuanXiang);
        if (huanXiangInfo == null)
            window.m_activityGroup.m_huanXiangBtn.visible = false;
        else
        {
            window.m_activityGroup.m_huanXiangBtn.m_finishGroup.visible = huanXiangInfo.baseInfo.completeTimes >= huanXiangInfo.maxTimes;
            window.m_activityGroup.m_huanXiangBtn.m_unOpenGroup.visible = huanXiangInfo.isOpen == 0;
            window.m_activityGroup.m_huanXiangBtn.m_openTipLabel.text = UIUtils.GetStrByLanguageID(71802015);
        }
    }

    private void RefreshExpPage()
    {
        ActivityActInfo expInfo = ChallegeService.Singleton.GetActivityInfoByType(ActivityType.Exp);
        if (expInfo == null)
            window.m_activityGroup.m_expBtn.visible = false;
        else
        {
            window.m_activityGroup.m_expBtn.m_finishGroup.visible = expInfo.baseInfo.completeTimes >= expInfo.maxTimes;
            window.m_activityGroup.m_expBtn.m_unOpenGroup.visible = expInfo.isOpen == 0;
            window.m_activityGroup.m_expBtn.m_openTipLabel.text = UIUtils.GetStrByLanguageID(71802006);
        }
    }

    private void OnActivityCtrl(int index = 0)
    {
        switch (window.m_activityGroup.m_activityCtrl.selectedIndex)
        {
            case 0:
                ChallegeService.Singleton.activityType = ActivityType.Gold;
                PetService.Singleton.zhenRongType = ZhenRongType.GoldTiaoZhan;
                break;
            case 1:
                ChallegeService.Singleton.activityType = ActivityType.Exp;
                PetService.Singleton.zhenRongType = ZhenRongType.ExpTiaoZhan;
                break;
            case 2:
                ChallegeService.Singleton.activityType = ActivityType.NvGeDouJia;
                PetService.Singleton.zhenRongType = ZhenRongType.NvGeDouJia;
                break;
            case 3:
                ChallegeService.Singleton.activityType = ActivityType.HuanXiang;
                PetService.Singleton.zhenRongType = ZhenRongType.HuanXiangTiaoZhan;
                break;
            default:
                break;
        }

        ActivityPanel activityPanel = window.m_activityPanel as ActivityPanel;
        activityPanel.RefreshView();
    }

    private void DoActionCall(object obj)
    {
        ActivityPanel activityPanel = window.m_activityPanel as ActivityPanel;

        List<ActivityActInfo> activityInfoList = ChallegeService.Singleton.ActivityActInfo.activityActInfo;
        int count = activityInfoList.Count;
        ActivityActInfo activityInfo = null;
        for (int i = 0; i < count; i++)
        {
            activityInfo = activityInfoList[i];
            if (activityInfo.baseInfo.seconds > 0)
                activityInfo.baseInfo.seconds--;
            else
                activityPanel.RefreshNormalVeiw();
        }

        activityPanel.RefreshTimeLabel();
    }

    private void OnFightEnd(GameEvent evt)
    {
        switch (ChallegeService.Singleton.activityType)
        {
            case ActivityType.Gold:
                window.m_activityGroup.m_activityCtrl.selectedIndex = 0;
                break;
            case ActivityType.Exp:
                window.m_activityGroup.m_activityCtrl.selectedIndex = 1;
                break;
            case ActivityType.NvGeDouJia:
                window.m_activityGroup.m_activityCtrl.selectedIndex = 2;
                break;
            case ActivityType.HuanXiang:
                window.m_activityGroup.m_activityCtrl.selectedIndex = 3;
                break;
            default:
                break;
        }
        OnActivityCtrl();
        RefreshView();
    }

    protected override void OnCloseBtn()
    {
        if(doAction != null && doAction.IsRunning)
            doAction.kill();

        if (table != null)
            table.Close();

        base.OnCloseBtn();
    }

    protected override void OnClose()
    {
        RestoreWndMgr.Singleton.SaveWndData<ActivityWindow>(Info);
        if (ChallegeService.Singleton.window == this)
            ChallegeService.Singleton.window = null;
        base.OnClose();

    }

}
