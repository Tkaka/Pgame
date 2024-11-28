using System;
using System.Runtime.InteropServices;
using UnityEngine;
using FairyGUI;

public class LobbyTouch : MonoBehaviour
{
    private float mCritDist;
    private const float missTouchTolerance = 7.07f;
    public Camera mMainCamera;
    public GameObject mRootGameObject;
    public float mWorldZoom = 1f;
    public Vector3 positionInitWld = Vector3.zero;
    private Vector3 posMoveCenter = Vector3.zero;
    public Vector2 prevTouchPosition = Vector2.zero;
    public Vector3 requestCameraPos = Vector3.zero;
    public Vector3 requestCameraRot = Vector3.zero;
    public Vector3 requestWorldRot = Vector3.zero;
    public float requestWorldZoom;
    private LobbyTouchStat status;

    private void AddStat(LobbyTouchStat add)
    {
        this.status |= add;
    }

    public Vector2 CorrectTouchDelta(Gesture gesture)
    {
        float f = Time.deltaTime / gesture.deltaTime;
        if (float.IsNaN(f) || float.IsInfinity(f))
        {
            f = 1f;
        }
        return (Vector2)(gesture.deltaPosition * f);
    }

    [ContextMenu("Refresh From Request")]
    public void Excute()
    {
        this.mWorldZoom = this.requestWorldZoom;
        this.MoveCameraByAbs(this.requestCameraPos);
        this.RotateCameraByAbs(this.requestCameraRot);
        this.RotateWorldByAbs(this.requestWorldRot);
    }

    private IntersectResult.IntersectType IntersectRayPlane(Ray ray, Plane pl, ref IntersectResult result, float tol = 1E-07f)
    {
        float f = Vector3.Dot(pl.normal, ray.direction);
        result.Reset();
        if (Mathf.Abs(f) > tol)
        {
            Vector3 vector = (Vector3)(pl.normal * pl.distance);
            Vector3 lhs = vector - ray.origin;
            result.param = Vector3.Dot(lhs, pl.normal) / f;
            if (result.param > tol)
            {
                result.position = ray.origin + ((Vector3)(ray.direction * result.param));
                return IntersectResult.IntersectType.IntersectNormal;
            }
        }
        return IntersectResult.IntersectType.IntersectNone;
    }

    private bool IsStatOn(LobbyTouchStat check)
    {
        return ((this.status & check) == check);
    }

    private bool IsTouchAvailable()
    {
        return true;
        //LobbyScene component = GlobalGOHolder.MainGameObject.GetComponent<LobbyScene>();
        //return component?.IsLobbyTouchAvailable;
    }

    private void MoveCameraByAbs(Vector3 pos)
    {
        if (this.mMainCamera != null)
        {
            this.mMainCamera.transform.position = pos;
        }
    }

    private void MoveWorldByAbs(Vector3 pos)
    {
        if (this.mRootGameObject != null)
        {
            this.mRootGameObject.transform.position = pos;
        }
    }

    private void OnDestroy()
    {
        //EasyTouch.On_TouchUp -= new EasyTouch.TouchUpHandler(this.OnTouchUp);
        //EasyTouch.On_TouchStart -= new EasyTouch.TouchStartHandler(this.OnTouchStart);
        //EasyTouch.On_TouchDown -= new EasyTouch.TouchDownHandler(this.OnTouchDown);
        //EasyTouch.On_PinchIn -= new EasyTouch.PinchInHandler(this.OnPinchIn);
        //EasyTouch.On_PinchOut -= new EasyTouch.PinchOutHandler(this.OnPinchOut);
        //EasyTouch.On_Twist -= new EasyTouch.TwistHandler(this.OnTwist);
    }

    private void OnPinchIn(Gesture gesture)
    {
        if (this.IsTouchAvailable())
        {
            this.ZoomWorld(gesture.deltaPinch);
        }
    }

    private void OnPinchOut(Gesture gesture)
    {
        if (this.IsTouchAvailable())
        {
            this.ZoomWorld(-gesture.deltaPinch);
        }
    }

    private void OnPinch(EventContext context)
    {
        if (this.IsTouchAvailable())
        {
            PinchGesture gesture = (PinchGesture)context.sender;
            this.ZoomWorld(gesture.delta);
        }
    }


    private void OnTouchDown(EventContext context)
    {
        SwipeGesture gesture = (SwipeGesture)context.sender;
        if (this.IsStatOn(LobbyTouchStat.OnTouch))
        {
            float num = Vector2.Distance(gesture.position, this.prevTouchPosition);
            if ((this.IsStatOn(LobbyTouchStat.MoveShip) || (num >= 7.07f)) && ((this.mMainCamera != null) && (gesture.delta.magnitude > 0f)))
            {
                Vector3 position = this.mMainCamera.WorldToScreenPoint(this.posMoveCenter);
                Vector2 vector2 = gesture.position - this.prevTouchPosition;
                position.x += vector2.x;
                position.y += vector2.y;
                Vector3 vector3 = this.mMainCamera.ScreenToWorldPoint(position);
                Vector3 posMoveCenter = this.posMoveCenter;
                Vector3 vector5 = (Vector3)(Quaternion.Euler(-this.mMainCamera.transform.eulerAngles) * (vector3 - posMoveCenter));
                posMoveCenter = this.mRootGameObject.transform.position;
                posMoveCenter.x += vector5.x;
                posMoveCenter.z += vector5.y;
                this.prevTouchPosition = gesture.position;
                if ((this.mCritDist < 1E-05f) || (Vector3.Distance(this.positionInitWld, posMoveCenter) < this.mCritDist))
                {
                    this.MoveWorldByAbs(posMoveCenter);
                }
                if (!this.IsStatOn(LobbyTouchStat.MoveShip))
                {
                    this.AddStat(LobbyTouchStat.MoveShip);
                }
            }
        }
    }

