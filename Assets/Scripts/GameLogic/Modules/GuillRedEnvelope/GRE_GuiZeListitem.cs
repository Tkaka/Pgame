using UI_GuillRedEnvelope;
using Data.Beans;

public class GRE_GuiZeListitem : UI_GRE_GuiZeListitem
{
    public new static GRE_GuiZeListitem CreateInstance()
    { return (GRE_GuiZeListitem)UI_GRE_GuiZeListitem.CreateInstance(); }
    public void Init(int id)
    {
        t_languageBean languageBean = ConfigBean.GetBean<t_languageBean,int>(id);
        if (languageBean != null)
        {
            int h = (int)(m_beijing.height - m_miaoshu.height);
            m_miaoshu.text = (id - 71602100 + 1) + "、" + languageBean.t_content;
            m_beijing.height = h + m_miaoshu.height;
        }
    }
}
