using DG.Tweening;
using UnityEngine;

public class HurtFlyState : HurtBaseState
{

    public float midH = 1.5f;

    public float dis = 3.0f;

    public float smallDis = 1.5f;

    //躺地上的时间
    private float lieTime = 0.7f;

    //击飞时间
    private float flyTime = 0.3f;

    //回来的时间
    private float backTime = 0.3f;

    private Tween tween;

    public override void OnEnter()
    {
        base.OnEnter();
        Debug.Log("enter hurt knock");
        //击飞
        float nowH = mActor.TransformExt.position.y;
        mActor.GetActionManager().PlayCommonAnimation(AniName.jifei.ToString());
        float nowDis = dis;
        if (HurtState == HurtSubState.SmallFly)
            nowDis = smallDis;
        Vector3 targetPos = mActor.OriginPos - nowDis * mActor.OriginDir;
        Vector3 startPos = mActor.TransformExt.position;
        Vector3 mid = Vector3.Lerp(startPos, targetPos, 0.5f);
        mid.y = midH;
        Vector3[] path = new Vector3[] { startPos, mid, targetPos };
        tween = mActor.TransformExt.DOPath(path, flyTime, PathType.CatmullRom, PathMode.Full3D, 5);
        tween.SetEase(Ease.Linear);
        tween.OnComplete(OnFlyCmp);
    }

    private long lieCoroId;
    private void OnFlyCmp()
    {
        //趟地
        lieCoroId = CoroutineManager.Singleton.delayedCall(lieTime, () =>
        {
            //往回跑
			mActor.GetActionManager().PlayCommonAnimation(AniName.move.ToString());
            tween = mActor.TransformExt.DOMove(mActor.OriginPos, backTime);
            tween.OnComplete(OnBackCmp);
        });
    }

    private void OnBackCmp()
    {
        mActor.changeState(ActorState.idle);
    }

    public override void OnReEnter()
    {
        Logger.err("击飞状态不可重入");
    }

    public override void OnLeave()
    {
        if (tween != null && tween.IsActive())
            tween.Kill();
        CoroutineManager.Singleton.stopCoroutine(lieCoroId);
    }

}
