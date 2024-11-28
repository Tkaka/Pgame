using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_Level;
using DG.Tweening;

public class NewChapterOpenWindow : BaseWindow {

    UI_NewChapterOpenWindow window;
    Tween tween;
    public override void OnOpen()
    {
        base.OnOpen();

        window = getUiWindow<UI_NewChapterOpenWindow>();

        InitView();
    }

    public override void InitView()
    {
        base.InitView();

        //window.m_mask.alpha = 0;
        //tween = window.m_mask.TweenFade(1, 1f);
        //tween.OnComplete(OnTweenFinished);

        window.m_anim.Play(OnTweenFinished);
    }

    private void OnTweenFinished()
    {
        OnCloseBtn();
    }

    protected override void OnCloseBtn()
    {
        if (tween != null && tween.IsActive())
            tween.Kill();
        tween = null;

        base.OnCloseBtn();
    }
}
