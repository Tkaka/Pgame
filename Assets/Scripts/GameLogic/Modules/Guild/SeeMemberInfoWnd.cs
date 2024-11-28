using UI_Guild;
using Message.Guild;
using Data.Beans;
using UnityEngine;
using FairyGUI;
using System.Collections.Generic;

public class SeeMemberInfoWnd : BaseWindow
{
    private UI_SeeMemberInfoWnd m_window;
    private Member m_merberInfo;

    public override void OnOpen()
    {
        base.OnOpen();
        m_window = getUiWindow<UI_SeeMemberInfoWnd>();
        m_window.m_btnClose.onClick.Add(Close);
        m_window.m_btnAddFriend.onClick.Add(_OnAddFriend);
        m_window.m_btnSendMsg.onClick.Add(_OnSendMsg);
        m_window.m_btnTiChu.onClick.Add(_OnTiChuClick);
        m_window.m_btnChuanZhi.onClick.Add(_OnChuanZhiClick);
        m_window.m_btnJiangZhi.onClick.Add(_OnJiangZhiClick);
        m_window.m_btnShenZhi.onClick.Add(_OnShenZhiClick);
        m_window.m_btnOk.onClick.Add(Close);

        m_merberInfo = Info.param as Member;
        if (m_merberInfo == null)
            return;

        m_window.m_objSelect.visible = false;
        _ShowBaseInfo();
        _ShowPetInfo();
    }

    private void _ShowPetInfo()
    {

    }

    private void _ShowBaseInfo()
    {
        m_window.m_txtName.text = m_merberInfo.name;
        m_window.m_txtTime.text = GuildService.Singleton.GetLastOnLineTime(m_merberInfo.lastLogin);
        m_window.m_txtContribution.text = m_merberInfo.contribution + "";
        UIGloader.SetUrl(m_window.m_headIocn.m_imgIcon, UIUtils.GetHeadIcon(m_merberInfo.model));
        m_window.m_txtLevel.text = "等级" + m_merberInfo.level;

        string strJob;
        if (m_merberInfo.job == (int)GuildService.EJobType.Chair_Man)
            strJob = "社长";
        else if (m_merberInfo.job == (int)GuildService.EJobType.Deputy_Leader)
            strJob = "副社长";
        else if (m_merberInfo.job == (int)GuildService.EJobType.Elite)
            strJob = "精英";
        else
            strJob = "社员";

        m_window.m_txtJob.text = strJob;
        m_window.m_txtFightPower.text = m_merberInfo.fightPower + "";
        long selfRoleId = RoleService.Singleton.GetRoleInfo().roleId;

        if (selfRoleId == m_merberInfo.roleId)
        {
            m_window.m_btnGroup.visible = false;
            m_window.m_OperationGroup.visible = false;
            m_window.m_btnOk.visible = true;
        }
        else
        {
            m_window.m_btnGroup.visible = true;
            m_window.m_OperationGroup.visible = true;
            m_window.m_btnOk.visible = false;
        }


        GuildInfo guildInfo = GuildService.Singleton.GetGuildInfo();
        m_window.m_btnChuanZhi.visible = _CanOperationJob((GuildService.EJobType)guildInfo.roleJob) &&
            _CanOperationJob((GuildService.EJobType)m_merberInfo.job);
        m_window.m_btnJiangZhi.visible = _CanJiangZhi((GuildService.EJobType)m_merberInfo.job);
        m_window.m_btnTiChu.visible = _CanOperationJob((GuildService.EJobType)m_merberInfo.job);
        m_window.m_btnShenZhi.visible = _CanShenZhi((GuildService.EJobType)m_merberInfo.job);
    }


    //主角能否操作指定职位
    private bool _CanOperationJob(GuildService.EJobType jobType)
    {
        GuildService.EAuthority needAuthority;
        switch (jobType)
        {
            case GuildService.EJobType.Chair_Man:
                needAuthority = GuildService.EAuthority.Change_ChairMan;
                break;
            case GuildService.EJobType.Deputy_Leader:
                needAuthority = GuildService.EAuthority.Change_DeputyLeader;
                break;
            case GuildService.EJobType.Elite:
                needAuthority = GuildService.EAuthority.Change_Elite;
                break;
            default:
                needAuthority = GuildService.EAuthority.Quit;
                break;
        }

        return GuildService.Singleton.IsHaveAuthority(needAuthority);
    }

