using Message.Task;
using System.Collections.Generic;
using Data.Beans;

public class TaskService : SingletonService<TaskService>
{
    public ResTaskInfo TaskInfo { get; private set; }
    private Dictionary<int, TaskInfo> taskDic = new Dictionary<int, TaskInfo>();
    public bool lingqu;

    public TaskService()
    {
        if (GameManager.Singleton.IsDebug)
            InitTestData();
    }
    protected override void RegisterEventListener()
    {
        base.RegisterEventListener();
        GED.NED.addListener(ResTaskInfo.MsgId,OnTaskInfo);
        GED.NED.addListener(ResTaskInfoChange.MsgId,OnTaskInfoChange);
        GED.NED.addListener(ResAddTask.MsgId, OnResAddTask);
    }
    protected override void UnRegisterEventListener()
    {
        base.UnRegisterEventListener();
        GED.NED.removeListener(ResTaskInfo.MsgId, OnTaskInfo);
        GED.NED.removeListener(ResTaskInfoChange.MsgId, OnTaskInfoChange);
        GED.NED.removeListener(ResAddTask.MsgId, OnResAddTask);
    }
    private void InitTestData()
    { }
    /// <summary>
    /// 从服务器得到任务列表
    /// </summary>
    /// <param name="evt"></param>
    private void OnTaskInfo(GameEvent evt)
    {
        TaskInfo = GetCurMsg<ResTaskInfo>(evt.EventId);
        if (TaskInfo != null && TaskInfo.taskInfos != null)
        {
            foreach (TaskInfo taskinfo in TaskInfo.taskInfos)
            {
                if (taskDic.ContainsKey(taskinfo.id))
                    taskDic[taskinfo.id] = taskinfo;
                else
                    taskDic.Add(taskinfo.id,taskinfo);
            }
        }
        OnHongDian();
    }
    /// <summary>
    /// //任务信息改变
    /// </summary>
    private void OnTaskInfoChange(GameEvent evt)
    {
        ResTaskInfoChange infoChange = GetCurMsg<ResTaskInfoChange>(evt.EventId);
        List<TaskInfo> taskInfos = infoChange.info;
        for (int i = 0; i < taskInfos.Count; ++i)
        {
            if (taskDic.ContainsKey(taskInfos[i].id))
                taskDic[taskInfos[i].id] = taskInfos[i];
            else
                taskDic.Add(taskInfos[taskInfos[i].id].id, taskInfos[i]);
        }
        GED.ED.dispatchEvent(EventID.OnTaskInfoChange);
        OnHongDian();
    }
    /// <summary>
    /// 得到任务表
    /// </summary>
    /// <returns></returns>
    public List<TaskInfo> GetTaskInfos()
    {
        List<TaskInfo> infos = new List<TaskInfo>();
        infos.AddRange(taskDic.Values);
        if (TaskInfo != null && TaskInfo.taskInfos != null)
        {
            return infos;
        }
        else
            return null;
    }
    /// <summary>
    /// 通过指定id得到单个任务
    /// </summary>
    /// <param name="taskid"></param>
    /// <returns></returns>
    public TaskInfo GetTaskInfo(int taskid)
    {
        if (taskDic.ContainsKey(taskid))
            return taskDic[taskid];
        else
            return null;
    }
    /// <summary>
    /// 增加或者移除移除任务
    /// </summary>
    /// <param name="evt"></param>
    private void OnResAddTask(GameEvent evt)
    {
        ResAddTask res = GetCurMsg<ResAddTask>(evt.EventId);
        if (res.isAdd)
        {
            for (int i = 0; i < res.info.Count; ++i)
            {
                taskDic.Add(res.info[i].id, res.info[i]);
            }
        }
        else
        {
            for (int i = 0; i < res.info.Count; ++i)
            {
                if (taskDic.ContainsKey(res.info[i].id))
                    taskDic.Remove(res.info[i].id);
                else
                    Logger.err("TaskService:要删除的任务不存在");
            }
        }
        TwoParam<bool, List<TaskInfo>> twoParam = new TwoParam<bool, List<TaskInfo>>();
        twoParam.value1 = res.isAdd;
        twoParam.value2 = res.info;
        GED.ED.dispatchEvent(EventID.OnRefreshTaskList,twoParam);
        OnHongDian();
    }
    /// <summary>
    /// 请求领奖
    /// </summary>
    /// <param name="taskid"></param>
    /// <param name="type"></param>
    public void OnReward(int taskid)
    {
        ReqReward msg = GetEmptyMsg<ReqReward>();
        msg.id = taskid;
        SendMsg(ref msg);
    }
    /// <summary>
    /// 请求一键领奖
    /// </summary>
    public void OnReqOneKeyReward()
    {
        ReqOneKeyReward msg = GetEmptyMsg<ReqOneKeyReward>();
        SendMsg(ref msg);
    }
    private void OnHongDian()
    {
        //日常任务
        t_taskBean taskBean;
        List<int> taskid = new List<int>();
        taskid.AddRange(taskDic.Keys);
        bool everyday = false;
        bool zhuxian = false;
        for (int i = 0; i < taskid.Count; ++i)
        {
            taskBean = ConfigBean.GetBean<t_taskBean,int>(taskid[i]);
            if (taskBean == null)
                continue;
            if (taskBean.t_type == 0 || taskBean.t_type == 2)
            {
                if (taskDic[taskid[i]].state == 1)
                {
                    everyday = true;
                    break;
                }
            }
        }
        for (int i = 0; i < taskid.Count; ++i)
        {
            taskBean = ConfigBean.GetBean<t_taskBean,int>(taskid[i]);
            if (taskBean == null)
                continue;
            if (taskBean.t_type == 1)
            {
                if (taskDic[taskid[i]].state == 1)
                {
                    zhuxian = true;
                    break;
                }
            }
        }
        if (FuncService.Singleton.IsFuncOpen(1501))
        {
            RedDotManager.Singleton.SetRedDotValue("Task/everyday", everyday);
        }
        RedDotManager.Singleton.SetRedDotValue("Task/zhuxian", zhuxian);
    }
    public override void ClearData()
    {
        TaskInfo = null;
        if (taskDic.Count > 0)
            taskDic.Clear();
        base.ClearData();
    }
}
