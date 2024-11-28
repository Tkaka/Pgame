using Data.Beans;
using Message.Bag;
using UI_Beibao;
using UnityEngine;
using FairyGUI;

public class ItemUseWindow : BaseWindow
{

    private UI_ItemUseWindow window;

    private GridInfo gridInfo = null;
    private t_itemBean bean = null;

    public override void OnOpen()
    {
        base.OnOpen();
        window = getUiWindow<UI_ItemUseWindow>();
        int key = (int)Info.param;
        gridInfo = BagService.Singleton.GetGrid(key);
        window.m_mask.onClick.Add(OnCloseBtn);
        if (gridInfo != null)
        {
            InitView();
            PlayPopupAnim(window.m_mask, window.m_popupView);
        }
        else
        {
            Logger.err("ItemSellWindow:OnOpen:can not find GridInfo : " + key);
        }
    }

    public override void InitView()
    {
        base.InitView();
        if (gridInfo != null)
        {
            bean = ConfigBean.GetBean<t_itemBean, int>(gridInfo.itemInfo.id);
            window.m_popupView.m_closeBtn.onClick.Add(OnCloseBtn);
            window.m_popupView.m_sellBtn.onClick.Add(OnSellBtn);
            window.m_popupView.m_btnMax.onClick.Add(OnMaxBtn);
            window.m_popupView.m_btnMin.onClick.Add(OnMinBtn);
            window.m_popupView.m_slider.onChanged.Add(OnSidlerValueChange);
            window.m_popupView.m_slider.value = 0;
            OnSidlerValueChange();
            if (bean != null)
            {
                UIGloader.SetUrl(window.m_popupView.m_icon.m_borderLoader, UIUtils.GetItemBorder(bean.t_id));
                UIGloader.SetUrl(window.m_popupView.m_icon.m_iconLoader, bean.t_icon);
                window.m_popupView.m_icon.m_numTxt.visible = false;
                window.m_popupView.m_itemName.text = bean.t_name;
                window.m_popupView.m_itemName.color = UIUtils.GetItemColor(bean.t_id);
            }
        }
    }

    private int sellNum;
    private void OnSidlerValueChange()
    {
        //Logger.log(window.m_slider.value +"");
        if (gridInfo != null && bean != null)
        {
            //int sellNum = (int)(window.m_slider.value * itemInfo.num * 0.01f);
            sellNum = Mathf.CeilToInt((float)window.m_popupView.m_slider.value * gridInfo.itemInfo.num * 0.01f);
            sellNum = Mathf.Max(1, sellNum);
            window.m_popupView.m_sellNum.text = sellNum + "/" + gridInfo.itemInfo.num;
        }
    }

    private void OnSellBtn()
    {
        if (sellNum > 0 && gridInfo != null)
        {
            if (sellNum <= gridInfo.itemInfo.num)
                BagService.Singleton.ReqItemUse(gridInfo.id, sellNum);
            else
                Logger.err("ItemSellWindow:OnSellBtn:sellnum > itemInfo.num:" + sellNum + "_" + gridInfo.itemInfo.num);
        }
        else
        {
            Logger.err("ItemSellWindow:OnSellBtn:" + sellNum + "_" + gridInfo);
        }
        Close();
    }


    private void OnMaxBtn()
    {
        sellNum = gridInfo.itemInfo.num;
        window.m_popupView.m_slider.value = 100;
        window.m_popupView.m_sellNum.text = sellNum + "/" + gridInfo.itemInfo.num;

    }

    private void OnMinBtn()
    {
        sellNum = 1;
        window.m_popupView.m_slider.value = 1;
        window.m_popupView.m_sellNum.text = sellNum + "/" + gridInfo.itemInfo.num;
    }


    protected override void OnClose()
    {
        base.OnClose();
        gridInfo = null;
        bean = null;
    }
}
