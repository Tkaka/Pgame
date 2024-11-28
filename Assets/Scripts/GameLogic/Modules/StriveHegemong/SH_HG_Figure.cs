using UI_StriveHegemong;
using Data.Beans;
using Message.Pet;

public class SH_HG_Figure : UI_SH_HG_Figure
{
    public int petId;
    public new static SH_HG_Figure CreateInstance()
    {
        return (SH_HG_Figure)UI_SH_HG_Figure.CreateInstance();
    }
    public void Init(EquipedPetInfo petInfo,bool baqiang = false)
    {
        if (petInfo != null)
        {
            petId = petInfo.id;
            m_dengji.text = petInfo.level.ToString();
            UIGloader.SetUrl(m_pinjie,UIUtils.GetBorderByQuality(petInfo.color));
            UIGloader.SetUrl(m_touxiang,UIUtils.GetPetStartIcon(petInfo.id, petInfo.star));
            StarList star = new StarList((UI_Common.UI_StarList)m_xingji);
            star.SetStar(petInfo.star);
        }
        else
        {
            petId = 0;
            m_dengji.visible = false;
            m_pinjie.visible = false;
            m_touxiang.visible = false;
            m_xingji.visible = false;
        }
        if(baqiang)
            m_zhankuang.visible = false;
    }
}
