using DG.Tweening;
using UI_Battle;
using Data.Beans;

public class BattleEnemyInfo
{
    private UI_EnemyInfoPanel view;

    private SimpleInterval interval;

    private LNumber totalHp = 1;

    private LNumber fixRate = 0;

    private LNumber fixHp = 0;

    private bool isAniCmp = false;

    public BattleEnemyInfo(UI_EnemyInfoPanel view)
    {
        interval = new SimpleInterval();
        this.view = view;
        //fixRate = 2 / (LNumber)10;
        fixRate = LNumber.Create(0, 0650);
        InitView();
    }

    public void InitView()
    {
        view.m_hpBar.fillAmount = 0;
        //view.m_name.text = BattleService.Singleton.GetWaveName();
    }

    public void OnNewWave()
    {
        view.m_xianShouVal.text = FightService.Singleton.GetWavePrecedeVal() + "";
        if (FightManager.Singleton.IsReplay)
        {
            view.m_name.text = ReplayManager.Singleton.GetEnemyName();
        }
        else
        {
            view.m_name.text = FightService.Singleton.GetWaveName();
        }
         
    }

    public void ShowHpBar()
    {
        totalHp = FightManager.Singleton.GetTotalHp(ActorCamp.CampEnemy, true);
        FightManager.Singleton.CurWaveEnemyTotalHp = totalHp;
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
            nowHp = FightManager.Singleton.GetTotalHp(ActorCamp.CampEnemy, false) + fixHp;
            view.m_hpBar.fillAmount = (float)(nowHp / totalHp);
        }
    }

    public void OnClose()
    {
        if (interval != null)
            interval.Kill();
    }

}

