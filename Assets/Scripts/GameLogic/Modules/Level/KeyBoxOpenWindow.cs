using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_Level;
using Message.Bag;

public class KeyBoxOpenWindow : BaseWindow {

    UI_KeyBoxOpenWindow window;

    public LevelDataManager levelData
    {
        get { return Info.param as LevelDataManager; }
    }

    public override void OnOpen()
    {
        base.OnOpen();

        window = getUiWindow<UI_KeyBoxOpenWindow>();

        window.m_popupView.m_receiveBtn.onClick.Add(OnReveiveBtnClick);
        window.m_mask.onClick.Add(OnCloseBtn);
        window.m_popupView.m_closeBtn.onClick.Add(OnCloseBtn);
        InitView();
        PlayPopupAnim(window.m_mask, window.m_popupView);
    }

    public override void InitView()
    {
        base.InitView();

        InitChapterUnReceiveBox();
    }

    public override void AddEventListener()
    {
        base.AddEventListener();
        GED.ED.addListener(EventID.OnLevelOpenBox, OnOpenBox);
    }

    public override void RemoveEventListener()
    {
        base.RemoveEventListener();
        GED.ED.removeListener(EventID.OnLevelOpenBox, OnOpenBox);
    }

    /// <summary>
    /// 显示没有领取的箱子
    /// </summary>
    private void InitChapterUnReceiveBox()
    {
        levelData.SetUnReceiveBox();
        List<int> chapterList = new List<int>();
        chapterList.AddRange(levelData.UnReceiveBoxChapterDic.Keys);
        int count = chapterList.Count;

        int chapterID = 0;
        KeyBoxReceiveItem keyBoxReceiveItem = null;
        for (int i = 0; i < count; i++)
        {
            chapterID = chapterList[i];
            keyBoxReceiveItem = KeyBoxReceiveItem.CreateInstance() as KeyBoxReceiveItem;
            keyBoxReceiveItem.ChapterID = chapterID;
            keyBoxReceiveItem.Init(levelData);
            window.m_popupView.m_chapterBoxList.AddChild(keyBoxReceiveItem);
        }
    }

    private void OnOpenBox(GameEvent evt)
    {
        // 打开一键领取的界面
        ThreeParam<bool, List<Message.Bag.ItemInfo>, string> param = new ThreeParam<bool, List<Message.Bag.ItemInfo>, string>();
        param.value1 = false;
        param.value2 = evt.Data as List<Message.Bag.ItemInfo>;
        WinMgr.Singleton.Open<BoxReceiveWidow>(WinInfo.Create(false, null, true, param), UILayer.Popup);
        OnCloseBtn();
    }

    private void OnReveiveBtnClick()
    {
        // 发送请求
        LevelService.Singleton.ReqFastOpenBox();
        //WinMgr.Singleton.Open<KeyBoxReceiveWidow>(WinInfo.Create(false, null, true, null), UILayer.Popup);
    }

    protected override void OnCloseBtn()
    {
        window = null;

        base.OnCloseBtn();
    }
}
