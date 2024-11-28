using Data.Beans;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum ComboType
{
    Normal = 1,      //一般
    NotBad = 2,     //不错
    Good = 3,        //很棒
    Perfect = 4,      //完美 
}

public enum SUB_ADD
{
    Sub = -1,
    Add = 1,
}

public enum Percent_Int
{
    Percent,
    Int,
}

/// <summary>
/// 技能效果效果类型
/// 0=强化被动加属性 1=添加buff 2=附加A溅射N/百分比N 3=吸取防御力N%,自己拿M% 4=技能主效果伤害 
/// 5=技能主效果治疗（技能攻击力百分比*攻击者攻击力 + 技能数值）6=吸取攻击力N%，自己拿M%
/// 7=技能额外效果造成灼烧（灼烧伤害=技能攻击力百分比*攻击者攻击力 + 技能数值）
/// </summary>
public enum SkillEffectType
{
    PropertyChange = 0,          //强化被动加属性
    AddBuff = 1,                     //添加buff
    Sputtering = 2,                 //溅射类
    Absorb = 3,                      //吸取类
    MainEffectHurt = 4,           //技能主效果伤害
    MainEffectCure = 5,           //技能主效果伤治愈 
    MainEffectFiring = 6,         //技能主效果灼烧
    //ChangeSoul = 7,               //双子转魂
    FuHuo = 9,
}

public class AttackUtils
{

    public static MainEffectRes CurMainEftRes;

    /// <summary>
    /// 计算技能主效果
    /// </summary>
    /// <param name="skillId"></param>
    /// <param name="attacker"></param>
    public static void ComputeMainEffect(Skill skill)
    {
        if (skill == null)
        {
            Logger.err("AttackUtils:ComputeMainEffect:Skill is Null");
            return;
        }

        Actor attacker = skill.Owner;
        t_skillBean skillBean = skill.getSkillBean();
        if (attacker == null || skillBean == null)
        {
            Logger.err("AttackUtils:ComputeMainEffect:attacker or skillBean is Null");
            return;
        }

        t_skill_effectBean eftBean = ConfigBean.GetBean<t_skill_effectBean, int>(skillBean.t_main_effect_id);
        if (eftBean != null)
        {
            List<long> defenders = RangeSelector.Singleton.GetTargets(eftBean.t_scope_id, attacker, skill.TargetID, true);
            skill.defenders = defenders;
            bool hasBaoJi = false;
            bool hasGeDang = false;
            if (eftBean.t_effect_type == (int)SkillEffectType.MainEffectHurt)
            {
                for (int i = 0; i < defenders.Count; i++)
                {
                    Actor defender = ActorManager.Singleton.Get(defenders[i]);
                    if (defender == null)
                        continue;
                    defender.attackerId = attacker.getActorId();
                    CurMainEftRes = ComputeHurt(skill, defender);
                    CurMainEftRes.showId = skill.ShowID;

                    if (CurMainEftRes.IsCritical)
                        hasBaoJi = true;
                    if (CurMainEftRes.IsGeDang)
                        hasGeDang = true;

                    //受伤处理
                    OnHurt(skill, defender, CurMainEftRes);
                }
                if (hasBaoJi)
                {
                    TriggerParam param = TriggerParam.Create(TriggerEnum.OnBaoJi, attacker.getActorId());
                    TriggerManager.Singleton.OnEvtTriggered(param);
                }
                if (hasGeDang)
                {
                    TriggerParam param = TriggerParam.Create(TriggerEnum.OnBaoJi, attacker.getActorId());
                    TriggerManager.Singleton.OnEvtTriggered(param);
                }
            }
            else if (eftBean.t_effect_type == (int)SkillEffectType.MainEffectCure)
            {
                //回血
                for (int i = 0; i < defenders.Count; i++)
                {
                    Actor defender = ActorManager.Singleton.Get(defenders[i]);
                    if(defender != null)
                        ComputeCure(skill, defender);
                }
            }
        }
    }


