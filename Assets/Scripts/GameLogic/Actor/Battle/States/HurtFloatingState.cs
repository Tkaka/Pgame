using UnityEngine;

public class HurtFloatingState : HurtBaseState
{

    // S=Vo*t+1/2*a*t²
    // Vt＝Vo+a*t
    //最大高度
    private float maxH = 14.0f;
    //力量作用时间(毫秒)
    private float time = 0.1f;
    //力矩加速度
    private float a = 14;
    //重力加速度
    private float g = -10.7f;
    //当前速度
    private float curSpeed;

    private float originY;

    private float enterTime;

    private bool isFinish = false;

    public override void SetOwner(Actor actor, HurtSubState subState)
    {
        base.SetOwner(actor, subState);
        originY = actor.OriginPos.y;
    }

    public override void OnEnter()
    {
        isFinish = false;
        enterTime = Time.time;
        //Debug.Log("OnEnter enterTime:" + enterTime);
        mActor.GetActionManager().Stop();
        mActor.GetActionManager().PlayCommonAnimation(AniName.fukong.ToString());
    }

    public override void OnReEnter()
    {
        isFinish = false;
        enterTime = Time.time;
        //Debug.Log("OnReEnter enterTime:" + enterTime);
        mActor.GetActionManager().Stop();
        mActor.GetActionManager().PlayCommonAnimation(AniName.fukong.ToString());
        if (curSpeed < 0)
            curSpeed = 0;
    }

    public override void OnUpdate()
    {
        if (isFinish)
            return;
        float nowTime = Time.time;
        //Debug.Log("OnUpdate nowTime:" + enterTime);
        //上升
        if (nowTime - enterTime < time)
        {
            float a1 = (a + g);
            curSpeed = curSpeed + a1 * Time.deltaTime;
            float h = curSpeed * Time.deltaTime + 0.5f * a1 * a1 * Time.deltaTime;
            float curY = mActor.TransformExt.position.y;
            if (curY + h > maxH)
                mActor.TransformExt.setY(maxH);
            else
                mActor.TransformExt.setY(curY + h);
            //Debug.Log("up:" + curSpeed + " _ " + (curY + h));
        }
        //下降
        else
        {
            curSpeed = curSpeed + g * Time.deltaTime;
            float h = curSpeed * Time.deltaTime + 0.5f * g * Time.deltaTime * Time.deltaTime;
            float curY = mActor.TransformExt.position.y;
            if (curY + h < originY)
            {
                isFinish = true;
                mActor.TransformExt.setY(originY);
                mActor.GetActionManager().PlayCommonAnimation(AniName.jidao.ToString(), OnAniCmp);
            }
            else
            {
                mActor.TransformExt.setY(curY + h);
            }
            //Debug.Log("down:" + curSpeed + " _ " + (curY + h) + "_oY:" + originY);
        }
    }

    private void OnAniCmp(int key)
    {
        mActor.changeState(ActorState.idle);
    }

    public override void OnLeave()
    {
        curSpeed = 0;
        isFinish = false;
        mActor.TransformExt.setY(originY);
    }

}


