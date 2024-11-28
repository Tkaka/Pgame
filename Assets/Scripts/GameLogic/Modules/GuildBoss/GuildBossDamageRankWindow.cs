using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_GuildBoss;
using Message.GuildBoss;
using FairyGUI;

public class GuildBossDamageRankWindow : BaseWindow {

    UI_GuildBossDamageRankWindow window;
    List<DamageRankItem> rankList;
    int bossID;

    public override void OnOpen()
    {
        base.OnOpen();

        window = getUiWindow<UI_GuildBossDamageRankWindow>();
        TwoParam<int, List<DamageRankItem>> param = Info.param as TwoParam<int, List<DamageRankItem>>;
        rankList = param.value2;
        bossID = param.value1;

        InitView();
    }

    public override void AddEventListener()
    {
        base.AddEventListener();

        GED.ED.addListener(EventID.OnResDamageRank, OnResDamageRank);
    }

    public override void RemoveEventListener()
    {
        base.RemoveEventListener();

        GED.ED.removeListener(EventID.OnResDamageRank, OnResDamageRank);
    }

    public override void InitView()
    {
        base.InitView();
        window.m_mask.onClick.Add(OnCloseBtn);
        window.m_closeBtn.onClick.Add(OnCloseBtn);
        InitRankList();
    }

    private void InitRankList()
    {
        window.m_damageRankList.itemRenderer = RenderListItem;
        window.m_damageRankList.SetVirtual();
        window.m_damageRankList.numItems = rankList.Count;

        window.m_damageRankList.scrollPane.onPullUpRelease.Add(OnPullUpRelease);
    }

    private void RenderListItem(int index, GObject obj)
    {
        GuildBossDamageRankItem item = obj as GuildBossDamageRankItem;
        item.index = index;
        item.itemInfo = rankList[index];
        item.RefreshView();
    }


    private void OnPullUpRelease()
    {
        int startIndex = rankList.Count;
        GuildBossService.Singleton.ReqDamageRank(bossID, startIndex);
    }

    private void OnResDamageRank(GameEvent evt)
    {
        rankList.AddRange(evt.Data as List<DamageRankItem>);

        window.m_damageRankList.numItems = rankList.Count;
    }

    protected override void OnCloseBtn()
    {
        base.OnCloseBtn();
    }
}
