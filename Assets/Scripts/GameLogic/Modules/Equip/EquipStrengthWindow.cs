using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_Equip;
using FairyGUI;
using Message.Pet;
using Data.Beans;

public class EquipStrengthWindow : BaseWindow {

    UI_EquipStrengthWindow _window;
    EquipStrengthPanel _strengthPanel;
    EquipDataManager _equipDataManager;
    PetItem curSelectPetItem;
    JueXingPanl juexing;
    List<EquipItem> equipItemList = new List<EquipItem>();
    public EquipItem curSelectedEquipItem;
    public bool isCanClick = false;

    ActorUI actor;

    UITable table;

    public EquipDataManager equipData
    {
        get { return _equipDataManager; }
    }

    public override void OnOpen()
    {
        base.OnOpen();

        _window = getUiWindow<UI_EquipStrengthWindow>();
        ThreeParam<int, int, int> param = (ThreeParam<int, int, int>)Info.param;
        _equipDataManager = new EquipDataManager(param);
        if (_equipDataManager.PetInfoList == null || _equipDataManager.PetInfoList.Count <= 0)
            return;

        TouchEnable();
        InitView();
        PlayOpenEffect();
    }

    public override void PlayOpenEffect()
    {
        base.PlayOpenEffect();

        (_window.m_comomTop as UI_Common.UI_commonTop).m_anim.Play();
        _window.m_equipToggleGroup.m_anim.Play();
    }

    public override void AddEventListener()
    {
        base.AddEventListener();

        GED.ED.addListener(EventID.OnPetShuXingChanged, OnPetAttributeChanged);
        GED.ED.addListener(EventID.ResBagUpdate, OnBagUpdate);
    }

    public override void RemoveEventListener()
    {
        base.RemoveEventListener();

        GED.ED.removeListener(EventID.OnPetShuXingChanged, OnPetAttributeChanged);
        GED.ED.removeListener(EventID.ResBagUpdate, OnBagUpdate);
    }

    private void OnPetAttributeChanged(GameEvent evt)
    {
        PetInfo petInfo = evt.Data as PetInfo;
        equipData.CurSelectPetID = petInfo.petId;
        equipData.RefreshData();


        RefreshEquipItem();
        RefreshPetList();
        RefreshPetInfo();
        table.Refresh(true);
    }

    private void OnBagUpdate(GameEvent evt)
    {
        RefreshPetList();
        RefreshEquipItem();
    }

    public override void InitView()
    {
        base.InitView();

        InitPetList();
        InitEquipItems();
        BindEvent();
        InitChildPanel();
        InitSwitchBtnState();
        RefreshPetInfo();
        _window.m_equipCtril.selectedIndex = _equipDataManager.StartPage;
        (_window.m_comomTop as UI_Common.UI_commonTop).m_title.text = "装备强化";
    }

    private void InitChildPanel()
    {
        _strengthPanel = new EquipStrengthPanel(this, _window.m_strengthPanel);
        juexing = new JueXingPanl(this, _window.m_jueXingPanel);

        table = new UITable();
        table.Init(_window.m_equipToggleGroup.m_ctrl, OnToggleChange, _strengthPanel, juexing);
        table.AddFuncLock(1, 1103, _window.m_equipToggleGroup.m_awakenBtn);
        table.Refresh();

        table.AddBtnAnim(_window.m_equipToggleGroup.m_awakenBtn);
        table.AddBtnAnim(_window.m_equipToggleGroup.m_strengthBtn);
    }

    private void BindEvent()
    {
        (_window.m_comomTop as UI_Common.UI_commonTop).m_closeBtn.onClick.Add(OnCloseBtn);
        _window.m_petList.onClickItem.Add(OnClickPetItem);
        _window.m_switchLeftBtn.onClick.Add(OnSwitchLeftBtnClick);
        _window.m_switchRightBtn.onClick.Add(OnSwitchRightBtnClick);
        _window.m_modelToucher.onClick.Add(OnModelClick);
    }

    private void OnToggleChange(int index)
    {
        _window.m_equipCtril.selectedIndex = _window.m_equipToggleGroup.m_ctrl.selectedIndex;
    }

