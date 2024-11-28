using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 游戏状态
/// </summary>
public class GameState
{
	public const string Invalid = "Invalid";
    public const string UpdateRes = "UpdateRes";            //更新资源
	public const string Loading = "Loading";                     // 登陆
    public const string MainCity = "MainCity";           // 游戏主城
	public const string Battle = "Battle";                   // 战斗状态
    public const string BattleReplay = "Replay";        //战斗回放
	public const string Login = "Login";            // pvp匹配状态
    public const string Character = "Character";     // 选人
    public const string Guild = "Guild";            //工会广场状态
}

[System.Serializable]
public class DebugInfo
{
    public bool IsDebug = false;
    public string debugState = GameState.Battle;
    public int missionId = 10101;
    public int petAtk = 100;
    public int monsterAtk = 10;
    public bool maxMp = false;
    public List<int> pets = new List<int>();
}

public class GameManager : SingletonBehaviour<GameManager>
{

	private StateMachine mStateMachine;

    public DebugInfo DebugInfo;

    public bool IsDebug
    {
        get
        {
            if (DebugInfo != null)
                return DebugInfo.IsDebug;
            return false;
        }
    }

	private string CurrentState = GameState.Login;
    private string LastState = GameState.Invalid;      //上一个状态

    public string GetLastState()
    {
        return LastState;
    }

    protected override void Awake()
	{
		base.Awake();
        //全局日志
        Debuger.Enabled = true;
        Application.logMessageReceived += Debuger.OnSystemLog;
        //Debuger.SetTrigger();

        Logger.print("----------Application Awake-------------");
        #if UNITY_EDITOR
            Logger.CurLevel = Logger.Level.Print | Logger.Level.Log | Logger.Level.Error | Logger.Level.Warning;
        #else
            Logger.CurLevel = Logger.Level.Error;
        #endif
        //Debug.logger.logEnabled = false;
        Application.runInBackground = true;
		Application.targetFrameRate = 60;

        QualitySetting.Singleton.Init();
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
	}


	protected override void Start ()
	{
        if (!GameConfig.Init())
        {
            Logger.err("无法初始化配置!");
            Application.Quit();
        }
        else
        {
            DontDestroyOnLoad(gameObject);

            // 初始化单件
            initSingleton();
            Logger.print("---initializeSingleton end---");

            // 初始化游戏状态机
            initGameState();

            // 初始化玩家信息
            //PlayerInfo.Singleton.init();

            //初始化窗口
            WinMgr.Singleton.Init();

            if (IsDebug)
            {
                //加载资源依赖
                ResDepManager.Singleton.LoadDeps();
                ConfigManager.Singleton.LoadBeans();
                //加载表
                Data.Containers.GameDataManager.Instance.loadAll();
                // 初始化所有Service
                new InitServiceCmd().Excute();
                BattleService.Singleton.Init(DebugInfo.missionId);
                // 窗口注册
                new RegisterWindowCmd().Excute();
                CurrentState = DebugInfo.debugState;
            }
            else
            {
                CurrentState = GameState.UpdateRes;
            }
            changeState(CurrentState);
        }
    }

	protected override void Update ()
	{
		base.Update();
		if (mStateMachine != null)
			mStateMachine.update();
        MessageHandle.GetInstance().Update();
        CPlayerInput.GetInstance().Update();
        //CTimerManager.GetInstance().Update();
	}

	private void initSingleton()
	{
		gameObject.GetOrAddComponent<CoroutineManager>();
    }

	private void initGameState()
	{
		mStateMachine = new StateMachine(this);
        mStateMachine.registerState(GameState.UpdateRes, new GameUpdateResState());
        mStateMachine.registerState(GameState.Login, new GameLoginState());
        mStateMachine.registerState(GameState.Character, new GameCharacterState());
        mStateMachine.registerState(GameState.Loading, new GameLoadingState());
        mStateMachine.registerState(GameState.MainCity, new GameMainCityState());
        mStateMachine.registerState(GameState.Battle, new GameBattleState());
        mStateMachine.registerState(GameState.BattleReplay, new GameBattleReplayState());
        mStateMachine.registerState(GameState.Guild, new GameGuildState());
	}

	public void changeState(string gameState, object obj = null)
	{
		if (mStateMachine != null)
		{
            if (CurrentState != GameState.Loading)
            {
                LastState = CurrentState;
            }
            CurrentState = gameState;
			mStateMachine.changeState(gameState, obj);
		}
	}

    public bool IsStateOf(string gameState)
    {
        return CurrentState == gameState;
    }

    private void _SaveDataToLocal()
    {
        ChatService.Singleton.SaveLocalChatInfo();
    }

    protected override void OnApplicationPause(bool paused)
    {
        if(paused == false)
        {
            //回到游戏
            if (LoginService.Singleton != null)
                LoginService.Singleton.DoRelogin();
        }else
        {
            //切出游戏
        }
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        _SaveDataToLocal();
        ThreadHandler.StopAll();

        WinMgr.Singleton.Uninit();
        AudioManager.Singleton.StopAll();
        ActorManager.Singleton.RemoveAll();
        SceneLoader.Singleton.OnGameLeave();
        ActorManagerMC.Singleton.RemoveAll();

        ServiceManager.Singleton.ClearData();
        ResPack.CheckResRelease();
        UIResMgr.Singleton.CheckPkgRef();

        ResManager.Singleton.GC();
        ResManager.Singleton.DebugAllRefNow();

        CPlayerInput.GetInstance().ClearClickEffect();
        AttributeTipShow.Singleton.OnClose();
    }
}
