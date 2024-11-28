using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_GuildBoss;
using Data.Beans;

public class PassRewardItem : UI_passRewardItem {

    public int bossID;

    public new static PassRewardItem CreateInstance()
    {
        return UI_passRewardItem.CreateInstance() as PassRewardItem;
    }

    public void InitView()
    {
        t_guild_bossBean guildBossBean = ConfigBean.GetBean<t_guild_bossBean, int>(bossID);
        if (guildBossBean != null)
        {
            t_monster_boosBean petBean = ConfigBean.GetBean<t_monster_boosBean, int>(guildBossBean.t_pet);
            if(petBean != null)
            {
                m_bossNameLabel.text = petBean.t_name;
                //UIGloader.SetUrl(m_bossIconLoader, UIUtils.GetPetStartIcon(guildBossBean.t_pet, GuildBossService.Singleton.guildBossDefaultStar));
            }

            // 奖励列表
            if (!string.IsNullOrEmpty(guildBossBean.t_first_award))
            {
                string[] awardsArr = guildBossBean.t_first_award.Split(';');
                int count = awardsArr.Length;
                CommonItem item = null;
                string[] itemInfo = null;
                for (int i = 0; i < count; i++)
                {
                    itemInfo = awardsArr[i].Split('+');
                    if (itemInfo.Length == 2)
                    {
                        item = CommonItem.CreateInstance();
                        item.itemId = int.Parse(itemInfo[0]);
                        item.itemNum = int.Parse(itemInfo[1]);
                        item.isShowNum = true;
                        item.RefreshView();
                        item.scale = new Vector2(0.6f, 0.6f);

                        m_rewardList.AddChild(item);
                    }
                }
                if (item != null)
                    m_rewardList.columnCount = -(int)(item.width * 0.4f) + 20;
            }
        }
    }
}
