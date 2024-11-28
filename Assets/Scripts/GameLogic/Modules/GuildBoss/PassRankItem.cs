using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_GuildBoss;
using Message.GuildBoss;
using System;
using Data.Beans;

public class PassRankItem : UI_passRankItem {

    public GuildRankItem rankInfo;

    public new static PassRankItem CreateInstance()
    {
        return UI_passRankItem.CreateInstance() as PassRankItem;
    }

    public void RefreshView()
    {
        t_iconBean iconBean = ConfigBean.GetBean<t_iconBean, int>(rankInfo.icon);
        if (iconBean != null)
        {
            UIGloader.SetUrl(m_unionIconLoader, iconBean.t_icon);
        }
       
        m_unionNameLabel.text = rankInfo.name;
        DateTime completeTime = TimeUtils.javaTimeToCSharpTime(rankInfo.completeTime);
        m_passTimeLabel.text = completeTime.ToString("yyyy-MM-dd HH:mm");
        // 刷新排名
        if (rankInfo.rank < 3)
        {
            m_rankLoader.visible = true;
            m_rankLabel.visible = false;
            string rankIcon = "";
            switch (rankInfo.rank)
            {
                case 0:
                    rankIcon = "first";
                    break;
                case 1:
                    rankIcon = "second";
                    break;
                case 2:
                    rankIcon = "third";
                    break;
                default:
                    break;
            }
            UIGloader.SetUrl(m_rankLoader, UIUtils.GetLoaderUrl(WinEnum.UI_GuildBoss, rankIcon));
        }
        else
        {
            m_rankLabel.visible = true;
            m_rankLoader.visible = false;

            m_rankLabel.text = rankInfo.rank + 1 + "";
        }
        

    }
}
