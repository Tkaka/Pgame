
using Data.Beans;
using Message.Bag;
using System.Collections.Generic;

public class BagService : SingletonService<BagService>
{
    //背包数据
    public ResBagInfo BagInfo { private set; get; }

    private Dictionary<int, GridInfo> gridDic = new Dictionary<int, GridInfo>();

    public BagService()
    {
        if (GameManager.Singleton.IsDebug)
            InitTestData();
    }

    protected override void RegisterEventListener()
    {
        base.RegisterEventListener();
        GED.NED.addListener(ResBagInfo.MsgId, OnResBagInfo);
        GED.NED.addListener(ResItemSell.MsgId, OnResItemSell);
        GED.NED.addListener(ResBoxItemUse.MsgId, OnResBoxItemUse);
        GED.NED.addListener(ResBagUpdate.MsgId, OnResBagUpdate);
    }

    protected override void UnRegisterEventListener()
    {
        base.UnRegisterEventListener();
        GED.NED.removeListener(ResBagInfo.MsgId, OnResBagInfo);
        GED.NED.removeListener(ResItemSell.MsgId, OnResItemSell);
        GED.NED.removeListener(ResBoxItemUse.MsgId, OnResBoxItemUse);
        GED.NED.removeListener(ResBagUpdate.MsgId, OnResBagUpdate);
    }

    private void OnResBagInfo(GameEvent evt)
    {
        BagInfo = GetCurMsg<ResBagInfo>(evt.EventId);
        if (BagInfo != null && BagInfo.grids != null)
        {
            foreach (GridInfo grid in BagInfo.grids)
            {
                if (gridDic.ContainsKey(grid.id))
                    gridDic[grid.id] = grid;
                else
                    gridDic.Add(grid.id, grid);
            }
        }
    }

    private void OnResBagUpdate(GameEvent evt)
    {
        ResBagUpdate msg = GetCurMsg<ResBagUpdate>(evt.EventId);
        if (msg != null && msg.grids != null)
        {
            foreach (GridInfo grid in msg.grids)
            {
                if (gridDic.ContainsKey(grid.id))
                {
                    gridDic[grid.id].itemInfo.id = grid.itemInfo.id;
                    gridDic[grid.id].itemInfo.num = grid.itemInfo.num;
                }
                else
                {
                    BagInfo.grids.Add(grid);
                    gridDic.Add(grid.id, grid);
                }
            }
            GED.ED.dispatchEvent(EventID.ResBagUpdate, msg);
            GED.ED.dispatchEvent(EventID.ResBoxItemUse, msg);
        }
    }

    /// <summary>
    /// TODO:分堆处理
    /// </summary>
    /// <param name="isSort"></param>
    /// <returns></returns>
    public List<GridInfo> GetGrids(bool isSort=true)
    {
        List<GridInfo> res = new List<GridInfo>();
        if (BagInfo != null && BagInfo.grids != null)
        {

            t_itemBean bean = null;
            foreach (GridInfo info in BagInfo.grids)
            {
                if (info.itemInfo.num <= 0)
                    continue;
                bean = ConfigBean.GetBean<t_itemBean, int>(info.itemInfo.id);
                if (bean != null)
                {
                    SeparateHeap(info, bean, ref res);
                }
            }
            if (isSort)
            {
                res.Sort(SortAll);
            }
        }
        return res;
    }

    public GridInfo GetGrid(int id)
    {
        if (gridDic.ContainsKey(id))
        {
            return gridDic[id];
        }
        Logger.log("BagService:GetGrid:can not find grid:" + id);
        return null;
    }

    public List<GridInfo> GetGridsByCategory(ItemCategory category, bool isSort=true)
    {
        List<GridInfo> res = new List<GridInfo>();
        if (BagInfo != null && BagInfo.grids != null)
        {
            t_itemBean bean = null;
            foreach (GridInfo info in BagInfo.grids)
            {
                if (info.itemInfo.num <= 0)
                    continue;
                bean = ConfigBean.GetBean<t_itemBean, int>(info.itemInfo.id);
                if (bean.t_tab == (int)category)
                {
                    //res.Add(info);
                    SeparateHeap(info, bean, ref res);
                }
            }
            if (isSort)
            {
                res.Sort(SortCommon);
            }
        }
        return res;
    }

