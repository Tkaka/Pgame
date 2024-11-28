using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_GuildBattle;

public class GuildBattleZhenRongItem : UI_guildBattleZhenRongItem {

    public int petCount;
    public int limitCount;
    private List<int> petList;
    GuildBattleBuZhengWindow parentWindow;
    public new static GuildBattleZhenRongItem CreateInstance()
    {
        return UI_guildBattleZhenRongItem.CreateInstance() as GuildBattleZhenRongItem;
    }

    public void RefreshView(int limitCount, List<int> petList, GuildBattleBuZhengWindow parentWindow)
    {
        this.limitCount = limitCount;
        this.petList = petList;
        this.parentWindow = parentWindow;

        RefreshPetList();
    }

    private void RefreshPetList()
    {
        int count = petList.Count;
        if (count != m_petList.numItems)
            return;
        GuildBattlePetItem petItem = null;
        for (int i = 0; i < count; i++)
        {
            petItem = m_petList.GetChildAt(i) as GuildBattlePetItem;
            petItem.petID = petList[i];
            petItem.isShanZhen = true;
            petItem.InitItem(parentWindow);
            petItem.RefreshItem();
        }

        ChangeData();
    }
    /// <summary>
    /// 改变child
    /// </summary>
    public bool ChangePetList(GuildBattlePetItem childPetItem, int changePetID)
    {
        if (childPetItem.petID == 0)
        {
            if (IsCanChangeChild())
            {
                childPetItem.petID = changePetID;
                childPetItem.RefreshItem();
                ChangeData();
                return true;
            }
            else
            {
                TipWindow.Singleton.ShowTip("队伍人数已满！");
                return false;
            }
        }
        else
        {
            childPetItem.petID = changePetID;
            childPetItem.RefreshItem();
            ChangeData();
            return true;
        }
    }
    /// <summary>
    /// 改变数据
    /// </summary>
    public void ChangeData()
    {
        long fightPower = 0;
        int xianShouZhi = 0;

        Message.Pet.PetInfo petInfo = null;
        GuildBattlePetItem petItem = null;
        int count = m_petList.numItems;
        for (int i = 0; i < count; i++)
        {
            petItem = m_petList.GetChildAt(i) as GuildBattlePetItem;
            if (petItem.petID != 0)
            {
                petInfo = PetService.Singleton.GetPetInfo(petItem.petID);
                if (petInfo != null)
                {
                    fightPower += PetService.Singleton.GetPetFightPower(petItem.petID);
                    xianShouZhi += petInfo.priority;
                }
            }
        }

        m_xianShouZhi.text = xianShouZhi + "";
        m_fightPower.text = fightPower + "";
    }

    public bool IsCanChangeChild()
    {
        int count = m_petList.numItems;
        int haveCount = 0;
        GuildBattlePetItem petItem;
        for (int i = 0; i < count; i++)
        {
            petItem = m_petList.GetChildAt(i) as GuildBattlePetItem;
            if (petItem.petID != 0)
                haveCount++;
        }
        return haveCount < limitCount;
    }
}
