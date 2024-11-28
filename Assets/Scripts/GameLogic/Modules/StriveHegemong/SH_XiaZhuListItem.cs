using Message.KingFight;
using System;
using UI_StriveHegemong;

public class SH_XiaZhuListItem : UI_SH_XZ_XiaZhuListItem
{
    public BetInfo info;
    public new static SH_XiaZhuListItem CreateInstance()
    {
        return (SH_XiaZhuListItem)UI_SH_XZ_XiaZhuListItem.CreateInstance();
    }
    public void Init(BetInfo petid,bool xiazhu)
    {
        info = petid;
        AddKeyEvent();
        FillData();
        if (!xiazhu)
        {
            m_GaoJiXiaZhu.grayed = true;
            m_PuTongXiaZhu.grayed = true;
        }
    }
    private void FillData()
    {
        UIGloader.SetUrl(m_TouXiang,"");
        UIGloader.SetUrl(m_beijing,"");
        m_putongbenjin.text = "10万（）";
        m_gaojibenjin.text = "20万（）";
        m_name.text = info.name;
        if (info.odds == -1)
        {
            m_PeiLv.text = "无人下注（非）";
        }
        else
            m_PeiLv.text = info.odds.ToString();
        m_dengji.text = info.level.ToString();
        m_paiming.text = info.rank.ToString();
        if (StriveHegemongService.Singleton.betInfo.hasRoleId())
        {
            m_WiXiaZhu.visible = false;
            if (info.roleId == StriveHegemongService.Singleton.betInfo.roleId)
            {
                if (StriveHegemongService.Singleton.betInfo.type == 0)
                    m_xiazhujine.text = "10万（）";
                else if (StriveHegemongService.Singleton.betInfo.type == 1)
                    m_xiazhujine.text = "20万（）";
            }
            else
            {
                m_YiXiaZhu.visible = false;
            }
        }
        else
        {
            m_YiXiaZhu.visible = false;
        }
    }
    private void AddKeyEvent()
    {
        m_PuTongXiaZhu.onClick.Add(OnPuTongXiaZhu);
        m_GaoJiXiaZhu.onClick.Add(OnGaoJiXiaZhu);
    }
    /// <summary>
    /// 下完注后看服务器回不回消息，回的话就不管
    /// 不回的话就分发游戏内消息告诉下注窗口界面这边下的是什么注
    /// </summary>
    private void OnPuTongXiaZhu()
    {
        StriveHegemongService.Singleton.OnReqBet(0,info.roleId);
        GED.ED.dispatchEvent(EventID.OnStriveHegemongXiaZhu);
    }
    private void OnGaoJiXiaZhu()
    {
        StriveHegemongService.Singleton.OnReqBet(1,info.roleId);
        GED.ED.dispatchEvent(EventID.OnStriveHegemongXiaZhu);
    }
    public override void Dispose()
    {
        base.Dispose();
    }
}
