using UI_Battle;
using UnityEngine;
using Data.Beans;

public class BattleSet : UI_BattleSet
{
    public new static BattleSet CreateInstance()
    {
        return UI_BattleSet.CreateInstance() as BattleSet;
    }

    public void OnOpen()
    {
        m_autoBtn.onClick.Add(OnAutoBtn);
        m_speedBtn.onClick.Add(_OnSpeedBtn);
        m_pauseBtn.onClick.Add(_OnPauseBtn);
        m_speedBtn.text = "x" + FightManager.Singleton.GameSpeed;
        _ResetAutoBtn();
    }

    public void SetAotoBtnTouchable(bool touchable)
    {
        m_autoBtn.touchable = touchable;
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
        _ResetAutoBtn();
    }

    private void _ResetAutoBtn()
    {
        if (FightManager.Singleton.IsAuto)
        {
            m_autoBtn.m_aniImg.visible = true;
            m_autoBtn.m_ani.Play();
            m_autoBtn.selected = true;
        }
        else
        {
            m_autoBtn.m_aniImg.visible = false;
            m_autoBtn.m_ani.Stop();
            m_autoBtn.selected = false;
        }
    }

    private void _OnSpeedBtn()
    {
        float speed = FightManager.Singleton.ResetGameSpeed();
        GPlayerPrefs.SetFloat(GPlayerPrefs.BattleSpeedKey, speed);
        m_speedBtn.text = "x" + speed;
    }

    private void _OnPauseBtn()
    {
        BattleCDCtrl.Singleton.PauseToggle(true);
        Time.timeScale = 0.0f;
        WinMgr.Singleton.Open<PauseWindow>(null, UILayer.Popup);
    }


    public void OnClose()
    {
    }

}