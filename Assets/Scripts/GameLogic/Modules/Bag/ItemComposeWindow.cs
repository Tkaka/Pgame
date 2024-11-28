using Data.Beans;
using Message.Bag;
using UI_Beibao;
using UnityEngine;
using FairyGUI;

public class ItemComposeWindow : BaseWindow
{

    private UI_ItemComposeWindow window;
    private int m_maxNum = 1; //最大数量

    //private GridInfo gridInfo = null;
    private int m_itemId;

    public override void OnOpen()
    {
        base.OnOpen();
        window = getUiWindow<UI_ItemComposeWindow>();

        TwoParam<int, int> param = Info.param as TwoParam<int, int>;
        if (param == null)
        { 
            Close();
            return;
        }

        m_itemId = param.value1;
        m_maxNum = param.value2;

        window.m_mask.onClick.Add(OnCloseBtn);
        if (m_itemId != 0)
        {
            InitView();
            PlayPopupAnim(window.m_mask, window.m_popupView);
        }
        else
        {
            Logger.err("ItemSellWindow:OnOpen:can not find GridInfo : " + m_itemId);
        }
    }

    public override void InitView()
    {
        base.InitView();
        if (m_itemId != 0)
        {
            window.m_popupView.m_closeBtn.onClick.Add(OnCloseBtn);
            window.m_popupView.m_btnCompose.onClick.Add(OnSellBtn);
            window.m_popupView.m_btnMax.onClick.Add(OnMaxBtn);
            window.m_popupView.m_btnMin.onClick.Add(OnMinBtn);
            window.m_popupView.m_slider.onChanged.Add(OnSidlerValueChange);
            window.m_popupView.m_slider.value = 0;
            OnSidlerValueChange();
            CommonItem commonItem = window.m_popupView.m_icon as CommonItem;
            if (commonItem != null)
            {
                commonItem.itemId = m_itemId;
                commonItem.isShowNum = false;
                commonItem.RefreshView();
            }

            window.m_popupView.m_itemName.text = UIUtils.GetItemName(m_itemId);
            string strDes = "";
            t_itemBean itemBean = ConfigBean.GetBean<t_itemBean, int>(m_itemId);
            if (itemBean != null)
            {
                string[] needIteInfos = GTools.splitString(itemBean.t_compose_arg, ';');
                if (needIteInfos != null && needIteInfos.Length > 0)
                {
                    //取第一个
                    int[] info = GTools.splitStringToIntArray(needIteInfos[0], '+');
                    if (info != null && info.Length >= 2)
                    {
                        strDes = string.Format("{0}个{1}合成{2}", info[1], UIUtils.GetItemName(info[0]), UIUtils.GetItemName(m_itemId));
                        window.m_popupView.m_txtDes.text = strDes;
                    }

                }
            }
            //if (bean != null)
            //{
            //    if (!string.IsNullOrEmpty(bean.t_quality))
            //        UIGloader.SetUrl(window.m_icon.m_borderLoader,UIUtils.GetLoaderUrl(WinEnum.UI_Common, UIUtils.GetItemBorderByQuality(int.Parse(bean.t_quality))));
            //    UIGloader.SetUrl(window.m_icon.m_iconLoader, bean.t_icon);
            //    window.m_icon.m_numTxt.visible = false;
            //    window.m_itemName.text = bean.t_name;
            //}
        }
    }

    private int sellNum;
    private void OnSidlerValueChange()
    {
        //Logger.log(window.m_slider.value +"");
        if (m_itemId != 0)
        {
            //int sellNum = (int)(window.m_slider.value * itemInfo.num * 0.01f);
            sellNum = Mathf.CeilToInt((float)window.m_popupView.m_slider.value * m_maxNum * 0.01f);
            sellNum = Mathf.Max(1, sellNum);
            window.m_popupView.m_sellNum.text = sellNum + "/" + m_maxNum;
        }
    }

    private void OnSellBtn()
    {
        if (sellNum > 0 && m_itemId != 0)
        {
            if (sellNum <= m_maxNum)
                BagService.Singleton.ReqItemCompose(m_itemId, sellNum);
            else
                Logger.err("ItemSellWindow:OnSellBtn:sellnum > itemInfo.num:" + sellNum + "_" + m_maxNum);
        }
        else
        {
            Logger.err("ItemSellWindow:OnSellBtn:" + sellNum + "_");
        }
        Close();
    }


    private void OnMaxBtn()
    {
        sellNum = m_maxNum;
        window.m_popupView.m_slider.value = 100;
        window.m_popupView.m_sellNum.text = sellNum + "/" + m_maxNum;

    }

    private void OnMinBtn()
    {
        sellNum = 1;
        window.m_popupView.m_slider.value = 1;
        window.m_popupView.m_sellNum.text = sellNum + "/" + m_maxNum;
    }


    protected override void OnClose()
    {
        base.OnClose();
  
    }
}