    public static void OnHurt(Skill skill, Actor defender, MainEffectRes res)
    {
        if (skill == null)
        {
            Logger.err("AttackUtils触发效果时skill为空");
            return;
        }
        Actor attacker = skill.Owner;

        //扣血
        defender.ChangeProperty(PropertyType.Hp, -res.hurt);
        //Debug.LogError("------------------------>>>伤害改变" + res.hurt.raw);

        //受伤事件
        TriggerParam tparam = TriggerParam.Create(TriggerEnum.OnHurt, defender.getActorId());
        tparam.hurt = res.hurt;
        TriggerManager.Singleton.OnEvtTriggered(tparam);

        //受伤怒气
        defender.ChangeProperty(PropertyType.Mp, defender.getProperty(PropertyType.HurtGetMp));
        //受到伤害加怒气
        LNumber hpBase = defender.getBaseProperty(PropertyType.Hp);
        LNumber per = res.hurt / hpBase;
        LNumber mpVal = BattleParam.PercentHurtGetMp * per * 100;
        defender.ChangeProperty(PropertyType.Mp, mpVal);
        //Logger.err(defender.Name + " 逻辑受伤获得怒气:" + mpVal);
        if(defender.isActorType(ActorType.Boss))
            Logger.log(defender.Name + " 逻辑受伤时的怒气:" + defender.getProperty(PropertyType.Mp));

        //标记当前角色即将死亡
        if (defender.getProperty(PropertyType.Hp) <= 0)
        {
            defender.IsActuallyDead = true;

            //杀死一个单位获得怒气(战魂相关)
            LNumber val = attacker.getProperty(PropertyType.KillGetMp);
            attacker.ChangeProperty(PropertyType.Mp, val);

            //死亡事件
            tparam = TriggerParam.Create(TriggerEnum.OnDead, defender.getActorId());
            TriggerManager.Singleton.OnEvtTriggered(tparam);

            //标记死亡 
            ActorTurnStatus status = FightManager.Singleton.Grid.Get(defender.getCamp(), defender.GridId);
            if (status != null)
                status.IsActuallyDead = true;
            //切换选择对象
            if (defender.getActorId() == FightManager.Singleton.LastSeletedId)
                FightManager.Singleton.SelectAGoodEnemy();

            //死亡表现
            ShowDead dead = new ShowDead();
            dead.actorId = defender.getActorId();
            dead.timeNode = TimeNode.SkillCmp;
            ViewUtils.Singleton.AddShow(dead);

            //判断是否复活
            if (defender.getProperty(PropertyType.FuHuoLv) > 0)
            {
                defender.WillReborn = defender.BuffMgr.WillReborn(); 
            }

        }

        //计算本轮总伤害
        if (skill.Owner.isCampOf(ActorCamp.CampFriend))
        {
            FightManager.Singleton.CurTurnHurt += (long)res.hurt;
        }

        ShowHurtEffect showHurt = new ShowHurtEffect();
        showHurt.timeNode = TimeNode.Hurt;
        showHurt.actorId = defender.getActorId();
        showHurt.skillConfig = skill.SkillConfig;
        showHurt.mainEftRes = MainEffectRes.Clone(res);
        ViewUtils.Singleton.AddShow(showHurt);
    }

    /// <summary>
    /// 触发额外效果
    /// </summary>
    public static void TriggerExtraEffect(Skill skill, bool beforeMain)
    {
        List<EffectInfo> eftList = null;
        if (beforeMain)
            eftList = skill.beforeEffectList;
        else
            eftList = skill.afterEffectList;
        if (eftList != null && eftList.Count > 0)
        {
            for (int i = 0; i < eftList.Count; i++)
            {
                eftList[i].targetId = skill.TargetID;
                ApplyEffect(eftList[i]);
            }
        }

        //判断是否存在致命/斩灭buff, 如果存在触发一个额外效果
        if (beforeMain)
        {
            int behead = (int)skill.Owner.getProperty(PropertyType.HasBehead);
            if (behead > 0)
            {
                EffectInfo eftInfo = new EffectInfo();
                eftInfo.effectId = behead;
                eftInfo.level = 1;
                eftInfo.targetId = skill.TargetID;
                eftInfo.skill = skill;
                eftInfo.ownerId = skill.Owner.getActorId();
                ApplyEffect(eftInfo);
            }
        }
    }

