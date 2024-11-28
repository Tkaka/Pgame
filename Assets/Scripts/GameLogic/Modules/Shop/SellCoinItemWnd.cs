using Message.Role;
using UnityEngine;
using FairyGUI;
using UI_Shop;
using Message.Shop;
using Data.Beans;
using System.Collections.Generic;
using Message.Bag;


public class SellCoinItemWnd : BaseWindow
{
    private UI_SellCoinItemWnd m_window;
    private List<GridInfo> m_itemList = new List<GridInfo>();

    public override void OnOpen()
    {
        base.OnOpen();
        m_window = getUiWindow<UI_SellCoinItemWnd>();
        m_window.m_btnClose.onClick.Add(Close);
        m_window.m_btnOk.onClick.Add(_OnOkClick);
        _Init();

        if (m_itemList.Count == 0)
        {
            Close();
            return;
        }

        _ShowInfo();
    }

    private void _Init()
    {
        m_window.m_itemList.SetVirtual();
        m_window.m_itemList.itemProvider = _ItemProvider;
        m_window.m_itemList.itemRenderer = _ItemRender;

        foreach (var info in BagService.Singleton.BagInfo.grids)
        {
            ItemInfo itemInfo = info.itemInfo;
            if (itemInfo == null || itemInfo.id == 0 || itemInfo.num == 0)
                continue;

            t_itemBean itemBean = ConfigBean.GetBean<t_itemBean, int>(itemInfo.id);
            if (itemBean == null)
                continue;

            //金币出售物类型
            if (itemBean.t_type == 33)
            {
                m_itemList.Add(info);
            }
        }

    }

    private void _ShowInfo()
    {
        m_window.m_itemList.numItems = m_itemList.Count;
        int totalNum = 0;
        foreach (var gridInfo in m_itemList)
        {
            ItemInfo info = gridInfo.itemInfo;
            t_itemBean itemBean = ConfigBean.GetBean<t_itemBean, int>(info.id);
            if (itemBean == null)
                continue;

            int price = 0;
            int.TryParse(itemBean.t_value, out price);
            totalNum += (price * info.num);
        }

        m_window.m_txtCoinNum.text = totalNum + "";
    }


    private void _OnOkClick()
    {
        List<SellInfo> sellList = new List<SellInfo>();
        foreach (var gridInfo in m_itemList)
        {
            SellInfo sellInfo = new SellInfo();
            sellInfo.gridId = gridInfo.id;
            sellInfo.num = gridInfo.itemInfo.num;
            sellList.Add(sellInfo);
        }

        BagService.Singleton.ReqSellItem(sellList);
        Close();
    }

    private string _ItemProvider(int index)
    {
        return UI_objCoinItemCell.URL;
    }

    private void _ItemRender(int index, GObject obj)
    {
        UI_objCoinItemCell cell = obj as UI_objCoinItemCell;
        if (cell == null)
            return;

        if (index < 0 || index >= m_itemList.Count)
            return;



        ItemInfo itemInfo = m_itemList[index].itemInfo;
        t_itemBean itemBean = ConfigBean.GetBean<t_itemBean, int>(itemInfo.id);
        if (itemBean == null)
            return;

        cell.m_txtName.text = itemBean.t_name;
        cell.m_txtName.color = UIUtils.GetItemColor(itemInfo.id);
        cell.m_num.text = string.Format("X{0}", BagService.Singleton.GetItemNum(itemInfo.id));


        CommonItem itemIcon = cell.m_itemIcon as CommonItem;
        itemIcon.Init(itemInfo.id, 0, false);
        itemIcon.RefreshView();
    }

    protected override void OnClose()
    {
        base.OnClose();
        m_itemList.Clear();
    }
}