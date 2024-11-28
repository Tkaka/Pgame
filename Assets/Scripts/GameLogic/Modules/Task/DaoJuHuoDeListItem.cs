//using UI_TaskSystem;
//using Data.Beans;
///// <summary>
///// 奖励列表元件
///// </summary>
//public class DaoJuHuoDeListItem : UI_DaoJuHuoDeListItem
//{
//    public new static DaoJuHuoDeListItem CreateInstance()
//    {
//        return (DaoJuHuoDeListItem)UI_DaoJuHuoDeListItem.CreateInstance();
//    }
//    public void Init(int itemid,int number)
//    {
//        t_itemBean itemBean = ConfigBean.GetBean<t_itemBean,int>(itemid);
//        if (itemBean == null)
//        {
//            Logger.err("DaiJuHuoDeListItem:Init:未能从道具表获得该数据------" + itemid);
//            return;
//        }
//        m_Icon.url = PathEnum.Icons + itemBean.t_icon;
//        m_name.text = itemBean.t_name;
//        m_number.text = number.ToString();
//    }
//    public override void Dispose()
//    {
//        base.Dispose();
//    }
//}
