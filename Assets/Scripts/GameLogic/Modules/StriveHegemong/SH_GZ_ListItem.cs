using UI_StriveHegemong;
using Data.Beans;

public class SH_GZ_ListItem : UI_SH_GZ_ListItem
{
    public new static SH_GZ_ListItem CreateInstance()
    {
        return (SH_GZ_ListItem)UI_SH_GZ_ListItem.CreateInstance();
    }
    public void Init(string guize)
    {
        if (guize != null)
        {
            int guizeid = int.Parse(guize);
            t_languageBean languageBean = ConfigBean.GetBean<t_languageBean,int>(guizeid);
            if (languageBean == null)
            {
                Logger.err("SH_GZ_listItem:Init;语言包没有对应id");
                return;
            }
            m_describe.text = languageBean.t_content;
            height = m_describe.height; 
        }
        
    }
    public override void Dispose()
    {
        base.Dispose();
    }
}
