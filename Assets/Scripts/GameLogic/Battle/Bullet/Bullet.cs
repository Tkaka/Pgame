using UnityEngine;
using DG.Tweening;

public class Bullet : BaseBehaviour
{

    protected Vector3 mTargetPos = Vector3.zero;

    protected Tweener mTweener;

    protected float flySpeed;

    protected override void Start()
    {
        base.Start();
    }

    public virtual void setSkill(float speed, Vector3  targetPos)
    {
        flySpeed = speed;
        mTargetPos = targetPos;
        Fly();
    }

    protected virtual void Fly()
    {
        float flyTime = GetFlyTime();
        mTweener = TransformExt.DOMove(mTargetPos, flyTime);
        mTweener.OnComplete(OnFlyCmp);
        mTweener.SetEase(Ease.Linear);
    }

    protected virtual void OnFlyCmp()
    {
        //Res.Singleton.destroy(gameObject);
        Destroy(gameObject);
    }

    protected virtual float GetFlyTime()
    {
        float res = 0;
        if (flySpeed <= 0)
        {
            Logger.err("除0了 BulletId:");
            return res;
        }
        float dis = Vector3.Distance(mTargetPos, TransformExt.position);
        res = dis / flySpeed;
        return res;
    }

    /// <summary>
    /// 当销毁对象时
    /// </summary>
    protected override void OnDestroy()
    {
        if (mTweener != null && mTweener.IsActive())
            mTweener.Kill();
        mTweener = null;
        base.OnDestroy();
    }

}