using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_ShenQi;
using Data.Beans;
using Message.Team;
using Message.Bag;
using FairyGUI;

public class ShenQiMainWidow : BaseWindow {
    /// <summary>
    /// 是否正在培养
    /// </summary>
    private bool isInCulture;
    private GoWrapper wrapper;
    /// <summary>
    /// 是否是单次培养，0否，1是
    /// </summary>
    private int isSingle;
    UI_ShenQiMainWidow window;

    public override void OnOpen()
    {
        base.OnOpen();

        window = getUiWindow<UI_ShenQiMainWidow>();

        BindEvent();
        InitView();
        RefreshView();
    }

    private void BindEvent()
    {
        window.m_useOneTimeBtn.onClick.Add(OnClickOneTimeCultureBtn);
        window.m_useTenTimeBtn.onClick.Add(OnClickTenTimeCultureBtn);
        window.m_cancelBtn.onClick.Add(OnCancelBtnClick);
        window.m_saveBtn.onClick.Add(OnSaveBtnClick);
        window.m_unlockBtn.onClick.Add(OnUnlockBtnClick);
        window.m_introduceShenQiBtn.onClick.Add(OnIntroduceShenQiBtnClick);
        window.m_nengYuanAddBtn.onClick.Add(OnNengYuanAddBtnClick);
        window.m_tenResAddBtn.onClick.Add(OnNengYuanAddBtnClick);
        window.m_closeTenResBtn.onClick.Add(OnCloseTenResBtnClick);
        window.m_backBtn.onClick.Add(OnCloseBtn);
        window.m_switchLeftBtn.onClick.Add(OnSwitchLeftBtnClick);
        window.m_switchRightBtn.onClick.Add(OnSwitchRightBtnClick);
        window.m_openRecommendBtn.onChanged.Add(OnOpenRecommendBtnChanged);
        window.m_peiYangBtnCtrl.onChanged.Add(OnCultureCtrlChanged);
    }

    public override void AddEventListener()
    {
        base.AddEventListener();

        GED.ED.addListener(EventID.OnResShenQiCulture, OnResShenQiCulture);
        GED.ED.addListener(EventID.OnResShenQiUnlock, OnResShenQiUnlock);
        GED.ED.addListener(EventID.OnResArtifactTrainSave, OnResArtifactTrainSave);
    }

    public override void RemoveEventListener()
    {
        base.RemoveEventListener();

        GED.ED.removeListener(EventID.OnResShenQiCulture, OnResShenQiCulture);
        GED.ED.removeListener(EventID.OnResShenQiUnlock, OnResShenQiUnlock);
        GED.ED.removeListener(EventID.OnResArtifactTrainSave, OnResArtifactTrainSave);
    }

    public override void InitView()
    {
        base.InitView();

        window.m_tenCultureResGroup.visible = false;
        ShenQiService.Singleton.curShenQiID = 1;
        window.m_openRecommendBtn.selected = true;
        window.m_peiYangBtnCtrl.selectedIndex = 0;
        OnCultureCtrlChanged();
    }


    public override void RefreshView()
    {
        base.RefreshView();

        RefreshShenQiList();
        RefreshShenQiListBaseInfo();
        RefreshModel();
        RefreshRightView();
    }

    /// <summary>
    /// 刷新神器头像列表区域
    /// </summary>
    public void RefreshShenQiList()
    {
        int count = window.m_shenQiList.numChildren;
        ShenQiItem shenQiItem = null;
        if (count == 0)
        {
            List<t_artifactBean> artifactBeanList = ConfigBean.GetBeanList<t_artifactBean>();
            count = artifactBeanList.Count;
            for (int i = 0; i < count; i++)
            {
                shenQiItem = ShenQiItem.CreateInstance();
                shenQiItem.shenQiID = artifactBeanList[i].t_id;
                //Logger.err(shenQiItem.shenQiID.ToString());
                shenQiItem.Init(this);
                window.m_shenQiList.AddChild(shenQiItem);
            }
        }
        else
        {
            for (int i = 0; i < count; i++)
            {
                shenQiItem = window.m_shenQiList.GetChildAt(i) as ShenQiItem;
                shenQiItem.RefreshView();
            }
        }
    }

