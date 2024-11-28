using FairyGUI;
using UI_DailyActivity;

public class DailyPanelNotice : BaseDailyPanel
{
    private UI_ActivityNotice view;
    protected override void initView()
    {
        view = panelView as UI_ActivityNotice;
        var start = TimeUtils.javaTimeToCSharpTime(actData.startTime);
        var end = TimeUtils.javaTimeToCSharpTime(actData.endTime);

        view.m_txtTime.text = string.Format("{0}年{1}月{2}日{3:D2}时-{4}年{5}月{6}日{7:D2}时", start.Year, start.Month, start.Day, start.Hour, end.Year, end.Month, end.Day, end.Hour);
        view.m_txtContent.text = actData.desc;
    }

    protected override void countDown(object param)
    {
        var now = TimeUtils.javaTimeToCSharpTime(TimeUtils.currentMilliseconds());
        var end = TimeUtils.javaTimeToCSharpTime(actData.endTime);
        var left = end - now;

        if (left.Days > 0)
            view.m_txtLeft.text = string.Format("{0}天{1:D2}:{2:D2}:{3:D2}", left.Days, left.Hours, left.Minutes, left.Seconds);
        else
            view.m_txtLeft.text = string.Format("{0:D2}:{1:D2}:{2:D2}", left.Hours, left.Minutes, left.Seconds);
    }

    public static void GotoWindow(int id)
    {

    }
}