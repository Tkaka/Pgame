using FairyGUI;
using UI_StriveHegemong;

public class SH_MainWindow : BaseWindow
{
    private UI_SH_MainWindow window;
    private SH_ZS_NO zhusai_no;
    private SH_ZS_OFF zhusai_off;
    private SH_HG_baqiangJianKuang eightfinal = null;//八强回顾
    private SH_WoDeSaiCheng wodesaicheng = null;
    private SH_HG_MyCondition wodehuigu = null;
    private int KaiSai;
    private int mainrace = 0;//主赛场显示那张面板控制，0为未开赛，1为已开赛，2为八强赛开始
    private int curIndex;//当前控制器的页面
    private ScrollPane scrollPane;
    public override void OnOpen()
    {
        base.OnOpen();
        window = getUiWindow<UI_SH_MainWindow>();
        
        
        mainrace = StriveHegemongService.Singleton.open;
        if (mainrace == 1 || mainrace == 2)
        {
            window.m_SH_Type.selectedIndex = 1;
            StriveHegemongService.Singleton.OnReqCloseMainInfo();
            StriveHegemongService.Singleton.OnReqOpenSelfInfo();
            curIndex = 1;
        }
        else
        {
            StriveHegemongService.Singleton.OnReqOpenMainInfo();
            window.m_SH_Type.selectedIndex = 0;
            curIndex = 0;
        }
        scrollPane = window.m_HuiGuList.scrollPane;
        AddKeyEvent();

    }
    public override void AddEventListener()
    {
        base.AddEventListener();
        GED.ED.addListener(EventID.OnStriveHegemongOpen, OnActivityOpen);
        GED.ED.addListener(EventID.OnStriveHegemongEightGameOpen,OnEightGameOpen);
        GED.ED.addListener(EventID.OnStriveHegemongJoin,OnBaoMingChengGong);
        GED.ED.addListener(EventID.OnTargetFightInfo, OnTargetFightInfo);
        GED.ED.addListener(EventID.OnStriveHegemongOpenMainWindow,OnOpenMainWindow);
        GED.ED.addListener(EventID.OnNextCompetitionOpen, OnOpenMainWindowSleep);//主赛场界面刷新
    }
    public override void RemoveEventListener()
    {
        base.RemoveEventListener();
        GED.ED.removeListener(EventID.OnStriveHegemongOpen, OnActivityOpen);
        GED.ED.removeListener(EventID.OnStriveHegemongEightGameOpen, OnEightGameOpen);
        GED.ED.removeListener(EventID.OnStriveHegemongJoin,OnBaoMingChengGong);
        GED.ED.removeListener(EventID.OnTargetFightInfo, OnTargetFightInfo);
        GED.ED.removeListener(EventID.OnStriveHegemongOpenMainWindow, OnOpenMainWindow);
        GED.ED.removeListener(EventID.OnNextCompetitionOpen, OnOpenMainWindowSleep);
        
    }
    
