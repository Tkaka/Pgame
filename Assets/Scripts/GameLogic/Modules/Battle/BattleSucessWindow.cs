using Data.Beans;
using Message.Bag;
using Message.Dungeon;
using Message.Fight;
using UI_Battle;
using Message.Challenge;
using System.Collections.Generic;
using System;


public class BattleSucessWindow : BaseWindow
{
    private UI_BattleSucessWindow m_window;
    //private ActivityDungeonResult m_info;
    private ResFightResultInfo m_msg;
    private float m_hurtlv = 0;
  
    private int m_expNum = 0;
    private int m_coinNum = 0;          
    public override void OnOpen()
    {
        base.OnOpen();
        m_window = getUiWindow<UI_BattleSucessWindow>();
        m_window.onClick.Add(Close);
        m_msg = Info.param as ResFightResultInfo;
        if (m_msg == null)
            Close();

        //m_info = m_msg.result as ActivityDungeonResult;
        _Init();
        InitView();
    }

    private void _ShowSucessOrFailed(float hurt)
    {
        if (hurt > 0.75)
        {
            //完美及以上为成功
            m_window.m_imgBg.grayed = false;
            m_window.m_objTitle.grayed = false;
        }
        else
        {
            m_window.m_imgBg.grayed = true;
            m_window.m_objTitle.grayed = true;
        }

    }

    private void _Init()
    {
        if (m_msg.fightType == (int)EFightType.CoinDungeon || m_msg.fightType == (int)EFightType.ExpDungeon)
        {
            int monsterId = ChallegeService.Singleton.GetMonterIdByFightType((EFightType)m_msg.fightType, m_msg.fightTypeParam);
            var monsterBean = ConfigBean.GetBean<t_monsterBean, int>(monsterId);
            if (monsterBean != null)
            {
                ActivityDungeonResult info = m_msg.result as ActivityDungeonResult;
                m_hurtlv = (monsterBean.t_hp - info.hurtBlood) * 1.0f / monsterBean.t_hp;
            }
        }
    }

    public override void InitView()
    {
        base.InitView();
        List<ItemInfo> itemList;
        List<ItemInfo> totalItemList = null;
        if (m_msg.fightType == (int)EFightType.GuildBossDungeon)
        {
            GuildBossResult info = m_msg.result as GuildBossResult;
            totalItemList = info.rewards;
        }
        else
        {
            ActivityDungeonResult info = m_msg.result as ActivityDungeonResult;
            totalItemList = info.items;
        }
        LevelService.Singleton.FilterItem(totalItemList, out m_expNum, out m_coinNum, out itemList);
        
        m_window.m_lvTxt.text = RoleService.Singleton.GetRoleInfo().level + "";
        m_window.m_exp.text = m_expNum + "";
        m_window.m_gold.text = m_coinNum + "";
        ShowItems(itemList);
         
        switch ((EFightType)m_msg.fightType)
        {
            case EFightType.ExpDungeon:
                _ShowExpList();
                _ShowSucessOrFailed(m_hurtlv);
                m_window.m_objTitle.text = GetTitleDes(m_hurtlv);
                break;
            case EFightType.CoinDungeon:
                _ShowGoldList();
                _ShowSucessOrFailed(m_hurtlv);
                m_window.m_objTitle.text = GetTitleDes(m_hurtlv);
                break;
            case EFightType.HuanXiangDungeon:
                _ShowHuangXiangList();
                m_window.m_objTitle.text = "非常完美";
                break;
            case EFightType.WomanFighterDungeon:
                _ShowWomenList();
                m_window.m_objTitle.text = "非常完美";
                break;
            case EFightType.GuildBossDungeon:
                _ShowGuildBossInfo();
                m_window.m_objTitle.text = "非常完美";
                break;
        }
    }

    //帮会boss
    private void _ShowGuildBossInfo()
    {
        GuildBossResult info = m_msg.result as GuildBossResult;
        UI_objDesItem desCell = UI_objDesItem.CreateInstance();
        desCell.m_txtDes.text = string.Format("[color=#FFCC00]伤害:[/color] {0}", info.damage);
        m_window.m_desList.AddChild(desCell);

        UI_objDesItem desCel2 = UI_objDesItem.CreateInstance();
        desCel2.m_txtDes.text = string.Format("[color=#FFCC00]伤害百分比:[/color] {0}%", (int)Math.Ceiling(info.damage * 0.0001f));
        m_window.m_desList.AddChild(desCel2);
    }

