using UI_StriveHegemong;
using Data.Beans;
using System.Collections.Generic;
using Message.Pet;
using Message.KingFight;
using FairyGUI;

public class SH_BaQiangListItem : UI_SH_BaQiangListItem
{
    private int type;//场次类型，0为四强赛，1为半决赛，2为决赛
    private int time;//倒计时
    private int[] changci = { 71702217,71702218,71702219};//四强赛以后语言包id
    private DoActionInterval doAction;
    private FightInfo fightInfo;
    public new static SH_BaQiangListItem CreateInstance()
    {
        return (SH_BaQiangListItem)UI_SH_BaQiangListItem.CreateInstance();
    }
    public void Init(FightInfo fight,int index)
    {

        type = index - 10;
        fightInfo = fight;
        if (fightInfo.hasTime())
        {
            time = fightInfo.time;
            OnMyData();
            OnThreData();
            FillData();
        }
        else
        {
            FillData();
            OnMyData();
            OnThreData();
        }
        AddKeyEvent();
      
    }
    private void AddKeyEvent()
    {
        m_my.onClickItem.Add(On_BQ_WoDeZhongRong);
        m_ther.onClickItem.Add(On_BQ_DuiShouZhenRong);
        m_fenxiangBtn.onClick.Add(OnFenXiang);
        m_GuanZhanBtn.onClick.Add(OnGuanZhan);
        m_luxiangBtn.onClick.Add(Onluxiang);
        m_BaoXiangBtn.onClick.Add(OnOpenBox);
    }
    private void AddEvent()
    {
        GED.ED.addListener(EventID.OnTargetFightInfo,OnTherrInfo);
    }
    private void RemoveEvent()
    {
        GED.ED.removeListener(EventID.OnTargetFightInfo, OnTherrInfo);
    }
    private void FillData()
    {
        t_languageBean languageBean = ConfigBean.GetBean<t_languageBean, int>(changci[type]);
        if (languageBean == null)
        {
            Logger.err("SH_BaQiangListItem:Init:语言包内没有对应的赛程语句---" + changci[type]);
        }
        else
        {
            m_ChangCi.text = languageBean.t_content;
        }
        m_BaoXiang.visible = false;
        if (fightInfo.hasTime())
        {
            m_duizhanIcon.text = "对战";
            if (time < 0)
            {
                m_daojishi.visible = false;
                m_duizhanIcon.visible = true;
                m_BeiZhan.visible = false;
                m_GuanZhanBtn.visible = true;
            }
            else
            {
                m_GuanZhanBtn.visible = false;
                m_BeiZhan.visible = true;
                m_daojishi.visible = true;
                m_duizhanIcon.visible = false;
                m_luxiangBtn.visible = false;
                m_fenxiangBtn.visible = false;
                if (doAction == null)
                {
                    doAction = new DoActionInterval();
                    doAction.doAction(1, OnDaoJiShi);
                }
            }
        }
        else
        {
            //判断是否有战果，有则战斗完毕，没有则在匹配中
            if (fightInfo.hasResult())
            {
                m_duizhanIcon.visible = false;
                m_BaoXiang.visible = true;
                m_BeiZhan.visible = false;
                m_daojishi.visible = false;
                m_GuanZhanBtn.visible = false;
                m_luxiangBtn.visible = true;
                m_fenxiangBtn.visible = true;
            }
            else
            {
                m_duizhanIcon.text = "匹配中";
                m_BeiZhan.visible = false;
                m_daojishi.visible = false;
                m_duizhanIcon.visible = true;
                m_fenxiangBtn.visible = false;
                m_GuanZhanBtn.visible = false;
                m_luxiangBtn.visible = false;
                m_ther_level.text = 0 + "";
            }
            if (fightInfo.hasRoleId())
            {

            }
            else
            {
                languageBean = ConfigBean.GetBean<t_languageBean, int>(61801034);
                if (languageBean != null)
                    m_ther_name.text = languageBean.t_content;
                else
                    m_ther_name.text = "语言包内没有对方未上阵宠物的数据";
            }
        }
      
    }
    private void OnMyData()
    {
        //我的上阵宠物本地保存的依旧是上阵有id，没上阵位置为0，长度为10
        List<int> myRaceInfo = StriveHegemongService.Singleton.shangzhenList;
        SH_HG_Figure figure;
        for (int i = 0; i < 3; ++i)
        {
            if (myRaceInfo[type * 3 + i] != 0)
            {
                EquipedPetInfo petInfo = new EquipedPetInfo();
                PetBaseInfo petBaseInfo = PetService.Singleton.GetPetInfo(myRaceInfo[i]).basInfo;
                petInfo.color = petBaseInfo.color;
                petInfo.level = petBaseInfo.level;
                petInfo.star = petBaseInfo.star;
                petInfo.id = PetService.Singleton.GetPetInfo(myRaceInfo[i]).petId;

                figure = SH_HG_Figure.CreateInstance();
                figure.Init(petInfo, true);
                figure.scale = new UnityEngine.Vector2(0.8f, 0.9f);
                m_my.AddChild(figure);
            }
            else
            {
                figure = SH_HG_Figure.CreateInstance();
                figure.Init(null,true);
                figure.scale = new UnityEngine.Vector2(0.8f, 0.9f);
                m_my.AddChild(figure);
            }
        }
        //m_my.columnGap = 5;
        if (fightInfo.hasResult())
        {
            //如果有胜利或者失败信息
            if (fightInfo.result == 0)
            {
                //胜利
                UIGloader.SetUrl(m_wodezhankuang,"");
            }
            else if (fightInfo.result == 1)
            {
                UIGloader.SetUrl(m_wodezhankuang,"");
            }//失败
        }
        m_my_name.text = RoleService.Singleton.RoleInfo.roleInfo.roleName;
        m_my_level.text = RoleService.Singleton.RoleInfo.roleInfo.level.ToString();
    }
    private void OnThreData()
    {
        List<FightInfo> petFights = StriveHegemongService.Singleton.myRaceInfo.infos;
        FightInfo fightInfo = petFights[10 + type];
        SH_HG_Figure figure;
        if (fightInfo.roleId != -1)
        {
            for (int i = 0; i < fightInfo.petBaseInfo.Count; ++i)
            {
                figure = SH_HG_Figure.CreateInstance();
                figure.Init(fightInfo.petBaseInfo[i],true);
                //figure.height = 100;
                //figure.width = 100;
                figure.scale = new UnityEngine.Vector2(0.8f, 0.9f);
                m_ther.AddChild(figure);
            }
            if (fightInfo.petBaseInfo.Count < 3)
            {
                for (int i = 0; i < (3 - fightInfo.petBaseInfo.Count); ++i)
                {
                    figure = SH_HG_Figure.CreateInstance();
                    figure.Init(null, true);
                    //figure.height = 100;
                    //figure.width = 100;
                    figure.scale = new UnityEngine.Vector2(0.8f, 0.9f);
                    m_ther.AddChild(figure);
                }
            }
            m_ther_name.text = fightInfo.name;
            m_ther_level.text = fightInfo.level.ToString();
        }
        else if(fightInfo.roleId == -1)
        {
            for (int i = 0; i < 3; ++i)
            {
                figure = SH_HG_Figure.CreateInstance();
                figure.Init(null, true);
                figure.scale = new UnityEngine.Vector2(0.8f, 0.9f);
                m_ther.AddChild(figure);
            }
            m_ther_name.text = "未匹配到对手（非）";
            m_my_level.visible = false;
        }
      
        //m_ther.columnGap = 5;
        if (fightInfo.hasResult())
        {
            //如果有胜利或者失败信息
            if (fightInfo.result == 0)
            {
                //失败
                UIGloader.SetUrl(m_wodezhankuang, "");
            }
            else if (fightInfo.result == 1)
            { UIGloader.SetUrl(m_wodezhankuang, ""); }//胜利

        }
    }
    private void OnDaoJiShi(object obj)
    {
        time--;
        if (time < 0)
        {
            if (doAction != null)
            {
                doAction.kill();
                doAction = null;
                m_BeiZhan.visible = false;
                m_duizhanIcon.visible = false;
            }
        }
        m_daojishi.text = time / 60 + ":" + time % 60;
    }
    private void On_BQ_WoDeZhongRong(EventContext context)
    {
        //查看我的参赛阵容
        WinInfo info = new WinInfo();
        info.param = true;
        WinMgr.Singleton.Open<SH_BQ_CanSaiZhenRongWindow>(info,UILayer.Popup);
    }
    private void On_BQ_DuiShouZhenRong(EventContext context)
    {
        SH_HG_Figure figure = context.data as SH_HG_Figure;
        if (figure != null)
        {
            if (fightInfo.roleId == -1)
            {
                //请求对手信息
                TipWindow.Singleton.ShowTip("当前未匹配到玩家，无法查看阵容（非）");
            }
            else
            {
                StriveHegemongService.Singleton.OnReqFightInfo(fightInfo.roleId);
            }
        }
    }
    private void OnTherrInfo(GameEvent evt)
    {
        //填写对手参赛阵容
        WinInfo info = new WinInfo();
        info.param = false;
        WinMgr.Singleton.Open<SH_BQ_CanSaiZhenRongWindow>(info, UILayer.Popup);
    }
    private void OnFenXiang()
    {
        TipWindow.Singleton.ShowTip("分享");
    }
    private void OnGuanZhan()
    {
        TipWindow.Singleton.ShowTip("观战");
    }
    private void Onluxiang()
    {
        TipWindow.Singleton.ShowTip("录像");
    }
    private void OnOpenBox()
    {
        StriveHegemongService.Singleton.OnResOpenBox(fightInfo.index, fightInfo.boxstate);
    }
    public override void Dispose()
    {
        if (doAction != null)
        {
            doAction.kill();
            doAction = null;
        }
        fightInfo = null;
        base.Dispose();
    }
}
