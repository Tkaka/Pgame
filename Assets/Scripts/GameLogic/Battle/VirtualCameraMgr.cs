
using Cinemachine;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using FairyGUI;

public class VirtualCameraMgr : MonoBehaviour
{
    private CinemachineVirtualCamera CurCamera;

    private CinemachineBrain vCameraBrain;

    private CinemachineVirtualCamera playerCamera;

    private List<CinemachineVirtualCamera> vCameras = new List<CinemachineVirtualCamera>();

    private Camera sceneCamera;

    private Tween tween;

    private Tween uiTween;

    private Vector3 uiOrgPos;

    private Vector3 curCamOrgPos;

    private float originCamDur;

    [Tooltip("振屏时间")]
    public float shakeDuration = 0.2f;

    [Tooltip("振屏力度")]
    public float shakeStrength = 0.3f;

    [Tooltip("振幅")]
    public int shakeVibrato = 50;

    [Tooltip("随机移动范围")]
    public float shakeRandom = 3.0f;

    /// <summary>
    /// 镜头特效挂点
    /// </summary>
    public GameObject CamEft;

    public static VirtualCameraMgr Singleton { get; private set; }

    void Awake()
    {
        Singleton = this;
        Camera mainCam = Camera.main;
        vCameraBrain = mainCam.GetComponent<CinemachineBrain>();
        originCamDur = vCameraBrain.m_DefaultBlend.m_Time;
        Transform child = mainCam.transform.GetChild(0);
        if (child != null)
        {
            sceneCamera = child.GetComponent<Camera>();
            //sceneCamera.fieldOfView = mainCam.fieldOfView;
        }
        else
        {
            Logger.err("找不到场景相机");
        }
        playerCamera = transform.Find("PlayerCam").GetComponent<CinemachineVirtualCamera>();
        FindCameras();
        CurCamera = vCameras[0];
        curCamOrgPos = CurCamera.transform.position;
    }

    public void SetCameraEase(float dur)
    {
        vCameraBrain.m_DefaultBlend.m_Time = dur;
    }

     void Start()
    {
        if (StageCamera.main != null)
            uiOrgPos = StageCamera.main.transform.position;
    }

    protected void Update()
    {
        if (Camera.main != null && sceneCamera != null)
            sceneCamera.fieldOfView = Camera.main.fieldOfView;
    }

    public void Shake()
    {
        //curCamOrgPos = CurCamera.transform.position;
        if (tween != null && tween.IsActive())
            tween.Kill();
        if (uiTween != null && uiTween.IsActive())
            uiTween.Kill();
        if (StageCamera.main != null)
            uiTween = StageCamera.main.DOShakePosition(shakeDuration, shakeStrength, shakeVibrato, shakeRandom);
        tween = CurCamera.transform.DOShakePosition(shakeDuration, shakeStrength, shakeVibrato, shakeRandom);
        tween.OnComplete(OnShakeCmp);
    }

    public void Shake(float dur, float str, int vib, float dis)
    {
        if (tween != null && tween.IsActive())
            tween.Kill();
        if (uiTween != null && uiTween.IsActive())
            uiTween.Kill();
        if (StageCamera.main != null)
            uiTween = StageCamera.main.DOShakePosition(dur, str, vib, dis);
        tween = CurCamera.transform.DOShakePosition(dur, str, vib, dis);
        tween.OnComplete(OnShakeCmp);
    }

    private long camEftId;
    public void PlayCamEft()
    {
        if (CamEft == null)
            return;
        if (camEftId > 0)
            CoroutineManager.Singleton.stopCoroutine(camEftId);
        CamEft.SetActive(true);
        CoroutineManager.Singleton.delayedCall(1.0f, () =>
        {
            CamEft.SetActive(false);
            camEftId = 0;
        });
    }

    private void OnShakeCmp()
    {
        if (StageCamera.main != null)
            StageCamera.main.transform.position = uiOrgPos;
        CurCamera.transform.position = curCamOrgPos;
    }

    public void ChangeCamera(int index)
    {
        vCameraBrain.m_DefaultBlend.m_Time = originCamDur;
        if (index >= 0 && index < vCameras.Count)
        {
            playerCamera.Priority = 1;
            for (int i = 0; i < vCameras.Count; i++)
            {
                if (i == index)
                {
                    //vCameras[i].gameObject.SetActive(true);
                    vCameras[i].Priority = 50;
                    CurCamera = vCameras[i];
                    curCamOrgPos = CurCamera.transform.position;
                    //sceneCamera.fieldOfView = vCameras[i].m_Lens.FieldOfView;
                }
                else
                {
                    //vCameras[i].gameObject.SetActive(false);
                    vCameras[i].Priority = 10;
                }
            }
        }
    }

    public void ChangeToPlayerCamera(Transform lookAt, Transform follow)
    {
        vCameraBrain.m_DefaultBlend.m_Time = originCamDur;
        playerCamera.LookAt = lookAt;
        playerCamera.Follow = follow;
        playerCamera.Priority = 100;
        //FightManager.Singleton.ChangeCameraCulling(false, originCamDur);
        //sceneCamera.fieldOfView = playerCamera.m_Lens.FieldOfView;
    }

    public CinemachineVirtualCamera GetCurCamera()
    {
        return CurCamera;
    }

    private void FindCameras()
    {
        int index = 0;
        bool flag = true;
        string name = "";
        CinemachineVirtualCamera cam;
        while (flag)
        {
            name = "MainCam" + index;
            Transform trigger = transform.Find(name);
            if (trigger != null)
            {
                cam = trigger.GetComponent<CinemachineVirtualCamera>();
                if (cam != null)
                {
                    vCameras.Add(cam);
                }
                else
                {
                    Logger.err("SpawnerManager:findTriggers:不能找到该节点" + name);
                }
                index++;
            }
            else
            {
                flag = false;
            }
        }
    }


    private int oldMainCulling = 0;
    private int oldSceneCulling = 0;
    public void ToggleCameraCulling(bool flag)
    {
        if (flag)
        {
            Camera camera = Camera.main;
            if (camera != null)
                camera.cullingMask = oldMainCulling;
            if (sceneCamera != null)
                sceneCamera.cullingMask = oldSceneCulling;
        }
        else
        {
            Camera camera = Camera.main;
            if (camera != null)
            {
                oldMainCulling = camera.cullingMask;
                camera.cullingMask = 0;
            }
            if (sceneCamera != null)
            {
                oldSceneCulling = sceneCamera.cullingMask;
                sceneCamera.cullingMask = 0;
            }
        }
    }

}
