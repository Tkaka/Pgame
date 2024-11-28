using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using DG.Tweening;

public class SkillConfig : MonoBehaviour
{
    [Tooltip("动作名字")]
    public AniName aniName = AniName.idle;

    [Tooltip("是否近战")]
    public bool closeCombat = false;

    [Tooltip("技能ID")]
    public int skillId;

    [Tooltip("技能等级")]
    public int skillLevel = 1;

    [Tooltip("技能类型")]
    public SkillType skillType;

    [Tooltip("技能模板")]
    public SkillTemplate skillTemplate = SkillTemplate.CommonSkill;

    [Tooltip("前摇特效")]
    public string headEffect;

    [Tooltip("攻击声音")]
    public string attackSound;

    [Tooltip("攻击声音")]
    public string hitSound;

    [Tooltip("攻击特效")]
    public string attackEffect;

    [Tooltip("攻击特效2")]
    public string handEffect;

    [Tooltip("命中特效")]
    public string hitEffect;

    //[Tooltip("子弹类型")]
    //public BulletTemplate bulletType;

    [Tooltip("子弹预制件")]
    public string bulletPrefab;

    [Tooltip("子弹命中特效")]
    public string bulletHitEffect;

    [Tooltip("子弹飞行速度")]
    public float flySpeed;

    [Tooltip("子弹持续时间")]
    public int bulletDuration;

    [Tooltip("子弹攻击间隔")]
    public int bulletInterval;

    [Tooltip("命中变色")]
    public Color32 hitColor;

    public ColorElement colorElement = ColorElement.White01;

    [Tooltip("技能流程")]
    public SkillKeyFrame[] skillFlowList = new SkillKeyFrame[]
   {
        new SkillKeyFrame(SkillKeyFrame.Type.MoveForward),
        new SkillKeyFrame(SkillKeyFrame.Type.HeadEft),
        new SkillKeyFrame(SkillKeyFrame.Type.AtkEft),
        new SkillKeyFrame(SkillKeyFrame.Type.PlayAni),
        new SkillKeyFrame(SkillKeyFrame.Type.MoveBackward)
   };

    [Tooltip("伤害关键帧")]
    public SkillKeyFrame[] keyframes;

    [Tooltip("其他关键帧")]
    public SkillKeyFrame[] otherKeyframes;

    [Tooltip("连击帧")]
    public int comboFrame;

    [Tooltip("跳回来的距离")]
    public float moveBack;

    [Tooltip("技能总时长(毫秒)")]
    public int skillTotalTime = 1000;

    [Tooltip("技能释放站位点")]
    public StandingPoint standingPoint;
	
	[Tooltip("技能释放站位点偏移")]
    public Vector2 standOffset = Vector2.zero;

    [Tooltip("伤害延迟")]
    public float hurtDelay;

    [Tooltip("特写镜头")]
    public PlayableDirector closeShot;

    [Tooltip("是否隐藏其他角色")]
    public bool hideOthers;

    [Tooltip("只显示自己的时间")]
    public float onlyShowSelfTime;

    [Tooltip("特效挂点")]
    public MountPoint[] mountPoints;
    
    [Tooltip("技能震屏持续时间")]
    public float shakeCameraDur = 0.2f;
    [Tooltip("技能震屏强度")]
    public float shakeCameraStrength = 0.3f;
    [Tooltip("技能震屏振幅")]
    public int shakeCameraVibrato = 1000;
    [Tooltip("技能震屏幅度")]
    public float shakeCameraDistance = 3;

    private Dictionary<MountPointType, MountPoint> mpDic;

    private void Start()
    {
        if (mountPoints != null && mountPoints.Length > 0)
        {
            mpDic = new Dictionary<MountPointType, MountPoint>();
            for (int i = 0; i < mountPoints.Length; i++)
            {
                mpDic.Add(mountPoints[i].type, mountPoints[i]);
            }
        }
        if (closeShot != null)
        {
            foreach (PlayableBinding bind in closeShot.playableAsset.outputs)
            {
                if (bind.streamName == "Cinemachine Track")
                {
                    closeShot.SetGenericBinding(bind.sourceObject, Camera.main.GetComponent<Cinemachine.CinemachineBrain>());
                }
            }
        }
    }

    public MountPoint GetMountPoint(MountPointType type)
    {
        if (mpDic == null)
            return null;
        MountPoint val;
        mpDic.TryGetValue(type, out val);
        return val;
    }


    public SkillKeyFrame GetOtherKeyframe(int index)
    {
        if (index >= 0 && index < otherKeyframes.Length)
        {
            return otherKeyframes[index];
        }
        return null;
    }

    /// <summary>
    /// 是否有添加buff的关键帧
    /// </summary>
    /// <returns></returns>
    public bool HasBuffKeyframe()
    {
        if (otherKeyframes != null && otherKeyframes.Length > 0)
        {
            for (int i = 0; i < otherKeyframes.Length; i++)
            {
                if (otherKeyframes[i].type == SkillKeyFrame.Type.AddBuff)
                    return true;
            }
        }
        return false;
    }

    public float GetFreezeTime()
    {
        float time = 0;
        if (otherKeyframes != null && otherKeyframes.Length > 0)
        {
            for (int i = 0; i < otherKeyframes.Length; i++)
            {
                if (otherKeyframes[i].type == SkillKeyFrame.Type.FreezeFrame)
                    time += otherKeyframes[i].FreezeTime;
            }
        }
        return time/Time.timeScale;
    }

    public bool HasMoveForward()
    {
        if (skillFlowList != null && skillFlowList.Length > 0)
        {
            for (int i = 0; i < skillFlowList.Length; i++)
            {
                if (skillFlowList[i].type == SkillKeyFrame.Type.MoveForward)
                    return true;
            }
        }
        return false;
    }


    public int[] GetOtherKeyframes()
    {
        if (otherKeyframes != null && otherKeyframes.Length > 0)
        {
            List<int> res = new List<int>();
            for (int i = 0; i < otherKeyframes.Length; i++)
            {
                res.Add(otherKeyframes[i].keyFrame);
            }
            return res.ToArray();
        }
        return null;
    }

    public SkillKeyFrame GetKeyframe(int index)
    {
        if (index >= 0 && index < keyframes.Length)
        {
            return keyframes[index];
        }
        return null;
    }

    public int[] GetKeyframes(bool excludeHurt=false)
    {
        if (keyframes != null && keyframes.Length > 0)
        {
            List<int> res = new List<int>();
            for (int i = 0; i < keyframes.Length; i++)
            {
                if (excludeHurt && keyframes[i].type == SkillKeyFrame.Type.Hurt)
                    continue;
                res.Add(keyframes[i].keyFrame);
            }
            return res.ToArray();
        }
        return null;
    }

    public long GetMaxKeyframe()
    {
        return (long)(keyframes[keyframes.Length - 1].keyFrame * 1000.0f / 30);
    }

}