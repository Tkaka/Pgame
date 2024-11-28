using UI_Chat;
using FairyGUI;
using UnityEngine;
using System;
using Message.Chat;
using Message.Role;
using System.Collections.Generic;
using Data.Beans;

public class TrumpetWnd : BaseWindow
{
    private UI_TrumpetWnd m_window;
    private int m_normalTrumpetItemId = ConfigBean.GetBean<t_globalBean, int>(502006).t_int_param;         //普通喇叭道具ID
    private int m_supperTrumpetItemId = ConfigBean.GetBean<t_globalBean, int>(502007).t_int_param;   //尊贵喇叭
    private int m_comsumeDiamondNum = ConfigBean.GetBean<t_globalBean, int>(502005).t_int_param;
    private int m_curSelectColor = 1;
    public override void OnOpen()
    {
        base.OnOpen();
        m_window = getUiWindow<UI_TrumpetWnd>();
        m_window.m_btnClose.onClick.Add(Close);
        m_window.m_btnLeft.onClick.Add(_OnLeftClick);
        m_window.m_btnRight.onClick.Add(_OnRightClick);
        m_window.m_btnColor.onClick.Add(_OnRightClick);
        m_window.m_btnGift.onClick.Add(_OnGiftClick);
        m_window.m_btnSend.onClick.Add(_OnSendClick);
        InitView();


    }

    public override void InitView()
    {
        base.InitView();
        _ShowNormalPageInfo();
        _ShowSpecialPageInfo();
    }

    //显示普通喇叭页面信息
    private void _ShowNormalPageInfo()
    {
        m_window.m_txtTrumpetNum.text = BagService.Singleton.GetItemNum(m_normalTrumpetItemId) + "";
        m_window.m_txtDiamondNum.text = 10 + "";

    }

    //显示尊享页面信息
    private void _ShowSpecialPageInfo()
    {
        int trumpetNum = BagService.Singleton.GetItemNum(m_supperTrumpetItemId);
        m_window.m_txtComsumeTrumpet.text = string.Format("1/{0}", trumpetNum);

        var cbean = ConfigBean.GetBean<t_trumpet_colorBean, int>(m_curSelectColor);
        if (cbean != null)
        {
            m_window.m_btnColor.m_txtColor.text = cbean.t_color_icon;
        }

        m_window.m_colorList.RemoveChildren(0, -1, true);
        var beans = ConfigBean.GetBeanList<t_trumpet_colorBean>();
        for (int i = 0; i < beans.Count; i++)
        {
            t_trumpet_colorBean bean = beans[i];
            UI_btnColor cell = UI_btnColor.CreateInstance();
            //cell.m_imgColor
            cell.m_txtColor.text = bean.t_color_icon;
            m_window.m_colorList.AddChild(cell);

            cell.onClick.Add(() => {
                m_curSelectColor = bean.t_id;
                m_window.m_btnColor.m_txtColor.text = bean.t_color_icon;
            });
        }

    }

    private void _OnLeftClick()
    {
        m_window.m_colorControl.selectedIndex = 0;
    }


    private void _OnRightClick()
    {
        m_window.m_colorControl.selectedIndex = 1;
    }

    private void _OnGiftClick()
    {

    }

    private void _OnSendClick()
    {
        ChatInfo chatInfo = new ChatInfo();
        chatInfo.bellColor = m_curSelectColor;
        chatInfo.bellFontType = m_window.m_fontControl.selectedIndex;
        chatInfo.channel = (int)ChatService.EChannelType.World;
        chatInfo.content = m_window.m_txtInput.text;
        chatInfo.iconId = 0;            //头像框
        chatInfo.isBell = true;
        chatInfo.level = RoleService.Singleton.GetRoleInfo().level;
        chatInfo.playerName = RoleService.Singleton.GetRoleInfo().roleName;
        chatInfo.roleId = RoleService.Singleton.GetRoleInfo().roleId;
        chatInfo.vipLevel = RoleService.Singleton.GetRoleInfo().vip;

        int consumeType = 0;
        if (m_window.m_TrumpetTypeControl.selectedIndex == 1)
        {
            //尊享喇叭
            consumeType = 2;
            chatInfo.bellStyle = m_window.m_moveControl.selectedIndex;
            if (BagService.Singleton.GetItemNum(m_supperTrumpetItemId) <= 0)
            {
                TipWindow.Singleton.ShowTip("尊享喇叭不足");
                return;
            }
        }
        else
        {
            if (BagService.Singleton.GetItemNum(m_normalTrumpetItemId) > 0)
            {
                consumeType = 1;
            }
            else
            {
                if (RoleService.Singleton.GetCurrencyNum((int)RoleService.ECurrencyType.DIAMOND) >= m_comsumeDiamondNum)
                {
                    consumeType = 0;
                }
                else
                {
                    TipWindow.Singleton.ShowTip("宝石数量不足");
                    return;
                }
            }
        }

        ChatService.Singleton.ReqSendChatInfo(chatInfo, consumeType);
    }


    protected override void OnClose()
    {
        base.OnClose();
    }
}