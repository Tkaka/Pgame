using UI_GeDouJia;
using Data.Beans;
using System.Collections.Generic;

public class SuMingJiHuoWindow : BaseWindow
{
    private UI_SuMingJiHuoWindow window;
    t_fetterBean fetterBean;

    public override void OnOpen()
    {
        InitView();
    }
    public override void InitView()
    {
        window = getUiWindow<UI_SuMingJiHuoWindow>();
        window.m_CloseBtn.onClick.Add(CloseBtn);
        if (Info.param == null)
        {
            Logger.err("SuNingJiHUo:InitView:未获得宠物id");
            return;
        }
        t_petBean petBean = ConfigBean.GetBean<t_petBean,int>((int)Info.param);
        if (petBean == null)
        {
            Logger.err("SuMingJiHuoWindow:InitView:未在宠物表找到对应宠物-----" + petBean.t_id);
            return;
        }
        List<int> fetterid = PetService.Singleton.suMingId;
        window.m_number.text = fetterid.Count.ToString();
        SuMingListItem suMingListItem = null;
        window.m_SuMingList.RemoveChildren(0,-1,true);
        for (int i = 0; i < fetterid.Count; ++i)
        {
            if (UIUtils.OnFetterState(petBean.t_id, fetterid[i]))
            {
                suMingListItem = SuMingListItem.CreateInstance();
                suMingListItem.Init(fetterid[i]);
                window.m_SuMingList.AddChild(suMingListItem);
            }
        }
    }

    private void CloseBtn()
    {
        if(window != null)
            window = null;
        fetterBean = null;
        Close();
    }
}
