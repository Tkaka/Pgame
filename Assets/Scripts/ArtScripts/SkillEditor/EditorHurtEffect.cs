using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 伤害效果
/// </summary>
public class EditorHurtEffect
{

    private SkillConfig skillConfig;

    private Queue<int> hurtCounts = new Queue<int>();

    private Queue<long> hurts;

    private Actor defender;

    private KeyframeCaller keyframeCaller = new KeyframeCaller();

    /// <summary>
    /// 按关键帧拆分
    /// </summary>
    /// <param name="hurt"></param>
    /// <param name="skillConfig"></param>
    private void SplitHurt(long hurt, SkillConfig skillConfig)
    {
        SkillKeyFrame[] frames = skillConfig.keyframes;
        int hurtCount = 0;
        int totalCount = 0;
        List<int> keyframes = new List<int>();
        for (int i = 0; i < frames.Length; i++)
        {
            if (frames[i].type == SkillKeyFrame.Type.Hurt || frames[i].type == SkillKeyFrame.Type.BulletHurt)
            {
                hurtCount = frames[i].hurtCount;
                if (hurtCount <= 0)
                    hurtCount = 1;
                totalCount += hurtCount;
                hurtCounts.Enqueue(hurtCount);
                keyframes.Add(frames[i].keyFrame);
            }
        }
        if (totalCount <= 0)
        {
            hurtCounts.Enqueue(1);
            totalCount = 1;
            hurts = new Queue<long>();
            hurts.Enqueue(hurt);
            keyframeCaller.RegisterKeyFrameCallback(new int[1] { 10 }, ShowHurt);
            keyframeCaller.Run();
        }
        else
        {
            hurts = GTools.DevideNum(hurt, totalCount);
            keyframeCaller.RegisterKeyFrameCallback(keyframes.ToArray(), ShowHurt);
            keyframeCaller.Run();
        }
    }

    public void Apply(Skill skill, Actor defender, long hurt)
    {
        this.skillConfig = skill.SkillConfig;
        this.defender = defender;
        SplitHurt(hurt, skillConfig);
    }

    public void ShowHurt(int key)
    {
        long totalHurt = 0;
        if (hurts != null)
        {
            int counts = hurtCounts.Dequeue();
            long hpChange = 0;
            for (int i = 0; i < counts; i++)
            {
                hpChange = hurts.Dequeue();
                totalHurt += hpChange;
                HurtNumberMgr.Singleton.Emit(defender, NumberType.Hurt, hpChange, false);
            }
        }
        //defender.changeProperty(PropertyType.Hp, -totalHurt);
        if (!defender.IsDestoryed)
        {
            defender.monoBehavior.OnHit(skillConfig.hitColor);
            SkillKeyFrame keyframe = skillConfig.GetKeyframe(key);
            defender.changeState(ActorState.hurt, keyframe.hurtState);
            //defender.changeState(ActorState.hurt, HurtSubState.Normal);

            //播放受击特效
            string hitEft = skillConfig.hitEffect;
            if (keyframe.type == SkillKeyFrame.Type.BulletHurt)
                hitEft = skillConfig.bulletHitEffect;

            if (!string.IsNullOrEmpty(hitEft))
            {
                Transform trans = defender.monoBehavior.hitPos;
                Vector3 pos = Vector3.zero;
                if (trans != null)
                    pos = trans.position;
                FightManager.R.LoadGo(hitEft, pos);
            }
        }
    }

}
