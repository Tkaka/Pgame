
using DG.Tweening;
using Message.Role;
using UI_Battle;

public class BattlePlayerInfo
{

    private UI_PlayerInfoPanel view;

    private SimpleInterval interval;

    private LNumber totalHp = 1;

    private bool isAniCmp = false;

    private LNumber fixRate = 0;

    private LNumber fixHp = 0;

    public BattlePlayerInfo(UI_PlayerInfoPanel view)
    {
        interval = new SimpleInterval();
        this.view = view;
        fixRate = LNumber.Create(0, 0650);
        InitView();
    }

    public void InitView()
    {
        view.m_hpBar.fillAmount = 0;
 

        int precedeVal = 0;
        if (FightManager.Singleton.IsReplay)
        {
            precedeVal = ReplayManager.Singleton.thisFirstVal;
            view.m_name.text = ReplayManager.Singleton.GetThisFirstName();
        }
        else
        {
            precedeVal = RoleService.Singleton.GetPrecedeVal();
            ResRoleInfo resRoleInfo = RoleService.Singleton.RoleInfo;
            if (resRoleInfo != null && resRoleInfo.roleInfo != null)
            {
                view.m_name.text = resRoleInfo.roleInfo.roleName;
            }
        }
        view.m_xianShouVal.text = precedeVal + "";
    }

    public void ShowHpBar()
    {
        totalHp = FightManager.Singleton.GetTotalHp(ActorCamp.CampFriend, true);
        fixHp = totalHp * fixRate;
        totalHp += fixHp;
        view.m_hpBar.fillAmount = 0;
        Tween tween = DOTween.To(() => view.m_hpBar.fillAmount, x => view.m_hpBar.fillAmount = x, 1.0f, 0.5f);
        tween.OnComplete(() => {
            isAniCmp = true;
            tween = null;
        });
        interval.DoUpdate(UpdateHp);
    }

    private LNumber nowHp;
    public void UpdateHp()
    {
        if (isAniCmp)
        {
            nowHp = FightManager.Singleton.GetTotalHp(ActorCamp.CampFriend, false) + fixHp;
            view.m_hpBar.fillAmount = (float)(nowHp / totalHp);
        }
    }

    public void OnClose()
    {
        if(interval != null)
            interval.Kill();
    }

}

