using UI_GuillRedEnvelope;
using Message.Guild;
using Data.Beans;

public class GRE_RedEnvelopeItem : UI_GRE_RedEnvelopeItem
{
    //1602019 1602020 1602021红包解锁条件全局表id
    private Hongbao hongbao;
    private GRE_DataManger dataManger;
    public int hongbaoId = 0;
    public new static GRE_RedEnvelopeItem CreateInstance()
    {
        return (GRE_RedEnvelopeItem)UI_GRE_RedEnvelopeItem.CreateInstance();
    }
    /// <summary>
    /// 红包数据加载
    /// </summary>
    /// <param name="index">下标，1代表金币，2代表钻石，3代表神器之源</param>
    public void Init(int index,GRE_DataManger manger)
    {
        hongbaoId = index;
        dataManger = manger;
        if (dataManger != null)
        {
            hongbao = dataManger.GetHongbaoInfo(index);
            if(hongbao != null)
                 OnFillData();
        }
    }
    private void OnFillData()
    {
        t_globalBean globalBean = null;
        if (hongbao.id == 1)
        {
            globalBean = ConfigBean.GetBean<t_globalBean, int>(1602019);
            m_name.text = "金币红包";
            m_yiqiangguo.visible = dataManger.jinbi;
        }
        else if (hongbao.id == 2)
        {
            globalBean = ConfigBean.GetBean<t_globalBean, int>(1602020);
            m_name.text = "钻石红包";
            m_yiqiangguo.visible = dataManger.zuanshi;
        }
        else if (hongbao.id == 3)
        {
            globalBean = ConfigBean.GetBean<t_globalBean,int>(1602021);
            m_name.text = "神器之源红包";
            m_yiqiangguo.visible = dataManger.shenqi;
        }
        if (globalBean != null)
        {
            string miaoshu = "社团{0}级解锁红包";
            if (globalBean.t_int_param <= (GuildService.Singleton.GetGuildInfo()).level)
            { m_JieSuo.visible = false; }
            else
            {
                m_JieSuo.visible = true;
                m_JieSuoYuYan.text = string.Format(miaoshu,globalBean.t_int_param);
            }
        }
    }

    public void OnFaHongBaoInit(int index)
    {
        hongbaoId = index;
        m_yiqiangguo.visible = false;
        t_globalBean globalBean = null;
        if (index == 1)
        {
            globalBean = ConfigBean.GetBean<t_globalBean, int>(1602019);
            m_name.text = "金币红包";
        }
        else if (index == 2)
        {
            globalBean = ConfigBean.GetBean<t_globalBean, int>(1602020);
            m_name.text = "钻石红包";
        }
        else if (index == 3)
        {
            globalBean = ConfigBean.GetBean<t_globalBean, int>(1602021);
            m_name.text = "神器之源红包";
        }
        if (globalBean != null)
        {
            string miaoshu = "社团{0}级解锁红包";
            if (globalBean.t_int_param <= (GuildService.Singleton.GetGuildInfo()).level)
            { m_JieSuo.visible = false; }
            else
            {
                m_JieSuo.visible = true;
                m_JieSuoYuYan.text = string.Format(miaoshu, globalBean.t_int_param);
            }
        }
    }
    public override void Dispose()
    {
        hongbao = null;
        dataManger = null;
        base.Dispose();
    }
}
