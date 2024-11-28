using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_Strength;
using Data.Beans;
using FairyGUI;
using DG.Tweening;

public class StrengthShengJi : TabPage{

    private UI_shengJi shengJiUI;

    private StrengthWindow parentWindow;
    private int _exp;
    private long _coroutineID = -1;
    private Tweener _tweener;
    private int tempLv;
    private GameObject lvUpEffGO;      // 升级粒子特效
    private ResPack resPack;
    private bool isInCoroutine = false;
    private SimpleInterval simpleInterval;
    private float effectTime;          //特效的时间
    public StrengthShengJi(UI_shengJi shengJi, StrengthWindow window)
    {
        this.shengJiUI = shengJi;
        this.parentWindow = window;

        GED.ED.addListener(EventID.ResBagUpdate, OnBagUpdate);
        InitExpList();
    }

    public StrengthDataManager StrengthData
    {
        get { return parentWindow.strengthData; }
    }
    /// <summary>
    /// 经验条是否满了
    /// </summary>
    public bool ExpBarIsFull
    {
        get
        {
            return shengJiUI.m_expJinDuTiao.max == shengJiUI.m_expJinDuTiao.value;
        }
    }

    public int GetCurExp()
    {
        return (int)shengJiUI.m_expJinDuTiao.value;
    }
    private void InitExpList()
    {
        var num = shengJiUI.m_expPropList.numChildren;
        ExpPropItem expPropItem;
        for (int i = 0; i < num; i++)
        {
            expPropItem = shengJiUI.m_expPropList.GetChildAt(i) as ExpPropItem;
            expPropItem.PropID = parentWindow.strengthData.ExpPropIDs[i];
            expPropItem.index = i;
            expPropItem.Init(this);
        }
    }
    public void RefreshExpBar(int exp)
    {
        _exp += exp;
        if (isInCoroutine ==false)
        {
            isInCoroutine = true;
            tempLv = StrengthData.CurSelectPetInfo.basInfo.level;
            _coroutineID = CoroutineManager.Singleton.startCoroutine(RefreshExpBar());
        }
    }

    private void OnBagUpdate(GameEvent evt)
    {
        StrengthData.SetExpPropDict();
        RefreshExpPropList();
    }

    IEnumerator RefreshExpBar()
    {
        Message.Pet.PetInfo petInfo = parentWindow.strengthData.CurSelectPetInfo;
        int newValue = (int)shengJiUI.m_expJinDuTiao.value + _exp;
        float interval = 0.1f;
        while (shengJiUI.m_expJinDuTiao.max <= newValue)
        {
            if (_tweener != null && _tweener.IsActive())
                _tweener.Kill();

            _exp -= (int)(shengJiUI.m_expJinDuTiao.max - shengJiUI.m_expJinDuTiao.value);
            _tweener = shengJiUI.m_expJinDuTiao.TweenValue(shengJiUI.m_expJinDuTiao.max, interval);
            Message.Role.RoleInfo roleInfo = RoleService.Singleton.GetRoleInfo();
            if (roleInfo.level <= tempLv)
            {
                shengJiUI.m_expJinDuTiao.value = shengJiUI.m_expJinDuTiao.max;
                yield return new WaitForSeconds(interval);
                RefreshProgressTip();
                CoroutineManager.Singleton.stopCoroutine(_coroutineID);
                isInCoroutine = false;
                yield return null;
            }
            else
            {
                tempLv++;

                yield return new WaitForSeconds(interval);
                shengJiUI.m_expJinDuTiao.max = PetService.Singleton.GetCurLevelExp(StrengthData.CurSelectPetInfo.petId, tempLv);
                shengJiUI.m_expJinDuTiao.value = 0;
                newValue = (int)shengJiUI.m_expJinDuTiao.value + _exp;
            }
            ShowLevelUpEffect();
            RefreshProgressTip();
        }
        _exp = 0;
        isInCoroutine = false;
        if (_tweener != null && _tweener.IsActive())
            _tweener.Kill();
        _tweener = shengJiUI.m_expJinDuTiao.TweenValue(newValue, interval);
        RefreshProgressTip();
    }
    public void LongPressRefreshBar(int exp, int useNum)
    {
        shengJiUI.m_useNumLabel.visible = true;
        shengJiUI.m_useNumLabel.text = string.Format("X{0}", useNum);

        Message.Pet.PetInfo petInfo = parentWindow.strengthData.CurSelectPetInfo;
        int newValue = (int)shengJiUI.m_expJinDuTiao.value + exp;

        while (shengJiUI.m_expJinDuTiao.max <= newValue)
        {
            Message.Role.RoleInfo roleInfo = RoleService.Singleton.GetRoleInfo();
            if (roleInfo.level <= petInfo.basInfo.level)
            {
                shengJiUI.m_expJinDuTiao.value = shengJiUI.m_expJinDuTiao.max;
                return;
            }
            newValue -= PetService.Singleton.GetCurLevelExp(StrengthData.CurSelectPetInfo.petId, StrengthData.CurSelectPetInfo.basInfo.level);
            petInfo.basInfo.level++;
            shengJiUI.m_expJinDuTiao.max = PetService.Singleton.GetCurLevelExp(StrengthData.CurSelectPetInfo.petId, StrengthData.CurSelectPetInfo.basInfo.level);
            RefreshProgressTip();
            ShowLevelUpEffect();
        }
        shengJiUI.m_expJinDuTiao.value = newValue;
        RefreshLvView();
        RefreshProgressTip();
    }