    private void InitEquipItems()
    {

        t_petBean petBean = ConfigBean.GetBean<t_petBean, int>(equipData.CurSelectPetID);
        if (petBean == null)
            return;
        string[] typeArr = petBean.t_commet_equip_id.Split('+');

        EquipItem weaponItem = _window.m_weaponItem as EquipItem;
        weaponItem.selectedPos = equipData.CurSelectEquipPos;
        weaponItem.Init((int)EquipPosition.Weapon, equipData.CurSelectPetID, int.Parse(typeArr[0]), OnEquipClick);

        EquipItem clothItem = _window.m_clothItem as EquipItem;
        clothItem.selectedPos = equipData.CurSelectEquipPos;
        clothItem.Init((int)EquipPosition.Cloth, equipData.CurSelectPetID, int.Parse(typeArr[1]), OnEquipClick);

        EquipItem kuZiItem = _window.m_kuZiItem as EquipItem;
        kuZiItem.selectedPos = equipData.CurSelectEquipPos;
        kuZiItem.Init((int)EquipPosition.KuZi, equipData.CurSelectPetID, int.Parse(typeArr[2]), OnEquipClick);

        EquipItem shoesItem = _window.m_shoesItem as EquipItem;
        shoesItem.selectedPos = equipData.CurSelectEquipPos;
        shoesItem.Init((int)EquipPosition.Shoes, equipData.CurSelectPetID, int.Parse(typeArr[3]), OnEquipClick);

        EquipItem huiZhanItem = _window.m_huiZhanItem as EquipItem;
        huiZhanItem.selectedPos = equipData.CurSelectEquipPos;
        huiZhanItem.Init((int)EquipPosition.HuiZhan, equipData.CurSelectPetID, -1, OnEquipClick);
        FuncService.Singleton.SetFuncLock(huiZhanItem, 1102);

        EquipItem miJiItem = _window.m_miJiItem as EquipItem;
        miJiItem.selectedPos = equipData.CurSelectEquipPos;
        miJiItem.Init((int)EquipPosition.MiJi, equipData.CurSelectPetID, -1, OnEquipClick);
        FuncService.Singleton.SetFuncLock(miJiItem, 1102);

        equipItemList.Add(weaponItem);
        equipItemList.Add(clothItem);
        equipItemList.Add(kuZiItem);
        equipItemList.Add(shoesItem);
        equipItemList.Add(huiZhanItem);
        equipItemList.Add(miJiItem);

        // 设置默认选择的装备
        EquipItem item = null;
        for (int i = 0; i < equipItemList.Count; i++)
        {
            item = equipItemList[i];
            if (item.equipPos == equipData.CurSelectEquipPos)
            {
                curSelectedEquipItem = item;
                break;
            }
        }
    }

    private void RefreshEquipItem()
    {
        int count = equipItemList.Count;
        EquipItem equipItem = null;
        for (int i = 0; i < count; i++)
        {
            equipItem = equipItemList[i];
            equipItem.petID = equipData.CurSelectPetID;
            equipItem.selectedPos = equipData.CurSelectEquipPos;
            equipItem.RefreshItem();
        }
    }

    public override void RefreshView()
    {
        base.RefreshView();

        equipData.SetSpecialExpDict();
        PetEquip petEquip = equipData.GetCurSelectPetEquip();
        equipData.SetSPMatreials(petEquip.color);
        table.Refresh();
        RefreshEquipItem();
    }

    private void InitPetList()
    {
        _window.m_petList.itemRenderer = RenderListItem;
        _window.m_petList.SetVirtual();
        _window.m_petList.numItems = equipData.PetInfoList.Count;
        int scrollIndex = equipData.GetInitScrollIndex();
        _window.m_petList.ScrollToView(scrollIndex);
    }

    private void RefreshPetList()
    {
        _window.m_petList.numItems = equipData.PetInfoList.Count; 
    }

    private void RenderListItem(int index, GObject obj)
    {
        PetItem petItem = obj as PetItem;
        petItem.petID = equipData.PetInfoList[index].petId;
        petItem.RefreshItem(equipData.CurSelectPetID, PetItemType.PetEquip);
        if (petItem.petID == equipData.CurSelectPetID)
        {
            curSelectPetItem = petItem;
        }
    }

    private void OnClickPetItem()
    {
        PetItem petItem = _window.m_petList.touchItem as PetItem;
        OnPetChange(petItem);
    }

    private void OnEquipClick(EquipItem sender)
    {
        if (sender.equipPos != equipData.CurSelectEquipPos)
        {
            equipData.CurSelectEquipPos = sender.equipPos;
            equipData.CurSelectEquipType = sender.equipType;
            curSelectedEquipItem = sender;
            RefreshView();
        }
    }

