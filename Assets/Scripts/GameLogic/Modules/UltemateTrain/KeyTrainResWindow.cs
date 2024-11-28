using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_UltemateTrain;
using Message.Challenge;
/// <summary>
/// 一键爬塔的步骤
/// </summary>
enum SkipTrainStep
{
    BoxReward = 1,          // 宝箱奖励
    BuyProperty = 2,        // 属性加成
    SecretBox = 3,          // 隐秘宝箱
}

public class KeyTrainResWindow : BaseWindow {

    UI_KeyTrainResWindow window;

    JumpBoxRewardPanel jumpBoxRewardPanel;
    JumpBuyPropertyPanel jumpBuyPropertyPanel;
    JumpBuyBoxPanel jumpBuyBoxPanel;

    private SkipTrainStep step;
    /// <summary>
    /// 当前购买的属性层数下标
    /// </summary>
    private int curBuyPropertyIndex;

    private SkipTrainStep Step
    {
        get { return step; }
        set
        {
            step = value;
            OnStepChanged();
            RefreshBtnState();
        }
    }

    public override void OnOpen()
    {
        base.OnOpen();

        window = getUiWindow<UI_KeyTrainResWindow>();
        curBuyPropertyIndex = 0;

        BindEvent();
        InitView();
        RefreshView();

        Step = SkipTrainStep.BoxReward;

    }

    private void BindEvent()
    {
        window.m_closeBtn.onClick.Add(OnCloseBtnClick);
        window.m_nextStepBtn.onClick.Add(OnNextBtnClick);
        window.m_buyNextPropertyBtn.onClick.Add(OnBuyNextPropertyBtnClick);
    }

    public override void InitView()
    {
        base.InitView();

        jumpBoxRewardPanel = new JumpBoxRewardPanel(window.m_jumpBoxRewardPanel);
        jumpBuyPropertyPanel = new JumpBuyPropertyPanel(window.m_jumpBuyPropertyPanel);
        jumpBuyPropertyPanel.CurPropertyIndex = curBuyPropertyIndex;
        jumpBuyBoxPanel = new JumpBuyBoxPanel(window.m_jumpBuyBoxPanel);

        
    }

    public override void RefreshView()
    {
        base.RefreshView();

        RefreshText();
    }

    private void RefreshBtnState()
    {
        window.m_progressTipGroup.visible = step != SkipTrainStep.SecretBox;
        window.m_buyNextPropertyGroup.visible = step == SkipTrainStep.BuyProperty;
        window.m_closeBtn.visible = step == SkipTrainStep.BuyProperty;
        window.m_nextStepBtn.visible = Step != SkipTrainStep.BuyProperty;

        window.m_boxRewardTipLabel.color = Color.white;
        window.m_boxRewardTipLabel.textFormat.bold = false;
        window.m_propertyTipLabel.color = Color.white;
        window.m_propertyTipLabel.textFormat.bold = false;
        switch (Step)
        {
            case SkipTrainStep.BoxReward:
                window.m_boxRewardTipLabel.color = Color.blue;
                window.m_boxRewardTipLabel.textFormat.bold = true ;
                break;
            case SkipTrainStep.BuyProperty:
                window.m_propertyTipLabel.color = Color.blue;
                window.m_propertyTipLabel.textFormat.bold = true;
                break;
            case SkipTrainStep.SecretBox:
                break;
            default:
                break;
        }
    }

    private void RefreshText()
    {
        ResTrialSkip trainSkipInfo = UltemateTrainService.Singleton.trainSkipInfo;
        if(trainSkipInfo != null)
        {
            int remainFloorNum = trainSkipInfo.attrs.Count - 1 - curBuyPropertyIndex;
            window.m_remainFloorLabel.text = string.Format("还剩{0}层可购买", remainFloorNum);
        }
    }

    #region 按钮事件 ****************************************************************************************************************
    private void OnCloseBtnClick()
    {
        // 打开二次确认界面
        AgainConfirmWindow.Singleton.ShowTip("确定退出加成选择界面并自动购买加成吗？", AutoBuyProperty, JumpBuyProperty);
    }

    private void OnNextBtnClick()
    {
        if (Step == SkipTrainStep.SecretBox)
        {
            // 如果还有没买的宝箱，提示，否则直接退出
            if (IsExistUnopenedSecretBox())
                AgainConfirmWindow.Singleton.ShowTip("还有未开启的宝箱，确定跳过此步骤吗？", JumpBuySecretBox);
            else
                OnCloseBtn();
        }
        else
            Step++;

    }

    private void OnBuyNextPropertyBtnClick()
    {
        ResTrialSkip trainSkipInfo = UltemateTrainService.Singleton.trainSkipInfo;
        if (trainSkipInfo != null)
        {
            curBuyPropertyIndex++;
            if (trainSkipInfo.attrs.Count <= curBuyPropertyIndex + 1)
            {
                //最后一层了
                window.m_buyNextPropertyGroup.visible = false;
                window.m_nextStepBtn.visible = true;
            }

            jumpBuyPropertyPanel.CurPropertyIndex = curBuyPropertyIndex;
        }

        RefreshText();
    }

    private void OnStepChanged()
    {
        jumpBuyPropertyPanel.OnHide();
        jumpBoxRewardPanel.OnHide();
        jumpBuyBoxPanel.OnHide();
        switch (step)
        {
            case SkipTrainStep.BoxReward:
                jumpBoxRewardPanel.OnShow();
                break;
            case SkipTrainStep.BuyProperty:
                jumpBuyPropertyPanel.OnShow();
                break;
            case SkipTrainStep.SecretBox:
                jumpBuyBoxPanel.OnShow();
                break;
            default:
                break;
        }
    }
    #endregion

    #region 消息回调 ***************************************************************************************************************

    private void AutoBuyProperty()
    {
        UltemateTrainService.Singleton.ReqTrialAutoBuyBuff();
        Step++;
    }

    private void JumpBuyProperty()
    {
        Step++;
    }

    private void JumpBuySecretBox()
    {
        OnClose();
        UltemateTrainService.Singleton.trainSkipInfo = null;
    }
    #endregion

    #region 简单数据处理 ************************************************************************************************************
    /// <summary>
    /// 是否存在没有打开的隐秘宝箱
    /// </summary>
    /// <returns></returns>
    private bool IsExistUnopenedSecretBox()
    {
        return false;
    }

    #endregion

    protected override void OnCloseBtn()
    {
        jumpBoxRewardPanel.OnClose();
        jumpBuyBoxPanel.OnClose();
        jumpBuyPropertyPanel.OnClose();

        base.OnCloseBtn();
    }
}
