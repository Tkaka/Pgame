using System.Collections.Generic;
using UI_Top;
using UnityEngine;
using FairyGUI;
using Message.Rank;
using Data.Beans;
using Message.Pet;

public class Top_mainWindow : BaseWindow
{
    private UI_Top_mainWindow window;
    private List<int> roleIds;//排名
    private string[] zhandouli = {"等级","战力" };
    private string[] guanka = { "等级","征战总得星"};
    private string[] mingrentang = {"先手值","宠物数量" };
    private string[] chongwu = { "宠物", "战斗力" };
    private string[] shetuan = {"社长","总战力" };
    private string[] quanhuangzhengba = {"胜利场次","积分" };
    private string[] cur;//当前指向
    private DoActionInterval doAction;
    private int time = 5;
    public override void OnOpen()
    {
        base.OnOpen();
        window = getUiWindow<UI_Top_mainWindow>();
        AddKeyEvent();
        //请求数据
        InitView();
        TopService.Singleton.OnReqRankData(TopService.Singleton.topType, 0);
    }
    public override void AddEventListener()
    {
        base.AddEventListener();
        GED.ED.addListener(EventID.OnTopDataChange, OnDataChange);
        GED.ED.addListener(EventID.ResRoleInfo, OnRoleInfoChange);
    }
    public override void RemoveEventListener()
    {
        base.RemoveEventListener();
        GED.ED.removeListener(EventID.OnTopDataChange, OnDataChange);
        GED.ED.removeListener(EventID.ResRoleInfo, OnRoleInfoChange);
    }
    private void AddKeyEvent()
    {
        window.m_PaiHangList.SetVirtual();
        window.m_type.onChanged.Add(OnTypeChange);
        window.m_PaiHangList.itemRenderer = OnitemRenderer;
        window.m_PaiHangList.itemProvider = OnitemTiGong;
        window.m_PaiHangList.scrollPane.onPullUpRelease.Add(OnShangLa);
        window.m_CloseBtn.onClick.Add(OnCloseBtn);
        window.m_ChaKanBtn.onClick.Add(OnChaKan);
        window.m_PaiHangList.onClickItem.Add(OnChaKanXiangQing);
        FuncService.Singleton.SetFuncLock(window.m_QuanHuangBtn, 7017);
        FuncService.Singleton.SetFuncLock(window.m_guildBtn, 7016);
        FuncService.Singleton.SetFuncLock(window.m_HallBtn, 7014);
    }
    private void OnitemRenderer(int index, GObject obj)
    {
        if (window.m_type.selectedIndex != 4)
        {
            Top_ListItem listItem = obj as Top_ListItem;
            listItem.Init(index);
        }
        else
        {
            Top_GuildListItem listItem = obj as Top_GuildListItem;
            listItem.Init(index);
        }
    }
    private string OnitemTiGong(int index)
    {
        if (window.m_type.selectedIndex == 4)
        {
            return Top_GuildListItem.URL;
        }
        else
        {
            return UI_Top_ListItem.URL;
        }
    }
    private void OnTypeChange()
    {
        int index = window.m_type.selectedIndex;
        if ((int)TopService.Singleton.topType - 1 == index)
        {
            //切换的是当前页面，不会向服务器发送请求
        }
        else
        {
            if (window.m_type.selectedIndex == 5)
            {
                if (!(FuncService.Singleton.IsFuncOpen(7017)))
                {
                    window.m_type.selectedIndex = (int)(TopService.Singleton.topType) - 1;
                    return;
                }
            }
            switch (window.m_type.selectedIndex)
            {
                case 0: {TopService.Singleton.topType = TopType.Fight_Power; } break;
                case 1: {TopService.Singleton.topType = TopType.Mission_Star; } break;
                case 2: {TopService.Singleton.topType = TopType.Hall; } break;
                case 3: {TopService.Singleton.topType = TopType.Role_Fight; } break;
                case 4: {TopService.Singleton.topType = TopType.Guild;  } break;
                case 5: { TopService.Singleton.topType = TopType.King_Fight;} break;
            }
           
            //得到排行信息
            roleIds = TopService.Singleton.OnRoleIdInfo();
            if (roleIds.Count == 0)
            {
                TopService.Singleton.OnReqRankData((TopType)index + 1, 0);
                return;
            }
            else
            {
                if (time > 0)
                {
                }
                else
                { //切换新页面
                    TopService.Singleton.OnReqRankData((TopType)index + 1, 0);
                }
            }
            roleIds.Sort();
            window.m_PaiHangList.numItems = roleIds.Count;
            window.m_PaiHangList.RefreshVirtualList();
            time = 5;
            RefreshView();
            FillOne();
            OnMyData();
        }
    }
    private void OnShangLa()
    {
        int curpage = 0;
        //上拉事件
        switch (TopService.Singleton.topType)
        {
            case TopType.Fight_Power: { curpage = TopService.Singleton.Fight_page + 1; } break;
            case TopType.Mission_Star: { curpage = TopService.Singleton.Mission_page + 1; } break;
            case TopType.Hall: { curpage = TopService.Singleton.Hall_page + 1; } break;
            case TopType.Role_Fight: { curpage = TopService.Singleton.Pet_page + 1; } break;
            case TopType.Guild: { curpage = TopService.Singleton.Guild_page + 1;  } break;
            case TopType.King_Fight: { curpage = TopService.Singleton.King_page + 1; } break;
        }
        TopService.Singleton.OnReqRankData((TopType)window.m_type.selectedIndex + 1,curpage);
    }
    //接收到数据后的回调
    private void OnDataChange(GameEvent evt)
    {
        roleIds = TopService.Singleton.OnRoleIdInfo();
        roleIds.Sort();
        window.m_PaiHangList.numItems = roleIds.Count;
        window.m_PaiHangList.RefreshVirtualList();
        time = 5;
        FillOne();
        OnMyData();
        RefreshView();
    }
    public override void InitView()
    {
        base.InitView();
        int curIndex = (int)TopService.Singleton.topType;
        window.m_type.selectedIndex = curIndex - 1;
        doAction = new DoActionInterval();
        doAction.doAction(1.0f, OnDaoJiShi);
        RefreshView();
    }
    public override void RefreshView()
    {
        base.RefreshView();
        switch (window.m_type.selectedIndex)
        {
            case 0: cur = zhandouli; break;
            case 1: cur = guanka; break;
            case 2: cur = mingrentang; break;
            case 3: cur = chongwu; break;
            case 4: cur = shetuan; break;
            case 5: cur = quanhuangzhengba; break;
        }
        if (window.m_type.selectedIndex == 4)
            window.m_FenGeXian.m_juesemign.text = "社团";
        else
            window.m_FenGeXian.m_juesemign.text = "角色名";
        window.m_FenGeXian.m_paiming.text = "排名";
        window.m_FenGeXian.m_type1.text = cur[0];
        window.m_FenGeXian.m_type2.text = cur[1];
    }
    private void OnDaoJiShi(object obj)
    {
        time--;
    }
    /// <summary>
    /// 第一名数据
    /// </summary>
    private void FillOne()
    {
        string level = "等级：{0}";
        string guildname = "社长：{0}";
        
        if (TopService.Singleton.topType == TopType.Guild)
        {
            GuildRankData guild = TopService.Singleton.GetGuildData(0);
            if (guild != null)
            {
                window.m_name.text = guild.name;
                window.m_level.text = string.Format(level, guild.level);
                window.m_GuilName.text = string.Format(guildname, guild.chairManName);
            }
            else
            {
                window.m_name.text = "";
                window.m_level.text = "";
                window.m_GuilName.text = "";
            }
        }
        else
        {
            RankData rankData = TopService.Singleton.OnGetRankData(0);
            //头像加载
            t_headBean headBean = ConfigBean.GetBean<t_headBean,int>(rankData.icon);
            UIGloader.SetUrl(window.m_Icon, headBean.t_icon);
            if (rankData != null)
            {
                window.m_name.text = rankData.name;
                window.m_level.text = string.Format(level, rankData.roleLevel);
                if (string.IsNullOrEmpty(rankData.guildName))
                {
                    string weijiaru = "未加入";
                    window.m_GuilName.text = string.Format(guildname, weijiaru);
                }
                else
                    window.m_GuilName.text = string.Format(guildname, rankData.guildName);
            }
            else
            {
                window.m_name.text = "";
                window.m_level.text = "";
                window.m_GuilName.text = "";
            }
        }
    }
    private void OnChaKan()
    {
        RankData roledata = TopService.Singleton.OnGetRankData(0);
        if (TopService.Singleton.topType == TopType.Guild)
        {
            WinInfo info = new WinInfo();
            info.param = 0;
            WinMgr.Singleton.Open<Top_XiangQingWindow>(info, UILayer.Popup);
        }
        else if (TopService.Singleton.topType == TopType.Role_Fight)
        {
            //查看宠物
            TopService.Singleton.OnReqPetData(roledata.left, roledata.roleId);
        }
        else
        {
            //查看角色
            TopService.Singleton.OnJueSeXiangQing(roledata.roleId);
        }
    }
    /// <summary>
    /// 自己的数据
    /// </summary>
    private void OnMyData()
    {
        if (TopService.Singleton.topType == TopType.Guild)
        {
            GuildRankData guildRank = TopService.Singleton.OnGetMyGuildData();
            window.m_Guild_MyData.m_ziji.visible = true;
            window.m_Guild_MyData.visible = true;
            window.m_Rank_MyData.visible = false;
            OnMyGuildData(guildRank);
        }
        else
        {
            RankData rankData = TopService.Singleton.OnGetMyRankData();
            window.m_Guild_MyData.m_ziji.visible = true;
            window.m_Guild_MyData.visible = false;
            window.m_Rank_MyData.visible = true;
            OnMyRankData(rankData);
        }
    }
    /// <summary>
    /// 我的社团信息
    /// </summary>
    /// <param name="guildRank"></param>
    private void OnMyGuildData(GuildRankData guildRank)
    {

        if (RoleService.Singleton.RoleInfo.roleInfo.guildId < 0)
        {
            window.m_Guild_MyData.m_SheTuan.visible = false;
            window.m_Guild_MyData.m_weijiaru.visible = true;
            return;
        }
        window.m_Guild_MyData.m_SheTuan.visible = true;
        window.m_Guild_MyData.m_weijiaru.visible = false;
        if (guildRank != null)
        {
            if (guildRank.rank < 3)
            {
                window.m_Guild_MyData.m_paiming_number.visible = false;
                window.m_Guild_MyData.m_paiming_Icon.visible = true;
                UIGloader.SetUrl(window.m_Guild_MyData.m_paiming_Icon, OnGetIconName(guildRank.rank));
            }
            else
            {
                window.m_Guild_MyData.m_paiming_Icon.visible = false;
                window.m_Guild_MyData.m_paiming_number.visible = true;
                window.m_Guild_MyData.m_paiming_number.text = (guildRank.rank + 1).ToString();
            }
            window.m_Guild_MyData.m_guildName.text = guildRank.name;
            //UIGloader.SetUrl(m_huizhang,)社团徽章图片加载
            OnGuildType(guildRank.guildType);//类型
            window.m_Guild_MyData.m_guildLevel.text = guildRank.level.ToString();
            window.m_Guild_MyData.m_shezhang.text = guildRank.chairManName.ToString();
            window.m_Guild_MyData.m_zongzhanli.text = guildRank.fightPower.ToString();
        }
        else
        {
            window.m_Guild_MyData.m_SheTuan.visible = false;
            Logger.err("未能拿到对应排名社团数据");
            return;
        }
    }
    /// <summary>
    /// 我的排行榜信息
    /// </summary>
    /// <param name="rankData"></param>
    private void OnMyRankData(RankData rankData)
    {
        window.m_Rank_MyData.m_ziji.visible = true;
        if (rankData == null)
        {
            window.m_Rank_MyData.m_WiShangBang.text = "很遗憾未上榜";
            window.m_Rank_MyData.m_WiShangBang.visible = true;
            window.m_Rank_MyData.m_YiShangBang.visible = false;
            return;
        }
        window.m_Rank_MyData.m_WiShangBang.visible = false;
        window.m_Rank_MyData.m_YiShangBang.visible = true;
        if (rankData.rank < 3)
        {
            window.m_Rank_MyData.m_paiming_Icon.visible = true;
            UIGloader.SetUrl(window.m_Rank_MyData.m_paiming_Icon, OnGetIconName(rankData.rank));
            window.m_Rank_MyData.m_paiming_number.visible = false;
        }
        else
        {
            window.m_Rank_MyData.m_paiming_Icon.visible = false;
            window.m_Rank_MyData.m_paiming_number.visible = true;
            window.m_Rank_MyData.m_paiming_number.text = (rankData.rank + 1) + "";
        }
        window.m_Rank_MyData.m_name.text = rankData.name;
        t_titleBean titleBean = ConfigBean.GetBean<t_titleBean, int>(rankData.title);
        if (titleBean != null)
        {
            window.m_Rank_MyData.m_chenghao.text = titleBean.t_name;
        }
        else
        {
            window.m_Rank_MyData.m_chenghao.text = rankData.title + "";
        }
        if (TopService.Singleton.topType == TopType.Role_Fight)
            OnPet(rankData.left);
        else
        {
            window.m_Rank_MyData.m_left.visible = true;
            window.m_Rank_MyData.m_Pet.visible = false;
            window.m_Rank_MyData.m_left.text = rankData.left.ToString();
        }
        window.m_Rank_MyData.m_right.text = rankData.right.ToString();
    }
    /// <summary>
    /// 获得宠物头像
    /// </summary>
    /// <param name="petId"></param>
    private void OnPet(int petId)
    {
        int petid = PetService.Singleton.GetHightestFightPowerPet();
        PetInfo petInfo = PetService.Singleton.GetPetInfo(petid);
        window.m_Rank_MyData.m_left.visible = false;

        t_petBean petBean = ConfigBean.GetBean<t_petBean, int>(petId);
        if (petBean != null)
        {
            window.m_Rank_MyData.m_Pet.visible = true;
            UI_Common.UI_petItem item = ((UI_Common.UI_petItem)window.m_Rank_MyData.m_Rankpet);
            item.m_petName.visible = false;
            string icon = UIUtils.GetPetStartIcon(petId, petInfo.basInfo.star);
            UIGloader.SetUrl(item.m_iconLoader, icon);
            StarList star = new StarList(item.m_starList);
            star.SetStar(petInfo.basInfo.star);
            UIGloader.SetUrl(item.m_borderBg, UIUtils.GetLoaderUrl(WinEnum.UI_Common, UIUtils.GetBorderByQuality(petInfo.basInfo.color)));
            PetQualityDou qualityDou = item.m_petQualityDou as PetQualityDou;
            if (qualityDou != null)
                qualityDou.InitView(petInfo.basInfo.color);
            item.m_levelLabel.text = petInfo.basInfo.level.ToString();
            item.m_shangZhenGroup.visible = false;
            item.m_redPoint.visible = false;
            item.m_selectIcon.visible = false;
        }
        else
        {
            Logger.err("未能获得宠物！");
        }
    }
    private string OnGetIconName(int rank)
    {
        string name = "";
        switch (rank)
        {
            case 0: name = "ui://" + WinEnum.UI_Top + "/diyiming"; break;
            case 1: name = "ui://" + WinEnum.UI_Top + "/dierming"; break;
            case 2: name = "ui://" + WinEnum.UI_Top + "/disanming"; break;
            default: break;
        }
        return name;
    }

