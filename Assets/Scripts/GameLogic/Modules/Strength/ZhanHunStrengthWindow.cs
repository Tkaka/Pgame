using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_Strength;
using Message.Pet;
using Data.Beans;
using FairyGUI;

public class ZhanHunStrengthWindow : BaseWindow {

    UI_ZhanHunStrengthWindow _window;
    ThreeParam<int, int, int> _threenPara;
    ZhanHunStrengthDataManager _dataManager;
    private int tempLv;
    private double sliderOldValue;
    private t_pet_soulBean petSoulBean;
    private SoulInfo soulInfo;
    private int selectIndex = -1;

    public ZhanHunStrengthDataManager DataManager
    {
        get { return _dataManager; }
    }

    public override void OnOpen()
    {
        base.OnOpen();

        _window = getUiWindow<UI_ZhanHunStrengthWindow>();
        _threenPara = Info.param as ThreeParam<int, int, int>;
        _dataManager = new ZhanHunStrengthDataManager();
        tempLv = 0;
        sliderOldValue = 0;

        if (_dataManager.GridInfoList == null)
            return;

        InitView();
        PlayPopupAnim(_window.m_mask, _window.m_popupView);
    }

    public override void AddEventListener()
    {
        base.AddEventListener();

        GED.ED.addListener(EventID.OnPetShuXingChanged, OnStrengthSuccess);
    }

    public override void RemoveEventListener()
    {
        base.RemoveEventListener();

        GED.ED.removeListener(EventID.OnPetShuXingChanged, OnStrengthSuccess);
    }

    public override void InitView()
    {
        base.InitView();

        PetInfo petInfo = PetService.Singleton.GetPetByID(_threenPara.value3);
        petSoulBean = ConfigBean.GetBean<t_pet_soulBean, int>(_threenPara.value1);
        if (petInfo == null || petSoulBean == null)
            return;
        soulInfo = petInfo.soulInfo.souls[_threenPara.value2];

        BindEvent();

        _window.m_popupView.m_unfullGroup.visible = true;
        _window.m_popupView.m_fullGroup.visible = false;

        InitMaterialList();
        RefreshZhanHunDetailView();
        RefreshPrograssBar();
        RefreshMaterialView();
        RefreshMaterialList();
    }

    private void InitMaterialList()
    {
        _window.m_popupView.m_zhanHunCaiLiaoList.itemRenderer = RenderListItem;
        _window.m_popupView.m_zhanHunCaiLiaoList.SetVirtual();
        _window.m_popupView.m_zhanHunCaiLiaoList.numItems = DataManager.GridInfoList.Count;
        SetSelectFirstItem();
    }
    /// <summary>
    /// 选择第一个Item
    /// </summary>
    private void SetSelectFirstItem()
    {
        if (DataManager.GridInfoList.Count != 0)
        {
            int realIndex = 0;
            ZhanHunMaterialItem materialItem;
            if (selectIndex != -1)
            {
                materialItem = GetItemByItemIndex(selectIndex);
                if (materialItem != null)
                {
                    materialItem.m_selectIcon.visible = false;
                }
            }
            _window.m_popupView.m_zhanHunCaiLiaoList.ScrollToView(0);
            realIndex = _window.m_popupView.m_zhanHunCaiLiaoList.ItemIndexToChildIndex(0);
            materialItem = _window.m_popupView.m_zhanHunCaiLiaoList.GetChildAt(0) as ZhanHunMaterialItem;
            materialItem.m_selectIcon.visible = true;
            selectIndex = 0;
        }
    }

    private ZhanHunMaterialItem GetItemByItemIndex(int index)
    {
        ZhanHunMaterialItem materialItem = null;
        index = _window.m_popupView.m_zhanHunCaiLiaoList.ItemIndexToChildIndex(index);
        if (index >= 0 && index < _window.m_popupView.m_zhanHunCaiLiaoList.numChildren)
        {
            materialItem = _window.m_popupView.m_zhanHunCaiLiaoList.GetChildAt(index) as ZhanHunMaterialItem;
        }

        return materialItem;
    }

