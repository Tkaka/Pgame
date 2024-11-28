using UnityEngine;
using FairyGUI;
using System.Collections.Generic;
using System.Collections;

public class GameMainCityState : GameBaseState
{
    private long moveLoopId;
    private List<Vector3> wayPointList = new List<Vector3>();
    private List<ActorMC> actorList = new List<ActorMC>();
    private List<int> mcPetIDList = new List<int>();
    private List<int> mcChangePetIDList = new List<int>();

    private string winId;
    public static bool EnterGameCamAniPlayed = false;
    public bool enterGameAniEnd = false;
    public override void onEnter(object obj = null)
    {
        base.onEnter(obj);
        Logger.log("enter GameMainCityState state");

        //SoundManager.Singleton.PlayBGM(PathEnum.Sound + "bgm_mainCity");
        AudioManager.Singleton.PlayBGM("snd_bgm_maincity");

        actorList.Clear();
        CreateActors();
        
        //打开主界面UI
        //UIPackage.AddPackage(WinEnum.BasePath + WinEnum.UI_MainCityWindow);
        //winId = WinMgr.Singleton.Open<MainCityWindow>(WinEnum.UI_MainCity, WinEnum.UI_MainCityWindow, true);
        GED.ED.addListenerOnce(EventID.MainCityDollyCmp, OnCamDollyCmp);
        GED.ED.addListenerOnce(EventID.PetInfoInit, _RefreshAllPets);
        //addListener(EventID.OnShangZhenChongWuId, OnShangZhenChongWuChanged);
        addListener(EventID.OnPetShuXingChanged, OnPetStarUp);
        addListener(EventID.OnPetTeamListChanged, CreateActors);
        //GED.ED.addListenerOnce(EventID.OnShangZhenChongWuId, OnShangZhenChongWuChanged);

        initWayPoints();
        
        moveLoopId = CoroutineManager.Singleton.startCoroutine(randomMoveLoop());

        MainCityCamCtrl.Singleton.PlayCameraAni(!EnterGameCamAniPlayed);
        if (EnterGameCamAniPlayed)
            OnCamDollyCmp(null);
        EnterGameCamAniPlayed = true;
    }

    private void _RefreshAllPets(object obj = null)
    {
        List<int> teamIDList = PetService.Singleton.GetTeamList(ZhenRongType.Normal);
        if (teamIDList == null)
            return;

        // 获得最新的主城宠物列表以及改变的宠物列表
        int count = teamIDList.Count;
        int petID = 0;
        for (int i = 0; i < count; i++)
        {
            if (mcPetIDList.Count == 0)
            {
                mcPetIDList.AddRange(teamIDList);
                mcChangePetIDList.AddRange(teamIDList);
                break;
            }

            if (mcPetIDList.Count != count)
                break;

            petID = teamIDList[i];
            if (mcPetIDList[i] != petID)
            {
                // 如果跟主城之前的宠物列表不一样，那么就替换掉，一样的就不管
                int changePetID = mcPetIDList[i];
                mcPetIDList[i] = petID;
                mcChangePetIDList.Add(petID);
                // 移除主城的模型
                ActorMC aMC = null;
                for (int j = 0; j < actorList.Count; j++)
                {
                    aMC = actorList[j];
                    if (aMC.getTemplateId() == changePetID && aMC.getActorType() == ActorType.Pet)
                    {
                        actorList.RemoveAt(j);
                        ActorManagerMC.Singleton.Remove(aMC.getActorId());
                        break;
                    }
                }
            }
        }

        var dir = Vector3.forward;
        var cam = Camera.main;
        if (cam != null)
        {
            dir = -cam.transform.forward;
            dir.y = 0;
            dir.Normalize();
        }


        /*count = mcChangePetIDList.Count;
        for (int i = 0; i < count; i++)
        {
            if (mcChangePetIDList[i] == 0)
                continue;

            Message.Pet.PetInfo petInfo = PetService.Singleton.GetPetInfo(mcChangePetIDList[i]);
            int star = petInfo == null ? -1 : petInfo.basInfo.star;
            ActorParam param = ActorParam.create(WayPointMgr.Singleton.petSpawnerPoints[i].position, dir, star);
            var actormc = ActorManagerMC.Create(mcChangePetIDList[i], ActorType.Pet, ActorCamp.CampFriend, param, mcChangePetIDList[i]);
            actormc.setDirection(dir);
            actorList.Add(actormc);

            if (MainCityWindow.guidePet == null)
            {
                //找一个不动的怪作为引导对象
                var roleInfo = RoleService.Singleton.GetRoleInfo();
                if (roleInfo == null || roleInfo.level < 10)
                    MainCityWindow.guidePet = actormc;
            }
        }*/

        // 每次操作完清空
        mcChangePetIDList.Clear();
    }

