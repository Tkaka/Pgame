using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_Activity;
using FairyGUI;
using Data.Beans;

public class ActivityCardItem : UI_tiaoZhanCardItem {

    public ChallengeType type;
    bool isClick = false;
    TiaoZhanWindow parentWindow;
    public new static ActivityCardItem CreateInstance()
    {
        return UI_tiaoZhanCardItem.CreateInstance() as ActivityCardItem;
    }

    public void Init(TiaoZhanWindow parentWindow)
    {
        m_toucher.onClick.Add(OnClickItem);
        this.parentWindow = parentWindow;

        AddListener();
        InitView();
    }

    private void InitView()
    {
        InitNormalView();
        InitItemList();
    }

    private void InitNormalView()
    {
        string str = "";
        string bgIcon = "";
        switch (type)
        {
            case ChallengeType.ZhongJiShiLian:
                str = "终极试炼";
                FuncService.Singleton.SetFuncLock(this, 1801);
                m_bgLoader.grayed = !FuncService.Singleton.IsFuncOpen(1801);
                bgIcon += "slt";
                break;
            case ChallengeType.HuoDongGuanQia:
                str = "金币挑战";
                FuncService.Singleton.SetFuncLock(this, 18021);
                m_bgLoader.grayed = !FuncService.Singleton.IsFuncOpen(18021);
                bgIcon += "hdgq";
                break;
            case ChallengeType.KeLongMoShi:
                str = "克隆模式";
                FuncService.Singleton.SetFuncLock(this, 1803);
                m_bgLoader.grayed = !FuncService.Singleton.IsFuncOpen(1803);
                bgIcon += "klzdz";
                break;
            default:
                break;
        }
        m_desLabel.text = str;
        // TODO ：多倍奖励的提示
        m_activityGroup.visible = false;
        UIGloader.SetUrl(m_bgLoader, UIUtils.GetLoaderUrl(WinEnum.UI_Activity, bgIcon));
    }

    private void InitItemList()
    {
        //1801005 终极试炼展示道具  180209 活动关卡玩法展示   1803004 克隆组队战展示道具
        int globalID = 0;
        switch (type)
        {
            case ChallengeType.ZhongJiShiLian:
                globalID = 1801005;
                break;
            case ChallengeType.HuoDongGuanQia:
                globalID = 180209;
                break;
            case ChallengeType.KeLongMoShi:
                globalID = 1803004;
                break;
            default:
                break;
        }

        t_globalBean globalBean = ConfigBean.GetBean<t_globalBean, int>(globalID);
        if (globalBean != null)
        {
            if (!string.IsNullOrEmpty(globalBean.t_string_param))
            {
                string[] showItemIDArr = globalBean.t_string_param.Split('+');
                int length = showItemIDArr.Length;
                CommonItem propItem = null;
                m_propList.RemoveChildren(0, -1, true);
                Message.Bag.GridInfo gridInfo = null;
                for (int i = 0; i < length; i++)
                {
                    propItem = CommonItem.CreateInstance();
                    propItem.itemId = int.Parse(showItemIDArr[i]);
                    gridInfo = BagService.Singleton.GetGrid(propItem.itemId);
                    propItem.itemNum = gridInfo == null ? 0 : gridInfo.itemInfo.num;
                    propItem.isShowNum = false;
                    propItem.AddPopupEvent();
                    propItem.RefreshView();
                    propItem.scale = new Vector2(0.6f, 0.6f);
                    m_propList.AddChild(propItem);
                }
                m_propList.columnGap = -(int)(propItem.size.x * 0.4f) + 5;
            }
        }
    }

    private void AddListener()
    {
        GED.ED.addListener(EventID.OnResTeamFightMonsterInfo, OnResTeamFightMonsterInfo);
        GED.ED.addListener(EventID.OnResTeamFightTeamInfo, OnResTeamFightTeamInfo);
    }

    private void RemoveListener()
    {
        GED.ED.removeListener(EventID.OnResTeamFightMonsterInfo, OnResTeamFightMonsterInfo);
        GED.ED.removeListener(EventID.OnResTeamFightTeamInfo, OnResTeamFightTeamInfo);
    }

    #region 事件处理

    private void OnClickItem()
    {
        switch (type)
        {
            case ChallengeType.ZhongJiShiLian:
                if (FuncService.Singleton.TipFuncNotOpen(1801))
                {
                    UltemateTrainService.Singleton.parentWindow = parentWindow;
                    UltemateTrainService.Singleton.ReqUltemateTrialInfo();
                }
                break;
            case ChallengeType.HuoDongGuanQia:
                if (FuncService.Singleton.TipFuncNotOpen(18021))
                {
                    ChallegeService.Singleton.window = parentWindow;
                    ChallegeService.Singleton.ReqActivityActInfo();
                }
                break;
            case ChallengeType.KeLongMoShi:
                if (FuncService.Singleton.TipFuncNotOpen(1803))
                {
                    CloneTeamFightService.Singleton.ReqTeamFightInfo();
                }
                break;
            default:
                break;
        }

        isClick = true;
    }

    private void OnResTeamFightMonsterInfo(GameEvent evt)
    {
        if (type == ChallengeType.KeLongMoShi)
        {
            parentWindow.OpenChild<CloneMainWindow>(WinInfo.Create(false, parentWindow.winName, true, evt.Data));
            isClick = false;
        }
    }
    private void OnResTeamFightTeamInfo(GameEvent evt)
    {
        if(type == ChallengeType.KeLongMoShi && isClick)
        {
            parentWindow.OpenChild<CloneTeamWindow>(WinInfo.Create(false, parentWindow.winName, true, null));
            isClick = false;
        }  
    }

    #endregion;
    public override void Dispose()
    {
        RemoveListener();

        base.Dispose();
    }
}
