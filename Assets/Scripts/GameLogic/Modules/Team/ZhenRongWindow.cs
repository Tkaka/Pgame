using Data.Beans;
using FairyGUI;
using Message.Pet;
using System.Collections.Generic;
using UI_BuZhen;
using UnityEngine;

//可以合成的地方加红点的预计方案是给按键动态生成一个装载器,装载器上添加图片即可
/// <summary>
/// 动态创建装载器
/// GLoader aLoader = new GLoader();
///aLoader.SetSize(100,100);
///aLoader.url = "ui://包名/图片名";
/// </summary>

public class ZhenRongWindow : BaseWindow
{

    private UI_ZhenRongWindow window;
    //创建动画控制器
    public ActorUI actor = null;

    private int level;
    private List<EquipItem> equipList = new List<EquipItem>();
    private List<ZhenRongPetItem> petItemList = new List<ZhenRongPetItem>();
    private List<int> petIDList = new List<int>();
    /// <summary>
    ///当前选中的宠物
    /// </summary>
    private ZhenRongPetItem curSelectPet;
    private bool isCanClickModel = true;
    private bool isClickChangeBtn = false;
    /// <summary>
    /// 当前选择的宠物下标
    /// </summary>
    private int curIndex;
    //当前模型id
    private int curPetId;
    public int CurPetID
    {
        get { return curPetId; }
    }

    public override void OnOpen()
    {
        base.OnOpen();
        window = getUiWindow<UI_ZhenRongWindow>();
        AddKeyEvent();
        AddBtnLock();
        InitView();
        RefreshView();
    }

    public override void PlayOpenEffect()
    {
        base.PlayOpenEffect();

        (window.m_commonTop as UI_Common.UI_commonTop).m_anim.Play();
    }
    private void AddKeyEvent()
    {
        window.m_PeiYuBtn.onClick.Add(OnPeiYuBtn);
        window.m_ZhuangBeiBtn.onClick.Add(OnZhuangBeiBtn);
        (window.m_commonTop as UI_Common.UI_commonTop).m_closeBtn.onClick.Add(OnCloseBtn);
        window.m_buZhenBtn.onClick.Add(OnBuZhenBtn);
        //为更换按键添加事件，转到宠物更换列表
        window.m_genHuanBtn.onClick.Add(OnGengHuan);
        window.m_xiangQingToucher.onClick.Add(OnXiangQing);
        window.m_toucher.onClick.Add(OnClickPet);
        window.m_jinhualian.onClick.Add(OnopenJinHuLian);
    }

    public override void AddEventListener()
    {
        base.AddEventListener();
        GED.ED.addListener(EventID.OnShangZhenChongWuId, OnGengHuanMoXing);
        GED.ED.addListener(EventID.ResBagUpdate, OnBgUpdate);
        GED.ED.addListener(EventID.OnPetTeamListChanged, PetTeamListChanged);
        GED.ED.addListener(EventID.OnPetShuXingChanged, OnPetShuXingChanged);
    }

    public override void RemoveEventListener()
    {
        base.RemoveEventListener();
        GED.ED.removeListener(EventID.OnShangZhenChongWuId, OnGengHuanMoXing);
        GED.ED.removeListener(EventID.ResBagUpdate, OnBgUpdate);
        GED.ED.removeListener(EventID.OnPetTeamListChanged, PetTeamListChanged);
        GED.ED.removeListener(EventID.OnPetShuXingChanged, OnPetShuXingChanged);
    }

    private void AddBtnLock()
    {
        FuncService.Singleton.SetFuncLock(window.m_ZhuangBeiBtn, 1101);
    }

    public override void InitView()
    {
        
        base.InitView();

        InitData();
        InitEquipList();
        InitPetList();

        (window.m_commonTop as UI_Common.UI_commonTop).m_title.text = "阵容";
    }
    private void OnopenJinHuLian()
    {
        WinMgr.Singleton.Open<JinHuaLianWindow>(WinInfo.Create(false,null,false,curPetId),UILayer.Popup);
    }
    private void InitData()
    {
        SetPetIDList();
        if (Info.param != null)
        {
            curPetId = (int)Info.param;
        }
        else
        {
            // 默认选中第一个
            curPetId = petIDList[0];
            curIndex = 0;
        }
    }

