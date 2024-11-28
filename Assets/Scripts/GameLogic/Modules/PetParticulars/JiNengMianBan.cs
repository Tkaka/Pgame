using Message.Pet;
using Message.Rank;
using UI_PetParticulars;
using Data.Beans;
using System.Collections.Generic;

public class JiNengMianBan : UI_JiNengMianBan
{
    //得到星级在调用循环
    private PetInfo petInfo;
    private t_petBean petBean;
    public new static JiNengMianBan CreateInstance()
    {
        return (JiNengMianBan)UI_JiNengMianBan.CreateInstance();
    }
    public void Init(int petId,int type = 0)
    {
        petInfo = PetService.Singleton.GetPetInfo(petId);
        petBean = ConfigBean.GetBean<t_petBean,int>(petId);
        if (petBean != null)
        {
            if (type == 0)
            {
                if (petInfo == null)
                {
                    OnWeiHuoDe();
                }
                else
                {
                    OnYiHuoDe();
                }
            }
            else
            {
                OnPeiHangBang();
            }
        }
        else
        {
            Logger.err("JiNengMianBan:Init:传入的宠物id无法在宠物表找到" + petId);
        }
    }
    private void OnWeiHuoDe()
    {
        m_JiNengList.RemoveChildren(0, -1, true);
        int star = petBean.t_hecheng_star;
        if (string.IsNullOrEmpty(petBean.t_init_skillID))
        {
            Logger.err("未能获取到技能id" + petBean.t_id);
            return;
        }
        int[] jinengid = GTools.splitStringToIntArray(petBean.t_init_skillID);
        JiNengListItem listItem;
        for (int i = 0; i < jinengid.Length; ++i)
        {
            listItem = JiNengListItem.CreateInstance();
            if (i < star)
            {
                listItem.Init(jinengid[i], i, 1, true);
            }
            else
                listItem.Init(jinengid[i],i,1,false);
            m_JiNengList.AddChild(listItem);
        }
    }
    private void OnYiHuoDe()
    {
        m_JiNengList.RemoveChildren(0, -1, true);
        JiNengListItem listItem;
        List<SkillInfo> skillList = new List<SkillInfo>();
        skillList.AddRange(petInfo.skillInfo.skillInfos);
        skillList.Sort(SortPanl);
        for (int i = 0; i < skillList.Count; ++i)
        {
            listItem = JiNengListItem.CreateInstance();
            if (i < petInfo.basInfo.star)
            {
                listItem.Init(skillList[i].id, i, skillList[i].level, true);
            }
            else
            {
                listItem.Init(skillList[i].id, i, skillList[i].level, false);
            }
            m_JiNengList.AddChild(listItem);
        }
    }
    //由排行榜打开的宠物详情
    private void OnPeiHangBang()
    {
        Petdata petdata = TopService.Singleton.GetPetdata();
        JiNengListItem listItem;
        for (int i = 0; i < petdata.skillinfo.skillInfos.Count; ++i)
        {
            listItem = JiNengListItem.CreateInstance();
            if (i < petdata.baseinfo.star)
            {
                listItem.Init(petdata.skillinfo.skillInfos[i].id, i, petdata.skillinfo.skillInfos[i].level, true);
            }
            else
            {
                listItem.Init(petdata.skillinfo.skillInfos[i].id, i, petdata.skillinfo.skillInfos[i].level, false);
            }
            m_JiNengList.AddChild(listItem);
        }
    }
    private int SortPanl(SkillInfo a, SkillInfo b)
    {
        t_skillBean skillBeanA = ConfigBean.GetBean<t_skillBean, int>(a.id);
        t_skillBean skillBeanB = ConfigBean.GetBean<t_skillBean, int>(b.id);
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
    public override void Dispose()
    {
        petInfo = null;
        petBean = null;
        base.Dispose();
    }
}
