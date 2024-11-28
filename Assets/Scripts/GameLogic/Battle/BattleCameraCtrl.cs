using UnityEngine;
using DG.Tweening;
using FairyGUI;

public class BattleCameraCtrl : SingletonBehaviour<BattleCameraCtrl>
{

    private Camera mainCamera;

    private Tween tween;

    private Tween uiTween;

    private Vector3 uiOrgPos;

    private Vector3 sceneOrgPos;

    //振屏时间
    public float shakeDuration = 0.2f;

    //振屏力度
    public float shakeStrength = 0.3f;

    //振幅
    public int shakeVibrato = 50;

    //随机移动范围
    public float shakeRandom = 3.0f;

    protected override void Awake()
    {
        base.Awake();
        mainCamera = Camera.main;
    }

    public void ToogleMainCam(bool flag)
    {
        mainCamera.gameObject.SetActive(flag);
    }


    protected override void Start()
    {
        if(StageCamera.main != null)
            uiOrgPos = StageCamera.main.transform.position;
        sceneOrgPos = TransformExt.position;
    }

    public void Shake()
    {
        if (tween != null && tween.IsActive())
            tween.Kill();
        if (uiTween != null && uiTween.IsActive())
            uiTween.Kill();
        if (StageCamera.main != null)
            uiTween = StageCamera.main.DOShakePosition(shakeDuration, shakeStrength, shakeVibrato, shakeRandom);
        tween = TransformExt.DOShakePosition(shakeDuration, shakeStrength, shakeVibrato, shakeRandom);
        tween.OnComplete(OnShakeCmp);
    }

    private void OnShakeCmp()
    {
        if (StageCamera.main != null)
            StageCamera.main.transform.position = uiOrgPos;
        TransformExt.position = sceneOrgPos;
    }

}