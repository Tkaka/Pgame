using System.Collections.Generic;
using Message.Role;
using Data.Beans;
using UnityEngine;
using Message.Profound;



public class StoneInfoExtra
{
    public StoneInfoExtra(StoneInfo stoneInfo, bool isUsing, int type)
    {
        this.stoneInfo = stoneInfo;
        this.isUsing = isUsing;
        this.type = type;
    }

    public StoneInfo stoneInfo;
    public bool isUsing;
    public int type;       //来源类型（记录最起始的位置1背包  2装备  不会随拖拽改变）
}

public class AoyiService : SingletonService<AoyiService>
{

    public class PropertyInfo
    {
        public int propertyId;
        public float propertyValue;
    }

    public enum EAyDrawType
    {
        DiamondOne = 1,    //钻石单抽
        DiamondTen = 2,    //钻石十连抽
        CoinTen = 3,       //金币十连抽
        CoinOne = 4,       //金币单抽 
    }

    public enum EStonePage
    {
        None,
        Primiry,  //初级
        Middle,   //中级
        Ultima,   //究极
    }

    public enum EStoneLevelUpType
    {
        None,
        SingleLevel,   //升级
        OneKey,        //一键强化
        OneKey50,      //一键50
    }

    public enum EOpertionType
    {
        Fist = -1,        //拳头
        Foot = -2,        //脚
        Right = 3,       //右
        RightDown = 4,  //右下
        Down = 5,       //下
        LeftDown = 6,   //左下
        Left = 7,       //左
    }

    public enum EAoyiType
    {
        None,
        GongFang,  //攻防
        GongXue,   //攻血
        FangXue,   //防血
        Shang,     //伤
        Mian,      //免
    }

    private List<StoneInfo> m_stoneBagList = new List<StoneInfo>();       //奥义石背包列表

    private Dictionary<int, StoneInfo> m_stoneBagDic = new Dictionary<int, StoneInfo>();  //奥义石背包字典

    private Dictionary<int, PartGridInfo> m_petStoneInfoDic = new Dictionary<int, PartGridInfo>(); //宠物的奥义石数据

    private Dictionary<int, int> m_rewardDic = new Dictionary<int, int>();                   //1可领 2已领

    private Dictionary<EStonePage, int> m_pageGridNumDic = new Dictionary<EStonePage, int>();    //每个奥义页格子的最大数量

    private Dictionary<EAyDrawType, DrawCountInfo> m_drawInfoDic = new Dictionary<EAyDrawType, DrawCountInfo>();  //抽奖次数信息

    protected override void RegisterEventListener()
    {
        base.RegisterEventListener();
        GED.NED.addListener(ResBagStoneList.MsgId, _ResBagStoneList);
        GED.NED.addListener(ResBagStoneDel.MsgId, _ResBagStoneDel);
        GED.NED.addListener(ResStoneGridInfos.MsgId, _ResStoneGridInfos);
        GED.NED.addListener(ResStoneBreak.MsgId, _ResStoneBreak);
        GED.NED.addListener(ResStoneLevel.MsgId, _ResStoneLevel);
        GED.NED.addListener(ResGetedReward.MsgId, _ResGetedReward);
        GED.NED.addListener(ResStoneListChange.MsgId, _ResStoneListChange);
        GED.NED.addListener(ResAoyiActive.MsgId, _ResAoyiActive);
        GED.NED.addListener(ResSingleStoneChange.MsgId, _ResSingleStoneChange);
        GED.NED.addListener(ResAoyiDraw.MsgId, _ResAoyiDraw);
        GED.NED.addListener(ResDrawCountInfo.MsgId, _ResDrawCountInfo);
    }

    protected override void UnRegisterEventListener()
    {
        base.UnRegisterEventListener();
        GED.NED.removeListener(ResBagStoneList.MsgId, _ResBagStoneList);
        GED.NED.removeListener(ResBagStoneDel.MsgId, _ResBagStoneDel);
        GED.NED.removeListener(ResStoneGridInfos.MsgId, _ResStoneGridInfos);
        GED.NED.removeListener(ResStoneBreak.MsgId, _ResStoneBreak);
        GED.NED.removeListener(ResStoneLevel.MsgId, _ResStoneLevel);
        GED.NED.removeListener(ResGetedReward.MsgId, _ResGetedReward);
        GED.NED.removeListener(ResStoneListChange.MsgId, _ResStoneListChange);
        GED.NED.removeListener(ResAoyiActive.MsgId, _ResAoyiActive);
        GED.NED.removeListener(ResSingleStoneChange.MsgId, _ResSingleStoneChange);
        GED.NED.removeListener(ResAoyiDraw.MsgId, _ResAoyiDraw);
        GED.NED.removeListener(ResDrawCountInfo.MsgId, _ResDrawCountInfo);
    }

