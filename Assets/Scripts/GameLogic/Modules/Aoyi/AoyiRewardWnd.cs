using UI_AoYi;
using UI_Common;
using Message.Profound;
using Message.Pet;
using FairyGUI;
using System.Collections.Generic;
using Data.Beans;
using UnityEngine;
using System;

public class AoyiRewardWnd : BaseWindow
{

    private UI_AoyiRewardWnd m_window;

    private int m_petId;
    private List<int> m_petSkillList;

    private int m_getCoinNum = 0;
    private int m_getAyNum = 0;

    public override void OnOpen()
    {
        base.OnOpen();
        m_window = getUiWindow<UI_AoyiRewardWnd>();
        m_window.m_btnClose.onClick.Add(Close);
        m_window.m_btnOneKeyGet.onClick.Add(_OnKeyGetClick);

        m_petId = (int)Info.param;
        _Init();
        _ShowList();

    }

    private void _Init()
    {
        m_window.m_mainList.SetVirtual();
        m_window.m_mainList.itemProvider = _ItemProvider;
        m_window.m_mainList.itemRenderer = _ItemRender;

        t_globalBean gBean = ConfigBean.GetBean<t_globalBean, int>(210105);
        if (gBean != null)
        {
            string[] strItems = GTools.splitString(gBean.t_string_param, ';');
            for (int i = 0; i < strItems.Length; i++)
            {
                int[] arrItem = GTools.splitStringToIntArray(strItems[i], '+');
                if (arrItem == null || arrItem.Length < 2)
                    continue;

                if (arrItem[0] == (int)RoleService.ECurrencyType.GOLD)
                    m_getCoinNum = arrItem[1];

                if (arrItem[0] == (int)RoleService.ECurrencyType.AoyiJingHua)
                    m_getAyNum = arrItem[1];          
            }
        }
    }

    private string _ItemProvider(int index)
    {
        return UI_objAyRewardCell.URL;
    }

    private void _ItemRender(int index, GObject obj)
    {
        UI_objAyRewardCell cell = obj as UI_objAyRewardCell;
        if (cell == null)
            return;

        if (index < 0 || index >= m_petSkillList.Count)
            return;

        int aoyiId = m_petSkillList[index];
        t_aoyi_zuheBean zuheBean = ConfigBean.GetBean<t_aoyi_zuheBean, int>(aoyiId);
        if (zuheBean == null)
            return;

        cell.m_txtAoyiName.text = zuheBean.t_name;
        cell.m_txtCoinNum.text = m_getCoinNum + "";
        cell.m_txtAyNum.text = m_getAyNum + "";

        int state = AoyiService.Singleton.GetAySkillActiveState(aoyiId);
        AoyiIconList aoyiIconList = cell.m_ayIconList as AoyiIconList;
        if (aoyiIconList != null)
        {
            aoyiIconList.RefreshView(GTools.splitStringToIntList(zuheBean.t_group, '+'), state > 0);
        }

        cell.m_imgBg.grayed = state == 0;
        cell.m_btnGetReward.visible = state == 1;
        cell.m_objActive.visible = state == 2;
        cell.m_btnGetReward.onClick.Clear();
        cell.m_btnGetReward.onClick.Add(() => 
        {
            AoyiService.Singleton.ReqGetReward(false, m_petId, aoyiId);
        });
    }

    private void _ShowList()
    {
        t_petBean petBean = ConfigBean.GetBean<t_petBean, int>(m_petId);
        if (petBean == null)
            return;

       m_petSkillList = GTools.splitStringToIntList(petBean.t_aoyi, '+');

        m_petSkillList.Sort(_SortFun);
        m_window.m_mainList.numItems = m_petSkillList.Count;

        _CheckShowOneKeyBtn();

    }

    private int _SortFun(int a, int b)
    {
        int stateA = AoyiService.Singleton.GetAySkillActiveState(a);
        int stateB = AoyiService.Singleton.GetAySkillActiveState(b);
        if(stateA != stateB)
            return -stateA.CompareTo(stateB);

        int numA = GTools.splitString(ConfigBean.GetBean<t_aoyi_zuheBean, int>(a).t_group, '+').Length;
        int numB = GTools.splitString(ConfigBean.GetBean<t_aoyi_zuheBean, int>(b).t_group, '+').Length;
        return numA.CompareTo(numB);
    }

    private void _CheckShowOneKeyBtn()
    {
        bool isShow = false;
        for (int i = 0; i < m_petSkillList.Count; i++)
        {
            if (AoyiService.Singleton.GetAySkillActiveState(m_petSkillList[i]) == 1)
            {
                //存在未领取的
                isShow = true;
                break;
            }
        }

        m_window.m_btnOneKeyGet.visible = isShow;
    }

    public override void AddEventListener()
    {
        base.AddEventListener();
        GED.ED.addListener(EventID.AoyiRewardStateChange, _OnAoyiRewardStateChange);
    }

    public override void RemoveEventListener()
    {
        base.RemoveEventListener();
        GED.ED.removeListener(EventID.AoyiRewardStateChange, _OnAoyiRewardStateChange);
    }


    private void _OnAoyiRewardStateChange(GameEvent evt)
    {

        m_window.m_mainList.RefreshVirtualList();
        _CheckShowOneKeyBtn();
    }


    private void _OnKeyGetClick()
    {
        AoyiService.Singleton.ReqGetReward(true, m_petId);
    }

    protected override void OnClose()
    {
        base.OnClose();
    }
}