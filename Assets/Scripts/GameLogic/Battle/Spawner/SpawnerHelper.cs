
using Data.Beans;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerHelper : SingletonTemplate<SpawnerHelper>
{
    private SpawnerManager spawnerMgr;

    private List<WaveTrigger> mTriggerList = new List<WaveTrigger>();

    private List<GameMovePath> pathList = new List<GameMovePath>();

    private GameMovePath pathPlayer;
    
    //boss出场点
    public Transform BossShot { private set; get; }

    private int mTriggerCount = 0;        //触发点个数
    public int GetTriggerCount
    {
        get { return mTriggerCount; }
    }

    public int CurWave { get; set; }

    public int WaveCount { get; set; }

    //开始刷怪的波数
    public int StartWave { get; set; }

    public Light ActorLight { private set; get; }

    /// <summary>
    /// 用于获取真正的triggerid
    /// </summary>
    public int RealWave
    {
        get { return StartWave + CurWave; }
    }

    public void Init(SpawnerManager spawnerMgr)
    {
        this.spawnerMgr = spawnerMgr;
        spawnerMgr.FindTriggers(mTriggerList);
        mTriggerCount = spawnerMgr.GetTriggerCount;
        spawnerMgr.FindPaths(pathList);
        Transform trans = spawnerMgr.TransformExt.Find("Paths/pathPlayer");
        if (trans != null)
            pathPlayer = trans.GetComponent<GameMovePath>();
        else
            Debug.LogError("SpawnerManager:Awake:can not find player path");
        BossShot = spawnerMgr.TransformExt.Find("BossShot");
        if (spawnerMgr.TransformExt.parent != null)
        {
            Transform lightTrans = spawnerMgr.TransformExt.parent.Find("ActorLight");
            if (lightTrans != null)
            {
                ActorLight = lightTrans.GetComponent<Light>();
                if (ActorLight == null)
                    Logger.err("场景没有角色灯光");
            }
        }
    }

    public void InitMonsters(int missionId)
    {
        t_dungeon_actBean bean = ConfigBean.GetBean<t_dungeon_actBean, int>(missionId);
        if (bean != null)
        {
            for (int i = 0; i < mTriggerList.Count-StartWave; i++)
            {
                //t_wave_monster1
                System.Reflection.PropertyInfo proInfo = bean.GetType().GetProperty("t_wave_monster" + (i + 1));
                if (proInfo != null)
                {
                    System.Object obj = proInfo.GetValue(bean, null);
                    mTriggerList[i + StartWave].InitMonster((string)obj);
                }

            }
        }
    }

    //初始化敌方
    public void InitEnemys(int wave = -1)
    {
        BaseFight fightData = FightService.Singleton.GetCurFightData();
        fightData.InitEnemys(mTriggerList);
    }

    public void InitPets()
    {
        if (mTriggerList != null && mTriggerList.Count > 0)
        {
            List<int> list = null;
            if (GameManager.Singleton.IsDebug)
                list = GameManager.Singleton.DebugInfo.pets;
            else
                list = FightService.Singleton.GetFrendPetStandPos();
            if (RealWave >= 0 && RealWave < mTriggerList.Count)
                mTriggerList[RealWave].InitPets(list);
            else
                Logger.err("找不到刷怪点：" + RealWave);
        }
    }

    public void InitPets(int wave, List<Message.Replay.MsgActor> pets)
    {
        if (mTriggerList == null || mTriggerList.Count <= 0)
            return;

        for (int i = 0; i < pets.Count; ++i)
        {
            var list = mTriggerList[StartWave + wave];
            if (pets[i].camp == (int)ActorCamp.CampFriend)
            {
                var spawner = list.PetSpawnerList[pets[i].gridId];
                spawner.actorId = pets[i].tmpId;
                spawner.roleId = pets[i].actorId;
                spawner.Type = (ActorType)pets[i].type;
                spawner.camp = (ActorCamp)pets[i].camp;
            }
            else if (pets[i].camp == (int)ActorCamp.CampEnemy)
            {
                var spawner = list.MonsterSpawnerList[pets[i].gridId];
                spawner.actorId = pets[i].tmpId;
                spawner.roleId = pets[i].actorId;
                spawner.Type = (ActorType)pets[i].type;
                spawner.camp = (ActorCamp)pets[i].camp;
            }
        }
    }
    
    public bool IsBossLevel()
    {
        return CurWave == (WaveCount - 1);
    }

    public bool NextIsBossLevel()
    {
        return (CurWave + 1) >= (WaveCount - 1);
    }

    public void SpawnPets()
    {
        if (RealWave >= 0 && RealWave < mTriggerCount)
        {
            WaveTrigger trigger = mTriggerList[RealWave];
            foreach (ActorSpawner spawner in trigger.PetSpawnerList)
            {
                SpawnImpl(spawner);
            }
        }
    }

    public void SpawnMonster()
    {
        if (RealWave >= 0 && RealWave < mTriggerCount)
        {
            WaveTrigger trigger = mTriggerList[RealWave];
            foreach (ActorSpawner spawner in trigger.MonsterSpawnerList)
            {
                SpawnImpl(spawner);
            }
            GED.ED.dispatchEvent(EventID.MonsterSpawnCmp);
        }
    }

    public void SpawnPlayer()
    {
        if (RealWave >= 0 && RealWave < mTriggerCount)
        {
            WaveTrigger trigger = mTriggerList[RealWave];
            ActorParam param = ActorParam.create(trigger.PlayerTrans.position, trigger.PlayerTrans.forward);
            ActorPlayer actor = ActorManager.Create(100, ActorType.Player, ActorCamp.CampFriend, param) as ActorPlayer;
            if (actor != null)
            {
                actor.changeState(ActorState.idle);
                if (ActorLight != null)
                    actor.ResetLight(ActorLight);
            }
        }
    }

    private void SpawnImpl(ActorSpawner spawner)
    {
        if (spawner == null || spawner.actorId <= 0)
            return;

        Actor act = null;
        ActorCamp camp = ActorCamp.CampEnemy;
        if (spawner.Type == ActorType.Pet)
        {
            FightManager.R.LoadGo("eff_chusheng", spawner.TransformExt.position);
            camp = spawner.camp;
            //TODO：获取等级
            ActorPet actor = ActorManager.Create(spawner.actorId, spawner.Type, camp,
                ActorParam.create(spawner.TransformExt.position, spawner.TransformExt.forward, -1, spawner.Id), spawner.roleId) as ActorPet;
            actor.ToggleVisible(false);
            CoroutineManager.Singleton.delayedCall(0.5f, () =>
            {
                actor.ToggleVisible(true);
                BattleWindMgr.CurrentBtlWin.OnSpawnerActor(actor);
                AudioManager.Singleton.PlayEffect("snd_dengchang");
            });

            act = actor;
        }
        else if (spawner.Type == ActorType.Monster)
        {
            ActorMonster actor = ActorManager.Create(spawner.actorId, spawner.Type, camp, 
                ActorParam.create(spawner.TransformExt.position, spawner.TransformExt.forward, -1, spawner.Id), spawner.roleId) as ActorMonster;
            BattleWindMgr.CurrentBtlWin.OnSpawnerActor(actor);

            act = actor;
        }
        else if (spawner.Type == ActorType.Boss)
        {
            var actor = ActorManager.Create(spawner.actorId, spawner.Type, camp, 
                ActorParam.create(spawner.TransformExt.position, spawner.TransformExt.forward, -1, spawner.Id), spawner.roleId);
            BattleWindMgr.CurrentBtlWin.OnSpawnerActor(actor);

            act = actor;
        }
        else
        {
            Logger.err("ActorSpawner:Spawn:未知的角色类型" + spawner.Type);
        }

        if (act != null)
            spawner.roleId = act.getActorId();

        CmdCollectMgr.Singleton.InitActor(act);
    }


    public Transform GetAssist(int index = -1)
    {
        WaveTrigger trigger = GetTrigger();
        if (trigger != null)
        {
            return trigger.GetAssist(index);
        }
        return null;
    }

    public WaveTrigger GetTrigger(int index = -1)
    {
        if (index < 0)
            index = RealWave;
        if (index >= 0 && index < mTriggerCount)
        {
            return mTriggerList[index];
        }
        Logger.err("SpawnerManager：GetTrigger：无法找到trigger" + index);
        return null;
    }

    public PathParam GetPathParam(int index, int gridId)
    {
        int pathIndex = gridId % 3;
        if (pathIndex < 0 && pathIndex >= pathList.Count)
            return null;
        PathParam res = new PathParam();
        GameMovePath path = pathList[pathIndex];
        if (path != null)
        {
            GameMovePath.GameWaypoint wp;
            for (int i = 0; i < path.m_Waypoints.Length; i++)
            {
                wp = path.m_Waypoints[i];
                if (wp.zoneId == index)
                    res.path.Add(path.EvaluatePosition(i));
            }
        }
        WaveTrigger nextTrigger = GetTrigger(index + 1);
        if (nextTrigger != null && nextTrigger.PlayerTrans != null)
        {
            res.path.Add(nextTrigger.PetSpawnerList[gridId].TransformExt.position);
            res.dir = nextTrigger.PetSpawnerList[gridId].TransformExt.forward;
        }
        return res;
    }

    public PathParam GetPlayerPathParam(int index)
    {
        PathParam res = new PathParam();
        if (pathPlayer != null)
        {
            GameMovePath.GameWaypoint wp;
            for (int i = 0; i < pathPlayer.m_Waypoints.Length; i++)
            {
                wp = pathPlayer.m_Waypoints[i];
                if (wp.zoneId == index)
                    res.path.Add(pathPlayer.EvaluatePosition(i));
            }
        }
        WaveTrigger nextTrigger = GetTrigger(index + 1);
        if (nextTrigger != null && nextTrigger.PlayerTrans != null)
        {
            res.path.Add(nextTrigger.PlayerTrans.position);
            res.dir = nextTrigger.PlayerTrans.forward;
        }
        return res;
    }

    /// <summary>
    /// 获取刷怪点的中心坐标
    /// </summary>
    /// <returns></returns>
    public Vector3 GetColCenter(ActorCamp camp, int col, int index = -1)
    {
        WaveTrigger trigger = GetTrigger(index);
        if (trigger != null)
            return trigger.GetCenterPos(camp, col);
        return Vector3.zero;
    }

    public Vector3 GetRowCenter(ActorCamp camp, int row, int index = -1)
    {
        WaveTrigger trigger = GetTrigger(index);
        if (trigger != null)
            return trigger.GetRowCenter(camp, row);
        return Vector3.zero;
    }


    //获得当前波敌方宠物中与己方存活的宠物Id的对象
    public List<Actor> GetSamePetInfo()
    {
        List<Actor> petDic = new List<Actor>();
        if (mTriggerList.Count > CurWave && CurWave < 0)
            return petDic;

        WaveTrigger waveTrigger = mTriggerList[CurWave];
        if (waveTrigger != null)
        {
            for (int i = 0; i < waveTrigger.MonsterSpawnerList.Count; i++)
            {
                ActorSpawner enemyActor = waveTrigger.MonsterSpawnerList[i];
                
                if (enemyActor.actorId > 0  )
                {
                    //FightManager.Singleton.Grid.IsRealAlive(ActorCamp.CampFriend, enemyActor.actorId)
                    //当前自身活着的宠物里有相同的宠物
                    Actor actor = ActorManager.Singleton.Get(enemyActor.roleId);
                    if (actor is ActorBoss)
                    {
                        t_monster_boosBean bossBean = ConfigBean.GetBean<t_monster_boosBean, int>(enemyActor.actorId);
                        if (bossBean != null && FightManager.Singleton.Grid.IsRealAlive(ActorCamp.CampFriend, bossBean.t_pet_id))
                        {
                            petDic.Add(actor);
                        }
                    }
           
                }

            }
        }

        return petDic;
    }

    public void Clear()
    {
        mSingleton = null;
        spawnerMgr = null;
    }

}