    public override void ClearData()
    {
        base.ClearData();
        m_stoneBagList.Clear();
        m_stoneBagDic.Clear();
        m_rewardDic.Clear();
        m_petStoneInfoDic.Clear();
    }

    //------------------------------------------------------------------res
    //奥义石抽奖信息
    private void _ResAoyiDraw(GameEvent evt)
    {
        ResAoyiDraw msg = GetCurMsg<ResAoyiDraw>(evt.EventId);
        WinMgr.Singleton.Open<DrawResultWnd>(WinInfo.Create(false, null, false, msg), UILayer.Popup);
        //if(msg.)
    }

    //奥义石抽奖次数信息
    private void _ResDrawCountInfo(GameEvent evt)
    {
        ResDrawCountInfo msg = GetCurMsg<ResDrawCountInfo>(evt.EventId);
        for (int i = 0; i < msg.countInfos.Count; i++)
        {
            DrawCountInfo info = msg.countInfos[i];
            t_aoyi_drawBean drawBean = ConfigBean.GetBean<t_aoyi_drawBean, int>(info.id);
            if (drawBean != null)
            {
                //服务器发的免费次数是已抽的免费次数  这里转成剩余免费次数
                info.freeCount = drawBean.t_free_num - info.freeCount;
            }

            if (m_drawInfoDic.ContainsKey((EAyDrawType)info.id))
            {
                m_drawInfoDic[(EAyDrawType)info.id] = info;
            }
            else
            {
                m_drawInfoDic.Add((EAyDrawType)info.id, info);
            }
        }

        _RefreshAoyiDrawRewardRed();
        GED.ED.dispatchEvent(EventID.AoyiDrawRewardInfoChange);
    }

    //奥义石初始和添加
    private void _ResBagStoneList(GameEvent evt)
    {
        ResBagStoneList msg = GetCurMsg<ResBagStoneList>(evt.EventId);
        
        for (int i = 0; i < msg.stoneInfos.Count; i++)
        {
            StoneInfo info = msg.stoneInfos[i];

            if (m_stoneBagDic.ContainsKey(info.id))
            {
                //单个赋值是为了用起删除的那块内存
                m_stoneBagDic[info.id].id = info.id;
                m_stoneBagDic[info.id].itemId = info.itemId;
                m_stoneBagDic[info.id].minLevel = info.minLevel;
                m_stoneBagDic[info.id].bigLevel = info.bigLevel;
                m_stoneBagDic[info.id].exp = info.exp;
            }
            else
            {
                m_stoneBagDic.Add(info.id, info);
                m_stoneBagList.Add(info);
            }
        }

        _RefreshAllPetAoyiStoneRed();

    }

    //背包格子移除
    private void _ResBagStoneDel(GameEvent evt)
    {
        ResBagStoneDel msg = GetCurMsg<ResBagStoneDel>(evt.EventId);
        Dictionary<int, StoneInfo> a = new Dictionary<int, StoneInfo>();
        for (int i = 0; i < msg.gridId.Count; i++)
        {
            int gridId = msg.gridId[i];
            if (m_stoneBagDic.ContainsKey(gridId))
            {
                //字典和列表共用一块内存  删除一个就行
                //m_stoneBagDic[gridId].id = 0;
                m_stoneBagDic[gridId].itemId = 0;
                m_stoneBagDic[gridId].minLevel = 0;
                m_stoneBagDic[gridId].bigLevel = 0;
                m_stoneBagDic[gridId].exp = 0;
            }
        }

        _RefreshAllPetAoyiStoneRed();
    }

