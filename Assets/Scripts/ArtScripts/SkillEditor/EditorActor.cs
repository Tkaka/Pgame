
public class EditorActor : Actor
{

    public ActionManager ActionMgr;

    public EditorActor(int temlateId, ActorType type, ActorCamp camp, long roleId)
       : base(temlateId, type, camp, roleId)
    {
        mStateMachine = new StateMachine(this);
        mActionManager = new ActionManager(this);
        CdMgr = new CdMgr();
    }

    public override bool initialize(ActorParam instanceData)
    {
        return true;
    }

    public void Init(ActorBehavior actorBehavior)
    {
        mBehavior = actorBehavior;
        mBehavior.actorId = 999;
        this.mShowObj = mBehavior.gameObject;
        mActionManager.SetSimpleAnimation(mBehavior.saniComp);
        registerAllState();
        PropertyMgr = new ActorPropertyManager(this);
        PropertyMgr.InitForEditor();
        ViewPropertyMgr.Initialize();
        changeState(ActorState.idle);
        OriginPos = TransformExt.position;
        OriginDir = TransformExt.forward;
    }

    public override void registerAllState()
    {
        mStateMachine.registerState(ActorState.idle, new ActorIdleState());
        mStateMachine.registerState(ActorState.hurt, new ActorHurtState());
    }


    public void PlayAni(string name)
    {
        mActionManager.PlayCommonAnimation(name);
    }

    protected override void loadPrefab(ActorParam instanceData)
    {
        
    }

    protected override void SetActorTypes()
    {
        
    }

    public override bool IsActorRace(ActorRace raceType)
    {
        return false;
    }

    public override void update()
    {
        base.update();
    }

}