    //能否降职
    private bool _CanJiangZhi(GuildService.EJobType jobType)
    {
        GuildInfo guildInfo = GuildService.Singleton.GetGuildInfo();
        t_guildBean guildBean = ConfigBean.GetBean<t_guildBean, int>(guildInfo.level);
        if (guildBean == null)
            return false;

        bool canJiangZhi = true;
        switch (jobType)
        {
            case GuildService.EJobType.Chair_Man:
                canJiangZhi = guildInfo.deputyLeaderNum < guildBean.t_vice_chairman_num;
                break;
            case GuildService.EJobType.Deputy_Leader:
                canJiangZhi = guildInfo.eliteNum < guildBean.t_elite_num;
                break;
            case GuildService.EJobType.Elite:
                canJiangZhi = true;
                break;
            default:
                canJiangZhi = false;
                break;
        }

        if (canJiangZhi)
            return _CanOperationJob(jobType);

        return false;
    }

    //能否升职
    private bool _CanShenZhi(GuildService.EJobType jobType)
    {
        GuildInfo guildInfo = GuildService.Singleton.GetGuildInfo();
        t_guildBean guildBean = ConfigBean.GetBean<t_guildBean, int>(guildInfo.level);
        if (guildBean == null)
            return false;

        bool canShenZhi = true;
        switch (jobType)
        {
            case GuildService.EJobType.Chair_Man:
                canShenZhi = false;
                break;
            case GuildService.EJobType.Deputy_Leader:
                canShenZhi = false;
                break;
            case GuildService.EJobType.Elite:
                canShenZhi = guildInfo.deputyLeaderNum < guildBean.t_vice_chairman_num;
                break;
            default:
                canShenZhi = guildInfo.eliteNum < guildBean.t_elite_num || guildInfo.deputyLeaderNum < guildBean.t_vice_chairman_num;
                break;
        }

        if (canShenZhi)
            return _CanOperationJob(jobType);

        return false;
    }

    private void _ShowSelectList(int jobType)
    {
        m_window.m_btnList.RemoveChildren(0, -1, true);
        for (int i = (int)GuildService.EJobType.Deputy_Leader; i < jobType; i++)
        {
            UI_zhiweiCell cell = UI_zhiweiCell.CreateInstance();
            string strDes = "";
            if (i == (int)GuildService.EJobType.Elite)
                strDes = "精英";
            else if (i == (int)GuildService.EJobType.Deputy_Leader)
                strDes = "副社长";
            else
                continue;

            cell.m_txtDes.text = strDes;
            m_window.m_btnList.AddChild(cell);
            int newJob = i;
            cell.onClick.Add(()=> {
                GuildService.Singleton.ReqOperateMember(m_merberInfo.roleId, m_merberInfo.job, newJob);
                m_window.m_objSelect.visible = false;
            });
        }

    }

    private void _OnAddFriend()
    {
        Debug.Log("添加好友");
    }

    private void _OnSendMsg()
    {
        Debug.Log("私聊");
    }

    private void _OnTiChuClick()
    {
        GuildService.Singleton.ReqOperateMember(m_merberInfo.roleId, m_merberInfo.job, -1);
        Close();
    }

    private void _OnChuanZhiClick()
    {
        GuildInfo guildInfo = GuildService.Singleton.GetGuildInfo();
        GuildService.Singleton.ReqOperateMember(m_merberInfo.roleId, m_merberInfo.job, guildInfo.roleJob);
        Close();
    }

    private void _OnJiangZhiClick()
    {
        GuildService.Singleton.ReqOperateMember(m_merberInfo.roleId, m_merberInfo.job, m_merberInfo.job + 1);
        Close();
    }

    private void _OnShenZhiClick()
    {
        if (m_window.m_objSelect.visible)
        {
            m_window.m_objSelect.visible = false;
        }
        else
        {
            m_window.m_objSelect.visible = true;
            _ShowSelectList(m_merberInfo.job);
        }
         
    }

    protected override void OnClose()
    {
        base.OnClose();
    }
}