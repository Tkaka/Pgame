using UI_Battle;
using DG.Tweening;
using UnityEngine;
using FairyGUI;
using Message.Pet;
using Data.Beans;

public class PetHeadBar : UI_PetHeadBar , IHeadBar
{

    private ActorPet actor;

    private bool isAniCmp = false;

    private Tween tween;

    private GoWrapper blueFullEft;

    private GoWrapper headEft;

    private SwipeGesture swipeGesture;

    private bool SwipeSuccess = false;

    public bool IsInited { private set; get; }

    public void Init(Actor actor)
    {
        this.actor = actor as ActorPet;
        if (this.actor == null)
        {
            Logger.err("PetHeadBar:Init:非法注册");
            return;
        }
        this.actor.headBar = this;
        int petId = actor.getTemplateId();
        int star = 0;
        int color = 0;

        //加载头像和边框 
        if (FightManager.Singleton.IsReplay)
        {
            ReplayManager.Singleton.GetPetInfo(petId, ActorCamp.CampFriend, out star, out color);
        }
        else
        {
            PetInfo petInfo = PetService.Singleton.GetPetInfo(actor.getTemplateId());
            if (petInfo != null)
            {
                star = petInfo.basInfo.star;
                color = petInfo.basInfo.color;
            }
        }
 
        t_petBean bean = ConfigBean.GetBean<t_petBean, int>(petId);
        //m_borderLoader.url = UIUtils.GetLoaderUrl(WinEnum.UI_Common, UIUtils.GetBorderByQuality(petInfo.basInfo.color));
        //m_iconLoader.url = UIUtils.GetIconPath(bean);
        string borderUrl = UIUtils.GetLoaderUrl(WinEnum.UI_Common, UIUtils.GetBorderByQuality(color));
        UIGloader.SetUrl(m_borderLoader, borderUrl);
        UIGloader.SetUrl(m_iconLoader, UIUtils.GetIconPath(bean));
        m_starList.RemoveChildren(0, -1, true);
        for (int i = 0; i < star; i++)
        {
            GImage img = UIPackage.CreateObject(WinEnum.UI_Common, "UI_TY_tubiao_xing_huangse_xiao").asImage;
            m_starList.AddChild(img);
        }

        // 加载宠物类型
        t_petBean petBean = ConfigBean.GetBean<t_petBean, int>(petId);
        if (petBean != null)
            UIGloader.SetUrl(m_typeLoader, UIUtils.GetLoaderUrl(WinEnum.UI_Common, UIUtils.GetPetTypeUrl(petBean.t_type)));

        //宠物品质
        PetQualityDou petQualityDou = m_petBean as PetQualityDou;
        petQualityDou.InitView(color);


        GameObject go = FightManager.R.LoadGo("eff_ui_battle_bluefire_01");
        blueFullEft = new GoWrapper(go);
        m_blueEftPos.SetNativeObject(blueFullEft);
        blueFullEft.gameObject.SetActive(false);

        swipeGesture = new SwipeGesture(m_touchHolder);
        swipeGesture.onMove.Add(OnSwipeMove);
        swipeGesture.onEnd.Add(OnSwipeEnd);
        swipeGesture.Enable(false);
        //m_touchHolder.onTouchEnd.Add(OnTouchEnd);
        m_touchHolder.onClick.Add(OnClick);
        //m_touchHolder.onTouchBegin.Add(OnClick);

        go = FightManager.R.LoadGo("eff_ui_skill_yindao_cast");
        headEft = new GoWrapper(go);
        m_headEftPos.SetNativeObject(headEft);
        headEft.gameObject.SetActive(false);

        IsInited = true;
        alpha = 0.5f;
        touchable = false;
        m_smallSkillAni.touchable = false;
        m_smallSkillAni.visible = false;
        m_txtSpuerSkill.visible = false;
        m_txtSkill.visible = false;
    }

    public new static PetHeadBar CreateInstance()
    {
        return (PetHeadBar)UI_PetHeadBar.CreateInstance();
    }

    public void ResetStatus()
    {
        if (actor != null && !actor.IsDestoryed)
        {
            //判断是否昏迷,麻痹,冰冻
            if (actor.getProperty(PropertyType.IsDizziness) > 0 ||
               actor.getProperty(PropertyType.IsNumbness) > 0 ||
                actor.getProperty(PropertyType.IsIce) > 0)
            {
                if (m_iconLoader != null)
                    m_iconLoader.grayed = true;
                ToogleSwipe(false);
                TouchToggle(false);
            }
            //判断是否沉默
            else if (actor.getProperty(PropertyType.IsSilence) > 0)
            {
                if (m_iconLoader != null)
                    m_iconLoader.grayed = false;
                ToogleSwipe(false);
                TouchToggle(true);
            }
            else
            {
                if (m_iconLoader != null)
                    m_iconLoader.grayed = false;
                //能够释放大招
                if (actor.IsCanMasterSkill())
                    swipeGesture.Enable(true);
                else
                    swipeGesture.Enable(false);
                TouchToggle(true);
            }
            if (FightManager.Singleton.IsStateOf(FightState.AutoAttack) || 
                FightManager.Singleton.IsStateOf(FightState.MoveState))
            {
                TouchToggle(false);
            }
        }
        else
        {
            touchable = false;
            alpha = 0.5f;
            if (m_iconLoader != null)
                m_iconLoader.grayed = true;
        }
    }

