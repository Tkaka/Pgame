using UI_AoYi;
using UI_Common;
using Message.Profound;
using Message.Pet;
using FairyGUI;
using System.Collections.Generic;
using Data.Beans;
using UnityEngine;
using System;

public class AoyiOnekeyStrengthWnd : BaseWindow
{
    private UI_AoyiOnekeyStrengthWnd m_window;
    private int m_petId;
    private AoyiService.EStonePage m_page;
    private int m_curTargetLevel = 0;                           //当前目标等级
    private int m_maxLevel = 110;                               //最大等级
    private Dictionary<StoneInfo, bool> m_selectStoneDic = new Dictionary<StoneInfo, bool>();
    private Dictionary<int, AoyiService.PropertyInfo> m_curAddProperty = new Dictionary<int, AoyiService.PropertyInfo>();
    private Dictionary<int, AoyiService.PropertyInfo> m_targetLevelAddProperty = new Dictionary<int, AoyiService.PropertyInfo>();

    public override void OnOpen()
    {
        base.OnOpen();
        m_window = getUiWindow<UI_AoyiOnekeyStrengthWnd>();
        m_window.m_btnClose.onClick.Add(Close);
        m_window.m_btnOneKey.onClick.Add(_OnOneKeyStrengthClick);
        m_window.m_slider.onChanged.Add(_OnSidlerValueChange);
        m_window.m_slider.value = 0;
        _Init();
        _ShowStonesInfo();
        _ShowTargetLevelInfo();
    }


    private void _Init()
    {
        TwoParam<int, AoyiService.EStonePage> param = Info.param as TwoParam<int, AoyiService.EStonePage>;
        if (param == null)
        {
            Debug.LogError("传入参数异常");
            Close();
            return;
        }

        m_petId = param.value1;
        m_page = param.value2;

        StonePage page = AoyiService.Singleton.GetPetPageStoneInfos(m_petId, m_page);
        if (page == null || page.stones.Count == 0)
        {
            TipWindow.Singleton.ShowTip("当前页没有装备任何奥义石!");
            Close();
            return;
        }

        for (int i = 0; i < page.stones.Count; i++)
        {
            StoneInfo stoneInfo = page.stones[i];
            if (!m_selectStoneDic.ContainsKey(stoneInfo))
            {
                //默认选中
                m_selectStoneDic.Add(stoneInfo, true);
            }
        }
    }

    //显示装备的石头信息
    private void _ShowStonesInfo()
    {

        m_window.m_StoneGridList.RemoveChildren(0, -1, true);
        foreach (var info in m_selectStoneDic)
        {
            StoneInfo stoneInfo = info.Key;
            UI_AoyiStrengthCell cell = UI_AoyiStrengthCell.CreateInstance();
            cell.m_checkBox.selected = info.Value;
            AoyiCommonItem commonItem = cell.m_aoyiItem as AoyiCommonItem;
            if (commonItem != null)
            {
                commonItem.RefreshView(stoneInfo.itemId, stoneInfo.bigLevel * 10 + stoneInfo.minLevel);
            }

            cell.m_checkBox.onChanged.Add(() =>
            {
                m_selectStoneDic[stoneInfo] = cell.m_checkBox.selected;
                _ShowTargetLevelInfo();
            });
            m_window.m_StoneGridList.AddChild(cell);
        }
    }

    //显示当前属性信息
    private void _ShowPropertyInfo()
    {
        m_window.m_propertyList.RemoveChildren(0, -1, true);
        _InitAddPropertyList();
        _InitAddPropertyList(m_curTargetLevel);

        foreach (var info in m_curAddProperty)
        {
            AoyiService.PropertyInfo curProperty = info.Value;
            UI_objPropertyCel3 cell = UI_objPropertyCel3.CreateInstance();
            t_attr_nameBean propertyBean = ConfigBean.GetBean<t_attr_nameBean, int>(curProperty.propertyId);
            if (propertyBean == null)
                continue;

            cell.m_txtPropertyName.text = propertyBean.t_name_id;
            if (propertyBean.t_value_type == 0)
            {
                cell.m_txtPropertyValue.text = curProperty.propertyValue * 0.01 + "%";
                if (m_targetLevelAddProperty.ContainsKey(info.Key))
                {
                    cell.m_txtPropertyNextValue.text = m_targetLevelAddProperty[info.Key].propertyValue * 0.01 + "%";
                }
                

            }
            else
            {
                cell.m_txtPropertyValue.text = curProperty.propertyValue + "";
                if (m_targetLevelAddProperty.ContainsKey(info.Key))
                {
                    cell.m_txtPropertyNextValue.text = m_targetLevelAddProperty[info.Key].propertyValue + "";
                }
            }

            m_window.m_propertyList.AddChild(cell);
        }
    }


