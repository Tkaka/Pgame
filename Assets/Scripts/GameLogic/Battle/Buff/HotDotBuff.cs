/// <summary>
/// 持续性的属性buff（HP，MP，中毒，流血，回复）
/// </summary>
public class HotDotBuff : BaseBuff
{
    private LNumber perValue;

    private LNumber intValue;

    protected bool isBaseProperty;           //是否对基础属性进行操作

    protected PropertyType propertyType;      //由读表获得（因为表现规则不一样）

    protected LNumber resultVal;                     //最终影响值

    public override void ParseParam(int effectId, TwoParam<LNumber, LNumber> param)
    {
        base.ParseParam(effectId, param);
        InitValue(param);
    }

    public override void ParseParam(int effectId, int buffId, TwoParam<LNumber, LNumber> param)
    {
        base.ParseParam(effectId, buffId, param);
        InitValue(param);
    }

    private void InitValue(TwoParam<LNumber, LNumber> param)
    {
        if (EffectBean != null)
        {
            perValue = param.value1/10000;
            intValue = param.value2;
        }
        if (BuffBean != null)
        {
            if (BuffBean.t_base_cur == 1)
                isBaseProperty = false;
            else
                isBaseProperty = true;
        }
        if (System.Enum.IsDefined(typeof(PropertyType), BuffBean.t_property_id))
        {
            propertyType = (PropertyType)BuffBean.t_property_id;
            if (propertyType != PropertyType.Hp && propertyType != PropertyType.Mp)
                Logger.err("HotDotBuff不应该处理除HP，MP以外的属性" + TemplateId);
        }
        else
        {
            Logger.err("PropertyBuff属性参数错误:" + BuffBean.t_property_id);
        }
    }


    public override void ApplyEffect(bool fromTurnStart)
    {
        base.ApplyEffect(fromTurnStart);
        if (Owner != null)
        {
            LNumber oldVal = 0;
            if(isBaseProperty)
                oldVal = Owner.getBaseProperty(propertyType);
            else
                oldVal = Owner.getProperty(propertyType);

            resultVal = oldVal * perValue + intValue;
            Owner.PrintProperty("F=>");
            Owner.ChangeProperty(propertyType, (int)SubOrAdd * resultVal);
            //Logger.log("HP_MP 生效了: " + propertyType.ToString()  + ":"+ resultVal);
            Logger.log("==>" + TemplateId + "Buff生效了" + propertyType.ToString() + "_perVal:" + perValue + "_intVal:" + intValue + "_resultVal:" + resultVal);
            Owner.PrintProperty("A=>");
            if (resultVal > 0)
            {
                //扣血回怒气
                if (propertyType == PropertyType.Hp && SubOrAdd == SUB_ADD.Sub)
                {
                    LNumber hpBase = Owner.getBaseProperty(PropertyType.Hp);
                    LNumber per = resultVal / hpBase;
                    LNumber mpVal = BattleParam.PercentHurtGetMp * per * 100;
                    //回复怒气
                    Owner.ChangeProperty(PropertyType.Mp, mpVal);

                    ShowChangeProperty showMp = new ShowChangeProperty();
                    showMp.value = mpVal;
                    showMp.actorId = Owner.getActorId();
                    showMp.propertyType = PropertyType.Mp;
                    if (fromTurnStart)
                        showMp.timeNode = TimeNode.TurnStart;
                    else
                        showMp.timeNode = TimeNode.Buffkeyframe;

                    //回合开始的buff直接表现
                    if (fromTurnStart)
                        showMp.Show();
                    else
                        ViewUtils.Singleton.AddShow(showMp);
                }

                //扣血 加血 表现
                ShowChangeProperty show = new ShowChangeProperty();
                show.value = (int)SubOrAdd * resultVal;
                show.actorId = Owner.getActorId();
                show.propertyType = propertyType;
                if (fromTurnStart)
                    show.timeNode = TimeNode.TurnStart;
                else
                    show.timeNode = TimeNode.Buffkeyframe;
                //回合开始的buff直接表现
                if (fromTurnStart)
                    show.Show();
                else
                    ViewUtils.Singleton.AddShow(show);

                //如果有扣血致死
                //如果是每回合开始，则立即表现
                //如果是添加buff时立即生效，则等到添加buff时再表现
                if (Owner.isDead())
                {
                    ActorTurnStatus status = FightManager.Singleton.Grid.Get(Owner.getCamp(), Owner.GridId);
                    if (status != null)
                        status.IsActuallyDead = true;

                    ShowDead dead = new ShowDead();
                    dead.actorId = Owner.getActorId();
                    if (fromTurnStart)
                        dead.timeNode = TimeNode.TurnStart;
                    else
                        dead.timeNode = TimeNode.SkillCmp;
                    //回合开始的buff直接表现
                    if (fromTurnStart)
                        dead.Show();
                    else
                        ViewUtils.Singleton.AddShow(dead);
                }

            }
        }
    }

    public override void OnRemove(bool fromTurnStart)
    {
        //do nothing
    }

}