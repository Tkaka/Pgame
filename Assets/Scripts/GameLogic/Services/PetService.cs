using Message.Pet;
using System.Collections.Generic;
using Data.Beans;

public enum PetType
{
    Atk = 1,     // 攻
    Def = 2,     // 防
    Skill = 3,   // 技
}

public enum ZhenRongType
{
    Normal = 1,                       // 主线，精英、竞技场
    EMeng = 2,                        // 噩梦
    ZhongJiShiLian = 3,               // 终极试炼
    GoldTiaoZhan = 4,                 // 金币挑战
    ExpTiaoZhan = 5,                  // 经验挑战
    NvGeDouJia = 6,                   // 女格斗家
    HuanXiangTiaoZhan = 7,            // 幻象挑战
    GuildBoss = 8,                    // 社团副本
}

public class PetService : SingletonService<PetService>
{
    public ResPetInfo PetInfo { get; private set; }

    private Dictionary<int, PetInfo> petDic = new Dictionary<int, PetInfo>();

    private Dictionary<int, PetPropertyMgr> m_petPropertyMgrDic = new Dictionary<int, PetPropertyMgr>();

    // 条件属性 key: 两位 pos * 10 + petType
    private Dictionary<int, Dictionary<PropertyType, PropertyStruct>> m_conditionProperty = new Dictionary<int, Dictionary<PropertyType, PropertyStruct>>();

    public ZhenRongType zhenRongType { get; set; }
    public bool yongyou;

    public bool suMingJiHuo { get; set; }
    public List<int> suMingId = new List<int>();

    List<int> backTeamList = new List<int>();

    public bool suMing { get; set; }
    public PetService()
    {
        if (GameManager.Singleton.IsDebug)
            InitTestData();
    }

    protected override void RegisterEventListener()
    {
        base.RegisterEventListener();
        GED.NED.addListener(ResPetInfo.MsgId, OnPetInfo);
        GED.NED.addListener(ResPetResetFormation.MsgId, OnPetShangZhen);
        GED.NED.addListener(ResPetInfoUpdate.MsgId, OnPetInfoUpdate);
        GED.NED.addListener(ResPetEquipAwaken.MsgId, OnJueXingJieGuo);
        GED.NED.addListener(ResActivePetSolder.MsgId, OnJiBanJiHuo);
        GED.NED.addListener(ResPetFragmentCompose.MsgId, OnResPetFragmentCompose);
        GED.NED.addListener(ResPetSkillReplace.MsgId, OnSkillReplace);
        GED.NED.addListener(ResPetSkillLevelUp.MsgId, OnJiNengShengJi);
        GED.NED.addListener(ResPetAddExp.MsgId, OnPetAddExp);
        GED.NED.addListener(ResPetLevelUp.MsgId, OnPetLevelUp);
        GED.NED.addListener(ResPetColorUp.MsgId, OnPetColorUp);
        GED.NED.addListener(ResPetStarUp.MsgId, OnPetStarUp);
        GED.NED.addListener(ResPetSoulUp.MsgId, OnPetSoulUp);
        GED.NED.addListener(ResPetEquipInfo.MsgId, OnPetEquipInfo);
        GED.NED.addListener(ResPetExtPropertyChange.MsgId, OnPetExtPropertyChange);
        GED.NED.addListener(ResExtConditionPropertyChange.MsgId, OnExtConditionPropertyChange);
    }

    protected override void UnRegisterEventListener()
    {
        base.UnRegisterEventListener();
        GED.NED.removeListener(ResPetInfo.MsgId, OnPetInfo);
        GED.NED.removeListener(ResPetResetFormation.MsgId, OnPetShangZhen);
        GED.NED.removeListener(ResPetInfoUpdate.MsgId, OnPetInfoUpdate);
        GED.NED.removeListener(ResPetEquipAwaken.MsgId, OnJueXingJieGuo);
        GED.NED.removeListener(ResActivePetSolder.MsgId, OnJiBanJiHuo);
        GED.NED.removeListener(ResPetFragmentCompose.MsgId, OnResPetFragmentCompose);
        GED.NED.removeListener(ResPetSkillReplace.MsgId, OnSkillReplace);
        GED.NED.removeListener(ResPetSkillLevelUp.MsgId, OnJiNengShengJi);
        GED.NED.removeListener(ResPetAddExp.MsgId, OnPetAddExp);
        GED.NED.removeListener(ResPetLevelUp.MsgId, OnPetLevelUp);
        GED.NED.removeListener(ResPetColorUp.MsgId, OnPetColorUp);
        GED.NED.removeListener(ResPetStarUp.MsgId, OnPetStarUp);
        GED.NED.removeListener(ResPetSoulUp.MsgId, OnPetSoulUp);
        GED.NED.removeListener(ResPetEquipInfo.MsgId, OnPetEquipInfo);
    }

    private void OnPetExtPropertyChange(GameEvent evt)
    {
        ResPetExtPropertyChange msg = GetCurMsg<ResPetExtPropertyChange>(evt.EventId);
        if (msg == null)
            return;

        int petId = msg.petId;
        if (m_petPropertyMgrDic.ContainsKey(petId))
        {
            m_petPropertyMgrDic[petId].ChangeExtProperty(msg.property);
            GED.ED.dispatchEvent(EventID.ResRoleInfo);
        }
    }

    private void OnExtConditionPropertyChange(GameEvent evt)
    {
        ResExtConditionPropertyChange msg = GetCurMsg<ResExtConditionPropertyChange>(evt.EventId);
        if (msg == null)
            return;

        List<ConditionProperty> list = msg.property;
        if (list.Count == 0)
            return;

        foreach (var conditionPro in list)
        {
            int key = conditionPro.pos * 10 + conditionPro.petType;
            if (!m_conditionProperty.ContainsKey(key))
            {
                Dictionary<PropertyType, PropertyStruct> dic1 = new Dictionary<PropertyType, PropertyStruct>();
                m_conditionProperty.Add(key, dic1);
            }

            Dictionary<PropertyType, PropertyStruct> dic = m_conditionProperty[key];
            List<Property> proList = conditionPro.property;
            foreach (var pro in proList)
            {
                // 存在的属性
                if (dic.ContainsKey((PropertyType)pro.id))
                {
                    PropertyStruct stru = dic[(PropertyType)pro.id];
                    stru.AddPropertyValue((EPropertyFlag)pro.flag, (LNumber)pro.value);
                }
                else
                {
                    PropertyStruct stru = new PropertyStruct((PropertyType)pro.id);
                    stru.SetPropertyValue((EPropertyFlag)pro.flag, (LNumber)pro.value);
                    dic.Add((PropertyType)pro.id, stru);
                }
            }
        }

        foreach (var info in petDic)
        {
            if (m_petPropertyMgrDic.ContainsKey(info.Key))
            {
                m_petPropertyMgrDic[info.Key].RefreshFighrPowert();
            }
        }
        GED.ED.dispatchEvent(EventID.ResRoleInfo);
    }

    private void OnPetEquipInfo(GameEvent evt)
    {
        ResPetEquipInfo msg = GetCurMsg<ResPetEquipInfo>(evt.EventId);
        if (msg == null)
            return;

        int petId = msg.petId;
        PetEquip equip = msg.equip;
        if (m_petPropertyMgrDic.ContainsKey(petId))
        {
            m_petPropertyMgrDic[petId].PetEquipInfoChange(equip);
            GED.ED.dispatchEvent(EventID.ResRoleInfo);
        }

        if (petDic.ContainsKey(petId))
        {
            PetInfo petInfo = petDic[petId];
            PetEquipInfo equipInfo = petInfo.equipInfo;
            PetEquip info = equipInfo.equips[equip.id];
            if (info != null)
            {
                equipInfo.equips[equip.id] = equip;
                GED.ED.dispatchEvent(EventID.OnPetShuXingChanged, petInfo);
            }
        }
    }