    /// <summary>
    /// 根据最大叠加数量分堆
    /// </summary>
    /// <param name="grid"></param>
    /// <param name="bean"></param>
    /// <param name="cont"></param>
    private void SeparateHeap(GridInfo grid, t_itemBean bean,  ref List<GridInfo> cont)
    {
        int max = bean.t_max;
        if (max <= 0)
        {
            max = 1;
            Logger.err("BagService:SeparateHeap:t_item.t_max is zero: " + bean.t_max);
            //return;
        }

        if (grid.itemInfo.num > 0)
        {
            if (grid.itemInfo.num > max)
            {
                int heapNum = 0;
                int leftNum = grid.itemInfo.num % max;
                if (leftNum == 0)
                    heapNum = grid.itemInfo.num / max;
                else
                    heapNum = grid.itemInfo.num / max + 1;
                for (int i = 0; i < heapNum; i++)
                {
                    GridInfo info = new GridInfo();
                    info.id = grid.id;
                    info.itemInfo = new ItemInfo();
                    info.itemInfo.id = grid.itemInfo.id;
                    if (leftNum > 0 && (i == heapNum - 1))
                        info.itemInfo.num = leftNum;
                    else
                        info.itemInfo.num = max;
                    cont.Add(info);
                }
            }
            else
            {
                cont.Add(grid);
            }
        }
    }

    /// <summary>
    /// 批量通过itemID获得item的数量
    /// </summary>
    /// <param name="itemDict">item的id做key，GridInfo做Value的字典</param>
    public void SetGridInfoByIDs(Dictionary<int, GridInfo> itemDict)
    {
        GridInfo gridInfo = null;

        List<int> itemIDList = new List<int>(itemDict.Keys);
        int count = itemDict.Count;
        int id = 0;

        for (int i = 0; i < count; i++)
        {
            id = itemIDList[i];
            gridInfo = GetGridInfoByID(id);
            itemDict[id] = gridInfo;
        }
    }
    /// <summary>
    /// 通过道具id获得背包中的格子信息(没分堆处理)
    /// </summary>
    /// <returns></returns>
    public GridInfo GetGridInfoByID(int itemID)
    {
        GridInfo gridInfo = null;
        for (int i = 0; i < BagInfo.grids.Count; i++)
        {
            gridInfo = BagInfo.grids[i];
            if (gridInfo.itemInfo.id == itemID)
            {
                return gridInfo;
            }
        }

        return null;
    }

    public void ReqItemUse(int gridId, int num, string arg="")
    {
        ReqItemUse msg = GetEmptyMsg<ReqItemUse>();
        msg.gridId = gridId;
        msg.num = num;
        msg.arg = arg;
        SendMsg(ref msg);
    }

    public void ReqSellItem(int gridId, int sellNum)
    {
        ReqItemSell msg = GetEmptyMsg<ReqItemSell>();
        SellInfo sellInfo = new SellInfo();
        sellInfo.gridId = gridId;
        sellInfo.num = sellNum;
        msg.sellInfo.Add(sellInfo);
        SendMsg(ref msg);
    }

    public void ReqSellItem(List<SellInfo> sellInfos)
    {
        ReqItemSell msg = GetEmptyMsg<ReqItemSell>();
        msg.sellInfo.AddRange(sellInfos);
        SendMsg(ref msg);
    }

    private void OnResItemSell(GameEvent evt)
    {
        ResItemSell msg = GetCurMsg<ResItemSell>(evt.EventId);
        GED.ED.dispatchEvent(EventID.ResItemSell, msg);
        TipWindow.Singleton.ShowTip("获得金币:" + msg.gold);
    }

    private void OnResBoxItemUse(GameEvent evt)
    {
        ResBoxItemUse msg = GetCurMsg<ResBoxItemUse>(evt.EventId);
        GED.ED.dispatchEvent(EventID.ResBoxItemUse, msg);
        ThreeParam<bool, List<Message.Bag.ItemInfo>, string> param = new ThreeParam<bool, List<Message.Bag.ItemInfo>, string>();
        param.value1 = false;
        param.value2 = msg.items;
        WinMgr.Singleton.Open<BoxReceiveWidow>(WinInfo.Create(false, null, false, param), UILayer.Popup);

        //WinMgr.Singleton.Open<ItemGetWindow>(WinInfo.Create(false, null, false, msg), UILayer.Popup);
        //OpenChild<ItemGetWindow>(WinInfo.Create(false, winName, false, msg));
    }

    /// <summary>
    /// 根据道具id获取数量 
    /// </summary>
    /// <param name="itemId"></param>
    /// <returns></returns>
    public int GetItemNum(int itemId)
    {
        if (BagInfo != null && BagInfo.grids != null)
        {
            foreach (GridInfo grid in BagInfo.grids)
            {
                if (grid.itemInfo.id == itemId)
                    return grid.itemInfo.num;
            }
        }
        return 0;
    }

    /// <summary>
    /// 根据道具id获取格子信息
    /// </summary>
    /// <param name="itemId"></param>
    /// <returns></returns>
    public GridInfo GetGridInfo(int itemId)
    {
        if (BagInfo != null && BagInfo.grids != null)
        {
            foreach (GridInfo grid in BagInfo.grids)
            {
                if (grid.itemInfo.id == itemId)
                    return grid;
            }
        }
        return null;
    }
    

