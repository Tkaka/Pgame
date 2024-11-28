using UI_PetParticulars;
using Data.Beans;

public class JianJieMianBan : UI_JianJieMianBan
{
    private float offsetH;
    //简介面板
    public new static JianJieMianBan CreateInstance()
    {
        return (JianJieMianBan)UI_JianJieMianBan.CreateInstance();
    }
    public void Init(int petid)
    {
        offsetH = this.height - m_jianjie.height;
        t_petBean petBean = ConfigBean.GetBean<t_petBean,int>(petid);
        if (petBean == null)
        {
            Logger.err("JianJieMianBan:Init:未能在宠物表找到对应宠物---" + petid);
            return;
        }
        if (string.IsNullOrEmpty(petBean.t_desc2))
        {
            Logger.err("JianJieMianBan:Init:宠物表简介字段没有对应id---" + petid);
            return;
        }
        m_jianjie.text = petBean.t_desc2;
        m_miaoshuditu.height = m_jianjie.height + 15;
        this.height = offsetH + m_jianjie.height;
    }
}
