using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_UltemateTrain;
using Message.Challenge;

public class KeyJumpTipWindow : BaseWindow {

    UI_KeyJumpTipWindow window;

    public override void OnOpen()
    {
        base.OnOpen();

        window = getUiWindow<UI_KeyJumpTipWindow>();

        BindEvent();
    }

    private void BindEvent()
    {
        window.m_refuseBtn.onClick.Add(OnCloseBtn);
        window.m_closeBtn.onClick.Add(OnCloseBtn);
        window.m_jumpBtn.onClick.Add(OnJumpBtnClick);
        window.m_tipShowBtn.onChanged.Add(OnTipShowBtnChanged);
    }

    public override void AddEventListener()
    {
        base.AddEventListener();

        GED.ED.addListener(EventID.OnResTrialSkip, OnResTrialSkip);
    }

    public override void RemoveEventListener()
    {
        base.RemoveEventListener();

        GED.ED.removeListener(EventID.OnResTrialSkip, OnResTrialSkip);
    }

    public override void InitView()
    {
        base.InitView();


    }
    #region 按钮事件 ******************************************************************************

    private void OnJumpBtnClick()
    {
        // 发送一键爬塔的请求
        UltemateTrainService.Singleton.ReqUltemateTrialSkip();
    }

    private void OnTipShowBtnChanged()
    {
        int isCloseTip = window.m_tipShowBtn.selected ? 1 : 0;
        PlayerPrefs.SetInt(PlayerPrefsKeys.close_Train_JumpTip, isCloseTip);
    }
    #endregion 

    private void OnResTrialSkip(GameEvent evt)
    {
        WinMgr.Singleton.Open<KeyTrainResWindow>(null, UILayer.Popup);
        OnCloseBtn();
    }

    protected override void OnCloseBtn()
    {
        base.OnCloseBtn();
    }
}