    public static bool ApplyEffect(EffectInfo info, TriggerParam triggerParam=null)
    {
        t_skill_effectBean eftBean = ConfigBean.GetBean<t_skill_effectBean, int>(info.effectId);
        if (eftBean != null)
        {
            int skillLevel = Mathf.Max(0, info.level - 1);
            Actor attacker = ActorManager.Singleton.Get(info.ownerId);

            //判断条件是否达成
            bool isConditionMeet = true;
            //达成条件的索引
            int firstMeetConditionId = -1;
            if (!string.IsNullOrEmpty(eftBean.t_condition_id))
            {
                //如果包含减号则是或，加号则是与关系
                if (eftBean.t_condition_id.Contains('-'))
                {
                    int[] conditionArr = GTools.splitStringToIntArray(eftBean.t_condition_id, '-');
                    for (int i = 0; i < conditionArr.Length; i++)
                    {
                        if (conditionArr[i] <= 0)
                            continue;
                        if (ConditionJudger.Singleton.IsConditionMeet(conditionArr[i], attacker, info.targetId))
                        {
                            firstMeetConditionId = i;
                            isConditionMeet = true;
                            break;
                        }
                        else
                        {
                            isConditionMeet = false;
                        }
                    }
                }
                else
                {
                    int[] conditionArr = GTools.splitStringToIntArray(eftBean.t_condition_id);
                    for (int i = 0; i < conditionArr.Length; i++)
                    {
                        if (conditionArr[i] <= 0)
                            continue;
                        if (!ConditionJudger.Singleton.IsConditionMeet(conditionArr[i], attacker, info.targetId))
                            isConditionMeet = false;
                    }
                }
            }
            //条件未达成
            if (!isConditionMeet)
                return false;

            /**********************选择效果目标*****************************/
            //有些效果需要技能主效果目标(一定来自技能额外效果)
            List<long> defenders = null;
            if(info.skill != null)
                defenders = info.skill.defenders;
            if (eftBean.t_scope_id <= 0)
            {
                Logger.err(eftBean.t_id + "效果范围id为0");
            }
            t_skill_scopeBean scopeBean = ConfigBean.GetBean<t_skill_scopeBean, int>(eftBean.t_scope_id);
            if(scopeBean != null && scopeBean.t_range_type != (int)LineupType.SkillMainEffect)
            {
                defenders = RangeSelector.Singleton.GetTargets(eftBean.t_scope_id, attacker, info.targetId);  //被动效果目标就是自己
            }
            if (defenders.Count <= 0)
                Logger.log(info.effectId + "效果范围目标为空");
            /*************************************************************/

            SkillEffectType eftType = (SkillEffectType)eftBean.t_effect_type;
            //触发概率
            int rate = eftBean.t_rate_base + skillLevel * eftBean.t_rate_grow;
            for (int i = 0; i < defenders.Count; i++)
            {
                Actor defender = ActorManager.Singleton.Get(defenders[i]);
                if (defender == null)
                    continue;

                //每个目标单独计算概率
                //复活单独处理(不判断概率直接加上，概率作为死亡时候的复活概率)
                if (eftBean.t_effect_type != (int)SkillEffectType.FuHuo)
                {
                    //检查概率
                    Logger.log(info.effectId + "效果为 " + defender.Name +  " 附加效果的概率为：" + rate);
                    if (!WillOccur(rate))
                        continue;
                }
              
                switch (eftType)
                {
                    case SkillEffectType.PropertyChange:
                        //战斗外属性，不需要处理
                        break;
                    case SkillEffectType.FuHuo:
                        RebornBuff rbuff = new RebornBuff();
                        rbuff.triggerType = triggerParam.triggerType;
                        TwoParam<LNumber, LNumber> param = new TwoParam<LNumber, LNumber>();
                        param.value1 = skillLevel;
                        rbuff.ParseParam(info.effectId, param);
                        defender.BuffMgr.Add(rbuff);
                        break;
                    case SkillEffectType.AddBuff:
                        AddBuff(defender, info, triggerParam);
                        break;
                    case SkillEffectType.Sputtering:
                        Logger.log("溅射处理");
                        DoSputtering(defender, info);
                        break;
                    case SkillEffectType.Absorb:
                        Logger.log("吸取类处理");
                        DoAbsorb(attacker, defender, info, triggerParam);
                        break;
                    case SkillEffectType.MainEffectFiring:
                        Logger.log("灼烧处理");
                        FiringEffect(defender, info, attacker.getProperty(PropertyType.Atk), triggerParam);
                        break;
                    default:
                        Logger.err("未实现的被动效果类型" + info.effectId + "_" + eftType);
                        break;
                }
            }

            return true;
        }
        return false;
    }

    /// <summary>
    /// 处理溅射
    /// </summary>
    /// <param name="attacker"></param>
    /// <param name="defender"></param>
    /// <param name="effectId"></param>
    /// <param name="skillLevel"></param>
    public static void DoSputtering(Actor defender, EffectInfo info)
    {
        t_skill_effectBean eftBean = ConfigBean.GetBean<t_skill_effectBean, int>(info.effectId);
        if (eftBean != null)
        {
            int skillLevel = Mathf.Max(0, info.level - 1);
            int rate = eftBean.t_param1_base + skillLevel * eftBean.t_param1_grow;
            LNumber param = GTools.ScaleInt2LNumber(rate);
            LNumber resHurt = param * CurMainEftRes.hurt;

            MainEffectRes res = MainEffectRes.Clone(CurMainEftRes);
            res.hurt = resHurt;
            //受伤处理 
            OnHurt(info.skill, defender, res);
        }
    }

