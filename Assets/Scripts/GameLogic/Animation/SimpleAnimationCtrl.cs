using UnityEngine;

public class SimpleAnimationCtrl : IAnimationCtrl
{

    private SimpleAnimation sani;

    public SimpleAnimationCtrl(SimpleAnimation sani)
    {
        this.sani = sani;
    }

    public bool IsExist(string name)
    {
        if (sani == null)
            return false;
        SimpleAnimation.State state = sani.GetState(name);
        if (state == null)
            return false;
        return state.clip;
    }

    public float GetAniLen(string name)
    {
        AnimationClip clip = GetClip(name);
        if (clip != null)
            return clip.length;
        return 0;
    }

    public AnimationClip GetClip(string name)
    {
        if (sani != null)
        {
            SimpleAnimation.State state = sani.GetState(name);
            if (state != null)
            {
                return state.clip;
            }
            else
            {
                Logger.err("SimpleAnimationCtrl:GetClip:Can not find state: " + name);
            }
        }
        return null;
    }

    public void PlayAnimation(string aniName, float crossDur = 0.2f, float speed = 1)
    {
        if (sani != null)
        {
            sani[aniName].speed = speed;
            sani[aniName].time = 0.0f;
            //sani.Play(aniName);
            //return;
            if (crossDur > 0)
                sani.CrossFade(aniName, crossDur);
            else
                sani.Play(aniName);
        }
    }

}