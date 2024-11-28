using UnityEngine;
using UnityEngine.Playables;

public class AddGoPlayableBeheviour : PlayableBehaviour
{
    public string path;
    public string goResName;
    public Transform parentNode;

    private GameObject obj;
    private bool loaded;

    // Called when the owning graph starts playing
    public override void OnGraphStart(Playable playable)
    {

    }

    // Called when the owning graph stops playing
    public override void OnGraphStop(Playable playable)
    {

    }

    // Called when the state of the playable is set to Play
    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        if (Application.isPlaying)
        {
            loaded = true;
            obj = ResManager.Singleton.LoadObjSync(goResName) as GameObject;
			if(obj != null)
            {
                if (parentNode == null)
                {
                    var parentObj = GameObject.Find(path);
                    if(parentObj != null)
				        obj.transform.SetParent(parentObj.transform, false);
                }else
                {
                    obj.transform.SetParent(parentNode, false);
                }
            }
            ResManager.Singleton.AddRef(goResName);
        }
    }

    // Called when the state of the playable is set to Paused
    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        if (Application.isPlaying)
        {
            if(loaded)
            {
                loaded = false;
                UnityEngine.Object.DestroyImmediate(obj);
                ResManager.Singleton.ReturnObj(goResName);
            }
        }
    }

    // Called each frame while the state is set to Play
    public override void PrepareFrame(Playable playable, FrameData info)
    {
        
    }
}
