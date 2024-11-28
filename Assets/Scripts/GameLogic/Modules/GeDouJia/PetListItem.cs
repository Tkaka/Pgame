using Data.Beans;
using Message.Pet;
using System.Collections.Generic;
using UI_GeDouJia;
using UnityEngine;

public class PetListItem : UI_PetListItem
{
    public int petid;
    public GeDouJiaWindow parentWindow;

    public new static PetListItem CreateInstance()
    {
        return (PetListItem)UI_PetListItem.CreateInstance();
    }

    public void InitView(int petId)
    {
        // 功能开放限制
        FuncService.Singleton.SetFuncLock(m_equipBtn, 1101);
        m_equipBtn.GetChildAt(0).grayed = !FuncService.Singleton.IsFuncOpen(1101);

        petid = petId;
        bool isGet = IsGetThisPet();
        m_getGroup.visible = isGet;
        m_notGetGroup.visible = !isGet;
        m_equipBtn.onClick.Add(OnEquipBtn);
        m_strengthenBtn.onClick.Add(OnStrengthenBtn);
        m_sourceBtn.onClick.Add(OnSourceBtn);
        m_headLoader.onClick.Add(XiangQing);
        m_CompoundBtn.onClick.Add(OnHeCheng);
        t_petBean bean = ConfigBean.GetBean<t_petBean, int>(petid);
        if (bean == null)
        {
            Logger.err("没有此宠物！");
            return;
        }
        ChangeShuXingImg(bean.t_type);
        //在宠物表加资质
        UIGloader.SetUrl(m_iconBorder, UIUtils.GetLoaderUrl(WinEnum.UI_Common, UIUtils.GetBorderByQuality(3)));
        PetInfo petInfo = PetService.Singleton.GetPetInfo(petid);
        int star = petInfo == null ? -1 : petInfo.basInfo.star;
        UIGloader.SetUrl(m_headLoader, UIUtils.GetIconPath(bean, star));
        //是否拥有
        if (isGet)
        {
            m_levelGroup.visible = true;
            m_headLoader.grayed = false;
            m_iconBorder.grayed = false;
            m_startlist.grayed = false;
            PetInfo petinfo = PetService.Singleton.GetPetInfo(petid);
            if (petinfo == null)
            {
                Logger.err("未获得宠物" + petid);
                return;
            }
            //根据宠物类型切换图标
            
            //资质框、名字的颜色修改
            int color = petinfo.basInfo.color;
            Start(petinfo.basInfo.star);
            ZiZhiKuangJiaZai(color);
            NameColor(color);
            m_Level.text = petinfo.basInfo.level.ToString();
            m_petName.text = UIUtils.GetPingJiePetName(petid, color, petinfo.basInfo.star);
            m_petName.color = UIUtils.GetColorByQuality(petinfo.basInfo.color);
            m_fightPower.text = (PetService.Singleton.GetPetFightPower(petinfo.petId)).ToString();
            m_Level.text = petinfo.basInfo.level.ToString();

            //根据是否上阵显示已上阵
            if (PetService.Singleton.ShangZhenList(petid))
            {
                m_isEquip.visible = true;
            }
            else
            {
                m_isEquip.visible = false;
            }
            //进度条
            m_fragProgress.visible = !isGet;
        }
        else
        {
            m_PinJieDian.visible = false;
            //变灰
            m_headLoader.grayed = true;
            m_iconBorder.grayed = true;
            m_startlist.grayed = true;
            m_levelGroup.visible = false;

            m_isEquip.visible = false;
            Start(bean.t_hecheng_star);

            //根据背包数据获取当前值
            m_fragProgress.value = BagService.Singleton.GetItemNum(bean.t_fragment_id);
            t_itemBean itemBean = ConfigBean.GetBean<t_itemBean, int>(bean.t_fragment_id);
            if (itemBean == null)
            {
                Logger.err(petid.ToString());
                Logger.err("道具表没有此宠物碎片id" + bean.t_id.ToString());

                return;
            }
            //将字符串分割成数组
            int[] suipia = GTools.splitStringToIntArray(itemBean.t_value);
            m_fragProgress.max = suipia[0];
            if (m_fragProgress.value >= m_fragProgress.max)
            {
                m_CompoundBtn.visible = true;
                m_sourceBtn.visible = false;
            }
            else
            {
                m_CompoundBtn.visible = false;
                m_sourceBtn.visible = true;
            }
            m_petName.text = UIUtils.GetPingJiePetName(petid, 1, bean.t_hecheng_star);
            m_petName.color = UIUtils.GetColorByQuality(1);
            //获取背包碎片数据
            m_fragProgress.visible = true;
            m_ShuZhi.text = m_fragProgress.value.ToString() + "/" + m_fragProgress.max.ToString();
        }

    }
    /// <summary>
    /// 是否拥有该宠物
    /// </summary>
    /// <returns></returns>
    private bool IsGetThisPet()
    {
        PetInfo petInfo = PetService.Singleton.GetPetInfo(petid);
        return petInfo != null;
    }
    private void OnHeCheng()
    {
        t_petBean bean = ConfigBean.GetBean<t_petBean, int>(petid);
        int suipianid = ConfigBean.GetBean<t_petBean,int>(petid).t_fragment_id;

        Message.Bag.GridInfo grid = BagService.Singleton.GetGridInfo(suipianid);
        //BagService.Singleton.ReqItemUse(grid.id,(int)m_fragProgress.max);
        PetService.Singleton.ReqPetCompose(petid);
    }
    /// <summary>
    /// 装备界面
    /// </summary>
    private void OnEquipBtn()
    {
        if (FuncService.Singleton.TipFuncNotOpen(1101))
        {
            ThreeParam<int, int, int> twoPara = new ThreeParam<int, int, int>();
            twoPara.value1 = petid;                                 // 宠物ID
            twoPara.value2 = (int)EquipPosition.Weapon;             // 装备部位
            twoPara.value3 = (int)EquipPanelType.EquipStrength;     // 打开的类型
            parentWindow.OpenChild<EquipStrengthWindow>(WinInfo.Create(false, null, true, twoPara));
        }
    }
    /// <summary>
    /// 宠物强化
    /// </summary>
    private void OnStrengthenBtn()
    {
        TwoParam<int, StrengthType> twoParam = new TwoParam<int, StrengthType>();
        twoParam.value1 = petid;                        // 宠物ID
        twoParam.value2 = StrengthType.None;            // 强化类型
        parentWindow.OpenChild<StrengthWindow>(WinInfo.Create(false, null, true, twoParam));
    }