    //女格斗家
    private void _ShowWomenList()
    {
        ActivityDungeonResult info = m_msg.result as ActivityDungeonResult;
        UI_objDesItem desCell = UI_objDesItem.CreateInstance();
        desCell.m_txtDes.text = string.Format("[color=#FFCC00]回合数:[/color] {0}", info.turnNum);
        m_window.m_desList.AddChild(desCell);

    }

    //显示幻象列表
    private void _ShowHuangXiangList()
    {
        ActivityDungeonResult info = m_msg.result as ActivityDungeonResult;
        UI_objDesItem desCell = UI_objDesItem.CreateInstance();
        desCell.m_txtDes.text = string.Format("[color=#FFCC00]通关评分:[/color] {0}", info.hurtBlood);
        m_window.m_desList.AddChild(desCell);

        desCell = UI_objDesItem.CreateInstance();
        desCell.m_txtDes.text = string.Format("[color=#FFCC00]{0}[/color] 位宠物相同", info.samePetNum);
        m_window.m_desList.AddChild(desCell);
    }

    private void _ShowExpList()
    {
        ActivityDungeonResult info = m_msg.result as ActivityDungeonResult;
        UI_objDesItem desCell = UI_objDesItem.CreateInstance();
        desCell.m_txtDes.text = string.Format("[color=#FFCC00]通关评分:[/color] {0}", info.hurtBlood);
        m_window.m_desList.AddChild(desCell);

        desCell = UI_objDesItem.CreateInstance();
        desCell.m_txtDes.text = string.Format("[color=#FFCC00]受损程度:[/color] {0}%", Math.Ceiling((m_hurtlv * 100)));
        m_window.m_desList.AddChild(desCell);
    }

    private void _ShowGoldList()
    {
        ActivityDungeonResult info = m_msg.result as ActivityDungeonResult;
        UI_objDesItem desCell = UI_objDesItem.CreateInstance();
        desCell.m_txtDes.text = string.Format("[color=#FFCC00]通关评分:[/color] {0}", info.hurtBlood);
        m_window.m_desList.AddChild(desCell);

        desCell = UI_objDesItem.CreateInstance();
        desCell.m_txtDes.text = string.Format("[color=#FFCC00]受损程度:[/color] {0}%", Math.Ceiling((m_hurtlv * 100)));
        m_window.m_desList.AddChild(desCell);

        desCell = UI_objDesItem.CreateInstance();
        desCell.m_txtDes.text = string.Format("[color=#FFCC00]获得金币:[/color] {0}", m_coinNum);
        m_window.m_desList.AddChild(desCell);
    }

    //获得标题描述
    private string GetTitleDes(float hurt)
    {
        string title = "";
        if (hurt >= 0 && hurt < 0.25)
            title = "一般";
        else if (hurt >= 0.25 && hurt < 0.5)
            title = "普通";
        else if (hurt >= 0.5 && hurt < 0.75)
            title = "很好";
        else if (hurt >= 0.75 && hurt < 1)
            title = "完美";
        else
            title = "非常完美";

        return title;
    }

    private void ShowItems(List<ItemInfo> itemList)
    {
     
        if (itemList != null && itemList.Count > 0)
        {
            foreach (ItemInfo item in itemList)
            {
                t_itemBean bean = ConfigBean.GetBean<t_itemBean, int>(item.id);
                if (bean != null)
                {
                    if (bean.t_type == (int)ItemType.Gold)
                    {
                        m_window.m_gold.text = item.num + "";
                        continue;
                    }
                    CommonItem itemIcon = CommonItem.CreateInstance();
                    itemIcon.itemId = item.id;
                    itemIcon.itemNum = item.num;
                    itemIcon.isShowNum = true;
                    itemIcon.SetIconScale(0.8f, 0.8f);
                    itemIcon.RefreshView();

                    m_window.m_itemList.AddChild(itemIcon);
                }
            }
        }
    }

    protected override void OnClose()
    {
        base.OnClose();
        BattleService.Singleton.QuitBattle();
    }
}