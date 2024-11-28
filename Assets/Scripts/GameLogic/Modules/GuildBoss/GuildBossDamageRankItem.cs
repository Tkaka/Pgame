using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_GuildBoss;
using Message.GuildBoss;

public class GuildBossDamageRankItem : UI_guildBossDamageRankItem {

    public DamageRankItem itemInfo;
    public int index;

    public new static GuildBossDamageRankItem CreateInstance()
    {
        return UI_guildBossDamageRankItem.CreateInstance() as GuildBossDamageRankItem;
    }

    public void RefreshView()
    {
        m_nameLabel.text = itemInfo.name;
        m_rank.text = itemInfo.rank + 1 + "";
        m_damageLabel.text = itemInfo.damage + "";

        m_bg.visible = index % 2 == 0;
    }
}
