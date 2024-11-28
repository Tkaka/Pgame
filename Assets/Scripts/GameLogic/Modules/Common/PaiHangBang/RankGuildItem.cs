using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_Common;
using Message.Rank;

public class RankGuildItem : UI_rankGuildItem {

    public GuildRankData rankData;
    public RankType rankType;

    public new static RankGuildItem CreateInstance()
    {
        return UI_rankGuildItem.CreateInstance() as RankGuildItem;
    }

    public void InitView()
    {
        m_toucher.onClick.Add(ClickItem);
        RefreshView();
    }

    public void RefreshView()
    {
        if (IsJoinGuild())
        {
            m_selfIcon.visible = IsSelfGuild();
            m_SheTuan.visible = true;

            // TODO ： 排行前三使用图片
            m_rankIcon.visible = false;
            m_rankNum.visible = true;
            m_rankNum.text = rankData.rank + "";

            m_guildLevel.text = string.Format("等级{0}", rankData.level);
            m_guildName.text = rankData.name;

            // TODO ： 加载显示公会的徽章类型
            m_sheZhangName.text = rankData.chairManName;
            m_totalFightPower.text = rankData.fightPower + "";
        }
        else
        {
            m_SheTuan.visible = false;
            m_selfIcon.visible = false;
            m_unJoinTip.visible = true;
        }
        
    }
    /// <summary>
    /// 是否是自己的公会
    /// </summary>
    /// <returns></returns>
    private bool IsSelfGuild()
    {
        Message.Role.RoleInfo roleInfo = RoleService.Singleton.GetRoleInfo();
        return rankData.name.Equals(roleInfo.guildName);
    }
    /// <summary>
    /// 是否加入了公会
    /// </summary>
    /// <returns></returns>
    private bool IsJoinGuild()
    {
        Message.Role.RoleInfo roleInfo = RoleService.Singleton.GetRoleInfo();
        return roleInfo.guildId > 0;
    }

    private void ClickItem()
    {
        // 显示社团详情
    }
}
