using UnityEngine.Playables;
using UnityEngine;
using System;

[Serializable]
public class TL_TimeBehaviorcs : PlayableBehaviour
{

    /// <summary>
    /// 曲线
    /// </summary>
    public AnimationCurve curve = new AnimationCurve();
    /// <summary>
    /// 当前时间进度
    /// </summary>
    private float curTime;
    /// <summary>
    /// 最大时间
    /// </summary>
    private float maxTime;

    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        maxTime = (float)PlayableExtensions.GetDuration(playable);
        curTime = 0;
    }

    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        Time.timeScale = 1;
    }

    public override void PrepareFrame(Playable playable, FrameData info)
    {
        curTime += info.deltaTime;
        //曲线值
        Time.timeScale = curve.Evaluate(curTime / maxTime);
    }

}
