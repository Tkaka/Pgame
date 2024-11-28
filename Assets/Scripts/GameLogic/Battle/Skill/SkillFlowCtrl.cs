using DG.Tweening;
using UnityEngine;

public class SkillFlowCtrl
{
    private Skill skill;

    private Actor actor;

    private Vector3 oldPosition;

    private bool IsComboShown = false;

    public SkillFlowCtrl(Skill skill)
    {
        this.skill = skill;
        this.actor = skill.Owner;
    }

    public void SetOldPosition(Vector3 pos)
    {
        oldPosition = pos;
    }

    private float moveDur = 0.4f;
    public float MoveForward(Ease ease)
    {
        Vector3 dir = skill.skillPos - actor.TransformExt.position;
        float dis = Vector3.Distance(Vector3.zero, dir);
        moveDur = dis / GameConfig.Velocity;
        actor.setDirection(dir);
        if (ease != Ease.Unset)
            tween = actor.TransformExt.DOMove(skill.skillPos, moveDur).SetEase(ease);
        else
            tween = actor.TransformExt.DOMove(skill.skillPos, moveDur);
        actor.GetActionManager().PlayCommonAnimation("move", null, 2);
        return moveDur;
    }

    public float MoveBackward(Ease ease)
    {
        KillTween();
        Vector3 dir = oldPosition - actor.TransformExt.position;
        actor.setDirection(dir);
        if (ease != Ease.Unset)
            tween = actor.TransformExt.DOMove(oldPosition, moveDur).SetEase(ease);
        else
            tween = actor.TransformExt.DOMove(oldPosition, moveDur);
        actor.GetActionManager().PlayCommonAnimation("move", null, 2);
        return moveDur;
    }

    private Tween tween;
    public bool IsColMoveBack = true;
    /// <summary>
    /// 列向移动
    /// </summary>
    /// <param name="ease"></param>
    /// <returns></returns>
    public void ColMove(SkillKeyFrame keyframe)
    {
        KillTween();
        IsColMoveBack = false;
        Vector3 oldPos = actor.TransformExt.position;
        Vector3 targetPos = actor.TransformExt.position + keyframe.ColMoveDis * actor.TransformExt.forward;
        tween = actor.TransformExt.DOMove(targetPos, keyframe.ColMoveTime).SetEase(keyframe.ease).OnComplete(() =>
        {
            if (keyframe.Back)
            {
                tween = actor.TransformExt.DOMove(oldPos, keyframe.ColMoveTime).SetEase(keyframe.ease).OnComplete(() =>
                {
                    IsColMoveBack = true;
                });
            }
            else
            {
                IsColMoveBack = true;
            }
        });
    }

    private void KillTween()
    {
        if (tween != null && tween.IsActive())
            tween.Kill();
    }

    public void ShowCombo()
    {
        if (IsComboShown)
            return;
        IsComboShown = true;
        if (skill.showComboTip && skill.showComboCircle)
        {
            Actor defender = ActorManager.Singleton.Get(skill.TargetID);
            if (defender != null)
                ComboCtrl.Singleton.ChangeState(skill.cmdId, ComboStatus.Circle, defender.TransformExt.position);
        }
    }

    public void ShowAutoCombo()
    {
        if (!skill.showComboTip || IsComboShown)
            return;
        IsComboShown = true;
        ComboType comboType = skill.autoComboType;
        float time = ComboCtrl.Singleton.GetTime(comboType);
        CoroutineManager.Singleton.delayedCall(time, () =>
        {
            Actor defender = ActorManager.Singleton.Get(skill.TargetID);
            if (defender != null)
            {
                Vector3 targetPos = WinMgr.Singleton.WorldToScreen(defender.TransformExt.position, 1);
                ComboCtrl.Singleton.CheckLevel(time, targetPos);
            }
        });
    }

    public void OnStop()
    {
        IsComboShown = false;
        IsColMoveBack = true;
        KillTween();
    }

}
