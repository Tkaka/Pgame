using System.Collections.Generic;
using UI_StriveHegemong;
using Data.Beans;
using Message.KingFight;

public class SH_ExchangeWindow : BaseWindow
{
    private UI_SH_ExchangeWindow window;
    List<t_exchangeBean> beans;
    private int[] daibi = {4140061, 4140071,  4140051 };
    DoActionInterval doAction = null;

    public override void OnOpen()
    {
        window = getUiWindow<UI_SH_ExchangeWindow>();
        AddKeyEvent();
        beans = ConfigBean.GetBeanList<t_exchangeBean>();
        window.m_duihuanchenggong.visible = false;
        InitView();
    }
    public override void AddEventListener()
    {
        GED.ED.addListener(EventID.OnStriveHegemongExchange,OnZhanShi);
    }
    public override void RemoveEventListener()
    {
        GED.ED.removeListener(EventID.OnStriveHegemongExchange,OnZhanShi);
    }
    private void AddKeyEvent()
    {
        AddEventListener();
        window.m_CloseBtn.onClick.Add(OnCloseBtn);
    }
    public override void InitView()
    {
        RefreshView();
    }
    public override void RefreshView()
    {
        window.m_DuiHuanList.RemoveChildren(0,-1,true);
        SH_DH_DuiHuanListItem item;
        for (int i = 0; i < beans.Count; ++i)
        {
            item = SH_DH_DuiHuanListItem.CreateInstance();
            item.Init(beans[i]);
            window.m_DuiHuanList.AddChild(item);
        }
        window.m_OneNumber.text = BagService.Singleton.GetItemNum(daibi[0]).ToString();
        window.m_TowNumber.text = BagService.Singleton.GetItemNum(daibi[1]).ToString();
        window.m_ThreeNumber.text = BagService.Singleton.GetItemNum(daibi[2]).ToString();
    }
    private void OnZhanShi(GameEvent evt)
    {
        ResExchange exchange = evt.Data as ResExchange;
        if (exchange != null)
        {
            window.m_duihuanchenggong.visible = true;
            window.m_baoji.text = exchange.crit.ToString();
            window.m_number.text = exchange.num.ToString();
            t_itemBean itemBean = ConfigBean.GetBean<t_itemBean,int>(exchange.id);
            if (itemBean != null)
            {
                UIGloader.SetUrl(window.m_duihuanItem.m_beijing,UIUtils.GetItemBorder(exchange.id));
                UIGloader.SetUrl(window.m_duihuanItem.m_touxiang,UIUtils.GetItemIcon(itemBean.t_id));
                if (itemBean.t_type == 5)
                    window.m_duihuanItem.m_type.visible = true;
                else
                    window.m_duihuanItem.m_type.visible = false;
                window.m_duihuanItem.m_number.visible = false;

                if (doAction == null)
                {
                    doAction = new DoActionInterval();
                    doAction.doAction(1,OnGuanBi);
                }
            }
        }
    }
    private void OnGuanBi(object obj)
    {
        window.m_duihuanchenggong.visible = false;
        if (doAction != null)
        {
            doAction.kill();
            doAction = null; 
        }
    }
    protected override void OnCloseBtn()
    {
        if (doAction != null)
        {
            doAction.kill();
            doAction = null;
        }
        RemoveEventListener();
        window = null;
        base.OnCloseBtn();
    }
}
