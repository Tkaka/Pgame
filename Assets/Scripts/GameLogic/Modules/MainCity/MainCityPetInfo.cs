using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_Common;
using FairyGUI;
using Data.Beans;

public class MainCityPetInfo : UI_mainCityPetInfo {

    ActorMC actor;
    StarList starList;

    public new static MainCityPetInfo CreateInstance()
    {
        return UI_mainCityPetInfo.CreateInstance() as MainCityPetInfo;
    }

    public void RefreshView(ActorMC actorMc)
    {
        actor = actorMc;
        WinMgr.Singleton.Hud1Layer.AddChild(this);
        this.touchable = false;
        fixPos();
        UpdatePetInfo();
    }

    private void fixPos()
    {
        if (actor.IsDestoryed)
            return;

        Vector3 ownerPos = actor.monoBehavior.headBarPos;
        BoxCollider boxCollider = actor.monoBehavior.GetComponent<BoxCollider>();
        if (boxCollider != null)
            ownerPos.y -= boxCollider.size.y * 0.5f - boxCollider.center.y - 0.3f;
        else
            ownerPos.y += 1;
        Vector3 screenPos = Camera.main.WorldToScreenPoint(ownerPos);
        screenPos.y = Screen.height - screenPos.y; //convert to Stage coordinates system

        Vector3 pt = WinMgr.Singleton.Hud1Layer.GlobalToLocal(screenPos);
        this.SetXY(Mathf.RoundToInt(pt.x - actualWidth * 0.5f + 2), Mathf.RoundToInt(pt.y - actualHeight));
    }

    private void UpdatePetInfo()
    {
        int petID = actor.getTemplateId();
        Message.Pet.PetInfo petInfo = PetService.Singleton.GetPetInfo(petID);
        if (petInfo != null)
        {
            m_levelLabel.text = string.Format("{0}级", petInfo.basInfo.level);
            t_petBean petBean = ConfigBean.GetBean<t_petBean, int>(petID);
            if (petBean != null)
            {
                //m_nameLabel.text = UIUtils.GetPetName(petBean, petInfo.basInfo.star);
                //m_nameLabel.color = UIUtils.GetColorByQuality(petInfo.basInfo.color);

                UIGloader.SetUrl(m_typeLoader, UIUtils.GetLoaderUrl(WinEnum.UI_Common, UIUtils.GetPetTypeUrl(petBean.t_type)));
            }

            if (starList == null)
                starList = new StarList(m_starList);

            starList.SetStar(petInfo.basInfo.star);
        }
    }
}
