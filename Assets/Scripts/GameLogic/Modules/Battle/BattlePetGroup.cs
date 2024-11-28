using UI_Battle;
using UnityEngine;
using Data.Beans;

public class BattlePetGroup : UI_battlePetGroup
{

    public new static BattlePetGroup CreateInstance()
    {
        return UI_battlePetGroup.CreateInstance() as BattlePetGroup;
    }

    public void OnOpen()
    {

    }

    public void ShowPetHeadBar()
    {
        for (int i = 0; i < m_petList.numChildren; i++)
        {
            PetHeadBar headBar = (PetHeadBar)m_petList.GetChildAt(i);
            if (headBar != null)
            {
                headBar.ShowHeadBar();
            }
        }
    }


    public void ResetHearBarStatus()
    {
        for (int i = 0; i < m_petList.numChildren; i++)
        {
            PetHeadBar headBar = (PetHeadBar)m_petList.GetChildAt(i);
            if (headBar != null)
            {
                headBar.ResetStatus();
            }
        }
    }


    public void InitHeadBar(ActorPet actor)
    {
        if (actor == null)
            return;

        if (actor.getActorType() == ActorType.Pet && actor.getCamp() == ActorCamp.CampFriend)
        {
            PetHeadBar headBar = PetHeadBar.CreateInstance();
            if (headBar != null)
            {
                m_petList.AddChildAt(headBar, 0);
                headBar.Init(actor);
            }
        }

 
    }

    public void PetHeadBarSwipeToggle(bool flag)
    {
        for (int i = 0; i < m_petList.numChildren; i++)
        {
            PetHeadBar headBar = (PetHeadBar)m_petList.GetChildAt(i);
            if (headBar != null)
            {
                headBar.ToogleSwipe(flag);
            }
        }
    }

    public void OnClose()
    {
        for (int i = 0; i < m_petList.numChildren; i++)
        {
            PetHeadBar headBar = (PetHeadBar)m_petList.GetChildAt(i);
            if (headBar != null)
            {
                headBar.OnClose();
            }
        }
    }
}