    //初始宠物的奥义信息
    private void _ResStoneGridInfos(GameEvent evt)
    {
        ResStoneGridInfos msg = GetCurMsg<ResStoneGridInfos>(evt.EventId);
        for (int i = 0; i < msg.gridInfos.Count; i++)
        {
            if (!m_petStoneInfoDic.ContainsKey(msg.gridInfos[i].petId))
            {
                m_petStoneInfoDic.Add(msg.gridInfos[i].petId, msg.gridInfos[i]);
            }

            PartGridInfo info = msg.gridInfos[i];
            for (int index = 0; index < info.rewards.Count; index++)
            {
                Reward reward = info.rewards[index];
                if (m_rewardDic.ContainsKey(reward.id))
                {
                    m_rewardDic[reward.id] = reward.state;
                }
                else
                {
                    m_rewardDic.Add(reward.id, reward.state);
                }
            }

        }
    }



    //单个奥义石突破(废弃)
    private void _ResStoneBreak(GameEvent evt)
    {
        ResStoneBreak msg = GetCurMsg<ResStoneBreak>(evt.EventId);
        StoneInfo stoneInfo = GetPetStoneInfo(msg.petId, (EStonePage)msg.girdLevel, msg.id);
        if (stoneInfo != null)
        {
            stoneInfo.minLevel = 0;
            stoneInfo.bigLevel = msg.bigLevel;
            stoneInfo.exp = 0;
        }
    }

    //单个奥义石强化（废弃）
    private void _ResStoneLevel(GameEvent evt)
    {
        ResStoneLevel msg = GetCurMsg<ResStoneLevel>(evt.EventId);
        StoneInfo stoneInfo = GetPetStoneInfo(msg.petId, (EStonePage)msg.girdLevel, msg.id);
        if (stoneInfo != null)
        {
            stoneInfo.minLevel = msg.minLevel;
            stoneInfo.exp = msg.exp;
        }
    }

    //单个奥义石强化结果
    private void _ResSingleStoneChange(GameEvent evt)
    {
        ResSingleStoneChange msg = GetCurMsg<ResSingleStoneChange>(evt.EventId);
        StoneInfo stoneInfo = GetPetStoneInfo(msg.petId, (EStonePage)msg.girdLevel, msg.id);
        if (stoneInfo != null)
        {
            stoneInfo.minLevel = msg.minLevel;
            stoneInfo.bigLevel = msg.bigLevel;
            stoneInfo.exp = msg.exp;
        }
        else
        {
            Debug.LogError("强化结果返回一个当前不存在的石头");
        }

        GED.ED.dispatchEvent(EventID.AoyiStoneInfoChange);
    }

    //奖励领取结果
    private void _ResGetedReward(GameEvent evt)
    {
        ResGetedReward msg = GetCurMsg<ResGetedReward>(evt.EventId);
        for (int i = 0; i < msg.profoundIds.Count; i++)
        {
            if (m_rewardDic.ContainsKey(msg.profoundIds[i]))
            {
                m_rewardDic[msg.profoundIds[i]] = 2;
            }
            else
            {
                m_rewardDic.Add(msg.profoundIds[i], 2);
            }
        }

        GED.ED.dispatchEvent(EventID.AoyiRewardStateChange);
    }

    //单页石头列表变化（强化  装备 卸下）
    private void _ResStoneListChange(GameEvent evt)
    {
        ResStoneListChange msg = GetCurMsg<ResStoneListChange>(evt.EventId);
        StonePage stonePage = GetPetPageStoneInfos(msg.petId, (EStonePage)msg.girdLevel);
        if (stonePage == null)
        {
            if (m_petStoneInfoDic.ContainsKey(msg.petId))
            {
                StonePage newPage = new StonePage();
                newPage.girdLevel = msg.girdLevel;
                newPage.stones.AddRange(msg.stoneInfos);
                m_petStoneInfoDic[msg.petId].pages.Add(newPage);
            }
        }
        else
        {
            stonePage.stones.Clear();
            stonePage.stones.AddRange(msg.stoneInfos);
        }
        GED.ED.dispatchEvent(EventID.AoyiStoneInfoChange);
        _RefreshSinlgePetAoyiStoneRed(msg.petId);
    }

    //奥义链激活
    private void _ResAoyiActive(GameEvent evt)
    {
        ResAoyiActive msg = GetCurMsg<ResAoyiActive>(evt.EventId);
        if (m_rewardDic.ContainsKey(msg.profoundId))
        {
            m_rewardDic[msg.profoundId] = 1;
        }
        else
        {
            m_rewardDic.Add(msg.profoundId, 1);
        }
    }

