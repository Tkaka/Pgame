using System.Collections.Generic;
using UnityEngine;
using Data.Beans;

public class FightState
{
    public const string PreState = "PreState";
    public const string PrepareNextTurnState = "PrepareNextTurnState";
    public const string AutoAttack = "AutoAttack";
    public const string MannulNormalAttack = "MannulNormalAttack";
    public const string MannulAttack = "MannulAttack";
    public const string TurnStart = "TurnStart";
    public const string CatchPet = "CatchPet";
    public const string ShowRare = "ShowRare";
    public const string MoveState = "MoveState";
    public const string ReplayChangeWaveState = "ReplayChangeWave";
    public const string BossEnter = "BossEnter";
    public const string DialogState = "DialogState";
    public const string BossIntroduce = "BossIntroduce";
    public const string RunAway = "Runaway";    //逃跑
    public const string Idle = "Idle";
    public const string End = "End";
}

public enum ChangeReason
{
    None,
    Start,
    CanotAction,
    AllAttack,
    AllRemove,
    MoveCmp,
    MaxTurn,
    BossBriefCmp,
    DialogCmp,
    RunAwayCmp,
}

public class FightManager : SingletonTemplate<FightManager>
{

    public LineupGrid Grid { private set; get; }

    public bool PlayerTurn { get { return CurTurn == ActorCamp.CampFriend; } }

    public void ChangeTurn()
    {
        if (CurTurn == ActorCamp.CampFriend)
            CurTurn = ActorCamp.CampEnemy;
        else
            CurTurn = ActorCamp.CampFriend;
    }

    //当前是谁的回合
    public ActorCamp CurTurn = ActorCamp.CampFriend;

    //先手轮
    public ActorCamp FirstTurn = ActorCamp.CampFriend;

    public const float attackGap = 1.2f;   //攻击间隔：秒

    public const float turnGap = 0.5f;     //攻击间隔：秒

    public static float turnGapDelta = 0.0f;      //

    public const float soulBallTime = 0.8f;       //魂珠回收时间

    private StateMachine stateMachine;

    public List<SoulBall> soulBallList = new List<SoulBall>();

    public int TurnCount { get; set; }

    public long selectedId;

    public bool IsAuto = false;

    public bool CurIsAuto = false;

    public float GameSpeed { get; set; }

    public bool IsPause = false;

    public bool IsNewWave = true;

    public bool IsReplay = false;

    private int[] mMaxTurns;   //每波的最大轮数

    public int MaxTurnNum  //当前波的最大回合数
    {
        get
        {
            if (mMaxTurns == null || mMaxTurns.Length <= SpawnerHelper.Singleton.CurWave)
                return 20;
            else
                return mMaxTurns[SpawnerHelper.Singleton.CurWave];
        }
    }

    public RandomExt randomExt;
 

    public CdMgr CdMgr { private set; get; }

    /// <summary>
    /// 连击次数
    /// </summary>
    public static int ComboCount = 1;

    /// <summary>
    /// 连击加成
    /// </summary>
    public static LNumber ComboAdd;

    /// <summary>
    /// 该轮总伤害
    /// </summary>
    public long CurTurnHurt = 0;

    //战斗被动效果事件分发器
    public static GameEventDispatcher ED = new GameEventDispatcher();

    //资源管理
    public static ResPack R = new ResPack("fightManager");

    /// <summary>
    /// 连击提示模式
    /// </summary>
    public ComboModel comboModel = ComboModel.Line;

    //我方离场人员（包括重生等待复活的）
    public List<long> FriendLevelList { private set; get; }
    //敌方离场人员（包括重生等待复活的）
    public List<long> EnemyLevelList { private set; get; }

    //当前轮敌方总血量
    public LNumber CurWaveEnemyTotalHp { get; set; }

    //是否已经进入普攻阶段
    public bool normalAtkPhase;

    //是否已经扔过球
    public bool ballThrowed = false;

    public void SetRandomNum(long seed)
    {
        randomExt = new RandomExt(seed);
    }

    public void Stop()
    {
        GED.ED.removeListener(EventID.OnSelectActor, OnSelectedActor);
        if (stateMachine != null)
            stateMachine.changeState(FightState.Idle);
        FirstTurn = ActorCamp.CampFriend;
        CurTurn = ActorCamp.CampFriend;
        LastSeletedId = 0;
        TurnCount = 0;
        turnGapDelta = 0;
        mMaxTurns = null;
        IsNewWave = true;
        IsAuto = false;
        DestroySelector();
        if (soulBallList.Count > 0)
        {
            foreach (SoulBall ball in soulBallList)
            {
                ball.DestroySelf();
            }
            soulBallList.Clear();
        }
        if (FriendLevelList != null)
            FriendLevelList.Clear();
        if (EnemyLevelList != null)
            EnemyLevelList.Clear();
        Grid.Clear();
        CdMgr.clear();
        normalAtkPhase = false;
    }

