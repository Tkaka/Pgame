using Data.Beans;
using Message.Bag;
using Message.Dungeon;
using UI_Battle;
using Message.Fight;


public class NomalStarList : UI_nomalStarList
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

        m_cmpStar.grayed = !isCmp;
        m_cmpGrayKuang.visible = !isCmp;
        m_cmdLightKuang.visible = isCmp;

        m_pingFenStar.grayed = !isComboCmp;
        m_pingFengGrayKuang.visible = !isComboCmp;
        m_pingfengLightKuang.visible = isComboCmp;

        m_aliveStar.grayed = !isAliveCmp;
        m_aliveGrayKuang.visible = !isAliveCmp;
        m_aliveLightKuang.visible = isAliveCmp;

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
