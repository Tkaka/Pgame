using Message.Fight;
using System.Collections.Generic;
using Data.Beans;

public class BaseFight
{
    public string SceneName { protected set; get; }   //当前战斗场景名
    public int MaxTurnNum { protected set; get; }      //当前最大回合数
    public int WaveCount { protected set; get; }       //当前战斗最大波数

    protected long m_curFightId;                //当前战斗实例ID
    protected EFightType m_curFightType;        //当前的战斗类型
    protected int m_curFightTypeParam;          //当前战斗类型参数
    protected List<FightParam> m_friendPetFightParam;   //友方宠物的战斗参数
    protected List<FightParam> m_enemyPetFightParam;    //敌方宠物战斗参数
    public BaseFight(ResFight msg)
    {
        m_curFightId = msg.fightId;
        m_curFightType = (EFightType)msg.fightType;
        m_curFightTypeParam = msg.fightTypeParam;
        m_friendPetFightParam = msg.petFightParams;
        m_enemyPetFightParam = msg.enemyFightParam;
    }

    //获得战斗实例ID
    public long GetFightId()
    {
        return m_curFightId;
    }

    //获得战斗参数
    public FightParam GetParam(ActorCamp camp, int petId)
    {
        List<FightParam> fightParams = camp == ActorCamp.CampFriend ? m_friendPetFightParam : m_enemyPetFightParam;
        if (fightParams == null)
            return null;
        for (int i = 0; i < fightParams.Count; i++)
        {
            if (petId == fightParams[i].petId)
            {
                return fightParams[i];
            }
        }
        return null;
    }

    //设置基础属性
    public virtual void SetActorBaseProperty(Actor actor)
    {
        List<FightParam> fightParams = actor.getCamp() == ActorCamp.CampFriend ? m_friendPetFightParam : m_enemyPetFightParam;
        if (fightParams == null || fightParams.Count == 0)
            return;

        for (int i = 0; i < fightParams.Count; i++)
        {
            if (actor.getTemplateId() == fightParams[i].petId)
            {
                List<FightProperty> fightProperties = fightParams[i].fightPropertys;
                for (int j = 0; j < fightProperties.Count; j++)
                {

                    FightProperty fightProperty = fightProperties[j];

                    //if (actor.getCamp() == ActorCamp.CampFriend)
                    //{
                    //    actor.setBaseProperty((PropertyType)fightProperty.propertyId, 9999);
                    //}
                    //else
                        actor.setBaseProperty((PropertyType)fightProperty.propertyId, LNumber.Create(fightProperty.propertyValue, 0));


                }
                break;

            }

        }
    }

    //设置当前值
    public virtual void SetCurProperty(Actor actor)
    {

    }

    //获得每波怪的名字
    public virtual string GetWaveName()
    {
        //默认为战斗参数为关卡的处理
        t_dungeon_actBean bean = ConfigBean.GetBean<t_dungeon_actBean, int>(BattleService.Singleton.MissionId);
        if (bean == null)
            return "";

        int[] names = GTools.splitStringToIntArray(bean.t_monster_name);
        int index = SpawnerHelper.Singleton.CurWave;
        if (names != null && index < names.Length)
        {
            int lanId = names[index];
            t_languageBean lanBean = ConfigBean.GetBean<t_languageBean, int>(lanId);
            if (lanBean != null)
                return lanBean.t_content;
            return lanId + "";
        }

        return "";
    }



    //获得自身宠物站位
    public virtual List<int> GetFrendPetStandPos()
    {
        List<int> posList = new List<int>();
        int[] arr = new int[6];
        for (int i = 0; i < m_friendPetFightParam.Count; i++)
        {
            int pos = m_friendPetFightParam[i].pos;
            arr[pos] = m_friendPetFightParam[i].petId;
        }

        posList.AddRange(arr);
        return posList;
    }

    //获得敌方宠物站位
    public virtual List<int> GetEnemyStandPos()
    {
        List<int> posList = new List<int>();
        int[] arr = new int[6];
        for (int i = 0; i < m_enemyPetFightParam.Count; i++)
        {
            FightParam param = m_enemyPetFightParam[i];
            if (param.pos < arr.Length)
            {
                arr[param.pos] = param.petId;
            }
            else
            {
                Debuger.Err("服务器发下的站位异常" + param.pos);
            }

        }

        posList.AddRange(arr);

        return posList;
    }


    //获得敌方先手值
    public virtual int GetWavePrecedeVal()
    {
        int precedeVal = 0;

        t_dungeon_actBean bean = ConfigBean.GetBean<t_dungeon_actBean, int>(BattleService.Singleton.MissionId);
        if (bean == null)
            return 0;

        if (SpawnerHelper.Singleton.CurWave == 0)
            precedeVal = bean.t_priority_value1;
        if (SpawnerHelper.Singleton.CurWave == 1)
            precedeVal = bean.t_priority_value2;
        if (SpawnerHelper.Singleton.CurWave == 2)
            precedeVal = bean.t_priority_value3;

        return precedeVal;
    }


    public virtual void InitEnemys(List<WaveTrigger> triggerList)
    {

    }

}