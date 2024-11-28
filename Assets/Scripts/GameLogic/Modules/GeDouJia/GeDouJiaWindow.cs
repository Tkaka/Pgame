using UI_GeDouJia;
using UI_Common;
using Data.Beans;
using FairyGUI;
using System.Collections.Generic;
using Message.Pet;

public class GeDouJiaWindow : BaseWindow
{
    private UI_GeDouJiaWindow window;
    private List<PetInfo> petInfos;
    private List<t_petBean> petBeans;
    private List<t_petBean> allBeans;//所有的
    private int fengexianNumber;//加载分割线之前的item的数量
    private int zhanWeiCount;
    /// <summary>
    /// 分割线的高度
    /// </summary>
    private float fenGeXianHeight = 70f;
    private float petItemHeight = 150;

    public override void OnOpen()
    {
        base.OnOpen();
        window = getUiWindow<UI_GeDouJiaWindow>();
        allBeans = new List<t_petBean>();
        OnFenBiao();
        petBeans = allBeans;
        AddKeyEvent();
        InitView();
        PlayOpenEffect();
    }

    public override void PlayOpenEffect()
    {
        base.PlayOpenEffect();

        (window.m_BeiJing as UI_Common.UI_commonTop).m_anim.Play();
    }

    private void AddKeyEvent()
    {
        window.m_PetList.SetVirtual();
        petBeans = allBeans;
        window.m_PetList.itemRenderer = RenderListItem;
        window.m_PetList.itemProvider = itemProvider;
        ((UI_commonTop)(window.m_BeiJing)).m_closeBtn.onClick.Add(OnCloseBtn);
        //window.m_Type.onChanged.Add(OnFillChange);
    }
    public override void AddEventListener()
    {
        base.AddEventListener();
        GED.ED.addListener(EventID.ResPetFragmentCompose, OnHeCheng);
        GED.ED.addListener(EventID.OnPetShuXingChanged, OnPetPropertyChanged);
    }
    public override void RemoveEventListener()
    {
        base.RemoveEventListener();
        GED.ED.removeListener(EventID.ResPetFragmentCompose,OnHeCheng);
        GED.ED.removeListener(EventID.OnPetShuXingChanged, OnPetPropertyChanged);
    }
    private void OnHeCheng(GameEvent evt)
    {
        t_petBean t_Pet = ConfigBean.GetBean<t_petBean,int>((int)evt.Data);
        if (t_Pet == null)
        {
            Logger.err("GeDouJiaWindow:OnHeCheng:宠物列表没有对应数据------" + (int)evt.Data);
            return;
        }
        if (string.IsNullOrEmpty(t_Pet.t_name))
        {
            Logger.err("GeDouJiaWindow:OnHeCheng:宠物列表该宠物名字字段没有对应数据-----" + (int)evt.Data);
            return;
        }
        string[] names = GTools.splitString(t_Pet.t_name);
        int[] starrank = GTools.splitStringToIntArray(t_Pet.t_star_xingtai);

        RefreshView();
        PetInfo petInfo = PetService.Singleton.GetPetByID((int)evt.Data);
        TipWindow.Singleton.ShowTip("合成了宠物" + UIUtils.GetPetName(t_Pet, petInfo.basInfo.star));
        PetService.Singleton.yongyou = false;
        //WinInfo info = new WinInfo();
        //info.param = petInfo.petId;
        //WinMgr.Singleton.Open<HuoDeChongWuWindow>(info,UILayer.Popup);

        WinInfo info = new WinInfo();
        info.param = petInfo.petId;
        WinMgr.Singleton.Open<ChongWuDongHuaWindow>(info, UILayer.Popup);
    }

    /// <summary>
    ///有宠物信息改变时
    /// </summary>
    private void OnPetPropertyChanged(GameEvent evt)
    {
        RefreshView();
    }
    public override void InitView()
    {
        base.InitView();
        OnFenGeXianNumber();
        petBeans.Sort(SortImpl);
        if (IsGetFullPet())
        {
            window.m_PetList.numItems = petBeans.Count;
        }
        else
        {
            zhanWeiCount = fengexianNumber % 2 == 0 ? 1 : 2;
            window.m_PetList.numItems = petBeans.Count + zhanWeiCount + 1;
        }

        (window.m_BeiJing as UI_Common.UI_commonTop).m_title.text = "宝可梦管理";
    }

