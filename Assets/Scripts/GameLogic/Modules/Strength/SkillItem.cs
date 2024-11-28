using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_Strength;
using FairyGUI;
using Message.Pet;
using Data.Beans;
using Message.Role;
using System;

public class SkillItem : UI_jiNengItem
{
    
    private t_skillBean jineng;
    public  PetInfo petinfo;
    //技能当前等级
    private int gradeing;
    public int jinengId;
    private int skillpoint;
    private int type;

    StrengthSkillPanel skillPanel;

    public new static SkillItem CreateInstance()
    {
        return (SkillItem)UI_jiNengItem.CreateInstance();
    }

    //宠物，技能id,技能当前等级
    public void InitView(PetInfo pet,int jinengid,int dengji,int number,int typenumber, StrengthSkillPanel skillPanel)
    {
        this.skillPanel = skillPanel;
        petinfo = pet;
        skillpoint = number;
        if (petinfo == null)
        {
            Logger.err("没有传入宠物！");
            return;
        }
        jineng = ConfigBean.GetBean<t_skillBean, int>(jinengid);
        if (jineng == null)
        {
            Logger.err("此技能不存在！------" + jinengid);
            m_nameLabel.visible = true;
            return;
        }
        gradeing = dengji;
        type = typenumber;
        jinengId = jinengid;
        m_Shengji.onClick.Add(OnShengJi);
        m_TouXiang.onClick.Add(Onxiangqing);
        m_addBtn.onClick.Add(OnAddBtn);
        OnJieNengJiaoBiao();
        Data();
    }

