using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_UltemateTrain;
using Message.Challenge;
using Message.Bag;

public class JumpBuyBoxPanel : TabPage {

    UI_jumpBuyBoxPanel panel;
    List<BuyTimesGroup> buyTimesGroupList = new List<BuyTimesGroup>();

    public JumpBuyBoxPanel(UI_jumpBuyBoxPanel panel)
    {
        this.panel = panel;
        InitView();
    }

    private void InitView()
    {
        int count = panel.m_buyTimesList.numChildren;
        BuyTimesGroup timesGroup = null;
        for (int i = 0; i < count; i++)
        {
            timesGroup = new BuyTimesGroup(panel.m_buyTimesList.GetChildAt(i) as UI_buyTimesGroup);
            switch (i)
            {
                case 0:
                    timesGroup.buyTimes = 1;
                    break;
                case 1:
                    timesGroup.buyTimes = 2;
                    break;
                case 2:
                    timesGroup.buyTimes = 5;
                    break;
                case 3:
                    timesGroup.buyTimes = 10;
                    break;
                default:
                    break;
            }
            buyTimesGroupList.Add(timesGroup);
        }
    }

    private void AddListener()
    {
        GED.ED.addListener(EventID.OnResTrialBatchBoxOpen, OnResTrialBoxOpen);
    }

    private void RemoveListener()
    {
        GED.ED.removeListener(EventID.OnResTrialBatchBoxOpen, OnResTrialBoxOpen);
    }

    public override void OnClose()
    {
        RemoveListener();
        buyTimesGroupList.Clear();
        buyTimesGroupList = null;
        panel = null;
    }

    public override void OnHide()
    {
        RemoveListener();
        panel.visible = false;
    }

    public override void OnShow()
    {
        panel.visible = true;
        AddListener();
        RefreshView();
    }

    public override void RefreshView(bool isNet = false)
    {
        RefreshBtnList();
        RefreshBoxList();
    }

    private void RefreshBtnList()
    {
        int count = buyTimesGroupList.Count;
        BuyTimesGroup timesGroup = null;
        for (int i = 0; i <  count; i++)
        {
            timesGroup = buyTimesGroupList[i];
            timesGroup.RefreshView();
        }
    }

    private void RefreshBoxList()
    {
        int count = panel.m_jumpBoxList.numChildren;
        JumpBoxItem boxItem = null;
        if (count == 0)
        {
            ResTrialSkip trainSkipInfo = UltemateTrainService.Singleton.trainSkipInfo;
            if (trainSkipInfo != null)
            {
                count = trainSkipInfo.diamondBoxInfos.Count;
                for (int i = 0; i < count; i++)
                {
                    boxItem = JumpBoxItem.CreateInstance();
                    boxItem.boxInfo = trainSkipInfo.diamondBoxInfos[i];
                    boxItem.InitView();
                    panel.m_jumpBoxList.AddChild(boxItem);
                }
            }
        }
        else
        {
            for (int i = 0; i < count; i++)
            {
                boxItem = panel.m_jumpBoxList.GetChildAt(i) as JumpBoxItem;
                boxItem.RefreshView();
            }
        }
    }

    private void OnResTrialBoxOpen(GameEvent evt)
    {
        // 打开宝箱开启界面
        ResTrialBatchBoxOpen msg = evt.Data as ResTrialBatchBoxOpen;
        List<ItemInfo> itemList = UltemateTrainService.Singleton.TransformIntVsIntToItemInfo(msg.rewards);
        ThreeParam<bool, List<ItemInfo>, string> param = new ThreeParam<bool, List<ItemInfo>, string>();
        param.value1 = false;
        param.value2 = itemList;
        WinMgr.Singleton.Open<BoxReceiveWidow>(WinInfo.Create(false, null, true, param), UILayer.Popup);

        RefreshView();
    }
}
