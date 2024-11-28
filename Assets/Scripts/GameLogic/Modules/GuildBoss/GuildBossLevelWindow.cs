using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_GuildBoss;
using Message.GuildBoss;
using Data.Beans;
using FairyGUI;
using DG.Tweening;

public class GuildBossLevelWindow : BaseWindow {

    UI_GuildBossLevelWindow window;
    ResBossInfo bossInfo;
    private bool isClick;
    UIResPack resPack;
    Tween tween;
    public override void OnOpen()
    {
        base.OnOpen();

        window = getUiWindow<UI_GuildBossLevelWindow>();
        bossInfo = Info.param as ResBossInfo;

        BindEvent();
        InitView();
        RefreshView();
    }

    public override void AddEventListener()
    {
        base.AddEventListener();

        GED.ED.addListener(EventID.OnResFightStart, OnResFightStart);
        GED.ED.addListener(EventID.OnResGuildBossGetReward, OnResGuildBossGetReward);
        GED.ED.addListener(EventID.OnResDamageRank, OnResDamageRank);
        GED.ED.addListener(EventID.OnResBossInfo, OnResBossInfo);
    }

    public override void RemoveEventListener()
    {
        base.RemoveEventListener();

        GED.ED.removeListener(EventID.OnResFightStart, OnResFightStart);
        GED.ED.removeListener(EventID.OnResGuildBossGetReward, OnResGuildBossGetReward);
        GED.ED.removeListener(EventID.OnResDamageRank, OnResDamageRank);
        GED.ED.removeListener(EventID.OnResBossInfo, OnResBossInfo);
    }

    private void BindEvent()
    {
        window.m_startFightBtn.onClick.Add(OnStartFightBtnClick);
        window.m_receivedBtn.onClick.Add(OnReceiveBtnClick);
        window.m_alreadyReceivedBtn.onClick.Add(OnAlreadyReceivedBtnClick);
        window.m_backBtn.onClick.Add(OnCloseBtn);
        window.m_damageRnakBtn.onClick.Add(OnDamageRankBtnClick);
    }

    public override void InitView()
    {
        base.InitView();
        InitBossModel();
        InitRewardList();
        ShowBubble();
    }
    /// <summary>
    /// 初始化前3社团的信息
    /// </summary>
    private void RefreshFrontGuild()
    {
        int count = bossInfo.rank.Count;
        FrontGuildItem guildItem = null;
        window.m_frontGuildList.RemoveChildren(0, -1, true);
        for (int i = 0; i < count; i++)
        {
            guildItem = FrontGuildItem.CreateInstance();
            guildItem.itemInfo = bossInfo.rank[i];
            guildItem.InitView();
            window.m_frontGuildList.AddChild(guildItem);
        }
    }
    /// <summary>
    /// 初始化特殊伤害奖励
    /// </summary>
    private void RefreshSpecialDamageReward()
    {
        int hp = (int)(GuildBossService.Singleton.MAX_PROGRESS - bossInfo.hp * 0.01f);
        window.m_bossBloodProgress.value = hp;
        window.m_bossBloodProgress.max = GuildBossService.Singleton.MAX_PROGRESS;

        // 1603003 公会副本boss特殊伤害奖励
        int itemID = 0;
        int itemNum = 0;
        t_globalBean globalBean = ConfigBean.GetBean<t_globalBean, int>(1603003);
        if (globalBean != null && !string.IsNullOrEmpty(globalBean.t_string_param))
        {
            string[] itemInfo = globalBean.t_string_param.Split('+');
            if (itemInfo.Length == 2)
            {
                itemID = int.Parse(itemInfo[0]);
                itemNum = int.Parse(itemInfo[1]);
            }
        }

        SetSpecialItem(window.m_specialDamgeItem1 as CommonItem, itemID, itemNum, hp < 20);
        SetSpecialItem(window.m_specialDamgeItem2 as CommonItem, itemID, itemNum, hp < 40);
        SetSpecialItem(window.m_specialDamgeItem3 as CommonItem, itemID, itemNum, hp < 60);
        SetSpecialItem(window.m_specialDamgeItem4 as CommonItem, itemID, itemNum, hp < 80);
        SetSpecialItem(window.m_specialDamgeItem5 as CommonItem, itemID, itemNum, hp < 100);
    }

