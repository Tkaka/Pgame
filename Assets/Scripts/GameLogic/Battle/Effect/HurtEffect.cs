using Data.Beans;
using System.Collections.Generic;
using UnityEngine;


public class HurtEffect
{
    private SkillConfig skillConfig;

    private Actor defender;

    private KeyframeCaller keyframeCaller = new KeyframeCaller();

    private MainEffectRes skillRes;

    private long Hurt;

    /// <summary>
    /// 拆分的伤害数字
    /// </summary>
    private Queue<long> hurts;

    public void Apply(MainEffectRes skillRes, SkillConfig skillConfig, Actor defender)
    {
        this.skillRes = skillRes;
        this.skillConfig = skillConfig;
        this.defender = defender;
        Hurt = (long)skillRes.GetVal();
        if (Hurt < 1)
            Hurt = 1;
        SplitHurt();
    }

    /// <summary>
    /// 拆分伤害数字
    /// </summary>
    private void SplitHurt()
    {
        SkillKeyFrame[] frames = skillConfig.keyframes;
        int totalCount = 0;
        List<int> keyframes = new List<int>();
        for (int i = 0; i < frames.Length; i++)
        {
            if (frames[i].type == SkillKeyFrame.Type.Hurt || frames[i].type == SkillKeyFrame.Type.BulletHurt)
            {
                int hurtCount = frames[i].hurtCount;
                if (hurtCount <= 0)
                    hurtCount = 1;
                totalCount += hurtCount;
                keyframes.Add(frames[i].keyFrame);
            }
        }

        if (totalCount <= 0)
        {
            totalCount = 1;
            hurts = new Queue<long>();
            hurts.Enqueue((long)skillRes.hurt);
            keyframeCaller.RegisterKeyFrameCallback(new int[1] { 0 }, ShowHurt);
            keyframeCaller.Run();
        }
        else if (Hurt < totalCount)
        {
            hurts = new Queue<long>();
            for (int i = 0; i < totalCount; i++)
            {
                if (i < Hurt)
                    hurts.Enqueue(1);
                else
                    hurts.Enqueue(0);
            }
            keyframeCaller.RegisterKeyFrameCallback(keyframes.ToArray(), ShowHurt);
            keyframeCaller.Run();
        }
        else
        {
            hurts = GTools.DevideNum(Hurt, totalCount);
            keyframeCaller.RegisterKeyFrameCallback(keyframes.ToArray(), ShowHurt);
            keyframeCaller.Run();
        }

    }

    private bool firstHurtCalled = false;
    public void ShowHurt(int key)
    {
        long totalHurt = 0;
        SkillKeyFrame keyframe = skillConfig.GetKeyframe(key);
        if (hurts != null)
        {
            int counts = 1;
            if (keyframe != null)
                counts = keyframe.hurtCount;
            long hpChange = 0;
            for (int i = 0; i < counts; i++)
            {
                hpChange = hurts.Dequeue();
                totalHurt += hpChange;
                if (hpChange > 0)
                {
                    if (skillRes.isHurt)
                        HurtNumberMgr.Singleton.Emit(defender, NumberType.Hurt, hpChange, skillRes.IsCritical);
                    else
                        HurtNumberMgr.Singleton.Emit(defender, NumberType.Cure, hpChange, skillRes.IsCritical);
                }
                  
            }
        }

        if (skillRes.isHurt)
        {
            OnHurt(totalHurt, keyframe);
        }
        else
        {
            defender.ViewPropertyMgr.ChangeProperty(PropertyType.Hp, totalHurt);
        }
    }


    private void OnHurt(long totalHurt, SkillKeyFrame keyframe)
    {
        //第一次触发伤害时分发，分发事件(只分发一次)
        if (!firstHurtCalled)
        {
            firstHurtCalled = true;
            //加怒气(一次性获得)
            defender.ViewPropertyMgr.ChangeProperty(PropertyType.Mp, defender.getProperty(PropertyType.HurtGetMp));
            ViewUtils.Singleton.Show(skillRes.showId, TimeNode.FirstHurtKeyframe);
        }

        //加怒气
        LNumber hpBase = defender.getBaseProperty(PropertyType.Hp);
        LNumber per = totalHurt / hpBase;
        LNumber mpVal = BattleParam.PercentHurtGetMp * per * 100;
        defender.ViewPropertyMgr.ChangeProperty(PropertyType.Mp, mpVal);
        //Logger.err(defender.Name + " 表现受伤获得怒气:" + mpVal);

        if (FightService.Singleton.FightType == EFightType.CoinDungeon && defender.getCamp() == ActorCamp.CampEnemy)
        {
            DropItemMgr.Singleton.ShowDropItems(defender, totalHurt);
        }

        //扣血
        defender.ViewPropertyMgr.ChangeProperty(PropertyType.Hp, -totalHurt);
        //播放特效
        PlayHitEft(keyframe);
    }

    private void PlayHitEft(SkillKeyFrame keyframe)
    {
        if (!defender.IsDestoryed)
        {
            defender.monoBehavior.OnHit(skillConfig.hitColor);
            HurtSubState subState = HurtSubState.Normal;
            if (keyframe != null)
                subState = keyframe.hurtState;
            defender.changeState(ActorState.hurt, subState);
            AudioManager.Singleton.PlayEffect("snd_attack_hit");

            //播放受击特效
            string hitEft = skillConfig.hitEffect;
            if(keyframe.type == SkillKeyFrame.Type.BulletHurt)
                hitEft = skillConfig.bulletHitEffect;

            if (!string.IsNullOrEmpty(hitEft))
            {
                Transform trans = defender.monoBehavior.hitPos;
                Vector3 pos = Vector3.zero;
                if (trans != null)
                    pos = trans.position;
                //TODO:受击特效方向
                FightManager.R.LoadGo(hitEft, pos);
            }

        }
    }

}
