using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_GuildBoss;
using UI_Common;
using Message.GuildBoss;
using Message.Guild;
using Data.Beans;

public class GuildBossMianWindow : BaseWindow {

    UI_GuildBossMianWindow window;
    BuZhenColumn teamColumn;
    public bool isClick = false;
    private float guildBossItemWidth;
    public override void OnOpen()
    {
        base.OnOpen();
        RestoreWndMgr.Singleton.ClearData();
        window = getUiWindow<UI_GuildBossMianWindow>();

        BindEvent();
        InitView();
        RefreshView();
    }
    private void BindEvent()
    {
        window.m_backBtn.onClick.Add(OnCloseBtn);
        window.m_rewardBtn.onClick.Add(OnRewardBtnClick);
        window.m_keyReceiveBtn.onClick.Add(OnKeyReceiveBtnClcik);
        window.m_memberProgressBtn.onClick.Add(OnMemberProgressBtnClick);
        window.m_distributeBtn.onClick.Add(OnDistributeBtnClick);
        window.m_ruleBtn.onClick.Add(OnRuleBtnClick);
        window.m_switchLeftBtn.onClick.Add(OnSwitchLeftBtnClick);
        window.m_switchRightBtn.onClick.Add(OnSwitchRightBtnClick);
    }

    public override void AddEventListener()
    {
        base.AddEventListener();

        GED.ED.addListener(EventID.OnResAllotRecordInfo, OnResAllotRecordInfo);
        GED.ED.addListener(EventID.OnResProgressInfo, OnResProgressInfo);
        GED.ED.addListener(EventID.OnResGuildPassRankInfo, OnResGuildPassRankInfo);
        GED.ED.addListener(EventID.OnResBossInfo, OnResBossInfo);
        GED.ED.addListener(EventID.OnResGuildDungeonInfo, OnResGuildDungeonInfo);
        GED.ED.addListener(EventID.OnResGuildBossGetReward, OnResGuildBossGetReward);
    }

    public override void RemoveEventListener()
    {
        base.RemoveEventListener();

        GED.ED.removeListener(EventID.OnResAllotRecordInfo, OnResAllotRecordInfo);
        GED.ED.removeListener(EventID.OnResProgressInfo, OnResProgressInfo);
        GED.ED.removeListener(EventID.OnResGuildPassRankInfo, OnResGuildPassRankInfo);
        GED.ED.removeListener(EventID.OnResBossInfo, OnResBossInfo);
        GED.ED.removeListener(EventID.OnResGuildDungeonInfo, OnResGuildDungeonInfo);
        GED.ED.removeListener(EventID.OnResGuildBossGetReward, OnResGuildBossGetReward);
    }
    public override void InitView()
    {
        base.InitView();

        PetService.Singleton.zhenRongType = ZhenRongType.GuildBoss;
        teamColumn = new BuZhenColumn((UI_buZhenColumn)window.m_buZhenColumn);
        guildBossItemWidth = window.m_guildBossList.width * 0.25f;
        window.m_guildBossList.scrollPane.bouncebackEffect = false;
        window.m_guildBossList.scrollPane.onScroll.Add(OnScrollPanelScroll);
        window.m_guildBossList.scrollPane.onScrollEnd.Add(OnScrollPanelScrollEnd);

        InitGuildBossList();
    }

    private void InitGuildBossList()
    {
        ResGuildDungeonInfo guildBossInfo = GuildBossService.Singleton.guildBossInfo;
        if (guildBossInfo != null)
        {
            window.m_guildBossList.RemoveChildren(0, -1, true);
            int cout = guildBossInfo.bossInfo.id + 2;
            GuildBossItem bossItem = null;
            for (int i = 0; i < cout; i++)
            {
                bossItem = GuildBossItem.CreateInstance();
                bossItem.bossID = i + 1;
                bossItem.InitView(this);
                bossItem.width = guildBossItemWidth;

                window.m_guildBossList.AddChild(bossItem);
            }
            window.m_guildBossList.columnGap = 0;
        }
    }

    public override void RefreshView()
    {
        base.RefreshView();

        RefreshGuildSportsView();
        RefreshGuildBossList();
        RefreshKeyReceiveBtnState();
        RefreshSwitchBtnShow();
    }