    private void OnPetSoulUp(GameEvent evt)
    {
        ResPetSoulUp msg = GetCurMsg<ResPetSoulUp>(evt.EventId);
        if (msg == null)
            return;

        int petId = msg.petId;
        SoulInfo soul = msg.soul;

        if (petDic.ContainsKey(petId))
        {
            PetInfo petInfo = petDic[petId];
            SoulInfo info = petInfo.soulInfo.souls[soul.index];
            if (info != null)
            {
                petInfo.soulInfo.souls[soul.index] = soul;
                GED.ED.dispatchEvent(EventID.OnPetShuXingChanged, petInfo);
            }
        }
    }

    private void OnPetStarUp(GameEvent evt)
    {
        ResPetStarUp msg = GetCurMsg<ResPetStarUp>(evt.EventId);
        if (msg == null)
            return;

        int petId = msg.petId;
        int star = msg.star;
        int priority = msg.priority;

        if (petDic.ContainsKey(petId))
        {
            PetInfo info = petDic[petId];
            info.basInfo.star = star;
            info.priority = priority;

            if (m_petPropertyMgrDic.ContainsKey(petId))
            {
                m_petPropertyMgrDic[petId].PetBaseInfoChange(info);
            }
            GED.ED.dispatchEvent(EventID.OnPetShuXingChanged, info);
            GED.ED.dispatchEvent(EventID.ResRoleInfo);
        }
    }

    private void OnPetColorUp(GameEvent evt)
    {
        ResPetColorUp msg = GetCurMsg<ResPetColorUp>(evt.EventId);
        if (msg == null)
            return;

        int petId = msg.petId;
        int color = msg.color;
        int priority = msg.priority;

        if (petDic.ContainsKey(petId))
        {
            PetInfo info = petDic[petId];
            info.basInfo.color = color;
            info.priority = priority;

            if (m_petPropertyMgrDic.ContainsKey(petId))
            {
                m_petPropertyMgrDic[petId].PetBaseInfoChange(info);
            }
            GED.ED.dispatchEvent(EventID.OnPetShuXingChanged, info);
            GED.ED.dispatchEvent(EventID.ResRoleInfo);
        }
    }

    private void OnPetLevelUp(GameEvent evt)
    {
        ResPetLevelUp msg = GetCurMsg<ResPetLevelUp>(evt.EventId);
        if (msg == null)
            return;

        int petId = msg.petId;
        int exp = msg.exp;
        int level = msg.level;

        if (petDic.ContainsKey(petId))
        {
            PetInfo info = petDic[petId];
            info.basInfo.expRemain = exp;
            info.basInfo.level = level;

            if (m_petPropertyMgrDic.ContainsKey(petId))
            {
                m_petPropertyMgrDic[petId].PetBaseInfoChange(info);
            }

            GED.ED.dispatchEvent(EventID.OnPetShuXingChanged, info);
            GED.ED.dispatchEvent(EventID.ResRoleInfo);
        }
    }

    private void OnPetAddExp(GameEvent evt)
    {
        ResPetAddExp msg = GetCurMsg<ResPetAddExp>(evt.EventId);
        if (msg == null)
            return;

        int petId = msg.petId;
        int exp = msg.exp;

        if (petDic.ContainsKey(petId))
        {
            PetInfo info = petDic[petId];
            info.basInfo.expRemain = exp;
            GED.ED.dispatchEvent(EventID.OnPetShuXingChanged, info);
        }

    }

    private void OnResPetFragmentCompose(GameEvent evt)
    {
        ResPetFragmentCompose msg = GetCurMsg<ResPetFragmentCompose>(evt.EventId);
        if (msg != null && msg.petInfo != null)
        {
            int petId = msg.petInfo.petId;
            if (!petDic.ContainsKey(msg.petInfo.petId))
            {
                PetInfo.petsInfo.Add(msg.petInfo);
                petDic.Add(petId, msg.petInfo);

                m_petPropertyMgrDic.Add(petId, new PetPropertyMgr(msg.petInfo));
                GED.ED.dispatchEvent(EventID.ResPetFragmentCompose, msg.petInfo.petId);
            }
            else
            {
                Logger.err("PetService:AddPet:合成一个已经存在的宠物");
            }
        }
        else
        {
            Logger.err("BagService:OnResPetFragmentCompose:合成的宠物id不合法");
        }
    }

    private void OnPetInfo(GameEvent evt)
    {
        PetInfo = GetCurMsg<ResPetInfo>(evt.EventId);
        if (PetInfo != null && PetInfo.petsInfo != null)
        {
            foreach (PetInfo petInfo in PetInfo.petsInfo)
            {
                if (petDic.ContainsKey(petInfo.petId))
                    petDic[petInfo.petId] = petInfo;
                else
                    petDic.Add(petInfo.petId, petInfo);

                if (m_petPropertyMgrDic.ContainsKey(petInfo.petId))
                {
                    m_petPropertyMgrDic[petInfo.petId].InitPetProperty(petInfo);
                }
                else
                {
                    m_petPropertyMgrDic.Add(petInfo.petId, new PetPropertyMgr(petInfo));
                }
            }
        }

        zhenRongType = ZhenRongType.Normal;
        GED.ED.dispatchEvent(EventID.ResRoleInfo);
        GED.ED.dispatchEvent(EventID.PetInfoInit);
    }


    //战斗结束刷新宠物经验
    public void RefreshPetExp(PetExp info)
    {
        if (info != null && PetInfo != null && PetInfo.petsInfo != null)
        {
            //更新字典
            if (!petDic.ContainsKey(info.petId))
                return;

            petDic[info.petId].basInfo.level = info.level;
            petDic[info.petId].basInfo.totalExp = info.totalExp;
            petDic[info.petId].basInfo.expRemain = info.expRemain;


            //更新协议内容
            for (int i = 0; i < PetInfo.petsInfo.Count; i++)
            {
                if (PetInfo.petsInfo[i].petId == info.petId)
                {
                    PetInfo.petsInfo[i].basInfo.level = info.level;
                    PetInfo.petsInfo[i].basInfo.totalExp = info.totalExp;
                    PetInfo.petsInfo[i].basInfo.expRemain = info.expRemain;
                }
            }

        }
    }
    /// <summary>
    /// 更新宠物信息
    /// </summary>
    /// <param name="petInfo"></param>
    public void UpdatePetInfo(PetInfo info)
    {
        if (info != null && PetInfo != null && PetInfo.petsInfo != null)
        {
            //更新字典
            if (petDic.ContainsKey(info.petId))
                petDic[info.petId] = info;
            else
                petDic.Add(info.petId, info);

            //更新协议内容
            for (int i = 0; i < PetInfo.petsInfo.Count; i++)
            {
                if (PetInfo.petsInfo[i].petId == info.petId)
                {
                    PetInfo.petsInfo[i] = info;
                }
            }

        }
    }


