using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_GuildBoss;
using Message.GuildBoss;
using Data.Beans;

public class GuildBossDistributeItem : UI_guildBossDistributeItem {

    public AllotItem info;

    public new static GuildBossDistributeItem CreateInstance()
    {
        return UI_guildBossDistributeItem.CreateInstance() as GuildBossDistributeItem;
    }

    public void RefreshView()
    {
        UIGloader.SetUrl(m_iconLoader, UIUtils.GetHeadIcon(info.icon));

        m_nameLabel.text = info.name;

        t_guild_bossBean guildBossBean = ConfigBean.GetBean<t_guild_bossBean, int>(info.bossId);
        if (guildBossBean != null)
        {
            t_petBean petBean = ConfigBean.GetBean<t_petBean, int>(guildBossBean.t_pet);
            if(petBean != null)
            {
                m_bossName.text = UIUtils.GetPetName(petBean, GuildBossService.Singleton.guildBossDefaultStar);
            }
        }

        CommonItem item = m_rewardItem as CommonItem;
        item.itemId = info.itemId;
        item.itemNum = info.itemNum;
        item.isShowNum = true;
        item.RefreshView();
    }
}
