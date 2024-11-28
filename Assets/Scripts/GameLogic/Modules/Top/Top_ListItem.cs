using UI_Top;
using Message.Rank;
using Data.Beans;

public class Top_ListItem : UI_Top_ListItem
{
    private RankData roleData;
    public new static Top_ListItem CreateInstance()
    {
        return (Top_ListItem)UI_Top_ListItem.CreateInstance();
    }
    public void Init(int index)
    {
        roleData = TopService.Singleton.OnGetRankData(index);
        FillData();
    }
    private void FillData()
    {
        if (roleData != null)
        {
            if (roleData.rank < 3)
            {
                m_paiming_Icon.visible = true;
                UIGloader.SetUrl(m_paiming_Icon, OnGetIconName(roleData.rank));
                m_paiming_number.visible = false;
            }
            else
            {
                m_paiming_Icon.visible = false;
                m_paiming_number.visible = true;
                m_paiming_number.text = (roleData.rank + 1) + "";
            }
            m_name.text = roleData.name;
            t_titleBean titleBean = ConfigBean.GetBean<t_titleBean, int>(roleData.title);
            if (titleBean != null)
            {
                m_chenghao.text = titleBean.t_name;
            }
            else
            {
                m_chenghao.text = roleData.title + "";
            }
            if (TopService.Singleton.topType == TopType.Role_Fight)
                OnPet(roleData.left);
            else
            {
                m_left.visible = true;
                m_Pet.visible = false;
                m_left.text = roleData.left.ToString();
            }
            m_right.text = roleData.right.ToString();
        }
        else
        {
            Logger.err("未能获得相对排名数据，无法显示");
            return;
        }
    }
    private void OnPet(int petId)
    {
        m_left.visible = false;
      
        t_petBean petBean = ConfigBean.GetBean<t_petBean,int>(petId);
        if (petBean != null)
        {
            m_Pet.visible = true;
            UI_Common.UI_petItem item = ((UI_Common.UI_petItem)m_Rankpet);
            item.m_petName.visible = false;
            if (roleData.hasStar())
            {
                string icon = UIUtils.GetPetStartIcon(petId, roleData.star);
                UIGloader.SetUrl(item.m_iconLoader,icon);
                StarList star = new StarList(item.m_starList);
                star.SetStar(roleData.star);

            }
            if (roleData.hasColor())
            {
                UIGloader.SetUrl(item.m_borderBg, UIUtils.GetLoaderUrl(WinEnum.UI_Common, UIUtils.GetBorderByQuality(roleData.color)));
                PetQualityDou qualityDou = item.m_petQualityDou as PetQualityDou;
                if (qualityDou != null)
                    qualityDou.InitView(roleData.color);
            }
            if (roleData.hasLevel())
            {
                item.m_levelLabel.text = roleData.level.ToString();
            }
            item.m_shangZhenGroup.visible = false;
            item.m_redPoint.visible = false;
            item.m_selectIcon.visible = false;
        }
        else
        {
            Logger.err("未能获得宠物！");
        }
    }
    public void OnOpenXiangQing()
    {
        TopService.Singleton.OnReqPetData(roleData.left, roleData.roleId);
    }
    public void OnJueSeXiangQing()
    {
        TopService.Singleton.OnJueSeXiangQing(roleData.roleId);
    }
    public override void Dispose()
    {
        base.Dispose();
    }
    private string OnGetIconName(int rank)
    {
        string name = "";
        switch (rank + 1)
        {
            case 1:name = "ui://" + WinEnum.UI_Top + "/diyiming"; break;
            case 2:name = "ui://" + WinEnum.UI_Top + "/dierming";break;
            case 3:name = "ui://" + WinEnum.UI_Top + "/disanming"; break;
            default:break;
        }
        return name;
    }
}
