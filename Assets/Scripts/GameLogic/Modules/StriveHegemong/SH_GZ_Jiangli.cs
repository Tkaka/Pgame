using Data.Beans;
using UI_StriveHegemong;

public class SH_GZ_Jiangli : UI_SH_GZ_Jiangli
{
    public new static SH_GZ_Jiangli CreateInstance()
    {
        return (SH_GZ_Jiangli)UI_SH_GZ_Jiangli.CreateInstance();
    }
    public void Init(t_rank_rewardBean rank)
    {
        if (rank.t_begin == rank.t_end)
        {
            m_fanwei.text = "第" + rank.t_begin + "名";
        }
        else
        {
            m_fanwei.text = "第" + rank.t_begin + "-" + rank.t_end + "名";
        }
        OnJiangPin(rank.t_reward);
    }
    private void OnJiangPin(string jiangpin)
    {
        int[,] jiangli = UIUtils.splitStringTotwodimensionArry(jiangpin);
        SH_DH_DaoJuIcon daoju;
        int lenght = jiangli.GetUpperBound(0) + 1;
        int h = jiangli.GetUpperBound(1) + 1;
        for (int i = 0; i < lenght; ++i)
        {
            daoju = SH_DH_DaoJuIcon.CreateInstance();
            daoju.Init(jiangli[i,0],jiangli[i,1]);
            m_jingpin.AddChild(daoju);
        }
    }
}