    /// <summary>
    /// 初始话装备列表
    /// </summary>
    private void InitEquipList()
    {
        equipList.Clear();
        EquipItem equipItem = window.m_equipItem0 as EquipItem;
        equipItem.isShowSelect = false;
        equipItem.Init(0, curPetId, -1, OnClickEquipItem);
        equipList.Add(equipItem);

        equipItem = window.m_equipItem1 as EquipItem;
        equipItem.isShowSelect = false;
        equipItem.Init(1, curPetId, -1, OnClickEquipItem);
        equipList.Add(equipItem);


        equipItem = window.m_equipItem2 as EquipItem;
        equipItem.isShowSelect = false;
        equipItem.Init(2, curPetId, -1, OnClickEquipItem);
        equipList.Add(equipItem);

        equipItem = window.m_equipItem3 as EquipItem;
        equipItem.isShowSelect = false;
        equipItem.Init(3, curPetId, -1, OnClickEquipItem);
        equipList.Add(equipItem);

        equipItem = window.m_equipItem4 as EquipItem;
        equipItem.isShowSelect = false;
        equipItem.Init(4, curPetId, -1, OnClickEquipItem);
        equipList.Add(equipItem);

        equipItem = window.m_equipItem5 as EquipItem;
        equipItem.isShowSelect = false;
        equipItem.Init(5, curPetId, -1, OnClickEquipItem);
        equipList.Add(equipItem);
    }
    /// <summary>
    /// 初始化宠物列表
    /// </summary>
    private void InitPetList()
    {
        petItemList.Clear();
        ZhenRongPetItem petItem = window.m_petItem0 as ZhenRongPetItem;
        petItem.index = 0;
        petItem.Init(petIDList[0], curPetId, OnClickPetItem);
        petItemList.Add(petItem);

        petItem = window.m_petItem1 as ZhenRongPetItem;
        petItem.index = 1;
        petItem.Init(petIDList[1], curPetId, OnClickPetItem);
        petItemList.Add(petItem);

        petItem = window.m_petItem2 as ZhenRongPetItem;
        petItem.index = 2;
        petItem.Init(petIDList[2], curPetId, OnClickPetItem);
        petItemList.Add(petItem);

        petItem = window.m_petItem3 as ZhenRongPetItem;
        petItem.index = 3;
        petItem.Init(petIDList[3], curPetId, OnClickPetItem);
        petItemList.Add(petItem);

        petItem = window.m_petItem4 as ZhenRongPetItem;
        petItem.index = 4;
        petItem.Init(petIDList[4], curPetId, OnClickPetItem);
        petItemList.Add(petItem);

        petItem = window.m_petItem5 as ZhenRongPetItem;
        petItem.index = 5;
        petItem.Init(petIDList[5], curPetId, OnClickPetItem);
        petItemList.Add(petItem);
    }

