using UI_GuillRedEnvelope;
using Message.Guild;

public class GRE_TheHighestItem : UI_GRE_TheHighestItem
{
    public Hongbao hongbao;
    public GRE_DataManger dataManger;
    public int hongbaoId;
    public new static GRE_TheHighestItem CreateInstance()
    {
        return (GRE_TheHighestItem)UI_GRE_TheHighestItem.CreateInstance();
    }
    public void Init(long index, GRE_DataManger manger)
    {
        dataManger = manger;
        if (dataManger != null)
        {
            hongbao = dataManger.GetHongbaoInfo(index);
            if (hongbao != null)
            {
                OnFillData();
            }
        }
    }
    private void OnFillData()
    {
        hongbaoId = (int)hongbao.id;
        if (hongbao.id == 1)
        {
            string miaoshu = "{0}金币";
            m_allmoney.text = string.Format(miaoshu,hongbao.sumMoney);
        }
        else if (hongbao.id == 2)
        {
            string miaoshu = "{0}钻石";
            m_allmoney.text = string.Format(miaoshu,hongbao.sumMoney);
        }
        else if (hongbao.id == 3)
        {
            string miaoshu = "{0}神器之源";
            m_allmoney.text = string.Format(miaoshu,hongbao.sumMoney);
        }
        m_number.text = hongbao.naxNum - hongbao.roles.Count + "/" + hongbao.naxNum;
        m_zuigao.text = OnZuiGaoName();
    }
    private string OnZuiGaoName()
    {
        string name = "暂无";
        if (hongbao.roles.Count > 0)
        {
            name = hongbao.roles[0].name;
            for (int i = 0; i < hongbao.roles.Count - 1; ++i)
            {
                if (hongbao.roles[i].num > hongbao.roles[i + 1].num)
                    name = hongbao.roles[i].name;
            }
        }
        return name;
    }
    public override void Dispose()
    {
        hongbao = null;
        dataManger = null;
        base.Dispose();
    }
}