    private void OnTouchStart(EventContext context)
    {
        SwipeGesture gesture = (SwipeGesture)context.sender;
        this.AddStat(LobbyTouchStat.OnTouch);
        this.prevTouchPosition = gesture.position;
        if (this.mRootGameObject != null)
        {
            this.posMoveCenter = this.mRootGameObject.transform.position;
            IntersectResult result = new IntersectResult();
            Plane pl = new Plane(new Vector3(0f, 1f, 0f), new Vector3(0f, 0f, 0f));
            Ray ray = this.mMainCamera.ScreenPointToRay((Vector3)gesture.position);
            if (this.IntersectRayPlane(ray, pl, ref result, 1E-07f) == IntersectResult.IntersectType.IntersectNormal)
            {
                this.posMoveCenter = result.position;
            }
            this.RemoveStat(LobbyTouchStat.MoveShip);
        }
    }

    private void OnTouchUp()
    {
        this.RemoveStat(LobbyTouchStat.OnTouch);
        this.RemoveStat(LobbyTouchStat.MoveShip);
    }

    private void OnTwist(EventContext context)
    {
        RotationGesture gesture = (RotationGesture)context.sender;
        if (this.IsTouchAvailable())
        {
            this.RotateWorld(-gesture.delta * 3f);
        }
    }

    private void OnUpdateFirst(float val)
    {
        this.mWorldZoom = val;
        this.ZoomWorld(0f);
    }

    private void RemoveStat(LobbyTouchStat del)
    {
        this.status &= ~del;
    }

    private void RotateCameraByAbs(Vector3 rot)
    {
        if (this.mMainCamera != null)
        {
            this.mMainCamera.transform.rotation = Quaternion.Euler(rot);
        }
    }

    private void RotateWorld(float val)
    {
        if (this.mRootGameObject != null)
        {
            float num2 = val * 0.3f;
            Vector3 eulerAngles = this.mRootGameObject.transform.rotation.eulerAngles;
            eulerAngles.y += num2;
            this.RotateWorldByAbs(eulerAngles);
        }
    }

    private void RotateWorldByAbs(Vector3 rot)
    {
        if (this.mRootGameObject != null)
        {
            this.mRootGameObject.transform.rotation = Quaternion.Euler(rot);
        }
    }

    private void Start()
    {
        //EasyTouch.On_TouchUp += new EasyTouch.TouchUpHandler(this.OnTouchUp);
        //EasyTouch.On_TouchStart += new EasyTouch.TouchStartHandler(this.OnTouchStart);
        //EasyTouch.On_TouchDown += new EasyTouch.TouchDownHandler(this.OnTouchDown);
        //EasyTouch.On_PinchIn += new EasyTouch.PinchInHandler(this.OnPinchIn);
        //EasyTouch.On_PinchOut += new EasyTouch.PinchOutHandler(this.OnPinchOut);
        //EasyTouch.On_Twist += new EasyTouch.TwistHandler(this.OnTwist);

        GObject holder = GRoot.inst;
        SwipeGesture gesture1 = new SwipeGesture(holder);
        gesture1.onBegin.Add(OnTouchStart);
        //gesture1.onMove.Add(OnTouchDown);
        gesture1.onEnd.Add(OnTouchUp);
        

        PinchGesture gesture3 = new PinchGesture(holder);
        gesture3.onAction.Add(OnPinch);

        RotationGesture gesture4 = new RotationGesture(holder);
        gesture4.onAction.Add(OnTwist);

    }

    public void UpdateVar()
    {
        //this.mRootGameObject = GameObject.Find("Lobby(Ship)");
        //this.mMainCamera = GlobalGOHolder.MainCamera.GetComponent<Camera>();
        GameObject obj2 = GameObject.Find("Lobby(Ship)/LobbyCollision");
        if (obj2 != null)
        {
            Collider component = obj2.GetComponent<Collider>();
            if (component != null)
            {
                this.mCritDist = Mathf.Max(Mathf.Max(component.bounds.extents.x, component.bounds.extents.y), component.bounds.extents.z) * 2.5f;
                this.positionInitWld = this.mRootGameObject.transform.position;
            }
        }
    }

    private void ZoomWorld(float val)
    {
        this.mWorldZoom += val * 0.001f;
        this.mWorldZoom = Mathf.Clamp01(this.mWorldZoom);
        this.ZoomWorldByWorldZoom();
    }

    public void ZoomWorldByAbs(float val)
    {
        this.mWorldZoom = Mathf.Clamp01(val);
        this.ZoomWorldByWorldZoom();
    }

    private void ZoomWorldByWorldZoom()
    {
        if (this.mMainCamera != null)
        {
            float num7 = 2f + (9.62045f * this.mWorldZoom);
            float num8 = -7f + (-16.66291f * this.mWorldZoom);
            float num9 = 17f + (11.27557f * this.mWorldZoom);
            Vector3 eulerAngles = this.mMainCamera.transform.rotation.eulerAngles;
            eulerAngles.x = num9;
            this.RotateCameraByAbs(eulerAngles);
            Vector3 position = this.mMainCamera.transform.position;
            position.y = num7;
            position.z = num8;
            this.MoveCameraByAbs(position);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct IntersectResult
    {
        public float param;
        public Vector3 position;
        public void Reset()
        {
            this.param = 0f;
        }
        public enum IntersectType
        {
            IntersectNone,
            IntersectNormal,
            IntersectCoincident,
            IntersectDegenerate
        }
    }

    private enum LobbyTouchStat
    {
        None,
        OnTouch,
        MoveShip
    }
}