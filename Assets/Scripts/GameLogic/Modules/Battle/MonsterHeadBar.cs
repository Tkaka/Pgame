using FairyGUI;
using UI_Battle;
using UnityEngine;
using DG.Tweening;

public class MonsterHeadBar : UI_MonsterHeadBar, IHeadBar
{

    private Actor actor;

    private bool isAniCmp = true;

    private Tween tween;

    private GImage curBar;

    public void Init(Actor actor)
    {
        this.actor = actor;
        if (actor.isActorType(ActorType.Pet))
        {
            m_hpBar.visible = false;
            m_hpBarGreen.visible = true;
            curBar = m_hpBarGreen;
        }
        else
        {
            m_hpBar.visible = true;
            m_hpBarGreen.visible = false;
            curBar = m_hpBar;
        }
        WinMgr.Singleton.Hud1Layer.AddChild(this);
        visible = false;
        touchable = false;
    }

    public static new MonsterHeadBar CreateInstance()
    {
        return (MonsterHeadBar)UIPackage.CreateObject("UI_Battle", "MonsterHeadBar");
    }

    public void Destroy(float delay=0)
    {
        if (delay > 0)
        {
            tween = TweenFade(0, delay).OnComplete(() =>
            {
                Dispose();
            });
        }
        else
        {
            Dispose();
        }
    }

    public void ShowHeadBar()
    {
        /*visible = true;
        isAniCmp = false;
        m_hpBar.fillAmount = 0;
        tween = DOTween.To(() => m_hpBar.fillAmount, x => m_hpBar.fillAmount = x, 1.0f, 1.0f);
        tween.OnComplete(() => {
            isAniCmp = true;
            tween = null;
        });*/
    }

    public void ToggleVisible(bool flag)
    {
        visible = flag;
    }

    public void Update()
    {
        //修正血量 位置
        fixPos();
        updateHP();
    }

    protected void fixPos()
    {
        if (actor.IsDestoryed)
            return;
        Vector3 ownerPos = actor.monoBehavior.headBarPos;
        ownerPos.y += 1;
        Camera cam = Camera.main;
        if (cam != null)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(ownerPos);
            screenPos.y = Screen.height - screenPos.y; //convert to Stage coordinates system

            Vector3 pt = GRoot.inst.GlobalToLocal(screenPos);
            this.SetXY(Mathf.RoundToInt(pt.x - actualWidth * 0.5f), Mathf.RoundToInt(pt.y - actualHeight * 0.5f));
        }
    }

    private LNumber nowHp;
    private LNumber maxHp;
    private void updateHP()
    {
        if (isAniCmp)
        {
            bool show = false;
            if (!actor.CdMgr.isCoolDown("hpbar"))
                show = true;
            else if (BattleCDCtrl.Singleton.IsShowing && actor.IsSelected)
                show = true;
            if (show)
            {
                visible = true;
                nowHp = actor.ViewPropertyMgr.GetProperty(PropertyType.Hp);
                maxHp = actor.ViewPropertyMgr.GetBaseProperty(PropertyType.Hp);
                curBar.fillAmount = (float)(nowHp / maxHp);
            }
            else
            {
                visible = false;
            }
        }
    }

    public override void Dispose()
    {
        actor = null;
        if (tween != null && tween.IsActive())
        {
            tween.Kill();
            tween = null;
        }
        base.Dispose();
    }

    public void TouchToggle(bool flag)
    {
        touchable = flag;
    }

    public void ToogleSwipe(bool flag)
    {
        
    }

    public void OnDead()
    {

    }

    public void ResetStatus()
    {
        
    }

}
