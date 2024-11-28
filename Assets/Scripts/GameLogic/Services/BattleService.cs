using Data.Beans;
using FairyGUI;
using Message.Dungeon;
using Message.Pet;
using Message.Fight;
using System.Collections.Generic;

public class BattleService : SingletonService<BattleService>
{

    public int MissionId { private set; get; }

    //public string BattleScene { private set; get; }

    public List<List<int>> Gold = new List<List<int>>();

    public MissionResult FightRes { private set; get; }

    //是否为活动关卡
    public bool IsActivityMission;

    protected override void RegisterEventListener()
    {
        base.RegisterEventListener();
        GED.NED.addListener(ResFightStart.MsgId, OnFightStart);
        GED.NED.addListener(ResFightResult.MsgId, OnResFightRes);
    }

    protected override void UnRegisterEventListener()
    {
        base.UnRegisterEventListener();
        GED.NED.removeListener(ResFightStart.MsgId, OnFightStart);
        GED.NED.removeListener(ResFightResult.MsgId, OnResFightRes);
    }

    public string GetBattleScene()
    {
        t_dungeon_actBean missionBean = ConfigBean.GetBean<t_dungeon_actBean, int>(MissionId);
        if (missionBean != null)
            return missionBean.t_scene;
        return "";
    }

    public void Init(int missionId)
    {
        MissionId = missionId;
        //BattleScene = GetBattleScene();
        WaveCount = GetWaveCount();
        //SpawnerHelper.Singleton.WaveCount = WaveCount;
        t_dungeon_actBean missionBean = ConfigBean.GetBean<t_dungeon_actBean, int>(MissionId);
        if (missionBean != null)
        {
            //SpawnerHelper.Singleton.StartWave = missionBean.t_start_wave;
            IsActivityMission = (missionBean.t_act_type == 6);
        }
    }


    /// <summary>
    /// 递交战斗结果
    /// </summary>
    /// <param name="actID"></param>
    /// <param name="figthResult">0失败，1成功</param>
    /// <param name="star"></param>
    public void ReqFightResult(int actID, int figthResult, int star)
    {
        FightService.Singleton.ReqFightResult(figthResult, star);
        //ReqFightResult msg = GetEmptyMsg<ReqFightResult>();
        //msg.actId = actID;
        //msg.fightResult = figthResult;
        //msg.star = star;

        //SendMsg<ReqFightResult>(ref msg);
    }

    private void OnFightStart(GameEvent evt)
    {
        ResFightStart msg = GetCurMsg<ResFightStart>(evt.EventId);
        //结果（1：成功，-1：体力不足，-2：未开启）
        if (msg.result == 1)
        {
            Logger.log("fight start success");
            WinMgr.Singleton.CloseAll();
            SceneLoader.Singleton.nextState = GameState.Battle;
            //SceneLoader.Singleton.sceneName = BattleScene;
            GameManager.Singleton.changeState(GameState.Loading);
        }
        else if (msg.result == -1)
        {
            Logger.log("fight start success");
        }
        else if (msg.result == -2)
        {
            Logger.log("fight start success");
        }
    }

    public void OnResFightRes(ResFightResultInfo msg)
    {
        FightRes = msg.result as MissionResult;
        if (FightRes.result == 0)
        {
            BattleWindow.Singleton.OpenChild<BattleFailedWindow>(WinInfo.Create(false, null, false));
        }
        //战斗成功
        else
        {
            //更新宠物信息
            if (FightRes.petInfos != null)
            {
                foreach (PetExp info in FightRes.petInfos)
                {
                    PetService.Singleton.RefreshPetExp(info);
                }
            }
            BattleWindow.Singleton.OpenChild<BattleVictoryWindow>(WinInfo.Create(false, null, false));
            //BattleWindow.Singleton.OpenChild<BattleExpWindow>(WinInfo.Create(false, null, false));
            int lastActId = LevelService.Singleton.GetLastActID(LevelService.Singleton.LevelModel);
            if (lastActId <= MissionId)
            {
                RestoreWndMgr.Singleton.RemoveData<GuanQiaWindow>();
            }
            LevelService.Singleton.UpdateDungeonInfo(FightRes);
        }
    }

