using UI_Battle;
using UnityEngine;
using Data.Beans;
using FairyGUI;
public class BattleReplayWindow : BaseWindow, IBattleWindow
{
    private UI_BattleReplayWindow m_window;
    private BattleSet m_compBattleSet;

    private BattleInfo m_battleInfo;

    private BattlePetGroup m_battePetInfo;


    public override void OnOpen()
    {
        base.OnOpen();
        m_window = getUiWindow<UI_BattleReplayWindow>();
        m_compBattleSet = m_window.m_battleSet as BattleSet;
        if (m_compBattleSet != null)
        {
            m_compBattleSet.OnOpen();
            m_compBattleSet.SetAotoBtnTouchable(false);
        }

        m_battePetInfo = m_window.m_petInfo as BattlePetGroup;
        if (m_battePetInfo != null)
        {
            m_battePetInfo.OnOpen();
            m_battePetInfo.PetHeadBarSwipeToggle(false);
        }

        m_battleInfo = m_window.m_topInfo as BattleInfo;
        if (m_battleInfo != null)
            m_battleInfo.OnOpen();
        ComboCtrl.Singleton.Init(new GGraph());

    }



    public void BattleEnd(ActorCamp winCamp)
    {
        ReplayService.Singleton.ReplayResult();
 
    }


    public void ChangeFightState(string state, object param = null)
    {
        switch (state)
        {
            case FightState.AutoAttack:
                if (FightManager.Singleton.PlayerTurn)
                    m_battePetInfo.ResetHearBarStatus();
                break;
        }
    }

    public void ChangeTurn(ActorCamp camp)
    {
        if (m_battleInfo != null)
            m_battleInfo.OnNewTurn();
    }

    public void ChangeWave(bool startOrEnd)
    {
        if (m_battleInfo != null)
            m_battleInfo.ShowEnemyCampHpBar();
    }

    public void InitHpView()
    {
        m_battleInfo.ShowPlayerCampHpBar();
        m_battleInfo.ShowEnemyCampHpBar();
        m_battePetInfo.ShowPetHeadBar();
    }

    public void OnSpawnerActor(Actor actor)
    {
        m_battePetInfo.InitHeadBar(actor as ActorPet);
    }

    public override void ToogleVisible(bool flag)
    {

    }

    public void OnAutoBtn()
    {
        if (m_compBattleSet != null)
            m_compBattleSet.OnAutoBtn();
    }

    public void OnNewWave()
    {
        m_battleInfo.OnNewWave();
    }

    public void PlayComboAni()
    {

    }

    public void OnTrogglePetIcon(bool isShow)
    {
    }

    protected override void OnClose()
    {
        if (m_compBattleSet != null)
            m_compBattleSet.OnClose();
        if (m_battleInfo != null)
            m_battleInfo.OnClose();

        if (m_battePetInfo != null)
        {
            m_battePetInfo.OnClose();
        }

        ComboCtrl.Singleton.OnClose();
        base.OnClose();
    }
}