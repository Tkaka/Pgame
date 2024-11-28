using Data.Beans;
using Message.Pet;
using Message.Rank;
using UI_PetParticulars;

public class XinXiMianBan : UI_XinXiMianBan
{
    private UI_XinXiMianBan window;
    private  t_petBean petBen;
    private PetInfo petInfo;
    private bool xianshixiangqing = false;
    public new static XinXiMianBan CreateInstance()
    {
       
        return (XinXiMianBan)UI_XinXiMianBan.CreateInstance();
    }
    public void Init(int petid,int type = 0)//1、排行榜
    {
        m_XiangQingBtn.onClick.Add(OnChaKanXiangQing);
        petBen = ConfigBean.GetBean<t_petBean, int>(petid);
        if (petBen == null)
        {
            Logger.err("宠物表内没有此宠物id");
            return;
        }
        if (type == 0)
        {
            petInfo = PetService.Singleton.GetPetInfo(petid);
            if (PetService.Singleton.GetPetLevel(petid) == 0)
            {
                OnWeiHuoDe();
            }
            else
            {
                OnYiHuoDe();
            }
            xianshixiangqing = false;
            OnChaKanXiangQing();
        }
        else
        {
            OnRankPet();
        }
    }
    private void OnWeiHuoDe()
    {
        m_ZhanLi.visible = false;
        m_DengJi.text = 1 + "";
        m_GongJiLi.text = petBen.t_atk + "";
        m_FangYuLi.text = petBen.t_def + "";
        m_ShengMingZhi.text = petBen.t_hp + "";
        if (petBen.t_fragment_id != 0)
        {
            t_itemBean itemBean = ConfigBean.GetBean<t_itemBean,int>(petBen.t_fragment_id);
            if (itemBean != null)
            {
                if (itemBean != null)
                {
                    if (string.IsNullOrEmpty(itemBean.t_value))
                    {

                    }
                    else
                    {
                        int[] suipian = GTools.splitStringToIntArray(itemBean.t_value);
                        m_SuiPianJinDu.max = suipian[0];
                        int dangqian = BagService.Singleton.GetItemNum(itemBean.t_id);
                        m_SuiPianJinDu.value = dangqian;
                        m_jindu.text = dangqian + "/" + suipian[0];
                    }
                }
            }
        }
        t_globalBean globalBean = ConfigBean.GetBean<t_globalBean, int>(3);
        if (globalBean != null)
        {
            int[] zizhi = GTools.splitStringToIntArray(globalBean.t_string_param);
            int lenth = petBen.t_zizhi - 10;
            t_languageBean languageBean;
            if (zizhi.Length < lenth)
            {
                languageBean = ConfigBean.GetBean<t_languageBean, int>(zizhi[zizhi.Length - 1]);
            }
            else
            {
                languageBean = ConfigBean.GetBean<t_languageBean, int>(zizhi[lenth - 1]);
            }
            if (languageBean != null)
            {
                m_ZiZhi.text = languageBean.t_content;
            }
        }
    }
    private void OnYiHuoDe()
    {
        m_ZhanDouLizhi.text = PetService.Singleton.GetPetFightPower(petInfo.petId) + "";
        m_DengJi.text = petInfo.basInfo.level + "";
        t_globalBean globalBean = ConfigBean.GetBean<t_globalBean, int>(3);
        if (globalBean != null)
        {
            int[] zizhi = GTools.splitStringToIntArray(globalBean.t_string_param);
            int lenth = petBen.t_zizhi - 10;
            t_languageBean languageBean;
            if (zizhi.Length < lenth)
            {
                languageBean = ConfigBean.GetBean<t_languageBean, int>(zizhi[zizhi.Length - 1]);
            }
            else
            {
                languageBean = ConfigBean.GetBean<t_languageBean, int>(zizhi[lenth - 1]);
            }
            if (languageBean != null)
            {
                m_ZiZhi.text = languageBean.t_content;
            }
        }
        m_GongJiLi.text = ((int)(PetService.Singleton.GetPetPropertyValue(petInfo.petId,PropertyType.Atk))).ToString();
        m_FangYuLi.text = ((int)PetService.Singleton.GetPetPropertyValue(petInfo.petId, PropertyType.Def)) + "";
        m_ShengMingZhi.text = (int)PetService.Singleton.GetPetPropertyValue(petInfo.petId, PropertyType.Hp) + "";
        if (petBen.t_fragment_id != 0)
        {
            t_itemBean itemBean = ConfigBean.GetBean<t_itemBean, int>(petBen.t_fragment_id);
            if (itemBean != null)
            {
                if (itemBean != null)
                {
                    if (string.IsNullOrEmpty(itemBean.t_value))
                    {

                    }
                    else
                    {
                        int[] suipian = GTools.splitStringToIntArray(itemBean.t_value);
                        m_SuiPianJinDu.max = suipian[0];
                        int dangqian = BagService.Singleton.GetItemNum(itemBean.t_id);
                        m_SuiPianJinDu.value = dangqian;
                        m_jindu.text = dangqian + "/" + suipian[0];
                    }
                }
            }
        }
    }
    private void OnRankPet()
    {
        Petdata petdata = TopService.Singleton.GetPetdata();
        m_ZhanDouLizhi.text = petdata.fightPower + "";
        m_DengJi.text = petdata.baseinfo.level + "";
        t_globalBean globalBean = ConfigBean.GetBean<t_globalBean, int>(3);
        if (globalBean != null)
        {
            int[] zizhi = GTools.splitStringToIntArray(globalBean.t_string_param);
            int lenth = petBen.t_zizhi - 10;
            t_languageBean languageBean;
            if (zizhi.Length < lenth)
            {
                languageBean = ConfigBean.GetBean<t_languageBean, int>(zizhi[zizhi.Length - 1]);
            }
            else
            {
                languageBean = ConfigBean.GetBean<t_languageBean, int>(zizhi[lenth - 1]);
            }
            if (languageBean != null)
            {
                m_ZiZhi.text = languageBean.t_content;
            }
        }
        m_GongJiLi.text = OnGetPetAttribute(PropertyType.Atk).ToString();
        m_FangYuLi.text = OnGetPetAttribute(PropertyType.Def).ToString();
        m_ShengMingZhi.text = OnGetPetAttribute(PropertyType.Hp).ToString();
    }
    private void OnChaKanXiangQing()
    {
        string dakai = "查看详情";
        string guanbi = "关闭详情";
        if (xianshixiangqing)
        {
            m_XiangQingBtn.m_miaoshu.text = guanbi;
        }
        else
        {
            m_XiangQingBtn.m_miaoshu.text = dakai;
        }
        GED.ED.dispatchEvent(EventID.OnOpenXiangQingMianBan, xianshixiangqing);
        xianshixiangqing = !xianshixiangqing;
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
    {
        window = null;
        petBen = null;
    }
}
