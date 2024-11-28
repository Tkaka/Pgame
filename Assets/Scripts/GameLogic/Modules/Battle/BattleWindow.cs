using UI_Battle;
using UnityEngine;
using Data.Beans;

public class BattleWindow : SingletonWindow<BattleWindow>, IBattleWindow
{

    private UI_BattleWindow window;

    private BattlePlayerInfo playerInfo;

    private BattleEnemyInfo enemyInfo;

    public EvaluateTip EvaluateTip { private set; get; }

    public override void OnOpen()
    {
        base.OnOpen();
        InitView();
    }

    public override void InitView()
    {
        base.InitView();
        window = getUiWindow<UI_BattleWindow>();
        window.m_toucher.name = "comboToucher";
        ComboCtrl.Singleton.Init(window.m_toucher);
        window.m_autoBtn.onClick.Add(OnAutoBtn);
        window.m_speedBtn.onClick.Add(OnSpeedBtn);
        window.m_pauseBtn.onClick.Add(OnPauseBtn);
        window.m_comboCommon.visible = false;
        playerInfo = new BattlePlayerInfo(window.m_playerInfo);
        enemyInfo = new BattleEnemyInfo(window.m_enemyInfo);
        EvaluateTip = new EvaluateTip(window.m_evaluateTip);
        window.m_goldNum.text = "0";
        //window.m_speedBtn.text = "x" + FightManager.Singleton.GameSpeed;
        _SetSelectSpeed();
        window.m_turnTxt.text = "1/" + FightManager.Singleton.MaxTurnNum;
        ResetAutoBtn();
        //"#FFED10", #FF7B06
        UIUtils.SetTextVerGradient(window.m_countDown, new Color(0xff/255f, 0xed/255f, 0x10/255f), new Color(0xff / 255f, 0x7b / 255f, 0x06 / 255f));
        BattleCDCtrl.Singleton.Init(window.m_countDown, window.m_wuQiongImg);

        OnNewWave();
    }

    public Vector3 GetGoldPos()
    {
        return window.m_goldNum.position;
    }

    public void SetGoldNum()
    {
        window.m_goldNum.text = BattleService.Singleton.GetedGold + "";
    }

    private void OnSpeedBtn()
    {
        float speed = FightManager.Singleton.ResetGameSpeed();
        GPlayerPrefs.SetFloat(GPlayerPrefs.BattleSpeedKey, speed);
        _SetSelectSpeed();
    }

    private void _SetSelectSpeed()
    {
        float speed = FightManager.Singleton.GameSpeed;
        window.m_speedBtn.text = "x" + speed;
        if (speed > 1 || speed < 1)
            window.m_speedBtn.selected = true;
        else
            window.m_speedBtn.selected = false;
    }

    private void OnPauseBtn()
    {
        BattleCDCtrl.Singleton.PauseToggle(true);
        Time.timeScale = 0.0f;
        OpenChild<PauseWindow>(WinInfo.Create(false, winName, false));
    }


    public void OnAutoBtn()
    {
        FightManager.Singleton.IsAuto = !FightManager.Singleton.IsAuto;
        if (FightManager.Singleton.IsAuto)
        {
            //GPlayerPrefs.SetInt(GPlayerPrefs.BattleAutoKey, 1);
            //Tip:切换到手动状态伤害更高哦
        }
        else
        {
            //71901001  下回合切换回手动战斗
            t_languageBean bean = ConfigBean.GetBean<t_languageBean, int>(71901001);
            if (bean != null)
                TipWindow.Singleton.ShowTip(bean.t_content);
        }
        
        if (FightManager.Singleton.PlayerTurn 
            && FightManager.Singleton.IsStateOf(FightState.MannulAttack))
        {
            //Logger.err("change to auto attackstate");
            FightManager.Singleton.ChangeState(FightState.AutoAttack);
        }
        ResetAutoBtn();
    }

    private void ResetAutoBtn()
    {
        if (FightManager.Singleton.IsAuto)
        {
            window.m_autoBtn.m_aniImg.visible = true;
            window.m_autoBtn.m_ani.Play();
            window.m_autoBtn.selected = true;
        }
        else
        {
            window.m_autoBtn.m_aniImg.visible = false;
            window.m_autoBtn.m_ani.Stop();
            window.m_autoBtn.selected = false;
        }
    }

