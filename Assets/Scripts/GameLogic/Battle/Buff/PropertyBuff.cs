/// <summary>
/// 除去HP，MP以外的buff（非持续性）
/// </summary>
public class PropertyBuff : BaseBuff
{
    protected PropertyType propertyType;      //由读表获得（因为表现规则不一样）

    protected LNumber perValue;               //百分比值

    protected LNumber intValue;                //整型值

    protected LNumber resultVal;               //最终影响值
  
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
        if (param != null)
        {
            perValue = param.value1;
            intValue = param.value2;
        }
        if (BuffBean != null)
        {
            if (System.Enum.IsDefined(typeof(PropertyType), BuffBean.t_property_id))
            {
                propertyType = (PropertyType)BuffBean.t_property_id;
                if (propertyType == PropertyType.Hp || propertyType == PropertyType.Mp)
                    Logger.err("PropertyBuff不应该处理HP，MP相关属性");
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
            //LNumber oldVal = Owner.getBaseProperty(propertyType);
            //resultVal = oldVal * perValue + intValue;
            resultVal = perValue + intValue;
            Owner.PrintProperty("F=>");
            Owner.ChangeProperty(propertyType, (int)SubOrAdd * resultVal);
            Logger.log("==>effctId:" + EffectBean.t_id + "_buffId:" + TemplateId + "Buff生效了" + propertyType.ToString() + "_perVal:" + perValue + "_intVal:" + intValue + "_resultVal:" + resultVal);
            Owner.PrintProperty("B=>");
        }
    }

    public override void OnRemove(bool fromTurnStart)
    {
        base.OnRemove(fromTurnStart);
        SUB_ADD subAdd = SUB_ADD.Add;
        if (SubOrAdd == SUB_ADD.Add)
            subAdd = SUB_ADD.Sub;
        else
            subAdd = SUB_ADD.Add;
        Owner.ChangeProperty(propertyType, (int)subAdd * resultVal);
    }

}



