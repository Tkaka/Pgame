using UI_GeDouJia;
using Data.Beans;

public class ShangDianHeWanFaListItem : UI_ShangDianHeWanFaListItem
{
    private int type;//1为玩法，2为商店
    public new static ShangDianHeWanFaListItem CreateInstance()
    {
        return (ShangDianHeWanFaListItem)UI_ShangDianHeWanFaListItem.CreateInstance();
    }
    public void Init(int id,int yuyanid,int laiyuan = -1)
    {
        type = laiyuan;
        t_languageBean languageBean = ConfigBean.GetBean<t_languageBean,int>(yuyanid);
        if (languageBean != null)
        {
            m_HuoQuLuJing.text = languageBean.t_content;
        }
        else
        {
            m_HuoQuLuJing.text = "语言包没有对应id" + id;
        }
        t_moduleBean moduleBean = ConfigBean.GetBean<t_moduleBean,int>(id);
        if (moduleBean != null)
            m_type.text = moduleBean.t_name;
        m_QiangWangBtn.onClick.Add(OnTiaoZuan);
    }
    /// <summary>
    /// 跳转管理
    /// </summary>
    private void OnTiaoZuan()
    {
        if(type > 0)
            JumpWndMgr.Singleton.JumpToWnd((JumpType)type);
    }
}
