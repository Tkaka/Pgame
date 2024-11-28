using Data.Beans;
using UnityEngine;
using Message.Pet;
using Message.Rank;
using FairyGUI;
using System.Collections.Generic;
using UI_PetParticulars;

public enum XiangQingType
{
    ChongWuGuanLi,//从宠物管理打开
    ShangZhenLieBiao,//从上阵列表打开
    PaiHangBang,//从排行榜打开
}
public class ChongWuXiangQingWindow : BaseWindow
{
    private UI_ChongWuXiangQingWindow window;
    private UIResPack resPack;
    private ActorUI actor;
    private XinXiMianBan xinxi;
    private JiBanMianBan jiban;
    private JiNengMianBan jineng;
    private ShuXingMianBan shuxing;
    private ZhanHunMianBan zhanhun;
    private JianJieMianBan jianJie;
    private DingWeiMianBan dingwei;
    private int index;
    private XiangQingType xiangQingType;

    private bool isAnimEnd = true;

    private int petId;
    private List<int> petList; 
    public override void OnOpen()
    {
        base.OnOpen();
        window = getUiWindow<UI_ChongWuXiangQingWindow>();
        AddKeyEvent();
        TopRoleInfo.Hide(this);
        InitView();
    }
    private void AddKeyEvent()
    {
        window.m_colseBtn.onClick.Add(CloseBtn);
        window.m_xianshiqiehuan.onClick.Add(OnShuBiaoFangKai);
        window.m_next.onClick.Add(OnNextPet);//下一个宠物
        window.m_last.onClick.Add(OnLastPet);
        window.m_JinHuaLianBtn.onClick.Add(OnOpenJinHuanLian);
        GED.ED.addListener(EventID.OnOpenXiangQingMianBan, OnChaKanXiangQing);
    }

    public override void InitView()
    {
        base.InitView();


        if (Info.param == null)
        {
            Logger.err("未传入宠物ID");
            return;
        }
        TwoParam<int, List<int>> twoParam = Info.param as TwoParam<int, List<int>>;
        if (twoParam != null)
        {
            petId = twoParam.value1;
            petList = twoParam.value2;
            OnJiSuanXiaBia();//只计算index
        }
        else
        {
            TwoParam<int, XiangQingType> two =Info.param as TwoParam<int,XiangQingType>;
            if (two != null)
            {
                petId = two.value1;
                xiangQingType = two.value2;
                if (xiangQingType != XiangQingType.PaiHangBang)
                    { petList = OnGetList(); }
            }
            else
            {
                Logger.err("传参有误！");
                CloseBtn();
                return;
            }
        }
        //根据宠物ID获得宠物信息
        t_petBean petBean = ConfigBean.GetBean<t_petBean, int>(petId);
        if (petBean == null)
        {
            Logger.err("宠物表没有此id的数据");
            return;
        }
        resPack = new UIResPack(this);

        window.m_XiangQingList.RemoveChildren(0, -1, true);
        if (xinxi == null)
            xinxi = XinXiMianBan.CreateInstance();
        window.m_XiangQingList.AddChild(xinxi);
        if (dingwei == null)
            dingwei = DingWeiMianBan.CreateInstance();
        window.m_XiangQingList.AddChild(dingwei);
        if (jiban == null)
            jiban = JiBanMianBan.CreateInstance();
        window.m_XiangQingList.AddChild(jiban);
        if (jineng == null)
            jineng = JiNengMianBan.CreateInstance();
        window.m_XiangQingList.AddChild(jineng);
        if (zhanhun == null)
            zhanhun = ZhanHunMianBan.CreateInstance();
        window.m_XiangQingList.AddChild(zhanhun);
        if (jianJie == null)
            jianJie = JianJieMianBan.CreateInstance();
        window.m_XiangQingList.AddChild(jianJie);
        if (shuxing == null)
            shuxing = new ShuXingMianBan();
        if (xiangQingType == XiangQingType.PaiHangBang)
        {
            OnBeFormRank();
        }
        else
        {
            RefreshView();
        }
    }
    private void OnOpenJinHuanLian()
    {
        WinMgr.Singleton.Open<JinHuaLianWindow>(WinInfo.Create(false,null,false,petId),UILayer.Popup);
    }
    public override void RefreshView()
    {
        base.RefreshView();
        t_petBean bean = ConfigBean.GetBean<t_petBean, int>(petId);
        if (bean == null)
        {
            Logger.err("宠物表没有对信息" + petId);
            return;
        }
        window.m_ShuXingMianBan.visible = false;

        // 设置类型
        string type = "";
        if (bean.t_type == 1)
        {
            type = "gong02";
        }
        else if (bean.t_type == 2)
        {
            type = "fang02";
        }
        else if (bean.t_type == 3)
        {
            type = "ji02";
        }
        type = "ui://" + WinEnum.UI_Common + "/" + type;
        UIGloader.SetUrl(window.m_typeLoader, type);
        SheZhiMingZi();
        OnShowModel();
        FillData();
        SetStart();
    }
    private void FillData()
    {
        xinxi.Init(petId);
        jiban.Init(petId);
        jineng.Init(petId);
        zhanhun.Init(petId);
        jianJie.Init(petId);
        dingwei.Init(petId);
        shuxing.Init(window.m_ShuXingMianBan,petId);
        window.m_ShuXingMianBan.visible = false;
    }
    private void OnNextPet()//下一个宠物
    {
        petId = 0;
        while (petId == 0)
        {
            index++;
            if (index == petList.Count)
                index = 0; 
            petId = petList[index];
        }
        RefreshView();
    }
    private void OnLastPet()//上一个宠物
    {
        petId = 0;
        while (petId == 0)
        {
            index--;
            if (index < 0)
                index = petList.Count - 1;
            petId = petList[index];
        }
        RefreshView();
    }
    private void SheZhiMingZi()
    {
        t_petBean bean = ConfigBean.GetBean<t_petBean, int>(petId);
        if (bean != null)
        {
            window.m_Name.text = UIUtils.GetPetName(bean);
        }
        PetInfo petInfo = PetService.Singleton.GetPetInfo(petId);
        if (petInfo != null)
        {
            window.m_Name.color = UIUtils.GetColorByQuality(petInfo.basInfo.color);
        }
        else
        {
            window.m_Name.color = UIUtils.GetColorByQuality(1);
        }
        OnZhiZhi();
    }