    private int SortAll(GridInfo info1, GridInfo info2)
    {
        int res1 = 0;
        int res2 = 0;

        if (info1.itemInfo.num <= 0)
            return -1;
        if (info2.itemInfo.num <= 0)
            return 1;

        t_itemBean bean1 = ConfigBean.GetBean<t_itemBean, int>(info1.itemInfo.id);
        t_itemBean bean2 = ConfigBean.GetBean<t_itemBean, int>(info2.itemInfo.id);

        if (bean1.t_tab == (int)ItemCategory.Consume)
            res1 += 10000;
        if (bean2.t_tab == (int)ItemCategory.Consume)
            res2 += 10000;

        if(!string.IsNullOrEmpty(bean1.t_quality) && !string.IsNullOrEmpty(bean2.t_quality))
        {
            if (int.Parse(bean1.t_quality) > int.Parse(bean2.t_quality))
                res1 += 5000;
            else if (int.Parse(bean1.t_quality) < int.Parse(bean2.t_quality))
                res2 += 5000;
        }

        if (bean1.t_tab == (int)ItemCategory.Special)
            res1 += 1000;
        if (bean2.t_tab == (int)ItemCategory.Special)
            res2 += 1000;

        if (bean1.t_tab == (int)ItemCategory.Fragment)
            res1 += 100;
        if (bean2.t_tab == (int)ItemCategory.Fragment)
            res2 += 100;

        if (bean1.t_tab == (int)ItemCategory.Materials)
            res1 += 10;
        if (bean2.t_tab == (int)ItemCategory.Materials)
            res2 += 10;

        if (res1 > res2)
            return -1;
        else if (res1 < res2)
            return 1;

        return 0;
    }
    /// <summary>
    /// 请求合成目标装备
    /// </summary>
    /// <param name="itemId">目标装备id</param>
    /// <param name="num">合成的目标装备数量</param>
    public void OnReqItemCompose(int itemId,int num)//装备合成
    {
        ReqItemCompose req = GetEmptyMsg<ReqItemCompose>();
        req.itemId = itemId;
        req.num = num;
        SendMsg(ref req);
    }
    /// <summary>
    /// 排序道具列表(降序排列)
    /// 1.品质越高月靠前
    /// 2.item.t_tap 碎片>特殊>材料>装备材料>代币>消耗品
    /// 3.代币类型越大越靠后
    /// </summary>
    /// <param name="itemInfo1"></param>
    /// <param name="itemInfo2"></param>
    /// <returns></returns>
    public int SortItemInfo(ItemInfo itemInfo1, ItemInfo itemInfo2)
    {
        t_itemBean itemBean1 = ConfigBean.GetBean<t_itemBean, int>(itemInfo1.id);
        if (itemBean1 == null)
            return -1;
        t_itemBean itemBean2 = ConfigBean.GetBean<t_itemBean, int>(itemInfo2.id);
        if (itemBean2 == null)
            return 1;

        //  品质排序
        int quality1 = 0;
        int quality2 = 0;

        if (itemBean1.t_type > 0)
            quality1 = int.Parse(itemBean1.t_quality);
        else
            quality1 = UIUtils.GetDaiBiQulity(itemInfo1.num, itemInfo1.id);

        if (itemBean2.t_type > 0)
            quality2 = int.Parse(itemBean2.t_quality);
        else
            quality2 = UIUtils.GetDaiBiQulity(itemInfo2.num, itemInfo2.id);

        if(quality1 != quality2)
            return quality1.CompareTo(quality2);

        // item.t_tap 排序 记分：碎片1000， 特殊500，材料200， 装备材料100， 代币50，消耗品20
        int grade1 = 0;
        int grade2 = 0;
        ItemCategory itemCategry1 = (ItemCategory)itemBean1.t_tab;
        switch (itemCategry1)
        {
            case ItemCategory.Special:
                grade1 += 500;
                break;
            case ItemCategory.EquipMaterial:
                grade1 += 100;
                break;
            case ItemCategory.Materials:
                grade1 += 200;
                break;
            case ItemCategory.Fragment:
                grade1 += 1000;
                break;
            case ItemCategory.Consume:
                grade1 += 20;
                break;
            case ItemCategory.DaiBi:
                grade1 += 50;
                break;
            default:
                break;
        }

        ItemCategory itemCategry2 = (ItemCategory)itemBean2.t_tab;
        switch (itemCategry2)
        {
            case ItemCategory.Special:
                grade2 += 500;
                break;
            case ItemCategory.EquipMaterial:
                grade2 += 100;
                break;
            case ItemCategory.Materials:
                grade2 += 200;
                break;
            case ItemCategory.Fragment:
                grade2 += 1000;
                break;
            case ItemCategory.Consume:
                grade2 += 20;
                break;
            case ItemCategory.DaiBi:
                grade2 += 50;
                break;
            default:
                break;
        }

        if (grade1 != grade2)
            return -grade1.CompareTo(grade2);

        // 代币排序
        if (itemCategry1 == ItemCategory.DaiBi)
        {
            return -itemBean1.t_type.CompareTo(itemBean2.t_type);
        }

        return 0;
    }

