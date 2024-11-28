using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_UltemateTrain;

public class UltemateTrainRuleWindow : BaseWindow {

    UI_UltemateTrainRuleWindow window;

    public override void OnOpen()
    {
        base.OnOpen();

        window = getUiWindow<UI_UltemateTrainRuleWindow>();
        window.m_closeBtn.onClick.Add(OnCloseBtn);
        window.m_mask.onClick.Add(OnCloseBtn);

        InitView();
    }

    public override void InitView()
    {
        base.InitView();
    }
}
