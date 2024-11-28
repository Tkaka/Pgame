//此窗口为临时窗口  后面可能会删除
using UI_Guild;
using Message.Guild;
using Data.Beans;


public class GuildScenceWnd : BaseWindow
{
    private UI_GuildScenceWnd m_window;
    private string m_topWnd = "";
    private long m_coroutId = 0;
    private bool isClick;

    public override void OnOpen()
    {
        base.OnOpen();
        m_window = getUiWindow<UI_GuildScenceWnd>();
        m_window.m_btnClose.onClick.Add(CloseSelf);
        m_window.m_btnGuildHall.onClick.Add(_OnHallClick);
        m_window.m_objNotice.m_btnBianji.onClick.Add(_OnBianJiClick);
        m_window.m_btnReward.onClick.Add(_OnRewardClick);
        m_window.m_btnGuildShop.onClick.Add(_OnGuildShopClick);
        m_window.m_btnWish.onClick.Add(_OnWishClick);
        m_window.m_XunLianSuoBtn.onClick.Add(OnXunLianSuo);
        m_window.m_guildBossBtn.onClick.Add(OnGuildBossBtnClick);
        m_window.m_btnDonate.onClick.Add(_OnDonateClick);
        m_window.m_hongbaoBtn.onClick.Add(OnOpenHongBaoWindow);

        m_topWnd = WinMgr.Singleton.Open<TopRoleInfo>(null, UILayer.TopHUD);
        InitView();
 
    }

    public override void InitView()
    {
        base.InitView();
        _HideNotice();
        _BtnShow();
        _ShowNoticeDes();
        _CheckOpenFirstNotice();

        _RegisterRedDot("Guild/Reward", m_window.m_btnReward.m_imgRed);
        _RegisterRedDot("Guild/btnHall", m_window.m_btnGuildHall.m_imgRed);
    }

    public override void AddEventListener()
    {
        base.AddEventListener();
        GED.ED.addListener(EventID.GuildChange, _GuildIdChange);
        GED.ED.addListener(EventID.GuildInfo, _GuildInfoChange);
        GED.ED.addListener(EventID.GuildNoticeChange, _GuildNoticeChange);
        GED.ED.addListener(EventID.OnResGuildDungeonInfo, OnResGuildDungeonInfo);
    }

    public override void RemoveEventListener()
    {
        base.RemoveEventListener();
        GED.ED.removeListener(EventID.GuildChange, _GuildIdChange);
        GED.ED.removeListener(EventID.GuildInfo, _GuildInfoChange);
        GED.ED.removeListener(EventID.GuildNoticeChange, _GuildNoticeChange);
        GED.ED.removeListener(EventID.OnResGuildDungeonInfo, OnResGuildDungeonInfo);
    }

    private void _BtnShow()
    {
        m_window.m_objNotice.m_btnBianji.visible = GuildService.Singleton.IsHaveAuthority(GuildService.EAuthority.Change_Notice);

        GuildInfo guildInfo = GuildService.Singleton.GetGuildInfo();
        m_window.m_btnReward.visible = guildInfo.roleJob == (int)GuildService.EJobType.Chair_Man;
        m_window.m_hongbaoBtn.visible = guildInfo.level >= 4;
    }
    private void OnXunLianSuo()
    {
        WinMgr.Singleton.Open<GD_MainWindow>(new WinInfo(),UILayer.Popup);
    }
    private void _ShowNoticeDes()
    {
        GuildInfo guildInfo = GuildService.Singleton.GetGuildInfo();
        m_window.m_objNotice.m_txtDes.text = guildInfo.notice;
    }

