using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_GuildBattle;


public class GuildBattleMianWindow : BaseWindow {

    UI_GuildBattleMianWindow window;

    public override void OnOpen()
    {
        base.OnOpen();

        window = getUiWindow<UI_GuildBattleMianWindow>();

        BindEvent();
        InitView();
    }

    private void BindEvent()
    {
        (window.m_commonTop as UI_Common.UI_commonTop).m_closeBtn.onClick.Add(OnCloseBtn);
        window.m_baoMingBtn.onClick.Add(OnBaoMingBtnClick);
        window.m_jueSaibaoMingBtn.onClick.Add(OnBaoMingBtnClick);
        window.m_zhenRongBtn.onClick.Add(OnZhenRongBtnClick);
        window.m_exchangeBtn.onClick.Add(OnExchangeBtnClick);
    }

    public override void InitView()
    {
        base.InitView();
    }


    #region 按钮事件 *********************************************************************************************

    private void OnBaoMingBtnClick()
    {
        // 报名
    }

    private void OnZhenRongBtnClick()
    {
        // 进入阵容界面
        WinMgr.Singleton.Open<GuildBattleBuZhengWindow>(null, UILayer.Popup);
    }

    private void OnExchangeBtnClick()
    {
        // 进入兑换界面

    }

    #endregion;
}
