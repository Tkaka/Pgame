using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_Equip;
using Message.Pet;
using Message.Role;
using DG.Tweening;
using Data.Beans;
using FairyGUI;
using UI_Common;
using Message.Bag;

public class EquipStrengthPanel : TabPage
{
    private long coroutineID;
    private long keyCoroutineID;
    private int _exp;
    /// <summary>
    /// 一键升级的目标等级
    /// </summary>
    private int keyTargetLevel;
    private bool IsSP = false;
    private bool isLevelUp = false;
    private bool isClickLvUpBtn = false;       // 点击普通升级按钮

    UI_StrengthPanel strengthPanel;
    EquipStrengthWindow _parentWindow;
    private Tweener tweeneer;

    ResPack resPack;
    GameObject lvUpEffGO;                 // 升级粒子特效
    SimpleInterval simpleInterval;

    public EquipDataManager equipData
    {
        get { return _parentWindow.equipData; }
    }

    public EquipStrengthPanel(EquipStrengthWindow parentWindow, UI_StrengthPanel strengthPanel)
    {
        _parentWindow = parentWindow;
        this.strengthPanel = strengthPanel;
        coroutineID = -1;
        keyCoroutineID = -1;
        strengthPanel.m_shengPingEff.visible = false;

        InitRefreshSpecialExpItemList();
        BindEvent();
        LoadEquipEffect();
    }

    private void LoadEquipEffect()
    {
        resPack = new ResPack(strengthPanel);
        GameObject effectGO = resPack.LoadGo("eff_ui_zhuangbei", new Vector3(-11.9f, -21.4f, -135f));
        GoWrapper wrapper = new GoWrapper(effectGO);
        strengthPanel.m_effectHolder.SetNativeObject(wrapper);

        strengthPanel.m_equipModelAnim.Play();
    }

    private void RemoveEquipEffect()
    {
        if(resPack != null)
            resPack.ReleaseAllRes();
        resPack = null;

        if(strengthPanel != null)
            strengthPanel.m_equipModelAnim.Stop();

        if (simpleInterval != null && simpleInterval.IsRunning)
            simpleInterval.Kill();
        simpleInterval = null;

        if (lvUpEffGO != null)
            GameObject.DestroyImmediate(lvUpEffGO);
    }

    private void BindEvent()
    {
        strengthPanel.m_normalShengPingBtn.onClick.Add(OnNormalSPBtnClick);
        strengthPanel.m_specialShengPingBtn.onClick.Add(OnSpcialSPBtnClick);
        strengthPanel.m_normalUpgradeBtn.onClick.Add(OnUpgradeBtnClick);
        strengthPanel.m_normalKeyBtn.onClick.Add(OnKeyUpgradeBtnClick);
        strengthPanel.m_normalQuickBtn.onClick.Add(OnQuickUpgradeBtnClick);
        strengthPanel.m_specialQucikBtn.onClick.Add(OnQuickUpgradeBtnClick);
    }

    public override void OnClose()
    {
        strengthPanel = null;
        _parentWindow = null;

        if (tweeneer != null && tweeneer.IsActive())
            tweeneer.Kill();
        tweeneer = null;

        if(coroutineID != -1)
            CoroutineManager.Singleton.stopCoroutine(coroutineID);
        if(keyCoroutineID != -1)
            CoroutineManager.Singleton.stopCoroutine(keyCoroutineID);

        RemoveEquipEffect();
    }

    public override void OnHide()
    {
        RemoveEquipEffect();
    }

    public override void OnShow()
    {
        PetEquip petEquip = equipData.GetCurSelectPetEquip();
        equipData.SetSPMatreials(petEquip.color);
        RefreshView();
        LoadEquipEffect();
    }

    public override void RefreshView(bool isNet = false)
    {
        if (isNet && IsSP)
        {
            // 打开升品成功界面
            WinInfo winInfo = new WinInfo();
            winInfo.param = equipData;
            WinMgr.Singleton.Open<EquipSPSuccessWindow>(winInfo, UILayer.Popup);

            IsSP = false;
        }
        if (isNet && isClickLvUpBtn)
        {
            ShowShengJiEff();
            isClickLvUpBtn = false;
        }

        RefreshCommonView(isNet);

        strengthPanel.m_shengPingGroup.visible = _parentWindow.equipData.IsArriveMaxLevel();
        strengthPanel.m_upgradeGroup.visible = !_parentWindow.equipData.IsArriveMaxLevel();
        if (_parentWindow.equipData.IsArriveMaxLevel())
            RefreshShengPingView();
        else
            RefreshUpgradeView();

        RefreshExpProgressTip();
    }

