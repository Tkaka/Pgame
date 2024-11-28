using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data.Beans;

public class FightTurnStartState : FightBaseState
{

    private long coroId;
    public override void onEnter(object obj = null)
    {
        base.onEnter(obj);
        if (obj == null)
        {
            Logger.err("PrepareNextTurnState:参数为空");
            return;
        }
        ChangeReason reason = (ChangeReason)obj;

        //重置表现 
        CmdMgr.Singleton.Reset();
        ViewUtils.Singleton.Clear();

        //切换回合
        if (fm.IsNewWave)
        {
            DoNewWave(reason);
            BattleWindMgr.CurrentBtlWin.ChangeWave(true);
        }
        else
        {
            fm.ChangeTurn();
            DropItemMgr.Singleton.StarMove();
            //Debug.Log("----------------->>>当前轮" + FightManager.Singleton.CurTurn + "     " + FightManager.Singleton.TurnCount);
        }

        //检查是否超过最大轮次
        if (!IsMaxTurn())
        {
            BattleWindMgr.CurrentBtlWin.ChangeTurn(fm.PlayerTurn ? ActorCamp.CampFriend : ActorCamp.CampEnemy);
            coroId = CoroutineManager.Singleton.startCoroutine(TurnStart());
        }
    }

    private IEnumerator TurnStart()
    {
        //重生 或者 离场 归位
        if (Homing())
            yield return new WaitForSeconds(1.5f);

        //回合开始加怒气
        TurnStartAddMp();

        //触发回合开始事件
        TriggerParam tparam = TriggerParam.Create(TriggerEnum.OnTurnStart, 0);
        if (fm.PlayerTurn)
            tparam.camp = ActorCamp.CampFriend;
        else
            tparam.camp = ActorCamp.CampEnemy;
        TriggerManager.Singleton.OnEvtTriggered(tparam);

        //回合buff生效
        EffectBuff();

        //预留表现时间
        ViewUtils.Singleton.Show(0, TimeNode.TurnStart);
        yield return new WaitForSeconds(1.0f);

        //重置角色状态信息
        fm.ResetActorStatus();

        if (FightManager.Singleton.IsReplay)
        {
            int count = fm.CanAtkCount();
            if (count > 0)
            {
                fm.ChangeState(FightState.AutoAttack);
            }
            else
            {
                fm.ChangeState(FightState.PrepareNextTurnState, ChangeReason.AllAttack);
            }
     
        }
        else
        {
            FightManager.Singleton.normalAtkPhase = false;
            //强制同步一次血量和怒气
            ForceEqualsProperty();
            //判断可以行动的数量
            int count = fm.CanAtkCount();
            if (count > 0)
            {
                //切换到相应状态
                if (fm.PlayerTurn && !fm.IsAuto)
                    fm.ChangeState(FightState.MannulAttack);
                else
                    fm.ChangeState(FightState.AutoAttack);
            }
            else
            {
                fm.ChangeState(FightState.PrepareNextTurnState, ChangeReason.AllAttack);
            }
        }
    }

    private void ForceEqualsProperty()
    {
        ActorTurnStatus[] camp = null;
        if (fm.PlayerTurn)
            camp = fm.Grid.GetCamp(ActorCamp.CampFriend);
        else
            camp = fm.Grid.GetCamp(ActorCamp.CampEnemy);
        for (int i = 0; i < camp.Length; i++)
        {
            if (camp[i].actorId < 0)
                continue;
            Actor actor = ActorManager.Singleton.Get(camp[i].actorId);
            if (actor != null)
            {
                LNumber viewMp = actor.ViewPropertyMgr.GetProperty(PropertyType.Mp);
                LNumber mp = actor.getProperty(PropertyType.Mp);
                if (Mathf.Abs(viewMp - mp) > 2)
                    Logger.err(actor.Name + "表现怒气值和逻辑值对不上" + viewMp + "_" +mp);
                actor.ViewPropertyMgr.SetProperty(PropertyType.Mp, mp);

                LNumber viewHp = actor.ViewPropertyMgr.GetProperty(PropertyType.Hp);
                LNumber hp = actor.getProperty(PropertyType.Hp);
                if (Mathf.Abs(viewHp - hp) > 2)
                    Logger.err(actor.Name + "表现血量值和逻辑值对不上" + viewHp + "_" + hp);
                actor.ViewPropertyMgr.SetProperty(PropertyType.Hp, hp, false);
            }
        }
    }

    /// <summary>
    /// 每回合开始添加怒气
    /// </summary>
    private void TurnStartAddMp()
    {
        ActorTurnStatus[] camp = null;
        if(fm.PlayerTurn)
            camp = fm.Grid.GetCamp(ActorCamp.CampFriend);
        else
            camp = fm.Grid.GetCamp(ActorCamp.CampEnemy);
        for (int i = 0; i < camp.Length; i++)
        {
            if (camp[i].actorId < 0)
                continue;
            Actor actor = ActorManager.Singleton.Get(camp[i].actorId);
            if (actor != null)
            {
                actor.ChangeProperty(PropertyType.Mp, actor.getProperty(PropertyType.TurnGetMp));
                actor.ViewPropertyMgr.ChangeProperty(PropertyType.Mp, actor.getProperty(PropertyType.TurnGetMp));
            }
        }
    }

