using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_GuildBoss;

public class GuildBossRewardItem : UI_guildBossRewardItem {

    public string itemInfo;
    public int index;

    public new static GuildBossRewardItem CreateInstance()
    {
        return UI_guildBossRewardItem.CreateInstance() as GuildBossRewardItem;
    }

    public void InitView()
    {
        if (string.IsNullOrEmpty(itemInfo))
        {
            return;
        }
        m_rankLabel.text = string.Format("第{0}名", index); 
        string[] itemList = itemInfo.Split(';');
        int count = itemList.Length;
        CommonItem item = null;
        string[] itemInfoArr = null;
        m_rewardList.RemoveChildren(0, -1, true);
        for (int i = 0; i < count; i++)
        {
            itemInfoArr = itemList[i].Split('+');
            if (itemInfoArr.Length == 2)
            {
                item = CommonItem.CreateInstance();
                item.itemId = int.Parse(itemInfoArr[0]);
                item.itemNum = int.Parse(itemInfoArr[1]);
                item.isShowNum = true;
                item.RefreshView();
                item.scale = new Vector2(0.6f, 0.6f);
                m_rewardList.AddChild(item);
            }
        }
        if (item != null)
            m_rewardList.columnGap = -(int)(item.width * 0.4f) + 20;
    }
}
