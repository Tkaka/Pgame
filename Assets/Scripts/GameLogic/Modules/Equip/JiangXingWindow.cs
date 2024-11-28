using UI_Equip;
using Data.Beans;
using Message.Role;
using Message.Pet;

public class JiangXingWindow : BaseWindow
{
    UI_JiangXingWindow window;
    TwoParam<int, int> twoParam ;
    public override void OnOpen()
    {
        base.OnOpen();
        AddKeyEvent();
        InitView();
    }
    private void AddKeyEvent()
    {
        GED.ED.addListener(EventID.OnPetShuXingChanged,OnEquitChange);
    }
    //拿到装备id再加上货币数量即可以发送降星请求
    public override void InitView()
    {
        base.InitView();
        window = getUiWindow<UI_JiangXingWindow>();
        window.m_QuXiao.onClick.Add(CloseBtn);
        window.m_BeiJing.onClick.Add(CloseBtn);
        window.m_Close.onClick.Add(CloseBtn);
        if (Info.param == null)
        {
            Logger.err("未传入装备id");
            return;
        }
        t_globalBean global = ConfigBean.GetBean<t_globalBean,int>(106001);
        if (global == null)
        {
            Logger.err("全局表没有降星所需钻石数量参数");
        }
        RoleInfo roleInfo = RoleService.Singleton.GetRoleInfo();
        if (roleInfo == null)
        {
            Logger.err("降星界面未获取到玩家数据");
            return;
        }
        if (roleInfo.damond < global.t_int_param)
        {
            window.m_JiaGe.color = new UnityEngine.Color(255, 0, 0);
            window.m_QueDing.onClick.Add(OnJiangXingShiBai);
        }
        else
            window.m_QueDing.onClick.Add(OnJiangXing);
    }
    private void OnJiangXing()
    {
        twoParam = (TwoParam<int, int>)Info.param;
        Logger.log("装备降星");
        PetInfo pet = PetService.Singleton.GetPetInfo(twoParam.value1);
        if (pet == null)
        {
            Logger.err("降星界面未拿到宠物数据");
            return;
        }
        if (pet.equipInfo.equips == null || pet.equipInfo.equips.Count == 0)
        {
            Logger.err("降星界面未拿到宠物装备数据");
            return;
        }
        if (pet.equipInfo.equips[twoParam.value2].star > 0)
        {
            PetService.Singleton.ReqLowerStar(twoParam.value1, twoParam.value2);
            CloseBtn();
        }
        else
            TipWindow.Singleton.ShowTip("已是最低星级");
    }
    private void OnJiangXingShiBai()
    {
        ////跳转到充值
        //WinInfo info = new WinInfo();
        //TwoParam<int, LaiYuanType> twoParam = new TwoParam<int, LaiYuanType>();
        //twoParam.value1 = 2;
        //twoParam.value2 = LaiYuanType.Currency;
        //info.param = twoParam;
        //WinMgr.Singleton.Open<LaiYuanWindow>(info,UILayer.Popup);

    }
    private void OnEquitChange(GameEvent evt)
    {
        if (twoParam != null)
        {
            PetInfo pet = PetService.Singleton.GetPetInfo(twoParam.value1);
            if (pet.equipInfo.equips[twoParam.value2].star == 0)
            {
                CloseBtn();
            }
        }
    }
    public void CloseBtn()
    {
        GED.ED.removeListener(EventID.OnPetShuXingChanged, OnEquitChange);
        window = null;
        Close();
    }
}