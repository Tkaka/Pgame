using UI_GuillRedEnvelope;
//抢红包面板
public class GRE_QiangHongBao
{
    private UI_GRE_QiangHongBao window;
    private GRE_DataManger dataManger;
    public GRE_QiangHongBao(GRE_DataManger manger, UI_GRE_QiangHongBao qiang)
    {
        dataManger = manger;
        window = qiang;
    }
    public void Init()
    {
        if (window != null)
        {
            if (dataManger != null)
            {
                if (dataManger.hongbaoliebiao != null)
                    OnFillData();
                else
                    Logger.err("抢红包列表为空");
            }
        }
    }
    private void OnFillData()
    {
        //红包列表
        GRE_Top_Qiang_ListItem listItem;
        window.m_TaRenHongBaoList.RemoveChildren(0,-1,true);
        for (int i = 0; i < dataManger.hongbaoliebiao.hongbaos.Count; ++i)
        {
            listItem = GRE_Top_Qiang_ListItem.CreateInstance();
            listItem.Init(dataManger.hongbaoliebiao.hongbaos[i]);
            window.m_TaRenHongBaoList.AddChild(listItem);
        }
        window.m_wodetongzhiList.RemoveChildren(0,-1,true);
        GER_WoDeHuoDe wodelistItem;
        for (int i = 0; i < dataManger.hongbaoliebiao.logs.Count; ++i)
        {
            wodelistItem = GER_WoDeHuoDe.CreateInstance();
            wodelistItem.Init(dataManger.hongbaoliebiao.logs[i]);
            window.m_wodetongzhiList.AddChild(wodelistItem);
        }
    }
    public void Close()
    {
        window.m_TaRenHongBaoList.RemoveChildren(0, -1, true);
        window = null;
        dataManger = null;
    }
}
