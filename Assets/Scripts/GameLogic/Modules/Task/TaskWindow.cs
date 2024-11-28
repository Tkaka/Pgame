using System.Collections.Generic;
using UI_TaskSystem;
using Data.Beans;
using Message.Task;
using FairyGUI;
using Message.Role;
using System;

public class TaskWindow : BaseWindow
{
    List<TaskInfo> taskInfos;
    //日常任务表
    private List<int> everydayTaskList;
    //主线任务表]
    private List<int> zhuxianTaskList;
    //当前选中的页签,1代表日常任务,2代表主线任务
    private int presenttaskList;
    private int newtaskList;
    private TaskType taskType = new TaskType();

    UITable table;

    UI_TaskWindow window;
    public override void OnOpen()
    {
        base.OnOpen();
        InitView();
        PlayOpenEffect();
    }

    public override void PlayOpenEffect()
    {
        base.PlayOpenEffect();

        (window.m_commonTop as UI_Common.UI_commonTop).m_anim.Play();
        window.m_taskToggleGroup.m_anim.Play();
    }

    public override void AddEventListener()
    {
        base.AddEventListener();
        GED.ED.addListener(EventID.OnRefreshTaskList, OnRefreshTaskList);
        GED.ED.addListener(EventID.OnTaskInfoChange, OnTaskInfoChange);
        GED.ED.addListener(EventID.GuildEnter, OnColseWindow);
        GED.ED.addListener(EventID.OnTaskType,OnTaskType);
        GED.ED.addListener(EventID.OnResTeamFightMonsterInfo, OnResTeamFightMonsterInfo);
        GED.ED.addListener(EventID.OnResTrainInfo,OnZongJiShiLian);
    }
    public override void RemoveEventListener()
    {
        base.RemoveEventListener();
        GED.ED.removeListener(EventID.OnRefreshTaskList, OnRefreshTaskList);
        GED.ED.removeListener(EventID.OnTaskInfoChange, OnTaskInfoChange);
        GED.ED.removeListener(EventID.GuildEnter, OnColseWindow);
        GED.ED.removeListener(EventID.OnTaskType, OnTaskType);
        GED.ED.removeListener(EventID.OnResTeamFightMonsterInfo, OnResTeamFightMonsterInfo);
        GED.ED.removeListener(EventID.OnResTrainInfo, OnZongJiShiLian);
    }
    private void AddKeyEvent()
    {
        //FuncService.Singleton.SetFuncLock(window.m_taskToggleGroup.m_RiChangRenWuBtn, 1501);//日常任务按钮条件开启
        (window.m_commonTop as UI_Common.UI_commonTop).m_closeBtn.onClick.Add(CloseBtn);
        window.m_YiJianLingQu.onClick.Add(OnYiJianLingQu);
        window.m_taskToggleGroup.m_RiChangRenWuBtn.onClick.Add(OnRiChang);

        table = new UITable();
        table.Init(window.m_taskToggleGroup.m_ctrl, OnRenWuListChange);
        table.AddFuncLock(1, 1501, window.m_taskToggleGroup.m_RiChangRenWuBtn) ;
        table.AddBtnAnim(window.m_taskToggleGroup.m_ZhuXianRenWuBtn, window.m_taskToggleGroup.m_RiChangRenWuBtn);
    }
    public override void InitView()
    {
        base.InitView();
        window = getUiWindow<UI_TaskWindow>();

        (window.m_commonTop as UI_Common.UI_commonTop).m_title.text = "任务";
        
        everydayTaskList = new List<int>();
        zhuxianTaskList = new List<int>();
        AddKeyEvent();
        taskInfos = TaskService.Singleton.GetTaskInfos();
        if (taskInfos == null && taskInfos.Count == 0)
        {
            Logger.err("TaskWindow:AllocationBean:未从服务器获得任务列表");
            return;
        }
        if (FuncService.Singleton.IsFuncOpen(1501))
        {
            presenttaskList = 1;
            window.m_taskToggleGroup.m_ctrl.selectedIndex = 1;
        }
        else
        {
            presenttaskList = 2;
            window.m_taskToggleGroup.m_ctrl.selectedIndex = 0;
        }
        _RegisterRedDot("Task/everyday",window.m_taskToggleGroup.m_RiChangRenWuBtn.m_hongdian);
        _RegisterRedDot("Task/zhuxian", window.m_taskToggleGroup.m_ZhuXianRenWuBtn.m_hongdian);
        //整理服务器发来的任务列表
        AllocationBean();
        InitData();
    }
    /// <summary>
    /// 页签切换
    /// </summary>
    public override void RefreshView()
    {
        base.RefreshView();
        if(newtaskList != presenttaskList)
        {
            presenttaskList = newtaskList;
            InitData();
        }
    }
    private void OnRenWuListChange(int index)
    {
        switch (window.m_taskToggleGroup.m_ctrl.selectedIndex)
        {
            case 0:newtaskList = 2;break;
            case 1:
                {
                    if (FuncService.Singleton.TipFuncNotOpen(1501))
                    {
                        FuncService.Singleton.TipFuncNotOpen(1501);
                        OnRiChang();
                        newtaskList = 1;
                     }
            };break;
        }
        RefreshView();
    }
    private void OnRiChang()
    {
        if (!(FuncService.Singleton.IsFuncOpen(1501)))
        {
            window.m_taskToggleGroup.m_ctrl.selectedIndex = 0;
        }
    }
    //判断一键领取是否显示
    private void OnYiLingQu()
    {
        window.m_YiJianLingQu.visible = false;
        taskInfos = TaskService.Singleton.GetTaskInfos();
        window.m_YiJianLingQu.visible = false;
        if(window.m_taskToggleGroup.m_ctrl.selectedIndex == 0)
        { return; }
        RoleInfo roleInfo = RoleService.Singleton.GetRoleInfo();
        if (roleInfo == null)
        {
            Logger.err("未能获取到玩家信息,一键领取默认不显示");
            window.m_YiJianLingQu.visible = false;
            return;
        }
        //一键领取按钮显示等级
        t_globalBean bean = ConfigBean.GetBean<t_globalBean, int>(15012);
        if (bean == null)
        {
            Logger.err("TaskWindow:InitView:未能从全局表读取到一键领取按钮的显示等级数据，一键领取默认不显示！");
            window.m_YiJianLingQu.visible = false;
            return;
        }
        if (roleInfo.level >= bean.t_int_param)
        {
            for (int i = 0; i < taskInfos.Count; ++i)
            {
                if (taskInfos[i].state == 1)
                {
                    t_taskBean taskBean = ConfigBean.GetBean<t_taskBean,int>(taskInfos[i].id);
                    if (taskBean != null)
                    {
                        if (taskBean.t_type == 0 || taskBean.t_type == 2)
                        {
                            window.m_YiJianLingQu.visible = true;
                            break;
                        }
                    }
                }
            }
        }
    }
    private void InitData()
    {
        if (presenttaskList == 1)
            FillList(everydayTaskList);
        else if (presenttaskList == 2)
            FillList(zhuxianTaskList);
    }

