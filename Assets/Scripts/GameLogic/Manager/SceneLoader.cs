
using System;
using System.Collections;
using UnityEngine;

public class GSceneName
{
    public const string MaiCity = "lvl_maincity_feichuan_01";
    public const string Guild = "lvl_maincity_feichuan_01";
    public const string Battle = "s_test_luye_01";
}

public class SceneLoader : SingletonTemplate<SceneLoader>
{
    private long coroID = 0;

    public string sceneName;

    public string nextState;

    private Action<float> onProgress;

    private Action onFinish;

    private GameObject sceneObj;

    private ResPack resPacker = new ResPack("SceneLoader");

    private bool loaded;

    public void Load(Action onFinish, Action<float> onProgress)
    {
        resPacker.ReleaseAllRes();
        this.onFinish = onFinish;
        this.onProgress = onProgress;

        loaded = false;
        if (sceneObj != null)
            GameObject.Destroy(sceneObj);

        System.GC.Collect();
        ResManager.Singleton.GC();
        UIResMgr.Singleton.RemoveZeroRefPkg();

        if (!string.IsNullOrEmpty(sceneName))
        {
            coroID = CoroutineManager.Singleton.startCoroutine(Update());
            resPacker.Request(sceneName, sceneName, null, (res, dep, type) =>
            {
                if(sceneName == res)
                {
                    sceneObj = resPacker.GetObject(sceneName) as GameObject;
                    loaded = true;
                }
            });
        }
        else
        {
            loaded = true;
            if (onProgress != null)
                onProgress(1);
            if (onFinish != null)
            {
                if (!string.IsNullOrEmpty(nextState))
                {
                    GameManager.Singleton.changeState(nextState);
                }
                onFinish();
            }
            Clear();
        }
    }
    
    private IEnumerator Update()
    {
        float progress = 0f;
        while (true)
        {
            //进度条是假的
            if(!loaded)
            {
                //加载完成之前
                if(progress < 0.9f)
                    progress += UnityEngine.Random.Range(0.01f, 0.2f);
                else if(progress < 0.99f)
                    progress += 0.03f;

                if (onProgress != null)
                    onProgress(progress);
                yield return null;
            }
            else
            {
                progress = 1f;
                if (onProgress != null)
                    onProgress(progress);
                yield return 0.15f;

                if (!string.IsNullOrEmpty(nextState))
                    GameManager.Singleton.changeState(nextState);

                if (onFinish != null)
                    onFinish();

                Clear();
                break;
            }
        }
    }

    private void Clear()
    {
        CoroutineManager.Singleton.stopCoroutine(coroID);
        onFinish = null;
        onProgress = null;
    }

    public void OnGameLeave()
    {
        resPacker.ReleaseAllRes();
    }
}