    private void OnPetChange(PetItem petItem)
    {
        if (petItem.petID == equipData.CurSelectPetID)
            return;

        equipData.CurSelectPetID = petItem.petID;
        curSelectPetItem.RefreshItem(equipData.CurSelectPetID, PetItemType.PetEquip);
        curSelectPetItem = petItem;
        curSelectPetItem.RefreshItem(equipData.CurSelectPetID, PetItemType.PetEquip);

        RefreshView();
        RefreshPetInfo();
    }

    private void InitSwitchBtnState()
    {
        int petCount = equipData.PetInfoList.Count;
        _window.m_switchLeftBtn.visible = petCount > 1;
        _window.m_switchRightBtn.visible = petCount > 1;
    }

    protected override void OnCloseBtn()
    {

        if (_window != null)
        {
            _window = null;
            curSelectPetItem = null;
        }

        if (_equipDataManager != null)
        {
            _equipDataManager.OnClose();
            _equipDataManager = null;
        }

        table.Close();

        base.OnCloseBtn();
    }

    private void RefreshPetInfo()
    {
        RefreshPetModel();
        RefreshPetBaseInfo();
    }

    private void RefreshPetBaseInfo()
    {
        var petInfo = PetService.Singleton.GetPetByID(equipData.CurSelectPetID);
        var petBean = ConfigBean.GetBean<Data.Beans.t_petBean, int>(petInfo.petId);
        _window.m_petNameLabel.text = UIUtils.GetPingJiePetName(petInfo.petId, petInfo.basInfo.color, petInfo.basInfo.star);
        _window.m_petNameLabel.color = UIUtils.GetColorByQuality(petInfo.basInfo.color);

        if (petBean != null)
        {
            UIGloader.SetUrl(_window.m_petTypeLoader,UIUtils.GetLoaderUrl(WinEnum.UI_Common, UIUtils.GetPetTypeUrl(petBean.t_type)));
        }
    }

    private void RefreshPetModel()
    {
        this.CacheWrapper(_window.m_modelPos);
        GoWrapper wrapper = new GoWrapper();
        _window.m_modelPos.SetNativeObject(wrapper);
        PetInfo petInfo = PetService.Singleton.GetPetInfo(equipData.CurSelectPetID);
        int star = petInfo == null ? -1 : petInfo.basInfo.star;
        actor = this.NewActorUI(equipData.CurSelectPetID, star, ActorType.Pet, wrapper);
        actor.SetTransform(new Vector3(-58f, -9, 200), 200, new Vector3(0,160,0));
        actor.MouseRotate(_window.m_modelToucher);
        actor.PlayRandomAni(OnAnimEnd);
        isCanClick = false;
    }

    private void OnSwitchLeftBtnClick()
    {
        int firstPetID = equipData.PetInfoList[0].petId;
        int itemIndex = _window.m_petList.GetChildIndex(curSelectPetItem);
        itemIndex = _window.m_petList.ChildIndexToItemIndex(itemIndex);
        bool isAnim = false;
        if (equipData.CurSelectPetID == firstPetID)
        {
            // 第一个，转到最后一个
            itemIndex = equipData.PetInfoList.Count - 1;
            isAnim = false;
        }
        else
            itemIndex--;
        _window.m_petList.ScrollToView(itemIndex, isAnim);
        int realIndex = _window.m_petList.ItemIndexToChildIndex(itemIndex);
        PetItem petItem = _window.m_petList.GetChildAt(realIndex) as PetItem;
        OnPetChange(petItem);
    }

    private void OnSwitchRightBtnClick()
    {
        int endPetID = equipData.PetInfoList[equipData.PetInfoList.Count - 1].petId;
        int itemIndex = _window.m_petList.GetChildIndex(curSelectPetItem);
        itemIndex = _window.m_petList.ChildIndexToItemIndex(itemIndex);
        bool isAnim = false;
        if (equipData.CurSelectPetID == endPetID)
        {
            // 最后一个转到第一个
            itemIndex = 0;
            isAnim = false;
        }
        else
            itemIndex++;
        _window.m_petList.ScrollToView(itemIndex, isAnim);
        int realIndex = _window.m_petList.ItemIndexToChildIndex(itemIndex);
        PetItem petItem = _window.m_petList.GetChildAt(realIndex) as PetItem;
        OnPetChange(petItem);
    }

    public void TouchEnable()
    {
        _window.m_mask.touchable = false;
    }

    public void TouchUnEnable()
    {
        _window.m_mask.touchable = true;
    }

    private void OnModelClick()
    {
        if (isCanClick == true)
        {
            actor.PlayRandomAni(OnAnimEnd);
            isCanClick = false;
        }
    }

    private void OnAnimEnd()
    {
        isCanClick = true;
    }
}
