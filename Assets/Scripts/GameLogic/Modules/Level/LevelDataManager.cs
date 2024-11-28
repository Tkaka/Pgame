using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Message.Dungeon;
using Message.Pet;
using Data.Beans;

public enum LevelType
{
    NormalSmall = 0,          // 普通小怪
    NormalBig = 1,            // 普通打怪
    NormalBoss = 2,           // 普通终关boss
    EliteSmall = 3,           // 精英小怪
    EliteBoss = 4,            // 精英终关boss
}

public class LevelDataManager {

    /// <summary>
    /// 当前选择章节索引   从1开始
    /// </summary>
    private int currSelectChapterIndex;
    /// <summary>
    /// 当前选择的关卡ID
    /// </summary>
    private int currSelectLevelID;

    /// <summary>
    /// 没有领取的箱子，按章节整合了
    /// </summary>
    private Dictionary<int, Dictionary<int, BoxType>> unReceiveBoxChapterDic = new Dictionary<int, Dictionary<int, BoxType>>();
    /// <summary>
    /// 没有达到3星的关卡以及对应的章节
    /// </summary>
    private Dictionary<int, List<int>> unThreeStarLevelDict = new Dictionary<int, List<int>>();
    #region 属性-------------------------------------------------------------------------------------------------------
    /// <summary>
    /// 当前选择的章节索引
    /// </summary>
    public int CurrSelectChapterIndex
    {
        get { return currSelectChapterIndex; }
        set
        {
            if(currSelectChapterIndex == value)
                    return;

            currSelectChapterIndex = value;

            DungeonInfo dungeonInfo = LevelService.Singleton.GetDungeonInfo(LevelService.Singleton.LevelModel);
            if(dungeonInfo != null && dungeonInfo.chapterInfos.Count >= currSelectChapterIndex)
                LevelService.Singleton.currSelectChapterID = dungeonInfo.chapterInfos[currSelectChapterIndex - 1].chapterId;
           
        }
    }
    /// <summary>
    /// 当前选择的关卡ID
    /// </summary>
    public int CurrSelectLevelID
    {
        get { return currSelectLevelID; }
        set
        {
            if (currSelectLevelID == value)
            {
                return;
            }

            currSelectLevelID = value;
        }
    }
    /// <summary>
    /// 当前选择的章节ID
    /// </summary>
    public int CurrSelectChapterID
    {
        get { return LevelService.Singleton.currSelectChapterID; }
        set
        {
            if (LevelService.Singleton.currSelectChapterID == value)
            {
                return;
            }

            LevelService.Singleton.currSelectChapterID = value;
        }
    }
    public Dictionary<int ,Dictionary<int, BoxType>> UnReceiveBoxChapterDic
    {
        get { return unReceiveBoxChapterDic; }
    }

    public Dictionary<int, List<int>> UnThreeStarLevelDict
    {
        get { return unThreeStarLevelDict; }
    }

    #endregion

    public LevelDataManager()
    {
        InitData();
    }

