using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_Level;
using FairyGUI;
using Message.Dungeon;
using DG.Tweening;

public class LevelBoxItem : UI_levelBoxItem {

    public int levelID;
    private bool isKeyReceive;
    private int boxStatus;
    private long coroutineID;

    Tweener posTweener;
    private LevelDataManager levelData;

    public new static LevelBoxItem CreateInstance()
    {
        return UI_levelBoxItem.CreateInstance() as LevelBoxItem;
    }

    public void Init(LevelDataManager levelData, bool isKeyReceive = false)
    {
        this.levelData = levelData;
        this.isKeyReceive = isKeyReceive;
        coroutineID = -1;
        m_toucher.onClick.Add(OnClickBoxItem);

        RefreshView();
    }

    public void RefreshView()
    {
        RefreshBoxStatus();
        ShowAnimation();
        RefreshKeyReceive();
    }
    /// <summary>
    /// 刷新一键领取时的UI
    /// </summary>
    private void RefreshKeyReceive()
    {
        m_toucher.touchable = !isKeyReceive;
        m_topName.visible = isKeyReceive;
        int chapterIndex = levelID / 100 % 100;
        int levelIndex = levelID % 100;
        m_topName.text = string.Format("通关{0}-{1}", chapterIndex, levelIndex);
        UIGloader.SetUrl(m_iconLoader, UIUtils.GetLoaderUrl(WinEnum.UI_Level, UIUtils.GetBoxIcon(LevelService.Singleton.LevelModel, BoxType.Act, boxStatus)));
    }
    private void RefreshBoxStatus()
    {
        ActInfo actInfo = LevelService.Singleton.GetActInfoByID(levelID);
        boxStatus = actInfo.boxStatus;
    }

    private void ShowAnimation()
    {
        if (boxStatus == 0)
        {
            if (!m_anim.playing)
                m_anim.Play(AnimCompleteCall);
            m_teXiaoGroup.visible = true;
        }

        if (boxStatus != 0)
        {
            if (m_anim.playing)
                m_anim.Stop();
            m_teXiaoGroup.visible = false;
        }
    }
    
    private void AnimCompleteCall()
    {
        m_anim.Play(AnimCompleteCall);
    }


    private void OnClickBoxItem()
    {
        ThreeParam<BoxType, LevelDataManager, int> param = new ThreeParam<BoxType, LevelDataManager, int>();
        param.value1 = BoxType.Act;
        param.value2 = levelData;
        param.value3 = levelID;

        WinInfo winInfo = new WinInfo();
        winInfo.param = param;
        WinMgr.Singleton.Open<NormalBoxOpenWindow>(winInfo, UILayer.Popup);
    }

    public override void Dispose()
    {
        base.Dispose();

        if (posTweener != null && posTweener.IsActive())
        {
            posTweener.Kill();
        }
        posTweener = null;

        if (coroutineID != -1)
            CoroutineManager.Singleton.stopCoroutine(coroutineID);

        levelData = null;

    }
}
