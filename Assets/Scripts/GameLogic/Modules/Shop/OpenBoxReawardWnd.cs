using Message.Role;
using UnityEngine;
using FairyGUI;
using UI_Shop;
using Message.Shop;
using Data.Beans;
using System.Collections.Generic;

public class OpenBoxReawardWnd : BaseWindow
{
    private UI_OpenBoxReawardWnd m_window;
    private ResOpenResult m_msg;
    private DoActionInterval m_timer;
    private int m_curShowedNum = 0;   //当前已显示的道具数量

    public override void OnOpen()
    {
        base.OnOpen();
        m_window = getUiWindow<UI_OpenBoxReawardWnd>();
        m_window.m_btnOk.onClick.Add(Close);
        m_msg = Info.param as ResOpenResult;
        if (m_msg == null)
            return;

        InitView();
    }

    public override void InitView()
    {
        base.InitView();
        m_window.m_objGroup.visible = false;
        m_window.m_txtTitle.text = string.Format("恭喜获得装备觉醒碎片X{0},并赠送:", m_msg.suiPianNum);
        _ShowRewardInfo();

    }

    private void _ShowRewardInfo()
    {
        if (m_timer != null)
        {
            m_timer.kill();
            m_timer = null;
        }

        m_timer = new DoActionInterval();
        m_timer.doAction(0.3f, (param) =>
        {
            if (m_curShowedNum >= m_msg.items.Count)
            {
                m_timer.kill();
                m_timer = null;
                _ShowCoinInfo();
                return;
            }
            else
            {
                CommonItem cell = CommonItem.CreateInstance();
                cell.itemId = m_msg.items[m_curShowedNum].id;
                cell.itemNum = m_msg.items[m_curShowedNum].num;
                cell.isShowNum = true;
                cell.RefreshView();
                m_window.m_rewardList.AddChild(cell);
                m_window.m_rewardList.ScrollToView(m_window.m_rewardList.numChildren - 1, true, true);
            }

            m_curShowedNum++;
        });
    }

    private void _ShowCoinInfo()
    {
        m_window.m_objGroup.visible = true;
        if (m_msg.items.Count > 1)
        {
            _ShowOpen10Info();
        }
        else
        {
            _ShowOpen1Info();
        }
    }

    private void _ShowOpen1Info()
    {
        m_window.m_btnOpen1.visible = true;
        m_window.m_btnOpen10.visible = false;
        ResEquipBoxInfo info = ShopService.Singleton.GetEquipBoxInfo();
        if (info == null)
            return;


        t_equipawakenboxBean bean = ConfigBean.GetBean<t_equipawakenboxBean, int>(1);
        if (bean == null)
            return;
        int daiBiNum = BagService.Singleton.GetItemNum(bean.t_ticket);
        if (daiBiNum > 0 && info.free == false)
        {
            //m_window.m_imgComsume.url = UIUtils.GetItemIcon(bean.t_ticket);
            UIGloader.SetUrl(m_window.m_imgComsume, UIUtils.GetItemIcon(bean.t_ticket));
            m_window.m_txtNum.text = string.Format("{0}/{1}", daiBiNum, 1);
        }
        else
        {
            //m_window.m_imgComsume.url = UIUtils.GetItemIcon(-2); //钻石
            UIGloader.SetUrl(m_window.m_imgComsume, UIUtils.GetItemIcon(-2));
            if (info.free)
                m_window.m_txtNum.text = "0";
            else
                m_window.m_txtNum.text = bean.t_price + "";
        }

        m_window.m_btnOpen1.onClick.Clear();
        m_window.m_btnOpen1.onClick.Add(() =>
        {
            int type = 1;
            if (info.free)
                type = 2;
            else if (daiBiNum > 0)
                type = 3;
            else
                type = 1;
            ShopService.Singleton.ReqOpenBox(1, type);
            Close();
        });
    }

    private void _ShowOpen10Info()
    {
        m_window.m_btnOpen1.visible = false;
        m_window.m_btnOpen10.visible = true;
        t_equipawakenboxBean bean = ConfigBean.GetBean<t_equipawakenboxBean, int>(10);
        if (bean == null)
            return;

        int daiBiNum = BagService.Singleton.GetItemNum(bean.t_ticket);
        if (daiBiNum >= 10)
        {
            //m_window.m_imgComsume.url = UIUtils.GetItemIcon(bean.t_ticket);
            UIGloader.SetUrl(m_window.m_imgComsume, UIUtils.GetItemIcon(bean.t_ticket));
            m_window.m_txtNum.text = string.Format("{0}/{1}", daiBiNum, 10);
        }
        else
        {
           // m_window.m_imgComsume.url = UIUtils.GetItemIcon(-2); //钻石
            UIGloader.SetUrl(m_window.m_imgComsume, UIUtils.GetItemIcon(-2));
            m_window.m_txtNum.text = bean.t_price + "";
        }

        m_window.m_btnOpen10.onClick.Clear();
        m_window.m_btnOpen10.onClick.Add(() =>
        {
            int type = 1;
            if (daiBiNum >= 10)
                type = 3;
            else
                type = 1;
            ShopService.Singleton.ReqOpenBox(10, type);
            Close();
        });
    }


    protected override void OnClose()
    {
        base.OnClose();
        m_msg = null;
        if (m_timer != null)
        {
            m_timer.kill();
            m_timer = null;
        }
    }
}