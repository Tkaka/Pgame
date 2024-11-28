using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_Strength;
using Message.Pet;
using Data.Beans;

public class ZhanHunPanel : TabPage {

    UI_ZhanHunPanel _zhanHunPanel;
    StrengthWindow _parentWindow;
    /// <summary>
    /// 进入战魂界面的参数
    /// </summary>
    ThreeParam<int, int, int> _zhanHunStrengthPara;

    private List<int> _zhanHunIDS = new List<int>();
    List<ZhanHunItem> zhanHunList = new List<ZhanHunItem>();
    /// <summary>
    /// 当前选中的Item
    /// </summary>
    private ZhanHunItem selectedItem;

    public ZhanHunPanel(StrengthWindow parentWindow)
    {
        _parentWindow = parentWindow;
        _zhanHunPanel = parentWindow.Window.m_zhanHunPanel;
        _zhanHunStrengthPara = new ThreeParam<int, int, int>();
        _zhanHunPanel.m_strengthBtn.onClick.Add(OnClickStrengthBtn);
        _zhanHunPanel.m_unlockBtn.onClick.Add(OnUnlockBtnClick);
        RefreshZhanHunIDs();
        InitZhanHunList();
    }

    public StrengthDataManager StrengthData
    {
        get { return _parentWindow.strengthData; }
    }

    public override void OnHide()
    {
        
    }

    public override void OnShow()
    {
        RefreshView();
    }

    public override void OnClose()
    {
        _zhanHunIDS = null;
    }

    private void InitZhanHunList()
    {
        zhanHunList.Add(_zhanHunPanel.m_zhanHunItem1 as ZhanHunItem);
        zhanHunList.Add(_zhanHunPanel.m_zhanHunItem2 as ZhanHunItem);
        zhanHunList.Add(_zhanHunPanel.m_zhanHunItem3 as ZhanHunItem);
        zhanHunList.Add(_zhanHunPanel.m_zhanHunItem4 as ZhanHunItem);

        int num = zhanHunList.Count;

        for (int i = 0; i < num; i++)
        {
            ZhanHunItem zhanHunItem = zhanHunList[i];
            zhanHunItem.index = i;
            zhanHunItem.zhanHunID = _zhanHunIDS[i];
            zhanHunItem.Init(this);
        }
        // 默认选择第一个
        selectedItem = zhanHunList[0];
        selectedItem.m_selectedIcon.visible = true;
    }

    public override void RefreshView(bool isShow = false)
    {
        RefreshZhanHunIDs();
        RefreshZhanHunList();
        SetZhanHunStrengthPara(selectedItem.zhanHunID, selectedItem.index);
        RefreshZhanHunDetailPanel(selectedItem);
    }

    private void RefreshZhanHunIDs()
    {
        PetInfo petInfo = StrengthData.CurSelectPetInfo;
        t_petBean petBean = ConfigBean.GetBean<t_petBean, int>(petInfo.petId);

        _zhanHunIDS.Clear();
        _zhanHunIDS.AddRange(GTools.splitStringToIntArray(petBean.t_soul_detail_type));
    }

    private void RefreshZhanHunList()
    {
        int num = zhanHunList.Count;

        for (int i = 0; i < num; i++)
        {
            ZhanHunItem zhanHunItem = zhanHunList[i];
            zhanHunItem.zhanHunID = _zhanHunIDS[i];
            zhanHunItem.RefreshView();
        }
    }

