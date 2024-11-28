using System;
using UnityEngine;

public class BaseEffectMono : MonoBehaviour
{
	public float RecycleDelay = 1.0f;

    public float SpawnDelay = 0f;

    [Header("中等效果需删除对象")]
    public GameObject[] MiddleLevelHideObj;

    [Header("低等效果需删除对象")]
    [Header("也会隐藏中等效果对象，所以可不必把中等特效对象再拖到这里面")]
    public GameObject[] LowLevelHideObj;

    void Start()
    {
        if (SpawnDelay > 0)
        {
            gameObject.SetActive(false);
            Invoke("spawnDelayFun",SpawnDelay );
        }
        else
        {
            if (RecycleDelay != -1)
                Invoke("recycleSelf", RecycleDelay);
        }
    }

    protected virtual void spawnDelayFun()
    {
        gameObject.SetActive(true);
        if (RecycleDelay != -1)
            Invoke("recycleSelf", RecycleDelay);
    }

    public void recycleSelf()
    {
        Destroy(gameObject);
    }

	///
	public void DealLevel(int level)
	{
		
	}

}
