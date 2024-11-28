using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_MainCity;
using Data.Beans;

public enum HeadUnlockCoditionType
{
    PetShape = 1,       // 宠物形态
    HaveSkin = 2,       // 获得皮肤
}

public class ModifyHeadIconWindow : BaseWindow {

    UI_ModifyHeadIconWindow window;
    List<t_headBean> getedHeadBeanList = new List<t_headBean>();
    List<t_headBean> unGetedHeadBeanList = new List<t_headBean>();

    public override void OnOpen()
    {
        base.OnOpen();

        window = getUiWindow<UI_ModifyHeadIconWindow>();
        window.m_closeBtn.onClick.Add(OnCloseBtn);
        window.m_mask.onClick.Add(OnCloseBtn);

        InitData();
        InitView();
    }

    public override void AddEventListener()
    {
        base.AddEventListener();

        GED.ED.addListener(EventID.OnResModifyIcon, OnResModifyIcon);
    }

    public override void RemoveEventListener()
    {
        base.RemoveEventListener();

        GED.ED.removeListener(EventID.OnResModifyIcon, OnResModifyIcon);
    }
    public override void InitView()
    {
        base.InitView();

        InitHeadIconList();
    }

    private void InitData()
    {
        List<t_headBean> headBeanList = ConfigBean.GetBeanList<t_headBean>();
        int count = headBeanList.Count;
        t_headBean headBean = null;
        for (int i = 0; i < count; i++)
        {
            headBean = headBeanList[i];
            if (headBean.t_cond_type == (int)HeadUnlockCoditionType.PetShape)
            {
                // 宠物形态： 宠物ID+星级
                int[] coditionInfo = GTools.splitStringToIntArray(headBean.t_cond_arg);
                if (coditionInfo.Length == 2)
                {
                    Message.Pet.PetInfo petInfo = PetService.Singleton.GetPetInfo(coditionInfo[0]);
                    if (petInfo != null && petInfo.basInfo.star >= coditionInfo[1])
                        getedHeadBeanList.Add(headBean);
                    else
                        unGetedHeadBeanList.Add(headBean);
                }
                else
                {
                    Logger.err("头像表的数据有误：" + headBean.t_id);
                }
            }
            else
            {
                // 皮肤:道具ID
                //Message.Bag.GridInfo gridInfo = 
                unGetedHeadBeanList.Add(headBean);
            }
        }
    }

    private void InitHeadIconList()
    {
        window.m_headIconList.RemoveChildren(0, -1, true);

        HeadListItem item = HeadListItem.CreateInstance();
        item.Init(true, getedHeadBeanList);
        window.m_headIconList.AddChild(item);

        item = HeadListItem.CreateInstance();
        item.Init(false, unGetedHeadBeanList);
        window.m_headIconList.AddChild(item);

        window.m_headIconList.scrollItemToViewOnClick = false;
    }

    private void OnResModifyIcon(GameEvent evt)
    {
        OnCloseBtn();
    }
}
