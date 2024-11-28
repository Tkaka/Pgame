using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_Level;
using UI_Common;
using FairyGUI;
using Data.Beans;
using Message.Dungeon;

public class LevelMainWindow : BaseWindow, IGuidable {

    private float viewWidth;
    /// <summary>
    /// 是否在滚动的过程中切换了章节
    /// </summary>
    private bool isFlipPage;
    /// <summary>
    /// 是否改变了章节模式
    /// </summary>
    private bool isChangeModel;

    public UI_LevelMainWindow window;
    LevelDataManager levelData;
    SwipeGesture swipeGersture;
    BuZhenColumn buZhenColumn;
    /// <summary>
    /// 滑动的响应距离
    /// </summary>
    private float reponseDis = 0;

    public LevelDataManager LevelData
    {
        get { return levelData; }
    }

    public override void OnOpen()
    {
        base.OnOpen();
        RestoreWndMgr.Singleton.ClearData();
        InitData();
        InitView();
        RefreshView();
        BindEvent();
        PlayOpenEffect();
    }

    private void InitData()
    {
        window = getUiWindow<UI_LevelMainWindow>();
        levelData = new LevelDataManager();

        switch (LevelService.Singleton.LevelModel)
        {
            case LevelModel.None:
                PetService.Singleton.zhenRongType = ZhenRongType.Normal;
                break;
            case LevelModel.Main:
                PetService.Singleton.zhenRongType = ZhenRongType.Normal;
                break;
            case LevelModel.Elite:
                PetService.Singleton.zhenRongType = ZhenRongType.Normal;
                break;
            case LevelModel.Nightmare:
                PetService.Singleton.zhenRongType = ZhenRongType.EMeng;
                break;
            case LevelModel.Cycle:
                break;
            default:
                break;
        }

        viewWidth = window.m_levelList.scrollPane.viewWidth;
    }

    public override void PlayOpenEffect()
    {
        base.PlayOpenEffect();

        (window.m_commonTop as UI_commonTop).m_anim.Play();
    }

    public override void AddEventListener()
    {
        base.AddEventListener();
    

        GED.ED.addListener(EventID.OnFightSuccessed, OnFightSuccess);
        GED.ED.addListener(EventID.OnLevelOpenBox, OnBoxOpen);
        GED.ED.addListener(EventID.OnDungeonInfoUpdate, OnDungeonInfoUpdate);
        GED.ED.addListener(EventID.FunOpenEvent, _OnFunOpenEvent);
    }

    public override void RemoveEventListener()
    {
        base.RemoveEventListener();

        GED.ED.removeListener(EventID.OnFightSuccessed, OnFightSuccess);
        GED.ED.removeListener(EventID.OnLevelOpenBox, OnBoxOpen);
        GED.ED.removeListener(EventID.OnDungeonInfoUpdate, OnDungeonInfoUpdate);
        GED.ED.removeListener(EventID.FunOpenEvent, _OnFunOpenEvent);
    }

    public override void InitView()
    {
        base.InitView();

        InitChapterList();
        InitStarBoxList();
        RefreshDungeonBtnRedPoint();

        window.m_levelList.scrollPane.bouncebackEffect = false;
        window.m_levelList.scrollPane.mouseWheelEnabled = false;
        //window.m_levelList.scrollPane.touchEffect = false;
        //window.m_levelList.scrollItemToViewOnClick = false;
        isFlipPage = false;
        buZhenColumn = new BuZhenColumn((UI_buZhenColumn)window.m_buZhenColumn, true);
        (window.m_commonTop as UI_commonTop).m_title.text = "主线关卡";
        //swipeGersture = new SwipeGesture(window.m_levelList);
        //swipeGersture.onMove.Add(OnSwipeMove);
        //swipeGersture.onEnd.Add(OnSwipeEnd);

        window.m_btnSupperSweep.visible = FuncService.Singleton.IsFuncOpen(19011);
        ResetPanelPos();
    }

