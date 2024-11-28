using Message.Fight;
using System.Collections.Generic;
using Data.Beans;

//终极试炼的战斗数据
public class UltimateTrialFight : BaseFight
{
    public UltimateTrialFight(ResFight msg) : base(msg)
    {
        SceneName = "dungeons01_mysl_02_new";
        WaveCount = 1;
        MaxTurnNum = BattleParam.MaxTurn;
    }

    public override string GetWaveName()
    {
        return "终极试炼";
    }

    public override int GetWavePrecedeVal()
    {
        return 0;
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