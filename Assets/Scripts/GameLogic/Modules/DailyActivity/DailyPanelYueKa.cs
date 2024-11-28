using UI_DailyActivity;

public class DailyPanelYueKa : BaseDailyPanel
{
    UI_ActivityYueKa view;
    protected override void initView()
    {
        base.initView();
        view = panelView as UI_ActivityYueKa;
        refreshItemState(-1);
    }

    protected override void refreshItemState(int subId)
    {
        view.m_title.text = actData.title;
        view.m_desc.text = actData.desc;

        view.m_left1.text = string.Format("剩余{0}天", actData.child[0].progress);
        view.m_left2.text = string.Format("剩余{0}天", actData.child[1].progress);
        view.m_leftTitle.text = actData.child[0].desc;
        view.m_rightTitle.text = actData.child[1].desc;

        if(actData.child[0].state == 2)
        {
            //未激活
            view.m_left1.visible = false;
            view.m_leftBtn.visible = true;
        }else
        {
            view.m_left1.visible = true;
            view.m_leftBtn.visible = false;
        }

        if (actData.child[1].state == 2)
        {
            //未激活
            view.m_left2.visible = false;
            view.m_rightBtn.visible = true;
        }
        else
        {
            view.m_left2.visible = true;
            view.m_rightBtn.visible = false;
        }

        view.m_leftBtn.onClick.Clear();
        view.m_leftBtn.onClick.Add(()=>DailyPanelNotice.GotoWindow(actData.child[0].jump));

        view.m_rightBtn.onClick.Clear();
        view.m_rightBtn.onClick.Add(() => DailyPanelNotice.GotoWindow(actData.child[1].jump));
    }
}