    private void BindEvent()
    {
        window.m_leftChapterBtn.onClick.Add(OnLeftChapterBtnClick);
        window.m_rightChapterBtn.onClick.Add(OnRightChapterBtnClick);
        window.m_levelList.scrollPane.onScroll.Add(OnPanelScroll);
        window.m_levelList.scrollPane.onScrollEnd.Add(OnPanelScrollEnd);
        window.m_leftDoubleArrowBtn.onClick.Add(LeftDoubleArrowBtnClick);
        window.m_rightDoubleArrowBtn.onClick.Add(RightDoubleArrowBtnClick);

        (window.m_commonTop as UI_Common.UI_commonTop).m_closeBtn.onClick.Add(OnCloseBtn);

        //window.m_eliteLevelBtn.onClick.Add(OnEliteBtnClick);
        window.m_mainLevelBtn.onClick.Add(OnNormalLevelBtnClick);

        window.m_zhenRongBtn.onClick.Add(OnZhenRongBtnClick);
        window.m_keyReceiveBtn.onClick.Add(OnKeyReceiveBtnClick);
        window.m_quickJumpBtn.onClick.Add(OnQuickJumpBtnClick);

        window.m_btnSupperSweep.onClick.Add(OnClickSupperSweep);
    }
    /// <summary>
    /// 初始化章节列表
    /// </summary>
    private void InitChapterList()
    {
        // 根据当前开启的章节数去加载关卡

        DungeonInfo dungeonInfo = LevelService.Singleton.GetDungeonInfo(LevelService.Singleton.LevelModel);
        if (dungeonInfo == null)
            return;

        window.m_levelList.itemRenderer = ChapterListRender;
        window.m_levelList.SetVirtual();
        window.m_levelList.numItems = dungeonInfo.chapterInfos.Count;

    }
    /// <summary>
    /// 初始化章节宝箱列表
    /// </summary>
    private void InitStarBoxList()
    {
        StarBoxItem starBoxItem = null;

        starBoxItem = window.m_starBox1 as StarBoxItem;
        starBoxItem.Init(levelData);

        starBoxItem = window.m_starBox2 as StarBoxItem;
        starBoxItem.Init(levelData);

        starBoxItem = window.m_starBox3 as StarBoxItem;
        starBoxItem.Init(levelData);
    }

    private void ResetPanelPos()
    {
        int page = levelData.CurrSelectChapterIndex - 1;
        //window.m_levelList.scrollPane.SetPosX(viewWidth * page, true);
        //window.m_levelList.ScrollToView(page, false);
        window.m_levelList.scrollPane.currentPageX = page;
    }

    private void AddLevelList(int chapterID)
    {
        MainLevel mainLevel = UIPackage.CreateObject(WinEnum.UI_Level, "mainLevel") as MainLevel;
        window.m_levelList.AddChild(mainLevel);
        mainLevel.SetSize(GRoot.inst.width, GRoot.inst.height);
        mainLevel.AddRelation(window, RelationType.Size);
        mainLevel.chapterID = chapterID;
        mainLevel.Init(this);
    }
    

    public override void RefreshView()
    {
        base.RefreshView();

        t_dungeon_chapterBean chapterBean = ConfigBean.GetBean<t_dungeon_chapterBean, int>(levelData.CurrSelectChapterID);
        if(chapterBean != null)
            window.m_chapterName.text = chapterBean.t_name_id;

        RefreshLevelModelTip();
        RefreshBottomView();
        ShowNewChapterOpenEffect();
        RefreshChapterList();
        RefreshSwitchChapterBtn();
    }
    /// <summary>
    /// 显示新章节开启的特效
    /// </summary>
    private void ShowNewChapterOpenEffect()
    {
        if (levelData.IsOpenNewChapter())
        {
            // 播放新章节开启的特效
            WinMgr.Singleton.Open<NewChapterOpenWindow>(null, UILayer.Popup);
        }
    }

