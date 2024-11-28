using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public class TweenMovePlayableAsset : PlayableAsset
{
    [Tooltip("此项不填，自动生成")]
    public string path;
    [Tooltip("移动时的动画")]
    public string moveAniName;
    [Tooltip("移动结束播放的动画")]
    public string moveEndAni;
    [Tooltip("移动路径")]
    public List<Vector3> movePath;
    [Tooltip("移动时间")]
    public float moveTime = 1f;
    [Tooltip("移动缓动方式")]
    public DG.Tweening.Ease moveEase = DG.Tweening.Ease.Linear;
    [Tooltip("移动对象或者父节点")]
    public ExposedReference<Transform> parentNode;

    public override Playable CreatePlayable(PlayableGraph graph, GameObject go) {
        var playable = ScriptPlayable<TweenMovePlayableBehaviour>.Create(graph);
        playable.GetBehaviour().movePath = movePath.ToArray();
        playable.GetBehaviour().moveAniName = moveAniName;
        playable.GetBehaviour().moveEndAni = moveEndAni;
        playable.GetBehaviour().moveTime = moveTime;
        playable.GetBehaviour().moveEase = moveEase;

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
