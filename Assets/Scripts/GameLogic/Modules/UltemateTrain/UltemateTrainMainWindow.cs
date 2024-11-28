using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_UltemateTrain;
using Message.Challenge;
using Data.Beans;
using FairyGUI;
using DG.Tweening;

enum TrainFloorType
{
    None = 0,
    Monster = 1,        // 怪物层
    Box = 2,            // 宝箱层
    Property = 3,       // 属性层
}

public class UltemateTrainMainWindow : BaseWindow {

    UI_UltemateTrainMainWindow window;
    AdditionListPanel additionListPanel;
    UltemateFloorTipView floorTipView;
    private bool isClick = false;
    private bool isOpenFloorWindow = false;
    private bool isShowFloorAnim = false;
    private bool floorAnimIsPlay = false;
    private Transform playerParentTrans;
    private Transform monsterParentTrans;
    private GameObject sceneGO;
    private GameObject monsterGO;

    private SimpleInterval simpleInterval;
    private Camera CurCamera;
    public override void OnOpen()
    {
        base.OnOpen();
        RestoreWndMgr.Singleton.ClearData();
        window = getUiWindow<UI_UltemateTrainMainWindow>();
        PetService.Singleton.zhenRongType = ZhenRongType.ZhongJiShiLian;

        if (TopRoleInfo.instance != null)
            TopRoleInfo.instance.TopType = TopRoleInfoType.ZhongJiShiLian;

        BindEvent();
        InitView();
        RefreshView();
    }

    public override void PlayOpenEffect()
    {
        base.PlayOpenEffect();

        (window.m_commonTop as UI_Common.UI_commonTop).m_anim.Play();
    }

    public override void AddEventListener()
    {
        base.AddEventListener();

        GED.ED.addListener(EventID.OnResTrainFloor, OnResTrainFloor);
        GED.ED.addListener(EventID.OnResTrainInfo, OnResTrainInfo);
        GED.ED.addListener(EventID.OnResTrialScoreAwardInfo, OnResTrialScoreAwardInfo);
        GED.ED.addListener(EventID.OnBoxReceivedWindowClose, OnBoxReceivedWindowClose);
        GED.ED.addListener(EventID.OnResTrialSkip, OnResTrialSkip);
        GED.ED.addListener(EventID.OnLeavePropertyFloor, OnLeavePropertyFloor);
        GED.ED.addListener(EventID.OnLeaveSecretBoxWindow, OnLeaveSecretBoxWindow);
    }

    public override void RemoveEventListener()
    {
        base.RemoveEventListener();

        GED.ED.removeListener(EventID.OnResTrainFloor, OnResTrainFloor);
        GED.ED.removeListener(EventID.OnResTrainInfo, OnResTrainInfo);
        GED.ED.removeListener(EventID.OnResTrialScoreAwardInfo, OnResTrialScoreAwardInfo);
        GED.ED.removeListener(EventID.OnBoxReceivedWindowClose, OnBoxReceivedWindowClose);
        GED.ED.removeListener(EventID.OnResTrialSkip, OnResTrialSkip);
        GED.ED.removeListener(EventID.OnLeavePropertyFloor, OnLeavePropertyFloor);
        GED.ED.removeListener(EventID.OnLeaveSecretBoxWindow, OnLeaveSecretBoxWindow);
    }

    public override void InitView()
    {
        base.InitView();

        SetShowFloorAnim();
        LoadScene();
        InitFloorTipView();
        InitAdditionPropertyList();
        ShowKeySkipTipWindow();
        InitPlayerModel();

        (window.m_commonTop as UI_Common.UI_commonTop).m_title.text = "终极试炼";
    }

    private void LoadScene()
    {
        sceneGO = this.LoadGo("lvl_hd03_slt", new Vector3(100,0,0));

        if (sceneGO != null)
        {
            playerParentTrans = sceneGO.transform.Find("Character_point");
            monsterParentTrans = sceneGO.transform.Find("Monster_point");
            CurCamera = sceneGO.transform.Find("Main Camera").GetComponent<Camera>();

            ShowFloorAnim();
        }
    }

