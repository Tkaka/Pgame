using UI_Guild;
using Message.Guild;
using Data.Beans;
using UnityEngine;
using FairyGUI;
using System.Collections.Generic;
using System;

public class GuildBaseInfoWnd : BaseWindow
{
    public UI_GuildBaseInfoWnd m_window;
 

    public override void OnOpen()
    {
        base.OnOpen();
        m_window = getUiWindow<UI_GuildBaseInfoWnd>();
        m_window.m_btnChangeHuiZhang.onClick.Add(_OnHuiZhangClick);
        m_window.m_btnChangeName.onClick.Add(_OnChangeNameClick);
        m_window.m_btnQuit.onClick.Add(_OnQuitClick);
        m_window.m_btnSendMail.onClick.Add(_OnSendMailClick);
        m_window.m_btnShuoMing.onClick.Add(_OnShuoMingClick);
        m_window.m_cbxType.onChanged.Add(_OnCbxChange);
        InitView();
    }

    public override void AddEventListener()
    {
        base.AddEventListener();
        GED.ED.addListener(EventID.GuildBadgeChange, _OnBadgeChange);
        GED.ED.addListener(EventID.GuildNameChange, _OnNameChange);
        GED.ED.addListener(EventID.GuildMailNumChange, _OnEMailNumChange);
        GED.ED.addListener(EventID.GuildTypeChange, _OnGuildTypeChange);
        GED.ED.addListener(EventID.GuildMemberInfoChange, _OnGuildMemberInfoChange);
        GED.ED.addListener(EventID.GuildInfo, _OnGuildInfoChange);

    }

    public override void RemoveEventListener()
    {
        base.RemoveEventListener();
        GED.ED.removeListener(EventID.GuildBadgeChange, _OnBadgeChange);
        GED.ED.removeListener(EventID.GuildNameChange, _OnNameChange);
        GED.ED.removeListener(EventID.GuildMailNumChange, _OnEMailNumChange);
        GED.ED.removeListener(EventID.GuildTypeChange, _OnGuildTypeChange);
        GED.ED.removeListener(EventID.GuildMemberInfoChange, _OnGuildMemberInfoChange);
        GED.ED.removeListener(EventID.GuildInfo, _OnGuildInfoChange);
    }

    public override void InitView()
    {
        base.InitView();
        _ShowGuildBaseInfo();
        _ShowMemberList();
    }

    private void _OnBadgeChange(GameEvent evt)
    {
        _ShowHuiZhangIcon();
    }

    private void _OnNameChange(GameEvent evt)
    {
        GuildInfo guildInfo = GuildService.Singleton.GetGuildInfo();
        if (guildInfo == null)
            return;

        m_window.m_txtGuildName.text = guildInfo.name;
    }

    private void _OnEMailNumChange(GameEvent evt)
    {
        if (GuildService.Singleton.IsHaveAuthority(GuildService.EAuthority.Send_Mail))
        {
            GuildInfo guildInfo = GuildService.Singleton.GetGuildInfo();
            if (guildInfo == null)
                return;

            int maxNum = ConfigBean.GetBean<t_globalBean, int>(1601004).t_int_param;
            m_window.m_btnSendMail.m_txtMailNum.text = string.Format("{0}/{1}", (maxNum - guildInfo.mailNum), maxNum);
        }
    }

    private void _OnGuildTypeChange(GameEvent evt)
    {
        GuildInfo guildInfo = GuildService.Singleton.GetGuildInfo();
        if (guildInfo == null)
            return;

        if (GuildService.Singleton.IsHaveAuthority(GuildService.EAuthority.Change_Type))
        {
            m_window.m_cbxType.visible = true;
            m_window.m_txtGuildType.visible = false;
            m_window.m_cbxType.selectedIndex = guildInfo.guildType;
        }
        else
        {
            m_window.m_cbxType.visible = false;
            m_window.m_txtGuildType.visible = true;
            if (guildInfo.guildType == (int)GuildService.EGuildType.ATHLETICS)
                m_window.m_txtGuildType.text = "竞技";
            else
                m_window.m_txtGuildType.text = "休闲";
        }
    }

