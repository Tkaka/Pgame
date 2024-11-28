using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_TongXiangGuan;
using FairyGUI;
using Data.Beans;
using Message.Team;

public class ZhanTingItem : UI_zhanTingItem {

    public int zhanTingID;

    public new static ZhanTingItem CreateInstance()
    {
        return (ZhanTingItem)UIPackage.CreateObject("UI_TongXiangGuan", "zhanTingItem");
    }

    public void Init()
    {
        m_toucher.onClick.Add(OnItemClick);
        m_numLabel.text = zhanTingID + "";
        RefreshView();
    }

    public void RefreshView()
    {
        RefreshBaseView();
        RefreshTongXiangList();

    }

    private void RefreshBaseView()
    {
        bool isUnlock = IsUnlock();
        m_lockGroup.visible = !isUnlock;
        m_tongXiangList.visible = isUnlock;
        m_redPoint.visible = IsShowRedPoint();

        t_exhibitionBean exhibitionBean = ConfigBean.GetBean<t_exhibitionBean, int>(zhanTingID);
        if (exhibitionBean != null)
        {
            m_nameLabel.text = exhibitionBean.t_name;
            UIGloader.SetUrl(m_typeLoader, UIUtils.GetLoaderUrl(WinEnum.UI_Common, UIUtils.GetPetTypeUrl(exhibitionBean.t_type)));
            m_lvLimitLabel.text = string.Format("{0}级解锁", exhibitionBean.t_level);
        }
    }

    private void RefreshTongXiangList()
    {
        t_exhibitionBean exhibitionBean = ConfigBean.GetBean<t_exhibitionBean, int>(zhanTingID);
        if (exhibitionBean != null)
        {
            if (!string.IsNullOrEmpty(exhibitionBean.t_pets))
            {
                string[] petIDs = exhibitionBean.t_pets.Split('+');
                int haveNum = TongXiangGuanServices.Singleton.GetZhanTingStatueNum(zhanTingID - 1);
                int count = petIDs.Length;
                m_tongXiangList.RemoveChildren(0, -1, true);
                for (int i = 0; i < count; i++)
                {
                    int petID = int.Parse(petIDs[i]);
                    GImage img;
                    if (i < haveNum)
                        img = UIPackage.CreateObject(WinEnum.UI_TongXiangGuan, "tongXiangIconLiang").asImage;
                    else
                        img = UIPackage.CreateObject(WinEnum.UI_TongXiangGuan, "tongXiangIconHui").asImage;

                    m_tongXiangList.AddChild(img);
                }
            }
        }
    }


    /// <summary>
    /// 是否解锁
    /// </summary>
    /// <returns></returns>
    private bool IsUnlock()
    {
        Message.Role.RoleInfo roleInfo = RoleService.Singleton.GetRoleInfo();
        t_exhibitionBean exhibitionBean = ConfigBean.GetBean<t_exhibitionBean, int>(zhanTingID);
        if (exhibitionBean != null)
            return roleInfo.level >= exhibitionBean.t_level;

        return false;
    }
    /// <summary>
    /// 是否显示红点提示
    /// </summary>
    /// <returns></returns>
    private bool IsShowRedPoint()
    {
        if (!IsUnlock())
            return false;

        t_exhibitionBean exhibitionBean = ConfigBean.GetBean<t_exhibitionBean, int>(zhanTingID);
        if (exhibitionBean != null)
        {
            if (!string.IsNullOrEmpty(exhibitionBean.t_pets))
            {
                string[] petIDs = exhibitionBean.t_pets.Split('+');
                int count = petIDs.Length;
                int haveNum = TongXiangGuanServices.Singleton.GetZhanTingStatueNum(zhanTingID - 1);
                if (count <= haveNum)
                    return false;
            }
        }

        return true;
    }

    private void OnItemClick()
    {
        // 如果解锁了 进入展厅界面
        if (IsUnlock())
        {
            TongXiangGuanServices.Singleton.ReqExhibitionRoomInfo(zhanTingID);
        }
    }

    public void OnResExhibitionRoom()
    {
        Exhibition exhibition = TongXiangGuanServices.Singleton.exhibitionInfo;
        if (exhibition != null && exhibition.exhibitionId == zhanTingID)
        {
            WinMgr.Singleton.Open<ZhanTingWindow>(WinInfo.Create(), UILayer.Popup);
        }
        
    }
}
