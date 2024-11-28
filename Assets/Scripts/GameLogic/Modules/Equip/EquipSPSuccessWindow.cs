using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_Equip;
using UI_Common;
using Data.Beans;
using Message.Pet;

public class EquipSPSuccessWindow : BaseWindow {

    UI_EquipSPSuccessWindow window;
    EquipDataManager equipData;

    public override void OnOpen()
    {
        base.OnOpen();

        window = getUiWindow<UI_EquipSPSuccessWindow>();
        equipData = Info.param as EquipDataManager;

        InitView();
    }

    public override void InitView()
    {
        base.InitView();

        // 播放动效
        (window.m_leftStarAnim as UI_Common.UI_xingxing_ain_l).m_anim_L.Play();
        (window.m_rightStarAnim as UI_Common.UI_xingxing_ain_r).m_anim_R.Play();
        window.m_mask.onClick.Add(OnCloseBtn);
        SetOldEquipData();
        SetNewEquipData();
        SetData();
    }

    private void SetOldEquipData()
    {
        PetEquip petEquip = equipData.GetCurSelectPetEquip();

        UIGloader.SetUrl(window.m_oldBoarderLoader, UIUtils.GetLoaderUrl(WinEnum.UI_Common, UIUtils.GetBorderByQuality(petEquip.color - 1)));
        window.m_oldNameLabel.color = UIUtils.GetColorByQuality(petEquip.color - 1);
        window.m_oldNameLabel.text = UIUtils.GetPingJieEquipName(equipData.CurSelectPetID, (int)equipData.CurSelectEquipPos, petEquip.star, petEquip.color - 1);
        UIGloader.SetUrl(window.m_oldIconLoader, UIUtils.GetEquipIcon(equipData.CurSelectPetID, (int)equipData.CurSelectEquipPos, petEquip.star));
        window.m_oldLvLabel.text = string.Format("+{0}", equipData.GetCurColorMaxLevel(petEquip.color - 1));

        StarList starList = new StarList((UI_StarList)window.m_oldStarList);
        starList.SetStar(petEquip.star);

        // 设置豆子
        PetQualityDou qualityDou = window.m_oldQualityDou as PetQualityDou;
        qualityDou.InitView(petEquip.color - 1);
    }

    private void SetNewEquipData()
    {
        PetEquip petEquip = equipData.GetCurSelectPetEquip();

        UIGloader.SetUrl(window.m_newBoarderLoader, UIUtils.GetLoaderUrl(WinEnum.UI_Common, UIUtils.GetBorderByQuality(petEquip.color)));
        window.m_newNameLabel.color = UIUtils.GetColorByQuality(petEquip.color);
        window.m_newNameLabel.text = UIUtils.GetPingJieEquipName(equipData.CurSelectPetID, (int)equipData.CurSelectEquipPos, petEquip.star, petEquip.color);
        UIGloader.SetUrl(window.m_newIconLoader, UIUtils.GetEquipIcon(equipData.CurSelectPetID, (int)equipData.CurSelectEquipPos, petEquip.star));
        window.m_newLvLabel.text = string.Format("+{0}", petEquip.level) ;

        StarList starList = new StarList((UI_StarList)window.m_newStarList);
        starList.SetStar(petEquip.star);

        // 设置豆子
        PetQualityDou qualityDou = window.m_newQulaityDou as PetQualityDou;
        qualityDou.InitView(petEquip.color);
    }

    private void SetData()
    {
        ChangeAttributeList changeAttributeList = new ChangeAttributeList((UI_attributeChangeList)window.m_attributeChangeList);
        changeAttributeList.SetData(equipData);
    }

    protected override void OnCloseBtn()
    {
        base.OnCloseBtn();

        window = null;
        equipData = null;

        Close();
    }
}