    private void RefreshZhanHunDetailView()
    {
        //_window.m_nameLabel.text = UIUtils.GetStrByLanguageID(2000001);
        _window.m_popupView.m_nameLabel.text = petSoulBean.t_nameLanguageID;
        _window.m_popupView.m_lvLabel.text = soulInfo.level.ToString();
        // 战魂描述
        List<float> valueList = PetService.Singleton.GetZhanHunDesValueList(_threenPara.value1, _threenPara.value2, _threenPara.value3);
        switch (valueList.Count)
        {
            case 1:
                _window.m_popupView.m_descriptLabel.text = string.Format(petSoulBean.t_descriptID, valueList[0]);
                break;
            case 2:
                _window.m_popupView.m_descriptLabel.text = string.Format(petSoulBean.t_descriptID, valueList[0], valueList[1]);
                break;
            case 3:
                _window.m_popupView.m_descriptLabel.text = string.Format(petSoulBean.t_descriptID, valueList[0], valueList[1], valueList[2]);
                break;
            case 4:
                _window.m_popupView.m_descriptLabel.text = string.Format(petSoulBean.t_descriptID, valueList[0], valueList[1], valueList[2], valueList[3]);
                break;
            default:
                break;
        }

        //_window.m_iconLoader.url = UIUtils.GetLoaderUrl(WinEnum.UI_Common, petSoulBean.t_icon);
        UIGloader.SetUrl(_window.m_popupView.m_iconLoader, petSoulBean.t_icon);
    }

    private void RefreshPrograssBar()
    {
        PetInfo petInfo = PetService.Singleton.GetPetByID(_threenPara.value3);
        if (petInfo == null)
            return;

        t_pet_soulBean petSoulBean = ConfigBean.GetBean<t_pet_soulBean, int>(_threenPara.value1);
        if (petSoulBean == null)
        {
            return;
        }

        t_pet_soulup_expBean petSoulExpBean = PetService.Singleton.GetPetSoulUpBean(petSoulBean.t_exp_type, soulInfo.level);
        if (petSoulExpBean != null)
        {
            _window.m_popupView.m_realProBar.max = petSoulExpBean.t_exp;
            _window.m_popupView.m_realProBar.value = soulInfo.remainExp;
            // 计算预览的经验和等级
            int tempExp = DataManager.addExp + soulInfo.remainExp;
            tempLv = soulInfo.level;
            while (tempExp > petSoulExpBean.t_exp)
            {
                tempLv++;
                if (ExpIsFull())
                {
                    tempExp = petSoulExpBean.t_exp;
                    break;
                }
                tempExp -= petSoulExpBean.t_exp;
                petSoulExpBean = PetService.Singleton.GetPetSoulUpBean(petSoulBean.t_exp_type, tempLv);
            }

            _window.m_popupView.m_realProBar.visible = tempLv <= soulInfo.level;
            _window.m_popupView.m_previewLvGroup.visible = tempLv > soulInfo.level;
            _window.m_popupView.m_tempLvLabel.text = tempLv.ToString();

            _window.m_popupView.m_tempProBar.max = petSoulExpBean.t_exp;
            _window.m_popupView.m_tempProBar.value = tempExp;
            _window.m_popupView.m_proBarValueLabel.text = string.Format("{0}/{1}", tempExp, petSoulExpBean.t_exp);
        }
       
    }

    public void RefreshMaterialView()
    {
        _window.m_popupView.m_addExpLabel.text = DataManager.addExp.ToString();
        _window.m_popupView.m_addNumLabel.text = string.Format("{0}/{1}", DataManager.UseCaiLiaoNum, DataManager.selectCaiLiaoNum);
        _window.m_popupView.m_materialSlider.max = DataManager.selectCaiLiaoNum;
        _window.m_popupView.m_materialSlider.value = DataManager.UseCaiLiaoNum;
        _window.m_popupView.m_unAddTipLabel.visible = DataManager.SelectGridInfoDict.Count == 0;
        _window.m_popupView.m_addCaiLiaoGroup.visible = DataManager.SelectGridInfoDict.Count != 0;
        _window.m_popupView.m_goldLabel.text = DataManager.UseGoldNum.ToString();
        _window.m_popupView.m_goldLabel.color = DataManager.IsEnoughGold() ? Color.white : Color.red;
        if (DataManager.curSelectGridID != -1)
        {
            Message.Bag.GridInfo gridInfo = BagService.Singleton.GetGrid(DataManager.curSelectGridID);
            t_itemBean itemBean = ConfigBean.GetBean<t_itemBean, int>(gridInfo.itemInfo.id);
            _window.m_popupView.m_itemNameLabel.text = itemBean.t_name;
        }
    }