    private void RefreshShenQiListBaseInfo()
    {
        Artifact artifact = ShenQiService.Singleton.GetShenQiInfoByID(ShenQiService.Singleton.curShenQiID);
        bool isUnlock = artifact != null;
        window.m_unlockAddGroup.visible = isUnlock;

        window.m_nengYuanNumLabel.text = ShenQiService.Singleton.GetNengYuanNum() + "";
    }
    /// <summary>
    /// 刷新模型
    /// </summary>
    /// <param name="isChange">是否需要改变模型</param>
    private void RefreshModel(bool isChange = true)
    {
        if(isChange)
        {
            t_artifactBean artifactBean = ConfigBean.GetBean<t_artifactBean, int>(ShenQiService.Singleton.curShenQiID);
            if (artifactBean != null)
            {
                GameObject model = this.LoadGo(artifactBean.t_model);
                model.transform.localPosition = new Vector3(0, 0, 1000);
                model.transform.localScale = new Vector3(120, 120, 120);
                model.transform.localEulerAngles = new Vector3(0, 180, 0);

                if (wrapper == null)
                    wrapper = new GoWrapper(model);
                else
                {
                    GameObject.Destroy(wrapper.wrapTarget);
                    wrapper.setWrapTarget(model, false);
                }

                window.m_modelPos.SetNativeObject(wrapper);
                model.setLayer("UIActor");
                Artifact artifact = ShenQiService.Singleton.GetShenQiInfoByID(ShenQiService.Singleton.curShenQiID);
                if (artifact != null)
                    model.setLayer("UIActor");
                else
                    model.setLayer("UI");

                window.m_nameLabel.text = artifactBean.t_name;
                window.m_jianJieLabel.text = artifactBean.t_introduce_id;
            }
        }
        else
        {
            Artifact artifact = ShenQiService.Singleton.GetShenQiInfoByID(ShenQiService.Singleton.curShenQiID);
            if (artifact != null)
                wrapper.wrapTarget.setLayer("UIActor");
            else
                wrapper.wrapTarget.setLayer("UI");
            // 只需要将模型变亮就行
            //wrapper.wrapTarget.setLayer("UIActor");
        }
        
    }
    /// <summary>
    /// 刷新右边的UI，包括神器解锁条件，和神器的培养界面
    /// </summary>
    public void RefreshRightView()
    {
        Artifact artifact = ShenQiService.Singleton.GetShenQiInfoByID(ShenQiService.Singleton.curShenQiID);
        bool isUnlock = artifact != null;
        if (isUnlock)
        {
            window.m_unlockGroup.visible = true;
            window.m_lockGroup.visible = false;
            RefreshUnlockGroup();
        }
        else
        {
            window.m_unlockGroup.visible = false;
            window.m_lockGroup.visible = true;
            RefreshLockGroup();
        }
    }

    private void RefreshUnlockGroup()
    {
        Artifact artifact = ShenQiService.Singleton.GetShenQiInfoByID(ShenQiService.Singleton.curShenQiID);
        if (artifact != null)
        {
            RefreshAttrList();
            RefreshRightBtnState();
        }
    }

    private void RefreshAttrList(bool isInit = true, bool isShowChange = false)
    {
        Artifact artifact = ShenQiService.Singleton.GetShenQiInfoByID(ShenQiService.Singleton.curShenQiID);
        if (artifact != null)
        {
            int count = 0;
            ShenQiShuXingItem shenQiShuXingItem = null;
            if (isInit)
            {
                count = artifact.artifactAttrs.Count;
                window.m_propertyList.RemoveChildren(0, -1, true);
                
                for (int i = 0; i < count; i++)
                {
                    shenQiShuXingItem = ShenQiShuXingItem.CreateInstance();
                    shenQiShuXingItem.shenQiShuXinID = artifact.artifactAttrs[i].id;
                    shenQiShuXingItem.Init();
                    window.m_propertyList.AddChild(shenQiShuXingItem);
                }
            }
            else
            {
                count = window.m_propertyList.numChildren;
                for (int i = 0; i < count; i++)
                {
                    shenQiShuXingItem = window.m_propertyList.GetChildAt(i) as ShenQiShuXingItem;
                    shenQiShuXingItem.RefreshView(isShowChange);
                }
            }

        }
    }

