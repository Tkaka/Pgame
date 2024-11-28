using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_MainCity;
using Data.Beans;
using System;

public class RoleInfoWindow : BaseWindow {

    UI_RoleInfoWindow window;
    /// <summary>
    ///  体力恢复的时间
    /// </summary>
    int recoverTime;
    DoActionInterval doAction;
    CommonHeadIcon headIcon;
    public override void OnOpen()
    {
        base.OnOpen();

        window = getUiWindow<UI_RoleInfoWindow>();

        BindEvent();
        InitView();
    }

    public override void AddEventListener()
    {
        base.AddEventListener();

        GED.ED.addListener(EventID.OnResModifyIcon, OnRoleInfoChange);
        GED.ED.addListener(EventID.OnResModifyNickname, OnRoleInfoChange);
        GED.ED.addListener(EventID.OnYinChangJinBi, OnYinCang);
    }

    public override void RemoveEventListener()
    {
        base.RemoveEventListener();

        GED.ED.removeListener(EventID.OnResModifyIcon, OnRoleInfoChange);
        GED.ED.removeListener(EventID.OnResModifyNickname, OnRoleInfoChange);
        GED.ED.removeListener(EventID.OnYinChangJinBi, OnYinCang);
    }

    public override void InitView()
    {
        base.InitView();
        // 属性
        Message.Role.RoleInfo roleInfo = RoleService.Singleton.GetRoleInfo();
        window.m_nameLabel.text = roleInfo.roleName;
        window.m_guildName.text = roleInfo.guildName;
        window.m_levelLabel.text = roleInfo.level + "";
        int maxLevel = RoleService.Singleton.GetRoleMaxLevel();
        t_role_level_upBean roleLevelUpBean = ConfigBean.GetBean<t_role_level_upBean, int>(roleInfo.level);
        if (roleInfo.level >= maxLevel && roleLevelUpBean.t_exp <= roleInfo.curExp)
        {
            window.m_fullLevelTip.visible = true;
            window.m_expProgressTip.visible = false;
            window.m_expProgress.value = roleInfo.curExp;
            window.m_expProgress.max = roleInfo.curExp;
        }

        else
        {
            window.m_fullLevelTip.visible = false;
            window.m_expProgressTip.visible = true;
            int levelUpExp = 0;
            if (roleLevelUpBean != null)
            {
                levelUpExp = roleLevelUpBean.t_exp;
            }
            window.m_expProgressTip.text = string.Format("{0}/{1}", roleInfo.curExp, levelUpExp);
            window.m_expProgress.value = roleInfo.curExp;
            window.m_expProgress.max = levelUpExp;
        }
        window.m_lvLimitLabel.text = roleInfo.level + "";
        window.m_numberLabel.text = roleInfo.roleId + "";

        // 战斗力
        long fightPower = PetService.Singleton.GetRoleFightPower();
        window.m_fightPowerLabel.text = fightPower + "";
        // 称号
        Message.Achievement.ResAchievementInfo achievementInfo = AchievementService.Singleton.achievementinfo;
        if (achievementInfo != null)
        {
            int achievementID = achievementInfo.title;
            t_achievementBean achievementBean = ConfigBean.GetBean<t_achievementBean, int>(achievementID);
            if (achievementBean != null)
                window.m_achievementName.text = achievementBean.t_name;
        }
        // 头像
        headIcon = window.m_headIcon as CommonHeadIcon;
        headIcon.Init(roleInfo.headIconId, null, true);
        // 体力恢复时间
        int speed = RoleService.Singleton.GetTiLiHuiFuSpeed();
        if (roleLevelUpBean != null)
        {
            if (roleInfo.energy >= roleLevelUpBean.t_energy_max)
            {
                // 体力已满
                window.m_tiLiTimeLabel.text = "体力已满";
            }
            else
            {
                DateTime curTime = TimeUtils.currentServerDateTime();
                TimeSpan ts = curTime - new DateTime(1970, 1, 1);
                int nextTime = (int)(roleInfo.nextEnergyTime * 0.001f - ts.TotalSeconds);
                bool isRecoverOne = false;
                if (nextTime == 0)
                    isRecoverOne = true;
                
                int remainTiLiNum = roleLevelUpBean.t_energy_max - roleInfo.energy;
                remainTiLiNum -= isRecoverOne == false ? 1 : 0;
                int remainTime = remainTiLiNum * speed;
                recoverTime = nextTime + remainTime;

                doAction = new DoActionInterval();
                doAction.doAction(1, RefreshTiLiTime, null, true);
            }
        }
    }

    private void BindEvent()
    {
        window.m_closeBtn.onClick.Add(OnCloseBtn);
        window.m_mask.onClick.Add(OnCloseBtn);
        window.m_modifyHeadIconBtn.onClick.Add(OnModifyHeadIconClick);
        window.m_modifyNameBtn.onClick.Add(OnModifyNameClick);
    }

    private void RefreshTiLiTime(object obj)
    {
        recoverTime--;
        if (recoverTime <= 0)
        {
            // 体力恢复满了
            window.m_tiLiTimeLabel.text = "体力已满";
            doAction.kill();
        }
        else
        {
            window.m_tiLiTimeLabel.text = TimeUtils.FormatTime(recoverTime);
        }
    }
    private void OnYinCang(GameEvent evt)
    {
        bool xianshi = (bool)evt.Data;
        window.visible = xianshi;
    }

    #region 按钮事件响应 ******************************************************************************************************
    private void OnModifyHeadIconClick()
    {
        // 打开头像选择界面
        WinMgr.Singleton.Open<ModifyHeadIconWindow>(null, UILayer.Popup);
    }

    private void OnModifyNameClick()
    {
        // 打开名字修改界面
        WinMgr.Singleton.Open<ChnageNameWindow>(null, UILayer.Popup);
    }
    #endregion;

    private void OnRoleInfoChange(GameEvent evt)
    {
        Message.Role.RoleInfo roleInfo = RoleService.Singleton.GetRoleInfo();
        headIcon.headID = roleInfo.headIconId;
        headIcon.RefreshView();

        window.m_nameLabel.text = roleInfo.roleName;
    }

    protected override void OnClose()
    {
        if (doAction != null && doAction.IsRunning)
            doAction.kill();
        doAction = null;

        base.OnClose();
    }
}