    private int SortCommon(GridInfo info1, GridInfo info2)
    {
        int res1 = 0;
        int res2 = 0;

        if (info1.itemInfo.num <= 0)
            return -1;
        if (info2.itemInfo.num <= 0)
            return 1;

        t_itemBean bean1 = ConfigBean.GetBean<t_itemBean, int>(info1.itemInfo.id);
        t_itemBean bean2 = ConfigBean.GetBean<t_itemBean, int>(info2.itemInfo.id);
        if (!string.IsNullOrEmpty(bean1.t_quality) && !string.IsNullOrEmpty(bean2.t_quality))
        {
            if (int.Parse(bean1.t_quality) > int.Parse(bean2.t_quality))
                res1 += 10000;
            else if (int.Parse(bean1.t_quality) < int.Parse(bean2.t_quality))
                res2 += 10000;
        }
        

        if (bean1.t_type > bean2.t_type)
            res1 += 5000;
        else if (bean1.t_type < bean2.t_type)
            res2 += 5000;

        if (res1 > res2)
            return -1;
        else if (res1 < res2)
            return 1;

        return 0;
    }

    //获得道具可合成数量
    public int GetItemCanComposeNum(int itemId)
    {
        t_itemBean itemBean = ConfigBean.GetBean<t_itemBean, int>(itemId);
        if (itemBean == null)
            return 0;

        int minNum = 0; //满足条件的最小道具数
        string[] needIteInfos = GTools.splitString(itemBean.t_compose_arg, ';');
        if (needIteInfos != null && needIteInfos.Length > 0)
        {
            for (int i = 0; i < needIteInfos.Length; i++)
            {
                int[] needItemInfo = GTools.splitStringToIntArray(needIteInfos[i], '+');
                if (needItemInfo == null || needItemInfo.Length == 0)
                    continue;

                int itemNum = GetItemNum(needItemInfo[0]);
                if (needItemInfo[1] > 0)
                {
                    int num = itemNum / needItemInfo[1];
                    if (minNum == 0 || num < minNum)
                        minNum = num;
                }
               
            }
 
        }

        return minNum;
    }

    private void InitTestData()
    {
        BagInfo = new ResBagInfo();
        GridInfo gridInfo1 = new GridInfo();
        gridInfo1.id = 1;
        ItemInfo itemInfo1 = new ItemInfo();
        itemInfo1.id = 3001001;
        itemInfo1.num = 20;
        gridInfo1.itemInfo = itemInfo1;
        BagInfo.grids.Add(gridInfo1);

        GridInfo gridInfo2 = new GridInfo();
        gridInfo2.id = 1;
        ItemInfo itemInfo2 = new ItemInfo();
        itemInfo2.id = 3001002;
        itemInfo2.num = 20;
        gridInfo2.itemInfo = itemInfo2;
        BagInfo.grids.Add(gridInfo2);

        GridInfo gridInfo13 = new GridInfo();
        gridInfo13.id = 1;
        ItemInfo itemInfo3 = new ItemInfo();
        itemInfo3.id = 3001003;
        itemInfo3.num = 20;
        gridInfo13.itemInfo = itemInfo3;
        BagInfo.grids.Add(gridInfo13);

        GridInfo gridInfo4 = new GridInfo();
        gridInfo4.id = 1;
        ItemInfo itemInfo4 = new ItemInfo();
        itemInfo4.id = 3001006;
        itemInfo4.num = 20;
        gridInfo4.itemInfo = itemInfo4;
        BagInfo.grids.Add(gridInfo4);
    }

    public void ReqDiamondBuyItem(List<ItemInfo> itemInfoList)
    {
        ReqItemBuyCostDiamond msg = GetEmptyMsg<ReqItemBuyCostDiamond>();
        msg.items.AddRange(itemInfoList);

        SendMsg<ReqItemBuyCostDiamond>(ref msg);
    }


    //请求道具合成
    //参数：材料id, 合成的数量
    public void ReqItemCompose(int itemId, int num)
    {
        ReqItemCompose msg = new ReqItemCompose();
        msg.itemId = itemId;
        msg.num = num;
        SendMsg<ReqItemCompose>(ref msg);

    }

}
