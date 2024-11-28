using Data.Beans;
using Message.Bag;
using UI_Beibao;
using UnityEngine;

public class ItemSellWindow : BaseWindow
{

    private UI_ItemSellWindow window;

    private t_itemBean bean = null;

    private ThreeParam<int, int, int> param;

    public override void OnOpen()
    {
        base.OnOpen();
        window = getUiWindow<UI_ItemSellWindow>();
        param = (ThreeParam<int, int, int>)Info.param;
        window.m_mask.onClick.Add(OnCloseBtn);
        if (param != null)
        {
            InitView();
            PlayPopupAnim(window.m_mask, window.m_popupView);
        }
        else
        {
            Logger.err("ItemSellWindow:OnOpen:param is error");
        }
    }

    public override void InitView()
    {
        base.InitView();
        if (param != null)
        {
            bean = ConfigBean.GetBean<t_itemBean, int>(param.value1);
            window.m_popupView.m_closeBtn.onClick.Add(OnCloseBtn);
            window.m_popupView.m_sellBtn.onClick.Add(OnSellBtn);
            window.m_popupView.m_slider.onChanged.Add(OnSidlerValueChange);
            window.m_popupView.m_btnMin.onClick.Add(OnMinBtnClick);
            window.m_popupView.m_btnMax.onClick.Add(OnMaxBtnClick);
            window.m_popupView.m_slider.value = 0;
            window.m_popupView.m_slider.max = param.value2;
            OnSidlerValueChange();
            UIGloader.SetUrl(window.m_popupView.m_icon.m_borderLoader, UIUtils.GetItemBorder(param.value1));
            UIGloader.SetUrl(window.m_popupView.m_icon.m_iconLoader, bean.t_icon);
            window.m_popupView.m_icon.m_numTxt.visible = false;
            window.m_popupView.m_itemName.text = bean.t_name;
        }
    }

    private int sellNum;
    private void OnSidlerValueChange()
    {
        if (param != null && bean != null)
        {
            //int sellNum = (int)(window.m_slider.value * itemInfo.num * 0.01f);
            sellNum = Mathf.FloorToInt((float)window.m_popupView.m_slider.value);
            sellNum = Mathf.Max(0, sellNum);
            window.m_popupView.m_sellNum.text = sellNum + "/" + param.value2;
            window.m_popupView.m_sellPrice.text = sellNum * bean.t_sell_price + "";
        }
    }

    private void OnSellBtn()
    {
        if (sellNum > 0 && param != null)
        {
            if (sellNum <= param.value2)
                BagService.Singleton.ReqSellItem(param.value3, sellNum);
            else
                Logger.err("ItemSellWindow:OnSellBtn:sellnum > itemInfo.num:" + sellNum + "_" + param.value2);
        }
        else
        {
            Logger.err("ItemSellWindow:OnSellBtn:" + sellNum);
        }
        Close();
    }

    private void OnMinBtnClick()
    {
        sellNum = 0;
        RefreshSellInfo();
    }

    private void OnMaxBtnClick()
    {
        sellNum = param.value2;
        RefreshSellInfo();
    }

    private void RefreshSellInfo()
    {
        window.m_popupView.m_slider.value = sellNum;
        window.m_popupView.m_sellNum.text = sellNum + "/" + param.value2;
        window.m_popupView.m_sellPrice.text = sellNum * bean.t_sell_price + "";
    }

    protected override void OnClose()
    {
        base.OnClose();
        bean = null;
        param = null;
    }

}
