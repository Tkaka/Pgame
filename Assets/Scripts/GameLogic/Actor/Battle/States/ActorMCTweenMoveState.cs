using UnityEngine;
using DG.Tweening;

public class ActorMCTweenMoveState : ActorBaseState
{

    private Tween tween;

    private MCMoveParam pathParam;
    private ActorMC m_actorMc;

    public override void onEnter(object obj = null)
    {
        //base.onEnter(obj);
        m_actorMc = mOwner as ActorMC;
        if (m_actorMc == null)
        {
            Debug.LogError("角色为空");
            return;
        }
        m_actorMc.GetActionManager().PlayCommonAnimation(ActorState.move);
        StartMove(obj);
    }

    public override void onReEnter(object obj = null)
    {
        StartMove(obj);
    }

    private void StartMove(object obj)
    {
        pathParam = (MCMoveParam)obj;
        if (pathParam == null)
        {
            return;
        }

        //m_actorMc.setDirection(pathParam.dir);
        if(m_actorMc.ShowObj != null)
        {
            Vector3 oldDir = m_actorMc.ShowObj.transform.forward;
            m_actorMc.TweenDirection(pathParam.dir, Vector3.Angle(oldDir, pathParam.dir) * (0.2f / 180f));
        }
        else
        {
            m_actorMc.TweenDirection(pathParam.dir, 0.1f);
        }

        if (pathParam.oldPos.Equals(pathParam.targetPos))
        {
            ChangeToIdle();
        }
        else
        {

            tween = m_actorMc.TransformExt.DOMove(pathParam.targetPos, pathParam.dur);
            tween.SetEase(Ease.Linear);
            tween.OnComplete(ChangeToIdle);
        }
 
    }

    private void ChangeToIdle()
    {
        if (pathParam != null)
            m_actorMc.setDirection(pathParam.dir);
        m_actorMc.changeState(ActorState.idle);
        //if (m_actorMc.isActorType(ActorType.Player))
        //    GED.ED.dispatchEvent(EventID.PlayerMoveCmp);
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
        return ActorState.mc_move;
    }

}