    //技能替换
    private void OnSkillReplace(GameEvent evt)
    {
        ResPetSkillReplace msg = GetCurMsg<ResPetSkillReplace>(evt.EventId);

        PetInfo petInfo = GetPetInfo(msg.petId);
        for (int i = 0; i < petInfo.skillInfo.skillInfos.Count; ++i)
        {
            if (petInfo.skillInfo.skillInfos[i].id == msg.oldSkill)
            {
                petInfo.skillInfo.skillInfos[i].id = msg.newSkill;
                break;
            }
        }
    }
    /// <summary>
    /// 根据类型获得宠物阵容
    /// </summary>
    /// <param name="type"></param>
    /// <param name="isUIShow">是否是UI上显示的，UI上显示会调整位置</param>
    /// <returns></returns>
    public List<int> GetTeamList(ZhenRongType type, bool isUIShow = true)
    {
        if (PetInfo == null)
            return null;

        int typeIndex = (int)type;
        for (int i = 0; i < PetInfo.formationInfos.Count; i++)
        {
            FormationInfo formationInfo = PetInfo.formationInfos[i];
            if (formationInfo.type == typeIndex)
            {
                List<int> shangZhenIDList = new List<int>();
                shangZhenIDList.AddRange(formationInfo.formation);
                if (isUIShow && shangZhenIDList.Count >= 6)
                {
                    SortTeamList(shangZhenIDList);
                    // 位置 5,3,1,2,4,6
                    int[] teamArr = new int[6];
                    teamArr[2] = shangZhenIDList[0];
                    teamArr[3] = shangZhenIDList[1];
                    teamArr[1] = shangZhenIDList[2];
                    teamArr[4] = shangZhenIDList[3];
                    teamArr[0] = shangZhenIDList[4];
                    teamArr[5] = shangZhenIDList[5];

                    shangZhenIDList = new List<int>();
                    shangZhenIDList.AddRange(teamArr);
                }

                return shangZhenIDList;
            }
        }

        return null;
    }

    public long GetRoleFightPower()
    {
        List<int> list = GetTeamList(ZhenRongType.Normal, false);
        long fight = 0;
        foreach (var id in list)
        {
            fight += GetPetFightPower(id);
        }

        return fight;
    }

    private int SortImpl(PetInfo a, PetInfo b)
    {
        int resA = 0;
        int resB = 0;

        if (ShangZhenList(a.petId))
            resA += 10000;
        if (ShangZhenList(b.petId))
            resB += 10000;

        if (a.basInfo.level > b.basInfo.level)
            resA += 8000;
        else if (a.basInfo.level < b.basInfo.level)
            resB += 8000;

        if (a.fightInfo != null)
        {
            if (a.fightInfo.fightPower > b.fightInfo.fightPower)
                resA += 1000;
            else if ((a.fightInfo.fightPower < b.fightInfo.fightPower))
                resB += 1000;
        }
        if (a.petId > b.petId)
            resA += 500;
        else if (a.petId < b.petId)
            resB += 500;

        if (resA > resB)
            return -1;
        else if (resA == resB)
            return 0;
        else
            return 1;
    }


    public int GetSkillLevel(int petid, int skillId)
    {
        PetInfo petInfo = GetPetInfo(petid);
        if (petInfo != null)
        {
            List<SkillInfo> skillInfos = petInfo.skillInfo.skillInfos;
            for (int i = 0; i < skillInfos.Count; i++)
            {
                if (skillInfos[i].id == skillId)
                {
                    return skillInfos[i].level;
                }
            }
        }
        return 1;
    }


    public List<PetInfo> GetPetInfos(bool isSort = false)
    {
        if (PetInfo != null && PetInfo.petsInfo != null)
        {
            PetInfo.petsInfo.Sort(SortImpl);
            return PetInfo.petsInfo;
        }
        return null;
    }

    public PetInfo GetPetInfo(int petid)
    {
        if (petDic.ContainsKey(petid))
            return petDic[petid];
        else
            return null;
    }
    public PetInfo GetPetByID(int petID)
    {
        if (petDic.ContainsKey(petID))
        {
            return petDic[petID];
        }
        return null;
    }

    //将旧的上阵列表中的数据替换成新的
    public bool SetReplace(int oldId, int newId, ZhenRongType type)
    {
        List<int> zhenRongList = GetTeamList(type, false);
        if (zhenRongList == null)
            return false;

        if (PetInfo != null && zhenRongList != null)
        {
            if (oldId != 0)
            {
                for (int i = 0; i < 6; ++i)
                {
                    if (zhenRongList[i] == oldId)
                    {
                        zhenRongList[i] = newId;
                        ReqPetResetFormation req = GetEmptyMsg<ReqPetResetFormation>();
                        req.formationInfo = new FormationInfo();
                        for (int j = 0; j < 6; ++j)
                        {
                            req.formationInfo.formation.Add(zhenRongList[j]);
                        }
                        req.formationInfo.type = (int)type;
                        SendMsg(ref req);
                        return true;
                    }
                }
            }
            else
            {
                for (int i = 0; i < zhenRongList.Count; ++i)
                {
                    if (zhenRongList[i] == 0)
                    {
                        zhenRongList[i] = newId;
                        ReqPetResetFormation req = GetEmptyMsg<ReqPetResetFormation>();
                        req.formationInfo = new FormationInfo();
                        for (int j = 0; j < zhenRongList.Count; ++j)
                        {
                            req.formationInfo.formation.Add(zhenRongList[j]);
                        }
                        req.formationInfo.type = (int)type;
                        SendMsg(ref req);
                        return true;
                    }
                }
            }
            return false;
        }
        return false;
    }
    /// <summary>
    /// 一键上阵的功能， 取战斗力最高的6个，防最高
    /// </summary>
    public bool KeyShangZhen(ZhenRongType type)
    {
        List<int> zhenRongList = GetBestTeamList(type);
        if (zhenRongList == null)
            return false;

        SaveTeamToServer(zhenRongList, type);
        return true;
    }
    /// <summary>
    /// 获得最佳的阵容信息
    /// </summary>
    /// <returns></returns>
    public List<int> GetBestTeamList(ZhenRongType type)
    {
        List<PetInfo> petInfoList = new List<PetInfo>();
        petInfoList.AddRange(PetInfo.petsInfo);
        List<int> teamPetList = new List<int>();

        // 遍历宠物表，找出战力最高的6个
        FilterPetInfo(petInfoList);

        if (petInfoList.Count == 0)
            return null;

        int count = petInfoList.Count;
        int petID;
        for (int i = 0; i < count; i++)
        {
            petID = petInfoList[i].petId;
            if (teamPetList.Contains(petID))
                continue;

            if (teamPetList.Count < 6)
            {
                teamPetList.Add(petID);
            }
            else
            {
                for (int j = 0; j < teamPetList.Count; j++)
                {
                    if (GetPetFightPower(teamPetList[j]) < GetPetFightPower(petID))
                    {
                        int tempID = petID;
                        petID = teamPetList[j];
                        teamPetList[j] = tempID;
                    }
                }
            }
        }

        // 补全阵容数量 排序
        List<int> zhenRongList = GetTeamList(type);
        count = zhenRongList.Count;
        for (int i = teamPetList.Count; i < count; i++)
        {
            teamPetList.Add(0);
        }
        SortTeamList(teamPetList);

        return teamPetList;
    }

    /// <summary>
    /// 过滤宠物信息
    /// </summary>
    private void FilterPetInfo(List<PetInfo> petInfoList)
    {
        // 女格斗家特殊处理(暂时先按攻来过滤)
        if (zhenRongType == ZhenRongType.NvGeDouJia)
        {
            int count = petInfoList.Count;
            PetInfo petInfo = null;
            for (int i = count - 1; i >= 0; i--)
            {
                petInfo = petInfoList[i];
                t_petBean petBean = ConfigBean.GetBean<t_petBean, int>(petInfo.petId);
                if (petBean != null)
                {
                    if (petBean.t_type != (int)PetType.Atk)
                        petInfoList.RemoveAt(i);
                }
            }
        }

        // 终极试炼（过滤掉等级小于10）
        if (zhenRongType == ZhenRongType.ZhongJiShiLian)
        {
            int count = petInfoList.Count;
            PetInfo petInfo = null;
            for (int i = count - 1; i >= 0; i--)
            {
                petInfo = petInfoList[i];
                if (petInfo.basInfo.level < 10)
                {
                    petInfoList.RemoveAt(i);
                }
            }
        }

    }
    /// <summary>
    /// 获得战斗里最高的宠物id
    /// </summary>
    /// <returns></returns>
    public int GetHightestFightPowerPet()
    {
        PetInfo hightestPet = PetInfo.petsInfo[0];
        int count = PetInfo.petsInfo.Count;
        PetInfo petInfo = null;
        for (int i = 1; i < count; i++)
        {
            petInfo = PetInfo.petsInfo[i];
            hightestPet = hightestPet.fightInfo.fightPower >= petInfo.fightInfo.fightPower ? hightestPet : petInfo;
        }

        return hightestPet.petId;
    }

