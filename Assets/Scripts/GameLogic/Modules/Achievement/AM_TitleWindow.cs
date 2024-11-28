using UI_Achievement;
using Data.Beans;
using System.Collections.Generic;

public class AM_TitleWindow : BaseWindow
{
    private UI_AM_TitleWindow window;

    public override void OnOpen()
    {
        window = getUiWindow<UI_AM_TitleWindow>();

        window.m_CloseBtn.onClick.Add(OnCloseBtn);
        InitView();
    }
    public override void InitView()
    {
        int title = 123;
        if (AchievementService.Singleton.achievementinfo != null)
        {
            title = AchievementService.Singleton.achievementinfo.title;
        }
        List<t_titleBean> titleBeans = ConfigBean.GetBeanList<t_titleBean>();
        AM_TitleLietItem titleLietItem;
        int index = 0;
        for (int i = titleBeans.Count - 1; i >= 0; --i)
        {
            titleLietItem = AM_TitleLietItem.CreateInstance();
            if (title == titleBeans[i].t_id)
                index = i;
            titleLietItem.Init(titleBeans[i], title == titleBeans[i].t_id);
            window.m_TitleList.AddChild(titleLietItem);
        }
        window.m_TitleList.ScrollToView(titleBeans.Count - index - 1);
    }
    protected override void OnCloseBtn()
    {
        window = null;
        base.OnCloseBtn();
    }

}