using System.Collections.Generic;

public enum EVipWndType
{
    Vip,
    Recharge,
}

public class VipWndMgr
{

    private string m_vipWnd;
    private string m_rechargeWnd;
    private static VipWndMgr m_singleton;
    public static VipWndMgr Singleton
    {
        get
        {
            if (m_singleton == null)
            {
                m_singleton = new VipWndMgr();
            }
            return m_singleton;
        }
    }

    public void OpenWnd(EVipWndType type)
    {
        switch (type)
        {
            case EVipWndType.Recharge:
                {
                    if (!string.IsNullOrEmpty(m_vipWnd))
                    {
                        WinMgr.Singleton.Close(m_vipWnd);
                        m_vipWnd = "";
                    }

                   m_rechargeWnd = WinMgr.Singleton.Open<RechargeWnd>(null, UILayer.Popup);
                }
 
                break;
            case EVipWndType.Vip:
                {
                    if (!string.IsNullOrEmpty(m_rechargeWnd))
                    {
                        WinMgr.Singleton.Close(m_rechargeWnd);
                        m_rechargeWnd = "";
                    }

                    m_vipWnd = WinMgr.Singleton.Open<VipMainWnd>(null, UILayer.Popup);
                }
                break;
            default:
                break;
        }
    }

}