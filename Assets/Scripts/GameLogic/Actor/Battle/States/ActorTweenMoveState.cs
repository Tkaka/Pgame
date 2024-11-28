using UnityEngine;
using DG.Tweening;

public class ActorTweenMoveState : ActorBaseState
{

    private Tween tween;

    private PathParam pathParam;


    public override void onEnter(object obj = null)
    {
        base.onEnter(obj);
        StartMove(obj);
    }

    public override void onReEnter(object obj = null)
    {
        StartMove(obj);
    }

    private void StartMove(object obj)
    {
        pathParam = (PathParam)obj;
        if (pathParam == null)
        {
            return;
        }

        Vector3? lastPos = pathParam.GetLastPos();
        if (lastPos.HasValue)
        {
            if (tween != null && tween.IsActive())
                tween.Kill();
            float dur = pathParam.dur;
            if (pathParam.path.Count > 1)
            {
                float dis = GTools.distanceIgnoreY(lastPos.Value, mActor.TransformExt.position);
                if(dur <= 0)
                    dur = dis / mActor.Velocity;
                tween = mActor.TransformExt.DOPath(pathParam.path.ToArray(), dur, PathType.CatmullRom, PathMode.Full3D).SetLookAt(0.01f);
                tween.SetEase(Ease.Linear);
                tween.OnComplete(ChangeToIdle);
            }
            else
            {
                float dis = GTools.distanceIgnoreY(lastPos.Value, mActor.TransformExt.position);
                if (dur <= 0)
                    dur = dis / mActor.Velocity;
                tween = mActor.TransformExt.DOMove(lastPos.Value, dur);
                tween.SetEase(Ease.Linear);
                tween.OnComplete(ChangeToIdle);
            }
            mActor.OriginPos = lastPos.Value;
            mActor.OriginDir = pathParam.dir;
        }
        else
        {
            ChangeToIdle();
        }
    }

    private void ChangeToIdle()
    {
        if (pathParam != null)
            mActor.setDirection(pathParam.dir);
        mActor.changeState(ActorState.idle);
        if (mActor.isActorType(ActorType.Player))
            GED.ED.dispatchEvent(EventID.PlayerMoveCmp);
        GED.ED.dispatchEvent(EventID.ActorMoveCmp, mActor);
    }

    public override void onLeave(string stateKey)
    {
        base.onLeave(stateKey);
        pathParam = null;
        if (tween != null && tween.IsActive())
        {
            tween.Kill();
            tween = null;
        }
    }

    public override string getStateKey()
    {
        return ActorState.move;
    }

}