    /// <summary>
    /// 处理吸取(吸血不在此处理)
    /// </summary>
    /// <param name="defender"></param>
    /// <param name="effectId"></param>
    /// <param name="skillLevel"></param>
    public static void DoAbsorb(Actor attacker, Actor defender, EffectInfo info, TriggerParam triggerParam = null)
    {
        t_skill_effectBean eftBean = ConfigBean.GetBean<t_skill_effectBean, int>(info.effectId);
        if (eftBean != null)
        {
            int[] buffIds = new int[2];
            buffIds[0] = eftBean.t_buff_id;
            buffIds[1] = eftBean.t_extra_buff_id;
            Actor owner;
            int skillLevel = Mathf.Max(0, info.level - 1);
            for (int i = 0; i < 2; i++)
            {
                t_buffBean buffBean = ConfigBean.GetBean<t_buffBean, int>(buffIds[i]);
                if (buffBean != null)
                {
                    if (System.Enum.IsDefined(typeof(PropertyType), buffBean.t_property_id))
                    {
                        PropertyType ptype = (PropertyType)buffBean.t_property_id;
                        LNumber baseVal = defender.getBaseProperty(ptype);
                        Logger.log(defender.getTemplateId() + "__" + eftBean.t_id + " buff added......" + buffBean.t_id);
                        TwoParam<LNumber, LNumber> param = new TwoParam<LNumber, LNumber>();
                        //给defender添加debuff
                        if (i == 0)
                        {
                            param.value1 = GTools.ScaleInt2LNumber(eftBean.t_param1_base + skillLevel * eftBean.t_param1_grow);
                            param.value2 = eftBean.t_param2_base + skillLevel * eftBean.t_param2_grow;
                            param.value2 = baseVal * param.value1 + param.value2;
                            param.value1 = 0;
                            owner = defender;
                        }
                        //给自己添加buff
                        else
                        {
                            param.value1 = GTools.ScaleInt2LNumber(eftBean.t_param1_base + skillLevel * eftBean.t_param1_grow);
                            param.value2 = eftBean.t_param2_base + skillLevel * eftBean.t_param2_grow;
                            param.value2 = (baseVal * param.value1 + param.value2) * GTools.ScaleInt2LNumber(eftBean.t_param3);
                            param.value1 = 0;
                            owner = attacker;
                        }

                        if (buffBean.t_fun_type == (int)BuffType.ProperyBuff)
                        {
                            PropertyBuff pbuff = new PropertyBuff();
                            if (triggerParam != null)
                                pbuff.triggerType = triggerParam.triggerType;
                            pbuff.ParseParam(info.effectId, buffIds[i], param);
                            owner.BuffMgr.Add(pbuff);
                        }
                        else if (buffBean.t_fun_type == (int)BuffType.HotDotBuff)
                        {
                            HotDotBuff hotbuff = new HotDotBuff();
                            if (triggerParam != null)
                                hotbuff.triggerType = triggerParam.triggerType;
                            hotbuff.ParseParam(info.effectId, buffIds[i], param);
                            owner.BuffMgr.Add(hotbuff);
                        }
                        else
                        {
                            Logger.err("吸取时处理了未知属性类型：" + buffBean.t_fun_type);
                        }
                    }
                    else
                    {
                        Logger.err("吸取时处理了未知属性类型：" + buffBean.t_property_id);
                    }
                }//buff bean
            }//end if
        }//eft bean
    }

    /// <summary>
    /// 给目标添加buff
    /// </summary>
    /// <param name="owner"></param>
    /// <param name="effectId"></param>
    /// <param name="skillLevel"></param>
    public static void AddBuff(Actor owner, EffectInfo info, TriggerParam triggerParam = null)
    {
        t_skill_effectBean eftBean = ConfigBean.GetBean<t_skill_effectBean, int>(info.effectId);
        if (eftBean != null)
        {
            t_buffBean buffBean = ConfigBean.GetBean<t_buffBean, int>(eftBean.t_buff_id);
            if (buffBean != null)
            {
                int skillLevel = Mathf.Max(0, info.level - 1);
                //Logger.log(owner.getTemplateId() + "__" + eftBean.t_id + " buff added......" + buffBean.t_id);
                TwoParam<LNumber, LNumber> param = new TwoParam<LNumber, LNumber>();
                param.value1 = eftBean.t_param1_base + skillLevel * eftBean.t_param1_grow;
                param.value2 = eftBean.t_param2_base + skillLevel * eftBean.t_param2_grow;
                switch (buffBean.t_fun_type)
                {
                    case (int)BuffType.HotDotBuff:
                        HotDotBuff hotbuff = new HotDotBuff();
                        if (triggerParam != null)
                            hotbuff.triggerType = triggerParam.triggerType;
                        hotbuff.ParseParam(info.effectId, param);
                        owner.BuffMgr.Add(hotbuff);
                        break;
                    case (int)BuffType.ProperyBuff:
                        PropertyBuff pbuff = new PropertyBuff();
                        if (triggerParam != null)
                            pbuff.triggerType = triggerParam.triggerType;
                        pbuff.ParseParam(info.effectId, param);
                        owner.BuffMgr.Add(pbuff);
                        break;
                    case (int)BuffType.Behead:
                    case (int)BuffType.Mortal:
                        BeHeadBuff mortalBuff = new BeHeadBuff();
                        if (triggerParam != null)
                            mortalBuff.triggerType = triggerParam.triggerType;
                        mortalBuff.ParseParam(info.effectId, param);
                        owner.BuffMgr.Add(mortalBuff);
                        break;
                    case (int)BuffType.MarkBuff:
                        MarkBuff mBuff = new MarkBuff();
                        if (triggerParam != null)
                            mBuff.triggerType = triggerParam.triggerType;
                        param.value1 = eftBean.t_param3;
                        mBuff.ParseParam(info.effectId, param);
                        owner.BuffMgr.Add(mBuff);
                        break;
                    case (int)BuffType.ChangeSoul:
                        ChangeSoulBuff soulBuff = new ChangeSoulBuff();
                        if (triggerParam != null)
                            soulBuff.triggerType = triggerParam.triggerType;
                        soulBuff.ParseParam(info.effectId, null);
                        owner.BuffMgr.Add(soulBuff);
                        break;
                }
            }
        }
    }

