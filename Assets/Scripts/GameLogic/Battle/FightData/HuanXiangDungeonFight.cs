using Message.Fight;
using System.Collections.Generic;
using Data.Beans;

//幻象本的战斗数据
public class HuanXiangDungeonFight : BaseFight
{
    public HuanXiangDungeonFight(ResFight msg) : base(msg)
    {
        m_curFightTypeParam = ChallegeService.Singleton.GetActivityActId(m_curFightType, m_curFightTypeParam);
        BattleService.Singleton.Init(m_curFightTypeParam);
        MaxTurnNum = BattleParam.MaxTurn;

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
                    string[] arrMonster = GTools.splitString((string)obj, ';');

                    int index = ChallegeService.Singleton.GetHuanxiangChallengeIndex();
                    if (arrMonster != null && arrMonster.Length > index)
                    {
                        triggerList[i + StartWave].InitMonster(arrMonster[index]);
                    }
                }

            }
        }
    }
}