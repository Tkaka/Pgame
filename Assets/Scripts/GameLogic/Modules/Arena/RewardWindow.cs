using UI_Arena;
using Message.Arena;
using System.Collections.Generic;
using UnityEngine;
using Data.Beans;
using FairyGUI;

public class RewardWindow : BaseWindow
{
    private UI_RewardWindow m_window;
    //private UI_RwardCell1 m_lastGetCell;                                     //上次点击领取的格子
    private int m_lastClickRank;                                             //上次点击领奖的档位id

    Dictionary<int, int> m_totalGetedReward = new Dictionary<int, int>();    //最高排名的总收益

    public override void OnOpen()
    {
        base.OnOpen();
        m_window = getUiWindow<UI_RewardWindow>();
        _InitList();
        _BindClickEvent();
        InitView();
    }

    private void _InitList()
    {
        m_window.m_page1.m_rankRewardList.SetVirtual();
        m_window.m_page1.m_rankRewardList.itemProvider = _HighRankItemProvider;
        m_window.m_page1.m_rankRewardList.itemRenderer = _HighRankItemRender;

        m_window.m_page2.m_rankRewardList.SetVirtual();
        m_window.m_page2.m_rankRewardList.itemProvider = _DailyRankItemProvider;
        m_window.m_page2.m_rankRewardList.itemRenderer = _DailyRankItemRender;


    }

    private string _HighRankItemProvider(int index)
    {
        return UI_RwardCell1.URL;
    }

    private void _HighRankItemRender(int index, GObject obj)
    {
        var rewards = ConfigBean.GetBeanList<t_top_rewardBean>();
        if (index < 0 || index >= rewards.Count)
            return;

        UI_RwardCell1 cell = obj as UI_RwardCell1;
        if (cell == null)
            return;

        t_top_rewardBean bean = rewards[(rewards.Count - 1 - index)];

        Reward rewrd = ArenaService.Singleton.GetRewardById(1, bean.t_id);
        if (rewrd == null)
            return;

        _OnHighestRankCellShow(cell, rewrd);

    }

    private string _DailyRankItemProvider(int index)
    {
        return UI_RwardCell2.URL;
    }

