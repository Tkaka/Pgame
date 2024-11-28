using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_CloneTeamFight;
using FairyGUI;
using Data.Beans;
using Message.Challenge;
using DG.Tweening;

public class CloneTeamWindow : BaseWindow {

    UI_CloneTeamWindow window;
    int petID;
    long coroutineID = -1;
    Tween tween;
    /// <summary>
    /// 邀请和通知的冷却时间
    /// </summary>
    private int coldTime = 10;
    private int worldColdTime = 0;
    private int sheTuanColdTime = 0;
    private int noticeColdTime = 0;

    private DoActionInterval actionInterval;


    public override void OnOpen()
    {
        base.OnOpen();

        window = getUiWindow<UI_CloneTeamWindow>();
        RestoreWndMgr.Singleton.ClearData();
        InitData();
        BindEvent();
        InitView();
        RefreshView();
    }
    private void BindEvent()
    {
        window.m_backBtn.onClick.Add(OnCloseBtn);
        window.m_ruleBtn.onClick.Add(OnRuleBtnClick);
        window.m_worldInviteBtn.onClick.Add(OnWorldInviteBtnClick);
        window.m_sheTuanInviteBtn.onClick.Add(OnSheTuanInviteBtnClick);
        window.m_noticeTeammateBtn.onClick.Add(OnNoticeTeammatesBtnClick);
        window.m_fightStartBtn.onClick.Add(OnFightStartBtnClick);
        window.m_forbidBtn.onChanged.Add(OnForbidBtnClick);
        window.m_modelToucher.onClick.Add(OnClickModel);
        window.m_leaveBtn.onClick.Add(OnLeaveBtnClick);
    }

    public override void AddEventListener()
    {
        base.AddEventListener();

        GED.ED.addListener(EventID.OnSelectPetListItem, OnSelectPetListItem);
        GED.ED.addListener(EventID.OnResTeamFightChangePet, OnResTeamFightChangePet);
        GED.ED.addListener(EventID.OnResTeamFightForbibFastEnter, OnResTeamFightForbibFastEnter);
        GED.ED.addListener(EventID.OnResTeamFightStart, OnResTeamFightStart);
        GED.ED.addListener(EventID.OnResTeamFightEnd, OnResTeamFightEnd);
        GED.ED.addListener(EventID.OnResTeamFightTeammateChange, OnResTeamFightTeammateChange);
        GED.ED.addListener(EventID.OnResTeamFightNoticeSuccess, OnResTeamFightNoticeSuccess);
        GED.ED.addListener(EventID.OnResTeamFightInviteSuccess, OnResTeamFightInviteSuccess);
    }

    public override void RemoveEventListener()
    {
        base.RemoveEventListener();

        GED.ED.removeListener(EventID.OnSelectPetListItem, OnSelectPetListItem);
        GED.ED.removeListener(EventID.OnResTeamFightChangePet, OnResTeamFightChangePet);
        GED.ED.removeListener(EventID.OnResTeamFightForbibFastEnter, OnResTeamFightForbibFastEnter);
        GED.ED.removeListener(EventID.OnResTeamFightStart, OnResTeamFightStart);
        GED.ED.removeListener(EventID.OnResTeamFightEnd, OnResTeamFightEnd);
        GED.ED.removeListener(EventID.OnResTeamFightTeammateChange, OnResTeamFightTeammateChange);
        GED.ED.removeListener(EventID.OnResTeamFightNoticeSuccess, OnResTeamFightNoticeSuccess);
        GED.ED.removeListener(EventID.OnResTeamFightInviteSuccess, OnResTeamFightInviteSuccess);
    }
    public override void InitView()
    {
        base.InitView();

        actionInterval = new DoActionInterval();
        actionInterval.doAction(1,RefreshColdTime , null);
        window.m_bubbleGroup.alpha = 0;

        ShowEnterTip();
        InitModel();
        InitBaseInfo();
    }
    private void InitData()
    {
        ResTeamFightTeamInfo teamFightInfo = CloneTeamFightService.Singleton.fightTeamInfo;
        if (teamFightInfo != null)
        {
            petID = teamFightInfo.petId;
        }
    }

