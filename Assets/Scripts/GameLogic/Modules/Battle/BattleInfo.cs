using UI_Battle;
using UnityEngine;
using Data.Beans;

public class BattleInfo : UI_BattleInfo
{
    private BattlePlayerInfo playerInfo;

    private BattleEnemyInfo enemyInfo;


    public new static BattleInfo CreateInstance()
    {
        return UI_BattleInfo.CreateInstance() as BattleInfo;
    }

    public void OnOpen()
    {
        playerInfo = new BattlePlayerInfo(m_playerInfo);
        enemyInfo = new BattleEnemyInfo(m_enemyInfo);
        OnNewWave();
        m_goldNum.text = "0";
         
        UIUtils.SetTextVerGradient(m_countDown, new Color(0xff / 255f, 0xed / 255f, 0x10 / 255f), new Color(0xff / 255f, 0x7b / 255f, 0x06 / 255f));
        BattleCDCtrl.Singleton.Init(m_countDown, m_wuQiongImg);
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

    public void OnNewWave()
    {
        string param = (SpawnerHelper.Singleton.CurWave + 1) + "/" + SpawnerHelper.Singleton.WaveCount;
        t_languageBean bean = ConfigBean.GetBean<t_languageBean, int>(71901002);
        if (bean != null)
        {
            m_waveTxt.text = string.Format(bean.t_content, param);
        }
        enemyInfo.OnNewWave();
    }

    public void OnNewTurn()
    {
        m_turnTxt.text = FightManager.Singleton.TurnCount + "/" + FightManager.Singleton.MaxTurnNum;
    }

    public void OnClose()
    {
        if (playerInfo != null)
            playerInfo.OnClose();

        if (enemyInfo != null)
            enemyInfo.OnClose();
    }
}