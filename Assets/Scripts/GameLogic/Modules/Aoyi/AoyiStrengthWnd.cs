using UI_AoYi;
using UI_Common;
using Message.Profound;
using Message.Pet;
using FairyGUI;
using System.Collections.Generic;
using Data.Beans;
using UnityEngine;
using System;

public class AoyiStrengthWnd : BaseWindow
{
    private UI_AoyiStrengthWnd m_window;
    private int m_maxLevel = 110;           //最大等级读全局表
    private StoneInfo m_stoneInfo;
    private int m_petId;
    private AoyiService.EStonePage m_page;

    public override void OnOpen()
    {
        base.OnOpen();
        m_window = getUiWindow<UI_AoyiStrengthWnd>();
        _BindEvent();

        ThreeParam<int, AoyiService.EStonePage, StoneInfo> threeParam = Info.param as ThreeParam<int, AoyiService.EStonePage, StoneInfo>;
        if (threeParam == null)
        {
            Debug.LogError("打开参数异常");
            Close();
            return;
        }

        m_petId = threeParam.value1;
        m_page = threeParam.value2;
        m_stoneInfo = threeParam.value3;

        _ShowStoneBaseInfo();
    }


    private void _BindEvent()
    {
        m_window.m_btnClose.onClick.Add(Close);
        m_window.m_btnLevelUp.onClick.Add(_OnLevelUpClick);
        m_window.m_btnOneKeyStrength.onClick.Add(_OnOneKeyClick);
        m_window.m_btnBreak.onClick.Add(_OnBreakClick);
    }

    private void _ShowStoneBaseInfo()
    {
        t_itemBean itemBean = ConfigBean.GetBean<t_itemBean, int>(m_stoneInfo.itemId);
        if (itemBean == null)
            return;

        string strName = string.Format("{0} +{1}", itemBean.t_name, m_stoneInfo.bigLevel);
        m_window.m_txtStoneName.text = strName;
        m_window.m_txtStoneName.color = UIUtils.GetItemColor(m_stoneInfo.itemId);

        AoyiIconList iconList = m_window.m_dicList as AoyiIconList;
        if (iconList != null)
        {
            iconList.RefreshView(m_petId, m_page, 0, m_stoneInfo.id);
        }

        m_window.m_txtLevel.text = string.Format("等级{0}", m_stoneInfo.minLevel);

        if (m_stoneInfo.bigLevel * 10 + m_stoneInfo.minLevel >= m_maxLevel)
        {
            //满级
            m_window.m_progressBar.value = 100;
            m_window.m_txtProgressNum.text = "Max";
            m_window.m_objFull.visible = true;
            m_window.m_groupLevelUp.visible = false;
            m_window.m_breakGroup.visible = false;
            m_window.m_txtLevelUpDes.visible = false;

            m_window.m_propertyList.RemoveChildren(0, -1, true);
            List<AoyiService.PropertyInfo> propertyInfoList = AoyiService.Singleton.GetStoneAddPropertyInfo(m_stoneInfo.itemId, m_stoneInfo.bigLevel * 10 + m_stoneInfo.minLevel);
            for (int i = 0; i < propertyInfoList.Count; i++)
            {
                AoyiService.PropertyInfo propertyInfo = propertyInfoList[i];
                UI_objPropertyCel1 cell = UI_objPropertyCel1.CreateInstance();
                t_attr_nameBean propertyBean = ConfigBean.GetBean<t_attr_nameBean, int>(propertyInfo.propertyId);
                if (propertyBean == null)
                    continue;

                cell.m_txtPropertyName.text = propertyBean.t_name_id;

                if (propertyBean.t_value_type == 0)
                {
                    cell.m_txtPropertyValue.text = (propertyInfo.propertyValue * 0.01) + "%";
                }
                else
                {
                    cell.m_txtPropertyValue.text = propertyInfo.propertyValue + "";
                }


                m_window.m_propertyList.AddChild(cell);

            }
        }
        else
        {
            if (m_stoneInfo.minLevel == 10)
            {
                _ShowBeakInfo();
            }
            else
            {
                _ShowLevelInfo();
            }
 
        }

        AoyiCommonItem commonItem = m_window.m_Icon as AoyiCommonItem;
        if (commonItem != null)
        {
            commonItem.RefreshView(m_stoneInfo.itemId, m_stoneInfo.bigLevel * 10 + m_stoneInfo.minLevel);
        }
    }