    private void ShowEnterTip()
    {
        if (Info.param == null)
            return;

        CloneTeamJoinType type = (CloneTeamJoinType)Info.param;
        int languageID = 0;
        switch (type)
        {
            case CloneTeamJoinType.CreatTeam:
                // 71803008   成功组建队伍
                languageID = 71803008;
                break;
            case CloneTeamJoinType.QuickJoin:
                // 71803010   成功加入讨伐队伍
                languageID = 71803010;
                break;
            default:
                break;
        }
        TipWindow.Singleton.ShowTip(languageID);
    }
    private void InitModel()
    {
        string modelName = UIUtils.GetPetStartModel(petID);
        GameObject model = this.LoadGo(modelName);
        model.transform.localPosition = new Vector3(0, 0, 500);
        model.transform.localEulerAngles = new Vector3(0, 180, 0);
        model.transform.localScale = new Vector3(100, 100, 100);

        window.m_modelPos.SetNativeObject(new GoWrapper(model));
        model.setLayer("UIActor");
    }
    private void InitBaseInfo()
    {
        t_petBean petBean = ConfigBean.GetBean<t_petBean, int>(petID);
        if (petBean != null)
        {
            window.m_nameLabel.text = string.Format("克隆人{0}", UIUtils.GetPetName(petBean));
        }
    }
    public override void RefreshView()
    {
        base.RefreshView();

        RefreshTeammateList();
        RefreshBtnState();
        RefreshTextInfo();
    }

    private void RefreshTeammateList()
    {
        ResTeamFightTeamInfo fightTeamInfo = CloneTeamFightService.Singleton.fightTeamInfo;
        if (fightTeamInfo != null)
        {
            window.m_teammateList.RemoveChildren(0, -1, true);
            CloneTeammateItem teammateItem = null;
            int count = CloneTeamFightService.Singleton.teamMaxRoleNum;
            for (int i = 0; i < count; i++)
            {
                teammateItem = CloneTeammateItem.CreateInstance();
                teammateItem.teamRoleInfo = CloneTeamFightService.Singleton.GetTeamFightRoleInfo(i);
                teammateItem.InitView();
                window.m_teammateList.AddChild(teammateItem);
            }
        }
    }

    private void RefreshBtnState()
    {
        bool isFighted = CloneTeamFightService.Singleton.IsFighted();
        bool isTeamFull = CloneTeamFightService.Singleton.IsTeamFull();
        bool isExistUnfinishedTeammate = CloneTeamFightService.Singleton.IsExistUnFinishedTeammate();
        bool isFinished = CloneTeamFightService.Singleton.IsFinishedFight();

        window.m_leaveBtn.visible = !isFighted;
        window.m_inviteGroup.visible = !isFinished;
        window.m_noticeTeammateBtn.visible = isFinished;
        window.m_fightStartBtn.visible = !isFinished;
        window.m_finishTipLabel.visible = isFinished;
    }

    private void RefreshTextInfo()
    {
        TeamFightRoleInfo fightRoleInfo = CloneTeamFightService.Singleton.GetCaptainRoleInfo();
        if (fightRoleInfo != null)
            window.m_teamName.text = string.Format("{0}的队伍", fightRoleInfo.roleName);

        fightRoleInfo = CloneTeamFightService.Singleton.GetPlayerFightRoleInfo();

        //71803001  队伍中{0}人完成讨伐，额外获得奖励{1}{2}
        //71803002  已有{0}人完成讨伐，额外获得奖励{1}{2}
        int finishedNum = CloneTeamFightService.Singleton.GetFinishedRoleNum();
        int languageID = 0;
        if (finishedNum < 3)
            languageID = 71803001;
        else
            languageID = 71803002;

        string baseStr = UIUtils.GetStrByLanguageID(languageID);
        string numStr = string.Format("[color=#00FF00]{0}[/color]", finishedNum);

        string itemIcon = "";
        t_petBean petBean = ConfigBean.GetBean<t_petBean, int>(petID);
        if (petBean != null)
            itemIcon = UIUtils.GetItemIcon(petBean.t_fragment_id);
        // 1803001  1803002  组队战第一，二个道具的数量
        int itemNum = 0;
        string[] numArr = null;
        t_globalBean globalBean = ConfigBean.GetBean<t_globalBean, int>(1803001);
        if(globalBean != null && !string.IsNullOrEmpty(globalBean.t_string_param))
        {
            numArr = globalBean.t_string_param.Split('+');
            itemNum = numArr.Length > finishedNum ? int.Parse(numArr[finishedNum]) : 0;
        }
        string item1Str = string.Format("<img src={0} width='20' height='20'/>[color=#00FF00]X{1}[/color]", itemIcon, itemNum);

        int item2ID = 0;
        globalBean = ConfigBean.GetBean<t_globalBean, int>(1803002);
        if (globalBean != null)
        {
            item2ID = globalBean.t_int_param;
            if(!string.IsNullOrEmpty(globalBean.t_string_param))
            {
                numArr = globalBean.t_string_param.Split('+');
                itemNum = numArr.Length > finishedNum ? int.Parse(numArr[finishedNum]) : 0;
            }
        }

        itemIcon =  UIUtils.GetItemIcon(item2ID);
        string item2Str = string.Format("<img src={0} width='20' height='20'/>[color=#00FF00]X{1}[/color]", itemIcon, itemNum);
        window.m_progressGetTipLabel.text = string.Format(baseStr, numStr, item1Str, item2Str);
    }

