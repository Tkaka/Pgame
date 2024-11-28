using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_Activity;
using FairyGUI;
using Data.Beans;

public class ActivityPropItem : UI_activityPropItem {

    public int itemID;
    public bool isCanTouch;
    private bool isShowItemInfo;

    private string winName;
    LongPressGesture longPressGesture;

    public new static UI_activityPropItem CreateInstance()
    {
        return (UI_activityPropItem)UIPackage.CreateObject("UI_Activity", "activityPropItem");
    }

    public void Init()
    {
        isShowItemInfo = false;

        if(isCanTouch)
        {
            longPressGesture = new LongPressGesture(m_toucher);
            longPressGesture.onAction.Add(OnLongPressAction);
            longPressGesture.trigger = 1;
            longPressGesture.onEnd.Add(OnLongPressEnd);
        }
        
        InitView();
    }

    private void InitView()
    {
        t_itemBean itemBean = ConfigBean.GetBean<t_itemBean, int>(itemID);
        if (itemBean != null)
        {
            UIGloader.SetUrl(m_iconLoader, UIUtils.GetItemIcon(itemID));
            UIGloader.SetUrl(m_boardLoader, UIUtils.GetItemBorder(itemID, 1000000));
        }
    }

    private void OnLongPressAction()
    {
        if (isShowItemInfo == false)
        {
            isShowItemInfo = true;
            ThreeParam<int, Vector2, GComponent> threePara = new ThreeParam<int, Vector2, GComponent>();
            threePara.value1 = itemID;
            threePara.value2 = new Vector2(this.x + this.size.x * 0.5f, this.y);
            threePara.value3 = this.parent;
            winName = WinMgr.Singleton.Open<ItemTipsWindow>(WinInfo.Create(false, null, true, threePara), UILayer.Popup);
        }
    }

    private void OnLongPressEnd()
    {
        if (isShowItemInfo == true)
        {
            isShowItemInfo = false;
            WinMgr.Singleton.Close(winName);
            winName = null;
        }
    }

    public override void Dispose()
    {
        winName = null;
        if (longPressGesture != null)
        {
            longPressGesture.Dispose();
        }

        base.Dispose();
    }
}
