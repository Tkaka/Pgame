using UI_Achievement;
using Message.Achievement;
using Data.Beans;
using System.Collections.Generic;
using FairyGUI;

public class AM_MainWindow : BaseWindow
{
    private UI_AM_MainWindow window;
    private t_titleBean titleBean;
    private List<AchievementInfo> adventureList = new List<AchievementInfo>(); //冒险
    private List<AchievementInfo> cultivateList = new List<AchievementInfo>(); //养成
    private List<AchievementInfo> activityList = new List<AchievementInfo>();  //活动
    private List<AchievementInfo> listInfo;      //
    public override void OnOpen()
    {
        base.OnOpen();
        window = getUiWindow<UI_AM_MainWindow>();
        AddKeyEvent();
        InitView();
    }
    
    public override void AddEventListener()
    {
        base.AddEventListener();
        GED.ED.addListener(EventID.OnAchievmentchage, OnAchievenmentSingletonChange);
        GED.ED.addListener(EventID.OnAchievementRank, OnOpenRankingWindow);
    }
    public override void RemoveEventListener()
    {
        base.RemoveEventListener();
        GED.ED.removeListener(EventID.OnAchievmentchage, OnAchievenmentSingletonChange);
        GED.ED.removeListener(EventID.OnAchievementRank, OnOpenRankingWindow);
    }
    public override void InitView()
    {
        base.InitView();
        //头像加载
        if (AchievementService.Singleton.achievementinfo == null)
        {
            Logger.err("AM_MainWindow:InitView:未能从服务器接收到称号相关数据！");
            return;
        }
        UIGloader.SetUrl( window.m_TouXiang,"");
        window.m_AchievementType.selectedIndex = 0;
        FillData();
        OnFenHuaList();
        //渲染列表
        OnAchievementChange();
    }
    private void AddKeyEvent()
    {
        //开启虚拟列表
        window.m_AM_List.SetVirtual();
        window.m_AM_List.itemRenderer = RenderListItem;

        window.m_ColseBtn.onClick.Add(OnCloseBtn);
        window.m_AM_RuleBtn.onClick.Add(OnOpenRuleWindow);
        window.m_AM_RankingBtn.onClick.Add(OnRankingBtn);
        //控制器改变通知
        window.m_AchievementType.onChanged.Add(OnAchievementChange);
    }
    private void FillData()
    {
        titleBean = ConfigBean.GetBean<t_titleBean, int>(AchievementService.Singleton.achievementinfo.title);
        if (titleBean == null)
        {
            Logger.err("AM_MainWindoe:FillData:称号表没有对应id数据,请检查称号表id" + AchievementService.Singleton.achievementinfo.title);
            return;
        }
        //称号
        window.m_AM_Title.text = titleBean.t_name;
        //积分
        window.m_AM_Integral.text = AchievementService.Singleton.achievementinfo.core.ToString() + "/" + AllCore();
        //总先手值
        window.m_AM_Precede.text = AchievementService.Singleton.achievementinfo.precedeValue.ToString();
    }
    //打开规则窗口
    private void OnOpenRuleWindow()
    {
        WinMgr.Singleton.Open<AM_TitleWindow>(WinInfo.Create(true, winName, true), UILayer.Popup);
    }
    //打开排行榜窗口
    private void OnOpenRankingWindow(GameEvent evt)
    {
        //排行榜信息
        List<AchievementRankInfo> rankInfos =  AchievementService.Singleton.achievementRank.info;
        int paiming = AchievementService.Singleton.achievementRank.rank;
    }
    //排行榜
    private void OnRankingBtn()
    {
        AchievementService.Singleton.OnReqAchievementRank(1);
    }
    /// <summary>
    /// 加载按钮状态
    /// </summary>
    private void OnBottonState()
    {
        window.m_AM_maoxian.m_HongDian.visible = false;
        window.m_AM_Yangcheng.m_HongDian.visible = false;
        window.m_AM_huodong.m_HongDian.visible = false;
        window.m_AM_MX_HongDian.visible = false;
        window.m_AM_YC_HongDian.visible = false;
        window.m_AM_HD_HongDian.visible = false;
        for (int i = 0; i < adventureList.Count; ++i)
        {
            if (adventureList[i].state == 1)
            {
                window.m_AM_maoxian.m_HongDian.visible = true;
                window.m_AM_MX_HongDian.visible = true;
                break;
            }
        }
        for (int i = 0; i < cultivateList.Count; ++i)
        {
            if (cultivateList[i].state == 1)
            {
                window.m_AM_Yangcheng.m_HongDian.visible = true;
                window.m_AM_YC_HongDian.visible = true;
                break;
            }
        }
        for (int i = 0; i < activityList.Count; ++i)
        {
            if (activityList[i].state == 1)
            {
                window.m_AM_huodong.m_HongDian.visible = true;
                window.m_AM_HD_HongDian.visible = true;
                break;
            }
        }

    }
    private void RenderListItem(int index, GObject obj)
    {
        AM_List_Item list_Item = obj as AM_List_Item;
        list_Item.Init(listInfo[index]);
    }
    private void OnAchievementChange()
    {
        if (window.m_AchievementType.selectedIndex == 0)
            listInfo = adventureList;
        else if (window.m_AchievementType.selectedIndex == 1)
            listInfo = cultivateList;
        else if (window.m_AchievementType.selectedIndex == 2)
            listInfo = activityList;
        window.m_AM_List.numItems = listInfo.Count;
        window.m_AM_List.RefreshVirtualList();
        OnBottonState();
    }
    /// <summary>
    /// 成就信息改变
    /// 没有数据过来，从新分表
    /// 刷新虚拟列表
    /// </summary>
    /// <param name="evt"></param>
    private void OnAchievenmentSingletonChange(GameEvent evt)
    {
        FillData();
        OnFenHuaList();
        OnAchievementChange();
    }
    /// <summary>
    /// 分表
    /// </summary>
    private void OnFenHuaList()
    {
        List<AchievementInfo> achievementInfos = AchievementService.Singleton.GetAchievementInfos();
        t_achievementBean bean;
        adventureList.Clear();
        cultivateList.Clear();
        activityList.Clear();
        for (int i = 0; i < achievementInfos.Count; ++i)
        {
            bean = ConfigBean.GetBean<t_achievementBean,int>(achievementInfos[i].id);
            if (bean == null)
            {
                Logger.err("AM_MainWindow:OnFenHuaList:成就表没有对应数据，请检查id是否正确---" + achievementInfos[i].id);
                continue;
            }
            if (bean.t_type == 1)
            {
                adventureList.Add(achievementInfos[i]);
            }
            else if (bean.t_type == 2)
            {
                cultivateList.Add(achievementInfos[i]);
            }
            else if (bean.t_type == 3)
            {
                activityList.Add(achievementInfos[i]);
            }
        }
        adventureList.Sort(SortPaml);
        cultivateList.Sort(SortPaml);
        activityList.Sort(SortPaml);
    }
    private int SortPaml(AchievementInfo a,AchievementInfo b)
    {
        int resA = 0;
        int resB = 0;

        if (a.state == 1)
            resA += 10000;
        if (b.state == 1)
            resB += 10000;

        if (a.state == 2)
            resA += 5000;
        if (b.state == 2)
            resB += 5000;

        if (a.id < b.id)
            resA += 1000;
        else if (a.id > b.id)
            resB += 1000;

        if (resA > resB)
            return -1;
        else if (resA == resB)
            return 0;
        else
            return 1;
    }
    private string AllCore()
    {
        int core = 0;
        List<AchievementInfo> infos = AchievementService.Singleton.GetAchievementInfos();
        t_achievementBean bean;
        for (int i = 0; i < infos.Count; ++i)
        {
            bean = ConfigBean.GetBean<t_achievementBean,int>(infos[i].id);
            if (bean == null)
            {
                Logger.err("AM_MainWindow:OnFenHuaList:成就表没有对应数据，请检查id是否正确---" + infos[i].id);
                continue;
            }
            if (bean != null)
            {
                if (string.IsNullOrEmpty(bean.t_reward))
                {
                    Logger.err("AM_MainWindow:AllCore:成就表此成就的奖励分数为空，计算出来的总成就积分将不准确！---" + bean.t_id);
                    continue;
                }
                int[] cores = GTools.splitStringToIntArray(bean.t_reward);
                for (int j = 0; j < cores.Length; ++j)
                { core += cores[j]; }
            }
        }
        return core.ToString();
    }
    protected override void OnCloseBtn()
    {
        RemoveEventListener();
        titleBean = null;
        adventureList = null;
        cultivateList = null;
        activityList = null;
        window = null;
        base.OnCloseBtn();
    }
}
