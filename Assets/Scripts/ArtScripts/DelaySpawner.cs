using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DelaySpawner : MonoBehaviour
{
    [System.Serializable]
    public struct SpawnInfo
    {
        public float delay;
        public string resName;
        public Transform parent;
    }

    public SpawnInfo[] delays;
    private List<string> loadedResList;
    private List<Coroutine> corList;
    void OnEnable()
    {
        if (delays == null || delays.Length == 0)
            return;

        corList = new List<Coroutine>();
        loadedResList = new List<string>();
        for (int i = 0; i < delays.Length; ++i)
        {
            if (string.IsNullOrEmpty(delays[i].resName))
                continue;
            corList.Add(StartCoroutine(delayCall(delays[i])));
        }
	}

    IEnumerator delayCall(SpawnInfo si)
    {
        if(si.delay == 0)
            spawn(si.resName, si.parent);
        else
            yield return new WaitForSeconds(si.delay);
        spawn(si.resName, si.parent);
    }

    void spawn(string res, Transform parent)
    {
        var obj = ResManager.Singleton.LoadObjSync(res) as GameObject;
        loadedResList.Add(res);
        if (obj != null)
            obj.transform.SetParent(parent, false);
    }

    private void OnDestroy()
    {
        if (corList != null)
        {
            for (int i = 0; i < corList.Count; ++i)
                StopCoroutine(corList[i]);
        }

        if (loadedResList != null)
        {
            for (int i = 0; i < loadedResList.Count; ++i)
                ResManager.Singleton.ReturnObj(loadedResList[i]);
        }
    }
}