    //获得当前选中的石头到目标等级增加的总属性列表(默认为当前等级属性)
    private void _InitAddPropertyList(int targetLevel = -1)
    {
        Dictionary<int, AoyiService.PropertyInfo> propertyDic = targetLevel == -1 ? m_curAddProperty : m_targetLevelAddProperty;
        propertyDic.Clear();

        foreach (var info in m_selectStoneDic)
        {
            if (info.Value == false)
                continue;

            StoneInfo stoneInfo = info.Key;
            int curLevel = stoneInfo.bigLevel * 10 + stoneInfo.minLevel;
            if (curLevel < targetLevel)
                curLevel = targetLevel;

            var propertyList = AoyiService.Singleton.GetStoneAddPropertyInfo(stoneInfo.itemId, curLevel);
            for (int i = 0; i < propertyList.Count; i++)
            {
                int propertyId = propertyList[i].propertyId;
                float propertyValue = propertyList[i].propertyValue;

                if (propertyDic.ContainsKey(propertyId))
                {
                    propertyDic[propertyId].propertyValue += propertyValue;
                }
                else
                {
                    AoyiService.PropertyInfo property = new AoyiService.PropertyInfo();
                    property.propertyId = propertyId;
                    property.propertyValue = propertyValue;
                    propertyDic.Add(propertyId, property);
                }
            }

        }
    }


    //滑动条变化
    private void _OnSidlerValueChange()
    {
        int lastLevel = m_curTargetLevel;

        m_curTargetLevel = Mathf.CeilToInt((float)m_window.m_slider.value * m_maxLevel * 0.01f);
        m_curTargetLevel = Mathf.Max(1, m_curTargetLevel);

        //没有变化不管
        if (lastLevel == m_curTargetLevel)
            return;

        _ShowTargetLevelInfo();

    }

    //显示拖动到目标等级的信息
    private void _ShowTargetLevelInfo()
    {
        m_window.m_txtLevelDes.text = string.Format("提升至{0}级", m_curTargetLevel);
        int coinNum = 0;
        int aoyiNum = 0;
        _GetCurNeedComsumeInfo2(out aoyiNum, out coinNum);

        long haveAoyiNum = RoleService.Singleton.GetCurrencyNum((int)RoleService.ECurrencyType.AoyiJingHua);
        long haveCoinNum = RoleService.Singleton.GetCurrencyNum((int)RoleService.ECurrencyType.GOLD);

        m_window.m_txtAyNum.text = string.Format("{0}/{1}", aoyiNum, haveAoyiNum);
        m_window.m_txtAyNum.color = haveAoyiNum >= aoyiNum ? Color.green : Color.red;

        m_window.m_txtCoinNum.text = string.Format("{0}/{1}", coinNum, haveCoinNum);
        m_window.m_txtCoinNum.color = haveCoinNum >= coinNum ? Color.green : Color.red;

        _ShowPropertyInfo();
    }

    //获得当前需要消耗的奥义精华
    private void _GetCurNeedComsumeInfo(out int comsumeAoyiNum, out int comsumeCoinNum)
    {
        comsumeAoyiNum = 0;
        comsumeCoinNum = 0;
        foreach (var info in m_selectStoneDic)
        {
            if (info.Value == false)
                continue;

            StoneInfo stoneInfo = info.Key;
            int curLevel = stoneInfo.bigLevel * 10 + stoneInfo.minLevel;
            if (curLevel >= m_curTargetLevel)
                continue;

            int quility = UIUtils.GetDefaultItemQuality(stoneInfo.itemId);
            int disLevel = m_curTargetLevel - curLevel;
            int lastBreakLevel = stoneInfo.bigLevel;    //上次石头的突破等级
            for (int i = 1; i <= disLevel; i++)
            {
                int stoneBreakLevel = (curLevel + i) / 10;
                stoneBreakLevel = stoneBreakLevel >= m_maxLevel / 10 ? 10 : stoneBreakLevel;

                t_aoyi_level_consumeBean comsumeBean = ConfigBean.GetBean<t_aoyi_level_consumeBean, int>(stoneBreakLevel * 10 + quility);
                if (comsumeBean == null)
                    continue;

                comsumeAoyiNum += comsumeBean.t_level_consume;
                if (stoneBreakLevel > lastBreakLevel)
                {
                    comsumeCoinNum += comsumeBean.t_break_consume;
                    lastBreakLevel = stoneBreakLevel;
                }
            }
        }

    }