    //（废弃）
    private void OnResFightRes(GameEvent evt)
    {
        //FightRes = GetCurMsg<ResFightResult>(evt.EventId);
        ////战斗失败
        //if (FightRes.result == 0)
        //{
        //    BattleWindow.Singleton.OpenChild<BattleFailedWindow>(WinInfo.Create(false, null, false));
        //}
        ////战斗成功
        //else
        //{
        //    //更新宠物信息
        //    if (FightRes.petInfos != null)
        //    {
        //        foreach (PetInfo info in FightRes.petInfos)
        //        {
        //            PetService.Singleton.UpdatePetInfo(info);
        //        }
        //    }
        //    BattleWindow.Singleton.OpenChild<BattleExpWindow>(WinInfo.Create(false, null, false));
        //    int lastActId = LevelService.Singleton.GetLastActID(LevelService.Singleton.LevelModel);
        //    if (lastActId <= MissionId)
        //    {
        //        RestoreWndMgr.Singleton.RemoveData<GuanQiaWindow>();
        //    }
        //    LevelService.Singleton.UpdateDungeonInfo(FightRes);
        //}
    }

    /// <summary>
    /// 获取该关卡有多少波怪物
    /// </summary>
    /// <returns></returns>
    public int GetWaveCount()
    {
        int res = 0;
        t_dungeon_actBean missionBean = ConfigBean.GetBean<t_dungeon_actBean, int>(MissionId);
        if (missionBean != null)
        {
            if (!IsNullWave(missionBean.t_wave_monster1))
                res++;
            if (!IsNullWave(missionBean.t_wave_monster2))
                res++;
            if (!IsNullWave(missionBean.t_wave_monster3))
                res++;
        }
        return res;
    }

    public bool IsNullWave(string monsters)
    {
        if (string.IsNullOrEmpty(monsters))
            return true;
        string[] arr = GTools.splitString(monsters, ';');
        if (arr == null || arr.Length <= 0)
            return true;

        for (int j = 0; j < arr.Length; j++)
        {
            int[] arrIds = GTools.splitStringToIntArray(arr[j], '+');
            for (int i = 0; i < arrIds.Length; i++)
            {
                if (arrIds[i] > 0)
                    return false;
            }
        }
 
        return true;
    }

    /// <summary>
    /// 获取宠物战斗结果状态
    /// </summary>
    /// <param name="petId"></param>
    /// <returns></returns>
    public int GetPetStatus(int petId)
    {
        if (FightRes.petInfos != null)
        {
            int targetIndex = 0;
            for (int i = 0; i < FightRes.petInfos.Count; i++)
            {
                if (FightRes.petInfos[i].petId == petId)
                {
                    targetIndex = i;
                    break;
                }
            }

            if (FightRes.petStatus != null)
            {
                if (targetIndex < FightRes.petStatus.Count)
                {
                    return FightRes.petStatus[targetIndex];
                }
            }

        }
        return 0;
    }


    /// <summary>
    /// 退出战斗
    /// </summary>
    public void QuitBattle(bool needLoading = true)
    {
        Clear();
        UnityEngine.Time.timeScale = 1.0f;
        WinMgr.Singleton.CloseAll();
        WinMgr.RemovePackage(WinEnum.UI_BattleEnd);
        WinMgr.RemovePackage(WinEnum.UI_Battle);


        TwoParam<string, bool> param = new TwoParam<string, bool>();
        param.value1 = GameState.Battle;
        param.value2 = needLoading;
        SceneLoader.Singleton.nextState = GameState.MainCity;
        SceneLoader.Singleton.sceneName = GSceneName.MaiCity;

       if (GameManager.Singleton.GetLastState() == GameState.Guild)
        {
            SceneLoader.Singleton.nextState = GameState.Guild;
            SceneLoader.Singleton.sceneName = GSceneName.Guild;
        }
        
        GameManager.Singleton.changeState(GameState.Loading, param);


    }


    public void Clear()
    {
        WaveCount = 0;
        MissionId = 0;
        Gold.Clear();
        GetedGold = 0;
        Wave0Gold.Clear();
        Wave1Gold.Clear();
        Wave2Gold.Clear();
    }

    /**********************************************************/