    private void _ShowNotice()
    {
        m_window.m_objNotice.visible = true;
        CoroutineManager.Singleton.stopCoroutine(m_coroutId);
        m_coroutId = CoroutineManager.Singleton.delayedCall(10, _HideNotice);
    }
    //  社团副本
    private void OnGuildBossBtnClick()
    {
        // TODO: 判断是否可以点击
        int openLv = GuildBossService.Singleton.GetGuildBossOpenLevel();
        GuildInfo guildInfo = GuildService.Singleton.GetGuildInfo();
        if (guildInfo != null)
        {
            if (guildInfo.level >= openLv)
            {
                GuildBossService.Singleton.ReqGuildDungeonInfo();
                isClick = true;
            }
            else
                TipWindow.Singleton.ShowTip("公会等级不足");
        }
    }
    private void OnOpenHongBaoWindow()
    {
        //t_globalBean globalBean = ConfigBean.GetBean<t_globalBean,int>(1602019);
        //if (globalBean != null)
        //{
        //    string miaoshu = "工会{0}级开放红包功能";
        //    if (globalBean.t_int_param > (GuildService.Singleton.GetGuildInfo()).level)
        //    {
        //        TipWindow.Singleton.ShowTip(string.Format(miaoshu, globalBean.t_int_param));
        //    }
        //    else
        //    {
                OpenChild<GRE_MainWindow>(WinInfo.Create(false, winName, true));
        //    }
        //}
        //else
        //{
        //    Logger.err("全局表没有金币红包等级开放限制条件字段" + 1602019);
        //}
    }
    private void OnResGuildDungeonInfo(GameEvent evt)
    {
        if (isClick)
        {
            isClick = false;
            WinMgr.Singleton.Open<GuildBossMianWindow>(null, UILayer.Popup);
        }
    }


    private void _HideNotice()
    {
        m_window.m_objNotice.visible = false;
        CoroutineManager.Singleton.stopCoroutine(m_coroutId);
        m_coroutId = CoroutineManager.Singleton.delayedCall(10, _ShowNotice);
    }

    //检查每天第一次进公会打开的公告
    private void _CheckOpenFirstNotice()
    {

        long roleId = RoleService.Singleton.GetRoleInfo().roleId;
        string strlastOpenTime = PlayerLocalData.GetData("time" + roleId, "0") as string;
        long lastOpenTime = 0;
        long.TryParse(strlastOpenTime, out lastOpenTime);

        if (lastOpenTime == 0 || !TimeUtils.TimeToString(TimeUtils.currentMilliseconds()).Equals(TimeUtils.TimeToString(lastOpenTime)))
        {
            WinMgr.Singleton.Open<FirstOpenNoticeWnd>(null, UILayer.Popup);
            PlayerLocalData.SetData("time" + roleId, TimeUtils.currentMilliseconds() + "");
        }

    }

    private void _GuildIdChange(GameEvent evt)
    {
        CloseSelf();
    }

    private void _GuildInfoChange(GameEvent evt)
    {
        _BtnShow();
    }

    private void _GuildNoticeChange(GameEvent evt)
    {
        _ShowNoticeDes();
    }

    public void _OnHallClick()
    {
        WinMgr.Singleton.Open<GuildHallWnd>();
    }

    public void _OnBianJiClick()
    {
        WinMgr.Singleton.Open<ModifyNoticeWnd>();
    }

    private void _OnRewardClick()
    {
        WinMgr.Singleton.Open<ChairmanRewardWnd>();
    }


    private void _OnGuildShopClick()
    {
        WinMgr.Singleton.Open<ShopMainWindow>(WinInfo.Create(false, null,true,4), UILayer.Popup);
    }

    private void _OnWishClick()
    {
        WinMgr.Singleton.Open<FragmentWishWnd>(null, UILayer.Popup);
    }

    private void _OnDonateClick()
    {
        WinMgr.Singleton.Open<DonateHallWnd>(null, UILayer.Popup);
    }

    public void CloseSelf()
    {
        base.Close();
 
        SceneLoader.Singleton.nextState = GameState.MainCity;
        SceneLoader.Singleton.sceneName = GSceneName.MaiCity;
        if (GameManager.Singleton != null)
            GameManager.Singleton.changeState(GameState.Loading);
    }

    protected override void OnClose()
    {
        base.OnClose();
        if (!m_topWnd.Equals(""))
            WinMgr.Singleton.Close(m_topWnd);

        CoroutineManager.Singleton.stopCoroutine(m_coroutId);

    }
}