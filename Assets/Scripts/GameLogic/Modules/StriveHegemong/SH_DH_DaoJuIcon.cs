using UI_StriveHegemong;
using Data.Beans;
public class SH_DH_DaoJuIcon : UI_SH_DH_DaoJuIcon
{
    public new static SH_DH_DaoJuIcon CreateInstance()
    {
        return (SH_DH_DaoJuIcon)UI_SH_DH_DaoJuIcon.CreateInstance();
    }
    public void Init(int itemId,int number)
    {
        t_itemBean bean = ConfigBean.GetBean<t_itemBean,int>(itemId);
        //品阶
        if (itemId > 0)
            UIGloader.SetUrl(m_beijing, UIUtils.GetItemBorder(itemId));
        else
             UIGloader.SetUrl(m_beijing, UIUtils.GetItemBorder(itemId, number));
        //头像
        UIGloader.SetUrl(m_touxiang, UIUtils.GetItemIcon(itemId));

        //类型
        m_number.text = number.ToString();
        //数量
        if (bean.t_type != 5)
            m_type.visible = false;
    }
}