    private void RefreshRightBtnState()
    {
        if (isInCulture)
        {
            window.m_saveBtn.visible = true;
            window.m_peiYangGroup.visible = false;
        }
        else
        {
            window.m_saveBtn.visible = false;
            window.m_peiYangGroup.visible = true;

            Artifact artifact = ShenQiService.Singleton.GetShenQiInfoByID(ShenQiService.Singleton.curShenQiID);
            if (artifact != null)
            {
                window.m_oneTimeRemainLabel.text = string.Format("{0}/20", artifact.singleTimesOdd);
                window.m_tenTimeRemainLabel.text = string.Format("{0}/20", artifact.tenTimesOdd);
            }
        }
    }
    /// <summary>
    /// 显示属性列表的改变
    /// </summary>
    private void ShowAttrListChange()
    {
        int count = window.m_propertyList.numChildren;
        ShenQiShuXingItem shenQiShuXingItem = null;
        for (int i = 0; i < count; i++)
        {
            shenQiShuXingItem = window.m_propertyList.GetChildAt(i) as ShenQiShuXingItem;
            shenQiShuXingItem.RefreshView(true);
        }
    }

    private void RefreshLockGroup()
    {
        t_artifactBean artifactBean = ConfigBean.GetBean<t_artifactBean, int>(ShenQiService.Singleton.curShenQiID);
        if (artifactBean != null && !string.IsNullOrEmpty(artifactBean.t_open_condition))
        {
            int count = window.m_unlockCoditionList.numChildren;
            UnlockCoditionItem item = null;
            ResArtifactInfo artifactInfo = ShenQiService.Singleton.artifactInfo;
            if (artifactInfo != null)
            {
                string[] openCoditionArr = artifactBean.t_open_condition.Split(';');
                string[] openCoditionIDArr = artifactBean.t_condition_id.Split('+');
                if (artifactInfo != null)
                {
                    if (count == 0)
                    {
                        count = openCoditionArr.Length;
                        for (int i = 0; i < count; i++)
                        {
                            item = UnlockCoditionItem.CreateInstance();
                            item.condition = openCoditionArr[i];
                            if (i < openCoditionIDArr.Length)
                                item.conditionID = int.Parse(openCoditionIDArr[i]);
                            if (i < artifactInfo.conditions.Count)
                                item.conditionState = artifactInfo.conditions[i];
                            item.RefreshView();
                            window.m_unlockCoditionList.AddChild(item);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < count; i++)
                        {
                            item = window.m_unlockCoditionList.GetChildAt(i) as UnlockCoditionItem;
                            if(i < openCoditionArr.Length)
                                item.condition = openCoditionArr[i];
                            if (i < openCoditionIDArr.Length)
                                item.conditionID = int.Parse(openCoditionIDArr[i]);
                            if (i < artifactInfo.conditions.Count)
                                item.conditionState = artifactInfo.conditions[i];
                            item.RefreshView();
                        }
                    }
                }
                
            }
        }
    }