    IEnumerator ShowBubble()
    {
        // 点击模型后，显示5秒后消失
        window.m_bubbleGroup.visible = true;
        window.m_bubbleLabel.text = "ZZZZZZZZZzzzzzzzz";
        window.m_bubbleGroup.alpha = 0;
        tween = window.m_bubbleGroup.TweenFade(1, 0.5f);
        yield return new WaitForSeconds(5);
        coroutineID = -1;
        tween = window.m_bubbleGroup.TweenFade(0, 0.5f);
    }

    private void RefreshColdTime(object obj)
    {
        if (worldColdTime > 0)
            worldColdTime--;

        if (sheTuanColdTime > 0)
            sheTuanColdTime--;

        if (noticeColdTime > 0)
            noticeColdTime--;

    }

    #region 按钮事件 ***********************************************************************************************************
    private void OnRuleBtnClick()
    {
        // 打开规则界面
        WinMgr.Singleton.Open<CloneRuleWindow>(null, UILayer.Popup);
    }

    private void OnFightStartBtnClick()
    {
        bool isTeamFull = CloneTeamFightService.Singleton.IsTeamFull();
        if (isTeamFull)
        {
            // 战斗开始请求
            CloneTeamFightService.Singleton.ReqTeamFightStart();
        }
        else
        {
            AgainConfirmWindow.Singleton.ShowTip("队伍成员未满，是否进入战斗?", OnFightStart);
        }
    }

    private void OnSheTuanInviteBtnClick()
    {
        if (sheTuanColdTime == 0)
        {
            bool isTeamFull = CloneTeamFightService.Singleton.IsTeamFull();
            if (isTeamFull)
            {
                // 71803011    队伍已满员了，快去讨伐克隆人吧
                TipWindow.Singleton.ShowTip(71803011);
            }
            else
            {
                // 邀请请求
                CloneTeamFightService.Singleton.ReqTeamFightSendInviteMessage((int)ChatService.EChannelType.Guild);
            }
            sheTuanColdTime = coldTime;
        }
        else
            // 71803013 发送通知过于频繁会打扰到队友的，耐心等等看吧
            TipWindow.Singleton.ShowTip(71803013);
    }

    private void OnWorldInviteBtnClick()
    {
        if (worldColdTime == 0)
        {
            bool isTeamFull = CloneTeamFightService.Singleton.IsTeamFull();
            if (isTeamFull)
            {
                // 71803011    队伍已经满员了，快去讨伐克隆人吧
                TipWindow.Singleton.ShowTip(71803011);
            }
            else
            {
                // 邀请请求
                CloneTeamFightService.Singleton.ReqTeamFightSendInviteMessage((int)ChatService.EChannelType.World);
            }
            worldColdTime = coldTime;
        }
        else
            // 71803013 发送通知过于频繁会打扰到队友的，耐心等等看吧
            TipWindow.Singleton.ShowTip(71803013);
        
    }

