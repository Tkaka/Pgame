using Data.Beans;
using FairyGUI;
using Message.Dungeon;
using Message.Fight;
using Message.Replay;
using System.Collections.Generic;

//战斗类型
public enum EFightType
{
    None,
    CoinDungeon = 1,        //金币副本
    ExpDungeon = 2,         //经验副本
    WomanFighterDungeon = 3,//女格斗家副本
    HuanXiangDungeon = 4,   //幻像副本
    ZhongJiShiLian = 5,     //终极试炼
    CloneDungeon = 6,       //克隆组队战
    GuildBossDungeon = 7,   //公会boss
    Level = 8,              //关卡
    Arena  = 9,            // 竞技场
    King_Fight= 10,     // 争霸赛
    Guild_War = 11,     //社团战
}

public class FightService : SingletonService<FightService>
{
    private EFightType m_curFightType;        //当前的战斗类型
    private int m_curFightTypeParam;          //当前战斗类型参数
                                              //private long m_curFightId;                //当前战斗实例ID
                                              //private List<FightParam> m_friendPetFightParam;   //友方宠物的战斗参数
                                              //private List<FightParam> m_enemyPetFightParam;    //敌方宠物战斗参数

    private int[] m_maxTurn;            //每波的最大轮数    
    private int m_maxWaveCount;
    private int m_maxTurnNum = 0;

    public int WaveCount                //当前战斗最大波数
    {
        private set { m_maxWaveCount = value; }
        get { return m_maxWaveCount; }
    }       
    public int MaxTurnNum              //当前最大回合数
    {
        private set { m_maxTurnNum = value; }
        get { return m_maxTurnNum; }
    }        
    public string SceneName { get { return m_fightData.SceneName; } }   //当前战斗场景名

    private BaseFight m_fightData;
    public EFightType FightType { get { return m_curFightType; } }


    public FightService()
    {
        BattleParam.Init();
        if (GameManager.Singleton.IsDebug)
        {
            m_curFightType = EFightType.Level;
            //WaveCount = 3;
            //MaxTurnNum = 20;
            SpawnerHelper.Singleton.WaveCount = 3;
            FightManager.Singleton.SetMaxTurns(3, 20);
            m_curFightTypeParam = 10101;
            ResFight resFight = new ResFight();
            resFight.fightType = (int)EFightType.Level;
            resFight.fightTypeParam = m_curFightTypeParam;
            m_fightData = new LevelFight(resFight);
        }
    }

    protected override void RegisterEventListener()
    {
        base.RegisterEventListener();
        GED.NED.addListener(ResFight.MsgId, _ResFight);
        GED.NED.addListener(ResFightResultInfo.MsgId, _ResFightResultInfo);
    }

    protected override void UnRegisterEventListener()
    {
        base.UnRegisterEventListener();
        GED.NED.removeListener(ResFight.MsgId, _ResFight);
        GED.NED.removeListener(ResFightResultInfo.MsgId, _ResFightResultInfo);
    }

    public override void ClearData()
    {
        base.ClearData();
        m_curFightType = EFightType.None;
        m_curFightTypeParam = -1;
        //m_curFightId = 0;
        //m_friendPetFightParam = null;
        //m_enemyPetFightParam = null;
        m_fightData = null;
    }

    //------------------------------------------------------------Res
    //开始战斗返回
    private void _ResFight(GameEvent evt)
    {
        ResFight msg = GetCurMsg<ResFight>(evt.EventId);
        switch ((EFightType)msg.fightType)
        {
            case EFightType.Level:
                m_fightData = new LevelFight(msg);
                break;
            case EFightType.CoinDungeon:
                m_fightData = new CoinDungeonFight(msg);
                break;
            case EFightType.ExpDungeon:
                m_fightData = new ExpDungeonFight(msg);
                break;
            case EFightType.HuanXiangDungeon:
                m_fightData = new HuanXiangDungeonFight(msg);
                break;
            case EFightType.WomanFighterDungeon:
                m_fightData = new WomanFighterDungeonFight(msg);
                break;
            case EFightType.CloneDungeon:
                m_fightData = new CloneDungeonFight(msg);
                break;
            case EFightType.GuildBossDungeon:
                m_fightData = new GuildDungeonFight(msg);
                break;
            case EFightType.ZhongJiShiLian:
                m_fightData = new UltimateTrialFight(msg);
                break;
               
        }
        //m_curFightId = msg.fightId;
        m_curFightType = (EFightType)msg.fightType;
        m_curFightTypeParam = msg.fightTypeParam;
        m_maxTurnNum = m_fightData.MaxTurnNum;
        m_maxWaveCount = m_fightData.WaveCount;
        //m_enemyPetFightParam = msg.enemyFightParam;
        //m_friendPetFightParam = msg.petFightParams;

        // _Init();

        CmdCollectMgr.Singleton.PrepareCollect();

        SpawnerHelper.Singleton.WaveCount = m_fightData.WaveCount;
        FightManager.Singleton.SetMaxTurns(m_fightData.WaveCount, m_fightData.MaxTurnNum);
        FightManager.Singleton.SetRandomNum(10000);
        WinMgr.Singleton.CloseAll();
        SceneLoader.Singleton.nextState = GameState.Battle;
        SceneLoader.Singleton.sceneName = SceneName;
        GameManager.Singleton.changeState(GameState.Loading);
       



    }

