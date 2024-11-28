using UI_Beibao;
using Data.Beans;
using Message.Bag;

public class BagWindowCtrl
{

    private UI_BagWindow window;

    private BagWindow owner;
    private GridInfo m_curGrid;

    /// 当前选中索引
    /// </summary>
    public int LastSelectedIndex = 0;

    public int LastSelectedGridId = 0;

    public BagWindowCtrl(BagWindow owner)
    {
        this.owner = owner;
        this.window = owner.window;
        window.m_sellBtn.onClick.Add(OnSellBtn);
        window.m_useBtn.onClick.Add(OnUseBtn);
        window.m_compoundBtn.onClick.Add(OnCompoundBtn);
    }

    public void OnItemSelect(GridInfo info)
    {
        m_curGrid = info;
        //GridInfo info = BagService.Singleton.GetGrid(LastSelectedGridId);
        if (info != null)
        {
            t_itemBean bean = ConfigBean.GetBean<t_itemBean, int>(info.itemInfo.id);
            //GridInfo itemInfo = BagService.Singleton.GetGrid(info.id);
            if (bean != null && info != null)
            {
                CommonItem listItem = window.m_selectIcon as CommonItem;
                listItem.Init(info.itemInfo.id, info.itemInfo.num);
                listItem.RefreshView();

                window.m_itemNum.text = info.itemInfo.num + "";
                window.m_itemName.color = UIUtils.GetItemColor(info.itemInfo.id);
                window.m_itemName.text = bean.t_name;
                window.m_desc.text = bean.t_describe;

                //不可出售
                if (bean.t_sell == 0)
                {
                    window.m_cannotSell.visible = true;
                    window.m_canSell.visible = false;
                    (window.m_sellBtn.GetChildAt(0)).grayed = true;
                    window.m_sellBtn.touchable = false;
                }
                else
                {
                    window.m_cannotSell.visible = false;
                    window.m_canSell.visible = true;
                    window.m_sellBtn.GetChildAt(0).grayed = false;
                    window.m_sellBtn.touchable = true;
                    window.m_sellPrice.text = bean.t_sell_price + "";
                }

                //不可使用
                if (bean.t_use == 0)
                {
                    window.m_useBtn.GetChildAt(0).grayed = true;
                    window.m_useBtn.touchable = false;
                }
                else
                {
                    window.m_useBtn.GetChildAt(0).grayed = false;
                    window.m_useBtn.touchable = true;
                }

                if (bean.t_tab == 3 && bean.t_type != 5)
                {
                    //(碎片类型(不包括宠物碎片)显示合成)合成
                    window.m_compoundBtn.visible = true;
                    window.m_useBtn.visible = false;
                    int target;
                    bool canCompoundBtn = _CheckItemCanCompound(info.itemInfo.id, out target) > 0;
                    window.m_compoundBtn.grayed = !canCompoundBtn;
                    window.m_compoundBtn.touchable = canCompoundBtn;
                }
                else
                {
                    window.m_compoundBtn.visible = false;
                    window.m_useBtn.visible = true;
                }
            }
            else
            {
                Logger.err("BagWindow:OnItemSelect:can not find item:" + info.itemInfo.id);
            }
        }
    }

    public void OnItemSelected()
    {

    }

    private void OnSellBtn()
    {
        //GridInfo info = BagService.Singleton.GetGrid(LastSelectedGridId);
        if (m_curGrid != null && m_curGrid.itemInfo.num == 1)
        {
            BagService.Singleton.ReqSellItem(m_curGrid.id, 1);
        }
        else
        {
            ThreeParam<int, int, int> param = new ThreeParam<int, int, int>();
            param.value1 = m_curGrid.itemInfo.id;
            param.value2 = m_curGrid.itemInfo.num;
            param.value3 = m_curGrid.id;
            owner.OpenChild<ItemSellWindow>(WinInfo.Create(false, owner.winName, false, param));
        }
    }

    public void OnItemSell(int sellNum)
    {
        GridInfo info = BagService.Singleton.GetGrid(LastSelectedGridId);
        if (info != null)
        {
            if (info.itemInfo.num == sellNum)
            {
                //LastSelectedIndex = LastSelectedIndex 
            }
            window.m_allList.RefreshVirtualList();
        }
    }

    private void OnUseBtn()
    {
        //GridInfo info = BagService.Singleton.GetGrid(LastSelectedGridId);
        if (m_curGrid != null)
        {
            t_itemBean bean = ConfigBean.GetBean<t_itemBean, int>(m_curGrid.itemInfo.id);
            if (bean != null)
            {
                if (!JumpWndMgr.Singleton.JumpToWnd((JumpType)bean.t_use_jump, bean.t_par))
                {
                    if (bean.t_script_id == 0)
                    {
                        TipWindow.Singleton.ShowTip("该道具不能主动使用");
                        return;
                    }

                    if (m_curGrid != null)
                    {
                        if (m_curGrid.itemInfo.num == 1)
                            BagService.Singleton.ReqItemUse(m_curGrid.id, 1);
                        else
                            owner.OpenChild<ItemUseWindow>(WinInfo.Create(false, owner.winName, false, m_curGrid.id));
                    }
                }
                
            }
        }
    }

    private void OnCompoundBtn()
    {
        GridInfo info = BagService.Singleton.GetGrid(LastSelectedGridId);
        if (info != null)
        {
            int target = 0;
            int num = _CheckItemCanCompound(info.itemInfo.id, out target);
            if (num <= 0)
            {
                TipWindow.Singleton.ShowTip("数量不足,无法合成");
                return;
            }
            else if (num == 1)
            {
                BagService.Singleton.ReqItemCompose(target, 1);
            }
            else
            {
                TwoParam<int, int> param = new TwoParam<int, int>();
                param.value1 = target;
                param.value2 = num;
                WinMgr.Singleton.Open<ItemComposeWindow>(WinInfo.Create(false, null, false, param), UILayer.Popup);
            }

        }
    }

    //检查道具能否(在背包)合成
    private int _CheckItemCanCompound(int itemId, out int target)
    {
        target = 0;
        t_itemBean itemBean = ConfigBean.GetBean<t_itemBean, int>(itemId);
        if (itemBean == null)
            return 0;

        //不是碎片
        if (itemBean.t_tab != 3)
            return 0;

        //宠物碎片不直接在背包合成
        if (itemBean.t_type == 5)
            return 0;


        target = itemBean.t_compose_target;
        return BagService.Singleton.GetItemCanComposeNum(target);


    }

    public void OnClose()
    {
        owner = null;
        window = null;
        LastSelectedGridId = 0;
        LastSelectedIndex = 0;

    }

    private int _GetDefaultPetId()
    {
        if (PetService.Singleton.GetPetInfos() == null || PetService.Singleton.GetPetInfos().Count == 0)
        {
            TipWindow.Singleton.ShowTip("当前没有宠物");
            return - 1;
        }

        return PetService.Singleton.GetPetInfos()[0].petId;
    }
    
}
