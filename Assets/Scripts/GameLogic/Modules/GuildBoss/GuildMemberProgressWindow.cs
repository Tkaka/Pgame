using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_GuildBoss;
using Message.GuildBoss;
using FairyGUI;

public class GuildMemberProgressWindow : BaseWindow {

    UI_GuildMemberProgressWindow window;

    List<ProgressItem> totalProgressList;
    List<ProgressItem> unfinishedProgresslist = new List<ProgressItem>();

    public override void OnOpen()
    {
        base.OnOpen();

        window = getUiWindow<UI_GuildMemberProgressWindow>();
        window.m_mask.onClick.Add(OnCloseBtn);
        window.m_closeBtn.onClick.Add(OnCloseBtn);
        totalProgressList = Info.param as List<ProgressItem>;
        UpdateUnfinishedProgressList();

        InitView();
    }

    public override void AddEventListener()
    {
        base.AddEventListener();

        GED.ED.addListener(EventID.OnResProgressInfo, OnResProgressInfo);
    }

    public override void RemoveEventListener()
    {
        base.RemoveEventListener();

        GED.ED.removeListener(EventID.OnResProgressInfo, OnResProgressInfo);
    }

    public override void InitView()
    {
        base.InitView();

        window.m_doubuleSelectBtn.onChanged.Add(OnDoubleBtnChange);

        InitProgressList();
    }

    private void InitProgressList()
    {
        window.m_memberList.scrollPane.onPullUpRelease.Add(OnPullUpRelease);
        window.m_memberList.RemoveChildren(0, -1, true);
        window.m_memberList.itemRenderer = RenderMemberItem;
        window.m_memberList.SetVirtual();
        window.m_memberList.numItems = totalProgressList.Count;
    }

    private void RenderMemberItem(int index, GObject obj)
    {
        MemberProgressItem progressItem = obj as MemberProgressItem;
        if (window.m_doubuleSelectBtn.selected)
            progressItem.progressInfo = unfinishedProgresslist[index];
        else
            progressItem.progressInfo = totalProgressList[index];
        progressItem.RefreshView();
    }
    /// <summary>
    /// 上拉刷新
    /// </summary>
    private void OnPullUpRelease()
    {
        int startIndex = totalProgressList.Count;
        GuildBossService.Singleton.ReqProgressInfo(startIndex);
    }

    private void OnResProgressInfo(GameEvent evt)
    {
        totalProgressList.AddRange(evt.Data as List<ProgressItem>);
        UpdateUnfinishedProgressList();
    }

    private void UpdateUnfinishedProgressList()
    {
        unfinishedProgresslist.Clear();
        int count = totalProgressList.Count;
        ProgressItem item = null;
        for (int i = 0; i < count; i++)
        {
            item = totalProgressList[i];
            if (item.progress < GuildBossService.Singleton.GetMaxFightTimes())
            {
                unfinishedProgresslist.Add(item);
            }
        }
    }

    private void OnDoubleBtnChange()
    {
        if (window.m_doubuleSelectBtn.selected)
            window.m_memberList.numItems = unfinishedProgresslist.Count;
        else
            window.m_memberList.numItems = totalProgressList.Count;
    }

    protected override void OnCloseBtn()
    {
        base.OnCloseBtn();
    }
}
