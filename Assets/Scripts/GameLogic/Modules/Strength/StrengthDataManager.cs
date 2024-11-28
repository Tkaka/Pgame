using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Message.Pet;
using Message.Bag;
using Data.Beans;

public enum StrengthType
{
    None = -1,               
    ShengPing = 0,             // 升品
    ShengJi = 1,               // 升级
    StarUp = 2,                // 升星
    JiNeng = 3,                // 技能
    ZhanHun = 4,               // 战魂
}

public class StrengthDataManager {

    private PetInfo _curSelectPetInfo;
    private StrengthType _curSelecType;
    /// <summary>
    /// 强化前的宠物属性
    /// </summary>
    private PetInfo _oldPetInfo;
    /// <summary>
    /// 经验药所需
    /// </summary>
    private Dictionary<int, Message.Bag.GridInfo> _expPropDict = new Dictionary<int, Message.Bag.GridInfo>();
    private int[] _expPropIDs = { 407001, 407002, 407003, 407004, 407005, 407006 };
    private int _curUseExpItemIndex;
    /// <summary>
    /// 升品材料所需
    /// </summary>
    private Dictionary<int, Message.Bag.GridInfo> _caiLiaoPropDcit = new Dictionary<int, Message.Bag.GridInfo>();
    private List<int> _caiLiaoList = new List<int>();
    private List<int> _caiLiaoNumList = new List<int>();
    /// <summary>
    /// 当前选择的战魂index
    /// </summary>
    public int selectZhanHunIndex { get; set; }

    public PetInfo CurSelectPetInfo
    {
        get { return _curSelectPetInfo; }
        set
        {
            if (_curSelectPetInfo == value)
            {
                return;
            }

            _curSelectPetInfo = value;
        }
    }

    public PetInfo OldPetInfo
    {
        get { return _oldPetInfo; }
    }

    public StrengthType CurSelectType
    {
        get { return _curSelecType; }
        set
        {
            _curSelecType = value;
        }
    }

    public int[] ExpPropIDs
    {
        get { return _expPropIDs; }
    }

    public Dictionary<int, Message.Bag.GridInfo> ExpPropDict
    {
        get { return _expPropDict; }
    }

    public int CurUseExpItemIndex
    {
        get { return _curUseExpItemIndex; }
        set
        {
            _curUseExpItemIndex = value;
        }
    }

    public void Init()
    {
        // 默认选择第一个
        selectZhanHunIndex = 0;
        SetExpPropDict();
        InitOldPetInfo();
    }

    private void InitOldPetInfo()
    {
        _oldPetInfo = new PetInfo();
        _oldPetInfo.basInfo = new PetBaseInfo();
        _oldPetInfo.fightInfo = new PetFightInfo();
    }

    public void Close()
    {
        _curSelectPetInfo = null;
        _expPropDict = null;
        _caiLiaoPropDcit = null;
        _caiLiaoList = null;
        _caiLiaoNumList = null;
        _expPropIDs = null;
    }

    /// <summary>
    /// 刷新
    /// </summary>
    public void UpdateOldPetInfo()
    {
        _oldPetInfo.petId = _curSelectPetInfo.petId;
        _oldPetInfo.basInfo.color = _curSelectPetInfo.basInfo.color;
        _oldPetInfo.basInfo.star = _curSelectPetInfo.basInfo.star;
        _oldPetInfo.fightInfo.atk = Mathf.FloorToInt(PetService.Singleton.GetPetPropertyValue(_oldPetInfo.petId, PropertyType.Atk));
        _oldPetInfo.fightInfo.def = Mathf.FloorToInt(PetService.Singleton.GetPetPropertyValue(_oldPetInfo.petId, PropertyType.Def));
        _oldPetInfo.fightInfo.hp = Mathf.FloorToInt(PetService.Singleton.GetPetPropertyValue(_oldPetInfo.petId, PropertyType.Hp));
        _oldPetInfo.fightInfo.fightPower = (int)PetService.Singleton.GetPetFightPower(_oldPetInfo.petId);
        _oldPetInfo.priority = _curSelectPetInfo.priority;
    }
    /// <summary>
    /// 获得初始的PetID，用于滚动滚动条到指定位置
    /// </summary>
    public int GetInitIndex()
    {
        List<PetInfo> petInfoList = PetService.Singleton.GetPetInfos(true);
        int num = petInfoList.Count;
        PetInfo petInfo;
        for (int i = 0; i < num; i++)
        {
            petInfo = petInfoList[i];
            if (petInfo == _curSelectPetInfo)
            {
                return i;
            }
        }

        return -1;
    }


