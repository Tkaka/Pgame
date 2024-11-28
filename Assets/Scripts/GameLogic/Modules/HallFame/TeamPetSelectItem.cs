using UI_HallFame;
using Data.Beans;
using Message.Pet;

public class TeamPetSelectItem : UI_TeamPetSelectItem
{
    private int petid;
    public new static TeamPetSelectItem CreateInstance()
    {
        return (TeamPetSelectItem)UI_TeamPetSelectItem.CreateInstance();
    }
    public void Init(int petId)
    {
        petid = petId;
        FillData();
    }
    private void FillData()
    {
        t_petBean petBean = ConfigBean.GetBean<t_petBean,int>(petid);
        if (petBean == null)
        {
            Logger.err("TeamPetSelectItem:FillData:未能从宠物表找到对应数据，请检查宠物表---" + petid);
            return;
        }
        if (petBean.t_ifadd == 0)
        {
            m_touXiang.grayed = true;
            return;
        }
        PetInfo petInfo = PetService.Singleton.GetPetInfo(petid);
        UIGloader.SetUrl(m_touXiang, UIUtils.GetPetStartIcon(petid));
        if (petInfo == null)
        { m_touXiang.grayed = true;}
    }
    public int GetPetid()
    {
        t_petBean petBean = ConfigBean.GetBean<t_petBean,int>(petid);
        if (petBean.t_ifadd == 0)
        { return 0; }
        else
        { return petBean.t_id; }
    }
}
