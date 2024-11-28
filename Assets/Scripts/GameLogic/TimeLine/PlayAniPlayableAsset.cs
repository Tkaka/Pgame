using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public class PlayAniPlayableAsset : PlayableAsset
{
    [Tooltip("此项不填，自动生成")]
    public string path;

    public string aniName;
    public ExposedReference<Transform> parentNode;

    public override Playable CreatePlayable(PlayableGraph graph, GameObject go) {
        var playable = ScriptPlayable<PlayAniPlayableBehaviour>.Create(graph);
        playable.GetBehaviour().aniName = aniName;
        Transform trans = parentNode.Resolve(graph.GetResolver());
        playable.GetBehaviour().parentNode = trans;

        if (trans != null)
        {
            path = trans.name;
            while (true)
            {
                trans = trans.parent;
                if (trans == null)
                    break;
                else
                    path = trans.name + "/" + path;
            }
        }
        playable.GetBehaviour().path = path;

        return playable;
    }
}
