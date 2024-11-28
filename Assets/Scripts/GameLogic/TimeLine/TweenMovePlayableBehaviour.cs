using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using DG.Tweening;

// A behaviour that is attached to a playable
public class TweenMovePlayableBehaviour : PlayableBehaviour
{
    public string path;
    public string moveAniName;
    public string moveEndAni;
    public Vector3[] movePath;
    public float moveTime = 1f;
    public Ease moveEase = Ease.Linear;
    public Transform parentNode;

    private Tweener tweener;
    // Called when the owning graph starts playing
    public override void OnGraphStart(Playable playable) {
		
	}

	// Called when the owning graph stops playing
	public override void OnGraphStop(Playable playable) {
		
	}

	// Called when the state of the playable is set to Play
	public override void OnBehaviourPlay(Playable playable, FrameData info) {
        if (Application.isPlaying)
        {
            if (parentNode == null)
            {
                var parentObj = GameObject.Find(path);
                if (parentObj != null)
                    parentNode = parentObj.transform;
            }
            if (parentNode == null)
            {
                Debug.LogError("找不到父节点对象>" + path);
                return;
            }

            if(movePath.Length > 0)
            {
                SimpleAnimation simpleAni = parentNode.GetComponentInChildren<SimpleAnimation>();
                if (simpleAni != null && !string.IsNullOrEmpty(moveAniName))
                    simpleAni.Play(moveAniName);
                tweener = parentNode.DOPath(movePath, moveTime).SetEase(moveEase).OnComplete(()=>{
                    if (simpleAni != null && !string.IsNullOrEmpty(moveEndAni))
                        simpleAni.Play(moveEndAni);
                });
            }
        }
	}

	// Called when the state of the playable is set to Paused
	public override void OnBehaviourPause(Playable playable, FrameData info) {
        if (Application.isPlaying && tweener != null)
            tweener.Kill();
    }

	// Called each frame while the state is set to Play
	public override void PrepareFrame(Playable playable, FrameData info) {
		
	}
}
