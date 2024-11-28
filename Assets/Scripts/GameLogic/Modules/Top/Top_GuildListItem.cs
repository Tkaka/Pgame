using UI_Top;
using Message.Rank;

public class Top_GuildListItem : UI_Top_GuildListItem
{
    private GuildRankData rankData;
    public int index;
    public new static Top_GuildListItem CreateInstance()
    {
        return (Top_GuildListItem)UI_Top_GuildListItem.CreateInstance();
    }
    public void Init(int xiabiao)
    {
        index = xiabiao;
        rankData = TopService.Singleton.GetGuildData(index);
        FillData();
    }
    private void FillData()
    {
        m_ziji.visible = false;
        m_weijiaru.visible = false;

        if (rankData != null)
        {
            if (rankData.rank > 4)
            {
                m_paiming_number.visible = false;
                m_paiming_Icon.visible = true;
                UIGloader.SetUrl(m_paiming_Icon, OnGetIconName(rankData.rank));
            }
            else
            {
                m_paiming_Icon.visible = false;
                m_paiming_number.visible = true;
                m_paiming_number.text = rankData.rank.ToString();
            }
            m_guildName.text = rankData.name;
            //UIGloader.SetUrl(m_huizhang,)社团徽章图片加载
            OnGuildType(rankData.guildType);//类型
            m_guildLevel.text = rankData.level.ToString();
            m_shezhang.text = rankData.chairManName.ToString();
            m_zongzhanli.text = rankData.fightPower.ToString();
        }
        else
        {
            m_SheTuan.visible = false;
            Logger.err("未能拿到对应排名社团数据");
            return;
        }
    }
    private string OnGetIconName(int rank)
    {
        string name = "";
        switch (rank)
        {
            case 1: name = "ui://" + WinEnum.UI_Top + "/diyiming"; break;
            case 2: name = "ui://" + WinEnum.UI_Top + "/dierming"; break;
            case 3: name = "ui://" + WinEnum.UI_Top + "/disanming"; break;
            default: break;
        }
        return name;
    }
    private void OnGuildType(int type)
    {
        switch (type)
        {
            case 1:m_type_name.text = "休闲";break;
            case 2:m_type_name.text = "竞技";break;
        }
    }
    public int GetIndex()
    {
        return rankData.rank - 1;
    }
}
