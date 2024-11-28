using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_GuildBoss;

public class GuildBossRankTip : UI_guildBossRankTip {

    public RewardRankType type;

    public new static GuildBossRankTip CreateInstance()
    {
        return UI_guildBossRankTip.CreateInstance() as GuildBossRankTip;
    }

    public void RefreshView()
    {
        string str = null;
        switch (type)
        {
            case RewardRankType.GuildJinJiRank:
                str = "公会竞技奖励";
                break;
            case RewardRankType.GuildPassRank:
                str = "公会排名奖励";
                break;
            case RewardRankType.SingleDamgeRank:
                str = "单个boss伤害排名奖励";
                break;
            case RewardRankType.ToatalDmageRank:
                str = "总伤害排名奖励";
                break;
            default:
                break;
        }
        m_tip.text = str;
    }
}
