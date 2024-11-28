using FairyGUI;
using System.Collections.Generic;
using UnityEngine;

public class UIResPack : ResPack
{
    public UIResPack(object owner) : base(owner)
    {

    }

    public override void ReleaseAllRes()
    {
        base.ReleaseAllRes();
        for (int i = 0; i < actorList.Count; ++i)
        {
            if (actorList[i] != null)
                actorList[i].destoryMe();
        }
        actorList.Clear();

        for (int i = 0; i < cacheList.Count; ++i)
        {
            if (cacheList[i] != null)
                cacheList[i].destoryMe();
        }
        cacheList.Clear();
    }

    private List<ActorUI> cacheList = new List<ActorUI>();
    private List<ActorUI> actorList = new List<ActorUI>();
    public ActorUI NewActorUI(int id, ActorType type, GoWrapper wrapper, bool uiMask = false, bool asyncLoad = true)
    {
        return NewActorUI(id, -1, type, wrapper, uiMask, asyncLoad);
    }

    public ActorUI NewActorUI(int id, int star, ActorType type, GoWrapper wrapper, bool uiMask = false, bool asyncLoad = true)
    {
        for (int i = 0; i < cacheList.Count; ++i)
        {
            var act = cacheList[i];
            if (act == null)
                continue;
            if (act.getActorType() != type)
                continue;
            if (act.getTemplateId() != id)
                continue;
            if (act.Star != star)
                continue;

            actorList.Add(act);
            cacheList.RemoveAt(i);

            act.Wrapper = wrapper;
            act.SetMask(uiMask);
            ActorParam param = ActorParam.create(Vector3.zero, Vector3.zero, star);
            act.initialize(param);
            return act;
        }

        //ÇåÀí»º´æ
        if (cacheList.Count > 5)
            clearCache(5);

        var actor = new ActorUI(id, type, asyncLoad);
        actor.Wrapper = wrapper;
        actor.SetMask(uiMask);
        ActorParam param1 = ActorParam.create(Vector3.zero, Vector3.zero, star);
        actor.initialize(param1);
        actorList.Add(actor);
        return actor;
    }

    private void clearCache(int left)
    {
        int num = cacheList.Count - left;
        for(int i=0; i<num; ++i)
        {
            if (cacheList[0] != null)
                cacheList[0].destoryMe();
            cacheList.RemoveAt(0);
        }
    }

    public void CacheWrapper(GoWrapper wrapper)
    {
        if (wrapper != null && !wrapper.isDisposed)
        {
            for(int i=0; i< actorList.Count; ++i)
            {
                if(actorList[i] != null && actorList[i].Wrapper == wrapper)
                {
                    cacheList.Add(actorList[i]);
                    actorList.RemoveAt(i);
                    break;
                }
            }

            if (wrapper.wrapTarget != null)
            {
                wrapper.wrapTarget.SetActive(false);
                wrapper.wrapTarget = null;
            }
        }
    }

    public void CacheWrapper(GObject obj)
    {
        if (obj != null)
            CacheWrapper(obj.displayObject as GoWrapper);
    }
}