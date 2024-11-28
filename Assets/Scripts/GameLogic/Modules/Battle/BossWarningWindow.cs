using System;
using System.Collections.Generic;
using DG.Tweening;

public class BossWarningWindow : BaseWindow
{
    public override void OnOpen()
    {
        base.OnOpen();
    }

    public override void Close(float delay)
    {
        base.Close(delay);
        view.TweenFade(0, 0.5f);
    }
}
