
public class FightAutoAttackState : FightBaseState
{

    private int MaxWhile = 20;

    private int NowCount = 0;

    public override void onLeave(string stateKey)
    {
        base.onLeave(stateKey);
        NowCount = 0;
    }

    public override void onEnter(object obj = null)
    {
        base.onEnter(obj);
        ComboCtrl.Singleton.ToggleTouch(false);

        if (fm.PlayerTurn)
            fm.CurIsAuto = true;
        else
            BattleCDCtrl.Singleton.Stop();

        //if (!fm.PlayerTurn)
        //    BattleCDCtrl.Singleton.Stop();

        //显示选中圈
        StartTurn();
    }

    public void StartTurn()
    {
        if (fm.Grid.AliveNum(ActorCamp.CampFriend) <= 0 || fm.Grid.AliveNum(ActorCamp.CampEnemy) <= 0)
        {
            Logger.err("FightAutoAttackState:不存在攻击目标");
            return;
        }

        if (FightManager.Singleton.IsReplay)
            ReplayManager.Singleton.AddTrunCMD(CmdMgr.Singleton);
        else
            DoMasterSkill();
    }

    private ActorCamp atkCamp;
    private void DoMasterSkill()
    {
        if (fm.PlayerTurn)
            atkCamp = ActorCamp.CampFriend;
        else
            atkCamp = ActorCamp.CampEnemy;

        if (fm.normalAtkPhase)
        {
            DoNormalSkill();
            return;
        }

        //考虑驱散状态的改变
        ActorTurnStatus status = GetNextOne(true);
        while (status != null)
        {
            NowCount++;
            if (NowCount > MaxWhile)
            {
                Logger.err("超过最大循环次数");
                break;
            }
            FightCmd cmd = new FightCmd();
            cmd.actorId = status.actorId;
            cmd.isMasterSkill = true;
            cmd.isManual = false;
            cmd.comboType = ComboType.Normal;
            cmd.showCircle = false;  //是否出圈
            CmdMgr.Singleton.Enqueue(cmd);
            status = GetNextOne(true);
        }
        DoNormalSkill();
    }

    private void DoNormalSkill()
    {
        //考虑驱散状态的改变
        ActorTurnStatus status = GetNextOne();
        while (status != null)
        {
            NowCount++;
            if (NowCount > MaxWhile)
            {
                Logger.err("超过最大循环次数");
                break;
            }
            FightCmd cmd = new FightCmd();
            cmd.actorId = status.actorId;
            cmd.isMasterSkill = false;
            cmd.isManual = false;
            cmd.skillId = status.skillId;
            //随机
            cmd.comboType = GetRandomCombo();
            cmd.showCircle = false; //是否出圈
            CmdMgr.Singleton.Enqueue(cmd);
            status = GetNextOne();
        }
    }

    private ActorTurnStatus GetNextOne(bool isMaster=false)
    {
        if (CmdMgr.Singleton.IsCurTurnClear)
            return null;
        ActorTurnStatus[] target = fm.Grid.GetCamp(atkCamp);
        for (int i = 0; i < target.Length; i++)
        {
            //如果预判已经清场，则永远添加不到队列了
            if (target[i].CanAction(isMaster))
                return target[i];
        }
        return null;
    }

    public static ComboType GetRandomCombo()
    {
        if (AttackUtils.WillOccurL(BattleParam.PerfectRate))
            return ComboType.Perfect;
        if (AttackUtils.WillOccurL(BattleParam.NotBabRate))
            return ComboType.NotBad;
        if (AttackUtils.WillOccurL(BattleParam.GoodRate))
            return ComboType.Good;
        if (AttackUtils.WillOccurL(BattleParam.PerfectRate))
            return ComboType.Perfect;
        return ComboType.Normal;
    }

    public override string getStateKey()
    {
        return FightState.AutoAttack;
    }

}
