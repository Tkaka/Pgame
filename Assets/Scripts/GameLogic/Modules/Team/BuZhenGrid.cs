using System.Collections.Generic;
using UI_BuZhen;
using UnityEngine;


public class BuZhenHolderWrapper
{
    public int index;
    public int ChongWuID;
    public UI_BuZhenHolder holder;
    public bool hasWrapper;
    public BuZhenHolderWrapper(int index, UI_BuZhenHolder holder)
    {
        this.index = index;
        this.holder = holder;
    }
}

public class BuZhenGrid
{
    private UI_BuZhenWindow window;

    private Vector3[] posArr = new Vector3[6];

    private Dictionary<UI_BuZhenHolder, BuZhenHolderWrapper> holderDic = new Dictionary<UI_BuZhenHolder, BuZhenHolderWrapper>();

    public Dictionary<UI_BuZhenHolder, BuZhenHolderWrapper> HolderDic
    {
        get { return holderDic; }
    }

    public Vector3[] PosArr
    {
        get { return posArr; }
    }

    private Vector2[][] grid = new Vector2[6][] { new Vector2[4], new Vector2[4], new Vector2[4], new Vector2[4], new Vector2[4], new Vector2[4] };

    public BuZhenGrid(UI_BuZhenWindow view)
    {
        window = view;
        InitGrid();
    }

    private void InitGrid()
    {
        //坐标位置
        posArr[0] = window.m_pos0.position;
        posArr[1] = window.m_pos1.position;
        posArr[2] = window.m_pos2.position;
        posArr[3] = window.m_pos3.position;
        posArr[4] = window.m_pos4.position;
        posArr[5] = window.m_pos5.position;

        holderDic.Add(window.m_pos0, new BuZhenHolderWrapper(0, window.m_pos0));
        holderDic.Add(window.m_pos1, new BuZhenHolderWrapper(1, window.m_pos1));
        holderDic.Add(window.m_pos2, new BuZhenHolderWrapper(2, window.m_pos2));
        holderDic.Add(window.m_pos3, new BuZhenHolderWrapper(3, window.m_pos3));
        holderDic.Add(window.m_pos4, new BuZhenHolderWrapper(4, window.m_pos4));
        holderDic.Add(window.m_pos5, new BuZhenHolderWrapper(5, window.m_pos5));

        //坐标0位置
        grid[0][0] = window.m_grid01.position;
        grid[0][1] = window.m_grid02.position;
        grid[0][2] = window.m_grid12.position;
        grid[0][3] = window.m_grid11.position;

        //坐标1位置
        grid[1][0] = window.m_grid11.position;
        grid[1][1] = window.m_grid12.position;
        grid[1][2] = window.m_grid22.position;
        grid[1][3] = window.m_grid21.position;

        //坐标2位置
        grid[2][0] = window.m_grid21.position;
        grid[2][1] = window.m_grid22.position;
        grid[2][2] = window.m_grid32.position;
        grid[2][3] = window.m_grid31.position;

        //坐标3位置
        grid[3][0] = window.m_grid00.position;
        grid[3][1] = window.m_grid01.position;
        grid[3][2] = window.m_grid11.position;
        grid[3][3] = window.m_grid10.position;

        //坐标4位置
        grid[4][0] = window.m_grid10.position;
        grid[4][1] = window.m_grid11.position;
        grid[4][2] = window.m_grid21.position;
        grid[4][3] = window.m_grid20.position;

        //坐标5位置
        grid[5][0] = window.m_grid20.position;
        grid[5][1] = window.m_grid21.position;
        grid[5][2] = window.m_grid31.position;
        grid[5][3] = window.m_grid30.position;
    }

    public void SwapHolderPos(UI_BuZhenHolder holder1, UI_BuZhenHolder holder2)
    {
        if (!holderDic.ContainsKey(holder1) || !holderDic.ContainsKey(holder1))
        {
            Logger.err("BuZhenGrid:SwapHolderPos:param is error");
            return;
        }
        int temp = holderDic[holder1].index;
        holderDic[holder1].index = holderDic[holder2].index;
        holderDic[holder2].index = temp;
    }

    public int IsInGrid(Vector2 pos, int excludeId=-1)
    {
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            if (i == excludeId)
                continue;
            if (GTools.IsInPolygon(pos, grid[i]))
            {
                return i;
            }
        }
        return -1;
    }

    public int GetHolderID(UI_BuZhenHolder holder)
    {
        if (holderDic.ContainsKey(holder))
        {
            return holderDic[holder].index;
        }
        return -1;
    }

    public BuZhenHolderWrapper GetHolderWrapper(UI_BuZhenHolder holder)
    {
        if (holderDic.ContainsKey(holder))
        {
            return holderDic[holder];
        }
        return null;
    }

    public BuZhenHolderWrapper GetWrapperById(int id)
    {
        foreach (KeyValuePair<UI_BuZhenHolder, BuZhenHolderWrapper> keyPair in holderDic)
        {
            if (keyPair.Value.index == id)
            {
                return keyPair.Value;
            }
        }
        return null;
    }

}
