
using UI_DailyActivity;
using UnityEngine;

public class WndDuiHuanNum : BaseWindow
{
    UI_WndDuiHuanNum window;
    private int maxNum;
    private int subActId;
    
    public override void OnOpen()
    {
        base.OnOpen();
        window = getUiWindow<UI_WndDuiHuanNum>();
    }

    public override void InitView()
    {
        base.InitView();
        window.m_popupView.m_closeBtn.onClick.Add(OnCloseBtn);
        window.m_popupView.m_sellBtn.onClick.Add(OnSellBtn);
        window.m_popupView.m_btnMax.onClick.Add(OnMaxBtn);
        window.m_popupView.m_btnMin.onClick.Add(OnMinBtn);
        window.m_popupView.m_slider.onChanged.Add(OnSidlerValueChange);
        window.m_popupView.m_slider.value = 0;

        var param = Info.param as TwoParam<int, int>;
        subActId = param.value1;
        maxNum = param.value2;

        var ab = ConfigBean.GetBean<Data.Beans.t_normal_activity_itemBean, int>(param.value1);
        if(ab == null)
        {
            Close();
            return;
        }

        var arr = ab.t_reward.Split('+');
        int itemId = int.Parse(arr[0]);
        int itemNum = int.Parse(arr[1]);

        OnSidlerValueChange();

        var bean = ConfigBean.GetBean<Data.Beans.t_itemBean, int>(itemId);
        if (bean != null)
        {
            CommonItem item = CommonItem.CreateInstance();
            item.Init(itemId, itemNum, true);
            window.m_popupView.AddChild(item);
            item.SetXY(window.m_popupView.m_place.x, window.m_popupView.m_place.y);

            window.m_popupView.m_itemName.text = bean.t_name;
            window.m_popupView.m_itemName.color = UIUtils.GetItemColor(bean.t_id);
        }
    }

    private int curNum;
    private void OnSidlerValueChange()
    {
        curNum = Mathf.CeilToInt((float)window.m_popupView.m_slider.value * maxNum * 0.01f);
        curNum = Mathf.Max(1, curNum);
        window.m_popupView.m_sellNum.text = curNum + "/" + maxNum;
    }

    private void OnSellBtn()
    {
        DailyActivityService.Singleton.ReqTaskRewards(subActId, curNum);
        Close();
    }

    private void OnMaxBtn()
    {
        curNum = maxNum;
        window.m_popupView.m_slider.value = 100;
        window.m_popupView.m_sellNum.text = curNum + "/" + maxNum;

    }

    private void OnMinBtn()
    {
        curNum = 1;
        window.m_popupView.m_slider.value = 1;
        window.m_popupView.m_sellNum.text = curNum + "/" + maxNum;
    }
}