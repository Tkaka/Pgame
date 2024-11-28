using Message.Role;
using UnityEngine;
using FairyGUI;
using UI_Shop;
using Message.Shop;
using Data.Beans;
using System.Collections.Generic;

public class ShopMainWindow : BaseWindow
{
    private UI_ShopMainWindow m_window;
    private string[] m_arrTitle = { "装备觉醒宝箱", "杂货商店", "荣誉商店", "试炼商店", "社团商店"};
    //private int[] m_arrOpenLevel = { 0, 0, 0, 0, 0};       //商店开启等级


    private DoActionInterval m_normalTime;
    private DoActionInterval m_supperTime;
    private Dictionary<GTextField, bool> m_txtTimeDic = new Dictionary<GTextField, bool>();
    private long m_supperRefreshTime = 0;


    private ShopService.EShopType m_curSelectShop;      //当前选中商店类型
    private string m_equipBoxWndName = "";              //装备宝箱窗口

    private ResGoodsInfo m_curGoodsInfo;
    ShopCommonList m_shopList;

    private bool lockChange = true;
    private UITable table;

    public override void OnOpen()
    {
        base.OnOpen();
        WinMgr.Singleton.Open<SellCoinItemWnd>(null, UILayer.Popup);
        m_window = getUiWindow<UI_ShopMainWindow>();
        (m_window.m_commonTop as UI_Common.UI_commonTop).m_closeBtn.onClick.Add(Close);
        m_window.m_btnRefresh.onClick.Add(_OnRefreshClick);

        m_shopList = m_window.m_objList as ShopCommonList;
        if (m_shopList != null)
        {
            m_shopList.Init();
        }
        else
        {
            Close();
            return;
        }

        InitView();
        PlayOpenEffect();
    }

    public override void PlayOpenEffect()
    {
        base.PlayOpenEffect();

        m_window.m_shopToggleGroup.m_anim.Play();
        (m_window.m_commonTop as UI_Common.UI_commonTop).m_anim.Play();
    }

    public override void InitView()
    {
        base.InitView();

        _InitTabList();

        (m_window.m_commonTop as UI_Common.UI_commonTop).m_title.text = "商店";
    }

    private int _GetCurOpenIndex()
    {
        for (int i = 0; i < m_arrTitle.Length; i++)
        {
            if (i == 0)
            {
                if (FuncService.Singleton.IsFuncOpen(1301))
                    return i;
            }

            switch ((ShopService.EShopType)i)
            {
                case ShopService.EShopType.Sundry:
                    {
                        if (FuncService.Singleton.IsFuncOpen(1302))
                            return i;
                    }
                    break;
                case ShopService.EShopType.Honor:
                    {
                        if (FuncService.Singleton.IsFuncOpen(1303))
                            return i;
                    }
                    break;
                case ShopService.EShopType.Trial:
                    {
                        if (FuncService.Singleton.IsFuncOpen(1304))
                        {
                            return i;
                        }
                    }
                    break;
                case ShopService.EShopType.Guild:
                    {
                        if (FuncService.Singleton.IsFuncOpen(1305))
                            return i;
                    }
                    break;
                default:
                    break;
            }
        }
        return 0;
    }


