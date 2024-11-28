using UI_Guild;
using Message.Guild;
using Data.Beans;
using UnityEngine;
using FairyGUI;
using System.Collections.Generic;


//审核窗口
public class CheckMemberJoinWnd : BaseWindow
{
    private UI_CheckMemberJoinWnd m_window;
    private DoActionInterval m_timer;
    private bool m_isCooling = false;       //招募按钮是否冷却中
    public override void OnOpen()
    {
        base.OnOpen();
        m_window = getUiWindow<UI_CheckMemberJoinWnd>();
        m_window.m_btnOneKeyRefuse.onClick.Add(_OnOneKeyRefuseClick);
        m_window.m_btnZhaoMu.onClick.Add(_OnZhaoMuClick);
        m_window.m_btnSetLimit.onClick.Add(_OnSetLimitClick);
        _ShowBaseInfo();
        _ShowApplyerInfo();
        _ShowZhaoMuDes();
    }

    public override void AddEventListener()
    {
        base.AddEventListener();
        GED.ED.addListener(EventID.JoinLimitChange, _JoinLimitChange);
        GED.ED.addListener(EventID.GuildApplyerInfo, _OnApplyerInfoChange);
    }

    public override void RemoveEventListener()
    {
        base.RemoveEventListener();
        GED.ED.removeListener(EventID.JoinLimitChange, _JoinLimitChange);
        GED.ED.removeListener(EventID.GuildApplyerInfo, _OnApplyerInfoChange);
    }

    private void _JoinLimitChange(GameEvent evt)
    {
        GuildInfo guildInfo = GuildService.Singleton.GetGuildInfo();
        string strLevel = GuildService.Singleton.GetLimitLevelDes(guildInfo.levelLimt);

        string strType = GuildService.Singleton.GetLimitTypeDes(guildInfo.limitType);

        m_window.m_txtLimit.text = string.Format("加入条件:{0}({1})", strLevel, strType);
    }

    private void _OnApplyerInfoChange(GameEvent evt)
    {
        _ShowApplyerInfo();
    }

    private void _ShowApplyerInfo()
    {
        m_window.m_apppyerList.RemoveChildren(0, -1, true);
        Dictionary<long, Applyer> applyerDic = GuildService.Singleton.GetApplyers();
        foreach (var info in applyerDic)
        {
            UI_CheckMemberCell cell = UI_CheckMemberCell.CreateInstance();
            _OnCellShow(cell, info.Value);
            m_window.m_apppyerList.AddChild(cell);
        }

    }

    private void _OnCellShow(UI_CheckMemberCell cell, Applyer applyer)
    {
        //cell.m_icon;
        cell.m_txtName.text = applyer.name;
        cell.m_txtLevel.text = string.Format("Lv{0}", applyer.level);
        cell.m_txtTime.text = TimeUtils.TimeToStringFormat(applyer.time, "{0}-{1}-{2}");
        cell.m_btnAgree.onClick.Clear();
        cell.m_btnRefuse.onClick.Clear();
        cell.m_btnAgree.onClick.Add(() =>
        {
            GuildService.Singleton.ReqOperateApplyer(applyer.roleId, true);
        });

        cell.m_btnRefuse.onClick.Add(() =>
        {
            GuildService.Singleton.ReqOperateApplyer(applyer.roleId, false);
        });
    }

    private void _ShowBaseInfo()
    {
        Dictionary<long, Applyer> applyerDic = GuildService.Singleton.GetApplyers();
        if (applyerDic.Count == 0)
        {
            m_window.m_txtNoMember.visible = true;
            m_window.m_txtNoMember.text = UIUtils.GetStrByLanguageID(71601026);
            m_window.m_btnOneKeyRefuse.grayed = true;
            m_window.m_btnOneKeyRefuse.enabled = false;
        }
        else
        {
            m_window.m_txtNoMember.visible = false;
            m_window.m_btnOneKeyRefuse.grayed = false;
            m_window.m_btnOneKeyRefuse.enabled = true;
        }

        GuildInfo guildInfo = GuildService.Singleton.GetGuildInfo();
        string strLevel = GuildService.Singleton.GetLimitLevelDes(guildInfo.levelLimt);

        string strType = GuildService.Singleton.GetLimitTypeDes(guildInfo.limitType);

        m_window.m_txtLimit.text = string.Format("加入条件:{0}({1})", strLevel, strType);
    }

    private void _ShowZhaoMuDes()
    {
        m_isCooling = false;
        long lastTime = GuildService.Singleton.GetLastZhaoMuTime();
        if (lastTime == 0)
        {
            m_window.m_btnZhaoMu.m_txtDes.text = "招募";
        }
        else
        {
            int targetMin = ConfigBean.GetBean<t_globalBean, int>(1601010).t_int_param;
            int second = (int)(TimeUtils.CalculateDelta(lastTime) / 1000);
            if (second > targetMin * 60)
            {
                m_window.m_btnZhaoMu.m_txtDes.text = "招募";
            }
            else
            {
                int remainSecond = (targetMin * 60) - second;
                m_isCooling = true;
                if (m_timer == null)
                    m_timer = new DoActionInterval();

                m_timer.doAction(1, (param) => {
                    remainSecond--;
                    m_window.m_btnZhaoMu.m_txtDes.text = TimeUtils.FormatTime2(remainSecond);
                    if (remainSecond == 0)
                    {
                        m_isCooling = false;
                        m_window.m_btnZhaoMu.m_txtDes.text = "招募";
                        m_timer.kill();

                    }
                }, null, true);
            }
        }
 
    }

    private void _OnOneKeyRefuseClick()
    {
        GuildService.Singleton.ReqClearApplyList();
    }

    private void _OnZhaoMuClick()
    {
        if (m_isCooling)
        {
            TipWindow.Singleton.ShowTip("招募冷却中");
            return;
        }
        
        GuildService.Singleton.ReqZhaoMu();
        _ShowZhaoMuDes();
    }

    private void _OnSetLimitClick()
    {
        WinMgr.Singleton.Open<SetLimitWnd>();
    }

    protected override void OnClose()
    {
        base.OnClose();
        if (m_timer != null)
            m_timer.kill();
    }
}