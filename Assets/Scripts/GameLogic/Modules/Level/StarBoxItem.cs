using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_Level;
using FairyGUI;
using Message.Dungeon;
using Data.Beans;

public class StarBoxItem : UI_starBoxItem {

    public int chapterID;
    public int index;
    private bool isKeyReceive;

    public LevelDataManager levelData;

    public new static UI_starBoxItem CreateInstance()
    {
        return (UI_starBoxItem)UIPackage.CreateObject("UI_Level", "starBoxItem");
    }

    public void Init(LevelDataManager levelData, bool isKeyReceive = false)
    {
        this.levelData = levelData;
        this.isKeyReceive = isKeyReceive;

        m_toucher.onClick.Add(OnClickBox);
    }

    public void RefreshView()
    {
        int boxStatus = -1;
        int model = (int)LevelService.Singleton.LevelModel;
        ChapterInfo chapterInfo = LevelService.Singleton.GetChapterInfoByID(chapterID);
        if(chapterInfo != null)
            boxStatus = chapterInfo.boxStatus[index];

        m_normalStarGroup.visible = !isKeyReceive;
        m_keyReceiveStarGroup.visible = isKeyReceive;
        string needStar = "";

        t_dungeon_chapterBean chapterBean = ConfigBean.GetBean<t_dungeon_chapterBean, int>(chapterID);
        if (chapterBean != null)
        {
            if (!string.IsNullOrEmpty(chapterBean.t_box))
            {
                string[] boxArr = chapterBean.t_box.Split(';');
                if (boxArr.Length > index)
                {
                    string[] boxInfo = boxArr[index].Split('+');
                    needStar = boxInfo[0];
                }
            }
        }

        m_normalStarNum.text = needStar;
        m_keyReceiveStarNum.text = needStar;

        RefreshBoxStatus(boxStatus);
    }
    /// <summary>
    ///  刷新箱子的状态
    /// </summary>
    /// <param name="status"></param>
    private void RefreshBoxStatus(int status)
    {
        m_redPoiint.visible = (status == 0 && !isKeyReceive);
        m_toucher.touchable = !isKeyReceive;

        if (status == 0)
        {
            m_flashIcon.visible = true;
            //m_anim.ChangeRepeat(-1);
            if(!m_anim.playing)
                m_anim.Play(AnimCompleteCall);
        }
        else
        {
            m_flashIcon.visible = false;
            if (m_anim.playing)
                m_anim.Stop();
        }

        UIGloader.SetUrl(m_iconLoader, UIUtils.GetLoaderUrl(WinEnum.UI_Level, UIUtils.GetBoxIcon(LevelService.Singleton.LevelModel, BoxType.Star, status, index)));
    }

    private void OnClickBox()
    {
        ThreeParam<BoxType, LevelDataManager, int> param = new ThreeParam<BoxType, LevelDataManager, int>();
        param.value1 = BoxType.Star;
        param.value2 = levelData;
        param.value3 = index;
        WinInfo winInfo = new WinInfo();
        winInfo.param = param;
        WinMgr.Singleton.Open<NormalBoxOpenWindow>(winInfo , UILayer.Popup);
    }

    private void AnimCompleteCall()
    {
        m_anim.Play(AnimCompleteCall);
    }

    public override void Dispose()
    {
        base.Dispose();

        levelData = null;
    }
}
