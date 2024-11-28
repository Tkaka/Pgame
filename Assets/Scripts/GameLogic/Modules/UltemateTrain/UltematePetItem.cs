using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_UltemateTrain;
using UI_Common;
using Message.Challenge;

public class UltematePetItem : UI_ultematePetItem {

    public int petID;

    public new static UltematePetItem CreateInstance()
    {
        return UI_ultematePetItem.CreateInstance() as UltematePetItem;
    }

    public void InitView()
    {
        m_toucher.onClick.Add(OnClickItem);
        RefreshView();
    }

    public void RefreshView()
    {
        m_noPetGroup.visible = petID == 0;
        m_petGroup.visible = petID != 0;
        if (petID != 0)
        {
            UIGloader.SetUrl(m_iconLoader,UIUtils.GetLoaderUrl(WinEnum.UI_Common, UIUtils.GetPetStartIcon(petID)));
            Message.Pet.PetInfo petInfo = PetService.Singleton.GetPetInfo(petID);
            if (petInfo != null)
            {
                UIGloader.SetUrl(m_boardLoader, UIUtils.GetLoaderUrl(WinEnum.UI_Common, UIUtils.GetBorderByQuality(petInfo.basInfo.color)));
                UIGloader.SetUrl(m_iconLoader, UIUtils.GetPetStartIcon(petID, petInfo.basInfo.star));
                StarList starList = new StarList(m_starList as UI_StarList);
                starList.SetStar(petInfo.basInfo.star);
                m_levelLabel.text = petInfo.basInfo.level + "";

                TrialPetStatus petStatue = UltemateTrainService.Singleton.GetTrialPetStatue(petID);
                int hp = 0;
                int eneger = 0;
                if (petStatue != null)
                {
                    hp = petInfo.fightInfo.hp - petStatue.hpLoss;
                    eneger = petStatue.anger;
                }
                else
                {
                    hp = petInfo.fightInfo.hp;
                    eneger = 0;
                }

                m_hpProgress.max = petInfo.fightInfo.hp;
                m_hpProgress.value = hp;

                m_energyProgress.max = 1000;
                m_energyProgress.value = eneger;
            }
        }
       
    }

    private void OnClickItem()
    {
        List<int> petList = PetService.Singleton.GetBestTeamList(ZhenRongType.ZhongJiShiLian);
        if(petList == null)
        {
            TipWindow.Singleton.ShowTip(61801036);
            return;
        }
        // 打开上阵界面
        TwoParam<int, ShangZhenSelectType> param = new TwoParam<int, ShangZhenSelectType>();
        param.value1 = petID;
        param.value2 = ShangZhenSelectType.Default;
        WinMgr.Singleton.Open<ShangZhenWindow>(WinInfo.Create(false, null, false, param), UILayer.Popup);
    }
}
