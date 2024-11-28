using UI_Guild;
using Message.Guild;
using Data.Beans;

public class GuildHallWnd : BaseWindow
{
    private UI_GuildHallWnd m_window;
    private string[] m_titleArr = {"基本信息","审核","日志" };
    private string m_curOpenWnd = "";

    public override void OnOpen()
    {
        base.OnOpen();
        m_window = getUiWindow<UI_GuildHallWnd>();
        m_window.m_btnClose.onClick.Add(Close);
        m_window.m_c1.onChanged.Add(_OnControlChanged);
        _InitTabList();
        InitView();

    }



    private void _InitTabList()
    {
        m_window.m_tabList.foldInvisibleItems = true;
        GuildInfo guildInfo = GuildService.Singleton.GetGuildInfo();
        bool isCanCheck = false;
        t_authorityBean authorityBean = ConfigBean.GetBean<t_authorityBean, int>(guildInfo.roleJob);
        if (authorityBean != null)
        {
            int[] authorityArr = GTools.splitStringToIntArray(authorityBean.t_authority_type, '+');
            if (authorityArr != null)
            {
                for (int i = 0; i < authorityArr.Length; i++)
                {
                    if ((int)GuildService.EAuthority.Check_ApplyList == authorityArr[i])
                    {
                        isCanCheck = true;
                        break;
                    }
                }
            }
        }

        for (int i = 0; i < m_titleArr.Length; i++)
        {
            UI_btnCell cell = UI_btnCell.CreateInstance();
            cell.m_txtDes.text = m_titleArr[i];
            m_window.m_tabList.AddChild(cell);
            if (i == 1)
            {

                cell.visible = isCanCheck;
                _RegisterRedDot("Guild/btnHall/Check", cell.m_imgRed);
            }
        }
    }

    public override void AddEventListener()
    {
        base.AddEventListener();
        GED.ED.addListener(EventID.GuildChange, _GuildIdChange);
    }

    public override void RemoveEventListener()
    {
        base.RemoveEventListener();
        GED.ED.removeListener(EventID.GuildChange, _GuildIdChange);
    }

    private void _GuildIdChange(GameEvent evt)
    {
        Close();
    }

    private void _OnControlChanged()
    {
        if (!m_curOpenWnd.Equals(""))
        {
            WinMgr.Singleton.Close(m_curOpenWnd);
        }

        switch (m_window.m_c1.selectedIndex)
        {
            case 0:
                //基本信息
                m_curOpenWnd = WinMgr.Singleton.Open<GuildBaseInfoWnd>();
                break;
            case 1:
                //审核
                m_curOpenWnd = WinMgr.Singleton.Open<CheckMemberJoinWnd>();
                break;
            case 2:
                //日志
                m_curOpenWnd = WinMgr.Singleton.Open<GuildLogWnd>();
                break;
            default:
                return;
        }
    }

    public override void InitView()
    {
        base.InitView();
        m_window.m_c1.selectedIndex = -1;
        m_window.m_c1.selectedIndex = 0;
    }


    protected override void OnClose()
    {
        base.OnClose();
        if (!m_curOpenWnd.Equals(""))
        {
            WinMgr.Singleton.Close(m_curOpenWnd);
        }
    }
}