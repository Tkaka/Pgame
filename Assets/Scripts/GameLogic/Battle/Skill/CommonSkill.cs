using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CommonSkill : Skill
{

    //  原始位置
    protected Vector3 mOldPosition;

    protected long flowCoroId;

    protected SimpleInterval mInterval = new SimpleInterval(true);

    protected override void doSkill()
    {
        base.doSkill();
        flowCoroId = CoroutineManager.Singleton.startCoroutine(RunSkillFlow());
    }

    protected bool addedBuff = false;
    protected virtual IEnumerator RunSkillFlow()
    {
        mOldPosition = Owner.TransformExt.position;
        skillFlow.SetOldPosition(Owner.TransformExt.position);
        SkillKeyFrame[] flows = SkillConfig.skillFlowList;
        for (int i = 0; i < flows.Length; i++)
        {
            if (flows[i].type == SkillKeyFrame.Type.MoveForward)
            {
                ChangeComboState(ComboStatus.RunForward);
                float moveDur = skillFlow.MoveForward(flows[i].ease);
                yield return new WaitForSeconds(moveDur);
                Owner.setDirection(skillDir);
            }
            else if (flows[i].type == SkillKeyFrame.Type.HeadEft)
            {
                playEffect(MountPointType.Head, SkillConfig.headEffect);
                ShowCloseShot();
                PlaySound();
            }
            else if (flows[i].type == SkillKeyFrame.Type.AtkEft)
            {
                playEffect(MountPointType.Attack, SkillConfig.attackEffect);
            }
            else if (flows[i].type == SkillKeyFrame.Type.PlayAni)
            {
                ChangeComboState(ComboStatus.UpHand);

                //如果是大招，隐藏其他角色
                if (closeShot && SkillConfig.hideOthers)
                {
                    ToggleOthersVisible(false);
                }

                //如果没有buff关键帧，则在播放动画的时候添加buff
                if (!SkillConfig.HasBuffKeyframe())
                {
                    addedBuff = true;
                    ViewUtils.Singleton.Show(ShowID, TimeNode.Buffkeyframe);
                }

                //播放伤害数字
                ViewUtils.Singleton.Show(ShowID, TimeNode.Hurt);

                //播放动画
                string aniName = SkillConfig.aniName.ToString();
                playAnimation(aniName, onActionFinish, onKeyAction);
                float aniLen = Owner.GetActionManager().GetAniLen(aniName);
                yield return new WaitForSeconds(aniLen);
            }
            else if (flows[i].type == SkillKeyFrame.Type.MoveBackward)
            {
                if (!addedBuff)
                {
                    addedBuff = true;
                    Logger.log(Owner.Name + "warning在技能最后才表现加buff");
                    ViewUtils.Singleton.Show(ShowID, TimeNode.Buffkeyframe);
                }

                //非大招在技能末尾表现加怒气
                if (!IsMasterSkill())
                    Owner.ViewPropertyMgr.ChangeProperty(PropertyType.Mp, Owner.getProperty(PropertyType.AtkGetMp));

                if (closeShot && SkillConfig.hideOthers)
                    ToggleOthersVisible(true);

                ViewUtils.Singleton.Show(ShowID, TimeNode.SkillCmp);
                float moveDur = skillFlow.MoveBackward(flows[i].ease);
                yield return new WaitForSeconds(moveDur);
                OnMoveBackward();
            }
        }
    }

    protected virtual void OnlyShowSelf()
    {
        Dictionary<long, Actor> actors = ActorManager.Singleton.Actors;
        if (actors != null && defenders != null)
        {
            foreach (long key in actors.Keys)
            {
                if (actors[key].isActorType(ActorType.Player))
                    continue;
                if (key != Owner.getActorId())
                    actors[key].ToggleVisible(false, false);
            }
        }
    }

    protected virtual void ToggleOthersVisible(bool flag)
    {
        Dictionary<long, Actor> actors = ActorManager.Singleton.Actors;
        if (actors != null && defenders != null)
        {
            foreach (long key in actors.Keys)
            {
                if (actors[key].isActorType(ActorType.Player))
                    continue;
                if (key != Owner.getActorId() && defenders.IndexOf(key) < 0)
                    actors[key].ToggleVisible(flag, false);
            }
        }
    }

    protected virtual void OnMoveBackward()
    {
        ChangeComboState(ComboStatus.Finish);
        Owner.getStateMachine().changeState(ActorState.idle);
    }

    protected virtual void ChangeComboState(ComboStatus status, Vector3? worldPos = null)
    {
        if (showComboTip)
            ComboCtrl.Singleton.ChangeState(cmdId, status, worldPos, showComboCircle);
    }

    protected override void onActionFinish(int key)
    {
        //do nothing
        if (!skillFlow.IsColMoveBack)
        {
            Debug.LogError("动作播放完毕时，ColMove还没有返回");
        }
    }

    protected Tween freezeTween;
    protected override void onKeyAction(int key)
    {
        SkillKeyFrame keyframe = SkillConfig.GetOtherKeyframe(key);
        if (keyframe.type == SkillKeyFrame.Type.Hurt)
        {

        }
        else if (keyframe.type == SkillKeyFrame.Type.Splash)
        {
            VirtualCameraMgr.Singleton.PlayCamEft();
        }
        else if (keyframe.type == SkillKeyFrame.Type.CloseShot)
        {

        }
        else if (keyframe.type == SkillKeyFrame.Type.HeadEft)
        {
            playEffect(MountPointType.Head, SkillConfig.headEffect);
            ShowCloseShot();
            PlaySound();
        }
        else if (keyframe.type == SkillKeyFrame.Type.AtkEft)
        {
            playEffect(MountPointType.Attack, SkillConfig.attackEffect);
        }
        else if (keyframe.type == SkillKeyFrame.Type.Circle)
        {
            if (isMannul)
                skillFlow.ShowCombo();
            else
                skillFlow.ShowAutoCombo();
        }
        else if (keyframe.type == SkillKeyFrame.Type.Bullet)
        {
            ShootBullet(keyframe);
        }
        else if (keyframe.type == SkillKeyFrame.Type.Thunder)
        {
            if (!string.IsNullOrEmpty(SkillConfig.handEffect))
            {
                FightManager.R.LoadGo(SkillConfig.handEffect, GetThunderPoint(keyframe.thunderPos));
            }
        }
        else if (keyframe.type == SkillKeyFrame.Type.MoveForward)
        {
            if (keyframe.ease != Ease.Unset)
                Owner.TransformExt.DOMove(skillPos, GameConfig.OffsetDuring).SetEase(keyframe.ease);
            else
                Owner.TransformExt.DOMove(skillPos, GameConfig.OffsetDuring);
        }
        else if (keyframe.type == SkillKeyFrame.Type.AddBuff)
        {
            if (addedBuff)
                Logger.err("有buff关键帧，不应该在之前添加过buff");
            //计算额外效果(技能主效果之后)
            ViewUtils.Singleton.Show(ShowID, TimeNode.Buffkeyframe);
        }
        else if (keyframe.type == SkillKeyFrame.Type.ColMove)
        {
            skillFlow.ColMove(keyframe);
        }
        else if(keyframe.type == SkillKeyFrame.Type.ShakeCamera)
        {
            VirtualCameraMgr.Singleton.Shake(SkillConfig.shakeCameraDur, SkillConfig.shakeCameraStrength, SkillConfig.shakeCameraVibrato, SkillConfig.shakeCameraDistance);
        }
        else if (keyframe.type == SkillKeyFrame.Type.FreezeFrame)
        {
            if (keyframe.FreezeTime > 0)
            {
                if (freezeTween != null && freezeTween.IsActive())
                    freezeTween.Kill();
                //float duration = keyframe.FreezeTime / Time.timeScale;
                freezeTween = DOTween.To(() => Time.timeScale, x => Time.timeScale = x, 0, keyframe.FreezeTime);
                freezeTween.SetUpdate(true);
                freezeTween.timeScale = Time.timeScale;
                freezeTween.SetLoops(2, LoopType.Yoyo);
                freezeTween.OnComplete(() =>
                {
                    Time.timeScale = FightManager.Singleton.GameSpeed;
                });
            }
        }
        else
        {
            Logger.err("CommonSkill:onKeyAction:无法识别的关键帧类型：" + keyframe.type);
        }
    }

    public override void stop()
    {
        base.stop();
        addedBuff = false;
        skillFlow.OnStop();
        CoroutineManager.Singleton.stopCoroutine(flowCoroId);
        if (freezeTween != null && freezeTween.IsActive())
            freezeTween.Kill();
        //大招完毕之后，重新倒计时
        if(IsMasterSkill() && FightManager.Singleton.IsStateOf(FightState.MannulAttack))
           BattleCDCtrl.Singleton.PauseToggle(false);
    }

}
