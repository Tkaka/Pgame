using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_Strength;
using Message.Pet;
using Data.Beans;

public class ShengPingSuccessWindow : BaseWindow {

    UI_ShengPingSuccessWindow _window;
    private PetInfo _oldPetInfo;
    private PetInfo _newPetInfo;

    public override void OnOpen()
    {
        base.OnOpen();
        _window = getUiWindow<UI_ShengPingSuccessWindow>();
        StrengthDataManager strengthData = Info.param as StrengthDataManager;
        _oldPetInfo = strengthData.OldPetInfo;
        _newPetInfo = strengthData.CurSelectPetInfo;

        InitView();
    }

    public override void InitView()
    {
        base.InitView();

        // 播放打开特效
        _window.m_anim.Play();
        (_window.m_leftStarAnim as UI_Common.UI_xingxing_ain_l).m_anim_L.Play();
        (_window.m_rightStarAnim as UI_Common.UI_xingxing_ain_r).m_anim_R.Play();

        t_petBean petBean = ConfigBean.GetBean<t_petBean, int>(_newPetInfo.petId);
        _window.m_mask.onClick.Add(OnCloseBtn);

        UIGloader.SetUrl(_window.m_oldBorderLoader, UIUtils.GetLoaderUrl(WinEnum.UI_Common, UIUtils.GetBorderByQuality(_oldPetInfo.basInfo.color)));
        string iconPath = UIUtils.GetIconPath(petBean);
        UIGloader.SetUrl(_window.m_oldIconLoader, iconPath);
        _window.m_oldNameLabel.text = UIUtils.GetPingJiePetName(_oldPetInfo.petId, _oldPetInfo.basInfo.color, _oldPetInfo.basInfo.star);
        _window.m_oldNameLabel.color = UIUtils.GetColorByQuality(_oldPetInfo.basInfo.color);
        _window.m_oldZhanDouLiLabel.text = _oldPetInfo.fightInfo.fightPower.ToString();

        UIGloader.SetUrl(_window.m_newBorderLoader, UIUtils.GetLoaderUrl(WinEnum.UI_Common, UIUtils.GetBorderByQuality(_newPetInfo.basInfo.color)));
        UIGloader.SetUrl(_window.m_newIconLoader, iconPath);
        _window.m_newNameLabel.text = UIUtils.GetPingJiePetName(_newPetInfo.petId, _newPetInfo.basInfo.color, _newPetInfo.basInfo.star);
        _window.m_newNameLabel.color = UIUtils.GetColorByQuality(_newPetInfo.basInfo.color);
        _window.m_newZhanDouLiLabel.text = PetService.Singleton.GetPetFightPower(_newPetInfo.petId).ToString();

        // TODO: 先手值的显示
        _window.m_oldXianShouZhiLabel.text = _oldPetInfo.priority + "";
        _window.m_newXianShouZhiLabel.text = _newPetInfo.priority + "";

        // 品质豆子
        PetQualityDou petQualityDou = _window.m_oldPetQualityDou as PetQualityDou;
        petQualityDou.InitView(_oldPetInfo.basInfo.color);

        petQualityDou = _window.m_newPetQualityDou as PetQualityDou;
        petQualityDou.InitView(_newPetInfo.basInfo.color);
    }

    protected override void OnCloseBtn()
    {
        if (_window.m_anim.playing)
        {
            _window.m_anim.Stop();
        }

        base.OnCloseBtn();
    }
}
