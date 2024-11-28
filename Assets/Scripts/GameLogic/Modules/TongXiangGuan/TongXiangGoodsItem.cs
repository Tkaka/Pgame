using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_TongXiangGuan;
using FairyGUI;
using Message.Team;
using Data.Beans;

public class TongXiangGoodsItem : UI_tongXiangGoodsItem {

    public int index;
    ResPack resPack;
    public new static TongXiangGoodsItem CreateInstance()
    {
        return (TongXiangGoodsItem)UIPackage.CreateObject("UI_TongXiangGuan", "tongXiangGoodsItem");
    }

    public void Init()
    {
        m_buyBtn.onClick.Add(OnBuyBtnClick);
        m_switchGoodsBtn.onClick.Add(OnSwitchGoodsBtnClick);

        resPack = new ResPack(this);

        InitModel();
        RefreshView();
    }

    private void InitModel()
    {
        Statue statue = TongXiangGuanServices.Singleton.statueInfo;
        if (statue != null)
        {
            int tongXiangID = UIUtils.GetTongXiangID(statue.petId, TongXiangGuanServices.Singleton.curMaterialIndex);
            t_statueBean statueBean = ConfigBean.GetBean<t_statueBean, int>(tongXiangID);
            if (statueBean != null)
            {
                GameObject model = resPack.LoadGo(statueBean.t_model);
                model.transform.localPosition = new Vector3(0, 0, 300);
                model.transform.localEulerAngles = new Vector3(0, 180, 0);
                model.transform.localScale = new Vector3(100, 100, 100);

                GoWrapper wrapper = new GoWrapper(model);
                m_modelPos.SetNativeObject(wrapper);
                model.setLayer("UIActor");
            }
        }
    }

    public void RefreshView()
    {
        RefreshBaseInfo();
        RefreshBtnStates();
    }

    private void RefreshBaseInfo()
    {
        Statue statue = TongXiangGuanServices.Singleton.statueInfo;
        if (statue != null)
        {
            int tongXiangID = UIUtils.GetTongXiangID(statue.petId, TongXiangGuanServices.Singleton.curMaterialIndex);
            t_statueBean statueBean = ConfigBean.GetBean<t_statueBean, int>(tongXiangID);
            if (statueBean != null)
            {
                if (!string.IsNullOrEmpty(statueBean.t_add_prop))
                {
                    string[] addPropArr = statueBean.t_add_prop.Split(';');
                    if (addPropArr.Length == 4)
                    {
                        addPropArr = addPropArr[index].Split('+');

                        if (addPropArr.Length == 3)
                        {
                            m_atkLabel.text = addPropArr[0];
                            m_defLabel.text = addPropArr[1];
                            m_hpLabel.text = addPropArr[2];
                        }
                    }
                }
            }
        }

        m_rankName.text = UIUtils.GetTongXiangRankName((TongXiangRank)index);
        m_rankName.color = UIUtils.GetTongXiangRankColor((TongXiangRank)index);

        m_switchSuccessIcon.visible = false;
    }