    /// <summary>
    /// 灼烧效果
    /// </summary>
    public static void FiringEffect(Actor owner, EffectInfo info, LNumber attackerAtk, TriggerParam triggerParam = null)
    {
        t_skill_effectBean eftBean = ConfigBean.GetBean<t_skill_effectBean, int>(info.effectId);
        if (eftBean != null)
        {
            int skillLevel = Mathf.Max(0, info.level - 1);
            HotDotBuff hotbuff = new HotDotBuff();
            if (triggerParam != null)
                hotbuff.triggerType = triggerParam.triggerType;
            TwoParam<LNumber, LNumber> param = new TwoParam<LNumber, LNumber>();
            param.value1 = GTools.ScaleInt2LNumber(eftBean.t_param1_base + skillLevel * eftBean.t_param1_grow);
            param.value2 = eftBean.t_param2_base + skillLevel * eftBean.t_param2_grow;
            LNumber res = param.value1 * attackerAtk + param.value2;
            param.value2 = res;
            param.value1 = 0;
            hotbuff.ParseParam(info.effectId, param);
            owner.BuffMgr.Add(hotbuff);
        }
    }

    /// <summary>
    /// 计算治愈效果
    /// </summary>
    /// <param name="skill"></param>
    /// <returns></returns>
    public static void ComputeCure(Skill skill, Actor defender)
    {
        if (skill == null || skill.Owner == null || defender == null)
            return;
        Actor attacker = skill.Owner;
        //基础治疗值 = 施法者.攻击力*技能百分比 + 技能固定值
        LNumber atk = attacker.getProperty(PropertyType.Atk);
        LNumber cure = atk * skill.SkillEffectPer + skill.SkillEffectFixed;
        //最终治疗值 = 基础治疗值 * ( 1 + 治疗方.治疗率 ） * （ 1 + 被治疗方.治疗效果）
        //治疗的总值 = 治疗者的攻击力*（治疗者的技能伤害百分比 + 治疗者的技能数值 ）* （1 + 治疗者的伤害率）*（ 1 + 治疗者的治疗率 ） *（ 1 + 被治疗者的治疗效果）
        cure = cure * (1+ attacker.getProperty(PropertyType.ShangHaiLv)/10000) * 
                            (1 + attacker.getProperty(PropertyType.ZhiLiaoLv)/10000) * 
                            (1 + defender.getProperty(PropertyType.ZhiLiaoXiaoGuo)/10000);
        defender.ChangeProperty(PropertyType.Hp, cure);
        Logger.log(defender.Name + "被治愈时的血量" + defender.getProperty(PropertyType.Hp) + " 加了的血：" + cure);

        MainEffectRes res = new MainEffectRes();
        res.isHurt = false;
        res.cure = cure;
        res.skillId = skill.getTemplateId();
        res.showId = skill.ShowID;
        res.attackId = attacker.getActorId();

        ShowHurtEffect hurtEft = new ShowHurtEffect();
        hurtEft.actorId = defender.getActorId();
        hurtEft.mainEftRes = MainEffectRes.Clone(res);
        hurtEft.skillConfig = skill.SkillConfig;
        hurtEft.timeNode = TimeNode.Hurt;

        ViewUtils.Singleton.AddShow(hurtEft);
    }

