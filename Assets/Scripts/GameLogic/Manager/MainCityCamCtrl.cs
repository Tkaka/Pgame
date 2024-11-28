using FairyGUI;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Camera))]
public class MainCityCamCtrl : SingletonBehaviour<MainCityCamCtrl>
{
    [Header("进入地图时动画虚拟相机")]
    public GameObject vCam;

    [Header("缩放时相机角度基准")]
    public float initAngle = 28;
    [Header("缩放时相机角度增量曲线")]
    public AnimationCurve pinchAngleCurve = new AnimationCurve(new Keyframe(0, -10f), new Keyframe(1, 10f));
    [Header("缩放时相机高度基准")]
    public float initPosY = 17f;
    [Header("缩放时相机高度增量曲线")]
    public AnimationCurve pinchPosCurve = new AnimationCurve(new Keyframe(0, -6f), new Keyframe(1, 0f));

    [Header("拖动时参考点")]
    public Vector3 dragControlPos = Vector3.zero;
    [Header("计算距离时的参考点偏移")]
    public Vector3 dragOffsetCenterPos = Vector3.zero;
    [Header("拖动时离最大参考点距离")]
    public float maxDragDeltaDistance = 30f;

    [Header("旋转时相机旋转中心距离")]
    public float rotateRadiua = 15f;

    [Header("拖动灵敏度")]
    public float easeMove = 0.01f;
    [Header("缩放灵敏度")]
    public float easePinch = 10f;
    [Header("旋转灵敏度")]
    public float easeRotaion = -1f;

    private GObject touchHolder;
    private SwipeGesture swipeGesture;
    private PinchGesture pinchGesture;
    private RotationGesture rotationGesture;

    private int pinchedTime;
    public void SetTouchHolder(GObject go)
    {
        touchHolder = go;
        vCam.SetActive(false);

        swipeGesture = new SwipeGesture(touchHolder);
        swipeGesture.onMove.Add(OnSwipeMove);

        pinchGesture = new PinchGesture(touchHolder);
        pinchGesture.onAction.Add(OnPinch);

        //rotationGesture = new RotationGesture(touchHolder);
        //rotationGesture.onAction.Add(OnRotate);

        pinchedTime = 0;
    }

    public void PlayCameraAni(bool value)
    {
        if (vCam == null)
            return;

        vCam.SetActive(value);
        if (!value)
            return;

        var cart = vCam.GetComponentInChildren<Cinemachine.CinemachineDollyCart>();
        if (cart != null)
            cart.m_Position = 0;
    }

    private void OnRotate(EventContext context)
    {
        RotationGesture gesture = (RotationGesture)context.sender;
        rotationCalculate(gesture.delta);
    }

    private void rotationCalculate(float delta)
    {
        var cam = Camera.main.transform;
        var forward = cam.forward;

        var center = forward * rotateRadiua + cam.position;
        cam.RotateAround(center, cam.up, delta * easeRotaion);
        cam.LookAt(center);
    }

    private void OnPinch(EventContext context)
    {
        PinchGesture gesture = (PinchGesture)context.sender;
        pinchCalculate(gesture.delta);
    }

    private void pinchCalculate(float delta)
    {
        var cam = Camera.main;
        float fov = -delta * easePinch + cam.fieldOfView;
        cam.fieldOfView = Mathf.Clamp(fov, 15f, 45f);
        float time = (cam.fieldOfView - 15f) / (45f - 15f);

        var trans = cam.transform;
        var rot = trans.localRotation.eulerAngles;
        rot.x = initAngle + pinchAngleCurve.Evaluate(time);
        var pos = trans.position;
        pos.y = initPosY + pinchPosCurve.Evaluate(time);

        pinchedTime++;
        if (pinchedTime > 10)
        {
            DOTween.Kill(trans);
            trans.position = pos;
            trans.localRotation = Quaternion.Euler(rot);
        }
        else
        {
            //防止首次位置对不上的抖动
            trans.DOMove(pos, 0.3f);
            trans.DOLocalRotate(rot, 0.3f);
        }
    }

    protected override void Update()
    {

        Debug.DrawLine(Camera.main.transform.position, Camera.main.transform.forward * 50 + Camera.main.transform.position);
    }

    private void OnSwipeMove(EventContext context)
    {
        SwipeGesture gesture = (SwipeGesture)context.sender;
#if UNITY_EDITOR
        if (Input.GetKey(KeyCode.LeftShift))
        {
            pinchCalculate(gesture.delta.x * 0.01f);
            //rotationCalculate(gesture.delta.y * 0.1f);
            return;
        }
#endif

        Vector3 touchDir = new Vector3();
        touchDir.x = gesture.delta.x * easeMove;
        touchDir.y = -gesture.delta.y * easeMove;
        Vector3 pos = TransformExt.position;

        //取当前相机正前方方向
        Vector3 forward = Camera.main.transform.forward;
        forward.y = 0;
        pos -= forward.normalized * touchDir.y;

        //取当前相机正右方方向
        Vector3 right = Camera.main.transform.right;
        right.y = 0;
        pos -= right.normalized * touchDir.x;

        //判断投影距离是否越界
        if (Vector2.Distance(new Vector2(pos.x + dragOffsetCenterPos.x, pos.z + dragOffsetCenterPos.z), new Vector2(dragControlPos.x, dragControlPos.z)) < maxDragDeltaDistance)
            TransformExt.position = pos;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        if (swipeGesture != null)
        {
            swipeGesture.onMove.Remove(OnSwipeMove);
            swipeGesture.Dispose();
            swipeGesture = null;
        }
        if (pinchGesture != null)
        {
            pinchGesture.onAction.Remove(OnPinch);
            pinchGesture.Dispose();
            pinchGesture = null;
        }
        if(rotationGesture != null)
        {
            rotationGesture.onAction.Remove(OnRotate);
            rotationGesture.Dispose();
            rotationGesture = null;
        }
    }
}
