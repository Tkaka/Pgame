using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_CloneTeamFight;
using Message.Challenge;

public enum CloneTeamJoinType
{
    CreatTeam = 1,          // 创建队伍
    QuickJoin = 2,          // 快速加入
}
public class CloneMainWindow : BaseWindow {

    UI_CloneMainWindow window;
    CloneItem curSelectItem;

    public CloneTeamJoinType curSelectType;

    public List<int> clonePetList { get; private set; }

    public override void OnOpen()
    {
        base.OnOpen();

        window = getUiWindow<UI_CloneMainWindow>();

        InitView();
    }

    public override void AddEventListener()
    {
        base.AddEventListener();

        GED.ED.addListener(EventID.OnResTeamFightTeamInfo, OnResTeamFightTeamInfo);
    }

    public override void RemoveEventListener()
    {
        base.RemoveEventListener();

        GED.ED.removeListener(EventID.OnResTeamFightTeamInfo, OnResTeamFightTeamInfo);
    }

    public override void InitView()
    {
        base.InitView();

        clonePetList = Info.param as List<int>;
        window.m_backBtn.onClick.Add(OnCloseBtn);
        window.m_ruleBtn.onClick.Add(OnRuleBtnClick);
        window.m_cloneTeamList.onClickItem.Add(OnClickCloneRoleItem);

        InitBaseInfo();
        InitCloneRoleList();
    }

    private void InitBaseInfo()
    {
        window.m_cardNum.text = string.Format("X{0}", GetCloneInviteCardNum());
    }

    private void InitCloneRoleList()
    {
        int count = clonePetList.Count;
        CloneItem cloneItem;
        window.m_cloneTeamList.RemoveChildren(0, -1, true);
        for (int i = 0; i < count; i++)
        {
            cloneItem = CloneItem.CreateInstance();
            window.m_cloneTeamList.AddChild(cloneItem);
            cloneItem.petID = clonePetList[i];
            cloneItem.index = i;
            cloneItem.InitView(this);
        }
    }

    private void OnRuleBtnClick()
    {
        // 打开规则面板
        WinMgr.Singleton.Open<CloneRuleWindow>(null, UILayer.Popup);
    }

    private void OnClickCloneRoleItem()
    {
        if (curSelectItem != null)
            curSelectItem.RefreshView(false);
        curSelectItem = window.m_cloneTeamList.touchItem as CloneItem;
        curSelectItem.RefreshView(true);
    }

    private void OnResTeamFightTeamInfo(GameEvent evt)
    {
        WinMgr.Singleton.Open<CloneTeamWindow>(WinInfo.Create(false, null, true, curSelectType), UILayer.Popup);
        OnCloseBtn();
    }

    #region 数据处理 **************************************************************************
    public int GetCloneInviteCardNum()
    {
        // 414008 组队邀请卡
        Message.Bag.GridInfo gridInfo = BagService.Singleton.GetGridInfoByID(414008);
        int num = gridInfo == null ? 0 : gridInfo.itemInfo.num;

        return num;
    }
    #endregion;

    protected override void OnCloseBtn()
    {
        base.OnCloseBtn();
    }
}
