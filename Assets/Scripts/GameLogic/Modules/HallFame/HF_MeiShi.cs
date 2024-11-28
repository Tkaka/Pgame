using UI_HallFame;
using Data.Beans;

public class HF_MeiShi : UI_HF_MeiShi
{
    private int number;
    private t_itemBean itemBean;
    public new static HF_MeiShi CreateInstance()
    {
        return (HF_MeiShi)UI_HF_MeiShi.CreateInstance();
    }
    public void Init(int item,bool petAcquire)
    {
        itemBean = ConfigBean.GetBean<t_itemBean,int>(item);
        if (itemBean == null)
        {
            Logger.err("HF_MeiShi:Init:道具表没有对应id的美食，请检查战队表中的美食数据！---" + item);
            return;
        }
        FillData();
        m_MeiShiIcon.grayed = !petAcquire;
        m_Name.grayed = !petAcquire;
        m_number.grayed = !petAcquire;
    }
    private void FillData()
    {
        m_Name.text = itemBean.t_name;
        m_Name.color = UIUtils.GetItemColor(itemBean.t_id);
        number = BagService.Singleton.GetItemNum(itemBean.t_id);

        m_number.text = number.ToString();
        if (number == 0)
            m_MeiShiIcon.grayed = true;
        UIGloader.SetUrl(m_MeiShiIcon, UIUtils.GetItemIcon(itemBean.t_id));
    }
    public string GetCateIcon()
    {
        return itemBean.t_icon;
    }
    public override void Dispose()
    {
        itemBean = null;
        base.Dispose();
    }
    public int GetItemId()
    { return itemBean.t_id; }
    public int GetNumber()
    {return number;}
}