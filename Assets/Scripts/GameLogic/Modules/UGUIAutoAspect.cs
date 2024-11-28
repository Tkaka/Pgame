using UnityEngine;
using UnityEngine.UI;

public class UGUIAutoAspect : BaseBehaviour
{
    public Canvas mCanvas;

    public CanvasScaler mCanvasScaler;

    //美术参考设计尺寸
    public const float ReferenceScreenWidth = 1280;
    public const float ReferenceScreenHeight = 720;

    protected override void Start()
    {
        Init();
    }

    public void Init()
    {
        mCanvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        mCanvasScaler.referenceResolution = new Vector2(ReferenceScreenWidth, ReferenceScreenHeight);
        // 强制更新一次canvas属性
        //mCanvasScaler.SendMessage("Update");
        AutoAspect();
    }

    private void AutoAspect()
    {
        float matchWidthOrHeight = 0f;
        float scaleW = Screen.width / ReferenceScreenWidth;
        float scaleH = Screen.height / ReferenceScreenHeight;
        if (scaleW > scaleH)
            matchWidthOrHeight = 1;
        else if (scaleW < scaleH)
            matchWidthOrHeight = 0;
        else
            matchWidthOrHeight = 0.5f;

        //matchWidthOrHeight = scaleW > scaleH ? 1 : 0;
        mCanvasScaler.matchWidthOrHeight = matchWidthOrHeight;
    }
}