    public void SortTeamList(List<int> teamList)
    {
        int count = teamList.Count;
        backTeamList.Clear();
        // 去掉null的index
        for (int i = count - 1; i >= 0; i--)
        {
            int petID = teamList[i];
            if (petID == 0)
            {
                teamList.RemoveAt(i);
            }
        }
        // 排序前排
        teamList.Sort(SortPetListFront);
        if (teamList.Count > 3)
        {
            for (int i = 3; i < teamList.Count; i++)
            {
                backTeamList.Add(teamList[i]);
            }
            // 排序后排
            backTeamList.Sort(SortPetListBack);
            for (int i = 0; i < backTeamList.Count; i++)
            {
                teamList[3 + i] = backTeamList[i];
            }
        }

        // 补全阵容数量
        for (int i = teamList.Count; i < count; i++)
        {
            teamList.Add(0);
        }
    }
    /// <summary>
    /// 阵容队列排序 （前排 防技攻)
    /// </summary>
    /// <param name="petID1"></param>
    /// <param name="petID2"></param>
    /// <returns></returns>
    public int SortPetListFront(int petID1, int petID2)
    {
        // 类别 
        // 前排    防：500    技：200   攻：100
        int score1 = 0;
        int score2 = 0;
        t_petBean petBean1 = ConfigBean.GetBean<t_petBean, int>(petID1);
        t_petBean petBean2 = ConfigBean.GetBean<t_petBean, int>(petID2);
        if (petBean1 == null)
        {
            return 1;
        }

        if (petBean2 == null)
        {
            return -1;
        }

        PetType petType1 = (PetType)petBean1.t_type;
        PetType petType2 = (PetType)petBean2.t_type;

        switch (petType1)
        {
            case PetType.Atk:
                score1 = 100;
                break;
            case PetType.Def:
                score1 = 500;
                break;
            case PetType.Skill:
                score1 = 200;
                break;
            default:
                break;
        }
        switch (petType2)
        {
            case PetType.Atk:
                score2 = 100;
                break;
            case PetType.Def:
                score2 = 500;
                break;
            case PetType.Skill:
                score2 = 200;
                break;
            default:
                break;
        }
        if (score1 != score2)
            return -score1.CompareTo(score2);

        // 战斗力:前排攻击从低到高，后排防御从低到高
        PetInfo petInfo1 = GetPetInfo(petID1);
        PetInfo petInfo2 = GetPetInfo(petID2);

        if (petInfo1 == null)
        {
            return -1;
        }

        if (petInfo2 == null)
        {
            return 1;
        }

        if (petType1 == PetType.Atk)
            return GetPetFightPower(petInfo1.petId).CompareTo(GetPetFightPower(petInfo2.petId));

        return -GetPetFightPower(petInfo1.petId).CompareTo(GetPetFightPower(petInfo2.petId));
    }
    /// <summary>
    /// 阵容队列排序 (后排 攻技防)
    /// </summary>
    /// <param name="petID1"></param>
    /// <param name="petID2"></param>
    /// <returns></returns>
    public int SortPetListBack(int petID1, int petID2)
    {
        // 类别 
        // 后排    防：100，  技：200， 攻：500
        int score1 = 0;
        int score2 = 0;
        t_petBean petBean1 = ConfigBean.GetBean<t_petBean, int>(petID1);
        t_petBean petBean2 = ConfigBean.GetBean<t_petBean, int>(petID2);
        if (petBean1 == null)
        {
            return 1;
        }

        if (petBean2 == null)
        {
            return -1;
        }

        PetType petType1 = (PetType)petBean1.t_type;
        PetType petType2 = (PetType)petBean2.t_type;

        switch (petType1)
        {
            case PetType.Atk:
                score1 = 500;
                break;
            case PetType.Def:
                score1 = 100;
                break;
            case PetType.Skill:
                score1 = 200;
                break;
            default:
                break;
        }
        switch (petType2)
        {
            case PetType.Atk:
                score2 = 500;
                break;
            case PetType.Def:
                score2 = 100;
                break;
            case PetType.Skill:
                score2 = 200;
                break;
            default:
                break;
        }
        if (score1 != score2)
            return -score1.CompareTo(score2);

        // 战斗力:后排防御从低到高  其他从高到低
        PetInfo petInfo1 = GetPetInfo(petID1);
        PetInfo petInfo2 = GetPetInfo(petID2);

        if (petInfo1 == null)
        {
            return -1;
        }

        if (petInfo2 == null)
        {
            return 1;
        }

        if (petType2 == PetType.Def)
            return GetPetFightPower(petInfo1.petId).CompareTo(GetPetFightPower(petInfo2.petId));

        return -GetPetFightPower(petInfo1.petId).CompareTo(GetPetFightPower(petInfo2.petId));
    }

    /// <summary>
    /// 得到上阵列表中的第一个宠物的下标
    /// </summary>
    /// <returns></returns>
    public int GetFirstIndex(ZhenRongType type)
    {
        int index = 0;
        List<int> zhenRongList = GetTeamList(type);
        if (zhenRongList == null)
            return index;

        for (int i = 0; i < zhenRongList.Count; ++i)
        {
            if (zhenRongList[i] != 0)
            {
                index = i;
                break;
            }
        }

        return index;
    }

    //根据id判断是否在上阵列表中
    public bool ShangZhenList(int petId)
    {
        List<int> zhenRongList = GetTeamList(zhenRongType);
        if (zhenRongList == null)
            return false;

        for (int i = 0; i < zhenRongList.Count; ++i)
        {
            if (zhenRongList[i] == petId)
                return true;
        }

        return false;
    }

