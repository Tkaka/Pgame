using UI_StriveHegemong;
using Data.Beans;

public class SH_DH_DuiHuanListItem : UI_SH_DH_DuiHuanListItem
{
    private t_exchangeBean exchangeBean;
    private int itemId;
    public new static SH_DH_DuiHuanListItem CreateInstance()
    {
        return (SH_DH_DuiHuanListItem)UI_SH_DH_DuiHuanListItem.CreateInstance();
    }
    public void Init(t_exchangeBean bean)
    {
        exchangeBean = bean;
        FillInitDaiBi();
        FillInitDaoJu();
        m_DuiHuanBtn.onClick.Add(OnDuiHuanBtn);
    }
    private void FillInitDaiBi()
    {
        if (string.IsNullOrEmpty(exchangeBean.t_need))
        { }
        else
        {
            int[] daibi = GTools.splitStringToIntArray(exchangeBean.t_need);
            t_itemBean itemBean = ConfigBean.GetBean<t_itemBean, int>(daibi[0]);
            if (itemBean == null)
            {
                Logger.err("SH_DH_DuiHuanListItem:FillDaiBi:道具表没有此道具---" + daibi[0]);
            }
            else
            {
                if (string.IsNullOrEmpty(itemBean.t_icon))
                {
                    Logger.err("SH_DH_DuiHuanListItem:FillDaiBi:道具表没有对应图片---" + daibi[0]);
                }
                else
                {
                    UIGloader.SetUrl(m_DaiBi.m_touxiang, UIUtils.GetItemIcon(daibi[0]));
                }
                if (string.IsNullOrEmpty(itemBean.t_quality))
                {
                    Logger.err("SH_DH_DuiHuanListItem:FillDaiBi:道具表没有对应品质---" + daibi[0]);
                }
                else
                {
                    UIGloader.SetUrl(m_DaiBi.m_beijing,UIUtils.GetItemBorder(itemBean.t_id));
                }
                if (daibi[1] > BagService.Singleton.GetItemNum(daibi[0]))
                {
                    m_DaiBi.m_number.color = new UnityEngine.Color(255, 0, 0);
                    m_DuiHuanBtn.grayed = true;
                }
                m_DaiBi.m_number.text = daibi[1].ToString();
                m_DaiBi.m_type.visible = false;
            }
        }
      
    }
    private void FillInitDaoJu()
    {
        if (string.IsNullOrEmpty(exchangeBean.t_item))
        { }
        else
        {
            int[] item = GTools.splitStringToIntArray(exchangeBean.t_item);
            t_itemBean itemBean = ConfigBean.GetBean<t_itemBean, int>(item[0]);
            if (item == null)
            { Logger.err("SH_DH_DuiHuanListItem:FillDaiBi:道具表没有此道具---" + item[0]); }
            else
            {
                itemId = itemBean.t_id;
                if (string.IsNullOrEmpty(itemBean.t_icon))
                {
                    Logger.err("SH_DH_DuiHuanListItem:FillDaiBi:道具表没有对应图片---" + item[0]);
                }
                else
                {
                    UIGloader.SetUrl(m_DaoJu.m_touxiang,UIUtils.GetItemIcon(item[0]));
                }
                if (string.IsNullOrEmpty(itemBean.t_quality))
                {
                    Logger.err("SH_DH_DuiHuanListItem:FillDaiBi:道具表没有对应品质---" + item[0]);
                }
                else
                {
                    UIGloader.SetUrl(m_DaoJu.m_beijing, UIUtils.GetItemBorder(itemBean.t_id));
                }
                m_DaoJu.m_number.text = item[1].ToString();
                if (itemBean.t_type == 5)
                    m_DaoJu.m_type.visible = true;
                else
                    m_DaoJu.m_type.visible = false;
            }
        }
    }
    private void OnDuiHuanBtn()
    {
        if (string.IsNullOrEmpty(exchangeBean.t_need))
        {
        }
        else
        {
            int[] daibi = GTools.splitStringToIntArray(exchangeBean.t_need);
            t_itemBean itemBean = ConfigBean.GetBean<t_itemBean, int>(daibi[0]);
            if (itemBean == null)
            {
                Logger.err("SH_DH_DuiHuanListItem:OnDuiHuanBtn:道具表没有此道具---" + daibi[0]);
            }
            if (daibi[1] > BagService.Singleton.GetItemNum(daibi[0]))
            {
                TipWindow.Singleton.ShowTip("徽章不足无法兑换（非）");
            }
            else
            {
                StriveHegemongService.Singleton.OnReqExchange(itemId);
            }
        }
    }
    public override void Dispose()
    {
        exchangeBean = null;
        base.Dispose();
    }
}