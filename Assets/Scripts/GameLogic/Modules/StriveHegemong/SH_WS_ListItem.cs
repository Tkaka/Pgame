using Data.Beans;
using Message.KingFight;
using Message.Pet;
using System.Collections.Generic;
using UI_StriveHegemong;

public class SH_WS_ListItem : UI_SH_WS_ListItem
{
    private DoActionInterval doAction = null;
    private List<TeamInfo> myPetId;
    private PetInfo petinfo;
    private FightInfo fightInfo;
    private int time;//倒计时
    private int[] changci = { 71702207, 71702208, 71702209, 71702210, 71702211, 71702212, 71702213, 71702214, 71702215, 71702216 };//场次语言包id
    public new static SH_WS_ListItem CreateInstance()
    {
        return (SH_WS_ListItem)UI_SH_WS_ListItem.CreateInstance();
    }
    public void Init(FightInfo info,int session = 0)
    {
        AddKeyEvent();
        myPetId = StriveHegemongService.Singleton.teanInfo.team;
        t_languageBean languageBean = ConfigBean.GetBean<t_languageBean, int>(changci[session]);
        if (languageBean == null)
        {
            Logger.err("SH_ZR_SaiCheng:Init:语言包内没有场次的语言！---" + changci[session]);
        }
        else
        {
            m_changci.text = languageBean.t_content;
        }
        if (myPetId != null)
        {
            if (session > myPetId.Count)
            {
                petinfo = null;
            }
            else
            {
                for (int i = 0; i < myPetId.Count; ++i)
                {
                    if (myPetId[i].index == session)
                    {
                        petinfo = PetService.Singleton.GetPetInfo(myPetId[session].petId);
                        break;
                    }
                }
            }
        }
        if (info == null)
        { OnWeiPiPei(); }
        else
        {
            fightInfo = info;
            OnMyInfo();
            if (info.hasResult())
            { OnZhanDouWanCheng(); }
            else
            {
                if (info.time > 0)
                { OnYiPiPei(); }
                else if (info.time < 0)
                { OnZhanDouZhong(); }
            }
        }
      
    }
    private void AddKeyEvent()
    {
        //事件 查看我的阵容、查看对手阵容，领取宝箱、布阵按钮、观看录像、查看战斗
        m_myZhenRong.onClick.Add(OnMyZhenRong);
        m_therZhenRong.onClick.Add(OnTherZhenRong);
        m_BaoXiangBtn.onClick.Add(OnLingQuBaoXiang);
        m_luxiang.onClick.Add(OnChaKanLuXiang);
        m_guanzhan.onClick.Add(OnGuanZhan);
    }
    private void OnMyInfo()
    {
        if (petinfo != null)
        {
            UIGloader.SetUrl(m_my.m_pinjie,UIUtils.GetBorderByQuality(petinfo.basInfo.color));
            m_my.m_dengji.text = petinfo.basInfo.level.ToString();
            UIGloader.SetUrl(m_my.m_touxiang,UIUtils.GetPetStartIcon(petinfo.petId));
            if (fightInfo != null)
            {
                if (fightInfo.hasResult())
                {
                    if (fightInfo.result == 0)
                    {
                        m_my.m_zhankuang.visible = true;
                        UIGloader.SetUrl(m_my.m_zhankuang,"");//胜利图片
                    }
                    else
                    {
                        m_my.m_zhankuang.visible = true;
                        UIGloader.SetUrl(m_my.m_zhankuang,"");//失败图片
                        m_my.grayed = true;
                    }
                }
                else
                    m_my.m_zhankuang.visible = false;
            }
            SetMyStart(petinfo.basInfo.star);
            m_myName.text = RoleService.Singleton.RoleInfo.roleInfo.roleName;//我的名字
            m_myLevel.text = RoleService.Singleton.RoleInfo.roleInfo.level.ToString();
        }
        else
        {
            m_my.m_zhankuang.visible = true;
            UIGloader.SetUrl(m_my.m_zhankuang,"");//失败图片
            m_my.grayed = true;
            UIGloader.SetUrl(m_my.m_pinjie,"");
            m_my.m_dengji.text = "";
            UIGloader.SetUrl(m_my.m_touxiang,"");
            m_myName.text = "我方未上阵宠物，战斗失败";//我的名字
        }
    }
    private void OnTherInfo()
    {
        if (fightInfo.petBaseInfo.Count != 0)
        {
            UIGloader.SetUrl(m_ther.m_pinjie,UIUtils.GetBorderByQuality(fightInfo.petBaseInfo[0].color));
            m_ther.m_dengji.text = fightInfo.petBaseInfo[0].level.ToString();
            UIGloader.SetUrl(m_ther.m_touxiang,UIUtils.GetPetStartIcon(fightInfo.petBaseInfo[0].id, fightInfo.petBaseInfo[0].star));

            SetTherStart(fightInfo.petBaseInfo[0].star);
            m_therName.text = fightInfo.name;
            m_therLevel.text = fightInfo.level.ToString();
        }
        else
        {
            m_ther.m_dengji.text = "";
            m_therName.text = "本轮空直接晋级";
            m_therLevel.text = "";
        }
        if (fightInfo.hasResult())
        {
            if (fightInfo.result == 1)
            {
                m_ther.m_zhankuang.visible = true;
                UIGloader.SetUrl(m_ther.m_zhankuang,"");//胜利图片
            }
            else
            {
                m_ther.m_zhankuang.visible = true;
                UIGloader.SetUrl(m_ther.m_zhankuang,"");//失败图片
                m_ther.grayed = true;
            }
        }
        else
            m_ther.m_zhankuang.visible = false;
    }
    private void OnMyZhenRong()
    {
        WinInfo info = new WinInfo();
        info.param = true;
        WinMgr.Singleton.Open<SH_DuiShou_ZhenRongWindow>(info,UILayer.Popup);
    }
    private void OnTherZhenRong()
    {
        if (fightInfo != null && fightInfo.hasRoleId())
        {
            WinInfo info = new WinInfo();
            WinMgr.Singleton.Open<SH_DuiShou_ZhenRongWindow>(info, UILayer.Popup);
        }
        
    }
    /// <summary>
    /// 领取宝箱
    /// </summary>
    private void OnLingQuBaoXiang()
    {
        StriveHegemongService.Singleton.OnResOpenBox(fightInfo.index,fightInfo.boxstate);
    }
    /// <summary>
    /// 查看录像
    /// </summary>
    private void OnChaKanLuXiang()
    { }
    /// <summary>
    /// 观看战斗
    /// </summary>
    private void OnGuanZhan()
    { }
    /// <summary>
    /// 战斗完成，显示回放与分享
    /// </summary>
    private void OnZhanDouWanCheng()
    {
        m_HuiKan.visible = true;
        m_guanzhan.visible = false;
        m_beizhan.visible = false;
        m_JieShu.visible = true;
        m_ZhanDouZhong.visible = false;
        m_DuiZhan.visible = true;
        m_daojishi.visible = false;
        if (fightInfo.result == 0)
        {
            //加载黄金宝箱图片
        }
        else if (fightInfo.result == 1)
        {
            //加载白银宝箱的图片
        }
        OnTherInfo();
    }
    /// <summary>
    /// 战斗中，显示可观战按钮
    /// </summary>
    private void OnZhanDouZhong()
    {
        m_HuiKan.visible = false;
        m_guanzhan.visible = true;
        m_beizhan.visible = false;
        m_JieShu.visible = false;
        m_ZhanDouZhong.visible = true;
        m_DuiZhan.visible = false;
        m_daojishi.visible = false;

        OnTherInfo();
    }
    /// <summary>
    /// 已匹配，未战斗,显示倒计时
    /// </summary>
    private void OnYiPiPei()
    {
        time = fightInfo.time;
        FillTime();

        m_HuiKan.visible = false;
        m_guanzhan.visible = false;
        m_beizhan.visible = true;
        m_JieShu.visible = false;
        m_ZhanDouZhong.visible = false;

        OnTherInfo();
    }
    /// <summary>
    /// 未匹配
    /// </summary>
    private void OnWeiPiPei()
    {
        m_HuiKan.visible = false;
        m_beizhan.visible = false;
        m_ZhanDouZhong.visible = false;
        m_JieShu.visible = false;
        m_guanzhan.visible = false;

        UIGloader.SetUrl(m_ther.m_touxiang,"");
        UIGloader.SetUrl(m_ther.m_pinjie,"");
        m_ther.m_dengji.visible = false;
        m_ther.m_xingji.visible = false;
        m_ther.m_zhankuang.visible = false;
        m_shangzhen.text = "????";
    }
    private void SetMyStart(int star)
    {
        StarList starList = new StarList((UI_Common.UI_StarList)m_my.m_xingji);
        starList.SetStar(star);
    }
    private void SetTherStart(int star)
    {
        StarList starList = new StarList((UI_Common.UI_StarList)m_ther.m_xingji);
        starList.SetStar(star);
    }
    /// <summary>
    /// 加载时间
    /// </summary>
    private void FillTime()
    {
        doAction = new DoActionInterval();
        doAction.doAction(1,OnDaoJiShi,null,true);
    }
    private void OnDaoJiShi(object obj)
    {
        time--;
        if (time < 0)
        {
            doAction.kill();
            doAction = null;
            OnZhanDouZhong();
        }
        m_daojishi.text = time / 60+ ":" + time % 60;
        
    }
    /// <summary>
    /// 加载战况
    /// </summary>
    private void FillCondition()
    { }
    public override void Dispose()
    {
        if (doAction != null)
        {
            doAction.kill();
            doAction = null;
        }
        myPetId = null;
        base.Dispose();
    }
}