    //战斗结果
    private void _ResFightResultInfo(GameEvent evt)
    {
        ResFightResultInfo msg = GetCurMsg<ResFightResultInfo>(evt.EventId);
 

        switch ((EFightType)msg.fightType)
        {
            case EFightType.Level:
                {
                    BattleService.Singleton.OnResFightRes(msg);
                }  
                break;
            case EFightType.CoinDungeon:
            case EFightType.ExpDungeon:
            case EFightType.HuanXiangDungeon:   
            case EFightType.WomanFighterDungeon:
                {
                    ChallegeService.Singleton.OnResActivityFightEnd(msg);
                    ActivtyBattleResultMgr.Singleton.OpenBattleResultWnd(msg);
                }
                
                break;
            case EFightType.ZhongJiShiLian:
                {
                    //Debuger.Log("-------------------------->>>Result" + msg.result.result);
                    UltemateTrainService.Singleton.OnResTrialFightEnd(msg);
                    if (msg.result.result == 0)
                    {
                        BattleWindow.Singleton.OpenChild<BattleFailedWindow>(WinInfo.Create(false, null, false));
                    }
                    else
                    {
                        WinMgr.Singleton.Open<UltimateSucessWindow>(WinInfo.Create(false, null, false, msg), UILayer.Popup);
                    }
                }
                break;
            case EFightType.CloneDungeon:
                {
                    CloneTeamFightService.Singleton.OnResTeamFightEnd(msg);
                }
                break;
            case EFightType.GuildBossDungeon:
                {
                    if (msg.result.result == 0)
                    {
                        BattleWindow.Singleton.OpenChild<BattleFailedWindow>(WinInfo.Create(false, null, false));
                    }
                    else
                    {
                        WinMgr.Singleton.Open<BattleSucessWindow>(WinInfo.Create(false, null, false, msg), UILayer.Popup);
                    }
                }
                break;
            case EFightType.Arena:
                //竞技场属于重播

                ReplayService.Singleton.SetFightResult(msg);
                //if (msg.result.result == 0)
                //{
                //    BattleWindow.Singleton.OpenChild<BattleFailedWindow>(WinInfo.Create(false, null, false));
                //}
                //else
                //{
                //    WinMgr.Singleton.Open<ArenaSucessWindow>(WinInfo.Create(false, null, false, msg), UILayer.Popup);
                //}
                break;
        }

    }

    //---------------------------------------------------------------Req
    //请求开始战斗
    public void ReqFight(EFightType fightType, int fightTypeParam)
    {

        ReqFight msg = GetEmptyMsg<ReqFight>();
        msg.fightType = (int)fightType;
        msg.fightTypeParam = fightTypeParam;
        SendMsg<ReqFight>(ref msg);
    }


    //递交战斗结果
    //参数：战斗实例ID  战斗结果
    public void ReqFightResult(int result, long star)
    {
        ReqFightResultInfo msg = GetEmptyMsg<ReqFightResultInfo>();


        msg.fightId = m_fightData.GetFightId();
        msg.fightResult = result;
        msg.resultParam = star;

        ReplayInfo replayInfo = CmdCollectMgr.Singleton.GetReplayInfo();
        replayInfo.firstWin = result == 1 ? true : false;
        replayInfo.randomNum = 10000;
        msg.replayInfo = replayInfo;
        SendMsg<ReqFightResultInfo>(ref msg);

        //ReplayService.Singleton.SetReplayData(replayInfo);
    }


    //-----------------------------------------------------------------------------------------------
    public void SetCurWaveMaxTurn(int turn)
    {
        m_maxTurnNum = turn;
    }

    public void SetMaxWave(int wave)
    {
        m_maxWaveCount = wave;
    }


    public FightParam GetParam(ActorCamp camp, int petId)
    {
        return m_fightData.GetParam(camp, petId);

    }

    //设置实体的当前属性
    public void SetActorCurProperty(Actor actor)
    {
        m_fightData.SetCurProperty(actor);
    }

    //设置宠物基础属性
    public void SetActorProperty(Actor actor)
    {
        m_fightData.SetActorBaseProperty(actor);

    }

  

    //每波怪的名字
    public string GetWaveName()
    {

        return m_fightData.GetWaveName();

    }

    //获得自身宠物站位
    public List<int>GetFrendPetStandPos()
    {
        return m_fightData.GetFrendPetStandPos();

    }

    //获得敌方宠物站位
    public List<int> GetEnemyStandPos()
    {
        return m_fightData.GetEnemyStandPos();

    }

    public BaseFight GetCurFightData()
    {
        return m_fightData;
    }

    //获得敌方先手值
    public int GetWavePrecedeVal()
    {
        if (FightManager.Singleton.IsReplay)
        {
            return ReplayManager.Singleton.EnemyFirstVal;
        }

        return m_fightData.GetWavePrecedeVal();
    }




}