    private void RefreshZhanHunDetailPanel(ZhanHunItem zhanHunItem)
    {
        // TODO : 语言包的ID替换成正确的ID
        t_pet_soulBean petSoulBean = ConfigBean.GetBean<t_pet_soulBean, int>(zhanHunItem.zhanHunID);
        PetInfo petInfo = StrengthData.CurSelectPetInfo;
        if (petInfo == null || petSoulBean == null)
            return;

        _zhanHunPanel.m_nameLabel.text = petSoulBean.t_nameLanguageID;
        _zhanHunPanel.m_lvLabel.text =  string.Format("等级{0}", petInfo.soulInfo.souls[zhanHunItem.index].level);
        _zhanHunPanel.m_iconLvLabel.text = petInfo.soulInfo.souls[zhanHunItem.index].level + "";
        // TODO : 战魂详情描述的语言包ID
        List<float> valueList = PetService.Singleton.GetZhanHunDesValueList(zhanHunItem.zhanHunID, zhanHunItem.index, StrengthData.CurSelectPetInfo.petId);
        switch (valueList.Count)
        {
            case 1:
                _zhanHunPanel.m_descriptLabel.text = string.Format(petSoulBean.t_descriptID, valueList[0]);
                break;
            case 2:
                _zhanHunPanel.m_descriptLabel.text = string.Format(petSoulBean.t_descriptID, valueList[0], valueList[1]);
                break;
            case 3:
                _zhanHunPanel.m_descriptLabel.text = string.Format(petSoulBean.t_descriptID, valueList[0], valueList[1], valueList[2]);
                break;
            case 4:
                _zhanHunPanel.m_descriptLabel.text = string.Format(petSoulBean.t_descriptID, valueList[0], valueList[1], valueList[2], valueList[3]);
                break;
            default:
                break;
        }
       
        bool isUnlock = PetService.Singleton.ZhanHunIsUnlock(zhanHunItem.index, StrengthData.CurSelectPetInfo.petId);

        if (isUnlock)
        {
            _zhanHunPanel.m_unlockBtn.visible = false;
            bool isFullLevel = IsFullLevel();
            _zhanHunPanel.m_strengthBtn.visible = !isFullLevel;
            _zhanHunPanel.m_fullLevelGroup.visible = isFullLevel;
        }
        else
        {
            _zhanHunPanel.m_fullLevelGroup.visible = false;
            _zhanHunPanel.m_strengthBtn.visible = false;
            _zhanHunPanel.m_unlockBtn.visible = true;
        }

        UIGloader.SetUrl(_zhanHunPanel.m_zhanHunIconLoader, petSoulBean.t_icon);
        _zhanHunPanel.m_zhanHunIconLoader.grayed = !isUnlock;
    }
    /// <summary>
    /// 设置进入战魂强化界面的参数
    /// </summary>
    private void SetZhanHunStrengthPara(int zhanHunID, int zhanHunIndex)
    {
        _zhanHunStrengthPara.value1 = zhanHunID;
        _zhanHunStrengthPara.value2 = zhanHunIndex;
        _zhanHunStrengthPara.value3 = _parentWindow.strengthData.CurSelectPetInfo.petId;
    }

    public void OnClickZhanHunItem(int index)
    {
        ZhanHunItem zhanHunItem = zhanHunList[index];
        if (selectedItem == zhanHunItem)
            return;
        if(selectedItem != null)
            selectedItem.m_selectedIcon.visible = false;
        selectedItem = zhanHunItem;
        selectedItem.m_selectedIcon.visible = true;

        SetZhanHunStrengthPara(zhanHunItem.zhanHunID, zhanHunItem.index);
        RefreshZhanHunDetailPanel(zhanHunItem);
    }


    private void OnClickStrengthBtn()
    {
        WinInfo winInfo = new WinInfo();
        winInfo.param = _zhanHunStrengthPara;
        WinMgr.Singleton.Open<ZhanHunStrengthWindow>(winInfo, UILayer.Popup);
    }

    private void OnUnlockBtnClick()
    {
        int unlockColor = PetService.Singleton.GetZhanHunUnlockColor(selectedItem.index);
        Color color = UIUtils.GetColorByQuality(unlockColor);
        string unlockColorStr = UIUtils.GetColorName(unlockColor);
        string colorHtml = ColorUtility.ToHtmlStringRGB(color);
        TipWindow.Singleton.ShowTip(string.Format("宝贝达到[color=#{0}]{1}[/color]解锁", colorHtml, unlockColorStr));
    }

    private bool IsFullLevel()
    {
        int maxLevel = PetService.Singleton.GetZhanHunMaxLevel();
        SoulInfo soulInfo = StrengthData.CurSelectPetInfo.soulInfo.souls[selectedItem.index];
        return soulInfo.level >= maxLevel;
    }
}
