using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_Activity;

public class TiaoZhanWindow : BaseWindow {

    UI_TiaoZhanWindow window;

    public override void OnOpen()
    {
        base.OnOpen();

        window = getUiWindow<UI_TiaoZhanWindow>();
        (window.m_commonTop as UI_Common.UI_commonTop).m_closeBtn.onClick.Add(OnCloseBtn);

        RestoreWndMgr.Singleton.ClearData();
        InitView();
        PlayOpenEffect();
    }

    public override void PlayOpenEffect()
    {
        base.PlayOpenEffect();

        (window.m_commonTop as UI_Common.UI_commonTop).m_anim.Play();
        ShowCardAnim();
    }

    public override void InitView()
    {
        base.InitView();

        (window.m_commonTop as UI_Common.UI_commonTop).m_title.text = "挑战";
        InitActivityCard();
    }

    private void InitActivityCard()
    {
        List<int> activityCardList = ChallegeService.Singleton.ChallengeList;

        window.m_cardList.RemoveChildren(0, -1, true);
        int count = activityCardList.Count;
        ActivityCardItem cardItem = null;
        int cardType = (int)ChallengeType.ZhongJiShiLian;
        // 当下标为偶数时的位置偏移
        float offsetH = 35;
        for (int i = 0; i < count; i++)
        {
            cardItem = ActivityCardItem.CreateInstance();
            cardItem.type = (ChallengeType)activityCardList[i];
            cardItem.type = (ChallengeType)cardType;
            cardItem.Init(this);
            window.m_cardList.AddChild(cardItem);
            cardItem.y += i % 2 == 0 ? offsetH : 0;
            cardType++;
        }
    }

    private void ShowCardAnim()
    {
        int count = window.m_cardList.numItems;
        ActivityCardItem cardItem = null;
        float delayInterval = 0.1f;
        for (int i = 0; i < count; i++)
        {
            cardItem = window.m_cardList.GetChildAt(i) as ActivityCardItem;
            cardItem.m_anim.Play(1, delayInterval * i, null);
        }
    }

    protected override void OnCloseBtn()
    {
        if (ChallegeService.Singleton.window == this)
            ChallegeService.Singleton.window = null;

        base.OnCloseBtn();
    }
}