    private void OnShowModel()
    {
        resPack.CacheWrapper(window.m_Model);
        var warrper = new GoWrapper();
        window.m_Model.SetNativeObject(warrper);
        PetInfo petInfo = PetService.Singleton.GetPetInfo(petId);
        int star = petInfo == null ? -1 : petInfo.basInfo.star;
        actor = resPack.NewActorUI(petId, star, ActorType.Pet, warrper);
        actor.SetTransform(new Vector3(-137f, -76f, 500), 380, new Vector3(0, 165.48f, 0));
        actor.MouseRotate(window.m_xianshiqiehuan);
        actor.PlayRandomAni(OnAnimEnd);
        isAnimEnd = false;
    }
    //星级加载
    private void SetStart()
    {
        PetInfo petInfo = PetService.Singleton.GetPetInfo(petId);
        window.m_starList.RemoveChildren(0, -1, true);
        UI_xingjiListItem xingjiListItem;
        if (petInfo != null)
        {
            for (int i = 0; i < petInfo.basInfo.star; ++i)
            {
                xingjiListItem = UI_xingjiListItem.CreateInstance();
                window.m_starList.AddChild(xingjiListItem);
            }
        }
        else
        {
            t_petBean petBean = ConfigBean.GetBean<t_petBean, int>(petId);
            for (int i = 0; i < petBean.t_hecheng_star; ++i)
            {
                xingjiListItem = UI_xingjiListItem.CreateInstance();
                window.m_starList.AddChild(xingjiListItem);
            }
        }
    }
    private void OnShuBiaoFangKai(EventContext evt)
    {
        if (isAnimEnd == true)
        {
            actor.PlayRandomAni(OnAnimEnd);
            isAnimEnd = false;
        }
    }

    private void OnAnimEnd()
    {
        isAnimEnd = true;
    }

