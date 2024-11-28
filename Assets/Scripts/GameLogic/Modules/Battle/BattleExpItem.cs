using Data.Beans;
using Message.Pet;
using UI_Battle;
using UI_Common;

public class BattleExpItem : UI_BattleExpItem
{

    public int petId;
    public int addExp;

    public new static BattleExpItem CreateInstance()
    {
        return (BattleExpItem)UI_BattleExpItem.CreateInstance();
    }

    public void RefreshView()
    {
        PetInfo info = PetService.Singleton.GetPetInfo(petId);
        if (info != null)
        {
            t_petBean bean = ConfigBean.GetBean<t_petBean, int>(petId);
            UIGloader.SetUrl(m_petIcon.m_borderLoder, UIUtils.GetBorderUrl(info.basInfo.color));
            UIGloader.SetUrl(m_petIcon.m_headLoader, UIUtils.GetIconPath(bean));
            m_petIcon.m_lvTxt.text = info.basInfo.level + "";
            StarList starList = new StarList((UI_StarList)m_petIcon.m_starList);
            starList.SetStar(info.basInfo.star);
        }

        int status = BattleService.Singleton.GetPetStatus(petId);
        //只加了经验
        if (status == 0)
        {
            m_addExpG.visible = true;
            m_levelupG.visible = false;
            m_expFull.visible = false;
            m_txtExp.text = addExp + "";
        }
        //升级了
        else if (status == 1)
        {
            m_addExpG.visible = false;
            m_levelupG.visible = true;
            m_expFull.visible = false;
        }
        //经验加满了
        else
        {
            m_addExpG.visible = false;
            m_levelupG.visible = false;
            m_expFull.visible = true;
        }

    }

}


