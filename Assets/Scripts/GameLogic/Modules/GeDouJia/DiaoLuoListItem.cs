using UI_GeDouJia;
using Data.Beans;

public class DiaoLuoListItem : UI_DiaoLuoListItem
{
    public new static DiaoLuoListItem CreateInstance()
    {
        return (DiaoLuoListItem)UI_DiaoLuoListItem.CreateInstance();
    }
    public void Init(int itemId)
    {
        t_itemBean itemBean = ConfigBean.GetBean<t_itemBean,int>(itemId);
        if (itemBean == null)
        {
            Logger.err("未能获得道具参数" + itemId);
            return;
        }
        UIGloader.SetUrl(m_BianKuang, UIUtils.GetBorderUrl(int.Parse(itemBean.t_quality)));
        UIGloader.SetUrl(m_TouXiang, itemBean.t_icon);
    }
}
