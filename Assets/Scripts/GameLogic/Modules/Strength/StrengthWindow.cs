
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_Strength;
using UI_Common;
using FairyGUI;
using Message.Pet;
using Message.Role;

public class StrengthWindow : BaseWindow
{

    private UI_StrengthWindow window;
    public UI_StrengthWindow Window
    {
        get { return window; }
    }
    private StrengthShengPing shengPingUI;
    private StrengthShengJi shengJiUI;
    private JinHuaPanel jinHuaPanel;
    private StrengthSkillPanel skillUI;
    private ZhanHunPanel zhanHunUI;
    private PetItem curSelectPetItem;
    UITable table;

    private ActorUI actor;

    private List<PetInfo> petInfoList;

    public StrengthDataManager strengthData;

    private bool isCanClick = true;
    public override void OnOpen()
    {
        base.OnOpen();

        InitData();
        InitView();
        TouchEnable();
        PlayOpenEffect();
    }

    private void InitData()
    {
        TwoParam<int, StrengthType> infoParm = Info.param as TwoParam<int, StrengthType>;
        int petID = infoParm.value1;
        // 如果传过来的宠物ID为0，那么默认选择第一个宠物
        petInfoList = new List<PetInfo>();
        petInfoList.AddRange(PetService.Singleton.GetPetInfos(true));
        if (petID == 0 && petInfoList.Count > 0)
        {
            petID = petInfoList[0].petId;
        }

        strengthData = new StrengthDataManager();
        strengthData.Init();
        strengthData.CurSelectPetInfo = PetService.Singleton.GetPetByID(petID);
        strengthData.CurSelectType = infoParm.value2;
    }

    public override void PlayOpenEffect()
    {
        base.PlayOpenEffect();

        (window.m_commonTop as UI_commonTop).m_anim.Play();
        window.m_toggleGroup.m_anim.Play();
    }

    public override void InitView()
    {
        base.InitView();

        window = getUiWindow<UI_StrengthWindow>();

        (window.m_commonTop as UI_commonTop).m_title.text = "宝可梦强化";

        shengJiUI = new StrengthShengJi(window.m_shengJi, this);
        shengPingUI = new StrengthShengPing(window.m_shengPing, this);
        jinHuaPanel = new JinHuaPanel(this);
        skillUI = new StrengthSkillPanel(this);
        zhanHunUI = new ZhanHunPanel(this);
        
        table = new UITable();
        table.Init(window.m_toggleGroup.m_ctrl, _onCtrlChange, shengPingUI, shengJiUI, jinHuaPanel, skillUI, zhanHunUI);
        table.AddFuncLock(1, 1001, window.m_toggleGroup.m_levelUpBtn);
        table.AddFuncLock(2, 1003, window.m_toggleGroup.m_starUpBtn);
        table.AddFuncLock(3, 1004, window.m_toggleGroup.m_skillBtn);
        table.AddFuncLock(4, 1005, window.m_toggleGroup.m_zhanHunBtn);

        table.AddBtnAnim(window.m_toggleGroup.m_shengPingBtn, window.m_toggleGroup.m_levelUpBtn, window.m_toggleGroup.m_starUpBtn,
            window.m_toggleGroup.m_skillBtn, window.m_toggleGroup.m_zhanHunBtn);

        // 进入默认打开那个界面
        if (strengthData.CurSelectType != StrengthType.None)
        {
            window.m_toggleGroup.m_ctrl.selectedIndex = (int)strengthData.CurSelectType;
            strengthData.CurSelectType = StrengthType.None;
        }
        table.Refresh(false);
        
        (window.m_commonTop as UI_commonTop).m_closeBtn.onClick.Add(OnCloseBtn);
        window.m_switchLeftBtn.onClick.Add(OnSwitchLeftBtnClick);
        window.m_switchRightBtn.onClick.Add(OnSwitchRightBtnClick);
        window.m_bottomSwitchLeftBtn.onClick.Add(OnSwitchLeftBtnClick);
        window.m_bottomSwitchRightBtn.onClick.Add(OnSwitchRightBtnClick);
        window.m_modelToucher.onClick.Add(OnClickModel);

        RefreshPetBaseInfo();
        InitPetList();
        InitBtnLimit();
        RefreshToggleRedPoint();
    }
    
