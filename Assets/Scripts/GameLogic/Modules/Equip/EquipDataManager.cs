using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Message.Pet;
using Message.Role;
using Message.Bag;
using Data.Beans;

/// <summary>
/// 装备的部位
/// </summary>
public enum EquipPosition
{
    Weapon = 0,      // 武器
    Cloth = 1,       // 衣服
    KuZi = 2,        // 裤子
    Shoes = 3,       // 鞋子
    HuiZhan = 4,     // 徽章
    MiJi = 5,        // 秘籍
}

public enum EquipPanelType
{
    EquipStrength = 0,    // 装备强化
    EquipAwake = 1,       // 装备觉醒
}

public class EquipDataManager {

    private int _curSelectPetID;
    /// <summary>
    /// 当前选中装备的类型（15种）
    /// </summary>
    private int _curSelectType;
    /// <summary>
    /// 快速升级达到的目标品质
    /// </summary>
    private int _targetColor;
    private int _totalGold;
    private int _totalTokenNum;
    private int _totalDiamond;
    private int _targetLevel;
    private int _useTotalExp;

    private EquipPosition _curSelectEquipPos;
    private List<PetInfo> petInfoList = new List<PetInfo>();
    private Dictionary<int, Message.Bag.GridInfo> specialExpGridInfoDict = new Dictionary<int, Message.Bag.GridInfo>();
    private int[] specialXunZhangIDs = { 211001, 211002, 211003, 211004 };    
    private int[] specialMiJiIDs = { 210000, 210002, 210003, 210004 }; 
    private List<int> spMaterialsIDList = new List<int>();
    private List<ItemInfo> needBuyItemList = new List<ItemInfo>();
    private Dictionary<int, int> spMaterialsNumDict = new Dictionary<int, int>();
    private Dictionary<int, Message.Bag.GridInfo> spMaterialsGridInfoDict = new Dictionary<int, Message.Bag.GridInfo>();
    /// <summary>
    /// 快速升级时使用的道具id和数量字典
    /// </summary>
    private Dictionary<int, int> quickUpgradeUseExpItemDict = new Dictionary<int, int>();
    /// <summary>
    /// 装备升品，或者觉醒前的属性值
    /// </summary>
    public Dictionary<PropertyType, PropertyStruct> oldDictProperty = new Dictionary<PropertyType, PropertyStruct>();
    private int _startPage = 0;   //打开界面初始页签

    #region 属性----------------------------------------------------------------------------------------------------------------
    public int StartPage
    {
        private set { _startPage = value; }
        get { return _startPage; }
    }

    public int CurSelectPetID
    {
        get { return _curSelectPetID; }
        set { _curSelectPetID = value; }
    }

    public int CurSelectEquipType
    {
        get { return _curSelectType; }
        set { _curSelectType = value; }
    }

    public int TargetColor
    {
        get { return _targetColor; }
        set { _targetColor = value; }
    }

    public int TargetLevel
    {
        get { return _targetLevel; }
    }

    public int TotalGold
    {
        get { return _totalGold; }
    }

    public int TotalTokenNum
    {
        get { return _totalTokenNum; }
    }

    public int TotalDiamon
    {
        get { return _totalDiamond; }
    }

    /// <summary>
    /// 部位
    /// </summary>
    public EquipPosition CurSelectEquipPos
    {
        get { return _curSelectEquipPos; }
        set { _curSelectEquipPos = value; }
    }

    public List<PetInfo> PetInfoList
    {
        get { return petInfoList; }
    }
	public EquipDataManager(ThreeParam<int, int,int> threeParam)
    {
        InitData(threeParam);
    }

    public int[] SpecialXunZhangIDs
    {
        get { return specialXunZhangIDs; }
    }

    public int[] SpecialMiJiIDs
    {
        get { return specialMiJiIDs; }
    }

    public List<int> SPMaterialsIDList
    {
        get { return spMaterialsIDList; }
    }

    public Dictionary<int, int> QuickUpgradeUseExpItemDict
    {
        get { return quickUpgradeUseExpItemDict; }
    }

    public List<ItemInfo> NeedBuyMaterialList
    {
        get { return needBuyItemList; }
    }

    #endregion;

    private void InitData(ThreeParam<int, int,int> threeParam)
    {
        petInfoList.AddRange(PetService.Singleton.GetPetInfos(true));
        if (petInfoList.Count <= 0)
        {
            return;
        }
        FilterPetInfoList();
        if (threeParam.value1 == 0)
        {
            // 宠物id == 0，取第一个宠物
            _curSelectPetID = petInfoList[0].petId;
        }
        else
            _curSelectPetID = threeParam.value1;
        _curSelectEquipPos = (EquipPosition)threeParam.value2;
        StartPage = threeParam.value3;
        SetInitEquipType();
        InitSpecialExpGridInfo();
        RefreshData();
    }

    private void SetInitEquipType()
    {
        t_petBean petBean = ConfigBean.GetBean<t_petBean, int>(_curSelectPetID);
        if (petBean == null || string.IsNullOrEmpty(petBean.t_commet_equip_id))
            return;
        string[] typeArr = petBean.t_commet_equip_id.Split('+');
        switch (_curSelectEquipPos)
        {
            case EquipPosition.Weapon:
            case EquipPosition.Cloth:
            case EquipPosition.KuZi:
            case EquipPosition.Shoes:
                _curSelectType = int.Parse(typeArr[(int)_curSelectEquipPos]);
                break;
            case EquipPosition.HuiZhan:
                break;
            case EquipPosition.MiJi:
                break;
            default:
                break;
        }
    }

    public void RefreshData()
    {
        SetSpecialExpDict();
        PetEquip petEquip = GetCurSelectPetEquip();
        SetSPMatreials(petEquip.color);
    }

    private void FilterPetInfoList()
    {
        PetInfo petInfo;
        int count = petInfoList.Count;
        for (int i = count -1; i >= 0; i--)
        {
            petInfo = petInfoList[i];
            if (petInfo.basInfo.level <= 0)
                petInfoList.RemoveAt(i);
        }
    }
    //通过Id获得宠物引用
    public PetInfo GetPet(int petid)
    {
        int number = petInfoList.Count;
        for (int i = 0; i < number; ++i)
        {
            if (petid == PetInfoList[i].petId)
                return PetInfoList[i];
        }
        return null;
    }
    /// <summary>
    /// 设置
    /// </summary>
    private void InitSpecialExpGridInfo()
    {
        SetSpecialExpDict();
    }


    public void SetSpecialExpDict()
    {
        if (_curSelectEquipPos == EquipPosition.HuiZhan || _curSelectEquipPos == EquipPosition.MiJi)
        {
            int[] ids = _curSelectEquipPos == EquipPosition.HuiZhan ? specialXunZhangIDs : specialMiJiIDs;
            int count = ids.Length;
            specialExpGridInfoDict.Clear();
            for (int i = 0; i < count; i++)
            {
                specialExpGridInfoDict.Add(ids[i], null);
            }

            BagService.Singleton.SetGridInfoByIDs(specialExpGridInfoDict);
        } 
    }

    public Message.Bag.GridInfo GetSpecialExpGridInfoByID(int itemID)
    {
        if (!specialExpGridInfoDict.ContainsKey(itemID))
            return null;

        return specialExpGridInfoDict[itemID];
    }