    private void OnChaKanXiangQing(GameEvent evt)
    {
        bool xianshi = (bool)evt.Data;
        if (xianshi)
        {
            window.m_zhaunhuan.Play();
            window.m_ShuXingMianBan.visible = true;
        }
        else
        {
            window.m_zhaunhuan.PlayReverse();
            window.m_ShuXingMianBan.visible = false;
        }
    }
    private void OnBeFormRank()
    {
        window.m_nextBtn.visible = false;
        window.m_lastBtn.visible = false;
        window.m_last.visible = false;
        window.m_next.visible = false;

        xinxi.Init(petId,1);
        jiban.Init(petId);
        jineng.Init(petId,1);
        zhanhun.Init(petId,1);
        jianJie.Init(petId);
        dingwei.Init(petId);
        shuxing.Init(window.m_ShuXingMianBan, petId,1);
        window.m_ShuXingMianBan.visible = false;
        
        OnShowModel();
        OnSetRankPetNameAndStar();
    }
    private void OnZhiZhi()
    {
        t_petBean bean = ConfigBean.GetBean<t_petBean,int>(petId);
        //资质
        window.m_ZiZhi.text = UIUtils.GetZiZhiStr(bean.t_zizhi);
        //t_globalBean globalBean = ConfigBean.GetBean<t_globalBean, int>(3);
        //if (globalBean != null)
        //{
        //    int[] zizhi = GTools.splitStringToIntArray(globalBean.t_string_param);
        //    int lenth = bean.t_zizhi - 10;
        //    t_languageBean languageBean;
        //    if (zizhi.Length < lenth)
        //    {
        //        languageBean = ConfigBean.GetBean<t_languageBean, int>(zizhi[zizhi.Length - 1]);
        //    }
        //    else
        //    {
        //        languageBean = ConfigBean.GetBean<t_languageBean, int>(zizhi[lenth - 1]);
        //    }
        //    if (languageBean != null)
        //    {
        //        window.m_ZiZhi.text = languageBean.t_content;
        //    }
        //}
    }
    private void OnSetRankPetNameAndStar()
    {
        Petdata petdata = TopService.Singleton.GetPetdata();
        if (petdata == null)
            return;

        //设置名字品阶和颜色
        t_petBean petBean = ConfigBean.GetBean<t_petBean, int>(petId);
        if(petBean != null)
            window.m_Name.text = UIUtils.GetPetName(petBean, petdata.baseinfo.star);
        window.m_Name.color = UIUtils.GetColorByQuality(petdata.baseinfo.color);
        PetInfo petInfo = PetService.Singleton.GetPetInfo(petId);
        //星级
        window.m_starList.RemoveChildren(0, -1, true);
        UI_xingjiListItem xingjiListItem;
        for (int i = 0; i < petdata.baseinfo.star; ++i)
        {
            xingjiListItem = UI_xingjiListItem.CreateInstance();
            window.m_starList.AddChild(xingjiListItem);
        }
        OnZhiZhi();
    }
    private List<int> OnGetList()
    {
        List<int> pets = new List<int>();
        if (xiangQingType == XiangQingType.ChongWuGuanLi)
        {
            List<t_petBean> petBeans = ConfigBean.GetBeanList<t_petBean>();
            List<t_petBean> petBean = new List<t_petBean>();
            for (int i = 0; i < petBeans.Count; ++i)
            {
                if(petBeans[i].t_ifadd != 0)
                    petBean.Add(petBeans[i]);
            }
            petBean.Sort(SortImpl);
            for (int i = 0; i < petBean.Count; ++i)
            {
                if (petBean[i].t_id == petId)
                {
                    index = i;
                }
                pets.Add(petBean[i].t_id);
            }
        }
        else if(xiangQingType == XiangQingType.ShangZhenLieBiao)
        {
            pets = PetService.Singleton.GetTeamList(ZhenRongType.Normal);
            for(int i = 0; i < pets.Count; ++i)
            {
                if (petId == pets[i])
                    index = i;
            }
        }
        return pets;
    }
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
            if (itemBean != null)
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
            return petGetTypeA.CompareTo(petGetTypeB);
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
            if (fragmentA != fragmentB)
                return -fragmentA.CompareTo(fragmentB);
        }
        // 按ID排序
        return a.t_id.CompareTo(b.t_id);
    }
    private void OnJiSuanXiaBia()
    {
        for (int i = 0; i < petList.Count; ++i)
        {
            if (petList[i] == petId)
                index = i;
        }
    }
    private void CloseBtn()
    {
        GED.ED.removeListener(EventID.OnOpenXiangQingMianBan, OnChaKanXiangQing);
        if (resPack != null)
        {
            resPack.ReleaseAllRes();
            resPack = null;
        }
        if (xinxi != null)
        {
            xinxi.Close();
            xinxi = null;
        }
        if (shuxing != null)
        {
            shuxing.Close();
            shuxing = null;
        }
        if (jiban != null)
        {
            jiban.Dispose();
            jiban = null;
        }
        if (jineng != null)
        {
            jineng.Dispose();
            jineng = null;
        }
        if (shuxing != null)
        {
            shuxing.Close();
            shuxing = null;
        }
        window = null;
        actor = null;
        Close();
    }

}
