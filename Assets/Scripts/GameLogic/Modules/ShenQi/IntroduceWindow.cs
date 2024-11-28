using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_ShenQi;

public class IntroduceWindow : BaseWindow {

    UI_IntroduceWindow window;

    public override void OnOpen()
    {
        base.OnOpen();

        window = getUiWindow<UI_IntroduceWindow>();
        window.m_closeBtn.onClick.Add(OnCloseBtn);
        window.m_mask.onClick.Add(OnCloseBtn);
    }

    protected override void OnCloseBtn()
    {
        base.OnCloseBtn();
    }
}
