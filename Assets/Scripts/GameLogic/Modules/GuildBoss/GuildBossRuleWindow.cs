using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_GuildBoss;

public class GuildBossRuleWindow : BaseWindow {

    UI_GuildBossRuleWindow window;

    public override void OnOpen()
    {
        base.OnOpen();

        window = getUiWindow<UI_GuildBossRuleWindow>();
        window.m_closeBtn.onClick.Add(OnCloseBtn);
        window.m_mask.onClick.Add(OnCloseBtn);
    }

    protected override void OnCloseBtn()
    {
        base.OnCloseBtn();
    }
}
