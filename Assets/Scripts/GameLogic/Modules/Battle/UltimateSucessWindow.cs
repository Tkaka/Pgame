using Data.Beans;
using Message.Bag;
using Message.Dungeon;
using Message.Fight;
using UI_Battle;
using UI_Level;
using Message.Challenge;
using System.Collections.Generic;
using System;

public class UltimateSucessWindow : BaseWindow
{
    private UI_UltimateSucessWindow m_window;
    private ResFightResultInfo m_msg;
    public override void OnOpen()
    {
        base.OnOpen();
        m_window = getUiWindow<UI_UltimateSucessWindow>();
        m_window.onClick.Add(Close);
        m_msg = Info.param as ResFightResultInfo;
        if (m_msg == null)
        {
            Close();
            return;
        }

        _ShowInfo();
    }

    private void _ShowInfo()
    {
        TrailResult trailResult = m_msg.result as TrailResult;
        if (trailResult == null)
            return;


        m_window.m_star1.visible = trailResult.result >= 1;
        m_window.m_star2.visible = trailResult.result >= 2;
        m_window.m_star3.visible = trailResult.result >= 3;


        m_window.m_txtStarNum.text = string.Format("{0}星加成", trailResult.result);
        int floor = m_msg.fightTypeParam / 1000;
        int difficulty = m_msg.fightTypeParam % 1000;
        int baseScore = 0;
        t_trialBean trralBean = ConfigBean.GetBean<t_trialBean, int>(floor);
        if (trralBean != null)
        {
            int[] arrBaseScore = GTools.splitStringToIntArray(trralBean.t_base_score, '+');
            if (arrBaseScore != null && arrBaseScore.Length > difficulty)
            {
                baseScore = arrBaseScore[difficulty];
            }
        }

        double rate = 1;
        t_globalBean gBean = ConfigBean.GetBean<t_globalBean, int>(1801006);
        if (gBean != null)
        {
            int[] arrRate = GTools.splitStringToIntArray(gBean.t_string_param, '+');
            if (arrRate != null && arrRate.Length >= trailResult.result)
                rate = arrRate[trailResult.result - 1] * 0.0001;
        }

        m_window.m_txtbaseScore.text = baseScore + "";
        m_window.m_txtRate.text = rate + "";
        m_window.m_txtTotalScore.text = Math.Floor(baseScore * rate) + "";
    }

    protected override void OnClose()
    {
        base.OnClose();
        BattleService.Singleton.QuitBattle();
    }
}