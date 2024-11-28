using UI_Guild;
using Message.Guild;
using System;
using Data.Beans;
using Message.Chat;
using UnityEngine;

public class objWishPlayerCell : UI_objWishPlayerCell
{

    private WishRole m_wishInfo;
    private int m_maxWishNum;           //最大许愿数量
    private int m_maxHelpNum;           //最大赠予数量

    public new static objWishPlayerCell CreateInstance()
    {
        return UI_objWishPlayerCell.CreateInstance() as objWishPlayerCell;
    }

    public void Init(WishRole wishRole)
    {
        m_wishInfo = wishRole;
        t_promiseBean bean = ConfigBean.GetBean<t_promiseBean, int>(m_wishInfo.type);
        if (bean != null)
        {
            m_maxWishNum = bean.t_quantity;
            m_maxHelpNum = bean.t_Grant;
        }
    }


    public void RefreshView()
    {
        //m_imgIcon.m_imgIcon.url = m_wishInfo.icon;
        m_txtName.text = m_wishInfo.name;
        m_txtLevel.text = string.Format("等级{0}", m_wishInfo.level);
        if (m_wishInfo.roleId == RoleService.Singleton.GetRoleInfo().roleId)
        {
            _ShowRoleInfo();
        }
        else
        {
            _ShowPlayerInfo();
        }

    }

    private void _ShowRoleInfo()
    {
        if (m_wishInfo.itemId == -1)
        {
            //未许愿
            m_addGroup.visible = true;
            m_itemGropu.visible = false;
            m_btnForHelp.visible = false;
            m_btnGift.visible = false;
            m_wished.visible = false;
            m_btnWish.visible = true;

        }
        else
        {
            m_addGroup.visible = false;
            m_itemGropu.visible = true;
            m_btnGift.visible = false;
            m_btnWish.visible = false;
            m_wished.visible = m_wishInfo.num == 0;
            m_btnForHelp.visible = m_wishInfo.num > 0;
            _ShowWishInfo();


        }

        m_btnAdd.onClick.Add(_OnAddWishClick);
        m_btnWish.onClick.Add(_OnAddWishClick);
        m_btnForHelp.onClick.Add(_OnForHelpClick);
    }

    private void _ShowPlayerInfo()
    {
        m_addGroup.visible = false;
        m_itemGropu.visible = true;
        m_btnForHelp.visible = false;
        m_btnWish.visible = false;

        m_wished.visible = m_wishInfo.num == 0;
        m_btnGift.visible = m_wishInfo.num > 0;
        m_btnGift.grayed = m_wishInfo.presentNum >= m_maxHelpNum;

        m_btnGift.onClick.Clear();
        m_btnGift.onClick.Add(_OnGiftClick);
        _ShowWishInfo();

    }

    private void _ShowWishInfo()
    {
        CommonItem itemCell = m_ItemIcon as CommonItem;
        if (itemCell != null)
        {
            itemCell.itemId = m_wishInfo.itemId;
            itemCell.isShowNum = false;
            itemCell.RefreshView();
        }

        m_txtHaveNum.text = string.Format("拥有:{0}", BagService.Singleton.GetItemNum(m_wishInfo.itemId));
        m_txtProgress.text = string.Format("{0}/{1}", m_maxWishNum - m_wishInfo.num, m_maxWishNum);
        if (m_maxWishNum > 0)
        {
            m_progressBar.value = (m_maxWishNum - m_wishInfo.num) / m_maxWishNum;
        }
        else
        {
            Debuger.Log("最大许愿数量异常: " + m_maxWishNum);
        }
 
        

    }

    private void _OnAddWishClick()
    {
        Debuger.Log("添加许愿道具点击");
        WinMgr.Singleton.Open<WishSelectWnd>(null, UILayer.Popup);
    }


    //求助点击
    private void _OnForHelpClick()
    {
        if (GuildService.Singleton.GetForHelpIsCooling())
        {
            TipWindow.Singleton.ShowTip("求助冷却中");
            return;
        }
        ChatInfo chatInfo = new ChatInfo();
        chatInfo.channel = (int)ChatService.EChannelType.Guild;
        string strDes = UIUtils.GetStrByLanguageID(71602007);
        t_itemBean itemBean = ConfigBean.GetBean<t_itemBean, int>(m_wishInfo.itemId);
        if (itemBean != null)
        {
            string color = UIUtils.ColorToHex(UIUtils.GetItemColor(m_wishInfo.itemId));
            string name = string.Format("<font color=\"#{0}\">{1}</font>", color, itemBean.t_name);
            strDes = string.Format(strDes, name);
        }
 
        chatInfo.content = strDes;
        //chatInfo.jumpType = (int)ChatService.EJumpType.Help;
        chatInfo.roleId = RoleService.Singleton.GetRoleInfo().roleId;
        chatInfo.playerName = RoleService.Singleton.GetRoleInfo().roleName;
        chatInfo.level = RoleService.Singleton.GetRoleInfo().level;
        chatInfo.iconId = RoleService.Singleton.GetRoleInfo().headIconId;
        ChatService.Singleton.ReqSendChatInfo(chatInfo);
        GuildService.Singleton.SetForHelpCooling();
        TipWindow.Singleton.ShowTip("求助成功!");
    }

    private void _OnGiftClick()
    {
        if (RoleService.Singleton.IsExitGuildCooling())
        {
            TipWindow.Singleton.ShowTip(UIUtils.GetStrByLanguageID(61601035));
            return;
        }

        if (m_wishInfo.presentNum >= m_maxHelpNum)
        {
            TipWindow.Singleton.ShowTip("今日捐献次数不足");
            return;
        }

        t_promiseBean bean = ConfigBean.GetBean<t_promiseBean, int>(m_wishInfo.type);
        if (bean == null)
        {
            return;
        }

        string[] strRewards = GTools.splitString(bean.t_Return, ';');
        if (strRewards == null || strRewards.Length == 0)
            return;

        string des = "";
        for (int i = 0; i < strRewards.Length; i++)
        {
            int[] itemInfo = GTools.splitStringToIntArray(strRewards[i], '+');
            if (itemInfo == null || itemInfo.Length < 2)
                continue;
            t_itemBean itemBean = ConfigBean.GetBean<t_itemBean, int>(itemInfo[0]);
            if (itemBean == null)
                continue;

            if (des.Equals(""))
            {
                des = string.Format("{0}{1}", itemBean.t_name, itemInfo[1]);
            }
            else
            {
                des = string.Format("{0},{1}{2}", des, itemBean.t_name, itemInfo[1]);
            }
            
        }

        string strItemName = "碎片";
        t_itemBean tBean = ConfigBean.GetBean<t_itemBean, int>(m_wishInfo.itemId);
        if (tBean != null)
            strItemName = tBean.t_name;

        des = string.Format("是否将{0}赠予玩家{1},并获得{2}", strItemName, m_wishInfo.name, des);
        CommonTipsManager.GetInstance().ShowTips(TipsType.SingleButton, "赠与确认", des, ()=> {
            GuildService.Singleton.ReqPresent(m_wishInfo.roleId);
        });



 
        //Debuger.Log("赠送点击");
    }
}