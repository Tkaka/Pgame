
using UI_Strength;
using Data.Beans;
using Message.Pet;
using FairyGUI;
using Message.Role;
using System;
using System.Collections.Generic;

public class StrengthSkillPanel : TabPage
{

    UI_jiNeng _skillPanel;
    StrengthWindow _parentWindow;
    //宠物信息
    private PetInfo petInfo;
    private PetPropertyMgr propertyMgr;

    //角色信息
    private RoleInfo roleinfo;
    private DoActionInterval doAction = null;
    private TimeUtils time  = null;

    //新的时间
    private int newtime;
    //技能点数
    private int skillpoint;
    private int allskillpoint;
    private bool first;
    private int jinengdianId;

    List<string> attributeTips = new List<string>();
    UnityEngine.Vector2 pos;

    public void AddEventLisent()
    {
        GED.ED.addListener(EventID.OnJiNengDianGouMai, OnJiNengDianChange);//技能点改变
        GED.ED.addListener(EventID.OnJiNengShengJi,OnJiNengShengjI);
    }
    public void RemoveEventLisent()
    {
        GED.ED.removeListener(EventID.OnJiNengDianGouMai, OnJiNengDianChange);
        GED.ED.removeListener(EventID.OnJiNengShengJi, OnJiNengShengjI);
    }

    public StrengthSkillPanel(StrengthWindow strengthWindow)
    {
        _parentWindow = strengthWindow;
        roleinfo = RoleService.Singleton.GetRoleInfo();
        AddEventLisent();
        Init();
    }

    public StrengthDataManager StrengthData
    {
        get { return _parentWindow.strengthData; }
    }

    public void Init()
    {
        _skillPanel = _parentWindow.Window.m_jiNeng;
        OnAllSkillPoint();
        time = new TimeUtils();
        first = true;
        _skillPanel.m_jiNengList.onClickItem.Add(OnHuoDeJiNengId);
        _skillPanel.m_JiNengDianGouMai.onClick.Add(OnOpenJiNengDian);
        jinengdianId = 0;
        skillpoint = roleinfo.skillPoints;
        SkillDoAction();

        petInfo = StrengthData.CurSelectPetInfo;
        if (petInfo == null)
            return;
        propertyMgr = new PetPropertyMgr(petInfo);

        RefreshView();
    }
    private void OnJiNengDianChange(GameEvent evt)
    {
        ResSkillPointChange change = evt.Data as ResSkillPointChange;
        if (change != null)
        {
            if (change.hasBuy())
            {
                if (change.buy)
                {
                    TipWindow.Singleton.ShowTip("技能点购买成功（非）");
                }
            }
            skillpoint = change.num;
            if (change.num >= 20)
            {
                RoleService.Singleton.RoleInfo.roleInfo.skillPointsBuyCount += 1;
            }
            RefreshView();
        }
    }

    public void RefreshView()
    {
        t_petBean petBean = ConfigBean.GetBean<t_petBean, int>(petInfo.petId);
        UIGloader.SetUrl(_skillPanel.m_typeLoder,UIUtils.GetLoaderUrl(WinEnum.UI_Common, UIUtils.GetPetTypeUrl(petBean.t_type)));
        _skillPanel.m_nameLabel.text = UIUtils.GetPingJiePetName(petInfo.petId, petInfo.basInfo.color, petInfo.basInfo.star);
        _skillPanel.m_nameLabel.color = UIUtils.GetColorByQuality(petInfo.basInfo.color);
        _skillPanel.m_levelLabel.text = petInfo.basInfo.level.ToString();
        propertyMgr.RefreshFighrPowert();
        _skillPanel.m_zhanDouLiLabel.text = ((int)propertyMgr.GetFightPower()).ToString();
        
        SkillDoAction();
        if (skillpoint > 0)
        {
            _skillPanel.m_JiNengDianGouMai.visible = false;
        }
        else
        {
            _skillPanel.m_JiNengDianGouMai.visible = true;
        }
        //星级
        RefreshStarList(petInfo.basInfo.star);
        RefreshSkillList();
    }
    private void OnHuoDeJiNengId(EventContext context)
    {
        SkillItem skill = (SkillItem)context.data;
        jinengdianId = skill.jinengId;
        RefreshView();
    }

    private void RefreshStarList(int star)
    {
        int num = _skillPanel.m_starList.numChildren;
        _skillPanel.m_starList.RemoveChildren(0, -1, true);
        for (int i = 0; i < 7; i++)
        {
            if (i < star)
            {
                _skillPanel.m_starList.AddChild(UI_Xing_Liang.CreateInstance());
            }
            else
            {
                _skillPanel.m_starList.AddChild(UI_Xing_An.CreateInstance());
            }
        }
    }