    private void RefreshConsumeView()
    {
        t_artifactBean artifactBean = ConfigBean.GetBean<t_artifactBean, int>(ShenQiService.Singleton.curShenQiID);
        if (artifactBean != null)
        {
            window.m_consumeGoldGroup.visible = true;
            window.m_consumeDiamondGroup.visible = true;
            switch (ShenQiService.Singleton.curCultureMethod)
            {
                case CultureMethod.Primary:
                    window.m_consumeGoldGroup.visible = false;
                    window.m_consumeDiamondGroup.visible = false;
                    break;
                case CultureMethod.Middles:
                    window.m_consumeDiamondGroup.visible = false;
                    break;
                case CultureMethod.Senior:
                    window.m_consumeGoldGroup.visible = false;
                    break;
                default:
                    break;
            }
            if (!string.IsNullOrEmpty(artifactBean.t_comsume))
            {
                string[] consumeArr = artifactBean.t_comsume.Split(';');
                window.m_consumeNengYuanLabel.text = consumeArr[0].Split('+')[1];
                window.m_consumeGoldLabel.text = consumeArr[1].Split('+')[1];
                window.m_consumeDiamondLabel.text = consumeArr[2].Split('+')[1];
            }
        }
    }
    /// <summary>
    /// 刷新培养结果列表
    /// </summary>
    private void RefreshCultureList(bool isRefresh = false)
    {
        if (isRefresh)
        {
            int count = window.m_tenResList.numChildren;
            PeiYangResItem cultureResItem = null;
            for (int i = 0; i < count; i++)
            {
                cultureResItem = window.m_tenResList.GetChildAt(i) as  PeiYangResItem;
                bool isSave = window.m_openRecommendBtn.selected;
                cultureResItem.RefreshView(isSave);
            }
        }
        else
        {
            window.m_tenResList.RemoveChildren(0, -1, true);
            List<ArtifactRandomAttr> randomAttrList = ShenQiService.Singleton.randomAttrList;
            int count = randomAttrList.Count;
            PeiYangResItem cultureResItem = null;
            for (int i = 0; i < count; i++)
            {
                cultureResItem = PeiYangResItem.CreateInstance();
                cultureResItem.attr = randomAttrList[i];
                cultureResItem.Init();
                window.m_tenResList.AddChild(cultureResItem);
            }
        }
        window.m_tenNengYuanLabel.text = ShenQiService.Singleton.GetNengYuanNum() + "";
    }

    public void OnChangeShenQiItem()
    {
        RefreshRightView();
        RefreshShenQiList();
        RefreshShenQiListBaseInfo();
        RefreshModel(true);
    }

    #region  事件响应 ------------------------------------------------------------------

    private void OnSwitchLeftBtnClick()
    {
        // 滑到第一个
        window.m_shenQiList.ScrollToView(0, true);
    }

    private void OnSwitchRightBtnClick()
    {
        // 滑到最后一个
        int count = window.m_shenQiList.numChildren;
        window.m_shenQiList.ScrollToView(count - 1, true);
    }
    private void OnClickOneTimeCultureBtn()
    {
        Artifact shenQi = ShenQiService.Singleton.GetShenQiInfoByID(ShenQiService.Singleton.curShenQiID);
        if (shenQi != null)
        {
            if (shenQi.singleTimesOdd > 0)
            {
                isSingle = 1;
                OnClickPeiYanBtn();
            }
            else
            {
                TipWindow.Singleton.ShowTip("今日次数以用完!");
            }
        }
    }

    private void OnOpenRecommendBtnChanged()
    {
        int isOpen = window.m_openRecommendBtn.selected ? 1 : 0;
        PlayerLocalData.SetData(PlayerLocalDataKey.OpenAutoRecommend, isOpen);
        RefreshCultureList(true);
    }

    private void OnClickTenTimeCultureBtn()
    {
        Artifact shenQi = ShenQiService.Singleton.GetShenQiInfoByID(ShenQiService.Singleton.curShenQiID);
        if (shenQi != null)
        {
            if (shenQi.tenTimesOdd > 0)
            {
                isSingle = 0;
                OnClickPeiYanBtn();
            }
            else
            {
                TipWindow.Singleton.ShowTip("今日次数以用完!");
            }
        }
    }

    private void OnCancelBtnClick()
    {
        if (isInCulture)
        {
            int count = ShenQiService.Singleton.saveInfoList.Count;
            for (int i = 0; i < count; i++)
            {
                ShenQiService.Singleton.saveInfoList[i] = 0;
            }

            ShenQiService.Singleton.ReqArtifactTrainSave();
        }
    }

    private void OnSaveBtnClick()
    {
        if (isInCulture)
        {
            if (isSingle == 1 && ShenQiService.Singleton.saveInfoList.Count != 0)
                ShenQiService.Singleton.saveInfoList[0] = 1;

            ShenQiService.Singleton.ReqArtifactTrainSave();
        }
    }

    private void OnCultureCtrlChanged()
    {
        ShenQiService.Singleton.curCultureMethod = (CultureMethod)(window.m_peiYangBtnCtrl.selectedIndex + 1);
        RefreshConsumeView();
    }