    public void OnClose()
    {
        petInfoList = null;
        specialExpGridInfoDict = null;
    }
    /// <summary>
    /// 获得初始选中的宠物下标
    /// </summary>
    /// <returns></returns>
    public int GetInitScrollIndex()
    {
        int count = petInfoList.Count;
        for (int i = 0; i < count; i++)
        {
            if (petInfoList[i].petId == _curSelectPetID)
            {
                return i;
            }
        }
        return -1;
    }
    /// <summary>
    /// 是否到达当前品阶的最大等级
    /// </summary>
    /// <returns></returns>
    public bool IsArriveMaxLevel()
    {
        PetEquip petEquip = GetCurSelectPetEquip();

        int maxLv = int.MaxValue;
        switch (_curSelectEquipPos)
        {
            case EquipPosition.Weapon:
            case EquipPosition.Cloth:
            case EquipPosition.KuZi:
            case EquipPosition.Shoes:
                t_equip_colorupBean normalColorUpBean = ConfigBean.GetBean<t_equip_colorupBean, int>(petEquip.color);
                if (normalColorUpBean == null)
                    return false;
                maxLv = normalColorUpBean.t_lv_max;
                break;
            case EquipPosition.HuiZhan:
            case EquipPosition.MiJi:
                t_special_equip_lvcolorupBean specailColorUpBean = GetSpecialEquipLvColorUpBean(petEquip.color);
                if (specailColorUpBean == null)
                    return false;
                maxLv = specailColorUpBean.t_lv_max;
                break;
            default:
                break;
        }
        return petEquip.level >= maxLv;
    }
    /// <summary>
    /// 是否解锁快速升级的按钮
    /// </summary>
    /// <returns></returns>
    public bool UnlockQuickBtn()
    {
        RoleInfo roleInfo = RoleService.Singleton.GetRoleInfo();
        // TODO : 解锁等级条件
        return roleInfo.level >= 45;
    }

    public PetEquip GetCurSelectPetEquip()
    {
        PetInfo petInfo = PetService.Singleton.GetPetByID(_curSelectPetID);
        PetEquip petEquip = null;
        if (petInfo.equipInfo.equips.Count > 0)
        {
            petEquip = petInfo.equipInfo.equips[(int)CurSelectEquipPos];
        }

        return petEquip;
    }

    /// <summary>
    /// 获得属性值 (如果是普通装备 那么就是攻击，防御，生命)
    /// </summary>
    /// <param name="star">星级</param>
    /// <param name="level"></param>
    /// <param name="color">品阶</param>
    /// <returns></returns>
    public List<int> GetAttributeData(int star, int level, int color)
    {
        List<int> dataList = new List<int>();
        switch (_curSelectEquipPos)
        {
            case EquipPosition.Weapon:
            case EquipPosition.Cloth:
            case EquipPosition.KuZi:
            case EquipPosition.Shoes:
                SetNormalEquipAttributeData(dataList, color, level, star);
                break;
            case EquipPosition.HuiZhan:
            case EquipPosition.MiJi:
                SetSpecialEquipAttributeData(dataList, color, level, star);
                break;
            default:
                break;
        }

        return dataList;
    }

    // 获取装备属性 [key:属性类型 value:属性结构]
    public Dictionary<PropertyType, PropertyStruct> GetAttributeData()
    {
        PetPropertyMgr mgr = PetService.Singleton.GetPetPropertyMgr(_curSelectPetID);
        EquipProperty pro = mgr.GetPetEquipProperty(_curSelectEquipPos);

        return pro.GetEquipPropertyDic();
    }

    /// 根据参数获取装备属性 
    /// <param name="star">星级</param>
    /// <param name="level"></param>
    /// <param name="color">品阶</param>
    public Dictionary<PropertyType, PropertyStruct> GetAttributeDataByParam(int star, int level, int color)
    {
        PetPropertyMgr mgr = PetService.Singleton.GetPetPropertyMgr(_curSelectPetID);
        return mgr.GetPetEquipProperty(_curSelectEquipPos, level, color, star);
    }

    /// <summary>
    /// 更新缓存的装备属性
    /// </summary>
    public void UpdateOldEquipProperty()
    {
        oldDictProperty.Clear();
        Dictionary<PropertyType, PropertyStruct> curPropertyDcit = GetAttributeData();
        foreach (var key in curPropertyDcit.Keys)
        {
            oldDictProperty.Add(key, curPropertyDcit[key]);
        }
    }

    private void SetNormalEquipAttributeData(List<int> dataList, int color, int level, int star)
    {
        t_equip_attrBean equipAttrBean = GetEquipAttrBean(color);
        t_equip_colorupBean equipColorUpBean = ConfigBean.GetBean<t_equip_colorupBean, int>(color);
        if (equipColorUpBean == null || equipAttrBean == null)
            return;

        int baseLevel = equipColorUpBean.t_lv_base;
        int levelDiff = level - baseLevel;

        // 获得攻击，防御，生命的基础总值
        int atkAll = 0;
        int defAll = 0;
        int hpAll = 0;
        if (!string.IsNullOrEmpty(equipAttrBean.t_base_atk)
            && !string.IsNullOrEmpty(equipAttrBean.t_base_def)
            && !string.IsNullOrEmpty(equipAttrBean.t_base_hp))
        {
            string[] atkAllArr = equipAttrBean.t_base_atk.Split('+');
            atkAll = int.Parse(atkAllArr[star]);

            string[] defAllArr = equipAttrBean.t_base_def.Split('+');
            defAll = int.Parse(defAllArr[star]);

            string[] hpAllArr = equipAttrBean.t_base_hp.Split('+');
            hpAll = int.Parse(hpAllArr[star]);
        }

        // 获得攻击，防御，生命的每级增加值
        int atkAdd = 0;
        int defAdd = 0;
        int hpAdd = 0;
        if (!string.IsNullOrEmpty(equipAttrBean.t_lv_up_atk)
            && !string.IsNullOrEmpty(equipAttrBean.t_lv_up_def)
            && !string.IsNullOrEmpty(equipAttrBean.t_lv_up_hp))
        {
            string[] atkAddArr = equipAttrBean.t_lv_up_atk.Split('+');
            atkAdd = int.Parse(atkAddArr[star]);

            string[] defAddArr = equipAttrBean.t_lv_up_def.Split('+');
            defAdd = int.Parse(defAddArr[star]);

            string[] hpAddArr = equipAttrBean.t_lv_up_hp.Split('+');
            hpAdd = int.Parse(hpAddArr[star]);
        }


        int atk = atkAll + levelDiff * atkAdd;
        int def = defAll + levelDiff * defAdd;
        int hp = hpAll + levelDiff * hpAdd;

        dataList.Add(atk);
        dataList.Add(def);
        dataList.Add(hp);
    }

    /// <summary>
    /// 获得属性ID
    /// </summary>
    /// <returns></returns>
    public int[] GetIDList()
    {
        string[] idStrArr = null;
        t_globalBean globalBean = null;
        switch (_curSelectEquipPos)
        {
            case EquipPosition.Weapon:
                globalBean = ConfigBean.GetBean<t_globalBean, int>(101);
                if (globalBean != null && !string.IsNullOrEmpty(globalBean.t_string_param))
                {
                    idStrArr = globalBean.t_string_param.Split('+');
                }
                break;
            case EquipPosition.Cloth:
                globalBean = ConfigBean.GetBean<t_globalBean, int>(102);
                if (globalBean != null && !string.IsNullOrEmpty(globalBean.t_string_param))
                {
                    idStrArr = globalBean.t_string_param.Split('+');
                }
                break;
            case EquipPosition.KuZi:
                globalBean = ConfigBean.GetBean<t_globalBean, int>(103);
                if (globalBean != null && !string.IsNullOrEmpty(globalBean.t_string_param))
                {
                    idStrArr = globalBean.t_string_param.Split('+');
                }
                break;
            case EquipPosition.Shoes:
                globalBean = ConfigBean.GetBean<t_globalBean, int>(104);
                if (globalBean != null && !string.IsNullOrEmpty(globalBean.t_string_param))
                {
                    idStrArr = globalBean.t_string_param.Split('+');
                }
                break;
            case EquipPosition.HuiZhan:
                globalBean = ConfigBean.GetBean<t_globalBean, int>(105);
                if (globalBean != null && !string.IsNullOrEmpty(globalBean.t_string_param))
                {
                    idStrArr = globalBean.t_string_param.Split('+');
                }
                break;
            case EquipPosition.MiJi:
                globalBean = ConfigBean.GetBean<t_globalBean, int>(106);
                if (globalBean != null && !string.IsNullOrEmpty(globalBean.t_string_param))
                {
                    idStrArr = globalBean.t_string_param.Split('+');
                }
                break;
            default:
                break;
        }

        // 获得属性ID
        int[] addIdArr = new int[idStrArr.Length];
        int count = addIdArr.Length;

        for (int i = 0; i < count; i++)
        {
            addIdArr[i] = int.Parse(idStrArr[i]);
        }

        return addIdArr;
    }


