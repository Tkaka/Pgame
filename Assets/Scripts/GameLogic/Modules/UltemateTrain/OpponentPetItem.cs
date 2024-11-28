using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_UltemateTrain;
using Message.Challenge;
using Data.Beans;
using UI_Common;

public class OpponentPetItem : UI_opponentPetItem {

    public TrialMonsterSimpleInfo monsterInfo;

    public new static OpponentPetItem CreateInstance()
    {
        return UI_opponentPetItem.CreateInstance() as OpponentPetItem;
    }

    public void InitView()
    {
        if (monsterInfo != null)
        {
            StarList starList = new StarList(m_starList as UI_StarList);
            starList.SetStar(monsterInfo.star);

            UIGloader.SetUrl(m_boardLoader, UIUtils.GetLoaderUrl(WinEnum.UI_Common, UIUtils.GetBorderByQuality(monsterInfo.color)));
            UIGloader.SetUrl(m_iconLoader, UIUtils.GetPetStartIcon(monsterInfo.petId, monsterInfo.star));
            m_levelLabel.text = monsterInfo.level + "";
        }
        
    }
}
