using UI_BuZhen;
using Data.Beans;
using Message.Pet;
using UnityEngine;
using Message.Challenge;

public class ShangZhenListItem : UI_YiShangZhen
{
    public int petid;
    public bool isEnterID;

    public string packageName;

    public string winName;

    public new static ShangZhenListItem CreateInstance()
    {
        return (ShangZhenListItem)UI_YiShangZhen.CreateInstance();
    }

    public void InitView(bool isGet, ShangZhenSelectType type)
    {
        bool isSelect = type != ShangZhenSelectType.Default;

        PetInfo petinfo = PetService.Singleton.GetPetInfo(petid);
        t_petBean mBean = ConfigBean.GetBean<t_petBean, int>(petid);
        if (petinfo != null && mBean != null)
        {
            m_Name.text = mBean.t_name;
            UIGloader.SetUrl(m_TouXiang.m_TouXiangKuang,UIUtils.GetIconPath(mBean, petinfo.basInfo.star));
            //资质图标修改
            int level = petinfo.basInfo.color;
            ZiZhiKuangJiaZai(level);
            NameColor(level);
            m_Name.text = UIUtils.GetPingJiePetName(petid, level, petinfo.basInfo.star);
            SetShuXingTuBiao(mBean.t_type);

            if (!isGet && isSelect == false)
            {
                m_TouXiang.m_ShangZhenTuBiao.visible = isGet;
                m_ShangZhenBtn.visible = true;
                m_ShangZhenBtn.onClick.Add(OnShangZhen);
            }
            else
                m_ShangZhenBtn.visible = false;

            m_fightPowerLable.text = PetService.Singleton.GetPetFightPower(petid) + "";
            m_levelNotEnoughIcon.visible = false;
            ZhenRongType zhenRongType = PetService.Singleton.zhenRongType;
            this.grayed = false;
            m_zhenWanIcon.visible = false;
            if (zhenRongType == ZhenRongType.ZhongJiShiLian)
            {
                m_fightPowerGroup.y -= m_progressGroup.height;
                m_progressGroup.visible = true;
                TrialPetStatus petStatue = UltemateTrainService.Singleton.GetTrialPetStatue(petid);
                m_hpProgress.max = petinfo.fightInfo.hp;
                m_energyProgress.max = 1000;
                if (petStatue != null)
                {
                    m_hpProgress.value = petinfo.fightInfo.hp - petStatue.hpLoss;
                    m_energyProgress.value = petStatue.anger;
                    if(petStatue.dead == 1)
                    {
                        this.grayed = true;
                        m_zhenWanIcon.visible = true;
                    }
                }
                else
                {
                    m_hpProgress.value = petinfo.fightInfo.hp;
                    m_energyProgress.value = 0;
                }

                // 1801004 终极试炼上阵的最低等级
                t_globalBean globalBean = ConfigBean.GetBean<t_globalBean, int>(1801004);
                if (globalBean != null && isSelect == false)
                {
                    if (petinfo.basInfo.level < globalBean.t_int_param)
                    {
                        m_levelNotEnoughIcon.visible = true;
                        m_ShangZhenBtn.visible = false;
                    }
                }
            }
            else
                m_progressGroup.visible = false;


            if (isSelect)
            {
                m_levelNotEnoughIcon.visible = false;
                m_ShangZhenBtn.visible = false;
                if (type == ShangZhenSelectType.CloneChangePet && isEnterID)
                {
                    m_selectBtn.visible = false;
                }
                else
                {
                    m_selectBtn.visible = true;
                    m_selectBtn.onClick.Add(OnSelectBtnClick);
                }
                
            }
            else
                m_selectBtn.visible = false;
        }
        OnStarList();
    }
    private void OnShangZhen()
    {
        ShangZhenWindow window = WinMgr.Singleton.GetWindow<ShangZhenWindow>(winName);

        GameEvent evt = GameEventFactory.create();
        evt.EventId = (int)EventID.OnShangZhenChongWuId;
        TwoParam<int, int> twoPara = new TwoParam<int, int>();
        twoPara.value1 = window.oldPetId;
        twoPara.value2 = petid;
        evt.Data = twoPara;

        PetService.Singleton.SetReplace(window.oldPetId, petid, PetService.Singleton.zhenRongType);
        GED.ED.dispatchEvent(evt);
        window.Close();
    }

    private void OnSelectBtnClick()
    {
        string winFullName = string.Format("{0}.{1}", packageName, winName);
        ShangZhenWindow window = WinMgr.Singleton.GetWindow<ShangZhenWindow>(winFullName);
        if (window != null)
        {
            window.Close();
        }
        GED.ED.dispatchEvent(EventID.OnSelectPetListItem, petid);
 
    }

    private void SetShuXingTuBiao(int shuxing)
    {
        UIGloader.SetUrl(m_ShuXing, UIUtils.GetLoaderUrl(WinEnum.UI_Common, UIUtils.GetPetTypeUrl(shuxing)));
    }
    private void ZiZhiKuangJiaZai(int zizhi)
    {
        string name;
        name = UIUtils.GetLoaderUrl(WinEnum.UI_Common, UIUtils.GetBorderByQuality(zizhi));
        UIGloader.SetUrl(m_TouXiang.m_PingJieKuang,name);
    }
    //宠物名字颜色
    private void NameColor(int zizhi)
    {
        m_Name.color = UIUtils.GetColorByQuality(zizhi);
        PetQualityDou qualityDouj = m_TouXiang.m_PinJieDian as PetQualityDou;
        if (qualityDouj != null)
        {
            qualityDouj.InitView(zizhi);
        }
    }
    private void OnStarList()
    {
        PetInfo petInfo = PetService.Singleton.GetPetInfo(petid);
        if (petInfo != null)
        {
            StarList star = new StarList((UI_Common.UI_StarList)m_TouXiang.m_starList);
            star.SetStar(petInfo.basInfo.star);
        }
    }
}
