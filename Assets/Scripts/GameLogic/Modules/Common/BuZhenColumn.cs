using System.Collections;
using System.Collections.Generic;
using UI_Common;
using DG.Tweening;

public class BuZhenColumn {

    UI_buZhenColumn buZhenColumn;
    Tween tween;
    private float hidePosX;
    /// <summary>
    /// 隐藏时的偏移位置
    /// </summary>
    private float offsetX = 15f;
    /// <summary>
    /// 是否是可见的
    /// </summary>
    private bool isShow;
    private string startBtnTitle;
    public bool IsShow
    {
        get { return isShow; }
        set
        {
            if (isShow == value)
                return;
            isShow = value;
            if (tween != null && tween.IsActive())
                tween.Kill();
            if (isShow)
                tween = buZhenColumn.m_columnGroup.TweenMoveX(0, 0.5f);
            else
                tween = buZhenColumn.m_columnGroup.TweenMoveX(hidePosX, 0.5f);
        }
    }

    public BuZhenColumn(UI_buZhenColumn buZhenColumn, bool isHide = false, string startBtnTitle = "")
    {
        this.buZhenColumn = buZhenColumn;
        this.startBtnTitle = startBtnTitle;
        hidePosX = buZhenColumn.width + offsetX;
        if (isHide)
        {
            buZhenColumn.m_columnGroup.x = hidePosX;
            isShow = false;
        }

        InitView();
        RefreshView();
        AddListener();
        BindEvent();
    }

    private void InitView()
    {
        int count = buZhenColumn.m_zhenRongList.numChildren;
        BuZhenItem buZhenItem = null;
        for (int i = 0; i < count; i++)
        {
            buZhenItem = buZhenColumn.m_zhenRongList.GetChildAt(i) as BuZhenItem;
            buZhenItem.Init();
        }

        if (!string.IsNullOrEmpty(startBtnTitle))
            buZhenColumn.m_adoptZhenRong.text = startBtnTitle;
    }

    private void BindEvent()
    {
        buZhenColumn.m_keyShangZhen.onClick.Add(OnKeyShangZhenBtnClick);
        buZhenColumn.m_adoptZhenRong.onClick.Add(OnAdoptZhenRongBtnClick);
    }

    public void RefreshView()
    {
        int count = buZhenColumn.m_zhenRongList.numChildren;
        List<int> shangZhenIDList = PetService.Singleton.GetTeamList(PetService.Singleton.zhenRongType);
        if (shangZhenIDList == null || shangZhenIDList.Count < count)
            return;

        BuZhenItem buZhenItem = null;
        for (int i = 0; i < count; i++)
        {
            int id = shangZhenIDList[i];
            buZhenItem = buZhenColumn.m_zhenRongList.GetChildAt(i) as BuZhenItem;
            buZhenItem.petID = id;
            buZhenItem.RefreshView();
        }
    }

    private void OnKeyShangZhenBtnClick()
    {
        PetService.Singleton.KeyShangZhen(PetService.Singleton.zhenRongType);
        //RefreshView();
    }

    private void OnAdoptZhenRongBtnClick()
    {
        // 打开布阵界面
        WinMgr.Singleton.Open<BuZhenWindow>(null, UILayer.Popup);
    }

    private void OnPetTeamListChanged(GameEvent evt)
    {
        RefreshView();
    }

    private void AddListener()
    {
        GED.ED.addListener(EventID.OnPetTeamListChanged, OnPetTeamListChanged);
    }
    public void OnDispose()
    {
        GED.ED.removeListener(EventID.OnPetTeamListChanged, OnPetTeamListChanged);
        if (tween != null && tween.IsActive())
        {
            tween.Kill();
            tween = null;
        }
    }
}
