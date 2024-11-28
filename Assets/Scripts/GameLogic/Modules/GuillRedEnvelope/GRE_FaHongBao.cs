using FairyGUI;
using UI_GuillRedEnvelope;
using Data.Beans;

public class GRE_FaHongBao
{
    private UI_GRE_FaHongBao window;
    private GRE_DataManger datamanger;
    public GRE_FaHongBao(GRE_DataManger manger, UI_GRE_FaHongBao faHongBao)
    {
        window = faHongBao;
        datamanger = manger;
        window.m_honhbaoList.onClickItem.Add(OnFaHongBao);
        GED.ED.addListener(EventID.OnGuildHongBaoNumChange, OnChange);
    }
    public void Init()
    {
        if (datamanger != null)
        {
            if (window != null)
            {
                OnFillData();
                OnNumber();
            }
        }
    }
    private void OnFillData()
    {
        GRE_RedEnvelopeItem listItem;
        window.m_honhbaoList.RemoveChildren(0,-1,true);
        for (int i = 0; i < 3; ++i)
        {
            listItem = GRE_RedEnvelopeItem.CreateInstance();
            listItem.OnFaHongBaoInit(i + 1);
            window.m_honhbaoList.AddChild(listItem);
        }
    }
    private void OnFaHongBao(EventContext context)
    {
        GRE_RedEnvelopeItem listItem = context.data as GRE_RedEnvelopeItem;
        if (listItem != null)
        {
            int num = OnGetNumber();
            int shengyu = num - datamanger.number;
            if (shengyu > 0)
            {
                WinInfo winInfo = new WinInfo();
                TwoParam<int, GRE_DataManger> twoParam = new TwoParam<int, GRE_DataManger>();
                twoParam.value1 = listItem.hongbaoId;
                twoParam.value2 = datamanger;
                winInfo.param = twoParam;
                //打开发红包窗口
                WinMgr.Singleton.Open<GRE_FaHongBaoWindow>(winInfo, UILayer.Popup);
            }
            else
            {
                if(num > 0)
                    TipWindow.Singleton.ShowTip("发红包次数已用完,提升VIP等级可增加次数");
                else
                    TipWindow.Singleton.ShowTip("您当前不是VIP无法发红包");
            }
        }
        else
            Logger.err("类型转换失败");
    }
    private void OnChange(GameEvent evt)
    {
        OnNumber();
    }
    //剩余购买此次数
    private void OnNumber()
    {
        int num = OnGetNumber();//可发红包次数
        int shengyu = num - datamanger.number;
        string miaoshu = "{0}/{1}";
        window.m_number.text = string.Format(miaoshu, shengyu, num);
        if (shengyu <= 0)
        {
            window.m_number.color = new UnityEngine.Color(255, 0, 0);
        }
    }
    private int OnGetNumber()
    {
        int number = 0;
        t_vipBean vipBean = ConfigBean.GetBean<t_vipBean,int>(RoleService.Singleton.RoleInfo.roleInfo.vip);
        number = vipBean.t_fhb;
        return number;
    }
    public void Close()
    {
        GED.ED.removeListener(EventID.OnGuildHongBaoNumChange, OnChange);
        window = null;
        datamanger = null;
    }
}
