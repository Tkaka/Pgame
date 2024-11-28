using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_UltemateTrain;
using Data.Beans;
using Message.Challenge;

public class ScoreRewardItem : UI_scoreRewardItem {

    public int index;

    public new static ScoreRewardItem CreateInstance()
    {
        return UI_scoreRewardItem.CreateInstance() as ScoreRewardItem;
    }

    public void RefreshView()
    {
        RefreshBaseInfo();
        RefreshItemList();
    }

    private void RefreshBaseInfo()
    {
        t_trial_scoreBean scoreBean = GetScoreBean();
        if (scoreBean != null)
            m_coditionLabel.text = string.Format("积分达到{0}", scoreBean.t_score);

        m_receivedGroup.visible = false;
        m_canReveiveIcon.visible = false;
        if (IsReachScore())
        {
            if (IsReceived())
                m_receivedGroup.visible = true;
            else
                m_canReveiveIcon.visible = true;
        }
    }

    private void RefreshItemList()
    {
        int count = m_rewardItemList.numItems;
        CommonItem commonItem = null;
        t_trial_scoreBean scoreBean = GetScoreBean();
        if (scoreBean != null && !string.IsNullOrEmpty(scoreBean.t_award))
        {
            string[] awardArr = scoreBean.t_award.Split(';');
            string[] awardInfoArr = null;
            if (count == 0)
            {
                count = awardArr.Length;
                for (int i = 0; i < count; i++)
                {
                    awardInfoArr = awardArr[i].Split('+');
                    if (awardInfoArr.Length == 2)
                    {
                        commonItem = CommonItem.CreateInstance();
                        commonItem.itemId = int.Parse(awardInfoArr[0]);
                        commonItem.itemNum = int.Parse(awardInfoArr[1]);
                        commonItem.isShowNum = true;
                        commonItem.AddPopupEvent();
                        commonItem.RefreshView();
                        commonItem.scale = new Vector2(0.9f, 0.9f);

                        m_rewardItemList.AddChild(commonItem);
                    }
                }

                m_rewardItemList.columnGap = -(int)(commonItem.width * 0.1f) + 15;
            }
            else
            {
                for (int i = 0; i < count; i++)
                {
                    if (i < awardArr.Length)
                    {
                        awardInfoArr = awardArr[i].Split('+');
                        if (awardInfoArr.Length == 2)
                        {
                            commonItem = m_rewardItemList.GetChildAt(i) as CommonItem;
                            commonItem.itemId = int.Parse(awardInfoArr[0]);
                            commonItem.itemNum = int.Parse(awardInfoArr[1]);
                            commonItem.RefreshView();
                        }
                    }
                }
            }
        }
    }

    private bool IsReachScore()
    {
        t_trial_scoreBean scoreBean = GetScoreBean();
        ResTrialInfo trainInfo = UltemateTrainService.Singleton.trainInfo;
        if (trainInfo != null)
        {
            int todayScore = trainInfo.trialInfo.score;
            TrialScoreAwardInfo trainScoreAwardInfo = UltemateTrainService.Singleton.trainScoreAwardInfo;
            if (trainScoreAwardInfo != null)
            {
                int oldScore = trainScoreAwardInfo.score;
                int curScore = oldScore + todayScore;

                return curScore >= scoreBean.t_score;
            }
        }
        return false;
    }

    private bool IsReceived()
    {
        TrialScoreAwardInfo scoreInfo = UltemateTrainService.Singleton.trainScoreAwardInfo;
        if (scoreInfo != null && scoreInfo.num.Count > index)
            return scoreInfo.num[index] == 1;

        return false;
    }

    private t_trial_scoreBean GetScoreBean()
    {
        List<t_trial_scoreBean> trialScoreBeanList = ConfigBean.GetBeanList<t_trial_scoreBean>();
        t_trial_scoreBean scoreBean = null;
        if (trialScoreBeanList.Count > index)
            scoreBean = trialScoreBeanList[index];

        return scoreBean;
    }

    public void ClickItem()
    {
        if (!IsReceived())
        {
            UltemateTrainService.Singleton.ReqTrialScoreAwardGet(index);
        }
    }
}
