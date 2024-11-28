using Message.Achievement;
using System.Collections.Generic;


public class AchievementService : SingletonService<AchievementService>
{
    public ResAchievementInfo achievementinfo { get; private set; }
    private Dictionary<int, AchievementInfo> achievementList = new Dictionary<int, AchievementInfo>();
    public ResAchievementRank achievementRank { get; private set; }

    public AchievementService()
    {
        if (GameManager.Singleton.IsDebug)
            InitTestData();
    }
    /// <summary>
    /// 测试数据
    /// </summary>
    private void InitTestData()
    {
        achievementinfo = new ResAchievementInfo();
        achievementinfo.title = 7;
        achievementinfo.precedeValue = 4567;
        achievementinfo.core = 2234;
    }
    protected override void RegisterEventListener()
    {
        base.RegisterEventListener();
        GED.NED.addListener(ResAchievementInfo.MsgId, OnAchievementInfo);
        GED.NED.addListener(ResAchievementChange.MsgId, OnAchievementChange);
        GED.NED.addListener(ResFinish.MsgId, OnAchievementFinish);
        GED.NED.addListener(ResAchievementRank.MsgId, OnAchievementRank);
    }
    protected override void UnRegisterEventListener()
    {
        base.UnRegisterEventListener();
        GED.NED.removeListener(ResAchievementInfo.MsgId, OnAchievementInfo);
        GED.NED.removeListener(ResAchievementChange.MsgId, OnAchievementChange);
        GED.NED.removeListener(ResFinish.MsgId, OnAchievementFinish);
        GED.NED.removeListener(ResAchievementRank.MsgId, OnAchievementRank);
    }
    /// <summary>
    /// 获取成就任务列表
    /// </summary>
    /// <param name="evt"></param>
    private void OnAchievementInfo(GameEvent evt)
    {
        achievementinfo = GetCurMsg<ResAchievementInfo>(evt.EventId);
        if (achievementinfo != null && achievementinfo.infos != null)
        {
            foreach (AchievementInfo info in achievementinfo.infos)
            {
                if (achievementList.ContainsKey(info.id))
                    achievementList[info.id] = info;
                else
                    achievementList.Add(info.id,info);
            }
        }
    }
    /// <summary>
    /// 成就信息改变
    /// </summary>
    private void OnAchievementChange(GameEvent evt)
    {
        ResAchievementChange change = GetCurMsg<ResAchievementChange>(evt.EventId);
        List<AchievementInfo> infos = change.info;
        for (int i = 0; i < infos.Count; ++i)
        {
            if (achievementList.ContainsKey(infos[i].id))
            {
                achievementList[infos[i].id] = infos[i];
                for (int j = 0; j < achievementinfo.infos.Count; ++j)
                {
                    if (achievementinfo.infos[j].id == infos[i].id)
                    {
                        achievementinfo.infos[j] = infos[i];
                        break;
                    }
                }
            }
            else
            {
                if (achievementinfo != null)
                {
                    achievementList.Add(infos[i].id, infos[i]);
                    achievementinfo.infos.Add(infos[i]);
                }
            }
                
        }
        GED.ED.dispatchEvent(EventID.OnAchievmentchage);
    }
    /// <summary>
    /// 领取成就后成就界面总信息改变
    /// </summary>
    /// <param name="evt"></param>
    private void OnAchievementFinish(GameEvent evt)
    {
        ResFinish res = GetCurMsg<ResFinish>(evt.EventId);
        if (achievementList.ContainsKey(res.info.id))
        {
            achievementList[res.info.id] = res.info;
            for (int j = 0; j < achievementinfo.infos.Count; ++j)
            {
                if (achievementinfo.infos[j].id == res.info.id)
                {
                    achievementinfo.infos[j] = res.info;
                    break;
                }
            }
        }
        else
        {
            achievementList.Add(res.info.id, res.info);
            achievementinfo.infos.Add(res.info);
        }
        achievementinfo.core = res.core;
        achievementinfo.precedeValue = res.precedeValue;
        achievementinfo.title = res.title;
        GED.ED.dispatchEvent(EventID.OnAchievmentchage);
    }
    /// <summary>
    /// 获得成就排行榜
    /// </summary>
    private void OnAchievementRank(GameEvent evt)
    {
        achievementRank = GetCurMsg<ResAchievementRank>(evt.EventId);
    }
    /// <summary>
    /// 获得单个成就的信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public AchievementInfo GetAchievementInfo(int id)
    {
        AchievementInfo info = null;
        if (achievementList.ContainsKey(id))
            info = achievementList[id];
        return info;
    }
    /// <summary>
    /// 得到任务列表
    /// </summary>
    /// <returns></returns>
    public List<AchievementInfo> GetAchievementInfos()
    {
        if (achievementinfo != null && achievementinfo.infos != null)
            return achievementinfo.infos;
        return null;
    }
    /// <summary>
    /// 请求领取成就奖励
    /// </summary>
    public void OnReqFinish(int id)
    {
        ReqFinish req = GetEmptyMsg<ReqFinish>();
        req.id = id;
        SendMsg(ref req);
    }
    /// <summary>
    /// 请求排行榜信息
    /// </summary>
    public void OnReqAchievementRank(int index)
    {
        ReqAchievementRank req = GetEmptyMsg<ReqAchievementRank>();
        req.begin = index;
        SendMsg(ref req);
    }
    public override void ClearData()
    {
        achievementinfo = null;
        if(achievementList.Count > 0)
            achievementList.Clear();
        achievementRank = null;
        base.ClearData();
    }
}
