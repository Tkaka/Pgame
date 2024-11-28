
using System.Collections.Generic;
using UnityEngine;

public class CmdMgr : SingletonTemplate<CmdMgr>
{
    private Queue<FightCmd> cmdQueue;

    public bool IsCurTurnClear = false;

    public long lastActorId;

    private bool isStop = false;

    public int normalCmdNum { private set; get; }
    public int masterCmdNum { private set; get; }

    //当前队列id
    public int CurCmdId { private set; get; }

    //当前执行id
    public int CurExcuteId { private set; get; }

    public CmdMgr()
    {
        cmdQueue = new Queue<FightCmd>();
    }

    private float atkGap = 1.2f;
    private float lastAtkTime = 0.0f;

    //public int testCount = 0;
   public void Update()
    {
        if (isStop)
            return;
        float now = Time.time;
        if ((now - lastAtkTime) > atkGap)
        {
            FightCmd cmd = Dequeue();
            if (cmd != null)
            {
                //testCount++;
                //Debug.Log("------------------执行指令数" + testCount + "            " + ActorManager.Singleton.Get(cmd.actorId).Name);
                lastAtkTime = now;
                if (cmd.isMasterSkill)
                {
                    isStop = true;
                    cmd.callback = MasterAniCallback;
                    CurExcuteId = cmd.cmdId;
                    cmd.Excute();
                }
                else
                {
                    normalCmdNum--;
                    atkGap = 1.2f;
                    CurExcuteId = cmd.cmdId;

                    if (masterCmdNum > 0 && cmdQueue.Count > 0 || cmdQueue.Count > 0)
                        cmd.showCircle = false;

                    cmd.Excute();
                }

                // CurExcuteId = -1;
            }
        }
        else
        {
            //Logger.log("atk gap:" + (now - lastAtkTime));
        }
    }

    /// <summary>
    /// 大招回调
    /// </summary>
    private void MasterAniCallback(float skillTime)
    {
        isStop = false;
        atkGap = skillTime;
        lastAtkTime = Time.time;
    }

    public void Enqueue(FightCmd cmd)
    {
        //预算伤害值,使用技能成功才放入队列中
        if (PreCompute(cmd))
        {
            //Debug.Log("当前波数------------------》》》》》" + SpawnerHelper.Singleton.CurWave + "               " + "当前轮数" + FightManager.Singleton.TurnCount);
            Actor actor = ActorManager.Singleton.Get(cmd.actorId);
            if (actor != null)
            {
                FightManager.Singleton.CmdEnqueue(actor.getCamp(), actor.GridId, cmd.isMasterSkill);
                cmd.cmdId = CurCmdId;
                CurCmdId++;
                cmdQueue.Enqueue(cmd);
                if (FightManager.Singleton.PlayerTurn)
                {
                    if (!cmd.isMasterSkill)
                        normalCmdNum++;
                    else
                    {
                        normalCmdNum = 1;
                        masterCmdNum++;
                    }
                }

                CmdCollectMgr.Singleton.SetCmd(cmd);
            }
        }
    }

    /// <summary>
    /// 预判
    /// </summary>
    /// <param name="cmd"></param>
    /// <returns>技能是否使用成功</returns>
    public bool PreCompute(FightCmd cmd)
    {
        Actor actor = ActorManager.Singleton.Get(cmd.actorId);
        if (actor != null)
        {
            //首先判断存活数量 
            int aliveNum = FightManager.Singleton.Grid.AliveNum(AttackUtils.GetEnemyCamp(actor.getCamp()));
            if (aliveNum > 0)
            {
                int skillId = cmd.skillId;
                if (cmd.isMasterSkill)
                {
                    skillId = actor.MasterSkillId;
                    cmd.skillId = skillId;
                }
                Skill skill = actor.getSkillManager().getSkill(skillId);
                if (skill != null)
                {
                    //首先选择参考目标
                    ViewUtils.Singleton.realFlow++;
                    skill.ShowID = ViewUtils.Singleton.realFlow;
                    skill.SelectTarget();

                    AttackUtils.TriggerExtraEffect(skill, true);
                    AttackUtils.ComputeMainEffect(skill);
                    AttackUtils.TriggerExtraEffect(skill, false);
                    //大招扣除怒气 / 普通攻击增加怒气
                    if (cmd.isMasterSkill)
                        actor.ChangeProperty(PropertyType.Mp, -BattleParam.MaxMp);
                    else
                        actor.ChangeProperty(PropertyType.Mp, actor.getProperty(PropertyType.AtkGetMp));

                    AttackUtils.CurMainEftRes = null;

                    //如何判断是最后一个
                    //1.最后一个将所有怪物致死 2.最后一个入列
                    //再次判断存活数量
                    aliveNum = FightManager.Singleton.Grid.AliveNum(AttackUtils.GetEnemyCamp(actor.getCamp()));
                    if (aliveNum <= 0)
                    {
                        cmd.isLastOne = true;
                        lastActorId = actor.getActorId();
                    }
                    else
                    {
                        if (FightManager.Singleton.IsLastOne(actor.getCamp(), actor.getActorId()))
                        {
                            cmd.isLastOne = true;
                            lastActorId = actor.getActorId();
                        }
                    }
                    
                    return true;
                }
                else
                {
                    Logger.err("使用了未注册的技能" + skillId);
                }
            }
            else
            {
                IsCurTurnClear = true;
            }
        }
        return false;
    }

    public FightCmd Peek()
    {
        if (cmdQueue.Count <= 0)
        {
            return null;
        }
        return cmdQueue.Peek();
    }

    public FightCmd Dequeue()
    {
        if (cmdQueue.Count <= 0)
        {
            return null;
        }
        return cmdQueue.Dequeue();
    }

    public void Reset()
    {
        Clear();
        CurCmdId = 0;
        CurExcuteId = 0;
        atkGap = 1.2f;
        isStop = false;
        IsCurTurnClear = false;
        lastActorId = 0;
        masterCmdNum = 0;
        normalCmdNum = 0;
    }

    public void Stop()
    {
        isStop = true;
    }

    public void Clear()
    {
        Stop();
        if (cmdQueue != null)
        {
            if (cmdQueue.Count > 0)
            {
                Logger.err("CmdMgr存在未执行的指令");
                foreach (FightCmd cmd in cmdQueue)
                {
                    Actor actor = ActorManager.Singleton.Get(cmd.actorId);
                    Logger.err(actor.Name);
                }
            }
            cmdQueue.Clear();
        }
    }

}