    private void SetSpecialEquipAttributeData(List<int> dataList, int color, int level, int star)
    {
        for (int i = 1; i <= color; i++)
        {
            SetSpecialCurColorData(dataList, i, level, star);
        }

        // 加上升星加成的属性值
        t_special_equip_starup_argBean specialEquipStarUpArgBean = GetSpecialEquipStarUpArgBean(star);
        if (specialEquipStarUpArgBean != null)
        {
            dataList[0] += specialEquipStarUpArgBean.t_add_shuXing;
        }
    }
    /// <summary>
    /// 获得特殊装备某一品某一星的属性值
    /// </summary>
    /// <param name="dataList"></param>
    /// <param name="color"></param>
    /// <param name="level"></param>
    /// <param name="star"></param>
    private void SetSpecialCurColorData(List<int> dataList, int color, int level, int star)
    {
        t_special_equip_lvcolorupBean specialEquipLvColorUpBean = GetSpecialEquipLvColorUpBean(color);
        t_special_equip_attrBean specialEquipAttrBean = GetSpecialEquipAttrBean(color);
        if (specialEquipAttrBean == null || specialEquipLvColorUpBean == null)
        {
            return;
        }

        int maxLevel = specialEquipLvColorUpBean.t_lv_max;
        int baseLevel = specialEquipLvColorUpBean.t_lv_base;
        int diffLevel = 0;
        if (maxLevel <= level)
        {
            diffLevel = maxLevel - baseLevel;
        }
        else
        {
            diffLevel = level - baseLevel;
        }

        int data = specialEquipAttrBean.t_add_v_list * diffLevel + specialEquipAttrBean.t_base_v_list;
        if (dataList.Count == 0)
        {
            dataList.Add(data);
        }
        else
        {
            dataList[0] += data;
        }
    }


    #region 升品-----------------------------------------------------------------------------------------------------
    /// <summary>
    /// 升品金币是否足够
    /// </summary>
    /// <returns></returns>
    public bool IsEnoughSPGold()
    {
        PetEquip petEquip = GetCurSelectPetEquip();
        int needGold = int.MaxValue;
        switch (_curSelectEquipPos)
        {
            case EquipPosition.Weapon:
            case EquipPosition.Cloth:
            case EquipPosition.KuZi:
            case EquipPosition.Shoes:
                needGold = GetNormalSPNeedGold(petEquip.color);
                break;
            case EquipPosition.HuiZhan:
            case EquipPosition.MiJi:
                needGold = GetSpecialSPNeedGold(petEquip.color);
                break;
            default:
                break;
        }
        RoleInfo roleInfo = RoleService.Singleton.GetRoleInfo();
        return roleInfo.gold >= needGold;
    }
    /// <summary>
    /// 装备等级是否达到升品的等级
    /// </summary>
    /// <param name="quality"></param>
    /// <returns></returns>
    public bool IsArriveSPLv(int quality)
    {
        int needLv = GetColorUpNeedEquipLv(quality);
        PetEquip petEquip = GetCurSelectPetEquip();

        return petEquip.level >= needLv;
    }
    /// <summary>
    /// 宠物等级是否可以升品
    /// </summary>
    /// <returns></returns>
    public bool PetLvIsCanColorUp(int quality)
    {
        t_equip_colorupBean equipColorUpBean = ConfigBean.GetBean<t_equip_colorupBean, int>(quality);
        if (equipColorUpBean == null)
            return false;

        //PetInfo petInfo = PetService.Singleton.GetPetByID(_curSelectPetID);
        RoleInfo roleInfo = RoleService.Singleton.GetRoleInfo();
        int limitLv = equipColorUpBean.t_lv_limit;
        limitLv += (int)CurSelectEquipPos;
        return roleInfo.level >= limitLv;
    }

    public int GetColorUpNeedRoleLv(int quality)
    {
        t_equip_colorupBean equipColorUpBean = ConfigBean.GetBean<t_equip_colorupBean, int>(quality);
        if (equipColorUpBean == null)
            return int.MaxValue;

        return equipColorUpBean.t_lv_limit + (int)CurSelectEquipPos;
    }
    /// <summary>
    /// 获得下一品需要的装备等级
    /// </summary>
    /// <returns></returns>
    public int GetColorUpNeedEquipLv(int quality)
    {
        int needLv = int.MaxValue;
        switch (_curSelectEquipPos)
        {
            case EquipPosition.Weapon:
            case EquipPosition.Cloth:
            case EquipPosition.KuZi:
            case EquipPosition.Shoes:
                needLv = GetNormalColorUpNeedLv(quality);
                break;
            case EquipPosition.HuiZhan:
            case EquipPosition.MiJi:
                needLv = GetSpecialColorUpNeedLv(quality);
                break;
            default:
                break;
        }

        return needLv;
    }
    /// <summary>
    /// 获得普通装备升当前品需要的装备等级
    /// </summary>
    /// <returns></returns>
    private int GetNormalColorUpNeedLv(int quality)
    {
        t_equip_colorupBean equipColorUpBean = ConfigBean.GetBean<t_equip_colorupBean, int>(quality);
        if (equipColorUpBean == null)
            return int.MaxValue;

        return equipColorUpBean.t_lv_max;
    }
    /// <summary>
    /// 获得特殊装备升当前品需要的装备等级
    /// </summary>
    /// <returns></returns>
    private int GetSpecialColorUpNeedLv(int quality)
    {
        t_special_equip_lvcolorupBean specialEquipLvColorUpBean = GetSpecialEquipLvColorUpBean(quality);
        if (specialEquipLvColorUpBean == null)
            return int.MaxValue;

        return specialEquipLvColorUpBean.t_lv_max;
    }

    /// <summary>
    /// 通过品阶获得相应的升品材料
    /// </summary>
    /// <param name="quality"></param>
    /// <returns></returns>
    public void SetSPMatreials(int quality, bool isClear = true)
    {
        // 获得id和数量
        if (isClear)
        {
            spMaterialsIDList.Clear();
            spMaterialsNumDict.Clear();
        }
        t_equip_colorupBean equipColorUpBean = ConfigBean.GetBean<t_equip_colorupBean, int>(quality);
        if (equipColorUpBean == null)
            return;
        string[] needItemInfos = GTools.splitString(equipColorUpBean.t_item, ';');
        if (needItemInfos == null)
            return;

        int count = needItemInfos.Length;
        int[] itemInfoArr = null;
        for (int i = 0; i < count; i++)
        {
            itemInfoArr = GTools.splitStringToIntArray(needItemInfos[i]);
            if (itemInfoArr.Length == 1)
                continue;

            int id = itemInfoArr[0];
            int num = itemInfoArr[1];
            if (spMaterialsIDList.Contains(id))
            {
                spMaterialsNumDict[id] += num;
            }
            else
            {
                spMaterialsIDList.Add(id);
                spMaterialsNumDict.Add(id, num);
            }
        }
        // 设置GridInfo
        spMaterialsGridInfoDict.Clear();
        count = spMaterialsIDList.Count;
        for (int i = 0; i < count; i++)
        {
            spMaterialsGridInfoDict.Add(spMaterialsIDList[i], null);
        }
        BagService.Singleton.SetGridInfoByIDs(spMaterialsGridInfoDict);
    }
    /// <summary>
    /// 获得升品需要的金币
    /// </summary>
    /// <returns></returns>
    public int GetNormalSPNeedGold(int quality)
    {
        t_equip_colorupBean equipColorUpBean = ConfigBean.GetBean<t_equip_colorupBean, int>(quality);
        if (equipColorUpBean == null)
            return 10000;

        return equipColorUpBean.t_gold;
    }

