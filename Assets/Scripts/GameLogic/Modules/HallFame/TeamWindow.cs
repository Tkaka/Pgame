using UI_HallFame;
using Data.Beans;
using Message.Pet;
using FairyGUI;
using UnityEngine;
using System.Collections.Generic;
using Message.Team;
using Message.Bag;

public class TeamWindow : BaseWindow
{
    private UI_TeamWindow window;
    private DoActionInterval doAction;
    private int currpetid;//当前选中的宠物id
    private int currTeamid;//当前选中的战队的id;
    private HaoGanDuMianBan HaoGanDu;
    private t_petBean petBean;
    private ActorUI actor;
    private List<t_hof_teamBean> teams;

    public override void OnOpen()
    {
        window = getUiWindow<UI_TeamWindow>();
        HaoGanDu = new HaoGanDuMianBan(window.m_HF_haogandu);
        teams = ConfigBean.GetBeanList<t_hof_teamBean>();
        window.m_jiantou.Play();
        AddKeyEvent();
        InitView();
    }
    public override void AddEventListener()
    {
        base.AddEventListener();
        GED.ED.addListener(EventID.OnResHofSingleInfo,OnSleepHaoGanDuMianBan);
        GED.ED.addListener(EventID.OnItemList, OnOpenColorUpWindow);
    }
    public override void RemoveEventListener()
    {
        base.RemoveEventListener();
        GED.ED.removeListener(EventID.OnResHofSingleInfo, OnSleepHaoGanDuMianBan);
        GED.ED.removeListener(EventID.OnItemList, OnOpenColorUpWindow);
    }
    private void  AddKeyEvent()
    {
        AddEventListener();
        window.m_Close.onClick.Add(OnCloseBtn);
        window.m_PetList.onClickItem.Add(OnPetSelect);
        window.m_last.onClick.Add(OnLast);
        window.m_next.onClick.Add(OnNext);
    }
    
    public override void InitView()
    {
        if (Info.param == null)
        {
            Logger.err("TeamWindow:InitView:未获得宠物id,请为打开窗口函数传参");
            return;
        }
        currpetid = (int)Info.param;
        petBean = ConfigBean.GetBean<t_petBean,int>(currpetid);
        if (petBean == null)
        {
            Logger.err("TeamWindow:InitView:未能在宠物表找到对应数据，请检查宠物id是否正确---" + currpetid);
            return;
        }
        if (petBean.t_team == 0)
        {
            Logger.err("TeamWindow:InitView:未能在宠物表冲屋所属战队的id，请检查该宠物t_team字段---" + currpetid);
            return;
        }
        doAction = new DoActionInterval();
        doAction.doAction(3, OnQiPaoYuYan, null, true);
        currTeamid = petBean.t_team;
        FillPetList();
        RefreshView();
    }
    public void OnSleepHaoGanDuMianBan(GameEvent evt)
    {
        OnYiHuoDe();
    }
    public override void RefreshView()
    {
        PetInfo info = PetService.Singleton.GetPetInfo(currpetid);
        petBean = ConfigBean.GetBean<t_petBean,int>(currpetid);
        OnQiPaoYuYan(null);
        //从新加载模型
        LoadMode();
        //播放动效
        window.m_MoXingChuChang.Play();
        if (info == null)
        {
            OnWeiHuoDe();
        }
        else
        {
            OnYiHuoDe();
        }
    }
    /// <summary>
    /// 获得宠物列表
    /// 1、获得战队id
    /// 2、填充列表
    /// 3、得到下标
    /// 4、加载对应模型
    /// </summary>
    private void FillPetList()
    {
        TeamPetSelectItem selectItem;
        int team = petBean.t_team;
        int index = 0;
        if (team == currTeamid)
        {
            t_hof_teamBean fameBean = ConfigBean.GetBean<t_hof_teamBean, int>(team);
            if (fameBean != null)
            {
                if (string.IsNullOrEmpty(fameBean.t_pets))
                {
                    Logger.err("战队表所属宠物列表有误，请检查战队id---" + fameBean.t_id);
                    return;
                }
                else
                {
                    int[] teamsid = GTools.splitStringToIntArray(fameBean.t_pets);
                    window.m_PetList.RemoveChildren(0, -1, true);
                    for (int i = 0; i < teamsid.Length; ++i)
                    {
                        selectItem = TeamPetSelectItem.CreateInstance();
                        selectItem.Init(teamsid[i]);
                        window.m_PetList.AddChild(selectItem);
                        if (teamsid[i] == petBean.t_id)
                            index = i;
                    }
                    if (window.m_TeamPet.selectedIndex == index)
                    {

                    }
                    window.m_PetList.selectedIndex = index;
                }
            }
        }
        else
        {
            t_hof_teamBean fameBean = ConfigBean.GetBean<t_hof_teamBean, int>(currTeamid);
            if (string.IsNullOrEmpty(fameBean.t_pets))
            {
                Logger.err("战队表所属宠物列表有误，请检查战队id---" + fameBean.t_id);
                return;
            }
            else
            {
                int[] teamsid = GTools.splitStringToIntArray(fameBean.t_pets);
                bool biaoji = true;
                window.m_PetList.RemoveChildren(0, -1, true);
                for (int i = 0; i < teamsid.Length; ++i)
                {
                    selectItem = TeamPetSelectItem.CreateInstance();
                    selectItem.Init(teamsid[i]);
                    window.m_PetList.AddChild(selectItem);
                    PetInfo info = PetService.Singleton.GetPetInfo(teamsid[i]);
                    if (info != null && biaoji)
                    {
                        biaoji = false;
                        index = i;
                    }
                }
                currpetid = teamsid[index];
                window.m_PetList.selectedIndex = index;
            }
        }
    }
    private void OnOpenColorUpWindow(GameEvent evt)
    {
        TwoParam<HofItem, List<ItemInfo>> twoParam = (TwoParam<HofItem, List<ItemInfo>>)evt.Data;
        WinInfo info = new WinInfo();
        info.param = twoParam;
        WinMgr.Singleton.Open<ColorUpWindow>(info, UILayer.Popup);
    }
    private void OnLast()
    {
        currTeamid = GetTeamIndex(true);
        FillPetList();
        RefreshView();
    }
    private void OnNext()
    {
        currTeamid = GetTeamIndex(false);
        FillPetList();
        RefreshView();
    }
    private void OnPetSelect(EventContext context)
    {
        TeamPetSelectItem listItem = context.data as TeamPetSelectItem;
        if (listItem != null)
        {
            int petid = listItem.GetPetid();
            if (petid != 0)
            {
                currpetid = petid;
                //从新加载模型
                LoadMode();
                //播放动效
                window.m_MoXingChuChang.Play();
                RefreshView();
            }
            else
            {
                TipWindow.Singleton.ShowTip("该格斗家即将到来，敬请期待（当前非语言包文字）");
            }
        }
    }
    //气泡语言
    private void OnQiPaoYuYan(object obj)
    {
        if (!(string.IsNullOrEmpty(petBean.t_qipao)))
        {
            int[] qipao = GTools.splitStringToIntArray(petBean.t_qipao);
            int index = Random.Range(0,qipao.Length - 1);
            t_languageBean languageBean = ConfigBean.GetBean<t_languageBean,int>(qipao[index]);
            if (languageBean != null)
                window.m_qipanyuyan.text = languageBean.t_content;
        }
    }
    