    private void OnNoticeTeammatesBtnClick()
    {
        bool isExistUnfinishedTeamate = CloneTeamFightService.Singleton.IsExistUnFinishedTeammate();
        if (isExistUnfinishedTeamate)
        {
            if (noticeColdTime == 0)
            {
                // 通知队友
                CloneTeamFightService.Singleton.ReqTeamFightNotifyTeammates();
                noticeColdTime = coldTime;
            }
            else
                // 71803013 发送通知过于频繁会打扰到队友的，耐心等等看吧
                TipWindow.Singleton.ShowTip(71803013);
        }
        else
            TipWindow.Singleton.ShowTip("没有可通知的队友");
    }

    private void OnForbidBtnClick()
    {
        // 禁止快速加入
        CloneTeamFightService.Singleton.ReqTeamFightForbidFastEnter(window.m_forbidBtn.selected);
    }

    private void OnClickModel()
    {
        // 显示气泡
        if (coroutineID == -1)
        {
            coroutineID = CoroutineManager.Singleton.startCoroutine(ShowBubble());
        }
    }

    private void OnLeaveBtnClick()
    {
        bool isCaptain = CloneTeamFightService.Singleton.PlayerIsCaptain();
        if (isCaptain)
        {
            ResTeamFightTeamInfo teamFightInfo = CloneTeamFightService.Singleton.fightTeamInfo;
            if (teamFightInfo != null && teamFightInfo.teammates.Count == 1)
            {
                AgainConfirmWindow.Singleton.ShowTip("队伍内没有其他成员，若退出队伍，该队伍自动解散，是否退出?", OnLeaveTeamFight);
            }
            else
            {
                AgainConfirmWindow.Singleton.ShowTip("您是队长，退出队伍后队长将转让给其他队员，是否退出?", OnLeaveTeamFight);
            }
        }
        else
        {
            CloneTeamFightService.Singleton.ReqTeamFightLeaveTeam();
        }
    }
    #endregion;

    #region 回调事件 ***************************************************************************************************
    private void OnSelectPetListItem(GameEvent evt)
    {
        int petID = (int)evt.Data;
        CloneTeamFightService.Singleton.ReqTeamFightChangePet(petID);
    }

    private void OnResTeamFightChangePet(GameEvent evt)
    {
        RefreshView();
    }

    private void OnResTeamFightForbibFastEnter(GameEvent evt)
    {
        //window.m_forbidBtn.selected = (bool)evt.Data;
    }

    private void OnResTeamFightStart(GameEvent evt)
    {
        // 战斗开始，进入战斗
        CloneTeamFightService.Singleton.ReqTeamFightEnd(1);
    }

    private void OnResTeamFightEnd(GameEvent evt)
    {
        // 打开胜利界面，开宝箱
        WinMgr.Singleton.Open<CloneWinWindow>(null, UILayer.Popup);
        RefreshView();
    }
    /// <summary>
    /// 战斗开始
    /// </summary>
    private void OnFightStart()
    {
        FightService.Singleton.ReqFight(EFightType.CloneDungeon, -1);
        //CloneTeamFightService.Singleton.ReqTeamFightStart();
    }
    /// <summary>
    /// 离开战斗
    /// </summary>
    private void OnLeaveTeamFight()
    {
        CloneTeamFightService.Singleton.ReqTeamFightLeaveTeam();
    }

    private void OnResTeamFightTeammateChange(GameEvent evt)
    {
        OnCloseBtn();
    }

    private void OnResTeamFightNoticeSuccess(GameEvent evt)
    {
        TipWindow.Singleton.ShowTip("召唤队友发送成功");
    }

    private void OnResTeamFightInviteSuccess(GameEvent evt)
    {
        // 71803009    发送邀请成功，可以在聊天内看到邀请信息
        TipWindow.Singleton.ShowTip(71803009);
    }

    #endregion
    #region 数据处理 ***************************************************************************************************

    #endregion;
    protected override void OnCloseBtn()
    {
        base.OnCloseBtn();
    }

    protected override void OnClose()
    {
        base.OnClose();
        RestoreWndMgr.Singleton.SaveWndData<CloneTeamWindow>(Info, UILayer.Popup);
    }
}
