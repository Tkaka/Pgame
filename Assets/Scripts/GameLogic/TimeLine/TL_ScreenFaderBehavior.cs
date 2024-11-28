using UnityEngine.Playables;
using UnityEngine;
using FairyGUI;
using System;

[Serializable]
public class TL_ScreenFaderBehavior : PlayableBehaviour
{

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
    }

    public override void OnGraphStart(Playable playable)
    {
    }

    public override void OnGraphStop(Playable playable)
    {
    }

    public override void PrepareFrame(Playable playable, FrameData info)
    {
        curTime += info.deltaTime;
    }

}
