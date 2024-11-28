using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_GuildBoss;
using Message.GuildBoss;

public class MemberProgressItem : UI_memberProgressItem {

    public ProgressItem progressInfo;

    public new static MemberProgressItem CreateInstance()
    {
        return UI_memberProgressItem.CreateInstance() as MemberProgressItem;
    }

    public void RefreshView()
    {
        UIGloader.SetUrl(m_iconLoader, UIUtils.GetHeadIcon(progressInfo.icon));
        m_nameLabel.text = progressInfo.name;
        m_levelLabel.text = string.Format("等级{0}", progressInfo.level);
        m_positionLabel.text = GuildService.Singleton.GetJobDes(progressInfo.job);
        m_progressLabel.text = string.Format("{0}/{1}", progressInfo.progress, GuildBossService.Singleton.GetMaxFightTimes());
        m_memberProgress.value = progressInfo.progress;
        m_memberProgress.max = GuildBossService.Singleton.GetMaxFightTimes();
    }
}
