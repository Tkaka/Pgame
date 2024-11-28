using System.Collections;
using System.Collections.Generic;
using UI_UltemateTrain;
using Data.Beans;
using FairyGUI;

public class TrainRankRewardPanel : TabPage {

    UI_trainRankRewardPanel panel;
    List<t_trial_rankBean> trialRankBeanList;

    public TrainRankRewardPanel(UI_trainRankRewardPanel panel)
    {
        this.panel = panel;

        InitView();
    }

    private void InitView()
    {
        panel.m_switchLeftBtn.onClick.Add(OnSwitchLeftBtnClick);
        panel.m_switchRightBtn.onClick.Add(OnSwitchRightBtnClick);

        InitBaseInfo();
        InitRewardList();
    }

    private void InitBaseInfo()
    {
        panel.m_curRankLabel.text = 515 + "";
        t_trial_rankBean rankBean = GetTrialRankBean();
        if (rankBean != null)
        {
            if(rankBean.t_rank_from == rankBean.t_rank_end)
                panel.m_curRankRange.text = string.Format("当前排名档次({0})", rankBean.t_rank_from);
            else
                panel.m_curRankRange.text = string.Format("当前排名档次({0}-{1})", rankBean.t_rank_from, rankBean.t_rank_end);


            if (!string.IsNullOrEmpty(rankBean.t_award))
            {
                string[] rankRewardArr = rankBean.t_award.Split(';');
                int count = rankRewardArr.Length;
                string[] rankInfoArr = null;
                CommonItem commonItem = null;
                for (int i = 0; i < count; i++)
                {
                    rankInfoArr = rankRewardArr[i].Split('+');
                    if (rankInfoArr.Length == 2)
                    {
                        commonItem = CommonItem.CreateInstance();
                        commonItem.itemId = int.Parse(rankInfoArr[0]);
                        commonItem.itemNum = int.Parse(rankInfoArr[1]);
                        commonItem.isShowNum = true;
                        commonItem.RefreshView();
                        commonItem.scale = new UnityEngine.Vector2(0.6f, 0.6f);

                        panel.m_getRewardList.AddChild(commonItem);
                    }
                }
                panel.m_getRewardList.columnGap = -(int)(commonItem.width * 0.4f) + 25;
            }
        }
    }

    private void InitRewardList()
    {
        trialRankBeanList = ConfigBean.GetBeanList<t_trial_rankBean>();
        int count = trialRankBeanList.Count;
        panel.m_rankRewardList.itemRenderer = RenderItem;
        panel.m_rankRewardList.SetVirtual();
        panel.m_rankRewardList.numItems = count;
    }

    private void RenderItem(int index, GObject obj)
    {
        if (index < trialRankBeanList.Count)
        {
            t_trial_rankBean rankBean = trialRankBeanList[index];
            RankRangeRewardItem item = obj as RankRangeRewardItem;
            item.rankBean = rankBean;
            item.RefreshView();
        }
    }

    private void OnSwitchLeftBtnClick()
    {
        panel.m_rankRewardList.scrollPane.ScrollLeft(1, true);
    }

    private void OnSwitchRightBtnClick()
    {
        panel.m_rankRewardList.scrollPane.ScrollRight(1, true);
    }

    private t_trial_rankBean GetTrialRankBean()
    {
        int curRank = 515;
        t_trial_rankBean trialRankBean;
        List<t_trial_rankBean> trialRankBeanList = ConfigBean.GetBeanList<t_trial_rankBean>();
        int count = trialRankBeanList.Count;
        for (int i = 0; i < count; i++)
        {
            trialRankBean = trialRankBeanList[i];
            if (trialRankBean.t_rank_from <= curRank && trialRankBean.t_rank_end >= curRank)
                return trialRankBean;
        }

        return null;
    }

    public override void OnClose()
    {
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

    }


}
