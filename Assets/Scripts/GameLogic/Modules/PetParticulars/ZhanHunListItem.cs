using Data.Beans;
using UI_PetParticulars;
public class ZhanHunListItem : UI_ZhanHunListItem
{
    t_pet_soulBean soulBean;
    public int zhanhunId;
    public int level;
    public new static ZhanHunListItem CreateInstance()
    {
        return (ZhanHunListItem)UI_ZhanHunListItem.CreateInstance();
    }
    public void Init(int zhanhunid,int dengji)
    {
        soulBean = ConfigBean.GetBean<t_pet_soulBean,int>(zhanhunid);
        if (soulBean == null)
        {
            Logger.err("未能在战魂表找到对应数据" + zhanhunid);
            return;
        }
        zhanhunId = zhanhunid;
        level = dengji;
        FillData();
    }
    private void FillData()
    {
        if (level < 0)
        {
            m_touxiang.grayed = true;
        }
        UIGloader.SetUrl(m_touxiang,soulBean.t_icon);
    }
}
