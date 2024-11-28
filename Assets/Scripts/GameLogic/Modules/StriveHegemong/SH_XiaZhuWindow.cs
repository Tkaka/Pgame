using Message.KingFight;
using System;
using System.Collections.Generic;
using UI_StriveHegemong;
using Data.Beans;

public class SH_XiaZhuWindow : BaseWindow
{
    private List<BetInfo> pets;
    private bool xiazhu;
    /// <summary>
    /// 发送下注信息
    /// 接收下注赔率信息
    /// </summary>
    private UI_SH_XiaZhuWindow window;
    public override void OnOpen()
    {
        base.OnOpen();
        window = getUiWindow<UI_SH_XiaZhuWindow>();
        StriveHegemongService.Singleton.OnReqBetInfo();
        AddKeyEvent();
        DateTime dateTime = TimeUtils.currentServerDateTime2();
        int currhour = dateTime.Hour;
        t_globalBean bean = ConfigBean.GetBean<t_globalBean,int>(1702001);
        string[] time = GTools.splitString(bean.t_string_param);
        int[] time2 = GTools.splitStringToIntArray(time[0],':');
        if (time2[0] > currhour)
            xiazhu = false;
        else
            xiazhu = true;
        
    }
    //得到前20名的链表
    public override void AddEventListener()
    {
        GED.ED.addListener(EventID.OnStriveHegemongBetInfo,OnShuaXin);
        GED.ED.addListener(EventID.OnStriveHegemongXiaZhu,OnXiaZhu);
    }
    public override void RemoveEventListener()
    {
        GED.ED.removeListener(EventID.OnStriveHegemongBetInfo,OnShuaXin);
        GED.ED.removeListener(EventID.OnStriveHegemongXiaZhu, OnXiaZhu);
    }
    private void AddKeyEvent()
    {
        window.m_CloseBtn.onClick.Add(OnCloseBtn);
    }
    public override void InitView()
    {
        pets = StriveHegemongService.Singleton.betInfo.info;
        base.InitView();
        RefreshView();
    }
    public override void RefreshView()
    {
        window.m_XiaZhuList.RemoveChildren(0,-1,true);
        pets.Sort(StorPaml);
        SH_XiaZhuListItem listItem;
        for (int i = 0; i < pets.Count; ++i)
        {
            listItem = SH_XiaZhuListItem.CreateInstance();
            listItem.Init(pets[i],xiazhu);
            window.m_XiaZhuList.AddChild(listItem);
        }
    }
    private void OnShuaXin(GameEvent evt)
    {
        InitView();
    }
    protected override void OnCloseBtn()
    {
        RemoveEventListener();
        window = null;
        base.OnCloseBtn();
    }
    private void OnXiaZhu(GameEvent evt)
    {
        OnCloseBtn();
    }
    private int StorPaml(BetInfo a,BetInfo b)
    {
        int resA = 0;
        int resB = 0;

        if (a.rank < b.rank)
            resA += 1000;
        else if (a.rank > b.rank)
            resB += 1000;

        if (resA < resB)
            return 1;
        else if (resA == resB)
            return 0;
        else
            return -1;
    }

}
