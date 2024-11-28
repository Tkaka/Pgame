using UI_Arena;
using Message.Arena;
using System.Collections.Generic;
using UnityEngine;
using Data.Beans;
using FairyGUI;
using Message.Role;

public class RankWindow : BaseWindow
{
    private UI_RankWindow m_window;

    public override void OnOpen()
    {
        base.OnOpen();
        m_window = getUiWindow<UI_RankWindow>();
        m_window.m_btnClose.onClick.Add(Close);
         
        InitView();
    }

    public override void InitView()
    {
        base.InitView();
        _ShowRankList();
        _ShowMyInfo();
    }

    private void RenderListItem(int index, GObject obj)
    {
        RoleInfo roleInfo = RoleService.Singleton.GetRoleInfo();
        List<RankInfo> rankList = ArenaService.Singleton.GetRankList();
        if (index <= rankList.Count - 1)
        {
            UI_RankCell cell = obj as UI_RankCell;
            if (cell == null)
                return;

            cell.visible = true;
            RankInfo info = rankList[index];
            if (roleInfo.roleId == info.roleId)
            {
                //_ShowMySelfInfo(info);
                if (info.rank > 50)
                {
                    cell.visible = false;
                    return;
                }
            }

            if (info.rank == 1)
            {
                _ShowFirstPlayerInfo(info);
            }

            _OnRankCellShow(cell, rankList[index]);
        }

    }

    private void _ShowRankList()
    {
        m_window.m_rankList.SetVirtual();
        m_window.m_rankList.foldInvisibleItems = true;
        m_window.m_rankList.itemRenderer = RenderListItem;

        List<RankInfo> rankList = ArenaService.Singleton.GetRankList();
        if (rankList == null)
        {
            return;
        }
        m_window.m_rankList.numItems = rankList.Count;

        //List<RankInfo> rankList = ArenaService.Singleton.GetRankList();
        //RoleInfo roleInfo = RoleService.Singleton.GetRoleInfo();
        //for (int i = 0; i < rankList.Count; i++)
        //{
        //    RankInfo info = rankList[i];
        //    if (roleInfo.roleId == info.roleId)
        //    {
        //        _ShowMySelfInfo(info);
        //        if (info.rank > 50)
        //            continue;
        //    }

        //    if (info.rank == 1)
        //    {
        //        _ShowFirstPlayerInfo(info);
        //    }

        //    UI_RankCell cell = UI_RankCell.CreateInstance();
        //    _OnRankCellShow(cell, info);

        //    m_window.m_rankList.AddChild(cell);
        //}
    }

    private void _OnRankCellShow(UI_RankCell obj, RankInfo info)
    {
        RoleInfo roleInfo = RoleService.Singleton.GetRoleInfo();
        obj.m_imgSelf.visible = roleInfo.roleId == info.roleId ? true : false;
 

        if (info.rank <= 3)
        {
            obj.m_imgRank.visible = true;
            //读前三特有的图片
             
        }
        else
        {
            obj.m_imgRank.visible = false;
        }

        obj.m_txtRank.text = info.rank + "";
        obj.m_txtName.text = info.roleName;
        obj.m_txtSheTuan.text = info.guildName;
        obj.m_txtLevel.text = info.level + "";
        obj.m_txtFightPower.text = info.fightPower + "";

        obj.onClick.Clear();
        obj.onClick.Add(() => { ArenaService.Singleton.ReqSeeOther(info.roleId); });
 
    }



    //显示本榜最强信息
    private void _ShowFirstPlayerInfo(RankInfo info)
    {
        //m_window.m_imgIcon.url = info.iconId + "";   //读头像框
        m_window.m_txtFirstLevel.text = info.level + "";
        m_window.m_txtFirstName.text = info.roleName;
        m_window.m_txtFirstSheTuan.text = info.guildName;

        m_window.m_btnSeeInfo.onClick.Add(() => {

            ArenaService.Singleton.ReqSeeOther(info.roleId);
        });
    }

    //显示我自己信息
    private void _ShowMySelfInfo(RankInfo info)
    {
        m_window.m_myInfo.m_txtLevel.text = info.level + "";
        m_window.m_myInfo.m_txtName.text = info.roleName;
         
        m_window.m_myInfo.m_imgSelf.visible = true;
        m_window.m_myInfo.m_txtSheTuan.text = info.guildName;
        m_window.m_myInfo.m_txtRank.text = info.rank + "";
        m_window.m_myInfo.m_txtFightPower.text = info.fightPower + "";

    }

    private void _ShowMyInfo()
    {
        List<RankInfo> rankList = ArenaService.Singleton.GetRankList();
        RoleInfo roleInfo = RoleService.Singleton.GetRoleInfo();
        for (int i = 0; i < rankList.Count; i++)
        {
            RankInfo info = rankList[i];
            if (roleInfo.roleId == rankList[i].roleId)
            {
                _ShowMySelfInfo(info);
                return;
            }

        }
    }
    protected override void OnClose()
    {
        base.OnClose();

    }

}