    private void RefreshTeamColumnView()
    {
        teamColumn.RefreshView();
    }
    /// <summary>
    /// 刷新公会竞技的View
    /// </summary>
    private void RefreshGuildSportsView()
    {
        // 刷新本公会的信息
        GuildInfo selfGuildInfo = GuildService.Singleton.GetGuildInfo();
        window.m_selfGuildName.text = selfGuildInfo.name;
        ResGuildDungeonInfo guildBossInfo = GuildBossService.Singleton.guildBossInfo;
        window.m_selfBloodProgress.value = guildBossInfo.bossInfo.hp * 0.01f;
        window.m_selfBloodProgress.max = GuildBossService.Singleton.MAX_PROGRESS;
        window.m_selfBBProgressText.text = string.Format("{0}%", guildBossInfo.bossInfo.hp * 0.01f);

        // 刷新对手公会的信息
        RivalInfo opponentInfo = guildBossInfo.bossInfo.rivalInfo;
        if (opponentInfo == null)
        {
            window.m_opponentBBProgressText.visible = false;
            window.m_noneOpponentTip.visible = true;
            window.m_opponentBloodProgress.value = 0;
            window.m_opponentBBProgressText.text = "";
        }
        else
        {
            window.m_noneOpponentTip.visible = false;
            window.m_opponentGuildName.text = opponentInfo.name;
            window.m_opponentBloodProgress.value = opponentInfo.hp * 0.01f;
            window.m_opponentBloodProgress.max = GuildBossService.Singleton.MAX_PROGRESS;
            window.m_opponentBBProgressText.text = string.Format("{0}%", opponentInfo.hp * 0.01f);
        }

        // 刷新boss信息
        t_guild_bossBean guildBossBean = ConfigBean.GetBean<t_guild_bossBean, int>(guildBossInfo.bossInfo.id);
        if (guildBossBean != null)
        {
            //UIGloader.SetUrl(window.m_bossIconLoader, UIUtils.GetPetStartIcon(guildBossBean.t_pet, GuildBossService.Singleton.guildBossDefaultStar));
            t_monster_boosBean petBean = ConfigBean.GetBean<t_monster_boosBean, int>(guildBossBean.t_pet);
            if (petBean != null)
            {
                window.m_fightBossName.text = string.Format("本次竞技-{0}", petBean.t_name);
            }
        }
    }
    /// <summary>
    /// 刷新公会boss列表
    /// </summary>
    private void RefreshGuildBossList()
    {
        int oldCount = window.m_guildBossList.numItems;
        ResGuildDungeonInfo guildBossInfo = GuildBossService.Singleton.guildBossInfo;
        if (guildBossInfo != null)
        {
            int newCount = guildBossInfo.bossInfo.id;
            GuildBossItem bossItem = null;
            if (newCount == oldCount - 2)
            {
                // 没开新的boss
                bossItem = window.m_guildBossList.GetChildAt(newCount - 1) as GuildBossItem;
                bossItem.RefreshView();
            }
            else
            {
                // 开了新的boss, 刷3加1
                for (int i = newCount - 2; i < oldCount; i++)
                {
                    bossItem = window.m_guildBossList.GetChildAt(i) as GuildBossItem;
                    bossItem.RefreshView();
                }

                bossItem = GuildBossItem.CreateInstance();
                bossItem.bossID = oldCount + 1;
                bossItem.InitView(this);
                window.m_guildBossList.AddChild(bossItem);
            }
        }
    }
    /// <summary>
    /// 刷新左右切换按钮的显示状态
    /// </summary>
    private void RefreshSwitchBtnShow()
    {
        window.m_switchLeftBtn.visible = !IsScrollMostLeft();
        window.m_switchRightBtn.visible = !IsScrollMostRight();
    }
    /// <summary>
    /// 一键领取按钮的显示
    /// </summary>
    private void RefreshKeyReceiveBtnState()
    {
        ResGuildDungeonInfo guildBossInfo = GuildBossService.Singleton.guildBossInfo;
        if (guildBossInfo != null)
        {
            window.m_keyReceiveGroup.visible = guildBossInfo.canGetRewardBossIds.Count > 0;
        }
    }