    private void ShowFloorAnim()
    {
        if (floorAnimIsPlay)
            return;

        floorAnimIsPlay = true;
        // 隐藏窗口UI
        HideView();

        if (sceneGO != null)
        {
            Animator[] animators = sceneGO.GetComponentsInChildren<Animator>();
            int count = animators.Length;
            for (int i = 0; i < count; i++)
            {
                animators[i].enabled = true;
                //if(i == 0)
                //    animators[0].Play("lvl_hd03_slt_up",0,0);
                animators[i].Play(animators[i].GetCurrentAnimatorStateInfo(0).shortNameHash, 0, 0);
                animators[i].speed = isShowFloorAnim ? 1 : 100;
            }
        }
        ShowFloorEffect();
        // 播放动画时隐藏怪物模型
        if (monsterGO != null)
            monsterGO.SetActive(false);
        // 5是 动画片段的持续时间
        float interVale = isShowFloorAnim ? 5 : 0f;
        simpleInterval = new SimpleInterval();
        simpleInterval.DoActionWithTimes(interVale, OnFloorAnimEnd);
    }
    /// <summary>
    /// 显示爬塔时的粒子效果
    /// </summary>
    private void ShowFloorEffect()
    {
        if(sceneGO != null)
        {
            GameObject floorEffectGO = sceneGO.transform.Find("hd03_slt_01_run/lvl_hd03_slt_effect").gameObject;
            if (floorEffectGO != null)
            {
                floorEffectGO.SetActive(false);
                if(isShowFloorAnim)
                    floorEffectGO.SetActive(true);
            }
        }
    }
    /// <summary>
    /// 爬塔动画结束
    /// </summary>
    private void OnFloorAnimEnd()
    {
        // 刷新怪物模型
        RefreshMonsterModel();
        // 禁用掉动画，不然镜头无法抖动
        Animator anim = sceneGO.GetComponentInChildren<Animator>();
        if (anim != null && isShowFloorAnim)
            anim.enabled = false;
        if (isShowFloorAnim)
        {
            if (StageCamera.main != null)
                StageCamera.main.DOShakePosition(0.2f, 0.3f, 50, 3.0f);
            if (CurCamera != null)
                CurCamera.transform.DOShakePosition(0.2f, 0.3f, 50, 3.0f);
        }
        floorAnimIsPlay = false;
        // 显示UI界面
        ShowView();
    }

    private void HideView()
    {
        window.visible = false;
        TopRoleInfo.Hide(this);
    }

    private void ShowView()
    {
        window.visible = true;
        TopRoleInfo.Show();
        PlayOpenEffect();
    }

    /// <summary>
    /// 设置是否需要显示爬塔动画
    /// </summary>
    /// <param name="evt"></param>
    private void SetShowFloorAnim()
    {
        if (IsReachMaxFloor())
        {
            TipWindow.Singleton.ShowTip("恭喜你完成今天的终极试炼");
            isShowFloorAnim = false;
            return;
        }
        int result = UltemateTrainService.Singleton.fightResult;
        isShowFloorAnim = result == 1;
    }

    private void InitFloorTipView()
    {
        floorTipView = new UltemateFloorTipView(window.m_floorTipView);
    }
    /// <summary>
    /// 初始化增加的属性列表
    /// </summary>
    private void InitAdditionPropertyList()
    {
        window.m_additionListPanel.visible = false;
        additionListPanel = new AdditionListPanel(window.m_additionListPanel);
    }
    /// <summary>
    /// 显示一键爬塔的提示窗口
    /// </summary>
    private void ShowKeySkipTipWindow()
    {
        if (IsCanKeySkip())
        {
            WinMgr.Singleton.Open<KeyJumpTipWindow>(null, UILayer.Popup);
        }
    }

    private void InitPlayerModel()
    {
        t_professionBean professionBean = ConfigBean.GetBean<t_professionBean, int>(100);
        if (professionBean != null)
        {
            GameObject playerGo = this.LoadGo(professionBean.t_city_prefab);
            playerGo.transform.SetParent(playerParentTrans);
            playerGo.transform.localPosition = Vector3.zero;
        }
    }

    public override void RefreshView()
    {
        base.RefreshView();

        RefreshBaseInfo();
        RefreshAdditionInfo();
        RefreshFloorTip();
    }

    private void RefreshBaseInfo()
    {
        ResTrialInfo trainInfo = UltemateTrainService.Singleton.trainInfo;
        if (trainInfo != null)
        {
            window.m_floorNum.text = trainInfo.trialInfo.floor + "";
            window.m_starNum.text = trainInfo.trialInfo.star + "";
            window.m_integralNum.text = trainInfo.trialInfo.score + "";
        }
    }

    private void RefreshAdditionInfo()
    {
        ResTrialInfo trainInfo = UltemateTrainService.Singleton.trainInfo;
        if (trainInfo != null)
            additionListPanel.PropertyList = trainInfo.buffs;
    }
    /// <summary>
    /// 刷新玩家层数的提示
    /// </summary>
    private void RefreshFloorTip()
    {
        ResTrialInfo trialInfo = UltemateTrainService.Singleton.trainInfo;
        if (trialInfo != null)
        {
            floorTipView.Floor = trialInfo.trialInfo.floor;
        }
    }

