using UI_GeDouJia;
using Data.Beans;
using Message.Pet;

public class SuMingPetItem : UI_SuMingPetItem
{
    public new static SuMingPetItem CreateInstance()
    {
        return (SuMingPetItem)UI_SuMingPetItem.CreateInstance();
    }

    public void Init(int petid, bool JiaHao)
    {
        t_petBean petBean = ConfigBean.GetBean<t_petBean,int>(petid);
        PetInfo petinfo = PetService.Singleton.GetPetInfo(petid);
        if (petBean == null || petinfo == null)
        {
            Logger.err("SuMingPetItem:Init:宠物表没有此宠物的id");
            return;
        }
        if (string.IsNullOrEmpty(petBean.t_icon))
        {
            Logger.err("SuMingPetItem:Init:未获得宠物表中的宠物头像列表-----" + petid );
            return;
        }
        int star = 0;
        
        string[] icons = GTools.splitString(petBean.t_icon);
        int[] stars = GTools.splitStringToIntArray(petBean.t_star_xingtai);
        if (icons.Length != stars.Length)
        {
            Logger.err("头像属性与星级属相对应不正确---" + petBean.t_id);
            return;
        }
        for (int i = 0; i < icons.Length - 1; ++i)
        {
            if (icons.Length == 1)
            {
                star = 0;
                break;
            }
            else
            {
                if (petinfo.basInfo.star >= stars[i] && petinfo.basInfo.star <= stars[i + 1])
                    break;
            }
            star++;
        }
        UIGloader.SetUrl(m_TouXiang,icons[star]);
        m_JiaHao.visible = JiaHao;
    }
    public override void Dispose()
    {
        base.Dispose();
    }
}
