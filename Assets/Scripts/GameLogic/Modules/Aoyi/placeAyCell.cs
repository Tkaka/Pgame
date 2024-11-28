using UI_AoYi;
using UI_Common;
using Message.Profound;
using Message.Pet;
using FairyGUI;
using System.Collections.Generic;
using Data.Beans;

public class PlaceAyCell : UI_placeAyCell
{

    public new static PlaceAyCell CreateInstance()
    {
        return UI_placeAyCell.CreateInstance() as PlaceAyCell;
    }

    public void RefreshView(StoneInfo stoneInfo, string dicIcon)
    {
        if (stoneInfo != null)
        {
            m_itemIcon.visible = true;
            m_imgDic.visible = false;

            AoyiCommonItem commonItem = this.m_itemIcon as AoyiCommonItem;
            if (commonItem != null)
            {
                commonItem.RefreshView(stoneInfo.itemId, stoneInfo.bigLevel * 10 + stoneInfo.minLevel);
            }
        }
        else
        {
            m_itemIcon.visible = false;
            m_imgDic.visible = true;
            UIGloader.SetUrl(m_imgDic, dicIcon);
        }
    }
}