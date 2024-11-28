
using UnityEngine;
using FairyGUI;
using DG.Tweening;

/// <summary>
/// 非战斗型的实体
/// </summary>

public abstract class AngelaBaby : ActorBase
{
    protected ResPack resPacker;
    // 动作管理器
    protected ActionManager mActionManager;
    // 实体状态机
    protected StateMachine mStateMachine;
    // 冷却管理器
    public CdMgr CdMgr { protected set; get; }
    // 主城行走速度
    protected float mVelocity = 2.5f;
    public float Velocity { get { return mVelocity; } }

    public AngelaBaby(int temlateId, ActorType type, ActorCamp camp, long roleId)
        : base(temlateId, type, camp, roleId)
    {
        resPacker = new ResPack(this);
        mStateMachine = new StateMachine(this);
        mActionManager = new ActionManager(this);
        CdMgr = new CdMgr();
    }

    // 初始化
    public override bool initialize(ActorParam instanceData)
    {
        SetActorTypes();
        loadPrefab(instanceData);
        addComponent(instanceData);
        registerAllState();
        changeState(ActorState.idle);
        return true;
    }

    protected virtual void addComponent(ActorParam instanceData)
    {
        if (mShowObj == null)
            return;

        // GameObject
        mBehavior = mShowObj.GetOrAddComponent<ActorBehavior>();
        mBehavior.actorId = mId;

        mActionManager.SetSimpleAnimation(mBehavior.saniComp);
    }

    /// <summary>
    /// 返回状态机 (只允许在需要绕过打断验证的情况下直接使用内置状态机)
    /// </summary>
    /// <returns></returns>
    public StateMachine getStateMachine()
    {
        return mStateMachine;
    }

    /// <summary>
    /// 显示与隐藏 
    /// </summary>
    /// <param name="flag"></param>
    public virtual void ToggleVisible(bool flag, bool onlyMain=true)
    {
        if (mShowObj != null)
        {
            if (onlyMain)
                monoBehavior.mainObj.gameObject.SetActive(flag);
            else
                mShowObj.SetActive(flag);
        }
    }

    /// <summary>
    /// 注册状态
    /// </summary>
    /// <param name="stateType"></param>
    /// <param name="state"></param>
    public void registerState(string stateType, State state)
    {
       mStateMachine.registerState(stateType, state);
    }

    public abstract void registerAllState();

    public void FaceOrBackCamera(bool isFace)
    {
        Vector3 rot = Camera.main.transform.rotation.eulerAngles;
        Vector3 nowRot = TransformExt.rotation.eulerAngles;
        if (isFace)
            TransformExt.rotateY(rot.y + 180-nowRot.y);
        else
            TransformExt.rotateY(rot.y-nowRot.y);
    }

    /*****************************move methods*************************/
    /// <summary>
    /// 往正前方移动
    /// </summary>
    public virtual void move()
    {
        TransformExt.Translate(getSpeed(), Space.World);
    }

    public virtual void move(Vector3 dir)
    {
        Vector3 speed = dir.normalized * mVelocity * Time.deltaTime;
        TransformExt.Translate(speed);
    }

    protected virtual Vector3 getSpeed()
    {
        return TransformExt.forward * mVelocity * Time.deltaTime;
    }

    /*****************************move methods*************************/

    /// <summary>
    /// 设置朝向
    /// </summary>
    /// <param name="vec"></param>
    public void setDirection(Vector3 vec)
    {
        if (TransformExt != null )
        {
            TransformExt.forward = vec.normalized;
        }
    }

    protected Tween rotTween;
    public void TweenDirection(Vector3 vec, float dur=0.2f)
    {
        if (TransformExt != null)
        {
            if (rotTween != null && rotTween.IsActive())
                rotTween.Kill();
            Quaternion rot = Quaternion.LookRotation(vec);
            TransformExt.DORotate(rot.eulerAngles, dur);
        }
    }

    protected abstract void loadPrefab(ActorParam instanceData);

    public virtual void createHeadBar()
    {

    }

    /// <summary>
    /// 返回动画管理器
    /// </summary>
    /// <returns></returns>
    public ActionManager GetActionManager()
    {
        return mActionManager;
    }

    /// <summary>
    /// 改变状态
    /// </summary>
    /// <param name="state">CommonState基本状态 NoneActionState没有动作的状态</param>
    /// <param name="actionId"></param>
    /// <param name="obj"></param>
    /// <returns></returns>
    public virtual bool changeState(string state, object obj = null)
    {

        if (isStateOf(ActorState.dead))
        {
            return false;
        }

        //攻击状态不能被任何状态打断(主要处理攻击的时候被伤害)
        if (isStateOf(ActorState.attack))
        {
            Logger.err(state + "切换失败，正处于技能状态之中");
            return false;
        }

        //移动或者待机直接onReEnter避免再次播动画
        if ((state == ActorState.move || state == ActorState.idle || state == ActorState.hurt) && isStateOf(state))
        {
            ActorBaseState actorState = mStateMachine.getStateByKey(state) as ActorBaseState;
            if (actorState != null)
                actorState.onReEnter(obj);
            return false;
        }

        //已经不再需要动作保护时间
        /*if (mActionManager.CanInterruptCurrentAction())
        {
            mStateMachine.changeState(state, obj);
            return true;
        }*/

        mStateMachine.changeState(state, obj);
        return true;
    }

    public bool isExistState(string stateKey)
    {
        return mStateMachine.isExist(stateKey);
    }

    public override void update()
    {
        if(mStateMachine != null)
            mStateMachine.update();
    }

    public override void onTriggerEnter(Collider other)
    {
    }

    public override void onTriggerStay(Collider other)
    {
    }

    public override void onTriggerExit(Collider other)
    {
    }

    /// <summary>
    /// 当前状态是否为某个状态 
    /// </summary>
    /// <param name="state"></param>
    /// <returns></returns>
    public bool isStateOf(string state)
    {
        string nowState = mStateMachine.getCurrentState();
        if (nowState == state)
            return true;
        return false;
    }

    public bool IsDestoryed { protected set; get; }
    /// <summary>
    /// 直接销毁改角色（与killme的区别是后者会走死亡流程）
    /// </summary>
    public override void destoryMe()
    {
        mActionManager.Clear();
        mStateMachine.clear();
        CdMgr.clear();
        mBehavior = null;
        if (mShowObj != null)
        {
            Object.DestroyImmediate(mShowObj);
            mShowObj = null;
        }
        IsDestoryed = true;
        resPacker.ReleaseAllRes();
    }
}