    public void AddCloseShotCd(long dur)
    {
        CdMgr.addCoolDown("closeShot", dur);
    }

    public bool CanPlayCloseShot()
    {
        return CdMgr.isCoolDown("closeShot");
    }

    public void Update()
    {
        if(stateMachine != null)
            stateMachine.update();
    }

    public FightManager()
    {
        GameSpeed = 1.0f;
        ComboAdd = 1;
        stateMachine = new StateMachine(this);
        stateMachine.registerState(FightState.PreState, new FightPrepareState());
        stateMachine.registerState(FightState.PrepareNextTurnState, new PrepareNextTurnState());
        stateMachine.registerState(FightState.MoveState, new FightMoveState());
        stateMachine.registerState(FightState.ReplayChangeWaveState, new ReplayChangeWaveState());
        stateMachine.registerState(FightState.AutoAttack, new FightAutoAttackState());
        stateMachine.registerState(FightState.MannulAttack, new FightMannulAttackState());
        stateMachine.registerState(FightState.BossIntroduce, new FightBossIntroduceState());
        stateMachine.registerState(FightState.BossEnter, new BossEnterState());
        stateMachine.registerState(FightState.DialogState, new FightDialogState());
        stateMachine.registerState(FightState.TurnStart, new FightTurnStartState());
        stateMachine.registerState(FightState.RunAway, new FightRunAwayState());
        stateMachine.registerState(FightState.Idle, new FightIdleState());
        stateMachine.registerState(FightState.End, new FightEndState());
        Grid = new LineupGrid();
        FriendLevelList = new List<long>();
        EnemyLevelList = new List<long>();
        CdMgr = new CdMgr();
    }

    public bool IsStateOf(string state)
    {
        return stateMachine.getCurrentState() == state;
    }

    /*public float ResetGameSpeed()
    {
        if (GameSpeed == 1.0f)
        {
            GameSpeed = 1.5f;
            Time.timeScale = 1.5f;
        }
        else if (GameSpeed == 1.5f)
        {
            GameSpeed = 2.0f;
            Time.timeScale = 2.0f;
        }
        else if (GameSpeed == 2.0f)
        {
            GameSpeed = 0.8f;
            Time.timeScale = 0.8f;
        }
        else if (GameSpeed == 0.8f)
        {
            GameSpeed = 1.0f;
            Time.timeScale = 1.0f;
        }
        ComboCtrl.Singleton.SetSpeed();
        return GameSpeed;
    }*/

    public float ResetGameSpeed()
    {
        if (GameSpeed == 1.0f)
        {
            GameSpeed = 1.2f;
            Time.timeScale = 1.2f;
        }
        else if (GameSpeed == 1.2f)
        {
            GameSpeed = 0.8f;
            Time.timeScale = 0.8f;
        }
        else if (GameSpeed == 0.8f)
        {
            GameSpeed = 1.0f;
            Time.timeScale = 1.0f;
        }
        ComboCtrl.Singleton.SetSpeed();
        return GameSpeed;
    }

 


    public void Start()
    {
        MasterSkillCtrl.Singleton.Init();
        GED.ED.addListener(EventID.OnSelectActor, OnSelectedActor);
        Time.timeScale = GameSpeed;
        IsNewWave = true;
        PrepareNextTurn(ChangeReason.Start);
        setCameraCulling(true);
    }

    public void PrepareNextTurn(ChangeReason reason)
    {
        ChangeState(FightState.PrepareNextTurnState, reason);
    }

    /// <summary>
    /// 重置角色状态（在此处理 眩晕 麻痹 冰冻 --- 三者都无法行动）
    /// </summary>
    public void ResetActorStatus()
    {
        ActorTurnStatus[] target = null;
        if (PlayerTurn)
        {
            target = Grid.PlayerCamp;
        }
        else
        {
            target = Grid.EnemyCamp;
        }
        for (int i = 0; i < target.Length; i++)
        {
            target[i].attacked = false;
            target[i].InCmdQueue = false;
            target[i].MasterInCmdQueue = false;
            if (target[i].actorId <= 0)
                continue;
            Actor actor = ActorManager.Singleton.Get(target[i].actorId);
            if (actor != null)
            {
                bool willSmallSkill = false;
                target[i].skillId = AttackUtils.WillAtkSkillId(actor, out willSmallSkill);
                actor.WillUseSmallSkill = willSmallSkill;
            }
        }
    }


    //设置所有波的最大轮数
    public void SetMaxTurns(int wave, int turn)
    {
        mMaxTurns = new int[wave];
        for (int i = 0; i < mMaxTurns.Length; i++)
        {
            mMaxTurns[i] = turn;
        }
    }

