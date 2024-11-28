using FairyGUI;
using UI_Loading;
using UnityEngine;

public class UpdateLoadingWindow : SingletonWindow<UpdateLoadingWindow>
{
    private UI_UpdateLoadingWindow win;
    public override void OnOpen()
    {
        win = getUiWindow<UI_UpdateLoadingWindow>();
        ChangeProgress(-1f);
        ShowTip("玩命加载中...");

        Stage.inst.onStageResized.Add(fixScreen);
        loadBgImage();
    }

    public void ShowTip(string tip)
    {
        win.m_txtTip.text = tip;
    }

    public void ChangeProgress(float progress)
    {
        if (progress < 0)
        {
            win.m_preBar.visible = false;
        }
        else
        {
            win.m_preBar.visible = true;
            win.m_loadingBar.fillAmount = progress;
        }
    }

    private UIGloader loader;
    private void loadBgImage()
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

    protected override void OnClose()
    {
        Stage.inst.onStageResized.Remove(fixScreen);
        base.OnClose();
        loader = null;
    }
}