    public override void RefreshView()
    {
        base.RefreshView();
        OnFenGeXianNumber();
        petBeans.Sort(SortImpl);
        if (IsGetFullPet())
        {
            window.m_PetList.numItems = petBeans.Count;
        }
        else
        {
            zhanWeiCount = fengexianNumber % 2 == 0 ? 1 : 2;
            window.m_PetList.numItems = petBeans.Count + zhanWeiCount + 1;
        }

    }
    //列表渲染
    private void RenderListItem(int index, GObject obj)
    {
        int realIndex = index;
        if (!IsGetFullPet())
        {
            if (zhanWeiCount == 1)
            {
                if (index == fengexianNumber)
                {
                    obj.height = petItemHeight;
                    return;
                }
                if (index == fengexianNumber + 1)
                {
                    obj.height = fenGeXianHeight;
                    return;
                }
            }
            else
            {
                if (index == fengexianNumber)
                {
                    obj.height = petItemHeight;
                    return;
                }
                if (index == fengexianNumber + 1 || index == fengexianNumber + 2)
                {
                    obj.height = fenGeXianHeight;
                    return;
                }
            }

            if (index > fengexianNumber + zhanWeiCount)
                realIndex -= zhanWeiCount + 1;
        }

        PetListItem listItem = obj as PetListItem;
        listItem.height = petItemHeight;
        if (listItem.parentWindow == null)
            listItem.parentWindow = this;
        listItem.InitView(petBeans[realIndex].t_id);
    }
    //iten提供
    private string itemProvider(int index)
    {
        if (!IsGetFullPet())
        {
            if (zhanWeiCount == 1)
            {
                if (index == fengexianNumber)
                    return UI_FenGeXian.URL;
                else if (index == fengexianNumber + 1)
                    return UI_ZhanWei.URL;
            }
            else
            {
                if (index == fengexianNumber)
                    return UI_ZhanWei.URL;
                else if (index == fengexianNumber + 1)
                    return UI_FenGeXian.URL;
                else if (index == fengexianNumber + 2)
                    return UI_ZhanWei.URL;
            }
        }

        return PetListItem.URL;
    }
    protected override void OnCloseBtn()
    {
        RemoveEventListener();
        petBeans = null;
        allBeans = null;
        petInfos = null;
        window = null;
        base.OnCloseBtn();
    }
    /// <summary>
    /// 宠物拥有状态
    /// </summary>
    enum PetGetState
    {
        HeCheng = 1,              // 可合成
        Geted = 2,                // 已拥有
        UnGeted = 3,              // 未拥有
    }
    private int SortImpl(t_petBean a, t_petBean b)
    {
        if (a == null)
            return 1;
        if (b == null)
            return -1;
        // 排序规则 ： 未拥有可合成（按id排） 》 已拥有（上阵》未上阵 （战力 （按id排））） 》 未拥有（按碎片 （按id排））
        PetGetState petGetTypeA = PetGetState.UnGeted;
        PetGetState petGetTypeB = PetGetState.UnGeted;

        int fragmentA = BagService.Singleton.GetItemNum(a.t_fragment_id);
        int fragmentB = BagService.Singleton.GetItemNum(b.t_fragment_id);

        PetInfo petInfoA = PetService.Singleton.GetPetInfo(a.t_id);
        t_itemBean itemBean = null;
        // 计算A的获得状态
        if (petInfoA == null)
        {
            // 未拥有A  判断是否可合成
            itemBean = ConfigBean.GetBean<t_itemBean, int>(a.t_fragment_id);
            if(itemBean != null)
            {
                int[] number = GTools.splitStringToIntArray(itemBean.t_value);
                if (number[0] <= fragmentA)
                    petGetTypeA = PetGetState.HeCheng;
                else
                    petGetTypeA = PetGetState.UnGeted;
            }
        }
        else
            petGetTypeA = PetGetState.Geted;

        // 计算B的获得状态
        PetInfo petInfoB = PetService.Singleton.GetPetInfo(b.t_id);
        itemBean = null;
        if (petInfoB == null)
        {
            // 未拥有B  判断是否可合成
            itemBean = ConfigBean.GetBean<t_itemBean, int>(b.t_fragment_id);
            if (itemBean != null)
            {
                int[] number = GTools.splitStringToIntArray(itemBean.t_value);
                if (number[0] <= fragmentB)
                    petGetTypeB = PetGetState.HeCheng;
                else
                    petGetTypeB = PetGetState.UnGeted;
            }
        }
        else
            petGetTypeB = PetGetState.Geted;

        if (petGetTypeA != petGetTypeB)
            return ((int)petGetTypeA).CompareTo((int)petGetTypeB);
        int scoreA = 0;
        int scoreB = 0;
        if (petGetTypeA == PetGetState.Geted)
        {
            // 上阵 》 战斗力
            bool shangZhenA = PetService.Singleton.ShangZhenList(a.t_id);
            bool shangZhenB = PetService.Singleton.ShangZhenList(b.t_id);
            scoreA += shangZhenA ? 1 : 0;
            scoreB += shangZhenB ? 1 : 0;
            // 一个上阵，一个没上阵
            if (scoreA != scoreB)
                return -scoreA.CompareTo(scoreB);

            // 按战力排
            float fightPowerA = PetService.Singleton.GetPetFightPower(petInfoA.petId);
            float fightPowerB = PetService.Singleton.GetPetFightPower(petInfoB.petId);
            if (fightPowerA != fightPowerB)
                return -fightPowerA.CompareTo(fightPowerB);
        }
        else
        {
            // 按碎片数量
            if(fragmentA != fragmentB)
                return -fragmentA.CompareTo(fragmentB);
        }
        // 按ID排序
        return a.t_id.CompareTo(b.t_id);
    }
    private void OnFenGeXianNumber()
    {
        int number = 0;
        PetInfo petInfo = null;
        t_itemBean itemBean = null;
        for (int i = 0; i < petBeans.Count; i++)
        {
            petInfo = PetService.Singleton.GetPetInfo(petBeans[i].t_id);
            if (petInfo != null)
            {
                number++;
                continue;
            }

            int itemNum = BagService.Singleton.GetItemNum(petBeans[i].t_fragment_id);
            itemBean = ConfigBean.GetBean<t_itemBean, int>(petBeans[i].t_fragment_id);
            if (itemBean != null)
            {
                int[] fragmentNum = GTools.splitStringToIntArray(itemBean.t_value);
                if (itemNum >= fragmentNum[0])
                    number++; 
            }
        }
        fengexianNumber = number;
    }
    /// <summary>
    /// 分表
    /// </summary>
    private void OnFenBiao()
    {
        List<t_petBean> beans = ConfigBean.GetBeanList<t_petBean>();
        for (int i = 0; i < beans.Count; ++i)
        {
            if (beans[i].t_ifadd != 0)
            {
                allBeans.Add(beans[i]);
            }
        }
    }
    /// <summary>
    /// 是否已经可以解锁所有宠物
    /// </summary>
    private bool IsGetFullPet()
    {
        return fengexianNumber >= petBeans.Count;
    }
}