    private void RefreshExpPropList()
    {
        var num = shengJiUI.m_expPropList.numChildren;
        ExpPropItem expPropItem;
        for (int i = 0; i < num; i++)
        {
            expPropItem = shengJiUI.m_expPropList.GetChildAt(i) as ExpPropItem;
            expPropItem.PropID = parentWindow.strengthData.ExpPropIDs[i];
            expPropItem.RefreshView();
        }
    }

    private void RefreshProgressTip()
    {
        Message.Role.RoleInfo roleInfo = RoleService.Singleton.GetRoleInfo();
        if (roleInfo.level == StrengthData.CurSelectPetInfo.basInfo.level && ExpBarIsFull)
        {
            shengJiUI.m_fullLevelTip.visible = true;
            shengJiUI.m_progressTip.visible = false;
        }
        else
        {
            shengJiUI.m_fullLevelTip.visible = false;
            shengJiUI.m_progressTip.visible = true;
            shengJiUI.m_progressTip.text = string.Format("{0}/{1}", shengJiUI.m_expJinDuTiao.value, shengJiUI.m_expJinDuTiao.max);
        }
        
    }

    public override void OnHide()
    {
        CoroutineManager.Singleton.stopCoroutine(_coroutineID);
        if (_tweener != null && _tweener.IsActive())
            _tweener.Kill();
        _tweener = null;
    }

    public override void OnShow()
    {
        RefreshView();
    }

    public override void OnClose()
    {
        shengJiUI = null;
        GED.ED.removeListener(EventID.ResBagUpdate, OnBagUpdate);
        parentWindow = null;

        if (_tweener != null && _tweener.IsActive())
        {
            _tweener.Kill();
        }
        _tweener = null;

        if (simpleInterval != null && simpleInterval.IsRunning)
            simpleInterval.Kill();
        simpleInterval = null;

        if (lvUpEffGO != null)
            GameObject.DestroyImmediate(lvUpEffGO);
        lvUpEffGO = null;
    }

    public override void RefreshView(bool isShow = false)
    {
        var petInfo = parentWindow.strengthData.CurSelectPetInfo;
        shengJiUI.m_expJinDuTiao.max = PetService.Singleton.GetCurLevelExp(StrengthData.CurSelectPetInfo.petId, StrengthData.CurSelectPetInfo.basInfo.level);
        shengJiUI.m_expJinDuTiao.value = parentWindow.strengthData.GetCurExp();
        RefreshProgressTip();
        RefreshLvView();
        RefreshExpPropList();

        if (isShow)
        {
            shengJiUI.m_useNumLabel.visible = false;
        }
        else
        {
            shengJiUI.m_useNumLabel.visible = false;
        }
    }

    private void ShowLevelUpEffect()
    {
        if (shengJiUI.m_levelUpAnim.playing)
            shengJiUI.m_levelUpAnim.Stop();

        shengJiUI.m_levelUpAnim.Play();

        // 显示升级成功的粒子特效
        if (resPack == null)
        {
            resPack = new ResPack(this);
            lvUpEffGO = resPack.LoadGo("eff_level_up");
            if(lvUpEffGO != null)
            {
                GoWrapper wrapper = new GoWrapper(lvUpEffGO);
                shengJiUI.m_lvEffecPos.SetNativeObject(wrapper);
                lvUpEffGO.transform.localPosition = new Vector3(-97,-50,340);
            }
        }
        
        if(lvUpEffGO != null)
        {
            lvUpEffGO.SetActive(true);
            ParticleSystem ps = lvUpEffGO.GetComponentInChildren<ParticleSystem>();
            if (ps != null && ps.isPlaying)
                ps.Stop(true);
            ps.Play(true);

            if (simpleInterval == null)
                simpleInterval = new SimpleInterval();
            simpleInterval.Kill();
            simpleInterval.DoActionWithTimes(ps.main.duration, OnEffectEnd);
        }
    }

    private void OnEffectEnd()
    {
        lvUpEffGO.SetActive(false);
    }

    private void RefreshLvView()
    {
        Message.Pet.PetInfo petInfo = parentWindow.strengthData.CurSelectPetInfo;
        shengJiUI.m_levelLabel.text = PetService.Singleton.GetPetLevel(petInfo.petId).ToString();
    }
}