    private void BindEvent()
    {
        (window.m_commonTop as UI_Common.UI_commonTop).m_closeBtn.onClick.Add(OnCloseBtn);
        window.m_additionLoader.onTouchBegin.Add(OnAdditionLoaderTouchBegin);
        window.m_additionLoader.onTouchEnd.Add(OnAdditionLoaderTouchEnd);
        window.m_additionLoader.onRollOut.Add(OnAdditionLoaderTouchEnd);
        window.m_rankBtn.onClick.Add(OnRankBtnClick);
        window.m_rewardBtn.onClick.Add(OnRewardBtnClick);
        window.m_shopBtn.onClick.Add(OnShopBtnClick);
        window.m_ruleBtn.onClick.Add(OnRuleBtnClick);
        window.m_monsterToucher.onClick.Add(OnClickMonsterFloor);
    }

    private void RefreshMonsterModel()
    {
        if (monsterGO != null)
            GameObject.DestroyImmediate(monsterGO);

        string monsterName = "pet_chaomeng_battle";
        int globalID = 0;
        ResTrialInfo trialInfo = UltemateTrainService.Singleton.trainInfo;
        if (trialInfo != null)
        {
            TrainFloorType floorType = GetCurFloorType(trialInfo.trialInfo.floor);
            switch (floorType)
            {
                case TrainFloorType.Monster:
                    if (trialInfo.trialInfo.floor == 9 || trialInfo.trialInfo.floor % 10 == 9)
                        globalID = 1801008;
                    else
                        globalID = 1801007;
                    break;
                case TrainFloorType.Box:
                    globalID = 1801009;
                    break;
                case TrainFloorType.Property:
                    globalID = 1801010;
                    break;
                default:
                    break;
            }
        }

        t_globalBean globalBean = ConfigBean.GetBean<t_globalBean, int>(globalID);
        if (globalBean != null)
            monsterName = globalBean.t_string_param;

        monsterGO = this.LoadGo(monsterName);
        if (monsterGO != null)
        {
            monsterGO.transform.SetParent(monsterParentTrans);
            monsterGO.transform.localPosition = Vector3.zero;
            monsterGO.transform.localEulerAngles = new Vector3(0, 180, 0);
        }
    }

    #region 按钮事件 **************************************************************************************************************************

    private void OnAdditionLoaderTouchBegin()
    {
        window.m_additionListPanel.visible = true;
    }

    private void OnAdditionLoaderTouchEnd()
    {
        window.m_additionListPanel.visible = false;
    }

    private void OnRankBtnClick()
    {
        // 打开排行榜
    }

    private void OnRewardBtnClick()
    {
        // 发送奖励请求
        UltemateTrainService.Singleton.ReqTrialScoreAwardInfo();
    }

    private void OnShopBtnClick()
    {
        // 打开商店界面
        WinMgr.Singleton.Open<ShopMainWindow>(WinInfo.Create(false, null, false, 3), UILayer.Popup);
    }

    private void OnRuleBtnClick()
    {
        // 打开规则界面
        WinMgr.Singleton.Open<UltemateTrainRuleWindow>(null, UILayer.Popup);
    }

    private void OnClickMonsterFloor()
    {
        isShowFloorAnim = true;
        ShowFloorAnim();
        return;

        if (!IsReachMaxFloor())
        {
            // 请求该层信息
            UltemateTrainService.Singleton.ReqUltemateTrialFloorInfo();
            isClick = true;
        }
    }

    #endregion;

    #region 消息事件 *******************************************************************************************************************
    /// <summary>
    /// 购买buff回调
    /// </summary>
    private void OnResTrainFloor(GameEvent evt)
    {
        if (isClick)
        {
            ResTrialInfo trainInfo = UltemateTrainService.Singleton.trainInfo;
            if (trainInfo != null)
            {
                // 判断上一层的类别，如果是怪物层不减
                TrainFloorType floorType = GetCurFloorType(trainInfo.trialInfo.floor);
                additionListPanel.PropertyList = trainInfo.buffs;
                
                switch (floorType)
                {
                    case TrainFloorType.Monster:
                        WinMgr.Singleton.Open<SelectOpponentWindow>(WinInfo.Create(false, null, true, evt.Data as List<TrialMonster>), UILayer.Popup);
                        break;
                    case TrainFloorType.Box:
                        List<Message.Bag.ItemInfo> itemList = UltemateTrainService.Singleton.TransformIntVsIntToItemInfo(evt.Data as List<IntVsInt>);
                        ThreeParam<bool, List<Message.Bag.ItemInfo>,string> param = new ThreeParam<bool, List<Message.Bag.ItemInfo>,string>();
                        param.value1 = true;
                        param.value2 = itemList;
                        WinMgr.Singleton.Open<BoxReceiveWidow>(WinInfo.Create(false, null, true, param), UILayer.Popup);
                        break;
                    case TrainFloorType.Property:
                        WinMgr.Singleton.Open<BuyPropertyWindow>(WinInfo.Create(false, null, true, evt.Data), UILayer.Popup);
                        break;
                    default:
                        break;
                }
                isOpenFloorWindow = true;
            }

            isClick = false;
        }
        else
        {
            RefreshView();
        }
    }
    /// <summary>
    /// 试炼信息回调
    /// </summary>
    /// <param name="evt"></param>
    private void OnResTrainInfo(GameEvent evt)
    {
        RefreshBaseInfo();
    }