    private void OnOpenMainWindow(GameEvent evt)
    {
        KaiSai = StriveHegemongService.Singleton.open;
        mainrace = StriveHegemongService.Singleton.open;
        InitView();
        window.m_HuiGuList.ScrollToView(1);
        window.m_HG_baqiangsaicheng.visible = false;
    }
    private void AddKeyEvent()
    {
        window.m_CloseBtn.onClick.Add(OnCloseBtn);
        window.m_DuiHuanBtn.onClick.Add(OnOpenDuiHuanWindow);
        window.m_SH_Type.onChanged.Add(OnTypeChange);
        //window.m_PaiHangBtn.onClick.Add(OnOpenPaiHangWinDow);
        window.m_chuangkouceshi.onClick.Add(TestWindow);
        window.m_HG_wodesaiceng.onClick.Add(OnWoDeHuiGu);
        window.m_HG_baqiangsaicheng.onClick.Add(OnBaQiangHuiGu);
        scrollPane.onScrollEnd.Add(OnListChange);
        window.m_GuiZeBtn.onClick.Add(OnOpenGuiZeWindow);
    }
    private void TestWindow()
    {
        WinInfo info = new WinInfo();
        //WinMgr.Singleton.Open<SH_WoDeSaiCheng_BuZhenWindow>(info, UILayer.Popup);
        WinMgr.Singleton.Open<SH_DF_BuZhenWindow>(info,UILayer.Popup);
    }
    public override void InitView()
    {
        base.InitView();
        
        RefreshView();
    }
    public override void RefreshView()
    {
        base.RefreshView();
        mainrace = StriveHegemongService.Singleton.open;

        OnMainShow();
    }
    /// <summary>
    /// 主显示类型切换，当回发消息
    /// 使用这个主要是为了处理打开和关闭
    /// 我的赛程和主赛场界面时给服务器发消息
    /// 避免刷新面板时也在向服务器发送消息
    /// </summary>
    private void OnTypeChange()
    {
        if (mainrace == 2)
        {
            if (window.m_SH_Type.selectedIndex == 2)
            {
                window.m_SH_Type.selectedIndex = curIndex;
                return;
            }
        }
        if (window.m_SH_Type.selectedIndex == 1)
        {
            if (StriveHegemongService.Singleton.join == false)
            {
                TipWindow.Singleton.ShowTip("未报名不能查看（非）");
                window.m_SH_Type.selectedIndex = curIndex;
            }
        }
        if (window.m_SH_Type.selectedIndex == 2)
        {
            if (StriveHegemongService.Singleton.open != 0)
            {
                TipWindow.Singleton.ShowTip("已开战不能查看昨日回顾");
                window.m_SH_Type.selectedIndex = curIndex;
            }
        }
        if (window.m_SH_Type.selectedIndex != curIndex)
        {
            switch (window.m_SH_Type.selectedIndex)
            {
                case 0:
                    {
                        StriveHegemongService.Singleton.OnReqCloseSelfInfo();
                        StriveHegemongService.Singleton.OnReqOpenMainInfo();
                        OnMainShow();
                    }
                    break;
                case 1:
                    {
                        OnMyRace();
                        StriveHegemongService.Singleton.OnReqCloseMainInfo();
                        StriveHegemongService.Singleton.OnReqOpenSelfInfo();
                    }
                    break;
                case 2:
                    {
                        OnZuoRiHuiGu();
                        StriveHegemongService.Singleton.OnReqYesterday();
                    }
                    break;
            }
        }
        
        curIndex = window.m_SH_Type.selectedIndex;
    }
    /// <summary>
    /// 窗口显示加载，主管三个面板，根据情况加载不同的面板并为其传参
    /// 主要用于刷新面板
    /// </summary>
    private void OnMainShow()
    {
        switch (window.m_SH_Type.selectedIndex)
        {
            case 0:
                {
                    OnMainRace();
                } break;
            case 1:
                {
                    OnMyRace();
                } break;
            case 2:
                {
                    OnZuoRiHuiGu();
                } break;
        }
    }
    /// <summary>
    /// 主赛场显示面板控制
    /// </summary>
    private void OnMainRace()
    {
        //判断正处于什么状态，0、未开赛 1、已开赛 2、已开赛进8强
        switch (mainrace)
        {
            case 0:
                {
                    if (zhusai_no == null)
                    {zhusai_no = new SH_ZS_NO(window.m_SH_ZS_weikaizhan); }
                    zhusai_no.Init();
                    window.m_SH_ZS_weikaizhan.visible = true;
                    window.m_SH_ZS_yikaizhan.visible = false;
                    window.m_EigheFinal.visible = false;
                    if (eightfinal != null)
                    {
                        eightfinal.Close();
                        eightfinal = null;
                    }
                } break;
            case 1:
                {
                    if (zhusai_off == null)
                    { zhusai_off = new SH_ZS_OFF(window.m_SH_ZS_yikaizhan); }
                    zhusai_off.Init();
                    window.m_SH_ZS_weikaizhan.visible = false;
                    if (zhusai_no != null)
                    {
                        zhusai_no.Close();
                        zhusai_no = null;
                    }
                    window.m_SH_ZS_yikaizhan.visible = true;
                    window.m_EigheFinal.visible = false;
                } break;
            case 2:
                {
                    if (eightfinal == null)
                    { eightfinal = new SH_HG_baqiangJianKuang(window.m_EigheFinal); }
                    eightfinal.Init();
                    window.m_EigheFinal.visible = true;
                    window.m_SH_ZS_weikaizhan.visible = false;
                    window.m_SH_ZS_yikaizhan.visible = false;
                    if (zhusai_off != null)
                    {
                        zhusai_off.Close();
                        zhusai_off = null;
                    }
                } break;
        }
    }
    /// <summary>
    /// 我的赛程
    /// </summary>
    private void OnMyRace()
    {
        if (wodesaicheng == null)
        {
            wodesaicheng = new SH_WoDeSaiCheng(window.m_wodesaicheng);
        }
        wodesaicheng.Init();

    }
    //收到此消息后，并以开展并将刷新主界面
    private void OnOpenMainWindowSleep(GameEvent evt)
    {
        if (window.m_SH_Type.selectedIndex == 0)
        {
            window.m_SH_ZS_weikaizhan.visible = false;
            window.m_SH_ZS_yikaizhan.visible = true;
            window.m_EigheFinal.visible = false;
        }
    }
    private void OnActivityOpen(GameEvent evt)
    {
        mainrace = 1;
        if (zhusai_off == null)
        { zhusai_off = new SH_ZS_OFF(window.m_SH_ZS_yikaizhan); }
        zhusai_off.Init();

        RefreshView();
    }
    private void OnEightGameOpen(GameEvent evt)
    {
        mainrace = 2;
        RefreshView();
    }
    //报名成功
    private void OnBaoMingChengGong(GameEvent evt)
    {
    }
    private void OnOpenDuiHuanWindow()
    {
        WinInfo info = new WinInfo();
        WinMgr.Singleton.Open<SH_ExchangeWindow>(info,UILayer.Popup);
    }
    private void OnTargetFightInfo(GameEvent evt)
    {
        if ((window.m_SH_Type.selectedIndex == 2 && window.m_HuiGuList.selectedIndex == 0) || window.m_SH_Type.selectedIndex == 0)
        {
            WinInfo info = new WinInfo();
            WinMgr.Singleton.Open<SH_BQ_CanSaiZhenRongWindow>(info, UILayer.Popup);
        }
        
    }
    //昨日回顾
    private void OnZuoRiHuiGu()
    {
        window.m_HuiGuList.RemoveChildren(0, -1, true);
        //加载两张面板填写两张面板
        eightfinal = SH_HG_baqiangJianKuang.CreateInstance();
        eightfinal.HuiGuiInit();
        window.m_HuiGuList.AddChild(eightfinal);
        wodehuigu = SH_HG_MyCondition.CreateInstance();
        wodehuigu.Init();
        window.m_HuiGuList.AddChildAt(wodehuigu,0);
    }
    private void OnOpenGuiZeWindow()
    {
        WinMgr.Singleton.Open<SH_RuleWindow>(new WinInfo(),UILayer.Popup);
    }
    private void OnWoDeHuiGu()
    {
        window.m_HuiGuList.ScrollToView(0,true);
        window.m_HG_wodesaiceng.visible = false;
        window.m_HG_baqiangsaicheng.visible = true;
    }
    private void OnBaQiangHuiGu()
    {
        window.m_HuiGuList.ScrollToView(1,true);
        window.m_HG_baqiangsaicheng.visible = false;
        window.m_HG_wodesaiceng.visible = true;
    }
    private void OnListChange()
    {
        int xiabiao =(int)( window.m_HuiGuList.scrollPane.posX / window.m_HuiGuList.width);
        if (xiabiao == 0)
        {
            window.m_HG_wodesaiceng.visible = false;
            window.m_HG_baqiangsaicheng.visible = true;
        }
        else if(xiabiao == 1)
        {
            window.m_HG_baqiangsaicheng.visible = false;
            window.m_HG_wodesaiceng.visible = true;
        }
    }

    protected override void OnCloseBtn()
    {
       
        if (zhusai_no != null)
        {
            zhusai_no.Close();
            zhusai_no = null;
        }
        if (zhusai_off != null)
        {
            zhusai_off.Close();
            zhusai_off = null;
        }
        if (eightfinal != null)
        {
            eightfinal.Close();
            eightfinal = null;
        }
        if (wodesaicheng != null)
        {
            wodesaicheng.Close();
            wodesaicheng = null;
        }
        window = null;
        base.OnCloseBtn();
    }
}
