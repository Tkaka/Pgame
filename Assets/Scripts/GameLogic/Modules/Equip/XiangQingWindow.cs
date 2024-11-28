using UI_Equip;
using Data.Beans;

public class XiangQingWindow : BaseWindow
{
    UI_XiangQingWindow window;
    //道具id
    private int daojuId;
    private int suipianId;
    private int suipianShuLiang;
    private t_itemBean iten;
    public override void OnOpen()
    {
        base.OnOpen();
        window = getUiWindow<UI_XiangQingWindow>();
        AddEvntLisent();
        InitView();
    }
    public void AddEvntLisent()
    {
        GED.ED.addListener(EventID.ResBagUpdate, OnWanCheng);
    }
    public void RemoveEventLisent()
    {
        GED.ED.removeListener(EventID.ResBagUpdate, OnWanCheng);
    }
    public override void InitView()
    {
        //根据道具id读取道具表内数据
        base.InitView();
        window.m_CloseBtn.onClick.Add(CloseBtn);
        window.m_BeiJing.onClick.Add(CloseBtn);
       // FillData();
        if (Info.param == null)
        {
            Logger.err("未传入道具id");
            return;
        }
        else
           daojuId = (int)Info.param;
        //根据道具id到道具表中获取其碎片id和合成所需数量
        iten = ConfigBean.GetBean<t_itemBean, int>(daojuId);
        if (iten == null)
        {
            Logger.err("道具表中没有此道具");
            return;
        }
        //得到碎片id
        if (string.IsNullOrEmpty(iten.t_compose_arg))
        {
            Logger.err("该道具道具表没有t_value字段无法显示数据" + iten.t_id);
            return;
        }
        int[] suipianid = GTools.splitStringToIntArray(iten.t_compose_arg);
        suipianId = suipianid[0];
        suipianShuLiang = suipianid[1];

        FillData();
        window.m_LaiYuanBtn.onClick.Add(OnLaiYuanWindow);
        window.m_HeCheng.onClick.Add(OnHeCheng);
    }
    private void OnWanCheng(GameEvent evt)
    { FillData(); }
    private void FillData()
    {
        //数量
        int value = BagService.Singleton.GetItemNum(suipianId);
        window.m_Name.text = iten.t_name;
        window.m_Name.color = UIUtils.GetItemColor(suipianId);
        t_itemBean itemBean = ConfigBean.GetBean<t_itemBean,int>(suipianId);
        UIGloader.SetUrl(window.m_TouXiang, itemBean.t_icon);
        UIGloader.SetUrl(window.m_pinjie, UIUtils.GetItemBorder(suipianId));
        if (itemBean == null)
        {
            Logger.err("道具表中没有这个道具信息");
            return;
        }
        //将道具id转换为格子id传入背包中获取道具数量
        window.m_ShuLiang.text = value.ToString() + "/" + suipianShuLiang.ToString();
        window.m_SuiPianJinDu.value = value;
        window.m_SuiPianJinDu.max = suipianShuLiang;
        window.m_MiaoShu.text = itemBean.t_describe;
        if (value >= suipianShuLiang)
        {
            window.m_LaiYuanBtn.visible = false;
            window.m_HeCheng.visible = true;
        }
        else
        {
            window.m_LaiYuanBtn.visible = true;
            window.m_HeCheng.visible = false;
        }
    }

    private void OnLaiYuanWindow()
    {
        //跳转到来源窗口
        TwoParam<int, int> two = new TwoParam<int, int>();
        two.value1 = suipianId;
        two.value2 = suipianShuLiang;
        WinInfo info = new WinInfo();
        info.param = two;
        WinMgr.Singleton.Open<LaiYuanWindow>(info,UILayer.Popup);
    }
    private void OnHeCheng()
    {
        //将道具id发送合成请求给服务器
        //根据道具id获取格子id
        Message.Bag.GridInfo grid = BagService.Singleton.GetGridInfo(suipianId);
        BagService.Singleton.ReqItemUse(grid.id,suipianShuLiang);
        GED.ED.dispatchEvent(EventID.OnWuQiHunSuiPianHeCheng);
        FillData();
    }
    private void CloseBtn()
    {
        RemoveEventLisent();
        window = null;
        iten = null;
        Close();
    }
}
