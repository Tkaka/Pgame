
using UnityEngine;

public interface IAnimationCtrl
{
    AnimationClip GetClip(string name);

    void PlayAnimation(string aniName, float crossDur=0.2f, float speed=1.0f);

    float GetAniLen(string name);

    bool IsExist(string name);

}