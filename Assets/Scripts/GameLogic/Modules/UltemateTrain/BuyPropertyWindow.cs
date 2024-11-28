using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_UltemateTrain;
using Message.Challenge;

public class BuyPropertyWindow : BaseWindow {

    UI_BuyPropertyWindow window;
    TrialFloorAttr floorAttr;

    public override void OnOpen()
    {
        base.OnOpen();

        window = getUiWindow<UI_BuyPropertyWindow>();
        floorAttr = Info.param as TrialFloorAttr;
        window.m_backBtn.onClick.Add(OnBackBtnClick);

        RefreshView();
    }

    public override void InitView()
    {
        base.InitView();
    }

    public override void AddEventListener()
    {
        base.AddEventListener();

        GED.ED.addListener(EventID.OnResTrainFloor, OnResTrainFloor);
    }

    public override void RemoveEventListener()
    {
        base.RemoveEventListener();

        GED.ED.removeListener(EventID.OnResTrainFloor, OnResTrainFloor);
    }

    public override void RefreshView()
    {
        base.RefreshView();

        RefreshPropertyList();
        RefreshBaseInfo();
    }

    private void RefreshBaseInfo()
    {
        ResTrialInfo trainInfo = UltemateTrainService.Singleton.trainInfo;
        if (trainInfo != null)
        {
            window.m_remainStarNumLabel.text = trainInfo.trialInfo.star + "";
        }
    }

    private void RefreshPropertyList()
    {
        int count = window.m_propertyList.numItems;
        FloorPropertyItem propertyItem = null;
        TrialSingleAttr attr = null;
        if (count == 0)
        {
            count = floorAttr.attrs.Count;
            for (int i = 0; i < count; i++)
            {
                attr = floorAttr.attrs[i];
                propertyItem = FloorPropertyItem.CreateInstance();
                propertyItem.attr = attr;
                propertyItem.index = i;
                propertyItem.InitView();
                propertyItem.RefreshView();

                window.m_propertyList.AddChild(propertyItem);
            }
        }
        else
        {
            for (int i = 0; i < count; i++)
            {
                if (i < floorAttr.attrs.Count)
                {
                    propertyItem = window.m_propertyList.GetChildAt(i) as FloorPropertyItem;
                    propertyItem.attr = floorAttr.attrs[i];
                    propertyItem.RefreshView();
                }
            }
        }
    }

    private void OnResTrainFloor(GameEvent evt)
    {
        floorAttr = evt.Data as TrialFloorAttr;
        if(floorAttr != null)
            RefreshView();
    }

    private void OnBackBtnClick()
    {
        UltemateTrainService.Singleton.trainInfo.trialInfo.floor++;
        GED.ED.dispatchEvent(EventID.OnLeavePropertyFloor);
        OnCloseBtn();
    }

    protected override void OnCloseBtn()
    {
        base.OnCloseBtn();
    }
}
