using Message.Fight;
using System.Collections.Generic;
using Data.Beans;

//经验本的战斗数据
public class ExpDungeonFight : BaseFight
{
    public ExpDungeonFight(ResFight msg) : base(msg)
    {
        m_curFightTypeParam = ChallegeService.Singleton.GetActivityActId(m_curFightType, m_curFightTypeParam);
        BattleService.Singleton.Init(m_curFightTypeParam);
        MaxTurnNum = BattleParam.ExpOrCoinMaxTurn;

        WaveCount = BattleService.Singleton.WaveCount;
        SceneName = BattleService.Singleton.GetBattleScene();
    }

    public override void InitEnemys(List<WaveTrigger> triggerList)
    {
        base.InitEnemys(triggerList);
        int StartWave = SpawnerHelper.Singleton.StartWave;
        t_dungeon_actBean bean = ConfigBean.GetBean<t_dungeon_actBean, int>(BattleService.Singleton.MissionId);
        if (bean != null)
        {
            for (int i = 0; i < triggerList.Count - StartWave; i++)
            {
                //t_wave_monster1
                System.Reflection.PropertyInfo proInfo = bean.GetType().GetProperty("t_wave_monster" + (i + 1));
                if (proInfo != null)
                {
                    System.Object obj = proInfo.GetValue(bean, null);
                    triggerList[i + StartWave].InitMonster((string)obj);
                }

            }
        }
    }

    public override void SetCurProperty(Actor actor)
    {
        base.SetCurProperty(actor);

        //经验怪不能攻击
        if(actor.getCamp() == ActorCamp.CampEnemy)
            actor.SetProperty(PropertyType.CanAttack, 1);
    }
}