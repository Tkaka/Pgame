using UI_Common;
using Data.Beans;

public class RoleLevelUpWnd : BaseWindow
{
    private UI_RoleLevelUpWnd m_window;
    public override void OnOpen()
    {
        base.OnOpen();
        m_window = getUiWindow<UI_RoleLevelUpWnd>();
        m_window.onClick.Add(Close);
        // 播放动效
        m_window.m_animation.Play();
        m_window.m_leftStarAnim.m_anim_L.Play();
        m_window.m_rightStarAnim.m_anim_R.Play();
        InitView();
    }

    public override void InitView()
    {
        base.InitView();

        TwoParam<int, int> param = Info.param as TwoParam<int, int>;
        if (param == null)
            return;

        t_role_level_upBean preLevelBean = ConfigBean.GetBean<t_role_level_upBean, int>(param.value1);
        t_role_level_upBean nextLevelBean = ConfigBean.GetBean<t_role_level_upBean, int>(param.value2);
        if (preLevelBean == null || nextLevelBean == null)
        {
            Close();
            return;
        }
             

        
        m_window.m_txtLevelPre.text = param.value1 + "";
        m_window.m_txtLevelNext.text = param.value2 + "";

        //因为服务器是先加的体力 再发的等级改变
        m_window.m_txtTiLiPre.text = (RoleService.Singleton.GetRoleInfo().energy - _GetAddEnergy(param.value1, param.value2)) +  "";
        m_window.m_txtTiLiNext.text = RoleService.Singleton.GetRoleInfo().energy + "";
        m_window.m_txtTLLimitPre.text = preLevelBean.t_energy_max + "";
        m_window.m_txtTLLimitNext.text = nextLevelBean.t_energy_max + "";

    }

    private int _GetAddEnergy(int startLevel, int endLevel)
    {
        if (startLevel >= endLevel)
            return 0;

        int addValue = 0;
        for (int i = startLevel; i < endLevel; i++)
        {
            t_role_level_upBean levelBean = ConfigBean.GetBean<t_role_level_upBean, int>(i);
            if (levelBean == null)
                continue;

            addValue += levelBean.t_energy_recover;

        }

        return addValue;
    }

    protected override void OnClose()
    {
        base.OnClose();
    }
}