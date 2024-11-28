using UI_Common;
using FairyGUI;
using FairyGUI.Utils;
using Data.Beans;
using UI_Arena; 
public class JinjiIconCell : UI_JinJiIconCell
{

    //道具id
    public int itemId;

    //道具数量 
    public int itemNum;
    public bool isShowNum = false;

    public new static JinjiIconCell CreateInstance()
    {
        return UI_JinJiIconCell.CreateInstance() as JinjiIconCell;
    }

    public void RefreshView()
    {

        m_numTxt.visible = isShowNum;

        t_itemBean bean = ConfigBean.GetBean<t_itemBean, int>(itemId);
        if (bean != null)
        {
            int quality = UIUtils.GetDefaultItemQuality(itemId);
            if (quality != -1)
                UIGloader.SetUrl(m_borderLoader, UIUtils.GetItemBorder(itemId));
            UIGloader.SetUrl(m_iconLoader, bean.t_icon);
            m_numTxt.text = itemNum + "";
            m_fragIcon.visible = false;
        }
    }

    public override void Dispose()
    {
        base.Dispose();
    }
}
