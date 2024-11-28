using UI_StriveHegemong;
using Data.Beans;

public class SH_GZ_Time : UI_SH_GZ_Time
{
    public new static SH_GZ_Time CreateInstance()
    {
        return (SH_GZ_Time)UI_SH_GZ_Time.CreateInstance();
    }
    public void Init(string time)
    {
        t_languageBean languageBean;
        if (time != null)
        {
            string[] type = GTools.splitString(time, ';');
            if (type.Length != 3)
            {
                Logger.err("SH_GZ_Time:Init:比赛时间段数据不正确");
                return;
            }
            //第一组
            {
                int[] one = GTools.splitStringToIntArray(type[0]);
                languageBean = ConfigBean.GetBean<t_languageBean,int>(one[0]);
                if (languageBean == null)
                {
                    Logger.err("SH_GZ_listItem:Init;语言包没有第一组时间对应id");
                }
                else
                {
                    m_OneTime.text = languageBean.t_content;
                }
                languageBean = ConfigBean.GetBean<t_languageBean,int>(one[1]);
                if (languageBean == null)
                {
                    Logger.err("SH_GZ_listItem:Init;语言包没有第一组内容对应id");
                }
                else
                {
                    m_OneAffair.text = languageBean.t_content;
                }
            }
            //第二组
            {
                int[] two = GTools.splitStringToIntArray(type[1]);
                languageBean = ConfigBean.GetBean<t_languageBean, int>(two[0]);
                if (languageBean == null)
                {
                    Logger.err("SH_GZ_listItem:Init;语言包没有第二组时间对应id");
                }
                else
                {
                    m_TwoTime.text = languageBean.t_content;
                }
                languageBean = ConfigBean.GetBean<t_languageBean, int>(two[1]);
                if (languageBean == null)
                {
                    Logger.err("SH_GZ_listItem:Init;语言包没有第二组内容对应id");
                }
                else
                {
                    m_TwoAffair.text = languageBean.t_content;
                }
            }
            //第二组
            {
                int[] three = GTools.splitStringToIntArray(type[2]);
                languageBean = ConfigBean.GetBean<t_languageBean, int>(three[0]);
                if (languageBean == null)
                {
                    Logger.err("SH_GZ_listItem:Init;语言包没有第三组时间对应id");
                }
                else
                {
                    m_ThreeTime.text = languageBean.t_content;
                }
                languageBean = ConfigBean.GetBean<t_languageBean, int>(three[1]);
                if (languageBean == null)
                {
                    Logger.err("SH_GZ_listItem:Init;语言包没有第三组内容对应id");
                }
                else
                {
                    m_ThreeAffair.text = languageBean.t_content;
                }
            }
        }
    }
    public override void Dispose()
    {
        base.Dispose();
    }
}
