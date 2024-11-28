using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_Equip;
using Message.Pet;
using UI_Common;
using FairyGUI;

public class QuickUpgradeWindow : BaseWindow {

    EquipDataManager equipData;
    UI_QuickUpgradeWindow window;
    private List<int> ids = new List<int>();

    public EquipDataManager EquipData
    {
        get { return equipData; }
    }

    public override void OnOpen()
    {
        base.OnOpen();

        window = getUiWindow<UI_QuickUpgradeWindow>();
        equipData = Info.param as EquipDataManager;
        equipData.InitQuickUpgradeDefaultData();

        RefreshData();

        InitView();
        BindEvent();
    }

    private void RefreshData()
    {
        ids.Clear();
        switch (equipData.CurSelectEquipPos)
        {
            case EquipPosition.Weapon:
            case EquipPosition.Cloth:
            case EquipPosition.KuZi:
            case EquipPosition.Shoes:
                ids.AddRange(equipData.SPMaterialsIDList);
                break;
            case EquipPosition.HuiZhan:
                ids.AddRange(equipData.SpecialXunZhangIDs);
                break;
            case EquipPosition.MiJi:
                ids.AddRange(equipData.SpecialMiJiIDs);
                break;
            default:
                break;
        }
    }

    public override void InitView()
    {
        base.InitView();

        InitNormalView();
        RefreshView();
    }

    public override void AddEventListener()
    {
        base.AddEventListener();

        GED.ED.addListener(EventID.OnPetShuXingChanged, OnPetShuXingChanged);
    }

    public override void RemoveEventListener()
    {
        base.RemoveEventListener();

        GED.ED.removeListener(EventID.OnPetShuXingChanged, OnPetShuXingChanged);
    }

    private void OnPetShuXingChanged(GameEvent evt)
    {
        OnCloseBtn();
    }

    private void InitNormalView()
    {
        window.m_quickItemList.itemRenderer = RenderListItem;
        window.m_quickItemList.SetVirtual();

        PetEquip petEquip = equipData.GetCurSelectPetEquip();
        window.m_nameLabel.text = UIUtils.GetPingJieEquipName(equipData.CurSelectPetID, (int)equipData.CurSelectEquipPos, petEquip.star, petEquip.color);
        window.m_nameLabel.color = UIUtils.GetColorByQuality(petEquip.color);
        UIGloader.SetUrl(window.m_iconLoader,UIUtils.GetEquipIcon(equipData.CurSelectPetID, (int)equipData.CurSelectEquipPos, petEquip.star));
        UIGloader.SetUrl(window.m_boardLoader, UIUtils.GetLoaderUrl(WinEnum.UI_Common, UIUtils.GetBorderByQuality(petEquip.color)));

        // 设置代币图标
        int daiBiID = 0;
        switch (equipData.CurSelectEquipPos)
        {
            case EquipPosition.Weapon:
            case EquipPosition.Cloth:
            case EquipPosition.KuZi:
            case EquipPosition.Shoes:
                daiBiID = (int)ItemType.Damond;
                break;
            case EquipPosition.HuiZhan:
                daiBiID = (int)ItemType.HonorCurrency;
                break;
            case EquipPosition.MiJi:
                daiBiID = (int)ItemType.ShiLianCurrency;
                break;
            default:
                break;
        }
        UIGloader.SetUrl(window.m_coinLoader, UIUtils.GetItemIcon(daiBiID));

        window.m_lvLabel.text = petEquip.level.ToString();

        StarList starList = new StarList((UI_StarList)window.m_starList);
        starList.SetStar(petEquip.star);
    }

    public override void RefreshView()
    {
        base.RefreshView();

        RefreshData();

        window.m_goldNum.text = equipData.TotalGold.ToString();
        window.m_goldNum.color = equipData.IsQUEnoughGold() ? Color.white : Color.red;
        window.m_targetLvLabel.text = equipData.TargetLevel.ToString();
        window.m_targetPingJie.text = UIUtils.GetColorName(equipData.TargetColor); ;
        window.m_targetPingJie.color = UIUtils.GetColorByQuality(equipData.TargetColor);

        switch (equipData.CurSelectEquipPos)
        {
            case EquipPosition.Weapon:
            case EquipPosition.Cloth:
            case EquipPosition.KuZi:
            case EquipPosition.Shoes:
                window.m_coinNumLabel.text = equipData.TotalDiamon.ToString();
                break;
            case EquipPosition.HuiZhan:
            case EquipPosition.MiJi:
                window.m_coinNumLabel.text = equipData.TotalTokenNum.ToString();
                break;
            default:
                break;
        }
        window.m_coinNumLabel.color = equipData.IsQUEnoughTokenNum() ? Color.white : Color.red;
        window.m_quickItemList.numItems = ids.Count;
        // 按钮的处理
        window.m_reduceBtn.grayed = !equipData.IsCanReduceColorLevel();
        window.m_reduceBtn.touchable = equipData.IsCanReduceColorLevel();

        window.m_addBtn.grayed = !equipData.IsCanAddColorLevel();
        window.m_addBtn.touchable = equipData.IsCanAddColorLevel();

        bool isReadMaxColor = equipData.IsReachMaxColor();
        window.m_confirmBtn.grayed = isReadMaxColor;
        window.m_confirmBtn.touchable = !isReadMaxColor;
    }

