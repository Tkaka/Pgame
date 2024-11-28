using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_Activity;
using FairyGUI;
using Data.Beans;
using Message.Challenge;

public class ActivityPanel : UI_activityPanel {

    public new static UI_activityPanel CreateInstance()
    {
        return (UI_activityPanel)UIPackage.CreateObject("UI_Activity", "activityPanel");
    }

    public void Init()
    {
        m_canJiaBtn.onClick.Add(OnCanJiaBtnClick);
        m_ruleDetailBtn.onClick.Add(OnRuleDetailBtnClick);
    }

    public void RefreshView()
    {
        RefreshNormalVeiw();
        RefreshItemList();
        RefreshTimeLabel();
    }

    public void RefreshNormalVeiw()
    {
        ActivityActInfo activityInfo = ChallegeService.Singleton.GetActivityInfoByType(ChallegeService.Singleton.activityType);
        if (activityInfo != null)
        {
            m_remainTimesGroup.visible = IsShowRemainTimesGroup();
            int remianTimes = activityInfo.maxTimes - activityInfo.baseInfo.completeTimes;
            m_remainTimesLabel.text = string.Format("{0}/{1}", remianTimes, activityInfo.maxTimes);

            m_remainTimeGroup.visible = IsShowRemainTimeGroup();

            m_unOpenTipLabel.visible = activityInfo.isOpen == 0;
            // 语言ID ： 
            int openTimeID = 0;
            switch (ChallegeService.Singleton.activityType)
            {
                case ActivityType.Gold:
                    openTimeID = 71802006;
                    break;
                case ActivityType.Exp:
                    openTimeID = 71802006;
                    break;
                case ActivityType.NvGeDouJia:
                    openTimeID = 71802003;
                    break;
                case ActivityType.HuanXiang:
                    openTimeID = 71802002;
                    break;
                default:
                    break;
            }

            m_unOpenTipLabel.text = UIUtils.GetStrByLanguageID(openTimeID);
            m_unOpenGroup.visible = activityInfo.isOpen == 0;
            m_canJiaBtn.visible = activityInfo.isOpen == 1;

            // TODO不同的模式加载不同的背景图和不同的类型
            //UIGloader.SetUrl(m_bgLoader, "UI_TY_anjiankuang_lanse");
            UIGloader.SetUrl(m_typeLoader, ChallegeService.Singleton.GetActivityTypeIcon());
        }
    }

    public bool IsShowRemainTimesGroup()
    {
        ActivityActInfo activityInfo = ChallegeService.Singleton.GetActivityInfoByType(ChallegeService.Singleton.activityType);
        if (activityInfo != null)
        {
            if (activityInfo.isOpen == 1)
            {
                if (activityInfo.baseInfo.seconds <= 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        return false;
    }

    public bool IsShowRemainTimeGroup()
    {
        ActivityActInfo activityInfo = ChallegeService.Singleton.GetActivityInfoByType(ChallegeService.Singleton.activityType);
        if (activityInfo != null)
        {
            if (activityInfo.isOpen == 1)
            {
                if (activityInfo.baseInfo.completeTimes < activityInfo.maxTimes && activityInfo.baseInfo.seconds > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        return false;
    }

    private void RefreshItemList()
    {
        m_propList.RemoveChildren(0, -1, true);
        // 180201 金币  180202 经验 180203 女格斗家 180204 幻象  掉落全局表ID
        int globlaID = 0;
        switch (ChallegeService.Singleton.activityType)
        {
            case ActivityType.Gold:
                globlaID = 180201;
                break;
            case ActivityType.NvGeDouJia:
                globlaID = 180203;
                break;
            case ActivityType.HuanXiang:
                globlaID = 180204;
                break;
            case ActivityType.Exp:
                globlaID = 180202;
                break;
            default:
                break;
        }

        t_globalBean globalBean = ConfigBean.GetBean<t_globalBean, int>(globlaID);
        if (globalBean != null)
        {
            if (!string.IsNullOrEmpty(globalBean.t_string_param))
            {
                string[] itemIDArr = globalBean.t_string_param.Split('+');
                int length = itemIDArr.Length;
                CommonItem propItem = null;
                Message.Bag.GridInfo gridInfo = null;
                m_propList.RemoveChildren(0, -1, true);
                for (int i = 0; i < length; i++)
                {
                    propItem = CommonItem.CreateInstance();
                    propItem.itemId = int.Parse(itemIDArr[i]);
                    gridInfo = BagService.Singleton.GetGrid(propItem.itemId);
                    propItem.itemNum = gridInfo == null ? 0 : gridInfo.itemInfo.num;
                    propItem.isShowNum = false;
                    propItem.AddPopupEvent();
                    propItem.RefreshView();
                    propItem.SetScale(0.8f, 0.8f);
                    m_propList.AddChild(propItem);
                }
            }
        }
    }

    public void RefreshTimeLabel()
    {
        ActivityActInfo activityInfo = ChallegeService.Singleton.GetActivityInfoByType(ChallegeService.Singleton.activityType);
        if (activityInfo != null)
        {
            int remainTime = activityInfo.baseInfo.seconds;
            int min = remainTime / 60;
            int second = remainTime % 60;
            m_remianTimeLabel.text = string.Format("{0}分{1}秒", min, second);
        }
    }

    private void OnCanJiaBtnClick()
    {
        // 打开难度选择界面
        WinMgr.Singleton.Open<SelectDifficultyWindow>(null, UILayer.Popup);
    }

    private void OnRuleDetailBtnClick()
    {
        WinMgr.Singleton.Open<RuleDetailWindow>(null, UILayer.Popup);
    }
}
