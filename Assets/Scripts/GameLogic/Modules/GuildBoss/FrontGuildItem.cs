using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_GuildBoss;
using Message.GuildBoss;

public class FrontGuildItem : UI_frontGuildItem {

    public StringVsLong itemInfo;

    public new static FrontGuildItem CreateInstance()
    {
        return UI_frontGuildItem.CreateInstance() as FrontGuildItem;
    }

    public void InitView()
    {
        m_guildName.text = itemInfo.str;

        m_perfectTip.visible = itemInfo.num <= 0;
        m_progressValue.visible = itemInfo.num > 0;
        float blood = GuildBossService.Singleton.MAX_PROGRESS - itemInfo.num * 0.01f;
        m_progressValue.text = string.Format("{0}%", blood);
        m_bloodProgress.value = GuildBossService.Singleton.MAX_PROGRESS - itemInfo.num;
        m_bloodProgress.max = GuildBossService.Singleton.MAX_PROGRESS;
    }
}