    private void _InitTabList()
    {
        int selectParam = -1;
        if (Info.param != null)
        {
            selectParam = (int)Info.param;
        }

        if (selectParam == -1)
            selectParam = _GetCurOpenIndex();

        //装备觉醒宝箱
        _RegisterRedDot("Shop/EquipBox", m_window.m_shopToggleGroup.m_equipAwakeBtn.m_m_redPoint);
        FuncService.Singleton.SetFuncLock(m_window.m_shopToggleGroup.m_equipAwakeBtn, 1301);

        table = new UITable();
        table.Init(m_window.m_shopToggleGroup.m_ctrl, _OnTabCtrlChanged);
        table.AddFuncLock(0, 1301, m_window.m_shopToggleGroup.m_equipAwakeBtn);
        table.AddFuncLock(1, 1302, m_window.m_shopToggleGroup.m_zaHuoShopBtn);
        table.AddFuncLock(2, 1303, m_window.m_shopToggleGroup.m_rongYuBoxBtn);
        table.AddFuncLock(3, 1304, m_window.m_shopToggleGroup.m_shiLianShopBtn);
        table.AddFuncLock(4, 1305, m_window.m_shopToggleGroup.m_sheTuanShopBtn);

        table.AddBtnAnim(m_window.m_shopToggleGroup.m_equipAwakeBtn, m_window.m_shopToggleGroup.m_zaHuoShopBtn, m_window.m_shopToggleGroup.m_rongYuBoxBtn
           ,m_window.m_shopToggleGroup.m_shiLianShopBtn, m_window.m_shopToggleGroup.m_sheTuanShopBtn);

        if (m_window.m_shopToggleGroup.m_ctrl.selectedIndex == selectParam)
            _OnTabCtrlChanged(selectParam);
        else
            m_window.m_shopToggleGroup.m_ctrl.selectedIndex = selectParam;

        //int curSelectCell = selectParam;
        //for (int i = 0; i < m_arrTitle.Length; i++)
        //{

        //    UI_tabCell cell = UI_tabCell.CreateInstance();
        //    cell.m_txtDescribe.text = m_arrTitle[i];
        //    cell.relatedController = m_window.m_c1;
        //    m_window.m_tabList.AddChild(cell);

        //    if (i == 0)
        //    {
        //        //装备觉醒宝箱
        //        _RegisterRedDot("Shop/EquipBox", cell.m_imgRed);
        //        FuncService.Singleton.SetFuncLock(cell, 1301);
        //    }
        //    else if (i == (int)ShopService.EShopType.Sundry)
        //    {
        //        FuncService.Singleton.SetFuncLock(cell, 1302);
        //    }
        //    else if (i == (int)ShopService.EShopType.Honor)
        //    {
        //        FuncService.Singleton.SetFuncLock(cell, 1303);
        //    }
        //    else if (i == (int)ShopService.EShopType.Trial)
        //    {
        //        FuncService.Singleton.SetFuncLock(cell, 1304);
        //    }
        //    else if (i == (int)ShopService.EShopType.Guild)
        //    {
        //        FuncService.Singleton.SetFuncLock(cell, 1305);
        //    }


        //    if (curSelectCell == -1 || curSelectCell == i)
        //    {
        //        curSelectCell = i;
        //        cell.selected = true;
        //        if (m_window.m_c1.selectedIndex == i)
        //        {
        //            m_window.m_c1.selectedIndex = -1;
        //        }

        //        m_window.m_c1.selectedIndex = i;
        //    }


        //}

    }

    public override void AddEventListener()
    {
        base.AddEventListener();
        GED.ED.addListener(EventID.ShopPrepareRefresh, _OnPrepareRefresh);
        GED.ED.addListener(EventID.ShopRefresh, _OnRefresh);
        GED.ED.addListener(EventID.ShopBuyResult, _OnBuyedResult);
    }

    public override void RemoveEventListener()
    {
        base.RemoveEventListener();
        GED.ED.removeListener(EventID.ShopPrepareRefresh, _OnPrepareRefresh);
        GED.ED.removeListener(EventID.ShopRefresh, _OnRefresh);
        GED.ED.removeListener(EventID.ShopBuyResult, _OnBuyedResult);
    }

    //准备刷新
    private void _OnPrepareRefresh(GameEvent evt)
    {
        List<int> shopTypes = evt.Data as List<int>;
        if (shopTypes == null)
            return;

        for (int i = 0; i < shopTypes.Count; i++)
        {
            if (shopTypes[i] == (int)m_curSelectShop)
            {
                ShopService.Singleton.ReqGoodsInfo((int)m_curSelectShop);
                break;
            }
        }
         
    }

    //开始刷新
    private void _OnRefresh(GameEvent evt)
    {
        if (ShopService.Singleton.GetShopInfo(m_curSelectShop) != null)
        {
            _ShowShopInfo(ShopService.Singleton.GetShopInfo(m_curSelectShop));
        }
    }

