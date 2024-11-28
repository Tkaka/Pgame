using UnityEngine.Playables;
using System;
using UnityEngine;


public class TimelineController : BaseBehaviour
{

    private PlayableDirector director;

    protected override void Awake()
    {
        base.Awake();
        director = GetComponent<PlayableDirector>();
        foreach (PlayableBinding bind in director.playableAsset.outputs)
        {
            //Debug.Log(bind.streamName);
            if (bind.streamName == "Cinemachine Track")
            {
                director.SetGenericBinding(bind.sourceObject, Camera.main.GetComponent<Cinemachine.CinemachineBrain>());
            }
        }
    }

    public void Play(Action callBack = null)
    {
        if (director != null && director.playableAsset != null)
        {
            director.Play();
            delayCall((float)director.playableAsset.duration, () =>
            {
                if (callBack != null)
                    callBack();
                Destroy(gameObject);
            });
        }
    }


}