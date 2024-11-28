using UI_Guild;
using Message.Guild;
using Data.Beans;
using FairyGUI;
using System.Collections.Generic;

public class WishSelectWnd : BaseWindow
{

    private UI_WishSelectWnd m_window;
    private string []m_strArr = {"11+12", "13", "14"}; //页签表
    private int m_curSelectItemId = 0;
    private Dictionary<int, int> m_typeItemIdDic = new Dictionary<int, int>();

    public override void OnOpen()
    {
        base.OnOpen();
        m_window = getUiWindow<UI_WishSelectWnd>();
        m_window.m_btnClose.onClick.Add(Close);
        m_window.m_tabControl.onChanged.Add(_OnTabChanged);
        m_window.m_btnOk.onClick.Add(_OnWishClick);
        InitView();
    }

    public override void InitView()
    {
        base.InitView();
        m_window.m_tabControl.selectedIndex = -1;
        m_window.m_tabControl.selectedIndex = 0;
    }

    private List<int> _GetRewardInfo(string ids, out int maxNum)
    {
        List<int> itemList = new List<int>();
        maxNum = 0;
        int[] arrId = GTools.splitStringToIntArray(ids, '+');
        for (int i = 0; i < arrId.Length; i++)
        {
            int type = arrId[i];
            t_promiseBean bean = ConfigBean.GetBean<t_promiseBean, int>(type);
            if (bean != null)
            {
                int[] arrItems = GTools.splitStringToIntArray(bean.t_item_id, '+');
                if (arrItems == null || arrItems.Length == 0)
                    continue;

                for (int index = 0; index < arrItems.Length; index++)
                {
                    int itemId = arrItems[index];
                    itemList.Add(itemId);

                    if (!m_typeItemIdDic.ContainsKey(itemId))
                    {
                        m_typeItemIdDic.Add(itemId, type);
                    }

                }

                if (maxNum == 0)
                    maxNum = bean.t_quantity;
            }
        }

        return itemList;
    }


    private void _OnTabChanged()
    {
        if (m_window.m_tabControl.selectedIndex < 0 || m_window.m_tabControl.selectedIndex >= m_strArr.Length)
            return;

        int maxNum;
        List<int> rewardList = _GetRewardInfo(m_strArr[m_window.m_tabControl.selectedIndex], out maxNum);
        m_window.m_txtNum.text = string.Format("最大获得个数:{0}", maxNum);

        if (rewardList == null || rewardList.Count == 0)
            return;

        m_window.m_itemList.RemoveChildren(0, -1, true);
        for (int i = 0; i < rewardList.Count; i++)
        {
            int itemId = rewardList[i];
            CommonItem cell = CommonItem.CreateInstance();
            cell.itemId = itemId;
            cell.isShowNum = false;
            cell.RefreshView();
            m_window.m_itemList.AddChild(cell);

            if (m_curSelectItemId == 0 && i == 0)
            {
                m_curSelectItemId = itemId;
                _ShowCurSelectItem();
            }

            cell.onClick.Clear();
            cell.onClick.Add(() => {
                m_curSelectItemId = itemId;
                _ShowCurSelectItem();
            });
        }
    }


    private void _ShowCurSelectItem()
    {
        CommonItem itemIcon = m_window.m_imgIcon as CommonItem;
        if (itemIcon == null)
            return;

        itemIcon.itemId = m_curSelectItemId;
        itemIcon.isShowNum = false;
        itemIcon.RefreshView();
    }

    private void _OnWishClick()
    {
        if (!m_typeItemIdDic.ContainsKey(m_curSelectItemId))
        {
            Debuger.Log("道具未对应资质类型" + m_curSelectItemId);
            return;
        }
        GuildService.Singleton.ReqWish(m_typeItemIdDic[m_curSelectItemId], m_curSelectItemId);
        Close();
    }


    protected override void OnClose()
    {
        base.OnClose();
    }
}