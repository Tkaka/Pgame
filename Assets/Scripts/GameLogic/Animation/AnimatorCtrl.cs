using UnityEngine;

public class AnimatorCtrl : IAnimationCtrl
{
    private Animator mAnimator;
    private Map<string, AnimationClip> mClipMap = new Map<string, AnimationClip>();
    private AnimatorStateInfo mState;

    public AnimatorCtrl(Animator animator)
    {
        mAnimator = animator;
        if (mAnimator != null)
        {
            RuntimeAnimatorController ac = mAnimator.runtimeAnimatorController;
            string aniName;
            for (int i = 0; i < ac.animationClips.Length; i++)
            {
                //Debug.LogError(ac.animationClips[i].name + " : " + ac.animationClips[i].length);
                aniName = ac.animationClips[i].name;
                mClipMap.add(aniName, ac.animationClips[i]);
            }
        }
        else
        {
            Logger.err("Error Animator is null");
        }
    }

    public AnimationClip GetClip(string name)
    {
        AnimationClip clip = mClipMap.get(name);
        if (clip == null)
            Logger.err("找不到动画：" + name);
        return clip;
    }

    /// <summary>
    /// 得到状态动画长度，混合层则返回上身动作长度
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public float GetAniLen(string name)
    {
        if (mAnimator == null)
            return 0;
        AnimationClip clip = GetClip(name);
        if (clip == null)
            return 0;
        return clip.length;
    }

    public void PlayAnimation(string aniName, float crossDur = 0.2f, float speed = 1.0f)
    {
        if (mAnimator.HasState(0, Animator.StringToHash("base layer." + aniName)))
        {
            mAnimator.SetLayerWeight(0, 1);
            if (!string.IsNullOrEmpty(aniName))
            {
                mAnimator.Play(aniName, 0, 0);
            }
        }
    }

    public bool IsExist(string name)
    {
        if (mClipMap == null)
            return false;
        AnimationClip clip = mClipMap.get(name);
        return clip != null;
    }
}
