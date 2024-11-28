
using Data.Beans;
using UnityEngine;

public class BossEnterState : FightBaseState
{

    public override void onEnter(object obj = null)
    {
        base.onEnter(obj);
        ShowBoss();
    }

    private Actor boss = null;
    private void ShowBoss()
    {
        t_dungeon_actBean bean = ConfigBean.GetBean<t_dungeon_actBean, int>(BattleService.Singleton.MissionId);
        if (bean != null && bean.t_boss_id > 0)
        {
            t_monster_boosBean bossBean = ConfigBean.GetBean<t_monster_boosBean, int>(bean.t_boss_id);
            if (bossBean != null)
            {
                ActorTurnStatus status = fm.Grid.GetByTemplateId(ActorCamp.CampEnemy, bean.t_boss_id);
                if (status != null)
                {
                    Transform trans = SpawnerHelper.Singleton.BossShot;
                    if (trans != null)
                    {
                        ActorParam actorParam = ActorParam.create(trans.position, trans.forward);
                        boss = ActorManager.Create(bossBean.t_id, ActorType.Boss, ActorCamp.CampEnemy, actorParam, 0, false);
                        if (boss != null)
                        {
                            //播放出场动作
                            string aniName = AniName.chuchang.ToString();
                            if (boss.GetActionManager().IsExist(aniName))
                            {
                                BattleWindow.Singleton.ToogleVisible(false);
                                //隐藏其他角色
                                ActorManager.Singleton.ToggleVisible(ActorCamp.CampAll, false);
                                boss.GetActionManager().PlayCommonAnimation(AniName.chuchang.ToString(), OnAniCmp);
                                //将镜头节点active设置为true
                                if (boss.monoBehavior.entranceShot != null)
                                {
                                    VirtualCameraMgr.Singleton.ToggleCameraCulling(false);
                                    boss.monoBehavior.entranceShot.SetActive(true);
                                }
                                return;
                            }
                            else
                            {
                                Logger.err(boss.Name + " 没有出场动作");
                                boss.destoryMe();
                            }
                        }
                    }
                    else
                    {
                        Logger.err(bossBean.t_name + " 找不到出场动画挂点");
                    }
                }
            }
        }
        fm.ChangeState(FightState.PrepareNextTurnState, ChangeReason.BossBriefCmp);
    }

    private void OnAniCmp(int key)
    {
        VirtualCameraMgr.Singleton.ToggleCameraCulling(true);
        if (boss != null)
        {
            boss.monoBehavior.entranceShot.SetActive(false);
            boss.getStateMachine().changeState(ActorState.idle);
            boss.destoryMe();
            boss = null;
        }

        fm.ChangeState(FightState.PrepareNextTurnState, ChangeReason.BossBriefCmp);
    }

    public override void onLeave(string stateKey)
    {
        base.onLeave(stateKey);
        ActorManager.Singleton.ToggleVisible(ActorCamp.CampAll, true);
        BattleWindow.Singleton.ToogleVisible(true);
    }

    public override string getStateKey()
    {
        return FightState.AutoAttack; ;
    }

}