    /// <summary>
    /// 刷新是否开放了按钮对应的功能
    /// </summary>
    private void InitBtnLimit()
    {
        RoleInfo role = RoleService.Singleton.GetRoleInfo();
        // TODO：读表获得限制条件
        if (role.level >= 40)
        {
            window.m_toggleGroup.m_zhanHunBtn.visible = role.level >= 40;
        }

        bool isShowSwitchBtn = petInfoList.Count > 1;
        window.m_switchLeftBtn.visible = isShowSwitchBtn;
        window.m_switchRightBtn.visible = isShowSwitchBtn;
        window.m_bottomSwitchLeftBtn.visible = isShowSwitchBtn;
        window.m_bottomSwitchRightBtn.visible = isShowSwitchBtn;
    }

    private void InitPetList()
    {
        if (PetService.Singleton.PetInfo == null)
        {
            return;
        }
        //FilterPetInfoList(petInfoList);
        var num = petInfoList.Count;

        window.m_strengthPetList.itemRenderer = RenderListItem;
        window.m_strengthPetList.SetVirtual();
        window.m_strengthPetList.numItems = num;
        window.m_strengthPetList.onClickItem.Add(OnClickPetItem);
        int index = strengthData.GetInitIndex();
        if (index != -1)
        {
            window.m_strengthPetList.ScrollToView(index);
        }
    }

    private void _onCtrlChange(int idx)
    {
        window.m_strengthCtrl.selectedIndex = window.m_toggleGroup.m_ctrl.selectedIndex;
        if (window.m_strengthCtrl.previsousIndex >= 3 && idx < 3)
            RefreshPetModel();

        RefreshToggleRedPoint();
    }

    private void FilterPetInfoList(List<PetInfo> petInfoList)
    {
        int num = petInfoList.Count;
        PetInfo petInfo;
        for (int i = num - 1; i >= 0; i--)
        {
            petInfo = petInfoList[i];
            if (petInfo.basInfo.level <= 0)
            {
                petInfoList.RemoveAt(i);
            }
        }
    }

    private void RenderListItem(int index, GObject obj)
    {
        PetItem petItem = obj as PetItem;
        PetInfo petInfo = petInfoList[index];
        petItem.petID = petInfo.petId;
        petItem.RefreshItem(strengthData.CurSelectPetInfo.petId, PetItemType.Pet);
        if (petInfo.petId == strengthData.CurSelectPetInfo.petId)
        {
            curSelectPetItem = petItem;
        }
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
        //strengthData.UpdateOldPetInfo();
        PetInfo petInfo = evt.Data as PetInfo;
        if (petInfo.petId == strengthData.CurSelectPetInfo.petId)
        {
            strengthData.CurSelectPetInfo = petInfo;
            curSelectPetItem.RefreshItem(strengthData.CurSelectPetInfo.petId, PetItemType.Pet);
        }

        table.Refresh(true);
        RefreshPetBaseInfo();
    }
    private void OnClickPetItem()
    {
        PetItem petItem = window.m_strengthPetList.touchItem as PetItem;
        OnPetChange(petItem);
    }

    private void OnPetChange(PetItem petItem)
    {
        PetInfo petInfo = PetService.Singleton.GetPetByID(petItem.petID);
        if (petInfo == strengthData.CurSelectPetInfo)
        {
            return;
        }
        strengthData.CurSelectPetInfo = petInfo;

        curSelectPetItem.RefreshItem(strengthData.CurSelectPetInfo.petId, PetItemType.Pet);
        curSelectPetItem = petItem;
        curSelectPetItem.RefreshItem(strengthData.CurSelectPetInfo.petId, PetItemType.Pet);

        table.Refresh(false);
        RefreshPetBaseInfo();
    }

    protected override void OnCloseBtn()
    {
        window = null;
        strengthData = null;
        table.Close();
        if (strengthData != null)
            strengthData.Close();


        base.OnCloseBtn();
    }

    /// <summary>
    /// 刷新宠物的基本信息
    /// </summary>
    private void RefreshPetBaseInfo()
    {
        RefreshPetModel();
        RefreshPetInfo();
    }
    /// <summary>
    /// 刷新頁簽上的紅點
    /// </summary>
    private void RefreshToggleRedPoint()
    {
        if (window.m_toggleGroup.m_ctrl.selectedIndex != 0)
        {
            window.m_toggleGroup.m_shengPingBtn.m_redPoint.visible = PetService.Singleton.IsPetCanColorUp(strengthData.CurSelectPetInfo.petId);
        }
        else
            window.m_toggleGroup.m_shengPingBtn.m_redPoint.visible = false;
    }
    private void RefreshPetModel()
    {
        this.CacheWrapper(window.m_modelPos);
        GoWrapper wrapper = new GoWrapper();
        window.m_modelPos.SetNativeObject(wrapper);
        int petID = strengthData.CurSelectPetInfo.petId;
        PetInfo petInfo = PetService.Singleton.GetPetInfo(petID);
        int star = petInfo == null ? -1 : petInfo.basInfo.star;
        actor = this.NewActorUI(petID, star, ActorType.Pet, wrapper);
        actor.SetTransform(new Vector3(-142.6f, -40f, 500), 260, new Vector3(0, 165.48f, 0));
        actor.PlayRandomAni(OnAnimEnd);
        // 模型旋转
        actor.MouseRotate(window.m_modelToucher);
        isCanClick = false;
    }

