using Data.Beans;
using System.Collections.Generic;

public class BattleStatistics : SingletonTemplate<BattleStatistics>
{
    //1=一般 2=不错 3=很好 4=完美一击

    public int normalCount;

    public int notBadCount;

    public int goodCount;

    public int perfectCount;


    /// <summary>
    /// 连击是否完成
    /// </summary>
    /// <returns></returns>
    public bool IsComboCmp()
    {
        t_dungeon_actBean bean = ConfigBean.GetBean<t_dungeon_actBean, int>(BattleService.Singleton.MissionId);
        if (bean != null)
        {
            int[] val = GTools.splitStringToIntArray(bean.t_star_hit);
            if (val != null && val.Length == 2)
            {
                int total = normalCount + notBadCount + goodCount + perfectCount;
                if (System.Enum.IsDefined(typeof(ComboType), val[0]))
                {
                    ComboType combo = (ComboType)val[0];
                    if (val[1] >= 10000)
                    {
                        int needCount = val[1] / 10000;
                        int count = 0;
                        if (combo == ComboType.Normal)
                            count = total;
                        else if (combo == ComboType.NotBad)
                            count = (notBadCount + goodCount + perfectCount);
                        else if (combo == ComboType.Good)
                            count = (goodCount + perfectCount);
                        else if (combo == ComboType.Perfect)
                            count = perfectCount;
                        if (count >= needCount)
                            return true;
                    }
                    else
                    {
                        float needRate = val[1] / 10000;
                        if (total <= 0)
                            total = 1;
                        float rate = 0;
                        if (combo == ComboType.Normal)
                            rate = 1;
                        else if (combo == ComboType.NotBad)
                            rate = (notBadCount + goodCount + perfectCount) / total;
                        else if (combo == ComboType.Good)
                            rate = (goodCount + perfectCount) / total;
                        else if (combo == ComboType.Perfect)
                            rate = perfectCount / total;
                        if (rate > needRate)
                            return true;
                    }
                }
                else
                {
                    Logger.err("关卡连击类型参数有误：" + val[0]);
                }
            }
            else
            {
                Logger.err("关卡连击参数有误：" + bean.t_star_hit);
            }
        }
        return false;
    }


    public bool IsAliveCmp()
    {
        t_dungeon_actBean bean = ConfigBean.GetBean<t_dungeon_actBean, int>(BattleService.Singleton.MissionId);
        if (bean != null)
        {
            int aliveNum = FightManager.Singleton.Grid.AliveNum(ActorCamp.CampFriend);
            int allNum = 0;
            List<int> list = FightService.Singleton.GetFrendPetStandPos();// Singleton.GetTeamList(ZhenRongType.Normal, false);
            if (list != null && list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i] > 0)
                        allNum++;
                }
            }
            int deadNum = allNum - aliveNum;
            if (deadNum < 0)
                deadNum = 0;
            if (deadNum <= bean.t_star_dead)
                return true;
        }
        return false;
    }

    public int GetStar()
    {
        int star = 1;
        if (IsAliveCmp())
            star++;
        if (IsComboCmp())
            star++;
        return star;
    }

    public void Clear()
    {
        normalCount = 0;
        notBadCount = 0;
        goodCount = 0;
        perfectCount = 0;
    }

}