    //========================================================================================================请求
    public void ReqLevelUp(int petId, EStonePage page, int partId, EStoneLevelUpType type)
    {
        ReqLevelUp msg = GetEmptyMsg<ReqLevelUp>();
        msg.petId = petId;
        msg.girdLevel = (int)page;
        msg.gridId = partId;
        msg.type = (int)type;
        SendMsg<ReqLevelUp>(ref msg);
    }

    public void ReqLevelBreak(int petId, int partId, EStonePage page)
    {
        ReqLevelBreak msg = GetEmptyMsg<ReqLevelBreak>();
        msg.petId = petId;
        msg.gridId = partId;
        msg.girdLevel = (int)page;
        SendMsg<ReqLevelBreak>(ref msg);
    }

    public void ReqEquip(int petId, EStonePage page, List<EquipInfo> equipInfo)
    {
        if (equipInfo == null)
            return;

        ReqEquip msg = GetEmptyMsg<ReqEquip>();
        msg.petId = petId;
        msg.girdLevel = (int)page;
        for (int i = 0; i < equipInfo.Count; i++)
        {
            msg.equipInfos.Add(equipInfo[i]);
        }

        SendMsg<ReqEquip>(ref msg);
        
    }

    public void ReqUnEquip(int petId, EStonePage gridLevel)
    {
        ReqUnEquip msg = GetEmptyMsg<ReqUnEquip>();
        msg.petId = petId;
        msg.girdLevel = (int)gridLevel;
        SendMsg<ReqUnEquip>(ref msg);
    }

    public void ReqResolve(List<int> grids)
    {
        if (grids == null)
            return;

        ReqResolve msg = GetEmptyMsg<ReqResolve>();
        for (int i = 0; i < grids.Count; i++)
        {
            msg.gridIds.Add(grids[i]);
        }

        SendMsg<ReqResolve>(ref msg);
    }


    public void ReqGetReward(bool isOneKey, int petId, int profoundId = -1)
    {
        ReqGetReward msg = GetEmptyMsg<ReqGetReward>();
        if (profoundId != -1)
        {
            msg.profoundId = profoundId;
        }
        msg.petId = petId;
        msg.oneKey = isOneKey;
        SendMsg<ReqGetReward>(ref msg);
    }

    public void ReqBigOneKeyStrength(int petId, EStonePage gridLevel, int targetLevel, List<int> parts)
    {
        if (parts == null)
            return;

        ReqBigOneKeyStrength msg = GetEmptyMsg<ReqBigOneKeyStrength>();
        msg.petId = petId;
        msg.girdLevel = (int)gridLevel;
        msg.targetLevel = targetLevel;
        for (int i = 0; i < parts.Count; i++)
        {
            msg.id.Add(parts[i]);
        }

        SendMsg<ReqBigOneKeyStrength>(ref msg);
    }

    public void ReqAoyiDraw(int id)
    {
        ReqAoyiDraw msg = GetEmptyMsg<ReqAoyiDraw>();
        msg.id = id;
        SendMsg<ReqAoyiDraw>(ref msg);
    }
    //===========================================================================================数据
    //获得已装备的石头信息(装备的格子ID从1开始)
    public StoneInfo GetPetStoneInfo(int petId, EStonePage page, int gridId)
    {
        if (m_petStoneInfoDic.ContainsKey(petId))
        {
            for (int i = 0; i < m_petStoneInfoDic[petId].pages.Count; i++)
            {
                StonePage stonePage = m_petStoneInfoDic[petId].pages[i];
                if ((int)page == stonePage.girdLevel)
                {
                    for (int index = 0; index < stonePage.stones.Count; index++)
                    {
                        if (stonePage.stones[index].id == gridId)
                            return stonePage.stones[index];
                    }
                }
            }
        }

        return null;
    }

    //获得选中宠物选中页签的奥义石列表
    public StonePage GetPetPageStoneInfos(int petId, EStonePage page)
    {
        if (m_petStoneInfoDic.ContainsKey(petId))
        {
            for (int i = 0; i < m_petStoneInfoDic[petId].pages.Count; i++)
            {
                StonePage stonePage = m_petStoneInfoDic[petId].pages[i];
                if ((int)page == stonePage.girdLevel)
                {
                    return stonePage;
                }
            }
        }

        return null;
    }

