using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_Level;

public class QuickJumpWindow : BaseWindow {

    UI_QuickJumpWindow window;

    private LevelDataManager levelData
    {
        get { return Info.param as LevelDataManager; }
    }

    public override void OnOpen()
    {
        base.OnOpen();

        window = getUiWindow<UI_QuickJumpWindow>();
        window.m_mask.onClick.Add(OnCloseBtn);
        window.m_popupView.m_closeBtn.onClick.Add(OnCloseBtn);
        InitView();
        PlayPopupAnim(window.m_mask, window.m_popupView);
    }

    public override void InitView()
    {
        base.InitView();

        InitQuickChapterList();
    }
    /// <summary>
    /// 初始话快速跳转的列表
    /// </summary>
    private void InitQuickChapterList()
    {
        List<int> chapterIDList = new List<int>();
        chapterIDList.AddRange(levelData.UnThreeStarLevelDict.Keys);
        int count = chapterIDList.Count;

        QuickJumpChapterItem quickJumpChapterItem = null;
        for (int i = 0; i < count; i++)
        {
            quickJumpChapterItem = QuickJumpChapterItem.CreateInstance() as QuickJumpChapterItem;
            quickJumpChapterItem.chapterID = chapterIDList[i];
            quickJumpChapterItem.Init(levelData);
            window.m_popupView.m_quickJumpChaterList.AddChild(quickJumpChapterItem);
        }
    }

    protected override void OnCloseBtn()
    {

        base.OnCloseBtn();
    }
}
