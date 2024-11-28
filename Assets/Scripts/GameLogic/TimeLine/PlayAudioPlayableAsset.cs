using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public class PlayAudioPlayableAsset : PlayableAsset
{
    public bool loop = false;
    public string audioResName;
    public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
    {
        var playable = ScriptPlayable<PlayAudioPlayableBehaviour>.Create(graph);
        playable.GetBehaviour().loop = loop;
        playable.GetBehaviour().audioResName = audioResName;

        return playable;
    }
}