    private void RefreshBtnStates()
    {
        if (IsUse())
        {
            m_buyGroup.visible = false;
            m_switchGoodsBtn.visible = false;
            m_useTip.visible = true;
            m_geDouJiaTipGroup.visible = false;
        }
        else
        {
            if (IsHave())
            {
                m_buyGroup.visible = false;
                m_switchGoodsBtn.visible = true;
                m_useTip.visible = false;
                m_geDouJiaTipGroup.visible = false;
            }
            else
            {
                m_buyGroup.visible = true;
                m_switchGoodsBtn.visible = false;
                m_useTip.visible = false;

                int material = TongXiangGuanServices.Singleton.curMaterialIndex;
                int priceID = UIUtils.GetTongXiangPriceID(material, index+1);
                t_statue_priceBean priceBean = ConfigBean.GetBean<t_statue_priceBean, int>(priceID);

                bool isEnoughDaiBi = IsEnoughDaiBi();
                if (index > 1)
                {
                    m_diamondGroup.visible = true;
                    m_goldGroup.visible = false;
                    bool isHaveThisPet = IsHaveThisPet();

                    if (priceBean != null)
                        m_diamondNum.text = priceBean.t_price + "";

                    if (isHaveThisPet)
                    {
                        m_geDouJiaTipGroup.visible = false;
                    }
                    else
                    {
                        m_geDouJiaTipGroup.visible = true;
                        Statue statue = TongXiangGuanServices.Singleton.statueInfo;
                        if (statue != null)
                        {
                            t_petBean petBean = ConfigBean.GetBean<t_petBean, int>(statue.petId);
                            if (petBean != null)
                            {
                                string petName = UIUtils.GetPetName(petBean);
                                m_geDouJiaTipLabel.text = string.Format("需获得格斗家{0}", petName);
                            }
                        }
                        m_buyBtn.grayed = true;
                        m_buyBtn.touchable = false;
                    }

                    m_diamondNum.color = isEnoughDaiBi ? Color.white : Color.red;
                }
                else
                {
                    m_geDouJiaTipGroup.visible = false;
                    m_diamondGroup.visible = false;
                    m_goldGroup.visible = true ;
                    m_goldNum.color = isEnoughDaiBi ? Color.white : Color.red;

                    if (priceBean != null)
                        m_goldNum.text = priceBean.t_price + "";
                }
            }
        }
    }
    /// <summary>
    /// 是否拥有
    /// </summary>
    /// <returns></returns>
    public bool IsHave()
    {
        Statue statue = TongXiangGuanServices.Singleton.statueInfo;
        if (statue != null)
        {
            int count = statue.statueUnitId.Count;
            for (int i = 0; i < count; i++)
            {
                int haveIndex = UIUtils.GetTongXiangRank(statue.statueUnitId[i]);
                int material = UIUtils.GetTongXiangMaterial(statue.statueUnitId[i]);
                if (haveIndex == index && material == TongXiangGuanServices.Singleton.curMaterialIndex)
                {
                    return true;
                }
            }
        }

        return false;
    }
    /// <summary>
    /// 是否使用
    /// </summary>
    /// <returns></returns>
    public bool IsUse()
    {
        Statue statue = TongXiangGuanServices.Singleton.statueInfo;
        if (statue != null && statue.currentStatueId != 0)
        {
            int curIndex = UIUtils.GetTongXiangRank(statue.currentStatueId);
            int curMaterial = UIUtils.GetTongXiangMaterial(statue.currentStatueId);
            return (curIndex == index && TongXiangGuanServices.Singleton.curMaterialIndex == curMaterial);
        }

        return false;
    }
    /// <summary>
    /// 代币是否足够
    /// </summary>
    /// <returns></returns>
    public bool IsEnoughDaiBi()
    {
        int material = TongXiangGuanServices.Singleton.curMaterialIndex;
        int priceID = UIUtils.GetTongXiangPriceID(material, index + 1);
        t_statue_priceBean priceBean = ConfigBean.GetBean<t_statue_priceBean, int>(priceID);

        if (priceBean != null)
        {
            Message.Role.RoleInfo roleInfo = RoleService.Singleton.GetRoleInfo();
            if (index > 1)
                return roleInfo.damond >= priceBean.t_price;
            else
                return roleInfo.gold >= priceBean.t_price;
        }

        return false;
    }
    /// <summary>
    /// 是否拥有这个宠物
    /// </summary>
    /// <returns></returns>
    private bool IsHaveThisPet()
    {
        Statue statue = TongXiangGuanServices.Singleton.statueInfo;
        if (statue != null)
        {
            Message.Pet.PetInfo petInfo = PetService.Singleton.GetPetByID(statue.petId);

            return petInfo != null;
        }

        return false;
    }

    private void OnBuyBtnClick()
    {
        if (!IsEnoughDaiBi())
        {
            int itemID = 0;
            if (index > 1)
                itemID = (int)ItemType.Damond;
            else
                itemID = (int)ItemType.Gold;
            TwoParam<int, int> twoParam = new TwoParam<int, int>();
            twoParam.value1 = itemID;
            twoParam.value2 = -1
;            WinMgr.Singleton.Open<LaiYuanWindow>(WinInfo.Create(false, null, true, twoParam), UILayer.Popup);
        }
        else
        {
            // 发送购买请求
            Statue statue = TongXiangGuanServices.Singleton.statueInfo;
            if (statue != null)
            {
                int material = TongXiangGuanServices.Singleton.curMaterialIndex;
                int zhanTingID = TongXiangGuanServices.Singleton.exhibitionInfo.exhibitionId;
                TongXiangGuanServices.Singleton.ReqStatueBuy(statue.petId, material, index, zhanTingID);
            }
        }
    }

    private void OnSwitchGoodsBtnClick()
    {
        // 发送更换请求
        Statue statue = TongXiangGuanServices.Singleton.statueInfo;
        if (statue != null)
        {
            int newStatueID = UIUtils.GetStatueID(statue.petId, TongXiangGuanServices.Singleton.curMaterialIndex, index);
            int zhanTingID = TongXiangGuanServices.Singleton.exhibitionInfo.exhibitionId;
            TongXiangGuanServices.Singleton.ReqStatueExchange(statue.currentStatueId, newStatueID, zhanTingID);
        }
    }

    public override void Dispose()
    {
        resPack.ReleaseAllRes();
        resPack = null;

        base.Dispose();
    }
}
