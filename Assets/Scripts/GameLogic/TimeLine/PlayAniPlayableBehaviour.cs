using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

// A behaviour that is attached to a playable
public class PlayAniPlayableBehaviour : PlayableBehaviour
{
    public string path;

    public string aniName;
    public Transform parentNode;

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

            if(!string.IsNullOrEmpty(aniName))
            {
                SimpleAnimation simpleAni = parentNode.GetComponentInChildren<SimpleAnimation>();
                if (simpleAni != null)
                    simpleAni.Play(aniName);
            }
        }
    }

	// Called when the state of the playable is set to Paused
	public override void OnBehaviourPause(Playable playable, FrameData info) {
		
	}

	// Called each frame while the state is set to Play
	public override void PrepareFrame(Playable playable, FrameData info) {
		
	}
}
