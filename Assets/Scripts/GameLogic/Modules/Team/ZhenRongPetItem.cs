using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_BuZhen;

public class ZhenRongPetItem : UI_zhenRongPetItem {

    public int petID;
    public int selectID;
    public int index;
    System.Action<ZhenRongPetItem> clickCall;

	public void Init(int petID, int selectID, System.Action<ZhenRongPetItem> clickCall)
    {
        this.clickCall = clickCall;
        this.petID = petID;
        this.selectID = selectID;

        m_itemToucher.onClick.Add(OnClickItem);
        RefreshItem();
    }

    public void RefreshItem()
    {
        if (petID == 0)
        {
            m_unGetGroup.visible = true;
            m_petItem.visible = false;
        }
        else
        {
            m_unGetGroup.visible = false;
            m_petItem.visible = true;
            PetItem petItem = m_petItem as PetItem;
            petItem.petID = petID;
            petItem.RefreshItem(selectID, PetItemType.Pet, false);
        }
    }

    private void  OnClickItem()
    {
        if (clickCall != null)
        {
            clickCall(this);
        }
    }
}
