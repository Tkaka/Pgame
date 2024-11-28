using System;
using System.Collections.Generic;
using UI_Battle;
using DG.Tweening;
using Data.Beans;

public class SkillPanel 
{

    private UI_SkillPanel skillPanel;

    private Tween tween;

    private float oldX;

    private int hurtNum;

    private int updateNum = 0;

    private bool sideInCmp = false;

    private int skillId;

    private long sideOutCoroId;

    private bool IsShowing()
    {
        return skillId > 0;
    }

    public void Init(UI_SkillPanel skillPanel)
    {
        this.skillPanel = skillPanel;
        this.skillPanel.visible = false;
        oldX = skillPanel.x;
    }

    private void ResetSkillPanel(bool flag)
    {
        skillPanel.m_skillIcon.visible = false;
        skillPanel.m_skillName.visible = flag;
        skillPanel.m_hurtTxt.visible = !flag;
        skillPanel.m_hurtNumber.visible = !flag;
        if (flag)
        {
            t_skillBean bean = ConfigBean.GetBean<t_skillBean, int>(skillId);
            if (bean != null)
            {
                skillPanel.m_skillName.text = bean.t_name;
            }
        }
        else
        {
            skillPanel.m_hurtNumber.text = updateNum.ToString();
        }
    }


    public void SideIn(int skillId)
    {
        if (IsShowing())
            return;
        this.skillId = skillId;
        hurtNum = 0;
        GED.ED.addListener(EventID.OnProduceHurt, OnProduceHurt);
        ResetSkillPanel(true);
        skillPanel.visible = true;
        skillPanel.x += 500;
        tween = skillPanel.TweenMoveX(oldX, 0.3f);
       CoroutineManager.Singleton.delayedCall(0.8f, OnSideInCmp);
    }

    private void OnSideInCmp()
    {
        ResetSkillPanel(false);
        sideInCmp = true;
        OnProduceHurt(null);
        sideOutCoroId = CoroutineManager.Singleton.delayedCall(3.0f, SideOut);
    }

    private void OnProduceHurt(GameEvent evt)
    {
        if (evt != null)
        {
            hurtNum += (int)evt.Data;
        }
        if (sideInCmp)
        {
            if (tween != null && tween.IsActive())
                tween.Kill();
            skillPanel.m_hurtNumber.text = updateNum.ToString();
            tween = DOTween.To(() => updateNum, x => updateNum = x, hurtNum, 0.5f);
            tween.OnUpdate(UpdateTween);
        }
        else
        {
            updateNum = hurtNum;
        }
    }

    private void UpdateTween()
    {
        skillPanel.m_hurtNumber.text = updateNum.ToString();
    }

    public void SideOut()
    {
        if (!IsShowing())
            return;
        if (sideOutCoroId > 0)
            CoroutineManager.Singleton.stopCoroutine(sideOutCoroId);
        sideInCmp = false;
        skillId = 0;
        updateNum = 0;
        GED.ED.removeListener(EventID.OnProduceHurt, OnProduceHurt);
        tween = skillPanel.TweenMoveX(oldX, 0.3f);
        tween.OnComplete(() =>
        {
            skillPanel.visible = false;
        });
    }

}