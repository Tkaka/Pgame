using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WaveTrigger : BaseBehaviour
{

    [HideInInspector]
    public int Id;

    public int MonsterSpawnerCount { private set; get; }       //怪物刷怪点个数

    public int PetSpawnerCount { private set; get; }       //怪物刷怪点个数

    private float mSpawnGap = 0.0f;    //刷怪间隙

    public List<ActorSpawner> MonsterSpawnerList { private set; get; }

    public List<ActorSpawner> PetSpawnerList { private set; get; }

    public Transform[] AssistTrans{ private set; get; }

    public Transform PlayerTrans { private set; get; }

    public bool PetSpawned { private set; get; }

    public bool MonsterSpawned { private set; get; }


    //初始化单个怪物
    public void InitSingleMonster(int monsteId, int pos)
    {
        if (MonsterSpawnerList.Count <= pos || pos < 0)
        {
            Debug.LogError("初始化怪物到一个不存在的位置");
            return;
        }

        MonsterSpawnerList[pos].actorId = monsteId;
        if (monsteId >= 10000000)
            MonsterSpawnerList[pos].Type = ActorType.Boss;
        else
            MonsterSpawnerList[pos].Type = ActorType.Monster;
        MonsterSpawnerList[pos].camp = ActorCamp.CampEnemy;
    }

    public void InitMonster(string monsters)
    {
        if (!string.IsNullOrEmpty(monsters))
        {
            int[] arr = GTools.splitStringToIntArray(monsters);
            if (arr != null && arr.Length > 0)
            {
                for (int i = 0; i < arr.Length; i++)
                {
                    if (i < MonsterSpawnerList.Count)
                    {
                        MonsterSpawnerList[i].actorId = arr[i];
                        if (arr[i] >= 10000000)
                            MonsterSpawnerList[i].Type = ActorType.Boss;
                        else
                            MonsterSpawnerList[i].Type = ActorType.Monster;
                        MonsterSpawnerList[i].camp = ActorCamp.CampEnemy;
                    }
                    else
                    {
                        Logger.log("MonsterTrigger:InitMonster:怪物列表超过刷怪点" + arr[i]);
                    }
                }
            }
            else
            {
                Logger.err("MonsterTrigger:InitMonster:怪物列表为null" + monsters);
            }
        }
    }

    public void InitPets(List<int> list)
    {
        //List<int> list = PetService.Singleton.GetTeamList(ZhenRongType.Normal, false);
        if (list != null && list.Count > 0)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (i < PetSpawnerList.Count)
                {
                    PetSpawnerList[i].actorId = list[i];
                    PetSpawnerList[i].Type = ActorType.Pet;
                    PetSpawnerList[i].camp = ActorCamp.CampFriend;
                }
                else
                {
                    Logger.log("MonsterTrigger:InitMonster:宠物上阵列表超过刷怪点" + list[i]);
                }
            }
        }
        else
        {
            Logger.err("MonsterTrigger:InitPets:上阵列表为空");
        }
    }

    //初始化敌方的宠物
    public void InitEnemyPets(List<int> list)
    {
        if (list != null && list.Count > 0)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (i < MonsterSpawnerList.Count)
                {
                    MonsterSpawnerList[i].actorId = list[i];
                    MonsterSpawnerList[i].Type = ActorType.Pet;
                    MonsterSpawnerList[i].camp = ActorCamp.CampEnemy;
                }
                else
                {
                    Logger.log("MonsterTrigger:InitMonster:宠物上阵列表超过刷怪点" + list[i]);
                }
            }
        }
        else
        {
            Logger.err("MonsterTrigger:InitPets:上阵列表为空");
        }
    }

    protected override void Awake()
    {
        base.Awake();
        MonsterSpawnerList = new List<ActorSpawner>();
        PetSpawnerList = new List<ActorSpawner>();
        AssistTrans = new Transform[3];
        FindMonsterSpawners();
        FindPetSpawners();
        FindAssists();
        PlayerTrans = TransformExt.Find("Fight_Point/Player");
    }

    protected override void Start()
    {
        base.Start();
        if (Id > 0)
            PetSpawned = true;
    }

    private void FindMonsterSpawners()
    {
        int index = 0;
        bool flag = true;
        string triggerName = "";
        ActorSpawner monsterSpawner;
        while (flag)
        {
            //triggerName = "MonsterSpawner" + index;
            triggerName = "Fight_Point/Right/All/pos" + index;
            Transform spawner = TransformExt.Find(triggerName);
            if (spawner != null)
            {
                monsterSpawner = spawner.GetComponent<ActorSpawner>();
                if (monsterSpawner != null)
                {
                    MonsterSpawnerList.Add(monsterSpawner);
                    monsterSpawner.Id = MonsterSpawnerCount;
                    MonsterSpawnerCount++;
                }
                else
                {
                    Logger.err("不能找到该节点" + triggerName);
                }
                index++;
            }
            else
            {
                flag = false;
            }
        }
    }

    private void FindPetSpawners()
    {
        int index = 0;
        bool flag = true;
        string triggerName = "";
        ActorSpawner monsterSpawner;
        while (flag)
        {
            //triggerName = "PetSpawner" + index;
            triggerName = "Fight_Point/Left/All/pos" + index;
            Transform spawner = TransformExt.Find(triggerName);
            if (spawner != null)
            {
                monsterSpawner = spawner.GetComponent<ActorSpawner>();
                if (monsterSpawner != null)
                {
                    PetSpawnerList.Add(monsterSpawner);
                    monsterSpawner.Id = PetSpawnerCount;
                    PetSpawnerCount++;
                }
                else
                {
                    Logger.log("SpawnerManager:findPetSpawners:不能找到该节点" + triggerName);
                }
                index++;
            }
            else
            {
                flag = false;
            }
        }
    }

    private void FindAssists()
    {
        int index = 0;
        bool flag = true;
        string nodeName = "";
        while (flag)
        {
            nodeName = "Fight_Point/Center/Assist" + index;
            Transform spawner = TransformExt.Find(nodeName);
            if (spawner != null)
            {
                AssistTrans[index] = spawner;
                index++;
            }
            else
            {
                flag = false;
            }
        }
        if (index < 3)
        {
            Logger.err("WaveTrigger:FindAssists:找不到辅助点");
        }
    }

    public Transform GetAssist(int index)
    {
        if (index >= 0 && index < 3)
        {
            return AssistTrans[index];
        }
        Logger.err("WaveTrigger:GetAssist:无法找到技能辅助点");
        return AssistTrans[0];
    }

    public Vector3 GetRowCenter(ActorCamp camp, int row)
    {
        int spawnerCount = 0;
        List<ActorSpawner> spawnerList;
        if (camp == ActorCamp.CampEnemy)
        {
            spawnerCount = MonsterSpawnerCount;
            spawnerList = MonsterSpawnerList;
        }
        else
        {
            spawnerCount = PetSpawnerCount;
            spawnerList = PetSpawnerList;
        }

        if (spawnerCount >= 6)
        {
            int gridId = 1;
            if (row == GridEnum.Row0)
                gridId = 1;
            else if (row == GridEnum.Row1)
                gridId = 4;
            ActorSpawner spawner = spawnerList[gridId];
            return spawnerList[gridId].TransformExt.position;
        }
        Logger.err("刷怪点不足6个");
        return Vector3.zero;
    }

    public Vector3 GetCenterPos(ActorCamp camp, int col)
    {
        int spawnerCount = 0;
        List<ActorSpawner> spawnerList;
        if (camp == ActorCamp.CampEnemy)
        {
            spawnerCount = MonsterSpawnerCount;
            spawnerList = MonsterSpawnerList;
        }
        else
        {
            spawnerCount = PetSpawnerCount;
            spawnerList = PetSpawnerList;
        }

        if (spawnerCount >= 6)
        {
            int front = col;
            int back = col + 3;
            ActorSpawner pre = spawnerList[front];
            ActorSpawner next = spawnerList[back];
            return Vector3.Lerp(pre.TransformExt.position, next.TransformExt.position, 0.5f);
        }
        Logger.err("刷怪点不足6个");
        return Vector3.zero;
    }

}