    public int GetSpecialSPNeedGold(int quality)
    {
        t_special_equip_lvcolorupBean specialEquipLvColorUpBean = GetSpecialEquipLvColorUpBean(quality);
        if (specialEquipLvColorUpBean == null)
            return int.MaxValue;

        string[] numArr = specialEquipLvColorUpBean.t_token_nums.Split('+');
        if (numArr.Length < 2)
            return int.MaxValue;

        return int.Parse(numArr[1]);
    }

    /// <summary>
    /// 获得升下一品需要的代币数量
    /// </summary>
    /// <returns></returns>
    public int GetNeedTokenNum(int quality)
    {
        t_special_equip_lvcolorupBean specialEquipLvColorUpBean = GetSpecialEquipLvColorUpBean(quality);
        if (specialEquipLvColorUpBean == null)
            return int.MaxValue;

        if (string.IsNullOrEmpty(specialEquipLvColorUpBean.t_token_nums))
            return int.MaxValue;
        string[] numArr = specialEquipLvColorUpBean.t_token_nums.Split('+');


        return int.Parse(numArr[0]);
    }
    /// <summary>
    /// 获得材料的需要数量
    /// </summary>
    /// <returns></returns>
    public int GetMaterialNeedNum(int itemID)
    {
        return spMaterialsNumDict[itemID];
    }
    /// <summary>
    /// 获得材料的拥有数量
    /// </summary>
    /// <returns></returns>
    public int GetMaterialHaveNum(int itemID)
    {
        Message.Bag.GridInfo gridInfo = spMaterialsGridInfoDict[itemID];
        int num = gridInfo == null ? 0 : gridInfo.itemInfo.num;
        return num;
    }
    /// <summary>
    /// 升品材料是否足够
    /// </summary>
    /// <returns></returns>
    public bool SPMaterilIsEnough()
    {
        int count = spMaterialsIDList.Count;
        int materialId;
        int materialHaveNum;
        int materialNeedNum;
        for (int i = 0; i < count; i++)
        {
            materialId = spMaterialsIDList[i];
            materialHaveNum = GetMaterialHaveNum(materialId);
            materialNeedNum = GetMaterialNeedNum(materialId);
            if (materialHaveNum < materialNeedNum)
            {
                return false;
            }
        }

        return true;
    }
    /// <summary>
    /// 升品的代币是否足够
    /// </summary>
    /// <returns></returns>
    public bool SPTokenIsEnough()
    {
        RoleInfo roleInfo = RoleService.Singleton.GetRoleInfo();
        PetEquip petEquip = GetCurSelectPetEquip();
        int needNum = GetNeedTokenNum(petEquip.color);
        if (_curSelectEquipPos == EquipPosition.HuiZhan)
        {
            return roleInfo.honorCurrency >= needNum;
        }
        else if(_curSelectEquipPos == EquipPosition.MiJi)
        {
            return roleInfo.trailCurrency >= needNum;
        }

        return false;
    }

    /// <summary>
    /// 获得需要购买的材料列表
    /// </summary>
    /// <returns></returns>
    public void SetNeedBuyMaterials()
    {
        needBuyItemList.Clear();
        int count = spMaterialsIDList.Count;
        int materialId;
        int materialHaveNum;
        int materialNeedNum;
        ItemInfo itemInfo = null;

        for (int i = 0; i < count; i++)
        {
            materialId = spMaterialsIDList[i];
            materialHaveNum = GetMaterialHaveNum(materialId);
            materialNeedNum = GetMaterialNeedNum(materialId);
            if (materialHaveNum < materialNeedNum)
            {
                itemInfo = new ItemInfo();
                itemInfo.id = materialId;
                itemInfo.num = materialNeedNum - materialHaveNum;
                needBuyItemList.Add(itemInfo);
            }
        }
    }
    /// <summary>
    /// 获得买材料的钻石
    /// </summary>
    /// <returns></returns>
    public int GetBuyMaterialDiamond()
    {
        int count = needBuyItemList.Count;
        int diamond = 0;
        t_itemBean itemBean = null;
        ItemInfo itemInfo = null;
        for (int i = 0; i < count; i++)
        {
            itemInfo = needBuyItemList[i];
            itemBean = ConfigBean.GetBean<t_itemBean, int>(itemInfo.id);
            if (itemBean != null)
            {
                diamond += int.Parse(itemBean.t_value) * itemInfo.num;
            }
        }

        return diamond;
    }
    /// <summary>
    /// 获得缺的材料里不能买的道具
    /// </summary>
    /// <returns></returns>
    public List<int> GetNoBuyMaterialList()
    {
        int count = needBuyItemList.Count;
        List<int> idList = new List<int>();

        int id = 0;
        t_itemBean itemBean = null;
        for (int i = 0; i < count; i++)
        {
            id = needBuyItemList[i].id;
            itemBean = ConfigBean.GetBean<t_itemBean, int>(id);
            if (itemBean != null)
            {
                int value = int.Parse(itemBean.t_value);
                if (value == 0)
                {
                    idList.Add(id);
                }
            }
        }

        return idList;
    }
    #endregion


    #region 升级----------------------------------------------------------------------------------------------------
    /// <summary>
    /// 装备升级金币是否足够
    /// </summary>
    /// <returns></returns>
    public bool IsEnoughUpgradeGold()
    {
        PetEquip petEquip = GetCurSelectPetEquip();
        int xiuZhenGold = GetLevelUpgradeGold(petEquip.level, petEquip.color);
        RoleInfo roleInfo = RoleService.Singleton.GetRoleInfo();
        return roleInfo.gold >= xiuZhenGold;
    }
    /// <summary>
    /// 获得当前等级升级需要的金币
    /// </summary>
    /// <returns></returns>
    public int GetLevelUpgradeGold(int level, int color)
    {
        t_equip_levelupBean equipLvBean = GetNormalEquipLvUpBean(color);
        t_equip_colorupBean equipColorUpBean = ConfigBean.GetBean<t_equip_colorupBean, int>(color);
        if (equipLvBean == null || equipColorUpBean == null)
            return int.MaxValue;

        int needGold = equipLvBean.t_gold_base + equipLvBean.t_gold_add * (level - equipColorUpBean.t_lv_base);

        return needGold;
    }
    /// <summary>
    /// 获得特殊装备的升级经验值
    /// </summary>
    /// <returns></returns>
    public int GetSpecialLvUpExp(int level, int color)
    {
        t_special_equip_lvcolorupBean specialEquipLvupBean = GetSpecialEquipLvColorUpBean(color);
        if (specialEquipLvupBean == null)
            return -1;

        int lvUpExp = specialEquipLvupBean.t_exp_base + specialEquipLvupBean.t_exp_add * (level - specialEquipLvupBean.t_lv_base);
        return lvUpExp;
    }

    public int GetCurColorMaxLevel(int color)
    {
        int maxLevel = int.MaxValue;
        switch (_curSelectEquipPos)
        {
            case EquipPosition.Weapon:
            case EquipPosition.Cloth:
            case EquipPosition.KuZi:
            case EquipPosition.Shoes:
                maxLevel = GetNormalEquipCurColorMaxLevel(color);
                break;
            case EquipPosition.HuiZhan:
            case EquipPosition.MiJi:
                maxLevel = GetSpecialEquipCurColorMaxLevel(color);
                break;
            default:
                break;
        }

        return maxLevel;
    }

    private int GetNormalEquipCurColorMaxLevel(int color)
    {
        t_equip_colorupBean equipColorUpBean = ConfigBean.GetBean<t_equip_colorupBean, int>(color);
        if (equipColorUpBean == null)
            return int.MaxValue;

        return equipColorUpBean.t_lv_max;
    }

    private int GetSpecialEquipCurColorMaxLevel(int color)
    {
        t_special_equip_lvcolorupBean equipColorUpBean = GetSpecialEquipLvColorUpBean(color);
        if (equipColorUpBean == null)
            return int.MaxValue;

        return equipColorUpBean.t_lv_max;
    }
    #endregion


