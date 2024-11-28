using Message.Fight;
using System.Collections.Generic;
using Data.Beans;

//克隆组队战的战斗数据
public class CloneDungeonFight : BaseFight
{
    public CloneDungeonFight(ResFight msg) : base(msg)
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

    public override List<int> GetEnemyStandPos()
    {
        int petId = 0;
        List<int> posList = new List<int>();
        if (m_enemyPetFightParam != null && m_enemyPetFightParam.Count > 0)
        {
            petId = m_enemyPetFightParam[0].petId;
        }
        else
        {
            Debuger.Err("克隆战没有收到对阵敌人数据");
        }

        //6个位置
        for (int i = 0; i < 6; i++)
        {
            posList.Add(petId);
        }

        return posList;
    }

    public override void InitEnemys(List<WaveTrigger> triggerList)
    {
        base.InitEnemys(triggerList);
        int RealWave = SpawnerHelper.Singleton.RealWave;
        if (triggerList != null && triggerList.Count > 0)
        {
            List<int> list = GetEnemyStandPos();
            if (RealWave >= 0 && RealWave < triggerList.Count)
                triggerList[RealWave].InitEnemyPets(list);
            else
                Logger.err("找不到刷怪点：" + RealWave);
        }
    }
}