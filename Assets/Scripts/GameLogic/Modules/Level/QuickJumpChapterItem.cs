using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_Level;
using FairyGUI;
using Data.Beans;

public class QuickJumpChapterItem : UI_quickJumpChapterItem {

    public int chapterID;

    private LevelDataManager levelData;

    public new static UI_quickJumpChapterItem CreateInstance()
    {
        return (UI_quickJumpChapterItem)UIPackage.CreateObject("UI_Level", "quickJumpChapterItem");
    }

    public void Init(LevelDataManager levelData)
    {
        this.levelData = levelData;

        InitView();
    }

    private void InitView()
    {
        InitNormalView();

        List<int> actIDList = levelData.UnThreeStarLevelDict[chapterID];
        int count = actIDList.Count;

        QuickJumpLevelItem quickJumpLevelItem = null;
        for (int i = 0; i < count; i++)
        {
            quickJumpLevelItem = QuickJumpLevelItem.CreateInstance() as QuickJumpLevelItem;
            quickJumpLevelItem.levelID = actIDList[i];
            quickJumpLevelItem.Init();
            m_actList.AddChild(quickJumpLevelItem);
        }
    }

    private void InitNormalView()
    {
        t_dungeon_chapterBean chapterBean = ConfigBean.GetBean<t_dungeon_chapterBean, int>(chapterID);
        if (chapterBean != null)
        {
            int index = chapterID % 100;
            // 600 第{0}章
            string str = UIUtils.GetStrByLanguageID(600);
            m_chapterNumLabel.text = string.Format(str, index);
            m_chapterName.text = chapterBean.t_name_id;
        }
    }

    public override void Dispose()
    {
        levelData = null;

        base.Dispose();
    }
}