    private void RefreshMaterialList()
    {
        _window.m_popupView.m_zhanHunCaiLiaoList.numItems = DataManager.GridInfoList.Count;
        SetSelectFirstItem();
    }

    private void RenderListItem(int index, GObject obj)
    {
        ZhanHunMaterialItem zhanHunMaterialItem = obj as ZhanHunMaterialItem;
        zhanHunMaterialItem.gridID = DataManager.GridInfoList[index].id;
        if (zhanHunMaterialItem.parentWindow == null)
        {
            zhanHunMaterialItem.Init(this);
        }
        else
        {
            zhanHunMaterialItem.RefreshItem();
        }

        zhanHunMaterialItem.m_selectIcon.visible = index == selectIndex;
    }
    /// <summary>
    /// 战魂经验是否已满
    /// </summary>
    /// <returns></returns>
    private bool ExpIsFull()
    {
        int maxLevel = PetService.Singleton.GetZhanHunMaxLevel();
        if (tempLv >= maxLevel)
            return true;
        //if(tempLv == maxLevel -1)
        //{
        //    t_pet_soulup_expBean petSoulExpBean = PetService.Singleton.GetPetSoulUpBean(petSoulBean.t_exp_type, tempLv);
        //    int tempExp = soulInfo.remainExp + DataManager.addExp;
        //    return tempExp >= petSoulExpBean.t_exp;
        //}

        return false;
    }

    protected override void OnCloseBtn()
    {
        _window = null;
        _threenPara = null;
        _dataManager = null;

        base.OnCloseBtn();
    }

    private void BindEvent()
    {
        _window.m_popupView.m_closeBtn.onClick.Add(OnCloseBtn);
        _window.m_mask.onClick.Add(OnCloseBtn);
        _window.m_popupView.m_addBtn.onClick.Add(OnClickAddBtn);
        _window.m_popupView.m_reduceBtn.onClick.Add(OnClickReduceBtn);
        _window.m_popupView.m_strengthBtn.onClick.Add(OnClickStrengthBtn);
        _window.m_popupView.m_zuanShiStrengthBtn.onClick.Add(OnClickZuanShiStrengthBtn);
        _window.m_popupView.m_materialSlider.onChanged.Add(OnMaterialValueChanged);
        _window.m_popupView.m_zhanHunCaiLiaoList.onClickItem.Add(OnMaterialListClickItem);
    }

    private void OnClickAddBtn()
    {
        if (DataManager.curSelectGridID == -1)
            return;

        if (ExpIsFull())
        {
            TipWindow.Singleton.ShowTip("战魂等级已满");
            return;
        }
        
        Message.Bag.GridInfo gridInfo = BagService.Singleton.GetGrid(DataManager.curSelectGridID);
        if (DataManager.UseCaiLiaoNum >= gridInfo.itemInfo.num)
        {
            TipWindow.Singleton.ShowTip("已经达到最大值了，不能在增加了");
            return;
        }
        t_itemBean itemBean = ConfigBean.GetBean<t_itemBean, int>(gridInfo.itemInfo.id);
        if(itemBean != null)
            DataManager.addExp += itemBean.t_soul_exp;
        DataManager.UseCaiLiaoNum++;
        RefreshPrograssBar();
        RefreshMaterialView();
        ZhanHunMaterialItem materialIndex;
        if (selectIndex != -1)
        {

            materialIndex = GetItemByItemIndex(selectIndex);
        }
    }

    private void OnClickReduceBtn()
    {
        if (DataManager.curSelectGridID == -1)
            return;

        if (DataManager.UseCaiLiaoNum <= 0)
        {
            Logger.log("已经没有材料可减少了!");
            return;
        }

        Message.Bag.GridInfo gridInfo = BagService.Singleton.GetGrid(DataManager.curSelectGridID);
        t_itemBean itemBean = ConfigBean.GetBean<t_itemBean, int>(gridInfo.itemInfo.id);
        DataManager.addExp -= itemBean.t_soul_exp;
        DataManager.UseCaiLiaoNum--;
        RefreshPrograssBar();
        RefreshMaterialView();
        if (selectIndex != -1)
        {
            ZhanHunMaterialItem materialItem = GetItemByItemIndex(selectIndex);
            if (materialItem != null)
                materialItem.RefreshItem();
        }
    }

