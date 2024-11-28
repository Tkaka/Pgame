
//using Message.Bag;
//using UI_Beibao;
//using Data.Beans;

//public class ItemGetWindow : BaseWindow
//{

//    private UI_ItemGetWindow window;

//    public override void OnOpen()
//    {
//        base.OnOpen();
//        window = getUiWindow<UI_ItemGetWindow>();
//        window.m_okBtn.onClick.Add(OnCloseBtn);
//        InitView();
//    }

//    public override void InitView()
//    {
//        base.InitView();
//        ResBoxItemUse msg = (ResBoxItemUse)Info.param;
//        if (msg != null && msg.items != null)
//        {
//            CommonItem item = null;
//            t_itemBean bean = null;
//            foreach (ItemInfo info in msg.items)
//            {
//                bean = ConfigBean.GetBean<t_itemBean, int>(info.id);
//                if (bean != null)
//                {
//                    item = CommonItem.CreateInstance();
//                    item.itemId = info.id;
//                    item.itemNum = info.num;
//                    item.isShowNum = true;
//                    item.RefreshView(true);
//                    window.m_getList.AddChild(item);
//                }
//            }
//        }
//    }

//}