    private void SetSpecialItem(CommonItem item, int itemID, int itemNum, bool isGrayed = true)
    {
        item.itemId = itemID;
        item.itemNum = itemNum;
        item.isShowNum = true;
        item.grayed = isGrayed;
        item.AddPopupEvent();
        item.RefreshView();
    }
    /// <summary>
    /// 初始化基础信息
    /// </summary>
    private void RefreshBaseInfo()
    {
        int maxTimes = GuildBossService.Singleton.GetMaxFightTimes();
        window.m_timesLabel.text = string.Format("{0}/{1}", maxTimes -bossInfo.progress, maxTimes);

        if (bossInfo.hp <= 0)
        {
            window.m_bossRemainBloodGroup.visible = false;
            window.m_passGroup.visible = true;
        }
        else
        {
            window.m_bossRemainBloodGroup.visible = true;
            window.m_passGroup.visible = false;
            window.m_bloodReaminLabel.text = string.Format("{0}%", bossInfo.hp * 0.01f);
            window.m_bloodRemainProgress.value = bossInfo.hp * 0.01f;
            window.m_bloodRemainProgress.max = GuildBossService.Singleton.MAX_PROGRESS;
        }

        t_guild_bossBean guildBossBean = ConfigBean.GetBean<t_guild_bossBean, int>(bossInfo.id);
        if (guildBossBean != null)
        {
            t_monster_boosBean monsterBean = ConfigBean.GetBean<t_monster_boosBean, int>(guildBossBean.t_pet);
            if (monsterBean != null)
            {
                window.m_nameLabel.text = monsterBean.t_name;
            }

            window.m_specialDesLabel.text = guildBossBean.t_special_id;
        }
    }

    private void InitBossModel()
    {
        t_guild_bossBean guildBossBean = ConfigBean.GetBean<t_guild_bossBean, int>(bossInfo.id);
        if (guildBossBean != null)
        {
            resPack = new UIResPack(this);
            GoWrapper wrapper = new GoWrapper();
            window.m_modelPos.SetNativeObject(wrapper);
            ActorUI actor = resPack.NewActorUI(guildBossBean.t_pet, ActorType.Boss, wrapper);
            actor.SetTransform( new Vector3(0, 0, 500), 150, new Vector3(0, 180, 0));
        }
    }
    private void InitRewardList()
    {
        t_guild_bossBean guildBossBean = ConfigBean.GetBean<t_guild_bossBean, int>(bossInfo.id);
        if (guildBossBean != null && !string.IsNullOrEmpty(guildBossBean.t_first_award))
        {
            string[] awardArr = guildBossBean.t_first_award.Split(';');
            int count = awardArr.Length;
            CommonItem item = null;
            string[] itemArr = null;
            for (int i = 0; i < count; i++)
            {
                itemArr = awardArr[i].Split('+');
                if (itemArr.Length == 2)
                {
                    item = CommonItem.CreateInstance();
                    item.itemId = int.Parse(itemArr[0]);
                    item.itemNum = int.Parse(itemArr[1]);
                    item.isShowNum = true;
                    item.AddPopupEvent();
                    item.RefreshView();
                    item.scale = new Vector2(0.6f, 0.6f);

                    window.m_firstRewardList.AddChild(item);
                }
                
            }
            if(item != null)
                window.m_firstRewardList.columnGap = -(int)(item.width * 0.4f) + 20;
        }
    }

    private void ShowBubble()
    {
        window.m_bubleGroup.scale = Vector2.zero;
        tween = window.m_bubleGroup.TweenScale(Vector2.one, 0.5f);
    }

    private void RefreshBtnState()
    {
        if (IsReceived())
        {
            window.m_receivedBtn.visible = true;
            window.m_receivedBtn.grayed = true;
            window.m_alreadyReceivedBtn.visible = false;
        }
        else
        {
            bool isCanReceived = IsCanReceive();
            window.m_receivedBtn.visible = true;
            window.m_alreadyReceivedBtn.visible = false;
            window.m_receivedBtn.grayed = !isCanReceived;
        }

        window.m_startFightBtn.grayed = IsPass();
    }