    /// <summary>
    /// 暴击\格挡\免伤计算:
    /// 1:受击目标为多个时,分开计算;
    /// 2:先随机判定是否暴击,如果暴击,则不再判断格挡,伤害计算为暴击伤害;
    /// 3:如果没暴击,则判断是否格挡;如果格挡,伤害计算为格挡伤害;
    /// 4:如果没有格挡,那就是一次普通伤害;
    /// 5:将不同类型的伤害针对伤害率和免伤率进行伤害计算;
    /// </summary>
    /// <param name="skill"></param>
    /// <param name="defender"></param>
    public static MainEffectRes ComputeHurt(Skill skill, Actor defender)
    {
        MainEffectRes res = new MainEffectRes();
        if (skill == null || defender == null)
            return res;

        res.attackId = skill.Owner.getActorId();
        res.skillId = skill.getTemplateId();
        //命中判定
        Actor attacker = skill.Owner;
        t_skillBean skillBean = skill.getSkillBean();

        LNumber hurt = 0;

        //基础伤害 = 攻击 - f(Lv差) * 防御；最小值为1;
        //Lv差为受击宝贝 - 攻击宝贝，每个受击宝贝单独计算；
        //f(Lv差）PVP时 = 1，PVE时，20级为满；每级增加0.025; 配置为万分比,250
        int lvDiff = 1;
        LNumber baseHurt = attacker.getProperty(PropertyType.Atk) - lvDiff * defender.getProperty(PropertyType.Def);
        //Logger.err(attacker.getProperty(PropertyType.Atk) + "__" + defender.getProperty(PropertyType.Def));
        if (baseHurt < 1)
            baseHurt = 1;

        //判断是否暴击(最终暴击率 = 攻.暴击率 - 防.抗爆率;然后以最终暴击率算概率)
        LNumber baoJiLv = attacker.getProperty(PropertyType.BaoJiLv) - defender.getProperty(PropertyType.KangBaoJiLv);
        if (WillOccurL(baoJiLv))
        {
            Logger.log(attacker.Name + "攻击" + defender.Name + "时暴击了");
            res.IsCritical = true;
            TriggerParam tparam = TriggerParam.Create(TriggerEnum.OnBeiBaoJi, defender.getActorId());
            TriggerManager.Singleton.OnEvtTriggered(tparam);
            //暴击伤害 = （基础伤害 * 技能伤害系数 + 技能伤害值）*( 1 + 基础暴击伤害倍率 + 攻.暴击强度 )
            hurt = (baseHurt * skill.SkillEffectPer + skill.SkillEffectFixed) *
                (1 + BattleParam.BaseBaoJiBeiLv + attacker.getBaseProperty(PropertyType.BaoJiQiangDu)/10000);
        }
        else
        {
            //判断是否格挡(最终格挡率 = 防.格挡率 - 攻.破击率; 然后以最终格挡率算概率)
            LNumber geDangLv = defender.getProperty(PropertyType.GeDangLv) - attacker.getProperty(PropertyType.PoJiLv);
            if (WillOccurL(geDangLv))
            {
                Logger.log(attacker.Name + "攻击" + defender.Name + "时格挡了");
                res.IsGeDang = true;
                TriggerParam tparam = TriggerParam.Create(TriggerEnum.OnGeDang, defender.getActorId());
                TriggerManager.Singleton.OnEvtTriggered(tparam);
                //格挡伤害 = （基础伤害* 技能伤害系数 +技能伤害值）*(1 - 基础格挡伤害减免 - 防.格挡强度); 最小值为1;
                hurt = (baseHurt * skill.SkillEffectPer + skill.SkillEffectFixed) *
                   (1 - BattleParam.BaseGeDangJianMian - defender.getBaseProperty(PropertyType.GeDangQiangDu)/10000);
            }
            else
            {
                //普通伤害 = （基础伤害 * 技能伤害系数 + 技能伤害值）
                hurt = baseHurt * skill.SkillEffectPer + skill.SkillEffectFixed;
            }
        }

        //对攻防技-伤害率加成
        LNumber hurtRate = attacker.getProperty(PropertyType.ShangHaiLv);
        if (defender.IsGrowType(PetType.Atk))
            hurtRate += attacker.getProperty(PropertyType.DuiGongShangHaiLv);
        else if (defender.IsGrowType(PetType.Def))
            hurtRate += attacker.getProperty(PropertyType.DuiFangShangHaiLv);
        else if (defender.IsGrowType(PetType.Skill))
            hurtRate += attacker.getProperty(PropertyType.DuiJiShangHaiLv);

        //对攻防技-免伤率加成
        LNumber antiHurt = defender.getProperty(PropertyType.MianShangLv);
        if (attacker.IsGrowType(PetType.Atk))
            antiHurt += defender.getProperty(PropertyType.DuiGongMianShangLv);
        else if (attacker.IsGrowType(PetType.Def))
            antiHurt += defender.getProperty(PropertyType.DuiFangMianShangLv);
        else if (attacker.IsGrowType(PetType.Skill))
            antiHurt += defender.getProperty(PropertyType.DuiJiMianShangLv);

        //最终伤害 = 上述伤害 * (1 + 攻.伤害率 - 防.免伤率);
        hurt = hurt * (1 + hurtRate / 10000 - antiHurt / 10000);
        //Logger.err(attacker.getProperty(PropertyType.ShangHaiLv) + "__" + defender.getProperty(PropertyType.MianShangLv));


        //如果是绝技
        if (skill.IsMasterSkill())
        {
            //绝技伤害 = 最终伤害 * (1 + 攻击方.绝技伤害率 - 防御方.绝技防御率); 最小值为1;
            hurt = hurt * (1 + attacker.getProperty(PropertyType.JueJiShangHaiLv) - attacker.getProperty(PropertyType.JueJiFangYuLv));
        }


        hurt *= FightManager.ComboAdd;
        //Logger.log("combo add:" + FightManager.ComboAdd);

        //伤害加成1处理
        hurt = hurt * (1 + attacker.getProperty(PropertyType.HurtAdd1) / 10000);
        hurt = hurt * (1 + attacker.getProperty(PropertyType.HurtAdd2) / 10000);

        //最小值为1;
        if (hurt < 1)
            hurt = 1;

        if(GameManager.Singleton.IsDebug &&
            attacker.isCampOf(ActorCamp.CampFriend))
            hurt = GameManager.Singleton.DebugInfo.petAtk;

        if (GameManager.Singleton.IsDebug &&
            attacker.isCampOf(ActorCamp.CampEnemy))
            hurt = GameManager.Singleton.DebugInfo.monsterAtk;

        res.hurt = hurt;
        return res;
    }