    /// <summary>
    /// 
    /// </summary>
    private string OnGetGuildIconName(int rank)
    {
        string name = "";
        switch (rank)
        {
            case 1: name = "ui://" + WinEnum.UI_Top + "/diyiming"; break;
            case 2: name = "ui://" + WinEnum.UI_Top + "/dierming"; break;
            case 3: name = "ui://" + WinEnum.UI_Top + "/disanming"; break;
            default: break;
        }
        return name;
    }
    private void OnGuildType(int type)
    {
        switch (type)
        {
            case 1: window.m_Guild_MyData.m_type_name.text = "休闲"; break;
            case 2: window.m_Guild_MyData.m_type_name.text = "竞技"; break;
        }
    }
    private void OnChaKanXiangQing(EventContext context)
    {
        if (TopService.Singleton.topType == TopType.Guild)
        {
            Top_GuildListItem guildlistItem = context.data as Top_GuildListItem;
            if (guildlistItem == null)
            {
                Logger.err("类型不正确无法打开");
                return;
            }
            WinInfo info = new WinInfo();
            info.param = guildlistItem.index;
            WinMgr.Singleton.Open<Top_XiangQingWindow>(info,UILayer.Popup);
            return;
        }
        Top_ListItem listItem = context.data as Top_ListItem;
        if (listItem != null)
        {
            if (TopService.Singleton.topType == TopType.Role_Fight)
            {
                listItem.OnOpenXiangQing();
            }
            else
            {
                listItem.OnJueSeXiangQing();
            }
        }
    }
    private void OnRoleInfoChange(GameEvent evt)
    {
        FuncService.Singleton.SetFuncLock(window.m_QuanHuangBtn, 7017);
        FuncService.Singleton.SetFuncLock(window.m_guildBtn, 7016);
        FuncService.Singleton.SetFuncLock(window.m_HallBtn, 7014);
    }
    protected override void OnCloseBtn()
    {
        if (doAction != null)
        {
            doAction.kill();
            doAction = null;
        }
        RemoveEventListener();
        window = null;
        roleIds = null;
        base.OnCloseBtn();
    }
}
