using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_UltemateTrain;
using Message.Challenge;

public enum OpponentType
{
    Easy = 0,              // 简单
    Middle = 1,            // 中等
    Hard = 2,              // 困难
}

public class SelectOpponentWindow : BaseWindow {

    UI_SelectOpponentWindow window;
    List<TrialMonster> monsterList;

    public int curType;

    public override void OnOpen()
    {
        base.OnOpen();

        window = getUiWindow<UI_SelectOpponentWindow>();
        monsterList = Info.param as List<TrialMonster>;
        window.m_keyShangZhenBtn.onClick.Add(OnKeyShangZhenBtnClick);
        window.m_backBtn.onClick.Add(OnCloseBtn);

        InitView();
        RefreshView();
    }

    public override void InitView()
    {
        base.InitView();

        InitOpponentList();
    }

    public override void AddEventListener()
    {
        base.AddEventListener();

        GED.ED.addListener(EventID.OnPetTeamListChanged, OnPetTeamListChanged);
        GED.ED.addListener(EventID.OnBuZhenStarFightBtnClick, OnBuZhenStarFightBtnClick);
        GED.ED.addListener(EventID.OnResTrialFightStart, OnResTrialFightStart);
    }

    public override void RemoveEventListener()
    {
        base.RemoveEventListener();

        GED.ED.removeListener(EventID.OnPetTeamListChanged, OnPetTeamListChanged);
        GED.ED.removeListener(EventID.OnBuZhenStarFightBtnClick, OnBuZhenStarFightBtnClick);
        GED.ED.removeListener(EventID.OnResTrialFightStart, OnResTrialFightStart);
    }

    private void InitOpponentList()
    {
        int count = monsterList.Count;
        OpponentItem opponentItem = null;
        window.m_opponentList.RemoveChildren(0, -1, true);
        for (int i = 0; i < count; i++)
        {
            opponentItem = OpponentItem.CreateInstance();
            opponentItem.monster = monsterList[i];
            opponentItem.type = i;
            opponentItem.InitView(this);
            window.m_opponentList.AddChild(opponentItem);
        }
    }

    public override void RefreshView()
    {
        base.RefreshView();

        RefreshShangZhenList();
        RefreshFightPowerInfo();
    }

    private void RefreshShangZhenList()
    {
        int count = window.m_shanZhenList.numItems;
        List<int> shangZhenList = PetService.Singleton.GetTeamList(ZhenRongType.ZhongJiShiLian, true);
        UltematePetItem petItem = null;
        if (count == 0)
        {
            count = shangZhenList.Count;
            TrialPetStatus petStatue = null;
            for (int i = 0; i < count; i++)
            {
                if(shangZhenList[i] != 0)
                {
                    petStatue = UltemateTrainService.Singleton.GetTrialPetStatue(shangZhenList[i]);
                    if (petStatue != null && petStatue.dead == 1)
                        shangZhenList[i] = 0;
                }

                petItem = UltematePetItem.CreateInstance();
                petItem.petID = shangZhenList[i];
                petItem.InitView();

                window.m_shanZhenList.AddChild(petItem);
            }
        }
        else
        {
            for (int i = 0; i < count; i++)
            {
                if (i < shangZhenList.Count)
                {
                    petItem = window.m_shanZhenList.GetChildAt(i) as UltematePetItem;
                    petItem.petID = shangZhenList[i];
                    petItem.RefreshView();
                }
            }
        }
    }

    private void RefreshFightPowerInfo()
    {
        long totalFightPower = 0;
        List<int> shangZhenList = PetService.Singleton.GetTeamList(ZhenRongType.ZhongJiShiLian);
        int count = shangZhenList.Count;
        Message.Pet.PetInfo petInfo = null;
        for (int i = 0; i < count; i++)
        {
            petInfo = PetService.Singleton.GetPetInfo(shangZhenList[i]);
            if (petInfo != null)
                totalFightPower += PetService.Singleton.GetPetFightPower(shangZhenList[i]);
        }

        window.m_fightPowerLabel.text = totalFightPower + "";
    }

    private void OnKeyShangZhenBtnClick()
    {
        bool isShanZhen = PetService.Singleton.KeyShangZhen(ZhenRongType.ZhongJiShiLian);
        if (isShanZhen == false)
            TipWindow.Singleton.ShowTip(61801036);
    }

    private void OnPetTeamListChanged(GameEvent evt)
    {
        RefreshView();
    }

    private void OnBuZhenStarFightBtnClick(GameEvent evt)
    {
        UltemateTrainService.Singleton.ReqTrialFightStart(curType);
    }

    private void OnResTrialFightStart(GameEvent evt)
    {
        // 测试状态
        //List<TrialPetStatus> petStatueList = new List<TrialPetStatus>();

        //TrialPetStatus petStetus = new TrialPetStatus();
        //petStetus.anger = 200;
        //petStetus.hpLoss = 100;
        //petStatueList.Add(petStetus);

        //petStetus = new TrialPetStatus();
        //petStetus.anger = 250;
        //petStetus.hpLoss = 100;
        //petStatueList.Add(petStetus);

        //petStetus = new TrialPetStatus();
        //petStetus.anger = 200;
        //petStetus.hpLoss = 100;
        //petStetus.dead = 0;
        //petStatueList.Add(petStetus);

        //int result = Random.Range(0, 4);
        //UltemateTrainService.Singleton.ReqTrialFightEnd(1, petStatueList);

        OnCloseBtn();
    }

    protected override void OnCloseBtn()
    {
        base.OnCloseBtn();
    }
}
