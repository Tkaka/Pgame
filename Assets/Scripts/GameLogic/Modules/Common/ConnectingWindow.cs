
using UI_Common;
using DG.Tweening;

public class ConnectingWindow : SingletonWindow<ConnectingWindow>
{
    private UI_ConnectingWindow window;
    private bool inited;

    public override void OnOpen()
    {
        base.OnOpen();
        window = getUiWindow<UI_ConnectingWindow>();

        window.visible = false;
        window.touchable = false;
    }

    private long coroId = 0;
    public override void InitView()
    {
        base.InitView();
        if (inited)
            return;

        inited = true;
        window.visible = false;
        window.touchable = true;
        coroId = CoroutineManager.Singleton.delayedCall(0.5f, DelayCall);
    }

    private Tween tween;
    private void DelayCall()
    {
        window.visible = true;
        window.touchable = true;
        window.m_fill.fillAmount = 0;
        tween = DOTween.To(() => window.m_fill.fillAmount, x => window.m_fill.fillAmount = x, 1.0f, 0.5f);
        tween.SetLoops(-1, LoopType.Yoyo);
    }

    protected override void OnClose()
    {
        base.OnClose();
        CoroutineManager.Singleton.stopCoroutine(coroId);
        coroId = 0;
        if (tween != null && tween.IsActive())
            tween.Kill();
        tween = null;
    }

    public void Show()
    {
        InitView();
    }

    public void Hide()
    {
        inited = false;
        window.visible = false;
        window.touchable = false;
        CoroutineManager.Singleton.stopCoroutine(coroId);
        coroId = 0;
        if (tween != null && tween.IsActive())
            tween.Kill();
        tween = null;
    }

    public override void Open()
    {
        WinMgr.Singleton.Open<ConnectingWindow>();
    }
}