    public void InitHpView()
    {
        ShowPetHeadBar();
        ShowPlayerCampHpBar();
        ShowEnemyCampHpBar();
    }

    public void ShowPetHeadBar()
    {
        for (int i = 0; i < window.m_petList.numChildren; i++)
        {
            PetHeadBar headBar = (PetHeadBar)window.m_petList.GetChildAt(i);
            if (headBar != null)
            {
                headBar.ShowHeadBar();
            }
        }
    }

    public void ShowPlayerCampHpBar()
    {
        if (playerInfo != null)
        {
            playerInfo.ShowHpBar();
        }
    }

    public void ShowEnemyCampHpBar()
    {
        if (enemyInfo != null)
        {
            enemyInfo.ShowHpBar();
        }
    }


    /*public void PetHeadBarTouchToggle(bool flag)
    {
        for (int i = 0; i < window.m_petList.numChildren; i++)
        {
            PetHeadBar headBar = (PetHeadBar)window.m_petList.GetChildAt(i);
            if (headBar != null)
            {
                headBar.TouchToggle(flag);
            }
        }
    }*/

    public void PetHeadBarSwipeToggle(bool flag)
    {
        for (int i = 0; i < window.m_petList.numChildren; i++)
        {
            PetHeadBar headBar = (PetHeadBar)window.m_petList.GetChildAt(i);
            if (headBar != null)
            {
                headBar.ToogleSwipe(flag);
            }
        }
    }

    public void InitHeadBar(ActorPet actor)
    {
        PetHeadBar headBar = PetHeadBar.CreateInstance();
        if (headBar != null)
        {
            window.m_petList.AddChildAt(headBar, 0);
            headBar.Init(actor);
        }
    }

    public void PlayComboAni()
    {
        int combo = FightManager.ComboCount;
        /*if (combo == 6)
        {
            Logger.log("ddddddddddddddd");
        }*/
        if (combo == 6)
        {
            window.m_combo6.visible = true;
            window.m_combo6.m_t0.Stop();
            window.m_combo6.m_t0.Play();
            string res = FightManager.ComboAdd.ToString("f2");
            res = res.Replace("0", "");
            window.m_combo6.m_comboTxt.text = "x" + res;
            window.m_comboCommon.visible = false;
        }
        else if (combo >= 2 && combo <= 5)
        {
            //window.m_comboCommon.m_numLoader.url = "ui://" + WinEnum.UI_Battle + "/combo_" + combo;
            UIGloader.SetUrl(window.m_comboCommon.m_numLoader, "ui://" + WinEnum.UI_Battle + "/combo_" + combo);
            window.m_comboCommon.visible = true;
            window.m_comboCommon.m_t0.Stop();
            window.m_comboCommon.m_t0.Play();
            string res = FightManager.ComboAdd.ToString("f2");
            res = res.Replace("0", "");
            window.m_combo6.m_comboTxt.text = "x" + res;
            window.m_comboCommon.m_comboTxt.text = "x" + res;
            window.m_combo6.visible = false;
        }
        else
        {
            Logger.err("未知的Combo类型" + combo);
        }
    }

    public void HideComboTip()
    {
        window.m_combo6.visible = false;
        window.m_comboCommon.visible = false;
    }


    public void OnNewWave()
    {
        //71901002  第{0} 波
        string param = (SpawnerHelper.Singleton.CurWave + 1) + "/" + SpawnerHelper.Singleton.WaveCount;
        t_languageBean bean = ConfigBean.GetBean<t_languageBean, int>(71901002);
        if (bean != null)
        {
            window.m_waveTxt.text = string.Format(bean.t_content, param);
        }
        enemyInfo.OnNewWave();
    }

    public void OnNewTurn()
    {
        window.m_turnTxt.text = FightManager.Singleton.TurnCount + "/" + FightManager.Singleton.MaxTurnNum;
    }