    private void RefreshLevelModelTip()
    {
        UIGloader.SetUrl(window.m_chapterType, UIUtils.GetLoaderUrl(WinEnum.UI_Level, LevelService.Singleton.GetLevelModelTipIcon(LevelService.Singleton.LevelModel)));
        UIGloader.SetUrl(window.m_chapterNameBg, UIUtils.GetLoaderUrl(WinEnum.UI_Common, LevelService.Singleton.GetLevelModelTipNameBgIcon(LevelService.Singleton.LevelModel)));
    }
    /// <summary>
    /// 刷新切换章节的按钮的显示与隐藏
    /// </summary>
    private void RefreshSwitchChapterBtn()
    {
        window.m_leftChapterBtn.visible = levelData.CurrSelectChapterIndex > 1;
        window.m_rightChapterBtn.visible = levelData.CurrSelectChapterIndex < window.m_levelList.numItems;
        window.m_leftDoubleArrowBtn.visible = levelData.CurrSelectChapterIndex > 1 && levelData.IsOpenDoubleSwitchBtn();
        window.m_rightDoubleArrowBtn.visible = levelData.CurrSelectChapterIndex < window.m_levelList.numChildren && levelData.IsOpenDoubleSwitchBtn();
    }
    /// <summary>
    /// 刷新底部的UI
    /// </summary>
    private void RefreshBottomView()
    {
        RefreshStarBoxList();
        RefreshStarPrograss();
        RefreshBtnStatus();
    }
    /// <summary>
    /// 刷新星星的进度条
    /// </summary>
    private void RefreshStarPrograss()
    {
        ChapterInfo chapterInfo = LevelService.Singleton.GetChapterInfoByID(levelData.CurrSelectChapterID);
        int haveStar = 0;
        if (chapterInfo != null)
            haveStar = chapterInfo.star;

        int maxStar = levelData.GetChapterStarNum(levelData.CurrSelectChapterID);
        window.m_starProgress.value = haveStar;
        window.m_starProgress.max = maxStar;

        window.m_starNumLabel.text = string.Format("{0}/{1}", haveStar, maxStar);
    }
    /// <summary>
    /// 刷新章节按钮的状态
    /// </summary>
    private void RefreshBtnStatus()
    {
        //bool isUnlockElite = levelData.IsOpenEliteChapter();
        //window.m_eliteLevelBtn.m_btnIcon.grayed = !isUnlockElite;
        //window.m_eliteLevelBtn.m_lockIcon.visible = !isUnlockElite;

        window.m_keyReceiveBtn.visible = levelData.IsExitUnReceiveBox(LevelService.Singleton.LevelModel);

        window.m_quickJumpBtn.visible = levelData.IsShowQuickJumpBtn();
    }
    /// <summary>
    /// 刷新关卡模式的红点提示
    /// </summary>
    private void RefreshDungeonBtnRedPoint()
    {
        LevelModel curModel = LevelService.Singleton.LevelModel;
        window.m_mainLevelBtn.m_redPoint.visible = (curModel != LevelModel.Main && levelData.IsExitUnReceiveBox(LevelModel.Main));
        window.m_eliteLevelBtn.m_redPoint.visible = (curModel != LevelModel.Elite && levelData.IsExitUnReceiveBox(LevelModel.Elite));
    }
    /// <summary>
    /// 刷新化章节星级宝箱
    /// </summary>
    private void RefreshStarBoxList()
    {
        t_dungeon_chapterBean chapterBean = ConfigBean.GetBean<t_dungeon_chapterBean, int>(levelData.CurrSelectChapterID);
        if (chapterBean != null)
        {
            if (!string.IsNullOrEmpty(chapterBean.t_box))
            {
                string[] boxArr = chapterBean.t_box.Split(';');
                StarBoxItem starBoxItem = null;
                window.m_starBox1.visible = true;
                window.m_starBox2.visible = true;
                window.m_starBox3.visible = true;
                if (boxArr.Length == 1)
                {
                    window.m_starBox1.visible = false;
                    window.m_starBox2.visible = false;
                    starBoxItem = window.m_starBox3 as StarBoxItem;
                    starBoxItem.chapterID = levelData.CurrSelectChapterID;
                    starBoxItem.index = 0;
                    starBoxItem.RefreshView();
                }
                else if(boxArr.Length == 2)
                {
                    window.m_starBox1.visible = false;

                    starBoxItem = window.m_starBox2 as StarBoxItem;
                    starBoxItem.chapterID = levelData.CurrSelectChapterID;
                    starBoxItem.index = 0;
                    starBoxItem.RefreshView();

                    starBoxItem = window.m_starBox3 as StarBoxItem;
                    starBoxItem.chapterID = levelData.CurrSelectChapterID;
                    starBoxItem.index = 1;
                    starBoxItem.RefreshView();
                }
                else
                {
                    starBoxItem = window.m_starBox1 as StarBoxItem;
                    starBoxItem.chapterID = levelData.CurrSelectChapterID;
                    starBoxItem.index = 0;
                    starBoxItem.RefreshView();

                    starBoxItem = window.m_starBox2 as StarBoxItem;
                    starBoxItem.chapterID = levelData.CurrSelectChapterID;
                    starBoxItem.index = 1;
                    starBoxItem.RefreshView();

                    starBoxItem = window.m_starBox3 as StarBoxItem;
                    starBoxItem.chapterID = levelData.CurrSelectChapterID;
                    starBoxItem.index = 2;
                    starBoxItem.RefreshView();
                }
            }
        }
    }
    /// <summary>
    /// 刷新章节列表
    /// </summary>
    private void RefreshChapterList()
    {
        DungeonInfo dungeonInfo = LevelService.Singleton.GetDungeonInfo(LevelService.Singleton.LevelModel);
        window.m_levelList.numItems = dungeonInfo.chapterInfos.Count;
    }