    public void InitData()
    {
        InitChapterInfo();
    }
    /// <summary>
    /// 初始化章节信息
    /// </summary>
    private void InitChapterInfo()
    {
        int recentlyActID = 0;
        switch (LevelService.Singleton.LevelModel)
        {
            case LevelModel.Main:
                recentlyActID = LevelService.Singleton.NormalRecentlyID;
                break;
            case LevelModel.Elite:
                recentlyActID = LevelService.Singleton.EliteRecentlyID;
                break;
            case LevelModel.Nightmare:
                break;
            case LevelModel.Cycle:
                break;
            default:
                break;
        }

        currSelectLevelID = recentlyActID;
        t_dungeon_actBean actBean = ConfigBean.GetBean<t_dungeon_actBean, int>(recentlyActID);
        if (actBean != null)
        {
            int chapterID = actBean.t_chapter_id;
            LevelService.Singleton.currSelectChapterID = chapterID;
            if (!IsCanOpenNextChapter())
            {
                CurrSelectChapterID--;
            }
            currSelectChapterIndex = CurrSelectChapterID % 100;
        }
    }
    /// <summary>
    /// 是否打开了精英关卡
    /// </summary>
    /// <returns></returns>
    public bool IsOpenEliteChapter()
    {
        int unlockActID = GetOpenEliteChapterActID();
        ActInfo actInfo = LevelService.Singleton.GetActInfoByID(unlockActID);
        if (actInfo == null)
        {
            return false;
        }

        return actInfo.star > 0;
    }
    /// <summary>
    /// 获得打开精英关卡的ID
    /// </summary>
    /// <returns></returns>
    public int GetOpenEliteChapterActID()
    {
        // 全局表里配置解锁章节ID
        t_globalBean globalBean = ConfigBean.GetBean<t_globalBean, int>(19022);
        if (globalBean == null)
            return int.MaxValue;

        return globalBean.t_int_param;
    }
    /// <summary>
    /// 获得该章节的星星总数
    /// </summary>
    /// <returns></returns>
    public int GetChapterStarNum(int chapterID)
    {
        t_dungeon_chapterBean chapterBean = ConfigBean.GetBean<t_dungeon_chapterBean, int>(chapterID);
        if (chapterBean == null)
        {
            return int.MaxValue;
        }

        int actNum = 0;
        if (!string.IsNullOrEmpty(chapterBean.t_act_id))
        {
            string[] actArr = chapterBean.t_act_id.Split('+');
            actNum = actArr.Length;
        }
        // 每个关卡最多3星评分
        return actNum * 3;
    }
    /// <summary>
    /// 是否存在没有领取的箱子
    /// </summary>
    /// <returns></returns>
    public bool IsExitUnReceiveBox(LevelModel levelModel)
    {
        // 遍历所有的章节

        DungeonInfo dungeonInfo = LevelService.Singleton.GetDungeonInfo(levelModel);
        if (dungeonInfo == null)
        {
            return false;
        }

        List<ChapterInfo> chapterInfoList = dungeonInfo.chapterInfos;
        int count = chapterInfoList.Count;
        ChapterInfo chapterInfo = null;
        ActInfo actInfo = null;
        for (int i = 0; i < count; i++)
        {
            chapterInfo = chapterInfoList[i];
            // 遍历该章节的宝箱是否存在未领取的
            for (int j = 0; j < chapterInfo.boxStatus.Count; j++)
            {
                int status = chapterInfo.boxStatus[j];
                if (status == 0)
                    return true;
            }
            // 遍历章节的关卡宝箱是否存在未领取的
            for (int j = 0; j < chapterInfo.actInfos.Count; j++)
            {
                actInfo = chapterInfo.actInfos[j];
                if (actInfo.boxStatus == 0)
                    return true;
            }
        }

        return false;
    }
    /// <summary>
    /// 设置没有领取的箱子信息
    /// </summary>
    public void SetUnReceiveBox()
    {
        unReceiveBoxChapterDic.Clear();

        DungeonInfo dungeonInfo = LevelService.Singleton.GetDungeonInfo(LevelService.Singleton.LevelModel);
        if (dungeonInfo == null)
            return;

        List<ChapterInfo> chapterInfoList = dungeonInfo.chapterInfos;
        int count = chapterInfoList.Count;
        ChapterInfo chapterInfo = null;
        ActInfo actInfo = null;
        Dictionary<int, BoxType > boxInfoDict = null;
        for (int i = 0; i < count; i++)
        {
            chapterInfo = chapterInfoList[i];
            boxInfoDict = null;
            // 遍历章节的关卡宝箱是否存在未领取的
            for (int j = 0; j < chapterInfo.actInfos.Count; j++)
            {
                actInfo = chapterInfo.actInfos[j];
                if (actInfo.boxStatus == 0)
                {
                    if (boxInfoDict == null)
                    {
                        boxInfoDict = new Dictionary<int, BoxType>();
                    }

                    boxInfoDict.Add(actInfo.actId, BoxType.Act);
                }

            }
            // 遍历该章节的宝箱是否存在未领取的
            for (int j = 0; j < chapterInfo.boxStatus.Count; j++)
            {
                int status = chapterInfo.boxStatus[j];
                if (status == 0)
                {
                    if (boxInfoDict == null)
                    {
                        boxInfoDict = new Dictionary<int, BoxType>();
                    }

                    boxInfoDict.Add(j, BoxType.Star);
                }
            }

            if (boxInfoDict != null)
            {
                unReceiveBoxChapterDic.Add(chapterInfo.chapterId, boxInfoDict);
            }
        }
    }
    /// <summary>
    /// 设置没有达到3星的关卡信息
    /// </summary>
    public void SetUnThreeStarLevel()
    {
        // 遍历所有的开启的关卡
        unThreeStarLevelDict.Clear();
        DungeonInfo dungeonInfo = LevelService.Singleton.GetDungeonInfo(LevelService.Singleton.LevelModel);
        if (dungeonInfo == null)
            return;

        List<ChapterInfo> chapterInfoList = dungeonInfo.chapterInfos;
        int count = chapterInfoList.Count;

        ChapterInfo chapterInfo = null;
        ActInfo actInfo = null;
        List<int> actIDList = null;
        for (int i = 0; i < count; i++)
        {
            chapterInfo = chapterInfoList[i];
            actIDList = null;
            for (int j = 0; j < chapterInfo.actInfos.Count; j++)
            {
                actInfo = chapterInfo.actInfos[j];
                if (actInfo.star > 0 && actInfo.star < 3)
                {
                    if (actIDList == null)
                    {
                        actIDList = new List<int>();
                    }
                    actIDList.Add(actInfo.actId);
                }
            }

            if (actIDList != null)
            {
                unThreeStarLevelDict.Add(chapterInfo.chapterId, actIDList);
            }
        }
    }
    /// <summary>
    /// 是否显示快速跳转的按钮
    /// </summary>
    /// <returns></returns>
    public bool IsShowQuickJumpBtn()
    {
        int languageID = 0;
        switch (LevelService.Singleton.LevelModel)
        {
            case LevelModel.Main:
                // 19013  主线关卡快速跳转功能开启的等级限制
                languageID = 19013;
                break;
            case LevelModel.Elite:
                // 19023  精英关卡快速跳转功能开启的等级限制
                languageID = 19013;
                break;
            case LevelModel.Nightmare:
                break;
            case LevelModel.Cycle:
                break;
            default:
                break;
        }

        t_globalBean languageBean = ConfigBean.GetBean<t_globalBean, int>(languageID);
        if (languageBean == null)
            return false;

        int limitLv = languageBean.t_int_param;

        Message.Role.RoleInfo roleInfo = RoleService.Singleton.GetRoleInfo();
        return roleInfo.level >= limitLv;
    }
    /// <summary>
    /// 是否开启新章节
    /// </summary>
    /// <returns></returns>
    public bool IsOpenNewChapter()
    {
        if (!IsCanOpenNextChapter())
            return false;

        // 获得本地存储的最新章节的信息,如果跟当前最新章节不是一样的，那么就是新章节
        bool isNewChapter = false;
        switch (LevelService.Singleton.LevelModel)
        {
            case LevelModel.Main:
                isNewChapter = IsNewNormalChater();
                break;
            case LevelModel.Elite:
                isNewChapter = IsNewEliteChapter();
                break;
            case LevelModel.Nightmare:
                break;
            case LevelModel.Cycle:
                break;
            default:
                break;
        }

        return isNewChapter;
    }

