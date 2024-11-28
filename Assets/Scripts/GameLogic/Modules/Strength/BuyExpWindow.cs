using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_Strength;
using Data.Beans;
using Message.Role;

public class BuyExpWindow : BaseWindow {

    UI_BuyExpWindow _window;
    t_itemBean _itemBean;
    RoleInfo _role;
    private int _num;
    /// <summary>
    /// 最大购买数量
    /// </summary>
    private int _maxNum;
    /// <summary>
    /// 单价
    /// </summary>
    private int _perPrice;

    public override void OnOpen()
    {
        base.OnOpen();

        _window = getUiWindow<UI_BuyExpWindow>();
        _num = 1;
        _itemBean = Info.param as t_itemBean;
        _role = RoleService.Singleton.GetRoleInfo();

        _maxNum = 0;
        _perPrice = _itemBean.t_buy_price;
        t_globalBean globalBean = ConfigBean.GetBean<t_globalBean, int>(100102);
        if (globalBean != null)
        {
            _maxNum = globalBean.t_int_param / _perPrice;
        }

        BindEvent();
        InitView();
        PlayPopupAnim(_window.m_mask, _window.m_popupView);
    }

    public override void InitView()
    {
        base.InitView();

        UIGloader.SetUrl(_window.m_popupView.m_itemBorderLoader, UIUtils.GetItemBorder(_itemBean.t_id));
        UIGloader.SetUrl(_window.m_popupView.m_itemIconLoader, _itemBean.t_icon);
        //TODO : 下面这行代码解注释报错
        //_window.m_nameLabel.text = _itemBean.t_name;
        _window.m_popupView.m_valueLabel.text = string.Format("经验+{0}", _itemBean.t_value);
        RefreshTexts();
    }

    public override void AddEventListener()
    {
        base.AddEventListener();

        GED.ED.addListener(EventID.ResBagUpdate, OnBagUpdate);
    }

    public override void RemoveEventListener()
    {
        base.RemoveEventListener();

        GED.ED.removeListener(EventID.ResBagUpdate, OnBagUpdate);
    }

    private void OnBagUpdate(GameEvent evt)
    {
        // 收到背包刷新的消息，关闭购买界面
        TipWindow.Singleton.ShowTip("购买成功");
        OnCloseBtn();
    }

    /// <summary>
    /// 刷新文本
    /// </summary>
    private void RefreshTexts()
    {
        // 
        _window.m_popupView.m_numLabel.text = _num.ToString();
        _window.m_popupView.m_priceLabel.text = (_num * _perPrice).ToString();
    }

    protected override void OnCloseBtn()
    {
        _window = null;
        _itemBean = null;
        _role = null;

        base.OnCloseBtn();
    }

    private void BindEvent()
    {
        _window.m_popupView.m_addBtn.onClick.Add(OnAddBtnClick);
        _window.m_popupView.m_reduceBtn.onClick.Add(OnReduceBtnClick);
        _window.m_popupView.m_maxBtn.onClick.Add(OnMaxBtnClick);
        _window.m_popupView.m_buyBtn.onClick.Add(OnBuyBtnClick);
        _window.m_popupView.m_closeBtn.onClick.Add(OnCloseBtn);
        _window.m_mask.onClick.Add(OnCloseBtn);
    }

    private bool IsEnoughZuanShi()
    {
        int price = _num * _perPrice;
        return _role.damond >= price;
    }

    #region   事件回调函数 ------------------------------------------------------
    private void OnAddBtnClick()
    {
        // 判断是否有足够的钻石
        _num++;
        if (IsEnoughZuanShi())
        {
            if(_num > _maxNum)
            {
                TipWindow.Singleton.ShowTip("已达购买上限！");
                _num = _maxNum;
                return;
            }
            RefreshTexts();
        }
        else
        {
            _num--;
            TipWindow.Singleton.ShowTip("钻石不足！");
        }
    }

    private void OnReduceBtnClick()
    {
        if (_num <= 1)
        {
            TipWindow.Singleton.ShowTip("最少为一个！");
            return;
        }

        _num--;
        RefreshTexts();
    }

    private void OnMaxBtnClick()
    {
        // 计算可购买的最大数量
        _num = Mathf.FloorToInt(_role.damond / _perPrice);
        _num = _num <= 0 ? 1 : _num;
        _num = _num >= _maxNum ? _maxNum : _num;

        RefreshTexts();
    }

    private void OnBuyBtnClick()
    {
        if (IsEnoughZuanShi())
        {
            // 发送购买道具的消息
            Message.Bag.ItemInfo itemInfo = new Message.Bag.ItemInfo();
            itemInfo.id = _itemBean.t_id;
            itemInfo.num = _num;

            List<Message.Bag.ItemInfo> itemInfoList = new List<Message.Bag.ItemInfo>();
            itemInfoList.Add(itemInfo);

            BagService.Singleton.ReqDiamondBuyItem(itemInfoList);
        }
        else
        {
            TipWindow.Singleton.ShowTip("钻石不足！");
        }
    }

    #endregion
}