    public static int[] GetSkillHurt(int skillLevel, t_skillBean bean)
    {
        int[] arr = new int[2];
        if (bean == null)
            return arr;
        t_skill_effectBean eftBean = ConfigBean.GetBean<t_skill_effectBean, int>(bean.t_main_effect_id);
        if (eftBean != null)
        {
            int level = Mathf.Max(0, skillLevel - 1);
            arr[0] = eftBean.t_param1_base + level * eftBean.t_param1_grow;
            arr[1] = eftBean.t_param2_base + level * eftBean.t_param2_grow;
        }
        return arr;
    }

    private static Dictionary<ColorElement, Color32> color32Dic;
    public static Color32 GetHitColor(ColorElement element)
    {
        if (color32Dic == null)
        {
            color32Dic = new Dictionary<ColorElement, Color32>();
            color32Dic.Add(ColorElement.Blue01, new Color32(0, 118, 219, 255));
            color32Dic.Add(ColorElement.Yellow01, new Color32(255, 205,12, 255));
            color32Dic.Add(ColorElement.Purple01, new Color32(83, 0, 117, 255));
            color32Dic.Add(ColorElement.Purple02, new Color32(205, 83, 255, 255));
            color32Dic.Add(ColorElement.Orange01, new Color32(255, 90, 0, 255));
            color32Dic.Add(ColorElement.Green01, new Color32(0, 214, 1, 255));
            color32Dic.Add(ColorElement.Green02, new Color32(184, 255, 116, 255));
            color32Dic.Add(ColorElement.White01, new Color32(255, 255, 255, 255));
        }
        if (color32Dic.ContainsKey(element))
        {
            return color32Dic[element];
        }
        return Color.white;
    }


    /// <summary>
    /// 获取攻击技能id
    /// </summary>
    /// <param name="actor"></param>
    /// <param name="isMasterSkill"></param>
    /// <returns></returns>
    public static int WillAtkSkillId(Actor actor, out bool willSmallSkill)
    {
        int skillId = 0;
        willSmallSkill = false;
        if (actor.isActorType(ActorType.Pet) || actor.isActorType(ActorType.Boss))
        {
            LNumber rate = actor.getProperty(PropertyType.SmallSkillRateAdd) + BattleParam.SmallSkillRate;
            if (WillOccurL(rate))
            {
                skillId = actor.SmallSkillId;
                willSmallSkill = true;
            }
            else
            {
                skillId = actor.NormalSkillId;
            }
        }
        else if (actor.isActorType(ActorType.Monster))
        {
            skillId = UIUtils.GetNormalAttackSkillID(actor.getTemplateId());
        }
        return skillId;
    }