    private void Data()
    {
        //名字
        if (!string.IsNullOrEmpty(jineng.t_name))
            m_nameLabel.text = jineng.t_name;
        //动画播放
        if (jineng.t_icon == null)
            Logger.err("技能图片不存在");
        else
            UIGloader.SetUrl(m_TouXiang, jineng.t_icon);
        UIGloader.SetUrl( m_BeiJing, UIUtils.GetBorderUrl(1));
        if (string.IsNullOrEmpty(ConfigBean.GetBean<t_globalBean, int>(10041).t_string_param))
        {
            Logger.err("SkillIten:Data:未能在全局表中找到宠物技能解锁信息");
            return;
        }
        int[] jiesuotiaojian = GTools.splitStringToIntArray(ConfigBean.GetBean<t_globalBean,int>(10043).t_string_param);
        if (petinfo.basInfo.star >= jiesuotiaojian[type] || jiesuotiaojian[type] == 2)
        {
            m_jinbitubiao.visible = true;
            m_goldLabel.visible = true;
            m_addBtn.visible = true;
            m_lockTipLabel.visible = false;
            if (type == 1)
            {
                if (petinfo.basInfo.star < 2)
                {
                    m_suo.visible = true;
                    m_addBtn.grayed = true;
                    m_JiNengKuang.visible = false;
                }
                else
                {
                    m_suo.visible = false;
                    m_addBtn.grayed = false;
                    m_JiNengKuang.visible = true;
                }
            }
            else
                m_suo.visible = false;
            m_TouXiang.grayed = false;
            m_skillLevelLabel.visible = true;
            m_skillLevelLabel.text = gradeing.ToString();
            //计算升级所需金币
            //根据宠物资质去全局表寻找
            t_petBean petBean = ConfigBean.GetBean<t_petBean,int>(petinfo.petId);
            if (petBean == null)
            {
                Logger.err("Skill:Data:未能根据宠物id获得宠物" + petinfo.petId);
                return;
            }
            m_goldLabel.text = (shengjijinbi(petBean.t_zizhi)).ToString();

            if (skillpoint <= 0)
            {
                m_addBtn.grayed = true;
            }
        }
        else
        {
            m_TouXiang.grayed = true;
            m_JiNengKuang.visible = false;
            m_jinbitubiao.visible = false;
            m_goldLabel.visible = false;
            m_addBtn.visible = false;
            m_lockTipLabel.visible = true;
            m_lockTipLabel.text = "宠物" + (type + 1) + "星解锁";
            m_suo.visible = true;
            m_skillLevelLabel.visible = false;
        }
        if (petinfo.basInfo.level <= gradeing)
        {
            m_addBtn.grayed = true;
        }
        
        
    }
    private void OnAddBtn()
    {
        OnShengJi();
    }
    /// <summary>
    /// 点击升级事件
    /// </summary>
    private void OnShengJi()
    {
        if (petinfo.basInfo.level == gradeing)
            TipWindow.Singleton.ShowTip("技能等级已满请提升宠物等级");
        else if (petinfo.basInfo.star <= type)
            TipWindow.Singleton.ShowTip("请提升宠物星级");
        else if (skillpoint == 0)
        {
            //购买技能点窗口
            WinInfo info = new WinInfo();
            WinMgr.Singleton.Open<JiNengDianGouMaiWindow>(info, UILayer.Popup);
        }
        else
        {
            if (type == 1)
            {
                if (petinfo.basInfo.star <= 1)
                {
                    string tishi = "";
                    t_languageBean languageBean = ConfigBean.GetBean<t_languageBean,int>(7100406);
                    if (languageBean != null)
                    {
                        TipWindow.Singleton.ShowTip(languageBean.t_content);
                    }
                    else
                    {
                        TipWindow.Singleton.ShowTip("宠物星级不足，请提升宠物星级（非）");
                    }
                }
                else
                {
                    PetService.Singleton.JiNengShengJi(petinfo.petId, type);
                    skillPanel.ResetAtrributeTipStr(jinengId, this.LocalToGlobal(this.m_skillAtrributeTipPos.position));
                }
            }
            else
            {
                PetService.Singleton.JiNengShengJi(petinfo.petId, type);
                skillPanel.ResetAtrributeTipStr(jinengId, this.LocalToGlobal(this.m_skillAtrributeTipPos.position));
            }
        }
    }
    //获取升级金币
    private int shengjijinbi(int zizhi)
    {
        t_globalBean quanju = ConfigBean.GetBean<t_globalBean,int>(30001);
        t_skill_lvup_costBean xiaohaoBean = ConfigBean.GetBean<t_skill_lvup_costBean, int>(gradeing);
        if (xiaohaoBean == null)
        {
            Logger.err("SkillItem:Shengjijinbi:宝贝技能等级有误！");
            return 0;
        }
        int number = 0;
        //全局表
        if (zizhi == 11)
            quanju = ConfigBean.GetBean<t_globalBean, int>(30001);
        else if (zizhi == 12)
            quanju = ConfigBean.GetBean<t_globalBean, int>(30002);
        else if (zizhi == 14)
            quanju = ConfigBean.GetBean<t_globalBean, int>(30003);
        else if (zizhi == 15)
            quanju = ConfigBean.GetBean<t_globalBean, int>(30004);

        t_petBean bean = ConfigBean.GetBean<t_petBean,int>(petinfo.petId);
        if (bean == null)
        {
            Logger.err("SkillItem:Shengjijinbi:未能通过宝贝id获得宝贝信息");
            return 0;
        }
        int[] bilv = GTools.splitStringToIntArray(bean.t_skill_shengji);
        if (bilv.Length < jineng.t_type)
        {
            Logger.err("无法获取技能升级所需金币星级对应缩放比例");
            return 0;
        }
        float biliA = (float)bilv[jineng.t_type - 1] / (float)10000;
        float biliB = (float)quanju.t_int_param / (float)10000;
        if (xiaohaoBean != null)
        {
            if (zizhi != 13)
                number = (int)(xiaohaoBean.t_standard * biliA * biliB);
            else
                number = (int)(xiaohaoBean.t_standard * biliA);
        }
        //返回最终金币数量
        return number;
    }
    private void Onxiangqing()
    {
        TwoParam<int, int> twoParam = new TwoParam<int, int>();
        twoParam.value1 = jinengId;
        twoParam.value2 = gradeing;
        WinInfo info = new WinInfo();
        info.param = twoParam;
        WinMgr.Singleton.Open<SkillDetailWindow>(info,UILayer.Popup);
    }
    private void OnJieNengJiaoBiao()
    {
        if (jineng.t_type == 1)
        {
            m_XiaoJiNeng_JiaoBiao.visible = true;
            m_JueJi_JiaoBiao.visible = false;
            t_globalBean globalBean = ConfigBean.GetBean<t_globalBean, int>(10044);
            if (globalBean == null)
            {
                Logger.err("全局表没有小技能角标语言包id" + 10044);
            }
            else
            {
                t_languageBean languageBean = ConfigBean.GetBean<t_languageBean, int>(int.Parse(globalBean.t_string_param));
                if (languageBean == null)
                {
                    Logger.err("语言包表没有小技能角标语言包id" + 10044);
                }
                else
                {
                    m_JieNeng_type.text = languageBean.t_content;
                }
            }
        }
        else if (jineng.t_type == 2)
        {
            m_XiaoJiNeng_JiaoBiao.visible = false;
            m_JueJi_JiaoBiao.visible = true;
            t_globalBean globalBean = ConfigBean.GetBean<t_globalBean, int>(10045);
            if (globalBean == null)
            {
                Logger.err("全局表没有小技能角标语言包id" + 10045);
            }
            else
            {
                t_languageBean languageBean = ConfigBean.GetBean<t_languageBean, int>(int.Parse(globalBean.t_string_param));
                if (languageBean == null)
                {
                    Logger.err("语言包表没有小技能角标语言包id" + 10045);
                }
                else
                {
                    m_JieNeng_type.text = languageBean.t_content;
                }
            }
        }
        else
        {
            m_XiaoJiNeng_JiaoBiao.visible = false;
            m_JueJi_JiaoBiao.visible = false;
            m_JieNeng_type.visible = false;
        }
    }
    public override void Dispose()
    {
        
        base.Dispose();
        jineng = null;
        petinfo = null;

    }
}