    #region 表格获取----------------------------------------------------------------------------------------------------------

    /// <summary>
    /// 获得普通装备升级需求表
    /// </summary>
    /// <returns></returns>
    private t_equip_levelupBean GetNormalEquipLvUpBean(int color)
    {
        t_petBean petBean = ConfigBean.GetBean<t_petBean, int>(_curSelectPetID);
        if (petBean == null)
        {
            return null;
        }

        int id = color * 1000 + petBean.t_zizhi * 10 + (int)_curSelectEquipPos;
        t_equip_levelupBean equipLvUpBean = ConfigBean.GetBean<t_equip_levelupBean, int>(id);

        return equipLvUpBean;
    }

    /// <summary>
    /// 获得普通装备属性表
    /// </summary>
    /// <param name="color"></param>
    /// <returns></returns>
    public t_equip_attrBean GetEquipAttrBean(int color)
    {
        int id = _curSelectType * 100 + color;

        t_equip_attrBean equipAttrBean = ConfigBean.GetBean<t_equip_attrBean, int>(id);
        return equipAttrBean;
    }

    /// <summary>
    ///  获得特殊装备升级升品表
    /// </summary>
    /// <param name="color"></param>
    /// <returns></returns>
    public t_special_equip_lvcolorupBean GetSpecialEquipLvColorUpBean(int color)
    {
        int id = (int)_curSelectEquipPos + color * 100;
        return ConfigBean.GetBean<t_special_equip_lvcolorupBean, int>(id);
    }
    /// <summary>
    /// 获得特殊装备升星属性表
    /// </summary>
    /// <param name="star"></param>
    /// <returns></returns>
    public t_special_equip_starup_argBean GetSpecialEquipStarUpArgBean(int star)
    {

        int id = star * 100 + (int)_curSelectEquipPos;
        return ConfigBean.GetBean<t_special_equip_starup_argBean, int>(id);
    }

    public t_special_equip_attrBean GetSpecialEquipAttrBean(int color)
    {
        int id = color * 100 + _curSelectType + (int)_curSelectEquipPos;
        return ConfigBean.GetBean<t_special_equip_attrBean, int>(id);
    }

    #endregion;



    #region 快速升级 -----------------------------------------------------------------------------------------------------------

