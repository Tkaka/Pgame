using Data.Beans;
using UI_VIP;
public class VipTitle : UI_VipTitle
{


    //是否是vip窗口
    public void RefreshView(bool isVip = true)
    {
        t_vipBean vipBean = ConfigBean.GetBean<t_vipBean, int>(VipService.Singleton.VipLevel);
        if (vipBean == null)
            return;

        this.m_vipLevel.text = vipBean.t_id + "";
        if (vipBean.t_exp == -1)
        {
            //满级
            this.m_objGroup.visible = false;
            this.m_txtExpNum.text = "Max";
            this.m_expBar.value = 100;
        }
        else
        {
            this.m_objGroup.visible = true;
            this.m_rechargeNum.text = (vipBean.t_exp - VipService.Singleton.VipExp) + "";
            this.m_txtNextLevel.text = (vipBean.t_id + 1) + "";
            this.m_txtExpNum.text = string.Format("{0}/{1}", VipService.Singleton.VipExp, vipBean.t_exp);
            this.m_expBar.value = (1.0f * VipService.Singleton.VipExp / vipBean.t_exp) * 100;
        }

        m_btnVip.visible = !isVip;
        m_btnRecharge.visible = isVip;
        m_btnRecharge.onClick.Add(_OnRechargeClick);
        m_btnVip.onClick.Add(_OnVipClick);
    }

    //充值点击
    private void _OnRechargeClick()
    {
        //GMService.Singleton.ReqGm("item", "-88 100");
        VipWndMgr.Singleton.OpenWnd(EVipWndType.Recharge);
    }


    private void _OnVipClick()
    {
        VipWndMgr.Singleton.OpenWnd(EVipWndType.Vip);
    }

}