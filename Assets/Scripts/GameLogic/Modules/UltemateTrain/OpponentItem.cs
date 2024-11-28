using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI_UltemateTrain;
using Message.Challenge;
using Data.Beans;

public class OpponentItem : UI_opponentItem {

    public TrialMonster monster;
    public int type;
    private SelectOpponentWindow parentUI;

    public new static OpponentItem CreateInstance()
    {
        return UI_opponentItem.CreateInstance() as OpponentItem;
    }

    public void InitView(SelectOpponentWindow parentUI)
    {
        this.parentUI = parentUI;

        m_challengeBtn.onClick.Add(OnChallengeBtnClick);
        m_toucher.onTouchBegin.Add(OnItemTouchBegin);
        m_toucher.onTouchEnd.Add(OnItemTouchEnd);
        m_toucher.onRollOut.Add(OnItemTouchEnd);
        m_toucher.onClick.Add(OnItemClcik);

        m_petListGroup.visible = false;


        InitBaseInfo();
        InitItemState();
        InitPetList();
        
    }

    private void InitBaseInfo()
    {
        if (monster != null)
        {
            m_fightPowerLabel.text = string.Format("战力:{0}", monster.fightPower);
            ResTrialInfo trailInfo = UltemateTrainService.Singleton.trainInfo;
            if (trailInfo != null)
            {
                t_trialBean trialBean = ConfigBean.GetBean<t_trialBean, int>(trailInfo.trialInfo.floor);
                if (trialBean != null && !string.IsNullOrEmpty(trialBean.t_base_score))
                {
                    string[] baseScoreArr = trialBean.t_base_score.Split('+');
                    if (baseScoreArr.Length > type)
                        m_integralLabel.text = baseScoreArr[type];
                }
            }

            if (monster.canFight == 1)
            {
                this.grayed = false;
                this.m_lockIcon.visible = false;
            }
            else
            {
                this.grayed = true;
                this.m_lockIcon.visible = true;
            }
        }
        // 1801003 终极试炼怪物难度得星倍率
        t_globalBean globalBean = ConfigBean.GetBean<t_globalBean, int>(1801003);
        if (globalBean != null && !string.IsNullOrEmpty(globalBean.t_string_param))
        {
            string[] starTimesArr = globalBean.t_string_param.Split('+');
            if (starTimesArr.Length > type)
                m_starTimesLabel.text = starTimesArr[type];
        }
        // 设置类型图标
        OpponentType opponentType = (OpponentType)type;
        string iconName = "";
        switch (opponentType)
        {
            case OpponentType.Easy:
                iconName += "easyIcon";
                break;
            case OpponentType.Middle:
                iconName += "middleIcon";
                break;
            case OpponentType.Hard:
                iconName += "hardIcon";
                break;
            default:
                break;
        }
        UIGloader.SetUrl(m_difficultyLoader, UIUtils.GetLoaderUrl(WinEnum.UI_UltemateTrain, iconName));

        // 加载图片 图片为第一个宠物的图片
        TrialMonsterSimpleInfo monsterInfo = monster.monster[0];
        UIGloader.SetUrl(m_iconLoader, UIUtils.GetPetStartIcon(monsterInfo.petId, monsterInfo.star));

    }

    private void InitItemState()
    {
        if (monster != null)
        {
            bool isCanFight = monster.canFight == 1;
            this.grayed = !isCanFight;
            m_lockIcon.visible = !isCanFight;
        }
    }

    private void InitPetList()
    {
        int count = monster.monster.Count;
        m_petList.RemoveChildren(0, -1, true);
        OpponentPetItem petItem;
        for (int i = 0; i < count; i++)
        {
            petItem = OpponentPetItem.CreateInstance();
            petItem.monsterInfo = monster.monster[i];
            petItem.InitView();

            m_petList.AddChild(petItem);
        }
    }


    private void OnChallengeBtnClick()
    {
        if (monster != null)
        {
            if (monster.canFight == 0)
                TipWindow.Singleton.ShowTip("无法挑战这个对手!");
            else
            {   
                // 进入阵容界面
                WinMgr.Singleton.Open<BuZhenWindow>(null, UILayer.Popup);
                parentUI.curType = type;
            }
        }
       
    }

    private void OnItemClcik()
    {
        if (monster != null && monster.canFight == 0)
        {
            TipWindow.Singleton.ShowTip("无法挑战这个对手!");
        }
    }

    private void OnItemTouchBegin()
    {
        m_petListGroup.visible = true;
    }

    private void OnItemTouchEnd()
    {
        m_petListGroup.visible = false;
    }
}
