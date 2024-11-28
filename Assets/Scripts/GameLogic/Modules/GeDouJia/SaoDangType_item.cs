using UI_GeDouJia;
using Data.Beans;

public class SaoDangType_item : UI_SaoDangType_item
{
    public new static SaoDangType_item CreateInstance()
    {
        return (SaoDangType_item)UI_SaoDangType_item.CreateInstance();
    }
    public void Init(int cishu)
    {
        t_languageBean languageBean = ConfigBean.GetBean<t_languageBean,int>(71901008);
        if (languageBean != null)
        {
            title = string.Format(languageBean.t_content, cishu.ToString());
        }
    }
}