    //获得奥义页最大格子数量
    public int GetPageStoneGridNum(EStonePage page)
    {
        if (m_pageGridNumDic.ContainsKey(page))
            return m_pageGridNumDic[page];

        int num = 0;
        List<t_aoyi_pageBean> beans = ConfigBean.GetBeanList<t_aoyi_pageBean>();
        for (int i = 0; i < beans.Count; i++)
        {
            if (beans[i].t_page == (int)page)
            {
                num++;
            }
        }

        m_pageGridNumDic.Add(page, num);
        return num;
    }


    //获得奥义石增加属性信息
    //等级为总等级
    public List<PropertyInfo> GetStoneAddPropertyInfo(int itemId, int level)
    {
        List<PropertyInfo> propertyList = new List<PropertyInfo>();
        int quility = 0;
        t_itemBean itemBean = ConfigBean.GetBean<t_itemBean, int>(itemId);
        if (itemBean != null)
        {
            quility = UIUtils.GetDefaultItemQuality(itemId);
        }

        int stoneType = 0;
        t_aoyiBean aoyiBean = ConfigBean.GetBean<t_aoyiBean, int>(itemId);
        if (aoyiBean != null)
        {
            stoneType = aoyiBean.t_type;
        }

        t_aoyi_shuxingBean shuxingBean = ConfigBean.GetBean<t_aoyi_shuxingBean, int>(stoneType * 10 + quility);
        if (shuxingBean == null)
            return propertyList;

        string[] basePropertyInfos = GTools.splitString(shuxingBean.t_property, ';');
        int[] attachPropertyInfos = GTools.splitStringToIntArray(shuxingBean.t_rate, ';');
        if (basePropertyInfos == null || attachPropertyInfos == null || basePropertyInfos.Length != attachPropertyInfos.Length)
            return propertyList;

        for (int i = 0; i < basePropertyInfos.Length; i++)
        {
            int[] basePropertyInfo = GTools.splitStringToIntArray(basePropertyInfos[i], '+');
            if (basePropertyInfo == null || basePropertyInfo.Length < 2)
                continue;

            PropertyInfo propertyInfo = new PropertyInfo();
            propertyInfo.propertyId = basePropertyInfo[0];
            propertyInfo.propertyValue = basePropertyInfo[1] + (level * attachPropertyInfos[i] * 0.0001f);

            propertyList.Add(propertyInfo);
        }

        return propertyList;
    }

    //获得宠物奥义页石头增加的总属性
    public List<PropertyInfo> GetPetPageStoneAddProperty(int petId, EStonePage page)
    {
        List<PropertyInfo> propertyList = new List<PropertyInfo>();
        if (!m_petStoneInfoDic.ContainsKey(petId))
            return propertyList;


        PartGridInfo partGridInfo = m_petStoneInfoDic[petId];
        List<StoneInfo> stoneInfos = null;
        for (int i = 0; i < partGridInfo.pages.Count; i++)
        {
            if (partGridInfo.pages[i].girdLevel == (int)page)
            {
                stoneInfos = partGridInfo.pages[i].stones;
                break;
            }
        }

        if (stoneInfos == null)
            return propertyList;

        Dictionary<int, PropertyInfo> propertyDic = new Dictionary<int, PropertyInfo>();
        for (int i = 0; i < stoneInfos.Count; i++)
        {
            List<PropertyInfo> tempList = GetStoneAddPropertyInfo(stoneInfos[i].itemId, stoneInfos[i].bigLevel * 10 + stoneInfos[i].minLevel);
            for (int index = 0; index < tempList.Count; index++)
            {
                PropertyInfo propertyInfo = tempList[index];
                if (propertyDic.ContainsKey(propertyInfo.propertyId))
                {
                    propertyDic[propertyInfo.propertyId].propertyValue += propertyInfo.propertyValue;
                }
                else
                {
                    propertyDic.Add(propertyInfo.propertyId, propertyInfo);
                }
            }
        }

        foreach (var info in propertyDic)
        {
            propertyList.Add(info.Value);
        }
        

        return propertyList;
    }

    //激活的奥义链ID
    public int GetActiveAoyiId(int petId, EStonePage page)
    {
        StonePage pageInfo = GetPetPageStoneInfos(petId, page);
        if (pageInfo == null)
            return -1;

        return GetActiveAoyiId(petId, pageInfo.stones);
    }