    //显示突破信息
    private void _ShowBeakInfo()
    {
        m_window.m_breakGroup.visible = true;
        m_window.m_minLevelGroup.visible = false;
        m_window.m_objFull.visible = false;

        t_itemBean itemBean = ConfigBean.GetBean<t_itemBean, int>(m_stoneInfo.itemId);
        if (itemBean == null)
            return;

        int quility = UIUtils.GetDefaultItemQuality(m_stoneInfo.itemId);
        m_window.m_txtCurLevelName.text = string.Format("{0} +{1}", itemBean.t_name, m_stoneInfo.bigLevel);
        m_window.m_txtNextLevelName.text = string.Format("{0} +{1}", itemBean.t_name, m_stoneInfo.bigLevel + 1);
        m_window.m_txtCurLevelName.color = UIUtils.GetItemColor(m_stoneInfo.itemId);
        m_window.m_txtNextLevelName.color = UIUtils.GetItemColor(m_stoneInfo.itemId);

        t_aoyi_level_consumeBean consumeBean = ConfigBean.GetBean<t_aoyi_level_consumeBean, int>(m_stoneInfo.bigLevel * 10 + quility);
        if (consumeBean != null)
        {
            m_window.m_txtCoinNum.text = consumeBean.t_break_consume + "";
        }


        //显示奥义石图标
        AoyiCommonItem commonItem = m_window.m_curLevelIcon as AoyiCommonItem;
        if (commonItem != null)
        {
            commonItem.RefreshView(m_stoneInfo.itemId, m_stoneInfo.bigLevel * 10 + m_stoneInfo.minLevel);
        }

        AoyiCommonItem commonItemNext = m_window.m_nextLevelIcon as AoyiCommonItem;
        if (commonItemNext != null)
        {
            commonItemNext.RefreshView(m_stoneInfo.itemId, (m_stoneInfo.bigLevel + 1) * 10);
        }
    }

    //显示小等级升级信息
    private void _ShowLevelInfo()
    {
        m_window.m_breakGroup.visible = false;
        m_window.m_minLevelGroup.visible = true;

        m_window.m_groupLevelUp.visible = true;
        m_window.m_objFull.visible = false;
        m_window.m_txtLevelUpDes.visible = true;

        int quility = UIUtils.GetDefaultItemQuality(m_stoneInfo.itemId);
        t_aoyi_level_consumeBean consumeBean = ConfigBean.GetBean<t_aoyi_level_consumeBean, int>(m_stoneInfo.bigLevel * 10 + quility);
        if (consumeBean != null)
        {
            m_window.m_progressBar.value = (1.0f * m_stoneInfo.exp / consumeBean.t_level_consume) * 100;
            m_window.m_txtProgressNum.text = string.Format("{0}/{1}", m_stoneInfo.exp, consumeBean.t_level_consume);
        }

        t_itemBean itemBean = ConfigBean.GetBean<t_itemBean, int>(m_stoneInfo.itemId);
        if (itemBean == null)
            return;

        string strName = string.Format("{0} +{1}", itemBean.t_name, m_stoneInfo.bigLevel + 1);

        m_window.m_txtLevelUpDes.text = string.Format("达到10级后可突破为{0}", _GetAoyiStoneColor(strName, m_stoneInfo.itemId));

        long haveNum = RoleService.Singleton.GetCurrencyNum((int)RoleService.ECurrencyType.AoyiJingHua);
        int needNum = consumeBean.t_level_consume - m_stoneInfo.exp;

        m_window.m_txtCosumeNum.text = string.Format("{0}/{1}", haveNum, needNum);
        m_window.m_txtCosumeNum.color = haveNum >= needNum ? Color.green : Color.red;

        m_window.m_propertyList.RemoveChildren(0, -1, true);
        List<AoyiService.PropertyInfo> propertyInfoList = AoyiService.Singleton.GetStoneAddPropertyInfo(m_stoneInfo.itemId, m_stoneInfo.bigLevel * 10 + m_stoneInfo.minLevel);
        List<AoyiService.PropertyInfo> nextPropertyInfoList = AoyiService.Singleton.GetStoneAddPropertyInfo(m_stoneInfo.itemId, m_stoneInfo.bigLevel * 10 + m_stoneInfo.minLevel + 1);
        if (propertyInfoList.Count != nextPropertyInfoList.Count)
        {
            Debug.LogError("当前等级加的属性类型与下一等级的不一致");
            return;
        }

        for (int i = 0; i < propertyInfoList.Count; i++)
        {
            UI_objPropertyCel3 cell = UI_objPropertyCel3.CreateInstance();
            t_attr_nameBean propertyBean = ConfigBean.GetBean<t_attr_nameBean, int>(propertyInfoList[i].propertyId);
            if (propertyBean == null)
                continue;

            cell.m_txtPropertyName.text = propertyBean.t_name_id;
            if (propertyBean.t_value_type == 0)
            {
                cell.m_txtPropertyValue.text = propertyInfoList[i].propertyValue * 0.01 + "%";
                cell.m_txtPropertyNextValue.text = nextPropertyInfoList[i].propertyValue * 0.01 + "%";

            }
            else
            {
                cell.m_txtPropertyValue.text = propertyInfoList[i].propertyValue + "";
                cell.m_txtPropertyNextValue.text = nextPropertyInfoList[i].propertyValue + "";
            }

            m_window.m_propertyList.AddChild(cell);

        }


    }