    #region 按钮点击事件 ******************************************************************************************************************************
    /// <summary>
    /// 奖励按钮
    /// </summary>
    private void OnRewardBtnClick()
    {
        WinMgr.Singleton.Open<GuildBossRewradWindow>(null, UILayer.Popup);
    }
    /// <summary>
    /// 分配按钮点击
    /// </summary>
    private void OnDistributeBtnClick()
    {
        isClick = true;
        GuildBossService.Singleton.ReqAllotRecordInfo();
    }
    /// <summary>
    /// 规则按钮
    /// </summary>
    private void OnRuleBtnClick()
    {
        WinMgr.Singleton.Open<GuildBossRuleWindow>(null, UILayer.Popup);
    }
    /// <summary>
    /// 成员进度按钮
    /// </summary>
    private void OnMemberProgressBtnClick()
    {
        isClick = true;
        GuildBossService.Singleton.ReqProgressInfo(0);
    }
    /// <summary>
    /// 一键领取按钮
    /// </summary>
    private void OnKeyReceiveBtnClcik()
    {
        WinMgr.Singleton.Open<GuildBossPassRewardWindow>(null, UILayer.Popup);
    }
    /// <summary>
    /// 左边按钮
    /// </summary>
    private void OnSwitchLeftBtnClick()
    {
        window.m_guildBossList.scrollPane.ScrollLeft();
    }
    /// <summary>
    /// 右边按钮
    /// </summary>
    private void OnSwitchRightBtnClick()
    {
        if (!IsScrollMostRight())
        {
            window.m_guildBossList.scrollPane.ScrollRight();
        }
    }
    /// <summary>
    /// boss 列表滑动回调
    /// </summary>
    private void OnScrollPanelScroll()
    {
        // 背景跟着一起滑动
        window.m_guildBossBgList.scrollPane.posX = window.m_guildBossList.scrollPane.posX;
        RefreshSwitchBtnShow();
    }
    /// <summary>
    /// boss 列表滑动结束回调
    /// </summary>
    private void OnScrollPanelScrollEnd()
    {
        
    }
    #endregion

    #region 消息回调 **************************************************************************************************************************

    private void OnResAllotRecordInfo(GameEvent evt)
    {
        if (isClick)
        {
            WinMgr.Singleton.Open<GuildBossDistributeWindow>(WinInfo.Create(false, null, false, evt.Data), UILayer.Popup);
            isClick = false;
        }
    }

    private void OnResProgressInfo(GameEvent evt)
    {
        if (isClick)
        {
            isClick = false;
            WinMgr.Singleton.Open<GuildMemberProgressWindow>(WinInfo.Create(false, null, false, evt.Data), UILayer.Popup);
        }
    }

    private void OnResGuildPassRankInfo(GameEvent evt)
    {
        if (isClick)
        {
            isClick = false;
            WinMgr.Singleton.Open<GuildBossPassRankWindow>(WinInfo.Create(false, null, false, evt.Data), UILayer.Popup);
        }
    }

    private void OnResBossInfo(GameEvent evt)
    {
        if (isClick)
        {
            isClick = false;
            WinMgr.Singleton.Open<GuildBossLevelWindow>(WinInfo.Create(false, null, false, evt.Data), UILayer.Popup);
        }
    }

    private void OnResGuildDungeonInfo(GameEvent evt)
    {
        RefreshView();
    }
    private void OnResGuildBossGetReward(GameEvent evt)
    {
        RefreshKeyReceiveBtnState();
    }
    #endregion

    #region  数据处理 ***********************************************************************************************************************************
    /// <summary>
    /// 是否滚动到最右边了
    /// </summary>
    /// <returns></returns>
    private bool IsScrollMostRight()
    {
        return window.m_guildBossList.scrollPane.isRightMost;
    }

    private bool IsScrollMostLeft()
    {
        return window.m_guildBossList.scrollPane.posX == 0;
    }

    #endregion

    protected override void OnCloseBtn()
    {
        base.OnCloseBtn();
    }

    protected override void OnClose()
    {
        RestoreWndMgr.Singleton.SaveWndData<GuildBossMianWindow>(Info, UILayer.Popup);
        base.OnClose();
    }
}
