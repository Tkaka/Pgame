using Data.Beans;
using FastShadowReceiver;
using UnityEngine;
using System;
using UnityEngine.Playables;

public class ActorPlayer : Actor
{
    public ActorPlayer(int templateId, ActorType actorType, ActorCamp camp, long actorId = 0) :
        base(templateId, actorType, camp, actorId)
    {
    }

    public override bool IsActorRace(ActorRace raceType)
    {
        return false;
    }

    public override void registerAllState()
    {
        base.registerAllState();
        registerState(ActorState.move, new ActorTweenMoveState());
    }

    protected override void loadPrefab(ActorParam instanceData)
    {
        t_professionBean mBean = ConfigBean.GetBean<t_professionBean, int>(mTemplateId);
        //mShowObj = Res.Singleton.InstantiateModel(mBean.t_battle_prefab, instanceData.Pos);
        mShowObj = resPacker.LoadGo(mBean.t_battle_prefab, instanceData.Pos);
        if (mShowObj != null)
            mShowObj.transform.forward = instanceData.Dir;
    }

    protected override void SetActorTypes()
    {
        
    }

    public void ResetLight(Light shadowLight)
    {
        if (ShowObj != null)
        {
            Transform trans = ShowObj.transform.Find("Shadow/Projector");
            if (trans != null)
            {
                LightProjector lightProj = trans.GetComponent<LightProjector>();
                if (lightProj != null)
                    lightProj.SetLight(shadowLight);
            }
        }
    }

    public void ThrowBall(Action callback)
    {
        if (monoBehavior.entranceShot != null)
        {
            PlayableDirector pd = monoBehavior.entranceShot.GetComponent<PlayableDirector>();
            if (pd != null)
            {
                monoBehavior.entranceShot.gameObject.SetActive(true);
                pd.Play();
                CoroutineManager.Singleton.delayedCall((float)pd.duration, () =>
                {
                    pd.Stop();
                    monoBehavior.entranceShot.gameObject.SetActive(false);
                    if (callback != null)
                        callback();
                });
            }
        }
    }

}