    /// <summary>
    /// 获取怪物先手值
    /// </summary>
    /// <returns></returns>
    public int GetWavePrecedeVal()
    {
        t_dungeon_actBean bean = ConfigBean.GetBean<t_dungeon_actBean, int>(BattleService.Singleton.MissionId);
        if (bean == null)
            return 0;

        if (SpawnerHelper.Singleton.CurWave == 0)
            return bean.t_priority_value1;
        if (SpawnerHelper.Singleton.CurWave == 1)
            return bean.t_priority_value2;
        if (SpawnerHelper.Singleton.CurWave == 2)
            return bean.t_priority_value3;
        return 0;
    }

    public string GetWaveName()
    {
        t_dungeon_actBean bean = ConfigBean.GetBean<t_dungeon_actBean, int>(BattleService.Singleton.MissionId);
        int[] names = GTools.splitStringToIntArray(bean.t_monster_name);
        int index = SpawnerHelper.Singleton.CurWave;
        if (names != null && index < names.Length)
        {
            int lanId = names[index];
            t_languageBean lanBean = ConfigBean.GetBean<t_languageBean, int>(lanId);
            if (lanBean != null)
                return lanBean.t_content;
            return lanId+"";
        }
        return null;
    }


    public int GetedGold { private set; get; }
    private Queue<int> Wave0Gold = new Queue<int>();
    private Queue<int> Wave1Gold = new Queue<int>();
    private Queue<int> Wave2Gold = new Queue<int>();
    public int WaveCount { private set; get; }
    /// <summary>
    /// 金币拆分
    /// </summary>
    public void SeparateGold()
    {
        t_dungeon_actBean bean = ConfigBean.GetBean<t_dungeon_actBean, int>(MissionId);
        if (bean != null)
        {
            if (!string.IsNullOrEmpty(bean.t_wave_monster1))
            {
                int[] arr = GTools.splitStringToIntArray(GetStrMonster(bean.t_wave_monster1));
                int len = 0;
                for (int i = 0; i < arr.Length; i++)
                {
                    if (arr[i] > 0)
                        len++;
                }
                int[] goldArr = GTools.DevideNum(bean.t_drop_gold1, len);
                for (int i = 0; i < goldArr.Length; i++)
                {
                    Wave0Gold.Enqueue(goldArr[i]);
                }
            }
            if (!string.IsNullOrEmpty(bean.t_wave_monster2))
            {
                int[] arr = GTools.splitStringToIntArray(GetStrMonster(bean.t_wave_monster2));
                int len = 0;
                for (int i = 0; i < arr.Length; i++)
                {
                    if (arr[i] > 0)
                        len++;
                }
                int[] goldArr = GTools.DevideNum(bean.t_drop_gold2, len);
                for (int i = 0; i < goldArr.Length; i++)
                {
                    Wave1Gold.Enqueue(goldArr[i]);
                }
            }
            if (!string.IsNullOrEmpty(bean.t_wave_monster3))
            {
                int[] arr = GTools.splitStringToIntArray(GetStrMonster(bean.t_wave_monster3));
                int len = 0;
                for (int i = 0; i < arr.Length; i++)
                {
                    if (arr[i] > 0)
                        len++;
                }
                int[] goldArr = GTools.DevideNum(bean.t_drop_gold3, len);
                for (int i = 0; i < goldArr.Length; i++)
                {
                    Wave2Gold.Enqueue(goldArr[i]);
                }
            }
        }
    }

    public void AddGold(int wave)
    {
        if (wave == 0)
        {
            if(Wave0Gold != null && Wave0Gold.Count > 0)
                GetedGold += Wave0Gold.Dequeue();
        }
        else if (wave == 1)
        {
            if (Wave1Gold != null && Wave1Gold.Count > 0)
                GetedGold += Wave1Gold.Dequeue();
        }
        else if (wave == 2)
        {
            if (Wave2Gold != null && Wave2Gold.Count > 0)
                GetedGold += Wave2Gold.Dequeue();
        }
    }


    
    public string GetStrMonster(string strMonster)
    {
        if (EFightType.HuanXiangDungeon == FightService.Singleton.FightType)
        {
           int index = ChallegeService.Singleton.GetHuanxiangChallengeIndex();
            string[] arrStr = GTools.splitString(strMonster, ';');
            if (arrStr != null && arrStr.Length > index)
                return arrStr[index];
        }
        return strMonster;
    }

    /**********************************************************/
}

