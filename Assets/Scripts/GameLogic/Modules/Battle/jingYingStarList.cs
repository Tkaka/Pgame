using Data.Beans;
using Message.Bag;
using Message.Dungeon;
using UI_Battle;
using Message.Fight;


public class JingYingStarList : UI_jingYingStarList
{
    public void OnOpen()
    {
        _ShowStars();
    }

    private void _ShowStars()
    {
        bool isCmp = true;
        bool isComboCmp = BattleStatistics.Singleton.IsComboCmp();
        bool isAliveCmp = BattleStatistics.Singleton.IsAliveCmp();
        if (!isCmp)
            m_cmpStar.grayed = true;
        else
            m_cmpStar.grayed = false;

        if (!isComboCmp)
            m_comboStar.grayed = true;
        else
            m_comboStar.grayed = false;

        if (!isAliveCmp)
            m_aliveStar.grayed = true;
        else
            m_aliveStar.grayed = false;

        t_languageBean bean = ConfigBean.GetBean<t_languageBean, int>(30000);
        if (bean != null)
            m_cmpDesc.text = bean.t_content;
        bean = ConfigBean.GetBean<t_languageBean, int>(30006);
        if (bean != null)
            m_aliveDesc.text = bean.t_content;
        _SetStarTxt();
    }

    private void _SetStarTxt()
    {
        t_dungeon_actBean bean = ConfigBean.GetBean<t_dungeon_actBean, int>(BattleService.Singleton.MissionId);
        t_languageBean lanbean = null;
        if (bean != null && !string.IsNullOrEmpty(bean.t_star_hit))
        {
            int[] arr = GTools.splitStringToIntArray(bean.t_star_hit);
            if (arr != null && arr.Length >= 2)
            {
                switch (arr[0])
                {
                    case (int)ComboType.Normal:
                        lanbean = ConfigBean.GetBean<t_languageBean, int>(30002);
                        break;
                    case (int)ComboType.NotBad:
                        lanbean = ConfigBean.GetBean<t_languageBean, int>(30003);
                        break;
                    case (int)ComboType.Good:
                        lanbean = ConfigBean.GetBean<t_languageBean, int>(30004);
                        break;
                    case (int)ComboType.Perfect:
                        lanbean = ConfigBean.GetBean<t_languageBean, int>(30005);
                        break;
                    default:
                        break;
                }
            }
            if (lanbean != null)
            {
                if (arr[1] >= 10000)
                {
                    //个数
                    var lb = ConfigBean.GetBean<t_languageBean, int>(30009);
                    if (lb != null)
                        m_comboDesc.text = string.Format(lb.t_content, lanbean.t_content, arr[1] / 10000);
                }
                else
                {
                    //百分比
                    var lb = ConfigBean.GetBean<t_languageBean, int>(30001);
                    if (lb != null)
                        m_comboDesc.text = string.Format(lb.t_content, lanbean.t_content, arr[1] / 100);
                }

            }
        }
    }

    public void OnClose()
    {

    }
}
