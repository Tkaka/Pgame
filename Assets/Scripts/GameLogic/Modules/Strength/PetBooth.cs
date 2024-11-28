using UI_Strength;
using Data.Beans;
using UnityEngine;
using FairyGUI;
using Message.Pet;

public class PetBooth : UI_PetBooth 
{

    public int star;

    public int petId;
    UIResPack resPack = new UIResPack("JinHuaPanel/PetBooth");

    public new static PetBooth CreateInstance()
    {
        return (PetBooth)UI_PetBooth.CreateInstance();
    }

    public void Init(int star, int petId)
    {
        this.petId = petId;
        this.star = star;
        PetInfo petInfo = PetService.Singleton.GetPetInfo(petId);
        if (petInfo != null)
        {
            StarList starList = new StarList((UI_Common.UI_StarList)m_starList);
            starList.SetStar(star);
            if (star > petInfo.basInfo.star)
            {
                m_unlock.visible = true;
                m_unlockTxt.text = star + "星解锁";
            }
            else
            {
                m_unlock.visible = false;
            }
            ShowModel();
        }
    }
    private GoWrapper goWrapper;
    public void ShowModel()
    {
        PetInfo petInfo = PetService.Singleton.GetPetInfo(petId);
        t_petBean bean = ConfigBean.GetBean<t_petBean, int>(petId);
        if (petInfo != null && bean != null)
        {
            GoWrapper wrapper = new GoWrapper();
            m_modelHolder.SetNativeObject(wrapper);
            ActorUI actor = resPack.NewActorUI(bean.t_id, ActorType.Pet, wrapper);
            actor.SetTransform(new Vector3(0, 0, 500), 150, new Vector3(0, 180, 0));
        }
    }

    public override void Dispose()
    {
        if (resPack != null)
            resPack.ReleaseAllRes();
        resPack = null;
        base.Dispose();
    }

}
