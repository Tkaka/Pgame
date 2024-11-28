using Message.Replay;
using Message.Fight;
using System.Collections.Generic;

public class ReplayService : SingletonService<ReplayService>
{
    private ReplayInfo replay;
    //private WaveInfo curBattle;
    //private TurnInfo curTurn;
   private ResFightResultInfo m_fightResult;
    private long m_fightId = 0; 

    public override void ClearData()
    {
        base.ClearData();
        replay = null;
        m_fightResult = null;
    }

    public ReplayService()
    {
        //test data
        replay = new ReplayInfo();
        var wave = new WaveInfo();
        replay.waveList.Add(wave);
        wave.actorList.Add(new MsgActor { actorId = 10, camp = (int)ActorCamp.CampEnemy, gridId = 0, tmpId = 102, type = (int)ActorType.Pet });
        wave.actorList.Add(new MsgActor { actorId = 20, camp = (int)ActorCamp.CampFriend, gridId = 0, tmpId = 103, type = (int)ActorType.Pet });
        foreach(var act in wave.actorList)
        {
            for(int i=(int)PropertyType.Atk; i<(int)PropertyType.MaxPropertyType; ++i)
            {
                act.propertyTypes.Add(i);
                long value = 0;
                if (i == (int)PropertyType.Atk)
                    value = 1;
                if (i == (int)PropertyType.Hp)
                    value = 10000000;

                act.basePropertyVals.Add(value);
                act.propertyVals.Add(value);
            }
        }
        var turn = new TurnInfo();
        wave.turnList.Add(turn);
        turn.turns.Add(new FightCMD { actorId = 10, comboType = (int)ComboType.Good, skillId = 4990, isMasterSkill = false });

        turn = new TurnInfo();
        wave.turnList.Add(turn);
        turn.turns.Add(new FightCMD { actorId = 20, comboType = (int)ComboType.Perfect, skillId = 4990, isMasterSkill = false });

        //第二波
        wave = new WaveInfo();
        replay.waveList.Add(wave);
        wave.actorList.Add(new MsgActor { actorId = 100, camp = (int)ActorCamp.CampEnemy, gridId = 0, tmpId = 100, type = (int)ActorType.Pet });
        wave.actorList.Add(new MsgActor { actorId = 200, camp = (int)ActorCamp.CampFriend, gridId = 0, tmpId = 108, type = (int)ActorType.Pet });
        foreach (var act in wave.actorList)
        {
            for (int i = (int)PropertyType.Atk; i < (int)PropertyType.MaxPropertyType; ++i)
            {
                act.propertyTypes.Add(i);
                long value = 0;
                if (i == (int)PropertyType.Atk)
                    value = 1;
                if (i == (int)PropertyType.Hp)
                    value = 10000000;

                act.basePropertyVals.Add(value);
                act.propertyVals.Add(value);
            }
        }
        turn = new TurnInfo();
        wave.turnList.Add(turn);
        turn.turns.Add(new FightCMD { actorId = 200, comboType = (int)ComboType.Perfect, skillId = 4990, isMasterSkill = false });

        turn = new TurnInfo();
        wave.turnList.Add(turn);
        turn.turns.Add(new FightCMD { actorId = 100, comboType = (int)ComboType.Good, skillId = 4990, isMasterSkill = false });
    }

    public void SetActorProperty(Actor actor)
    {
        foreach(var battle in replay.waveList)
        {
            foreach(var msgAct in battle.actorList)
            {
                if(actor.getActorId() == msgAct.actorId)
                {
                    for (int i = 0; i < msgAct.propertyTypes.Count; ++i)
                    {
                        actor.setBaseProperty((PropertyType)msgAct.propertyTypes[i], LNumber.Create_Row(msgAct.basePropertyVals[i]));
                        actor.SetProperty((PropertyType)msgAct.propertyTypes[i], LNumber.Create_Row(msgAct.propertyVals[i]));
                    }
                    return;
                }
            }
        }
    }

    public List<SkillParam> GetSkillParams(Actor actor)    
    {
        List<SkillParam> skillParams = null;
        foreach (var wave in replay.waveList)
        {
            foreach (var msgAct in wave.actorList)
            {
                if (actor.getActorId() == msgAct.actorId)
                {
                    skillParams = msgAct.skills;
                    break;
                }
            }
        }

        return skillParams;
    }

    public ReplayInfo GetCurReplay()
    {
        return replay;
    }

    public void SetReplayData(ReplayInfo replay)
    {
        this.replay = replay;
    }