    #region 升品
    public void SetCaiLiaoData()
    {

        SetCaiLiaoArr();
        SetCaiLiaoNumArr();
        SetCaiLiaoGridInfoDict();
    }
    /// <summary>
    /// 获得所需材料的所有数量
    /// </summary>
    /// <returns></returns>
    public int GetNeedCaiLiaoNum()
    {
        int num = 0;
        int count = _caiLiaoNumList.Count;
        for (int i = 0; i < count; i++)
        {
            num += _caiLiaoNumList[i];
        }

        return num;
    }
    /// <summary>
    /// 获得当前拥有的材料数量
    /// </summary>
    /// <returns></returns>
    public int GetCurMaterialNum()
    {
        int num = 0;
        int count = 0;
        int needNum = 0;
        int tempNum = 0;
        foreach (var value in _caiLiaoPropDcit.Values)
        {
            tempNum = 0;
            if(value != null)
            {
                tempNum = value.itemInfo.num;
                needNum = _caiLiaoNumList[count];
                tempNum = tempNum >= needNum ? needNum : tempNum;
            }
            num += tempNum;
            count++;
        }

        return num;
    }

    private void SetCaiLiaoArr()
    {
        // 设置材料数组
        _caiLiaoList.Clear();

        t_pet_colorup_costBean qualityBean = GetPetColorUpCostBean();
        if (qualityBean == null)
            return;

        _caiLiaoList.Add(qualityBean.t_drug_id);
        _caiLiaoList.Add(qualityBean.t_main_id);
        _caiLiaoList.Add(qualityBean.t_vice_id1);
        _caiLiaoList.Add(qualityBean.t_vice_id2);
    }

    private void SetCaiLiaoNumArr()
    {
        // 设置材料数量数组
        _caiLiaoNumList.Clear();

        t_pet_colorup_costBean qualityBean = GetPetColorUpCostBean();
        if (qualityBean == null)
            return;

        _caiLiaoNumList.Add(qualityBean.t_drug_num);
        _caiLiaoNumList.Add(qualityBean.t_main_num);
        _caiLiaoNumList.Add(qualityBean.t_vice_num);
        _caiLiaoNumList.Add(qualityBean.t_vice_num);
    }

    private void SetCaiLiaoGridInfoDict()
    {
        _caiLiaoPropDcit.Clear();
        int num = _caiLiaoList.Count;
        // 设置材料的格子信息
        for (int i = 0; i < num; i++)
        {
            _caiLiaoPropDcit.Add(_caiLiaoList[i], null);
        }

        BagService.Singleton.SetGridInfoByIDs(_caiLiaoPropDcit);
    }

    /// <summary>
    /// 根据道具的下标获得对应的格子信息
    /// </summary>
    /// <returns></returns>
    public Message.Bag.GridInfo GetGridInfoByIndex(int index)
    {
        if (_caiLiaoList.Count > index)
        {
            return _caiLiaoPropDcit[_caiLiaoList[index]];
        }
        return null;
    }
    /// <summary>
    /// 获得道具升品所需数量
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public int GetNumByIndex(int index)
    {
        if (_caiLiaoNumList.Count >= index)
        {
            return _caiLiaoNumList[index];
        }
        return int.MaxValue;
    }

    public int GetItemIDByIndex(int index)
    {
        if (_caiLiaoList.Count >= index)
        {
            return _caiLiaoList[index];
        }
        return int.MaxValue;
    }

    /// <summary>
    /// 是否满足等级条件
    /// </summary>
    /// <returns></returns>
    public bool IsEnoughLv()
    {
        t_petBean petBean = ConfigBean.GetBean<t_petBean, int>(_curSelectPetInfo.petId);
        if (petBean == null)
            return false;

        int lv = 0;
        switch (_curSelecType)
        {
            case StrengthType.ShengPing:
                lv = GetShengPinLvRequire();
                break;
            case StrengthType.ShengJi:
                break;
            default:
                break;
        }
        lv = GetShengPinLvRequire();
        return _curSelectPetInfo.basInfo.level >= lv;
    }
    /// <summary>
    /// 获得升品等级需求
    /// </summary>
    /// <returns></returns>
    public int GetShengPinLvRequire()
    {
        t_petBean petBean = ConfigBean.GetBean<t_petBean, int>(_curSelectPetInfo.petId);
        int qualityLvId = _curSelectPetInfo.basInfo.color * 1000 + petBean.t_zizhi * 10 + petBean.t_type;
        t_pet_colorup_limitBean qualityLvBean = ConfigBean.GetBean<t_pet_colorup_limitBean, int>(qualityLvId);
        if (petBean == null || qualityLvBean == null)
            return int.MaxValue;

        return qualityLvBean.t_level;
    }

