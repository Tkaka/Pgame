using UI_HallFame;
using Data.Beans;

public class ExplainListItem : UI_ExplainListItem
{
    public new static ExplainListItem CreateInstance()
    {
        return (ExplainListItem)UI_ExplainListItem.CreateInstance();
    }
    public void Init(int num)
    {
        t_languageBean languageBean = ConfigBean.GetBean<t_languageBean,int>(num);
        if (languageBean == null)
        {
            Logger.err("语言包内没有对应id");
            return;
        }
        m_MiaoShu.text = languageBean.t_content;
        height = m_MiaoShu.height;
    }
}
