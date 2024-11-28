using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_UltemateTrain;
using Message.Challenge;

public class JumpBoxRewardPanel : TabPage {

    UI_jumpBoxRewardPanel panel;

    private long coroutineID;
    public JumpBoxRewardPanel(UI_jumpBoxRewardPanel panel)
    {
        this.panel = panel;
        InitView();
        RefreshView();
    }

    private void InitView()
    {
        ResTrialInfo trainInfo = UltemateTrainService.Singleton.trainInfo;
        if (trainInfo != null)
        {
            panel.m_tipLabel.text = string.Format("过了{0}层", trainInfo.skipFloor);
        }
    }

    public override void OnClose()
    {
        if (coroutineID != -1)
            CoroutineManager.Singleton.stopCoroutine(coroutineID);

        panel = null;
    }

    public override void OnHide()
    {
        panel.visible = false;
    }

    public override void OnShow()
    {
        panel.visible = true;
    }

    public override void RefreshView(bool isNet = false)
    {
        if (coroutineID != -1)
            CoroutineManager.Singleton.stopCoroutine(coroutineID);
        coroutineID = CoroutineManager.Singleton.startCoroutine(ShowRewardList());
    }

    private IEnumerator ShowRewardList()
    {
        ResTrialSkip trainSkipInfo = UltemateTrainService.Singleton.trainSkipInfo;
        if (trainSkipInfo != null)
        {
            int count = trainSkipInfo.rewards.Count;
            CommonItem commonItem;
            IntVsInt itemInfo;
            panel.m_rewardItemList.RemoveChildren(0, -1, true);
            for (int i = 0; i < count; i++)
            {
                itemInfo = trainSkipInfo.rewards[i];
                commonItem = CommonItem.CreateInstance();
                commonItem.itemId = itemInfo.int1;
                commonItem.itemNum = itemInfo.int2;
                commonItem.isShowNum = true;
                commonItem.RefreshView(true);
                panel.m_rewardItemList.AddChild(commonItem);

                commonItem.alpha = 0;
                commonItem.TweenFade(1, 0.3f);
                yield return new WaitForSeconds(0.3f);
            }
        }
    }

}
