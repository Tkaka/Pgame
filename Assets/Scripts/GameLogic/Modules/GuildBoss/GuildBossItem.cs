using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_GuildBoss;
using Message.GuildBoss;
using Data.Beans;
using DG.Tweening;

public class GuildBossItem : UI_guildBossItem {

    public int bossID;
    GuildBossMianWindow parentWindow;
    Tween tween;
    public new static GuildBossItem CreateInstance()
    {
        return UI_guildBossItem.CreateInstance() as GuildBossItem;
    }

    public void InitView(GuildBossMianWindow parentWindow)
    {
        this.parentWindow = parentWindow;
        m_bossRankBtn.onClick.Add(OnBossRankBtnClick);
        m_toucher.onClick.Add(OnClickItem);
        RefreshView();
    }

    public void RefreshView()
    {
        RefreshBaseInfo();
        RefreshPostion();
    }
    private void RefreshBaseInfo()
    {
        if (IsOpen())
        {
            m_openGroup.visible = true;
            m_unOpenIcon.visible = false;

            t_guild_bossBean guildBossBean = ConfigBean.GetBean<t_guild_bossBean, int>(bossID);
            if (guildBossBean != null)
            {
                t_monster_boosBean petBean = ConfigBean.GetBean<t_monster_boosBean, int>(guildBossBean.t_pet);
                if (petBean != null)
                {
                    m_bossNameLabel.text = petBean.t_name;
                    //UIGloader.SetUrl(m_iconLoader, UIUtils.GetPetStartIcon(guildBossBean.t_pet, GuildBossService.Singleton.guildBossDefaultStar));
                }
            }

            m_numLabel.text = bossID + "";
            m_bubbleGroup.alpha = 0;
            m_bubbleGroup.scale = Vector2.zero;

            ResGuildDungeonInfo guildBossInfo = GuildBossService.Singleton.guildBossInfo;
            if (guildBossInfo != null)
            {
                if (guildBossInfo.bossInfo.id == bossID)
                {
                    // 当前正在打的boss
                    m_bloodProgress.visible = true;
                    m_bloodProgressTip.visible = true;
                    m_additionLabel.visible = true;
                    m_perfictPass.visible = false;

                    m_bloodProgress.value = guildBossInfo.bossInfo.hp * 0.01f;
                    m_bloodProgress.max = GuildBossService.Singleton.MAX_PROGRESS;
                    m_bloodProgressTip.text = string.Format("{0}%", guildBossInfo.bossInfo.hp * 0.01f);

                    m_additionLabel.text = string.Format("全属性加成{0}%", guildBossInfo.bossInfo.attr);
                    m_bubbleGroup.alpha = 1;
                    m_bubbleGroup.TweenScale(Vector2.one, 0.5f);
                }
                else
                {
                    m_bloodProgress.visible = false;
                    m_bloodProgressTip.visible = false;
                    m_additionLabel.visible = false;
                    m_perfictPass.visible = true;
                }
            }
        }
        else
        {
            m_unOpenIcon.visible = true;
            m_openGroup.visible = false;
            m_bubbleGroup.visible = false;
        }
    }

    private void RefreshPostion()
    {
        int index = bossID - 1;
        // 1603010     副本boss头像偏移坐标
        t_globalBean globalBean = ConfigBean.GetBean<t_globalBean, int>(1603010);
        if (globalBean != null && !string.IsNullOrEmpty(globalBean.t_string_param))
        {
            string[] offsetArr = globalBean.t_string_param.Split('+');
            int count = offsetArr.Length;
            index %= count;
            this.y = int.Parse(offsetArr[index]);
        }

    }

    private void OnBossRankBtnClick()
    {
        parentWindow.isClick = true;
        GuildBossService.Singleton.ReqGuildPassRankInfo(bossID, 0);
    }

    private void OnClickItem()
    {
        if (IsOpen())
        {
            parentWindow.isClick = true;
            GuildBossService.Singleton.ReqBossInfo(bossID);
        }
        else
        {
            TipWindow.Singleton.ShowTip("需要通关前面所有社团副本，才能挑战该副本");
        }
    }

    /// <summary>
    /// 是否是正在打的boss
    /// </summary>
    /// <returns></returns>
    private bool IsCurrFightBoss()
    {
        ResGuildDungeonInfo guildBossInfo = GuildBossService.Singleton.guildBossInfo;
        if (guildBossInfo != null)
        {
            BossInfo bossInfo = guildBossInfo.bossInfo;
            return bossInfo.id == bossID;
        }

        return false;
    }
    /// <summary>
    /// 该boss是否开放了
    /// </summary>
    /// <returns></returns>
    private bool IsOpen()
    {
        ResGuildDungeonInfo guildBossInfo = GuildBossService.Singleton.guildBossInfo;
        if (guildBossInfo != null)
        {
            BossInfo bossInfo = guildBossInfo.bossInfo;
            return bossID <= bossInfo.id;
        }

        return false;
    }

    public override void Dispose()
    {
        if (tween != null && tween.IsActive())
            tween.Kill();
        tween = null;

        base.Dispose();
    }
}
