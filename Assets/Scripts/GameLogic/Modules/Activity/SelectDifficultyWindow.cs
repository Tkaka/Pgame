using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_Activity;
using Message.Challenge;
using DG.Tweening;

public class SelectDifficultyWindow : BaseWindow {

    UI_SelectDifficultyWindow window;
    
    public override void OnOpen()
    {
        base.OnOpen();

        window = getUiWindow<UI_SelectDifficultyWindow>();
        window.m_popupView.m_forkBtn.onClick.Add(OnCloseBtn);
        InitView();
        PlayPopupAnim(window.m_mask, window.m_popupView);
    }

    protected override void OnOpenEffectEnd()
    {
        base.OnOpenEffectEnd();

        ShowDifficultItemAnim();
    }

    public override void InitView()
    {
        base.InitView();
        InitDifficultyList();
    }

    private void InitDifficultyList()
    {
        int count = window.m_popupView.m_difficultList.numChildren;
        int difficulty = (int)DifficultyType.Easy;
        DifficultyItem difficultyItem = null;
        for (int i = 0; i < count; i++, difficulty++)
        {
            difficultyItem = window.m_popupView.m_difficultList.GetChildAt(i) as DifficultyItem;
            difficultyItem.difficultType = (DifficultyType)difficulty;
            difficultyItem.visible = false;
            difficultyItem.Init(winName);
        }
    }
    /// <summary>
    /// 显示选择难度的Item出现的动画
    /// </summary>
    private void ShowDifficultItemAnim()
    {
        int count = window.m_popupView.m_difficultList.numChildren;
        DifficultyItem difficultyItem = null;
        float delayTime = 0.05f;
        for (int i = 0; i < count; i++)
        {
            difficultyItem = window.m_popupView.m_difficultList.GetChildAt(i) as DifficultyItem;
            difficultyItem.visible = true;
            difficultyItem.m_anim.Play(1, delayTime * (count -i - 1), null);
        }
    }

    protected override void OnCloseBtn()
    {
        base.OnCloseBtn();
    }
}
