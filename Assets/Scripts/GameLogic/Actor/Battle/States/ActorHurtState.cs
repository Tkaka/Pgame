using System;
using System.Collections.Generic;

public class ActorHurtState : ActorBaseState
{

    private Dictionary<HurtSubState, HurtBaseState> stateDic = new Dictionary<HurtSubState, HurtBaseState>();

    private Dictionary<HurtSubState, int> priorityDic = new Dictionary<HurtSubState, int>();

    protected HurtBaseState mCurState;

    public ActorHurtState()
    {
        stateDic.Add(HurtSubState.Normal, new HurtNormalState());
        priorityDic.Add(HurtSubState.Normal, 0);
        stateDic.Add(HurtSubState.KnockOut, new HurtKnockOutState());
        priorityDic.Add(HurtSubState.KnockOut, 1);
        stateDic.Add(HurtSubState.Floating, new HurtFloatingState());
        priorityDic.Add(HurtSubState.Floating, 1);
        HurtFlyState hurtState = new HurtFlyState();
        stateDic.Add(HurtSubState.SmallFly, hurtState);
        priorityDic.Add(HurtSubState.SmallFly, 2);
        stateDic.Add(HurtSubState.Fly, hurtState);
        priorityDic.Add(HurtSubState.Fly, 2);
    }

    public override void onEnter(object obj = null)
    {
        mActor = mOwner as Actor;
        HurtSubState subState = GetHurtState(obj);
        ChangeState(subState);
    }

    private HurtSubState GetHurtState(object obj)
    {
        HurtSubState subState = HurtSubState.Normal;
        if (obj != null && System.Enum.IsDefined(typeof(HurtSubState), obj))
            subState = (HurtSubState)obj;
        else
            Logger.err("hurt state onenter param is error");
        return subState;
    }

    private void ChangeState(HurtSubState subState)
    {
        if (stateDic.ContainsKey(subState))
        {
            if(mCurState != null)
                mCurState.OnLeave();
            mCurState = stateDic[subState];
            mCurState.SetOwner(mActor, subState);
            mCurState.OnEnter();
            //UnityEngine.Debug.Log("change to state : " + subState);
        }
        else
        {
            Logger.err("未注册的伤害子状态");
        }
    }

    public override void onReEnter(object obj = null)
    {
        if (mCurState != null)
        {
            if (mCurState.HurtState == HurtSubState.Fly ||
                mCurState.HurtState == HurtSubState.SmallFly)
                return;

            HurtSubState subState = GetHurtState(obj);
            if (subState == mCurState.HurtState)
                mCurState.OnReEnter();
            else if (priorityDic[subState] >= priorityDic[mCurState.HurtState])
                ChangeState(subState);
        }
    }

    public override void onUpdate()
    {
        base.onUpdate();
        if (mCurState != null)
            mCurState.OnUpdate();
    }

    public override void onLeave(string stateKey)
    {
        base.onLeave(stateKey);
        if (mCurState != null)
        {
            mCurState.OnLeave();
            mCurState = null;
        }
    }

    public override string getStateKey()
    {
        return ActorState.hurt;
    }

}
