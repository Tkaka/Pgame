using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_GuildBoss;
using Message.GuildBoss;
using Message.Challenge;

public class GuildBossPassRewardWindow : BaseWindow {
    UI_GuildBossPassRewardWindow window;

    public override void OnOpen()
    {
        base.OnOpen();

        window = getUiWindow<UI_GuildBossPassRewardWindow>();

        InitView();
    }

    public override void AddEventListener()
    {
        base.AddEventListener();

        GED.ED.addListener(EventID.OnResGuildBossGetReward, OnResGuildBossGetReward);
    }

    public override void RemoveEventListener()
    {
        base.RemoveEventListener();

        GED.ED.removeListener(EventID.OnResGuildBossGetReward, OnResGuildBossGetReward);
    }

    public override void InitView()
    {
        base.InitView();

        window.m_mask.onClick.Add(OnCloseBtn);
        window.m_closeBtn.onClick.Add(OnCloseBtn);
        window.m_receiveBtn.onClick.Add(OnReceivedBtnClick);

        InitRewardList();
    }

    private void InitRewardList()
    {
        ResGuildDungeonInfo guildBossInfo = GuildBossService.Singleton.guildBossInfo;
        if (guildBossInfo != null)
        {
            int count = guildBossInfo.canGetRewardBossIds.Count;
            PassRewardItem rewardItem = null;
            for (int i = 0; i < count; i++)
            {
                rewardItem = PassRewardItem.CreateInstance();
                rewardItem.bossID = guildBossInfo.canGetRewardBossIds[i];
                rewardItem.InitView();
                window.m_passBossList.AddChild(rewardItem);
            }
        }
    }

    private void OnReceivedBtnClick()
    {
        GuildBossService.Singleton.ReqOneKeyGetFirstPassAward();
    }

    private void OnResGuildBossGetReward(GameEvent evt)
    {
        List<Message.Bag.ItemInfo> itemList = UltemateTrainService.Singleton.TransformIntVsIntToItemInfo(evt.Data as List<IntVsInt>);
        ThreeParam<bool, List<Message.Bag.ItemInfo>, string> param = new ThreeParam<bool, List<Message.Bag.ItemInfo>, string>();
        param.value1 = false;
        param.value2 = itemList;
        WinMgr.Singleton.Open<BoxReceiveWidow>(WinInfo.Create(false, null, false, param), UILayer.Popup);
        OnCloseBtn();
    }
    protected override void OnCloseBtn()
    {
        base.OnCloseBtn();
    }

}