    private void RefreshSkillList()
    {
        SkillItem skill;
        petInfo = StrengthData.CurSelectPetInfo;
        _skillPanel.m_jiNengList.RemoveChildren(0, -1, true);
        petInfo.skillInfo.skillInfos.Sort(SortPanl);//为技能按照技能类型排序
        for (int i = 0; i < petInfo.skillInfo.skillInfos.Count; ++i)
        {
            skill = SkillItem.CreateInstance();
            skill.InitView(petInfo, petInfo.skillInfo.skillInfos[i].id, petInfo.skillInfo.skillInfos[i].level,skillpoint,i, this);
            _skillPanel.m_jiNengList.AddChild(skill);
        }
    }
    //页签隐藏
    public override void OnHide()
    {
        RefreshView();
    }
    //页签切换
    public override void OnShow()
    {
        _skillPanel = _parentWindow.Window.m_jiNeng;
        jinengdianId = -1;
        RefreshView();
    }
    
    public override void OnClose()
    {
        RemoveEventLisent();
        _skillPanel = null;
        if (doAction != null)
        {
            doAction.kill();
            doAction = null;
        }
        _skillPanel = null;
        _parentWindow = null;
        petInfo = null;
        propertyMgr = null;
        //角色信息
        roleinfo = null;
        time = null;
    }
    //服务器回包
    public override void RefreshView(bool isShow = false)
    {
        _skillPanel = _parentWindow.Window.m_jiNeng;
        //更新玩家数据
        Logger.log("回包更新玩家数据");
      
        roleinfo = RoleService.Singleton.GetRoleInfo();
        if (roleinfo.skillPoints < allskillpoint)
        {
            if(first)
            {
                newtime = 299;
                first = false;
            }
            skillpoint--;
            skillpoint = roleinfo.skillPoints;
        }
        SkillDoAction();
        petInfo = StrengthData.CurSelectPetInfo;
        if (petInfo == null)
            return;
        propertyMgr.InitPetProperty(petInfo);
        RefreshView();
    }