    private bool IsNewNormalChater()
    {
        object newChapterObj = PlayerLocalData.GetData("NewNormalChapterID", null);
        int lastChapterID = 0;
        if (newChapterObj == null)
        {
            lastChapterID = GetLastChapterID();
            PlayerLocalData.SetData(PlayerLocalDataKey.NewNormalChapterID, lastChapterID);
            return true;
        }
        string newChapterIDStr = newChapterObj.ToString();
        int newChapterID = int.Parse(newChapterIDStr);
        lastChapterID = GetLastChapterID();

        if (newChapterID == lastChapterID)
            return false;

        PlayerLocalData.SetData(PlayerLocalDataKey.NewNormalChapterID, lastChapterID);
        return true;
    }

    private bool IsNewEliteChapter()
    {
        object newChapterObj = PlayerLocalData.GetData(PlayerLocalDataKey.NewEliteChapterID, null);
        int lastChapterID = 0;
        if (newChapterObj == null)
        {
            lastChapterID = GetLastChapterID();
            PlayerLocalData.SetData(PlayerLocalDataKey.NewEliteChapterID, lastChapterID);
            return true;
        }
        string newChapterIDStr = newChapterObj.ToString();
        int newChapterID = int.Parse(newChapterIDStr);
        lastChapterID = GetLastChapterID();

        if (newChapterID == lastChapterID)
            return false;

        PlayerLocalData.SetData(PlayerLocalDataKey.NewEliteChapterID, lastChapterID);
        return true;
    }
    /// <summary>
    /// 是否能开启下一个章节
    /// </summary>
    /// <returns></returns>
    public bool IsCanOpenNextChapter()
    {
        int lastChapterID = GetLastChapterID();
        t_dungeon_chapterBean chapterBean = ConfigBean.GetBean<t_dungeon_chapterBean, int>(lastChapterID);
        if (chapterBean == null)
            return false;

        int needLv = chapterBean.t_open_lv;
        Message.Role.RoleInfo roleInfo = RoleService.Singleton.GetRoleInfo();
        return roleInfo.level >= needLv;
    }
    /// <summary>
    /// 获得当前模式下的最新章节ID
    /// </summary>
    /// <returns></returns>
    public int GetLastChapterID()
    {
        int lastActID = 0;
        switch (LevelService.Singleton.LevelModel)
        {
            case LevelModel.Main:
                lastActID = LevelService.Singleton.NormalRecentlyID;
                break;
            case LevelModel.Elite:
                lastActID = LevelService.Singleton.EliteRecentlyID;
                break;
            case LevelModel.Nightmare:
                break;
            case LevelModel.Cycle:
                break;
            default:
                break;
        }

        t_dungeon_actBean actBean = ConfigBean.GetBean<t_dungeon_actBean, int>(lastActID);
        if (actBean == null)
            return 0;

        return actBean.t_chapter_id;
    }
    /// <summary>
    /// 判断通关后的最新章节ID是否是下一章的
    /// </summary>
    /// <returns></returns>
    public bool IsNextChapterAct(int actID)
    {
        // 获得当前章节的最后一个关卡ID
        t_dungeon_chapterBean chapterBean = ConfigBean.GetBean<t_dungeon_chapterBean, int>(LevelService.Singleton.currSelectChapterID);
        if (chapterBean == null)
            return false;
        if (!string.IsNullOrEmpty(chapterBean.t_act_id))
        {
            string[] actList = chapterBean.t_act_id.Split('+');
            int length = actList.Length;
            int lastActID = int.Parse(actList[length - 1]);

            return actID > lastActID;
        }

        return false;
    }

    /// <summary>
    ///  是否开启了快速切换的按钮
    /// </summary>
    /// <returns></returns>
    public bool IsOpenDoubleSwitchBtn()
    {
        int openLv = GetOpenQuickSiwtchBtnID();
        int lastMainLevelID = LevelService.Singleton.GetLastActID(LevelModel.Main);
        return lastMainLevelID > openLv;
    }
    /// <summary>
    /// 获得快速切换按钮的关卡id
    /// </summary>
    /// <returns></returns>
    public int GetOpenQuickSiwtchBtnID()
    {
        // 19031
        t_globalBean globalBean = ConfigBean.GetBean<t_globalBean, int>(19031);
        if (globalBean != null)
        {
            return globalBean.t_int_param;
        }

        return int.MaxValue;
    }

    private void RefreshData()
    {

    }

    public void OnClose()
    {
        // 保存当前最新的章节
        PlayerLocalData.Save();

        unReceiveBoxChapterDic.Clear();
        unReceiveBoxChapterDic = null;

        unThreeStarLevelDict.Clear();
        unThreeStarLevelDict = null;
    }
}
