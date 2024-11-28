using UI_Top;
using Message.Rank;
using Data.Beans;

public class Top_XiangQingWindow : BaseWindow
{
    private UI_Top_XiangQingWindow window;
    private GuildRankData guildData;
    private t_guildBean guildBean;
    public override void OnOpen()
    {
        base.OnOpen();
        window = getUiWindow<UI_Top_XiangQingWindow>();
        window.m_closeBtn.onClick.Add(OnCloseBtn);
        InitView();
    }
    public override void InitView()
    {
        base.InitView();
        if (Info.param == null)
        {
            Logger.err("未传入下标，无法加载详情");
            return;
        }
        int index = (int)Info.param;
        if (TopService.Singleton.topType == TopType.Guild)
        {
            guildData = TopService.Singleton.GetGuildData(index);
            Filldata();
        }
    }
    private void Filldata()
    {
        if (guildData == null)
        {
            Logger.err("未能获得数据，无法打开详情");
            return;
        }
        else
        {
            window.m_guild_name.text = guildData.name;
            window.m_guild_sir.text = guildData.chairManName;
            window.m_guild_gonggao.text = guildData.notice;
            guildBean = ConfigBean.GetBean<t_guildBean,int>(guildData.level);
            window.m_guild_chengyuan.text = guildData.memberNum + "/" + guildBean.t_member_num;
            //社团徽章和类型图片未设置
        }
    }
    protected override void OnCloseBtn()
    {
        window = null;
        base.OnCloseBtn();
    }
}
