using Data.Beans;
using FairyGUI;
using UI_Talent;
using Message.Talent;
using Message.Role;
using System.Collections.Generic;
using System;
using UnityEngine;

public class TalentMainWnd : BaseWindow
{
    private UI_TalentMainWnd m_window;
    private List<GObject> m_talentObjList = new List<GObject>();    //固定天赋点的天赋列表
    private Dictionary<int, List<int>> m_pageTalentDic = new Dictionary<int, List<int>>();
    private Dictionary<int, GObject> m_talentObjDic = new Dictionary<int, GObject>();
    private int m_curSelectPage = 1;      //当前选中页

    public override void OnOpen()
    {
        base.OnOpen();
        m_window = getUiWindow<UI_TalentMainWnd>();
        m_window.m_btnClose.onClick.Add(Close);
        m_window.m_c1.onChanged.Add(_OnControlChange);
        m_window.m_btnReset.onClick.Add(_OnResetClick);
        m_window.m_btnFrom.onClick.Add(_OnFromClick);
        _InitTalentObjList();
        InitView();
    }

    //初始化天赋点的现实对象列表
    private void _InitTalentObjList()
    {
        m_talentObjList.Clear();
        for (int i = 1; i <= 10; i++)
        {
            Type t = m_window.GetType();
 
            var property = t.GetField(string.Format("m_t{0}", i));
            if (property != null)
            {
                GObject obj = property.GetValue(m_window) as GObject;
                if (obj != null)
                {
                    m_talentObjList.Add(obj);
                }

            }


        }
    }

    public override void AddEventListener()
    {
        base.AddEventListener();
        GED.ED.addListener(EventID.TalentLevelUp, _OnTalentLevelUp);
        GED.ED.addListener(EventID.TalentReset, _OnTalentReset);
        GED.ED.addListener(EventID.TalentPageUnlock, _OnTalentPageUnLock);
    }

    public override void RemoveEventListener()
    {
        base.RemoveEventListener();
        GED.ED.removeListener(EventID.TalentLevelUp, _OnTalentLevelUp);
        GED.ED.removeListener(EventID.TalentReset, _OnTalentReset);
        GED.ED.removeListener(EventID.TalentPageUnlock, _OnTalentPageUnLock);
    }



    public override void InitView()
    {
        base.InitView();
        _ShowTalentPageInfo();
        _ShowRemainTalent();
        m_window.m_c1.selectedIndex = -1;
        m_window.m_c1.selectedIndex = 0;
    }


    private void _OnTalentLevelUp(GameEvent evt)
    {
        TwoParam<int,int> param = evt.Data as TwoParam<int, int>;
        if (param == null || param.value1 != m_curSelectPage)
        {
            return;
        }

        if (m_talentObjDic.ContainsKey(param.value2))
        {
            _ShowTalentInfo(m_talentObjDic[param.value2], param.value2);
        }

        _ShowCurPageDes();
        _ShowRemainTalent();
    }

    private void _OnTalentReset(GameEvent evt)
    {
        InitView();
    }

    private void _OnTalentPageUnLock(GameEvent evt)
    {
        _ShowTalentPageInfo();
    }

    //剩余天赋点数
    private void _ShowRemainTalent()
    {
        long talentNum = RoleService.Singleton.GetCurrencyNum((int)RoleService.ECurrencyType.TALENT);
        m_window.m_txtTalentNum.text = talentNum + "";
    }

    //控制器改变
    private void _OnControlChange()
    {
        switch (m_window.m_c1.selectedIndex)
        {
            case 0:
                m_curSelectPage = 1;
                break;
            case 1:
                m_curSelectPage = 2;
                break;
            case 2:
                m_curSelectPage = 3;
                break;
            case 3:
                m_curSelectPage = 4;
                break;
            default:
                return;
        }


        _ShowCurTalentPageInfo();
    }


    //显示天赋格子
    private void _OnShowTalentCell(UI_talentCell3 cell, string strTalentId, string iconName)
    {
        int[] arr = GTools.splitStringToIntArray(strTalentId, '+');
        if (arr == null || arr.Length < 3)
            return;

        UIGloader.SetUrl(cell.m_imgName, iconName);
        //cell.m_imgName.url = iconName;
        _ShowTalentInfo(cell.m_t1, arr[0]);
        _ShowTalentInfo(cell.m_t2, arr[1]);
        _ShowTalentInfo(cell.m_t3, arr[2]);
    }

    //显示天赋列表
    private void _ShowTalentList()
    {
        m_window.m_talentList.RemoveChildren(0, -1, true);
        t_talent_pageBean pageBean = ConfigBean.GetBean<t_talent_pageBean, int>(m_curSelectPage);
        string[] strTalent = GTools.splitString(pageBean.t_index, ';');
        string[] strIocnName = GTools.splitString(pageBean.t_title2, '+');
        for (int i = 0; i < strTalent.Length; i++)
        {
            UI_talentCell3 cell = UI_talentCell3.CreateInstance();
            string iconName = "";
            if (i < strIocnName.Length)
            {
                iconName = strIocnName[i];
            }
            _OnShowTalentCell(cell, strTalent[i], iconName);
            m_window.m_talentList.AddChild(cell);
        }
    }



