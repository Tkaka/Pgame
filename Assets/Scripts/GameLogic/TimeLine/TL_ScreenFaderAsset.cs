using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class TL_ScreenFaderAsset : PlayableAsset
{

    public TL_ScreenFaderBehavior template = new TL_ScreenFaderBehavior();

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        ScriptPlayable<TL_ScreenFaderBehavior> playable = ScriptPlayable<TL_ScreenFaderBehavior>.Create(graph, template);
        return playable;
    }

}
