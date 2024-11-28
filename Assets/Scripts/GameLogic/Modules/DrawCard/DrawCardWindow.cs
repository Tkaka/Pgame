using System.Collections.Generic;
using UnityEngine;
using UI_DrawCard;
using Message.DrawCard;
using FairyGUI;
using Data.Beans;

public class DrawCardWindow : BaseWindow
{
    private UI_DrawCardWindow window;
    private DoActionInterval doAction;
    private GameObject changjing;
    private Animator animator;
    //private Animation animation;
    private int type;//点击的抽奖类型
    private float time;//协程倒计时时间
    private int zhuangtai;//状态为1时为打开召唤窗口，状态为2时为关闭召唤窗口

    public override void OnOpen()
    {
        window = getUiWindow<UI_DrawCardWindow>();
        OnScence();
        AddKeyEvent();
        PlayOpenEffect();
    }

    public override void PlayOpenEffect()
    {
        base.PlayOpenEffect();

        ((UI_Common.UI_commonTop)window.m_taitou).m_anim.Play();
        //window.m_taitou.visible = true;
        //Logger.err("OnShow");
    }

    public override void AddEventListener()
    {
        GED.ED.addListener(EventID.OnDrawCardCloseZhaoHuan,OnGuanBiZhaoHuan);
        base.AddEventListener();
    }
    public override void RemoveEventListener()
    {
        GED.ED.removeListener(EventID.OnDrawCardCloseZhaoHuan, OnGuanBiZhaoHuan);
        base.RemoveEventListener();
    }
    /// <summary>
    /// 事件
    /// </summary>
    private void AddKeyEvent()
    {
        AddEventListener();
        ((UI_Common.UI_commonTop)window.m_taitou).m_closeBtn.onClick.Add(ClostBtn);
        ((UI_Common.UI_commonTop)window.m_taitou).m_title.text = "宝可梦召唤";
        window.m_JinBiZhaoHuan.onClick.Add(OnJinBiZhaoHuan);
        window.m_ZuanShiZhaoHuan.onClick.Add(OnZuanShiZhaoHuan);
        window.m_jinbuchoujiangBtn.onClick.Add(OnJinBiZhaoHuan);
        window.m_zuanshichoujiangBtn.onClick.Add(OnZuanShiZhaoHuan);
    }
    private void OnScence()
    {
        changjing = this.LoadGo("eff_ui_zhaohuan");
        changjing.transform.transform.localPosition = new Vector3(100,0,0);
        if (changjing != null)
        {
            animator = changjing.GetComponent<Animator>();
        }
    }
    private void ClostBtn()
    {
        RemoveEventListener();
        if(changjing != null)
            GameObject.DestroyObject(changjing);
        window = null;
        Close();
    }
    private void OnJinBi()
    {
        type = 1;
        //OpenChild<ZhaoHuanWindow>(WinInfo.Create(true, winName, true, 1));
        WinMgr.Singleton.Open<ZhaoHuanWindow>(WinInfo.Create(true, null, true, 1), UILayer.Popup);
    }
    private void OnZuanShi()
    {
        type = 2;
        //OpenChild<ZhaoHuanWindow>(WinInfo.Create(true, winName, true,2));
        WinMgr.Singleton.Open<ZhaoHuanWindow>(WinInfo.Create(true, null, true, 2), UILayer.Popup);
    }
    /// <summary>
    /// 钻石召唤
    /// </summary>
    private void OnZuanShiZhaoHuan()
    {
        if (animator != null)
        {
            animator.enabled = false;
            animator.enabled = true;
            if (doAction == null)
            {
                animator.speed = 1;
                //animator.playbackTime = 0;
                //animator.CrossFade("eff_ani_ui_zhaohuan_right", 0.1f, -1);
                 animator.Play("eff_ani_ui_zhaohuan_right");
                time = 1;
                type = 2;
                doAction = new DoActionInterval();
                doAction.doAction(1, OnDaoJiShi, null, true);
                TopRoleInfo.Hide(this);
                window.m_taitou.visible = false;
                zhuangtai = 1;
                window.m_all.visible = false;
            }
        }
    }
    /// <summary>
    ///金币召唤 
    /// </summary>
    private void OnJinBiZhaoHuan()
    {
        if (animator != null)
        {
            animator.enabled = false;
            animator.enabled = true;
            if (doAction == null)
            {
                //animator.enabled = true;
                animator.speed = 1;
                //animator.playbackTime = 0;
                //animator.CrossFade("eff_ani_ui_zhaohuan_left", 0.1f, -1);
                animator.Play("eff_ani_ui_zhaohuan_left");
                time = 1;
                type = 1;
                doAction = new DoActionInterval();
                doAction.doAction(1, OnDaoJiShi, null, true);
                TopRoleInfo.Hide(this);
                window.m_taitou.visible = false;
                zhuangtai = 1;
                window.m_all.visible = false;
            }
        }
    }
    private void OnGuanBiZhaoHuan(GameEvent evt)
    {
        if (doAction != null)
        {
            doAction.kill();
            doAction = null;
        }
        time = 1;
        if (animator != null)
        {
            if (type == 1)
            {
                animator.Play("eff_ani_ui_zhaohuan_left_back");
            }
            else if(type == 2)
            {
                animator.Play("eff_ani_ui_zhaohuan_right_back");
            }
        }
        zhuangtai = 2;
        doAction = new DoActionInterval();
        TopRoleInfo.Hide(this);
        doAction.doAction(1,OnDaoJiShi,null,true);
    }
    private void OnDaoJiShi(object obj)
    {
        time--;
        if (time < 0)
        {
            if (doAction != null)
            {
                doAction.kill();
                doAction = null;
            }
            if (zhuangtai == 1)
            {
                if (type == 1)
                {
                    OnJinBi();
                }
                else if (type == 2)
                {
                    OnZuanShi();
                }
            }
            else if (zhuangtai == 2)
            {
                window.m_all.visible = true;
                window.m_taitou.visible = true;
                TopRoleInfo.Show();
                PlayOpenEffect();
            }
        }
    }
}

