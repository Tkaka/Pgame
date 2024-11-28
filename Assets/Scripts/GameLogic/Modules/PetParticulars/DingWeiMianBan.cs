using UI_PetParticulars;
using Data.Beans;

public class DingWeiMianBan : UI_DingWeiMianBan
{
    private float offsetH;
    public new static DingWeiMianBan CreateInstance()
    {
        return (DingWeiMianBan)UI_DingWeiMianBan.CreateInstance();
    }
    public void Init(int petid)
    {
        offsetH = this.height - m_MiaoShu.height;
        t_petBean petBean = ConfigBean.GetBean<t_petBean, int>(petid);
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
        m_MiaoShu.text = petBean.t_desc1;
        m_miaoshuditu.height = m_MiaoShu.height + 15;
        this.height = offsetH + m_MiaoShu.height;
    }
}
