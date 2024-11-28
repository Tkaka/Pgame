using Data.Beans;
using UI_GuillRedEnvelope;
using Message.Guild;
class GRE_Top_Qiang_ListItem : UI_GRE_Top_Qiang_ListItem
{
    Hongbao hongbaoInfo;
    public new static GRE_Top_Qiang_ListItem CreateInstance()
    {
        return (GRE_Top_Qiang_ListItem)UI_GRE_Top_Qiang_ListItem.CreateInstance();
    }
    public void Init(Hongbao hongbao)
    {
        hongbaoInfo = hongbao;
        if (hongbaoInfo != null)
        {
            OnFillData();
            OnAnNiuXianShi();
        }
    }
    private void OnFillData()
    {
        //t_hongbaoBean hongbaoBean = ConfigBean.GetBean<t_hongbaoBean,int>(hongbaoInfo.type);
        //UIGloader.SetUrl(m_type_icon,hongbaoBean.t_icon);
        m_name.text = hongbaoInfo.name;
        m_number.text = hongbaoInfo.roles.Count + "/" + hongbaoInfo.naxNum;
    }
    private void OnAnNiuXianShi()
    {
        if (hongbaoInfo.roles.Count >= hongbaoInfo.naxNum)
        {
            m_yiqiangwan.visible = true;
            m_qianghongbaoBtn.visible = false;
            return;
        }
        m_yiqiangwan.visible = false;
        m_qianghongbaoBtn.visible = true;
        for (int i = 0; i < hongbaoInfo.roles.Count; ++i)
        {
            if (hongbaoInfo.roles[i].roleId == RoleService.Singleton.RoleInfo.roleInfo.roleId)
            {
                m_qianghongbaoBtn.m_miaoshu.text = "已抢过";
                m_qianghongbaoBtn.grayed = true;
                return;
            }
        }
        m_qianghongbaoBtn.grayed = false;
        m_qianghongbaoBtn.m_miaoshu.text = "抢红包";
        m_qianghongbaoBtn.onClick.Add(OnQiangHongBao);
    }
    private void OnQiangHongBao()
    {
        GuildService.Singleton.OnQiangHongBao(hongbaoInfo.id);
    }
}
