using UI_GuildDrill;
using Message.Guild;

public class GD_JiaSuiListItem : UI_GD_JiaSuiListItem
{
    public long roleId;//角色id
    public new static GD_JiaSuiListItem CreateInstance()
    {
        return (GD_JiaSuiListItem)UI_GD_JiaSuiListItem.CreateInstance();
    }
    public void Init(ExpHomeRole rolefo,bool jiasu)
    {
        m_name.text = rolefo.name;
        m_level.text = rolefo.level + "";
        m_HaoYouBiaoJi.grayed = rolefo.star;
        roleId = rolefo.roleId;
        m_jiasuBtn.onClick.Add(OnWeiTaJiaSuBtn);
        m_jiasuBtn.grayed = !jiasu;
        m_jiasuBtn.touchable = jiasu;
    }
    public void RefreshView(bool isok)
    {
        m_jiasuBtn.grayed = isok;
    }
    private void OnWeiTaJiaSuBtn()
    {
        //打开为他加速的窗口，传入角色id
        WinInfo info = new WinInfo();
        info.param = roleId;
        WinMgr.Singleton.Open<GD_WeTaJiaSuWindow>(info,UILayer.Popup);
    }
}
