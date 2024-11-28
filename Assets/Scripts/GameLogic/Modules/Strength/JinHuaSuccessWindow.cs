using UI_Strength;
using UI_Common;
using Message.Pet;
using Data.Beans;
using UnityEngine;

public class JinHuaSuccessWindow : BaseWindow
{

    private UI_JinHuaSuccessWindow window;

    private PetInfo _oldPetInfo;

    private PetInfo _newPetInfo;

    private StrengthDataManager strengthData;

    public override void OnOpen()
    {
        base.OnOpen();
        StrengthWindow pWin = WinMgr.Singleton.GetWindow<StrengthWindow>(Info.param.ToString());
        if (pWin != null)
        {
            strengthData = pWin.strengthData;
            _oldPetInfo = strengthData.OldPetInfo;
            _newPetInfo = strengthData.CurSelectPetInfo;
            InitView();
        }
    }

    public override void InitView()
    {
        base.InitView();
        window = getUiWindow<UI_JinHuaSuccessWindow>();
        window.m_okBtn.onClick.Add(OnCloseBtn);
        // 播放动效
        window.m_anim.Play();
        (window.m_leftStarAnim as UI_Common.UI_xingxing_ain_l).m_anim_L.Play();
        (window.m_rightStarAnim as UI_Common.UI_xingxing_ain_r).m_anim_R.Play();
        RefreshView();
    }

    public override void RefreshView()
    {
        base.RefreshView();

        t_petBean petBean = ConfigBean.GetBean<t_petBean, int>(_newPetInfo.petId);
        UI_PetHead petHead = (UI_PetHead)window.m_petHead;
        UIGloader.SetUrl(petHead.m_iconLoader, UIUtils.GetIconPath(petBean, _newPetInfo.basInfo.star));
        petHead.m_petName.text = UIUtils.GetPetName(petBean, _newPetInfo.basInfo.star);
        UIGloader.SetUrl(petHead.m_borderLoader, UIUtils.GetLoaderUrl(WinEnum.UI_Common, UIUtils.GetBorderByQuality(_newPetInfo.basInfo.color)));
        StarList starList = new StarList((UI_StarList)petHead.m_starList);
        starList.SetStar(_newPetInfo.basInfo.star);
        // 品质豆子
        PetQualityDou petQualityDou = petHead.m_petQualityDou as PetQualityDou;
        petQualityDou.InitView(_newPetInfo.basInfo.color);

        window.m_oldAtk.text = _oldPetInfo.fightInfo.atk + "";
        window.m_oldDef.text = _oldPetInfo.fightInfo.def.ToString();
        window.m_oldHp.text = _oldPetInfo.fightInfo.hp.ToString();
        window.m_oldFightPower.text = _oldPetInfo.fightInfo.fightPower.ToString();
        window.m_oldXianShouZhi.text = _oldPetInfo.priority + "";

        window.m_newAtk.text = Mathf.FloorToInt(PetService.Singleton.GetPetPropertyValue(_oldPetInfo.petId, PropertyType.Atk)) + "";
        window.m_newDef.text = Mathf.FloorToInt(PetService.Singleton.GetPetPropertyValue(_oldPetInfo.petId, PropertyType.Def)) + "";
        window.m_newHp.text = Mathf.FloorToInt(PetService.Singleton.GetPetPropertyValue(_oldPetInfo.petId, PropertyType.Hp)) + "";
        window.m_newFightPower.text = PetService.Singleton.GetPetFightPower(_oldPetInfo.petId).ToString();
        window.m_newXianShouZhi.text = _newPetInfo.priority + "";

        //判断是否解锁新技能
        window.m_boardIcon.visible = false;
        PetInfo petInfo = PetService.Singleton.GetPetInfo(_newPetInfo.petId);
        if (petInfo != null)
        {
             int count = petInfo.skillInfo.skillInfos.Count;
            SkillInfo skillInfo = null;
            for (int i = 0; i < count; i++)
            {
                skillInfo = petInfo.skillInfo.skillInfos[i];
                if (skillInfo != null)
                {
                    t_skillBean skillBean = ConfigBean.GetBean<t_skillBean, int>(skillInfo.id);
                    if (IsUnlockSkill(skillBean))
                    {
                        ShowSkill(skillBean);
                        return;
                    }
                }
            }
        }


        if (!IsUnlock)
        {
            window.m_skillIcon.visible = false;
            window.m_skillDesc.visible = false;
            window.m_unlockSkillTxt.visible = false;
        }
    }

    private bool IsUnlock = false;
    private void ShowSkill(t_skillBean growBean)
    {
        IsUnlock = true;
        UIGloader.SetUrl(window.m_skillIcon,  growBean.t_icon);
        window.m_skillDesc.text = growBean.t_name;
        window.m_boardIcon.visible = true;
    }

    public bool IsUnlockSkill(t_skillBean growBean)
    {
        int oldStar = _oldPetInfo.basInfo.star;
        int newStar = _newPetInfo.basInfo.star;
        if (growBean != null)
        {
            if (growBean.t_type > oldStar && growBean.t_type <= newStar)
            {
                return true;
            }
        }
        return false;
    }

}
