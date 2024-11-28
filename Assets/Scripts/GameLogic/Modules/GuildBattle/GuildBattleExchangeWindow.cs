using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_GuildBattle;
using Data.Beans;

public class GuildBattleExchangeWindow : BaseWindow {

    UI_GuildBattleExchangeWindow window;

    List<t_guild_battle_exchangeBean> exchangeBeanList;

    public override void OnOpen()
    {
        base.OnOpen();

        window = getUiWindow<UI_GuildBattleExchangeWindow>();

        InitView();
    }

    public override void InitView()
    {
        base.InitView();

        InitBaseInfo();
        InitExchangeList();
    }

    private void InitBaseInfo()
    {

    }

    private void InitExchangeList()
    {
        window.m_exchangeList.SetVirtual();
        window.m_exchangeList.itemRenderer = ExchangeListItemRender;

        exchangeBeanList = ConfigBean.GetBeanList<t_guild_battle_exchangeBean>();
        window.m_exchangeList.numItems = exchangeBeanList.Count;
    }

    private void ExchangeListItemRender(int index, FairyGUI.GObject obj)
    {
        GuildBattleExchangeItem item = obj as GuildBattleExchangeItem;
        if (item.exchangeBean == null)
        {
            item.exchangeBean = exchangeBeanList[index];
            item.InitView();
        }
        else
            item.RefreshView();
    }
}