    private void _OnGuildMemberInfoChange(GameEvent evt)
    {
        _ShowMemberList();
    }

    private void _OnGuildInfoChange(GameEvent evt)
    {
        InitView();
    }

    private void _OnCbxChange()
    {
        GuildService.Singleton.ReqChangeGuildType(m_window.m_cbxType.selectedIndex);
    }

    private void _ShowGuildBaseInfo()
    {
        GuildInfo guildInfo = GuildService.Singleton.GetGuildInfo();
        if (guildInfo == null)
            return;

        t_guildBean guildBean = ConfigBean.GetBean<t_guildBean, int>(guildInfo.level);
        if (guildBean == null)
            return;

        m_window.m_txtGuildName.text = guildInfo.name;
        m_window.m_txtGuildLevel.text = string.Format("等级{0}",guildInfo.level);
        m_window.m_txtPeopleNum.text = string.Format("{0}/{1}", guildInfo.memberNum, guildBean.t_member_num);
        m_window.m_txtPeopleNum.color = (guildInfo.memberNum >= guildBean.t_member_num) ? Color.red : Color.green;
        m_window.m_txtChairManNum.text = "1/1";
        m_window.m_txtViceChairMan.text = string.Format("{0}/{1}", guildInfo.deputyLeaderNum, guildBean.t_vice_chairman_num);
        m_window.m_txtGuildNum.text = guildInfo.id + "";
        m_window.m_txtGuildRank.text = guildInfo.rank + "";

        int openEliteLevel = ConfigBean.GetBean<t_globalBean, int>(1601011).t_int_param;
        if (guildInfo.level >= openEliteLevel)
        {
            m_window.m_txtEliteNum.text = string.Format("{0}/{1}", guildInfo.eliteNum, guildBean.t_elite_num);
        }
        else
        {
            m_window.m_txtEliteNum.text = string.Format("公会{0}级开启", openEliteLevel);
        }
 
        m_window.m_txtChairMan.text = guildInfo.chairmanName;

        m_window.m_btnChangeName.visible = GuildService.Singleton.IsHaveAuthority(GuildService.EAuthority.Change_Name);
        m_window.m_btnChangeHuiZhang.visible = GuildService.Singleton.IsHaveAuthority(GuildService.EAuthority.Change_Badge);
        m_window.m_btnQuit.m_txtQuit.text = GuildService.Singleton.IsHaveAuthority(GuildService.EAuthority.Break) ? "解散社团" : "退出社团";

        _OnGuildTypeChange(null);

        if (GuildService.Singleton.IsHaveAuthority(GuildService.EAuthority.Send_Mail))
        {
            m_window.m_btnSendMail.visible = true;
            int maxNum = ConfigBean.GetBean<t_globalBean, int>(1601004).t_int_param;
            m_window.m_btnSendMail.m_txtMailNum.text = string.Format("{0}/{1}", (maxNum - guildInfo.mailNum), maxNum);
        }
        else
        {
            m_window.m_btnSendMail.visible = false;
        }

        _ShowHuiZhangIcon();
    }

    private void _ShowHuiZhangIcon()
    {
        GuildInfo guildInfo = GuildService.Singleton.GetGuildInfo();
        if (guildInfo == null)
            return;

        t_iconBean guildBean = ConfigBean.GetBean<t_iconBean, int>(guildInfo.badge);
        if (guildBean == null)
            return;

        //Debug.Log("=================>>>>>显示徽章" + guildBean.t_icon);
        //m_window.m_imgHuiZhang.url = guildBean.;
        UIGloader.SetUrl(m_window.m_imgHuiZhang, guildBean.t_icon);
    }