    //获得当前需要消耗的奥义精华
    private void _GetCurNeedComsumeInfo2(out int comsumeAoyiNum, out int comsumeCoinNum)
    {
        comsumeAoyiNum = 0;
        comsumeCoinNum = 0;
        foreach (var info in m_selectStoneDic)
        {
            if (info.Value == false)
                continue;

            StoneInfo stoneInfo = info.Key;
            int curLevel = stoneInfo.bigLevel * 10 + stoneInfo.minLevel;
            if (curLevel >= m_curTargetLevel)
                continue;

            int quility = UIUtils.GetDefaultItemQuality(stoneInfo.itemId);
            int targetBeakLevel = m_curTargetLevel / 10;
            targetBeakLevel = targetBeakLevel > 10 ? 10 : targetBeakLevel;

            int disBreakLevel = targetBeakLevel - stoneInfo.bigLevel;
            if (disBreakLevel == 0)
            {
                //当前一次都不需要突破则只需算小等级消耗奥义精华数
                t_aoyi_level_consumeBean comsumeBean = ConfigBean.GetBean<t_aoyi_level_consumeBean, int>(stoneInfo.bigLevel * 10 + quility);
                if (comsumeBean == null)
                    continue;

                int disLevel = (m_curTargetLevel % 10) - stoneInfo.minLevel;
                comsumeAoyiNum += disLevel * comsumeBean.t_level_consume;
            }
            else
            {
                for (int i = 0; i <= disBreakLevel; i++)
                {
                    int stoneBreakLevel = stoneInfo.bigLevel + i;
                    t_aoyi_level_consumeBean comsumeBean = ConfigBean.GetBean<t_aoyi_level_consumeBean, int>(stoneBreakLevel * 10 + quility);
                    if (comsumeBean == null)
                        continue;

                    if (stoneBreakLevel == targetBeakLevel)
                    {
                        //已经是目标突破等级了  这时只需要计算突破的小等级数
                        int disLevel = m_curTargetLevel % 10;
                        comsumeAoyiNum += disLevel * comsumeBean.t_level_consume;
                        continue;
                    }

                    if (stoneBreakLevel == stoneInfo.bigLevel)
                    {
                        //算出第一次突破需要升多少小等级
                        int disLevel = 10 - stoneInfo.minLevel;
                        comsumeAoyiNum += disLevel * comsumeBean.t_level_consume;
                    }
                    else
                    {
                        comsumeAoyiNum += 10 * comsumeBean.t_level_consume;
                    }

                    comsumeCoinNum += comsumeBean.t_break_consume;

                }
            }
  
        }

    }

    private void _OnOneKeyStrengthClick()
    {
        List<int> parts = new List<int>();
        foreach (var info in m_selectStoneDic)
        {
            if (info.Value == false)
                continue;

            int targetLevel = info.Key.bigLevel * 10 + info.Key.minLevel;
            if (targetLevel >= m_curTargetLevel)
                continue;

            parts.Add(info.Key.id);
        }

        if (parts.Count == 0)
        {
            TipWindow.Singleton.ShowTip("没有符合强化条件的石头");
            return;
        }
        AoyiService.Singleton.ReqBigOneKeyStrength(m_petId, m_page, m_curTargetLevel, parts);
    }

    public override void AddEventListener()
    {
        base.AddEventListener();
        GED.ED.addListener(EventID.AoyiStoneInfoChange, _OnStoneInfoChange);
    }


    public override void RemoveEventListener()
    {
        base.RemoveEventListener();
        GED.ED.removeListener(EventID.AoyiStoneInfoChange, _OnStoneInfoChange);
    }


    private void _OnStoneInfoChange(GameEvent evt)
    {
        _ShowStonesInfo();
        _ShowTargetLevelInfo();
    }

    protected override void OnClose()
    {
        base.OnClose();
    }
}