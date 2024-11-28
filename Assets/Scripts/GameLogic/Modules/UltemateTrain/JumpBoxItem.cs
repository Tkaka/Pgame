using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_UltemateTrain;
using Message.Challenge;
using Data.Beans;

public class JumpBoxItem : UI_jumpBoxItem {

    public IntVsInt boxInfo;

    public new static JumpBoxItem CreateInstance()
    {
        return UI_jumpBoxItem.CreateInstance() as JumpBoxItem;
    }

    public void InitView()
    {
        m_toucher.onClick.Add(OnClickItem);
        RefreshView();
    }

    public void RefreshView()
    {
        string numStr = boxInfo.int1 >= 10 ? ("0" + boxInfo.int1) : (boxInfo.int1 + "");
        m_floorNumLabel.text = string.Format("{0}层", numStr);

        t_globalBean globalBean = ConfigBean.GetBean<t_globalBean, int>(1801002);
        if (globalBean != null)
        {
            m_remainTimesLabel.text = string.Format("以购买{0}/{1}", boxInfo.int2, globalBean.t_int_param);
        }
    }

    private void OnClickItem()
    {
        // 打开宝箱界面 
        if (IsHaveTimes())
        {
            WinMgr.Singleton.Open<SecretBoxWindow>(WinInfo.Create(false, null, true, boxInfo), UILayer.Popup);
        }
        else
        {
            TipWindow.Singleton.ShowTip("今日次数以用完!");
        }
    }

    private bool IsHaveTimes()
    {
        int maxTimes = UltemateTrainService.Singleton.GetSecretMaxOpenTimes();
        return boxInfo.int1 < maxTimes;
    }
}
