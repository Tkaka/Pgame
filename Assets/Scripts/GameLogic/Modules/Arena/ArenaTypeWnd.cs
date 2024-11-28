using UI_Arena;
using Message.Role;
using Message.Arena;
using UnityEngine;
using FairyGUI;
using Data.Beans;
using System.Collections.Generic;


public class ArenaTypeWnd : BaseWindow
{
    private enum EArenaType
    {
        None,
        JinJiChang,          //竞技场
        QuanHuangZhengBa,    //拳皇争霸
        SheTuanZhan,         //社团战
        ShiJieShouLing,      //世界首领
    }
    private UI_ArenaTypeWnd m_window;
    public override void OnOpen()
    {
        base.OnOpen();
        m_window = getUiWindow<UI_ArenaTypeWnd>();
        m_window.m_btnClose.onClick.Add(Close);
        InitView();
    }

    public override void InitView()
    {
        base.InitView();
        var typeBean = ConfigBean.GetBeanList<t_arena_typeBean>();
        for (int i = 0; i < typeBean.Count; i++)
        {
            t_arena_typeBean bean = typeBean[i];
            UI_objArenaTypeCell cell = UI_objArenaTypeCell.CreateInstance();
            cell.m_txtName.text = bean.t_icon;

            int []arrItems = GTools.splitStringToIntArray(bean.t_reward, '+');
            for (int index = 0; index < arrItems.Length; index++)
            {
                CommonItem iconCell = CommonItem.CreateInstance();
                iconCell.itemId = arrItems[index];
                iconCell.isShowNum = false;
                iconCell.SetIconScale(0.7f, 0.7f);
                iconCell.RefreshView();
                cell.m_RewardList.AddChild(iconCell);
            }

            switch ((EArenaType)bean.t_id)
            {
                case EArenaType.JinJiChang:
                    _RegisterRedDot("mainArean/ArenaPage", cell.m_imgRed);
                    break;
                default:
                    cell.m_imgRed.visible = false;
                    break;
            }

            int type = bean.t_id;
            m_window.m_mainList.AddChild(cell);
            cell.onClick.Add(() =>
            {
                switch ((EArenaType)type)
                {
                    case EArenaType.JinJiChang:
                        if (FuncService.Singleton.TipFuncNotOpen(1701))
                        {
                            WinMgr.Singleton.Open<ArenaMainWindow>(null, UILayer.Popup);
                        }
 
                        break;
                    case EArenaType.QuanHuangZhengBa:
                        WinMgr.Singleton.Open<SH_MainWindow>(new WinInfo(),UILayer.Popup);
                        break;
                    case EArenaType.SheTuanZhan:
                        //if (FuncService.Singleton.TipFuncNotOpen(160101))
                        //{
                        //    WinMgr.Singleton.Open<GuildBattleMianWindow>(null, UILayer.Popup);
                        //}
                        //WinMgr.Singleton.Open<GuildBattleMianWindow>(null, UILayer.Popup);
                        GuildBattleService.Singleton.ReqGuildBattleInfo();
                        break;
                    case EArenaType.ShiJieShouLing:
                        break;
                }
            });
        }

    }
    protected override void OnClose()
    {
        base.OnClose();
    }
}