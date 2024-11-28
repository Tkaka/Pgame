using UI_StriveHegemong;
using Message.Pet;
public class SH_BM_ListItem : UI_SH_BM_ListItem
{
    public int petId;
    public new static SH_BM_ListItem CreateInstance()
    {
        return (SH_BM_ListItem)UI_SH_BM_ListItem.CreateInstance();
    }
    public void Init(PetInfo info,bool xuanzhong = false)
    {
        m_xuanzhong.visible = xuanzhong;
        StarList star = new StarList((UI_Common.UI_StarList)m_xingji);
        star.SetStar(info.basInfo.star);
        UIGloader.SetUrl(m_pinjie, UIUtils.GetBorderByQuality(info.basInfo.color));
        UIGloader.SetUrl(m_touxiang, UIUtils.GetBorderByQuality(info.basInfo.color));
        petId = info.petId;
        m_dengji.text = info.basInfo.level.ToString();
    }
    public override void Dispose()
    {
        base.Dispose();
    }
}
