using Data.Beans;
using Message.Pet;
using Message.Rank;
using UI_PetParticulars;
using UnityEngine;

public class ShuXingMianBan
{
    private UI_ShuXingMianBan window;
    private int petid;
    private  t_petBean petBean;
    private PetInfo petInfo;
    public void Init(UI_ShuXingMianBan win,int id,int type = 0)
    {
        window = win;
        petid = id;
        if (type == 0)
        {
            petInfo = PetService.Singleton.GetPetInfo(petid);
            if (petInfo == null)
            {
                petBean = ConfigBean.GetBean<t_petBean, int>(petid);
                if (petBean == null)
                {
                    Logger.err("ShuXingMianBan:Init:无法获的宠物数据，请检查宠物id是否正确" + petid);
                    return;
                }
                OnWeiHuoDeData();
            }
            else
            {
                OnYiHuoDe();
            }
        }
        else if (type == 1)
        {
            OnPaiHangBang();
        }
    }
    /// <summary>
    /// ActorPropertyType,语言包id
    /// </summary>
    private void OnYiHuoDe()
    {
        
        window.m_GongJiZhi.text = Mathf.CeilToInt(PetService.Singleton.GetPetPropertyValue(petid,PropertyType.Atk)) + "";                                   //public int atk; // 攻击
        window.m_ShengMingZhi.text = Mathf.CeilToInt(PetService.Singleton.GetPetPropertyValue(petid, PropertyType.Hp)) + "";
        window.m_fangyu.text = Mathf.CeilToInt(PetService.Singleton.GetPetPropertyValue(petid, PropertyType.Def)) + "";
        window.m_BaoJiLvZhi.text = Mathf.CeilToInt((PetService.Singleton.GetPetPropertyValue(petid, PropertyType.BaoJiLv) / 10000 * 100)) + "" + "%";                                    //public int def; // 防御
        window.m_KangBaoLvZhi.text = Mathf.CeilToInt((PetService.Singleton.GetPetPropertyValue(petid, PropertyType.KangBaoJiLv) / 10000 * 100)) + "" + "%";
        window.m_BaoJiQiangDuZhi.text = Mathf.CeilToInt(PetService.Singleton.GetPetPropertyValue(petid, PropertyType.BaoJiQiangDu)) + "";                        //public int hp; // 血量
        window.m_GeDangLvZhi.text = Mathf.CeilToInt((PetService.Singleton.GetPetPropertyValue(petid, PropertyType.GeDangLv) / 10000 * 100)) + "%";
        window.m_PoJiLvZhi.text = Mathf.CeilToInt((PetService.Singleton.GetPetPropertyValue(petid, PropertyType.PoJiLv) / 10000 * 100)) + "%";                           //public int critical; // 暴击
        window.m_GeDangQiangDuZhi.text = Mathf.CeilToInt((PetService.Singleton.GetPetPropertyValue(petid, PropertyType.GeDangQiangDu))) + "";
        window.m_ShangHaiLvZhi.text = Mathf.CeilToInt((PetService.Singleton.GetPetPropertyValue(petid, PropertyType.ShangHaiLv) / 10000 * 100)) + "%";                      //public int antiCritical; // 抗暴
        window.m_MianShangLvZhi.text = Mathf.CeilToInt((PetService.Singleton.GetPetPropertyValue(petid, PropertyType.MianShangLv) / 10000 * 100)) + "%";
    }                                                                                                     //public int criticalDamage; // 暴伤
    private void OnWeiHuoDeData()//未获得
    {                                                                                                        //public int block; // 格挡
        //从服务器获得后的数据经过计算得到
        window.m_GongJiZhi.text = Mathf.CeilToInt(petBean.t_atk) + "";
        window.m_ShengMingZhi.text = Mathf.CeilToInt(petBean.t_hp) + "";
        window.m_fangyu.text = Mathf.CeilToInt(petBean.t_def) + "";
        window.m_BaoJiLvZhi.text = Mathf.CeilToInt(((float)petBean.t_baoji / 10000 * 100)) + "%";
        window.m_KangBaoLvZhi.text = Mathf.CeilToInt(((float)petBean.t_anti_baoji / 10000 * 100)) + "%";
        window.m_BaoJiQiangDuZhi.text = Mathf.CeilToInt((petBean.t_baoji_strength)) + "";
        window.m_GeDangLvZhi.text = Mathf.CeilToInt(((float)petBean.t_gedang/10000 * 100)) + "%";
        window.m_PoJiLvZhi.text = Mathf.CeilToInt(((float)petBean.t_poji/10000 * 100)) + "%";
        window.m_GeDangQiangDuZhi.text = Mathf.CeilToInt(((float)petBean.t_gedang_strength)) + "";
        window.m_ShangHaiLvZhi.text = Mathf.CeilToInt(((float)petBean.t_shanghai / 10000 * 100)) + "%";
        window.m_MianShangLvZhi.text = Mathf.CeilToInt(((float)petBean.t_mianshang / 10000 * 100)) + "%";
    }                                                                                                     //public int antiBlock; // 破击
    private void OnPaiHangBang()
    {
        window.m_GongJiZhi.text = OnGetPetAttribute(PropertyType.Atk) + "";
        window.m_ShengMingZhi.text = OnGetPetAttribute(PropertyType.Hp) + "";
        window.m_fangyu.text = OnGetPetAttribute(PropertyType.Def) + "";
        window.m_BaoJiLvZhi.text = (OnGetPetAttribute(PropertyType.BaoJiLv) / 10000 * 100) + "%";
        window.m_KangBaoLvZhi.text = (OnGetPetAttribute(PropertyType.KangBaoJiLv) / 10000 * 100) + "%";
        window.m_BaoJiQiangDuZhi.text = (OnGetPetAttribute(PropertyType.BaoJiQiangDu)) + "";
        window.m_GeDangLvZhi.text = (OnGetPetAttribute(PropertyType.GeDangLv) / 10000 * 1000) + "%";
        window.m_PoJiLvZhi.text = (OnGetPetAttribute(PropertyType.PoJiLv) / 10000 * 100) + "%";
        window.m_GeDangQiangDuZhi.text = (OnGetPetAttribute(PropertyType.GeDangQiangDu)) + "";
        window.m_ShangHaiLvZhi.text = (OnGetPetAttribute(PropertyType.ShangHaiLv) / 10000 * 100) + "%";
        window.m_MianShangLvZhi.text = (OnGetPetAttribute(PropertyType.MianShangLv) / 10000 * 100) + "%";
    }
    private float OnGetPetAttribute(PropertyType property)
    {
        Petdata petdata = TopService.Singleton.GetPetdata();
        for (int i = 0; i < petdata.rank.Count; ++i)
        {
            if (petdata.rank[i].id == (int)property)
            {
                return petdata.rank[i].value;
            }
        }
        return 0;
    }
    public void Close()
    {                                                                                                      //public int blockValue; // 格挡强度
        window = null;
        petBean = null;
        petInfo = null;                                                                                      //public int damageDeppen; // 伤害加深
    }
}                                                                                                           //public int damageAvoid; // 伤害减免

                                                                                                            //public int fightPower; // 战力