    protected override void OnClose()
    {
        ComboCtrl.Singleton.OnClose();
        BattleCDCtrl.Singleton.OnClose();
        if (playerInfo != null)
            playerInfo.OnClose();
        for (int i = 0; i < window.m_petList.numChildren; i++)
        {
            PetHeadBar headBar = (PetHeadBar)window.m_petList.GetChildAt(i);
            if (headBar != null)
            {
                headBar.OnClose();
            }
        }
        base.OnClose();
    }

    public void ChangeTurn(ActorCamp camp)
    {
        OnNewTurn();
    }

    public void ChangeWave(bool startOrEnd)
    {
        ShowEnemyCampHpBar();
    }

    public void ChangeFightState(string state, object param=null)
    {
        switch(state)
        {
            case FightState.AutoAttack:
            case FightState.MannulAttack:
                if(FightManager.Singleton.PlayerTurn)
                    ResetHearBarStatus();
                break;
            case FightState.MoveState:
                ResetHearBarStatus();
                break;
            case FightState.PrepareNextTurnState:
                if (param != null)
                {
                    if (FightManager.Singleton.PlayerTurn)
                    {
                        ChangeReason reason = (ChangeReason)param;
                        if (reason == ChangeReason.AllAttack || reason == ChangeReason.AllRemove)
                        {
                            long hurt = FightManager.Singleton.CurTurnHurt;
                            CoroutineManager.Singleton.delayedCall(FightManager.turnGapDelta, () =>
                            {
                                HideComboTip();
                                EvaluateTip.Show(hurt);
                            });
                        }
                    }
                }
                else
                {
                    Logger.err("PrepareNextTurnState:参数为空");
                }
                break;
            case FightState.MannulNormalAttack:
                //只要开始普通后，则不能再释放大招
                PetHeadBarSwipeToggle(false);
                break;
        }
    }

    public void ResetHearBarStatus()
    {
        for (int i = 0; i < window.m_petList.numChildren; i++)
        {
            PetHeadBar headBar = (PetHeadBar)window.m_petList.GetChildAt(i);
            if (headBar != null)
            {
                headBar.ResetStatus();
            }
        }
    }

    public void OnSpawnerActor(Actor actor)
    {
        if(actor.getActorType() == ActorType.Pet && actor.getCamp() == ActorCamp.CampFriend)
        {
            InitHeadBar(actor as ActorPet);
        }
    }

    public void OnTrogglePetIcon(bool isShow)
    {
        window.m_petGroup.visible = isShow;
    }

    public void BattleEnd(ActorCamp winCamp)
    {
        bool win = winCamp == ActorCamp.CampFriend;
        switch (FightService.Singleton.FightType)
        {
            case EFightType.Level:
                {
                    
                    if (win)
                    {
                        //播放特效
                        //UIEffectWrapper ewrapper = new UIEffectWrapper();
                        //ewrapper.SetEffect("eff_ui_tongguan_win");
                        AudioManager.Singleton.PlayEffect("snd_win");
                    }

                    CoroutineManager.Singleton.delayedCall(2, () =>
                    {
                        int star = BattleStatistics.Singleton.GetStar();
                        if (win)
                            BattleService.Singleton.ReqFightResult(BattleService.Singleton.MissionId, 1, star);
                        else
                            BattleService.Singleton.ReqFightResult(BattleService.Singleton.MissionId, 0, 0);
                    });
                }
                break;
            case EFightType.ExpDungeon:
            case EFightType.CoinDungeon:
                {
                    //long hurtBlood = (FightManager.Singleton.GetTotalHp(ActorCamp.CampEnemy, true) - FightManager.Singleton.GetTotalHp(ActorCamp.CampEnemy, false)).raw;
                    FightService.Singleton.ReqFightResult(1, FightManager.Singleton.GetTotalHp(ActorCamp.CampEnemy, false).raw);
                }
                break;
            case EFightType.HuanXiangDungeon:
            case EFightType.WomanFighterDungeon:
            case EFightType.ZhongJiShiLian:
            case EFightType.CloneDungeon:
            case EFightType.GuildBossDungeon:
                {
                    FightService.Singleton.ReqFightResult(win ? 1 : 0, (int)FightManager.Singleton.GetTotalHp(ActorCamp.CampEnemy, false).Ceiling);
                }
                break;
            default:
                break;
        }
 
    }
}