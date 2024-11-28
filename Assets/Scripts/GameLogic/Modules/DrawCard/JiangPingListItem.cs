using Data.Beans;
using UI_DrawCard;

public class JiangPingListItem : UI_JiangPingListItem
{
    private int num;
    t_itemBean itemBean;
    public new static JiangPingListItem CreateInstance()
    {
        return (JiangPingListItem)UI_JiangPingListItem.CreateInstance();
    }

    public void Init(int itemid,int num = 0)
    {
        if (itemid < -100)
        {
            m_all.visible = false;
        }
        else
        {
            m_StartList.visible = false;
            itemBean = ConfigBean.GetBean<t_itemBean, int>(itemid);
            if (itemBean == null)
            {
                Logger.err("JiangPingListItem:Init:道具表没有对应数据 ---" + itemid);
                return;
            }
            if (itemBean.t_type != 23)
            {
                if (num != 0)
                    m_number.text = num.ToString();
                else
                    m_number.visible = false;
                if (itemBean.t_type == 5)
                { m_SuiPian.visible = true; }
                else
                { m_SuiPian.visible = false; }
                //m_Name.text = "语言包里没数据";

                FillIcon();
            }
            else
            {
                m_SuiPian.visible = false;
                m_number.visible = false;
                m_StartList.visible = true;
                OnSetStar();
            }
            m_Name.text = itemBean.t_name;
        }
    }
    private void FillIcon()
    {
        if (!(string.IsNullOrEmpty(itemBean.t_quality)))
        {
            UIGloader.SetUrl(m_BeiJing, UIUtils.GetItemBorder(itemBean.t_id));
            UIGloader.SetUrl(m_TouXiang, UIUtils.GetItemIcon(itemBean.t_id));
        }
    }
    private void OnSetStar()
    {
        if (string.IsNullOrEmpty(itemBean.t_value))
        { }
        else
        {
            string[] value = GTools.splitString(itemBean.t_value,';');
            int petid = int.Parse(value[0]);
            t_petBean petBean = ConfigBean.GetBean<t_petBean,int>(petid);
            if (petBean == null)
            {
                Logger.err("未能获得对应宠物" + petid);
                return;
            }
            StarList starList = new StarList((UI_Common.UI_StarList)m_StartList);
            starList.SetStar(itemBean.t_star);
            UIGloader.SetUrl(m_BeiJing,UIUtils.GetItemBorder(itemBean.t_id));
            UIGloader.SetUrl(m_TouXiang,UIUtils.GetPetStartIcon(petBean.t_id));
        }
    }
}