    private void OnCamDollyCmp(object param)
    {
        WinMgr.Singleton.Close(winId);
        winId = WinMgr.Singleton.Open<MainCityWindow>(WinInfo.Create(false, null, true, this));
        enterGameAniEnd = true;
    }

    private void OnShangZhenChongWuChanged(GameEvent evt)
    {
        if (PetService.Singleton.zhenRongType == ZhenRongType.Normal)
        {
            // 处理掉被替换的那个宠物

            TwoParam<int, int> param = evt.Data as TwoParam<int, int>;
            if (param.value2 == 0)
                return;

            if (param.value1 != 0)
            {
                for (int i = 0; i < actorList.Count; ++i)
                {
                    if (actorList[i].getTemplateId() == param.value1)
                    {
                        ActorMC aMC = actorList[i];
                        actorList.RemoveAt(i);
                        ActorManagerMC.Singleton.Remove(aMC.getActorId());
                        break;
                    }
                }

                //List<int> teamIDList = PetService.Singleton.GetTeamList(ZhenRongType.Normal);
                //if (teamIDList == null)
                //    return;
                //int count = teamIDList.Count;
                //for (int i = 0; i < count; i++)
                //{
                //    if (teamIDList[i] == param.value2)
                //    {
                //        Message.Pet.PetInfo petInfo = PetService.Singleton.GetPetInfo(teamIDList[i]);
                //        int star = petInfo == null ? -1 : petInfo.basInfo.star;
                //        ActorParam actorParam = ActorParam.create(WayPointMgr.Singleton.petSpawnerPoints[i].position, Vector3.zero, star);
                //        var actormc = ActorManagerMC.Create(teamIDList[i], ActorType.Pet, ActorCamp.CampFriend, actorParam, teamIDList[i]);
                //        actorList.Add(actormc);
                //    }
                //}
            }
        }
    }

    private void OnPetStarUp(GameEvent evt)
    {
        /*Message.Pet.PetInfo petInfo = evt.Data as Message.Pet.PetInfo;
        ActorMC aMC = null;
        for (int i = 0; i < actorList.Count; i++)
        {
            aMC = actorList[i];
            if (aMC.getActorType() == ActorType.Player)
                continue;

            if (aMC.getTemplateId() == petInfo.petId)
            {
                actorList.RemoveAt(i);
                ActorManagerMC.Singleton.Remove(aMC.getActorId());
                ActorParam actorParam = ActorParam.create(WayPointMgr.Singleton.petSpawnerPoints[i].position, Vector3.zero, petInfo.basInfo.star);
                var actormc = ActorManagerMC.Create(petInfo.petId, ActorType.Pet, ActorCamp.CampFriend, actorParam, petInfo.petId);
                actorList.Add(actormc);
                break;
            }
        }*/
    }

    private void CreateActors(GameEvent evt = null)
    {
        //ActorParam param = ActorParam.create(WayPointMgr.Singleton.spawner0.position, Vector3.zero);
        //ActorManagerMC.Create(100, ActorType.Pet, ActorCamp.CampNeutral, param);

        //param = ActorParam.create(WayPointMgr.Singleton.spawner1.position, Vector3.zero);
        //ActorManagerMC.Create(101, ActorType.Pet, ActorCamp.CampNeutral, param);
        
        if (WayPointMgr.Singleton == null)
        {
            Debuger.Err("WayPointMgr为空");
            winId = WinMgr.Singleton.Open<MainCityWindow>();
            return;
        }

        var dir = Vector3.forward;
        var cam = Camera.main;
        if (cam != null)
        {
            dir = -cam.transform.forward;
            dir.y = 0;
            dir.Normalize();
        }

       /* ActorParam param = null;
        if (actorList.Count == 0)
        {
            param = ActorParam.create(WayPointMgr.Singleton.playerSpawner.position, Vector3.zero);
            var actor = ActorManagerMC.Create(100, ActorType.Player, ActorCamp.CampNeutral, param);
            actor.setDirection(dir);
            actorList.Add(actor);
        }*/

        _RefreshAllPets();
        //List<int> teamIDList = PetService.Singleton.GetTeamList(ZhenRongType.Normal);
        //if (teamIDList == null)
        //    return;
        //int count = teamIDList.Count;
        //for (int i = count-1; i >= 0; --i)
        //{
        //    for (int j = 0; j < actorList.Count; ++j)
        //    {
        //        if (actorList[j].getActorType() != ActorType.Pet)
        //            continue;

        //        if (actorList[j].getTemplateId() == teamIDList[i] || teamIDList[i] <= 0)
        //        {
        //            //之前已经创建了/没有的位置
        //            teamIDList.RemoveAt(i);
        //            break;
        //        }
        //    }
        //}

        //count = teamIDList.Count;
        //for (int i = 0; i < count; i++)
        //{
        //    if (teamIDList[i] == 0)
        //        continue;

        //    param = ActorParam.create(WayPointMgr.Singleton.petSpawnerPoints[i].position, dir);
        //    var actormc = ActorManagerMC.Create(teamIDList[i], ActorType.Pet, ActorCamp.CampFriend, param, teamIDList[i]);
        //    actormc.setDirection(dir);
        //    actorList.Add(actormc);

        //    if (MainCityWindow.guidePet == null)
        //    {
        //        //找一个不动的怪作为引导对象
        //        var roleInfo = RoleService.Singleton.GetRoleInfo();
        //        if (roleInfo == null || roleInfo.level < 10)
        //            MainCityWindow.guidePet = actormc;
        //    }
        //}
    }

