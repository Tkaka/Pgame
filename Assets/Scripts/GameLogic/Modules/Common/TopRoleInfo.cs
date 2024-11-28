using FairyGUI;
using Message.Role;
using UI_Common;
using Data.Beans;

public enum TopRoleInfoType
{
    Normal = 1,
    ZhongJiShiLian = 2,          // 终极试炼
    Guild = 3,                  // 公会
}

public class TopRoleInfo : BaseWindow
{
    private UI_TopRoleInfo uiRoleInfo;

    public static TopRoleInfo instance;
    private static BaseWindow hideWind;
    private TopRoleInfoType topType;
    public TopRoleInfoType TopType
    {
        get { return topType; }
        set
        {
            if (topType == value)
                return;

            topType = value;
            RefreshView();
        }
    }

    public static void Hide(BaseWindow window)
    {
        if(hideWind == null)
            GED.GuideED.addListener((int)GuideEventID.CloseWnd, onWindClose);
        hideWind = window;
        if (instance != null)
            instance.view.visible = false;
    }
    /// <summary>
    /// 播放打开动画
    /// </summary>
    public static void PlayOpenAnim()
    {
        if(instance != null)
        {
            UI_TopRoleInfo topRoleInfo = instance.view as UI_TopRoleInfo;
            if (topRoleInfo != null && !topRoleInfo.m_topRoleGroup.m_anim.playing)
            {
                topRoleInfo.m_topRoleGroup.m_anim.Play();
            }
        }
    }
    private static void onWindClose(GameEvent evt)
    {
        if(hideWind == null || hideWind.winName == (string)evt.Data)
        {
            Show();
        }
    }

    public static void Show()
    {
        hideWind = null;
        if (instance != null)
            instance.view.visible = true;
        GED.GuideED.removeListener((int)GuideEventID.CloseWnd, onWindClose);
    }

    public override void OnOpen()
    {       
        base.OnOpen();

        instance = this;
        uiRoleInfo = getUiWindow<UI_TopRoleInfo>();
        topType = TopRoleInfoType.Normal; 
        this.uiRoleInfo.m_topRoleGroup.m_goldToucher.onClick.Add(OnGoldClick);
        this.uiRoleInfo.m_topRoleGroup.m_damondToucher.onClick.Add(OnDamondClick);
        this.uiRoleInfo.m_topRoleGroup.m_energyToucher.onClick.Add(OnEnergyClick);
         
        RefreshView();
    }

    public override void InitView()
    {
        base.InitView();
    }

    public override void AddEventListener()
    {
        base.AddEventListener();
        GED.ED.addListener(EventID.ResRoleInfo, OnRoleInfoChange);
        GED.ED.addListener(EventID.CurrencyChange, _CurrencyChange);

    }

    public override void RemoveEventListener()
    {
        base.RemoveEventListener();
        GED.ED.removeListener(EventID.ResRoleInfo, OnRoleInfoChange);
        GED.ED.removeListener(EventID.CurrencyChange, _CurrencyChange);
    }
    
    //货币发生变化
    private void _CurrencyChange(GameEvent evt)
    {
        RoleInfo roleInfo = RoleService.Singleton.GetRoleInfo();
        RoleService.ECurrencyType type = (RoleService.ECurrencyType)evt.Data;
        switch (type)
        {
            case RoleService.ECurrencyType.GOLD:
                uiRoleInfo.m_topRoleGroup.m_goldTxt.text = roleInfo.gold + "";
                break;
            case RoleService.ECurrencyType.DIAMOND:
                uiRoleInfo.m_topRoleGroup.m_damondTxt.text = roleInfo.damond + "";
                break;
            case RoleService.ECurrencyType.ENERGY:
                setEnergyText(roleInfo.level, roleInfo.energy);
                break;
        }
    }
    
    public override void RefreshView()
    {
        RoleInfo roleInfo = RoleService.Singleton.GetRoleInfo();
        if (roleInfo != null)
        {
            uiRoleInfo.m_topRoleGroup.m_goldTxt.text = roleInfo.gold + "";
            uiRoleInfo.m_topRoleGroup.m_damondTxt.text = roleInfo.damond + "";

            if (topType == TopRoleInfoType.Normal)
            {
                uiRoleInfo.m_topRoleGroup.m_tiliGroup.visible = true;
                uiRoleInfo.m_topRoleGroup.m_commonGroup.visible = false;
            }
            else
            {
                uiRoleInfo.m_topRoleGroup.m_tiliGroup.visible = false;
                uiRoleInfo.m_topRoleGroup.m_commonGroup.visible = true;
            }

            switch (topType)
            {
                case TopRoleInfoType.Normal:
                    setEnergyText(roleInfo.level, roleInfo.energy);
                    break;
                case TopRoleInfoType.ZhongJiShiLian:
                    UIGloader.SetUrl(uiRoleInfo.m_topRoleGroup.m_commonIconLoader, UIUtils.GetBuyGoodsPriceIcon((int)ItemType.ShiLianCurrency));
                    uiRoleInfo.m_topRoleGroup.m_commonText.text = roleInfo.trailCurrency + "";
                    break;
                case TopRoleInfoType.Guild:
                    UIGloader.SetUrl(uiRoleInfo.m_topRoleGroup.m_commonIconLoader, UIUtils.GetBuyGoodsPriceIcon((int)ItemType.ShiLianCurrency));
                    uiRoleInfo.m_topRoleGroup.m_commonText.text = roleInfo.clubCurrency + "";
                    break;
                default:
                    break;
            }
        }
    }

    private void setEnergyText(int level, int energy)
    {
        var bean = ConfigBean.GetBean<Data.Beans.t_role_level_upBean, int>(level);
        int max = bean != null ? bean.t_energy_max : 100;
        int lanId = 0;
        if (energy == 0)
            lanId = 30304; //红色
        else if(energy >= max)
            lanId = 30302; //绿色
        else 
            lanId = 30303; //白色
        var gb = ConfigBean.GetBean<Data.Beans.t_globalBean, int>(lanId);
        string tip = gb != null ? gb.t_string_param : "{0}/{1}";
        uiRoleInfo.m_topRoleGroup.m_energyTxt.text = string.Format(tip, energy, max);
    }

    private void OnRoleInfoChange(GameEvent evt)
    {
        RefreshView();
    }

    public void OnWindowClose()
    {
        GED.ED.removeListener(EventID.ResRoleInfo, OnRoleInfoChange);
    }

    private void OnGoldClick()
    {
        UnityEngine.Debug.Log("----on gold click----");
        Logger.log("----on gold click----");
    }

    private void OnDamondClick()
    {
        Logger.log("----on damond click----");
    }

    private void OnEnergyClick()
    {
        //Logger.log("----on Energy click----");
        RoleService.Singleton.BuyEnergy();
    }

}
