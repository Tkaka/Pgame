using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_Activity;
using FairyGUI;
using Message.Challenge;
using Data.Beans;

public class DifficultyItem : UI_difficultyItem {

    private string winName;

    public DifficultyType difficultType;

    public new static UI_difficultyItem CreateInstance()
    {
        return (UI_difficultyItem)UIPackage.CreateObject("UI_Activity", "difficultyItem");
    }

    public void Init(string winName)
    {
        m_sweepBtn.onClick.Add(OnSweepBtnClick);
        m_toucher.onClick.Add(OnItemClick);

        this.winName = winName;

        InitView();
    }

    private void InitView()
    {
        ActivityActInfo activityInfo = ChallegeService.Singleton.GetActivityInfoByType(ChallegeService.Singleton.activityType);
        if (activityInfo != null)
        {
            int sweepDifficulty = activityInfo.baseInfo.sweepId;
            m_sweepBtn.visible = sweepDifficulty == (int)difficultType;
            m_lockIcon.visible = !IsOpen();
            m_bgLoader.grayed = !IsOpen();
        }
        // 设置难度的背景图
        string iconStr = "tznandu" + (int)difficultType;
        UIGloader.SetUrl(m_bgLoader, UIUtils.GetLoaderUrl(WinEnum.UI_Activity, iconStr));
    }

    /// <summary>
    /// 该难度是否是新开启的难度
    /// </summary>
    /// <returns></returns>
    private bool IsOpenNewDifficult()
    {
        if (IsOpen())
        {
            string dataKey = "";
            switch (ChallegeService.Singleton.activityType)
            {
                case ActivityType.Gold:
                    dataKey = "ActivityGold";
                    break;
                case ActivityType.NvGeDouJia:
                    dataKey = "ActivityNvGeDouJia";
                    break;
                case ActivityType.HuanXiang:
                    dataKey = "ActivityHuanXiang";
                    break;
                case ActivityType.Exp:
                    dataKey = "ActivityExp";
                    break;
                default:
                    break;
            }
            object dataObj = PlayerLocalData.GetData(dataKey, null);
            if (dataObj == null)
                return true;

            string dataStr = (string)dataObj;

        }

        return false;
    }

    private bool IsOpen()
    {
        int openLv = GetOpenLv();
        Message.Role.RoleInfo roleInfo = RoleService.Singleton.GetRoleInfo();
        if(roleInfo.level >= openLv)
        {
            ActivityActInfo activityActInfo = ChallegeService.Singleton.GetActivityInfoByType(ChallegeService.Singleton.activityType);
            if (activityActInfo != null)
            {
                return activityActInfo.baseInfo.record >= (int)difficultType;
            }
        }

        return false;
    }
    /// <summary>
    /// 是否没有挑战次数
    /// </summary>
    /// <returns></returns>
    private bool IsHaveTimes()
    {
        ActivityActInfo activityInfo = ChallegeService.Singleton.GetActivityInfoByType(ChallegeService.Singleton.activityType);
        if (activityInfo != null)
        {
            return activityInfo.maxTimes > activityInfo.baseInfo.completeTimes;
        }

        return false;
    }
    /// <summary>
    /// 是否冷却
    /// </summary>
    /// <returns></returns>
    private bool IsColdDown()
    {
        ActivityActInfo activityInfo = ChallegeService.Singleton.GetActivityInfoByType(ChallegeService.Singleton.activityType);
        if (activityInfo != null)
        {
            return activityInfo.baseInfo.seconds <= 0;
        }
        return false;
    }

    private void OnItemClick()
    {

        if (IsOpen())
        {
            if (IsHaveTimes())
            {
                if (IsColdDown())
                {
                    // 进入挑战界面
                    ChallegeService.Singleton.difficultyType = difficultType;
                    WinMgr.Singleton.Open<ZhenRongSelectWindow>(WinInfo.Create(false, null, true, difficultType), UILayer.Popup);

                    // 关闭难度选择界面
                    WinMgr.Singleton.Close(winName);
                }
                else
                {
                    //TODO ： 提示的语言ID
                    TipWindow.Singleton.ShowTip("冷却时间未到，请一会再来!");
                }
            }
            else
            {
                //TODO ： 提示的语言ID
                TipWindow.Singleton.ShowTip("今日挑战次数已用完!");
            }
        }
        else
        {
            // TODO : 提示语言ID
            int actBeanID = ChallegeService.Singleton.GetDiffictyActID((int)difficultType);
            if (actBeanID != -1)
            {
                t_dungeon_actBean activityActBean = ConfigBean.GetBean<t_dungeon_actBean, int>(actBeanID);
                if (activityActBean != null)
                {
                    string tipStr = string.Format("需要推到前一难度并且达到{0}级", activityActBean.t_level_limit);
                    TipWindow.Singleton.ShowTip(tipStr);
                }
            }
        }



    }
    private int GetOpenLv()
    {
        int actBeanID = ChallegeService.Singleton.GetDiffictyActID((int)difficultType);
        if (actBeanID != -1)
        {
            t_dungeon_actBean activityActBean = ConfigBean.GetBean<t_dungeon_actBean, int>(actBeanID);
            if (activityActBean != null)
            {
                return activityActBean.t_level_limit;
            }
        }
        return int.MaxValue;
    }
    private void OnSweepBtnClick()
    {
        if (IsHaveTimes())
        {
            if (IsOpen())
            {
                if (IsColdDown())
                {
                    // 进入挑战界面
                    ChallegeService.Singleton.ReqActivitySweep(ChallegeService.Singleton.activityType, difficultType);

                    // 关闭难度选择界面
                    WinMgr.Singleton.Close(winName);
                }
                else
                {
                    //TODO ： 提示的语言ID
                    TipWindow.Singleton.ShowTip("冷却时间未到，请一会再来!");
                }
            }
            else
            {
                // TODO : 提示语言ID
                int actBeanID = ChallegeService.Singleton.GetDiffictyActID((int)difficultType);
                if (actBeanID != -1)
                {
                    t_dungeon_actBean activityActBean = ConfigBean.GetBean<t_dungeon_actBean, int>(actBeanID);
                    if (activityActBean != null)
                    {
                        string tipStr = string.Format("需要推到前一难度并且达到{0}级", activityActBean.t_level_limit);
                        TipWindow.Singleton.ShowTip(tipStr);
                    }
                }
            }

        }
        else
        {
            //TODO ： 提示的语言ID
            TipWindow.Singleton.ShowTip("今日挑战次数已用完!");
        }
    }

    public override void Dispose()
    {
        winName = null;

        base.Dispose();
    }
}