    private void RefreshPetInfo()
    {
        var petInfo = strengthData.CurSelectPetInfo;
        var petBean = ConfigBean.GetBean<Data.Beans.t_petBean, int>(petInfo.petId);
        window.m_petNameLabel.text = UIUtils.GetPingJiePetName(petInfo.petId, petInfo.basInfo.color, petInfo.basInfo.star);
        window.m_petNameLabel.color = UIUtils.GetColorByQuality(petInfo.basInfo.color);
        UIGloader.SetUrl(window.m_petTypeLoader,UIUtils.GetLoaderUrl(WinEnum.UI_Common, UIUtils.GetPetTypeUrl(petBean.t_type)));

        // 刷新宠物的属性值

        window.m_atkLabel.text = Mathf.FloorToInt(PetService.Singleton.GetPetPropertyValue(petInfo.petId, PropertyType.Atk)) + "";
        window.m_defLabel.text = Mathf.FloorToInt(PetService.Singleton.GetPetPropertyValue(petInfo.petId, PropertyType.Def)) + "";
        window.m_hpLabel.text = Mathf.FloorToInt(PetService.Singleton.GetPetPropertyValue(petInfo.petId, PropertyType.Hp)) + "";
        window.m_zhanDouLiLabel.text = PetService.Singleton.GetPetFightPower(petInfo.petId) + "";
        // 设置宠物星级
        int maxStar = PetService.Singleton.GetPetMaxStar();
        window.m_petStarList.RemoveChildren(0,-1,true);
        for (int i = 0; i < maxStar; i++)
        {
            if (i < petInfo.basInfo.star)
            {
                window.m_petStarList.AddChild(UIPackage.CreateObject(WinEnum.UI_Common, "xing01"));
            }
            else
            {
                window.m_petStarList.AddChild(UIPackage.CreateObject(WinEnum.UI_Common, "xing02"));
            }
        }
    }
    private void OnSwitchLeftBtnClick()
    {
        int firstPetID = petInfoList[0].petId;
        int itemIndex = window.m_strengthPetList.GetChildIndex(curSelectPetItem);
        itemIndex = window.m_strengthPetList.ChildIndexToItemIndex(itemIndex);
        bool isTween = false;
        if (itemIndex == 0)
        {
            // 第一个, 跑到最后一个
            itemIndex = petInfoList.Count - 1;
            isTween = false;
        }
        else
            itemIndex--;

        window.m_strengthPetList.ScrollToView(itemIndex, isTween);
        int realIndex = window.m_strengthPetList.ItemIndexToChildIndex(itemIndex);
        PetItem petItem = window.m_strengthPetList.GetChildAt(realIndex) as PetItem;
        OnPetChange(petItem);

    }

    private void OnSwitchRightBtnClick()
    {
        int endPetID = petInfoList[petInfoList.Count - 1].petId;
        int itemIndex = window.m_strengthPetList.GetChildIndex(curSelectPetItem);
        itemIndex = window.m_strengthPetList.ChildIndexToItemIndex(itemIndex);
        bool isTween = false;
        if (endPetID == curSelectPetItem.petID)
        {
            // 最后一个, 跑到第一个
            itemIndex = 0;
            isTween = false;
        }
        else
            itemIndex++;

        window.m_strengthPetList.ScrollToView(itemIndex, isTween);
        int realIndex = window.m_strengthPetList.ItemIndexToChildIndex(itemIndex);
        PetItem petItem = window.m_strengthPetList.GetChildAt(realIndex) as PetItem;
        OnPetChange(petItem);
    }

    public void TouchEnable()
    {
        window.m_mask.touchable = false;
    }

    public void TouchUnEnable()
    {
        window.m_mask.touchable = true;
    }

    private void OnClickModel()
    {
        if (isCanClick)
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
