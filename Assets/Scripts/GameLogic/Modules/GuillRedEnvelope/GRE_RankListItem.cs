using UI_GuillRedEnvelope;
using Message.Guild;
using Data.Beans;
public class GRE_RankListItem : UI_GRE_RankListItem
{
    public new static GRE_RankListItem CreateInstance()
    {
        return (GRE_RankListItem)UI_GRE_RankListItem.CreateInstance();
    }
    
    public void Init(int index,HongbaoRole hongbaoRole)
    {
        OnPaiming(index);
        m_name.text = hongbaoRole.name;
        t_headBean headBean = ConfigBean.GetBean<t_headBean,int>(hongbaoRole.icon);
        UIGloader.SetUrl(m_touxiang_icon, headBean.t_icon);
        m_de_shuliang.text = hongbaoRole.num.ToString();
    }
    //发红包排行榜初始化
    public void FaInit(HongbaoRankRole rankRole)
    {
        OnPaiming(rankRole.rank - 1);
        m_name.text = rankRole.name;
        t_headBean headBean = ConfigBean.GetBean<t_headBean, int>(rankRole.icon);
        UIGloader.SetUrl(m_touxiang_icon, headBean.t_icon);
        m_Fa_zongliang.text = rankRole.value.ToString();
        m_geshu.text = rankRole.num.ToString();
    }
    //排名设置
    private void  OnPaiming(int rank)
    {
        if (rank < 3)
        {
            string my_name = "";
            switch (rank)
            {
                case 0: my_name = "ui://" + WinEnum.UI_Top + "/diyiming"; break;
                case 1: my_name = "ui://" + WinEnum.UI_Top + "/dierming"; break;
                case 2: my_name = "ui://" + WinEnum.UI_Top + "/disanming"; break;
                default: break;
            }
            UIGloader.SetUrl(m_paiming_icon, my_name);
            m_paiming_number.visible = false;
        }
        else
        {
            m_paiming_icon.visible = false;
            m_paiming_number.text = (rank + 1) + "";
        }
    }
}