    private int CompareFun(Member a, Member b)
    {
        if (a.job == b.job)
        {
            if (a.level == b.level)
            {
                if (a.contribution == b.contribution)
                    return a.fightPower > b.fightPower ? -1 : 1;
                else
                    return a.contribution > b.contribution ? -1:1;
            }
            else
            {
                return a.level > b.level ? -1:1;
            }
        }
        else
        {
            return a.job < b.job ?-1 : 1;
        }
      
    }

    private void _ShowMemberList()
    {
        List<Member> members = GuildService.Singleton.GetMembers();
        members.Sort(CompareFun);
        m_window.m_memberList.SetVirtual();
        m_window.m_memberList.itemProvider = _GetListItemResource;
        m_window.m_memberList.itemRenderer = _OnMemberCellShow;
        m_window.m_memberList.numItems = members.Count;

    }

    private void _OnMemberCellShow(int index, GObject obj)
    {
        List<Member> members = GuildService.Singleton.GetMembers();
        if (index >= members.Count)
            return;
        Member memberInfo = members[index];
        UI_remeberInfoCell cell = obj as UI_remeberInfoCell;
        cell.m_txtName.text = memberInfo.name;
        cell.m_txtLevel.text = "等级" + memberInfo.level;
        string strJob;
        if (memberInfo.job == (int)GuildService.EJobType.Chair_Man)
            strJob = "社长";
        else if (memberInfo.job == (int)GuildService.EJobType.Deputy_Leader)
            strJob = "副社长";
        else if (memberInfo.job == (int)GuildService.EJobType.Elite)
            strJob = "精英";
        else
            strJob = "社员";
        cell.m_txtJob.text = strJob;
        cell.m_txtTime.text = GuildService.Singleton.GetLastOnLineTime(memberInfo.lastLogin);

        cell.m_txtTotalContribution.text = memberInfo.contribution + "";
        cell.m_txtTodayContribution.text = memberInfo.dailyContribution + "";

        cell.onClick.Clear();
        cell.onClick.Add(() => {
            WinMgr.Singleton.Open<SeeMemberInfoWnd>(WinInfo.Create(false, null, true, memberInfo));
            //Debug.Log("查看详细信息" + memberInfo.roleId);
        });
    }

    private string _GetListItemResource(int index)
    {
        return UI_remeberInfoCell.URL;
    }
    private void _OnHuiZhangClick()
    {
        OneParam<Action<int>> oneParam = new OneParam<Action<int>>();
        oneParam.value = _OnCallBack;

        WinMgr.Singleton.Open<ChangeBadgeWnd>(WinInfo.Create(false, null, true, oneParam));
    }

    private void _OnCallBack(int id)
    {
        GuildService.Singleton.ReqChangeBadge(id);
    }

    private void _OnChangeNameClick()
    {
        WinMgr.Singleton.Open<GuildChangeNameWnd>();
    }

    private void _OnQuitClick()
    {
        string strDes;
        GuildInfo guildInfo = GuildService.Singleton.GetGuildInfo();
        if (GuildService.Singleton.IsHaveAuthority(GuildService.EAuthority.Break))
        {
            strDes = string.Format(UIUtils.GetStrByLanguageID(71601021), guildInfo.name);               
        }
        else
        {
            strDes = string.Format(UIUtils.GetStrByLanguageID(71601022), guildInfo.name);             
        }
        CommonTipsManager.GetInstance().ShowTips(TipsType.SingleButton, "提示", strDes, ()=>
        {
            if (GuildService.Singleton.IsHaveAuthority(GuildService.EAuthority.Break))
                GuildService.Singleton.ReqBreak();
            else
                GuildService.Singleton.ReqExit();
        });
    }

    private void _OnSendMailClick()
    {
        WinMgr.Singleton.Open<SendEmailWnd>();
    }

    private void _OnShuoMingClick()
    {
        WinMgr.Singleton.Open<GuildExplainWnd>();
    }

    protected override void OnClose()
    {
        base.OnClose();
    }
}