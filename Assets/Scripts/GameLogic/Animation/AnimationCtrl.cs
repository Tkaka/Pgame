using System;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCtrl : IAnimationCtrl
{

    private Animation mAnimation;

    public AnimationCtrl(Animation ani)
    {
        mAnimation = ani;
    }


    public AnimationClip GetClip(string name)
    {
        if(mAnimation != null)
            return mAnimation.GetClip(name);
        return null;
    }

    public float GetAniLen(string name)
    {
        AnimationClip clip = GetClip(name);
        if (clip != null)
            return clip.length;
        return 0;
    }

    public void PlayAnimation(string aniName, float crossDur = 0.2f, float speed=1.0f)
    {
        if (mAnimation != null)
        {
            //mAnimation.Stop();
            mAnimation[aniName].speed = speed;
            mAnimation[aniName].time = 0.0f;
            if (mAnimation != null)
                mAnimation.Play(aniName);
        }
    }

    public bool IsExist(string name)
    {
        AnimationClip clip = GetClip(name);
        return clip != null;
    }
}