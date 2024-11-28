using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_TongXiangGuan;
using FairyGUI;
using Data.Beans;

public class TongXiangGuanPage : UI_tongXiangGuanPage {

    public int pageIndex;

    public new static TongXiangGuanPage CreateInstance()
    {
        return (TongXiangGuanPage)UIPackage.CreateObject("UI_TongXiangGuan", "tongXiangGuanPage");
    }

    public void Init()
    {
        InitZhanTingList();
    }

    private void InitZhanTingList()
    {
        int index = 1;
        int count = 4;
        int exhibitionBeanIndex = 0;
        ZhanTingItem zhanTingItem = null;
        List<t_exhibitionBean> exhibitionBeanList = ConfigBean.GetBeanList<t_exhibitionBean>();
        int exhibitionCount = exhibitionBeanList.Count;
        t_exhibitionBean exhibitionBean = null;
        for (int i = 0; i < count; i++, index++)
        {
            exhibitionBeanIndex = 4 * pageIndex + i;
            if (exhibitionBeanIndex >= exhibitionCount)
                break;

            exhibitionBean = exhibitionBeanList[exhibitionBeanIndex];
            GGraph pos = this.GetChildAt(index) as GGraph;
            zhanTingItem = ZhanTingItem.CreateInstance();
            zhanTingItem.zhanTingID = exhibitionBean.t_id;
            zhanTingItem.Init();
            pos.ReplaceMe(zhanTingItem);
        }
    }

    public void RefreshView()
    {
        RefreshZhanTingList();
    }

    private void RefreshZhanTingList()
    {
        int count = 4;
        int index = 1;
        ZhanTingItem zhanTingItem = null;
        for (int i = 0; i < count; i++, index ++)
        {
            zhanTingItem = this.GetChildAt(index) as ZhanTingItem;
            zhanTingItem.RefreshView();
        }
    }

    public void OnResExhibitionRoom()
    {
        int count = 4;
        int index = 1;
        ZhanTingItem zhanTingItem = null;
        for (int i = 0; i < count; i++, index++)
        {
            zhanTingItem = this.GetChildAt(index) as ZhanTingItem;
            zhanTingItem.OnResExhibitionRoom();
        }
    }
}