    public static ActorCamp GetEnemyCamp(ActorCamp camp)
    {
        if (camp == ActorCamp.CampEnemy)
            return ActorCamp.CampFriend;
        else
            return ActorCamp.CampEnemy;
    }


    /// <summary>
    /// 根据配置表(相对阵营) 和 自身阵营 == 获取相应的阵营
    /// </summary>
    /// <param name="camp">配置表阵营</param>
    /// <param name="selfCamp"></param>
    /// <returns></returns>
    public static ActorCamp GetCamp(int camp, ActorCamp selfCamp)
    {
        ActorCamp configCamp = (ActorCamp)camp;
        if (configCamp == ActorCamp.CampFriend)
            return selfCamp;
        else if (configCamp == ActorCamp.CampEnemy)
            return GetEnemyCamp(selfCamp);
        return ActorCamp.CampAll;
    }


    /// <summary>
    /// 判断条件是否达成
    /// </summary>
    /// <param name="symbol">比较符号</param>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static bool Compare(CompareSymbol symbol, int a, int b)
    {
        switch (symbol)
        {
            case CompareSymbol.Greater:
                return a > b;
            case CompareSymbol.GreaterOrEqual:
                return a >= b;
            case CompareSymbol.Equal:
                return a == b;
            case CompareSymbol.Smaller:
                return a < b;
            case CompareSymbol.SmallerOrEqual:
                return a <= b;
            default:
                Logger.err("ConditionJudger:Compare:未知的比较符号:" + symbol);
                break;
        }
        return false;
    }

    public static bool Compare(CompareSymbol symbol, double a, double b)
    {
        switch (symbol)
        {
            case CompareSymbol.Greater:
                return a > b;
            case CompareSymbol.GreaterOrEqual:
                return a >= b;
            case CompareSymbol.Equal:
                return a == b;
            case CompareSymbol.Smaller:
                return a < b;
            case CompareSymbol.SmallerOrEqual:
                return a <= b;
            default:
                Logger.err("ConditionJudger:Compare:未知的比较符号");
                break;
        }
        return false;
    }

    public static void DropSoul(long targetId, Actor defender, SoulBallType soulType, LNumber val)
    {
        Transform trans = defender.monoBehavior.headBar;
        Vector3 pos = Vector3.zero;
        if (trans != null)
            pos = trans.position;

        GameObject obj = null;
        SoulBall soulBall = null;
        if (soulType == SoulBallType.Blue)
        {
            //obj = Res.Singleton.InstantiateCEffect("UI/eff_hunzhu_blue_small", pos);
            obj = FightManager.R.LoadGo("eff_hunzhu_blue_small", pos);
            soulBall = obj.AddComponent<SoulBall>();
            soulBall.Init(targetId, SoulBallType.Blue, val);
            FightManager.Singleton.soulBallList.Add(soulBall);
        }
        else if (soulType == SoulBallType.Red)
        {
            //obj = Res.Singleton.InstantiateCEffect("UI/eff_hunzhu_red_small", pos);
            obj = FightManager.R.LoadGo("eff_hunzhu_red_small", pos);
            soulBall = obj.AddComponent<SoulBall>();
            soulBall.Init(targetId, SoulBallType.Red, val);
            FightManager.Singleton.soulBallList.Add(soulBall);
        }
        else
        {
            Logger.err("未知的魂珠类型" + soulType);
        }
    }

    /// <summary>
    /// 效果唯一id
    /// </summary>
    /// <param name="camp">阵营</param>
    /// <param name="eftType">效果来源（技能，战魂，天赋）</param>
    /// <param name="index">技能id | 战魂id | 天赋id</param>
    /// <param name="gridId">站位格子id</param>
    /// <returns></returns>
    public static long GetEffectId(ActorCamp camp, EffectType eftType, int index, int gridId)
    {
        return (((long)camp & 0x000000000000FFFF) << 48) | 
                  (((long)eftType & 0x00000000000000FF) << 40) |
                  (((long)index & 0x00000000FFFFFFFF) << 8) |
                  (((long)gridId & 0x00000000000000FF));
    }


    public static bool WillOccur(int prob, int maxNum = 10000, int minNum = 1)
    {
        int res = /*FightManager.Singleton.randomExt.NextInt(maxNum + 1 - minNum); */UnityEngine.Random.Range(minNum, maxNum + 1);
        if (res <= prob)
            return true;
        else
            return false;
    }

    public static bool WillOccurL(LNumber prob, int maxNum = 10000, int minNum = 1)
    {
        int res = /*FightManager.Singleton.randomExt.NextInt(maxNum + 1 - minNum);*/UnityEngine.Random.Range(minNum, maxNum + 1);
        if (res <= prob)
            return true;
        else
            return false;
    }

}
