using UI_Arena;
using Message.Arena;
using System.Collections.Generic;
using UnityEngine;
using Data.Beans;
using FairyGUI;
using Message.Role;

public class SeeOtherInfoWindow : BaseWindow
{
    private UI_SeeOtherInfoWindow m_window;

    public override void OnOpen()
    {
        base.OnOpen();
        m_window = getUiWindow<UI_SeeOtherInfoWindow>();
        m_window.m_btnClose.onClick.Add(Close);
        m_window.m_btnSendMsg.onClick.Add(_OnSendMsgClick);
        m_window.m_btnAddFriend.onClick.Add(_OnAddFriend);
        InitView();
    }

    public override void InitView()
    {
        base.InitView();
        _ShowPlayerBaseInfo();

    }

    //显示玩家基础信息
    private void _ShowPlayerBaseInfo()
    {
        ResSeeOther msg = Info.param as ResSeeOther;
        if (msg == null)
            return;

        m_window.m_txtName.text = msg.playerName;
        m_window.m_txtLevel.text = msg.level + "";
        m_window.m_txtRank.text = msg.rank + "";
        m_window.m_txtVictoryCount.text = msg.victoryCount + "";
        m_window.m_txtXuanYan.text = msg.xuanYan;
        m_window.m_txtXianShou.text = msg.xianShou + "";
        m_window.m_txtFightPower.text = msg.fightPower + "";
        //m_window.m_imgIcon.url = msg.iconId + "";     //头像框后面加

        m_window.m_txtComeFrom.text = string.Format("来自社团：{0}", msg.guildName);

        //上阵宠物信息暂时空着
        for (int i = 0; i < msg.pets.Count; i++)
        {

        }

    }


    private void _OnSendMsgClick()
    {
        Debug.Log("发起聊天点击");
    }

    private void _OnAddFriend()
    {
        Debug.Log("添加好友点击");
    }

    protected override void OnClose()
    {
        base.OnClose();

    }

}