    private void ChapterListRender(int index, GObject obj)
    {
        MainLevel level = obj as MainLevel;
        DungeonInfo dungeonInfo = LevelService.Singleton.GetDungeonInfo(LevelService.Singleton.LevelModel);
        level.chapterID = dungeonInfo.chapterInfos[index].chapterId;
        level.Init(this);
    }    

    #region   事件响应---------------------------------------------------------------------------------------------

    private void OnLeftChapterBtnClick()
    {
        //window.m_levelList.scrollPane.ScrollLeft(1, true);
        int page = levelData.CurrSelectChapterIndex - 1;
        window.m_levelList.ScrollToView(page - 1, true);
        //window.m_levelList.scrollPane.currentPageX = page - 1;
        window.m_levelList.selectedIndex = page;
        levelData.CurrSelectChapterIndex--;
        RefreshView();
    }

    private void OnRightChapterBtnClick()
    {
        int curLastChapterID = levelData.GetLastChapterID();
        if (levelData.CurrSelectChapterID + 1 >= curLastChapterID && !levelData.IsCanOpenNextChapter())
        {
            t_dungeon_chapterBean chapterBean = ConfigBean.GetBean<t_dungeon_chapterBean, int>(curLastChapterID);
            if (chapterBean != null)
            {
                string tip = string.Format("下一个大地图需要等级{0}开启", chapterBean.t_open_lv);
                TipWindow.Singleton.ShowTip(tip);
            }
            return;
        }

        int page = levelData.CurrSelectChapterIndex - 1;
        window.m_levelList.ScrollToView(page + 1, true);
        //window.m_levelList.scrollPane.currentPageX = page + 1;
        window.m_levelList.selectedIndex = page;
        levelData.CurrSelectChapterIndex++;
        RefreshView();
    }
    private void OnPanelScroll()
    {
        int page = levelData.CurrSelectChapterIndex - 1;
        float scrollPanelPos = window.m_levelList.scrollPane.posX;
        int curLastChapterID = levelData.GetLastChapterID();
        if (levelData.CurrSelectChapterID + 1 >= curLastChapterID &&
            !levelData.IsCanOpenNextChapter() && scrollPanelPos > viewWidth * page)
        {
            t_dungeon_chapterBean chapterBean = ConfigBean.GetBean<t_dungeon_chapterBean, int>(curLastChapterID);
            if (chapterBean != null)
            {
                string tip = string.Format("下一个大地图需要等级{0}开启", chapterBean.t_open_lv);
                TipWindow.Singleton.ShowTip(tip);
            }

            int levelPage = levelData.CurrSelectChapterIndex - 1;
            window.m_levelList.scrollPane.SetPosX(viewWidth * levelPage, false);
            return;
        }
        // 如果滚动的距离少于页面的一半，则不刷新页面，否则刷新

        if (scrollPanelPos - viewWidth * page >= viewWidth * 0.5f)
        {
            levelData.CurrSelectChapterIndex++;
            RefreshView();
        }
        if (viewWidth * page - scrollPanelPos >= viewWidth * 0.5f)
        {
            levelData.CurrSelectChapterIndex--;
            RefreshView();
        }
    }

