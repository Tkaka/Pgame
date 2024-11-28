using System.Collections;
using System.Collections.Generic;
using UI_GuildBoss;
using Message.GuildBoss;
using FairyGUI;
using System;

public class GuildBossPassRankWindow : BaseWindow {

    UI_GuildBossPassRankWindow window;

    ResGuildRankInfo rankInfo;

    public override void OnOpen()
    {
        base.OnOpen();

        window = getUiWindow<UI_GuildBossPassRankWindow>();
        rankInfo = Info.param as ResGuildRankInfo;
        window.m_mask.onClick.Add(OnCloseBtn);
        window.m_closeBtn.onClick.Add(OnCloseBtn);
        InitView();
    }

    public override void InitView()
    {
        base.InitView();

        InitRankList();
        InitSelfGuildRankInfo();
    }

    private void InitRankList()
    {
        window.m_passRankList.scrollPane.onPullUpRelease.Add(OnPullUpRelease);

        window.m_passRankList.itemRenderer = RenderRankListItem;
        window.m_passRankList.SetVirtual();
        window.m_passRankList.numItems = rankInfo.ranks.Count;
    }

    private void RenderRankListItem(int index, GObject obj)
    {
        PassRankItem rankItem = obj as PassRankItem;
        rankItem.rankInfo = rankInfo.ranks[index];
        rankItem.RefreshView();
    }

    private void InitSelfGuildRankInfo()
    {
        window.m_selfRankLabel.text = rankInfo.self.rank + 1 + "";
        window.m_selfGuildNameLabel.text = rankInfo.self.name;
        if (rankInfo.self.completeTime == 0)
        {
            // 未完成
            window.m_unpassTipLabel.visible = true;
            window.m_selfPassTimeLabel.visible = false;
        }
        else
        {
            DateTime completeTime = TimeUtils.javaTimeToCSharpTime(rankInfo.self.completeTime);
            window.m_selfPassTimeLabel.text = completeTime.ToString("yyyy-MM-dd HH:mm");
            window.m_unpassTipLabel.visible = false;
        }
    }
    /// <summary>
    /// 上拉刷新
    /// </summary>
    private void OnPullUpRelease()
    {
        int startIndex = rankInfo.ranks.Count;
        GuildBossService.Singleton.ReqGuildPassRankInfo(rankInfo.bossId, startIndex);
    }

    protected override void OnCloseBtn()
    {
        base.OnCloseBtn();
    }

}
