using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_GuildBattle;
using Data.Beans;

public class GuildBattleExchangeItem : UI_guildBattleExchangeItem {

    public t_guild_battle_exchangeBean exchangeBean;

    public new static GuildBattleExchangeItem CreateInstance()
    {
        return UI_guildBattleExchangeItem.CreateInstance() as GuildBattleExchangeItem;
    }

    public void InitView()
    {
        m_exchangeBtn.onClick.Add(OnClickExchangeBtn);
        RefreshView();
    }

    public void RefreshView()
    {
        if (exchangeBean != null)
        {
            int itemID = exchangeBean.t_cost_id;
            CommonItem item = null;
            // 显示消耗的道具信息
            item = m_costItem as CommonItem;
            item.itemId = itemID;
            item.itemNum = exchangeBean.t_cost_num;
            item.isShowNum = true;
            item.AddPopupEvent();
            Color? numColor = IsCanExchange() ? Color.white : Color.red;
            item.RefreshView(false, numColor);

            // 显示兑换的道具信息
            itemID = exchangeBean.t_item_id;
            item = m_exchangeItem as CommonItem;
            item.itemId = itemID;
            item.itemNum = exchangeBean.t_item_num;
            item.isShowNum = true;
            item.AddPopupEvent();
            item.RefreshView();
        }

        m_exchangeBtn.grayed = !IsCanExchange();
    }

    private void OnClickExchangeBtn()
    {
        if (IsCanExchange())
        {
            GuildBattleService.Singleton.ReqExchange(exchangeBean.t_id);
        }
        else
        {
            TipWindow.Singleton.ShowTip("兑换材料不足！");
        }
        
    }
    /// <summary>
    /// 能否兑换
    /// </summary>
    /// <returns></returns>
    private bool IsCanExchange()
    {
        if (exchangeBean != null)
        {
            int needNum = exchangeBean.t_item_num;
            Message.Bag.GridInfo gridInfo = BagService.Singleton.GetGridInfoByID(exchangeBean.t_item_id);
            int haveNum = gridInfo == null ? 0 : gridInfo.itemInfo.num;
            return haveNum >= needNum;
        }

        return false;
    }
}
