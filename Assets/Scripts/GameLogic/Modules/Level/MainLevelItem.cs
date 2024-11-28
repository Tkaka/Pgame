using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_Level;
using FairyGUI;
using DG.Tweening;
using Data.Beans;
using Message.Dungeon;

public class MainLevelItem : UI_mainLevelItem {

    public int actID;

    private MainLevel parentUI;

    public new static UI_mainLevelItem CreateInstance()
    {
        return (UI_mainLevelItem)UIPackage.CreateObject("UI_Level", "mainLevelItem");
    }

    public LevelDataManager levelData
    {
        get { return parentUI.levelData; }
    }

    public void Init(MainLevel parentUI)
    {
        this.parentUI = parentUI;

        this.onClick.Clear();
        this.onClick.Add(OnClickLevelItem);
        m_anim.Play();

        RefreshView();
    }

    public void RefreshView()
    {
        RefreshLevelInfo();
        ShowAnimation();
        RefreshLevelTip();
        //RefreshQiPaoPosition();
    }

    private void RefreshQiPaoPosition()
    {
        
        if (LevelService.Singleton.NormalRecentlyID == actID)
        {
            //parentUI.SetChildIndex(this, parentUI.numChildren - 1);
            this.sortingOrder = int.MaxValue;
        }
        else
        {
            this.sortingOrder = 0;
        }
    }
    private void RefreshLevelTip()
    {
        ActInfo actInfo = LevelService.Singleton.GetActInfoByID(actID);
        m_tipText.visible = false;
        if (actInfo.star == 0)
        {
            t_dungeon_actBean actBean = ConfigBean.GetBean<t_dungeon_actBean, int>(actID);
            if (actBean != null && !string.IsNullOrEmpty(actBean.t_tip))
            {
                m_tipText.visible = true;
                m_tipText.text = actBean.t_tip;
            }
        }
        else
        {
            m_tipText.visible = false;
        }
    }
    private void RefreshLevelInfo()
    {
        MainTopItem topItem = m_topItem as MainTopItem;
        topItem.RefreshView(actID);
    }


    /// <summary>
    /// 显示将要攻打的关卡的上下抖动的动画
    /// </summary>
    private void ShowAnimation()
    {
        if (LevelService.Singleton.NormalRecentlyID == actID)
        {
            if (!m_anim.playing)
                m_anim.Play();
        }

        if (LevelService.Singleton.NormalRecentlyID != actID)
        {
            m_anim.Stop();
        }
    }

    private void OnClickLevelItem()
    {
        // 如果该关卡没有打开没反应，否则进入战斗准备界面
        if (actID <= LevelService.Singleton.NormalRecentlyID)
        {
            // 可以打
            OneParam<int> param = new OneParam<int>();
            param.value = actID;
            WinInfo winInfo = new WinInfo();
            winInfo.param = param;
            WinMgr.Singleton.Open<GuanQiaWindow>(winInfo, UILayer.Popup);
            //BattleService.Singleton.ReqFightResult(actID, 1, 3);
        }
    }

    public override void Dispose()
    {
        parentUI = null;
        base.Dispose();
    }
}
