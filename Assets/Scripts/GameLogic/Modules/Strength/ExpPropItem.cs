using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_Strength;
using FairyGUI;
using Data.Beans;
using Message.Bag;
using Message.Pet;
public class ExpPropItem : UI_expPropItem {

    private int _propID;
    private t_itemBean _prop;
    private Message.Bag.GridInfo _gridInfo;
    private StrengthShengJi _parentUI;
    private LongPressGesture _longPressGestur;
    private bool _isLongPress;
    public int index;
    private int limitLv;
    private int _num;
    /// <summary>
    /// 每次使用的个数
    /// </summary>
    private int numBase = 1;
    private float timer;
    private float maxBase = 1000;
    public int PropID
    {
        get { return _propID; }
        set
        {
            _propID = value;
            var itemList = ConfigBean.GetBeanList<t_itemBean>();
            _prop = ConfigBean.GetBean<t_itemBean, int>(_propID);
        }
    }

    public new static UI_expPropItem CreateInstance()
    {
        return (ExpPropItem)UIPackage.CreateObject("UI_Strength", "expPropItem");
    }

    public void Init(StrengthShengJi parentUI)
    {
        _parentUI = parentUI;
        m_expToucher.onClick.Add(OnClickItem);
        _longPressGestur = new LongPressGesture(m_expToucher);
        _longPressGestur.trigger = 1;
        _longPressGestur.interval = 0.1f;
        _longPressGestur.onAction.Add(OnLongPressItem);
        _longPressGestur.onEnd.Add(OnLongPressEnd);

        _parentUI.StrengthData.ExpPropDict.TryGetValue(_propID, out _gridInfo);
        var itemInfo = _gridInfo != null ? _gridInfo.itemInfo : null;
        var itemBean = ConfigBean.GetBean<t_itemBean, int>(_propID);
        if (itemBean != null)
        {
            m_tipLabel.text = string.Format("经验值+{0}", itemBean.t_value);
            UIGloader.SetUrl(m_borderBg, UIUtils.GetItemBorder(itemBean.t_id));
            UIGloader.SetUrl(m_caiLiaoIcon, itemBean.t_icon);
        }
        _num = 0;
        _isLongPress = false;

        InitLevelBuyLimit();
    }

    public void RefreshView()
    {

        _parentUI.StrengthData.ExpPropDict.TryGetValue(_propID, out _gridInfo);
        var itemInfo = _gridInfo != null ? _gridInfo.itemInfo : null;
        var itemBean = ConfigBean.GetBean<t_itemBean, int>(_propID);

        m_number.text = itemInfo != null ? itemInfo.num.ToString() : "";
        
        m_caiLiaoIcon.grayed = (_gridInfo == null || itemInfo.num <= 0);
        m_unFullGroup.visible = (_gridInfo == null || itemInfo.num <= 0);
    }

    private void OnClickItem()
    {
        if (_gridInfo == null || _gridInfo.itemInfo.num <= 0)
        {
            if (_isLongPress)
            {
                _isLongPress = !_isLongPress;
            }
            else
            {
                // TODO : 购买等级限制
                if (LevelBuyLimit())
                {
                    WinInfo winInfo = WinInfo.Create();
                    winInfo.param = _prop;
                    WinMgr.Singleton.Open<BuyExpWindow>(winInfo, UILayer.Popup);
                }
                else
                {
                    string str = string.Format("训练家达到等级{0}解锁", limitLv);
                    TipWindow.Singleton.ShowTip(str);
                }
            }
        }
        else
        {
            if (IsFullExp())
            {
                TipWindow.Singleton.ShowTip("经验值已满!");
                return;
            }
            _parentUI.StrengthData.ReqPetAddExp(_propID, 1);
            var itemBean = ConfigBean.GetBean<t_itemBean, int>(_propID);
            _parentUI.RefreshExpBar(int.Parse(itemBean.t_value));
            _gridInfo.itemInfo.num--;

            if (_gridInfo.itemInfo.num <= 0)
            {
                _parentUI.StrengthData.ExpPropDict[_propID] = null;
            }

            RefreshView();
        }
    }

    private void OnLongPressItem()
    {
        if (_num == -1)
            return;
        timer += _longPressGestur.interval;
        if (numBase < maxBase)
        {
            numBase = (int)Mathf.Pow(10, (int)timer);
        }
        // 长按连续使用道具
        _isLongPress = true;
        if (IsFullExp())
        {
            TipWindow.Singleton.ShowTip("经验值已满!");
            return;
        }
        if (_gridInfo == null)
            return;

        if ( _gridInfo.itemInfo.num > 0)
        { 
            var itemBean = ConfigBean.GetBean<t_itemBean, int>(_propID);
            int exp = int.Parse(itemBean.t_value);
            int useNum = _gridInfo.itemInfo.num >= numBase ? numBase : _gridInfo.itemInfo.num;
            int needNum = Mathf.CeilToInt(_parentUI.StrengthData.GetToMaxLevelNeedExp(_parentUI.GetCurExp()) / exp);
            useNum = needNum >= useNum ? useNum : needNum;
            _num += useNum;
            _parentUI.LongPressRefreshBar(exp * useNum, _num);
            _gridInfo.itemInfo.num -= useNum;

            RefreshView();
        }
        else
        {
            _parentUI.StrengthData.ReqPetAddExp(_propID, _num);
            _num = -1;
        }
    }

    private void OnLongPressEnd()
    {
        if (_num <= 0)
        {
            _num = 0;
            return;
        }

        _parentUI.StrengthData.ReqPetAddExp(_propID, _num);
        _num = 0;
    }

    private bool IsFullExp()
    {
        Message.Role.RoleInfo roleInfo = RoleService.Singleton.GetRoleInfo();
        var petInfo = _parentUI.StrengthData.CurSelectPetInfo;
        if (petInfo.basInfo.level >= roleInfo.level && _parentUI.ExpBarIsFull)
        {
            return true;
        }
        return false;
    }

    private void InitLevelBuyLimit()
    {
        // 100101  经验药购买限制
        t_globalBean globalBean = ConfigBean.GetBean<t_globalBean, int>(100101);
        if (globalBean != null)
        {
            int[] limitLvArr = GTools.splitStringToIntArray(globalBean.t_string_param);
            if (limitLvArr.Length == 1 && limitLvArr[0] == 0)
                limitLv = int.MaxValue;

            limitLv = limitLvArr.Length > index ? limitLvArr[index] : int.MaxValue;
        }
    }

    private bool LevelBuyLimit()
    {
        Message.Role.RoleInfo roleInfo = RoleService.Singleton.GetRoleInfo();
        return roleInfo.level >= limitLv;
    }
}
