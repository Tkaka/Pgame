using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_Activity;
using UI_Common;
using Data.Beans;
using Message.Challenge;

public class ZhenRongSelectWindow : BaseWindow {

    UI_ZhenRongSelectWindow window;
    BuZhenColumn buZhenColumn;
    List<int> itemIDList = new List<int>();
    List<int> huanXiangList = new List<int>();
    public override void OnOpen()
    {
        base.OnOpen();

        window = getUiWindow<UI_ZhenRongSelectWindow>();
        (window.m_commonTop as UI_Common.UI_commonTop).m_closeBtn.onClick.Add(OnCloseBtn);
        InitData();
        InitView();
    }

    public override void AddEventListener()
    {
        base.AddEventListener();

        GED.ED.addListener(EventID.OnBuZhenStarFightBtnClick, OnStarFightBtnClick);
        GED.ED.addListener(EventID.OnActivityFightStartRes, OnStarFightRes);
    }

    public override void RemoveEventListener()
    {
        base.RemoveEventListener();
        GED.ED.removeListener(EventID.OnBuZhenStarFightBtnClick, OnStarFightBtnClick);
        GED.ED.removeListener(EventID.OnActivityFightStartRes, OnStarFightRes);
    }

    private void InitData()
    {
        int id = ChallegeService.Singleton.GetDiffictyActID((int)ChallegeService.Singleton.difficultyType);
        if (id == -1)
            return;
        t_dungeon_actBean activityActBean = ConfigBean.GetBean<t_dungeon_actBean, int>(id);
        if (activityActBean != null)
        {
            if (!string.IsNullOrEmpty(activityActBean.t_drop_show_id))
            {
                if (ChallegeService.Singleton.activityType == ActivityType.HuanXiang)
                {
                    ActivityActInfo activityInfo = ChallegeService.Singleton.GetActivityInfoByType(ChallegeService.Singleton.activityType);
                    if (activityInfo != null)
                    {
                        string[] idArr;
                        int length = 0;
                        idArr = activityActBean.t_wave_monster1.Split(';');
                        length = idArr.Length;
                        if (length > activityInfo.phantomIndex)
                        {
                            idArr = idArr[activityInfo.phantomIndex].Split('+');
                            length = idArr.Length;
                            for (int i = 0; i < length; i++)
                            {
                                int tempID = int.Parse(idArr[i]);
                                t_itemBean itemBean = ConfigBean.GetBean<t_itemBean, int>(tempID);
                                if (itemBean != null)
                                {
                                    if (itemBean.t_type == (int)ItemType.PetFragment)
                                        huanXiangList.Add(tempID);
                                    else
                                        itemIDList.Add(tempID);
                                }
                            }
                        }
                    }
                }
                else
                {
                    string[] itemIDArr = activityActBean.t_drop_show_id.Split('+');
                    int length = itemIDArr.Length;
                    for (int i = 0; i < length; i++)
                    {
                        itemIDList.Add(int.Parse(itemIDArr[i]));
                    }
                }
            }
        }
    }

    public override void InitView()
    {
        base.InitView();

        InitNormalView();
        InitItemList();
        InitHuanXiangList();
    }

    private void InitItemList()
    {
        int length = itemIDList.Count;
        CommonItem propItem = null;
        Message.Bag.GridInfo gridInfo = null;
        window.m_propList.RemoveChildren(0, -1, true);
        for (int i = 0; i < length; i++)
        {
            propItem = CommonItem.CreateInstance();
            propItem.itemId = itemIDList[i];
            gridInfo = BagService.Singleton.GetGrid(propItem.itemId);
            propItem.itemNum = gridInfo == null ? 0 : gridInfo.itemInfo.num;
            propItem.isShowNum = false;
            propItem.AddPopupEvent();
            propItem.RefreshView();
            window.m_propList.AddChild(propItem);
        }
    }

    private void InitHuanXiangList()
    {
        if (ChallegeService.Singleton.activityType != ActivityType.HuanXiang)
        {
            window.m_huanXiangGroup.visible = false;
            return;
        }
        window.m_huanXiangGroup.visible = true;
        int length = huanXiangList.Count;
        CommonItem propItem;
        Message.Bag.GridInfo gridInfo = null;
        window.m_huanXiangList.RemoveChildren(0, -1, true);
        for (int i = 0; i < length; i++)
        {
            propItem = CommonItem.CreateInstance();
            propItem.itemId = huanXiangList[i];
            gridInfo = BagService.Singleton.GetGrid(propItem.itemId);
            propItem.itemNum = gridInfo == null ? 0 : gridInfo.itemInfo.num;
            propItem.isShowNum = false;
            propItem.AddPopupEvent();
            propItem.RefreshView();
            window.m_huanXiangList.AddChild(propItem);
        }
    }

    private void InitNormalView()
    {
        buZhenColumn = new BuZhenColumn((UI_buZhenColumn)window.m_buZhenColumn, false, "开始战斗");
        UIGloader.SetUrl(window.m_activityTypeLoader, ChallegeService.Singleton.GetActivityTypeIcon());
        (window.m_commonTop as UI_Common.UI_commonTop).m_title.text = "活动副本";
        InitSpecialRule();
    }

    private void InitSpecialRule()
    {
        switch (ChallegeService.Singleton.activityType)
        {
            case ActivityType.Gold:
            case ActivityType.Exp:
                window.m_specialRuleGroup.visible = true;
                break;
            case ActivityType.NvGeDouJia:
            case ActivityType.HuanXiang:
                window.m_specialRuleGroup.visible = false;
                break;

            default:
                break;
        }
        // 180210 特殊属性对应星级     180211 特殊属性值
        t_globalBean starGlobalBean = ConfigBean.GetBean<t_globalBean, int>(180210);
        t_globalBean valueGlobalBean = ConfigBean.GetBean<t_globalBean, int>(180211);
        if (starGlobalBean != null && valueGlobalBean != null)
        {
            // TODO : 语言ID
            if (!string.IsNullOrEmpty(starGlobalBean.t_string_param))
            {
                string[] starArr = starGlobalBean.t_string_param.Split('+');
                int difficultyIndex = (int)ChallegeService.Singleton.difficultyType;
                if (starArr.Length >= difficultyIndex)
                {
                    string star = starArr[difficultyIndex - 1];
                    window.m_specialRuleLabel.text = string.Format("{0}星以上宠物攻击+{1}", star, valueGlobalBean.t_int_param);
                }
            }
        }
    }

    private void OnStarFightBtnClick(GameEvent evt)
    {
        // 请求开始战斗
        ChallegeService.Singleton.ReqActivityFightStart(ChallegeService.Singleton.activityType, ChallegeService.Singleton.difficultyType);
    }

    private void OnStarFightRes(GameEvent evt)
    {
        int res = (int)evt.Data;
        if (res == 0)
        {
            // 请求战斗失败
        }
        else
        {
            // 开始战斗
            //ChallegeService.Singleton.ReqActivityFightEnd(1, 100);
            OnCloseBtn();
            WinMgr.Singleton.CloseAll();
            SceneLoader.Singleton.nextState = GameState.Battle;
            SceneLoader.Singleton.sceneName = FightService.Singleton.SceneName;
            GameManager.Singleton.changeState(GameState.Loading);
        }
    }

    protected override void OnCloseBtn()
    {
        huanXiangList.Clear();
        huanXiangList = null;

        itemIDList.Clear();
        itemIDList = null;

        if (buZhenColumn != null)
        {
            buZhenColumn.OnDispose();
        }

        base.OnCloseBtn();
    }
}