    private void RefreshShengPingView()
    {
        strengthPanel.m_normalShengPingGroup.visible = false;
        strengthPanel.m_specialShengPingGroup.visible = false;
        switch (_parentWindow.equipData.CurSelectEquipPos)
        {
            case EquipPosition.Weapon:
            case EquipPosition.Cloth:
            case EquipPosition.KuZi:
            case EquipPosition.Shoes:
                RefreshNormalSPView();
                break;
            case EquipPosition.HuiZhan:
            case EquipPosition.MiJi:
                RefreshSpecialSPView();
                break;
            default:
                break;
        }
    }

    private void RefreshCommonView(bool isNet = false)
    {
        SetLevelLabel();
        SetAttributeLabel(isNet);

        strengthPanel.m_useNumLabel.visible = false;

        PetEquip petEquip = equipData.GetCurSelectPetEquip();

        strengthPanel.m_nameLabel.text = UIUtils.GetPingJieEquipName(equipData.CurSelectPetID, (int)equipData.CurSelectEquipPos, petEquip.star, petEquip.color);
        UIGloader.SetUrl(strengthPanel.m_equipImg, UIUtils.GetEquipIcon(equipData.CurSelectPetID, (int)equipData.CurSelectEquipPos, petEquip.star));
        strengthPanel.m_nameLabel.color = UIUtils.GetColorByQuality(petEquip.color);

        RefreshStarList(petEquip.star);
    }

    private void ShowShengJiEff()
    {
        // 显示升级成功的UI特效
        strengthPanel.m_shengPingEff.visible = true;
        if (strengthPanel.m_shengPingEff.m_anim.playing)
            strengthPanel.m_shengPingEff.m_anim.Stop();
        strengthPanel.m_shengPingEff.m_anim.Play(OnLvUpAnimEnd);

        // 显示升级成功的粒子特效
        if(lvUpEffGO == null)
        {
            if (resPack == null)
                resPack = new ResPack(this);
            lvUpEffGO = resPack.LoadGo("eff_zb_level_up");
            if (lvUpEffGO != null)
            {
                GoWrapper wrapper = new GoWrapper(lvUpEffGO);
                strengthPanel.m_lvEffPos.SetNativeObject(wrapper);
                lvUpEffGO.transform.localPosition = new Vector3(48,33,492);
            }
        }

        if(lvUpEffGO != null)
        {
            lvUpEffGO.SetActive(true);
            ParticleSystem ps = lvUpEffGO.GetComponentInChildren<ParticleSystem>();
            if (ps != null)
            {
                if (ps.isPlaying)
                    ps.Stop(true);
                ps.Play(true);

                if (simpleInterval == null)
                    simpleInterval = new SimpleInterval();
                simpleInterval.Kill();
                simpleInterval.DoActionWithTimes(ps.main.duration, OnLvUpEffEnd);
            }
        }
    }

    private void OnLvUpEffEnd()
    {
        if(lvUpEffGO != null)
            lvUpEffGO.SetActive(false);
    }

    private void OnLvUpAnimEnd()
    {
        strengthPanel.m_shengPingEff.visible = false;
    }

    private void RefreshStarList(int star)
    {
        int maxStar = PetService.Singleton.GetPetMaxStar();
        strengthPanel.m_starList.RemoveChildren(0, -1, true);
        GObject obj = null;
        for (int i = 0; i < maxStar; i++)
        {
            if (i < star)
                obj = GetStarObj(true);
            else
                obj = GetStarObj(false);
            strengthPanel.m_starList.AddChild(obj);
        }
    }

    private GObject GetStarObj(bool isBright)
    {
        if (isBright)
            return UIPackage.CreateObject(WinEnum.UI_Common, "xing01");
        else
            return UIPackage.CreateObject(WinEnum.UI_Common, "xing02");
    }