    public void TouchToggle(bool flag)
    {
        if (actor == null || actor.IsDestoryed)
        {
            touchable = false;
            alpha = 0.5f;
            return;
        }
        touchable = flag;
        if(flag)
            alpha = 1.0f;
        else
            alpha = 0.5f;
    }

    public void SkilAniToggle(bool flag)
    {
        if (actor == null || actor.IsDestoryed)
        {
            m_smallSkillAni.visible = false;
            return;
        }
        if (flag)
            m_smallSkillAni.visible = true;
        else
            m_smallSkillAni.visible = false;
    }

    private void OnSwipeMove(EventContext context)
    {
        if (actor == null)
            return;
        if (!HasAliver())
            return;
        SwipeGesture gesture = (SwipeGesture)context.sender;
        if (gesture.delta.y < -5)
        {
            BattleCDCtrl.Singleton.ToogleSelectActor(false);
            SwipeSuccess = true;
            //Logger.log("x:" + gesture.delta.x, "y:" + gesture.delta.y);
            headEft.gameObject.SetActive(true);
        }
    }

    private void OnSwipeEnd(EventContext context)
    {
        if (actor == null)
            return;
        if (SwipeSuccess)
        {
            BattleCDCtrl.Singleton.ToogleSelectActor(true);
            SwipeSuccess = false;
            Logger.log(actor.Name + " big skill");
            ToogleSwipe(false);
            SkilAniToggle(false);
            headEft.gameObject.SetActive(false);
            GED.ED.dispatchEvent(EventID.OnPetBigSkill, actor.getActorId());
        }
    }

    private bool HasAliver()
    {
        int aliveNum = FightManager.Singleton.Grid.AliveNum(ActorCamp.CampEnemy);
        if (aliveNum > 0)
            return true;
        return false;
    }

    private void OnClick()
    {
        if (actor == null)
            return;
        if (!HasAliver())
            return;
        if (!SwipeSuccess)
        {
            TouchToggle(false);
            SkilAniToggle(false);
            ComboCtrl.Singleton.OnHeadBarClick(actor.getActorId());
        }
        else
        {
            SwipeSuccess = false;
        }
    }

    public void ShowHeadBar()
    {
        if (!IsInited)
            return;
        //alpha = 1.0f;
        visible = true;
        m_hpBar.fillAmount = 0;
        m_mpBar.fillAmount = 0;
        tween = DOTween.To(() => m_hpBar.fillAmount, x => m_hpBar.fillAmount = x, 1.0f, 0.5f);
        tween.OnComplete(() => {
            isAniCmp = true;
            tween = null;
        });
    }

    public void OnDead()
    {
        m_hpBar.fillAmount = 0;
        m_mpBar.fillAmount = 0;
        alpha = 0.5f;
        actor = null;
        touchable = false;
        if(blueFullEft != null && blueFullEft.gameObject != null)
            blueFullEft.gameObject.SetActive(false);
        m_iconLoader.grayed = true;
        ToogleSwipe(false);
        TouchToggle(false);
    }

    public void Update()
    {
        if (actor != null)
        {
            if (actor.IsDestoryed)
            {
                OnDead();
            }
            else
            {
                updateHP();
            }
        }
    }

    public void OnClose()
    {
        if(blueFullEft != null)
            blueFullEft.Dispose();
    }

    private LNumber nowHp;
    private LNumber maxHp;
    private LNumber nowMp;
    private LNumber maxMp;
    private void updateHP()
    {
        if (isAniCmp)
        {
            nowHp = actor.ViewPropertyMgr.GetProperty(PropertyType.Hp);
            maxHp = actor.ViewPropertyMgr.GetBaseProperty(PropertyType.Hp);
            m_hpBar.fillAmount = (float)(nowHp / maxHp);

            nowMp = actor.ViewPropertyMgr.GetProperty(PropertyType.Mp);
            maxMp = actor.ViewPropertyMgr.GetBaseProperty(PropertyType.Mp);
            m_mpBar.fillAmount = (float)(nowMp / maxMp);

            if (nowMp >= maxMp)
            {
                m_txtSpuerSkill.visible = true;
                if (blueFullEft != null && blueFullEft.gameObject != null)
                    blueFullEft.gameObject.SetActive(true);
            }
            else
            {
                m_txtSpuerSkill.visible = false;
                if (blueFullEft != null && blueFullEft.gameObject != null)
                    blueFullEft.gameObject.SetActive(false);
            }
            m_smallSkillAni.visible = actor.WillUseSmallSkill;
            m_txtSkill.visible = actor.WillUseSmallSkill;
        }
    }

    public void ToggleVisible(bool flag)
    {
        visible = flag;
    }

    public void ToogleSwipe(bool flag)
    {
        swipeGesture.Enable(flag);
    }

    public void Destroy(float delay = 0)
    {
        Dispose();    
    }

}