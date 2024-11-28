using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_Level;
using FairyGUI;
using Data.Beans;
using Message.Dungeon;

public class QuickJumpLevelItem : UI_quickJumpLevelItem {

    public int levelID;

    public new static UI_quickJumpLevelItem CreateInstance()
    {
        return (UI_quickJumpLevelItem)UIPackage.CreateObject("UI_Level", "quickJumpLevelItem");
    }

    public void Init()
    {
        m_fightBtn.onClick.Add(OnFightBtnClick);
        InitView();
    }

    private void InitView()
    {
        t_dungeon_actBean actBean = ConfigBean.GetBean<t_dungeon_actBean, int>(levelID);
        if (actBean != null)
        {
            int chapterIndex = levelID / 100 % 100;
            int levelIndex = levelID % 100;
            m_nameLable.text = string.Format("{0}-{1} {2}", chapterIndex, levelIndex, actBean.t_name_id);

            // 设置星星
            ActInfo actInof = LevelService.Singleton.GetActInfoByID(levelID);
            if (actInof != null)
            {
                int count = m_starList.numChildren;
                m_starList.RemoveChildren(0, -1, true);
                for (int i = 0; i < count; i++)
                {
                    if (i < actInof.star)
                        m_starList.AddChild(UIPackage.CreateObject(WinEnum.UI_Common, "xing01"));
                    else
                        m_starList.AddChild(UIPackage.CreateObject(WinEnum.UI_Common, "xing02"));
                }
            }

            //UIGloader.SetUrl(m_iconLoarder, actBean.t_icon);
            UIGloader.SetUrl(m_iconLoader, actBean.t_icon);
        }
    }

    private void OnFightBtnClick()
    {
        // 进入战斗准备界面
        OneParam<int> param = new OneParam<int>();
        param.value = levelID;
        WinInfo winInfo = new WinInfo();
        winInfo.param = param;
        WinMgr.Singleton.Open<GuanQiaWindow>(winInfo, UILayer.Popup);
    }
}
