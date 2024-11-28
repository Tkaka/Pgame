using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_CloneTeamFight;
using Message.Challenge;
using UI_Common;

public class CloneTeammateItem : UI_cloneTeammateItem {

    public TeamFightRoleInfo teamRoleInfo;

    public new static CloneTeammateItem CreateInstance()
    {
        return UI_cloneTeammateItem.CreateInstance() as CloneTeammateItem;
    }

    public void InitView()
    {
        m_toucher.onClick.Add(OnClickItem);
        RefreshView();
    }

    public void RefreshView()
    {
        RefreshBaseInfo();
    }

    private void RefreshBaseInfo()
    {
        if (teamRoleInfo != null)
        {
            m_teammateGroup.visible = true;

            StarList starList = new StarList((UI_StarList)m_starList);
            starList.SetStar(teamRoleInfo.star);

            UIGloader.SetUrl(m_iconLoader,UIUtils.GetPetStartIcon(teamRoleInfo.petId));
            UIGloader.SetUrl(m_boardLoader, UIUtils.GetLoaderUrl(WinEnum.UI_Common, UIUtils.GetBorderByQuality(teamRoleInfo.color)));

            m_lvLabel.text = teamRoleInfo.level + "";
            m_nameLabel.text = teamRoleInfo.roleName + "";

            m_captainIcon.visible = IsCaptain();
            m_switchIcon.visible = IsShowChangeIcon();

            int languageID = 0;
            Color color = Color.white;
            if (teamRoleInfo.count == 0)
            {
                languageID = 71803003;
                color = Color.red;
            }
            else if(teamRoleInfo.count < CloneTeamFightService.Singleton.cloneMaxTimes)
            {
                languageID = 71803005;
                color = Color.green;
            }
            else
            {
                languageID = 71803004;
                color = Color.green;
            }
            m_progressLabel.text = string.Format(UIUtils.GetStrByLanguageID(languageID), teamRoleInfo.count);
            m_progressLabel.color = color;
        }
        else
        {
            m_teammateGroup.visible = false;
            bool playerIsCaptain = CloneTeamFightService.Singleton.PlayerIsCaptain();
            if (playerIsCaptain)
            {
                m_addIcon.visible = true;
                m_emptyIcon.visible = false;
            }
            else
            {
                m_addIcon.visible = false;
                m_emptyIcon.visible = true;
            }
        }
    }

    private void OnClickItem()
    {
        if (teamRoleInfo != null)
        {
            if (IsSelf())
            {
                if (teamRoleInfo.count < CloneTeamFightService.Singleton.cloneMaxTimes)
                {
                    // 打开切换宠物界面
                    TwoParam<int, ShangZhenSelectType> param = new TwoParam<int, ShangZhenSelectType>();
                    param.value1 = teamRoleInfo.petId;
                    param.value2 = ShangZhenSelectType.CloneChangePet;
                    WinMgr.Singleton.Open<ShangZhenWindow>(WinInfo.Create(false, null, false, param), UILayer.Popup);
                }
            }
            else
            {
                // TODO： 打开其他玩家详情界面
            }
        }
        else
        {
            bool isCaptain = CloneTeamFightService.Singleton.PlayerIsCaptain();
            if (isCaptain)
            {
                // 邀请好友, 打开邀请好友界面
                WinMgr.Singleton.Open<CloneInviteFriendWindow>(null, UILayer.Popup);
            }
        }
    }

    #region 数据处理 *******************************************************************************************
    /// <summary>
    /// 是否是队长
    /// </summary>
    /// <returns></returns>
    private bool IsCaptain()
    {
        ResTeamFightTeamInfo teamFightInfo = CloneTeamFightService.Singleton.fightTeamInfo;
        if (teamRoleInfo != null && teamRoleInfo != null)
        {
            return teamRoleInfo.roleId == teamFightInfo.roleId;
        }

        return false;
    }
    /// <summary>
    /// 是否是玩家本身
    /// </summary>
    /// <returns></returns>
    private bool IsSelf()
    {
        if (teamRoleInfo != null)
        {
            Message.Role.RoleInfo roleInfo = RoleService.Singleton.GetRoleInfo();
            return roleInfo.roleId == teamRoleInfo.roleId;
        }

        return false;
    }
    /// <summary>
    /// 是否显示切换的图标
    /// </summary>
    /// <returns></returns>
    private bool IsShowChangeIcon()
    {
        return teamRoleInfo.count < CloneTeamFightService.Singleton.cloneMaxTimes && IsSelf();
    }
    #endregion;

    public override void Dispose()
    {
        teamRoleInfo = null;

        base.Dispose();
    }
}