    private void OnSourceBtn()
    {
        int suipianid = ConfigBean.GetBean<t_petBean, int>(petid).t_fragment_id;
        t_itemBean itemBean = ConfigBean.GetBean<t_itemBean,int>(suipianid);
        int xuqiu = 0;
        if (string.IsNullOrEmpty(itemBean.t_value))
        { }
        else
        {
            int[] shuliang = GTools.splitStringToIntArray(itemBean.t_value);
            xuqiu = shuliang[0];
        }
        TwoParam<int, int> twoParam = new TwoParam<int, int>();
        twoParam.value1 = suipianid;
        twoParam.value2 = xuqiu;
        WinMgr.Singleton.Open<LaiYuanWindow>(WinInfo.Create(false,null, false, twoParam),UILayer.Popup);
    }

    private void XiangQing()
    {
        TwoParam<int, XiangQingType> twoParam = new TwoParam<int, XiangQingType>();
        WinInfo info = new WinInfo();
        twoParam.value1 = petid;
        twoParam.value2 = XiangQingType.ChongWuGuanLi;
        info.param = twoParam;
        WinMgr.Singleton.Open<ChongWuXiangQingWindow>(info,UILayer.Popup);
    }

    private void ChangeShuXingImg(int shuxing)
    {
        UIGloader.SetUrl(m_typeLoader, UIUtils.GetLoaderUrl(WinEnum.UI_Common,UIUtils.GetPetTypeUrl(shuxing)));
    }

    private void ZiZhiKuangJiaZai(int pinjie)
    {
        string name;
        name = UIUtils.GetLoaderUrl(WinEnum.UI_Common, UIUtils.GetBorderByQuality(pinjie));
        UIGloader.SetUrl(m_iconBorder, name);
        PetInfo info = PetService.Singleton.GetPetInfo(petid);
        if (info != null)
        {
            PetQualityDou qualityDou = m_PinJieDian as PetQualityDou;
            if (qualityDou != null)
            {
                qualityDou.InitView(pinjie);
            }
        }
    }
    //宠物名字颜色
    private void NameColor(int zizhi)
    {
        m_petName.color = UIUtils.GetColorByQuality(zizhi);
    }
    private void Start(int star)
    {
        StarList starList = new StarList((UI_Common.UI_StarList)m_startlist);
        starList.SetStar(star);
    }

}