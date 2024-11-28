using Data.Beans;
using FairyGUI;
using UI_Shop;
using Message.Shop;
using Message.Role;

public class RewardPreviewWnd : BaseWindow
{
    private UI_RewardPreviewWnd m_window;
    public override void OnOpen()
    {
        base.OnOpen();
        m_window = getUiWindow<UI_RewardPreviewWnd>();
        m_window.m_popupView.m_btnClose.onClick.Add(Close);
        m_window.m_mask.onClick.Add(Close);
        m_window.m_popupView.m_ctrl.onChanged.Add(_OnControlChanged);
        InitView();
        PlayPopupAnim(m_window.m_mask, m_window.m_popupView);
    }

    public override void InitView()
    {
        base.InitView();
        m_window.m_popupView.m_ctrl.selectedIndex = -1;
        m_window.m_popupView.m_ctrl.selectedIndex = 0;
    }


    private void _OnControlChanged()
    {
        int gTabId = 0;
        if (m_window.m_popupView.m_ctrl.selectedIndex == 0)
        {
            //全局表ID
            gTabId = 130203;
        }
        else
        {
            gTabId = 130204;
        }

        t_globalBean gBean = ConfigBean.GetBean<t_globalBean, int>(gTabId);
        if (gBean == null)
            return;


        m_window.m_popupView.m_mainList.RemoveChildren(0, -1, true);
        int[] arr = GTools.splitStringToIntArray(gBean.t_string_param, '+');
        for (int i = 0; i < arr.Length; i++)
        {
            t_itemBean bean = ConfigBean.GetBean<t_itemBean, int>(arr[i]);
            if (bean == null)
                continue;

            UI_rewardCell rewardCell = UI_rewardCell.CreateInstance();
            CommonItem commonItem = rewardCell.m_itemIcon as CommonItem;
            commonItem.Init(arr[i], 0, false);
            commonItem.RefreshView(true);

            m_window.m_popupView.m_mainList.AddChild(rewardCell);
        }
    }


    protected override void OnClose()
    {
        base.OnClose();
    }
}