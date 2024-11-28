using System.Collections;
using System.Collections.Generic;
using UI_GuildBoss;
using Data.Beans;
using FairyGUI;

//  1公会竞技奖励  2公会排名奖励   3单个boss伤害排名奖励   4总伤害排名奖励
public enum RewardRankType
{
    GuildJinJiRank = 1,
    GuildPassRank = 2,
    SingleDamgeRank = 3,
    ToatalDmageRank = 4,
}

public class GuildBossRewradWindow : BaseWindow {

    UI_GuildBossRewradWindow window;
    RewardRankType type;

    private int jinJiRewardNum;
    private int otherRewardNum;
    private int tipIndex;
    public override void OnOpen()
    {
        base.OnOpen();

        window = getUiWindow<UI_GuildBossRewradWindow>();

        InitView();
    }

    public override void InitView()
    {
        base.InitView();
        window.m_closeBtn.onClick.Add(OnCloseBtn);
        window.m_mask.onClick.Add(OnCloseBtn);

        window.m_contentList.itemProvider = ItemProvider;
        window.m_contentList.itemRenderer = RenderListItem;
        window.m_contentList.SetVirtual();

        List<t_guild_boss_rankBean> guildBossRankList = ConfigBean.GetBeanList<t_guild_boss_rankBean>();
        jinJiRewardNum = 2;
        otherRewardNum = guildBossRankList.Count;
        int itemCount = jinJiRewardNum + 4 + otherRewardNum * 3;
        window.m_contentList.numItems = itemCount;

    }

    private void RenderListItem(int index, GObject obj)
    {
        if (index == tipIndex)
        {
            GuildBossRankTip item = obj as GuildBossRankTip;
            item.type = type;
            item.RefreshView();
        }
        else
        {
            GuildBossRewardItem item = obj as GuildBossRewardItem;
            if (type == RewardRankType.GuildJinJiRank)
            {
                // 1603001 胜利  1603002 失败
                t_globalBean globalBean = null;
                if (index - tipIndex == 1)
                    globalBean = ConfigBean.GetBean<t_globalBean, int>(1603001);
                else
                    globalBean = ConfigBean.GetBean<t_globalBean, int>(1603002);

                if (globalBean != null)
                {
                    item.itemInfo = globalBean.t_string_param;
                    item.index = index - tipIndex;
                    item.InitView();
                }
                return;
            }

            int guildBossRankID = index - tipIndex;
            t_guild_boss_rankBean guildBossRankBean = ConfigBean.GetBean<t_guild_boss_rankBean, int>(guildBossRankID);
            if (guildBossRankBean != null)
            {
                switch (type)
                {
                    case RewardRankType.GuildPassRank:
                        item.itemInfo = guildBossRankBean.t_guild_rank;
                        break;
                    case RewardRankType.SingleDamgeRank:
                        item.itemInfo = guildBossRankBean.t_single_damage;
                        break;
                    case RewardRankType.ToatalDmageRank:
                        item.itemInfo = guildBossRankBean.t_month_damage;
                        break;
                    default:
                        break;
                }
            }
            item.index = guildBossRankID;
            item.InitView();
        }
    }

    private string ItemProvider(int index)
    {
        // 公会竞技奖励
        tipIndex = 0;
        int limitIndex = 1 + jinJiRewardNum;
        if (index < limitIndex)
        {
            type = RewardRankType.GuildJinJiRank;
            if (index == tipIndex)
                return GuildBossRankTip.URL;
            else
                return GuildBossRewardItem.URL;
        }
        else
        {
            tipIndex = limitIndex;
            limitIndex += 1 + otherRewardNum;
        }
        // 公会排名奖励
        if (index < limitIndex)
        {
            type = RewardRankType.GuildPassRank;
            if (index == tipIndex)
                return GuildBossRankTip.URL;
            else
                return GuildBossRewardItem.URL;
        }
        else
        {
            tipIndex = limitIndex;
            limitIndex += 1 + otherRewardNum;
        }
        // 单个boss伤害排名奖励
        if (index < limitIndex)
        {
            type = RewardRankType.SingleDamgeRank;
            if (index == tipIndex)
                return GuildBossRankTip.URL;
            else
                return GuildBossRewardItem.URL;
        }
        else
        {
            tipIndex = limitIndex;
            limitIndex += 1 + otherRewardNum;
        }
        // 总伤害排名奖励
        if (index < limitIndex)
        {
            type = RewardRankType.ToatalDmageRank;
            if (index == tipIndex)
                return GuildBossRankTip.URL;
            else
                return GuildBossRewardItem.URL;
        }
        return null;
    }

    protected override void OnCloseBtn()
    {
        base.OnCloseBtn();
    }
}
