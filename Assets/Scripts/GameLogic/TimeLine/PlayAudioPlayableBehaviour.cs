using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

// A behaviour that is attached to a playable
public class PlayAudioPlayableBehaviour : PlayableBehaviour
{
    public bool loop = false;
    public string audioResName;

    private bool loaded;
    private GameObject sndObj;

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
            loaded = true;
            var clicp = ResManager.Singleton.LoadObjSync(audioResName, audioResName, typeof(AudioClip)) as AudioClip;
            if(clicp != null)
            {
                sndObj = new GameObject("playable_snd");
                var source = sndObj.AddComponent<AudioSource>();
                source.clip = clicp;
                source.loop = loop;
                source.Play();
            }
        }
    }

	// Called when the state of the playable is set to Paused
	public override void OnBehaviourPause(Playable playable, FrameData info) {
        if (Application.isPlaying && loaded)
        {
            loaded = false;
            ResManager.Singleton.ReturnObj(audioResName);
            Object.DestroyImmediate(sndObj);
        }
    }

	// Called each frame while the state is set to Play
	public override void PrepareFrame(Playable playable, FrameData info) {
		
	}
}
