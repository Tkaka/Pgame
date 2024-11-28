using UnityEngine;
using UnityEngine.Playables;
using System;

public class TL_TimeAsset : PlayableAsset
{

    public TL_TimeBehaviorcs template = new TL_TimeBehaviorcs();

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        ScriptPlayable<TL_TimeBehaviorcs> playable = ScriptPlayable<TL_TimeBehaviorcs>.Create(graph, template);
        return playable;
    }

}

