using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : SingletonBehaviour<SpawnerManager>
{

    private int mTriggerCount = 0;        //触发点个数
    public int GetTriggerCount
    {
        get { return mTriggerCount; }
    }

    public void FindTriggers(List<WaveTrigger> mTriggerList)
    {
        int index = 0;
        bool flag = true;
        string triggerName = "";
        WaveTrigger monsterTrigger;
        while (flag)
        {
            triggerName = "Trigger" + index;
            Transform trigger = TransformExt.Find(triggerName);
            if (trigger != null)
            {
                monsterTrigger = trigger.GetComponent<WaveTrigger>();
                if (monsterTrigger != null)
                {
                    mTriggerList.Add(monsterTrigger);
                    monsterTrigger.Id = mTriggerCount;
                    mTriggerCount++;
                }
                else
                {
                    Logger.err("SpawnerManager:findTriggers:不能找到该节点" + triggerName);
                }
                index++;
            }
            else
            {
                flag = false;
            }
        }
    }

    public void FindPaths(List<GameMovePath> pathList)
    {
        int index = 0;
        bool flag = true;
        string pathName = "";
        GameMovePath path;
        while (flag)
        {
            pathName = "Paths/path" + index;
            Transform pathTrans = TransformExt.Find(pathName);
            if (pathTrans != null)
            {
                path = pathTrans.GetComponent<GameMovePath>();
                if (path != null)
                {
                    pathList.Add(path);
                }
                else
                {
                    Logger.err("SpawnerManager:FindPaths:不能找到该节点" + pathName);
                }
                index++;
            }
            else
            {
                flag = false;
            }
        }
    }

    protected override void OnDestroy()
    {
        mTriggerCount = 0;
        base.OnDestroy();
    }

}