    private void SetLevelLabel()
    {
        PetEquip petEquip = equipData.GetCurSelectPetEquip();
        strengthPanel.m_levelLabel.text = string.Format("{0}/{1}", petEquip.level, equipData.GetCurColorMaxLevel(petEquip.color));
    }
    private void SetAttributeLabel(bool isShowAnim)
    {
        PetEquip petEquip = equipData.GetCurSelectPetEquip();
        if (isLevelUp == false)
        {
            isShowAnim = false;
        }
        else
        {
            isLevelUp = false;
        }
        // 显示等级的特效
        if (isShowAnim)
        {
            if (strengthPanel.m_levelAnim.playing)
                strengthPanel.m_levelAnim.Stop();
            strengthPanel.m_levelAnim.Play();
        }
        //int[] addIdArr = equipData.GetIDList();
        //int count = addIdArr.Length;
        // 获得属性值
        Dictionary<PropertyType, PropertyStruct> attributeData = equipData.GetAttributeData();


        int attributeNum = strengthPanel.m_atrributeList.numChildren;
        EquipAttributeLabelItem attributeItem = null;

        bool attach = true;
        if (attributeData.Count > 0)
        {
            switch (equipData.CurSelectEquipPos)
            {
                case EquipPosition.HuiZhan:
                case EquipPosition.MiJi:
                    attach = false;
                    break;
                default:
                    break;
            }
        }

        if (attributeData.Count <= 0)
        {
            return;
        }

        int num = 0;
        foreach (var pro in attributeData)
        {
            if (num < attributeNum)
            {
                attributeItem = strengthPanel.m_atrributeList.GetChildAt(num) as EquipAttributeLabelItem;
                attributeItem.RefreshView((int)pro.Key, attach ? (int)pro.Value.attachValue.Floor : pro.Value.percentValue, isShowAnim, equipData.CurSelectEquipPos);
            }
            else
            {
                attributeItem = (EquipAttributeLabelItem)EquipAttributeLabelItem.CreateInstance();
                strengthPanel.m_atrributeList.AddChild(attributeItem);
                attributeItem.RefreshView((int)pro.Key, attach ? (int)pro.Value.attachValue.Floor : pro.Value.percentValue, isShowAnim, equipData.CurSelectEquipPos);
            }
            num++;
        }

        // 多余的item隐藏掉
        for (int i = attributeData.Count; i < attributeNum; i++)
        {
            attributeItem = strengthPanel.m_atrributeList.GetChildAt(i) as EquipAttributeLabelItem;
            strengthPanel.m_atrributeList.RemoveChild(attributeItem);
            attributeItem.Dispose();
        }
    }