    //根据宠物Id返回宠物等级
    public int GetPetLevel(int petId)
    {
        if (petDic.ContainsKey(petId))
        {
            PetInfo info = petDic[petId];
            return info.basInfo.level;
        }
        return 0;
    }
    private void InitTestData()
    {
        PetInfo = new ResPetInfo();
        //PetInfo.teamPetsId.Add(100);
        //PetInfo.teamPetsId.Add(102);
        //PetInfo.teamPetsId.Add(108);
        //PetInfo.teamPetsId.Add(127);
        //PetInfo.teamPetsId.Add(110);
        //PetInfo.teamPetsId.Add(105);
        FormationInfo finfo = new FormationInfo();
        //finfo.formation.Add(135);
        //finfo.formation.Add(127);
        finfo.formation.Add(110);
        finfo.formation.Add(107);
        finfo.formation.Add(111);
        finfo.formation.Add(109);
        finfo.formation.Add(104);
        //finfo.formation.Add(116);
        //finfo.formation.Add(129);
        //finfo.formation.Add(102);
        finfo.type = (int)ZhenRongType.Normal;
        PetInfo.formationInfos.Add(finfo);


        PetInfo pet1 = new PetInfo();
        pet1.basInfo = new PetBaseInfo();
        pet1.petId = 111;
        pet1.basInfo.level = 1;
        pet1.basInfo.color = 1;
        pet1.basInfo.star = 3;
        PetInfo.petsInfo.Add(pet1);
        PetInfo pet2 = new PetInfo();
        pet2.basInfo = new PetBaseInfo();
        pet2.petId = 109;
        pet2.basInfo.level = 3;
        pet2.basInfo.color = 1;
        pet2.basInfo.star = 5;
        PetInfo.petsInfo.Add(pet2);
        PetInfo pet3 = new PetInfo();
        pet3.basInfo = new PetBaseInfo();
        pet3.petId = 108;
        pet3.basInfo.level = 0;
        pet3.basInfo.color = 1;
        pet3.basInfo.star = 1;
        PetInfo.petsInfo.Add(pet3);

        PetInfo pet4 = new PetInfo();
        pet4.basInfo = new PetBaseInfo();
        pet4.petId = 127;
        pet4.basInfo.level = 0;
        pet4.basInfo.color = 1;
        pet4.basInfo.star = 1;
        PetInfo.petsInfo.Add(pet4);

        PetInfo pet5 = new PetInfo();
        pet5.basInfo = new PetBaseInfo();
        pet5.petId = 110;
        pet5.basInfo.level = 0;
        pet5.basInfo.color = 1;
        pet5.basInfo.star = 1;
        PetInfo.petsInfo.Add(pet5);

        PetInfo pet6 = new PetInfo();
        pet6.basInfo = new PetBaseInfo();
        pet6.petId = 103;
        pet6.basInfo.level = 0;
        pet6.basInfo.color = 1;
        pet6.basInfo.star = 1;
        PetInfo.petsInfo.Add(pet6);

        if (PetInfo != null && PetInfo.petsInfo != null)
        {
            foreach (PetInfo petInfo in PetInfo.petsInfo)
            {
                petDic.Add(petInfo.petId, petInfo);
            }
        }

    }
    private void OnJiNengShengJi(GameEvent evt)
    {
        ResPetSkillLevelUp res = GetCurMsg<ResPetSkillLevelUp>(evt.EventId);

        if (res == null)
            return;

        int petId = res.petId;

        if (petDic.ContainsKey(petId))
        {
            PetInfo info = petDic[petId];
            SkillInfo skillInfo = info.skillInfo.skillInfos[res.index];
            skillInfo.level = res.level;
            GED.ED.dispatchEvent(EventID.OnJiNengShengJi, res.skillId);
        }

    }

    public void SaveTeamToServer(List<int> list, ZhenRongType type)
    {
        ReqPetResetFormation req = GetEmptyMsg<ReqPetResetFormation>();
        req.formationInfo = new FormationInfo();
        req.formationInfo.formation.AddRange(list);
        req.formationInfo.type = (int)type;
        SendMsg(ref req);
    }
    #region 数据处理 ***********************************************************************************************************************************
    private List<int> _zhanHunUnlockColorList = new List<int>();
    /// <summary>
    /// 战魂是否解锁
    /// </summary>
    /// <param name="index">战魂的下标</param>
    /// <param name="petID">宠物id</param>
    /// <returns></returns>
    public bool ZhanHunIsUnlock(int index, int petID)
    {
        if (_zhanHunUnlockColorList.Count == 0)
            SetZhanHunUnlockColors();

        if (_zhanHunUnlockColorList.Count >= index)
        {
            PetInfo petInfo = PetService.Singleton.GetPetByID(petID);
            return petInfo.basInfo.color >= _zhanHunUnlockColorList[index];
        }

        return false;
    }
    /// <summary>
    /// 获得战魂解锁的条件品阶
    /// </summary>
    /// <returns></returns>
    public int GetZhanHunUnlockColor(int index)
    {
        if (_zhanHunUnlockColorList.Count == 0)
            SetZhanHunUnlockColors();

        if (_zhanHunUnlockColorList.Count >= index)
        {
            return _zhanHunUnlockColorList[index];
        }

        return 10;
    }
    /// <summary>
    /// 设置战魂解锁的条件
    /// </summary>
    /// <returns></returns>
    private void SetZhanHunUnlockColors()
    {
        t_globalBean globalBean = ConfigBean.GetBean<t_globalBean, int>(90005);
        if (globalBean == null)
        {
            return;
        }

        if (!string.IsNullOrEmpty(globalBean.t_string_param))
        {
            string[] unlockColorArr = globalBean.t_string_param.Split('+');
            int count = unlockColorArr.Length;
            for (int i = 0; i < count; i++)
            {
                _zhanHunUnlockColorList.Add(int.Parse(unlockColorArr[i]));
            }
        }
    }
    /// <summary>
    /// 获得宠物给定等级的升级经验值
    /// </summary>
    /// <param name="petID"></param>
    /// <returns></returns>
    public int GetCurLevelExp(int petID, int level)
    {
        t_petBean petBean = ConfigBean.GetBean<t_petBean, int>(petID);
        t_pet_lvup_expBean spiritBean = ConfigBean.GetBean<t_pet_lvup_expBean, int>(level);
        if (petBean == null || spiritBean == null)
        {
            return int.MaxValue;
        }

        int standardNum = ConfigBean.GetBean<t_globalBean, int>(1005).t_int_param;
        int curNum = 0;
        switch (petBean.t_zizhi)
        {
            case 11:
                curNum = ConfigBean.GetBean<t_globalBean, int>(1003).t_int_param;
                break;
            case 12:
                curNum = ConfigBean.GetBean<t_globalBean, int>(1004).t_int_param;
                break;
            case 13:
                curNum = standardNum;
                break;
            case 14:
                curNum = ConfigBean.GetBean<t_globalBean, int>(1006).t_int_param;
                break;
            case 15:
                curNum = ConfigBean.GetBean<t_globalBean, int>(1007).t_int_param;
                break;
            default:
                break;
        }

        return spiritBean.t_exp * curNum / standardNum;
    }
    /// <summary>
    /// 获得宠物升星的最大星级
    /// </summary>
    /// <returns></returns>
    public int GetPetMaxStar()
    {
        //   100301  
        t_globalBean globalBean = ConfigBean.GetBean<t_globalBean, int>(100301);
        if (globalBean != null)
        {
            return globalBean.t_int_param;
        }

        Logger.err("获得全局表中的宠物最大星级数失败！");
        return 0;
    }
    /// <summary>
    /// 获得宠物和装备的最大品阶
    /// </summary>
    /// <returns></returns>
    public int GetMaxColor()
    {
        // 110201
        t_globalBean globalBean = ConfigBean.GetBean<t_globalBean, int>(110201);
        if (globalBean != null)
        {
            return globalBean.t_int_param;
        }

        Logger.err("获得全局表中的最大品阶数失败！");
        return 0;
    }
    /// <summary>
    /// 获得战魂的最大等级
    /// </summary>
    /// <returns></returns>
    public int GetZhanHunMaxLevel()
    {
        // 1005001
        t_globalBean globalBean = ConfigBean.GetBean<t_globalBean, int>(1005001);
        if (globalBean != null)
        {
            return globalBean.t_int_param;
        }

        return 0;
    }
    public bool ZhanHunIsReachMaxLevel(int index, int petID)
    {
        PetInfo petInfo = GetPetInfo(petID);
        if (petInfo != null)
        {
            return petInfo.soulInfo.souls[index].level >= GetZhanHunMaxLevel();
        }

        return false;
    }

