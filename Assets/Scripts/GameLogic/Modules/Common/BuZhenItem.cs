using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_Common;
using FairyGUI;
using Data.Beans;
using Message.Pet;

public class BuZhenItem : UI_buZhenItem {

    public int petID;
    StarList starList;

    public new static BuZhenItem CreateInstance()
    {
        return UI_buZhenItem.CreateInstance() as BuZhenItem;
    }

    public void Init()
    {
        m_toucher.onClick.Add(OnClickItem);
        starList = new StarList((UI_StarList)m_starList);
    }

    public void RefreshView()
    {
        m_petInfoGroup.visible = petID != 0;
        m_noPetGroup.visible = petID == 0;

        if (petID != 0)
        {
            PetInfo petInfo = PetService.Singleton.GetPetInfo(petID);
            if (petInfo != null)
            {
                starList.SetStar(petInfo.basInfo.star);

                t_petBean petBean = ConfigBean.GetBean<t_petBean, int>(petID);
                if (petBean != null)
                {
                    UIGloader.SetUrl(m_iconLoader,UIUtils.GetIconPath(petBean, petInfo.basInfo.star));
                    UIGloader.SetUrl(m_boardLoader, UIUtils.GetLoaderUrl(WinEnum.UI_Common, UIUtils.GetBorderByQuality(petInfo.basInfo.color)));
                }
            }
        }
    }

    private void OnClickItem()
    {
        WinInfo info = new WinInfo();
        TwoParam<int, ShangZhenSelectType> param = new TwoParam<int, ShangZhenSelectType>();
        param.value1 = petID;
        param.value2 = ShangZhenSelectType.Default;
        info.param = param;
        WinMgr.Singleton.Open<ShangZhenWindow>(info, UILayer.Popup);
    }
}