    public void SetFightResult(ResFightResultInfo result)
    {
        m_fightResult = result;
    }


    protected override void RegisterEventListener()
    {
        GED.NED.addListener(ResReplay.MsgId, onReplayMsg);

    }

    protected override void UnRegisterEventListener()
    {
        base.UnRegisterEventListener();
        GED.NED.removeListener(ResReplay.MsgId, onReplayMsg);
    }

    //显示重播结果
    public void ReplayResult()
    {
        if (m_fightResult == null)
        {
            WinMgr.Singleton.CloseAll();
            SceneLoader.Singleton.nextState = GameState.MainCity;
            SceneLoader.Singleton.sceneName = GSceneName.MaiCity;
            GameManager.Singleton.changeState(GameState.Loading);
            return;
        }


        switch ((EFightType)m_fightResult.fightType)
        {
            case EFightType.Arena:
                {
                    if (m_fightResult.result.result == 0)
                    {
                        WinMgr.Singleton.Open<BattleFailedWindow>(null, UILayer.Popup);
                        //BattleWindow.Singleton.OpenChild<BattleFailedWindow>(WinInfo.Create(false, null, false));
                    }
                    else
                    {
                        WinMgr.Singleton.Open<ArenaSucessWindow>(WinInfo.Create(false, null, false, m_fightResult), UILayer.Popup);
                    }
                }
                break;
            default:
                {
                    WinMgr.Singleton.CloseAll();
                    SceneLoader.Singleton.nextState = GameState.MainCity;
                    SceneLoader.Singleton.sceneName = GSceneName.MaiCity;
                    GameManager.Singleton.changeState(GameState.Loading);
                }
                break;
        }
 
    }



    private void onReplayMsg(GameEvent evt)
    {
        var msg = GetCurMsg<ResReplay>(evt.EventId);
        replay = msg.replay;
        m_fightId = msg.replayId;
        SpawnerHelper.Singleton.WaveCount = replay.waveList.Count;

        int[] turns = new int[replay.waveList.Count];
        for (int i = 0; i < replay.waveList.Count; i++)
        {
            WaveInfo waveInfo = replay.waveList[i];
            turns[i] = waveInfo.turnList.Count;
        }
        FightManager.Singleton.SetMaxTurns(turns);
        StartPlay();
    }


    public void StartPlay()
    {
        FightManager.Singleton.SetRandomNum(m_fightId);
        WinMgr.Singleton.CloseAll();
        SceneLoader.Singleton.nextState = GameState.BattleReplay;
        SceneLoader.Singleton.sceneName = "lvl_gq01_mysl_01";
        GameManager.Singleton.changeState(GameState.Loading);
    }

    /*
    /// <summary>
    /// 重置战斗记录
    /// </summary>
    /// <param name="version">战斗版本号</param>
    /// <param name="randomNum">战斗随机数</param>
    public void Reset(int version, int randomNum)
    {
        replay = new ReplayInfo();
        replay.version = version;
        replay.randomNum = randomNum;
    }

	public void BeginBattle(params Actor[] actors)
    {
        curBattle = new waveInfo();
        replay.battleList.Add(curBattle);

        for (int i=0; i<actors.Length; ++i)
        {
            var actor = actors[i];
            var ma = new MsgActor();
            curBattle.actorList.Add(ma);

            ma.actorId = actor.getActorId();
            ma.tmpId = actor.getTemplateId();
            ma.camp = (int)actor.getCamp();
            ma.type = (int)actor.getActorType();
            ma.gridId = actor.GridId;

            int num = (int)PropertyType.MaxPropertyType;
            for (int t = (int)PropertyType.None + 1; t < num; ++t)
            {
                ma.propertyTypes.Add(t);
                ma.basePropertyVals.Add(actor.getProperty((PropertyType)t).raw);
                ma.basePropertyVals.Add(actor.getBaseProperty((PropertyType)t).raw);
            }
        }
    }

    public void TunrBegin()
    {
        curTurn = new TurnInfo();
        curBattle.turnList.Add(curTurn);
    }

    public void PushCmd(FightCmd cmd)
    {
        FightCMD fight = new FightCMD();
        curTurn.turns.Add(fight);

        fight.actorId = cmd.actorId;
        fight.skillId = cmd.skillId;
        fight.isLastOne = cmd.isLastOne;
        fight.comboType = (int)cmd.comboType;
        fight.isMasterSkill = cmd.isMasterSkill;
    }

    public void BattleEnd()
    {
        curBattle = null;
        curTurn = null;

        var data = BaseMessage.GetMsgData(replay);
    }*/
}