    //获得激活的奥义ID
    public int GetActiveAoyiId(int petId, List<StoneInfo>stoneList)
    {
        //操作组合
        string operationGroup = "";
        bool isHaveQJ = false;  //是否有拳脚
        for (int i = 0; i < stoneList.Count; i++)
        {
            StoneInfo stoneInfo = stoneList[i];
            t_aoyiBean aoyiBean = ConfigBean.GetBean<t_aoyiBean, int>(stoneInfo.itemId);
            if (!string.IsNullOrEmpty(operationGroup))
            {
                operationGroup += ("+" + aoyiBean.t_dic);
            }
            else
            {
                operationGroup += aoyiBean.t_dic;
            }
             
            if (aoyiBean.t_dic == (int)EOpertionType.Fist || aoyiBean.t_dic == (int)EOpertionType.Foot)
            {
                //想激活奥义 有且只有一个拳或脚
                isHaveQJ = true;
                break;
            }
        }

        if (isHaveQJ == false)
            return -1;


        t_petBean petBean = ConfigBean.GetBean<t_petBean, int>(petId);
        if (petBean == null)
        {
            return -1;
        }

        int[] arrAoyi = GTools.splitStringToIntArray(petBean.t_aoyi, '+');
        if (arrAoyi == null || arrAoyi.Length == 0)
            return -1;

        int activeId = -1;
        for (int i = 0; i < arrAoyi.Length; i++)
        {
            t_aoyi_zuheBean zuheBean = ConfigBean.GetBean<t_aoyi_zuheBean, int>(arrAoyi[i]);
            if (zuheBean == null)
                continue;

            if (zuheBean.t_group.Equals(operationGroup))
            {
                activeId = zuheBean.t_id;
                break;
            }
        }

        return activeId;
    }

    //获得已激活奥义链石头中最低品质
    //参数：奥义链ID
    public int GetLowQuilityInStones(int petId, EStonePage page, int id)
    {
        StonePage stonePage = GetPetPageStoneInfos(petId, page);
        return GetLowQuilityInStones(id, stonePage.stones);

    }

    public int GetLowQuilityInStones(int id, List<StoneInfo> stoneInfos)
    {
        t_aoyi_zuheBean aoyiBean = ConfigBean.GetBean<t_aoyi_zuheBean, int>(id);
        if (aoyiBean == null)
            return -1;

        int[] arrDic = GTools.splitStringToIntArray(aoyiBean.t_group, '+');
        if (arrDic == null || arrDic.Length == 0)
            return -1;

        int quility = -1;

        for (int i = 0; i < stoneInfos.Count; i++)
        {
            //超过奥义链长度的不用管了
            if (i >= arrDic.Length)
            {
                break;
            }

            int itemQuility = UIUtils.GetDefaultItemQuality(stoneInfos[i].itemId);
            if (quility < 0 || itemQuility < quility)
                quility = itemQuility;
        }

        return quility;
    }

    //按品质排序(品质》方向》类型》等级)
    public int SortByQulity(StoneInfo a, StoneInfo b)
    {

        int quilityA = UIUtils.GetDefaultItemQuality(a.itemId);
        int quilityB = UIUtils.GetDefaultItemQuality(b.itemId);
        if (quilityA != quilityB)
            return -quilityA.CompareTo(quilityB);

        int dicA = ConfigBean.GetBean<t_aoyiBean, int>(a.itemId).t_dic;
        int dicB = ConfigBean.GetBean<t_aoyiBean, int>(b.itemId).t_dic;

        dicA = dicA < 0 ? dicA + 20 : dicA;
        dicB = dicB < 0 ? dicB + 20 : dicB;
        if (dicA != dicB)
            return -dicA.CompareTo(dicB);// > dicB ? 1 : -1;

        int typeA = ConfigBean.GetBean<t_aoyiBean, int>(a.itemId).t_type;
        int typeB = ConfigBean.GetBean<t_aoyiBean, int>(b.itemId).t_type;
        if (typeA != typeB)
            return -typeA.CompareTo(typeB);// > typeB ? 1 : -1;

        return -(a.bigLevel * 10 + a.minLevel).CompareTo((b.bigLevel * 10 + b.minLevel));// > (b.bigLevel * 10 + b.minLevel) ? 1 : -1;
    }

