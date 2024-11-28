using UI_Talent;
using FairyGUI;
using FairyGUI.Utils;
using Data.Beans;

public class TalentCell : UI_talentCell1
{

    //道具id
    public int talentId;


    public new static TalentCell CreateInstance()
    {
        return UI_talentCell1.CreateInstance() as TalentCell;
    }

    public void RefreshView()
    {
        t_talentBean talentBean = ConfigBean.GetBean<t_talentBean, int>(talentId);
        if (talentBean == null)
            return;

        int talentLevel = TalentService.Singleton.GetTalentLevel(talentId);
        UIGloader.SetUrl(m_imgIcon, talentBean.t_icon);
        //m_imgIcon.url = PathEnum.Icons + talentBean.t_icon;
        m_imgLock.visible = talentLevel < 0;
        m_txtLevel.visible = talentLevel >= 0;
        m_txtLevel.text = string.Format("{0}/{1}", talentLevel, talentBean.t_level_max);
        this.grayed = talentLevel < 0;
        
    }

    public override void Dispose()
    {
        base.Dispose();
    }
}