    private void RenderListItem(int index, GObject obj)
    {
        QuickUpgradeItem item = obj as QuickUpgradeItem;
        item.itemID = ids[index];
        item.Init(this);
    }
    private void BindEvent()
    {
        window.m_mask.onClick.Add(OnCloseBtn);
        window.m_cancelBtn.onClick.Add(OnCloseBtn);
        window.m_confirmBtn.onClick.Add(OnConfirmBtnClick);
        window.m_reduceBtn.onClick.Add(OnReduceBtnClick);
        window.m_addBtn.onClick.Add(OnAddBtnClick);
    }

    protected override void OnCloseBtn()
    {
        equipData = null;
        window = null;

        base.OnCloseBtn();
    }

    private void OnConfirmBtnClick()
    {
        // 判断是否升到最大品了

        // 判断 金币是否足够
        if (equipData.IsQUEnoughGold())
        {
            
                bool isEnoughDiamond = equipData.IsQUEnoughDiamond();
                bool isEnoughTokenNum = equipData.IsQUEnoughTokenNum();
                // 判断钻石或者币是否足够
                if (isEnoughDiamond && isEnoughTokenNum)
                {
                    switch (equipData.CurSelectEquipPos)
                    {
                        case EquipPosition.Weapon:
                        case EquipPosition.Cloth:
                        case EquipPosition.KuZi:
                        case EquipPosition.Shoes:
                            equipData.SetNeedBuyMaterials();
                            List<int> noBuyMaterialList = equipData.GetNoBuyMaterialList();
                            if (noBuyMaterialList.Count > 0)
                            {
                                // 进入购买界面
                                int itemid = noBuyMaterialList[noBuyMaterialList.Count - 1];
                                int itemnumber = equipData.GetMaterialNeedNum(itemid);
                                TwoParam<int, int> twoParam = new TwoParam<int, int>();
                                twoParam.value1 = itemid;
                                twoParam.value2 = itemnumber;
                                WinMgr.Singleton.Open<LaiYuanWindow>(WinInfo.Create(false,null ,false,twoParam), UILayer.Popup);
                            }
                            else
                            {
                                equipData.ReqQuickUpgrade();
                            }
                            break;
                        case EquipPosition.HuiZhan:
                        case EquipPosition.MiJi:
                            // 判断经验值是否够
                            if (equipData.IsEnoughExp() && equipData.IsEnoughTargetLevelExp())
                            {
                                equipData.ReqQuickUpgrade();
                            }
                            else
                            {
                                // 经验值不够 提示材料不足
                                TipWindow.Singleton.ShowTip("材料不足！");
                            }
                            break;
                        default:
                            break;
                    }

                }
                else
                {
                    // 不够显示来源界面
                    if (!isEnoughDiamond)
                    {
                    TwoParam<int, int> twoParam = new TwoParam<int, int>();
                    twoParam.value1 = (int)ItemType.Damond;
                    twoParam.value2 = -1;
                        WinMgr.Singleton.Open<LaiYuanWindow>(WinInfo.Create(false, null, false, twoParam), UILayer.Popup);
                    }
                    if (!isEnoughTokenNum)
                    {
                        ItemType tokenType = equipData.CurSelectEquipPos == EquipPosition.HuiZhan ? ItemType.HonorCurrency : ItemType.ShiLianCurrency;
                        TwoParam<int, int> twoParam = new TwoParam<int, int>();
                        twoParam.value1 = (int)tokenType;
                        twoParam.value2 = -1;
                   
                        WinMgr.Singleton.Open<LaiYuanWindow>(WinInfo.Create(false, null, false, twoParam), UILayer.Popup);
                    }
                }
            
        }
        else
        {
            TwoParam<int, int> twoParam = new TwoParam<int, int>();
            twoParam.value1 = (int)ItemType.Gold;
            twoParam.value2 = -1;
            // 打开金币来源界面
            WinMgr.Singleton.Open<LaiYuanWindow>(WinInfo.Create(false, null, false, twoParam), UILayer.Popup);
        }
    }

    private void OnReduceBtnClick()
    {
        // 减一品等级
        equipData.ReduceOneColorLevel();
        RefreshView();
    }

    private void OnAddBtnClick()
    {
        // 加一品等级
        equipData.AddOnColorLevel();
        RefreshView();
    }

    
}
