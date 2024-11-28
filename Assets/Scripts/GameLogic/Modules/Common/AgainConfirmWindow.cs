using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_Common;
using DG.Tweening;

public class AgainConfirmWindow : SingletonWindow<AgainConfirmWindow> {

    UI_AgainConfirmWindow window;
    private System.Action cbyes;
    private System.Action cbno;
    private bool cancelMaskDisable;

    public override void OnOpen()
    {
        base.OnOpen();

        window = getUiWindow<UI_AgainConfirmWindow>();
        //window.m_mask.onClick.Add(OnMaskCancelClick);

        window.visible = false;
        
        window.m_cancelBtn.onClick.Add(OnCancelBtnClick);
        window.m_confirmBtn.onClick.Add(OnConfirmBtnClick);
        window.m_centerYes.onClick.Add(OnConfirmBtnClick);
        window.m_close.onClick.Add(OnCancelBtnClick);
    }
    private Tween tween;
    public void ShowTip(string tipStr, System.Action callbackYes = null, System.Action callbackNo = null, bool disableCancel = false)
    {
        setDefaultStr();

        cbyes = callbackYes;
        cbno = callbackNo;
        cancelMaskDisable = disableCancel;
        window.m_close.visible = !disableCancel;

        window.m_cancelBtn.visible = true;
        window.m_confirmBtn.visible = true;
        window.m_centerYes.visible = false;
        
        window.m_tipLabel.text = tipStr;
        if (tween != null && tween.IsActive())
            tween.Kill();

        window.visible = true;
        window.m_panelGroup.alpha = 0;
        tween = window.m_panelGroup.TweenFade(1, 0.5f);
    }

    public void ShowTip(string title, string tipStr, System.Action callbackYes = null, System.Action callbackNo = null, bool disableCancel = false)
    {
        ShowTip(tipStr, callbackYes, callbackNo, disableCancel);
        window.m_txtTitle.text = title;
    }

    public void ShowTip(string title, string strYes, string strNo, string tipStr, System.Action callbackYes = null, System.Action callbackNo = null, bool disableCancel = false)
    {
        ShowTip(title, tipStr, callbackYes, callbackNo, disableCancel);
        window.m_confirmBtn.GetChild("title").asTextField.text = strYes;
        window.m_cancelBtn.GetChild("title").asTextField.text = strYes;
    }

    public void TipOneButton(string title, string strYes, string tipStr, System.Action callbackYes = null, System.Action callbackNo = null, bool disableCancel = true)
    {
        TipOneButton(tipStr, callbackYes, callbackNo, disableCancel);
        window.m_centerYes.GetChild("title").asTextField.text = strYes;
    }

    public void TipOneButton(string title, string tipStr, System.Action callbackYes = null, System.Action callbackNo = null, bool disableCancel = true)
    {
        TipOneButton(tipStr, callbackYes, callbackNo, disableCancel);
        window.m_txtTitle.text = title;
    }

    public void TipOneButton(string tipStr, System.Action callbackYes = null, System.Action callbackNo = null, bool disableCancel = true)
    {
        setDefaultStr();

        cbno = null;
        cbyes = callbackYes;
        cancelMaskDisable = disableCancel;
        window.m_close.visible = !disableCancel;

        window.visible = true;
        window.m_cancelBtn.visible = false;
        window.m_confirmBtn.visible = false;
        window.m_centerYes.visible = true;

        window.m_tipLabel.text = tipStr;
        if (tween != null && tween.IsActive())
            tween.Kill();

        window.m_panelGroup.alpha = 1;
        tween = window.m_panelGroup.TweenFade(1, 0.5f);
    }

    private void OnConfirmBtnClick()
    {
        HideWindow();
        if (cbyes != null)
            cbyes();
    }

    private void OnMaskCancelClick()
    {
        if (cancelMaskDisable)
            return;

        HideWindow();
    }

    private void OnCancelBtnClick()
    {
        HideWindow();
        if (cbno != null)
            cbno();
    }

    private void setDefaultStr()
    {
        window.m_txtTitle.text = "提示";
        window.m_centerYes.GetChild("title").asTextField.text = "确定";
        window.m_confirmBtn.GetChild("title").asTextField.text = "确定";
        window.m_cancelBtn.GetChild("title").asTextField.text = "取消";
    }

    private void HideWindow()
    {
        if (tween != null && tween.IsActive())
            tween.Kill();

        tween = window.m_panelGroup.TweenFade(0, 0.5f).OnComplete(()=>{
            window.visible = false;
            tween = null;
        });
    }
}