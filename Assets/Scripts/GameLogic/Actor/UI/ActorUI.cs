using System;
using System.Collections;
using Data.Beans;
using UnityEngine;

public class ActorUI : AngelaBaby
{
    private bool async = false;

    private Vector3 pos;
    private Vector3 rot;
    private float scale = -1f;
    private FairyGUI.GoWrapper wrapper;
    private bool registed = false;
    private bool enableMask = false;
    private Action randomEndCall;
    public int Star { get; private set; }

    public ActorUI(int templateId, ActorType type, bool asyncLoad = true)
        :base(templateId, type, ActorCamp.CampFriend, 0)
    {
        async = asyncLoad;
    }

    public override void registerAllState()
    {
        if (registed)
            return;

        registed = true;
        registerState(ActorState.idle, new ActorIdleStateMC());
    }

    public override bool initialize(ActorParam instanceData)
    {
        if (instanceData == null)
            instanceData = ActorParam.create(Vector3.zero, Vector3.zero);
        return base.initialize(instanceData);
    }

    protected override void loadPrefab(ActorParam instanceData)
    {
        if (mShowObj != null)
        {
            mShowObj.SetActive(true);
            return;
        }

        Star = instanceData.Star;
        if (isActorType(ActorType.Monster))
        {
            t_monsterBean mBean = ConfigBean.GetBean<t_monsterBean, int>(mTemplateId);
            if (mBean != null)
            {
                if (async)
                {
                    pos = instanceData.Pos;
                    this.resPacker.Request(mBean.t_battle_prefab, mBean.t_battle_prefab, null, asyncLoaded);
                }
                else
                {
                    asyncLoaded(mBean.t_battle_prefab, mBean.t_battle_prefab, null);
                }
            }
        }
        else if (isActorType(ActorType.Boss))
        {
            t_monster_boosBean mBean = ConfigBean.GetBean<t_monster_boosBean, int>(mTemplateId);
            if (mBean != null)
            {
                if (async)
                {
                    pos = instanceData.Pos;
                    this.resPacker.Request(mBean.t_prefab, mBean.t_prefab, null, asyncLoaded);
                }
                else
                {
                    asyncLoaded(mBean.t_prefab, mBean.t_prefab, null);
                }
            }
        }
        else
        {
            t_petBean mBean = ConfigBean.GetBean<t_petBean, int>(mTemplateId);
            if (mBean != null)
            {
                string prefab = UIUtils.GetCityPrefab(mBean, instanceData.Star);
                if (async)
                {
                    pos = instanceData.Pos;
                    this.resPacker.Request(prefab, prefab, null, asyncLoaded);
                }
                else
                {
                    asyncLoaded(prefab, prefab, null);
                }
            }
        }
    }
    
    private void asyncLoaded(string ab, string dep, Type type)
    {
        if(false == IsDestoryed && mShowObj == null && !wrapper.isDisposed)
        {
            mShowObj = this.resPacker.LoadGo(ab);
            Wrapper = wrapper;
            SetTransform(pos, scale, rot);
            addComponent(null);
        }
    }

    public void SetMask(bool isMaskUI)
    {
        enableMask = isMaskUI;
        if (wrapper != null && mShowObj != null)
            UIUtils.SetWrapperMask(wrapper, enableMask);
    }

    public FairyGUI.GoWrapper Wrapper
    {
        get
        {
            return wrapper;
        }
        set
        {
            wrapper = value;
            if (value != null && mShowObj != null)
            {
                value.wrapTarget = mShowObj;
                value.layer = LayerMask.NameToLayer("UIActor");
                UIUtils.SetWrapperMask(value, enableMask);
            }
        }
    }
    
    public void SetTransform(Vector3 _pos, float _scale = -1f, Vector3 ? _rot = null)
    {
        if(mShowObj != null)
        {
            mShowObj.transform.localPosition = _pos;
            if (_scale > 0)
                mShowObj.transform.localScale = Vector3.one * _scale;

            if (_rot != null)
                mShowObj.transform.localEulerAngles = _rot.Value;
        }
        else
        {
            pos = _pos;
            scale = _scale;
            if (_rot != null)
                rot = _rot.Value;
        }
    }

    private float mouseDownX;
    public void MouseRotate(FairyGUI.GObject com)
    {
        if (com == null)
            return;

        com.onTouchBegin.Clear();
        com.onTouchMove.Clear();
        com.onTouchBegin.Add((FairyGUI.EventContext context)=>{
            context.CaptureTouch();
            mouseDownX = context.inputEvent.x;
        });
        com.onTouchMove.Add((FairyGUI.EventContext context) =>{
            if (ShowObj != null)
            {
                ShowObj.transform.localEulerAngles += new Vector3(0, mouseDownX - context.inputEvent.x, 0);
                mouseDownX = context.inputEvent.x;
            }
        });
    }
    
    public void PlayAni(string name, Action<int> endCallBack)
    {
        if (mActionManager != null)
            mActionManager.PlayCommonAnimation(name, endCallBack);
    }

    private long aniTimer;
    private static string[] aniArr = { "attack", "skill1", "skill2" };
    public void PlayRandomAni(Action endCall = null)
    {
        randomEndCall = endCall;

        CoroutineManager.Singleton.stopCoroutine(aniTimer);
        if (mShowObj != null && mActionManager != null)
        {
            int num = 0;
            while (true)
            {
                num++;
                if (num > aniArr.Length)
                    break;
                int random = UnityEngine.Random.Range(0, aniArr.Length);
                if (mActionManager.GetAniLen(aniArr[random]) > 0)
                {
                    mActionManager.PlayCommonAnimation(aniArr[random], randomAniEnd);
                    break;
                }
            }
        }else
        {
            aniTimer = CoroutineManager.Singleton.startCoroutine(waitRandomAni());
        }
    }

    private IEnumerator waitRandomAni()
    {
        while(true)
        {
            if(mActionManager != null && mShowObj != null)
            {
                yield return new WaitForSeconds(0.1f);
                if (!mShowObj.activeSelf)
                    break;

                int num = 0;
                while (true)
                {
                    num++;
                    if (num > aniArr.Length)
                        break;
                    int random = UnityEngine.Random.Range(0, aniArr.Length);
                    if (mActionManager.GetAniLen(aniArr[random]) > 0)
                    {
                        mActionManager.PlayCommonAnimation(aniArr[random], randomAniEnd);
                        break;
                    }
                }
                break;
            }
            yield return null;
        }
    }

    private void randomAniEnd(int i)
    {
        if (mActionManager != null)
            mActionManager.PlayCommonAnimation("idle");

        if (randomEndCall != null)
            randomEndCall();
    }

    public void Stop()
    {
        if(mActionManager != null)
            mActionManager.Stop();
    }

    protected override void SetActorTypes()
    {
        if (isActorType(ActorType.Monster))
        {
        }
        else if (isActorType(ActorType.Boss))
        {
            t_monster_boosBean mBean = ConfigBean.GetBean<t_monster_boosBean, int>(mTemplateId);
            if (mBean != null)
            {
                t_pet_soulBean soulBean = ConfigBean.GetBean<t_pet_soulBean, int>(mBean.t_soul_detail_type);
                if (soulBean != null)
                    SoulType = (SoulType)soulBean.t_type;
            }
        }
        else
        {
            t_petBean mBean = ConfigBean.GetBean<t_petBean, int>(mTemplateId);
            if (mBean != null)
            {
                SoulType = UIUtils.GetSoulType(mBean.t_soul_detail_type);
            }
        }
    }

}