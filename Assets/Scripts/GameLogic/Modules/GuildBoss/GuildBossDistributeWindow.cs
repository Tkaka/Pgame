using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_GuildBoss;
using Message.GuildBoss;
using FairyGUI;

public class GuildBossDistributeWindow : BaseWindow {

    UI_GuildBossDistributeWindow window;

    List<AllotItem> recordInfoList;
    public override void OnOpen()
    {
        base.OnOpen();

        window = getUiWindow<UI_GuildBossDistributeWindow>();
        recordInfoList = Info.param as List<AllotItem>;

        InitView();
    }

    public override void InitView()
    {
        base.InitView();

        window.m_mask.onClick.Add(OnCloseBtn);
        window.m_closeBtn.onClick.Add(OnCloseBtn);

        window.m_contentList.RemoveChildren(0, -1, true);
        int count = recordInfoList.Count;
        window.m_contentList.itemRenderer = RenderRecordListItem;
        window.m_contentList.SetVirtual();
        window.m_contentList.numItems = count;
    }

    private void RenderRecordListItem(int index, GObject obj)
    {
        GuildBossDistributeItem item = obj as GuildBossDistributeItem;
        item.info = recordInfoList[index];
        item.RefreshView();
    }

    protected override void OnCloseBtn()
    {
        base.OnCloseBtn();
    }
}
