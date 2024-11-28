using UI_Arena;
using Message.Arena;
using System.Collections.Generic;
using UnityEngine;
using Data.Beans;
using FairyGUI;

public class ScoreRewardWindow : BaseWindow
{
    private UI_ScoreRewardWindow m_window;

    public override void OnOpen()
    {
        base.OnOpen();
        m_window = getUiWindow<UI_ScoreRewardWindow>();
        m_window.m_btnClose.onClick.Add(Close);
        m_window.m_btnOneKeyGet.onClick.Add(_OnClickOneKeyGet);
        InitView();
    }

    public override void InitView()
    {
        base.InitView();
        m_window.m_txtScore.text = ArenaService.Singleton.GetCurScore() + "";
        _ShowRewardInfo();
    }

    public override void AddEventListener()
    {
        base.AddEventListener();
        GED.ED.addListener(EventID.RewardStateChange, _OnRewardStateChange);
    }

    public override void RemoveEventListener()
    {
        base.RemoveEventListener();
        GED.ED.removeListener(EventID.RewardStateChange, _OnRewardStateChange);
    }

    private void _OnRewardStateChange(GameEvent evt)
    {
        
        int type = (int)evt.Data;
        if (type != 2)
        {
            return;
        }

        _ShowRewardInfo();
        //int count = m_window.m_mainList.numChildren;
        //List<Reward> rewards = ArenaService.Singleton.GetScoreRewards();
        //for (int i = 0; i < rewards.Count; i++)
        //{
        //    if (i + 1 < count)
        //    {
        //        UI_scoreCell2 cell = m_window.m_mainList.GetChildAt(i + 1) as UI_scoreCell2;
        //        _OnRewardCellShow(rewards[i], cell);
        //    }
        //}
    }

    private void _ShowRewardInfo()
    {
        m_window.m_mainList.RemoveChildren(0,-1,true);
        m_window.m_mainList.AddChild(UI_scoreCell1.CreateInstance());
        List<t_integral_rewardBean> rewards = ConfigBean.GetBeanList<t_integral_rewardBean>();
         

        if (rewards == null)
        {
            Debug.LogError("不存在积分奖励列表");
            return;
        }

        for (int i = 0; i < rewards.Count; i++)
        {
            Reward reward = ArenaService.Singleton.GetRewardById(2, rewards[i].t_id);
            if (reward == null)
                continue;

            UI_scoreCell2 cell = UI_scoreCell2.CreateInstance();
            _OnRewardCellShow(reward, cell);
            m_window.m_mainList.AddChild(cell);
        }
    }


    //奖励格子信息
    private void _OnRewardCellShow(Reward info, UI_scoreCell2 obj)
    {
        obj.m_gComplete.visible = info.state == 2 ? true : false;
        obj.m_gKeLingQu.visible = info.state == 1 ? true : false;
        obj.m_gWeiWangCheng.visible = info.state == 0 ? true : false;
        obj.m_txtScore.text = info.id + "";

        obj.onClick.Add(() =>
        {
            if (info.state == 1)
            {
                ArenaService.Singleton.ReqCoreReward(false, info.id);
            }

        });

        t_integral_rewardBean bean = ConfigBean.GetBean<t_integral_rewardBean, int>(info.id);
        if (bean == null)
            return;

        string[] strRewards = GTools.splitString(bean.t_reward, ';');
        if (strRewards == null)
        {
            Debug.LogError("不存在积分奖励， 表id  " + info.id);
            return;
        }

        for (int index = 0; index < strRewards.Length; index++)
        {
            int[] items = GTools.splitStringToIntArray(strRewards[index], '+');
            if (items.Length < 2)
                continue;

            CommonItem iconCell = CommonItem.CreateInstance();
            iconCell.itemId = items[0];
            iconCell.itemNum = items[1];
            iconCell.isShowNum = true;
            iconCell.SetIconScale(0.7f, 0.7f);
            iconCell.RefreshView();
            obj.m_rewardList.AddChild(iconCell);
        }

 
    }

    //一键领取
    private void _OnClickOneKeyGet()
    {
        bool canGet = false;
        Dictionary<int, Reward> rewardDic = ArenaService.Singleton.GetScoreRewards();
        foreach (var info in rewardDic)
        {
            if (info.Value.state == 1)
            {
                canGet = true;
                break;
            }
        }

        if (canGet)
        {
            ArenaService.Singleton.ReqCoreReward(true);
        }
        else
        {
            TipWindow.Singleton.ShowTip(UIUtils.GetStrByLanguageID(61702000));
        }
 
    }



    protected override void OnClose()
    {
        base.OnClose();

    }

}