    public override void RefreshView()
    {
        base.RefreshView();
        RefreshPetInfo();
    }
    /// <summary>
    /// 刷新宠物信息
    /// </summary>
    private void RefreshPetInfo()
    {
        RefreshPetBaseInfo();
        RefreshPetEquipInfo();
        RefreshPetModel();
    }
    /// <summary>
    /// 刷新宠物基本信息
    /// </summary>
    private void RefreshPetBaseInfo()
    {
        t_petBean petBean = ConfigBean.GetBean<t_petBean, int>(curPetId);
        PetInfo petInfo = PetService.Singleton.GetPetInfo(curPetId);
        if (petBean != null)
        {
            UIGloader.SetUrl(window.m_typeLoader, UIUtils.GetLoaderUrl(WinEnum.UI_Common, UIUtils.GetPetTypeUrl(petBean.t_type)));
            if (petInfo != null)
            {
                int star = petInfo.basInfo.star;
                window.m_nameLabel.text = UIUtils.GetPetName(petBean, star);
                window.m_nameLabel.color = UIUtils.GetColorByQuality(petInfo.basInfo.color);

                int count = PetService.Singleton.GetPetMaxStar();
                window.m_starList.RemoveChildren(0, -1, true);
                for (int i = 0; i < count; i++)
                {
                    window.m_starList.AddChild(GetPetStarObj(i < star));
                }

                window.m_dengji_num.text = petInfo.basInfo.level + "";
                window.m_zizhi_num.text = UIUtils.GetZiZhiStr(petBean.t_zizhi);
            }
        }
        // 属性值
        window.m_petZhanDouLi.text = PetService.Singleton.GetPetFightPower(curPetId) + "";
        window.m_gongji_num.text = Mathf.FloorToInt(PetService.Singleton.GetPetPropertyValue(curPetId, PropertyType.Atk)) + "";
        window.m_fangyu_num.text = Mathf.FloorToInt(PetService.Singleton.GetPetPropertyValue(curPetId, PropertyType.Def)) + "";
        window.m_shengming_num.text = Mathf.FloorToInt(PetService.Singleton.GetPetPropertyValue(curPetId, PropertyType.Hp)) + "";
        // 队伍战斗力
        window.m_teamZhanDouLi.text = PetService.Singleton.GetRoleFightPower() + "";
        // 羁绊
        //宠物宿命id列表
        if (petBean != null)
        {
            int[] names = GTools.splitStringToIntArray(petBean.t_fetter);
            int count = names.Length;
            string jiHuoColor = "[color=#FFE166]{0}[/color]";
            string noramColor = "[color=#FFFFFF]{0}[/color]";
            window.m_jiBianText.text = "";
            if (count > 1)
            {
                t_fetterBean fetterBean;
                for (int i = 0; i < count; ++i)
                {
                    fetterBean = ConfigBean.GetBean<t_fetterBean, int>(names[i]);
                    if (fetterBean != null)
                    {
                        //判断宿命是否激活
                        if (UIUtils.OnFetterState(curPetId, names[i]))
                        {
                            // 羁绊激活
                            window.m_jiBianText.text += string.Format(jiHuoColor, fetterBean.t_name);
                            window.m_jiBianText.text += "\n";
                        }
                        else
                        {
                            // 羁绊没激活
                            window.m_jiBianText.text += string.Format(noramColor, fetterBean.t_name);
                            window.m_jiBianText.text += "\n";
                        }
                    }
                    
                }
            }
        }
    }
    /// <summary>
    /// 刷新宠物装备信息
    /// </summary>
    private void RefreshPetEquipInfo()
    {
        int num = equipList.Count;
        EquipItem equipItem;
        for (int i = 0; i < num; i++)
        {
            equipItem = equipList[i];
            equipItem.petID = curPetId;
            equipItem.RefreshItem();
        }
    }
    /// <summary>
    /// 刷新模型
    /// </summary>
    private void RefreshPetModel()
    {
        this.CacheWrapper(window.m_holder);
        var warrper = new GoWrapper();
        window.m_holder.SetNativeObject(warrper);
        PetInfo petInfo = PetService.Singleton.GetPetInfo(curPetId);
        int star = petInfo == null ? -1 : petInfo.basInfo.star;
        actor = this.NewActorUI(curPetId, star, ActorType.Pet, warrper);
        actor.SetTransform(new Vector3(0, -210, 900), 450, new Vector3(0, 180, 0));
        actor.MouseRotate(window.m_toucher);
        actor.PlayRandomAni(OnAnimEndCall);
        isCanClickModel = false;
    }

    private void RefreshPetList()
    {
        int count = petItemList.Count;
        ZhenRongPetItem petItem = null;
        for (int i = 0; i < count; i++)
        {
            petItem = petItemList[i];
            petItem.petID = petIDList[i];
            petItem.selectID = curPetId;
            petItem.RefreshItem();
        }
    }
    private GObject GetPetStarObj(bool isBright)
    {
        if (isBright)
            return UIPackage.CreateObject(WinEnum.UI_Common, "xing01");
        else
            return UIPackage.CreateObject(WinEnum.UI_Common, "xing02");
    }

    private void OnClickPet(EventContext context)
    {
        if (actor != null && isCanClickModel)
        {
            actor.PlayRandomAni(OnAnimEndCall);
            isCanClickModel = false;
        }
    }
    #region 数据处理
    private void SetPetIDList()
    {
        petIDList.Clear();
        petIDList.AddRange(PetService.Singleton.GetTeamList(ZhenRongType.Normal, false));
        // 排序，宠物向前靠
        petIDList.Sort(SortPetList);
    }

    private int SortPetList(int a , int b)
    {
        if (a == 0)
            return 1;
        if (b == 0)
            return -1;

        return -1;
    }
    #endregion

