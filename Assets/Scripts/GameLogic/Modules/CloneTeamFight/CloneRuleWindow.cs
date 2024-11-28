using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_CloneTeamFight;

public class CloneRuleWindow : BaseWindow {

    UI_CloneRuleWindow window;
    public override void OnOpen()
    {
        base.OnOpen();

        window = getUiWindow<UI_CloneRuleWindow>();
        InitView();
    }

    public override void InitView()
    {
        base.InitView();
        window.m_closeBtn.onClick.Add(OnCloseBtn);
        window.m_mask.onClick.Add(OnCloseBtn);
    }

    protected override void OnCloseBtn()
    {
        base.OnCloseBtn();
    }
}