    /// <summary>
    /// 检查是否超过最大轮
    /// </summary>
    private bool IsMaxTurn()
    {
        if (fm.FirstTurn == fm.CurTurn)
        {
            fm.TurnCount++;
            if (fm.TurnCount > FightManager.Singleton.MaxTurnNum)
            {
                //提示战斗胜利或者失败
                LNumber totalHpPet = fm.GetTotalHp(ActorCamp.CampFriend, false);
                LNumber totalHpMonster = fm.GetTotalHp(ActorCamp.CampEnemy, false);
                if (totalHpPet > totalHpMonster)
                {
                    if(FightManager.Singleton.IsReplay)
                    {
                        fm.ChangeState(FightState.End, ActorCamp.CampFriend);
                    }
                    else
                    {
                        //如果还有怪物，则到下一轮, 如果没有直接战斗胜利
                        if (SpawnerHelper.Singleton.IsBossLevel())
                        {
                            fm.ChangeState(FightState.End, ActorCamp.CampFriend);
                        }
                        else
                        {
                            //TODO:最大回合杀敌敌方表现
                            ActorManager.Singleton.KillActorByCamp(ActorCamp.CampEnemy);
                            fm.ChangeState(FightState.MoveState, false);
                        }
                    }
                }
                else
                {
                    //战斗失败
                    fm.ChangeState(FightState.End, ActorCamp.CampEnemy);
                }
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// 新一轮处理
    /// </summary>
    private void DoNewWave(ChangeReason reason)
    {
        //判断先手值
        JugePrecede();
        fm.IsNewWave = false;
        fm.TurnCount = 0;
        fm.SelectAGoodEnemy();
        
        //清除所有buff
        if (reason != ChangeReason.Start)
        {
            ActorTurnStatus[] camp = fm.Grid.GetCamp(ActorCamp.CampFriend);
            for (int i = 0; i < camp.Length; i++)
            {
                if (camp[i].actorId < 0)
                    continue;
                Actor actor = ActorManager.Singleton.Get(camp[i].actorId);
                if (actor != null)
                {
                    actor.PrintProperty("过场前=>");
                    //清除所有buff
                    actor.BuffMgr.Dispel(int.MaxValue, DispelType.All, true);
                    //增加怒气
                    actor.ChangeProperty(PropertyType.Mp, BattleParam.WaveAddMp);
                    //表现属性
                    actor.ViewPropertyMgr.ChangeProperty(PropertyType.Mp, BattleParam.WaveAddMp);
                    actor.PrintProperty("过场后=>");
                    TriggerManager.Singleton.InitPassiveEffect(actor, actor.CoreSkillId, actor.CoreSkillLevel);
                }
            }
        }
    }

    //判断先手值
    private void JugePrecede()
    {
        if(FightManager.Singleton.IsReplay)
        {
            int val1 = ReplayManager.Singleton.thisFirstVal;
            int val2 = ReplayManager.Singleton.EnemyFirstVal;
            if(val1 > val2)
            {
                fm.FirstTurn = ActorCamp.CampFriend;
                fm.CurTurn = ActorCamp.CampFriend;
            }else
            {
                fm.FirstTurn = ActorCamp.CampEnemy;
                fm.CurTurn = ActorCamp.CampEnemy;
            }
        }
        else
        {
            int val = RoleService.Singleton.GetPrecedeVal();
            int wval = FightService.Singleton.GetWavePrecedeVal();
            if (val >= wval)
            {
                fm.FirstTurn = ActorCamp.CampFriend;
                fm.CurTurn = ActorCamp.CampFriend;
            }
            else
            {
                fm.FirstTurn = ActorCamp.CampEnemy;
                fm.CurTurn = ActorCamp.CampEnemy;
            }
        }
    }

    private bool Homing()
    {
        List<long> levelList = null;
        if (fm.PlayerTurn)
            levelList = FightManager.Singleton.FriendLevelList;
        else
            levelList = FightManager.Singleton.EnemyLevelList;
        if(levelList != null && levelList.Count > 0)
        {
            for (int i = 0; i < levelList.Count; i++)
            {
                Actor actor = ActorManager.Singleton.Get(levelList[i]);
                if (actor != null)
                {
                    if (actor.WillReborn)
                    {
                        DoReborn(actor);
                    }
                    else
                    {
                        Logger.err("未处理的回场角色:" + actor.Name);
                    }
                }
            }
            levelList.Clear();
            return true;
        }
        return false;
    }

    private void DoReborn(Actor actor)
    {
        //复活处理
        actor.WillReborn = false;
        actor.BuffMgr.Reborn();
        FightManager.Singleton.Add(actor.getCamp(), actor.getActorId(), actor.GridId);
        //强制切换到待机状态
        actor.getStateMachine().changeState(ActorState.idle);
        //播放复活特效 
        t_globalBean bean = ConfigBean.GetBean<t_globalBean, int>(4);
        if (bean != null)
            FightManager.R.LoadGo(bean.t_string_param, actor.TransformExt.position);
    }

    private bool EffectBuff()
    {
        ActorTurnStatus[] target = null;
        if (fm.PlayerTurn)
            target = fm.Grid.GetCamp(ActorCamp.CampFriend);
        else
            target = fm.Grid.GetCamp(ActorCamp.CampEnemy);
        int count = 0;
        for (int i = 0; i < target.Length; i++)
        {
            if (target[i].actorId > 0)
            {
                Actor actor = ActorManager.Singleton.Get(target[i].actorId);
                if (actor != null)
                {
                    count += actor.BuffMgr.OnTurnStart();
                }
            }
        }
        if (count > 0)
            return true;
        else
            return false;
    }

    public override void onLeave(string stateKey)
    {
        base.onLeave(stateKey);
        CoroutineManager.Singleton.stopCoroutine(coroId);
    }

    public override string getStateKey()
    {
        return FightState.TurnStart;
    }

}
