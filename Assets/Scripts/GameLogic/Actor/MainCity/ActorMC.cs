
using Data.Beans;
using UnityEngine;
using System.Collections.Generic;

public class MCMoveParam
{
    public Vector3 dir;
    public float dur;
    public Vector3 targetPos;
    public Vector3 oldPos;
}

public class ActorMC : AngelaBaby
{
    private HangPointnInfo m_hangWnd;
    MainCityPetInfo petInfo;
    MianCityPetName petName;
    public ActorMC(int temlateId, ActorType type, ActorCamp camp, long roleId) : base(temlateId, type, camp, roleId)
    {

    }

    public override void registerAllState()
    {
        registerState(ActorState.idle, new ActorIdleStateMC());
        registerState(ActorState.move, new ActorMoveStateMC());
        registerState(ActorState.mc_move, new ActorMCTweenMoveState());

    }

    protected override void loadPrefab(ActorParam instanceData)
    {
        if (isActorType(ActorType.Player))
        {
            t_professionBean mBean = ConfigBean.GetBean<t_professionBean, int>(mTemplateId);
            if (mBean != null)
            {
                mShowObj = resPacker.LoadGo(mBean.t_city_prefab, instanceData.Pos);
                //mShowObj = Res.Singleton.InstantiateModel(mBean.t_city_prefab, instanceData.Pos);
            }
        }
        else if (isActorType(ActorType.Pet))
        {
            t_petBean mBean = ConfigBean.GetBean<t_petBean, int>(mTemplateId);
            if (mBean != null)
            {
                mShowObj = resPacker.LoadGo(UIUtils.GetCityPrefab(mBean, instanceData.Star), instanceData.Pos);
                //mShowObj = Res.Singleton.InstantiateModel(UIUtils.GetCityPrefab(mBean), instanceData.Pos);
            }
        }
    }


    private long runAniCoroId = 0;
    public virtual void MoveToTarget(Vector3 targetPos)
    {
        if (isStateOf(ActorState.mc_move))
            return;

        Vector3 oldPos = TransformExt.position;
        MCMoveParam param = new MCMoveParam();
        param.targetPos = targetPos;
        param.dir = (targetPos - TransformExt.position).normalized;
        param.oldPos = TransformExt.position;
         
        float dis = GTools.distanceIgnoreY(targetPos, oldPos);
        float dur = dis / mVelocity;
        param.dur = dur;
        changeState(ActorState.mc_move, param);
        //monoBehavior.OnDead(dur, false);
        runAniCoroId = CoroutineManager.Singleton.delayedCall(dur, () =>
        {
            if(!isStateOf(ActorState.idle))
                changeState(ActorState.idle);
            //(headBar as MonsterHeadBar).ShowHeadBar();
        });
    }


    public void createHangPointInfo(string strInfo)
    {
        m_hangWnd = HangPointnInfo.CreateInstance();
        m_hangWnd.Init(this);
        m_hangWnd.RefreshView(strInfo);

    }


    public override void destoryMe()
    {
        base.destoryMe();
        RemovePetInfo();
        if (runAniCoroId != 0)
        {
            CoroutineManager.Singleton.stopCoroutine(runAniCoroId);
        }

        if (m_hangWnd != null)
        {
            m_hangWnd.Destroy();
            m_hangWnd = null;
        }
    }

    protected override void SetActorTypes()
    {
       
    }
    /// <summary>
    /// 刷新主城的信息显示
    /// </summary>
    public void RefreshMainCityPetInfo()
    {
        if (this.getActorType() == ActorType.Player)
            return;
        if (petInfo != null)
            petInfo.RefreshView(this);
        else
        {
            petInfo = MainCityPetInfo.CreateInstance();
            petInfo.RefreshView(this);
        }

        if(petName != null)
            petName.RefreshView(this);
        else
        {
            petName = MianCityPetName.CreateInstance();
            petName.RefreshView(this);
        }
    }
    public void HideMainCityPetInfo()
    {
        if (petInfo != null)
            petInfo.visible = false;
        if (petName != null)
            petName.visible = false;

        petInfo = null;
        petName = null;
    }

    private void RemovePetInfo()
    {
        if (petInfo != null)
            petInfo.Dispose();
        if (petName != null)
            petName.Dispose();
    }

}