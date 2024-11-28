using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_UltemateTrain;

public class UltemateFloorTipView  {

    UI_ultemateFloorTipView view;
    private int floor;
    private float listHeight;
    private float floorTipHeight;

    public int Floor
    {
        get { return floor; }
        set
        {
            floor = value;
            RefreshView();
        }
    }
    public UltemateFloorTipView(UI_ultemateFloorTipView view)
    {
        this.view = view;
        listHeight = view.m_floorTipList.height;

        FloorTipItem item = FloorTipItem.CreateInstance();
        floorTipHeight = item.height;
        item.Dispose();
    }

    private void RefreshView()
    {
        RefreshList();
        SetPos();
    }

    private void SetPos()
    {
        view.m_floorTipList.y = GetPosY(floor);
    }

    private float GetPosY(int floor)
    {
        float posY = 0;
        if (floor <= 0 || floor > 60)
            return -1;

        if (floor == 1)
        {
            posY = view.m_firstFloorGroup.position.y - listHeight + view.m_firstFloorGroup.height;
            return posY;
        }
        if (floor == 2)
        {
            posY = GetPosY(1) - view.m_firstFloorGroup.height;
            return posY;
        }

        if (floor <= 56)
        {
            float perStep = (view.m_lastFloorGroup.y - view.m_firstFloorGroup.y) / (56 - 2);
            posY = GetPosY(2) + perStep * (floor - 2);
            return posY;
        }

        posY = GetPosY(56) - floorTipHeight * (floor - 56);
        return posY;
    }

    private void RefreshList()
    {
        view.m_floorTipList.RemoveChildren(0, -1, true);

        int count = 3;
        int remianFloor = 60 - floor - 1;
        count = remianFloor < count ? remianFloor : count;
        //先添加其他层的tip
        FloorTipItem tipItem;
        for (int i = 0; i < count; i++)
        {
            tipItem = FloorTipItem.CreateInstance();
            tipItem.floor = floor + i + 1;
            tipItem.InitView();

            view.m_floorTipList.AddChildAt(tipItem, 0);
        }

        // 添加玩家当前的层数
        UI_playerTip playerTip = UI_playerTip.CreateInstance();
        view.m_floorTipList.AddChild(playerTip);
    }
}
