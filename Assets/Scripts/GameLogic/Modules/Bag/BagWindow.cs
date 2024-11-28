using Message.Bag;
using System.Collections.Generic;
using UI_Beibao;
using Data.Beans;
using FairyGUI;
using UI_Common;

/// <summary>
/// 背包窗口
/// </summary>
public class BagWindow : BaseWindow
{

    public UI_BagWindow window { private set; get; }
    private List<GridInfo> m_curGirds;
    private BagWindowCtrl windowCtrl;

    private int m_onePageGridNum = 16;
    private int m_oneLineGridNum = 4;
    private UITable m_table = new UITable();

    public override void OnOpen()
    {
        base.OnOpen();
        window = getUiWindow<UI_BagWindow>();
        m_table.Init(window.m_bagToggleGroup.m_ctrl, OnTabCtrlChanged);
        m_table.AddBtnAnim(window.m_bagToggleGroup.m_btnAll, window.m_bagToggleGroup.m_btnEquip, window.m_bagToggleGroup.m_btnCaiLiao,
            window.m_bagToggleGroup.m_btnSuiPian, window.m_bagToggleGroup.m_btnComsume);

        InitView();
        PlayOpenEffect();
    }

    public override void PlayOpenEffect()
    {
        base.PlayOpenEffect();

        window.m_bagToggleGroup.m_anim.Play();
        (window.m_commonTop as UI_Common.UI_commonTop).m_anim.Play();
    }

    private void OnTabCtrlChanged(int index = 0)
    {
        windowCtrl.LastSelectedIndex = 0;
        RefreshList();
    }

    private void RefreshList()
    {
        List<GridInfo> items = null;
        if (window.m_bagToggleGroup.m_ctrl.selectedIndex == 0)
        {
            items = BagService.Singleton.GetGrids();
        }
        else if (window.m_bagToggleGroup.m_ctrl.selectedIndex == 1)
        {
            items = BagService.Singleton.GetGridsByCategory(ItemCategory.EquipMaterial);
        }
        else if (window.m_bagToggleGroup.m_ctrl.selectedIndex == 2)
        {
            items = BagService.Singleton.GetGridsByCategory(ItemCategory.Materials);
        }
        else if (window.m_bagToggleGroup.m_ctrl.selectedIndex == 3)
        {
            items = BagService.Singleton.GetGridsByCategory(ItemCategory.Fragment);
        }
        else if (window.m_bagToggleGroup.m_ctrl.selectedIndex == 4)
        {
            items = BagService.Singleton.GetGridsByCategory(ItemCategory.Consume);
        }

        ShowList(items);
    }

    private void _RenderListItem(int index, GObject obj)
    {
        CommonItem bagItem = obj as CommonItem;
        if (bagItem == null)
            return;

        if (index < 0 || m_curGirds == null)
        {
            Debuger.Log("背包格子信息异常" + index);
            return;
        }

        if (index >= m_curGirds.Count)
        {
            bagItem.SetEmptyIcon();
            return;
        }


       GridInfo grid = m_curGirds[index];
       if (grid.itemInfo.num <= 0)
           return;

       t_itemBean bean = ConfigBean.GetBean<t_itemBean, int>(grid.itemInfo.id);
       if (bean != null)
       {
            bagItem.Init(grid.itemInfo.id, grid.itemInfo.num, true);
            bagItem.RefreshView();
            window.m_allList.onClickItem.Add(OnItemSelect);
            if (index == windowCtrl.LastSelectedIndex)
            {
                bagItem.SelectToggle(true);
                windowCtrl.OnItemSelect(grid);
            }
            else
            {
                bagItem.SelectToggle(false);
            }
        }
    }

    private string _ItemProvider(int index)
    {
        return CommonItem.URL;
    }

    public override void AddEventListener()
    {
        base.AddEventListener();
        GED.ED.addListener(EventID.ResBoxItemUse, OnBoxResItemUse);
    }

    public override void RemoveEventListener()
    {
        base.RemoveEventListener();
        GED.ED.removeListener(EventID.ResBoxItemUse, OnBoxResItemUse);
    }

    /// <summary>
    /// 开宝箱直接更新列表
    /// </summary>
    /// <param name="evt"></param>
    private void OnBoxResItemUse(GameEvent evt)
    {
        RefreshList();
    }

    public override void InitView()
    {
        base.InitView();
        window.m_allList.itemRenderer = _RenderListItem;
        window.m_allList.itemProvider = _ItemProvider;
        window.m_allList.defaultItem = CommonItem.URL;
        window.m_allList.SetVirtual();
        windowCtrl = new BagWindowCtrl(this);

        UI_commonTop commonTop = window.m_commonTop as UI_commonTop;
        if (commonTop != null)
        {
            commonTop.m_closeBtn.onClick.Add(OnCloseBtn);
        }

        //  TODO 临时
        (window.m_commonTop as UI_Common.UI_commonTop).m_title.visible = false;

        OnTabCtrlChanged();
    }

    private void ShowList(List<GridInfo> grids)
    {
        m_curGirds = grids;
        if (grids != null && grids.Count > 0)
        {
            if (windowCtrl.LastSelectedIndex >= m_curGirds.Count)
                windowCtrl.LastSelectedIndex = m_curGirds.Count - 1;
            windowCtrl.LastSelectedGridId = m_curGirds[windowCtrl.LastSelectedIndex].id;

            window.m_content.visible = true;
            window.m_nullTip.visible = false;

            //不足一页不足一页，不足一行补足一行

            int realGridNum = m_curGirds.Count;
            if (realGridNum < m_onePageGridNum)
            {
                realGridNum = m_onePageGridNum;
            }
            else
            {
                int remainNum = realGridNum % m_oneLineGridNum;
                realGridNum += (m_oneLineGridNum - remainNum);
            }
            window.m_allList.numItems = realGridNum;
        }
        else
        {
            window.m_content.visible = false;
            window.m_nullTip.visible = true;
            window.m_allList.numItems = 0;
        }
    }

    private void OnItemSelect(EventContext context)
    {
        int itemIndex = window.m_allList.selectedIndex;
        if (itemIndex < 0)
            return;
        windowCtrl.LastSelectedIndex = itemIndex;
        if(windowCtrl.LastSelectedIndex < m_curGirds.Count)
            windowCtrl.LastSelectedGridId = m_curGirds[windowCtrl.LastSelectedIndex].id;
        window.m_allList.RefreshVirtualList();
    }

    protected override void OnClose()
    {
        base.OnClose();
        windowCtrl.OnClose();
    }

}