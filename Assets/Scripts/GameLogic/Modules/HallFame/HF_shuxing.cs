using UI_HallFame;
using Data.Beans;

public class HF_shuxing : UI_HF_shuxing
{
    public new static HF_shuxing CreateInstance()
    {
        return (HF_shuxing)UI_HF_shuxing.CreateInstance();
    }

    public void Init(int type,int number)
    {
        t_attr_nameBean nameBean = ConfigBean.GetBean<t_attr_nameBean,int>(type);
        if (nameBean == null)
        {
            Logger.err("HF_shuxing:Init:属性错误为空！");
            return;
        }
        m_type.text = nameBean.t_name_id;
        m_number.text = "+";
        if (type > 3)
        {
            m_number.text += ((float)number / 10000 * 100).ToString() + "%";
        }
        else
            m_number.text += number.ToString();
    }
}
