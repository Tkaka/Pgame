using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_Common;
using UI_AoYi;
using Message.Bag;
using DG.Tweening;
using FairyGUI;
using Data.Beans;

public class BoxReceiveWidow : BaseWindow {

    long coroutineID;

    UI_BoxReceiveWidow window;
    List<ItemInfo> itemList;
    Tweener tweener;
    private string tipStr;

    public bool isSendMsg = false;
    public override void OnOpen()
    {
        base.OnOpen();

        window = getUiWindow<UI_BoxReceiveWidow>();
        ThreeParam<bool, List<ItemInfo>, string> param = Info.param as ThreeParam<bool, List<ItemInfo>, string>;
        itemList = param.value2;
        isSendMsg = param.value1;
        tipStr = param.value3;
        window.m_confirmBtn.onClick.Add(OnCofirmBtnClick);
        coroutineID = -1;

        itemList.Sort(BagService.Singleton.SortItemInfo);

        InitView();
    }

    public override void InitView()
    {
        base.InitView();

        // 播放动效
        window.m_leftStarAnim.m_anim_L.Play();
        window.m_rightStarAnim.m_anim_R.Play();
        if (tipStr != null)
            window.m_tipLabel.text = tipStr;
        InitItemList();
    }
    /// <summary>
    /// 刷新道具列表
    /// </summary>
    private void InitItemList()
    {
        // 道具是一个一个渐渐出现
        // 一次最多只能显示5个，如果多余5个，点击确定后在显示剩下的
        if (coroutineID != -1)
            CoroutineManager.Singleton.stopCoroutine(coroutineID);
        CoroutineManager.Singleton.startCoroutine(AddLevelPropItem());
    }

    IEnumerator AddLevelPropItem()
    {
        window.m_touchMask.touchable = true;
        int num = itemList.Count;
        int count = 0;
        //CommonItem commonItem = null;
        ItemInfo itemInfo = null;
        window.m_itemList.RemoveChildren(0, -1, true);

        if (tweener != null && tweener.IsActive())
            tweener.Kill();

        for (int i = num - 1; i >= 0; i--)
        {
            itemInfo = itemList[i];
            GObject cell = _OnItemCellShow(itemInfo);

            cell.alpha = 0;
            window.m_itemList.AddChild(cell);
            itemList.RemoveAt(i);

            count++;
            if (count >= 5)
            {
                break;
            }
        }

        for (int i = 0; i < count; i++)
        {
            GObject obj = window.m_itemList.GetChildAt(i); 
            tweener = DOTween.To(() => obj.alpha, alpha => obj.alpha = alpha, 1, 0.1f);
            yield return new WaitForSeconds(0.1f);
        }
        window.m_touchMask.touchable = false;
    }

    private GObject _OnItemCellShow(ItemInfo itemInfo)
    {
     
        t_itemBean itemBean = ConfigBean.GetBean<t_itemBean, int>(itemInfo.id);
        if (itemBean.t_tab == (int)ItemCategory.AoyiStone)
        {
            UIPackage.AddPackage(WinEnum.BasePath + WinEnum.UI_AoYi);
            AoyiCommonItem stoneItem = AoyiCommonItem.CreateInstance();
            stoneItem.RefreshView(itemInfo.id, 0, false);
            stoneItem.SetNum(itemInfo.num);
            return stoneItem;
        }
        else
        {
            CommonItem commonItem = CommonItem.CreateInstance();
            commonItem.itemId = itemInfo.id;
            commonItem.itemNum = itemInfo.num;
            commonItem.isShowNum = true;
            commonItem.RefreshView(true);
            return commonItem;
        }
 
    }

    private void OnCofirmBtnClick()
    {
        if (itemList.Count > 0)
        {
            InitItemList();
        }
        else
        {
            OnCloseBtn();
        }
    }

    protected override void OnCloseBtn()
    {
        if (coroutineID != -1)
            CoroutineManager.Singleton.stopCoroutine(coroutineID);
        itemList = null;

        if (isSendMsg)
        {
            GED.ED.dispatchEvent(EventID.OnBoxReceivedWindowClose);
        }

        base.OnCloseBtn();
    }
}
