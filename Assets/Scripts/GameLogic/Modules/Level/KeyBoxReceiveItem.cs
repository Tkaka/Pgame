using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_Level;
using FairyGUI;
using Data.Beans;

public class KeyBoxReceiveItem : UI_keyBoxReceiveItem {

    public int ChapterID;

    private LevelDataManager levelData;
    private float offsetH;

    public new static UI_keyBoxReceiveItem CreateInstance()
    {
        return (UI_keyBoxReceiveItem)UIPackage.CreateObject("UI_Level", "keyBoxReceiveItem");
    }

    public void Init(LevelDataManager levelData)
    {
        this.levelData = levelData;
        offsetH = this.height - m_boxList.height;
        RefreshView();
    }

    private void RefreshView()
    {
        Dictionary<int, BoxType> boxItemInfoDict = null;
        if (levelData.UnReceiveBoxChapterDic.TryGetValue(ChapterID, out boxItemInfoDict))
        {
            BoxType boxType;
            LevelBoxItem levelBoxItem = null;
            StarBoxItem starBoxItem = null;
            foreach (int id in boxItemInfoDict.Keys)
            {
                boxType = boxItemInfoDict[id];
                switch (boxType)
                {
                    case BoxType.Act:
                        levelBoxItem = LevelBoxItem.CreateInstance() as LevelBoxItem;
                        levelBoxItem.levelID = id;
                        levelBoxItem.Init(levelData, true);
                        m_boxList.AddChild(levelBoxItem);
                        break;
                    case BoxType.Star:
                        starBoxItem = StarBoxItem.CreateInstance() as StarBoxItem;
                        starBoxItem.chapterID = ChapterID;
                        starBoxItem.index = id;
                        starBoxItem.Init(levelData, true);
                        starBoxItem.RefreshView();
                        m_boxList.AddChild(starBoxItem);
                        break;
                    default:
                        break;
                }
            }
            int itemCount = boxItemInfoDict.Count;
            m_boxList.ResizeToFit(itemCount);
            // 80 是高度偏移
            this.height = m_boxList.height + offsetH;
            
        }
        //  第 字语言ID存在全局表的ID 19050
        t_globalBean globalBean = ConfigBean.GetBean<t_globalBean, int>(19050);
        string diFont = "";
        if (globalBean != null)
            diFont = UIUtils.GetStrByLanguageID(globalBean.t_int_param);

        m_chapterName.text = string.Format(diFont, ChapterID % 100);
    }

}
