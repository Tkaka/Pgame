using UI_Guild;
using Message.Guild;
using Data.Beans;
using UnityEngine;

public class GuildInfoCell : UI_GuildInfoCell
{
    public GuildListInfo guildInfo;
    public new static GuildInfoCell CreateInstance()
    {
        return UI_GuildInfoCell.CreateInstance() as GuildInfoCell;
    }

    public void RefreshView()
    {
        if (guildInfo == null)
            return;
        m_txtRank.text = guildInfo.rank + "";
        m_txtGuildType.text = guildInfo.guildType == (int)GuildService.EGuildType.RELAXATION ? "休闲" : "竞技";
        t_iconBean iconBean = ConfigBean.GetBean<t_iconBean, int>(guildInfo.badge);
        if (iconBean != null)
            UIGloader.SetUrl(m_imgBadge, iconBean.t_icon);


        m_txtName.text = guildInfo.name;
        m_txtGuildLevel.text = "等级" + guildInfo.level;

        t_guildBean guildBean = ConfigBean.GetBean<t_guildBean, int>(guildInfo.level);
        if (guildBean == null)
            return;

        m_txtMemberNum.text = string.Format("{0}/{1}", guildInfo.memberNum, guildBean.t_member_num);
        m_txtMemberNum.color = guildInfo.memberNum < guildBean.t_member_num ? Color.green : Color.red;
     
        m_txtLimitType.text = GuildService.Singleton.GetLimitTypeDes(guildInfo.limitType);
        m_txtLimitLevel.text = GuildService.Singleton.GetLimitLevelDes(guildInfo.limitLevel);
        if (guildInfo.isApplying)
        {
            m_objApplying.visible = true;
            m_objFull.visible = false;
            m_btnJoin.visible = false;
        }
        else
        {
            m_objApplying.visible = false;

            if (guildInfo.memberNum == guildBean.t_member_num)
            {
                m_objFull.visible = true;
                m_btnJoin.visible = false;
            }
            else
            {
                m_objFull.visible = false;
                m_btnJoin.visible = true;
            }

            if (guildInfo.limitLevel == -1 || guildInfo.limitType == 2)
            {
                //禁止加入
                m_btnJoin.visible = false;
            }
        }

        m_btnJoin.onClick.Clear();
        m_btnJoin.onClick.Add(()=>{
            int roleLevel = RoleService.Singleton.GetRoleInfo().level;
            int minLevel = ConfigBean.GetBean<t_globalBean, int>(1601012).t_int_param;
            if (guildInfo.limitLevel > roleLevel || roleLevel < minLevel)
            {
                TipWindow.Singleton.ShowTip("等级不足!");
                return;
            }

            m_objFull.visible = false;
            m_btnJoin.visible = false;
            m_objApplying.visible = true;
            GuildService.Singleton.ReqApplyJoin(false, guildInfo.id);     
        });


    }

    public override void Dispose()
    {
        base.Dispose();
    }
}