    /// <summary>
    /// 刷新普通装备升品界面
    /// </summary>
    private void RefreshNormalSPView()
    {
        strengthPanel.m_normalShengPingGroup.visible = true;
        PetEquip petEquip = equipData.GetCurSelectPetEquip();
        int needGold = equipData.GetNormalSPNeedGold(petEquip.color);
        int needLv = equipData.GetColorUpNeedRoleLv(petEquip.color);
        strengthPanel.m_normalSPGoldLabel.text = needGold.ToString();
        strengthPanel.m_normalSPGoldLabel.color = equipData.IsEnoughSPGold() ? Color.white : Color.red;
        strengthPanel.m_normalEnoughLvGroup.visible = equipData.PetLvIsCanColorUp(petEquip.color);
        strengthPanel.m_normalUnEnoughLvTip.visible = !equipData.PetLvIsCanColorUp(petEquip.color);
        strengthPanel.m_normalUnEnoughLvTip.text = string.Format("需求等级{0}", needLv);

        bool isReachMaxColor = equipData.IsReachMaxColor();
        strengthPanel.m_normalShengPingBtn.grayed = isReachMaxColor;
        strengthPanel.m_normalShengPingBtn.touchable = !isReachMaxColor;

        RefreshNormalMaterialList();
    }
    private void RefreshNormalMaterialList()
    {
        int count = equipData.SPMaterialsIDList.Count;
        SPMaterialItem materialItem;
        strengthPanel.m_itemList.RemoveChildren(0, -1, true);
        for (int i = 0; i < count; i++)
        {
            materialItem = SPMaterialItem.CreateInstance();
            materialItem.itemID = equipData.SPMaterialsIDList[i];
            materialItem.Init(this);
            strengthPanel.m_itemList.AddChild(materialItem);
        }
    }
    /// <summary>
    /// 刷新徽章和秘籍的升品界面
    /// </summary>
    private void RefreshSpecialSPView()
    {
        strengthPanel.m_specialShengPingGroup.visible = true;
        // TODO : 根据类型的不同加载不同货币ID
        PetEquip petEquip = equipData.GetCurSelectPetEquip();
        RoleInfo roleInfo = RoleService.Singleton.GetRoleInfo();
        //strengthPanel.m_specialEnoughLvGroup.visible = equipData.PetLvIsCanColorUp(petEquip.color);
        int haveCoinNum = equipData.CurSelectEquipPos == EquipPosition.MiJi ? roleInfo.trailCurrency : roleInfo.honorCurrency;
        int needCoinNum = equipData.GetNeedTokenNum(petEquip.color);

        strengthPanel.m_SpecialSPGoldNum.text = equipData.GetSpecialSPNeedGold(petEquip.color).ToString();
        strengthPanel.m_SpecialSPGoldNum.color = equipData.IsEnoughSPGold() ? Color.white : Color.red;
        strengthPanel.m_haveCoinNumLabel.text = haveCoinNum.ToString();
        strengthPanel.m_useCoinNum.text = needCoinNum.ToString();
        strengthPanel.m_useCoinNum.color = haveCoinNum >= needCoinNum ? Color.white : Color.red;

        int languageID = equipData.CurSelectEquipPos == EquipPosition.MiJi ? 71102002 : 71102001;
        strengthPanel.m_tipLabel.text = UIUtils.GetStrByLanguageID(languageID);

        int itemID = equipData.CurSelectEquipPos == EquipPosition.MiJi ? (int)ItemType.ShiLianCurrency : (int)ItemType.HonorCurrency;
        t_itemBean itemBean = ConfigBean.GetBean<t_itemBean, int>(itemID);
        if (itemBean != null)
        {
            UIGloader.SetUrl(strengthPanel.m_haveCoinLoader, UIUtils.GetItemIcon(itemID));
            UIGloader.SetUrl(strengthPanel.m_useCoinLoader, UIUtils.GetItemIcon(itemID));
        }

        //strengthPanel.m_specialUnEnoughLvTip.text = string.Format("需求等级{0}", equipData.GetColorUpNeedEquipLv(petEquip.color));
        //strengthPanel.m_specialUnEnoughLvTip.visible = equipData.PetLvIsCanColorUp(petEquip.color);
        strengthPanel.m_specialUnEnoughLvTip.visible = false;
    }

