using Message.KingFight;
using System.Collections.Generic;
using UI_StriveHegemong;
using Data.Beans;
using Message.Pet;
using UnityEngine;

public class SH_ZR_SaiCheng : UI_SH_ZR_SaiCheng
{
    private int[] changci = { 71702207, 71702208, 71702209, 71702210, 71702211, 71702212, 71702213, 71702214, 71702215, 71702216 };//场次语言包id
    public int xiabiao;
    public int petID;

    public new static SH_ZR_SaiCheng CreateInstance()
    {
        return (SH_ZR_SaiCheng)UI_SH_ZR_SaiCheng.CreateInstance();
    }
    public void Init(int ci,BaseInfo info)
    {
        xiabiao = ci;
        if (info != null)
        {
            m_juese.m_dengji.text = info.petBaseInfo.level.ToString();
            UIGloader.SetUrl(m_juese.m_pinjie,UIUtils.GetBorderByQuality(info.petBaseInfo.color));
            UIGloader.SetUrl(m_juese.m_touxiang,UIUtils.GetPetStartIcon(info.petBaseInfo.id, info.petBaseInfo.star));
            m_juese.m_xuanzhong.visible = false;
            StarList list = new StarList((UI_Common.UI_StarList)m_juese.m_xingji);
            list.SetStar(info.petBaseInfo.star);

            m_ZhanLiZhi.text = info.fightPower.ToString();
            m_xianshouzhi.text = info.precedeValue.ToString();
        }
        else
        {
            m_juese.m_dengji.visible = false;
            m_juese.m_pinjie.grayed = true;
            UIGloader.SetUrl(m_juese.m_touxiang,"");
            m_juese.m_xingji.visible = false;
            m_juese.m_xuanzhong.visible = false;
        }
        t_languageBean languageBean = ConfigBean.GetBean<t_languageBean, int>(changci[ci]);
        if (languageBean == null)
        {
            Logger.err("SH_ZR_SaiCheng:Init:语言包内没有场次的语言！---" + changci[ci]);
        }
        else
        {
            m_Changci.text = languageBean.t_content;
        }
    }
    public void OnMyInit(int petId,int index)
    {
        xiabiao = index;
        if (petId > 0)
        {
            PetInfo info = PetService.Singleton.GetPetInfo(petId);
            m_juese.m_dengji.text = info.basInfo.level.ToString();
            UIGloader.SetUrl(m_juese.m_pinjie,UIUtils.GetBorderByQuality(info.basInfo.color));
            UIGloader.SetUrl(m_juese.m_touxiang,UIUtils.GetPetStartIcon(info.petId));
            m_juese.m_xuanzhong.visible = false;
            StarList list = new StarList((UI_Common.UI_StarList)m_juese.m_xingji);
            list.SetStar(info.basInfo.star);

            m_ZhanLiZhi.text = info.fightInfo.fightPower.ToString();
            m_xianshouzhi.text = info.priority.ToString();
        }
        else
        {
            m_juese.m_dengji.visible = false;
            m_juese.m_pinjie.grayed = true;
            UIGloader.SetUrl(m_juese.m_touxiang,"");
            m_juese.m_xingji.visible = false;
            m_juese.m_xuanzhong.visible = false;
        }
        t_languageBean languageBean = ConfigBean.GetBean<t_languageBean, int>(changci[index]);
        if (languageBean == null)
        {
            Logger.err("SH_ZR_SaiCheng:Init:语言包内没有场次的语言！---" + changci[index]);
        }
        else
        {
            m_Changci.text = languageBean.t_content;
        }
    }
    public void RefreshView()
    {
        if (petID > 0)
        {
            m_juese.m_xuanzhong.visible = false;
            PetInfo info = PetService.Singleton.GetPetInfo(petID);
            m_ZhanLiZhi.text = info.fightInfo.fightPower.ToString();
            m_xianshouzhi.text = info.priority.ToString();
        }
        else
        {
            m_juese.m_dengji.visible = false;
            m_juese.m_pinjie.grayed = true;
            UIGloader.SetUrl(m_juese.m_touxiang,"");
            m_juese.m_xingji.visible = false;
            m_juese.m_xuanzhong.visible = false;
        }
        
    }

    public void InitView(int index)
    {
        xiabiao = index;
        t_languageBean languageBean = ConfigBean.GetBean<t_languageBean, int>(changci[xiabiao]);
        if (languageBean == null)
        {
            Logger.err("SH_ZR_SaiCheng:Init:语言包内没有场次的语言！---" + changci[xiabiao]);
        }
        else
        {
            m_Changci.text = languageBean.t_content;
        }

        m_juese.draggable = true;
        //m_juese.dragBounds = new Rect(180, 140, 900, 440);
        // 初始化宠物信息
        PetInfo info = PetService.Singleton.GetPetInfo(petID);
        m_juese.m_dengji.text = info.basInfo.level.ToString();
        UIGloader.SetUrl(m_juese.m_pinjie,UIUtils.GetBorderByQuality(info.basInfo.color));
        UIGloader.SetUrl(m_juese.m_touxiang,UIUtils.GetPetStartIcon(info.petId));

        StarList list = new StarList((UI_Common.UI_StarList)m_juese.m_xingji);
        list.SetStar(info.basInfo.star);

        RefreshView();
    }
    public override void Dispose()
    {
        base.Dispose();
    }
}