    //未获得
    private void OnWeiHuoDe()
    {
        HaoGanDu.Init(currTeamid, currpetid);
    }
    //已获得
    private void OnYiHuoDe()
    {
        HaoGanDu.Init(currTeamid, currpetid);
    }
    //为真返回上一个战队id，为假返回下一个战队id
    private int GetTeamIndex(bool team)
    {
        int teamid = 0;
        for (int i = 0; i < teams.Count; ++i)
        {
            if (currTeamid == teams[i].t_id)
            {
                if (i == 0)
                {
                    if (team)
                    { teamid = teams[teams.Count - 1].t_id; }
                    else
                    { teamid = teams[i + 1].t_id; }
                }
                else if (i > 0 && i < teams.Count - 1)
                {
                    if (team)
                    { teamid = teams[i - 1].t_id; }
                    else
                    { teamid = teams[i + 1].t_id; }
                }
                else if (i == teams.Count - 1)
                {
                    if (team)
                    { teamid = teams[teams.Count - 2].t_id; }
                    else
                    { teamid = teams[0].t_id; }
                }
                break;
            } 
        }
        return teamid;
    }
    
    private void LoadMode()
    {
        window.m_Name.text = UIUtils.GetPetName(petBean);

        GameObject model = LoadGo(UIUtils.GetPetStartModel(petBean.t_id));

        model.transform.localPosition = new Vector3(20, 0, 300);
        model.transform.localScale = new Vector3(150, 150, 150);
        model.transform.localEulerAngles = new Vector3(0, 180, 0);

        GoWrapper wrapper = new GoWrapper(model);
        UIUtils.SetWrapperMask(wrapper);
        window.m_MoXing.SetNativeObject(wrapper);
        model.setLayer("UIActor");
    }
    protected override void OnCloseBtn()
    {
        RemoveEventListener();
        if (doAction != null)
        {
            doAction.kill();
            doAction = null;
        }
        if (actor != null)
        { actor = null; }
        petBean = null;
        teams = null;
        if (HaoGanDu != null)
        {
            //HaoGanDu.Close();
            HaoGanDu = null;
        }
        base.OnCloseBtn();
    }

}
