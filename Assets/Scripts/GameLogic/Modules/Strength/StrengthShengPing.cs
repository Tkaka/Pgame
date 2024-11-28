using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_Strength;
using FairyGUI;
using Data.Beans;
using Message.Pet;

public class StrengthShengPing : TabPage {

    private UI_shengPing shengPingUI;
    private StrengthWindow parentWindow;

    private long _coroutineID;
    private bool isClickSP = false;
    private bool isAddEvent = false;

    public StrengthShengPing(UI_shengPing shengPing, StrengthWindow parentWindow)
    {
        this.shengPingUI = shengPing;
        this.parentWindow = parentWindow;
        _coroutineID = -1;
        BindEvent();
    }

    public StrengthDataManager StrengthData
    {
        get { return parentWindow.strengthData; }
    }

    private void AddListener()
    {
        if (isAddEvent == false)
        {
            GED.ED.addListener(EventID.ResBagUpdate, OnBagUpdate);
            isAddEvent = true;
        }
    }

    private void RemoveListener()
    {
        if (isAddEvent)
        {
            GED.ED.removeListener(EventID.ResBagUpdate, OnBagUpdate);
            isAddEvent = false;
        }
    }

    private void ShowAddShuXinTip()
    {
        //显示属性 1秒后消失
        int id = StrengthData.CurSelectPetInfo.petId;
        int color = StrengthData.CurSelectPetInfo.basInfo.color;

        int colorUpID = UIUtils.GetPetColorUpAttrSum(id, color);
        t_pet_colorup_attr_sumBean colorUpBean = ConfigBean.GetBean<t_pet_colorup_attr_sumBean, int>(colorUpID);

        shengPingUI.m_addAtkTipLabel.text = string.Format("攻击:{0}", colorUpBean.t_atk);
        shengPingUI.m_addDefTipLabel.text = string.Format("防御:{0}", colorUpBean.t_def);
        shengPingUI.m_addHpTipLabel.text = string.Format("生命:{0}", colorUpBean.t_hp);
        shengPingUI.m_showShuXingTrans.Play();
        _coroutineID = CoroutineManager.Singleton.startCoroutine(ShowTipView());
        parentWindow.TouchUnEnable();
    }

    IEnumerator ShowTipView()
    {
       
        yield return new WaitForSeconds(1.5f);
        shengPingUI.m_showShuXingTrans.Stop();
        HideAddTipView();
        // 打开升品成功提示
        WinInfo winInfo = WinInfo.Create();
        winInfo.param = StrengthData;
        WinMgr.Singleton.Open<ShengPingSuccessWindow>(winInfo, UILayer.Popup);
        CoroutineManager.Singleton.stopCoroutine(_coroutineID);
        _coroutineID = -1;
        parentWindow.TouchEnable();
    }

    public override void RefreshView(bool isShow = false)
    {
        parentWindow.strengthData.SetCaiLiaoData();

        PetInfo petInfo = parentWindow.strengthData.CurSelectPetInfo;
        t_pet_colorup_costBean qualityBean = parentWindow.strengthData.GetPetColorUpCostBean();
        t_petBean petBean = ConfigBean.GetBean<t_petBean, int>(petInfo.petId);

        var isEnoughLv = StrengthData.IsEnoughLv();
        var isEnoughGold = StrengthData.IsEnoughGold();
        if(qualityBean != null)
            shengPingUI.m_goldLabel.text = qualityBean.t_gold.ToString();
        shengPingUI.m_goldLabel.color = isEnoughGold ? Color.white : Color.red;
        shengPingUI.m_needLvLabel.visible = !isEnoughLv;
        shengPingUI.m_needLvLabel.text = string.Format("需求等级{0}", parentWindow.strengthData.GetShengPinLvRequire());
        shengPingUI.m_goldLabel.visible = parentWindow.strengthData.IsEnoughLv();
        shengPingUI.m_gold.visible = isEnoughLv;

        shengPingUI.m_materialJinDuTiao.max = StrengthData.GetNeedCaiLiaoNum(); 
        shengPingUI.m_materialJinDuTiao.value = StrengthData.GetCurMaterialNum() ;
        shengPingUI.m_progressTip.text = string.Format("{0}/{1}", shengPingUI.m_materialJinDuTiao.value, shengPingUI.m_materialJinDuTiao.max);

        RefreshCaiLiaoList();
        RefreshBtnRedPoint();
        if (isShow && isClickSP)
        {
            // TODO :显示成功特效和
            ShowAddShuXinTip();
            isClickSP = false;
        }

    }
    /// <summary>
    /// 刷新按鈕上的紅點
    /// </summary>
    private void RefreshBtnRedPoint()
    {
        shengPingUI.m_shengPBtn.m_redPoint.visible = PetService.Singleton.IsPetCanColorUp(StrengthData.CurSelectPetInfo.petId);
    }
    private void RefreshCaiLiaoList()
    {
        parentWindow.strengthData.SetCaiLiaoData();
        var num = shengPingUI.m_caiLiaoList.numChildren;
        for (int i = 0; i < num; i++)
        {
            var caiLiaoItem = shengPingUI.m_caiLiaoList.GetChildAt(i) as ShengPingCaiLiaoItem;
            caiLiaoItem.RefreshView(this, i);
        }
    }
    
    private void BindEvent()
    {
        shengPingUI.m_shengPBtn.onClick.Add(OnClickShengPBtn);
        AddListener();
    }

    private void OnClickShengPBtn()
    {
        // 道具是否满足
        if (IsEnoughCaiLiao)
        {
            // 等级是否满足
            if (parentWindow.strengthData.IsEnoughLv())
            {
                // 金币是否满足
                if (parentWindow.strengthData.IsEnoughGold())
                {
                    StrengthData.ReqShengPing();
                    isClickSP = true;
                }
                else
                {
                    TipWindow.Singleton.ShowTip(61300001);
                }
            }
            else
            {
                TipWindow.Singleton.ShowTip(61002001);
            }
            
        }
        else
        {
            TipWindow.Singleton.ShowTip(61801030);
        }

    }

    public override void OnHide()
    {
        if (_coroutineID != -1)
            CoroutineManager.Singleton.stopCoroutine(_coroutineID);
        RemoveListener();
    }

    public override void OnShow()
    {
        RefreshView();
        AddListener();
        _coroutineID = -1;
        HideAddTipView();
    }

    private void HideAddTipView()
    {
        shengPingUI.m_addAtkTipLabel.alpha = 0;
        shengPingUI.m_addDefTipLabel.alpha = 0;
        shengPingUI.m_addHpTipLabel.alpha = 0;
    }

    public override void OnClose()
    {
        RemoveListener();
        shengPingUI = null;
        parentWindow = null;

        if (_coroutineID != -1)
        {
            CoroutineManager.Singleton.stopCoroutine(_coroutineID);
        }
    }

    /// <summary>
    /// 材料是否满足
    /// </summary>
    private bool IsEnoughCaiLiao
    {
        get
        {
            var num = shengPingUI.m_caiLiaoList.numChildren;
            for (int i = 0; i < num; i++)
            {
                var caiLiaoItem = shengPingUI.m_caiLiaoList.GetChildAt(i) as ShengPingCaiLiaoItem;
                if (caiLiaoItem.IsFull == false)
                    return false;
            }
            return true;
        }
    }

    private void OnBagUpdate(GameEvent evt)
    {
        // 装备改变
        RefreshCaiLiaoList();
    }
}
