using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_Level;
using Message.Dungeon;
using Data.Beans;
using Message.Bag;

public enum BoxType
{
    Act = 0,    // 关卡宝箱
    Star = 1,   // 评星宝箱
}

public class NormalBoxOpenWindow : BaseWindow {
    /// <summary>
    /// 箱子的状态
    /// </summary>
    private int boxStatus = -1;
    private List<ItemInfo> boxItems = new List<ItemInfo>();

    UI_NormalBoxOpenWindow window;
    /// <summary>
    /// 第一个：宝箱类型，第二个：关卡数据，第三个：章节箱子的下标，或者关卡箱子的关卡ID
    /// </summary>
    ThreeParam<BoxType, LevelDataManager, int> windowPara;

    public override void OnOpen()
    {
        base.OnOpen();

        window = getUiWindow<UI_NormalBoxOpenWindow>();
        windowPara = Info.param as ThreeParam<BoxType, LevelDataManager, int>;

        BindEvent();
        InitView();
        PlayPopupAnim(window.m_mask, window.m_popupView);
    }

    private void BindEvent()
    {
        window.m_popupView.m_closeBtn.onClick.Add(OnCloseBtn);
        window.m_popupView.m_cancelBtn.onClick.Add(OnCloseBtn);
        window.m_popupView.m_receiveBtn.onClick.Add(OnReceiveBtnClick);
        window.m_mask.onClick.Add(OnCloseBtn);
    }

    public override void AddEventListener()
    {
        base.AddEventListener();
        GED.ED.addListener(EventID.OnLevelOpenBox, OnReceiveBox);
    }

    public override void RemoveEventListener()
    {
        base.RemoveEventListener();
        GED.ED.removeListener(EventID.OnLevelOpenBox, OnReceiveBox);
    }
    /// <summary>
    /// 领取宝箱
    /// </summary>
    private void OnReceiveBox(GameEvent evt)
    {
        // 收的领取了宝箱的奖励 ，关闭面板
        ThreeParam<bool, List<ItemInfo>, string> param = new ThreeParam<bool, List<ItemInfo>, string>();
        param.value1 = false;
        param.value2 = evt.Data as List<ItemInfo>;
        WinMgr.Singleton.Open<BoxReceiveWidow>(WinInfo.Create(false, null, true, param), UILayer.Popup);
        OnCloseBtn();
    }

    public override void InitView()
    {
        base.InitView();

        switch (windowPara.value1)
        {
            case BoxType.Act:
                InitActBoxView();
                break;
            case BoxType.Star:
                InitStarBoxView();
                break;
            default:
                break;
        }

        InitItemList();

        window.m_popupView.m_receiveBtn.visible = boxStatus == 0;
        window.m_popupView.m_cancelBtn.visible = boxStatus == -1;
        window.m_popupView.m_receiveTipLabel.visible = boxStatus == 1;
    }
    /// <summary>
    /// 初始化关卡箱子的ui
    /// </summary>
    private void InitActBoxView()
    {
        // 设置箱子的状态
        LevelDataManager levelData = windowPara.value2;
        ActInfo actInfo = LevelService.Singleton.GetActInfoByID(windowPara.value3);
        boxStatus = actInfo.boxStatus;
        // TODO：语言包处理
        window.m_popupView.m_tipLabel.text = "宝箱奖励";
        window.m_popupView.m_starGroup.visible = false;
        window.m_popupView.m_nameLabel.visible = true;
        t_dungeon_actBean actBean = ConfigBean.GetBean<t_dungeon_actBean, int>(windowPara.value3);
        if (actBean != null)
        {
            window.m_popupView.m_nameLabel.text = string.Format("通关{0}-{1} {2} 可领取", levelData.CurrSelectChapterID % 100, windowPara.value3 % 100, actBean.t_name_id);
            if (!string.IsNullOrEmpty(actBean.t_box_item_id))
            {
                string[] boxItemArr = actBean.t_box_item_id.Split(';');
                int count = boxItemArr.Length;
                ItemInfo itemInfo = null;
                for (int i = 0; i < count; i++)
                {
                    string[] itemInfoArr = boxItemArr[i].Split('+');
                    itemInfo = new ItemInfo();
                    if (itemInfoArr.Length >= 2)
                    {
                        itemInfo.id = int.Parse(itemInfoArr[0]);
                        itemInfo.num = int.Parse(itemInfoArr[1]);
                        boxItems.Add(itemInfo);
                    }
                }
            }
        }
    }
    /// <summary>
    /// 初始化章节星级箱子的UI
    /// </summary>
    private void InitStarBoxView()
    {
        // 设置箱子的状态
        LevelDataManager levelData = windowPara.value2;
        ChapterInfo chapterInof = LevelService.Singleton.GetChapterInfoByID(levelData.CurrSelectChapterID);
        if (windowPara.value3 < chapterInof.boxStatus.Count)
        {
            boxStatus = chapterInof.boxStatus[windowPara.value3];
        }
        window.m_popupView.m_tipLabel.text = "评星宝箱";
        window.m_popupView.m_starGroup.visible = true;
        window.m_popupView.m_nameLabel.visible = false;

        t_dungeon_chapterBean chapterBean = ConfigBean.GetBean<t_dungeon_chapterBean, int>(levelData.CurrSelectChapterID);
        int needStar = 0;
        if (chapterBean != null)
        {
            if (!string.IsNullOrEmpty(chapterBean.t_box))
            {
                string[] boxArr = chapterBean.t_box.Split(';');
                if (boxArr.Length > windowPara.value3)
                {
                    // 获得箱子中的道具信息
                    string[] boxInfo = boxArr[windowPara.value3].Split('+');
                    needStar = int.Parse(boxInfo[0]);

                    int count = boxInfo.Length / 2;
                    ItemInfo itemInfo = null;
                    for (int i = 0; i < count; i++)
                    {
                        itemInfo = new ItemInfo();
                        itemInfo.id = int.Parse(boxInfo[i * 2 + 1]);
                        itemInfo.num = int.Parse(boxInfo[i * 2 + 2]);
                        boxItems.Add(itemInfo);
                    }
                }
            }
        }
        ChapterInfo chapterInfo = LevelService.Singleton.GetChapterInfoByID(LevelService.Singleton.currSelectChapterID);
        int haveStar = chapterInfo.star;
        window.m_popupView.m_starNum.text = string.Format("{0}/{1}", haveStar, needStar);
    }
    /// <summary>
    /// 显示宝箱里的道具信息
    /// </summary>
    private void InitItemList()
    {
        int count = boxItems.Count;
        CommonItem propItem;
        for (int i = 0; i < count; i++)
        {
            propItem = CommonItem.CreateInstance();
            window.m_popupView.m_itemList.AddChild(propItem);
            propItem.itemId = boxItems[i].id;
            propItem.itemNum = boxItems[i].num;
            propItem.isShowNum = true;
            propItem.AddPopupEvent();
            propItem.RefreshView();
        }
    }

    private void OnReceiveBtnClick()
    {
        LevelDataManager levelData = windowPara.value2;
        // 点击领取按钮，发送请求
        switch (windowPara.value1)
        {
            case BoxType.Act:
                LevelService.Singleton.ReqOpenActBox(windowPara.value3);
                break;
            case BoxType.Star:
                LevelService.Singleton.ReqOpenChapterBox(levelData.CurrSelectChapterID, windowPara.value3);
                break;
            default:
                break;
        }
    }

    protected override void OnClose()
    {
        base.OnClose();

        window = null;
        windowPara = null;
    }
}
