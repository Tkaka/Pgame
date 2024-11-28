using Message.Fight;
using System.Collections.Generic;
using Data.Beans;

//公会boss的战斗数据
public class GuildDungeonFight : BaseFight
{
    public GuildDungeonFight(ResFight msg) : base(msg)
    {
        SceneName = "dungeons01_mysl_02_new";
        WaveCount = 1;
        MaxTurnNum = BattleParam.MaxTurn;
    }

    public override string GetWaveName()
    {
        int petId = 0;
        if (m_enemyPetFightParam != null && m_enemyPetFightParam.Count > 0)
        {
            petId = m_enemyPetFightParam[0].petId;
        }

        t_petBean petBean = ConfigBean.GetBean<t_petBean, int>(petId);
        if (petBean != null)
            return petBean.t_name;
        else
            return "";
    }

    public override int GetWavePrecedeVal()
    {
        return 0;
    }

    public override void InitEnemys(List<WaveTrigger> triggerList)
    {
        t_guild_bossBean guildBossBean = ConfigBean.GetBean<t_guild_bossBean, int>(m_curFightTypeParam);
        int RealWave = SpawnerHelper.Singleton.RealWave;
        if (guildBossBean != null)
        {
            if (m_enemyPetFightParam.Count > 0)
            {
                FightParam fightParam = m_enemyPetFightParam[0];
                if (RealWave >= 0 && RealWave < triggerList.Count)
                {
                    triggerList[RealWave].InitSingleMonster(guildBossBean.t_pet, fightParam.pos);
                }
            }

 

        }
    }


    public override void SetCurProperty(Actor actor)
    {
        base.SetCurProperty(actor);

        if (actor.getCamp() == ActorCamp.CampEnemy)
        {
            //公会boss的属性是当前血量
            if (m_enemyPetFightParam.Count > 0)
            {
                FightParam fightParam = m_enemyPetFightParam[0];
                List<FightProperty> fightProperties = fightParam.fightPropertys;
                if (fightProperties.Count > 0)
                {
                    FightProperty fightProperty = fightProperties[0];
                    actor.SetProperty(PropertyType.Hp, LNumber.Create_Row(fightProperty.propertyValue));
                }
            }
             
        }
    }
}