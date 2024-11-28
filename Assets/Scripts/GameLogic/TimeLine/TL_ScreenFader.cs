using DG.Tweening;
using FairyGUI;
using UnityEngine;

public class TL_ScreenFader : BaseBehaviour
{

    private GGraph screen;

    private Tween tween;

    public UIPanel uiPanel;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        Debug.Log("OnEnable");
        if (uiPanel != null && uiPanel.ui != null)
        {
            GComponent gc = uiPanel.ui.asCom;
            if (gc != null)
            {
                screen = gc.GetChild("fader").asGraph;
                screen.visible = false;
            }
            if (screen != null)
            {
                screen.visible = true;
                tween = DOTween.To(() => screen.alpha, x => screen.alpha = x, 0.0f, 1.0f);
                tween.OnComplete(() => {
                    tween = null;
                });
            }
        }
    }

}
