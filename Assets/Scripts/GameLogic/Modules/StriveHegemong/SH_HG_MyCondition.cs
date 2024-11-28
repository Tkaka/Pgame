using Data.Beans;
using Message.KingFight;
using System.Collections.Generic;
using UI_StriveHegemong;

public class SH_HG_MyCondition : UI_SH_HG_MyCondition
{
    public new static SH_HG_MyCondition CreateInstance()
    {
        return (SH_HG_MyCondition)UI_SH_HG_MyCondition.CreateInstance();
    }
    public SH_HG_MyCondition()
    {
        GED.ED.addListener(EventID.OnStriveHegemongHuiGu, OnWoDeHuiGu);
    }
    public void Init()
    {
        //判断昨日是否参赛
        if (StriveHegemongService.Singleton.Yesterday == null || StriveHegemongService.Singleton.Yesterday.myinfos.Count == 0)
        {
            OnFillData();
        }
        else
        {
            OffFillData();
            FillList();
        }
    }
    //填写文本数据未参赛
    private void OnFillData()
    {
        m_zhankuang.text = "0胜0负";
        m_jifen.text = 0 + "";
        m_paiming.text = 0 + "";
    }
    //填写文本数据，昨日参赛
    private void OffFillData()
    {
        string zhankuang = "{0}胜{1}负";
        m_paiming.text = StriveHegemongService.Singleton.Yesterday.rank.ToString();
        m_jifen.text = StriveHegemongService.Singleton.Yesterday.core.ToString();
        int sheng = StriveHegemongService.Singleton.Yesterday.winNum;
        int fu = StriveHegemongService.Singleton.Yesterday.failedNum;
        m_zhankuang.text = string.Format(zhankuang,sheng.ToString(),fu.ToString());
        t_themeBean themeBean = ConfigBean.GetBean<t_themeBean, int>(StriveHegemongService.Singleton.maininfo.playId);
        if (themeBean == null)
        {
            Logger.err("SH_WoDeSaiCheng:OnWeiKaiSaiFillData:未从玩法表获得对应数据，请检查玩法id是否正确---" + StriveHegemongService.Singleton.maininfo.playId);
        }
        else
        {
            string zhuti;
            zhuti = themeBean.t_topic + ":";
            if (themeBean.t_type != 0)
            {
                zhuti += string.Format(themeBean.t_desc, (themeBean.t_desc_number).ToString());
            }
            else
                zhuti += themeBean.t_desc;
            m_zhuti.text = zhuti;
        }
    }
    //填写列表数据
    private void FillList()
    {
        List<FightInfo> myRaceInfo = StriveHegemongService.Singleton.Yesterday.myinfos;
        m_WoDeZhanKuang.RemoveChildren(0, -1, true);

        SH_WS_ListItem listItem;
        for (int i = 0; (i < myRaceInfo.Count && i < 10); ++i)
        {
            listItem = SH_WS_ListItem.CreateInstance();
            listItem.Init(myRaceInfo[i], i);
            m_WoDeZhanKuang.AddChildAt(listItem, 0);
        }
        SH_BaQiangListItem baQiangListItem;
        for (int i = 10; i < myRaceInfo.Count; ++i)
        {
            baQiangListItem = SH_BaQiangListItem.CreateInstance();
            baQiangListItem.Init(myRaceInfo[i], i);
            m_WoDeZhanKuang.AddChildAt(baQiangListItem, 0);
        }
    }
    private void OnWoDeHuiGu(GameEvent evt)
    {
        Init();
    }
    public override void Dispose()
    {
        GED.ED.removeListener(EventID.OnstriveHegemongMySelfInfo, OnWoDeHuiGu);
        base.Dispose();
    }
    
}
