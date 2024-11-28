using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_Level;
using FairyGUI;
using Data.Beans;

public class MainLevel :  UI_mainLevel{
    /// <summary>
    /// 章节ID
    /// </summary>
    public int chapterID;

    private LevelMainWindow parentUI;
    List<Vector2> posList = new List<Vector2>();
    private t_dungeon_chapterBean chapterBean = null;
    private float scale;
    private List<GComponent> levelItemList = new List<GComponent>();
    private List<LevelBoxItem> levelBoxList = new List<LevelBoxItem>();

    public new static UI_mainLevel CreateInstance()
    {
        return (UI_mainLevel)UIPackage.CreateObject("UI_Level", "mainLevel");
    }

    public LevelDataManager levelData
    {
        get { return parentUI.LevelData; }
    }

    public void Init(LevelMainWindow parentUI)
    {
        this.parentUI = parentUI;
        chapterBean = ConfigBean.GetBean<t_dungeon_chapterBean, int>(chapterID);
        if (chapterBean != null)
        {
            this.SetSize(GRoot.inst.width, GRoot.inst.height);
            this.AddRelation(parentUI.window, RelationType.Size);
            InitBg();
            InitActPos();
            InitLevelList();
            InitLevelBox();
        }
        else
            Logger.err("没有这个章节ID：" + chapterID);
    }
    /// <summary>
    /// 初始化章节背景
    /// </summary>
    private void InitBg()
    {
        bool isAnsy = chapterID != levelData.CurrSelectChapterID;
        UIGloader.SetUrl(m_bg, chapterBean.t_bg, isAnsy);
        // 适配
        if (m_bg.texture == null)
            return;

        float scaleW = GRoot.inst.width / WinMgr.DesignResolutionW;
        float scaleH = GRoot.inst.height / WinMgr.DesignResolutionH;
        if (scaleW >= scaleH)
        {
            scale = scaleW;
            m_bg.SetScale(scaleW, scaleW);
        }
        else
        {
            scale = scaleH;
            m_bg.SetScale(scaleH, scaleH);
        }
    }
    /// <summary>
    /// 初始化关卡的位置
    /// </summary>
    private void InitActPos()
    {
        string[] posArr = GTools.splitString(chapterBean.t_act_pos, ';');
        if (posArr != null)
        {
            int count = posArr.Length;
            int[] posInfo = null;
            Vector2 vector2;
            posList.Clear();
            for (int i = 0; i < count; i++)
            {
                posInfo = GTools.splitStringToIntArray(posArr[i]);
                if (posInfo.Length == 2)
                {
                    vector2 = new Vector2(posInfo[0], posInfo[1]);
                    vector2 *= scale;
                    posList.Add(vector2);
                }
            }
        }
    }
    private void InitLevelList()
    {
        int count = levelItemList.Count;
        for (int i = 0; i < count; i++)
        {
            this.RemoveChild(levelItemList[i], true);
        }
        // 根据章节Index获得对应章节的位置列表
        levelItemList.Clear();
        if (chapterBean != null)
        {
            switch (LevelService.Singleton.LevelModel)
            {
                case LevelModel.Main:
                    InitNormalActList();
                    break;
                case LevelModel.Elite:
                    InitEliteActList();
                    break;
                case LevelModel.Nightmare:
                    break;
                case LevelModel.Cycle:
                    break;
                default:
                    break;
            }
           
        }

    }
    /// <summary>
    ///  初始化普通关卡列表
    /// </summary>
    private void InitNormalActList()
    {
        if (!string.IsNullOrEmpty(chapterBean.t_act_id))
        {
            string[] actIDArr = chapterBean.t_act_id.Split('+');
            int count = actIDArr.Length;
            MainLevelItem mainLevelItem = null;

            for (int i = 0; i < count; i++)
            {
                mainLevelItem = MainLevelItem.CreateInstance() as MainLevelItem;
                this.AddChild(mainLevelItem);
                mainLevelItem.position = new Vector2(m_bg.position.x + posList[i].x, m_bg.position.y + posList[i].y); 
                mainLevelItem.actID = int.Parse(actIDArr[i]);
                mainLevelItem.Init(this);
                levelItemList.Add(mainLevelItem);
            }
        }
    }

    private void InitEliteActList()
    {
        if (!string.IsNullOrEmpty(chapterBean.t_act_id))
        {
            string[] actIDArr = chapterBean.t_act_id.Split('+');
            int count = actIDArr.Length;

            EliteLevelItem eliteLevelItem = null;
            for (int i = 0; i < count; i++)
            {
                eliteLevelItem = EliteLevelItem.CreateInstance() as EliteLevelItem;
                this.AddChild(eliteLevelItem);
                eliteLevelItem.actID = int.Parse(actIDArr[i]);
                eliteLevelItem.position = new Vector2(m_bg.position.x + posList[i].x, m_bg.position.y + posList[i].y);
                eliteLevelItem.Init(this);
                levelItemList.Add(eliteLevelItem);
            }
        }
    }
    /// <summary>
    /// 初始化关卡宝箱
    /// </summary>
    private void InitLevelBox()
    {
        int count = levelBoxList.Count;
        for (int i = 0; i < count; i++)
        {
            this.RemoveChild(levelBoxList[i], true);
        }
        string[] boxPosArr = GTools.splitString(chapterBean.t_act_box_pos, ';');
        if (boxPosArr == null)
            return;

        int[] actIDArr = GTools.splitStringToIntArray(chapterBean.t_act_id);
        count = boxPosArr.Length;
        t_dungeon_actBean actBean = null;
        LevelBoxItem levelBoxItem = null;
        int[] boxPosInfo = null;
        for (int i = 0; i < count; i++)
        {
            boxPosInfo = GTools.splitStringToIntArray(boxPosArr[i]);
            if (boxPosArr.Length == 1)
                return;

            actBean = ConfigBean.GetBean<t_dungeon_actBean, int>(actIDArr[i]);
            if (actBean != null && !string.IsNullOrEmpty(actBean.t_box_item_id))
            {
                levelBoxItem = LevelBoxItem.CreateInstance();
                levelBoxItem.levelID = actIDArr[i];
                levelBoxItem.Init(levelData);
                if(boxPosArr.Length > i)
                {
                    levelBoxItem.x = m_bg.x + boxPosInfo[0] * scale;
                    levelBoxItem.y = m_bg.y + boxPosInfo[1] * scale;
                }

                this.AddChild(levelBoxItem);
                levelBoxList.Add(levelBoxItem);
            }
        }
    }

    public GComponent GetLevelItemByLevelID(int actID)
    {
        t_dungeon_chapterBean chapterBean = ConfigBean.GetBean<t_dungeon_chapterBean, int>(chapterID);
        if (chapterBean != null)
        {
            int[] actIDArr = GTools.splitStringToIntArray(chapterBean.t_act_id, '+');
            int count = actIDArr.Length;
            if (count > 1)
            {
                for (int i = 0; i < count; i++)
                {
                    if (actIDArr[i] == actID)
                    {
                        return levelItemList[i];
                    }
                }
            }
        }

        return null;
    }

    public override void Dispose()
    {
        parentUI = null;
        if (posList != null)
            posList.Clear();
        posList = null;

        base.Dispose();
    }
}
