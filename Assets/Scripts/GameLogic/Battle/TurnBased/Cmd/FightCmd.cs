using System;
using UnityEngine;

public class FightCmd 
{

    //命令id
    public int cmdId;

    //角色ID
    public long actorId;

    //是否大招
    public bool isMasterSkill;

    //技能id
    public int skillId;

    //是否手动
    public bool isManual;

    //是否是最后一个
    public bool isLastOne;

    public ComboType comboType;  //连击类型

    public bool showCircle;      //是否出现光圈

    public Action<float> callback;

    public float Excute()
    {
        Actor actor = ActorManager.Singleton.Get(actorId);
        if (actor != null)
        {
            if (isMasterSkill)
            {
                OnAniCmp();
                //MasterSkillCtrl.Singleton.Play(OnAniCmp);
            }
            else
            {
                actor.WillUseSmallSkill = false;
                OnAniCmp();
            }
        }
        return 0;
    }

    private void OnAniCmp()
    {
        Actor actor = ActorManager.Singleton.Get(actorId);
        float skillTime = actor.Attack(this);
        FightManager.turnGapDelta = skillTime;
        FightManager.Singleton.Attack(actor.getCamp(), actor.GridId, isMasterSkill);
        if (isMasterSkill)
        {
            Logger.log(actor.Name + "  master skill mp:" + actor.getProperty(PropertyType.Mp));
            if (callback != null)
                callback(skillTime + 0.5f);
        }
        else
        {
            Logger.log(actor.Name + "  normal attack mp:" + actor.getProperty(PropertyType.Mp));
            //FightManager.Singleton.Attack(actor.getCamp(), actor.GridId, isMasterSkill);
        }
    }

    public bool ShowComboCircle()
    {
        //手动，不是最后一个
        return isManual && showCircle && !isLastOne;
    }

    public bool ShowComboTip()
    {
        if (isMasterSkill)
            return false;
        return !isLastOne;                     //手动自动都显示
    }

}