    public void SetMaxTurns(int[] turns)
    {
        mMaxTurns = turns;
    }

    /// <summary>
    /// 可以攻击的数量
    /// </summary>
    public int CanAtkCount()
    {
        int count = 0;
        ActorTurnStatus[] target = null;
        if (PlayerTurn)
            target = Grid.PlayerCamp;
        else
            target = Grid.EnemyCamp;
        for (int i = 0; i < target.Length; i++)
        {
            if (target[i].CanAction())
                count++;
        }
        return count;
    }

    public bool IsCurTurnCmp()
    {
        ActorTurnStatus[] target = null;
        if (PlayerTurn)
            target = Grid.PlayerCamp;
        else
            target = Grid.EnemyCamp;
        for (int i = 0; i < target.Length; i++)
        {
            if (target[i].InCmdQueue || target[i].MasterInCmdQueue)
                return false;
            else if (target[i].CanAction())
                return false;
        }
        return true;
    }


    public void Attack(ActorCamp camp, int gridId, bool masterSkill)
    {
        ActorTurnStatus[] target = Grid.GetCamp(camp);
        if (gridId < target.Length)
        {
            if (masterSkill)
            {
                target[gridId].MasterInCmdQueue = false;
            }
            else
            {
                target[gridId].attacked = true;
                target[gridId].InCmdQueue = false;
            }
        }
        //存活数量小于0时，等待remove接口去调用
        int aliveNum = Grid.AliveNum(AttackUtils.GetEnemyCamp(camp));
        if (aliveNum > 0 && IsCurTurnCmp())
        {
            PrepareNextTurn(ChangeReason.AllAttack);
        }
    }

    public void CmdEnqueue(ActorCamp camp, int gridId, bool masterSkill)
    {
        ActorTurnStatus[] target = Grid.GetCamp(camp);
        if (gridId < target.Length)
        {
            if(masterSkill)
                target[gridId].MasterInCmdQueue = true;
            else
                target[gridId].InCmdQueue = true;
        }
    }

    public void ChangeState(string stateKey, object obj=null)
    {
        if (stateMachine != null)
        {
            if (stateMachine.getCurrentState() == stateKey)
            {
                Logger.log("重复进入状态" + stateKey);
                return;
            }
            stateMachine.changeState(stateKey, obj);
            BattleWindMgr.CurrentBtlWin.ChangeFightState(stateKey, obj);
        }
    }

    /*--------------------------------------------------------------------------------*/
    public void Add(ActorCamp camp, long actorId, int gridId)
    {
        Grid.Add(camp, actorId, gridId);
    }

    public void Remove(ActorCamp camp, long actorId, int gridId)
    {
        Grid.Remove(camp, actorId, gridId);
        //如果是敌方阵营，重新选择对象
        if (camp == ActorCamp.CampEnemy && LastSeletedId == actorId)
            SelectAGoodEnemy();

        if (Grid.AliveNum(ActorCamp.CampEnemy, false) <= 0 ||
            Grid.AliveNum(ActorCamp.CampFriend, false) <= 0)
        {
            turnGapDelta = 0;
            PrepareNextTurn(ChangeReason.AllRemove);
        }

    }

    public long GetNextActor(ActorCamp camp)
    {
        ActorTurnStatus[] target = Grid.GetCamp(camp);
        for (int i = 0; i < target.Length; i++)
        {
            if (target[i].actorId <= 0)
                continue;
            if (target[i].CanAction())
                return target[i].actorId;
        }
        return 0;
    }


    /// <summary>
    /// 未攻击的数量
    /// </summary>
    /// <param name="camp"></param>
    /// <returns></returns>
    public List<long> UnAtkCount(ActorCamp camp)
    {
        List<long> res = new List<long>();
        ActorTurnStatus[] target = Grid.GetCamp(camp);
        for (int i = 0; i < target.Length; i++)
        {
            if (target[i].CanAction())     //能够行动
            {
                res.Add(target[i].actorId);
            }
        }
        return res;
    }

    public bool IsLastOne(ActorCamp camp, long actorId)
    {
        List<long> unAtk = UnAtkCount(camp);
        if (unAtk.Count <= 0)
            return true;
        if (unAtk.Count == 1)
        {
            return unAtk[0] == actorId;
        }
        return false;
    }

    public long GetAGoodTarget(ActorCamp camp)
    {
        ActorTurnStatus[] target = Grid.GetCamp(camp);
        for (int i = 0; i < target.Length; i++)
        {
            if (target[i].actorId > 0 && !target[i].IsActuallyDead)
                return target[i].actorId;
        }
        return 0;
    }

    public void SelectAGoodEnemy()
    {
        OnSelectedActor(new GameEvent((int)EventID.OnSelectActor, GetAGoodTarget(ActorCamp.CampEnemy)));
    }