    public override void onUpdate()
    {
        ActorManagerMC.Singleton.Update();
        UpdatePetLisHeadInfo();
    }

    private void UpdatePetLisHeadInfo()
    {
        if (enterGameAniEnd == false)
            return;
        int count = actorList.Count;
        ActorMC aMC = null;
        for (int i = 0; i < count; i++)
        {
            aMC = actorList[i];
            if (aMC == null)
                continue;
            aMC.RefreshMainCityPetInfo();
        }
    }

    public override void onLeave(string stateKey)
    {
        base.onLeave(stateKey);

        mcPetIDList.Clear();
        HideMainCityPetInfo();
        actorList.Clear();
        wayPointList.Clear();
        WinMgr.Singleton.Close(winId);
        ActorManagerMC.Singleton.RemoveAll();
        ActorManagerMC.Singleton.Clear();
        CoroutineManager.Singleton.stopCoroutine(moveLoopId);

        GED.ED.removeListener(EventID.MainCityDollyCmp, OnCamDollyCmp);
        GED.ED.removeListener(EventID.PetInfoInit, _RefreshAllPets);
        removeListener(EventID.OnShangZhenChongWuId, OnShangZhenChongWuChanged);
        removeListener(EventID.OnPetTeamListChanged, CreateActors);
    }

    private void HideMainCityPetInfo()
    {
        int count = actorList.Count;
        ActorMC aMC = null;
        for (int i = 0; i < count; i++)
        {
            aMC = actorList[i];
            aMC.HideMainCityPetInfo();
        }
    }

    public override string getStateKey()
    {
        return GameState.MainCity;
    }

    private void initWayPoints()
    {
        /*var points = WayPointMgr.Singleton.wayPoints;
        if (points == null)
            return;

        wayPointList.Clear();
        for (int i = 0; i < points.Count; ++i)
        {
            if (points[i] != null)
                wayPointList.Add(points[i].position);
        }*/
    }

    private WaitForSeconds alwaysWait = new WaitForSeconds(0.1f);
    private WaitForSeconds actorWait = new WaitForSeconds(0.3f);
    private WaitForSeconds loopWait = new WaitForSeconds(1f);

    private IEnumerator randomMoveLoop()
    {
        while(true)
        {
            for (int i=0; i< actorList.Count; ++i)
            {
                var actor = actorList[i];
                if (MainCityWindow.guidePet == actor)
                    continue;
                if (actor.ShowObj == null)
                    continue;
                if (!actor.isStateOf(ActorState.idle))
                    continue;
                if (Random.Range(0, 100) > 30) //移动几率
                    continue;
                if (Random.Range(0, 100) > 50) //等待几率
                    yield return actorWait;

                yield return alwaysWait;

                //随机移动
                if (actor != null)
                {
                    int randomIdx = Random.Range(0, wayPointList.Count);
                    Vector3 actorPos = actor.ShowObj.transform.position;
                    Vector3 pointPos = wayPointList[randomIdx];
                    pointPos.y = actorPos.y;

                    float distance = Vector3.Distance(actorPos, pointPos);
                    if (distance > 1f)
                    {
                        float moveStep = Random.Range(0.3f, Mathf.Min(distance - 0.3f, 10f));
                        Vector3 targetPos = actorPos + (pointPos - actorPos).normalized * moveStep;
                        actor.MoveToTarget(targetPos);
                    }
                }
            }
            yield return loopWait;
        }
    }
}