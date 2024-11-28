using Data.Beans;
using UI_VIP;
public class RechargeCell : UI_RechargeCell
{
    public new static RechargeCell CreateInstance()
    {
        return UI_RechargeCell.CreateInstance() as RechargeCell;
    }

    public void RefreshView(int id)
    {
        t_rechargeBean bean = ConfigBean.GetBean<t_rechargeBean, int>(id);
        if (bean == null)
            return;

        m_objRecommond.visible = bean.t_recommend == 1;
        m_txtPrice.text = "￥" + (bean.t_price * 0.1f);
        UIGloader.SetUrl(m_imgIcon, bean.t_icon);
        int extraNum = 0;
        int[] extraInfo = GTools.splitStringToIntArray(bean.t_extra_give_item, '+');
        if (extraInfo != null && extraInfo.Length >= 2)
        {
            extraNum = extraInfo[1];
        }

        m_txtExtraDiamond.text = string.Format(bean.t_extra_give_des, extraNum);
        m_txtDiamond.text = string.Format("{0}钻石", bean.t_diamond);

        this.onClick.Clear();
        this.onClick.Add(() => {
            string strDes = string.Format("充值￥{0}获得钻石{1},确定？", bean.t_price * 0.1f, bean.t_diamond);
            AgainConfirmWindow.Singleton.TipOneButton("充值", strDes, () =>
            {
                RechargeService.Singleton.ReqRecharge(id);
            }, null, false);
        });
    }

}