    private string _GetAoyiStoneColor(string name, int itemId)
    {
        Color color = UIUtils.GetItemColor(itemId);
        string rgb = ColorUtility.ToHtmlStringRGB(color);

        return string.Format("[color=#{0}]{1}[/color]", rgb, name);
    }

    public override void AddEventListener()
    {
        base.AddEventListener();
        GED.ED.addListener(EventID.AoyiStoneInfoChange, _AoyiStoneInfoChange);
    }

    public override void RemoveEventListener()
    {
        base.RemoveEventListener();
        GED.ED.removeListener(EventID.AoyiStoneInfoChange, _AoyiStoneInfoChange);
    }

    private void _AoyiStoneInfoChange(GameEvent evt)
    {
        _ShowStoneBaseInfo();
    }

    //当前能否强化
    private bool _CheckCanStrength()
    {
        int quility = UIUtils.GetDefaultItemQuality(m_stoneInfo.itemId);
        t_aoyi_level_consumeBean consumeBean = ConfigBean.GetBean<t_aoyi_level_consumeBean, int>(m_stoneInfo.bigLevel * 10 + quility);
        if (consumeBean == null)
            return false;

        long haveNum = RoleService.Singleton.GetCurrencyNum((int)RoleService.ECurrencyType.AoyiJingHua);
        int needNum = consumeBean.t_level_consume - m_stoneInfo.exp;
        if (haveNum < needNum)
        {
            TipWindow.Singleton.ShowTip("奥义石精华不足");
            return false;
        }

        return true;

    }

    private void _OnLevelUpClick()
    {
        if (_CheckCanStrength() == false)
            return;

        AoyiService.Singleton.ReqLevelUp(m_petId, m_page, m_stoneInfo.id, AoyiService.EStoneLevelUpType.SingleLevel);
    }

    private void _OnOneKeyClick()
    {
        if (_CheckCanStrength() == false)
            return;

        AoyiService.Singleton.ReqLevelUp(m_petId, m_page, m_stoneInfo.id, AoyiService.EStoneLevelUpType.OneKey);
    }

    private void _OnBreakClick()
    {
        int quility = UIUtils.GetDefaultItemQuality(m_stoneInfo.itemId);
        t_aoyi_level_consumeBean consumeBean = ConfigBean.GetBean<t_aoyi_level_consumeBean, int>(m_stoneInfo.bigLevel * 10 + quility);
        if (consumeBean == null)
            return;

        long haveNum = RoleService.Singleton.GetCurrencyNum((int)RoleService.ECurrencyType.GOLD);
        if (haveNum < consumeBean.t_break_consume)
        {
            TipWindow.Singleton.ShowTip("金币不足!");
            return;
        }

        AoyiService.Singleton.ReqLevelBreak(m_petId, m_stoneInfo.id, m_page);
    }

    protected override void OnClose()
    {
        base.OnClose();
    }
}