    public override void RefreshView()
    {
        base.RefreshView();

        RefreshBtnState();
        RefreshFrontGuild();
        RefreshSpecialDamageReward();
        RefreshBaseInfo();
    }

    #region 按钮事件  ******************************************************************************************************

    private void OnStartFightBtnClick()
    {

        GuildBossService.Singleton.ReqFightStart(bossInfo.id);
    }

    private void OnReceiveBtnClick()
    {
        if (IsCanReceive())
        {
            GuildBossService.Singleton.ReqGetFirstPassAward(bossInfo.id);
        }
        else
        {
            TipWindow.Singleton.ShowTip("需完美通关");
        }
    }

    private void OnAlreadyReceivedBtnClick()
    {
        // 提示
        TipWindow.Singleton.ShowTip("该奖励已领取");
    }

    private void OnDamageRankBtnClick()
    {
        GuildBossService.Singleton.ReqDamageRank(bossInfo.id, 0);
        isClick = true;
    }

    #endregion

    #region 消息回调  **********************************************************************************************************
    private void OnResFightStart(GameEvent evt)
    {
        // 开始战斗
        GuildBossService.Singleton.ReqFightEnd(55555, bossInfo.id);
    }

    private void OnResGuildBossGetReward(GameEvent evt)
    {
        if (isClick)
        {
            List<Message.Bag.ItemInfo> itemList = UltemateTrainService.Singleton.TransformIntVsIntToItemInfo(evt.Data as List<Message.Challenge.IntVsInt>);
            ThreeParam<bool, List<Message.Bag.ItemInfo>, string> param = new ThreeParam<bool, List<Message.Bag.ItemInfo>, string>();
            param.value1 = false;
            param.value2 = itemList;
            WinMgr.Singleton.Open<BoxReceiveWidow>(WinInfo.Create(false, null, false, param), UILayer.Popup);

            RefreshView();
            isClick = false;
        }
    }

    private void OnResDamageRank(GameEvent evt)
    {
        TwoParam<int, List<DamageRankItem>> param = new TwoParam<int, List<DamageRankItem>>();
        param.value1 = bossInfo.id;
        param.value2 = evt.Data as List<DamageRankItem>;
        WinMgr.Singleton.Open<GuildBossDamageRankWindow>(WinInfo.Create(false, null, false, param), UILayer.Popup);
    }

    private void OnResBossInfo(GameEvent evt)
    {
        bossInfo = evt.Data as ResBossInfo;
        RefreshView();
    }
    #endregion;

    #region 数据处理 ****************************************************************************************************
    private bool IsCanReceive()
    {
        ResGuildDungeonInfo guildBossInfo = GuildBossService.Singleton.guildBossInfo;
        if (guildBossInfo != null)
        {
            int count = guildBossInfo.canGetRewardBossIds.Count;
            for (int i = 0; i < count; i++)
            {
                if (bossInfo.id == guildBossInfo.canGetRewardBossIds[i])
                {
                    return true;
                }
            }
        }

        return false;
    }
    /// <summary>
    /// 是否领取过
    /// </summary>
    /// <returns></returns>
    private bool IsReceived()
    {
        if (IsCanReceive() == false)
        {
            return IsPass();
        }

        return false;
    }
    /// <summary>
    /// 是否已通关
    /// </summary>
    /// <returns></returns>
    private bool IsPass()
    {
        ResGuildDungeonInfo guildBossInfo = GuildBossService.Singleton.guildBossInfo;
        if (guildBossInfo != null)
        {
            if (bossInfo.id < guildBossInfo.bossInfo.id)
            {
                return true;
            }

            if (bossInfo.id == guildBossInfo.bossInfo.id && bossInfo.hp <= 0)
            {
                return true;
            }
        }

        return false;
    }
    #endregion
    protected override void OnCloseBtn()
    {
        if (resPack != null)
            resPack.ReleaseAllRes();
        if (tween != null && tween.IsActive())
            tween.Kill();
        tween = null;

        GuildBossService.Singleton.ReqGuildDungeonInfo();

        base.OnCloseBtn();
    }
}
