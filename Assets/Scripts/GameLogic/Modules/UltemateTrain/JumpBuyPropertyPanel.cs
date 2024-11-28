using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_UltemateTrain;
using Message.Challenge;

public class JumpBuyPropertyPanel : TabPage {

    UI_jumpBuyPropertyPanel panel;
    /// <summary>
    /// 当前购买的层数下标
    /// </summary>
    private int curPropertyIndex;
    private AdditionListPanel additionListPanel;
    TrialFloorAttr trainFloorAttr = null;
    public int CurPropertyIndex
    {
        set
        {
            curPropertyIndex = value;
            ResTrialSkip trainSkipInfo = UltemateTrainService.Singleton.trainSkipInfo;
            if (trainSkipInfo != null)
            {
                trainFloorAttr = trainSkipInfo.attrs[curPropertyIndex];
            }
            RefreshView();
        }
    }

    public JumpBuyPropertyPanel(UI_jumpBuyPropertyPanel panel)
    {
        this.panel = panel;

        panel.m_additionLoader.onTouchBegin.Add(OnAdditionLoaderTouchBegin);
        panel.m_additionLoader.onTouchEnd.Add(OnAdditionLoaderTouchEnd);
        panel.m_additionLoader.onRollOut.Add(OnAdditionLoaderTouchEnd);

        InitView();
    }

    private void AddListener()
    {
        GED.ED.addListener(EventID.OnResTrainFloor, OnResTrainFloor);
    }

    private void RemoveListener()
    {
        GED.ED.removeListener(EventID.OnResTrainFloor, OnResTrainFloor);
    }

    private void InitView()
    {
        additionListPanel = new AdditionListPanel(panel.m_additionListPanel);
        panel.m_additionListPanel.visible = false;
    }

    private void OnAdditionLoaderTouchBegin()
    {
        panel.m_additionListPanel.visible = true;
    }

    private void OnAdditionLoaderTouchEnd()
    {
        panel.m_additionListPanel.visible = false;
    }

    private void OnResTrainFloor(GameEvent evt)
    {
        trainFloorAttr = evt.Data as TrialFloorAttr;
        if(trainFloorAttr != null)
             RefreshView();
    }

    public override void OnClose()
    {
        RemoveListener();
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
    }

    public override void RefreshView(bool isNet = false)
    {
        RefreshBaseInfo();
        RefreshPropertyList();
        RefreshAdditionPanel();
    }

    private void RefreshPropertyList()
    {
        if (trainFloorAttr != null)
        {
            int count = panel.m_propertyList.numChildren;
            FloorPropertyItem floorPropertyItem = null;
            if (count == 0)
            {
                count = trainFloorAttr.attrs.Count;
                for (int i = 0; i < count; i++)
                {
                    floorPropertyItem = FloorPropertyItem.CreateInstance();
                    floorPropertyItem.attr = trainFloorAttr.attrs[i];
                    floorPropertyItem.index = i;
                    floorPropertyItem.InitView();
                    floorPropertyItem.RefreshView();
                    panel.m_propertyList.AddChild(floorPropertyItem);
                }
            }
            else
            {
                for (int i = 0; i < count; i++)
                {
                    floorPropertyItem = panel.m_propertyList.GetChildAt(i) as FloorPropertyItem;
                    if (i < trainFloorAttr.attrs.Count)
                        floorPropertyItem.attr = trainFloorAttr.attrs[i];
                    floorPropertyItem.RefreshView();
                }
            }
        }
    }

    private void RefreshBaseInfo()
    {
        ResTrialInfo trialInfo = UltemateTrainService.Singleton.trainInfo;
        if (trialInfo != null)
        {
            panel.m_remainStarNumLabel.text = trialInfo.trialInfo.star + "";
        }

        ResTrialSkip trainSkipInfo = UltemateTrainService.Singleton.trainSkipInfo;
        if (trainSkipInfo != null)
        {
            TrialFloorAttr floorAttr = trainSkipInfo.attrs[curPropertyIndex];
            panel.m_floorNumLabel.text = string.Format("{0}层剩余", floorAttr.attrs[0].floor);
        }
    }

    private void RefreshAdditionPanel()
    {
        ResTrialInfo trainInfo = UltemateTrainService.Singleton.trainInfo;
        if (trainInfo != null)
            additionListPanel.PropertyList = trainInfo.buffs;
    }

}
