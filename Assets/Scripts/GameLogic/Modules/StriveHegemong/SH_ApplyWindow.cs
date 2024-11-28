using UI_StriveHegemong;
using Message.Pet;
using Data.Beans;
using System.Collections.Generic;
using FairyGUI;

/// <summary>
/// 从服务器获得初始的上阵列表
/// </summary>
public class SH_ApplyWindow : BaseWindow
{
    private UI_SH_ApplyWindow window;
    List<PetInfo> petInfos;
    List<PetInfo> shangzhen = new List<PetInfo>();
    public override void OnOpen()
    {
        base.OnOpen();
        window = getUiWindow<UI_SH_ApplyWindow>();
        window.m_shuxing.selectedIndex = 0;
        AddKeyEvent();
        InitView();
    }
    private void AddKeyEvent()
    {
        window.m_CloseBtn.onClick.Add(OnCloseBtn);
        window.m_shuxing.onChanged.Add(OnFenLei);
        window.m_YiJianShangZhen.onClick.Add(OnYiJianShanZhen);
        window.m_QueRenTiJiao.onClick.Add(OnTiJiao);
        window.m_forword.onClick.Add(OnListForword);
        window.m_next.onClick.Add(OnListNext);
        window.m_PetList.onClickItem.Add(OnQieHuanZhuangTai);
    }
    public override void InitView()
    {
        base.InitView();
        if (PetService.Singleton.PetInfo == null || PetService.Singleton.PetInfo.petsInfo == null)
        {
            Logger.err("SH_ApplyWindow:InitView:未能获得宠物列表信息，无法填充宠物列表！");
            return;
        }
        petInfos = PetService.Singleton.PetInfo.petsInfo;
        RefreshView();
    }
    public override void RefreshView()
    {
        base.RefreshView();
        OnFenLei();
        OnShangZhenList();
    }
    private void OnFenLei()
    {
        int shuxing = window.m_shuxing.selectedIndex;
        switch (shuxing)
        {
            case 0: OnAll(); break;
            case 1: OnAttack(); break;
            case 2: OnDef(); break;
            case 3: OnSkill(); break;
        }
    }
    private void OnListForword()
    {
        window.m_ShangZhenList.ScrollToView(0, true);
    }
    private void OnListNext()
    {
        window.m_ShangZhenList.ScrollToView(window.m_ShangZhenList.numChildren - 1, true);
    }
    private void OnAll()
    {
        window.m_PetList.RemoveChildren(0, -1, true);
        SH_BM_ListItem listItem;
        bool sz = false;
        for (int i = 0; i < petInfos.Count; ++i)
        {
            sz = false;
            listItem = SH_BM_ListItem.CreateInstance();
            for (int j = 0; j < shangzhen.Count; ++j)
            {
                if (petInfos[i].petId == shangzhen[j].petId)
                {
                    sz = true;
                    break;
                }
            }
            listItem.Init(petInfos[i], sz);
            window.m_PetList.AddChild(listItem);
        }
    }
    private void OnAttack()
    {
        window.m_PetList.RemoveChildren(0, -1, true);
        t_petBean petBean;
        SH_BM_ListItem listItem;
        bool sz = false;
        for (int i = 0; i < petInfos.Count; ++i)
        {
            sz = false;
            petBean = ConfigBean.GetBean<t_petBean,int>(petInfos[i].petId);
            if (petBean == null)
            {
                Logger.err("SH_ApplyWindow:OnAttack:未能在宠物表找到对应宠物---" + petInfos[i].petId);
                continue;
            }
            if (petBean.t_type == 1)
            {
                listItem = SH_BM_ListItem.CreateInstance();
                for (int j = 0; j < shangzhen.Count; ++j)
                {
                    if (petInfos[i].petId == shangzhen[j].petId)
                    {
                        sz = true;
                        break;
                    }
                }
                listItem.Init(petInfos[i], sz);
                window.m_PetList.AddChild(listItem);
            }
        }
    }
    private void OnDef()
    {
        window.m_PetList.RemoveChildren(0, -1, true);
        t_petBean petBean;
        SH_BM_ListItem listItem;
        bool sz = false;
        for (int i = 0; i < petInfos.Count; ++i)
        {
            sz = false;
            petBean = ConfigBean.GetBean<t_petBean, int>(petInfos[i].petId);
            if (petBean == null)
            {
                Logger.err("SH_ApplyWindow:OnDef:未能在宠物表找到对应宠物---" + petInfos[i].petId);
                continue;
            }
            if (petBean.t_type == 2)
            {
                listItem = SH_BM_ListItem.CreateInstance();
                for (int j = 0; j < shangzhen.Count; ++j)
                {
                    if (petInfos[i].petId == shangzhen[j].petId)
                    {
                        sz = true;
                        break;
                    }
                }
                listItem.Init(petInfos[i], sz);
                window.m_PetList.AddChild(listItem);
            }
        }
    }
    private void OnSkill()
    {
        window.m_PetList.RemoveChildren(0, -1, true);
        t_petBean petBean;
        SH_BM_ListItem listItem;
        bool sz = false;
        for (int i = 0; i < petInfos.Count; ++i)
        {
            sz = false;
            petBean = ConfigBean.GetBean<t_petBean, int>(petInfos[i].petId);
            if (petBean == null)
            {
                Logger.err("SH_ApplyWindow:OnSkill:未能在宠物表找到对应宠物---" + petInfos[i].petId);
                continue;
            }
            if (petBean.t_type == 3)
            {
                listItem = SH_BM_ListItem.CreateInstance();
                for (int j = 0; j < shangzhen.Count; ++j)
                {
                    if (petInfos[i].petId == shangzhen[j].petId)
                    {
                        sz = true;
                        break;
                    }
                }
                listItem.Init(petInfos[i], sz);
                window.m_PetList.AddChild(listItem);
            }
        }
    }
    private void OnShangZhenList()
    {
        window.m_ShangZhenList.RemoveChildren(0,-1,true);
        SH_BM_ListItem listItem;
        for (int i = 0; i < shangzhen.Count; ++i)
        {
            listItem = SH_BM_ListItem.CreateInstance();
            listItem.Init(shangzhen[i],false);
            window.m_ShangZhenList.AddChild(listItem);
        }
    }
    /// <summary>
    /// 一键上阵
    /// </summary>
    private void OnYiJianShanZhen()
    {
        shangzhen.Clear();
        List<PetInfo> pets = new List<PetInfo>();
        for (int i = 0; i < petInfos.Count; ++i)
        {
            pets.Add(petInfos[i]);
        }
        pets.Sort(SortPaml);
        for (int i = 0; i < pets.Count; ++i)
        {
            if (i == 10)
                break;
            shangzhen.Add(pets[i]);
        }
        RefreshView();
    }
    private void OnQieHuanZhuangTai(EventContext context)
    {
        SH_BM_ListItem listItem = (SH_BM_ListItem)context.data;
        if (listItem.m_xuanzhong.visible)
        {
            listItem.m_xuanzhong.visible = false;
            for (int i = 0; i < shangzhen.Count; ++i)
            {
                if (listItem.petId == shangzhen[i].petId)
                {
                    shangzhen.RemoveAt(i);
                    break;
                }
            }
        }
        else
        {
            if (shangzhen.Count < 10)
            {
                listItem.m_xuanzhong.visible = true;
                for (int i = 0; i < petInfos.Count; ++i)
                {
                    if (petInfos[i].petId == listItem.petId)
                    {
                        shangzhen.Add(petInfos[i]);
                        break;
                    }
                }
            }
            else
            {
                TipWindow.Singleton.ShowTip("已达到最大上阵人数");
            }
        }

        RefreshView();
    }
    /// <summary>
    /// 将上阵列表发送到服务器
    /// </summary>
    private void OnTiJiao()
    {
        List<int> list = new List<int>();
        for (int i = 0; i < shangzhen.Count; ++i)
        {
            list.Add(shangzhen[i].petId);
        }
        StriveHegemongService.Singleton.OnReqSetTeam(list);
        OnCloseBtn();
    }
    private int SortPaml(PetInfo a,PetInfo b)
    {
        int resA = 0;
        int resB = 0;

        if (a.fightInfo.fightPower > b.fightInfo.fightPower)
            resA += 10000;
        else if (a.fightInfo.fightPower < b.fightInfo.fightPower)
            resB += 10000;

        if (a.petId > b.petId)
            resA += 1000;
        else if (a.petId < b.petId)
            resB += 1000;

        if (resA > resB)
            return 1;
        else if (resA == resB)
            return 0;
        else
            return -1;
    }
    protected override void OnCloseBtn()
    {
        window = null;
        base.OnCloseBtn();
    }
}