    private void OnClickStrengthBtn()
    {
        if (DataManager.curSelectGridID == -1)
            return;

        if (DataManager.IsEnoughGold())
        {
            // 发送强化请求
            DataManager.ReqZhanHunStrength(_threenPara.value3, _threenPara.value2);
        }
        else
        {
            TipWindow.Singleton.ShowTip("金币不足!");
        }
    }

    private void OnClickZuanShiStrengthBtn()
    {
        Logger.log("click zuan shi strengthBtn");
        // 打开钻石强化界面
        WinInfo info = new WinInfo();
        info.param = _threenPara;
        WinMgr.Singleton.Open<ZHDiamondStrenthWindow>(info, UILayer.Popup);
    }

    private void OnMaterialValueChanged()
    {
        Message.Bag.GridInfo gridInfo = BagService.Singleton.GetGrid(DataManager.curSelectGridID);
        if (gridInfo == null)
        {
            return;
        }
        if(_window.m_popupView.m_materialSlider.value > sliderOldValue)
        {
            if (ExpIsFull())
            {
                TipWindow.Singleton.ShowTip("战魂等级已满");
                _window.m_popupView.m_materialSlider.value = sliderOldValue;
                return; 
            }
        }
        t_itemBean itemBean = ConfigBean.GetBean<t_itemBean, int>(gridInfo.itemInfo.id);
        DataManager.addExp += itemBean.t_soul_exp * ((int)_window.m_popupView.m_materialSlider.value - DataManager.UseCaiLiaoNum);
        DataManager.UseCaiLiaoNum = (int)_window.m_popupView.m_materialSlider.value;
        RefreshPrograssBar();
        RefreshMaterialView();
        if (selectIndex != -1)
        {
            ZhanHunMaterialItem materialItem = GetItemByItemIndex(selectIndex);
            if (materialItem != null)
                materialItem.RefreshItem();
        }

        sliderOldValue = _window.m_popupView.m_materialSlider.value;
    }

    private void OnMaterialListClickItem()
    {
        ZhanHunMaterialItem materialItem = _window.m_popupView.m_zhanHunCaiLiaoList.touchItem as ZhanHunMaterialItem;
        if (DataManager.curSelectGridID == materialItem.gridID)
        {
            Logger.log("id 相同");
            return;
        }
        DataManager.curSelectGridID = materialItem.gridID;
        if (selectIndex != -1)
        {
            ZhanHunMaterialItem selelctItem = GetItemByItemIndex(selectIndex);
            if (selelctItem != null)
                selelctItem.m_selectIcon.visible = false;
        }
        materialItem.m_selectIcon.visible = true;
        selectIndex = _window.m_popupView.m_zhanHunCaiLiaoList.GetChildIndex(materialItem);
        selectIndex = _window.m_popupView.m_zhanHunCaiLiaoList.ChildIndexToItemIndex(selectIndex);

        RefreshMaterialView();
    }

    private void OnStrengthSuccess(GameEvent evt)
    {
        PetInfo petInfo = evt.Data as PetInfo;
        soulInfo = petInfo.soulInfo.souls[_threenPara.value2];
        // 清空数据
        DataManager.RefreshData();
        RefreshZhanHunDetailView();
        if (ExpIsFull())
        {
            _window.m_popupView.m_fullGroup.visible = true;
            _window.m_popupView.m_unfullGroup.visible = false;
            _window.m_popupView.m_previewLvGroup.visible = false;
            _window.m_popupView.m_tempProBar.visible = false;
            _window.m_popupView.m_realProBar.visible = true;
            _window.m_popupView.m_realProBar.value = _window.m_popupView.m_realProBar.max;
        }
        else
        {
            _window.m_popupView.m_fullGroup.visible = false;
            _window.m_popupView.m_unfullGroup.visible = true;
            RefreshPrograssBar();
            RefreshMaterialView();
            RefreshMaterialList();
        }
    }
}