    private void OnPanelScrollEnd()
    {

    }

    private void OnSwipeMove()
    {
        if (isFlipPage == false)
        {
            if (swipeGersture.delta.x < -reponseDis)
            {
                
                if (levelData.CurrSelectChapterIndex < window.m_levelList.numItems)
                {
                    OnRightChapterBtnClick();
                }
                else
                {
                    int curLastChapterID = levelData.GetLastChapterID();
                    if (levelData.CurrSelectChapterID + 1 >= curLastChapterID && !levelData.IsCanOpenNextChapter())
                    {
                        t_dungeon_chapterBean chapterBean = ConfigBean.GetBean<t_dungeon_chapterBean, int>(curLastChapterID);
                        if (chapterBean != null)
                        {
                            string tip = string.Format("下一个大地图需要等级{0}开启", chapterBean.t_open_lv);
                            TipWindow.Singleton.ShowTip(tip);
                        }
                    }
                }
            }
            else if(swipeGersture.delta.x  > reponseDis)
            {
                if (levelData.CurrSelectChapterIndex > 1)
                {
                    OnLeftChapterBtnClick();
                }
            }
            isFlipPage = true;
        }
    }

    private void OnSwipeEnd()
    {
        isFlipPage = false;
    }

    private void LeftDoubleArrowBtnClick()
    {
        // 点击后回到第一个章节
        window.m_levelList.scrollPane.SetPosX(0, true);
    }

    private void RightDoubleArrowBtnClick()
    {
        // 点击后跳转到当前开启的最大的章节
        window.m_levelList.scrollPane.SetPosX(levelData.CurrSelectChapterIndex * viewWidth, true);
    }

    private void OnEliteBtnClick()
    {
        if (LevelService.Singleton.LevelModel == LevelModel.Elite)
        {
            return;
        }
        if (!levelData.IsOpenEliteChapter())
        {
            // 没开启，提示
            int openEliteID = levelData.GetOpenEliteChapterActID();
            t_dungeon_actBean actBean = ConfigBean.GetBean<t_dungeon_actBean, int>(openEliteID);
            if (actBean != null)
            {
                string tipStr = string.Format("通关{0}-{1} {2} 开启精英关卡", actBean.t_chapter_id % 100, openEliteID % 100, actBean.t_name_id);
                TipWindow.Singleton.ShowTip(tipStr);
            }
            return;
        }
        isChangeModel = true;
        PetService.Singleton.zhenRongType = ZhenRongType.Normal;
        LevelService.Singleton.LevelModel = LevelModel.Elite;
        levelData.InitData();
        RefreshView();
        ResetPanelPos();
        RefreshDungeonBtnRedPoint();
    }

