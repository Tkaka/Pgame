using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_UltemateTrain;

enum TrainScorePanelType
{
    HightestScore = 0,               // 最高积分奖励面板
    RankReward = 1,                  // 排名奖励面板
}

public class TrainScoreRewardWindow : BaseWindow {

    UI_TrainScoreRewardWindow window;

    HightestScorePanel hightesScorePanel;
    TrainRankRewardPanel trainRankRewardPanel;
    TrainScorePanelType panelType;
    public override void OnOpen()
    {
        base.OnOpen();

        window = getUiWindow<UI_TrainScoreRewardWindow>();

        window.m_backBtn.onClick.Add(OnCloseBtn);
        window.m_rewardCtrl.onChanged.Add(OnRewardCtrlChanged);

        InitView();
    }

    public override void InitView()
    {
        base.InitView();

        hightesScorePanel = new HightestScorePanel(window.m_hightestScorePanel);
        trainRankRewardPanel = new TrainRankRewardPanel(window.m_trainRankRewardPanel);

        window.m_rewardCtrl.selectedIndex = 0;
        OnRewardCtrlChanged();
    }

    private void OnRewardCtrlChanged()
    {
        hightesScorePanel.OnHide();
        trainRankRewardPanel.OnHide();
        panelType = (TrainScorePanelType)window.m_rewardCtrl.selectedIndex;

        switch (panelType)
        {
            case TrainScorePanelType.HightestScore:
                hightesScorePanel.OnShow();
                break;
            case TrainScorePanelType.RankReward:
                trainRankRewardPanel.OnShow();
                break;
            default:
                break;
        }
    }

   

    protected override void OnCloseBtn()
    {
        hightesScorePanel.OnClose();
        trainRankRewardPanel.OnClose();

        base.OnCloseBtn();
    }
}