    public void InitQuickUpgradeDefaultData()
    {
        _totalGold = 0;
        _totalTokenNum = 0;
        _useTotalExp = 0;
        _totalDiamond = 0;
        _targetColor = 0;
        _targetLevel = 0;

        spMaterialsIDList.Clear();
        spMaterialsNumDict.Clear();
        spMaterialsGridInfoDict.Clear();
        quickUpgradeUseExpItemDict.Clear();

        switch (_curSelectEquipPos)
        {
            case EquipPosition.Weapon:
            case EquipPosition.Cloth:
            case EquipPosition.KuZi:
            case EquipPosition.Shoes:
                SetNormalEquipDefaultData();
                break;
            case EquipPosition.HuiZhan:
            case EquipPosition.MiJi:
                SetSpecailEquipDefaultData();
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 设置普通装备快速升级的默认数据,等级，金币，品阶,使用的道具
    /// </summary>
    private void SetNormalEquipDefaultData()
    {
        PetEquip petEquip = GetCurSelectPetEquip();
        _targetColor = petEquip.color;
        _targetLevel = petEquip.level;
        while ((_targetColor < PetService.Singleton.GetMaxColor()) && PetLvIsCanColorUp(_targetColor))
        {
            SetSPMatreials(_targetColor, false);
            if (SPMaterilIsEnough())
            {
                // 加上升品的金币
                _totalGold += GetNormalSPNeedGold(_targetColor);
                // 加上升级的金币
                _totalGold += GetToMaxLevelNeedGold(_targetColor, _targetLevel);

                _targetLevel = GetColorUpNeedEquipLv(_targetColor);
                _targetColor++;
                if (_targetColor >= PetService.Singleton.GetMaxColor())
                    break;
            }
            else
            {
                RemoveColorMaterials(_targetColor);
                break;
            }
        }

        _totalGold += GetToMaxLevelNeedGold(_targetColor, _targetLevel);
        _targetLevel = GetColorUpNeedEquipLv(_targetColor);
    }

    private void SetSpecailEquipDefaultData()
    {
        SetSpecialDefaultTargetLevel();
        CalculateUseExpItems(_useTotalExp);
        SetSpecialQuickUpgradeGold();
        SetSpecialQUNeedToken();
    }

    private void SetSpecialQUNeedToken()
    {
        PetEquip petEquip = GetCurSelectPetEquip();
        for (int i = petEquip.color; i < _targetColor; i++)
        {
            _totalTokenNum += GetNeedTokenNum(i);
        }
    }

    /// <summary>
    /// 获得特殊装备的默认显示等级
    /// </summary>
    /// <returns></returns>
    public void SetSpecialDefaultTargetLevel()
    {
        // 计算出当前等级能达到的最大品阶(品阶没限制)
        // 计算每一品需要的总经验，然后从小到大去使用经验道具
        PetEquip petEquip = GetCurSelectPetEquip();
        int targetLv = petEquip.level;
        int maxColor = PetService.Singleton.GetMaxColor();
        int tempExp = 0;
        int haveTotalExp = GetCurHaveTotalExp();
        t_special_equip_lvcolorupBean specialEquipLvColorBean = null;
        for (int i = petEquip.color; i <= maxColor; i++)
        {
            tempExp += GetToMaxLevelNeedExp(i, targetLv);
            if (haveTotalExp < tempExp)
            {
                _targetColor = i;
                break;
            }
            specialEquipLvColorBean = GetSpecialEquipLvColorUpBean(i);
            if (specialEquipLvColorBean != null)
            {
                targetLv = specialEquipLvColorBean.t_lv_max;
            }
            _targetColor = i;
            _useTotalExp = tempExp;
        }

        _targetLevel = GetColorUpNeedEquipLv(_targetColor);
    }
    /// <summary>
    /// 当前拥有的经验总值
    /// </summary>
    /// <returns></returns>
    public int GetCurHaveTotalExp()
    {
        int[] ids = _curSelectEquipPos == EquipPosition.HuiZhan ? specialXunZhangIDs : specialMiJiIDs;
        int count = ids.Length;
        int id = 0;
        int totalExp = 0;
        for (int i = 0; i < count; i++)
        {
            id = ids[i];
            Message.Bag.GridInfo gridInfo = specialExpGridInfoDict[id];
            if (gridInfo != null)
            {
                t_itemBean itemBean = ConfigBean.GetBean<t_itemBean, int>(gridInfo.itemInfo.id);
                string[] valueArr = itemBean.t_value.Split('+');
                totalExp += int.Parse(valueArr[0]) * gridInfo.itemInfo.num;
            }
        }

        return totalExp;
    }
    /// <summary>
    /// 计算使用的经验道具
    /// </summary>
    /// <returns></returns>
    private void CalculateUseExpItems(int useExp)
    {
        int[] ids = _curSelectEquipPos == EquipPosition.HuiZhan ? specialXunZhangIDs : specialMiJiIDs;
        int count = ids.Length;
        int id;
        int perExp = 0;
        int useNum = 0;

        quickUpgradeUseExpItemDict.Clear();

        Message.Bag.GridInfo gridInfo;
        t_itemBean itemBean;

        for (int i = 0; i < count ; i++)
        {
            id = ids[i];
            useNum = 0;
            gridInfo = specialExpGridInfoDict[id];
            itemBean = ConfigBean.GetBean<t_itemBean, int>(id);
            if(itemBean != null)
                 perExp = int.Parse(itemBean.t_value.Split('+')[0]);

            int num = gridInfo == null ? 0 : gridInfo.itemInfo.num;
            for (int j = 0; j < num; j++)
            {
                useExp -= perExp;
                useNum++;
                if (useExp <= 0)
                {
                    if (quickUpgradeUseExpItemDict.ContainsKey(id))
                    {
                        quickUpgradeUseExpItemDict[id] += useNum;
                    }
                    else
                    {
                        quickUpgradeUseExpItemDict.Add(id, useNum);
                    }

                    return;
                }
            }
            if(useNum != 0)
                quickUpgradeUseExpItemDict.Add(id, useNum);
        }
    }

    /// <summary>
    /// 获得当前宠物等级能到达的最大品质
    /// </summary>
    /// <returns></returns>
    public int GetMaxColor()
    {
        PetEquip petEquip = GetCurSelectPetEquip();
        int maxColor = petEquip.color;
        int maxLevel = GetColorUpNeedEquipLv(maxColor);
        //PetInfo petInfo = PetService.Singleton.GetPetByID(_curSelectPetID);
        RoleInfo roleInfo = RoleService.Singleton.GetRoleInfo();
        int petLevel = roleInfo.level;
        while (petLevel >= maxLevel)
        {
            maxColor++;
            maxLevel = GetColorUpNeedEquipLv(maxColor);
        }

        return maxColor;
    }
    /// <summary>
    /// 获得从当前等级升到当前品的最大等级需要的总经验值
    /// </summary>
    /// <returns></returns>
    public int GetToMaxLevelNeedExp(int color, int baseLevel)
    {
        t_special_equip_lvcolorupBean specialEquipLvColorBean = GetSpecialEquipLvColorUpBean(color);
        int totalExp = 0;
        if (specialEquipLvColorBean != null)
        {
            for (int i = baseLevel; i < specialEquipLvColorBean.t_lv_max; i++)
            {
                totalExp += GetSpecialLvUpExp(i, color);
            }
        }

        return totalExp;
    }

    private int GetToMaxLevelNeedGold(int color, int baseLevel)
    {
        t_equip_colorupBean equipColorUpBean = ConfigBean.GetBean<t_equip_colorupBean, int>(color);
        if (equipColorUpBean == null)
            return 1000000;

        int needGold = 0;
        for (int i = baseLevel; i < equipColorUpBean.t_lv_max; i++)
        {
            needGold += GetLevelUpgradeGold(i, color);
        }

        return needGold;
    }
    
    /// <summary>
    /// 获得特殊装备快速升级使用的金币数量
    /// </summary>
    /// <returns></returns>
    public void SetSpecialQuickUpgradeGold()
    {
        // 使用了多少的经验道具*单价 + 升品消耗的
        int perPrice = 0;
        t_itemBean itemBean;
        _totalGold = 0;
        foreach (var key in quickUpgradeUseExpItemDict.Keys)
        {
            itemBean = ConfigBean.GetBean<t_itemBean, int>(key);
            if (itemBean != null)
            {
                perPrice = int.Parse(itemBean.t_value.Split('+')[1]);
                _totalGold += perPrice * quickUpgradeUseExpItemDict[key];
            }
        }

        PetEquip petEquip = GetCurSelectPetEquip();
        for (int i = petEquip.color; i <= _targetColor; i++)
        {
            _totalGold += GetSpecialSPNeedGold(i);
        }
    }
    /// <summary>
    /// 获得特殊装备默认显示时的代币数量
    /// </summary>
    /// <returns></returns>
    public int GetSpecialTokenNum()
    {
        int totalTokenNum = 0;
        PetEquip petEquip = GetCurSelectPetEquip();
        for (int i = petEquip.color; i <= _targetColor; i++)
        {
            totalTokenNum += GetNeedTokenNum(i);
        }

        return totalTokenNum;
    }
    /// <summary>
    /// 快速升级的经验值是否足
    /// </summary>
    /// <returns></returns>
    public bool IsEnoughExp()
    {
        int haveTotalExp = GetCurHaveTotalExp();
        return haveTotalExp >= _useTotalExp;
    }

    public bool IsEnoughTargetLevelExp()
    {
        PetEquip petEquip = GetCurSelectPetEquip();
        t_special_equip_lvcolorupBean equipColorUpBean = GetSpecialEquipLvColorUpBean(_targetColor);
        if (equipColorUpBean == null)
        {
            return false;
        }

        int needExp = GetToMaxLevelNeedExp(_targetColor, equipColorUpBean.t_lv_base);
        int haveTotalExp = GetCurHaveTotalExp();
        return haveTotalExp >= needExp;
    }
    /// <summary>
    /// 快速升级的金币是否足够
    /// </summary>
    /// <returns></returns>
    public bool IsQUEnoughGold()
    {
        RoleInfo roleInfo = RoleService.Singleton.GetRoleInfo();
        return roleInfo.gold >= _totalGold;
    }
    /// <summary>
    /// 是否达到最大品了
    /// </summary>
    /// <returns></returns>
    public bool IsReachMaxColor()
    {
        PetEquip petEquip = GetCurSelectPetEquip();
        if (petEquip != null)
        {
            int maxColor = PetService.Singleton.GetMaxColor();
            return petEquip.color >= maxColor;
        }

        return true;
    }
    /// <summary>
    /// 快速升级的金币是否足够
    /// </summary>
    /// <returns></returns>
    public bool IsQUEnoughDiamond()
    {
        RoleInfo roleInfo = RoleService.Singleton.GetRoleInfo();
        return roleInfo.damond >= _totalDiamond;
    }

    /// <summary>
    /// 快速升级的代币是否足够
    /// </summary>
    /// <returns></returns>
    public bool IsQUEnoughTokenNum()
    {
        RoleInfo roleInfo = RoleService.Singleton.GetRoleInfo();
        int haveTokenNum = _curSelectEquipPos == EquipPosition.MiJi ? roleInfo.trailCurrency : roleInfo.honorCurrency;
        return haveTokenNum >= _totalTokenNum;
    }
   
    /// <summary>
    /// 是否能减少等级
    /// </summary>
    /// <returns></returns>
    public bool IsCanReduceColorLevel()
    {
        int colorLevel = colorLevel = GetLevelInColor(_targetColor);
        PetEquip petEquip = GetCurSelectPetEquip();
        if ((_targetLevel - colorLevel) <= petEquip.level || (_targetLevel - colorLevel) <= 1)
        {
            return false;
        }
        return true;
    }
    /// <summary>
    /// 是否能增加等级
    /// </summary>
    /// <returns></returns>
    public bool IsCanAddColorLevel()
    {
        bool isCanAdd = false;
        switch (_curSelectEquipPos)
        {
            case EquipPosition.Weapon:
            case EquipPosition.Cloth:
            case EquipPosition.KuZi:
            case EquipPosition.Shoes:
                isCanAdd = IsNoramlCanAddColorLevel();
                break;
            case EquipPosition.HuiZhan:
            case EquipPosition.MiJi:
                isCanAdd = IsSpecialCanAddColorLevel();
                break;
            default:
                break;
        }

        return isCanAdd;
    }

    private bool IsNoramlCanAddColorLevel()
    {
        int maxColor = GetMaxColor();
        t_equip_colorupBean equipColorUpBean = ConfigBean.GetBean<t_equip_colorupBean, int>(maxColor);
        if (equipColorUpBean == null)
            return false;

        return equipColorUpBean.t_lv_max > _targetLevel;
    }

    private bool IsSpecialCanAddColorLevel()
    {
        t_special_equip_lvcolorupBean specialEquipLvColorUpBean = GetSpecialEquipLvColorUpBean(_targetColor);
        if (specialEquipLvColorUpBean == null)
            return false;
        // 判断当前品是否是最大品
        int maxColor = PetService.Singleton.GetMaxColor();
        if (maxColor <= _targetColor)
            return false;

        // 判断是当前品阶的最大等级还是最小等级
        int addExp = 0;
        int maxLevel = specialEquipLvColorUpBean.t_lv_max;
        if (_targetLevel == maxLevel)
        {
            // 经验值计算是使用下一品的
            addExp = GetToMaxLevelNeedExp(_targetColor + 1, maxLevel);
        }
        else
        {
            // 经验值计算使用这一品的
            addExp = GetToMaxLevelNeedExp(_targetColor, specialEquipLvColorUpBean.t_lv_max);
        }
        int haveTotalExp = GetCurHaveTotalExp();
        return haveTotalExp >= (_useTotalExp + addExp);
    }

    /// <summary>
    /// 减少一个品阶等级
    /// </summary>
    /// <returns></returns>
    public void ReduceOneColorLevel()
    {
        switch (_curSelectEquipPos)
        {
            case EquipPosition.Weapon:
            case EquipPosition.Cloth:
            case EquipPosition.KuZi:
            case EquipPosition.Shoes:
                NormalReduceOnColorLevel();
                break;
            case EquipPosition.HuiZhan:
            case EquipPosition.MiJi:
                SpecialReduceOnColorLevel();
                break;
            default:
                break;
        }
    }

    private void NormalReduceOnColorLevel()
    {
        // 判断当前等级是这个品阶的最大等级还是最小等级
        t_equip_colorupBean equipColorUpBean = ConfigBean.GetBean<t_equip_colorupBean, int>(_targetColor);
        if (equipColorUpBean == null)
            return;

        int colorLevel = 0;
        int baseLevel = 0;


        // 如果是当前品的最小等级，那么即减少等级还减少品阶
        equipColorUpBean = ConfigBean.GetBean<t_equip_colorupBean, int>(_targetColor);
        if (equipColorUpBean == null)
            return;

        baseLevel = equipColorUpBean.t_lv_base;
        _totalGold -= GetToMaxLevelNeedGold(_targetColor, baseLevel);
        colorLevel = GetLevelInColor(_targetColor);

        _targetColor--;
        RemoveColorMaterials(_targetColor);
        SetNeedBuyMaterial();

        _totalGold -= GetNormalSPNeedGold(_targetColor);

        _targetLevel -= colorLevel;

    }

    private void SpecialReduceOnColorLevel()
    {
        // 判断当前等级是这个品阶的最大等级还是最小等级
        t_special_equip_lvcolorupBean specialEquipLvColorUpBean = GetSpecialEquipLvColorUpBean(_targetColor);
        if (specialEquipLvColorUpBean == null)
            return;

        //int maxLevel = specialEquipLvColorUpBean.t_lv_max;
        int colorLevel = 0;
        int baseLevel = 0;
        //if (_targetLevel == maxLevel)
        //{
        //    // 如果是当前品的最大等级，那么只减少等级
        //    colorLevel = GetLevelInColor(_targetColor);
        //    baseLevel = specialEquipLvColorUpBean.t_lv_base;
        //    _useTotalExp -= GetToMaxLevelNeedExp(_targetColor, baseLevel);
        //    CalculateUseExpItems(_useTotalExp);
        //    SetSpecialQuickUpgradeGold();
        //    _targetLevel -= colorLevel;
        //}
        //else
        //{
        //    // 如果是当前品的最小等级，那么即减少等级还减少品阶
        //    _targetColor--;
        //    colorLevel = GetLevelInColor(_targetColor);
        //    baseLevel = specialEquipLvColorUpBean.t_lv_base;
        //    _useTotalExp -= GetToMaxLevelNeedExp(_targetColor, baseLevel);
        //    CalculateUseExpItems(_useTotalExp);
        //    SetSpecialQuickUpgradeGold();
        //    _targetLevel -= colorLevel;
        //    _totalTokenNum -= GetNeedTokenNum(_targetColor);
        //}

        // 如果是当前品的最小等级，那么即减少等级还减少品阶

        colorLevel = GetLevelInColor(_targetColor);
        baseLevel = specialEquipLvColorUpBean.t_lv_base;
        _useTotalExp -= GetToMaxLevelNeedExp(_targetColor, baseLevel);
        _targetColor--;
        CalculateUseExpItems(_useTotalExp);
        SetSpecialQuickUpgradeGold();
        _targetLevel -= colorLevel;
        _totalTokenNum -= GetNeedTokenNum(_targetColor);
    }
    /// <summary>
    /// 移除当前升品需要的材料
    /// </summary>
    /// <param name="color"></param>
    private void RemoveColorMaterials(int color)
    {
        // 获得id和数量
        t_equip_colorupBean equipColorUpBean = ConfigBean.GetBean<t_equip_colorupBean, int>(color);
        if (equipColorUpBean == null)
            return;

        string[] itemInfos = GTools.splitString(equipColorUpBean.t_item, ';');
        if (itemInfos == null)
            return;

        int count = itemInfos.Length;
        int[] itemInfoArr = null;
        for (int i = 0; i < count; i++)
        {
            itemInfoArr = GTools.splitStringToIntArray(itemInfos[i]);
            if (itemInfoArr.Length == 1)
                continue;

            int id = itemInfoArr[0];
            int num = itemInfoArr[1];
            if (spMaterialsIDList.Contains(id))
            {
                spMaterialsNumDict[id] -= num;
                if (spMaterialsNumDict[id] <= 0)
                {
                    spMaterialsNumDict.Remove(id);
                    spMaterialsIDList.Remove(id);
                }
            }
        }
        // 设置GridInfo
        count = spMaterialsIDList.Count;
        spMaterialsGridInfoDict.Clear();
        for (int i = 0; i < count; i++)
        {
            spMaterialsGridInfoDict.Add(spMaterialsIDList[i], null);
        }
        BagService.Singleton.SetGridInfoByIDs(spMaterialsGridInfoDict);
    }

    /// <summary>
    /// 获得当前品阶中的等级数
    /// </summary>
    /// <param name="color"></param>
    /// <returns></returns>
    private int GetLevelInColor(int color)
    {
        int colorLevel = 5;
        
        switch (_curSelectEquipPos)
        {
            case EquipPosition.Weapon:
            case EquipPosition.Cloth:
            case EquipPosition.KuZi:
            case EquipPosition.Shoes:
                colorLevel = GetNormalEquipLevelInColor(color);
                break;
            case EquipPosition.HuiZhan:
            case EquipPosition.MiJi:
                colorLevel = GetSpecialEquipLevelInColor(color);
                break;
            default:
                break;
        }
        return colorLevel;
    }

    private int GetNormalEquipLevelInColor(int color)
    {
        t_equip_colorupBean equipColorBean = ConfigBean.GetBean<t_equip_colorupBean, int>(color);
        if (equipColorBean == null)
            return 5;

        return equipColorBean.t_lv_max - equipColorBean.t_lv_base;
    }

    private int GetSpecialEquipLevelInColor(int color)
    {
        t_special_equip_lvcolorupBean specialLvColorUpBean = GetSpecialEquipLvColorUpBean(color);
        if (specialLvColorUpBean == null)
            return 5;

        return specialLvColorUpBean.t_lv_max - specialLvColorUpBean.t_lv_base;
    }

    /// <summary>
    /// 增加一个品阶的等级
    /// </summary>
    /// <returns></returns>
    public void AddOnColorLevel()
    {
        switch (_curSelectEquipPos)
        {
            case EquipPosition.Weapon:
            case EquipPosition.Cloth:
            case EquipPosition.KuZi:
            case EquipPosition.Shoes:
                NormalAddOnColorLevel();
                break;
            case EquipPosition.HuiZhan:
            case EquipPosition.MiJi:
                SpecialAddOneColorLevel();
                break;
            default:
                break;
        }
    }

    private void NormalAddOnColorLevel()
    {
        // 判断当前等级是这个品阶的最大等级还是最小等级
        t_equip_colorupBean equipColorUpBean = ConfigBean.GetBean<t_equip_colorupBean, int>(_targetColor);
        if (equipColorUpBean == null)
            return;

        int maxLevel = equipColorUpBean.t_lv_max;
        int colorLevel = 0;
        int baseLevel = 0;
        if (_targetLevel == maxLevel)
        {
            // 如果是当前品的最大等级，那么即增加等级，也加品
            SetSPMatreials(_targetColor, false);
            SetNeedBuyMaterial();
            _totalGold += GetNormalSPNeedGold(_targetColor);
            _targetColor++;
            colorLevel = GetLevelInColor(_targetColor);
            equipColorUpBean = ConfigBean.GetBean<t_equip_colorupBean, int>(_targetColor);
            if (equipColorUpBean == null)
                return;

            baseLevel = equipColorUpBean.t_lv_base;
            _totalGold += GetToMaxLevelNeedGold(_targetColor, baseLevel);
            _targetLevel += colorLevel;
        }
        else
        {
            // 如果是当前品的最小等级，那么值增加等级
            colorLevel = GetLevelInColor(_targetColor);
            _totalGold += GetToMaxLevelNeedGold(_targetColor, _targetLevel);
            _targetLevel += colorLevel;
        }

    }

    private void SpecialAddOneColorLevel()
    {
        // 判断当前等级是这个品阶的最大等级还是最小等级
        t_special_equip_lvcolorupBean specialEquipLvColorUpBean = GetSpecialEquipLvColorUpBean(_targetColor);
        if (specialEquipLvColorUpBean == null)
            return;

        int maxLevel = specialEquipLvColorUpBean.t_lv_max;
        int colorLevel = 0;
        int baseLevel = 0;
        int haveTotalExp = GetCurHaveTotalExp();
        if (maxLevel == _targetLevel)
        {
            // 如果是当前品的最大等级，那么即增加等级，也加品
            _targetColor++;
            specialEquipLvColorUpBean = GetSpecialEquipLvColorUpBean(_targetColor);
            if (specialEquipLvColorUpBean == null)
                return;

            baseLevel = specialEquipLvColorUpBean.t_lv_base;
            colorLevel = GetLevelInColor(_targetColor);

            _targetLevel += colorLevel;
            _useTotalExp += GetToMaxLevelNeedExp(_targetColor, baseLevel);
            _totalTokenNum += GetNeedTokenNum(_targetColor - 1);
            CalculateUseExpItems(_useTotalExp);
            SetSpecialQuickUpgradeGold();
        }
        else
        {
            // 如果是当前品的最小等级，那么值增加等级
            baseLevel = specialEquipLvColorUpBean.t_lv_base;
            colorLevel = GetLevelInColor(_targetColor);

            _targetLevel += colorLevel;
            _useTotalExp += GetToMaxLevelNeedExp(_targetColor, baseLevel);
            CalculateUseExpItems(_useTotalExp);
            SetSpecialQuickUpgradeGold();
        }
    }

    private void SetNeedBuyMaterial()
    {
        int count = spMaterialsIDList.Count;
        int materialId;
        int materialHaveNum;
        int materialNeedNum;
        t_itemBean itemBean = null;
        _totalDiamond = 0;
        for (int i = 0; i < count; i++)
        {
            materialId = spMaterialsIDList[i];
            materialHaveNum = GetMaterialHaveNum(materialId);
            materialNeedNum = GetMaterialNeedNum(materialId);
            if (materialHaveNum < materialNeedNum)
            {
                itemBean = ConfigBean.GetBean<t_itemBean, int>(materialId);
                if (itemBean == null)
                    return;

                if (itemBean.t_buy_price == 0)
                {
                    break;
                }

                _totalDiamond += (materialNeedNum - materialHaveNum) * itemBean.t_buy_price;
            }
        }

        
    }



    #endregion


    #region   请求-----------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// 请求普通装备升级
    /// </summary>
    public void ReqNormalEquipUpgrade(int targetLevel)
    {
        int equipIndex = (int)CurSelectEquipPos;
        PetService.Singleton.ReqNormalEquipUpgrade(_curSelectPetID, equipIndex, targetLevel);
    }
    /// <summary>
    /// 请求特殊装备升级
    /// </summary>
    public void ReqSpecialEquipUpgrade(int itemID, int num)
    {
        Message.Bag.GridInfo gridInfo = specialExpGridInfoDict[itemID];
        PetService.Singleton.ReqSpecialEquipUpgrade(_curSelectPetID, (int)_curSelectEquipPos, gridInfo.id, num);
    }
    /// <summary>
    /// 请求普通装备升品
    /// </summary>
    public void ReqNormalEquipSP()
    {
        List<Message.Pet.GridInfo> gridInfoList = new List<Message.Pet.GridInfo>();

        int count = spMaterialsIDList.Count;
        Message.Pet.GridInfo gridInfo = null;
        for (int i = 0; i < count; i++)
        {
            gridInfo = new Message.Pet.GridInfo();
            int id = spMaterialsIDList[i];
            if (spMaterialsNumDict.ContainsKey(id))
            {
                if (spMaterialsGridInfoDict[id] != null)
                {
                    gridInfo.gridId = spMaterialsGridInfoDict[id].id;
                    gridInfo.num = spMaterialsNumDict[id];
                    gridInfoList.Add(gridInfo);
                }
                
            }

        }

        PetService.Singleton.ReqNormalEquipSP(_curSelectPetID, (int)_curSelectEquipPos, gridInfoList);
    }
    /// <summary>
    /// 请求特殊装备升品
    /// </summary>
    public void ReqSpecialEquipSP()
    {
        PetService.Singleton.ReqSpecialEquipSP(_curSelectPetID, (int)_curSelectEquipPos);
    }



    /// <summary>
    /// 请求装备快速升级
    /// </summary>
    public void ReqQuickUpgrade()
    {
        switch (_curSelectEquipPos)
        {
            case EquipPosition.Weapon:
            case EquipPosition.Cloth:
            case EquipPosition.KuZi:
            case EquipPosition.Shoes:
                ReqNormalFastLvUp();
                break;
            case EquipPosition.HuiZhan:
            case EquipPosition.MiJi:
                ReqSpecialFastLvUp();
                break;
            default:
                break;
        }
    }

    private void ReqNormalFastLvUp()
    {
        List<Message.Pet.GridInfo> gridInfoList = new List<Message.Pet.GridInfo>();
        int count = spMaterialsIDList.Count;
        int id;
        int num;
        int gridID;

        Message.Pet.GridInfo gridInfo = null;

        for (int i = 0; i < count; i++)
        {
            id = spMaterialsIDList[i];
            num = GetMaterialHaveNum(id);
            if (spMaterialsGridInfoDict.ContainsKey(id))
            {
                if (spMaterialsGridInfoDict[id] != null)
                {
                    gridID = spMaterialsGridInfoDict[id].id;

                    gridInfo = new Message.Pet.GridInfo();
                    gridInfo.gridId = gridID;
                    gridInfo.num = num;

                    gridInfoList.Add(gridInfo);
                }
            } 
        }

        PetService.Singleton.ReqNormalFastLvUp(_curSelectPetID, (int)_curSelectEquipPos, _targetLevel, gridInfoList);
    }

    private void ReqSpecialFastLvUp()
    {
        List<Message.Pet.GridInfo> gridInfoList = new List<Message.Pet.GridInfo>();
        int id;
        int num;
        int gridID;

        int[] ids = _curSelectEquipPos == EquipPosition.HuiZhan ? specialXunZhangIDs : specialMiJiIDs;
        int count = ids.Length;

        Message.Pet.GridInfo gridInfo = null;

        for (int i = 0; i < count; i++)
        {
            id = ids[i];
            if (quickUpgradeUseExpItemDict.ContainsKey(id))
            {
                num = quickUpgradeUseExpItemDict[id];
                if (num == 0)
                    continue;
                gridID = specialExpGridInfoDict[id].id;

                gridInfo = new Message.Pet.GridInfo();
                gridInfo.gridId = gridID;
                gridInfo.num = num;

                gridInfoList.Add(gridInfo);
            }
            
        }

        PetService.Singleton.ReqSpecialFastLvUp(_curSelectPetID, (int)_curSelectEquipPos, _targetLevel, gridInfoList);
    }


    #endregion
}
