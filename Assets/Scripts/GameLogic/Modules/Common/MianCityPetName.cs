using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_Common;
using Data.Beans;

public class MianCityPetName : UI_mainCityPetName
{

    ActorMC aMC;

    public new static MianCityPetName CreateInstance()
    {
        return UI_mainCityPetName.CreateInstance() as MianCityPetName;
    }

    public void RefreshView(ActorMC actorMC)
    {
        aMC = actorMC;
        WinMgr.Singleton.Hud1Layer.AddChild(this);
        this.touchable = false;
        fixPos();
        UpdatePetInfo();
    }
    private void fixPos()
    {
        if (aMC.IsDestoryed)
            return;

        Vector3 ownerPos = aMC.monoBehavior.transform.position;
        BoxCollider boxCollider = aMC.monoBehavior.GetComponent<BoxCollider>();
        if (boxCollider != null)
            ownerPos.y -= boxCollider.size.y * 0.5f - boxCollider.center.y + 0.2f;
        else
            ownerPos.y += 1;
        Vector3 screenPos = Camera.main.WorldToScreenPoint(ownerPos);
        screenPos.y = Screen.height - screenPos.y; //convert to Stage coordinates system

        Vector3 pt = WinMgr.Singleton.Hud1Layer.GlobalToLocal(screenPos);
        // 5 名字x轴位置偏移
        this.SetXY(Mathf.RoundToInt(pt.x - actualWidth * 0.5f + 5), Mathf.RoundToInt(pt.y));
    }

    private void UpdatePetInfo()
    {
        int petID = aMC.getTemplateId();
        Message.Pet.PetInfo petInfo = PetService.Singleton.GetPetInfo(petID);
        if (petInfo != null)
        {
            t_petBean petBean = ConfigBean.GetBean<t_petBean, int>(petID);
            if (petBean != null)
            {
                m_nameLabel.text = UIUtils.GetPetName(petBean, petInfo.basInfo.star);
                m_nameLabel.color = UIUtils.GetColorByQuality(petInfo.basInfo.color);
            }
        }
    }
}