    //按指令排序（方向》品质》类型》等级）
    public int SortByDic(StoneInfo a, StoneInfo b)
    {

        int dicA = ConfigBean.GetBean<t_aoyiBean, int>(a.itemId).t_dic;
        int dicB = ConfigBean.GetBean<t_aoyiBean, int>(b.itemId).t_dic;

        dicA = dicA < 0 ? dicA + 20 : dicA;
        dicB = dicB < 0 ? dicB + 20 : dicB;
        if (dicA != dicB)
            return -dicA.CompareTo(dicB);// > dicB ? 1 : -1;

        int quilityA = UIUtils.GetDefaultItemQuality(a.itemId);
        int quilityB = UIUtils.GetDefaultItemQuality(b.itemId);
        if (quilityA != quilityB)
            return -quilityA.CompareTo(quilityB);// > quilityB ? 1 : -1;

        int typeA = ConfigBean.GetBean<t_aoyiBean, int>(a.itemId).t_type;
        int typeB = ConfigBean.GetBean<t_aoyiBean, int>(b.itemId).t_type;
        if (typeA != typeB)
            return -typeA.CompareTo(typeB);// > typeB ? 1 : -1;

        return -(a.bigLevel * 10 + a.minLevel).CompareTo((b.bigLevel * 10 + b.minLevel));// > (b.bigLevel * 10 + b.minLevel) ? 1 : -1;
    }

    //按效果排序（类型》方向》品质》等级）
    public int SortByType(StoneInfo a, StoneInfo b)
    {
        int typeA = ConfigBean.GetBean<t_aoyiBean, int>(a.itemId).t_type;
        int typeB = ConfigBean.GetBean<t_aoyiBean, int>(b.itemId).t_type;
        if (typeA != typeB)
            return -typeA.CompareTo(typeB);// > typeB ? 1 : -1;

        int dicA = ConfigBean.GetBean<t_aoyiBean, int>(a.itemId).t_dic;
        int dicB = ConfigBean.GetBean<t_aoyiBean, int>(b.itemId).t_dic;

        dicA = dicA < 0 ? dicA + 20 : dicA;
        dicB = dicB < 0 ? dicB + 20 : dicB;
        if (dicA != dicB)
            return -dicA.CompareTo(dicB);// > dicB ? 1 : -1;

        int quilityA = UIUtils.GetDefaultItemQuality(a.itemId);
        int quilityB = UIUtils.GetDefaultItemQuality(b.itemId);
        if (quilityA != quilityB)
            return -quilityA.CompareTo(quilityB);// > quilityB ? 1 : -1;

        return -(a.bigLevel * 10 + a.minLevel).CompareTo((b.bigLevel * 10 + b.minLevel));// > (b.bigLevel * 10 + b.minLevel) ? 1 : -1;
    }


    //当前宠物页对应部位装备的是否是对应的石头
    public bool EquipPartIsChange(int petID, EStonePage pageType, StoneInfoExtra stoneInfoExtra, int partID)
    {
        //是从背包来的格子就意味着发生变化了
        if (stoneInfoExtra.type == 1)
            return true;

        bool isChange = true;
        StonePage page = AoyiService.Singleton.GetPetPageStoneInfos(petID, pageType);
        if (page != null)
        {
            for (int i = 0; i < page.stones.Count; i++)
            {
                StoneInfo stoneInfo = page.stones[i];

                if (stoneInfo.id == partID)
                {
                    if (stoneInfoExtra.stoneInfo == stoneInfo)
                        isChange = false;
                    break;
                }
            }
        }

        return isChange;
    }

    public List<StoneInfo> GetBagList()
    {
        return m_stoneBagList;
    }

    //通过格子ID在背包中获得道具信息
    public StoneInfo GetStoneInfoByGridId(int gridId)
    {
        if (m_stoneBagDic.ContainsKey(gridId))
        {
            return m_stoneBagDic[gridId];
        }

        return null;
    }

    //获得奥义技能的激活状态
    //0未激活 1激活未领 2已领
    public int GetAySkillActiveState(int aoyiId)
    {
        if (m_rewardDic.ContainsKey(aoyiId))
            return m_rewardDic[aoyiId];

        return 0;
    }

    //部位是否已解锁
    public bool EquipGridIsUnLock(EStonePage page, int partId)
    {
        t_aoyi_pageBean pageBean = ConfigBean.GetBean<t_aoyi_pageBean, int>((int)page * 10 + partId);
        if (pageBean == null)
            return false;

        return RoleService.Singleton.GetRoleInfo().level >= pageBean.t_level_limit;
    }

