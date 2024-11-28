using Data.Beans;

public class RebornBuff : BaseBuff
{

    protected PropertyType propertyType;      //由读表获得（因为表现规则不一样）

    protected LNumber perValue;                     //复活概率百分比值

    private LNumber HpPer;                             //复活继承生命值

    private LNumber MpPer;                             //复活继承怒气值

    //3 = 复活时，是否再次附加此buff；（1 = 是，0 = 否）
    private bool applyAgain;


    public override void ParseParam(int effectId, TwoParam<LNumber, LNumber> param)
    {
        base.ParseParam(effectId, param);
        if (EffectBean != null && param != null)
        {
            int skillLevel = (int)param.value1;
            perValue = EffectBean.t_rate_base + skillLevel * EffectBean.t_rate_grow;            
            HpPer = GTools.ScaleInt2LNumber(EffectBean.t_param1_base + skillLevel * EffectBean.t_param1_grow); 
            MpPer = GTools.ScaleInt2LNumber(EffectBean.t_param2_base + skillLevel * EffectBean.t_param2_grow);
            if (EffectBean.t_param3 == 1)
                applyAgain = true;
            else
                applyAgain = false;
        }
        if (BuffBean != null)
        {
            if (System.Enum.IsDefined(typeof(PropertyType), BuffBean.t_property_id))
            {
                propertyType = (PropertyType)BuffBean.t_property_id;
                if (propertyType != PropertyType.FuHuoLv)
                    Logger.err("PropertyBuff不应该处理非复活的属性");
            }
            else
            {
                Logger.err("PropertyBuff属性参数错误:" + BuffBean.t_property_id);
            }
        }
    }

    public override void ApplyEffect(bool fromTurnStart)
    {
        base.ApplyEffect(fromTurnStart);
        if (!IsEffect)
        {
            IsEffect = true;
            Owner.ChangeProperty(propertyType, perValue);
            Logger.log("fuhuo buff effect: " + propertyType.ToString() + "_resultVal:" + Owner.getProperty(propertyType));
        }
    }

    public override void OnRemove(bool fromTurnStart)
    {
        base.OnRemove(fromTurnStart);
        Owner.ChangeProperty(propertyType, -perValue);
    }

    public bool WillReborn()
    {
        return AttackUtils.WillOccurL(perValue);
    }

    public void DoReborn()
    {
        LNumber hpbase = Owner.getBaseProperty(PropertyType.Hp);
        Owner.SetProperty(PropertyType.Hp, hpbase * HpPer);
        LNumber mpbase = Owner.getBaseProperty(PropertyType.Mp);
        Owner.SetProperty(PropertyType.Mp, mpbase * MpPer);
        Logger.log("复活血量：" + Owner.getProperty(PropertyType.Hp));
        Logger.log("复活蓝量：" + Owner.getProperty(PropertyType.Mp));
        //复活直接表现
        Owner.ViewPropertyMgr.SetProperty(PropertyType.Hp, Owner.getProperty(PropertyType.Hp));
        Owner.ViewPropertyMgr.SetProperty(PropertyType.Mp, Owner.getProperty(PropertyType.Mp));
        Owner.IsActuallyDead = false;
        if (applyAgain)
        {
            triggerType = TriggerEnum.OnTurnStart;
            //Owner.BuffMgr.Add(this);
        }
        else
        {
            Owner.BuffMgr.Remove(this, true);
        }
    }

}
