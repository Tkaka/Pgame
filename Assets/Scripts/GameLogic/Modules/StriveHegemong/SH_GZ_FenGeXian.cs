using Data.Beans;
using UI_StriveHegemong;

public class SH_GZ_FenGeXian : UI_SH_GZ_FenGeXian
{
    public new static SH_GZ_FenGeXian CreateInstance()
    {
        return (SH_GZ_FenGeXian)UI_SH_GZ_FenGeXian.CreateInstance();
    }
    public void Init(string name)
    {
        if (name != null)
        {
            int guizeid = int.Parse(name);
            t_languageBean languageBean = ConfigBean.GetBean<t_languageBean, int>(guizeid);
            if (languageBean == null)
            {
                Logger.err("SH_GZ_listItem:Init;语言包没有对应id");
                return;
            }
            m_name.text = languageBean.t_content;
        }
    }
    public override void Dispose()
    {
        base.Dispose();
    }
}
