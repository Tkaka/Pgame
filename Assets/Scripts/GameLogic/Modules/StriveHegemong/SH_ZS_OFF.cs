using UI_StriveHegemong;
using Message.KingFight;
using System.Collections.Generic;

public class SH_ZS_OFF
{
    UI_SH_ZS_OFF window;
    private List<string> xianshi;
    private int alltime = 180;//总的时间
    private int awaittime = 300;
    private int time;//比赛进行了的时间
    private bool bisai = true;//是否正在比赛
    private int number;//刷新条数
    DoActionInterval xianshishuaxin = null;
    public SH_ZS_OFF(UI_SH_ZS_OFF zs)
    {
        window = zs;
        AddEventListener();
        time = 0;
        number = 0;
    }
    /// <summary>
    /// 玩法类型信息由 StriveHegemongService.Singleton.maininfo.playId;得到
    /// </summary>
    public void Init()
    {
        window.m_xianshiList.RemoveChildren(0,-1,true);
        //List<FightInfo> fights = StriveHegemongService.Singleton.myRaceInfo.infos;
        //for (int i = 0; i < fights.Count; ++i)
        //{
        //    if (i == fights.Count - 1)
        //    {
        //        if (fights[i].hasTime())
        //        {
        //            if (fights[i].time > 0)
        //            {
        //                time = fights[i].time;
        //                bisai = false;
        //            }
        //            else
        //            {
        //                bisai = true;
        //            }
        //        }
        //    }
        //}
    }
    private void AddEventListener()
    {
        GED.ED.addListener(EventID.OnNextCompetitionOpen,OnNextCompetitionOpen);
    }
    private void RemoveEventListener()
    {
        GED.ED.removeListener(EventID.OnNextCompetitionOpen, OnNextCompetitionOpen);
    }
    private void OnNextCompetitionOpen(GameEvent evt)
    {
        ResCourseInfo info = (ResCourseInfo)evt.Data;
        window.m_Changci.text = "第" + info.index + "场" ;
        xianshi = info.info;
        if (xianshi.Count > 0)
        {
            if (xianshishuaxin == null)
            {
                xianshishuaxin = new DoActionInterval();
                xianshishuaxin.doAction(1.0f, OnXianshiShuaXin);
            }
        }
        //更新总的时间
        alltime = 300;
        bisai = true;
        //得到比赛时间，总秒数
    }
    private void OnXianshiShuaXin(object obj)
    {
        ////得到我的赛程信息，得到最后一个赛程的时间
        //int surplustime;
        //if (bisai)
        //    surplustime = alltime - time;
        //else
        //    surplustime = awaittime - time;

        //if (bisai)
        //{
        SH_ZS_OFF_ListIten listIten;
        listIten = SH_ZS_OFF_ListIten.CreateInstance();
        Logger.err(string.Format("{0}/{1}", number, xianshi.Count));
        if (number >= xianshi.Count)
        {
            number = 0;
        }

        listIten.Init(xianshi[number++]);
        window.m_xianshiList.AddChildAt(listIten, 0);

        if (number == xianshi.Count - 1)
        {
            number = 0;
        }
        if (window.m_xianshiList.numItems == 30)
        {
            window.m_xianshiList.RemoveChildren(10,30,true);
        }
        //}
        //window.m_time.text = (surplustime / 60) + ":" + (surplustime % 60);
    }
    
    public void Close()
    {
        if (xianshishuaxin != null)
        {
            xianshishuaxin.kill();
            xianshishuaxin = null;
        }
        xianshi = null;
        RemoveEventListener();
    }
}
