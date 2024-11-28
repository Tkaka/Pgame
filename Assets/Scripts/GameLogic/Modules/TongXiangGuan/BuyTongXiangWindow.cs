using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_TongXiangGuan;
using Data.Beans;
using Message.Team;

public class BuyTongXiangWindow : BaseWindow {

    UI_BuyTongXiangWindow window;

    int oldSelectIndex;
    string[] openLvArr = null;

    public override void OnOpen()
    {
        base.OnOpen();

        window = getUiWindow<UI_BuyTongXiangWindow>();
        window.m_backBtn.onClick.Add(OnCloseBtn);
        window.m_btnList.onClickItem.Add(OnClickMaterialBtn);
        TongXiangGuanServices.Singleton.curMaterialIndex = 1;

        InitView();
    }

    public override void AddEventListener()
    {
        base.AddEventListener();

        GED.ED.addListener(EventID.OnExhibitionInfoChange, OnExhibitionInfoChanged);
    }

    public override void RemoveEventListener()
    {
        base.RemoveEventListener();

        GED.ED.removeListener(EventID.OnExhibitionInfoChange, OnExhibitionInfoChanged);
    }

    public override void InitView()
    {
        base.InitView();
        InitLabel();
        InitMaterialBtnList();
        InitGoodsList();
    }

    private void InitLabel()
    {
        Statue statue = TongXiangGuanServices.Singleton.statueInfo;
        if (statue != null)
        {
            t_petBean petBean = ConfigBean.GetBean<t_petBean, int>(statue.petId);
            string nameStr = "";
            if(petBean != null)
            {
                nameStr += UIUtils.GetPetName(petBean);
                window.m_tongXiangNameLabel.text = nameStr;
            }

            if (statue.currentStatueId != 0)
            {
                window.m_curUseTipLabel.visible = true;
                TongXiangMaterial material = (TongXiangMaterial)(UIUtils.GetTongXiangMaterial(statue.currentStatueId));
                TongXiangRank rank = (TongXiangRank)(UIUtils.GetTongXiangRank(statue.currentStatueId));
                window.m_curUseTipLabel.text = string.Format("当前场馆中的铜像：{0}{1}{2}", UIUtils.GetTongXiangRankName(rank), nameStr, UIUtils.GetTongXiangMaterialName(material));
            }
            else
            {
                window.m_curUseTipLabel.visible = false;
            }
            
        }
    }

    private void InitMaterialBtnList()
    {
        // 铜像馆页签解锁的全局id 80606
        t_globalBean globalBean = ConfigBean.GetBean<t_globalBean, int>(80606);
        TongXiangMaterialBtn materialBtn = null;
        if (!string.IsNullOrEmpty(globalBean.t_string_param))
        {
            openLvArr = globalBean.t_string_param.Split('+');
            int length = openLvArr.Length;
            for (int i = 0; i < length; i++)
            {
                materialBtn = TongXiangMaterialBtn.CreateInstance();
                window.m_btnList.AddChild(materialBtn);
                materialBtn.unlockLv = int.Parse(openLvArr[i]);
                materialBtn.material = i + 1;
                materialBtn.Init();
            }
        }
        // 默认选择第一个
        window.m_btnList.selectedIndex = 0;
        materialBtn = window.m_btnList.GetChildAt(0) as TongXiangMaterialBtn;
        materialBtn.ClickItem(true);
    }

    private void InitGoodsList()
    {
        Statue statue = TongXiangGuanServices.Singleton.statueInfo;
        if (statue != null)
        {
            int tongXiangID = UIUtils.GetTongXiangID(statue.petId, TongXiangGuanServices.Singleton.curMaterialIndex);
            t_statueBean statueBean = ConfigBean.GetBean<t_statueBean, int>(tongXiangID);
            if (statueBean != null && !string.IsNullOrEmpty(statueBean.t_add_prop))
            {
                string[] propArr = statueBean.t_add_prop.Split(';');
                int length = propArr.Length;
                TongXiangGoodsItem goodsItem = null;
                for (int i = 0; i < length; i++)
                {
                    goodsItem = TongXiangGoodsItem.CreateInstance();
                    window.m_goodsList.AddChild(goodsItem);
                    goodsItem.index = i;
                    goodsItem.Init();
                }
            }
        }
    }

    public override void RefreshView()
    {
        base.RefreshView();

        RefreshGoodsList();
        RefreshTipLabel();
    }

    private void RefreshGoodsList()
    {
        int count = window.m_goodsList.numChildren;
        TongXiangGoodsItem goodsItem = null;
        for (int i = 0; i < count; i++)
        {
            goodsItem = window.m_goodsList.GetChildAt(i) as TongXiangGoodsItem;
            goodsItem.RefreshView();
        }
    }

    private void RefreshTipLabel()
    {
        Statue statue = TongXiangGuanServices.Singleton.statueInfo;
        if (statue != null)
        {
            t_petBean petBean = ConfigBean.GetBean<t_petBean, int>(statue.petId);
            string nameStr = "";
            if (petBean != null)
            {
                nameStr += UIUtils.GetPetName(petBean);
            }

            if (statue.currentStatueId != 0)
            {
                window.m_curUseTipLabel.visible = true;
                TongXiangMaterial material = (TongXiangMaterial)(UIUtils.GetTongXiangMaterial(statue.currentStatueId));
                TongXiangRank rank = (TongXiangRank)(UIUtils.GetTongXiangRank(statue.currentStatueId));
                window.m_curUseTipLabel.text = string.Format("当前场馆中的铜像：{0}{1}{2}", UIUtils.GetTongXiangRankName(rank), nameStr, UIUtils.GetTongXiangMaterialName(material));
            }
        }
    }

    private void OnClickMaterialBtn()
    {
        int index = window.m_materialCtrl.selectedIndex;
        if (IsMaterialBtnUnlocked(index))
        {
            TongXiangGuanServices.Singleton.curMaterialIndex = window.m_materialCtrl.selectedIndex + 1;
            RefreshGoodsList();

            TongXiangMaterialBtn materialBtn = window.m_btnList.GetChildAt(oldSelectIndex) as TongXiangMaterialBtn;
            materialBtn.ClickItem(false);
            materialBtn = window.m_btnList.GetChildAt(index) as TongXiangMaterialBtn;
            materialBtn.ClickItem(true);
            oldSelectIndex = index;
        }
        else
        {
            TipWindow.Singleton.ShowTip(string.Format("{0}级解锁", GetMaterialUnlockLv(index)));
            window.m_materialCtrl.SetSelectedIndex(oldSelectIndex);
        }
    }
    #region 数据处理 ***********************************************************************************************************************
    private int GetMaterialUnlockLv(int index)
    {
        if (openLvArr != null && openLvArr.Length > index)
        {
            return int.Parse(openLvArr[index]);
        }

        return int.MaxValue;
    }

    private bool IsMaterialBtnUnlocked(int index)
    {
        int unlockLv = GetMaterialUnlockLv(index);
        Message.Role.RoleInfo roleInfo = RoleService.Singleton.GetRoleInfo();

        return roleInfo.level >= unlockLv;
    }
    #endregion
    private void OnExhibitionInfoChanged(GameEvent evt)
    {
        RefreshView();
    }

    private void OnResBuy(GameEvent evt)
    {
        RefreshView();
    }
    protected override void OnCloseBtn()
    {
        base.OnCloseBtn();
    }
}
