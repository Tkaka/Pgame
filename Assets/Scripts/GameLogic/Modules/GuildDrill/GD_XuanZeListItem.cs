using UI_GuildDrill;
using Message.Pet;

public class GD_XuanZeListItem : UI_GD_XuanZeListItem
{
    private PetInfo pet;
    public new static GD_XuanZeListItem CreateInstance()
    {
        return (GD_XuanZeListItem)UI_GD_XuanZeListItem.CreateInstance();
    }

    public void Init(PetInfo info)
    {
        pet = info;
        m_xuanzeBtn.onClick.Add(OnXuanZe);
        FillData();
        OnPetTouXiang();
    }
    private void FillData()
    {
        m_manji.visible = false;
        m_name.text = UIUtils.GetPingJiePetName(pet.petId,pet.basInfo.color,pet.basInfo.star);
        m_level.text = pet.basInfo.level + "";
        m_jingyanjindu.value = pet.basInfo.expRemain;
        m_jingyanjindu.max = PetService.Singleton.GetCurLevelExp(pet.petId,pet.basInfo.level);
        m_jingyanjindu.m_number.text = (pet.basInfo.expRemain)+ "/" + (PetService.Singleton.GetCurLevelExp(pet.petId, pet.basInfo.level));
        if (m_jingyanjindu.value == m_jingyanjindu.max)
        {
            m_jingyanjindu.m_man.visible = true;
            m_jingyanjindu.m_man.width = m_jingyanjindu.width;
            if (pet.basInfo.level == RoleService.Singleton.RoleInfo.roleInfo.level)
            {
                m_manji.visible = true;
                m_xuanzeBtn.visible = false;
            }
        }
    }
    private void OnPetTouXiang()
    {
        UIGloader.SetUrl(m_pet.m_touxiang,UIUtils.GetPetStartIcon(pet.petId));
        UIGloader.SetUrl(m_pet.m_pinjie, UIUtils.GetLoaderUrl(WinEnum.UI_Common, UIUtils.GetBorderByQuality(pet.basInfo.color)));
        StarList starList = new StarList((UI_Common.UI_StarList)m_pet.m_xingji);
        starList.SetStar(pet.basInfo.star);
    }
    private void OnXuanZe()
    {
        GED.ED.dispatchEvent(EventID.OnGuildDrillChangePet,pet.petId);
    }
}
