using Message.DailyActivity;
using System.Collections.Generic;

public enum DiscountType
{
    StoreEquipBoxOne = 501, //商店装备宝箱单抽
    StoreEquipBoxTen = 502, //商店装备宝箱十连抽
}

public enum DropTimeType
{
    MainLevel = 601,        //主线
    Gold = 602,             //金币关卡
    Exp = 603,              //经验
    FinalGold = 604,        //终极试炼金币
}


public class DailyActivityService : SingletonService<DailyActivityService>
{
    private Dictionary<int, ActivityData> actMap = new Dictionary<int, ActivityData>();
    private Dictionary<DiscountType, int> discountMap = new Dictionary<DiscountType, int>();
    private Dictionary<DropTimeType, int> dropMap = new Dictionary<DropTimeType, int>();

    protected override void RegisterEventListener()
    {
        base.RegisterEventListener();
        GED.NED.addListener(ResActivityInfo.MsgId, onActInfo);
        GED.NED.addListener(ResChangeSubActivityState.MsgId, onActChange);

        for (int i=1; i<10; ++i)
        {
            var data = new ActivityData();
            data.id = i;
            var bean = ConfigBean.GetBean<Data.Beans.t_normal_activityBean, int>(i);
            if(bean != null)
            {
                data.desc = bean.t_desc;
                data.endTime    = 222222222;
                data.startTime  = 111111111;
                data.title = bean.t_content;
                data.leftTitle = bean.t_left_content;
                data.mark = bean.t_mark;
                data.sort = i;
                data.type = bean.t_type;
                data.icon = bean.t_icon;

                var arr = bean.t_child.Split('+');
                foreach(var t in arr)
                {
                    int sid = int.Parse(t);
                    var sb = ConfigBean.GetBean<Data.Beans.t_normal_activity_itemBean, int>(sid);
                    if(sb != null)
                    {
                        var item = new SubActivityData();
                        item.id = sid;
                        item.reward = sb.t_reward;
                        item.condition = sb.t_finish_condition;
                        item.cost = sb.t_cost;
                        item.desc = sb.t_desc;
                        item.progress = 2;
                        item.totalCount = 10;
                        item.state = 1;
                        data.child.Add(item);
                    }
                }
            }
            actMap.Add(i, data);
        }
    }

    public List<ActivityData> GetActivityData()
    {
        return new List<ActivityData>(actMap.Values);
    }

    private void onActInfo(GameEvent evt)
    {
        var dis = new int[] { (int)DiscountType .StoreEquipBoxOne, (int)DiscountType.StoreEquipBoxTen};
        var drop = new int[] { (int)DropTimeType.MainLevel, (int)DropTimeType.Gold, (int)DropTimeType.FinalGold, (int)DropTimeType.Exp};


        List<int> askList = new List<int>();
        var msg = GetCurMsg<ResActivityInfo>(evt.EventId);
        for (int i = 0, len = msg.actList.Count; i < len; ++i)
        {
            var act = msg.actList[i];
            var bean = ConfigBean.GetBean<Data.Beans.t_normal_activityBean, int>(act.id);
            if (bean == null || bean.t_version != act.version)
            {
                //客户端版本号与服务器不一致，需要重新获取完整数据
                askList.Add(act.id);
            }else
            {
                bool outOfTime = false;
                for (int j = 0, num = act.child.Count; j < num; ++j)
                {
                    var cb = ConfigBean.GetBean<Data.Beans.t_normal_activity_itemBean, int>(act.child[j].id);
                    if(cb != null && cb.t_version != act.child[j].version)
                    {
                        outOfTime = true;
                        askList.Add(act.id);
                        break;
                    }
                }
                if (outOfTime)
                    continue;

                var data = new ActivityData();
                data.id = act.id;
                data.endTime = act.endTime;
                data.startTime = act.startTime;

                data.mark = bean.t_mark;
                data.icon = bean.t_icon;
                data.leftTitle = bean.t_left_content;
                data.title = bean.t_content;
                data.desc = bean.t_desc;
                for (int j = 0, num = act.child.Count; j < num; ++j)
                {
                    var ch = act.child[j];
                    var child = new SubActivityData();
                    child.id = ch.id;
                    child.state = ch.state;
                    child.progress = ch.progress;
                    child.totalCount = ch.totalCount;
                    child.startTime = ch.startTime;

                    var cb = ConfigBean.GetBean<Data.Beans.t_normal_activity_itemBean, int>(ch.id);
                    if(cb != null)
                    {
                        child.reward = cb.t_reward;
                        child.cost = cb.t_cost;
                        child.desc = cb.t_desc;
                        child.condition = cb.t_finish_condition;
                    }
                    data.child.Add(child);

                    bool flag = false;
                    for (int k = 0; k < dis.Length; ++k)
                    {
                        if (child.condition.StartsWith(dis[i] + "+"))
                        {
                            var arr = child.condition.Split('+');
                            discountMap[(DiscountType)dis[k]] = int.Parse(arr[1]);
                            flag = true;
                            break;
                        }
                    }

                    if (!flag)
                    {
                        for (int k = 0; k < drop.Length; ++k)
                        {
                            if (child.condition.StartsWith(drop[i] + "+"))
                            {
                                var arr = child.condition.Split('+');
                                dropMap[(DropTimeType)drop[k]] = int.Parse(arr[1]);
                                break;
                            }
                        }
                    }
                }
                actMap[act.id] = data;
            }
        }

        for (int i = 0, len = msg.actDataList.Count; i < len; ++i)
        {
            actMap[msg.actDataList[i].id] = msg.actDataList[i];
            for (int j = 0, num = msg.actDataList[i].child.Count; j < num; ++j)
            {
                var child = msg.actDataList[i].child[j];
                bool flag = false;
                for (int k = 0; k < dis.Length; ++k)
                {
                    if (child.condition.StartsWith(dis[i] + "+"))
                    {
                        var arr = child.condition.Split('+');
                        discountMap[(DiscountType)dis[k]] = int.Parse(arr[1]);
                        flag = true;
                        break;
                    }
                }

                if (!flag)
                {
                    for (int k = 0; k < drop.Length; ++k)
                    {
                        if (child.condition.StartsWith(drop[i] + "+"))
                        {
                            var arr = child.condition.Split('+');
                            dropMap[(DropTimeType)drop[k]] = int.Parse(arr[1]);
                            break;
                        }
                    }
                }
            }   
        }

        if(askList.Count > 0)
        {
            //有过期的配置，请求最新的配置
            var sMsg = GetEmptyMsg<ReqActivityInfo>();
            sMsg.actList.AddRange(askList);
            SendMsg(ref sMsg);
        }else
        {
            refreshRedot();
            GED.ED.dispatchEvent(EventID.DailyActivityRefresh);
        }
    }
    
