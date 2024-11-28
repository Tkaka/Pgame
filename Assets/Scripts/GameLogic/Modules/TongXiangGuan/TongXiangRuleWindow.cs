using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_TongXiangGuan;

public class TongXiangRuleWindow : BaseWindow {

    UI_TongXiangRuleWindow window;

    public override void OnOpen()
    {
        base.OnOpen();

        window = getUiWindow<UI_TongXiangRuleWindow>();
        window.m_closeBtn.onClick.Add(OnCloseBtn);
    }

    protected override void OnCloseBtn()
    {
        base.OnCloseBtn();
    }
}