    //显示固定个数的天赋
    private void _ShowFixedTalent()
    {
        t_talent_pageBean pageBean = ConfigBean.GetBean<t_talent_pageBean, int>(m_curSelectPage);
        if (pageBean == null)
            return;

        if (!m_pageTalentDic.ContainsKey(m_curSelectPage))
        {
            List<int> talentList = new List<int>();
            string[] strTalent = GTools.splitString(pageBean.t_index, ';');
            for (int i = 0; i < strTalent.Length; i++)
            {
                int talentNum = 3;
                if (i == 1)
                {
                    //第二排4个，其它排3个
                    talentNum = 4;
                }

                int[] arr = GTools.splitStringToIntArray(strTalent[i], '+');
                for (int j = 0; j < talentNum; j++)
                {
                    if (arr != null && arr.Length > 0 && j < arr.Length)
                    {
                        //第一个为全局表ID
                        talentList.Add(arr[j]);
                    }
                    else
                    {
                        talentList.Add(-1);
                    }
                }
 
            }

            m_pageTalentDic.Add(m_curSelectPage, talentList);
        }

        List<int> tList = m_pageTalentDic[m_curSelectPage];
        if (tList.Count != m_talentObjList.Count)
        {
            Debug.LogError("天赋点个数异常");
            return;
        }
        for (int i = 0; i < m_talentObjList.Count; i++)
        {
            if (tList[i] == -1)
            {
                m_talentObjList[i].visible = false;
            }
            else
            {
                m_talentObjList[i].visible = true;
                _ShowTalentInfo(m_talentObjList[i], tList[i]);
            }
        }

    }

    private void _ShowTalentInfo(GObject obj, int id)
    {
        TalentCell cell;
        cell = obj as TalentCell;
        if (cell == null)
        {
            UI_talentCell2 cell2 = obj as UI_talentCell2;
            if (cell2 != null)
            {
                cell = cell2.m_objTalent as TalentCell;
            }
        }

        if (cell != null)
        {
            cell.talentId = id;
            cell.RefreshView();
        }

        cell.onClick.Clear();
        cell.onClick.Add(()=>
        {
            WinMgr.Singleton.Open<TalentShowWnd>(WinInfo.Create(false, null, false, id), UILayer.Popup);
        });

        if (!m_talentObjDic.ContainsKey(id))
        {
            m_talentObjDic.Add(id, obj);
        }
    }

    //显示天赋页签信息
    private void _ShowTalentPageInfo()
    {
        TalentPage page1 = TalentService.Singleton.GetTalentPageInfo(1);
        TalentPage page2 = TalentService.Singleton.GetTalentPageInfo(2);
        TalentPage page3 = TalentService.Singleton.GetTalentPageInfo(3);
        TalentPage page4 = TalentService.Singleton.GetTalentPageInfo(4);
        m_window.m_btnZhenXing.grayed = page1 == null ? true : false;
        m_window.m_btnZhenXing.m_imgLock.visible = page1 == null ? true : false;

        m_window.m_btnJinXiu.grayed = page2 == null ? true : false;
        m_window.m_btnJinXiu.m_imgLock.visible = page2 == null ? true : false;

        m_window.m_btnShenZao.grayed = page3 == null ? true : false;
        m_window.m_btnShenZao.m_imgLock.visible = page3 == null ? true : false;

        m_window.m_btnJiDou.grayed = page4 == null ? true : false;
        m_window.m_btnJiDou.m_imgLock.visible = page4 == null ? true : false;

    }

    //当前选中天赋页的信息
    private void _ShowCurTalentPageInfo()
    {

        _ShowCurPageDes();

        m_talentObjDic.Clear();
        if (m_curSelectPage == 4)
        {
            _ShowTalentList();
        }
        else
        {
            _ShowFixedTalent();
        }
    }

    //显示当前天赋页的描述信息
    private void _ShowCurPageDes()
    {
        t_talent_pageBean pageBean = ConfigBean.GetBean<t_talent_pageBean, int>(m_curSelectPage);
        if (pageBean == null)
            return;

        TalentPage pageInfo = TalentService.Singleton.GetTalentPageInfo(m_curSelectPage);

        m_window.m_txtTalentDes.text = pageBean.t_des;
        //m_window.m_imgType.url = pageBean.t_title;
        UIGloader.SetUrl(m_window.m_imgType, pageBean.t_title);

        if (pageInfo == null)
        {
            //当前天赋页未解锁
            m_window.m_txtUnLockDes.text = string.Format(UIUtils.GetStrByLanguageID(70802501), pageBean.t_level, pageBean.t_befor_point);
        }
        else
        {
            m_window.m_txtUnLockDes.text = string.Format(UIUtils.GetStrByLanguageID(70802502), pageInfo.num);
        }
    }


    //重置点击
    private void _OnResetClick()
    {
        int cosumeNum = ConfigBean.GetBean<t_globalBean, int>(80201).t_int_param;
        string strDes = string.Format(UIUtils.GetStrByLanguageID(70802503), cosumeNum);
        CommonTipsManager.GetInstance().ShowTips(TipsType.SingleButton, UIUtils.GetStrByLanguageID(70802505), strDes, ()=>{
            long haveDiamondNum = RoleService.Singleton.GetCurrencyNum((int)RoleService.ECurrencyType.DIAMOND);
            if (haveDiamondNum < cosumeNum)
            {
                TipWindow.Singleton.ShowTip("钻石不足");
                return;
            }
            TalentService.Singleton.ReqReset();
        });

    }

    private void _OnFromClick()
    {
        string strDes = ConfigBean.GetBean<t_languageBean, int>(70802500).t_content;
        //strDes = strDes.Replace(@"\n", "\n");
        CommonTipsManager.GetInstance().ShowTips(TipsType.SingleButton, UIUtils.GetStrByLanguageID(70802504), strDes, () => {
            Debug.Log("=================>>跳转到关卡");
        });
    }

    protected override void OnClose()
    {
        base.OnClose();
        m_talentObjDic.Clear();
        m_talentObjList.Clear();
        m_pageTalentDic.Clear();
    }
}