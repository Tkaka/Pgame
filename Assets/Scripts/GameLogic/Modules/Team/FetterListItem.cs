using UI_BuZhen;
using Data.Beans;

public class FetterListItem : UI_FetterListItem
{
    public new static FetterListItem CreateInstance()
    {
        return (FetterListItem)UI_FetterListItem.CreateInstance();
    }

    public void Init(int fetterid,bool jihuo)
    {
        t_fetterBean fetterBean = ConfigBean.GetBean<t_fetterBean, int>(fetterid);
        if (fetterBean == null)
        {
            Logger.err("ZhenRongWindow:FetterListItem:Init():羁绊表中未获得对应id数据");
            return;
        }
        if (jihuo)
        {
            m_Name.text = "[color=#FFFF00]" + fetterBean.t_name + "[/color]";
        }
        else
        {
            m_Name.text = "[color=#808080]" + fetterBean.t_name + "[/color]";
        }
    }
    public override void Dispose()
    {
        base.Dispose();
    }
}