    //指定宠物指定页的奥义技能是否生效
    public bool GetCurPageAoyiIsEffect(int petId, EStonePage page)
    {
        int activeId = GetActiveAoyiId(petId, page);
        if (activeId == -1)
            return false;

        bool isEffect = true;
        int activeQuility = GetLowQuilityInStones(petId, page, activeId);

        for (int i = (int)EStonePage.Primiry; i <= (int)EStonePage.Ultima; i++)
        {
            if ((int)page == i)
                continue;

            int id = GetActiveAoyiId(petId, (EStonePage)i);
            if (id == activeId)
            {
                //有重复激活的ID(先看品质  然后再看页签)
                int quility = GetLowQuilityInStones(petId, (EStonePage)i, id);
                if (quility < activeQuility)
                {
                    continue;
                } 
                else if (quility > activeQuility)
                {
                    isEffect = false;
                    break;
                }
                else
                {
                    if ((int)page < i)
                    {
                        isEffect = false;
                        break;
                    }
                }
     
            }
        }

        return isEffect;
    }

    //或奥义抽奖次数信息
    public DrawCountInfo GetAoyiDrawCountInfo(EAyDrawType type)
    {
        if (m_drawInfoDic.ContainsKey(type))
        {
            return m_drawInfoDic[type];
        }   
        else
        {
            DrawCountInfo info = new DrawCountInfo();
            info.id = (int)type;
            info.drawCount = 0;

            t_aoyi_drawBean bean = ConfigBean.GetBean<t_aoyi_drawBean, int>((int)type);
            if (bean != null)
                info.freeCount = bean.t_free_num;

            m_drawInfoDic.Add(type, info);
            return info;
        }
    }

    //------------------------------------------------------------刷新红点
    //刷新奥义抽奖红点
    private void _RefreshAoyiDrawRewardRed()
    {
        string coinDrawPath = "Aoyi/DrawReward/Coin";
        string diamondDrawPath = "Aoyi/DrawReward/Diamond";

        RedDotManager.Singleton.SetRedDotValue(coinDrawPath, GetAoyiDrawCountInfo(EAyDrawType.CoinOne).freeCount > 0);
        RedDotManager.Singleton.SetRedDotValue(diamondDrawPath, GetAoyiDrawCountInfo(EAyDrawType.DiamondOne).freeCount > 0);
    }

    //刷新所有宠物的奥义红点
    private void _RefreshAllPetAoyiStoneRed()
    {
        foreach (var info in m_petStoneInfoDic)
        {
            _RefreshSinlgePetAoyiStoneRed(info.Key);
        }
    }

    //刷新单个宠物所有部位上奥义石头红点
    private void _RefreshSinlgePetAoyiStoneRed(int petId)
    {
        for (int i = (int)EStonePage.Primiry; i <= (int)EStonePage.Ultima; i++)
        {
            for (int j = 0; j < GetPageStoneGridNum((EStonePage)i); j++)
            {
                //部位从1开始
                int part = j + 1;
                string path = GetPetAoyiRedPath(petId, (EStonePage)i, part);
                RedDotManager.Singleton.SetRedDotValue(path, PartCanEquipStone(petId,(EStonePage)i, part));
            }
        }
    }

    public string GetPetAoyiRedPath(int petId = -1, EStonePage page = EStonePage.None, int part = -1)
    {
        if (petId == -1)
            return "Aoyi";
        
        if(page == EStonePage.None)
            return string.Format("Aoyi/{0}", petId);
        
        if(part == -1)
            return string.Format("Aoyi/{0}/{1}", petId, page);


        return string.Format("Aoyi/{0}/{1}/{2}", petId, page, part);
    }


    //指定部位是否可以装备石头
    public bool PartCanEquipStone(int petId, EStonePage page, int part)
    {
        if (EquipGridIsUnLock(page, part) == false)
            return false;

        bool isHaveStone = false;
        //背包中是否有石头
        for (int i = 0; i < m_stoneBagList.Count; i++)
        {
            if (m_stoneBagList[i].itemId > 0)
            {
                //需要这样判断的原因是被移除的石头只是id清零
                isHaveStone = true;
                break;
            }
        }

        if (isHaveStone == false)
            return false;

        //当前部位没装备石头
        return GetPetStoneInfo(petId, page, part) == null;

    }
}