    private void RefreshUpgradeView()
    {
        strengthPanel.m_normalEquipUpgrad.visible = false;
        strengthPanel.m_specialEquipUpgrade.visible = false;
        switch (_parentWindow.equipData.CurSelectEquipPos)
        {
            case EquipPosition.Weapon:
            case EquipPosition.Cloth:
            case EquipPosition.KuZi:
            case EquipPosition.Shoes:
                RefreshNormalUpgradeView();
                break;
            case EquipPosition.HuiZhan:
            case EquipPosition.MiJi:
                RefreshSpecialUpgradeView();
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// 刷新普通装备升级界面
    /// </summary>
    private void RefreshNormalUpgradeView()
    {
        PetEquip petEquip = equipData.GetCurSelectPetEquip();
        strengthPanel.m_normalEquipUpgrad.visible = true;
        strengthPanel.m_normalQuickBtn.visible = _parentWindow.equipData.UnlockQuickBtn();
        strengthPanel.m_normalUpgradeGoldLabel.color = _parentWindow.equipData.IsEnoughUpgradeGold() ? Color.white : Color.red;
        strengthPanel.m_normalUpgradeGoldLabel.text = _parentWindow.equipData.GetLevelUpgradeGold(petEquip.level, petEquip.color).ToString();
    }
    /// <summary>
    /// 刷新特殊装备升级界面
    /// </summary>
    private void RefreshSpecialUpgradeView()
    {
        strengthPanel.m_specialEquipUpgrade.visible = true;
        strengthPanel.m_specialQucikBtn.visible = _parentWindow.equipData.UnlockQuickBtn();
        InitSpecialExpBar();
        RefreshSpecilExpItemList();
    }
    /// <summary>
    /// 初始话特殊装备升级的经验条
    /// </summary>
    private void InitSpecialExpBar()
    {
        PetEquip petEquip = _parentWindow.equipData.GetCurSelectPetEquip();
        int remainExp = petEquip.exp;
        int curMaxExp = _parentWindow.equipData.GetSpecialLvUpExp(petEquip.level, petEquip.color);
        if (remainExp < curMaxExp)
        {
            strengthPanel.m_expPrograssBar.value = petEquip.exp;
            strengthPanel.m_expPrograssBar.max = _parentWindow.equipData.GetSpecialLvUpExp(petEquip.level, petEquip.color);
        }
        else
        {
            strengthPanel.m_expPrograssBar.value = 0;
            RefreshSpecialExpBar(petEquip.exp);
        }
    }
    /// <summary>
    /// 刷新特殊装备升级的经验条
    /// </summary>
    public void RefreshSpecialExpBar(int exp)
    {
        _exp = exp;
        if (coroutineID != -1)
            CoroutineManager.Singleton.stopCoroutine(coroutineID);

        coroutineID = CoroutineManager.Singleton.startCoroutine(RefreshExpBar());
    }

    IEnumerator RefreshExpBar()
    {
        IsSP = false;
        int newValue = (int)strengthPanel.m_expPrograssBar.value + _exp;
        float interval = 0.001f;
        int maxLevel = 0;
        PetEquip petEquip = equipData.GetCurSelectPetEquip();
        t_special_equip_lvcolorupBean specialEquipLvColorUpBean = equipData.GetSpecialEquipLvColorUpBean(petEquip.color);
        if (specialEquipLvColorUpBean == null)
        {
            CoroutineManager.Singleton.stopCoroutine(coroutineID);
            coroutineID = -1;
            yield break;
        }
        maxLevel = specialEquipLvColorUpBean.t_lv_max;
        while (strengthPanel.m_expPrograssBar.max <= newValue && specialEquipLvColorUpBean != null)
        {
            newValue -= (int)strengthPanel.m_expPrograssBar.max;
            tweeneer = strengthPanel.m_expPrograssBar.TweenValue(strengthPanel.m_expPrograssBar.max, interval);
            yield return new WaitForSeconds(interval);

            isLevelUp = true;
            petEquip.level++;
            ShowShengJiEff();
            if(petEquip.level >= maxLevel)
            {
                // 达到等级上限了，就转换成升品界面了
                if (tweeneer != null && tweeneer.IsActive())
                    tweeneer.Kill();
                CoroutineManager.Singleton.stopCoroutine(coroutineID);
                coroutineID = -1;
                RefreshCommonView(true);
                yield break;
            }

            
            strengthPanel.m_expPrograssBar.max = equipData.GetSpecialLvUpExp(petEquip.level, petEquip.color);
            RefreshCommonView(true);
        }

        strengthPanel.m_expPrograssBar.value = newValue;
        petEquip.exp = newValue;
        RefreshExpProgressTip();
    }
    /// <summary>
    /// 刷新特殊装备的经验条显示
    /// </summary>
    private void RefreshExpProgressTip()
    {
        strengthPanel.m_specialEqupExpTip.text = string.Format("{0}/{1}", strengthPanel.m_expPrograssBar.value, strengthPanel.m_expPrograssBar.max);
    }

    /// <summary>
    /// 刷新长按时的经验条显示
    /// </summary>
    public void RefreshLongPressExpBar(int exp, int num)
    {
        IsSP = false;
        strengthPanel.m_useNumLabel.visible = true;
        strengthPanel.m_useNumLabel.text = num.ToString();
        int newVlaue = (int)strengthPanel.m_expPrograssBar.value + exp;
        PetEquip petEquip = equipData.GetCurSelectPetEquip();
        t_special_equip_lvcolorupBean specialEquipLvColorUpBean = equipData.GetSpecialEquipLvColorUpBean(petEquip.color);

        while (strengthPanel.m_expPrograssBar.max <= newVlaue)
        {
            petEquip.level++;
            isLevelUp = true;
            ShowShengJiEff();
            if (petEquip.level >= specialEquipLvColorUpBean.t_lv_max)
            {
                //达到等级上限了，转换成升品界面
                RefreshCommonView(true);
                break;
            }
            newVlaue -= (int)strengthPanel.m_expPrograssBar.max;
            strengthPanel.m_expPrograssBar.max = equipData.GetSpecialLvUpExp(petEquip.level, petEquip.color);
            ShowLevelUpView();
        }
        strengthPanel.m_expPrograssBar.value = newVlaue;
        RefreshExpProgressTip();
    }
    /// <summary>
    /// 刷新特殊装备升级需要的经验道具列表
    /// </summary>
    private void RefreshSpecilExpItemList()
    {
        int num = strengthPanel.m_SpecialExpList.numChildren;
        SpecialExpItem expPropItem;
        int[] ids = equipData.CurSelectEquipPos == EquipPosition.HuiZhan ? equipData.SpecialXunZhangIDs : equipData.SpecialMiJiIDs;
        for (int i = 0; i < num; i++)
        {
            expPropItem = strengthPanel.m_SpecialExpList.GetChildAt(i) as SpecialExpItem;
            expPropItem.itemID = ids[i];
            expPropItem.RefreshItem();
        }
    }

    private void InitRefreshSpecialExpItemList()
    {
        int num = strengthPanel.m_SpecialExpList.numChildren;
        SpecialExpItem expPropItem;
        int[] ids = equipData.CurSelectEquipPos == EquipPosition.HuiZhan ? equipData.SpecialXunZhangIDs : equipData.SpecialMiJiIDs;
        for (int i = 0; i < num; i++)
        {
            expPropItem = strengthPanel.m_SpecialExpList.GetChildAt(i) as SpecialExpItem;
            expPropItem.itemID = ids[i];
            expPropItem.Init(this);
        }
    }

    private void ShowLevelUpView()
    {
        SetLevelLabel();
        SetAttributeLabel(true);
    }

    IEnumerator KeyUpgradeCoroutine()
    {
        PetEquip petEquip = equipData.GetCurSelectPetEquip();
        keyTargetLevel = petEquip.level;
        int limitLevel = equipData.GetColorUpNeedEquipLv(petEquip.color);

        RoleInfo roleInfo = RoleService.Singleton.GetRoleInfo();
        int needGold;
        bool isChange = false;
        while (equipData.IsEnoughUpgradeGold() && keyTargetLevel < limitLevel)
        {
            needGold = equipData.GetLevelUpgradeGold(keyTargetLevel, petEquip.color);
            roleInfo.gold -= needGold;
            keyTargetLevel++;
            petEquip.level++;
            isChange = true;
            isLevelUp = true;
            _parentWindow.curSelectedEquipItem.RefreshItem();
            // 刷新装备属性
            PetPropertyMgr mgr = PetService.Singleton.GetPetPropertyMgr(equipData.CurSelectPetID);
            mgr.PetEquipInfoChange(petEquip);
            RefreshCommonView(true);
            RefreshNormalUpgradeView();
            ShowShengJiEff();
            yield return new WaitForSeconds(0.2f);
        }

        if (isChange == false)
        {
            TipWindow.Singleton.ShowTip("金币不足!");
        }
        else
        {
            equipData.ReqNormalEquipUpgrade(keyTargetLevel);
        }
        yield return new WaitForSeconds(0.1f);
        keyCoroutineID = -1;
        _parentWindow.TouchEnable();
    }

    #region 按钮响应事件

    private void OnNormalSPBtnClick()
    {
        if (keyCoroutineID != -1)
            return;

        IsSP = true;
        PetEquip petEquip = equipData.GetCurSelectPetEquip();
        if (equipData.PetLvIsCanColorUp(petEquip.color))
        {
            if (equipData.IsEnoughSPGold())
            {
                if (equipData.SPMaterilIsEnough())
                {
                    //升品
                    equipData.UpdateOldEquipProperty();
                    equipData.ReqNormalEquipSP();
                }
                else
                {
                    equipData.SetNeedBuyMaterials();
                    int needDiamond = equipData.GetBuyMaterialDiamond();
                    List<int> noBuyMaterialList = equipData.GetNoBuyMaterialList();
                    if (noBuyMaterialList.Count != 0)
                    {
                        // 打开来源界面
                        int needNum = equipData.GetMaterialNeedNum(noBuyMaterialList[0]);
                        TwoParam<int, int> twoParam = new TwoParam<int, int>();
                        twoParam.value1 = noBuyMaterialList[0];
                        twoParam.value2 = needNum;
                        WinMgr.Singleton.Open<LaiYuanWindow>(WinInfo.Create(false, null, false, twoParam), UILayer.Popup);
                    }
                    else
                    {
                        // 打开购买界面
                        WinInfo winInfo = new WinInfo();
                        winInfo.param = equipData.NeedBuyMaterialList;
                        WinMgr.Singleton.Open<DiamondBuyMaterialWindow>(winInfo, UILayer.Popup);
                    }
                }
            }
            else
            {
                TwoParam<int, int> twoParan = new TwoParam<int, int>();
                twoParan.value1 = (int)ItemType.Gold;
                twoParan.value2 = -1;
                WinInfo winInfo = new WinInfo();
                winInfo.param = twoParan;
                WinMgr.Singleton.Open<LaiYuanWindow>(winInfo, UILayer.Popup);
            }
        }
        else
        {
            TipWindow.Singleton.ShowTip("宠物等级不足，装备无法提升至下一品质");
        }
        
    }

    private void OnSpcialSPBtnClick()
    {
        if (keyCoroutineID != -1)
            return;

        IsSP = true;
        PetEquip petEquip = equipData.GetCurSelectPetEquip();
        // 特殊装备升品没有等级限制
        //if (equipData.IsArriveSPLv(petEquip.color))
        //{
            if (equipData.IsEnoughSPGold())
            {
                if (equipData.SPTokenIsEnough())
                {
                    equipData.UpdateOldEquipProperty();
                    equipData.ReqSpecialEquipSP();
                }
                else
                {
                //string tip = equipData.CurSelectEquipPos == EquipPosition.MiJi ? "试炼币不足" : "荣誉币不足";
                //TipWindow.Singleton.ShowTip(tip);
                ItemType tokenType = equipData.CurSelectEquipPos == EquipPosition.HuiZhan ? ItemType.HonorCurrency : ItemType.ShiLianCurrency;
                TwoParam<int, int> twoParam = new TwoParam<int, int>();
                twoParam.value1 = (int)tokenType;
                twoParam.value2 = -1;

                WinMgr.Singleton.Open<LaiYuanWindow>(WinInfo.Create(false, null, false, twoParam), UILayer.Popup);
            }
            }
            else
            {
                TipWindow.Singleton.ShowTip("金币不足！");
            }
        //}
        //else
        //{
        //    TipWindow.Singleton.ShowTip("战队等级不足，装备无法提升至下一品质");
        //}
    }

    private void OnQuickUpgradeBtnClick()
    {
        IsSP = false;
        // 打开快速升级面板
        WinInfo winInfo = new WinInfo();
        winInfo.param = equipData;
        WinMgr.Singleton.Open<QuickUpgradeWindow>(winInfo, UILayer.Popup);
    }

    private void OnKeyUpgradeBtnClick()
    {
        if (keyCoroutineID == -1)
        {
            IsSP = false;
            _parentWindow.TouchUnEnable();
            PetEquip petEquip = equipData.GetCurSelectPetEquip();
            CoroutineManager.Singleton.stopCoroutine(keyCoroutineID);
            keyCoroutineID = CoroutineManager.Singleton.startCoroutine(KeyUpgradeCoroutine());
        }
    }

    private void OnUpgradeBtnClick()
    {
        if (equipData.IsEnoughUpgradeGold())
        {
            IsSP = false;
            isClickLvUpBtn = true;
            PetEquip petEquip = equipData.GetCurSelectPetEquip();
            int targetLevel = petEquip.level + 1;
            equipData.ReqNormalEquipUpgrade(targetLevel);
        }
        else
        {
            TipWindow.Singleton.ShowTip("金币不足!");
        }
    }

    #endregion

}