    //购买刷新
    private void _OnBuyedResult(GameEvent evt)
    {
        ThreeParam<int, int, int> param = evt.Data as ThreeParam<int, int, int>;
        if (param == null)
            return;

        if ((int)m_curSelectShop != param.value3)
            return;

        if (ShopService.Singleton.GetShopInfo(m_curSelectShop) == null)
        {
            return;

        }

        _ShowShopInfo(ShopService.Singleton.GetShopInfo(m_curSelectShop));
    }

    //页签改变
    private void _OnTabCtrlChanged(int index)
    {
        SetSelectedShop();
        if (!_FunIsOpen(m_curSelectShop))
            return;
        m_window.m_c1.selectedIndex = m_window.m_shopToggleGroup.m_ctrl.selectedIndex;
        if (m_curSelectShop == 0)
        {
            if (m_equipBoxWndName.Equals(""))
            {
                m_equipBoxWndName = WinMgr.Singleton.Open<EquipBoxItem>(null, UILayer.Popup);
            }
             
        }
        else
        {
            if (!m_equipBoxWndName.Equals(""))
            {
                WinMgr.Singleton.Close(m_equipBoxWndName);
                m_equipBoxWndName = "";
            }
            if (ShopService.Singleton.GetShopInfo(m_curSelectShop) != null)
            {
                if (ShopService.Singleton.GetShopIsRefreshed(m_curSelectShop))
                {
                    ShopService.Singleton.ReqGoodsInfo((int)m_curSelectShop);
                    return;
                }
                _ShowShopInfo(ShopService.Singleton.GetShopInfo(m_curSelectShop));
            }
            else
            {
                ShopService.Singleton.ReqGoodsInfo((int)m_curSelectShop);
            }
        }
 

        _ShowShopCurrenyInfo();
    }

    private void BtnStateChange(EventContext context)
    {
        if (!_FunIsOpen(m_curSelectShop))
        {
            m_window.m_shopToggleGroup.m_ctrl.selectedIndex = m_window.m_shopToggleGroup.m_ctrl.previsousIndex;
            return;
        }
        else
        {
            m_window.m_shopToggleGroup.m_ctrl.selectedIndex = (int)m_curSelectShop;
        }
    }

    private void SetSelectedShop()
    {
        switch (m_window.m_shopToggleGroup.m_ctrl.selectedIndex)
        {
            case 0:
                //装备觉醒宝箱
                m_curSelectShop = 0;
                break;
            case 1:
                //杂货商店
                m_curSelectShop = ShopService.EShopType.Sundry;
                break;
            case 2:
                //荣誉商店
                m_curSelectShop = ShopService.EShopType.Honor;

                break;
            case 3:
                //试炼商店
                m_curSelectShop = ShopService.EShopType.Trial;

                break;
            case 4:
                //社团商店
                m_curSelectShop = ShopService.EShopType.Guild;

                break;
            case 5:
                //道具商店
                m_curSelectShop = ShopService.EShopType.Prop;
                break;
        }
    }

    private bool _FunIsOpen(ShopService.EShopType shopType)
    {
        if((int)shopType == 0)
            return FuncService.Singleton.TipFuncNotOpen(1301);

        switch (shopType)
        {
            case ShopService.EShopType.Sundry:
                return FuncService.Singleton.TipFuncNotOpen(1302);
            case ShopService.EShopType.Honor:
                return FuncService.Singleton.TipFuncNotOpen(1303);
            case ShopService.EShopType.Trial:
                return FuncService.Singleton.TipFuncNotOpen(1304);
            case ShopService.EShopType.Guild:
                return FuncService.Singleton.TipFuncNotOpen(1305);
            default:
                return false;
        }
    }

    //显示商店信息
    private void _ShowShopInfo(ResGoodsInfo info)
    {
        //m_window.m_mainList.RemoveChildren(0, -1, true);
         
        if (info == null)
            return;

        m_curGoodsInfo = info;

        m_shopList.RefreshView(m_curGoodsInfo);

        _ShowNormalShopRefreshInfo(info);
    }