    public bool IsEnoughGold()
    {
        var roleInfo = RoleService.Singleton.GetRoleInfo();
        t_petBean petBean = ConfigBean.GetBean<t_petBean, int>(_curSelectPetInfo.petId);
        if (petBean == null)
            return false;

        int gold = 0;
        switch (_curSelecType)
        {
            case StrengthType.ShengPing:
                t_pet_colorup_costBean petColorUpCostBean = GetPetColorUpCostBean();
                if (petColorUpCostBean == null)
                {
                    gold = int.MaxValue;
                }
                else
                {
                    gold = GetPetColorUpCostBean().t_gold;
                }
                break;
            case StrengthType.ShengJi:
                break;
            default:
                break;
        }

        return roleInfo.gold >= gold;
    }
    /// <summary>
    ///  获得升品消耗表
    /// </summary>
    /// <returns></returns>
    public t_pet_colorup_costBean GetPetColorUpCostBean()
    {
        t_petBean petBean = ConfigBean.GetBean<t_petBean, int>(_curSelectPetInfo.petId);
        int id = (_curSelectPetInfo.basInfo.color + 1) * 1000 + petBean.t_type * 100 + petBean.t_zizhi;
        return ConfigBean.GetBean<t_pet_colorup_costBean, int>(id); 
    }

    #endregion

    #region 升级---------------------------------------------------------------------------------------------

    public void SetExpPropDict()
    {
        _expPropDict.Clear();
        var num = _expPropIDs.Length;
        for (int i = 0; i < num; i++)
        {
            _expPropDict.Add(_expPropIDs[i], null);
        }

        BagService.Singleton.SetGridInfoByIDs(_expPropDict);
    }
    

    public int GetCurExp()
    {
        return _curSelectPetInfo.basInfo.expRemain;
    }

    #endregion

    #region 发送请求

    /// <summary>
    /// 请求宠物升品
    /// </summary>
    public void ReqShengPing()
    {
        UpdateOldPetInfo();

        List<Message.Pet.GridInfo> gridList = new List<Message.Pet.GridInfo>();
        int num = _caiLiaoList.Count;
        for (int i = 0; i < num; i++)
        {
            Message.Pet.GridInfo gridInfo = new Message.Pet.GridInfo();
            gridInfo.gridId = _caiLiaoPropDcit[_caiLiaoList[i]].id;
            gridInfo.num = _caiLiaoNumList[i];
            gridList.Add(gridInfo);
        }

        PetService.Singleton.ReqPetShengPing(_curSelectPetInfo.petId, gridList);
    }

    /// <summary>
    /// 请求宠物吃经验
    /// </summary>
    public void ReqPetAddExp(int itemID, int num)
    {
        Message.Pet.GridInfo gridInfo = new Message.Pet.GridInfo();
        Message.Bag.GridInfo bagGridInfo = _expPropDict[itemID];
        gridInfo.gridId = bagGridInfo.id;
        gridInfo.num = num;

        PetService.Singleton.ReqAddPetExp(_curSelectPetInfo.petId, gridInfo);
    }

    public float GetToMaxLevelNeedExp(float baseExp)
    {
        float needExp;
        int level = CurSelectPetInfo.basInfo.level;
        int curMaxExp = PetService.Singleton.GetCurLevelExp(CurSelectPetInfo.petId, level);
        needExp = curMaxExp - baseExp;
        Message.Role.RoleInfo roleInfo = RoleService.Singleton.GetRoleInfo();
        level++;
        while (level < roleInfo.level)
        {
            curMaxExp = PetService.Singleton.GetCurLevelExp(CurSelectPetInfo.petId, level);
            needExp += curMaxExp;
            level++;
        }

        return needExp;
    }

    #endregion;
}
