using FairyGUI;
using Message.DailyActivity;
using System.Collections;
using System.Collections.Generic;
using UI_Common;
using UI_DailyActivity;

public class WndDailyRoot : BaseWindow
{
    UI_WndDailyRoot window;
    private List<UI_TabButton> tabList = new List<UI_TabButton>();
    private Dictionary<int, BaseDailyPanel> panelList = new Dictionary<int, BaseDailyPanel>();
    private List<ActivityData> dataList;
    private int lastIdx;
    
    public override void OnOpen()
    {
        base.OnOpen();
        window = getUiWindow<UI_WndDailyRoot>();

        UI_commonTop commonTop = window.m_top as UI_commonTop;
        if (commonTop != null)
            commonTop.m_closeBtn.onClick.Add(OnCloseBtn);
        window.m_table.m_list.onClickItem.Add(onTabChange);

        if (DailyActivityService.Singleton.ReqActivityInfo())
            window.visible = false;
        else
            InitView();
    }

    public override void RemoveEventListener()
    {
        GED.ED.removeListener(EventID.DailyActivityRefresh, onMsgBack);
    }

    public override void AddEventListener()
    {
        GED.ED.addListener(EventID.DailyActivityRefresh, onMsgBack);
    }


    private void onMsgBack(GameEvent evt)
    {
        window.visible = true;
        InitView();
    }

    public override void InitView()
    {
        var enu = panelList.GetEnumerator();
        while (enu.MoveNext())
        {
            var panel = enu.Current.Value;
            if (panel != null)
                panel.Dispose();
        }
        enu.Dispose();

        dataList = DailyActivityService.Singleton.GetActivityData();
        dataList.Sort((a, b) => {
            return a.sort.CompareTo(b.sort);
        });
        updateTable();
    }

    private void updateTable()
    {
        tabList.Clear();
        float pos = window.m_table.m_list.scrollPane.posY;
        window.m_table.m_list.RemoveChildrenToPool();

        for(int i=0, len = dataList.Count; i<len; ++i)
        {
            var data = dataList[i];
            var tab = window.m_table.m_list.GetFromPool(UI_TabButton.URL) as UI_TabButton;
            this._RegisterRedDot("DAct/" + data.id, tab.m_xhd);

            window.m_table.m_list.AddChild(tab);
            tabList.Add(tab);

            tab.m_tabTxt1.text = data.leftTitle;
            tab.m_tabTxt2.text = data.leftTitle;
            UIGloader.SetUrl(tab.m_showIcon, data.icon);
            if(string.IsNullOrEmpty(data.mark))
            {
                tab.m_mark.visible = false;
            }else
            {
                tab.m_mark.visible = true;
                UIGloader.SetUrl(tab.m_mark, data.mark);
            }

            tab.m_mark.SetScale(0.1f, 0.1f);
            tab.m_showIcon.SetScale(0.1f, 0.1f);
        }
        window.m_table.m_list.scrollPane.posY = pos;

        if (window.m_table.m_list.GetChildren().Length > lastIdx)
            (window.m_table.m_list.GetChildAt(lastIdx) as GButton).selected = true;
        selectIdx(lastIdx);
    }

    private void onTabChange()
    {
        int idx = window.m_table.m_list.selectedIndex;
        //tabList[lastIdx].sortingOrder = tabList[idx].sortingOrder;
        //tabList[idx].sortingOrder = 3500;
        selectIdx(idx);
    }
    
    private void selectIdx(int idx)
    {
        if(panelList.ContainsKey(lastIdx))
            panelList[lastIdx].Hide();
        lastIdx = idx;

        var data = dataList[idx];
        if (panelList.ContainsKey(idx))
        {
            panelList[idx].Show();
        }
        else
        {
            BaseDailyPanel panel = null;
            switch (data.type)
            {
                case 1: //公告
                    panel = new DailyPanelNotice();
                    break;
                case 2: //兑换
                    panel = new DailyPanelDuiHuan();
                    break;
                case 3: //任务
                    panel = new DailyPanelTask();
                    break;
                case 4: //月卡
                    panel = new DailyPanelYueKa();
                    break;
            }

            if (panel != null)
            {
                panel.Init(data, window.m_contentBg);
                panelList[idx] = panel;
            }
        }
    }

    public override void PlayOpenEffect()
    {
        base.PlayOpenEffect();
        (window.m_top as UI_commonTop).m_anim.Play();
    }

    protected override void OnClose()
    {
        base.OnClose();
        var enu = panelList.GetEnumerator();
        while(enu.MoveNext())
        {
            var panel = enu.Current.Value;
            if (panel != null)
                panel.Dispose();
        }
        enu.Dispose();
    }
}

public abstract class BaseDailyPanel
{
    private bool timerAdd = false;

    protected GComponent panelView;
    protected ActivityData actData;

    public void Init(ActivityData data, GComponent parent)
    {
        GED.ED.addListener(EventID.DailySubActivityChangeState, changeState);

        actData = data;
        switch(data.type)
        {
            case 1:
                panelView = UI_ActivityNotice.CreateInstance();
                break;
            case 2:
                panelView = UI_ActivityDuiHuan.CreateInstance();
                break;
            case 3:
                panelView = UI_ActivityTask.CreateInstance();
                break;
            case 4:
                panelView = UI_ActivityYueKa.CreateInstance();
                break;
            default:
                panelView = UI_ActivityNotice.CreateInstance();
                break;
        }

        if (panelView != null)
            parent.AddChild(panelView);
        else
            Debuger.Err("未定义的活动类型");
        initView();
        countDown(null);
    }
    
    private void changeState(GameEvent evt)
    {
        var param = evt.Data as TwoParam<int, int>;
        if (param.value1 == actData.id)
            refreshItemState(param.value2);
    }

    protected virtual void refreshItemState(int subId)
    {

    }

    protected virtual void initView()
    {

    }

    protected virtual void countDown(object param)
    {

    }

    public virtual void Show()
    {
        panelView.visible = true;
        if (!timerAdd)
            Timers.inst.Add(1, 0, countDown);
        timerAdd = true;
    }

    public virtual void Hide()
    {
        panelView.visible = false;
        if (timerAdd)
            Timers.inst.Remove(countDown);
        timerAdd = false;
    }

    public virtual void Dispose()
    {
        Hide();
        panelView.parent.RemoveChild(panelView, true);
        GED.ED.removeListener(EventID.DailySubActivityChangeState, changeState);
    }
}