    private void SheZhiJiNengDian(object obj)
    {
        //计算恢复时间
        if (newtime < 0)
        {
            t_globalBean globalBean = ConfigBean.GetBean<t_globalBean, int>(100402);
            if (globalBean == null)
            {
                Logger.err("技能点回复速度为空");
                newtime = 300 - 1;
            }
            else
            {
                newtime = globalBean.t_int_param - 1;
            }
        }
        if (skillpoint == 0)
        {
            _skillPanel.m_JiNengDian.text = skillpoint + "/" + allskillpoint;
            _skillPanel.m_JiNengDian.color = new UnityEngine.Color(255, 0, 0);
            skillpoint -= 1;
        }
        else if (skillpoint < 0)
        {
            t_languageBean languageBean = ConfigBean.GetBean<t_languageBean, int>(7100402);
            if (languageBean == null)
            {
                _skillPanel.m_JiNengDian.text = TimeUtils.FormatTime(newtime);
            }
            else
            {
                _skillPanel.m_JiNengDian.text = string.Format(languageBean.t_content, TimeUtils.FormatTime(newtime));
            }
        }
        else if(skillpoint > 0 && skillpoint < allskillpoint)
        {
            string jinengdian = skillpoint.ToString();
            t_languageBean languageBean = ConfigBean.GetBean<t_languageBean, int>(7100401);
            if (languageBean != null)
            {
                _skillPanel.m_JiNengDian.text = string.Format(languageBean.t_content, jinengdian, allskillpoint.ToString());
            }
            else
            {
                _skillPanel.m_JiNengDian.text = jinengdian;
            }
        }
        newtime--;
    }
    private void SkillDoAction()
    {
        if (skillpoint >= allskillpoint)
        {
            string jinengdian = skillpoint.ToString();
            t_languageBean languageBean = ConfigBean.GetBean<t_languageBean, int>(7100401);
            if (languageBean != null)
            {
                _skillPanel.m_JiNengDian.text = string.Format(languageBean.t_content, jinengdian, allskillpoint.ToString());
            }
            else
            {
                _skillPanel.m_JiNengDian.text = jinengdian;
            }
            _skillPanel.m_JiNengDian.color = new UnityEngine.Color(0, 255, 0);
        }
        if (skillpoint > 0 && skillpoint < allskillpoint)
        {
            string jinengdian = skillpoint.ToString();
            t_languageBean languageBean = ConfigBean.GetBean<t_languageBean, int>(7100401);
            if (languageBean != null)
            {
                _skillPanel.m_JiNengDian.text = string.Format(languageBean.t_content, jinengdian, allskillpoint.ToString());
            }
            else
            {
                _skillPanel.m_JiNengDian.text = jinengdian;
            }
            _skillPanel.m_JiNengDian.color = new UnityEngine.Color(255, 255, 255);
            //得到当前服务器时间，加上恢复一个技能点的时间，将结果保存在Roservice的对应字段中
            long currentTime = TimeUtils.currentServerDateTime().Ticks;//当前客户端时间
            DateTime dt = new DateTime(1970, 1, 1);
            TimeSpan ts = new TimeSpan(currentTime - dt.Ticks);
            //当前客户端时间
            int seconds = (int)ts.TotalSeconds;
            seconds += OnHuiFuShiJian();
            if(RoleService.Singleton.RoleInfo.roleInfo.nextPointTime == 0)
                RoleService.Singleton.RoleInfo.roleInfo.nextPointTime = seconds;
        }
        else
        {
            RoleInfo roleinfo = RoleService.Singleton.GetRoleInfo();
            long lastRefreshTime = roleinfo.nextPointTime;
            int huifu = 0;
            t_globalBean globalBean = ConfigBean.GetBean<t_globalBean, int>(100402);
            if (globalBean == null)
            {
                Logger.err("技能点回复速度为空");
                huifu = 300;
            }
            else
            {
                huifu = globalBean.t_int_param;
            }
            long currentTime = TimeUtils.currentServerDateTime().Ticks;//当前客户端时间
            DateTime dt = new DateTime(1970, 1, 1);
            TimeSpan ts = new TimeSpan(currentTime - dt.Ticks);
            //当前客户端时间
            int seconds = (int)ts.TotalSeconds;
            skillpoint = roleinfo.skillPoints;
            newtime = (int)(lastRefreshTime - seconds);

            if (doAction == null)
            {
                doAction = new DoActionInterval();
                doAction.doAction(1, SheZhiJiNengDian,null,true);
            }
        }
    }
    private void OnJiNengShengjI(GameEvent evt)
    {
        int skillId = (int)evt.Data;
        SkillItem skill;
        RefreshView();
        AttributeTipShow.Singleton.ShowTips(pos, attributeTips);
    }
    //获取总的技能点数
    private void OnAllSkillPoint()
    {
        t_role_level_upBean bean = ConfigBean.GetBean<t_role_level_upBean,int>(roleinfo.level);
        if (bean != null)
        {
            allskillpoint = bean.t_skillpoint;
        }
        else
        {
            Logger.err("未能从训练家成长表获得技能点上限");
            allskillpoint = 20;
        }
    }
    private int OnHuiFuShiJian()//技能点恢复时间，秒
    {
        return 300;
    }
    /// <summary>
    /// 设置技能升级显示的属性文本
    /// </summary>
    public void ResetAtrributeTipStr(int skillID, UnityEngine.Vector2 pos)
    {
        this.pos = pos;
        attributeTips.Clear();
        t_skillBean skillBean = ConfigBean.GetBean<t_skillBean, int>(skillID);
        if (skillBean != null)
        {
            //判断技能类型，主动或者被动
            List<int> skillEffectIDs = new List<int>();
            if (skillBean.t_type < 3)
            {
                // 主动技能
                skillEffectIDs.Add(skillBean.t_main_effect_id);
            }
            else
            {
                // 被动技能
                int[] effectIDS = GTools.splitStringToIntArray(skillBean.t_bd_effect_id);
                if (effectIDS.Length != 0 && effectIDS[0] != 0)
                    skillEffectIDs.AddRange(effectIDS);
            }

            int count = skillEffectIDs.Count;
            t_skill_effectBean effectBean;
            t_attr_nameBean attrNameBean;
            string tipStr = "";
            for (int i = 0; i < count; i++)
            {
                effectBean = ConfigBean.GetBean<t_skill_effectBean, int>(skillEffectIDs[i]);
                if (effectBean != null)
                {
                    if (effectBean.t_property1 != 0)
                    {
                        attrNameBean = ConfigBean.GetBean<t_attr_nameBean, int>(effectBean.t_property1);
                        tipStr = string.Format("{0}+{1}%", attrNameBean.t_name_id, (effectBean.t_param1_grow * 0.01f).ToString("0.00"));
                        attributeTips.Add(tipStr);
                    }

                    if (effectBean.t_property2 != 0)
                    {
                        attrNameBean = ConfigBean.GetBean<t_attr_nameBean, int>(effectBean.t_property2);
                        tipStr = string.Format("{0}+{1}%", attrNameBean.t_name_id, (effectBean.t_param2_grow * 0.01f).ToString("0.00"));
                        attributeTips.Add(tipStr);
                    }
                }
            }
        }
    }
    private void OnOpenJiNengDian()
    {
        WinInfo info = new WinInfo();
        WinMgr.Singleton.Open<JiNengDianGouMaiWindow>(info, UILayer.Popup);
    }
    private int SortPanl(SkillInfo a, SkillInfo b)
    {
        t_skillBean skillBeanA = ConfigBean.GetBean<t_skillBean,int>(a.id);
        t_skillBean skillBeanB = ConfigBean.GetBean<t_skillBean,int>(b.id);
        if (skillBeanA == null || skillBeanB == null)
        {
            Logger.err("未能在技能表找到对应技能id---" + "a:" + a.id + "b:" + b.id);
            return 0;
        }

        if (skillBeanA.t_type > skillBeanB.t_type)
            return 1;
        else if (skillBeanA.t_type < skillBeanB.t_type)
            return -1;
        else
            return 0;
    }
}