    private void _DailyRankItemRender(int index, GObject obj)
    {
        UI_RwardCell2 cell = obj as UI_RwardCell2;
        if (cell == null)
            return;

        var rewardBeans = ConfigBean.GetBeanList<t_dailytop_rewardBean>();
        if (index < 0 || index >= rewardBeans.Count)
            return;

        t_dailytop_rewardBean rewardBean = rewardBeans[index];

        cell.m_txtQuJian.text = string.Format("第{0}名-第{1}名", rewardBean.t_start, rewardBean.t_end);
        string[] strRewards = GTools.splitString(rewardBean.t_reward, ';');
        if (strRewards == null)
        {
            Debug.LogError("空的奖励信息, 每日排名奖励表ID" + rewardBean.t_id);
            return;
        }

        cell.m_rewardList.RemoveChildren(0, -1, true);
        for (int i = 0; i < strRewards.Length; i++)
        {
            int[] items = GTools.splitStringToIntArray(strRewards[i], '+');
            if (items.Length < 2)
                continue;

            CommonItem iconCell = CommonItem.CreateInstance();
            iconCell.itemId = items[0];
            iconCell.itemNum = items[1];
            iconCell.isShowNum = true;
            iconCell.SetIconScale(0.7f, 0.7f);
            iconCell.RefreshView();
            cell.m_rewardList.AddChild(iconCell);
        }

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

    private void _BindClickEvent()
    {
        m_window.m_btnClose.onClick.Add(Close);
        m_window.m_btnBestReward.onClick.Add(_OnHighestRankRewardClick);
        m_window.m_btnDailyReward.onClick.Add(_OnDailyRewardClick);

    }

    public override void InitView()
    {
        base.InitView();
        _ShowHighestRankReward();
        _ShowHighestRankTotalReward();
        _ShowDailySectionInfo();
        _ShowDailyReawrd();
        _ShowHighestRank();
    }

    //奖励状态信息改变
    private void _OnRewardStateChange(GameEvent param)
    {
        int type = (int)param.Data;
        if (type == 1)
        {


            m_window.m_page1.m_rankRewardList.RefreshVirtualList();
             
            _ShowHighestRankTotalReward();
        }
    }

    //显示最高排名
    private void _ShowHighestRank()
    {
        m_window.m_page1.m_txtRank.text = ArenaService.Singleton.GetHighestRank() + "";
    }

    //显示每日排名奖励
    private void _ShowDailyReawrd()
    {
        var rewardBeans = ConfigBean.GetBeanList<t_dailytop_rewardBean>();
        m_window.m_page2.m_rankRewardList.numItems = rewardBeans.Count;
    }

    //显示每日排名所在区间信息
    private void _ShowDailySectionInfo()
    {
        t_dailytop_rewardBean curBean = null; 
        var rewardBeans = ConfigBean.GetBeanList<t_dailytop_rewardBean>();
        int myRank = ArenaService.Singleton.GetRoleRank();
        m_window.m_page2.m_txtRank.text = myRank + "";
        for (int i = 0; i < rewardBeans.Count; i++)
        {
            if (myRank >= rewardBeans[i].t_start && myRank <= rewardBeans[i].t_end)
            {
                curBean = rewardBeans[i];
                break;
            }
        }

        if (curBean == null)
        {
            m_window.m_page2.m_txtCurRankDes.text = "当前无奖励可领取";
        }
        else
        {
            m_window.m_page2.m_txtCurRankDes.text = string.Format("当前排名档次({0}-{1})可获得奖励：", curBean.t_start, curBean.t_end);
            string[] strRewards = GTools.splitString(curBean.t_reward, ';');
            for (int index = 0; index < strRewards.Length; index++)
            {
                int[] items = GTools.splitStringToIntArray(strRewards[index], '+');
                if (items.Length < 2)
                    continue;

                //UI_JinJiIconCell iconCell = UI_JinJiIconCell.CreateInstance();
                //_OnRewardCellShow(iconCell, items[0], items[1]);
                CommonItem iconCell = CommonItem.CreateInstance();
                iconCell.itemId = items[0];
                iconCell.itemNum = items[1];
                iconCell.isShowNum = true;
                iconCell.SetIconScale(0.7f, 0.7f);
                iconCell.RefreshView();
                m_window.m_page2.m_rewardList.AddChild(iconCell);
            }
        }
         

    }

    //显示最高排名奖励
    private void _ShowHighestRankReward()
    {
        var rewards = ConfigBean.GetBeanList<t_top_rewardBean>();
        if (rewards == null)
        {
            Debug.LogError("奖励列表为空");
            return;
        }

        m_window.m_page1.m_rankRewardList.numItems = rewards.Count;

    }

    //最高排名已领取的奖励总收益
    private void _ShowHighestRankTotalReward()
    {
        m_window.m_page1.m_rewrdList.RemoveChildren(0, -1, true);
        m_totalGetedReward.Clear();
        Dictionary<int, Reward> infoRewards = ArenaService.Singleton.GetHighestRankRewards();
        if (infoRewards == null)
        {
            Debug.LogError("最高排名奖励列表为空");
            return;
        }

        foreach (var info in infoRewards)
        {
            Reward reward = info.Value;
            t_top_rewardBean rewardBean = ConfigBean.GetBean<t_top_rewardBean, int>(reward.id);
            if (rewardBean == null)
            {
                Debug.LogError("不存在的最高排名奖励ID" + reward.id);
                continue;
            }
            if (reward.state != 2)
            {
                //不是已领取的不管
                continue;
            }

            string[] strRewards = GTools.splitString(rewardBean.t_reward, ';');
            if (strRewards == null)
            {
                Debug.LogError("空的奖励信息, 最高排名奖励表ID" + reward.id);
                continue;
            }

            for (int index = 0; index < strRewards.Length; index++)
            {
                int[] items = GTools.splitStringToIntArray(strRewards[index], '+');
                if (items.Length < 2)
                    continue;

                //已领取
                if (m_totalGetedReward.ContainsKey(items[0]))
                    m_totalGetedReward[items[0]] += items[1];
                else
                    m_totalGetedReward.Add(items[0], items[1]);

            }
        }

        foreach (var info in m_totalGetedReward)
        {
            //UI_JinJiIconCell iconCell = UI_JinJiIconCell.CreateInstance();
            //_OnRewardCellShow(iconCell, info.Key, info.Value);
            CommonItem iconCell = CommonItem.CreateInstance();
            iconCell.itemId = info.Key;
            iconCell.itemNum = info.Value;
            iconCell.isShowNum = true;
            iconCell.SetIconScale(0.7f, 0.7f);
            iconCell.RefreshView();
            m_window.m_page1.m_rewrdList.AddChild(iconCell);
        }
    }



    private void _OnHighestRankCellShow(UI_RwardCell1 obj, Reward info)
    {
 
        t_top_rewardBean rewardBean = ConfigBean.GetBean<t_top_rewardBean, int>(info.id);
        if (rewardBean == null)
        {
            Debug.LogError("不存在的最高排名奖励ID" + info.id);
            return;
        }

        //obj.m_bg.url = "";
        obj.m_gKeLing.visible = info.state == 1 ? true : false;
        obj.m_gYiLing.visible = info.state == 2 ? true : false;
        obj.m_txtQuJian.text = string.Format("排名进入{0}", info.id);

        obj.onClick.Clear();
        obj.onClick.Add(() =>
        {
            if (info.state == 1)
            {
                //可领取
                ArenaService.Singleton.ReqRankReward(info.id);
                m_lastClickRank = info.id;
            }
        });

        string[] strRewards = GTools.splitString(rewardBean.t_reward, ';');
        if (strRewards == null)
        {
            Debug.LogError("空的奖励信息, 最高排名奖励表ID" + info.id);
            return;
        }

        obj.m_rewardList.RemoveChildren(0, -1, true);
        for (int i = 0; i < strRewards.Length; i++)
        {
            int[] items = GTools.splitStringToIntArray(strRewards[i], '+');
            if (items.Length < 2)
                continue;

            //UI_JinJiIconCell iconCell = UI_JinJiIconCell.CreateInstance();
            //_OnRewardCellShow(iconCell, items[0], items[1]);
            CommonItem iconCell = CommonItem.CreateInstance();
            iconCell.itemId = items[0];
            iconCell.itemNum = items[1];
            iconCell.isShowNum = true;
            iconCell.SetIconScale(0.7f, 0.7f);
            iconCell.RefreshView();
            obj.m_rewardList.AddChild(iconCell);

        }
    }

    private void _OnHighestRankRewardClick()
    {
        m_window.m_pageControl.selectedIndex = m_window.m_btnBestReward.selected ? 0 : 1;
    }

    private void _OnDailyRewardClick()
    {
        m_window.m_pageControl.selectedIndex = m_window.m_btnDailyReward.selected ? 1 : 0;
    }

    protected override void OnClose()
    {
        base.OnClose();
        m_totalGetedReward = null;

    }

}