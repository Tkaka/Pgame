using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Message.Role;
using Data.Beans;
using Message.Pet;

public class ZhanHunStrengthDataManager {

    /// <summary>
    /// 增加的经验值
    /// </summary>
    private int _addExp;
    /// <summary>
    /// 选择的材料的最大数量
    /// </summary>
    public int selectCaiLiaoNum;
    /// <summary>
    /// 使用材料的数量
    /// </summary>
    private int _useCaiLiaoNum;
    /// <summary>
    /// 消耗的金币数量
    /// </summary>
    private int _useGoldNum;
    private int _curSelectGridID = -1;
    private List<Message.Bag.GridInfo> _gridInfoList = new List<Message.Bag.GridInfo>();
    /// <summary>
    /// 选择的格子ID和数量
    /// </summary>
    private Dictionary<int, int> _selectGridInfoDict = new Dictionary<int, int>();

    public List<Message.Bag.GridInfo> GridInfoList
    {
        get { return _gridInfoList; }
        set { _gridInfoList = value; }
    }

    public int UseCaiLiaoNum
    {
        get { return _useCaiLiaoNum; }
        set
        {
            _useCaiLiaoNum = value;
            SetSelectItem();
        }
    }

    public int addExp
    {
        get { return _addExp; }
        set
        {
            _addExp = value;
            t_globalBean globalBean = ConfigBean.GetBean<t_globalBean, int>(90001);
            _useGoldNum = (int)(_addExp * globalBean.t_int_param);
        }
    }

    public int UseGoldNum
    {
        get { return _useGoldNum; }
    }

    public int curSelectGridID
    {
        get { return _curSelectGridID; }
        set
        {
            _curSelectGridID = value;
            _useCaiLiaoNum = GetSelectItemNum(_curSelectGridID);
            selectCaiLiaoNum = BagService.Singleton.GetGrid(_curSelectGridID).itemInfo.num;
        }
    }

    public Dictionary<int, int> SelectGridInfoDict
    {
        get { return _selectGridInfoDict; }
    }

    public ZhanHunStrengthDataManager()
    {
        RefreshData();
        
    }

    public void RefreshData()
    {
        //_gridInfoList = BagService.Singleton.GetGrids();
        _gridInfoList.Clear();
        _gridInfoList.AddRange(BagService.Singleton.BagInfo.grids);
        FilterGridInfo();
        _gridInfoList.Sort(SortMaterialList);
        _addExp = 0;
        _useCaiLiaoNum = 0;
        _useGoldNum = 0;
        _selectGridInfoDict.Clear();
        // 默认选择第一个
        SetDefaultItem();
    }
    private void FilterGridInfo()
    {
        int count = _gridInfoList.Count;
        for (int i = count - 1; i >= 0; i--)
        {
            if (_gridInfoList[i] == null || _gridInfoList[i].itemInfo.num == 0)
            {
                _gridInfoList.RemoveAt(i);
            }
        }
    }
    private int SortMaterialList(Message.Bag.GridInfo gridInfoA, Message.Bag.GridInfo gridInfoB)
    {
        if (gridInfoA == null)
            return -1;
        if (gridInfoB == null)
            return 1;

        t_itemBean itemBeanA = ConfigBean.GetBean<t_itemBean, int>(gridInfoA.itemInfo.id);
        t_itemBean itemBeanB = ConfigBean.GetBean<t_itemBean, int>(gridInfoB.itemInfo.id);

        if (itemBeanA == null)
            return -1;
        if (gridInfoB == null)
            return 1;
        //  战魂经验最前
        if (itemBeanA.t_type == (int)ItemType.ZhanHunExp)
            return -1;
        if (itemBeanB.t_type == (int)ItemType.ZhanHunExp)
            return 1;

        // 道具品质
        int qualityA = 0;
        int qualityB = 0;
        if (itemBeanA.t_type < 0)
        {
            qualityA = UIUtils.GetDaiBiQulity(gridInfoA.itemInfo.id, gridInfoA.itemInfo.num);
        }
        else
            qualityA = int.Parse(itemBeanA.t_quality);

        if (itemBeanB.t_type < 0)
        {
            qualityB = UIUtils.GetDaiBiQulity(gridInfoB.itemInfo.id, gridInfoB.itemInfo.num);
        }
        else
            qualityB = int.Parse(itemBeanB.t_quality);

        if(qualityA != qualityB)
            return -qualityA.CompareTo(qualityB);

        // id顺序
        return gridInfoA.itemInfo.id.CompareTo(gridInfoB.itemInfo.id);
    }

    public void SetDefaultItem()
    {
        if (_gridInfoList == null || _gridInfoList.Count == 0)
            return;

        selectCaiLiaoNum = _gridInfoList[0].itemInfo.num;
        curSelectGridID = _gridInfoList[0].id;
    }

    public bool IsEnoughGold()
    {
        RoleInfo role = RoleService.Singleton.GetRoleInfo();
        return role.gold >= _useGoldNum;
    }

    public bool IsSelectItem(int gridID)
    {
        if (_selectGridInfoDict.ContainsKey(gridID))
        {
            return true;
        }

        return false;
    }

    public void SetSelectItem()
    {
        if (_selectGridInfoDict.ContainsKey(_curSelectGridID))
        {
            _selectGridInfoDict[_curSelectGridID] = _useCaiLiaoNum;
        }
        else
        {
            _selectGridInfoDict.Add(_curSelectGridID, _useCaiLiaoNum);
        }
        if (_useCaiLiaoNum == 0)
        {
            _selectGridInfoDict.Remove(_curSelectGridID);
        }
    }

    public int GetSelectItemNum(int gridID)
    {
        int num = 0;
        if (_selectGridInfoDict.ContainsKey(gridID))
        {
            num = _selectGridInfoDict[gridID];
        }
        return num;
    }

    public void ReqZhanHunStrength(int petID, int index)
    {
        PetService.Singleton.ReqZhanHunStrength(petID, index, _selectGridInfoDict);
    }

}
