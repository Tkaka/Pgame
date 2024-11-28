using UI_GuillRedEnvelope;
using UnityEngine;

public class GRE_MainWindow : BaseWindow
{
    private UI_GRE_MainWindow window;
    private GRE_DataManger manger;
    private GRE_SheTuanHongBao sheTuanHongBao;
    private GRE_FaHongBao faHongBao;
    private GRE_QiangHongBao qiangHongBao;

    public override void OnOpen()
    {
        base.OnOpen();
        window = getUiWindow<UI_GRE_MainWindow>();
        manger = new GRE_DataManger();
        AddKeyEvent();
        window.m_type.selectedIndex = 0;
        faHongBao = new GRE_FaHongBao(manger, window.m_fahongbao);
        sheTuanHongBao = new GRE_SheTuanHongBao(manger, window.m_shetuanhongbao);
        qiangHongBao = new GRE_QiangHongBao(manger,window.m_QiangHongBao);
        OnTypeChange();
        //PlayerPrefs.DeleteAll();
    }
    public void AddKeyEvent()
    {
        ((UI_Common.UI_commonTop)window.m_taitou).m_closeBtn.onClick.Add(OnCloseBtn);
        ((UI_Common.UI_commonTop)window.m_taitou).m_title.text = "社团红包";
        window.m_type.onChanged.Add(OnTypeChange);
        window.m_guizeBtn.onClick.Add(OnGuiZe);
        window.m_paihangBtn.onClick.Add(OnOpenPaiHangBang);
    }
    public override void AddEventListener()
    {
        base.AddEventListener();
        GED.ED.addListener(EventID.OnGuildRedDataManger, OnSheTuanHongBao);
        GED.ED.addListener(EventID.OnGuildSleepQiangHongBao, OnQiangHongBao);
    }
    public override void RemoveEventListener()
    {
        base.RemoveEventListener();
        GED.ED.removeListener(EventID.OnGuildRedDataManger, OnSheTuanHongBao);
        GED.ED.removeListener(EventID.OnGuildSleepQiangHongBao, OnQiangHongBao);
    }
    public override void RefreshView()
    {
        base.RefreshView();
    }
    private void OnTypeChange()
    {
        switch (window.m_type.selectedIndex)
        {
            case 0:
                {
                    GuildService.Singleton.OnReqOpenHongbaoPage();
                } break;
            case 1:
                {
                    faHongBao.Init();
                } break;
            case 2:
                {
                    GuildService.Singleton.ReqQiangHongBaoList();
                } break;
        }
        RefreshView();
    }
    private void OnSheTuanHongBao(GameEvent ev)
    {
        sheTuanHongBao.Init();
    }
    private void OnQiangHongBao(GameEvent ev)
    {
        qiangHongBao.Init();
    }
    private void OnGuiZe()
    {
        WinMgr.Singleton.Open<GRE_GuiZeWindow>(new WinInfo(),UILayer.Popup);
    }
    private void OnOpenPaiHangBang()
    {
        //发红包排行榜
        GuildService.Singleton.OnReqHongbaoRank();
    }
    protected override void OnCloseBtn()
    {
        RemoveEventListener();
        if (manger != null)
            manger.Close();
        manger = null;
        if (sheTuanHongBao != null)
            sheTuanHongBao.Colse();
        sheTuanHongBao = null;
        if (faHongBao != null)
            faHongBao.Close();
        faHongBao = null;
        if (qiangHongBao != null)
            qiangHongBao.Close();
        qiangHongBao = null;
        window = null;
        base.OnCloseBtn();
    }
}