    public List<float> GetZhanHunDesValueList(int zhanHunID, int index, int petID)
    {
        List<float> valueList = new List<float>();
        t_pet_soulBean petSoulBean = ConfigBean.GetBean<t_pet_soulBean, int>(zhanHunID);
        int level = 1;
        PetInfo petInfo = GetPetInfo(petID);
        if (petInfo != null)
        {
            SoulInfo soulInfo = petInfo.soulInfo.souls[index];
            level = soulInfo.level;
        }
        if (petSoulBean != null)
        {
            int value = 0;
            if (petSoulBean.t_initValue1 != 0)
            {
                if (petSoulBean.t_initValue1 == -1)
                    valueList.Add(petSoulBean.t_initValue1);
                else
                {
                    value = petSoulBean.t_initValue1 + (level - 1) * petSoulBean.t_gropValue1;
                    valueList.Add(value);
                }
            }

            if (petSoulBean.t_initValue2 != 0)
            {
                if (petSoulBean.t_initValue2 == -1)
                    valueList.Add(petSoulBean.t_initValue2);
                else
                {
                    value = petSoulBean.t_initValue2 + (level - 1) * petSoulBean.t_groupValue2;
                    valueList.Add(value);
                }
            }

            if (petSoulBean.t_initValue3 != 0)
            {
                if (petSoulBean.t_initValue3 == -1)
                    valueList.Add(petSoulBean.t_initValue3);
                else
                {
                    value = petSoulBean.t_initValue3 + (level - 1) * petSoulBean.t_groupValue3;
                    valueList.Add(value);
                }
            }

            if (petSoulBean.t_initValue4 != 0)
            {
                if (petSoulBean.t_initValue4 == -1)
                    valueList.Add(petSoulBean.t_initValue4);
                else
                {
                    value = petSoulBean.t_initValue4 + (level - 1) * petSoulBean.t_groupValue4;
                    valueList.Add(value);
                }
            }
        }

        return valueList;
    }

    //获得宠物属性值
    public float GetPetPropertyValue(int petId, PropertyType propertyType)
    {
        if (m_petPropertyMgrDic.ContainsKey(petId))
            return m_petPropertyMgrDic[petId].GetPropertyValue(propertyType);

        return 0;
    }

