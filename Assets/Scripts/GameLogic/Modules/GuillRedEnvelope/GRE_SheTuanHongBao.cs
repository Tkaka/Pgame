using FairyGUI;
using UI_GuillRedEnvelope;

public class GRE_SheTuanHongBao
{
    private UI_GRE_SheTuanHongBao window;
    private GRE_DataManger manger;
    public GRE_SheTuanHongBao(GRE_DataManger datamanger, UI_GRE_SheTuanHongBao hongbao)
    {
        manger = datamanger;
        window = hongbao;
        window.m_shetuanhongbaoList.onClickItem.Add(OnSheTuanHongBao);
        window.m_topOneList.onClickItem.Add(OnQiangHongBao);
    }
    public void Init()
    {
        if (manger != null)
        {
            FillData();
        }
    }
    private void FillData()
    {
        GRE_RedEnvelopeItem envelopeItem;
        GRE_TheHighestItem highestItem;
        window.m_shetuanhongbaoList.RemoveChildren(0, -1, true);
        window.m_topOneList.RemoveChildren(0, -1, true);
        for (int i = 0; i < 3; ++i)
        {
            envelopeItem = GRE_RedEnvelopeItem.CreateInstance();
            envelopeItem.Init(i + 1, manger);
            window.m_shetuanhongbaoList.AddChild(envelopeItem);

            highestItem = GRE_TheHighestItem.CreateInstance();
            highestItem.Init(i + 1, manger);
            window.m_topOneList.AddChild(highestItem);
        }
    }
    public void RefTopOneView()
    {
        if (manger != null)
        {
            FillData();
        }
    }
    private void OnSheTuanHongBao(EventContext context)
    {
        GRE_RedEnvelopeItem listItem = context.data as GRE_RedEnvelopeItem;
        if (listItem == null)
        {
            Logger.err("类型对应不正确");
            return;
        }
        int type = listItem.hongbaoId;
        if (type == 1)
        {
            if (manger.jinbi)
            {
                //已领过，打开排行榜
                WinInfo info = new WinInfo();
                TwoParam<int, GRE_DataManger> twoParam = new TwoParam<int, GRE_DataManger>();
                twoParam.value1 = type;
                twoParam.value2 = manger;
                info.param = twoParam;
                WinMgr.Singleton.Open<GER_PaiHangWindow>(info,UILayer.Popup);
            }
            else
            {
                //未领过。请求领奖
                GuildService.Singleton.OnQiangHongBao(1);
            }
        }
        else if (type == 2)
        {
            if (manger.zuanshi)
            {
                //已领过，打开排行榜
                WinInfo info = new WinInfo();
                TwoParam<int, GRE_DataManger> twoParam = new TwoParam<int, GRE_DataManger>();
                twoParam.value1 = type;
                twoParam.value2 = manger;
                info.param = twoParam;
                WinMgr.Singleton.Open<GER_PaiHangWindow>(info, UILayer.Popup);
            }
            else
            {
                //未领过。请求领奖
                GuildService.Singleton.OnQiangHongBao(2);
            }
        }
        else if (type == 3)
        {
            if (manger.shenqi)
            {
                //已领过，打开排行榜
                WinInfo info = new WinInfo();
                TwoParam<int, GRE_DataManger> twoParam = new TwoParam<int, GRE_DataManger>();
                twoParam.value1 = type;
                twoParam.value2 = manger;
                info.param = twoParam;
                WinMgr.Singleton.Open<GER_PaiHangWindow>(info, UILayer.Popup);
            }
            else
            {
                //未领过。请求领奖
                GuildService.Singleton.OnQiangHongBao(3);
            }
        }
    }
    private void OnQiangHongBao(EventContext context)
    {
        GRE_TheHighestItem listItem = context.data as GRE_TheHighestItem;
        if (listItem == null)
        {
            Logger.err("类型对应不正确");
            return;
        }
        int type = listItem.hongbaoId;
        if (type == 1)
        {
            if (manger.jinbi)
            {
                //已领过，打开排行榜
                WinInfo info = new WinInfo();
                TwoParam<int, GRE_DataManger> twoParam = new TwoParam<int, GRE_DataManger>();
                twoParam.value1 = type;
                twoParam.value2 = manger;
                info.param = twoParam;
                WinMgr.Singleton.Open<GER_PaiHangWindow>(info, UILayer.Popup);
            }
            else
            {
                //未领过。请求领奖
                GuildService.Singleton.OnQiangHongBao(1);
            }
        }
        else if (type == 2)
        {
            if (manger.zuanshi)
            {
                //已领过，打开排行榜
                WinInfo info = new WinInfo();
                TwoParam<int, GRE_DataManger> twoParam = new TwoParam<int, GRE_DataManger>();
                twoParam.value1 = type;
                twoParam.value2 = manger;
                info.param = twoParam;
                WinMgr.Singleton.Open<GER_PaiHangWindow>(info, UILayer.Popup);
            }
            else
            {
                //未领过。请求领奖
                GuildService.Singleton.OnQiangHongBao(2);
            }
        }
        else if (type == 3)
        {
            if (manger.shenqi)
            {
                //已领过，打开排行榜
                WinInfo info = new WinInfo();
                TwoParam<int, GRE_DataManger> twoParam = new TwoParam<int, GRE_DataManger>();
                twoParam.value1 = type;
                twoParam.value2 = manger;
                info.param = twoParam;
                WinMgr.Singleton.Open<GER_PaiHangWindow>(info, UILayer.Popup);
            }
            else
            {
                //未领过。请求领奖
                GuildService.Singleton.OnQiangHongBao(3);
            }
        }
    }
    public void Colse()
    {
        if (window != null)
            window.m_shetuanhongbaoList.RemoveChildren(0, -1, true);
        if (window != null)
            window.m_topOneList.RemoveChildren(0, -1, true);
        window = null;
        manger = null;
    }
}