    //显示商店需要的货币信息
    private void _ShowShopCurrenyInfo()
    {
        t_globalBean gBean = ConfigBean.GetBean<t_globalBean, int>(130201);
        if (gBean == null)
            return;

        string[] strInfo = GTools.splitString(gBean.t_string_param, ';');
        for (int i = 0; i < strInfo.Length; i++)
        {
            int[] arrInfo = GTools.splitStringToIntArray(strInfo[i], '+');
            if (arrInfo == null || arrInfo.Length < 3)
                continue;
            if (arrInfo[0] == (int)m_curSelectShop)
            {
                //m_window.m_imgComsume.url = UIUtils.GetItemIcon(arrInfo[1]);
                UIGloader.SetUrl(m_window.m_imgComsume, UIUtils.GetItemIcon(arrInfo[1]));
                m_window.m_txtNum.text = RoleService.Singleton.GetCurrencyNum(arrInfo[1]) + "";
                m_window.m_txtCoinDes.text = UIUtils.GetStrByLanguageID(arrInfo[2]);
                break;
            }
        }

    }




    //显示本商店普通商品的刷新信息
    private void _ShowNormalShopRefreshInfo(ResGoodsInfo info)
    {
        int totalRefresgCount = ConfigBean.GetBean<t_globalBean, int>(130103).t_int_param;
        m_window.m_txtCount.text = string.Format("{0}/{1}", totalRefresgCount - info.refreshCount, totalRefresgCount);

        int remainTime = -(int)(TimeUtils.CalculateDelta(info.refreshTime) / 1000);//ShopService.Singleton.GetToTargetRemainTime(130101);
        if (remainTime <= 0)
        {
            return;
        }

        if (m_normalTime != null)
        {
            m_normalTime.kill();
            m_normalTime = null;
        }

        m_normalTime = new DoActionInterval();
        m_normalTime.doAction(1, (param) =>
        {
            remainTime--;
            if (remainTime <= 0)
            {
                m_normalTime.kill();
                m_normalTime = null;
                return;
            }
            m_window.m_txtTime.text = TimeUtils.FormatTime(remainTime);
        });
         
    }


    //刷新点击
    private void _OnRefreshClick()
    {
        if (ShopService.Singleton.GetShopInfo(m_curSelectShop) != null)
        {
            ResGoodsInfo shopInfo = ShopService.Singleton.GetShopInfo(m_curSelectShop);
            int totalRefresgCount = ConfigBean.GetBean<t_globalBean, int>(130103).t_int_param;
            t_globalBean gBean = ConfigBean.GetBean<t_globalBean, int>(130105);
            int[] arr = GTools.splitStringToIntArray(gBean.t_string_param, '+');
            int comsume = 0;
            if (shopInfo.refreshCount > arr.Length - 1)
            {
                comsume = arr[arr.Length - 1];
            }
            else
            {
                comsume = arr[shopInfo.refreshCount];
            }

            string strDes = string.Format("立即刷新商店(不包括超级商品)需花费钻石：{0}\n今日剩余次数：{1}/{2}", 
                comsume, totalRefresgCount - shopInfo.refreshCount, totalRefresgCount);
            CommonTipsManager.GetInstance().ShowTips(TipsType.SingleButton, "刷新商店", strDes, () =>
            {
                ShopService.Singleton.ReqRefresh(shopInfo.shopType, 1);
            });
             
        }
    }


    protected override void OnClose()
    {
        if (m_normalTime != null)
            m_normalTime.kill();
        if (m_supperTime != null)
            m_supperTime.kill();

        m_txtTimeDic.Clear();
        if (!m_equipBoxWndName.Equals(""))
        {
            WinMgr.Singleton.Close(m_equipBoxWndName);
            m_equipBoxWndName = "";
        }

        if (table != null)
            table.Close();

        base.OnClose();
    }
}