    private void CloseBtn()
    {
        window.m_RenWuList.RemoveChildren(0,-1,true);
        everydayTaskList.Clear();
        everydayTaskList = null;
        zhuxianTaskList.Clear();
        zhuxianTaskList = null;
        if (window != null)
            window = null;
        RemoveEventListener();
        Close();
    }
    private void OnQieHuanRiChangRenWu()
    {
        newtaskList = 1;
        RefreshView();
    }
    private void OnQieHuanZhuXianRenWu()
    {
        newtaskList = 2;
        RefreshView();
    }
    /// <summary>
    /// 当接收到服务器发来的增加或者删除任务的消息时调用
    /// </summary>
    private void OnRefreshTaskList(GameEvent evt)
    {
        TwoParam<bool, List<TaskInfo>> twoParam = (TwoParam<bool, List<TaskInfo>>)evt.Data;
        List<TaskInfo> taskInfo = twoParam.value2;
        for (int i = 0; i < taskInfo.Count; ++i)
        {
            t_taskBean taskBean = ConfigBean.GetBean<t_taskBean, int>(taskInfo[i].id);
            if (taskBean == null)
            {
                Logger.err("TaskWindow:OnRefreshTaskList:未能在任务表中找到服务器发来的任务-----" + taskInfo[i].id);
                return;
            }
            //日常任务
            if (taskBean.t_type == 0 || taskBean.t_type == 2)
            {
                if (twoParam.value1)
                {
                    if (taskInfo[i].state == 3)
                    {
                        for (int j = 0; j < everydayTaskList.Count; ++j)
                        {
                            if (everydayTaskList[j] == taskInfo[i].id)
                            {
                                everydayTaskList.Remove(taskInfo[i].id);
                            }
                        }
                    }
                    else
                        everydayTaskList.Add(taskInfo[i].id);
                }
                else
                {
                    for (int j = 0; j < everydayTaskList.Count; ++j)
                    {
                        if (everydayTaskList[j] == taskInfo[i].id)
                        {
                            everydayTaskList.Remove(taskInfo[i].id);
                        }
                    }
                }
            }
            else if (taskBean.t_type == 1)//主线任务
            {
                if (twoParam.value1)
                {
                    if (taskInfo[i].state == 3)
                    {
                        for (int j = 0; j < zhuxianTaskList.Count; ++j)
                        {
                            if (zhuxianTaskList[j] == taskInfo[i].id)
                            {
                                zhuxianTaskList.Remove(taskInfo[i].id);
                            }
                        }
                    }
                    else
                    zhuxianTaskList.Add(taskInfo[i].id);
                }
                else
                {
                    for (int j = 0; j < zhuxianTaskList.Count; ++j)
                    {
                        if (zhuxianTaskList[j] == taskInfo[i].id)
                        {
                            zhuxianTaskList.Remove(taskInfo[i].id);
                        }
                    }
                }
            }
        }
        everydayTaskList.Sort(SortPaml);
        zhuxianTaskList.Sort(SortPaml);
        InitData();
    }
    private void OnTaskInfoChange(GameEvent evt)
    {
        everydayTaskList.Sort(SortPaml);
        zhuxianTaskList.Sort(SortPaml);
        InitData();
    }
 /// <summary>
 /// 加载当前任务列表
 /// </summary>
 /// <param name="list"></param>
    private void FillList( List<int> list)
    {
        OnYiLingQu();
        RenWuListItem taskItem = null;
        window.m_RenWuList.RemoveChildren(0,-1,true);
        for (int i = 0; i < list.Count; ++i)
        {
            taskItem = RenWuListItem.CreateInstance();
            taskItem.Init(list[i],i);
            window.m_RenWuList.AddChild(taskItem);
        }
        window.m_RenWuList.ScrollToView(0);
    }
    /// <summary>
    /// 填充任务表                                    
    /// </summary>
    private void AllocationBean()
    {
        t_taskBean taskBean = null;
        for (int i = 0; i < taskInfos.Count; ++i)
        {
            taskBean = ConfigBean.GetBean<t_taskBean, int>(taskInfos[i].id);
            if (taskBean == null)
            {
                Logger.err("TaskWindow:AllocationBean:未能从本地任务列表中找到服务器服务器发来的任务id------" + taskInfos[i].id);
                continue;
            }
            if (taskBean.t_type == 0 || taskBean.t_type == 2)
                everydayTaskList.Add(taskInfos[i].id);
            else if (taskBean.t_type == 1)
                zhuxianTaskList.Add(taskInfos[i].id);
        }
        everydayTaskList.Sort(SortPaml);
        zhuxianTaskList.Sort(SortPaml);
    }
    private int SortPaml(int a, int b)
    {
        int resA = 0;
        int resB = 0;

        //已在AllocationBean筛选过，不用再判空
        t_taskBean taskBeanA = ConfigBean.GetBean<t_taskBean, int>(a);
        t_taskBean taskBeanB = ConfigBean.GetBean<t_taskBean, int>(b);

        //已完成的排前面
        TaskInfo infoA = TaskService.Singleton.GetTaskInfo(a);
        TaskInfo infoB = TaskService.Singleton.GetTaskInfo(b);

        if (infoA != null)
        {
            if (infoA.state == 1)
                resA += 20000;
        }
        else
        {
            Logger.err("TaskWindow:OnRefreshTaskList:未能在任务表中找到服务器发来的任务-----" + a);
        }
        if (infoB != null)
        {
            if (infoB.state == 1)
                resB += 20000;
        }
        else
        {
            Logger.err("TaskWindow:OnRefreshTaskList:未能在任务表中找到服务器发来的任务-----" + b);
        }

        //类型筛选
        if (taskBeanA.t_type == 3)
            resA += 10000;
        if (taskBeanB.t_type == 3)
            resB += 10000;

        //优先级筛选
        if (taskBeanA.t_index < taskBeanB.t_index)
            resA += 1000;
        else if (taskBeanA.t_index > taskBeanB.t_index)
            resB += 1000;
        //下标筛选
        if (taskBeanA.t_id > taskBeanB.t_id)
            resA += 500;
        else if (taskBeanA.t_id < taskBeanB.t_id)
            resB += 500;

        if (resA < resB)
            return 1;
        else if (resA == resB)
            return 0;
        else
            return -1;
    }
    /// <summary>
    /// 一键领取
    /// </summary>
    private void OnYiJianLingQu()
    {
        TaskService.Singleton.OnReqOneKeyReward();
        window.m_YiJianLingQu.visible = false;
    }
    private void OnColseWindow(GameEvent evt)
    {
        switch (taskType)
        {
            case TaskType.SheTuanJuanXian:
                {
                    WinMgr.Singleton.Open<DonateHallWnd>(new WinInfo(),UILayer.Popup);
                    CloseBtn();
                } break;
        }
    }
    private void OnZongJiShiLian(GameEvent evt)
    {
        if (taskType == TaskType.ZongJiShiLian)
        {
            WinMgr.Singleton.Open<UltemateTrainMainWindow>(new WinInfo(),UILayer.Popup);
        }
    }
    //得到本次点击的任务的类型
    private void OnTaskType(GameEvent evt)
    {
        taskType = (TaskType)evt.Data;
        
    }
    //打开克隆组队战窗口
    private void OnResTeamFightMonsterInfo(GameEvent evt)
    {
        WinMgr.Singleton.Open<CloneMainWindow>(WinInfo.Create(false, null, true, evt.Data), UILayer.Popup);
    }
}
