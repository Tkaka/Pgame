using System.Collections;
using System.Collections.Generic;
using UI_UltemateTrain;
using Data.Beans;

public class RankRangeRewardItem : UI_rankRangeRewardItem {

    public t_trial_rankBean rankBean;

    public new static RankRangeRewardItem CreateInstance()
    {
        return UI_rankRangeRewardItem.CreateInstance() as RankRangeRewardItem;
    }

    public void RefreshView()
    {
        RefreshBaseInfo();
        RefreshAwardList();
    }

    private void RefreshBaseInfo()
    {
        if (rankBean != null)
        {
            if (rankBean.t_rank_from == rankBean.t_rank_end)
            {
                m_conditionLabel.text = string.Format("第{0}名", rankBean.t_rank_from);
            }
            else
            {
                m_conditionLabel.text = string.Format("第{0}名-第{1}名", rankBean.t_rank_from, rankBean.t_rank_end);
            }
        }
    }

    private void RefreshAwardList()
    {
        int count = m_rewardItemList.numItems;
        CommonItem commonItem = null;
        if (rankBean != null && !string.IsNullOrEmpty(rankBean.t_award))
        {
            string[] awardArr = rankBean.t_award.Split(';');
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
                        commonItem.scale = new UnityEngine.Vector2(0.9f, 0.9f);

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
                            commonItem.itemId = int.Parse(awardInfoArr[0]);
                            commonItem.itemNum = int.Parse(awardInfoArr[1]);
                            commonItem.RefreshView();
                        }
                    }
                }
            }
        }
    }
}