    // 获取战斗力
    public long GetPetFightPower(int petId)
    {
        if (m_petPropertyMgrDic.ContainsKey(petId))
            return m_petPropertyMgrDic[petId].GetFightPower().Floor;

        return 0;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public PetPropertyMgr GetPetPropertyMgr(int petId)
    {
        if (m_petPropertyMgrDic.ContainsKey(petId))
            return m_petPropertyMgrDic[petId];
        return null;
    }
    /// <summary>
    /// 获得战魂升级所需的经验表ID
    /// </summary>
    /// <returns></returns>
    public t_pet_soulup_expBean GetPetSoulUpBean(int soulType, int level)
    {
        int id = soulType * 100 + level;
        t_pet_soulup_expBean soulUpBean = ConfigBean.GetBean<t_pet_soulup_expBean, int>(id);
        return soulUpBean;
    }

    public void GetConditionProperty(PetInfo info, PropertyType type, out LNumber attachValue, out LNumber percentValue)
    {
        t_petBean bean = ConfigBean.GetBean<t_petBean, int>(info.petId);
        int petType = bean.t_type;
        List<int> list =  GetTeamList(ZhenRongType.Normal, false);
        int index = list.IndexOf(info.petId);

        int pos = 3;
        if (index >= 0 && index < 3)
            pos = 1;
        else if (index >= 3 && index <= 5)
            pos = 2;

        LNumber att = 0;
        LNumber per = 0;
        foreach (var conPro in m_conditionProperty)
        {
            int conPos = conPro.Key / 10;
            int conType = conPro.Key % 10;

            if (!conPro.Value.ContainsKey(type)) 
                continue;

            if ((conPos == 3 || conPos == pos) && (conType == 4 || conType == petType))
            {
                PropertyStruct stru = conPro.Value[type];
                att += stru.attachValue;
                per += stru.percentValue;
            }
        }

        attachValue = att;
        percentValue = per;
    }

    /**
     * pos  1前排 2后排
     * 
     **/
    public Dictionary<PropertyType, PropertyStruct> GetPosProperty(int pos)
    {
        Dictionary<PropertyType, PropertyStruct> property = new Dictionary<PropertyType, PropertyStruct>();
        foreach (var conPro in m_conditionProperty)
        {
            int conPos = conPro.Key / 10;
            int conType = conPro.Key % 10;

            if ((pos == conPos || conPos == 3) && conType == 4)
            {
                Dictionary<PropertyType, PropertyStruct>  pro = conPro.Value;
                foreach (var _pro in pro)
                {
                    if (property.ContainsKey(_pro.Key))
                    {
                        property[_pro.Key].AddPropertyValue(EPropertyFlag.Attach, _pro.Value.attachValue);
                        property[_pro.Key].AddPropertyValue(EPropertyFlag.Percent, _pro.Value.percentValue);
                    }
                    else
                    {
                        property.Add(_pro.Key, _pro.Value);
                    }
                }
            }
        }
        return property;
    }

    #endregion

    #region 请求 **************************************************************************************************************************
    /// <summary>
    /// 请求技能升级
    /// </summary>
    /// <param name="petId"></param>
    /// <returns></returns>
    ///
    public void JiNengShengJi(int petid,int index)
    {
        Logger.log("请求技能升级");
        ReqPetSkillLevelUp msg = GetEmptyMsg<ReqPetSkillLevelUp>();
        msg.petId = petid;
        msg.idx = index;
        SendMsg(ref msg);
    }

    public bool HasPet(int petId)
    {
        if (petDic.ContainsKey(petId))
        {
            PetInfo info = petDic[petId];
            return info.basInfo.level > 0;
        }
        return false;
    }

    private void OnPetShangZhen(GameEvent evt)
    {
        ResPetResetFormation shangzhen = GetCurMsg<ResPetResetFormation>(evt.EventId);

        if (shangzhen.formationInfo.formation.Count == 6)
        {
            for (int i = 0; i < PetInfo.formationInfos.Count; i++)
            {
                FormationInfo formationInfo = PetInfo.formationInfos[i];
                if (formationInfo.type == shangzhen.formationInfo.type)
                {
                    PetInfo.formationInfos[i] = shangzhen.formationInfo;
                }
            }

            GED.ED.dispatchEvent(EventID.OnPetTeamListChanged);
        }
        else
        {
            Logger.err(" PetService：OnPetShangZhen:服务器发送的上阵个数有误 ");
        }
    }
    /// <summary>
    /// 觉醒结果
    /// </summary>
    /// <param name="evt"></param>
    private void OnJueXingJieGuo(GameEvent evt)
    {
        ResPetEquipAwaken msg = GetCurMsg<ResPetEquipAwaken>(evt.EventId);
        if (msg.result == 1)
        {
            GED.ED.dispatchEvent(EventID.OnJueXingJieGuo);
        }
    }
    /// <summary>
    /// 接收激活羁绊消息
    /// </summary>
    public void OnJiBanJiHuo(GameEvent evt)
    {
        ResActivePetSolder msg = GetCurMsg<ResActivePetSolder>(evt.EventId);
        suMingJiHuo = msg.isActive;
        if (msg.isActive)
        {
            //激活宿命
            suMing = true;
        }
        else
        {
            /*宿命失效*/
            suMing = false;
        }
        suMingId.Clear();
        for (int i = 0; i < msg.tableIds.Count; ++i)
        {
            suMingId.Add(msg.tableIds[i]);
        }
    }

    private void OnPetInfoUpdate(GameEvent evt)
    {
        ResPetInfoUpdate petInfoUpdate = GetCurMsg<ResPetInfoUpdate>(evt.EventId);
        if (petInfoUpdate.petInfo != null)
        {
            UpdatePetInfo(petInfoUpdate.petInfo);
            GED.ED.dispatchEvent(EventID.OnPetShuXingChanged, petInfoUpdate.petInfo);
        }
        else
        {
            Logger.err("PetService: OnPetInfoUpdate: 服务器发送的信息为空");
        }
    }

    public void ReqPetShengPing(int petID, List<GridInfo> gridInfo)
    {
        ReqPetColorUp msg = GetEmptyMsg<ReqPetColorUp>();
        msg.petId = petID;
        int num = gridInfo.Count;
        for (int i = 0; i < num; i++)
        {
            msg.gridInfo.Add(gridInfo[i]);
        }

        SendMsg(ref msg);
    }

    public void ReqAddPetExp(int petID, GridInfo gridInfo)
    {
        ReqPetAddExp msg = GetEmptyMsg<ReqPetAddExp>();
        msg.petId = petID;
        msg.gridInfo = gridInfo;
        //msg.gridInfo.gridId = gridInfo.gridId;
        //msg.gridInfo.num = gridInfo.num;

        SendMsg(ref msg);
    }

    public void ReqPetStarUp(int petId, int gridId, int num)
    {
        ReqPetStarUp msg = GetEmptyMsg<ReqPetStarUp>();
        msg.petId = petId;
        msg.gridInfo = new GridInfo();
        msg.gridInfo.gridId = gridId;
        msg.gridInfo.num = num;
        SendMsg(ref msg);
    }


    public void ReqZhanHunStrength(int petID, int index, Dictionary<int,int> selectGridDict)
    {
        ReqPetSoulUpUseItem msg = GetEmptyMsg<ReqPetSoulUpUseItem>();
        msg.petId = petID;
        msg.idx = index;
        foreach (var key in selectGridDict.Keys)
        {
            GridInfo info = new GridInfo();
            info.gridId = key;
            info.num = selectGridDict[key];
            msg.grids.Add(info);
        }
        SendMsg(ref msg);
    }


    public void ReqDiamondZhanHunStrength(int petID, int index)
    {
        ReqPetSoulUpUseDiamond msg = GetEmptyMsg<ReqPetSoulUpUseDiamond>();
        msg.petId = petID;
        msg.idx = index;
        SendMsg(ref msg);
    }
    /// <summary>
    /// 请求秘籍和徽章的升级
    /// </summary>
    public void ReqSpecialEquipUpgrade(int petID, int equipIndex, int gridID, int itemNum)
    {
        ReqPetSpecialEquipLvUp msg = GetEmptyMsg<ReqPetSpecialEquipLvUp>();
        msg.petId = petID;
        GridInfo gridInfo = new GridInfo();
        gridInfo.gridId = gridID;
        gridInfo.num = itemNum;
        msg.grids.Add(gridInfo);
        msg.idx = equipIndex;

        SendMsg<ReqPetSpecialEquipLvUp>(ref msg);
    }
    /// <summary>
    /// 请求普通装备升级
    /// </summary>
    public void ReqNormalEquipUpgrade(int petID, int equipIndex, int targetLevel)
    {
        ReqPetEquipLvUp msg = GetEmptyMsg<ReqPetEquipLvUp>();
        msg.petId = petID;
        msg.idx = equipIndex;
        msg.targetLevel = targetLevel;

        SendMsg(ref msg);
    }
    /// <summary>
    /// 请求普通装备升品
    /// </summary>
    public void ReqNormalEquipSP(int petID, int equipIndex, List<GridInfo> gridInfoList)
    {
        ReqPetEquipColorUp msg = GetEmptyMsg<ReqPetEquipColorUp>();
        msg.petId = petID;
        msg.idx = equipIndex;
        msg.grids.AddRange(gridInfoList);

        SendMsg<ReqPetEquipColorUp>(ref msg);
    }
    /// <summary>
    /// 请求特殊装备升品
    /// </summary>
    public void ReqSpecialEquipSP(int petID, int equipIndex)
    {
        ReqPetSpecialEquipColorUp msg = GetEmptyMsg<ReqPetSpecialEquipColorUp>();
        msg.petId = petID;
        msg.idx = equipIndex;

        SendMsg<ReqPetSpecialEquipColorUp>(ref msg);
    }
    /// <summary>
    /// 请求特殊装备快速升级
    /// </summary>
    public void ReqSpecialFastLvUp(int petID, int equipIndex, int targetLevel, List<GridInfo> gridInfoList)
    {
        ReqPetSpecialEquipFastLvUp msg = GetEmptyMsg<ReqPetSpecialEquipFastLvUp>();
        msg.petId = petID;
        msg.idx = equipIndex;
        msg.targetLevel = targetLevel;
        msg.grids.AddRange(gridInfoList);

        SendMsg<ReqPetSpecialEquipFastLvUp>(ref msg);
    }
    /// <summary>
    /// 请求普通装备快速升级
    /// </summary>
    public void ReqNormalFastLvUp(int petID, int equipIndex, int targetLevel, List<GridInfo> gridInfoList)
    {
        ReqPetEquipFastLvUp msg = GetEmptyMsg<ReqPetEquipFastLvUp>();
        msg.petId = petID;
        msg.idx = equipIndex;
        msg.targetLevel = targetLevel;
        msg.grids.AddRange(gridInfoList);

        SendMsg<ReqPetEquipFastLvUp>(ref msg);
    }

    public void ReqEquipAwak(int petId, int index, List<GridInfo> grids)
    {
        ReqPetEquipAwaken msg = GetEmptyMsg<ReqPetEquipAwaken>();
        msg.petId = petId;
        msg.idx = index;
        int lenth = grids.Count;
        for (int i = 0; i < lenth; ++i)
        {
            msg.grids.Add(grids[i]);
        }
        SendMsg(ref msg);
    }
    //请求装备降星
    public void ReqLowerStar(int petid, int index)
    {
        ReqPetEquipStarDown msg = GetEmptyMsg<ReqPetEquipStarDown>();
        msg.petId = petid;
        msg.idx = index;
        SendMsg(ref msg);
    }

    public void ReqWNFragExchange(int petID, int num)
    {
        ReqFragmentTransform msg = GetEmptyMsg<ReqFragmentTransform>();

        msg.petId = petID;
        msg.num = num;
        SendMsg<ReqFragmentTransform>(ref msg);
    }

    public void ReqPetCompose(int petID)
    {
        ReqPetCompose msg = GetEmptyMsg<ReqPetCompose>();
        msg.petId = petID;

        SendMsg<ReqPetCompose>(ref msg);
    }
    #endregion;
    #region 红点提示判断 ： 宠物是否可以升品，宠物是否可以升星，宠物装备升品，宠物装备升星

    Dictionary<int, Message.Bag.GridInfo> materialDict = new Dictionary<int, Message.Bag.GridInfo>();
    List<int> materialIdList = new List<int>();
    List<int> needNumList = new List<int>();

    /// <summary>
    /// 是否需要显示红点，不管是宠物本身还是宠物的装备
    /// </summary>
    /// <returns></returns>
    public bool IsShowRedTip(int petID)
    {
        if (IsShowPetRedTip(petID) || IsShowPetEquipRedTip(petID))
        {
            return true;
        }

        return false;
    }

    public bool IsShowPetRedTip(int petID)
    {
        if (IsPetCanStarUp(petID) || IsPetCanColorUp(petID))
        {
            return true;
        }

        return false;
    }

    public bool IsShowPetEquipRedTip(int petID)
    {
        PetInfo petInfo = GetPetByID(petID);
        int equipCount = petInfo.equipInfo.equips.Count;

        for (int i = 0; i < equipCount; i++)
        {
            if (IsPetEquipCanColorUp(petID, i) || IsPetEquipCanStarUp(petID, i))
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// 宠物是否可以升星
    /// </summary>
    /// <returns></returns>
    public bool IsPetCanStarUp(int petID)
    {
        // 判断宠物的碎片是否足够
        t_petBean petBean = ConfigBean.GetBean<t_petBean, int>(petID);
        if (petBean == null)
            return false;
        // 得到碎片在背包中的数量
        int fragID = petBean.t_fragment_id;
        Message.Bag.GridInfo gridInfo = BagService.Singleton.GetGridInfoByID(fragID);
        int haveNum = gridInfo == null ? 0 : gridInfo.itemInfo.num;
        // 得到升星需要的碎片数量
        PetInfo petInfo = GetPetByID(petID);
        int needFragNum = UIUtils.GetStarUpCount(petID, petInfo.basInfo.star).value2;

        return haveNum >= needFragNum;
    }
    /// <summary>
    /// 宠物是否可以升品
    /// </summary>
    /// <param name="petID"></param>
    /// <returns></returns>
    public bool IsPetCanColorUp(int petID)
    {
        // 获得宠物升品的材料ID列表，判断背包中的是否足够
        t_petBean petBean = ConfigBean.GetBean<t_petBean, int>(petID);
        if (petBean == null)
            return false;

        PetInfo petInfo = PetService.Singleton.GetPetByID(petID);
        int id = petInfo.basInfo.color * 1000 + petBean.t_type * 100 + petBean.t_zizhi;
        t_pet_colorup_costBean petColorUpCostBean = ConfigBean.GetBean<t_pet_colorup_costBean, int>(id);
        if (petColorUpCostBean == null)
            return false;

        materialDict.Clear();
        materialDict.Add(petColorUpCostBean.t_drug_id, null);
        materialDict.Add(petColorUpCostBean.t_main_id, null);
        materialDict.Add(petColorUpCostBean.t_vice_id1, null);
        materialDict.Add(petColorUpCostBean.t_vice_id2, null);

        materialIdList.Clear();
        materialIdList.Add(petColorUpCostBean.t_drug_id);
        materialIdList.Add(petColorUpCostBean.t_main_id);
        materialIdList.Add(petColorUpCostBean.t_vice_id1);
        materialIdList.Add(petColorUpCostBean.t_vice_id2);

        needNumList.Clear();
        needNumList.Add(petColorUpCostBean.t_drug_num);
        needNumList.Add(petColorUpCostBean.t_main_num);
        needNumList.Add(petColorUpCostBean.t_vice_num);
        needNumList.Add(petColorUpCostBean.t_vice_num);

        BagService.Singleton.SetGridInfoByIDs(materialDict);

        int count = needNumList.Count;
        for (int i = 0; i < count; i++)
        {
            int tempID = materialIdList[i];
            int num = needNumList[i];
            int haveNum = materialDict[tempID] == null ? 0 : materialDict[tempID].itemInfo.num;

            if (num > haveNum)
            {
                return false;
            }
        }

        return true;
    }
    /// <summary>
    /// 宠物装备是否可以升品(只提示普通装备)
    /// </summary>
    /// <returns></returns>
    public bool IsPetEquipCanColorUp(int petID, int equipIndex)
    {
        // 获得宠物升品需要的材料ID列表，判断背包中是否足够
        PetInfo petInfo = GetPetByID(petID);
        if (petInfo == null)
            return false;

        PetEquip petEquip = petInfo.equipInfo.equips[equipIndex];

        if (equipIndex < (int)EquipPosition.HuiZhan)
        {
            // 普通装备
            t_equip_colorupBean equipColorUpBean = ConfigBean.GetBean<t_equip_colorupBean, int>(petEquip.color);
            if (equipColorUpBean == null)
                return false;

            string[] itemInfos = GTools.splitString(equipColorUpBean.t_item, ';');

            materialIdList.Clear();
            needNumList.Clear();
            materialDict.Clear();
            int count = itemInfos.Length;
            int[] itemInfoArr = null;
            for (int i = 0; i < count; i++)
            {
                itemInfoArr = GTools.splitStringToIntArray(itemInfos[i]);
                if (itemInfoArr.Length == 1)
                    continue;

                materialIdList.Add(itemInfoArr[0]);
                needNumList.Add(itemInfoArr[1]);
                materialDict.Add(materialIdList[i], null);
            }

            BagService.Singleton.SetGridInfoByIDs(materialDict);

            for (int i = 0; i < count; i++)
            {
                int tempID = materialIdList[i];
                int tempNum = needNumList[i];
                int haveNum = materialDict[tempID] == null ? 0 : materialDict[tempID].itemInfo.num;
                if (tempNum > haveNum)
                {
                    return false;
                }
            }

            return true;
        }
        else
        {
            // 特殊装备
            int id = petEquip.color * 100 + equipIndex;
            t_special_equip_lvcolorupBean seColorUpBean = ConfigBean.GetBean<t_special_equip_lvcolorupBean, int>(id);
            if (seColorUpBean != null)
            {
                int[] needNumArr = GTools.splitStringToIntArray(seColorUpBean.t_token_nums);
                if (needNumArr.Length > 0)
                {
                    Message.Role.RoleInfo roleInfo = RoleService.Singleton.GetRoleInfo();
                    if (equipIndex == 4)
                        return roleInfo.honorCurrency >= needNumArr[0];
                    else if (equipIndex == 5)
                        return roleInfo.trailCurrency >= needNumArr[0];
                }
            }

            return false;
        }
    }
    //判断是否可以升星
    public bool IsPetEquipCanStarUp(int petID, int petEquipIdex)
    {
        t_petBean bean = ConfigBean.GetBean<t_petBean,int>(petID);
        PetInfo petInfo = petDic[petID];
        int star = petInfo.equipInfo.equips[petEquipIdex].star;
        int juexingshishuliang = BagService.Singleton.GetItemNum(106000);

        int type = petEquipIdex;
        int juexingshi = 0;
        if (type == 0 || type == 4 || type == 5)
        {
            string[] xuqiu = null;
            if (type == 0)
            {
                xuqiu = GTools.splitString(bean.t_weap_star_id_nums, ';');
                for (int i = 0; i < xuqiu.Length; ++i)
                {
                    if (i == star)
                    {
                        int[] xuqiuId = GTools.splitStringToIntArray(xuqiu[i]);
                        if (xuqiuId.Length == 1)
                        {
                            if (juexingshishuliang >= xuqiuId[0])
                            { return true; }
                            else
                            { return false; }
                        }
                        else
                        {
                            int daojushuliang = BagService.Singleton.GetItemNum(xuqiuId[0]);
                            if (daojushuliang < xuqiuId[1])
                                return false;
                            if (juexingshishuliang < xuqiuId[2])
                                return false;
                            return true;
                        }
                    }
                }
            }
            else if(type == 4)
            {
                xuqiu = GTools.splitString(bean.t_badge_star_id, ';');
                for (int i = 0; i < xuqiu.Length; ++i)
                {
                    if (i == star)
                    {
                        int[] xuqiuId = GTools.splitStringToIntArray(xuqiu[i]);
                        
                        int daojushuliang = BagService.Singleton.GetItemNum(xuqiuId[0]);
                        if (daojushuliang < xuqiuId[1])
                            return false;
                        if (juexingshishuliang < xuqiuId[2])
                            return false;
                        return true;
                    }
                }
            }
            else if (type == 5)
            {
                xuqiu = GTools.splitString(bean.t_book_star_nums, ';');
                for (int i = 0; i < xuqiu.Length; ++i)
                {
                    if (i == star)
                    {
                        int[] xuqiuId = GTools.splitStringToIntArray(xuqiu[i]);

                        int daojushuliang = BagService.Singleton.GetItemNum(xuqiuId[0]);
                        if (daojushuliang < xuqiuId[1])
                            return false;
                        if (juexingshishuliang < xuqiuId[2])
                            return false;
                        return true;
                    }
                }
            }
        }
        else
        {
            t_globalBean global = ConfigBean.GetBean<t_globalBean, int>(106002);
            if (global == null)
                return false;
            int[] juexingshinumber = GTools.splitStringToIntArray(global.t_string_param);
            for (int i = 0; i < 4; ++i)
            {
                if (i == type)
                    juexingshi = juexingshinumber[i];
            }
            if (BagService.Singleton.GetItemNum(106000) < juexingshi)
                return false;
        }
        return true;
    }

    #endregion;
}