    private void OnUnlockBtnClick()
    {
        if (ShenQiService.Singleton.IsReachedAllCoditions())
        {
            // 发送神器解锁请求
            ShenQiService.Singleton.ReqArtifactUnlock();
        }
        else
        {
            TipWindow.Singleton.ShowTip("完成所有解锁条件以解锁该神器，加油吧！");
        }
    }

    private void OnClickPeiYanBtn()
    {
        int itemID = 0;
        LaiYuanType type = LaiYuanType.Currency;

        // 判断资源是否足
        if (ShenQiService.Singleton.IsEnoughNengYuang())
        {
            bool isCulture = true;
            switch (ShenQiService.Singleton.curCultureMethod)
            {
                case CultureMethod.Primary:

                    break;
                case CultureMethod.Middles:
                    if (!ShenQiService.Singleton.IsEnoughGold())
                    {
                        isCulture = false;
                        itemID = (int)ItemType.Gold;
                        type = LaiYuanType.Currency;
                    }
                    break;
                case CultureMethod.Senior:
                    if (!ShenQiService.Singleton.IsEnoughDiamond())
                    {
                        isCulture = false;
                        itemID = (int)ItemType.Damond;
                        type = LaiYuanType.Currency;
                    }
                    break;
                default:
                    break;
            }

            if (isCulture)
            {
                ShenQiService.Singleton.ReqArtifactTrain(isSingle);
                return;
            }
        }
        else
        {
            itemID = ShenQiService.Singleton.GetNengYuanID();
            type = LaiYuanType.Prop;
        }

        // 打开来源界面
        TwoParam<int, int> twoParam = new TwoParam<int, int>();
        twoParam.value1 = itemID;
        twoParam.value2 = ShenQiService.Singleton.GetNengYuanNum();
        WinMgr.Singleton.Open<LaiYuanWindow>(WinInfo.Create(false, null, true, twoParam), UILayer.Popup);
    }

    private void OnCloseTenResBtnClick()
    {
        // 二次确定弹窗
        AgainConfirmWindow.Singleton.ShowTip("是否关闭洗练界面，关闭后取消保存!", SavePeiYangRes);
    }

    private void OnIntroduceShenQiBtnClick()
    {
        // 直接打开说明面板
        WinMgr.Singleton.Open<IntroduceWindow>(null, UILayer.Popup);
    }

    private void OnNengYuanAddBtnClick()
    {
        // 打开能源来源界面
        TwoParam<int, int> twoParam = new TwoParam<int, int>();
        twoParam.value1 = ShenQiService.Singleton.GetNengYuanID();
        twoParam.value2 = ShenQiService.Singleton.GetNengYuanNum();
        WinMgr.Singleton.Open<LaiYuanWindow>(WinInfo.Create(false, null, true, twoParam), UILayer.Popup);
    }

    private void OnResShenQiCulture(GameEvent evt)
    {
        isInCulture = true;
        if (isSingle == 1)
        {
            RefreshAttrList(false, true);
        }
        else
        {
            window.m_tenCultureResGroup.visible = true;
            window.m_openRecommendBtn.selected = ShenQiService.Singleton.IsOpenAtuoRecomond();
            RefreshCultureList();
        }

        RefreshRightBtnState();
        RefreshShenQiListBaseInfo();
    }

    private void OnResShenQiUnlock(GameEvent evt)
    {
        RefreshRightView();
        RefreshShenQiList();
        RefreshModel(false);
        RefreshShenQiListBaseInfo();
    }

    private void OnResArtifactTrainSave(GameEvent evt)
    {
        isInCulture = false;
        RefreshAttrList(false, false);
        RefreshRightBtnState();
        if (isSingle == 0)
            window.m_tenCultureResGroup.visible = false;
    }
    /// <summary>
    /// 保存培养结果
    /// </summary>
    private void SavePeiYangRes()
    {
        window.m_tenCultureResGroup.visible = false;
        ShenQiService.Singleton.ReqArtifactTrainSave();
    }

    #endregion;
    protected override void OnClose()
    {
        if (wrapper != null)
            wrapper.Dispose();

        base.OnClose();
    }
}