    private void OnNormalLevelBtnClick()
    {
        if (LevelService.Singleton.LevelModel == LevelModel.Main)
        {
            return;
        }
        isChangeModel = true;
        PetService.Singleton.zhenRongType = ZhenRongType.Normal;
        LevelService.Singleton.LevelModel = LevelModel.Main;
        levelData.InitData();
        RefreshView();
        ResetPanelPos();
        RefreshDungeonBtnRedPoint();
    }

    private void OnZhenRongBtnClick()
    {
        buZhenColumn.IsShow = !buZhenColumn.IsShow;
    }

    private void OnTeamColumnAnimComplete()
    {
        if (window.m_buZhenColumn.scaleX == 0)
        {
            window.m_buZhenColumn.visible = false;
        }
    }

    private void OnKeyReceiveBtnClick()
    {
        // 打开一键领取的界面
        WinMgr.Singleton.Open<KeyBoxOpenWindow>(WinInfo.Create(false, null, true, levelData), UILayer.Popup);
    }

    private void OnQuickJumpBtnClick()
    {
        levelData.SetUnThreeStarLevel();
        WinMgr.Singleton.Open<QuickJumpWindow>(WinInfo.Create(false, null, true, levelData), UILayer.TopHUD);
    }

    //超级扫荡点击
    private void OnClickSupperSweep()
    {
        int model = LevelData.CurrSelectChapterID / 1000000;
        if (model == (int)LevelModel.Main)
        {
            //OpenChild<SupperSaoDangWindow>();
            WinMgr.Singleton.Open<SupperSaoDangWindow>(null, UILayer.TopHUD);
        }
        else if (model == (int)LevelModel.Elite)
        {
            WinMgr.Singleton.Open<eliteSupperSaoDangWnd>(null, UILayer.TopHUD);
  
        }
    }

    private void OnFightSuccess(GameEvent evt)
    {
        // 先判断打的关卡是不是新的
        int actID = (int)evt.Data;
        int curLastActID = LevelService.Singleton.GetLastActID(LevelService.Singleton.LevelModel);
        if (actID <= curLastActID)
        {
            // 打的是之前老的关卡
            RefreshView();
        }
        else
        {
            // 打的是新关卡
            // 判断是否通关了当前章节所有关卡
            if (levelData.IsNextChapterAct(curLastActID))
            {
                if (levelData.IsCanOpenNextChapter())
                {
                    // TODO ： 播放通关特效
                    RefreshView();
                    //InitChapterList();
                }
            }
            else
            {
                // TODO ： 播放下一关开通的特效
                RefreshView();
            }
        }
    }

    private void OnBoxOpen(GameEvent evt)
    {
        RefreshView();
    }

    private void OnDungeonInfoUpdate(GameEvent evt)
    {
        levelData.InitData();
        InitChapterList();
        if(levelData.IsCanOpenNextChapter())
            ResetPanelPos();
        RefreshView();
    }

    private void _OnFunOpenEvent(GameEvent evt)
    {
        int funID = (int)evt.Data;
        if (funID == 19011)
        {
            window.m_btnSupperSweep.visible = FuncService.Singleton.IsFuncOpen(19011);
        }
    }

    #endregion;

    protected override void OnClose()
    {
        window = null;
        swipeGersture = null;
        levelData.OnClose();

        if (buZhenColumn != null)
        {
            buZhenColumn.OnDispose();
        }
        RestoreWndMgr.Singleton.SaveWndData<LevelMainWindow>(Info);

        base.OnClose();
    }

    public GObject GetGuideObj(string param)
    {
        int[] paramArr = GTools.splitStringToIntArray(param, ',');
        if (paramArr.Length > 1)
        {
            // 关卡ID
            int actID = paramArr[1];
            int childIndex = levelData.CurrSelectChapterIndex - 1;
            int itemIndex = window.m_levelList.ChildIndexToItemIndex(childIndex);
            MainLevel mainLevel = window.m_levelList.GetChildAt(itemIndex) as MainLevel;
            return mainLevel.GetLevelItemByLevelID(actID);
        }

        return null;
    }
}
