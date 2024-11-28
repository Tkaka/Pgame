using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_XunLianJia;
using FairyGUI;

public class XunLianJiaWindow : BaseWindow {

    UI_XunLianJiaWindow window;
    GoWrapper wrapper;

    public override void OnOpen()
    {
        base.OnOpen();

        window = getUiWindow<UI_XunLianJiaWindow>();
        window.m_backBtn.onClick.Add(OnCloseBtn);

        InitView();
    }

    public override void InitView()
    {
        base.InitView();

        InitBtnState();
        BindEvent();
        InitModel();
    }

    private void InitModel()
    {
        GameObject model = this.LoadGo("Character/Character_Man");
        if (model == null)
            return;

        model.transform.localPosition = new Vector3(0, 0, 1000);
        model.transform.localScale = new Vector3(300, 300, 300);
        model.transform.localEulerAngles = new Vector3(0, 180, 0);

        wrapper = new GoWrapper(model);
        window.m_modelPos.SetNativeObject(wrapper);
        model.setLayer("UIActor");
    }

    private void InitBtnState()
    {
        FuncService.Singleton.SetFuncLock(window.m_tongXiangGuanBtn, 806);
        FuncService.Singleton.SetFuncLock(window.m_btnTianFu, 802);
        FuncService.Singleton.SetFuncLock(window.m_mingRenTanBtn, 803);
        FuncService.Singleton.SetFuncLock(window.m_shenQiBtn, 804);
        FuncService.Singleton.SetFuncLock(window.m_ChengJiuBtn, 805);
    }

    private void BindEvent()
    {
        window.m_tongXiangGuanBtn.onClick.Add(OnTongXiangGuanBtnClick);
        window.m_mingRenTanBtn.onClick.Add(OnHallFameWindow);
        window.m_ChengJiuBtn.onClick.Add(OnOpenAchievementWindow);
        window.m_shenQiBtn.onClick.Add(OnShenQiBtnClick);
        window.m_btnTianFu.onClick.Add(OnTianFuClick);
    }

    #region   按钮事件
    private void OnHallFameWindow()
    {
        if (FuncService.Singleton.TipFuncNotOpen(803))
        {
            WinMgr.Singleton.Open<HallFameListWindow>(WinInfo.Create(true, winName, true), UILayer.Popup);
        }
    }
    private void OnTongXiangGuanBtnClick()
    {
        if (FuncService.Singleton.TipFuncNotOpen(806))
        {
            TongXiangGuanServices.Singleton.ReqExhibitionInfo();
        }
    }
    private void OnShenQiBtnClick()
    {
        if (FuncService.Singleton.TipFuncNotOpen(804))
        {
            ShenQiService.Singleton.ReqArtifactInfo();
        }
    }
    private void OnOpenAchievementWindow()
    {
        if (FuncService.Singleton.TipFuncNotOpen(805))
        {
            WinMgr.Singleton.Open<AM_MainWindow>(WinInfo.Create(true, winName, true), UILayer.Popup);
        }
    }

    private void OnTianFuClick()
    {
        if (FuncService.Singleton.TipFuncNotOpen(802))
        {
            WinMgr.Singleton.Open<TalentMainWnd>(null, UILayer.Popup);
        }
    }
    #endregion


    protected override void OnCloseBtn()
    {
        if (wrapper != null)
            wrapper.Dispose();

        base.OnCloseBtn();
    }
}
