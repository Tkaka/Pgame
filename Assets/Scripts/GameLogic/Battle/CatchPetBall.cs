
using UnityEngine;
using UnityEngine.Playables;

public class CatchPetBall : BaseBehaviour
{
    public GameObject ball;

    public PlayableDirector closeShot;

    protected override void Awake()
    {
        if (closeShot != null)
        {
            foreach (PlayableBinding bind in closeShot.playableAsset.outputs)
            {
                if (bind.streamName == "Cinemachine Track")
                {
                    closeShot.SetGenericBinding(bind.sourceObject, Camera.main.GetComponent<Cinemachine.CinemachineBrain>());
                }
            }
        }
    }

    public void Play()
    {
        if (closeShot != null)
            closeShot.Play();
    }


}
