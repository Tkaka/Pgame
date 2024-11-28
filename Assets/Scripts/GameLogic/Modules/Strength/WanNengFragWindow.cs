using UI_Strength;
using UI_Common;
using Data.Beans;
using UnityEngine;
using Message.Bag;

public class WanNengFragWindow : BaseWindow
{

    private UI_WanNengFragWindow window;

    /// <summary>
    /// 第一个碎片ID， 第二个当前升星需要的数量, 第三个当前选择的宠物ID
    /// </summary>
    private ThreeParam<int, int, int> threeParam;   

    public override void OnOpen()
    {
        base.OnOpen();
        threeParam = Info.param as ThreeParam<int, int, int>;
        InitView();
    }

    private UI_ItemIcon wnIcon;
    private UI_ItemIcon targetIcon;
    public override void InitView()
    {
        base.InitView();
        window = getUiWindow<UI_WanNengFragWindow>();
        window.m_popupView.m_transBtn.onClick.Add(OnTransBtn);
        window.m_popupView.m_slider.onChanged.Add(OnSidlerValueChange);
        window.m_popupView.m_slider.onGripTouchEnd.Add(OnSliderTouchEnd);
        window.m_popupView.m_slider.changeOnClick = false;
        window.m_popupView.m_closeBtn.onClick.Add(OnCloseBtn);
        window.m_popupView.m_reduceBtn.onClick.Add(OnReduceBtnClick);
        window.m_popupView.m_addBtn.onClick.Add(OnAddBtnClick);
        window.m_mask.onClick.Add(OnCloseBtn);
        wnIcon = (UI_ItemIcon)window.m_popupView.m_wnIcon;
        targetIcon = (UI_ItemIcon)window.m_popupView.m_targetIcon;
        RefreshView();
        OnSidlerValueChange();
    }

    private int wnFragCount = 0;
    private int curFragCount = 0;
    private GridInfo wnGrid;
    public override void RefreshView()
    {
        base.RefreshView();
        t_globalBean globalBean = ConfigBean.GetBean<t_globalBean, int>(9999);
        if (globalBean != null)
        {
            t_itemBean itemBean = ConfigBean.GetBean<t_itemBean, int>(globalBean.t_int_param);
            if (itemBean != null)
            {
                UIGloader.SetUrl(wnIcon.m_iconLoader, itemBean.t_icon);
                UIGloader.SetUrl(wnIcon.m_borderLoader, UIUtils.GetItemBorder(itemBean.t_id));
                wnGrid = BagService.Singleton.GetGridInfo(itemBean.t_id);
                if(wnGrid != null)
                    wnFragCount = wnGrid.itemInfo.num;
                wnIcon.m_numTxt.text = 0 + "/" + wnFragCount;
                wnIcon.m_fragIcon.visible = false;
            }
            itemBean = ConfigBean.GetBean<t_itemBean, int>(threeParam.value1);
            if (itemBean != null)
            {
                UIGloader.SetUrl(targetIcon.m_iconLoader, itemBean.t_icon);
                UIGloader.SetUrl(targetIcon.m_borderLoader, UIUtils.GetItemBorder(itemBean.t_id));
                curFragCount = BagService.Singleton.GetItemNum(itemBean.t_id);
                targetIcon.m_numTxt.text = curFragCount + "";
                targetIcon.m_fragIcon.visible = true;
            }

            window.m_popupView.m_slider.touchable = wnFragCount > 0;
            window.m_popupView.m_slider.value = 0;
            window.m_popupView.m_slider.max = wnFragCount;
        }
    }

    private int transNum;
    private void OnSidlerValueChange()
    {
        if (transNum >= threeParam.value2)
        {
            TipWindow.Singleton.ShowTip("碎片已满");
            return;
        }
        transNum = Mathf.FloorToInt((float)window.m_popupView.m_slider.value);
        transNum = Mathf.Max(0, transNum);
        wnIcon.m_numTxt.text = transNum + "/" + wnFragCount;
    }

    private void OnSliderTouchEnd()
    {
        window.m_popupView.m_slider.value = transNum;
    }

    private void OnReduceBtnClick()
    {

        if (wnFragCount <= 0)
        {
            TipWindow.Singleton.ShowTip("没有万能碎片");
            return;
        }

        if (transNum <= 0)
        {
            TipWindow.Singleton.ShowTip("转换数量为0");
            return;
        }

        transNum--;
        window.m_popupView.m_slider.value = transNum;
        wnIcon.m_numTxt.text = transNum + "/" + wnFragCount;
    }

    private void OnAddBtnClick()
    {
        if (wnFragCount <= 0)
        {
            TipWindow.Singleton.ShowTip("没有万能碎片");
            return;
        }

        if (transNum >= threeParam.value2)
        {
            TipWindow.Singleton.ShowTip("碎片已满");
            return;
        }

        if (transNum >= wnFragCount)
        {
            TipWindow.Singleton.ShowTip("没有多余的万能碎片");
            return;
        }

        transNum++;
        window.m_popupView.m_slider.value = transNum;
        wnIcon.m_numTxt.text = transNum + "/" + wnFragCount;

    }

    private void OnTransBtn()
    {
        if (wnFragCount <= 0)
        {
            TipWindow.Singleton.ShowTip("你还没有万能碎片哦");
            return;
        }
        if (wnGrid != null)
        {
            if (transNum <= 0)
            {
                TipWindow.Singleton.ShowTip("转换数量为0");
            }
            else
            {
                //BagService.Singleton.ReqItemUse(wnGrid.id, transNum, twoParam.value1 + "");
                PetService.Singleton.ReqWNFragExchange(threeParam.value3, transNum);
                Close();
            }
        }
    }

}
