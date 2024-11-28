using System.Collections;
using System.Collections.Generic;
using UI_UltemateTrain;
using Message.Challenge;
using Message.Bag;
using FairyGUI;
using Data.Beans;

public class HightestScorePanel : TabPage{

    UI_hightestScorePanel panel;

    public HightestScorePanel(UI_hightestScorePanel panel)
    {
        this.panel = panel;


        BindEvent();
        InitView();
        RefreshReceivedAwardList();
    }

    private void BindEvent()
    {
        panel.m_scoreToucher.onTouchBegin.Add(OnScoreTouchBegin);
        panel.m_scoreToucher.onTouchEnd.Add(OnScoreTouchEnd);
        panel.m_scoreToucher.onRollOut.Add(OnScoreTouchEnd);
        panel.m_switchLeftBtn.onClick.Add(OnSwitchLeftBtnClick);
        panel.m_switchRightBtn.onClick.Add(OnSwitchRightBtnClick);
        panel.m_scoreRewardList.onClickItem.Add(OnClickItem);
    }

    private void AddListener()
    {
        GED.ED.addListener(EventID.OnResTrialScoreAwardGet, OnResTrialScoreAwardGet);
    }

    private void RemoveListener()
    {
        GED.ED.removeListener(EventID.OnResTrialScoreAwardGet, OnResTrialScoreAwardGet);
    }

    private void InitView()
    {
        InitBaseInfo();
        InitRewardList();
    }

    private void InitBaseInfo()
    {
        // 初始话文本信息
        ResTrialInfo trainInfo = UltemateTrainService.Singleton.trainInfo;
        if (trainInfo != null)
        {
            int todayScore = trainInfo.trialInfo.score;
            TrialScoreAwardInfo trainScoreAwardInfo = UltemateTrainService.Singleton.trainScoreAwardInfo;
            if (trainScoreAwardInfo != null)
            {
                int oldScore = trainScoreAwardInfo.score;
                int curScore = oldScore + todayScore;

                panel.m_scoreFormuleLabel.text = string.Format("{0}={1}+{2}", curScore, oldScore, todayScore);
                panel.m_scoreLabel.text = curScore + "";
            }
        }

        panel.m_scoreFormulaGroup.visible = false;
    }

    private void InitRewardList()
    {
        // 设置虚拟列表
        panel.m_scoreRewardList.itemRenderer = RenderItem;
        panel.m_scoreRewardList.SetVirtual();

        List<t_trial_scoreBean> trailScoreBeanList = ConfigBean.GetBeanList<t_trial_scoreBean>();
        panel.m_scoreRewardList.numItems = trailScoreBeanList.Count;
    }

    private void RenderItem(int index, GObject obj)
    {
        ScoreRewardItem item = obj as ScoreRewardItem;
        item.index = index;
        item.RefreshView();
    }

    #region 事件回调 ****************************************************************************************************************
    private void OnScoreTouchBegin()
    {
        panel.m_scoreFormulaGroup.visible = true;
    }

    private void OnScoreTouchEnd()
    {
        panel.m_scoreFormulaGroup.visible = false;
    }

    private void OnSwitchLeftBtnClick()
    {
        panel.m_scoreRewardList.scrollPane.ScrollLeft(1, true);
    }

    private void OnSwitchRightBtnClick()
    {
        panel.m_scoreRewardList.scrollPane.ScrollRight(1, true);
    }

    private void OnResTrialScoreAwardGet(GameEvent evt)
    {
        // 打开宝箱界面
        List<ItemInfo> itemList = UltemateTrainService.Singleton.TransformIntVsIntToItemInfo(evt.Data as List<IntVsInt>);
        ThreeParam<bool, List<ItemInfo>, string> param = new ThreeParam<bool, List<ItemInfo>, string>();
        param.value1 = false;
        param.value2 = itemList;
        WinMgr.Singleton.Open<BoxReceiveWidow>(WinInfo.Create(false, null, true, param), UILayer.Popup);
        RefreshView();
    }

    private void OnClickItem()
    {
        ScoreRewardItem item = panel.m_scoreRewardList.touchItem as ScoreRewardItem;
        item.ClickItem();
    }

    #endregion


    public override void OnClose()
    {
        panel = null;
        RemoveListener();
    }

    public override void OnHide()
    {
        panel.visible = false;
        RemoveListener();
    }

    public override void OnShow()
    {
        panel.visible = true;
        AddListener();
    }

    public override void RefreshView(bool isNet = false)
    {
        RefreshScoreAwardList();
        RefreshReceivedAwardList();
    }
    /// <summary>
    /// 刷新奖励列表
    /// </summary>
    private void RefreshScoreAwardList()
    {
        panel.m_scoreRewardList.RefreshVirtualList();
    }
    /// <summary>
    /// 刷新获得的奖励列表
    /// </summary>
    private void RefreshReceivedAwardList()
    {
        TrialScoreAwardInfo trainScoreAwardInfo = UltemateTrainService.Singleton.trainScoreAwardInfo;
        if (trainScoreAwardInfo != null)
        {
            int count = trainScoreAwardInfo.rewards.Count;
            CommonItem item = null;
            IntVsInt awardInfo;
            panel.m_totalIncomeList.RemoveChildren(0, -1, true);
            for (int i = 0; i < count; i++)
            {
                awardInfo = trainScoreAwardInfo.rewards[i];
                item = CommonItem.CreateInstance();
                item.itemId = awardInfo.int1;
                item.itemNum = awardInfo.int2;
                item.isShowNum = true;
                item.RefreshView();
                item.scale = new UnityEngine.Vector2(0.6f, 0.6f);
                panel.m_totalIncomeList.AddChild(item);
            }
            if (item != null)
            {
                panel.m_totalIncomeList.columnGap = -(int)(item.size.x * 0.4f) + 10;
            }
        }
    }
}
