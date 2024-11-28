using UI_HallFame;
using Data.Beans;
using Message.Pet;

public class TeamListItem : UI_TeamListItem
{
    private t_petBean petBean;
    private t_itemBean itemBean;
    private int number;
    public new static TeamListItem CreateInstance()
    {
        return (TeamListItem)UI_TeamListItem.CreateInstance();
    }

    public void Init(int petid,bool item = false,int num = 0)
    {
        
        if (item)
        {
            itemBean = ConfigBean.GetBean<t_itemBean,int>(petid);
            if (itemBean == null)
            {
                Logger.log("TeamListItem:Init:未能从道具表获取到宠物id---" + petid);
                return;
            }
            number = num;
            FillItemData();
        }
        else
        {
            petBean = ConfigBean.GetBean<t_petBean, int>(petid);
            if (petBean == null)
            {
                Logger.log("TeamListItem:Init:未能从宠物表获取到宠物id---" + petid);
                return;
            }
            FillPetData();
        }
    }
    private void FillPetData()
    {
        if (petBean.t_ifadd == 1)
        {
            
            if (string.IsNullOrEmpty(petBean.t_icon))
            { Logger.err("TeamListItem:FillData:未能获得该宠物的头像数据---" + petBean.t_id); }
            else
            {
                string icon = UIUtils.GetPetStartIcon(petBean.t_id);
                UIGloader.SetUrl(m_TouXiang,icon);
            }
            //根据宠物表数据获得其所属战队id
            if (petBean.t_team == 0)
            {
                Logger.err("宠物表战队数据为空，请检查该数据---" + petBean.t_id);
            }
            else
            {
                t_hof_teamBean teamBean = ConfigBean.GetBean<t_hof_teamBean, int>(petBean.t_team);
                if (teamBean != null)
                {
                    if (string.IsNullOrEmpty(teamBean.t_food))
                    {
                        Logger.err("战队表食物数据为空，请检查该战队id---" + teamBean.t_id);
                        return;
                    }
                    int[] foods = GTools.splitStringToIntArray(teamBean.t_food);
                    m_HongDian.visible = false;
                    PetInfo info = PetService.Singleton.GetPetInfo(petBean.t_id);
                    if (info != null)
                    {
                        for (int i = 0; i < foods.Length; ++i)
                        {
                            int number = BagService.Singleton.GetItemNum(foods[i]);
                            if (number > 0)
                                m_HongDian.visible = true;
                        }
                    }
                }
            }
            PetInfo petInfo = PetService.Singleton.GetPetInfo(petBean.t_id);
            if (petInfo == null)
            {
                m_BeiJing.grayed = true;
                m_TouXiang.grayed = true;
            }
        }
        else if(petBean.t_ifadd == 0)//没有宠物，填问号图标
        {

        }
    }
    private void FillItemData()
    {
        m_HongDian.visible = false;
        UIGloader.SetUrl(m_TouXiang, UIUtils.GetItemIcon(itemBean.t_id));
        m_number.text = number.ToString();
        UIGloader.SetUrl(m_BeiJing, UIUtils.GetItemBorder(itemBean.t_id));
    }
    public override void Dispose()
    {
        petBean = null;
        base.Dispose();
    }
    public int GetPetid()
    {
        if (petBean != null)
            return petBean.t_id;
        else
            return 0;
    }
}
