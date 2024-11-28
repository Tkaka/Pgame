using UI_StriveHegemong;
using Message.KingFight;
using System;
using Data.Beans;
using System.Collections.Generic;

public class SH_WoDeSaiCheng
{
    private UI_SH_WoDeSaiCheng window;
    private DoActionInterval doAction;
    private List<FightInfo> myRaceInfo;//我的赛程
    private int time;//倒计时秒数


    public SH_WoDeSaiCheng(UI_SH_WoDeSaiCheng saicheng)
    {
        window = saicheng;

        AddKeyEvent();
        //计算时间
        if (StriveHegemongService.Singleton.maininfo == null)
        { }
        else
        {
            t_themeBean themeBean = ConfigBean.GetBean<t_themeBean, int>(StriveHegemongService.Singleton.maininfo.playId);
            if (themeBean == null)
            {
                Logger.err("SH_WoDeSaiCheng:OnWeiKaiSaiFillData:未从玩法表获得对应数据，请检查玩法id是否正确---" + StriveHegemongService.Singleton.maininfo.playId);
            }
            else
            {
                window.m_ZhuTi.text = themeBean.t_topic;
                if (themeBean.t_type != 0)
                {
                    window.m_ZhuTiXiaoGuo.text = string.Format(themeBean.t_desc, (themeBean.t_desc_number).ToString());
                }
                else
                    window.m_ZhuTiXiaoGuo.text = themeBean.t_desc;
            }
        }
    }
    private void AddKeyEvent()
    {
        GED.ED.addListener(EventID.OnStriveHegemongOpenBox, OnOpenBox);
        GED.ED.addListener(EventID.OnstriveHegemongMySelfInfo, OnYiKaiZhanShuaXin);
        window.m_BuZhenBtn.onClick.Add(OnBuZhenBtn);
    }
    public void Init()
    {
        if (StriveHegemongService.Singleton.open == 0)
        {
            OnWeiKaiSaiFillData();
        }
    }
    private void OnOpenBox(GameEvent evt)
    {
        
        myRaceInfo = StriveHegemongService.Singleton.myRaceInfo.infos;
        window.m_ZhanKuang.text = StriveHegemongService.Singleton.myRaceInfo.winNum + "胜" + StriveHegemongService.Singleton.myRaceInfo.failedNum + "负";
        window.m_JiFen.text = StriveHegemongService.Singleton.myRaceInfo.core.ToString();
        if (StriveHegemongService.Singleton.myRaceInfo.state == 0 || StriveHegemongService.Singleton.myRaceInfo.state == 1)
        {
            OnYiKaiSai();
        }
        else if (StriveHegemongService.Singleton.myRaceInfo.state == 2)
        {
            OnYiKaiSai();
            //进八强赛
            OnBaQiangSai();
        }
        OnTongZhi();
    }
    private void OnYiKaiZhanShuaXin(GameEvent evt)
    {
        
        window.m_SaiChengList.RemoveChildren(0, -1, true);
        if (StriveHegemongService.Singleton.myRaceInfo.state == 0)
        {
            OnWeiKaiSaiFillData();
        }
        else if (StriveHegemongService.Singleton.myRaceInfo.state == 1)
        {
            myRaceInfo = StriveHegemongService.Singleton.myRaceInfo.infos;
            OnYiKaiSai(); OnTongZhi();
        }
        else if (StriveHegemongService.Singleton.myRaceInfo.state == 2)
        {
            OnBaQiangSai();
            OnTongZhi();
        }
    }
    private void OnYiKaiSai()
    {
        window.m_SaiChengList.RemoveChildren(0, -1, true);

        SH_WS_ListItem listItem;
        for (int i = 0; (i < myRaceInfo.Count && i < 10); ++i)
        {
            listItem = SH_WS_ListItem.CreateInstance();
            listItem.Init(myRaceInfo[i], i);
            window.m_SaiChengList.AddChildAt(listItem, 0);
        }
        int shengchang = StriveHegemongService.Singleton.myRaceInfo.winNum;
        int fuchang = StriveHegemongService.Singleton.myRaceInfo.failedNum;
        if (StriveHegemongService.Singleton.myRaceInfo.state == 1)
        {
            string miaoshu = "拳皇争霸已结束，您获得{0}胜{1}负，未能晋级八强（非）";
            UI_SH_WS_FenGeXian fenGeXian = UI_SH_WS_FenGeXian.CreateInstance();
            fenGeXian.m_miaoshu.text = string.Format(miaoshu, shengchang.ToString(), fuchang.ToString());
            window.m_SaiChengList.AddChildAt(fenGeXian, 0);
            //通知
            string tongzhi = "拳皇争霸第{}场比赛已结束，您遗憾退场（非）";
            window.m_tongzhi.text = string.Format(tongzhi, myRaceInfo.Count.ToString());
        }
    }
    private void OnBaQiangSai()
    {
        int shengchang = 0;
        int fuchang = 0;
        for (int i = 0; i < 10; ++i)
        {
            if (myRaceInfo[i].hasResult())
            {
                if (myRaceInfo[i].result == 0)
                    shengchang += 1;
                else
                    fuchang += 1;
            }
        }
        string miaoshu = "拳皇争霸已结束，您获得{0}胜{1}负，成功晋级八强（非）";

        UI_SH_WS_FenGeXian fenGeXian = UI_SH_WS_FenGeXian.CreateInstance();
        fenGeXian.m_miaoshu.text = string.Format(miaoshu, shengchang.ToString(), fuchang.ToString());
        window.m_SaiChengList.AddChildAt(fenGeXian, 0);

        SH_BaQiangListItem baQiangListItem;
        for (int i = 10; i < myRaceInfo.Count; ++i)
        {
            baQiangListItem = SH_BaQiangListItem.CreateInstance();
            baQiangListItem.Init(myRaceInfo[i], i);
            window.m_SaiChengList.AddChildAt(baQiangListItem,0);
        }
    }
    private void OnWeiKaiSaiFillData()
    {
        window.m_SaiChengList.RemoveChildren(0,-1,true);
        //战况和积分
        window.m_ZhanKuang.text = "暂无";
        window.m_JiFen.text = "暂无";
        string tongzhi = "比赛将在{0}准时开始，工作人员正在努力准备中(非语言包)";
        string kaishishijian = "20:00";
        t_globalBean bean = ConfigBean.GetBean<t_globalBean, int>(1702002);
        if (bean == null)
        { }
        else
        {
            string[] opentime = GTools.splitString(bean.t_string_param, ';');
            kaishishijian = GTools.splitString(opentime[0])[0];
        }
        window.m_tongzhi.text = string.Format(tongzhi, kaishishijian);

        SH_WS_ListItem listItem = SH_WS_ListItem.CreateInstance();
        listItem.Init(null);
        window.m_SaiChengList.AddChild(listItem);
        SH_DaoJiShi daoJiShi = SH_DaoJiShi.CreateInstance();
        daoJiShi.Init();
        window.m_SaiChengList.AddChild(daoJiShi);
    }
    //布阵窗口
    private void OnBuZhenBtn()
    {
        if (StriveHegemongService.Singleton.myRaceInfo.state == 2)
        {
            WinMgr.Singleton.Open<SH_DF_BuZhenWindow>(new WinInfo(),UILayer.Popup);
        }
        else
        {
            WinMgr.Singleton.Open<SH_WoDeSaiCheng_BuZhenWindow>(new WinInfo(),UILayer.Popup);
        }
    }
    private int OnComputeTime()
    {
        int downtime = 0;

        //计算时间
        DateTime dateTime = TimeUtils.currentServerDateTime();
        int currhour = dateTime.Hour;
        int currminute = dateTime.Minute;
        int currsecound = dateTime.Second;

        //得到开始时间
        t_globalBean bean = ConfigBean.GetBean<t_globalBean,int>(1702002);
        if (bean == null)
        {
            if (currhour < 20)
            {
                downtime = (20 - currhour) * 60 * 60;
                downtime += (60 - currminute) * 60;
                downtime += (60 - currsecound);
            }
            else
            {
                return -1;
            }
        }
        else
        {
            string[] opentime = GTools.splitString(bean.t_string_param,';');
            opentime = GTools.splitString(opentime[0]);
            opentime = GTools.splitString(opentime[0], ':');
            int open = int.Parse(opentime[0]);
            if (currhour < open)
            {
                downtime = (open - currhour) * 60 * 60;
                downtime += (60 - currminute) * 60;
                downtime += (60 - currsecound);
            }
            else
            {
                return -1;
            }
        }
        //返回的是秒数
        return downtime;
    }
    private void OnTongZhi()
    {
        int shengchang = StriveHegemongService.Singleton.myRaceInfo.winNum;
        int fuchang = StriveHegemongService.Singleton.myRaceInfo.failedNum;
        string zhankuang = "{0}胜{1}负";
        zhankuang = string.Format(zhankuang, shengchang.ToString(), fuchang.ToString());
        window.m_ZhanKuang.text = zhankuang;
        window.m_JiFen.text = StriveHegemongService.Singleton.myRaceInfo.core.ToString();
        t_languageBean languageBean;
        if (myRaceInfo.Count > 10)
        {
            
            int[] changci = { 71702217, 71702218, 71702219 };//四强赛以后语言包id
            languageBean = ConfigBean.GetBean<t_languageBean, int>(changci[myRaceInfo.Count - 10 - 1]);
        }
        else
        {
            int[] changci = { 71702207, 71702208, 71702209, 71702210, 71702211, 71702212, 71702213, 71702214, 71702215, 71702216 };//场次语言包id
            languageBean = ConfigBean.GetBean<t_languageBean, int>(changci[myRaceInfo.Count - 1]);
        }
        if (languageBean == null)
        {
            Logger.err("SH_BaQiangListItem:Init:语言包内没有对应的赛程语句---");
        }
        else
        {
            if (StriveHegemongService.Singleton.myRaceInfo.state != 0)
            {
                if (myRaceInfo[myRaceInfo.Count - 1].hasTime())
                {
                    if (myRaceInfo[myRaceInfo.Count - 1].time > 0)
                    {
                        string tongzhi = "{0}正在准备中，您将与{1}争夺晋级名额（非）";
                        if (myRaceInfo[myRaceInfo.Count - 1].hasName())
                        {
                            string mingzi = myRaceInfo[myRaceInfo.Count - 1].name;
                            string xianshi = string.Format(tongzhi, languageBean.t_content, mingzi);
                            window.m_tongzhi.text = xianshi;
                        }
                        else
                            window.m_tongzhi.text = "本轮空，您将直接晋级（非）";
                    }
                    else
                    {
                        string tongzhi = "{0}正在进行中，您将与{1}争夺晋级名额（非）";
                        if (myRaceInfo[myRaceInfo.Count - 1].hasName())
                        {
                            string mingzi = myRaceInfo[myRaceInfo.Count - 1].name;
                            string xianshi = string.Format(tongzhi, languageBean.t_content, mingzi);
                            window.m_tongzhi.text = xianshi;
                        }
                        else
                            window.m_tongzhi.text = "本轮空，您将直接晋级（非）";
                    }
                }
                else
                {
                    if (myRaceInfo.Count > 10)
                    {

                        int[] changci = { 71702217, 71702218, 71702219 };//四强赛以后语言包id
                        languageBean = ConfigBean.GetBean<t_languageBean, int>(changci[myRaceInfo.Count - 10 - 1]);
                    }
                    else
                    {
                        int[] changci = { 71702207, 71702208, 71702209, 71702210, 71702211, 71702212, 71702213, 71702214, 71702215, 71702216 };//场次语言包id
                        languageBean = ConfigBean.GetBean<t_languageBean, int>(changci[myRaceInfo.Count - 1]);
                    }
                    if (languageBean == null)
                    {
                        Logger.err("SH_BaQiangListItem:Init:语言包内没有对应的赛程语句---");
                    }
                    else
                    {
                        string tongzhi = "{0}正在匹配中，请耐心等待";
                        window.m_tongzhi.text = string.Format(tongzhi,languageBean.t_content);
                    }
                }
                
            }
            else
            {
                if (myRaceInfo.Count == 13)
                {
                    if (myRaceInfo[12].result == 0)
                    {
                        window.m_tongzhi.text = "恭喜获得拳皇争霸冠军";
                    }
                    else
                    {
                        //未获得冠军
                        string tongzhi = "很遗憾你止步{0},获得拳皇争霸第名";
                        window.m_tongzhi.text = string.Format(tongzhi, languageBean.t_content);
                    }
                }
            }
        }
    }

    public void Close()
    {
        GED.ED.removeListener(EventID.OnStriveHegemongOpenBox,OnOpenBox);
        GED.ED.removeListener(EventID.OnstriveHegemongMySelfInfo, OnYiKaiZhanShuaXin);
        window = null;
        if (doAction != null)
        {
            doAction.kill();
            doAction = null;
        }
    }
}
