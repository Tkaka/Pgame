using UI_GuillRedEnvelope;
using FairyGUI;
using Data.Beans;

class GRE_FaHongBao_type : UI_GRE_FaHongBao_type
{
    private t_hongbaoBean hongbaoBean;
    private int number = 0;//已发红包次数
    private GRE_DataManger dataManger;
    public new static GRE_FaHongBao_type CreateInstance()
    {
        return (GRE_FaHongBao_type)UI_GRE_FaHongBao_type.CreateInstance();
    }
    public void Init(t_hongbaoBean hongbao,GRE_DataManger manger)
    {
        hongbaoBean = hongbao;
        dataManger = manger;
        if (dataManger != null)
        {
            if (hongbaoBean != null)
            {
                OnFillData();
                m_FaHongBao.onClick.Add(OnFaHongBao);
            }
        }
    }
    private void OnFillData()
    {
        OnHuoDe();
        OnName();
    }
    /// <summary>
    /// 发红包获得奖励加载
    /// </summary>
    private void OnHuoDe()
    {
        if (!(string.IsNullOrEmpty(hongbaoBean.t_reward)))
        {
            string[] jiangli = GTools.splitString(hongbaoBean.t_reward,';');
            int[] daoju1 = GTools.splitStringToIntArray(jiangli[0]);
            if (daoju1.Length >= 2)
            {
                UIGloader.SetUrl(m_huode1_icon, UIUtils.GetBuyGoodsPriceIcon(daoju1[0], daoju1[1]));
                m_huode1_number.text = daoju1[1].ToString();
            }
            else
                UIGloader.SetUrl(m_huode1_icon, UIUtils.GetItemIcon(daoju1[0]));
            if (jiangli.Length > 1)
            {
                int[] daoju2 = GTools.splitStringToIntArray(jiangli[1]);
                if (daoju2.Length >= 2)
                {
                    UIGloader.SetUrl(m_huode1_icon, UIUtils.GetItemIcon(daoju2[0], daoju2[1]));
                    m_huode1_number.text = daoju2[1].ToString();
                }
                else
                    UIGloader.SetUrl(m_huode1_icon, UIUtils.GetItemIcon(daoju2[0]));
            }
        }
    }
    //名字和奖励物品图片加载
    private void OnName()
    {
        string name = hongbaoBean.t_name;
        m_name.text = string.Format(name,hongbaoBean.t_num);
        if (!(string.IsNullOrEmpty(hongbaoBean.t_icon)))
        { UIGloader.SetUrl(m_type_Icon,hongbaoBean.t_icon); }
        //总额加载
        m_zonge_Number.text = hongbaoBean.t_itemNum.ToString();
        UIGloader.SetUrl(m_zonge_Icon,UIUtils.GetItemIcon(hongbaoBean.t_itemId,hongbaoBean.t_itemNum));
        //花费
        UIGloader.SetUrl(m_jiage_icon,UIUtils.GetItemIcon(-2,hongbaoBean.t_cost));
        m_jiage.text = hongbaoBean.t_cost.ToString();
        //vip等级限制
        m_vip_level.text = hongbaoBean.t_vip.ToString();
    }
    private void OnFaHongBao()
    {
        int num = OnGetNumber();
        int shengyu = num - dataManger.number;
        if (shengyu > 0)
        {
            if(RoleService.Singleton.RoleInfo.roleInfo.vip >= hongbaoBean.t_vip)
                GuildService.Singleton.OnFaHongBao(hongbaoBean.t_id);
            else
                TipWindow.Singleton.ShowTip("VIP等级不足无法发该红包");
        }
        else
        {
            TipWindow.Singleton.ShowTip("发红包次数已用完");
        }
    }
    private int OnGetNumber()
    {
        int number = 0;
        t_vipBean vipBean = ConfigBean.GetBean<t_vipBean, int>(RoleService.Singleton.RoleInfo.roleInfo.vip);
        number = vipBean.t_fhb;
        return number;
    }
    public override void Dispose()
    {
        dataManger = null;
        hongbaoBean = null;
        base.Dispose();
    }
}
