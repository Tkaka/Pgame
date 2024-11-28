using UI_Achievement;
using Data.Beans;
public class AM_TitleLietItem : UI_AM_TitleLietItem
{
    public new static AM_TitleLietItem CreateInstance()
    {
        return (AM_TitleLietItem)UI_AM_TitleLietItem.CreateInstance();
    }
    public void Init(t_titleBean titleBean,bool title)
    {
        m_Name.text = titleBean.t_name;
        m_number.text = titleBean.t_condition.ToString();
        m_vaule.visible = title;
    }
}
