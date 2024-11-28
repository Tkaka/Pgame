
public class FightMannulAttackState : FightBaseState
{

    public override void onEnter(object obj = null)
    {
        base.onEnter(obj);
        fm.CurIsAuto = false;
        GED.ED.addListener(EventID.OnPetNormalSkill, OnNormalSkill);
        GED.ED.addListener(EventID.OnPetBigSkill, OnMasterSkill);
        BattleCDCtrl.Singleton.Start();
    }

    private void OnNormalSkill(GameEvent evt)
    {
        if (!FightManager.Singleton.normalAtkPhase)
        {
            FightManager.Singleton.normalAtkPhase = true;
            BattleWindMgr.CurrentBtlWin.ChangeFightState(FightState.MannulNormalAttack);
        }
        //判断是否还有目标
        int aliveNum = fm.Grid.AliveNum(ActorCamp.CampEnemy);
        if (aliveNum <= 0)
        {
            Logger.log("已经没有存活的目标了");
            return;
        }
        ThreeParam<long, ComboType, bool> param = (ThreeParam<long, ComboType, bool>)evt.Data;
        if (param != null)
        {
            Actor actor = ActorManager.Singleton.Get(param.value1);
            if (actor != null)
            {
                ActorTurnStatus status = fm.Grid.Get(ActorCamp.CampFriend, actor.GridId);
                //判断是否可以行动
                if (status != null && status.CanAction())
                {
                    FightCmd cmd = new FightCmd();
                    cmd.isMasterSkill = false;
                    cmd.isManual = true;
                    cmd.actorId = param.value1;
                    cmd.comboType = param.value2;
                    cmd.showCircle = param.value3;
                    cmd.skillId = status.skillId;
                    CmdMgr.Singleton.Enqueue(cmd);
                    //每入队一个，都预先判断一次伤害，判断是否是否还有怪物
                    //如果有则连击，没有则不算连击了, 大招同逻辑处理
                    BattleCDCtrl.Singleton.ResetVal(BattleParam.NormalSkillCD);
                    BattleCDCtrl.Singleton.PauseToggle(true);
                }
                else
                {
                    Logger.err(actor.Name + " 无法行动");
                }
            }
            else
            {
                Logger.err("FightMannulAttackState:OnSmallSkill:找不到角色" + actor.GridId);
            }
        }
        else
        {
            Logger.err("FightMannulAttackState:OnSmallSkill:参数为空");
        }
    }

    private void OnMasterSkill(GameEvent evt)
    {
        long actorId = (long)evt.Data;
        if (actorId > 0)
        {
            //判断是否还有目标
            int aliveNum = fm.Grid.AliveNum(ActorCamp.CampEnemy);
            if (aliveNum <= 0)
            {
                Logger.log("已经没有存活的目标了");
                return;
            }
            Actor actor = ActorManager.Singleton.Get(actorId);
            if (actor != null)
            {
                ActorTurnStatus status = fm.Grid.Get(ActorCamp.CampFriend, actor.GridId);
                if (status != null && status.CanAction(true))
                {
                    FightCmd cmd = new FightCmd();
                    cmd.isMasterSkill = true;
                    cmd.isManual = true;
                    cmd.actorId = actorId;
                    cmd.comboType = ComboType.Normal;
                    cmd.showCircle = false;   //是否出圈
                    CmdMgr.Singleton.Enqueue(cmd);
                    BattleCDCtrl.Singleton.PauseToggle(true);
                }
            }
        }
        else
        {
            Logger.err("FightMannulAttackState:OnSmallSkill:actorId不合法" + actorId);
        }
    }

    public override void onLeave(string stateKey)
    {
        base.onLeave(stateKey);
        GED.ED.removeListener(EventID.OnPetNormalSkill, OnNormalSkill);
        GED.ED.removeListener(EventID.OnPetBigSkill, OnMasterSkill);
        ComboCtrl.Singleton.ToggleTouch(false);
        FightManager.ComboAdd = 1;
        BattleCDCtrl.Singleton.Stop();
    }

    public override string getStateKey()
    {
        return FightState.MannulAttack;
    }

}
