using UI_Beibao;
using Data.Beans;

public class BagListItem : UI_BagListItem
{

    //道具id
    public int itemId;

    //格子id
    public int gridId;

    //道具数量 
    public int itemNum;

    //是否显示数量
    public bool showNum;

    public new static BagListItem CreateInstance()
    {
        return (BagListItem)UI_BagListItem.CreateInstance();
    }

    public void Init(int itemId, int gridId, int itemNum, bool showNum=true)
    {
        this.itemId = itemId;
        this.gridId = gridId;
        this.itemNum = itemNum;
        this.showNum = showNum;
        RefreshView();
    }

    public void RefreshView()
    {
        t_itemBean bean = ConfigBean.GetBean<t_itemBean, int>(itemId);
        if (bean != null)
        {
            if (bean.t_type == (int)ItemType.PetFragment)
                m_fragIcon.visible = true;
            else
                m_fragIcon.visible = false;
            UIGloader.SetUrl(m_borderLoader, UIUtils.GetItemBorder(itemId));
            UIGloader.SetUrl(m_iconLoader, bean.t_icon);
            if (showNum)
            {
                m_numTxt.visible = true;
                m_numTxt.text = itemNum + "";
            }
            else
            {
                m_numTxt.visible = false;
            }
            m_selectBorder.visible = false;
        }
    }

    public void SelectToggle(bool flag)
    {
        m_selectBorder.visible = flag;
    }

    public void OnItemUse()
    {

    }


}
