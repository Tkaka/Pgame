
using Data.Beans;
using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class FightBossIntroduceState : FightBaseState
{

    public override void onEnter(object obj = null)
    {
        base.onEnter(obj);
        //TODO:判断是否是新boss
        //CoroutineManager.Singleton.delayedCall(1, DelayCall);
        CoroutineManager.Singleton.startCoroutine(ShowBoss());
    }

    private IEnumerator ShowBoss()
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
                    BattleWindow.Singleton.ToogleVisible(false);
                    BattleWindow.Singleton.OpenChild<CatchPetWarningWindow>(WinInfo.Create(false, null, false));
                    //判断是否有稀有宠物
                    Actor rareActor = ActorManager.Singleton.Get(status.actorId);
                    PlayableDirector shot = null;
                    if (rareActor != null)
                    {
                        shot = rareActor.monoBehavior.catchShot;
                        if (shot != null)
                        {
                            //隐藏战斗UI 
                            //BattleWindow.Singleton.ToogleVisible(false);
                            shot.Play();
                            //Logger.err("shot duration: " + shot.duration);
                            yield return new WaitForSeconds((float)shot.duration);
                        }
                    }
                }
            }
            else
            {
                StartNextTurn();
            }
        }
        else
        {
            StartNextTurn();
        }
    }

    private void DelayCall1()
    {
        t_dungeon_actBean bean = ConfigBean.GetBean<t_dungeon_actBean, int>(BattleService.Singleton.MissionId);
        if (bean != null && bean.t_boss_id > 0)
        {
            t_monster_boosBean bossBean = ConfigBean.GetBean<t_monster_boosBean, int>(bean.t_boss_id);
            if (bossBean != null)
            {
                GED.ED.addListenerOnce(EventID.OnBossBriefWindowClose, OnBossBriefWindowClose);
                BattleWindow.Singleton.OpenChild<BossBriefWindow>(WinInfo.Create(false, null, false, bean.t_boss_id));
            }
            else
            {
                StartNextTurn();
            }
        }
        else
        {
            StartNextTurn();
        }
    }


    private void OnBossBriefWindowClose(GameEvent evt)
    {
        StartNextTurn();
    }

    private void StartNextTurn()
    {
        fm.PrepareNextTurn(ChangeReason.BossBriefCmp);
        fm.CurTurn = ActorCamp.CampFriend;
    }

    public override string getStateKey()
    {
        return FightState.BossIntroduce;
    }

}