using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_Common;
using Message.Rank;

public enum RankType
{
    TongXiangGuan = 1,           // 铜像馆
    ZhanDouLi = 2,               // 战斗力
    LevelStar = 3,               // 关卡星星
    MingRenTang = 4,             // 名人堂
    GeDouJia = 5,                // 格斗家
    SheTuan = 6,                 // 社团
    QuanHuanZhengBa = 7,          // 拳皇争霸
    JinJiChang = 8,              // 竞技场
    ZhongJiShiLian = 9,          // 终极试炼
}

public class CommonRankPanel {

    private UI_commonRankPanel rankPanel;
    private RankType rankType;
    private List<GuildRankData> guildRankDataList = new List<GuildRankData>();
    private List<RankData> commonRankDataList = new List<RankData>();
    public CommonRankPanel(UI_commonRankPanel rankPanel)
    {
        this.rankPanel = rankPanel;
        rankPanel.m_PaiHangList.SetVirtual();
        rankPanel.m_PaiHangList.itemProvider = RankListItemProvide;
        rankPanel.m_PaiHangList.itemRenderer = RankListItemRender;
    }

    private string RankListItemProvide(int index)
    {
        if (rankType == RankType.SheTuan)
        {
            return UI_rankGuildItem.URL;
        }
        else
        {
            return UI_rankCommonItem.URL;
        }
    }

    private void RankListItemRender(int index, FairyGUI.GObject obj)
    {
        if (rankType == RankType.SheTuan)
        {
            RankGuildItem item = obj as RankGuildItem;
            item.rankData = guildRankDataList[index];
            item.rankType = rankType;
            if (item.rankData != null)
                item.RefreshView();
            else
                item.InitView();
        }
        else
        {
            RankCommonItem item = obj as RankCommonItem;
            item.rankData = commonRankDataList[index];
            item.rankType = rankType;
            if (item.rankData != null)
                item.RefreshView();
            else
                item.InitView();
        }
    }


    public void RefreshPanel(object rankData, RankType rankType, bool isAdd = false)
    {
        this.rankType = rankType;

        RefreshRankData(rankData, isAdd);
        RefreshTypeText();
        RefreshRankList();
        RefreshSelfRankItem();
    }
    private void RefreshRankData(object rankData, bool isAdd)
    {
        if (rankType == RankType.SheTuan)
        {
            if (isAdd == false)
                guildRankDataList.Clear();

            guildRankDataList.AddRange(rankData as List<GuildRankData>);

        }
        else
        {
            if (isAdd == false)
                commonRankDataList.Clear();

            commonRankDataList.AddRange(rankData as List<RankData>);
        }
    }
    /// <summary>
    /// 初始话类型文本
    /// </summary>
    private void RefreshTypeText()
    {
        string type1Str = "";
        string type2Str = "";

        switch (rankType)
        {
            case RankType.TongXiangGuan:
                break;
            case RankType.ZhanDouLi:
                type1Str = "等级";
                type2Str = "战斗力";
                break;
            case RankType.LevelStar:
                type1Str = "等级";
                type2Str = "征战总的星";
                break;
            case RankType.MingRenTang:
                type1Str = "先手值";
                type2Str = "格斗家数量";
                break;
            case RankType.GeDouJia:
                type1Str = "格斗家";
                type2Str = "战斗力";
                break;
            case RankType.SheTuan:
                type1Str = "社长";
                type2Str = "总战斗力";
                break;
            case RankType.QuanHuanZhengBa:
                type1Str = "胜利场次";
                type2Str = "积分";
                break;
            case RankType.JinJiChang:
                break;
            case RankType.ZhongJiShiLian:
                break;
            default:
                break;
        }

        rankPanel.m_type1.text = type1Str;
        rankPanel.m_type2.text = type2Str;
    }
    /// <summary>
    /// 刷新排行列表
    /// </summary>
    private void RefreshRankList()
    {
        if (rankType == RankType.SheTuan)
            rankPanel.m_PaiHangList.numItems = guildRankDataList.Count;
        else
            rankPanel.m_PaiHangList.numItems = commonRankDataList.Count;
    }
    /// <summary>
    /// 刷新自己的排行Item
    /// </summary>
    private void RefreshSelfRankItem()
    {
        if (rankType == RankType.SheTuan)
        {

        }
        else
        {

        }
    }
}