    /// <summary>
    /// 获取某个阵营的总血量
    /// </summary>
    /// <param name="camp"></param>
    /// <returns></returns>
    public LNumber GetTotalHp(ActorCamp camp, bool isBase)
    {
        LNumber res = 0;
        ActorTurnStatus[] target = Grid.GetCamp(camp);
        for (int i = 0; i < target.Length; i++)
        {
            if (target[i].actorId > 0)
            {
                Actor actor = ActorManager.Singleton.Get(target[i].actorId);
                if (actor != null)
                {
                    if(isBase)
                        res += actor.ViewPropertyMgr.GetBaseProperty(PropertyType.Hp);
                    else
                        res += actor.ViewPropertyMgr.GetProperty(PropertyType.Hp);
                    
                }
            }
        }
        return res;
    }

    private bool IsExistDialog()
    {
        return false;
        t_dungeon_actBean bean = ConfigBean.GetBean<t_dungeon_actBean, int>(BattleService.Singleton.MissionId);
        if (SpawnerHelper.Singleton.CurWave == 0)
        {
            return !string.IsNullOrEmpty(bean.t_wave_dialog1);
        }
        else if (SpawnerHelper.Singleton.CurWave == 1)
        {
            return !string.IsNullOrEmpty(bean.t_wave_dialog2);
        }
        else if (SpawnerHelper.Singleton.CurWave == 2)
        {
            return !string.IsNullOrEmpty(bean.t_wave_dialog3);
        }
        return false;
    }

    /*--------------------------------------------------------------------------------*/

    /// <summary>
    /// 该怪物是否能被选中
    /// </summary>
    /// <param name="actorId"></param>
    /// <returns></returns>
    public bool CanSelect(long actorId)
    {
        int row = Grid.InWhichRow(ActorCamp.CampEnemy, actorId);
        if (row == GridEnum.Row1)
        {
            int num = Grid.RowAliveNum(ActorCamp.CampEnemy, GridEnum.Row0);
            if (num > 0)
                return false;
            else
                return true;
        }
        else if (row == GridEnum.Row0)
        {
            return true;
        }
        return false;
    }

    private GameObject normalSelector;
    public long LastSeletedId { get; set; }
    private void OnSelectedActor(GameEvent e)
    {
        if (e != null && e.Data != null)
        {
            long actorId = (long)e.Data;
            if (!CanSelect(actorId))
                return;
            Actor actor = ActorManager.Singleton.Get(actorId);
            if (actor != null && actor.isCampOf(ActorCamp.CampEnemy))
            {
                //选中同一个直接返回
                if (e.EventId > 0 && LastSeletedId == actorId)
                {
                    ComboCtrl.Singleton.OnModelClick();
                    return;
                }
                actor.IsSelected = true;
                Actor lastActor = ActorManager.Singleton.Get(LastSeletedId);
                if (lastActor != null)
                    lastActor.IsSelected = false;
                LastSeletedId = actorId;
                Vector3 pos = actor.TransformExt.position;
                if (actor.monoBehavior.hitPos != null)
                    pos = actor.monoBehavior.hitPos.position;
                BattleCDCtrl.Singleton.ResetSelectorPos(pos);
            }
        }
    }

    public void DestroySelector()
    {
        UnityEngine.Object.Destroy(normalSelector);
        normalSelector = null;
    }

    /*--------------------------------------------------------------------------------*/

    private long cullingTimer;
    public void ChangeCameraCulling(bool isSkill, float aniDuration)
    {
        CoroutineManager.Singleton.stopCoroutine(cullingTimer);
        float changeTime, backTime;
        if (isSkill)
        {
            backTime = 0.5f;
            changeTime = aniDuration - 0.5f;
        }
        else
        {
            backTime = 0.5f;
            changeTime = aniDuration - 0.5f;
        }

        //将特殊场景部分显示出来
        if (changeTime > 0)
            cullingTimer = CoroutineManager.Singleton.delayedCall(changeTime, () => { setCameraCulling(false); });
        else
            setCameraCulling(false);

        //将特殊场景部分隐藏回去
        if (backTime < changeTime)
            backTime = changeTime;
        if (backTime > 0)
            cullingTimer = CoroutineManager.Singleton.delayedCall(changeTime, () => { setCameraCulling(true); });
        else
            setCameraCulling(true);
    }

    private void setCameraCulling(bool normalView)
    {
        if (!GameManager.Singleton.IsStateOf(GameState.Battle))
            return;

        var camera = Camera.main;
        if (camera == null)
            camera = Camera.current;
        if (camera == null)
            return;

        if (normalView)
            camera.cullingMask = LayerMask.GetMask("Default", "Actor", "Effect", "ShadowReciver");
        else
            camera.cullingMask = LayerMask.GetMask("Default", "Actor", "Effect", "ShadowReciver", "SceneShadow");
    }
}