    #region 按钮事件
    private void OnPeiYuBtn()
    {
        TwoParam<int, StrengthType> twoParam = new TwoParam<int, StrengthType>();
        twoParam.value1 = curPetId;                        // 宠物ID
        twoParam.value2 = StrengthType.None;            // 强化类型
        WinMgr.Singleton.Open<StrengthWindow>(WinInfo.Create(false, null, false, twoParam), UILayer.Popup);
    }
    private void OnZhuangBeiBtn()
    {
        if (FuncService.Singleton.TipFuncNotOpen(1101))
        {
            ThreeParam<int, int, int> twoPara = new ThreeParam<int, int, int>();
            twoPara.value1 = curPetId;                                 // 宠物ID
            twoPara.value2 = (int)EquipPosition.Weapon;             // 装备部位
            twoPara.value3 = (int)EquipPanelType.EquipStrength;     // 打开的类型
            WinMgr.Singleton.Open<EquipStrengthWindow>(WinInfo.Create(false, null, false, twoPara), UILayer.Popup);
        }
    }
    private void OnBgUpdate(GameEvent evt)
    {
        RefreshPetEquipInfo();
    }

    private void OnClickEquipItem(EquipItem sender)
    {
        // 进入装备强化界面
        if (FuncService.Singleton.TipFuncNotOpen(1101))
        {
            ThreeParam<int, int, int> twoPara = new ThreeParam<int, int, int>();
            twoPara.value1 = curPetId;                                 // 宠物ID
            twoPara.value2 = (int)EquipPosition.Weapon;             // 装备部位
            twoPara.value3 = (int)EquipPanelType.EquipStrength;     // 打开的类型
            WinMgr.Singleton.Open<EquipStrengthWindow>(WinInfo.Create(false, null, false, twoPara), UILayer.Popup);
        }
    }

    private void OnClickPetItem(ZhenRongPetItem sender)
    {
        curIndex = sender.index;
        if (sender.petID == 0)
        {
            // 更换格斗家窗口
            TwoParam<int, ShangZhenSelectType> param = new TwoParam<int, ShangZhenSelectType>();
            param.value1 = 0;
            param.value2 = ShangZhenSelectType.Default;
            WinMgr.Singleton.Open<ShangZhenWindow>(WinInfo.Create(false, null, true, param), UILayer.Popup);
        }
        else
        {
            // 刷新面板信息

            curPetId = sender.petID;
            RefreshPetList();
            RefreshView();
        }
    }

    private void OnBuZhenBtn()
    {
        OpenChild<BuZhenWindow>();
    }
    private void OnXiangQing()
    {
        if (curPetId != 0)
        {
            WinInfo winInfo = new WinInfo();
            TwoParam<int, XiangQingType> param = new TwoParam<int, XiangQingType>();
            param.value1 = curPetId;
            param.value2 = XiangQingType.ShangZhenLieBiao;
            winInfo.param = param;
            WinMgr.Singleton.Open<ChongWuXiangQingWindow>(winInfo, UILayer.Popup);
        }
    }

    //跳转到格斗家更换列表窗口
    private void OnGengHuan()
    {
        TwoParam<int, ShangZhenSelectType> param = new TwoParam<int, ShangZhenSelectType>();
        param.value1 = curPetId;
        param.value2 = ShangZhenSelectType.Default;
        WinMgr.Singleton.Open<ShangZhenWindow>(WinInfo.Create(false, null, true, param), UILayer.Popup);
    }
    private void OnGengHuanMoXing(GameEvent evt)
    {
        TwoParam<int, int> param = evt.Data as TwoParam<int, int>;
        if (param.value2 == 0)
        {
            return;
        }
        // 更新表和窗口
        // 跟换宠物列表
        petIDList[curIndex] = param.value2;
        curPetId = param.value2;
    }

    private void PetTeamListChanged(GameEvent evt)
    {
        RefreshView();
        RefreshPetList();
    }

    private void OnPetShuXingChanged(GameEvent evt)
    {
        RefreshPetList();
        RefreshPetInfo();
    }

    private void OnAnimEndCall()
    {
        isCanClickModel = true;
    }

    #endregion;

    protected override void OnCloseBtn()
    {
        RemoveEventListener();
        actor.destoryMe();
        actor = null;
        window = null;

        Close();
    }
}
