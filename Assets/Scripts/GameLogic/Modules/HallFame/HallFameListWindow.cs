using UI_HallFame;
using Message.Pet;
using Data.Beans;
using System.Collections.Generic;
using Message.Bag;
using Message.Team;

public class HallFameListWindow : BaseWindow
{
    private UI_HallFameListWindow window;

    public override void OnOpen()
    {
        window = getUiWindow<UI_HallFameListWindow>();
        HallFameService.Singleton.OnReqHofInfo();
        AddKeyEvent();
        InitView();
    }
    public override void AddEventListener()
    {
        base.AddEventListener();
        GED.ED.addListener(EventID.OnAllPriority, OnPriority);
    }
    public override void RemoveEventListener()
    {
        base.RemoveEventListener();
        GED.ED.removeListener(EventID.OnAllPriority, OnPriority);
    }
    public override void InitView()
    {
        FillData();
    }
    private void AddKeyEvent()
    {
        AddEventListener();
        window.m_CloseBtn.onClick.Add(OnCloseBtn);
    }
    private void OnPriority(GameEvent evt)
    {
        int number = (int)evt.Data;
        window.m_ZongXianShouZhi.text = number.ToString();
    }
    /// <summary>
    /// 填充数据函数，可用于外部更新列表
    /// </summary>
    private void FillData()
    {
        
        HallFameListItem hallFame;
        window.m_TimeList.RemoveChildren(0, -1, true);
        List<t_hof_teamBean> hall_Of_FameBeans = ConfigBean.GetBeanList<t_hof_teamBean>();
        for (int i = 0; i < hall_Of_FameBeans.Count; ++i)
        {
            hallFame = HallFameListItem.CreateInstance();
            hallFame.Init(hall_Of_FameBeans[i]);
            window.m_TimeList.AddChild(hallFame);
        }
    }
    public override void RefreshView()
    {
        FillData();
    }
   
    protected override void OnCloseBtn()
    {
        RemoveEventListener();
        window.m_TimeList.RemoveChildren(0, -1, true);
        window = null;
        base.Close();
    }
}
