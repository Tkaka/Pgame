
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorSkill : CommonSkill
{

    private List<EditorActor> defenders = new List<EditorActor>();

    public List<ActorBehavior> defenderList = null;

    public ActorBehavior target;

    public int targetCol = 0;

    public override void Init(SkillConfig config, Actor owner)
    {
        SkillConfig = config;
        mTemplateId = config.skillId;
        Owner = owner;
        SkillLevel = 1;
        int[] arr = AttackUtils.GetSkillHurt(SkillLevel, mSkillBean);
        SkillEffectPer = GTools.ScaleInt2LNumber(arr[0]);
        SkillEffectFixed = arr[1];
        skillFlow = new SkillFlowCtrl(this);
    }

    protected override IEnumerator RunSkillFlow()
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
                playEffect(MountPointType.Hand, SkillConfig.handEffect);
            }
            else if (flows[i].type == SkillKeyFrame.Type.PlayAni)
            {
                ChangeComboState(ComboStatus.UpHand);

                //如果没有buff关键帧，则在播放动画的时候添加buff
                if (!SkillConfig.HasBuffKeyframe())
                {
                    addedBuff = true;
                    ViewUtils.Singleton.Show(ShowID, TimeNode.Buffkeyframe);
                }
                
                //如果是大招，隐藏其他角色
                if (SkillConfig.hideOthers)
                {
                    if (SkillConfig.onlyShowSelfTime > 0)
                    {
                        OnlyShowSelf();
                        CoroutineManager.Singleton.delayedCall(SkillConfig.onlyShowSelfTime, () =>
                        {
                            ToggleActorsVisible(false, true);
                        });
                    }
                    else
                    {
                        ToggleActorsVisible(false, true);
                    }
                }

                //播放伤害数字
                CoroutineManager.Singleton.startCoroutine(ShowHurtEffect());
                //ViewUtils.Singleton.Show(ShowID, TimeNode.Hurt);

                //播放动画
                string aniName = Enum.GetName(typeof(AniName), SkillConfig.aniName);
                playAnimation(aniName, onActionFinish, onKeyAction);
                float aniLen = Owner.GetActionManager().GetAniLen(aniName);
                yield return new WaitForSeconds(aniLen);
            }
            else if (flows[i].type == SkillKeyFrame.Type.MoveBackward)
            {
                if (closeShot && SkillConfig.hideOthers)
                    ToggleOthersVisible(true);
                ViewUtils.Singleton.Show(ShowID, TimeNode.SkillCmp);
                float moveDur = skillFlow.MoveBackward(flows[i].ease);
                yield return new WaitForSeconds(moveDur);
                OnMoveBackward();
            }
        }
    }

    protected override void ToggleOthersVisible(bool flag)
    {
        foreach (ActorBehavior behavior in defenderList)
        {
            if (behavior == null)
                continue;
            behavior.gameObject.SetActive(true);
        }
    }

    protected override void OnlyShowSelf()
    {
        foreach (ActorBehavior behavior in defenderList)
        {
            if (behavior == null)
                continue;
            behavior.gameObject.SetActive(false);
        }
    }

    protected virtual void ToggleActorsVisible(bool otherFlag, bool skillFlag)
    {
        Dictionary<long, Actor> actors = ActorManager.Singleton.Actors;
        if (actors != null && defenders != null)
        {
            foreach (long key in actors.Keys)
            {
                if (actors[key].isActorType(ActorType.Player))
                    continue;
                if (key != Owner.getActorId() && defenders.IndexOf(actors[key] as EditorActor) < 0)
                    actors[key].ToggleVisible(otherFlag, false);
                else
                    actors[key].ToggleVisible(skillFlag, false);
            }
        }
    }

    protected override void ShowCloseShot()
    {
        if (SkillConfig.closeShot != null)
        {
            SkillConfig.closeShot.gameObject.SetActive(true);
            VirtualCameraMgr.Singleton.SetCameraEase(0);
            SkillConfig.closeShot.Play();
            CoroutineManager.Singleton.delayedCall((float)SkillConfig.closeShot.duration, () =>
            {
                SkillConfig.closeShot.gameObject.SetActive(false);
            });
        }
    }

    protected override void ShootBullet(SkillKeyFrame keyframe, Vector3? pos = null)
    {
        Vector3 startPos;
        if (pos.HasValue)
            startPos = pos.Value;
        else
            startPos = GetStartPos();

        GameObject obj = FightManager.R.LoadGo(SkillConfig.bulletPrefab, startPos);
        if (obj == null)
            return;
        obj.transform.forward = Owner.TransformExt.forward;
        Bullet bullet = obj.AddComponent<Bullet>();
        Vector3 targetPos = Vector3.zero;
        if (target != null && target.TransformExt != null)
        {
            targetPos = target.TransformExt.position;
            if (target.hitPos != null)
                targetPos = target.hitPos.position;
        }
        //如果是trigger模式把距离拉远
        if (keyframe.BulletModel == BulletModel.Trigger)
        {
            targetPos += Owner.TransformExt.forward * 5;
        }
        bullet.setSkill(SkillConfig.flySpeed, targetPos);
    }

    protected IEnumerator ShowHurtEffect()
    {
        if (defenderList != null || defenderList.Count > 0)
        {
            defenders.Clear();
            ActorManager.Singleton.Clear();
            foreach (ActorBehavior behavior in defenderList)
            {
                if (behavior == null)
                    continue;
                EditorActor actor = new EditorActor(999, ActorType.Boss, ActorCamp.CampEnemy, 0);
                actor.Init(behavior);
                defenders.Add(actor);
                ActorManager.Singleton.Add(actor);
            }

            int hurt = UnityEngine.Random.Range(900, 1000);
            for (int i = 0; i < defenders.Count; i++)
            {
                EditorHurtEffect hurtEffect = new EditorHurtEffect();
                hurtEffect.Apply(this, defenders[i], (long)hurt);
                yield return new WaitForSeconds(Mathf.Max(SkillConfig.hurtDelay, 0));
            }
        }
    }

    protected override Vector3 GetThunderPoint(ThunderPoint type)
    {
        switch (type)
        {
            case ThunderPoint.Target:
                if (target != null)
                    return target.TransformExt.position;
                return Vector3.zero;
            case ThunderPoint.OppositeCenter:
                return GetColCenter(GridEnum.Col1);
            case ThunderPoint.FrontRowCenter:
                return GetRowCenter(GridEnum.Row0);
            case ThunderPoint.BackRowCenter:
                return GetRowCenter(GridEnum.Row1);
            case ThunderPoint.ColCenter:
                return GetColCenter(targetCol);
        }
        return Vector3.zero;
    }


    protected override void OnMoveBackward()
    {
        base.OnMoveBackward();
        stop();
    }

    protected override void ChangeComboState(ComboStatus status, Vector3? worldPos = null)
    {
        //do nothing
    }

}