    private void onActChange(GameEvent evt)
    {
        var msg = GetCurMsg<ResChangeSubActivityState>(evt.EventId);
        if(actMap.ContainsKey(msg.id))
        {
            for (int i = 0, len = actMap[msg.id].child.Count; i < len; ++i)
            {
                var child = actMap[msg.id].child[i];
                if(child.id == msg.subId)
                {
                    child.progress = msg.progress;
                    child.state = msg.state;
                    break;
                }
            }
        }

        TwoParam<int, int> param = new TwoParam<int, int>();
        param.value1 = msg.id;
        param.value2 = msg.subId;
        GED.ED.dispatchEvent(EventID.DailySubActivityChangeState, param);

        refreshRedot();
    }

    private List<int> addRedList = new List<int>();
    private void refreshRedot()
    {
        for (int i = 0, len = addRedList.Count; i < len; ++i)
            RedDotManager.Singleton.SetRedDotValue("DAct/" + addRedList[i], false);

        addRedList.Clear();
        var enu = actMap.GetEnumerator();
        while (enu.MoveNext())
        {
            var act = enu.Current.Value;
            for (int i = 0, len = act.child.Count; i < len; ++i)
            {
                if (act.child[i].state == 1)
                {
                    addRedList.Add(act.id);
                    RedDotManager.Singleton.SetRedDotValue("DAct/" + act.id, true);
                    break;
                }
            }
        }
        enu.Dispose();
    }

    //获取折扣
    public int GetDiscount(DiscountType type)
    {
        if (discountMap.ContainsKey(type))
            return discountMap[type];
        return 100;
    }

    //获取掉落倍率
    public int GetDropTime(DropTimeType type)
    {
        if (dropMap.ContainsKey(type))
            return dropMap[type];
        return 1;
    }

    public void ReqTaskRewards(int subId, int num = 1)
    {
        var msg = GetEmptyMsg<ReqTaskReward>();
        msg.subId = subId;
        msg.num = num;
        SendMsg(ref msg);
    }

    private long lastInfoReqTime;
    public bool ReqActivityInfo()
    {
        //60秒内请求忽略
        if (TimeUtils.currentMilliseconds() - lastInfoReqTime < 60)
            return false;

        lastInfoReqTime = TimeUtils.currentMilliseconds();
        //不给任何id，则全部发送
        var sMsg = GetEmptyMsg<ReqActivityInfo>();
        SendMsg(ref sMsg);
        return true;
    }

    public override void ClearData()
    {
        base.ClearData();
        actMap.Clear();
        dropMap.Clear();
        discountMap.Clear();
    }
}