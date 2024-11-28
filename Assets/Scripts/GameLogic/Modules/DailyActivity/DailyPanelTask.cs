using FairyGUI;
using UI_DailyActivity;

public class DailyPanelTask : BaseDailyPanel
{
    UI_ActivityTask view;
    protected override void initView()
    {
        view = panelView as UI_ActivityTask;
        view.m_title.text = actData.title;
        view.m_desc.text = actData.desc;

        view.m_list.itemRenderer = renderItem;
        view.m_list.SetVirtual();
        view.m_list.numItems = actData.child.Count;
    }

    private void renderItem(int idx, GObject obj)
    {
        var item = obj as UI_TaskItem;
        var data = actData.child[idx];

        item.m_list.RemoveChildrenToPool();
        var arr = data.cost.Split(';');
        foreach (var tmp in arr)
        {
            var arr2 = tmp.Split('+');
            var icon = item.m_list.GetFromPool(CommonItem.URL) as CommonItem;
            icon.Init(int.Parse(arr2[0]), int.Parse(arr2[1]), true);
            item.m_list.AddChild(icon);
        }
        item.m_cost.text = data.desc;
        item.m_propress.text = string.Format("{0}/{1}", data.progress, data.totalCount);

        switch (data.state)
        {
            case 1: //可领取
                item.m_lqBtn.text = "领取";
                item.m_lqBtn.enabled = true;
                break;
            case 3: //已领取
                item.m_lqBtn.text = "已领取";
                item.m_lqBtn.enabled = true;
                break;
            default:
                item.m_lqBtn.text = "领取";
                item.m_lqBtn.enabled = false;
                break;
        }
        
        item.m_lqBtn.onClick.Clear();
        item.m_lqBtn.onClick.Add(()=>{
            DailyActivityService.Singleton.ReqTaskRewards(data.id);
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