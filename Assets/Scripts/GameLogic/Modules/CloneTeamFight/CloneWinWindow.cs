using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_CloneTeamFight;
using Message.Challenge;
using DG.Tweening;

public class CloneWinWindow : BaseWindow {

    UI_CloneWinWindow window;
    List<Message.Bag.ItemInfo> itemList = new List<Message.Bag.ItemInfo>();
    List<CloneWinBoxItem> boxList = new List<CloneWinBoxItem>();
    /// <summary>
    /// 打开宝箱的次数
    /// </summary>
    public int count = 0;

    public long coroutineID = -1;
    Tween tween;

    public override void OnOpen()
    {
        base.OnOpen();

        window = getUiWindow<UI_CloneWinWindow>();
        InitView();
        RefreshView();
    }

    public override void AddEventListener()
    {
        base.AddEventListener();

        GED.ED.addListener(EventID.OnResTeamFightOpenBox, OnResTeamFightOpenBox);
    }

    public override void RemoveEventListener()
    {
        base.RemoveEventListener();

        GED.ED.removeListener(EventID.OnResTeamFightOpenBox, OnResTeamFightOpenBox);
    }

    public override void InitView()
    {
        base.InitView();

        window.m_backBtn.onClick.Add(OnCloseBtn);
        window.m_rewardGroup.visible = false;
        window.m_rewardItemList.RemoveChildren(0, -1, true);
        InitBoxList();
    }

    private void InitBoxList()
    {
        int count = window.m_boxList.numItems;
        CloneWinBoxItem boxItem = null;
        for (int i = 0; i < count; i++)
        {
            boxItem = new CloneWinBoxItem((UI_cloneWinBoxItem)window.m_boxList.GetChildAt(i), this, i);
            boxList.Add(boxItem);
        }
    }

    public override void RefreshView()
    {
        base.RefreshView();

        window.m_backBtn.visible = count > 0;

        RefreshBoxList();
        RefreshRewardList();
    }

    private void RefreshBoxList()
    {
        int count = boxList.Count;
        CloneWinBoxItem boxItem;
        for (int i = 0; i < count; i++)
        {
            boxItem = boxList[i];
            boxItem.RefreshView();
        }
    }

    private void RefreshRewardList()
    {
        int count = itemList.Count;
        if (count != 0)
        {
            coroutineID = CoroutineManager.Singleton.startCoroutine(ShowRewardList()); 
        }
    }

    IEnumerator ShowRewardList()
    {
        window.m_rewardGroup.visible = true;
        int count = itemList.Count;
        CommonItem item = null;
        Message.Bag.ItemInfo itemInfo = null;
        for (int i = 0; i < count; i++)
        {
            itemInfo = itemList[i];
            item = CommonItem.CreateInstance();
            item.itemId = itemInfo.id;
            item.itemNum = itemInfo.num;
            item.isShowNum = true;
            item.AddPopupEvent();
            item.RefreshView();
            window.m_rewardItemList.AddChild(item);

            if (tween != null && tween.IsActive())
                tween.Kill();
            item.alpha = 0;
            tween = item.TweenFade(1, 0.3f);
            yield return new WaitForSeconds(0.3f);
        }

        coroutineID = -1;
    }

    private void OnResTeamFightOpenBox(GameEvent evt)
    {
        ResTeamFightOpenBox msg = evt.Data as ResTeamFightOpenBox;

        List<Message.Bag.ItemInfo> rewards = UltemateTrainService.Singleton.TransformIntVsIntToItemInfo(msg.awards);
        itemList.Clear();
        itemList.AddRange(rewards);
        count++;
        RefreshView();
    }

    protected override void OnCloseBtn()
    {
        if (coroutineID != -1)
            CoroutineManager.Singleton.stopCoroutine(coroutineID);
        if (tween != null && tween.IsActive())
            tween.Kill();
        tween = null;

        itemList.Clear();
        itemList = null;

        boxList.Clear();
        boxList = null;

        base.OnCloseBtn();
    }

    protected override void OnClose()
    {
        base.OnClose();
        BattleService.Singleton.QuitBattle();
    }
}
