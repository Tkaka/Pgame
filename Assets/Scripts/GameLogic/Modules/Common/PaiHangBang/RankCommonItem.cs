using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_Common;
using Message.Rank;
using Data.Beans;

public class RankCommonItem : UI_rankCommonItem {

    public RankData rankData;
    public RankType rankType;

    public new static RankCommonItem CreateInstance()
    {
        return UI_rankCommonItem.CreateInstance() as RankCommonItem;
    }

    public void InitView()
    {
        m_toucher.onClick.Add(OnClickItem);
        m_ChongWuXiangQingBtn.onClick.Add(OnClickPetItem);
        RefreshView();
    }

    public void RefreshView()
    {
        if (IsEnterRankPanel())
        {
            m_ziji.visible = IsSelf();
            m_YiShangBang.visible = true;
            m_WiShangBang.visible = false;

            // TODO： 前3名使用图片
            m_paiming_Icon.visible = false;
            m_paiming_number.text = rankData.rank + 1 + "";

            m_name.text = rankData.name;
            // 称号
            Message.Achievement.ResAchievementInfo achievementInfo = AchievementService.Singleton.achievementinfo;
            if (achievementInfo != null)
            {
                int achievementID = achievementInfo.title;
                t_achievementBean achievementBean = ConfigBean.GetBean<t_achievementBean, int>(achievementID);
                if (achievementBean != null)
                    m_chenghao.text = achievementBean.t_name;
            }

            if (rankType == RankType.GeDouJia)
            {
                m_Pet.visible = true;
                int petID = rankData.left;
                UI_petItem petItem = m_Rankpet as UI_petItem;
                UIGloader.SetUrl(petItem.m_borderBg, UIUtils.GetBorderByQuality(rankData.color));
                UIGloader.SetUrl(petItem.m_iconLoader, UIUtils.GetPetStartIcon(petID, rankData.star));

                StarList starList = new StarList(petItem.m_starList);
                starList.SetStar(rankData.star);
            }
            else
            {
                m_left.text = rankData.left + "";
            }

            m_right.text = rankData.right + "";
        }
        else
        {
            m_YiShangBang.visible = false;
            m_WiShangBang.visible = true;
            m_ziji.visible = false;
        }
    }

    /// <summary>
    /// 是否是自己
    /// </summary>
    /// <returns></returns>
    private bool IsSelf()
    {
        Message.Role.RoleInfo roleInfo = RoleService.Singleton.GetRoleInfo();
        return roleInfo.roleId == rankData.roleId;
    }
    /// <summary>
    /// 是否上榜
    /// </summary>
    /// <returns></returns>
    private bool IsEnterRankPanel()
    {
        return rankData.rank != -1;
    }
    

    private void OnClickItem()
    {

    }

    private void OnClickPetItem()
    {

    }

}
