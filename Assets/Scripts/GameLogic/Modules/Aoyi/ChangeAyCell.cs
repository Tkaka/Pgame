using UI_AoYi;
using UI_Common;
using Message.Profound;
using Message.Pet;
using FairyGUI;
using System.Collections.Generic;
using Data.Beans;
using System;
using UnityEngine;


public class ChangeAyCell : UI_changeAyCell
{

    private bool m_isEquipGrid = false;         //æ˜¯å¦æ˜¯è£…å¤‡ä¸Šçš„æ ¼å­  
    public StoneInfoExtra stoneExtraInfo;
    public int partID;                      //éƒ¨ä½ID
    private string m_wndName;

    //private ChangeAyCell m_moveItem = null;

    public bool isEquipGrid
    {
        get { return m_isEquipGrid; }
    }

    public new static ChangeAyCell CreateInstance()
    {
        return UI_changeAyCell.CreateInstance() as ChangeAyCell;
    }



    public void RefreshView(StoneInfoExtra stoneInfo, bool isUsing = false, int id = -1, int partId = -1)
    {
        //m_moveItem = null;
        m_isEquipGrid = false;
        _RemovePopupEvent();

        this.partID = partId;
        this.stoneExtraInfo = null;

        if (stoneInfo == null)
        {
            m_objBg.visible = true;
            m_txtUse.visible = false;
            m_itemIcon.visible = false;

            if (id == -1)
            {
                //ä¸éœ€è¦è§£é”
                m_txtUnLockDes.visible = false;
            }
            else
            {
                t_aoyi_pageBean bean = ConfigBean.GetBean<t_aoyi_pageBean, int>(id);
                if (bean == null)
                {
                    m_txtUnLockDes.visible = false;
                }
                else
                {
                    if (RoleService.Singleton.GetRoleInfo().level >= bean.t_level_limit)
                    {
                        m_txtUnLockDes.visible = false;
                    }
                    else
                    {
                        m_txtUnLockDes.visible = true;
                        m_txtUnLockDes.text = string.Format("{0}解锁”", bean.t_level_limit);
                    }
                }
            }
             
        }
        else
        {

            _ShowCellInfo(stoneInfo);
            m_txtUse.visible = isUsing;
        }
    }

    private void _ShowCellInfo(StoneInfoExtra stoneInfoExtra)
    {
        StoneInfo stoneInfo = stoneInfoExtra.stoneInfo;
        this.stoneExtraInfo = stoneInfoExtra;
        m_objBg.visible = false;
        m_itemIcon.visible = true;
        m_txtUnLockDes.visible = false;

        AoyiCommonItem commonItem = this.m_itemIcon as AoyiCommonItem;
        if (commonItem != null)
        {
            commonItem.RefreshView(stoneInfo.itemId, stoneInfo.bigLevel * 10 + stoneInfo.minLevel);
            //_AddPopupEvent();
        }
    }

    private void _AddPopupEvent()
    {
        this.m_toucher.onTouchBegin.Add(OnItemTouchBegin);
        this.m_toucher.onTouchEnd.Add(OnItemTouchEnd);
        //this.m_toucher.onRollOut.Add(OnItemTouchEnd);
    }

    private void _RemovePopupEvent()
    {
        this.m_toucher.onTouchBegin.Clear();
        this.m_toucher.onTouchEnd.Clear();
        //this.m_toucher.onRollOut.Clear();
    }


    private void OnItemTouchBegin()
    {
        if (stoneExtraInfo == null)
            return;

        FourParam<int, Vector2, GComponent, int> threePara = new FourParam<int, Vector2, GComponent, int>();
        threePara.value1 = stoneExtraInfo.stoneInfo.itemId;
        threePara.value2 = new Vector2(this.x + this.size.x * 0.5f, this.y);
        threePara.value3 = this.parent;
        threePara.value4 = stoneExtraInfo.stoneInfo.bigLevel * 10 + stoneExtraInfo.stoneInfo.minLevel;

        if (m_wndName != null)
            WinMgr.Singleton.Close(m_wndName);
        m_wndName = WinMgr.Singleton.Open<AoyiTipsWnd>(WinInfo.Create(false, null, true, threePara), UILayer.TopHUD);
    }

    private void OnItemTouchEnd()
    {
        WinMgr.Singleton.Close(m_wndName);
        m_wndName = null;
    }

}