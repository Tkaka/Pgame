using Data.Beans;
using System.Collections.Generic;

/**
 * 哪些地方会触发表现
 * 1.回合开始：1.1buff每回合生效 1.2 回合开始事件  可能的效果类型（扣血 加血 加buff 死亡 复活 等等）
 * 2.技能A：（扣血 加血 加buff 死亡 等等）
 * 3.技能B：（扣血 加血 加buff 死亡 等等）
 * ........
 * ........
 * 6.回合结束
 * 
 * 事件
 * 事件一定是由某个技能造成的？（除回合开始外）
 * 
 * 表现时间点：
 * 1.回合开始 2.第一个hurt关键帧 3.buff关键帧 4.技能完毕（死亡）
 * 
 */

/// <summary>
/// 技能主效果
/// </summary>
public class MainEffectRes
{
    public int showId;                        //显示id
    public int skillId;                          //技能id
    public long attackId;                    //攻击者id
    public bool isHurt = true;                       //是否是伤害技能
    public LNumber hurt;                   //伤害值
    public LNumber cure;                   //治愈值
    public bool IsCritical = false;        //是否暴击
    public bool IsGeDang = false;       //是否格挡
    public LNumber hurtGetMp;          //伤害获得的能量（不包含BattleParam.HurtGetMp）

    public LNumber GetVal()
    {
        if (isHurt)
            return hurt;
        else
            return cure;
    }
    
    public void Clear()
    {
        showId = 0;
        skillId = 0;
        attackId = 0;
        hurt = 0;
        cure = 0;
        isHurt = true;
        IsCritical = false;
        IsGeDang = false;
        hurtGetMp = 0;
    }

    public static MainEffectRes Clone(MainEffectRes res)
    {
        MainEffectRes eftRes = new MainEffectRes();
        eftRes.showId = res.showId;
        eftRes.attackId = res.attackId;
        eftRes.skillId = res.skillId;
        eftRes.isHurt = res.isHurt;
        eftRes.hurt = res.hurt;
        eftRes.cure = res.cure;
        eftRes.IsCritical = res.IsCritical;
        eftRes.IsGeDang = res.IsGeDang;
        eftRes.hurtGetMp = res.hurtGetMp;
        return eftRes;
    }

}

public class ShowBase
{

    public long actorId;

    public TimeNode timeNode = TimeNode.TurnStart;

    public virtual void Show()
    {

    }

    public virtual string Print()
    {
        return "";
    }

}


/// <summary>
/// 表现的时间节点
/// </summary>
public enum TimeNode
{
    TurnStart,                       //每轮开始
    MainEffectBefore,            //技能主效果之前
    Hurt,                             //播放动画的时候
    FirstHurtKeyframe,         //第一个伤害关键帧
    Buffkeyframe,                //buff关键帧
    SkillCmp,                      //技能结束
}

public class ViewUtils : SingletonTemplate<ViewUtils>
{

    /// <summary>
    /// 经过预判之后的flow
    /// </summary>
    public int realFlow;

    /// <summary>
    /// 当前表现的flow
    /// </summary>
    //public int curFlow;

    private Dictionary<int, Dictionary<TimeNode, List<ShowBase>>> showDic = new Dictionary<int, Dictionary<TimeNode, List<ShowBase>>>();

    public void AddShow(ShowBase show)
    {
        if (showDic.ContainsKey(realFlow))
        {
            Dictionary<TimeNode, List<ShowBase>> timeDic = showDic[realFlow];
            if (timeDic.ContainsKey(show.timeNode))
            {
                List<ShowBase> list = timeDic[show.timeNode];
                list.Add(show);
            }
            else
            {
                List<ShowBase> list = new List<ShowBase>();
                list.Add(show);
                timeDic.Add(show.timeNode, list);
            }
        }
        else
        {
            Dictionary<TimeNode, List<ShowBase>> timeDic = new Dictionary<TimeNode, List<ShowBase>>();
            List<ShowBase> list = new List<ShowBase>();
            list.Add(show);
            timeDic.Add(show.timeNode, list);
            showDic[realFlow] = timeDic;
        }
    }

    public void Show(int showID, TimeNode timeNode)
    {
        if (showDic.ContainsKey(showID))
        {
            Dictionary<TimeNode, List<ShowBase>> dic = showDic[showID];
            if (dic.ContainsKey(timeNode))
            {
                List<ShowBase> list = dic[timeNode];
                if (list != null && list.Count > 0)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        list[i].Show();
                    }
                    list.Clear();
                }
            }
        }
    }

    public TimeNode GetTimeNode(TriggerEnum triggerType)
    {
        switch (triggerType)
        {
            case TriggerEnum.OnBaoJi:
            case TriggerEnum.OnBeiBaoJi:
            case TriggerEnum.OnGeDang:
            case TriggerEnum.OnBeiGeDang:
                return TimeNode.FirstHurtKeyframe;
            case TriggerEnum.OnDebuff:
                return TimeNode.Buffkeyframe;
            case TriggerEnum.OnHurt:
                return TimeNode.Hurt;
            case TriggerEnum.OnDead:          //统一到技能完成时死亡
                return TimeNode.SkillCmp;
            case TriggerEnum.OnTurnStart:
                return TimeNode.TurnStart;
        }
        return TimeNode.Hurt;
    }

    public void Clear()
    {
        //curFlow = 0;
        realFlow = 0;
        int total = 0;
        foreach (int key in showDic.Keys)
        {
            Dictionary<TimeNode, List<ShowBase>> dic = showDic[key];
            foreach (TimeNode nodeKey in dic.Keys)
            {
                List<ShowBase> list = dic[nodeKey];
                if (list != null && list.Count > 0)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        Logger.err(key + " 存在未表现的效果:" + list[i].ToString() + "_ " + nodeKey + "_" + list[i].Print());
                    }
                    total += list.Count;
                    list.Clear();
                }
            }
        }
        if (total > 0)
        {
            Logger.err("存在未表现的效果:" + total);
        }

    }

}