    private void OnResTrialScoreAwardInfo(GameEvent evt)
    {
        // 打开奖励面板
        WinMgr.Singleton.Open<TrainScoreRewardWindow>(null, UILayer.Popup);
    }

    private void OnBoxReceivedWindowClose(GameEvent evt)
    {
        
        ResTrialInfo trainInfo = UltemateTrainService.Singleton.trainInfo;
        if (trainInfo != null)
        {
            TrainFloorType floorType = GetCurFloorType(trainInfo.trialInfo.floor);
            if (floorType == TrainFloorType.Box)
            {
                IntVsInt boxInfo = new IntVsInt();
                boxInfo.int1 = trainInfo.trialInfo.floor;
                boxInfo.int2 = 0;
                WinMgr.Singleton.Open<SecretBoxWindow>(WinInfo.Create(false, null, true, boxInfo), UILayer.Popup);
            }
        }
    }

    private void OnResTrialSkip(GameEvent evt)
    {
        RefreshView();
    }

    private void OnLeavePropertyFloor(GameEvent evt)
    {
        if (IsReachMaxFloor())
        {
            TipWindow.Singleton.ShowTip("恭喜你完成今天的终极试炼");
            return;
        }
        RefreshView();
        // 播放上升的动画
        isShowFloorAnim = true;
        ShowFloorAnim();
    }

    private void OnLeaveSecretBoxWindow(GameEvent evt)
    {
        if (IsReachMaxFloor())
        {
            TipWindow.Singleton.ShowTip("恭喜你完成今天的终极试炼");
            return;
        }
        RefreshView();
        // 播放上升的动画，隐藏怪物的模型
        isShowFloorAnim = true;
        ShowFloorAnim();
    }

    private bool IsReachMaxFloor()
    {
        ResTrialInfo trialInfo = UltemateTrainService.Singleton.trainInfo;
        if (trialInfo != null)
        {
            List<t_trialBean> trialBeanList = ConfigBean.GetBeanList<t_trialBean>();
            int maxFloor = trialBeanList.Count;
            return trialInfo.trialInfo.floor >= maxFloor;
        }

        return true;
    }

    #endregion

    #region 简单的数据处理
    /// <summary>
    /// 能否一键跳过
    /// </summary>
    /// <returns></returns>
    private bool IsCanKeySkip()
    {
        ResTrialInfo trainInfo = UltemateTrainService.Singleton.trainInfo;
        if (trainInfo != null)
        {
            // 是否开启不在提示功能
            //bool isCloseTip = true;
            //object obj = PlayerLocalData.GetData(PlayerLocalDataKey.CloseTrainJumpTip, null);
            //if (obj == null)
            //    isCloseTip = false;
            //else
            //{
            //    string showTipStr = obj.ToString();
            //    if (showTipStr == "0")
            //        isCloseTip = false;
            //}
            bool isCloseTip = false;

            // 判断当前层是否小于跳过的层数
            int curFloor = trainInfo.trialInfo.floor;
            int skipFloor = trainInfo.skipFloor;
            if (curFloor < skipFloor && isCloseTip == false)
            {
                return true;
            }
        }

        return false;
    }
    /// <summary>
    /// 获得当前层的类型
    /// </summary>
    private TrainFloorType GetCurFloorType(int floor)
    {
        TrainFloorType floorType = TrainFloorType.None;

        ResTrialInfo trainInfo = UltemateTrainService.Singleton.trainInfo;
        if (trainInfo != null)
        {
            additionListPanel.PropertyList = trainInfo.buffs;
            t_trialBean trialBean = ConfigBean.GetBean<t_trialBean, int>(floor);
            if (trialBean != null)
            {
                floorType = (TrainFloorType)trialBean.t_type;
            }
        }

        return floorType;
    }

    #endregion

    protected override void OnClose()
    {
        UltemateTrainService.Singleton.isOpenWindow = false;
        UltemateTrainService.Singleton.parentWindow = null;
        RestoreWndMgr.Singleton.SaveWndData<UltemateTrainMainWindow>(Info, UILayer.Popup);
        if (sceneGO != null)
            GameObject.DestroyImmediate(sceneGO);

        if (TopRoleInfo.instance != null)
            TopRoleInfo.instance.TopType = TopRoleInfoType.Normal;

        base.OnClose();
    }
}
