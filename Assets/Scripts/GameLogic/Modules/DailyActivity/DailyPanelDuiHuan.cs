using FairyGUI;
using UI_DailyActivity;
using Message.DailyActivity;

public class DailyPanelDuiHuan : BaseDailyPanel
{
    UI_ActivityDuiHuan view;
    protected override void initView()
    {
        view = panelView as UI_ActivityDuiHuan;
        view.m_title.text = actData.title;
        view.m_desc.text = actData.desc;

        view.m_list.itemRenderer = renderItem;
        view.m_list.SetVirtual();
        view.m_list.numItems = actData.child.Count;
    }

    private void renderItem(int idx, GObject obj)
    {
        var item = obj as UI_DuiHuanItem;
        var data = actData.child[idx];

        int canDhNum = int.MaxValue;
        item.m_list.RemoveChildrenToPool();
        string costStr = "";
        var arr = data.cost.Split(';');
        foreach(var tmp in arr)
        {
            var arr2 = tmp.Split('+');
            int costId = int.Parse(arr2[0]);
            int costNum = int.Parse(arr2[1]);
            var ib = ConfigBean.GetBean<Data.Beans.t_itemBean, int>(costId);
            if(ib != null)
                costStr += ib.t_name + "*" + arr2[1] + " ";

            var icon = item.m_list.GetFromPool(CommonItem.URL) as CommonItem;
            icon.Init(costId, costNum, true);
            item.m_list.AddChild(icon);

            int hasNum = BagService.Singleton.GetItemNum(costId);
            canDhNum = UnityEngine.Mathf.Min(hasNum / costNum);
        }
        item.m_cost.text = costStr;

        var arr3 = data.reward.Split('+');
        var ib3 = ConfigBean.GetBean<Data.Beans.t_itemBean, int>(int.Parse(arr3[0]));
        if (ib3 != null)
            item.m_target.text = ib3.t_name + "*" + arr3[1];
        else
            item.m_target.text = "未知道具*" + arr3[1];

        item.m_progress.text = string.Format("{0}/{1}", data.progress, data.totalCount);
        switch (data.state)
        {
            case 1: //可兑换
                //item.m_dhBtn.text = "兑换";
                item.m_dhBtn.enabled = canDhNum > 0 && data.progress < data.totalCount;
                break;
            default:
                //item.m_dhBtn.text = "领取";
                item.m_dhBtn.enabled = false;
                break;
        }

        item.m_dhBtn.onClick.Clear();
        item.m_dhBtn.onClick.Add(() => {
            if (canDhNum == 1)
            {
                DailyActivityService.Singleton.ReqTaskRewards(data.id);
            }
            else if(canDhNum > 1)
            {
                var param = new TwoParam<int, int>();
                param.value1 = data.id;
                param.value2 = canDhNum;
                WinMgr.Singleton.Open<WndDuiHuanNum>(WinInfo.Create(false, null, true, param), UILayer.Popup);
            }
        });
    }

    protected override void countDown(object param)
    {
        var now = TimeUtils.javaTimeToCSharpTime(TimeUtils.currentMilliseconds());
        var end = TimeUtils.javaTimeToCSharpTime(actData.endTime);
        var left = end - now;

        if (left.Days > 0)
            view.m_leftTime.text = string.Format("{0}天{1:D2}:{2:D2}:{3:D2}", left.Days, left.Hours, left.Minutes, left.Seconds);
        else
            view.m_leftTime.text = string.Format("{0:D2}:{1:D2}:{2:D2}", left.Hours, left.Minutes, left.Seconds);
    }

    protected override void refreshItemState(int subId)
    {
        base.refreshItemState(subId);
        for (int i = 0, len = actData.child.Count; i < len; ++i)
        {
            if (actData.child[i].id == subId)
            {
                int start = view.m_list.ChildIndexToItemIndex(0);
                int end = view.m_list.numChildren + start;
                if (i >= start && i <= end)
                    view.m_list.RefreshVirtualList();
                break;
            }
        }
    }
}