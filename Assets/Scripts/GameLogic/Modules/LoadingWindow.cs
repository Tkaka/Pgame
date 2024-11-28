using FairyGUI;
using UnityEngine;
using DG.Tweening;
using UI_Loading;

public class LoadingWindow : BaseWindow
{
    private UI_LoadingWindow loadingWin;

    public override void OnOpen()
    {
        base.OnOpen();
        loadingWin = getUiWindow<UI_LoadingWindow>();
        loadingWin.m_loadingBar.fillAmount = 0.0f;

        TwoParam<string, bool> param = Info.param as TwoParam<string, bool>;
        if (param != null)
            loadingWin.visible = param.value2;

        LoadBgImage();
        Stage.inst.onStageResized.Add(fixScreen);
        CoroutineManager.Singleton.delayedCall(0.2f, () =>
        {
            SceneLoader.Singleton.Load(OnLoadingCmp, OnProgrss);
        });
    }

    private UIGloader loader;
    private void LoadBgImage()
    {
        loader = new UIGloader();
        loader.autoSize = true;
        int random = Random.Range(1, 3);
        loader.SetUrl(string.Format("ui_loadingbg_0{0}/ui_loadingbg_0{0}", random), false);
        view.AddChildAt(loader, 0);
        fixScreen();
    }

    private void fixScreen()
    {
        if (loader == null || loader.texture == null)
            return;
        float scaleW = 1.0f * GRoot.inst.root.width / loader.texture.width;
        float scaleH = 1.0f * GRoot.inst.root.height / loader.texture.height;
        if (scaleW >= scaleH)
        {
            loader.SetScale(scaleW, scaleW);
        }
        else if (scaleW < scaleH)
        {
            loader.SetScale(scaleH, scaleH);
        }
        //让loader处于逻辑屏幕正中心
        loader.x = (GRoot.inst.root.width - loader.width * loader.scaleX) / 2;
        loader.y = (GRoot.inst.root.height - loader.height * loader.scaleY) / 2;
    }

    private void OnProgrss(float progress)
    {
        loadingWin.m_loadingBar.fillAmount = progress;
    }

    private void OnLoadingCmp()
    {
        Close();
        /*Tween tw = DOTween.To(() => loadingWin.m_loadingBar.fillAmount, x => loadingWin.m_loadingBar.fillAmount = x, 1.0f, 2);
        tw.OnComplete(()=> {
            GameManager.Singleton.changeState(GameState.MainCity);
        });*/
    }

    protected override void OnClose()
    {
        Stage.inst.onStageResized.Remove(fixScreen);
        base.OnClose();
        //loader.Dispose();
        loader = null;
    }

}